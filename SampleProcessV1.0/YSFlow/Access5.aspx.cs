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

public partial class Sample_Access5 : System.Web.UI.Page
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
          
            Query();
            SetButton();// txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
          

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>报告编制</b></font>";
        }
    }


    #region 报告相关
    private void initial()
    {
        DAl.Sample sampleobj = new DAl.Sample();
        drop_rwtype.SelectedIndex =0;
        
            lbl_reportNO.Text = "报告标识";
            lbl_AccessTime.Text = "委托时间";
            panel_wtdw.Visible = true;
            
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

        txt_Projectname.Text = "";



        txt_xmfzr.Text = "";
       
      
            rbl_wether.SelectedValue = "0";


            txt_remark1.Text = "";
       
            rbl_tkwether.SelectedValue = "0";


            txt_remark2.Text = "";

            txt_ReportNO.Text = "";

            txt_Remark4.Text = "";

            txt_Remark3.Text = "";
       
        

    }
    protected void drop_rwtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAl.Sample sampleobj = new DAl.Sample();
      
            lbl_reportNO.Text = "报告标识";
            lbl_AccessTime.Text = "委托时间";
            panel_wtdw.Visible = true;
            DataTable dtmode = sampleobj.GetMode("", "mode", "");
            drop_mode.DataSource = dtmode;
            drop_mode.DataTextField = "name";
            drop_mode.DataValueField = "code";
            drop_mode.DataBind();
        
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
        if (txt_samplequery.Text != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text + "%'";

        if (drop_tkwether.SelectedIndex != 0)
        {
            strSample += " and (DataFlag='"+drop_tkwether.SelectedValue.ToString().Trim()+"')";
        }

        if (Request.Cookies["Cookies"].Values["u_id"].ToString()!="123")
           strSample += " and  chargeman='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'";
        if (txt_QueryTime.Text != "")
            strDate = " and (year(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";//and hanwether=1
        string strSql = "select t_Y_FlowInfo.id,t_Y_FlowInfo.ReportAccessDate 时间,t_Y_FlowInfo.ItemType,ItemName 项目类型,ReportName 报告标识,urgent 备注,t_Y_FlowInfo.Ulevel,Projectname 项目名称,t_R_UserInfo.Name 项目负责人,rwclass,jcmethod,address,lxman,lxtel,lxemail,wtdepart,wether,varman1,vardate1,varremark1,t_Y_FlowInfo.chargeman,varman2,vardate2,varremark2,DataFlag,ReportNO,varremark4,vardate5,varremark5,hanwether from t_Y_FlowInfo,t_M_ItemInfo,t_R_UserInfo where  t_Y_FlowInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID='4') and chargeman=t_R_UserInfo.UserID     " + strSample + strDate + " order by t_Y_FlowInfo.ReportAccessDate";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dc = new DataColumn("监测方式");
        ds.Tables[0].Columns.Add(dc);
        DataColumn dc0 = new DataColumn("任务类型");
        ds.Tables[0].Columns.Add(dc0);
        DataColumn dc1 = new DataColumn("委托单位");
        ds.Tables[0].Columns.Add(dc1);
        DataColumn dc2 = new DataColumn("数据是否完成");
        ds.Tables[0].Columns.Add(dc2);
        DAl.Sample getobj = new DAl.Sample();
       DataTable dtmode= getobj.GetMode("","mode","");
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
            if (dr["DataFlag"].ToString() == "0")
                dr["数据是否完成"] = "未完成";
            else
                dr["数据是否完成"] = "完成";
            DataRow[] drsel = dtmode.Select("code='" + dr["jcmethod"].ToString() + "'");
            if (drsel.Length == 1)
                dr["监测方式"] = drsel[0]["name"].ToString();

            else
                dr["监测方式"] = "";

            //if (dr["wtdepart"].ToString().Trim() != "")
            //{
            //    DataRow[] drdep = dtstation.Select("id=" + dr["wtdepart"].ToString().Trim() + "");
            //    if (drdep.Length == 1)
                //    dr["委托单位"] = drdep[0]["单位全称"].ToString();

                //else
            dr["委托单位"] = dr["wtdepart"].ToString().Trim();
           // }

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
        initial();
        string slevel = "";
        
        btn_Save.Visible = true;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();

        lbl_Type.Text = "现场踏勘";

        txt_CreateDate.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim();//报告创建日期
       
        ListItem choose = new ListItem("请选择", "-1");
        if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text != "&nbsp;")//任务类型
            drop_rwtype.SelectedValue =grdvw_List.Rows[e.NewEditIndex].Cells[10].Text;
       
            panel_wtdw.Visible = drop_rwtype.SelectedValue == "1";

       
        drop_urgent.Text = "";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[6].Text != "&nbsp;")//备注
            drop_urgent.Text = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text;
        
     

        string itemtid = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        //drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue(itemtid).Selected = true;
        if (drop_rwtype.SelectedValue == "1")//委托监测
        {
            if (grdvw_List.Rows[e.NewEditIndex].Cells[29].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[29].Text.Trim() != "")
            {
                txt_wtdepart.Text = grdvw_List.Rows[e.NewEditIndex].Cells[31].Text.Trim();
            }
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
        }
        
        if (grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "")
        {
            txt_Projectname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim();
        }

        if (grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "")
        {
            txt_xmfzr.Text = grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim() != "")
        {
            slevel = grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim();
            drop_level.SelectedValue = slevel;
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[17].Text.Trim() != "1")
            rbl_wether.SelectedValue = "0";
        else
            rbl_wether.SelectedValue = "1";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[20].Text.Trim() != "&nbsp;")
            txt_remark1.Text = grdvw_List.Rows[e.NewEditIndex].Cells[20].Text.Trim();
        if (grdvw_List.Rows[e.NewEditIndex].Cells[25].Text.Trim() != "1")
            rbl_tkwether.SelectedValue = "0";
        else
            rbl_tkwether.SelectedValue = "1";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[24].Text.Trim() != "&nbsp;")
            txt_remark2.Text = grdvw_List.Rows[e.NewEditIndex].Cells[24].Text.Trim();
        if (grdvw_List.Rows[e.NewEditIndex].Cells[26].Text.Trim() != "&nbsp;")
            txt_ReportNO.Text = grdvw_List.Rows[e.NewEditIndex].Cells[26].Text.Trim();
        if (grdvw_List.Rows[e.NewEditIndex].Cells[27].Text.Trim() != "&nbsp;")
            txt_Remark4.Text = grdvw_List.Rows[e.NewEditIndex].Cells[27].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
        if (rbl_wether.SelectedIndex == 0)
            lbl_remark.Text = "踏勘情况";
        else
            lbl_remark.Text = "整改意见";
        //现场监测
        if (grdvw_List.Rows[e.NewEditIndex].Cells[29].Text.Trim() != "&nbsp;")
            txt_Remark3.Text = grdvw_List.Rows[e.NewEditIndex].Cells[29].Text.Trim();
      
            if (grdvw_List.Rows[e.NewEditIndex].Cells[30].Text.Trim() == "1")
            {
                ck_fa.Checked = true;
                Panel3.Visible = true;
            }
            else
            {
                ck_fa.Checked = false;
                Panel3.Visible = false;
            }
  

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
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            //{
            //    TableCell headerDel = new TableCell();
            //    headerDel.Text = "删除";
            //    headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //    headerDel.Width = 30;
            //    e.Row.Cells.Add(headerDel);
            //}
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

            //TableCell MenuSet = new TableCell();
            //MenuSet.Width = 60;
            //MenuSet.Style.Add("text-align", "center");
            //ImageButton btMenuSet = new ImageButton();
            //btMenuSet.ImageUrl = "~/images/Detail.gif";
            //btMenuSet.CommandName = "Select";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuSet.Controls.Add(btMenuSet);
            //e.Row.Cells.Add(MenuSet);

            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/images/Delete.gif";
            //ibtnDel.ID = "btn_delete";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            ////if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            ////{
            ////    ibtnDel.Enabled = false;
            ////}
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            //{
            //    tabcDel.Controls.Add(ibtnDel);
            //    e.Row.Cells.Add(tabcDel);

            //}
            //else
            //{
            //    ibtnDel.Visible = false;
            //}
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
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[22].Visible = false;
            e.Row.Cells[23].Visible = false;
            e.Row.Cells[24].Visible = false;
            e.Row.Cells[25].Visible = false;
            e.Row.Cells[26].Visible = false;
            e.Row.Cells[27].Visible = false;
            e.Row.Cells[28].Visible = false;
            e.Row.Cells[30].Visible = false;
            e.Row.Cells[31].Visible = false;
            e.Row.Cells[32].Visible = false;
            e.Row.Cells[29].Visible = false;
        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strReportId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;





        strSql = "DELETE FROM t_Y_FlowInfo WHERE id= '" + strReportId + "'";
        
       
        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.ExecuteCommand())
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

    protected void btn_back_Click(object sender, EventArgs e)
    {
        //保存项目信息项目
        Entity.AccessReport entity = new Entity.AccessReport();
        DAl.Report reportobj = new DAl.Report();

        //方案编写保存
        entity.ReportNO = "";//
        entity.CreateDate = DateTime.Now;//创建时间
        entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
        entity.StatusID = 3.5;//保存
        entity.backflag = 1;
        entity.Remark = txt_Remark4.Text.Trim();
        entity.ID = int.Parse(strReportId);

        if (reportobj.UpateYSReport(entity) == 1)
        {
            WebApp.Components.Log.SaveLog("报告编制退回保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 4);
            
               
           
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('报告编制退回保存成功！')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("报告编制退回保存失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 4);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('报告编制退回保存失败！')", true);

        }
        Query();
    }
    //提交
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();

        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
            // return;


        }
        else
        {
            Entity.AccessReport entity = new Entity.AccessReport();

            entity.CreateDate = DateTime.Now;//创建时间
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人

         
                entity.tkflag = 0;
                entity.StatusID = 5;//
           
            entity.backflag = 0;
            DAl.Report reportobj = new DAl.Report();
            entity.ReportNO = txt_ReportNO.Text.Trim();
            entity.Remark = txt_Remark4.Text.Trim();

            entity.ID = int.Parse(strReportId);
            if (reportobj.UpateYSReport(entity) == 1)
            {
                WebApp.Components.Log.SaveLog("报告编制保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);
            }
            else
            {
                WebApp.Components.Log.SaveLog("报告编制编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
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
        initial();
        lbl_Type.Text = "添加";
        btn_OK.Text = "确定";
        btn_Save.Visible = false;
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
            Entity.AccessReport entity = new Entity.AccessReport();
          
            entity.CreateDate = DateTime.Now;//创建时间
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人

            entity.ReportNO = txt_ReportNO.Text.Trim();
            entity.Remark = txt_Remark4.Text.Trim();
            
            entity.backflag = 0;
             DAl.Report reportobj = new DAl.Report();
            
            
                 entity.ID=int.Parse(strReportId);
                 if (reportobj.UpateYSReport(entity) == 1)
                {
                    WebApp.Components.Log.SaveLog("报告编制保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);
                }
                else
                {
                    WebApp.Components.Log.SaveLog("报告编制编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('数据添保存失败！')", true);

                }
        }
        Query();
    }
    private string Verify()
    {
        string strErrorInfo = "";

        //if (drop_ItemList.SelectedValue == "-1")
        //    strErrorInfo += "请选择项目类型！\\n";
        //else if (txt_CreateDate.Text == "")
        //    strErrorInfo += "请填写时间！\\n";

        //else if (txt_Projectname.Text.Trim() == "-1")
        //    strErrorInfo += "请选择项目名称！\\n";
        //else if (txt_wtdepart.Text.Trim() == "-1")
        //    strErrorInfo += "请选择委托单位！\\n";
        //else if (DropList_client.SelectedValue == "-1")
        //    strErrorInfo += "请选择区域！\\n";

        return strErrorInfo;
    }

    #endregion

    protected void txt_wtdepart_TextChanged(object sender, EventArgs e)
    {
        DAl.Station StationObj = new DAl.Station();
        DataTable dt =StationObj.GetStationByName(txt_wtdepart.Text.Trim());
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
    protected void rbl_tkwether_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_tkwether.SelectedValue.ToString()== "0")
            lbl_remark.Text = "踏勘情况";
        else
            lbl_remark.Text = "整改意见";
    }
}
