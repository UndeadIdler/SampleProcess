using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
 using DataAccess;
using WebApp.Components;
using ExtendWebControls;
using DAL;

public partial class AccessSample4 : System.Web.UI.Page
{
    private string strSelectedId//样品单号
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private DataTable dt_Sample//样品列表
    {
        get { return (DataTable)ViewState["dt_Sample"]; }
        set { ViewState["dt_Sample"] = value; }
    }
    private DataTable dt_analysis//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis"]; }
        set { ViewState["dt_analysis"] = value; }
    }
    Hashtable allitemlist = new Hashtable();
    private int samplestatus//样品单状态
    {
        get { return (int)ViewState["samplestatus"]; }
        set { ViewState["samplestatus"] = value; }
    }
    //private static Hashtable itemlist1=new Hashtable();
    //private static Hashtable itemlist2 = new Hashtable();
    //private static Hashtable itemlist3 = new Hashtable();
    //private static Hashtable itemlist4 = new Hashtable();
    //private static Hashtable itemlist5 = new Hashtable();
    private int flag
    {
        get { return (int)ViewState["flag"]; }
        set { ViewState["flag"] = value; }
    }
  private static bool controlflag=false;
    private string strReportId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportId"]; }
        set { ViewState["strReportId"] = value; }
    }
    private string strReportName//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportName"]; }
        set { ViewState["strReportName"] = value; }
    }
    private string strAnalysisId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strAnalysisId"]; }
        set { ViewState["strAnalysisId"] = value; }
    }

    private string strItemlist//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strItemlist"]; }
        set { ViewState["strItemlist"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strItemlist = "";
              txt_CreateDate.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_SampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_AccessSampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_AccessSampleTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_SampleTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            DAl.Sample sampleobj = new DAl.Sample();       
      DataTable dttype = sampleobj.GetMode("", "rwtype", "");
      DataRow dr= dttype.NewRow();
      dr["name"] = "所有";
      dr["code"] = "-1";
      dttype.Rows.Add(dr);
      dttype.AcceptChanges();
      drop_QueryRWtype.DataSource = dttype;
      drop_QueryRWtype.DataTextField = "name";
      drop_QueryRWtype.DataValueField = "code";
      drop_QueryRWtype.DataBind();
      drop_QueryRWtype.SelectedValue = "-1";


      DataTable dtpurpose = sampleobj.GetPurpose("", int.Parse(drop_QueryRWtype.SelectedValue.ToString()));
      drop_QueryProjectType.DataSource = dtpurpose;
      drop_QueryProjectType.DataTextField = "ItemName";
      drop_QueryProjectType.DataValueField = "ItemID";
      drop_QueryProjectType.DataBind();
      ListItem li = new ListItem("所有", "-1");
      drop_QueryProjectType.Items.Add(li);
      drop_QueryProjectType.SelectedIndex = drop_QueryProjectType.Items.Count - 1;
     
             Query();
            SetButton();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>样品接收</b></font>";
        }
    }
    #region 报告相关
    protected void SetButton()
    {
        controlflag=MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString());
        if (!controlflag)
        {
            //btn_Add.Enabled = false;
            //btn_OK.Enabled = false;
            btn_AddSample.Enabled = false;
            //btn_Save.Enabled = false;
            btn_OKSample.Enabled = false;
            //btn_item.Enabled = false;
            //for (int i = 0; i < grdvw_List.Rows.Count; i++)
            //{
            //  ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
            //    if(btn!=null)
            //  btn.Visible = false;

            //}
        }
        else
        {
            //btn_Add.Enabled = true;
            //btn_OK.Enabled = true;
            //btn_Save.Enabled = true;
            btn_AddSample.Enabled = true;
           // btn_OKSample.Enabled = true;
           // btn_item.Enabled = true;
            //for (int i = 0; i < grdvw_List.Rows.Count; i++)
            //{
            //    ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
            //    if(btn!=null)
            //    btn.Visible = true;
            //}
        }
    }

    private void initial(string id)
    {
        DAl.Sample sampleobj = new DAl.Sample();
      DataTable dttype = sampleobj.GetMode("", "rwtype", "");
      drop_rwtype.DataSource = dttype;
        drop_rwtype.DataTextField = "name";
        drop_rwtype.DataValueField = "code";
        drop_rwtype.DataBind();
        drop_rwtype.SelectedValue = id;
        if (drop_rwtype.SelectedIndex == 0)
        {
            lbl_reportNO.Text = "报告标识";
            lbl_AccessTime.Text = "接到时间";
           

            DataTable dtmode = sampleobj.GetMode("", "mode", "1");
            drop_mode.DataSource = dtmode;
            drop_mode.DataTextField = "name";
            drop_mode.DataValueField = "code";
            drop_mode.DataBind();

          
            DataTable dtpurpose = sampleobj.GetPurpose("",0);
            drop_ItemList.DataSource = dtpurpose;
            drop_ItemList.DataTextField = "ItemName";
            drop_ItemList.DataValueField = "ItemID";
            drop_ItemList.DataBind();
        }
        else
        {
            lbl_reportNO.Text = "报告标识";
            lbl_AccessTime.Text = "委托时间";
          

            DataTable dtmode = sampleobj.GetMode("", "mode", "");
            drop_mode.DataSource = dtmode;
            drop_mode.DataTextField = "name";
            drop_mode.DataValueField = "code";
            drop_mode.DataBind();
            DataTable dtpurpose = sampleobj.GetPurpose("",1);
            drop_ItemList.DataSource = dtpurpose;
            drop_ItemList.DataTextField = "ItemName";
            drop_ItemList.DataValueField = "ItemID";
            drop_ItemList.DataBind();
        }


       
        txt_lxtel.Text = "";
        txt_lxman.Text = "";
        txt_lxemail.Text = "";
        txt_address.Text = "";
        txt_ReportID.Text = "";
        txt_CreateDate.Text = "";
        txt_Projectname.Text = "";
        txt_wtdepart.Text = "";
        txt_xmfzr.Text = "";
        drop_urgent.Text = "";



    }
    protected void drop_rwtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAl.Sample sampleobj = new DAl.Sample();
        if (drop_rwtype.SelectedIndex == 0)
        {
            lbl_reportNO.Text = "报告标识";
            lbl_AccessTime.Text = "接到时间";
            //panel_wtdw.Visible = false;
            DataTable dtmode = sampleobj.GetMode("", "mode", "1");
            drop_mode.DataSource = dtmode;
            drop_mode.DataTextField = "name";
            drop_mode.DataValueField = "code";
            drop_mode.DataBind();
            DataTable dtpurpose = sampleobj.GetPurpose("",0);
            drop_ItemList.DataSource = dtpurpose;
            drop_ItemList.DataTextField = "ItemName";
            drop_ItemList.DataValueField = "ItemID";
            drop_ItemList.DataBind();
        }
        else
        {
            lbl_reportNO.Text = "报告标识";
            lbl_AccessTime.Text = "委托时间";
           // panel_wtdw.Visible = true;
            DataTable dtmode = sampleobj.GetMode("", "mode", "");
            drop_mode.DataSource = dtmode;
            drop_mode.DataTextField = "name";
            drop_mode.DataValueField = "code";
            drop_mode.DataBind();
            DataTable dtpurpose = sampleobj.GetPurpose("", 1);
            drop_ItemList.DataSource = dtpurpose;
            drop_ItemList.DataTextField = "ItemName";
            drop_ItemList.DataValueField = "ItemID";
            drop_ItemList.DataBind();
        }
    }
    //protected void SetButton()
    //{
    //    if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
    //    {
    //        btn_Add.Enabled = false;
    //        btn_OK.Enabled = false;

    //        btn_Save.Enabled = false;

    //    }
    //    else
    //    {
    //        btn_Add.Enabled = true;
    //        btn_OK.Enabled = true;
    //        btn_Save.Enabled = true;
    //        // btn_AddSample.Enabled = true;
    //        //  btn_OKSample.Enabled = true;
    //        // btn_item.Enabled = true;
    //        //for (int i = 0; i < grdvw_List.Rows.Count; i++)
    //        //{
    //        //    ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
    //        //    if(btn!=null)
    //        //    btn.Visible = true;
    //        //}
    //    }
    //}
    private void Query()
    {
        //strSelectedId=txt_samplequery.Text;
        string strSample = "";
        string strDate = "";
        if (txt_samplequery.Text != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text + "%'";

        if (drop_QueryRWtype.SelectedValue.ToString()!="-1")
            strSample += "and rwclass='" + drop_QueryRWtype.SelectedValue.ToString() + "'";
        if (drop_QueryProjectType.SelectedValue.ToString() != "-1")
            strSample += "and ItemType='" + drop_QueryProjectType.SelectedValue.ToString() + "'";
        if (txt_wtQuery.Text.Trim()!= "")
            strSample += "and wtdepart like'%" + txt_wtQuery.Text.Trim() + "%'";
        
        if (txt_QueryTime.Text != "")
            strDate = " and (year(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_M_ReporInfo.id,t_M_ReporInfo.ReportAccessDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,urgent 备注,t_M_ReporInfo.Ulevel,Projectname 项目名称,t_R_UserInfo.Name 项目负责人,rwclass,jcmethod,address,lxman,lxtel,lxemail,wtdepart,Green   from t_M_ReporInfo,t_M_ItemInfo,t_R_UserInfo where  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=1) and wetherscance=0  and chargeman=t_R_UserInfo.UserID  " + strSample + strDate + " order by t_M_ReporInfo.ReportAccessDate desc";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dc = new DataColumn("监测方式");
        ds.Tables[0].Columns.Add(dc);
        DataColumn dc0 = new DataColumn("任务类型");
        ds.Tables[0].Columns.Add(dc0);
        DataColumn dc1 = new DataColumn("委托单位");
        ds.Tables[0].Columns.Add(dc1);
        DataColumn dc11 = new DataColumn("是否走绿色通道");
        ds.Tables[0].Columns.Add(dc11);
        DAl.Sample getobj = new DAl.Sample();
        DataTable dtmode = getobj.GetMode("", "mode", "");
        DAl.Station get = new DAl.Station();
        DataTable dtstation = get.GetWTByName("");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急";
            else
                dr["紧急程度"] = "一般";
            if (dr["rwclass"].ToString() == "1")
                dr["任务类型"] = "委托监测";
            else
                dr["任务类型"] = "例行监测";
            DataRow[] drsel = dtmode.Select("code='" + dr["jcmethod"].ToString() + "'");
            if (drsel.Length == 1)
                dr["监测方式"] = drsel[0]["name"].ToString();

            else
                dr["监测方式"] = "";
            if (dr["Green"].ToString() == "1")
                dr["是否走绿色通道"] = "是";
            else
                dr["是否走绿色通道"] = "否";
           
            dr["委托单位"] = dr["wtdepart"].ToString();

        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
            int intColumnCount = grdvw_List.Rows[0].Cells.Count;
            grdvw_List.Rows[0].Cells.Clear();
            grdvw_List.Rows[0].Cells.Add(new TableCell());
            grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
        }
        ds.Dispose();

    }

    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string idtype = "0";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text != "&nbsp;")//任务类型
            idtype = grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim();
        // drop_rwtype.Items.FindByValue(grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim()).Selected = true;
        initial(idtype);
        string slevel = "";

       
            btn_OK.Visible = true;
       
            //btn_OK.Visible = false;
       // btn_Save.Visible = true;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();

        lbl_Type.Text = "样品接收";

        txt_CreateDate.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim();//报告创建日期

        ListItem choose = new ListItem("请选择", "-1");
       

       // panel_wtdw.Visible = drop_rwtype.SelectedValue == "1";

        if (grdvw_List.Rows[e.NewEditIndex].Cells[11].Text != "&nbsp;")//监测方式
            drop_mode.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[11].Text;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[12].Text != "&nbsp;")//地址
            txt_address.Text = grdvw_List.Rows[e.NewEditIndex].Cells[12].Text;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[13].Text != "&nbsp;")//联系人
            txt_lxman.Text = grdvw_List.Rows[e.NewEditIndex].Cells[13].Text;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[14].Text != "&nbsp;")//联系人手机
            txt_lxtel.Text = grdvw_List.Rows[e.NewEditIndex].Cells[14].Text;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[15].Text != "&nbsp;")//联系人邮箱
            txt_lxemail.Text = grdvw_List.Rows[e.NewEditIndex].Cells[15].Text;
        drop_urgent.Text = "";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[6].Text != "&nbsp;")//备注
            drop_urgent.Text = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text;

        if (grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim() != "")
        {
            slevel = grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim();
            drop_level.SelectedValue = slevel;
        }


        string itemtid = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        //drop_ItemList.Items.Add(choose);
       drop_ItemList.Items.FindByValue(itemtid).Selected = true;
       
            if (grdvw_List.Rows[e.NewEditIndex].Cells[21].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[21].Text.Trim() != "")
            {
                txt_wtdepart.Text = grdvw_List.Rows[e.NewEditIndex].Cells[21].Text.Trim();
            }
            if (grdvw_List.Rows[e.NewEditIndex].Cells[12].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[12].Text.Trim() != "")
            {
                txt_address.Text = grdvw_List.Rows[e.NewEditIndex].Cells[12].Text.Trim();
            }
            if (grdvw_List.Rows[e.NewEditIndex].Cells[13].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[13].Text.Trim() != "")
            {
                txt_lxman.Text = grdvw_List.Rows[e.NewEditIndex].Cells[13].Text.Trim();
            }
            if (grdvw_List.Rows[e.NewEditIndex].Cells[14].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[14].Text.Trim() != "")
            {
                txt_lxtel.Text = grdvw_List.Rows[e.NewEditIndex].Cells[14].Text.Trim();
            }
            if (grdvw_List.Rows[e.NewEditIndex].Cells[15].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[15].Text.Trim() != "")
            {
                txt_lxemail.Text = grdvw_List.Rows[e.NewEditIndex].Cells[15].Text.Trim();
            }
       
        if (grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "")
        {
            txt_xmfzr.Text = grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "")
        {
            txt_Projectname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[17].Text.Trim() == "1")
        {
            ck_green.Checked = true;
        }
        else
            ck_green.Checked = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();showAnalysis();hiddenDetailAnalysisAdd();", true);
        ReportSelectQuery();



    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "查看/编辑";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/images/Detail.gif";

            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;

            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strReportId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string[] deletelist = new string[3];
        string str = "select id from t_M_SampleInfor where ReportID= '" + strReportId + "'";
        strSql = "DELETE FROM t_M_MonitorItem ";
        string condition = "";
        int i = 0;
        DataSet ds = new MyDataOp(str).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == 0)
            {
                condition = " WHERE SampleID= '" + dr[0].ToString() + "'";
            }
            else
            {
                condition += " or SampleID= '" + dr[0].ToString() + "'";
            }
            i++;

        }



        strSql = "DELETE FROM t_M_SampleInfor WHERE ReportID= '" + strReportId + "'";
        deletelist.SetValue(strSql, 0);
        strSql = "DELETE FROM t_M_ReporInfo WHERE id= '" + strReportId + "'";
        deletelist.SetValue(strSql, 1);
        if (condition != "")
        {
            i = 3;
            //strSql = "DELETE FROM t_M_MonitorItem " + condition;
            //deletelist.SetValue(strSql, 2);
            strSql = "DELETE FROM t_MonitorItemDetail " + condition;
            deletelist.SetValue(strSql, 2);

        }
        else
            i = 2;

        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(i, deletelist))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + txt_ReportID.Text.Trim() + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
        }
        else
        {
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + txt_ReportID.Text.Trim() + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        Query();
    }
    #endregion

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        string strFlag = Verify();

        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
            return;
        }
        else
        {
            string compay = "0";
            string cyman = "0";

            //获取采样人，现场分析人
           // cyman = tbSampleMan.Text.Trim();
            DAl.User.Users userobj = new DAl.User.Users();
            Entity.User.Users user = null;//= new Entity.User.Users();
            user = userobj.GetUsers(txt_xmfzr.Text.Trim());
            if (user != null)
            {
                cyman = user.ID.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在项目负责人/报告编制人，请核实！');", true);
                return;
            }
            Entity.AccessReport entity = new Entity.AccessReport();
            entity.classID = int.Parse(drop_rwtype.SelectedValue.ToString());//任务类型
            if (entity.classID == 1)//委托任务
            {
                DAl.Station stationobj = new DAl.Station();
                DataTable dtstation = stationobj.GetWTByName(txt_wtdepart.Text.Trim());

                if (dtstation.Rows.Count > 0)
                {
                    compay = dtstation.Rows[0]["id"].ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该委托单位，请核实！');", true);
                    return;
                }
                entity.WTMan = compay;//委托单位
                entity.lxEmail = txt_lxemail.Text.Trim();
                entity.lxMan = txt_lxman.Text.Trim(); ;
                entity.lxtel = txt_lxtel.Text.Trim(); ;
                entity.address = txt_address.Text.Trim();
            }
            entity.CreateDate = DateTime.Now;//创建时间
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
            entity.WTDate = DateTime.Parse(txt_CreateDate.Text.Trim());//委托日期，任务接收日期
            entity.chargeman = cyman;//项目负责人
            entity.level = drop_level.SelectedValue.ToString();//紧急程度
            entity.Mode = drop_mode.SelectedValue.ToString();//监测方式
            entity.Remark = drop_urgent.Text.Trim();//备注
            entity.WTNO = txt_ReportID.Text.Trim();//委托协议编码，报告标识
            entity.ProjectName = txt_Projectname.Text.Trim();//项目名称
            entity.TypeID = int.Parse(drop_ItemList.SelectedValue.ToString().Trim());
            DAl.Report reportobj = new DAl.Report();
            entity.StatusID = 1;
            entity.ID = int.Parse(strReportId);
            if (reportobj.Upate(entity) == 1)
            {
                WebApp.Components.Log.SaveLog("创建样品原单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);


            }
            else
            {
                WebApp.Components.Log.SaveLog("创建样品原单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('数据添保存失败！')", true);

            }

        }
        Query();
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        txt_ReportID.Text = "";
        Query();
    }



    protected void btn_Add_Click(object sender, EventArgs e)
    {
        initial("0");
        lbl_Type.Text = "添加";
        //btn_OK.Text = "确定";
       // btn_Save.Visible = false;
        txt_ReportID.Text = "";
        //MyStaVoid.BindList("ClientName", "id", "select * from t_M_ClientInfo order by id", DropList_client);
        ListItem choose = new ListItem("请选择", "-1");
        //DropList_client.Items.Add(choose);
        //DropList_client.Items.FindByValue("-1").Selected = true;

        strReportId = "0";
        strReportName = "";
        txt_CreateDate.Text = "";
        txt_Projectname.Text = "";
        txt_wtdepart.Text = "";

        //MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);

        //drop_ItemList.Items.Add(choose);
        //drop_ItemList.Items.FindByValue("-1").Selected = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        drop_urgent.Text = "";
        // btn_Save.Visible = false;

        Query();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();

        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
            // return;


        }
        else
        {
            string compay = "0";
            string cyman = "0";

            //获取采样人，现场分析人
            DAl.User.Users userobj = new DAl.User.Users();
            Entity.User.Users user = null;//= new Entity.User.Users();
            user = userobj.GetUsers(txt_xmfzr.Text.Trim());
            if (user != null)
            {
                cyman = user.UserID.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在项目负责人/报告编制人，请核实！');", true);
                return;
            }
            Entity.AccessReport entity = new Entity.AccessReport();
            entity.classID = int.Parse(drop_rwtype.SelectedValue.ToString());//任务类型
            //if (entity.classID == 1)//委托任务
            //{
            //DAl.Station stationobj = new DAl.Station();
            //DataTable dtstation = stationobj.GetWTByName(txt_wtdepart.Text.Trim());

            //if (dtstation.Rows.Count > 0)
            //{
            compay = txt_wtdepart.Text.Trim();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该委托单位，请核实！');", true);
            //    return;
            //}
            entity.WTMan = compay;//委托单位
            entity.lxEmail = txt_lxemail.Text.Trim();
            entity.lxMan = txt_lxman.Text.Trim(); ;
            entity.lxtel = txt_lxtel.Text.Trim(); ;
            entity.address = txt_address.Text.Trim();
            //}
            entity.CreateDate = DateTime.Now;//创建时间
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
            entity.WTDate = DateTime.Parse(txt_CreateDate.Text.Trim());//委托日期，任务接收日期
            entity.chargeman = cyman;//项目负责人
            entity.level = drop_level.SelectedValue.ToString();//紧急程度
            entity.Mode = drop_mode.SelectedValue.ToString();//监测方式
            entity.Remark = drop_urgent.Text.Trim();//备注
            entity.WTNO = txt_ReportID.Text.Trim();//委托协议编码，报告标识
            entity.ProjectName = drop_ItemList.SelectedItem.Text.Trim(); ;//项目名称
            entity.TypeID = int.Parse(drop_ItemList.SelectedValue.ToString().Trim());
            DAl.Report reportobj = new DAl.Report();
            entity.StatusID =1;

            if (ck_green.Checked)
                entity.Green = 1;
            else
                entity.Green = 0;
                entity.ID = int.Parse(strReportId);
                if (reportobj.Upate(entity) == 1)
                {
                    WebApp.Components.Log.SaveLog("任务单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "alert('数据保存成功！')", true);
                    Query();
                }
                else
                {
                    WebApp.Components.Log.SaveLog("任务单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('数据添保存失败！')", true);

                }
          

        }
    }
    private string Verify()
    {
        string strErrorInfo = "";

        if (drop_ItemList.SelectedValue == "-1")
            strErrorInfo += "请选择项目类型！\\n";
        else if (txt_CreateDate.Text == "")
            strErrorInfo += "请填写时间！\\n";

        else if (txt_Projectname.Text.Trim() == "-1")
            strErrorInfo += "请选择项目名称！\\n";
        else if (txt_wtdepart.Text.Trim() == "-1")
            strErrorInfo += "请选择委托单位！\\n";
        //else if (DropList_client.SelectedValue == "-1")
        //    strErrorInfo += "请选择区域！\\n";

        return strErrorInfo;
    }



    protected void txt_wtdepart_TextChanged(object sender, EventArgs e)
    {
        DAl.Station StationObj = new DAl.Station();
        DataTable dt = StationObj.GetWTByName(txt_wtdepart.Text.Trim());
        if (dt != null)
        {
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["单位详细地址"].ToString() != "&nbsp;")
                    txt_address.Text = dt.Rows[0]["单位详细地址"].ToString();
                if (dt.Rows[0]["环保负责人"].ToString() != "&nbsp;")
                    txt_lxman.Text = dt.Rows[0]["环保负责人"].ToString();
                if (dt.Rows[0]["mobile3"].ToString() != "&nbsp;")
                    txt_lxtel.Text = dt.Rows[0]["mobile3"].ToString();
                if (dt.Rows[0]["电子邮箱"].ToString() != "&nbsp;")
                    txt_lxemail.Text = dt.Rows[0]["电子邮箱"].ToString();
            }
        }
    }



    #endregion
   
    /// <summary>
    /// 转换字符为m个字符长，不够前面补零
    /// </summary>
    /// <param name="strCode">待转换字符</param>
    /// <param name="m">结果字符长度</param>
    /// <returns>转换结果</returns>
    public static string CodeConvert(string strCode, int m)
    {
        string s = strCode;
        for (int i = 0; i < m; i++)
        {
            s = "0" + s;
        }
        s = s.Substring(s.Length - m, m);
        return s;
    }
   
   
   
    #region 样品单列表

    protected void btn_AddSample_Click(object sender, EventArgs e)
    {
        lbl_SampleDo.Text = "添加样品";
        strItemlist = "";
        DropList_SampleType.Text = "";
        strSelectedId ="0";
        //hid_status.Value = "0";
        txt_SampleTime.Text ="";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);
        ReportSelectQuery();
        InitialTable(0);
        //btn_OKSample.Visible = true;
        btn_Save.Visible = true;
        txt_AccessSampleTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txt_SampleTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
         

    }
  
    private void newDt()
    { 
        dt_analysis=null;
        dt_analysis=new DataTable();
        DataColumn dc6 = new DataColumn("ID");
        DataColumn dc0 = new DataColumn("现场分析");
        DataColumn dc1 = new DataColumn("分析项目");
        DataColumn dc2 = new DataColumn("ItemID");
        DataColumn dc3 = new DataColumn("分析值");
        DataColumn dc70 = new DataColumn("分析方法");
        DataColumn dc4 = new DataColumn("Remark");
        DataColumn dc5 = new DataColumn("fxDanID");
        DataColumn dc7 = new DataColumn("showid");
            dt_analysis.Columns.Add((dc6));
            dt_analysis.Columns.Add((dc0));
            dt_analysis.Columns.Add((dc1));
            dt_analysis.Columns.Add((dc2));
            dt_analysis.Columns.Add((dc3));
            dt_analysis.Columns.Add((dc70));
            dt_analysis.Columns.Add((dc4));
            dt_analysis.Columns.Add((dc5));
            dt_analysis.Columns.Add((dc7)); 
           
    }
    private void InitialTable(int flag)
    {
        newDt();
       
        //hid_status.Value = "";

        txt_SampleSource.Text = "";
        txt_AccessSampleTime.Text = "";
        txt_SampleTime.Text = "";
       // DropList_SampleType.Text = "";
        //tbTestTime.Text = "";
        tbSampleMan.Text = "";
        //tbTestPlace.Text = "";
        //txt_fxman.Text = "";
        //txt_fxtime.Text = "";
        //txt_jhman.Text = "";
        //txt_jhtime.Text = "";
        dt_Sample.Clear();

        for (int i = 0; i < flag; i++)
        {
            DataRow dr = dt_Sample.NewRow();
            dt_Sample.Rows.Add(dr);
            dt_Sample.AcceptChanges();

        }
        RepeaterSample.DataSource = dt_Sample;
        RepeaterSample.DataBind();
        for (int i = 0; i < flag; i++)
        {
            TextBox txt_SampleID = RepeaterSample.Items[i].FindControl("txt_SampleID") as TextBox;
            Label lbl_SampleID = RepeaterSample.Items[i].FindControl("lbl_SampleID") as Label;
            if (txt_SampleID.Text != "&nbsp;" && txt_SampleID.Text != "")
            {
                txt_SampleID.Visible = true;
                lbl_SampleID.Visible = true;
            }
        }
    }
    

    protected void grdvw_ReportDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList cbl = (CheckBoxList)e.Row.FindControl("cbl_item");

            if (cbl != null)
            {
               // BindCheckBoxList(cbl, e);
            }
        }
    }
    //检测样品单是否允许修改
    protected bool CheckSample(string sampleid)
    {
        bool ret = false;
        string checkstr = "select * from t_M_SampleInfor where SampleID='"+sampleid+"' and StatusID<3";
        DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
        if(dscheck!=null&& dscheck.Tables.Count>0)
            if (dscheck.Tables[0].Rows.Count > 0)
            {
                ret = true;
            }
        return ret;
    }

  
    protected void grdvw_ReportDetail_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        lbl_SampleDo.Text = "样品详单";
        DropList_SampleType.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[8].Text.Trim();
        InitialTable(1);
        btn_Save.Visible = true;
          
           strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
           if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[3].Text.Trim() != "&nbsp;")
               txt_SampleSource.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[3].Text.Trim();
           else
               txt_SampleSource.Text = "";
           txt_SampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
           txt_AccessSampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[6].Text.Trim();
           tbSampleMan.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[15].Text.Trim();

       #region 获取样品详细信息
        //样品编号
         TextBox txt_SampleID = RepeaterSample.Items[0].FindControl("txt_SampleID") as TextBox;
         txt_SampleID.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
         Label lbl_SampleID = RepeaterSample.Items[0].FindControl("lbl_SampleID") as Label;
         if (txt_SampleID.Text != "&nbsp;" && txt_SampleID.Text != "")
         {
             txt_SampleID.Visible = true;
             lbl_SampleID.Visible = true;
         }
        //采样点
         TextBox txt_SampleAddress = RepeaterSample.Items[0].FindControl("txt_SampleAddress") as TextBox;
         if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[5].Text.Trim()!="&nbsp;")
           txt_SampleAddress.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        //样品列表
           TextBox txt_Item = RepeaterSample.Items[0].FindControl("txt_Item") as TextBox;
           if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[18].Text.Trim() != "&nbsp;")
         txt_Item.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[18].Text.Trim();
           HiddenField hid_Item = RepeaterSample.Items[0].FindControl("hid_Item") as HiddenField;
           hid_Item.Value = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[19].Text.Trim();
        //样品性质
         TextBox txt_xz = RepeaterSample.Items[0].FindControl("txt_xz") as TextBox;
         if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[9].Text.Trim() != "&nbsp;")
           txt_xz.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[9].Text.Trim();
        //样品数量
           TextBox txt_num = RepeaterSample.Items[0].FindControl("txt_num") as TextBox;
           txt_num.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[14].Text.Trim();
           DAl.Sample sampleobj = new DAl.Sample();
           Panel panel_js = RepeaterSample.Items[0].FindControl("panel_js") as Panel;

           TextBox txt_s = panel_js.FindControl("txt_s") as TextBox;
           if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[16].Text.Trim() != "&nbsp;" && grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[16].Text.Trim()!="1900-01-01")
               txt_s.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[16].Text.Trim();
           TextBox txt_e = panel_js.FindControl("txt_e") as TextBox;
           if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[17].Text.Trim() != "&nbsp;" && grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[17].Text.Trim() != "1900-01-01")
               txt_e.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
          
            //获取样品单监测项信息
           GridView grv_Item = RepeaterSample.Items[0].FindControl("grv_Item") as GridView;
            DataTable dttemp = dt_analysis.Clone();
            DAl.Sample temp = new DAl.Sample();
            dttemp = temp.GetSampleDetail(txt_SampleID.Text);
            if (dttemp!=null&&dttemp.Rows.Count > 0)
            {
                dttemp.TableName = "list";
                DataView dv = new DataView();
                dv.Table = dttemp;

                dv.Sort = "showid";
               grv_Item.DataSource = dv;
                grv_Item.DataBind();
                Panel panel_Item = RepeaterSample.Items[0].FindControl("panel_Item") as Panel;
                  Panel panel_cg= RepeaterSample.Items[0].FindControl("panel_cg") as Panel;
                CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
                DataBindAll(cbl, txt_Item.Text,1);
                Panel panel_other = RepeaterSample.Items[0].FindControl("panel_other") as Panel;

                CheckBoxList cbl_other = panel_other.FindControl("cb_other") as CheckBoxList;
                DataBindAll(cbl_other, txt_Item.Text, 0);
            }
            else
            {
                grv_Item.DataSource = null;
                grv_Item.DataBind();
            }
            CheckBox ck_xcflag = RepeaterSample.Items[0].FindControl("ck_xcflag") as CheckBox;
            if (ck_xcflag.Checked)
            {
                ck_xcflag.Checked = true;
                grv_Item.Visible = true;
            }
            else
            {
                ck_xcflag.Checked = false;
                grv_Item.Visible = false;
            }
           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);
           ReportSelectQuery();
           ////根据样品状态来确定是否可以修改
           //if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[10].Text.Trim() == "0")
           //{
           //   btn_OKSample.Visible = true;
           //    //btn_Save.Visible = true;
           //}

           //else
           //{
           //   btn_OKSample.Visible = false;
           //   // btn_Save.Visible = false;
           //}
          
#endregion
    }
    protected void grdvw_ReportDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ReportDetail.PageIndex = e.NewPageIndex;
        ReportSelectQuery();
    }
    protected void grdvw_ReportDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableCell headerset = new TableCell();
            headerset.Text = "查看/编辑";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

            //TableCell headerset1 = new TableCell();
            //headerset1.Text = "提交";
            //headerset1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerset1.Width = 60;
            //e.Row.Cells.Add(headerset1);
            TableCell headerDel = new TableCell();
            if (controlflag)
            {
                headerDel.Text = "删除";
                headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                headerDel.Width = 30;
                e.Row.Cells.Add(headerDel);

            }
           
           
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
            ibtnDetail.CommandName = "Select";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            ////手动添加详细和删除按钮
            //TableCell tabcDetail1 = new TableCell();
            //tabcDetail1.Width = 60;
            //tabcDetail1.Style.Add("text-align", "center");
            //ImageButton ibtnDetail1 = new ImageButton();
            //ibtnDetail1.ImageUrl = "~/images/Detail.gif";
            //ibtnDetail1.CommandName = "Update";
            //tabcDetail1.Controls.Add(ibtnDetail1);
            //e.Row.Cells.Add(tabcDetail1);
            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            if (controlflag)
            {
                tabcDel.Controls.Add(ibtnDel);
                e.Row.Cells.Add(tabcDel);

            }
            else
                ibtnDel.Enabled = false;
           


        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;

            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[19].Visible = false;
        }
    }
    protected void btn_CancelReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();", true);
        Query();

    }
   
    /// <summary>
    /// 删除样品事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvw_ReportDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //if (grdvw_ReportDetail.Rows[e.RowIndex].Cells[10].Text.Trim() == "0")
        //{
            strSelectedId = grdvw_ReportDetail.Rows[e.RowIndex].Cells[1].Text;
        string typeid=grdvw_ReportDetail.Rows[e.RowIndex].Cells[7].Text;
         DateTime SampleDate=DateTime.Parse(grdvw_ReportDetail.Rows[e.RowIndex].Cells[6].Text);
         string Sampleid = grdvw_ReportDetail.Rows[e.RowIndex].Cells[2].Text.Trim();
            string strSql;
            string[] deletelist = new string[3];
            strSql = "DELETE FROM t_M_SampleInfor WHERE id= '" + strSelectedId + "'";
            int i = 0;
            deletelist.SetValue(strSql,i++);
            strSql = "DELETE FROM t_MonitorItemDetail WHERE SampleID= '" + Sampleid + "'";
            deletelist.SetValue(strSql, i++);
       DAl.Sample sampleobj=new DAl.Sample();
        string className= sampleobj.getType(typeid);
       switch (typeid)
       { case"7" :
       case "8":
       case "9": className=Sampleid.Substring(0, 2);
           break;
       default: className = Sampleid.Substring(0, 1);
               break;
       }
         int NO=1;
        if(className=="降")
             NO=1;
        else
            NO = int.Parse(Sampleid.Substring(Sampleid.Length - 3, 3));
            string checkstr = "select * from t_最新编号 where Date='"+SampleDate.ToString("yyMMdd")+"' and BhType='"+className+"' and LastBh='"+NO+"' and LastBh>0";
            DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
            if (dscheck.Tables[0].Rows.Count != 0)
            {
                strSql = "update t_最新编号  set LastBh=LastBh-1 where Date='" + SampleDate.ToString("yyMMdd") + "' and BhType='" + className + "'";
                deletelist.SetValue(strSql, i++);
            }
            MyDataOp mdo = new MyDataOp(strSql);
            if (mdo.DoTran(i, deletelist))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
                WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + grdvw_ReportDetail.Rows[e.RowIndex].Cells[5].Text + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            }
            else
            {
                WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + grdvw_ReportDetail.Rows[e.RowIndex].Cells[5].Text + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
            }
        //}
        //else { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('样品已提交分析，无法删除！')", true); }
        ReportSelectQuery();

    }
    //根据企业加载分析项目
    private void GetItem()
    {
       
        strItemlist = "";
        string strsql = "select t_M_ANItemInf.id, AIName 分析项目, t_hyItem.itemid ItemID,t_hyItem.fw 标准 from t_hyItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_hyItem.itemid inner join  t_CompabyBZ on t_CompabyBZ.bzid=t_hyItem.pid inner join t_委托单位 on t_委托单位.id=t_CompabyBZ.qyid inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=wrwtype where 单位全称='" + txt_SampleSource.Text.Trim() + "' and t_CompabyBZ.flag=0 and ClassName='" + DropList_SampleType.Text.Trim() + "' ";
        DataSet ds = new MyDataOp(strsql).CreateDataSet();


        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                //DataColumn dc = new DataColumn("ID");
                //ds.Tables[0].Columns.Add(dc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strItemlist+= dr["分析项目"].ToString() + ",";
                    }
                    //grv_Item.DataSource = ds;
                    //grv_Item.DataBind();

                }
            }
        }
    }
    private void GetItem2(int type)
    {
        string orderstr = "";
        if (type == 1)
            orderstr = " order by showid";
        strItemlist = "";
        string strsql = "select t_M_ANItemInf.id, AIName 分析项目, t_hyItem.itemid ItemID,t_hyItem.fw 标准 from t_hyItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_hyItem.itemid inner join  t_hyClassParam on t_hyClassParam.id=t_hyItem.pid inner join t_委托单位 on t_委托单位.行业类别=t_hyClassParam.hyid inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=wrwtype where 单位全称='" + txt_SampleSource.Text.Trim() + "'  and ClassName='" + DropList_SampleType.Text.Trim() + "'" + orderstr;
        DataSet ds = new MyDataOp(strsql).CreateDataSet();


        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                //DataColumn dc = new DataColumn("ID");
                //ds.Tables[0].Columns.Add(dc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strItemlist += dr["分析项目"].ToString() + ",";
                    }
                    //grv_Item.DataSource = ds;
                    //grv_Item.DataBind();

                }
            }
        }
    }
    //private void GetWSItem()
    //{
    //    strItemlist = "";
    //    string strsql = "select t_M_ANItemInf.id, AIName 分析项目, t_hyItem.itemid ItemID,t_hyItem.fw 标准 from t_hyItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_hyItem.itemid inner join  t_CompabyBZ on t_CompabyBZ.bzid=t_hyItem.pid inner join t_委托单位 on t_委托单位.id=t_CompabyBZ.qyid inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=wrwtype where 单位全称='" + txt_SampleSource.Text.Trim() + "' and t_CompabyBZ.flag=0 and ClassName='" + DropList_SampleType.Text.Trim() + "'";
    //    DataSet ds = new MyDataOp(strsql).CreateDataSet();


    //    if (ds != null)
    //    {
    //        if (ds.Tables.Count > 0)
    //        {
    //            //DataColumn dc = new DataColumn("ID");
    //            //ds.Tables[0].Columns.Add(dc);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in ds.Tables[0].Rows)
    //                {
    //                    strItemlist += dr["分析项目"].ToString() + ",";
    //                }
    //                //grv_Item.DataSource = ds;
    //                //grv_Item.DataBind();

    //            }
    //        }
    //    }
    //}
    //追加一行
    protected void btn_otheradd_Click(object sender, EventArgs e)
    {

        if (DropList_SampleType.Text.Trim() == "降水" && (RepeaterSample.Items.Count >= 1))
        {
            string strFlag = "降水一天不能添加多个样品！";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
            return;
        }
        else
        {
            ////根据企业情况确定样品分析项目
            ////TBD
            //if (drop_rwtype.SelectedValue == "0")
            //{
            //    switch (drop_ItemList.SelectedValue.ToString())
            //    {
            //        case "45":
            //            GetItem();
            //            break;
            //    }
            //}
            if (txt_SampleSource.Text != "")
            {
                if (drop_rwtype.SelectedValue == "0")
                {

                    switch (drop_ItemList.SelectedValue.ToString())
                    {
                        case "45":
                            if (strItemlist== "")
                                GetItem(); 
                                break;
                        case "48":
                                if (strItemlist == "")
                                    GetItem2(1);
                                break;
                        default:
                            if (strItemlist == "")
                                GetItem2(2);
                            break;

                    }
                }
                //else
                //{
                //    if (strItemlist== "")
                //        GetItem2();
                //}


            }
            dt_Sample.Clear();
            DataRow drnew = dt_Sample.NewRow();
            int i = 0;
            allitemlist.Clear();
            for (; i < RepeaterSample.Items.Count; i++)
            {
                DataRow dr = dt_Sample.NewRow();
                TextBox txt_SampleID = RepeaterSample.Items[i].FindControl("txt_SampleID") as TextBox;
                Label lbl_SampleID = RepeaterSample.Items[i].FindControl("lbl_SampleID") as Label;
                //样品编号
                if (txt_SampleID.Text.Trim() != "")
                {
                    txt_SampleID.Visible = true;
                    lbl_SampleID.Visible = true;
                }
                else
                {
                    txt_SampleID.Visible = true;
                    lbl_SampleID.Visible = true;
                }

                HiddenField hid_ID = RepeaterSample.Items[i].FindControl("hid_ID") as HiddenField;
                if (hid_ID.Value.Trim() != "")
                    dr["id"] = hid_ID.Value.Trim();

                dr["样品编号"] = txt_SampleID.Text.Trim();
                //采样点
                TextBox txt_SampleAddress = RepeaterSample.Items[i].FindControl("txt_SampleAddress") as TextBox;
                dr["采样点"] = txt_SampleAddress.Text.Trim();
                drnew["采样点"] = txt_SampleAddress.Text.Trim();
                //样品列表
                TextBox txt_Item = RepeaterSample.Items[i].FindControl("txt_Item") as TextBox;
                dr["分析项目"] = txt_Item.Text.Trim();
                drnew["分析项目"] = txt_Item.Text.Trim();
                if (txt_Item.Text!="")
                strItemlist = txt_Item.Text;
                HiddenField hid_Item = RepeaterSample.Items[i].FindControl("hid_Item") as HiddenField; ;
                dr["分析项目编码"] = hid_Item.Value.Trim();
                dr["分析项目编码"] = hid_Item.Value.Trim();
                //样品性质
                TextBox txt_xz = RepeaterSample.Items[i].FindControl("txt_xz") as TextBox;
                drnew["样品性状"] = txt_xz.Text.Trim();
                dr["样品性状"] = txt_xz.Text.Trim();
                //样品数量
                TextBox txt_num = RepeaterSample.Items[i].FindControl("txt_num") as TextBox;
                if (txt_num.Text.Trim() != "")
                {
                    dr["数量"] = txt_num.Text.Trim();
                    drnew["数量"] = txt_num.Text.Trim();
                }
                else
                {
                    dr["数量"] = "1";
                    drnew["数量"] = "1";
                }
                // CheckBox ck_xcflag = RepeaterSample.Items[i].FindControl("ck_xcflag") as CheckBox;
               
                dt_Sample.Rows.Add(dr);
                dt_Sample.AcceptChanges();
                GridView grv_Item = RepeaterSample.Items[i].FindControl("grv_Item") as GridView;
                DataTable dttemp = dt_analysis.Clone();
                foreach (GridViewRow grv in grv_Item.Rows)
                {
                    DataRow dritem = dttemp.NewRow();
                    CheckBox cb_flag = grv.FindControl("cb_flag") as CheckBox;
                    if (cb_flag.Checked)
                    dritem["现场分析"] ="1";
                    else
                        dritem["现场分析"] = "0";
                    dritem["分析项目"] = grv.Cells[3].Text.Trim();
                  
                    dritem["ItemID"] = grv.Cells[4].Text.Trim();
                    dritem["showid"] = grv.Cells[9].Text.Trim();
                    TextBox txt_Remark = grv.FindControl("txt_ItemRemark") as TextBox;
                  dritem["Remark"] = txt_Remark.Text.Trim();
                  dttemp.Rows.Add(dritem);
                }
                allitemlist.Add(i, dttemp);
                dt_analysis = dttemp.Copy();
               
            }
            DataRow drnewadd = dt_Sample.NewRow();
            if (DropList_SampleType.Text.Trim() != "地表水")
            {
                drnewadd = drnew;
            }
           
            allitemlist.Add(i++, dt_analysis);
            drnewadd["分析项目"] = strItemlist;
            if (drop_rwtype.SelectedValue == "0")
            {
                switch (drop_ItemList.SelectedValue.ToString().Trim())
                {
                    case "45":
                        drnewadd["采样点"] = "入网口";
                        break;
                   
                }
            }

            dt_Sample.Rows.Add(drnew);
            dt_Sample.AcceptChanges();
            RepeaterSample.DataSource = dt_Sample;
            RepeaterSample.DataBind();
        }
            ReportSelectQuery();
       
    }

    private void Bind()
    {
        for (int i = 0; i < RepeaterSample.Items.Count; i++)
        {
           
            //采样点
            DropDownListExtend txt_SampleAddress = RepeaterSample.Items[i].FindControl("txt_SampleAddress") as DropDownListExtend;
            DataBindAll(txt_SampleAddress);
            //样品性质
            DropDownListExtend txt_xz = RepeaterSample.Items[i].FindControl("txt_xz") as DropDownListExtend;
            DataBindXZ(txt_xz);
             Panel panel_js = RepeaterSample.Items[i].FindControl("panel_js") as Panel;
            if (DropList_SampleType.Text == "降水")
            {


                panel_js.Visible = true;
                TextBox txt_s = panel_js.FindControl("txt_s") as TextBox;
                TextBox txt_e = panel_js.FindControl("txt_e") as TextBox;
                txt_s.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00'})");
                txt_e.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00'})");

            }
            else
                panel_js.Visible = false;
            // RadioButtonList cbl_template = RepeaterSample.Items[i].FindControl("cbl_template") as RadioButtonList;
            if (DropList_SampleType.Text == "地表水")
            BindDBS(RepeaterSample.Items[i]);
        }
    }
    //查询出选中的报告的样品列表
    private void ReportSelectQuery()
    {
        string strSql = "SELECT  t_M_SampleInfor.id,t_M_SampleInfor.SampleID AS 样品编号,SampleSource 样品来源,t_M_SampleInfor.SampleDate AS 采样时间,t_M_SampleInfor.SampleAddress 采样点 ," +
      "t_M_SampleInfor.AccessDate AS 接样时间, " +
      " t_M_SampleInfor.TypeID,t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, " +
     " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID,t_M_SampleInfor.StatusID,t_M_SampleInfor.xcflag,t_M_SampleInfor.num 数量,cyman,starttime 开始时间,endtime 结束时间" +
" FROM  t_M_SampleInfor  INNER JOIN" +
     " t_M_AnalysisMainClassEx ON " +
    "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID " +

" WHERE " +
     " t_M_SampleInfor.ReportID=" + strReportId +
" ORDER BY t_M_SampleInfor.CreateDate desc ,convert(int,substring(t_M_SampleInfor.SampleID, { fn LENGTH(t_M_SampleInfor.SampleID)}-2,3)) desc ";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        DataColumn dccc = new DataColumn("分析项目");
        ds.Tables[0].Columns.Add(dccc);
        DataColumn dcc = new DataColumn("分析项目编码");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dccc1 = new DataColumn("样品状态");
        ds.Tables[0].Columns.Add(dccc1);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string getitemstr = "select AIName,MonitorItem from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + dr["样品编号"].ToString() + "' and delflag=0 order by showid";
            DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
            if (dsitem != null && dsitem.Tables.Count > 0)
            {
                foreach (DataRow drr in dsitem.Tables[0].Rows)
                {
                   dr["分析项目"] += drr[0].ToString() + ",";
                   dr["分析项目编码"] += drr[1].ToString() + ",";
                }
            }
            if (dr["StatusID"].ToString() == "0")
                dr["样品状态"] = "登记中";
            else
                dr["样品状态"] = "已提交";
        }
        dt_Sample = ds.Tables[0].Clone();
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_ReportDetail.DataSource = ds;
            grdvw_ReportDetail.DataBind();
            int intColumnCount = grdvw_ReportDetail.Rows[0].Cells.Count;
            grdvw_ReportDetail.Rows[0].Cells.Clear();
            grdvw_ReportDetail.Rows[0].Cells.Add(new TableCell());
            grdvw_ReportDetail.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_ReportDetail.DataSource = ds;
            grdvw_ReportDetail.DataBind();
        }
        ds.Dispose();
        Query();
       
    }
    protected void lbtn_chose_OnTextChanged(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        RepeaterItem parent = lbtn_chose.Parent as RepeaterItem;
       
        Panel panel_Item = parent.FindControl("panel_Item") as Panel;
        Panel panel_cg = panel_Item.FindControl("panel_cg") as Panel;
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
     // TextBox txt_address  panel_Item.FindControl("txt_SampleAddress") as TextBox;
        Panel panel_other = panel_Item.FindControl("panel_other") as Panel;
        CheckBoxList cbl_other = panel_other.FindControl("cb_other") as CheckBoxList;
        TextBox txt_Item = panel_Item.FindControl("txt_Item") as TextBox;
        if (panel_Item.Visible)
        {
            panel_Item.Visible = false;
            
        }
        else
        {
            panel_Item.Visible = true;
           
        }
        DataBindAll(cbl, txt_Item.Text.Trim(), 1);
        DataBindAll(cbl_other, txt_Item.Text.Trim(), 0);
        BindDBS(parent);
        grv_ItemBind(panel_Item);
        Bind();
    }
    //其他
    protected void btn_other_Onclick(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        //RepeaterItem parent = lbtn_chose.Parent.Parent as RepeaterItem;
        Panel panel_item = lbtn_chose.Parent as Panel;
        
        Panel panel_other = panel_item.FindControl("panel_other") as Panel;
        CheckBoxList cbl = panel_other.FindControl("cb_other") as CheckBoxList;
        TextBox txt_Item = panel_item.FindControl("txt_Item") as TextBox;
        GridView grv_Item = panel_other.FindControl("grv_Item") as GridView;
        cbl.AutoPostBack = false;
        if (panel_other.Visible)
        {
            panel_other.Visible = false;
        }
        else
        {
            panel_other.Visible = true;
            DataBindAll(cbl, txt_Item.Text.Trim(),0);
        }
        cbl.AutoPostBack = true;
        Bind();
    }
    protected void btn_cg_Onclick(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        RepeaterItem parent = lbtn_chose.Parent.Parent as RepeaterItem;
        Panel panel_item = parent.FindControl("panel_item") as Panel;
        Panel panel_cg = panel_item.FindControl("panel_cg") as Panel;
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
        TextBox txt_Item = panel_item.FindControl("txt_Item") as TextBox;
        GridView grv_Item = panel_cg.FindControl("grv_Item") as GridView;
        cbl.AutoPostBack = false;
        if (panel_cg.Visible)
        {
            panel_cg.Visible = false;
        }
        else
        {
            panel_cg.Visible = true;
            DataBindAll(cbl, txt_Item.Text.Trim(),1);
        }
        cbl.AutoPostBack = true;
        Bind();
    }

    #endregion

    #region new

  
    protected void btn_SampleSave_Click(object sender, EventArgs e)
    {
        string strFlag = VerifySample();//需要添加检查样品编号的有效性
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

            return;
        }
        else
        {
            string typename = "0";
            string compay = "0";
            string cyman = "";
            //获取样品类型编码
            DataSet myDR1 = new MyDataOp("select ClassID TypeID from t_M_AnalysisMainClassEx where ClassName='" + DropList_SampleType.Text.Trim() + "'").CreateDataSet();
            if (myDR1.Tables[0].Rows.Count > 0)
            {
                typename = myDR1.Tables[0].Rows[0]["TypeID"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该样品类型，请核实！');", true);
                return;
            }
            compay = txt_SampleSource.Text.Trim();
            ////获取样品来源编码
            //DataSet myDR2 = new MyDataOp("select id from t_企业信息 where 单位全称='" + txt_SampleSource.Text.Trim() + "'").CreateDataSet();
            //if (myDR2.Tables[0].Rows.Count > 0)
            //{
            //    compay = myDR2.Tables[0].Rows[0]["id"].ToString();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该企业，请核实！');", true);
            //    return;
            //}
            cyman = tbSampleMan.Text.Trim();
           
            #region 保存纪录
            string ReportID = "";
            ReportID = strReportId;
            List<Entity.Sample> entityList = new List<Entity.Sample>();
            DAl.Sample Dalobj = new DAl.Sample();

         string  retf =SaveInfo(ref entityList, 0,typename,compay,cyman,ReportID);
         if (retf == "")
         {
             //判断样品是否添加
             //TBD
             //数据入库
             int ret = 0;

             if (entityList.Count > 0)
             {
                 if (lbl_SampleDo.Text.Trim() == "添加样品")
                 {
                     ret = Dalobj.AddSample(entityList);
                 }
                 else
                     ret = Dalobj.UpdateSample(entityList);
             }
             else
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请添加样品！！');", true);
                 return;
             }


             if (ret == 0)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号重复!');", true);
             }
             if (ret == 1)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据保存成功!');", true);
                 btn_Save.Visible = true;
             }
             else
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);

         }
         else
             ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('"+retf+"');", true);


            #endregion
            ReportSelectQuery();
        }
    }
   
    protected string  SaveInfo( ref List<Entity.Sample> entityList,int status,string typename, string compay,string cyman,string ReportID)
    {
        string retstr = "";
        //List<Entity.Sample> entityList = new List<Entity.Sample>();
         #region   //初始化样品信息
        //获取企业的评价标准
        string bz = "";
        string bzid = "";
        string bzfw = "";
        try
        {
            DAl.Station getobj = new DAl.Station();
            DataTable dtbz = getobj.GetBzByName(compay,typename);
            bz = dtbz.Rows[0]["bz"].ToString();
            bzid = dtbz.Rows[0]["id"].ToString();
            //bzfw = dtbz.Rows[0]["fw"].ToString();
        }
        catch { }

            for (int i = 0; i < RepeaterSample.Items.Count; i++)
            {
               
               TextBox txt_SampleID = RepeaterSample.Items[i].FindControl("txt_SampleID") as TextBox;
               
                    #region 样品信息
                    Entity.Sample entity = new Entity.Sample();
                    Panel Panel_js = RepeaterSample.Items[i].FindControl("Panel_js") as Panel;
                    if (DropList_SampleType.Text.Trim() == "降水")
                    {
                        TextBox txt_s = Panel_js.FindControl("txt_s") as TextBox;
                        TextBox txt_e = Panel_js.FindControl("txt_e") as TextBox;
                        try
                        {
                            entity.starttime = DateTime.Parse(txt_s.Text.Trim());
                            entity.endtime = DateTime.Parse(txt_e.Text.Trim());
                        }
                        catch
                        {
                            retstr = "降水起止时间有误！";
                        }
                    }
                    else
                    {
                        entity.starttime = DateTime.Parse("1900-01-01 00:00:00");
                        entity.endtime = DateTime.Parse("1900-01-01 00:00:00");
                    }

                    if (strSelectedId != "0" && strSelectedId != "")//编辑信息则初始化记录id
                        entity.ID = int.Parse(strSelectedId);
                    else
                        entity.ID = 0;

                    entity.SampleID = txt_SampleID.Text;//样品ID
                    entity.ReportID = int.Parse(ReportID);//任务ID
                    entity.TypeID = int.Parse(typename);//样品类型
                    entity.SampleSource = compay;//样品来源
                    entity.SampleDate = DateTime.Parse(txt_SampleTime.Text.Trim());//采样时间
                    entity.AccessDate = DateTime.Parse(txt_AccessSampleTime.Text.Trim());//接样时间/现场检测时间
                    entity.CreateDate = DateTime.Now;//记录创建时间
                    entity.UserID = Request.Cookies["Cookies"].Values["u_id"].ToString();//记录创建人
                    entity.TestMan = cyman;//采样人/现场检测人
                    entity.datastatus = 0;//样品数据状态，待定
                    entity.StatusID =status;//样品单登记状态
                    entity.ypstatus = 0;//样品状态
                    entity.bz = bz;
                    entity.bzid = bzid;
                    entity.bzfw = bzfw;
                    CheckBox ck_xcflag = RepeaterSample.Items[i].FindControl("ck_xcflag") as CheckBox;//是否现场检测
                    if (ck_xcflag.Checked)
                        entity.xcflag = "1";
                    else
                        entity.xcflag = "0";
                    TextBox txt_SampleAddress = RepeaterSample.Items[i].FindControl("txt_SampleAddress") as TextBox;//采样点
                    entity.SampleAddress = txt_SampleAddress.Text.Trim();
                    TextBox txt_xz = RepeaterSample.Items[i].FindControl("txt_xz") as TextBox;//样品性质
                    entity.SampleProperty = txt_xz.Text.Trim();
                    TextBox txt_num = RepeaterSample.Items[i].FindControl("txt_num") as TextBox;//样品数量
                    entity.num = 1;
                    if (txt_num.Text.Trim() != "")
                    {
                        try
                        {
                            entity.num = int.Parse(txt_num.Text.Trim());
                        }
                        catch
                        {
                            retstr = "样品数量输入有误！";
                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品数量输入有误！');", true);
                            return retstr;
                        }
                    }
                    if (entity.num <= 0)
                        entity.num = 1;

                    #region 初始化分析项目信息

                    GridView grv_Item = RepeaterSample.Items[i].FindControl("grv_Item") as GridView;
                    if (grv_Item.Rows.Count > 0)
                    {
                        for (int j = 0; j < grv_Item.Rows.Count; j++)
                        {
                            Entity.SampleItem temp = new Entity.SampleItem();
                            if (grv_Item.Rows[j].Cells[1].Text.Trim() != "" && grv_Item.Rows[j].Cells[1].Text.Trim() != "&nbsp;")
                                try
                                {
                                    temp.ID = int.Parse(grv_Item.Rows[j].Cells[1].Text.Trim());//分析项目记录ID
                                }
                                catch
                                { temp.ID = 0; }
                            temp.CreateDate = entity.CreateDate;//创建时间
                            temp.UserID = entity.UserID;//创建人;
                            temp.MonitorItem = grv_Item.Rows[j].Cells[3].Text.Trim();
                            temp.MonitorID = int.Parse(grv_Item.Rows[j].Cells[4].Text.Trim());
                            temp.SampleID = entity.SampleID;

                            CheckBox cb_flag = grv_Item.Rows[j].FindControl("cb_flag") as CheckBox;//是否现场检测
                            if (cb_flag.Checked)
                            {
                                temp.ckflag = 1;
                                TextBox txt_itemvalue = grv_Item.Rows[j].FindControl("itemvalue") as TextBox;//分析值
                                temp.Value = txt_itemvalue.Text.Trim();
                                temp.statusID = 3;//分析单状态待交接
                                temp.AnalysisDate = entity.AccessDate;//现场分析时间
                                temp.AnalysisUserID = entity.TestMan;//现场分析人
                               // temp.Lydate = entity.AccessDate;//领用时间
                                temp.LyUser = entity.UserID;//领用人
                                temp.AnalysisUserID = entity.TestMan;//现场分析人
                            }
                            else
                            {
                                //非现场分析
                                temp.ckflag = 0;
                                //获取分工信息
                                List<string[]> outlist = new List<string[]>();
                                outlist = getuserid(temp.MonitorID, entity.TypeID.ToString());
                                //有指派
                                if (outlist.Count == 1)
                                {
                                    string[] str = outlist[0];

                                    //指定人员分工,保存指定记录表信息
                                    temp.zpcreateuser = entity.UserID;
                                    temp.zpto = str[0];
                                    temp.zpdate = entity.CreateDate;
                                    temp.statusID = 1;
                                }
                                //无指派
                                else
                                {
                                    temp.zpto = "";
                                    temp.zpcreateuser = "";
                                    temp.statusID = 0;

                                }
                            }

                            TextBox txt_ItemRemark = grv_Item.Rows[j].FindControl("txt_ItemRemark") as TextBox;//备注信息
                            temp.Remark = "";
                            if (txt_ItemRemark.Text.Trim() != "")
                                temp.Remark = txt_ItemRemark.Text.Trim();

                            entity.ItemValueList += temp.MonitorID + ",";
                            entity.ItemList += temp.MonitorItem + ",";
                            entity.SampleItemList.Add(temp);
                    #endregion
                    #endregion

                        }
                        entityList.Add(entity);
                    
                    }
            }
            return retstr;
            #endregion
    }


    //提交
    protected void btn_OKSave_Click(object sender, EventArgs e)
    {
        string strFlag = VerifySample();//需要添加检查样品编号的有效性
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

            return;
        }
        else
        {
            string typename = "0";
            string compay = "0";
            string cyman = "";
            //获取样品类型编码
            DataSet myDR1 = new MyDataOp("select ClassID TypeID from t_M_AnalysisMainClassEx where ClassName='" + DropList_SampleType.Text.Trim() + "'").CreateDataSet();
            if (myDR1.Tables[0].Rows.Count > 0)
            {
                typename = myDR1.Tables[0].Rows[0]["TypeID"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该样品类型，请核实！');", true);
                return;
            }
            compay = txt_SampleSource.Text.Trim();
          
            cyman = tbSampleMan.Text.Trim();
           
            #region 保存纪录
            string ReportID = "";
            ReportID = strReportId;
            List<Entity.Sample> entityList = new List<Entity.Sample>();
            DAl.Sample Dalobj = new DAl.Sample();
             string  retf =SaveInfo(ref entityList, 1,typename,compay,cyman,ReportID);
         if (retf == "")
         {
             //判断样品是否添加
             //TBD
             //数据入库
             int ret = 0;

             if (entityList.Count > 0)
             {
               if (lbl_SampleDo.Text.Trim() == "添加样品")
                 {
                     ret = Dalobj.AddSample(entityList);
                 }
                 else
                     ret = Dalobj.UpdateSample(entityList);
             }
             else
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请添加样品！！');", true);
                 return;
             }


             if (ret == 0)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号重复!');", true);
                     return;
             }
             else  if (ret == 1)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据保存成功!');", true);
                 btn_Save.Visible = true;
             }
             else
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);

         }
         else
             ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('"+retf+"');", true);
    

           #endregion
        ReportSelectQuery();
    }
    }
    protected void ck_xcflag_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ck_xcflag = sender as CheckBox;
    RepeaterItem parent=   ck_xcflag.Parent as RepeaterItem;
    GridView grv_Item = parent.FindControl("grv_Item") as GridView;
    HiddenField hid_Item = parent.FindControl("hid_Item") as HiddenField;
    CheckBoxList cbl = parent.FindControl("cb_analysisList") as CheckBoxList;
        if (ck_xcflag.Checked)
        {
            grv_Item.Visible = true;
        }
        else
            grv_Item.Visible = false;
        Bind();
       
    }
    protected void btn_CancelSample_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();", true);
        ReportSelectQuery();
    }
    private string VerifySample()
    {
        string strErrorInfo = "";


        if (DropList_SampleType.Text == "")
            strErrorInfo += "样品类型不能为空！\\n";
        else if (txt_SampleTime.Text == "")
        {
            strErrorInfo += "请输入接样时间！\\n";


        }
        //else if (DateTime.Parse(txt_SampleTime.Text.Trim()) > DateTime.Now)
        //    strErrorInfo += "接样时间不能大于当前时间！\\n";
        //else if (DateTime.Parse(txt_AccessSampleTime.Text.Trim()) > DateTime.Now)
        //    strErrorInfo += "采样时间/现场分析时间不能大于当前时间！\\n";
        //if (drop_ItemList.SelectedValue == " ")
       
        if (txt_AccessSampleTime.Text != "")
        {

            try
            {
                DateTime testtime = DateTime.Parse(txt_AccessSampleTime.Text.Trim());

            }
            catch
            {
                strErrorInfo += "采样时间/现场分析时间填写有误!";


            }
        }
        else
        {
            strErrorInfo += "采样时间/现场分析时间不能为空";


        }
        return strErrorInfo;
    }
   //获取在岗负责人员信息
    protected List<string[]> getuserid(int ItemID,string typeID)
    {
        List<string[]> outlist = new List<string[]>();
        string[] usermethod = new string[2];
        DataSet ds = new MyDataOp("select t_MoniterUser.role,t_MoniterUser.userid,[name] from  t_MoniterUser inner join t_R_UserInfo on t_R_UserInfo.UserID= t_MoniterUser.userid inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.AIID=t_MoniterUser.itemid and t_MoniterUser.typeid=t_M_AnalysisItemEx.ClassID where t_M_AnalysisItemEx.AIID='" + ItemID + "' and t_M_AnalysisItemEx.ClassID='" + typeID + "' and t_R_UserInfo.UserID not in (select userid from  t_R_OUTUserLog where convert(varchar(10),dtime,120)=convert(varchar(10),getdate(),120))").CreateDataSet();

        DataSet checkds = new MyDataOp("SELECT     userid FROM t_R_OUTUserLog WHERE CONVERT(varchar(10), dtime, 120) = CONVERT(varchar(10), GETDATE(), 120)").CreateDataSet();
     
        //这里的方法根据你自己的取数据的方法 
        DataRow[] dra = ds.Tables[0].Select("role='A'");
        DataSet tem = ds.Clone();
        if (dra.Length > 0)
        {
            bool flag = false;
            foreach (DataRow drr in checkds.Tables[0].Rows)
            {
                string b=dra[0]["userid"].ToString().Trim()+",";
              if( drr["userid"].ToString().LastIndexOf(b)>-1)
              {
                  flag = true; break;
              }
            }
            if (!flag)
            {
                DataRow[] drb = ds.Tables[0].Select("role='B'");
                if (drb.Length > 0)
                {
                    foreach (DataRow dr in drb)
                    {
                        ds.Tables[0].Rows.Remove(dr);
                        ds.Tables[0].AcceptChanges();
                    }
                }
            }
            else
            {
                foreach (DataRow dr in dra)
                {
                    ds.Tables[0].Rows.Remove(dr);
                    ds.Tables[0].AcceptChanges();
                }
            }
        }
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            usermethod[0] = dr["userid"].ToString();
            //usermethod[1] = dr["method"].ToString();
            outlist.Add(usermethod);
        }
        return outlist;

    }
    protected void DataBindAll(CheckBoxList cb,string itemvaluelsit,int type)
    {
        if (DropList_SampleType.Text.Trim().ToString().Trim() != "")
        {
            string str = "select  t_M_AnalysisItemEx.AIID id,t_M_ANItemInf.AIName,lower(t_M_ANItemInf.AICode) AICode from t_M_AnalysisItemEx,t_M_AnalysisMainClassEx,t_M_ANItemInf where t_M_AnalysisItemEx.ClassID=t_M_AnalysisMainClassEx.ClassID and t_M_ANItemInf.ID=t_M_AnalysisItemEx.AIID and type='" + type + "' and t_M_AnalysisMainClassEx.ClassName='" + DropList_SampleType.Text.Trim().ToString() + "' order by t_M_ANItemInf.orderid,t_M_ANItemInf.AIName ";
            //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            cb.DataSource = ds;
            cb.DataValueField = "id";
            cb.DataTextField = "AIName";
            cb.DataBind();
            if (ds.Tables[0].Rows.Count < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('没有该样品类型的分析项目，请先在分析项目管理中添加');", true);

                return;
            }
            else
            {
                string[] list = itemvaluelsit.Split(',');
                for (int i = 0; i < cb.Items.Count; i++)
                {
                    foreach (string item in list)
                    {
                        if (item.Trim() == cb.Items[i].Text.ToString().Trim())
                        {
                            cb.Items[i].Selected = true;
                        }
                        else
                        {
                            DataRow[] drsel = ds.Tables[0].Select("AICode='" + item.Trim().ToLower() + "'");
                            if (drsel.Length > 0)
                            {
                                for (int j = 0; j < drsel.Length; j++)
                                {
                                    if (drsel[j]["AIName"].ToString().Trim() == cb.Items[i].Text.ToString().Trim())
                                    {
                                        cb.Items[i].Selected = true;
                                    }
                                }
                            }
                        }
                    }

                    
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请先选择样品类型');", true);

            return;
        }

    }
    protected void DataBindAll(DropDownList cb)
    {
        if (DropList_SampleType.Text.Trim().ToString().Trim() != "")
        {
            string str = "select  id,outNO from t_OUTNO inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_OUTNO.type where  ClassName='" + DropList_SampleType.Text.Trim().ToString() + "'";
            //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            cb.DataSource = ds;
            cb.DataValueField = "id";
            cb.DataTextField = "outNO";
            cb.DataBind();
            
           
        }
        

    }
    protected void DataBindAll(DropDownListExtend cb)
    {
        Hashtable myHT = new Hashtable();
        if (DropList_SampleType.Text.Trim().ToString().Trim() != "")
        {
            string str = "select  id,outNO from t_OUTNO inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_OUTNO.type where  ClassName='" + DropList_SampleType.Text.Trim().ToString() + "'";
            //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0, length = ds.Tables[0].Rows.Count; i < length; i++)
                {
                    if (ds.Tables[0].Rows[i][1].ToString().Trim() != "" && ds.Tables[0].Rows[i][1].ToString().Trim() != null)
                    {
                        myHT.Add(ds.Tables[0].Rows[i][1].ToString().Trim(), ds.Tables[0].Rows[i][1].ToString().Trim());
                    }
                }
            }
            cb.Values = myHT;
        }


    }
    protected void DataBindjs(DropDownListExtend cb)
    {
        Hashtable myHT = new Hashtable();
        if (DropList_SampleType.Text.Trim().ToString().Trim() != "")
        {
            string str = "select  * from View_SampleAddress where  ClassName='" + DropList_SampleType.Text.Trim() + "' order by num ";
            //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0, length = ds.Tables[0].Rows.Count; i < length; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString().Trim() != "" && ds.Tables[0].Rows[i][0].ToString().Trim() != null)
                    {
                        myHT.Add(ds.Tables[0].Rows[i][0].ToString().Trim(), ds.Tables[0].Rows[i][0].ToString().Trim());
                    }
                }
            }
            cb.Values = myHT;
        }


    }
    protected void DataBindXZ(DropDownListExtend cb)
    {
        Hashtable myHT = new Hashtable();
        if (DropList_SampleType.Text.Trim().ToString().Trim() != "")
        {
            string str = "select  * from View_SampleProperty where  ClassName='" + DropList_SampleType.Text.Trim() + "' order by num ";
            //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
            DataSet ds = new MyDataOp(str).CreateDataSet();
           if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0, length = ds.Tables[0].Rows.Count; i < length; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString().Trim() != "" && ds.Tables[0].Rows[i][0].ToString().Trim() != null)
                    {
                        myHT.Add(ds.Tables[0].Rows[i][0].ToString().Trim(), ds.Tables[0].Rows[i][0].ToString().Trim());
                    }
                }
            }
            cb.Values = myHT;
        }


    }
    protected void RepeaterSample_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
       
        RepeaterItem seleItem = e.Item;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int i = seleItem.ItemIndex;

            Panel panel_Item = seleItem.FindControl("panel_Item") as Panel;
            grv_ItemBindOld(panel_Item,i);
            DropDownListExtend drop_address = seleItem.FindControl("txt_SampleAddress") as DropDownListExtend;
            
            DropDownListExtend txt_xz = seleItem.FindControl("txt_xz") as DropDownListExtend;
            DataBindXZ(txt_xz);
            Panel panel_js = seleItem.FindControl("panel_js") as Panel;
            if (DropList_SampleType.Text == "地表水")
            {
                DataBindAll(drop_address);
                BindDBS(seleItem);
                panel_js.Visible = false;
             
            }
            else if (DropList_SampleType.Text == "废水")
            {
                DataBindAll(drop_address);
                panel_js.Visible = false;
                TextBox txt_Item = panel_Item.FindControl("txt_Item") as TextBox;
                string[] list = txt_Item.Text.Trim().Split(',');
                Panel panel_cg = panel_Item.FindControl("panel_cg") as Panel;
                CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
                // TextBox txt_address  panel_Item.FindControl("txt_SampleAddress") as TextBox;
                Panel panel_other = panel_Item.FindControl("panel_other") as Panel;
                CheckBoxList cbl_other = panel_other.FindControl("cb_other") as CheckBoxList;
                DataBindAll(cbl, txt_Item.Text.Trim(), 1);
                DataBindAll(cbl_other, txt_Item.Text.Trim(), 0);
                grv_ItemBind(panel_Item);
                Bind();
               
            }
            else if (DropList_SampleType.Text == "降水")
            {
                panel_js.Visible = true;
                TextBox txt_s = panel_js.FindControl("txt_s") as TextBox;
                TextBox txt_e = panel_js.FindControl("txt_e") as TextBox;
                txt_s.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00'})");
                txt_e.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00'})");
                DataBindjs(drop_address);
               
            }
            else
            {
                DataBindjs(drop_address);
                panel_js.Visible = false;
               
            }
           
        }

    }
    
    protected void getinf(CheckBoxList cbl, GridView grv_Item,ref DataTable tempds,ref  string itemname,ref string itemvalue)
    {
       
        foreach (ListItem LI in cbl.Items)
        {
            if (LI.Selected)
            {
                int i = 0;
                foreach (GridViewRow grv in grv_Item.Rows)
                {
                    if (grv.Cells[3].Text.Trim() == LI.Text.ToString().Trim())
                    {
                        i++;
                        DataRow drnew = tempds.NewRow();
                        drnew["ID"] = grv.Cells[1].Text.Trim(); ;
                        drnew["分析项目"] = LI.Text;
                        drnew["ItemID"] = LI.Value;
                        CheckBox cb_flag = grv.FindControl("cb_flag") as CheckBox;
                        if (cb_flag.Checked)
                            drnew["现场分析"] = "1";
                        else
                            drnew["现场分析"] = "0";
                        drnew["分析项目"] = grv.Cells[3].Text.Trim();

                        drnew["ItemID"] = grv.Cells[4].Text.Trim();
                        drnew["showid"] = grv.Cells[9].Text.Trim();
                        TextBox txt_Remark = grv.FindControl("txt_ItemRemark") as TextBox;
                        drnew["Remark"] = txt_Remark.Text.Trim();
                        //drnew["现场分析"] = grv.Cells[2].Text.Trim();//是否现场分析

                        //if (grv.Cells[5].Text.Trim() != "&nbsp;")
                        //    drnew["分析值"] = grv.Cells[5].Text.Trim();//分析
                        //else
                        //    drnew["分析值"] = "";
                        //if (grv.Cells[7].Text.Trim() != "&nbsp;")
                        //    drnew["Remark"] = grv.Cells[7].Text.Trim();//分析
                        //else
                        //    drnew["Remark"] = "";
                        tempds.Rows.Add(drnew);
                        tempds.AcceptChanges();
                        itemname += LI.Text.Trim() + ",";
                        itemvalue += LI.Value.Trim() + ",";

                    }
                }

                if (i == 0)
                {
                    DataRow drnew = tempds.NewRow();
                    drnew["ID"] = "0";
                    drnew["分析项目"] = LI.Text;
                    drnew["ItemID"] = LI.Value;
                    drnew["现场分析"] = "0";//是否现场分析
                    switch (drnew["ItemID"].ToString().Trim())
                    {
                        case "2": drnew["showid"] = "1";//是否现场分析
                            break;
                        case "90": drnew["showid"] = "2";//是否现场分析
                            break;
                        case "82": drnew["showid"] = "3";//是否现场分析
                            break;
                        default:
                            drnew["showid"] = "99";//是否现场分析
                            break;
                    }
                    
                    tempds.Rows.Add(drnew);
                    tempds.AcceptChanges();
                    itemname += LI.Text.Trim() + ",";
                    itemvalue += LI.Value.Trim() + ",";
                }

            }

        }
    }
  protected void txt_Item_OnTextChanged(object sender, EventArgs e)
    {
        string s = DropList_SampleType.Text.Trim();
        TextBox txt_Item =sender as  TextBox;
        RepeaterItem parent = txt_Item.Parent as RepeaterItem;
      
        string[] list = txt_Item.Text.Trim().Split(',');
        CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
        Panel panel_Item = parent.FindControl("panel_Item") as Panel;
        Panel panel_cg = panel_Item.FindControl("panel_cg") as Panel;
      CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
     // TextBox txt_address  panel_Item.FindControl("txt_SampleAddress") as TextBox;
        Panel panel_other = panel_Item.FindControl("panel_other") as Panel;
        CheckBoxList cbl_other = panel_other.FindControl("cb_other") as CheckBoxList;
        DataBindAll(cbl, txt_Item.Text.Trim(), 1);
        DataBindAll(cbl_other, txt_Item.Text.Trim(), 0);
        BindDBS(parent);  
      grv_ItemBind(panel_Item);
        Bind();
       
    }
  protected void grv_ItemBindOld(Panel parent,int i)
  {
      CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
      GridView grv_Item = parent.FindControl("grv_Item") as GridView;
    
      DataTable tempds = allitemlist[i] as DataTable;
     // tempds.TableName = "list";
      grv_Item.DataSource = tempds;
      grv_Item.DataBind();
      grv_Item.Visible = ck_xcflag.Checked;
  }
  protected void grv_ItemBind(Panel parent)
  {
      
          CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
          GridView grv_Item = parent.FindControl("grv_Item") as GridView;
         TextBox txt_Item = parent.FindControl("txt_Item") as TextBox;
         HiddenField hid_Item = parent.FindControl("hid_Item") as HiddenField;
          DataTable tempds;
          Panel panel_cg = parent.FindControl("panel_cg") as Panel;
          Panel panel_other = parent.FindControl("panel_other") as Panel;
          CheckBoxList cb_analysisList = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
          CheckBoxList cb_other = panel_other.FindControl("cb_other") as CheckBoxList;
          DataBindAll(cb_analysisList, txt_Item.Text,1);
          DataBindAll(cb_other, txt_Item.Text, 0);
          tempds = dt_analysis.Clone();
          DataView dv = new DataView();
          tempds.TableName = "list";
          dv.Table = tempds;

          dv.Sort = "showid";
          string itemname = "";
          string itemvalue= "";
          getinf(cb_analysisList, grv_Item,ref tempds, ref itemname, ref itemvalue);
          getinf(cb_other, grv_Item, ref tempds, ref itemname, ref itemvalue); 
          txt_Item.Text = itemname;
          hid_Item.Value = itemvalue;
          grv_Item.DataSource = dv;
          grv_Item.DataBind();
          grv_Item.Visible = ck_xcflag.Checked;
         
  }
  protected void Change(Panel parent)
  {
     
          CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
          GridView grv_Item = parent.FindControl("grv_Item") as GridView;
          TextBox txt_Item = parent.FindControl("txt_Item") as TextBox;
          HiddenField hid_Item = parent.FindControl("hid_Item") as HiddenField;
          DataTable tempds;
          Panel temp = parent.FindControl("panel_cg") as Panel;
          Panel temp2 = parent.FindControl("panel_other") as Panel;
          CheckBoxList cb_analysisList = temp.FindControl("cb_analysisList") as CheckBoxList;
          CheckBoxList cb_other = temp2.FindControl("cb_other") as CheckBoxList;
         
          tempds = dt_analysis.Clone();
          string itemname = "";
          string itemvalue = "";
          getinf(cb_analysisList, grv_Item, ref tempds, ref itemname, ref itemvalue);
          getinf(cb_other, grv_Item, ref tempds, ref itemname, ref itemvalue);
          txt_Item.Text = itemname;
          hid_Item.Value = itemvalue;
          DataView dv = new DataView();
          tempds.TableName = "list";
          dv.Table = tempds;

          dv.Sort = "showid";
          grv_Item.DataSource = dv;
          grv_Item.DataBind();
          grv_Item.Visible = ck_xcflag.Checked;
          Bind();
  }
    private string getitemid(string item)
    {
        string code = "";
        if (item != "")
        {
            string sql = "select id from t_M_AnalysisItemEx where AIName='" + item + "'";
            DataSet ds = new MyDataOp(sql).CreateDataSet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                code = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        return code;
    }
    #region 分析项目编辑

    protected void grv_Item_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Width = 20;
            e.Row.Cells[2].Width = 150;
            TableCell headerset0 = new TableCell();
            headerset0.Text = "现场检测";
            headerset0.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset0.Width = 60;
            e.Row.Cells.Add(headerset0);
            TableCell headerset1 = new TableCell();
            headerset1.Text = "分析值";
            headerset1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset1.Width = 60;
           
            e.Row.Cells.Add(headerset1);
            headerset1.Visible = false;

            TableCell headerset11 = new TableCell();
            headerset11.Text = "分析方法";
            headerset11.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset11.Width = 400;
            TableCell headerRemark = new TableCell();
            headerRemark.Text = "备注";
            headerRemark.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerRemark.Width = 100;
            e.Row.Cells.Add(headerRemark);
           


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();


            //是否现场检测
            TableCell tabcDetail0 = new TableCell();
            tabcDetail0.Width = 60;
            tabcDetail0.Style.Add("text-align", "center");
            CheckBox cb_flag = new CheckBox();
            cb_flag.ID = "cb_flag";
            cb_flag.Text = "是";
            tabcDetail0.Controls.Add(cb_flag);
            e.Row.Cells.Add(tabcDetail0);

            //分析值
            TableCell tabcDetail1 = new TableCell();
            tabcDetail1.Width = 60;
            tabcDetail1.Style.Add("text-align", "center");
            TextBox itemvalue = new TextBox();
            itemvalue.ID = "itemvalue";
            itemvalue.Width = 75;
            tabcDetail1.Controls.Add(itemvalue);
            e.Row.Cells.Add(tabcDetail1);
            tabcDetail1.Visible = false;
            //分析方法
            TableCell tabcDetail11 = new TableCell();
            tabcDetail11.Width = 400;
            tabcDetail11.Style.Add("text-align", "center");
            RadioButtonList rbl = new RadioButtonList();
            rbl.ID = "rbl";
            rbl.Width = 400;
            tabcDetail11.Controls.Add(rbl);
            
            e.Row.Cells.Add(tabcDetail11);

            //备注
            TableCell tab_remark = new TableCell();

            tab_remark.Style.Add("text-align", "center");
            TextBox txt_ItemRemark = new TextBox();
            txt_ItemRemark.ID = "txt_ItemRemark";
            txt_ItemRemark.Width = 200;
            tab_remark.Controls.Add(txt_ItemRemark);
            e.Row.Cells.Add(tab_remark);

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;

        }
    }
    
    
    protected void grv_Item_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView item_grv = sender as GridView;
        string grvid = item_grv.ID;

        string itemid = e.Row.Cells[4].Text.Trim();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TextBox txt_value = (TextBox)e.Row.FindControl("itemvalue");
            //if (e.Row.Cells[5].Text.Trim() != "&nbsp;")
            //{
            //    txt_value.Text = e.Row.Cells[5].Text.Trim();
            //}
            CheckBox cb_xcfx = (CheckBox)e.Row.FindControl("cb_flag");
            if (e.Row.Cells[2].Text.Trim() == "1")
                cb_xcfx.Checked = true;
            else
                cb_xcfx.Checked = false;

            TextBox txt_ItemRemark = (TextBox)e.Row.FindControl("txt_ItemRemark");
            if (e.Row.Cells[7].Text.Trim() != "&nbsp;")
                txt_ItemRemark.Text = e.Row.Cells[7].Text.Trim();
            RadioButtonList rbl_Method = (RadioButtonList)e.Row.FindControl("rbl");
            if (e.Row.Cells[1].Text.Trim() != "&nbsp;")
            {
                BindCheckBoxList(rbl_Method,e.Row.Cells[4].Text.Trim());
            }
            //if (e.Row.Cells[16].Text.Trim() != "&nbsp;")
            //    rbl_Method.SelectedValue = e.Row.Cells[16].Text.Trim();
            //else
            //    rbl_Method.SelectedIndex = 0;
           

        }
    }
    private void BindCheckBoxList(RadioButtonList rbl, string ItemID)
    {
        DataSet ds = new MyDataOp("select t_M_AnStandard.id,t_M_AnStandard.Standard from  t_M_AIStandard inner join t_M_AnStandard on t_M_AnStandard.id=t_M_AIStandard.Standardid inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.ID=t_M_AIStandard.AIID  where t_M_AnalysisItemEx.ID='" + ItemID + "'").CreateDataSet();

        //这里的方法根据你自己的取数据的方法      
        rbl.DataSource = ds;
        rbl.DataValueField = "id";
        rbl.DataTextField = "Standard";
        rbl.DataBind();
    }
    #endregion
    #endregion



    protected void cb_analysisList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string s = DropList_SampleType.Text.Trim();
        CheckBoxList cb_analysisList = sender as CheckBoxList;
        Panel parent = cb_analysisList.Parent as Panel;
        Change(parent);

    }


    protected void manchoose_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in manchoose.Items)
        {
            if (li.Selected)
            {
                if (!tbSampleMan.Text.Trim().Contains(li.Text.Trim()+","))
                    tbSampleMan.Text += li.Text+",";
            }
        }

    }
    protected void btn_man_Click(object sender, EventArgs e)
    {
        if (panel_manchoose.Visible)
        {
            panel_manchoose.Visible = false;
        }
        else
        {
            panel_manchoose.Visible = true;
            manchoose.Items.Clear();
            DAl.User.Users userobj = new DAl.User.Users();
            DataTable dt = userobj.GetUsersDt("");
            manchoose.DataSource = dt;
            manchoose.DataTextField = "Name";
            manchoose.DataValueField = "UserID";
            manchoose.DataBind();
        }
    }

   
    protected void cb_cg_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb_cg=sender as CheckBox;
        Panel panel_cg = cb_cg.Parent as Panel;
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
        if (cb_cg.Checked)
        {
            //RepeaterItem panel_cg = cb_cg.Parent as RepeaterItem;

            foreach (ListItem li in cbl.Items)
            { li.Selected = true; }
            Change(panel_cg);

        }
        else
        {
            foreach (ListItem li in cbl.Items)
            { li.Selected = false; }
            Change(panel_cg);
        }
    }
    protected void cb_showother_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb_showother = sender as CheckBox;
        Panel panel_other = cb_showother.Parent as Panel;
        CheckBoxList cbl = panel_other.FindControl("cb_other") as CheckBoxList;
        if (cb_showother.Checked)
        {
            //RepeaterItem panel_cg = cb_cg.Parent as RepeaterItem;

            foreach (ListItem li in cbl.Items)
            { li.Selected = true; }
            Change(panel_other);

        }
        else
        {
            foreach (ListItem li in cbl.Items)
            { li.Selected = false; }
            Change(panel_other);
        }
    }
   
   
   
    private DataSet   GetItemDBSTemplate(string name)
    
    {
        string strsql = "select t_OUTNOParam.id,t_OUTNOParam.outid,remark 说明 ,list  监测项 from  t_OUTNOParam inner join t_OUTNO on t_OUTNO.id= outid where outNO='" + name + "'";
        DataSet ds = new MyDataOp(strsql).CreateDataSet();
        string strsql2 = "select t_M_ANItemInf.id, AIName 分析项目, t_OUTNOItem.itemid ItemID,t_OUTNOItem.fw,paramid from t_OUTNOItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_OUTNOItem.itemid  inner join t_OUTNO on t_OUTNO.id= outid where outNO='" + name + "'";
        DataSet ds2 = new MyDataOp(strsql2).CreateDataSet();

        // txt_Item.Enabled = false;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow[] drs = ds2.Tables[0].Select("paramid='" + dr["id"].ToString() + "'");
                        foreach (DataRow drr in drs)
                        {
                            dr["监测项"] += drr["分析项目"].ToString() + ",";
                        }
                    }
                }
                else
                {
                    DataRow dr = ds.Tables[0].NewRow();

                    foreach (DataRow drr in ds2.Tables[0].Rows)
                    {

                        dr["监测项"] += drr["分析项目"].ToString() + ",";

                    }
                    ds.Tables[0].Rows.Add(dr);
                }
                DataRow dradd=ds.Tables[0].NewRow();
                dradd["说明"]="其他";
                ds.Tables[0].Rows.Add(dradd);
                ds.AcceptChanges();
            }
        }
        return ds;
    }
    private void BindDBS(RepeaterItem seleItem)
    {

        RadioButtonList cbl_template = seleItem.FindControl("cbl_template") as RadioButtonList;
        TextBox drop_address = seleItem.FindControl("txt_SampleAddress") as TextBox;
        // txt_SampleAddress.Text = drop_address.Text;
        Label lbl_cg = seleItem.FindControl("lbl_cg") as Label;
        if (DropList_SampleType.Text == "地表水")
        {

            lbl_cg.Visible = true;
            cbl_template.Visible = true;
            cbl_template.DataSource = GetItemDBSTemplate(drop_address.Text.Trim());
            cbl_template.DataTextField = "说明";
            cbl_template.DataValueField = "监测项";
            cbl_template.DataBind();
        }
        else
        {
            lbl_cg.Visible = false;
            cbl_template.Visible = lbl_cg.Visible = false;
        }
    }
    //根据企业加载分析项目
    private string GetItemDBS(string name, GridView grv_Item)
    {
       string strItem = "";
        //string strsql = "select t_M_ANItemInf.id, AIName 分析项目, t_OUTNOItem.itemid ItemID from t_OUTNOItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_OUTNOItem.itemid inner join  t_OUTNO on t_OUTNO.id=t_OUTNOItem.outid  inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.AIID=t_M_ANItemInf.id inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_M_AnalysisItemEx.ClassID where outNO='" + txt_SampleSource.Text.Trim() + "' and t_M_AnalysisItemEx.ClassName='" + DropList_SampleType.Text.Trim() + "'";
       string strsql = "select t_M_ANItemInf.id, AIName 分析项目, t_OUTNOItem.itemid ItemID,t_M_ANItemInf.showid from t_OUTNOItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_OUTNOItem.itemid inner join  t_OUTNO on t_OUTNO.id=t_OUTNOItem.outid    where outNO='" + name + "' ";

        DataSet ds = new MyDataOp(strsql).CreateDataSet();
        DataTable tempds = dt_analysis.Clone();

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                //DataColumn dc = new DataColumn("ID");
                //ds.Tables[0].Columns.Add(dc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strItem += dr["分析项目"].ToString() + ",";
                        DataRow dradd = tempds.NewRow();
                        dradd["itemid"]=dr["id"].ToString();
                        dradd["分析项目"] = dr["分析项目"].ToString();
                        dradd["showid"] = dr["showid"].ToString();
                        tempds.Rows.Add(dradd);
                    }
                    DataView dv = new DataView();
                    tempds.TableName = "list";
                    dv.Table = tempds;

                    dv.Sort = "showid";
                    grv_Item.DataSource = dv;
                    grv_Item.DataBind();

                }
            }
        }
        return strItem;
    }
   

    protected void txt_SampleAddress_TextChanged(object sender, EventArgs e)
    {
        TextBox drop_address = sender as TextBox;
        RepeaterItem seleItem = drop_address.Parent as RepeaterItem;
        //TextBox txt_SampleAddress = seleItem.FindControl("txt_SampleAddress") as TextBox;
        TextBox txt_Item = seleItem.FindControl("txt_Item") as TextBox;
        GridView grv_Item = seleItem.FindControl("grv_Item") as GridView;
        RadioButtonList cbl_template = seleItem.FindControl("cbl_template") as RadioButtonList;
        // txt_SampleAddress.Text = drop_address.Text;
        Label lbl_cg = seleItem.FindControl("lbl_cg") as Label;
        if (DropList_SampleType.Text == "地表水")
        {

            lbl_cg.Visible = true;
            cbl_template.Visible = true;
            cbl_template.DataSource = GetItemDBSTemplate(drop_address.Text.Trim());
            cbl_template.DataTextField = "说明";
            cbl_template.DataValueField = "监测项";
            cbl_template.DataBind();
        }
        else
        {
            lbl_cg.Visible = false;
            cbl_template.Visible = lbl_cg.Visible = false;
            ;
        }
       
    }
    
    protected void cbl_template_SelectedIndexChanged1(object sender, EventArgs e)
    {
        RadioButtonList cbl_template = sender as RadioButtonList;
        Panel panel_1 = cbl_template.Parent as Panel;
        RepeaterItem seleItem = panel_1.Parent as RepeaterItem;
        TextBox txt_Item = seleItem.FindControl("txt_Item") as TextBox;
        for (int i = 0; i < cbl_template.Items.Count; i++)
        {
            if (cbl_template.Items[i].Selected)
            {
                txt_Item.Text = cbl_template.Items[i].Value;
            }
        }
        string[] list = txt_Item.Text.Trim().Split(',');
        CheckBox ck_xcflag = seleItem.FindControl("ck_xcflag") as CheckBox;
        Panel panel_Item = seleItem.FindControl("panel_Item") as Panel;
        grv_ItemBind(panel_Item);
        Bind();
    }
    protected void drop_QueryRWtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAl.Sample sampleobj = new DAl.Sample();
        DataTable dtpurpose = sampleobj.GetPurpose("", int.Parse(drop_QueryRWtype.SelectedValue.ToString()));
        drop_QueryProjectType.DataSource = dtpurpose;
        drop_QueryProjectType.DataTextField = "ItemName";
        drop_QueryProjectType.DataValueField = "ItemID";
        drop_QueryProjectType.DataBind();
        ListItem li=new ListItem("所有","-1");
        drop_QueryProjectType.Items.Add(li);
        drop_QueryProjectType.SelectedIndex = drop_QueryProjectType.Items.Count - 1;

    }
   
    protected void close_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Ib_close = sender as ImageButton;
         RepeaterItem seleItem = Ib_close.Parent as RepeaterItem;

         int n = 0;
           dt_Sample.Clear();
            DataRow drnew = dt_Sample.NewRow();
            allitemlist.Clear();
            for (int i = 0; i < RepeaterSample.Items.Count; i++)
            {
                if (seleItem.ItemIndex == 0 && lbl_SampleDo.Text != "添加样品")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('若要删除该样品，请直接在样品列表中删除！');", true);
                    return;
                }
                if (i != seleItem.ItemIndex)
                {
                    DataRow dr = dt_Sample.NewRow();
                    TextBox txt_SampleID = RepeaterSample.Items[i].FindControl("txt_SampleID") as TextBox;
                    Label lbl_SampleID = RepeaterSample.Items[i].FindControl("lbl_SampleID") as Label;
                    //样品编号
                    if (txt_SampleID.Text.Trim() != "")
                    {
                        txt_SampleID.Visible = true;
                        lbl_SampleID.Visible = true;
                    }
                    else
                    {
                        txt_SampleID.Visible = true;
                        lbl_SampleID.Visible = true;
                    }

                    HiddenField hid_ID = RepeaterSample.Items[i].FindControl("hid_ID") as HiddenField;
                    if (hid_ID.Value.Trim() != "")
                        dr["id"] = hid_ID.Value.Trim();

                    dr["样品编号"] = txt_SampleID.Text.Trim();
                    //采样点
                    TextBox txt_SampleAddress = RepeaterSample.Items[i].FindControl("txt_SampleAddress") as TextBox;
                    dr["采样点"] = txt_SampleAddress.Text.Trim();
                    drnew["采样点"] = txt_SampleAddress.Text.Trim();
                    //样品列表
                    TextBox txt_Item = RepeaterSample.Items[i].FindControl("txt_Item") as TextBox;
                    dr["分析项目"] = txt_Item.Text.Trim();
                    drnew["分析项目"] = txt_Item.Text.Trim();
                    if (txt_Item.Text != "")
                        strItemlist = txt_Item.Text;
                    HiddenField hid_Item = RepeaterSample.Items[i].FindControl("hid_Item") as HiddenField; ;
                    dr["分析项目编码"] = hid_Item.Value.Trim();
                    dr["分析项目编码"] = hid_Item.Value.Trim();
                    //样品性质
                    TextBox txt_xz = RepeaterSample.Items[i].FindControl("txt_xz") as TextBox;
                    drnew["样品性状"] = txt_xz.Text.Trim();
                    dr["样品性状"] = txt_xz.Text.Trim();
                    //样品数量
                    TextBox txt_num = RepeaterSample.Items[i].FindControl("txt_num") as TextBox;
                    if (txt_num.Text.Trim() != "")
                    {
                        dr["数量"] = txt_num.Text.Trim();
                        drnew["数量"] = txt_num.Text.Trim();
                    }
                    else
                    {
                        dr["数量"] = "1";
                        drnew["数量"] = "1";
                    }

                    dt_Sample.Rows.Add(dr);
                    dt_Sample.AcceptChanges();
                    GridView grv_Item = RepeaterSample.Items[i].FindControl("grv_Item") as GridView;
                    DataTable tempdt = dt_analysis.Clone();
                   
                    foreach (GridViewRow grv in grv_Item.Rows)
                    {
                        DataRow dritem = tempdt.NewRow();
                        CheckBox cb_flag = grv.FindControl("cb_flag") as CheckBox;
                        if (cb_flag.Checked)
                            dritem["现场分析"] = "1";
                        else
                            dritem["现场分析"] = "0";
                        dritem["分析项目"] = grv.Cells[3].Text.Trim();

                        dritem["ItemID"] = grv.Cells[4].Text.Trim();
                        dritem["showid"] = grv.Cells[9].Text.Trim();
                        TextBox txt_Remark = grv.FindControl("txt_ItemRemark") as TextBox;
                        dritem["Remark"] = txt_Remark.Text.Trim();
                        tempdt.Rows.Add(dritem);
                    }
                    allitemlist.Add(n++, tempdt);
                }               
            }       
            RepeaterSample.DataSource = dt_Sample;
            RepeaterSample.DataBind();
           
    }
    protected void cb_itemother_CheckedChanged(object sender, EventArgs e)
    {
 
        CheckBox cb_cg=sender as CheckBox;
        Panel panel_cg = cb_cg.Parent as Panel;
        CheckBoxList cbl = panel_cg.FindControl("cb_other") as CheckBoxList;
        if (cb_cg.Checked)
        {
            //RepeaterItem panel_cg = cb_cg.Parent as RepeaterItem;

            foreach (ListItem li in cbl.Items)
            { li.Selected = true; }
            Change(panel_cg);

        }
        else
        {
            foreach (ListItem li in cbl.Items)
            { li.Selected = false; }
            Change(panel_cg);
        }

    }
}
