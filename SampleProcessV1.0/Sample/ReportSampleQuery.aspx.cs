using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using OWC11;
using System.IO;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using WebApp.Components;
using Entity;

public partial class Sample_ReportSampleQuery : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string ysID//所选择操作列记录对应的id
    {
        get { return (string)ViewState["ysID"]; }
        set { ViewState["ysID"] = value; }
    }
    private string dataflag//
    {
        get { return (string)ViewState["dataflag"]; }
        set { ViewState["dataflag"] = value; }
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
    private string strItemId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strItemId"]; }
        set { ViewState["strItemId"] = value; }
    }
    private string strFxItemId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strFxItemId"]; }
        set { ViewState["strFxItemId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:calendar()");
            TextBox2.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
           // txt_ReportTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
             
          // TextBox2.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            
            //Query();
            ReportQuery();

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>报告查询</b></font>"; 
        }
    }
    #region 样品列表
   
    //查询出报告列表
    private void ReportQuery()
    {
        string strSample = "";
        string strDate = "";
        if (txt_QueryWTDW.Text.Trim() != "")
            strSample += "and wtdepart like'%" + txt_QueryWTDW.Text.Trim() + "%'";
        if (TextBox1.Text != "")
            strSample += "and ReportName like '%" + TextBox1.Text + "%'";
        
       
            dataflag = Request.Cookies["Cookies"].Values["u_flag"].ToString();

           
                string strSqlcheck = "select roleid,itemid from t_roleItem where roleid='" + Request.Cookies["Cookies"].Values["u_role"].ToString() + "'";

                DataSet dscheck = new MyDataOp(strSqlcheck).CreateDataSet();
                if (dscheck.Tables[0].Rows.Count > 0)
                {
                    strSample += " and (t_M_ReporInfo.ItemType in(select itemid from t_roleItem where roleid='" + Request.Cookies["Cookies"].Values["u_role"].ToString() + "') or (chargeman ='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'))";
                }
                else
                {
                    if (Request.Cookies["Cookies"].Values["u_level"].ToString() == "12")//zpto='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' or 
                    {
                        strSample += " and (chargeman ='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                    }
                }

           
        if (TextBox2.Text != "")
            strDate = " and (year(ReportAccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Year + "' AND month(ReportAccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Month + "' and day(ReportAccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Day + "')";// or (StatusID=3 and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))

        string strSql = "select t_M_ReporInfo.id,t_M_ReporInfo.ReportAccessDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,urgent 备注,t_M_ReporInfo.Ulevel, ProjectName  项目名称,t_R_UserInfo.Name 项目负责人,rwclass,jcmethod,address,lxman,lxtel,lxemail,wtdepart,statusID,syID,fileflag  from t_M_ReporInfo,t_M_ItemInfo,t_R_UserInfo  where  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID  and wetherscance=0   and chargeman=t_R_UserInfo.UserID  " + strSample + strDate + " order by t_M_ReporInfo.ReportAccessDate desc";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dc = new DataColumn("监测方式");
        ds.Tables[0].Columns.Add(dc);
        DataColumn dc0 = new DataColumn("任务类型");
        ds.Tables[0].Columns.Add(dc0);
        if (int.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) <= 6)
        {
            panel_wtdw.Visible = true;
            btn_print.Visible = true;
        }
        else
        {
            panel_wtdw.Visible = false;
            btn_print.Visible = false;
        }
        DataColumn dc1 = new DataColumn("委托单位");
   
        ds.Tables[0].Columns.Add(dc1);
       
       DataColumn dc2 = new DataColumn("任务状态");
         ds.Tables[0].Columns.Add(dc2);
         DataColumn dcreport = new DataColumn("报告是否已上传");
         ds.Tables[0].Columns.Add(dcreport);
        DAl.Sample getobj = new DAl.Sample();
        DataTable dtmode = getobj.GetMode("", "mode", "");
        DAl.Station get = new DAl.Station();
        DataTable dtstation = get.GetStationByName("");
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
           
                //if (dr["wtdepart"].ToString().Trim() != "")
                //{
                //    DataRow[] drdep = dtstation.Select("id=" + dr["wtdepart"].ToString().Trim() + "");
                //    if (drdep.Length == 1)
                    //    dr["委托单位"] = drdep[0]["单位全称"].ToString();

                    //else
            dr["委托单位"] = dr["wtdepart"].ToString().Trim();

                //}
           
            if (dr["StatusID"].ToString() == "1")
                dr["任务状态"] = "任务已下达";
            else if(dr["StatusID"].ToString() == "0")
               
                dr["任务状态"] = "任务编辑中";
             else
                dr["任务状态"] = "报告编制完成";
            if (dr["fileflag"].ToString() == "1")
                dr["报告是否已上传"] = "是";
            else
                dr["报告是否已上传"] = "否";

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
    }
    
    #region GridView相关事件响应函数
    protected void grdvw_ReportDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ReportDetail.PageIndex = e.NewPageIndex;
        ReportSelectQuery();
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        ReportQuery();
    }
    
    protected void grdvw_ReportDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (dataflag!="1")
                e.Row.Cells[11].Visible = false;
            else
                e.Row.Cells[11].Visible = true;
            // ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[10].Visible = false;
            //e.Row.Cells[16].Visible = false;
            if (int.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) <= 6)
            {
                e.Row.Cells[3].Visible = true;
            }
            else
                e.Row.Cells[3].Visible = false;
           
        }
    }
    protected void txt_wtdepart_TextChanged(object sender, EventArgs e)
    {
        DAl.Station StationObj = new DAl.Station();
        DataTable dt = StationObj.GetStationByName(txt_wtdepart.Text.Trim());
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
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {



            TableCell headerset = new TableCell();
            headerset.Text = "查看";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);
            TableCell headerDetail = new TableCell();
            headerDetail.Text = "报告查看";
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

            TableCell MenuSet = new TableCell();
            MenuSet.Width = 60;
            MenuSet.Style.Add("text-align", "center");
            ImageButton btMenuSet = new ImageButton();
            btMenuSet.ImageUrl = "~/images/Upload.gif";
            btMenuSet.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuSet.Controls.Add(btMenuSet);
            e.Row.Cells.Add(MenuSet);

           
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
            if (int.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) <= 6)
            {
                e.Row.Cells[22].Visible = true ;
            }
            else
                e.Row.Cells[22].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
        }
    }
    
   
    #endregion

    #region 其它函数


    private string Verify()
    {
        string strErrorInfo = "";
        //if (txt_SampleID.Text.Trim() == "")
        //{
        //    strErrorInfo += "样品编码不能为空！\\n";
        //}
        //else
        //{
        //    string str = "select * from t_M_SampleInfor where SampleID='" + txt_SampleID.Text.Trim() + "'";
        //    DataSet ds = new MyDataOp(str).CreateDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //       strErrorInfo= "样品编号不能重复!";
        //    else
        //        strErrorInfo= "";
        //}
        return strErrorInfo;
    }
   
    #endregion
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
           // panel_wtdw.Visible = false;
            DataTable dtmode = sampleobj.GetMode("", "mode", "1");
            drop_mode.DataSource = dtmode;
            drop_mode.DataTextField = "name";
            drop_mode.DataValueField = "code";
            drop_mode.DataBind();
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
        }
    }
    //选中某个报告，某个报告的样品单列表
              
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string idtype = "0";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text != "&nbsp;")//任务类型
            idtype = grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim();
        // drop_rwtype.Items.FindByValue(grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim()).Selected = true;
        initial(idtype);
        string slevel = "";

       // btn_Save.Visible = true;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//报告标识
       // strReportName = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        if (grdvw_List.Rows[e.NewEditIndex].Cells[18].Text.Trim() != "&nbsp;")
            ysID = grdvw_List.Rows[e.NewEditIndex].Cells[18].Text.Trim();
        else
            ysID = "";
        lbl_Type.Text = "编辑";

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
       
            if (grdvw_List.Rows[e.NewEditIndex].Cells[22].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[22].Text.Trim() != "")
            {
                txt_wtdepart.Text = grdvw_List.Rows[e.NewEditIndex].Cells[22].Text.Trim();
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
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
      
        ReportSelectQuery();

    }
    

    #endregion

    //查询出选中的报告的样品列表
    private void ReportSelectQuery()
    {
        string strSql = "select t_MonitorItemDetail.id,t_M_SampleInfor.SampleID AS 样品编号,SampleSource 样品来源, SampleAddress 采样点,t_M_ANItemInf.AIName 监测项,Name 指派给,t_MonitorItemDetail.zpdate 指派时间,t_MonitorItemDetail.lydate 领用时间,t_MonitorItemDetail.flag,t_MonitorItemDetail.xcflag,experimentvalue  分析数据" +
" ,t_M_ANItemInf.ID  itemid, method FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
    " t_M_AnalysisMainClassEx ON " +
   "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id and t_MonitorItemDetail.delflag=0  inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem  left join  t_R_UserInfo on t_R_UserInfo.UserID=zpto where ReportID='" + strReportId + "' ORDER BY t_M_SampleInfor.SampleSource,t_M_SampleInfor.SampleID,t_M_SampleInfor.AccessDate desc";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

   DataColumn dccc = new DataColumn("监测项状态");
  ds.Tables[0].Columns.Add(dccc);
  DataColumn dc1 = new DataColumn("现场分析");
  ds.Tables[0].Columns.Add(dc1);

  foreach (DataRow dr in ds.Tables[0].Rows)
  {

      if (int.Parse(dr["flag"].ToString()) == 0)
          dr["监测项状态"] = "未指派";
      else if (int.Parse(dr["flag"].ToString()) == 1)
          dr["监测项状态"] = "未领用";
      else if (int.Parse(dr["flag"].ToString()) == 2)
          dr["监测项状态"] = "分析中";
      else
          dr["监测项状态"] = "完成";

      if (int.Parse(dr["xcflag"].ToString()) == 0)
          dr["现场分析"] = "否";
      else
          dr["现场分析"] = "是";
      if(dataflag!="1")
          dr["分析数据"]="-";
  }

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
       // Query();
        //queryAnalysisItem();
        ReportQuery();
    }
   
    
    
   

    private bool CheckSampleInfo(string SampleID)
    {
        bool ret = false;
        string updatestr = "select * from t_MonitorItemDetail where SampleID='" + SampleID + "' and flag=0";
        DataSet dscheck = new MyDataOp(updatestr).CreateDataSet();
        if (dscheck != null && dscheck.Tables.Count > 0)
        {
            if (dscheck.Tables[0].Rows.Count > 0)
            {
                ret = true;
            }
        }
        return ret;
    }
    private bool CheckInfo(string DrawID)
    {
        bool ret = false;
        string updatestr = "select * from t_DrawSample where id='" + DrawID + "' and fxflag=0 and yxflag=0";
       DataSet dscheck = new MyDataOp(updatestr).CreateDataSet();
       if (dscheck != null && dscheck.Tables.Count > 0)
       {
           if (dscheck.Tables[0].Rows.Count > 0)
           {
               ret = true;
           }
       }
       return ret;
    }

    //数据交接中的保存
    protected void btn_SaveReport_Click(object sender, EventArgs e)
    {
        string[] strlist = new string[3];
        string strSql = @"update t_M_ReporInfo set ReportNumber='"+txt_ReportNUM.Text.Trim()+"', ReportdataRemark='" + txt_ReportRemark.Text.Trim() + "',ReportdataUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' ,ReportdataDate=getdate() where id='" + strReportId + "'";
            strlist.SetValue(strSql, 0);
            
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.DoTran(1, strlist);
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLog("数据交接中保存:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存成功！');", true);
            }
            else
            {
                WebApp.Components.Log.SaveLog("数据交接中保存:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
            }
       
            ReportQuery();

    }
    //数据交接中的提交
    protected void btn_SampleReport_Click(object sender, EventArgs e)
    {
        string strquery = "select * from  t_M_SampleInfor inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id where t_MonitorItemDetail.flag<=2 and ReportID='" + strReportId + "'";
        DataSet ds = new MyDataOp(strquery).CreateDataSet();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            int i=0;
            string[] list = new string[2];
            string ReportStr = "update t_M_ReporInfo set ReportNumber='"+txt_ReportNUM.Text.Trim()+"',  ReportdataRemark='" + txt_ReportRemark.Text.Trim() + "',ReportdataUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',ReportdataDate=getdate(),StatusID=2 where id='" + strReportId + "'";
            list.SetValue(ReportStr, i++);
            if(ysID!="")
                ReportStr = "update t_Y_FlowInfo set DataFlag=1,ReportdataUser='"+Request.Cookies["Cookies"].Values["u_id"].ToString()+"',ReportdataDate=getdate() where ID='" + ysID + "'";
            list.SetValue(ReportStr, i++);
                MyDataOp mdo = new MyDataOp(ReportStr);
                if (mdo.DoTran(i,list))
                {
                    WebApp.Components.Log.SaveLog("数据交接中提交:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存成功！');", true);
                }
                else
                {
                    WebApp.Components.Log.SaveLog("数据交接中提交:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存失败！');", true);
                }
           

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ddclick", "alert('数据交接失败,该报告中有未完成项目！');", true);
        }
        ReportQuery();
    }
    //数据交接中样品单的回退
    protected void btn_BackReport_Click(object sender, EventArgs e)
    {
        string checkStr = "select StatusID from t_M_ReporInfo  where  id='" + strReportId + "'";
        DataSet checkds = new MyDataOp(checkStr).CreateDataSet();
        if (checkds != null && checkds.Tables.Count > 0 && checkds.Tables[0].Rows.Count > 0)
        {
            if (checkds.Tables[0].Rows[0][0].ToString() == "1")
            {
                string ReportStr = "update t_M_ReporInfo set  StatusID=0 where id='" + strReportId + "'";
                string[] strlist = new string[3];
                strlist.SetValue(ReportStr, 0);
                //ReportStr = "update t_M_SampleInfor set StatusID=0,ReportdataRemark='" + txt_doremark.Text.Trim() + "' where ReportID='" + strReportId + "' and id='" + strSelectedId + "'";

                //strlist.SetValue(ReportStr, 1);
                ReportStr = @"Insert into t_M_Backinfo(SampleID,CreateDate,Remark,UserID,FuntionID) values('" + strSelectedId + "',getdate(),'" + txt_ReportRemark.Text.Trim() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',14)";

                strlist.SetValue(ReportStr, 1);

                MyDataOp mdo = new MyDataOp(ReportStr);
                if (mdo.DoTran(2, strlist))
                {
                    WebApp.Components.Log.SaveLog("数据交接中回退:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('任务退回成功！');", true);
                }
                else
                {
                    WebApp.Components.Log.SaveLog("数据交接中回退:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('任务退回失败！');", true);
                }
                ReportSelectQuery();
            }
            else
            { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('报告已退回！');", true); }
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
       
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        ReportQuery();
       
    }
    protected void btn_Close_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();", true);
    }
    protected void btn_print2_Click(object sender, EventArgs e)
    { }
    protected void btn_print_Click(object sender, EventArgs e)
    {

        int num = 1;
        string Reportstr = " select * from t_M_ReporInfo where t_M_ReporInfo.id='" + strReportId + "'";
        DataSet ReportDs = new MyDataOp(Reportstr).CreateDataSet();
        if (ReportDs != null && ReportDs.Tables.Count > 0)
        {
            Report entity = new Report();

            entity.WTMan = ReportDs.Tables[0].Rows[0]["wtdepart"].ToString();//委托单位
            entity.WTDate = ReportDs.Tables[0].Rows[0]["ReportAccessDate"].ToString();//委托日期

            string SampleListstr = "select  t_M_SampleInfor.*,SampleSource, CONVERT(varchar(100), SampleDate, 23) SampleDate,ClassName, ClassID, CONVERT(varchar(100), AccessDate, 23) AccessDate,TypeID from t_M_SampleInfor   inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_M_SampleInfor.TypeID where ReportID='" + strReportId + "' order by convert(int,substring(SampleID,2,9))";
            DataSet SampleListDs = new MyDataOp(SampleListstr).CreateDataSet();


            if (SampleListDs != null && SampleListDs.Tables.Count > 0 && SampleListDs.Tables[0].Rows.Count > 0)
            {
                DataView myDataView = new DataView(SampleListDs.Tables[0]);
                string[] strComuns = { "SampleSource1", "SampleSource", "SampleDate1", "ClassName", "ClassID", "AccessDate1" };
                DataTable dt = myDataView.ToTable(true, strComuns);
                //根据样品来源区分
                foreach (DataRow drsource in dt.Rows)
                {
                    if (entity.SampleSource == null)
                        entity.SampleSource = "";
                    if (!entity.SampleSource.Contains(drsource["SampleSource"].ToString()))
                    {
                        entity.SampleSource += drsource["SampleSource"].ToString() + "  ";
                    }
                    if (entity.SampleDate == null)
                        entity.SampleDate = "";
                    if (!entity.SampleDate.Contains(DateTime.Parse(drsource["SampleDate1"].ToString()).ToString("yyyy-MM-dd")))
                    {
                        entity.SampleDate += DateTime.Parse(drsource["SampleDate1"].ToString()).ToString("yyyy-MM-dd") + "  ";
                    }
                    entity.SampleType = drsource["ClassName"].ToString() + "  ";
                    entity.AccessDate = drsource["AccessDate1"].ToString() + "  ";
                    entity.TypeID = int.Parse(drsource["ClassID"].ToString());
                    entity.SampleList = new List<Sample>();
                    //string SampleItemtstrTotal = "select AIName,experimentvalue from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem  where SampleID='" + sampleentity.SampleID + "'";
                    //DataSet SampleItemDsTotal = new MyDataOp(SampleItemtstrTotal).CreateDataSet();

                    foreach (DataRow dr in SampleListDs.Tables[0].Rows)
                        {
                            Sample sampleentity = new Sample();
                            sampleentity.SampleAddress = dr["SampleAddress"].ToString();//采样点
                            sampleentity.SampleID = dr["SampleID"].ToString();//样品编号
                            sampleentity.SampleProperty = dr["SampleProperty"].ToString();//样品性状
                            string SampleItemtstr = "select AIName,experimentvalue from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem  where SampleID='" + sampleentity.SampleID + "'";
                            DataSet SampleItemDs = new MyDataOp(SampleItemtstr).CreateDataSet();
                            if (SampleItemDs != null && SampleItemDs.Tables.Count > 0 && SampleItemDs.Tables[0].Rows.Count > 0)
                            {
                                DataView ItemDataView = new DataView(SampleItemDs.Tables[0]);
                                string[] ItemComuns = { "AIName" };
                                DataTable dtItem = ItemDataView.ToTable(true, ItemComuns);
                                foreach (DataRow dr1 in dtItem.Rows)
                                {
                                    if (!entity.ItemList.Contains(dr1["AIName"].ToString()))
                                        entity.ItemList.Add(dr1["AIName"].ToString());
                                }
                                sampleentity.SampleItemList = new List<Entity.SampleItem>();
                               
                                foreach (DataRow dritem in SampleItemDs.Tables[0].Rows)
                                {
                                    SampleItem item = new SampleItem();
                                    item.MonitorItem = dritem["AIName"].ToString();
                                    item.Value = dritem["experimentvalue"].ToString();
                                    sampleentity.SampleItemList.Add(item);
                                }
                               
                            }
                            entity.SampleList.Add(sampleentity);
                        }
                    if (num <= entity.ItemList.Count / 4)
                    {
                        if (entity.ItemList.Count % 4 > 0)
                            num = entity.ItemList.Count / 4 + 1;
                        else num = entity.ItemList.Count / 4;

                    }
                }
            }
            print(entity,num);
        }

        //}
    }
    /// <summary>
    /// 删除超时文件
    /// </summary>
    /// <param name="strPath"></param>
    private void RemoveFiles(string strPath)
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] fiArr = di.GetFiles();
        foreach (System.IO.FileInfo fi in fiArr)
        {
            if (fi.Extension.ToString() == ".xls" || fi.Extension.ToString() == ".doc")
            {
                // if file is older than 2 minutes, we'll clean it up
                TimeSpan min = new TimeSpan(0, 0, 2, 0, 0);
                if (fi.CreationTime < DateTime.Now.Subtract(min))
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
                }
            }
        }
    }
    private void print(Report cobj,int max)
    {
        try
        {
            RemoveFiles(Server.MapPath(".") + "\\temp\\");
        }
        catch (Exception e)
        {
            Log.log alog = new Log.log();
            alog.Log(e.Message.ToString() + DateTime.Now.ToString());
        }

        Random rd = new Random();
        int oid = rd.Next(10000);
        Word.Application app = new Word.Application();
        Word.Document doc = new Word.Document();
        object missing = System.Reflection.Missing.Value;


        object IsSave = true;
        try
        {

            //TBD

            //string DocPath = ConfigurationManager.AppSettings["DocPath"].ToString();
            string DocPath = Server.MapPath("../");



            string TemplateFile = "";

            TemplateFile = DocPath + "Sample\\template\\Report"+max*4+".doc";


            //生成的具有模板样式的新文件
            string FileName = DocPath + "Sample\\temp\\" + oid.ToString() + ".doc";

            File.Copy(TemplateFile, FileName);


            object Obj_FileName = FileName;

            object Visible = false;

            object ReadOnly = false;



            //打开文件  

            doc = app.Documents.Open(ref Obj_FileName,

            ref missing, ref ReadOnly, ref missing,

            ref missing, ref missing, ref missing, ref missing,

            ref missing, ref missing, ref missing, ref Visible,

            ref missing, ref missing, ref missing,

            ref missing);

            doc.Activate();
            //     // 单位全称，企业地址，法人代表，机构代码，试用类型，COD，废水排放量，
            //     string[] sqArr = { "enterprise", "sytime", "bdtime", "xgx", "syman", "fxman" };

            foreach (Word.Bookmark bm in doc.Bookmarks)
            {
                switch (bm.Name)
                {
                    case "SampleType": bm.Select(); bm.Range.Text = cobj.SampleType; break;

                    case "SampleTime":
                        bm.Select(); bm.Range.Text = cobj.SampleDate; break;
                    case "SampleSource":
                        bm.Select(); bm.Range.Text = cobj.SampleSource;
                        break;
                    case "AccessDate":
                        bm.Select(); bm.Range.Text = cobj.AccessDate;
                        break;
                    case "ReportWriter":
                        bm.Select(); bm.Range.Text = cobj.ReportMan;

                        break;
                    case "ReportDate":
                        bm.Select(); bm.Range.Text = cobj.ReportTime;

                        break;
                    case "ReportCheckMan":
                        bm.Select(); bm.Range.Text = cobj.CheckMan;

                        break;
                    case "ReportCheckDate":
                        bm.Select(); bm.Range.Text = cobj.CheckTime;

                        break;
                    default: break;

                }
            }
            for (int j = 0; j < cobj.ItemList.Count; j++)
            {
                object name = "ItemName" + j;
                foreach (Word.Bookmark bm in doc.Bookmarks)
                {
                    if (name.ToString() == bm.Name)
                    {
                        Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref name);
                        itemstr.Select();
                        itemstr.Range.Text = cobj.ItemList[j];
                        break;
                    }
                }
            }
          
            int p = 0;
            for (int n = 0; n < max; n++)
            {
                int i = 0;
                foreach (Sample citem in cobj.SampleList)
                {
                    try
                    {

                        object markname = "SampleAddress" +n+"_"+ i;
                        Word.Bookmark infectbm = doc.Bookmarks.get_Item(ref markname);
                        infectbm.Select();
                        infectbm.Range.Text = citem.SampleAddress;

                        object itemname = "SampleID" + n + "_" + i;
                        Word.Bookmark infectitem = doc.Bookmarks.get_Item(ref itemname);
                        infectitem.Select();
                        infectitem.Range.Text = citem.SampleID;

                        object value1name = "SampleProperty" + n + "_" + i;
                        Word.Bookmark value1item = doc.Bookmarks.get_Item(ref value1name);
                        value1item.Select();
                        value1item.Range.Text = citem.SampleProperty;
                        if (cobj.ItemList.Count > 0)
                        {

                            int m = cobj.ItemList.Count;
                            if ((n + 1) * 4 < cobj.ItemList.Count)
                                m = (n + 1) * 4;
                            else
                                m = cobj.ItemList.Count;
                            for (int j =n*4; j < m; j++)
                            {
                                //object name = "ItemName" + j;
                                //Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref name);
                                //itemstr.Select();
                                //itemstr.Range.Text = cobj.ItemList[j];
                                foreach (SampleItem item in citem.SampleItemList)
                                {
                                    if (item.MonitorItem == cobj.ItemList[j])
                                    {
                                        object value2name = "ItemValue_" + j + "_" + i;
                                        foreach (Word.Bookmark bm in doc.Bookmarks)
                                        {
                                            if (value2name.ToString() == bm.Name)
                                            {
                                                Word.Bookmark value2item = doc.Bookmarks.get_Item(ref value2name);
                                                value2item.Select();
                                                value2item.Range.Text = item.Value.ToString();

                                                break;
                                            }
                                        }
                                    }
                                }


                            }
                        }

                        i++;
                    }
                    catch
                    { i++; break; }
                }
            }

            doc.Save();
            doc.Close(ref IsSave, ref missing, ref missing);
            app.Application.Quit(ref missing, ref missing, ref missing);

    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('temp/" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);
        }
        catch (Exception mes)
        {
            doc.Close(ref IsSave, ref missing, ref missing);
            app.Application.Quit(ref missing, ref missing, ref missing);
            Log.log alog = new Log.log();
            alog.Log(mes.Message.ToString() + DateTime.Now.ToString());
        }
    }
    private void print2(Report cobj)
    {
        try
        {
            RemoveFiles(Server.MapPath(".") + "\\temp\\");
        }
        catch (Exception e)
        {
            Log.log alog = new Log.log();
            alog.Log(e.Message.ToString() + DateTime.Now.ToString());
        }

        Random rd = new Random();
        int oid = rd.Next(10000);
        Word.Application app = new Word.Application();
        Word.Document doc = new Word.Document();
        object missing = System.Reflection.Missing.Value;


        object IsSave = true;
        try
        {

            //TBD

            //string DocPath = ConfigurationManager.AppSettings["DocPath"].ToString();
            string DocPath = Server.MapPath("../");



            string TemplateFile = "";

            TemplateFile = DocPath + "Sample\\template\\Report_jc.doc";


            //生成的具有模板样式的新文件
            string FileName = DocPath + "Sample\\temp\\" + oid.ToString() + ".doc";

            File.Copy(TemplateFile, FileName);


            object Obj_FileName = FileName;

            object Visible = false;

            object ReadOnly = false;



            //打开文件  

            doc = app.Documents.Open(ref Obj_FileName,

            ref missing, ref ReadOnly, ref missing,

            ref missing, ref missing, ref missing, ref missing,

            ref missing, ref missing, ref missing, ref Visible,

            ref missing, ref missing, ref missing,

            ref missing);

            doc.Activate();
            //     // 单位全称，企业地址，法人代表，机构代码，试用类型，COD，废水排放量，
            //     string[] sqArr = { "enterprise", "sytime", "bdtime", "xgx", "syman", "fxman" };

            foreach (Word.Bookmark bm in doc.Bookmarks)
            {
                switch (bm.Name)
                {
                    case "SampleType": bm.Select(); bm.Range.Text = cobj.SampleType; break;

                    case "SampleTime":
                        bm.Select(); bm.Range.Text = cobj.SampleDate; break;
                    case "SampleSource":
                        bm.Select(); bm.Range.Text = cobj.SampleSource;
                        break;
                    case "AccessDate":
                        bm.Select(); bm.Range.Text = cobj.AccessDate;
                        break;
                    case "ReportWriter":
                        bm.Select(); bm.Range.Text = cobj.ReportMan;

                        break;
                    case "ReportDate":
                        bm.Select(); bm.Range.Text = cobj.ReportTime;

                        break;
                    case "ReportCheckMan":
                        bm.Select(); bm.Range.Text = cobj.CheckMan;

                        break;
                    case "ReportCheckDate":
                        bm.Select(); bm.Range.Text = cobj.CheckTime;

                        break;
                    default: break;

                }
            }
            for (int j = 0; j < cobj.ItemList.Count; j++)
            {
                object name = "ItemName" + j;
                foreach (Word.Bookmark bm in doc.Bookmarks)
                {
                    if (name.ToString() == bm.Name)
                    {
                        Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref name);
                        itemstr.Select();
                        itemstr.Range.Text = cobj.ItemList[j];
                        break;
                    }
                }
            }
            int i = 0;
            int p = 0;
            foreach (Sample citem in cobj.SampleList)
            {
                try
                {

                    object markname = "SampleAddress" + i;
                    Word.Bookmark infectbm = doc.Bookmarks.get_Item(ref markname);
                    infectbm.Select();
                    infectbm.Range.Text = citem.SampleAddress;

                    object itemname = "SampleID" + i;
                    Word.Bookmark infectitem = doc.Bookmarks.get_Item(ref itemname);
                    infectitem.Select();
                    infectitem.Range.Text = citem.SampleID;

                    object value1name = "SampleProperty" + i;
                    Word.Bookmark value1item = doc.Bookmarks.get_Item(ref value1name);
                    value1item.Select();
                    value1item.Range.Text = citem.SampleProperty;
                    if (cobj.ItemList.Count > 0)
                    {

                        for (int j = 0; j < cobj.ItemList.Count; j++)
                        {
                            //object name = "ItemName" + j;
                            //Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref name);
                            //itemstr.Select();
                            //itemstr.Range.Text = cobj.ItemList[j];
                            foreach (SampleItem item in citem.SampleItemList)
                            {
                                if (item.MonitorItem == cobj.ItemList[j])
                                {
                                    object value2name = "ItemValue_" + j + "_" + i;
                                    foreach (Word.Bookmark bm in doc.Bookmarks)
                                    {
                                        if (value2name.ToString() == bm.Name)
                                        {
                                            Word.Bookmark value2item = doc.Bookmarks.get_Item(ref value2name);
                                            value2item.Select();
                                            value2item.Range.Text = item.Value.ToString();

                                            break;
                                        }
                                    }
                                }
                            }


                        }
                    }

                    i++;
                }
                catch
                { i++; break; }
            }

            doc.Save();
            doc.Close(ref IsSave, ref missing, ref missing);
            app.Application.Quit(ref missing, ref missing, ref missing);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('temp/" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);
        }
        catch (Exception mes)
        {
            doc.Close(ref IsSave, ref missing, ref missing);
            app.Application.Quit(ref missing, ref missing, ref missing);
            Log.log alog = new Log.log();
            alog.Log(mes.Message.ToString() + DateTime.Now.ToString());
        }
    }

    protected void grdvw_ReportDetail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        int flag = int.Parse(grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[8].Text.Trim().ToString());
        if (flag == 3)
        {
            List<Entity.SampleItem> entitylist = new List<Entity.SampleItem>();
            Entity.SampleItem entity = new Entity.SampleItem();
            entity.SampleID = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim().ToString();//样品编号
            entity.ID = int.Parse(grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().ToString());//监测项记录ID
            entity.statusID = 2;
            //指派人
            entity.zpcreateuser = Request.Cookies["Cookies"].Values["u_id"].ToString();
            //指派时间
            entity.zpdate = DateTime.Now;
            entitylist.Add(entity);
            DAl.DrawSample itemObj = new DAl.DrawSample();
            if (itemObj.SampleItemBack(entitylist) == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('退回分析保存成功！')", true);
                ReportSelectQuery();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('退回分析保存失败！')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('监测项目未分析完，无需退回！')", true);
        }
    }
    protected void grdvw_ReportDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ImageButton btn_edit = e.Row.FindControl("btn_edit") as ImageButton;
        //    if (e.Row.Cells[9].Text.Trim() != "1")
        //    {

        //        btn_edit.Visible =false;

        //    }
        //    else
        //        btn_edit.Visible =true;
        //}
    }
    //保存现场分析数据
    protected void btn_datasave_Click(object sender, EventArgs e)
    {
       
        List<Entity.SampleItem> entitylist = new List<Entity.SampleItem>();
        Entity.SampleItem entity = new Entity.SampleItem();
        entity.ID = int.Parse(hid_ID.Value);//监测项记录ID
        //entity.lyID = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().ToString();//监测项记录ID
        //分析值
       
        if (txt_value.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析值不能为空！')", true);
            return;
        }
        entity.Value = txt_value.Text.Trim();

        entity.Remark = "";
        entity.Method = rbl_method.SelectedValue;
        //分析登记人
        entity.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        //分析数据登记时间
        entity.AnalysisDate = DateTime.Now;
        entitylist.Add(entity);
        DAl.DrawSample itemObj = new DAl.DrawSample();
        if (itemObj.DataSave(entitylist) == 1)
        {
            panel_data.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('现场分析数据保存成功！')", true);
            
        ReportSelectQuery();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('现场分析数据保存失败！')", true);
        }
    }
    protected void grdvw_ReportDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        txt_SampleID.Text = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        hid_ID.Value = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        hidden_ItemID.Value = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[11].Text.Trim();
        DAl.Item getobj = new DAl.Item();
       //DataTable method= getobj.GetMethod(hidden_ItemID.Value);
       //rbl_method.DataSource = method;
       //rbl_method.DataTextField = "Standard";
       //rbl_method.DataValueField = "id";
       //rbl_method.DataBind();
        panel_data.Visible = true;
        txt_Item.Text = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        hidden_ItemID.Value= grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[11].Text.Trim();
        if (grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[10].Text.Trim() != "&nbsp;")
            txt_value.Text = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[10].Text.Trim();
        else
            txt_value.Text = "";
        if (grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[12].Text.Trim() != "&nbsp;")
            rbl_method.SelectedValue = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[12].Text.Trim();
        else
            rbl_method.SelectedIndex = 0;
    }
   
    protected void btn_xcclose_Click(object sender, ImageClickEventArgs e)
    {
        panel_data.Visible = false;
    }
    protected void grdvw_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        strReportId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('attachment.aspx?kw=" + strReportId + "&&file=" + HttpUtility.UrlEncode("ReportSampleQuery.aspx") + "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);

        ReportSelectQuery();
    }
}