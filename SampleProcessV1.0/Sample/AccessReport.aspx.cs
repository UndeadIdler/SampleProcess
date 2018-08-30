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

using WebApp.Components;

public partial class Sample_AccessReport : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strSampleId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSampleId"]; }
        set { ViewState["strSampleId"] = value; }
    }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_SampleType.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_CreateDate.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Query();
            SetButton();// txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
          

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>任务下达</b></font>";
        }
    }


    #region 报告相关
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
            lbl_AccessTime.Text="接到时间";
           
            DataTable dtmode = sampleobj.GetMode("", "mode","1");
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
           
            
            DataTable dtmode = sampleobj.GetMode("", "mode","");
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
           
            DataTable dtmode = sampleobj.GetMode("", "mode", "1");
            drop_mode.DataSource = dtmode;
            drop_mode.DataTextField = "name";
            drop_mode.DataValueField = "code";
            drop_mode.DataBind();
            DataTable dtpurpose = sampleobj.GetPurpose("", 0);
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
    }
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            btn_Add.Enabled = false;
            btn_OK.Enabled = false;
            
            btn_Save.Enabled = false;
           
        }
        else
        {
            btn_Add.Enabled = true;
            btn_OK.Enabled = true;
            btn_Save.Enabled = true;
           // btn_AddSample.Enabled = true;
          //  btn_OKSample.Enabled = true;
            // btn_item.Enabled = true;
            //for (int i = 0; i < grdvw_List.Rows.Count; i++)
            //{
            //    ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
            //    if(btn!=null)
            //    btn.Visible = true;
            //}
        }
    }
    private void Query()
    {
        //strSelectedId=txt_samplequery.Text;
        string strSample = "";
        string strDate = "";
        if (txt_samplequery.Text.Trim() != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text.Trim() + "%'";
        if (txt_QueryWTDW.Text.Trim()!="")
            strSample = "and wtdepart like'%" + txt_QueryWTDW.Text.Trim() + "%'";
            if (txt_QueryTime.Text.Trim() != "")
            strDate = " and (year(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
            string strSql = "select t_M_ReporInfo.id,t_M_ReporInfo.ReportAccessDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,urgent 备注,t_M_ReporInfo.Ulevel,Projectname 项目名称, ItemName 项目负责人,rwclass,jcmethod,address,lxman,lxtel,lxemail,wtdepart,Green , chargeman  from t_M_ReporInfo,t_M_ItemInfo where  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=0) and wetherscance=0   " + strSample + strDate + " order by t_M_ReporInfo.id";

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
       DataTable dtmode= getobj.GetMode("","mode","");
       DAl.Station get = new DAl.Station();
       DataTable dtstation = get.GetWTByName("");
         string strtemp = "select Name,UserID from t_R_UserInfo";
                DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["chargeman"].ToString().Trim() != "")
            {
                DataRow[] druser = ds_User.Tables[0].Select("UserID='" + dr["chargeman"].ToString().Trim() + "'");
                if (druser.Length == 1)
                    dr["项目负责人"] = druser[0]["Name"].ToString();

                else
                    dr["项目负责人"] = "";
            }
            else
                dr["项目负责人"] = "";
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
        ListItem choose = new ListItem("请选择", "-1");
        string idtype = "0";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text != "&nbsp;")//任务类型
            idtype=grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim();
            // drop_rwtype.Items.FindByValue(grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim()).Selected = true;
        initial(idtype);
        string slevel = "";
        
        btn_Save.Visible = true;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();

        lbl_Type.Text = "编辑";

        txt_CreateDate.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim();//报告创建日期

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
       

      
       
            if (grdvw_List.Rows[e.NewEditIndex].Cells[16].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[16].Text.Trim() != "")
            {
                txt_wtdepart.Text = grdvw_List.Rows[e.NewEditIndex].Cells[16].Text.Trim();
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
      
        string itemtid = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        //drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue(itemtid).Selected = true;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "")
        {
            txt_xmfzr.Text = grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "")
        {
            txt_Projectname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[17].Text.Trim() == "1" )
        {
            ck_green.Checked = true;
        }
        else
            ck_green.Checked = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
       

  

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

            //TableCell headerset = new TableCell();
            //headerset.Text = "样品列表";
            //headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerset.Width = 60;
            //e.Row.Cells.Add(headerset);
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            {
                TableCell headerDel = new TableCell();
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

            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);


            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.ID = "btn_delete";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            {
                tabcDel.Controls.Add(ibtnDel);
                e.Row.Cells.Add(tabcDel);

            }
            else
            {
                ibtnDel.Visible = false;
            }
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
            e.Row.Cells[18].Visible = false;
        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strReportId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string[] deletelist = new string[4];
        string str = "select id from t_M_SampleInfor where ReportID= '" + strReportId + "'";
       // strSql = "DELETE FROM t_MonitorItemDetail ";
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
            deletelist.SetValue(strSql,2);

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
            compay = txt_wtdepart.Text.Trim();
                //DAl.Station stationobj = new DAl.Station();
                //DataTable dtstation = stationobj.GetWTByName(txt_wtdepart.Text.Trim());

                //if (dtstation.Rows.Count > 0)
                //{
                //    compay = dtstation.Rows[0]["id"].ToString();
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
                if (ck_green.Checked)
                    entity.Green = 1;
                else
                    entity.Green = 0;
            entity.CreateDate = DateTime.Now;//创建时间
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
            entity.WTDate = DateTime.Parse(txt_CreateDate.Text.Trim());//委托日期，任务接收日期
            entity.chargeman = cyman;//项目负责人
            entity.level = drop_level.SelectedValue.ToString();//紧急程度
            entity.Mode = drop_mode.SelectedValue.ToString();//监测方式
            entity.Remark = drop_urgent.Text.Trim();//备注
            entity.WTNO = txt_ReportID.Text.Trim();//委托协议编码，报告标识
            entity.ProjectName = drop_ItemList.SelectedItem.Text.Trim();// txt_Projectname.Text.Trim();//项目名称
            entity.TypeID = int.Parse(drop_ItemList.SelectedValue.ToString().Trim());
            DAl.Report reportobj = new DAl.Report();
            entity.StatusID =1;
             if (strReportId == "" || strReportId == "0")
            {
              if( reportobj.Add(entity)==1)     
              {
                 
                    WebApp.Components.Log.SaveLog("创建样品原单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);

                }
                else
                {
                    WebApp.Components.Log.SaveLog("创建样品原单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添保存失败！')", true);

                }
              }
              else

              {
     entity.ID=int.Parse(strReportId);
                if (reportobj.Upate(entity)==1)
               
                {
                    WebApp.Components.Log.SaveLog("创建样品原单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);


                }
                else
                {
                    WebApp.Components.Log.SaveLog("创建样品原单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添保存失败！')", true);

                }
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
        btn_OK.Text = "确定";
        ck_green.Checked = false;
       // btn_Save.Visible = false;
        txt_ReportID.Text = "";
             ListItem choose = new ListItem("请选择", "-1");
      
        strReportId = "0";
        strReportName = "";
        txt_CreateDate.Text = DateTime.Now.ToString();
        txt_Projectname.Text = "";
        txt_wtdepart.Text = "";

       
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        drop_urgent.Text = "";
    
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
            
            compay = txt_wtdepart.Text.Trim();
                     entity.WTMan = compay;//委托单位
                entity.lxEmail = txt_lxemail.Text.Trim();
                entity.lxMan = txt_lxman.Text.Trim(); ;
                entity.lxtel = txt_lxtel.Text.Trim(); ;
                entity.address = txt_address.Text.Trim();
                if (ck_green.Checked)
                    entity.Green = 1;
                else
                     entity.Green =0;
               

            entity.CreateDate = DateTime.Now;//创建时间
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
            entity.WTDate=DateTime.Parse(txt_CreateDate.Text.Trim());//委托日期，任务接收日期
            entity.chargeman = cyman;//项目负责人
            entity.level = drop_level.SelectedValue.ToString();//紧急程度
            entity.Mode = drop_mode.SelectedValue.ToString();//监测方式
            entity.Remark = drop_urgent.Text.Trim();//备注
            entity.WTNO = txt_ReportID.Text.Trim();//委托协议编码，报告标识
            entity.ProjectName = drop_ItemList.SelectedItem.Text.Trim(); ;//项目名称
            entity.TypeID = int.Parse(drop_ItemList.SelectedValue.ToString().Trim());
            DAl.Report reportobj = new DAl.Report();
            entity.StatusID = 0;
            #region 添加新纪录
            if (strReportId == "" || strReportId == "0")
            {
              if( reportobj.Add(entity)==1)     
              {
                    btn_Save.Visible = true;
                    WebApp.Components.Log.SaveLog("创建任务单添加成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('任务创建成功！')", true);

                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据添加成功！')", true);


                }
                else
                {
                    WebApp.Components.Log.SaveLog("创建任务单添加失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('数据添加失败！')", true);

                }

            }
            #endregion
            else
            {
                 entity.ID=int.Parse(strReportId);
                if (reportobj.Upate(entity)==1)
                {
                    WebApp.Components.Log.SaveLog("创建任务单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);
                }
                else
                {
                    WebApp.Components.Log.SaveLog("创建任务单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添保存失败！')", true);

                }
            }

        }
        Query();
    }
    private string Verify()
    {
        string strErrorInfo = "";

        if (drop_ItemList.SelectedValue == "-1")
            strErrorInfo += "请选择项目类型！\\n";
        else if (txt_CreateDate.Text == "")
            strErrorInfo += "请填写时间！\\n";
      
        else if (DateTime.Parse(txt_CreateDate.Text.Trim()) > DateTime.Now)
            strErrorInfo += "接收不能大于当前时间！\\n";
        //else if (txt_Projectname.Text.Trim() == "")
        //    strErrorInfo += "请选择项目名称！\\n";
        else if (txt_wtdepart.Text.Trim() == "")
            strErrorInfo += "请选择委托单位！\\n";
        //else if (DropList_client.SelectedValue == "-1")
        //    strErrorInfo += "请选择区域！\\n";

        return strErrorInfo;
    }

    #endregion

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
               txt_lxman.Text= dt.Rows[0]["环保负责人"].ToString();
                if (dt.Rows[0]["mobile3"].ToString() != "&nbsp;")
               txt_lxtel.Text = dt.Rows[0]["mobile3"].ToString();
               if( dt.Rows[0]["电子邮箱"].ToString()!="&nbsp;")
               txt_lxemail.Text = dt.Rows[0]["电子邮箱"].ToString();
            }
        }
    }
}
