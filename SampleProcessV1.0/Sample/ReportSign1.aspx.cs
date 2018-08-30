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

using WebApp.Components;

public partial class Sample_ReportSign1 : System.Web.UI.Page
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
    private string strItemId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strItemId"]; }
        set { ViewState["strItemId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:calendar()");
            TextBox2.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_signtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
             
           TextBox2.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");

           SetButton();
            ReportQuery();

            grdvw_Report.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>报告签发</b></font>"; 
        }
    }
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            Button3.Enabled = false;
            Button4.Enabled = false;
            Button5.Enabled = false;
        }
        else
        {
            Button3.Enabled = true;
            Button4.Enabled = true;
            Button5.Enabled = true;
            // btn_AddSample.Enabled = true;
        }
    }
    #region 样品列表
    //private void MainQuery()
    //{
    //    string strSql = "select t_M_SampleInfor.ReportID,t_M_SampleInfor.ItemType,ItemName 项目类型, from t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_SampleInfor.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID and (StatusID=1 or ((StatusID=3 or StatusID=2)  and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))) order by t_M_SampleInfor.id ";

    //    DataSet ds = new MyDataOp(strSql).CreateDataSet();
    //    DataColumn dc = new DataColumn("是否上传报告");
    //    ds.Tables[0].Columns.Add(dc);
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        if (dr["ReportAddress"].ToString() == "")
    //            dr["是否上传报告"] = "否";
    //        else
    //            dr["是否上传报告"] = "是";

    //    }
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        //没有记录仍保留表头
    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        grdvw_List.DataSource = ds;
    //        grdvw_List.DataBind();
    //        int intColumnCount = grdvw_List.Rows[0].Cells.Count;
    //        grdvw_List.Rows[0].Cells.Clear();
    //        grdvw_List.Rows[0].Cells.Add(new TableCell());
    //        grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
    //    }
    //    else
    //    {
    //        grdvw_List.DataSource = ds;
    //        grdvw_List.DataBind();
    //    }
    //}

//    private void Query()
//    {
//        string strSql = "SELECT t_M_ReporInfo.ReportName AS 报告标识, t_M_SampleInfor.id,"+ 
//      "t_M_SampleInfor.AccessDate AS 接样时间, t_M_SampleInfor.ItemType,"+ 
//     " ItemName AS 项目类型, t_M_SampleInfor.SampleID AS 样品编号, "+
//      " t_M_SampleInfor.TypeID, t_M_SampleType.SampleType AS 样品类型, "+
//      " t_M_ReporInfo.ClientID, t_M_ClientInfo.ClientName AS 委托单位, "+
//     " t_M_ReporInfo.urgent, t_M_SampleInfor.ReportRemark AS 备注, " +
//     " t_M_SampleInfor.StatusID" +
//" FROM t_M_SampleInfor INNER JOIN"+
//     " t_M_ItemInfo ON t_M_SampleInfor.ItemType = t_M_ItemInfo.ItemID INNER JOIN"+
//     " t_M_SampleType ON "+
//    "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID INNER JOIN"+
//    "  t_M_ReporInfo INNER JOIN"+
//     " t_M_ClientInfo ON t_M_ReporInfo.ClientID = t_M_ClientInfo.id ON "+
//    "  t_M_SampleInfor.ReportID = t_M_ReporInfo.id"+
//" WHERE (t_M_SampleInfor.StatusID = 1) OR"+
//     " (t_M_SampleInfor.StatusID = 2)"+
//" ORDER BY t_M_SampleInfor.id";
//            // string strSql = "select ReportName 报告标识,t_M_SampleInfor.id,AccessDate 接样时间,t_M_SampleInfor.ItemType,ItemName 项目类型,SampleID 样品编号,t_M_SampleInfor.TypeID,t_M_SampleType.SampleType 样品类型,ClientID,ClientName 委托单位,urgent,ReportRemark 备注,StatusID from t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_SampleInfor.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID and (StatusID=1 or StatusID=2) order by t_M_SampleInfor.id ";// or ((StatusID=2)  and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))

//        DataSet ds = new MyDataOp(strSql).CreateDataSet();
//        //DataColumn dc = new DataColumn("是否上传报告");
//        //ds.Tables[0].Columns.Add(dc);
//        DataColumn dcc = new DataColumn("紧急程度");
//        ds.Tables[0].Columns.Add(dcc);
//        DataColumn dccc = new DataColumn("分析报告状态");
//        ds.Tables[0].Columns.Add(dccc);
//        foreach (DataRow dr in ds.Tables[0].Rows)
//        {
//            //if (dr["ReportAddress"].ToString() == "")
//            //    dr["是否上传报告"] = "否";
//            //else
//            //    dr["是否上传报告"] = "是";
//            if (dr["urgent"].ToString() == "0")
//                dr["紧急程度"] = "一般";
//            else
//                dr["紧急程度"] = "紧急";
//            if (dr["StatusID"].ToString() == "1")
//                dr["分析报告状态"] = "未完成";
//            else
//                dr["分析报告状态"] = "完成";



//        }
//        if (ds.Tables[0].Rows.Count == 0)
//        {
//            //没有记录仍保留表头
//            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
//            grdvw_List.DataSource = ds;
//            grdvw_List.DataBind();
//            int intColumnCount = grdvw_List.Rows[0].Cells.Count;
//            grdvw_List.Rows[0].Cells.Clear();
//            grdvw_List.Rows[0].Cells.Add(new TableCell());
//            grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
//        }
//        else
//        {
//            grdvw_List.DataSource = ds;
//            grdvw_List.DataBind();

//        } 
//    }
    //查询出报告列表
    private void ReportQuery()
    {
        string strSql = "select ReportName 报告标识,t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ClientID,ClientName 区域,ReportAddress,urgent 备注,ReportRemark 报告备注,StatusID,ReportWriteDate,ReportWriteRemark,ReportWriteUserID,ReportProofDate,ReportProofRemark,ReportProofRemark,ReportCheckDate,ReportProofUserID,Ulevel,ReportNumber 报告编号, wtdepart 委托单位 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and  (StatusID=4) order by  t_M_ReporInfo.id ";// or ((StatusID=2)  and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))

       DataSet ds = new MyDataOp(strSql).CreateDataSet();
        //DataColumn dc = new DataColumn("是否上传报告");
        //ds.Tables[0].Columns.Add(dc);
       DataColumn dcc = new DataColumn("紧急程度");
       ds.Tables[0].Columns.Add(dcc);
        DataColumn dccc = new DataColumn("报告审核状态");
        ds.Tables[0].Columns.Add(dccc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //if (dr["ReportAddress"].ToString() == "")
            //    dr["是否上传报告"] = "否";
            //else
            //    dr["是否上传报告"] = "是";
            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急";
            else
                dr["紧急程度"] = "一般";
            if (int.Parse(dr["StatusID"].ToString())==4)
                dr["报告审核状态"] = "完成";
            else if (int.Parse(dr["StatusID"].ToString()) < 4)
                dr["报告审核状态"] = "未完成";



        }
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportWriteUserID"] = drr["Name"].ToString();
                if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportProofUserID"] = drr["Name"].ToString();
                //if (dr["ReportCheckUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportCheckUserID"] = drr["Name"].ToString();
            }
        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_Report.DataSource = ds;
            grdvw_Report.DataBind();
            int intColumnCount = grdvw_Report.Rows[0].Cells.Count;
            grdvw_Report.Rows[0].Cells.Clear();
            grdvw_Report.Rows[0].Cells.Add(new TableCell());
            grdvw_Report.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_Report.DataSource = ds;
            grdvw_Report.DataBind();

        }
    }
    
    #region GridView相关事件响应函数
    protected void grdvw_ReportDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ReportDetail.PageIndex = e.NewPageIndex;
        ReportSelectQuery();
    }
    protected void grdvw_Report_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_Report.PageIndex = e.NewPageIndex;
        ReportQuery();
    }
    
    protected void grdvw_ReportDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            

            TableCell headerset = new TableCell();
            headerset.Text = "详细";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "报告上传";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);
            //TableCell headerDel = new TableCell();
            //headerDel.Text = "删除";
            //headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDel.Width = 30;
            //e.Row.Cells.Add(headerDel);
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

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;

            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
        }
    }
    protected void grdvw_Report_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {



            TableCell headerset = new TableCell();
            headerset.Text = "报告签发";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "报告上传";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);
           
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

            //TableCell MenuSet = new TableCell();
            //MenuSet.Width = 60;
            //MenuSet.Style.Add("text-align", "center");
            //ImageButton btMenuSet = new ImageButton();
            //btMenuSet.ImageUrl = "~/images/Upload.gif";
            //btMenuSet.CommandName = "Edit";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuSet.Controls.Add(btMenuSet);
            //e.Row.Cells.Add(MenuSet);

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[8].Visible = false;

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
            
        }
    }
   
   
    #endregion

    #region 其它函数


    private string Verify()
    {
        string strErrorInfo = "";
        if (txt_SampleID.Text.Trim() == "")
        {
            strErrorInfo += "样品编码不能为空！\\n";
        }
        else
        {
            string str = "select * from t_M_SampleInfor where SampleID='" + txt_SampleID.Text.Trim() + "'";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
               strErrorInfo= "样品编号不能重复!";
            else
                strErrorInfo= "";
        }
        return strErrorInfo;
    }
   
    #endregion

    //选中某个报告，某个报告的样品单列表
    protected void grdvw_Report_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        //grdvw_ListAnalysisItem.Visible = false;
        //grdvw_ListR.Visible = true;
        string ReportID = grdvw_Report.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        //strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        //strSampleId = grdvw_List.Rows[e.NewSelectedIndex].Cells[6].Text.Trim();
        txt_ReportID.Text = "";
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[1].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
       // txt_ReportID.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        strReportId = grdvw_Report.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        txt_ReportID.ReadOnly = true;
        txt_CreateDate.ReadOnly = true;
        //lbl_Type.Text = "报告编制";
        //LabMessage1.Text = "";
        //LabMessage2.Text = "";
        Label8.Text = "报告签发";
        txt_CreateDate.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[3].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEditAnalysis();", true);
        txt_itemname.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        txt_itemname.ReadOnly = true;
       if( grdvw_Report.Rows[e.NewSelectedIndex].Cells[21].Text.Trim()!= "&nbsp;")
        txt_Client.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[21].Text.Trim();
        else
        txt_Client.Text ="";
        txt_Client.ReadOnly = true;
        
        txt_ReportRemark.Text = "";
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[10].Text.Trim() != "&nbsp;")
        txt_ReportRemark.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[10].Text.Trim();
        //txt_CheckRemark.Text = "";
        //txt_checktime.Text = "";
        txt_VerifyTime.Text="";
        txt_VerifyRemark.Text="";
        //if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[13].Text.Trim() != "&nbsp;")
        //    txt_CheckRemark.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[13].Text.Trim();
        //if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[12].Text.Trim() != "&nbsp;")
        //    txt_checktime.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[12].Text.Trim();
        //txt_person.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[14].Text.Trim();
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[15].Text.Trim() != "&nbsp;")
            txt_VerifyTime.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[15].Text.Trim();
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[16].Text.Trim() != "&nbsp;")
            txt_VerifyRemark.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[16].Text.Trim();
        txt_signremark.Text = "";
        txt_verfiyName.Text = "";
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[17].Text.Trim() != "&nbsp;")
            txt_signremark.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
        txt_signtime.Text = "";
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[18].Text.Trim() != "&nbsp;")
            txt_signtime.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[18].Text.Trim();
        //if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[18].Text.Trim() != "&nbsp;")
        //    txt_signtime.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[18].Text.Trim();
        txt_verfiyName.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[19].Text.Trim();
        ReportSelectQuery();

    }
    

    #endregion

    //查询出选中的报告的样品列表
    private void ReportSelectQuery()
    {
        string strSql = "SELECT t_M_ReporInfo.ReportName AS 报告标识, t_M_SampleInfor.id," +
      "t_M_SampleInfor.AccessDate AS 接样时间, t_M_ReporInfo.ItemType," +
     " ItemName AS 项目类型, t_M_SampleInfor.SampleID AS 样品编号, " +
      " t_M_SampleInfor.TypeID, t_M_SampleType.SampleType AS 样品类型, " +
      " t_M_ReporInfo.ClientID, t_M_ClientInfo.ClientName AS 委托单位, " +
     " t_M_ReporInfo.urgent 备注, t_M_SampleInfor.ReportRemark AS 报告备注, " +
     " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID" +
" FROM t_M_ReporInfo inner join t_M_SampleInfor on t_M_SampleInfor.ReportID = t_M_ReporInfo.id INNER JOIN" +
     " t_M_ItemInfo ON t_M_ReporInfo.ItemType = t_M_ItemInfo.ItemID INNER JOIN" +
     " t_M_SampleType ON " +
    "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID INNER JOIN" +

     " t_M_ClientInfo ON t_M_ReporInfo.ClientID = t_M_ClientInfo.id " +
  
" WHERE " +
     //" (t_M_SampleInfor.StatusID<= 2) and" +
     "  t_M_SampleInfor.ReportID=" + strReportId +
" ORDER BY t_M_SampleInfor.id";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        //DataColumn dc = new DataColumn("是否上传报告");
        //ds.Tables[0].Columns.Add(dc);
        //DataColumn dcc = new DataColumn("紧急程度");
        //ds.Tables[0].Columns.Add(dcc);
        DataColumn dccc = new DataColumn("样品单状态");
        ds.Tables[0].Columns.Add(dccc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //if (dr["ReportAddress"].ToString() == "")
            //    dr["是否上传报告"] = "否";
            //else
            //    dr["是否上传报告"] = "是";
            //if (dr["Ulevel"].ToString() == "1")
            //    dr["紧急程度"] = "紧急";
            //else
            //    dr["紧急程度"] = "一般";
            if (int.Parse(dr["StatusID"].ToString())<=1)
                dr["样品单状态"] = "未完成";
            else
                dr["样品单状态"] = "完成";



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
    //某个样品的分析项目列表
    protected void grdvw_ReportDetail_RowSelecting(object sender, GridViewSelectEventArgs e)
    {

        strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        strSampleId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[6].Text.Trim();
        txt_SampleID.Text = strSampleId;
        txt_SampleID.ReadOnly = true;
        txt_AccessTime.ReadOnly = true;
        lbl_Type.Text = "详细";
        //LabMessage1.Text = "";
        //LabMessage2.Text = "";
        txt_doremark.Text = "";
        txt_AccessTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[3].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
        txt_ItemList.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        txt_ItemList.ReadOnly = true;
        txt_SampleType.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[8].Text.Trim();
        txt_SampleType.ReadOnly = true;
        if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[12].Text.Trim() != "&nbsp;")
            txt_doremark.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[12].Text.Trim();
      
        queryAnalysisItem();

    }
    //查询出某个样品的分析项目列表
    protected void queryAnalysisItem()
    {
        string sql = "select t_M_MonitorItem.id,MonitorItem,t_M_MonitorItem.SampleID,t_M_SampleInfor.SampleID 样品编号,AIName 分析项目 ,Num 数量,ReportDate  收到时间,flag,Remark 备注 from t_M_MonitorItem,t_M_AnalysisItem,t_M_SampleInfor where t_M_SampleInfor.id=t_M_MonitorItem.SampleID and t_M_MonitorItem.MonitorItem=t_M_AnalysisItem.id  and t_M_SampleInfor.id='" + strSelectedId + "' order by t_M_SampleInfor.SampleID";
        DataSet ds = new MyDataOp(sql).CreateDataSet();
        DataColumn dc = new DataColumn("状态");
        ds.Tables[0].Columns.Add(dc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["flag"].ToString() == "0")
                dr["状态"] = "未完成";
            else
                dr["状态"] = "已完成";

        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_ListAnalysisItem.DataSource = ds;
            grdvw_ListAnalysisItem.DataBind();
            int intColumnCount = grdvw_ListAnalysisItem.Rows[0].Cells.Count;
            grdvw_ListAnalysisItem.Rows[0].Cells.Clear();
            grdvw_ListAnalysisItem.Rows[0].Cells.Add(new TableCell());
            grdvw_ListAnalysisItem.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else {
            grdvw_ListAnalysisItem.DataSource = ds;
            grdvw_ListAnalysisItem.DataBind();
        }
        grdvw_ListAnalysisItem.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #3333FF;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>" + strSampleId + "的分析项目列表</b></font>";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        
        ReportSelectQuery();
    }
    #region 项目列表
    //分析项目列表
    protected void grdvw_ListAnalysisItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "填写收到时间";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

            ////手动添加详细和删除按钮
            //TableCell tabcDetail = new TableCell();
            //tabcDetail.Width = 60;
            //tabcDetail.Style.Add("text-align", "center");
            //ImageButton ibtnDetail = new ImageButton();
            //ibtnDetail.ImageUrl = "~/images/Detail.gif";
            //ibtnDetail.CommandName = "Edit";
            //tabcDetail.Controls.Add(ibtnDetail);
            //e.Row.Cells.Add(tabcDetail);

           

         
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
           
            //////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[8].Visible = false;
            


        }
    }
   
    ////填写某个分析项目报告收到时间
    //protected void grdvw_ListAnalysisItem_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    lbl_AnalysisName.Text = "报告校核";
    //    txt_Remark.Text = "";
    //    txt_detailNum.Text = "";
    //    txt_ReportTime.Text = "";
       
    //    txt_detailNum.Text = grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[6].Text;
    //    if (grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[7].Text != "&nbsp;")
    //        txt_ReportTime.Text = grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[7].Text;
    //    else
    //        txt_ReportTime.Text = "";
    //    strItemId = grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[1].Text;
    //    if (grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[5].Text!="&nbsp;")
    //    txt_AnalysisItem.Text = grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[5].Text;
    //    txt_AnalysisItem.ReadOnly = true;
    //    txt_detailNum.ReadOnly = true;
    //    if (grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[9].Text != "&nbsp;")
    //        txt_Remark.Text = grdvw_ListAnalysisItem.Rows[e.NewEditIndex].Cells[9].Text;
    //    back_flag.Checked = false;
       
      
      
    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);
        
    //    queryAnalysisItem();
    //}
    //分析项目列表退出
    protected void btn_ExitAnalysisItem_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
       
        queryAnalysisItem();
    }
    
    #endregion
    ////分析报告处理中提交报告编制
    //protected void btn_SampleSave_Click(object sender, EventArgs e)
    //{
    //    //分析报告未收到或有被退回到中分室，0-分析报告未完成，1-分析报告已完成
    //    string strQuery = "select * from t_M_MonitorItem where flag=0 and SampleID='" + strSelectedId + "'";
    //    DataSet ds = new MyDataOp(strQuery).CreateDataSet();
    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        string strSql="";
    //        string strQuery1 = "select * from t_M_BackInfo where (flag=0) and SampleID=" + strSelectedId + "";
    //        DataSet ds1 = new MyDataOp(strQuery1).CreateDataSet();
    //        if (ds1.Tables[0].Rows.Count == 0)
    //        {

    //            strSql = @"update t_M_SampleInfor set StatusID=2 where id='" + strSelectedId + "'";
    //        }
    //        else
    //        {
    //            strSql = @"update t_M_BackInfo set flag=1 , updateUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',UpdateDate=getdate() where SampleID='" + strSelectedId + "'";
    //        }
    //        MyDataOp mdo = new MyDataOp(strSql);
    //        bool blSuccess = mdo.ExecuteCommand();
    //        if (blSuccess == true)
    //        {
    //            Log.SaveLog("报告编制中提交保存:" + strSampleId + "（" + strSelectedId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
    //        }
    //        else
    //        {
    //            Log.SaveLog("报告编制中提交保存:" + strSampleId + "（" + strSelectedId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('该样品报告有未完成的分析报告！');", true);
    //    }
    //    queryAnalysisItem();
    //       // Query();
        
    //}
    //#region 填写单个分析项目收到时间及需要回退的备注
    //protected void btn_OKAnalysis_Click(object sender, EventArgs e)
    //{
    //    string strSql = "";
    //    int i = 0;
    //    string[] arrlist = new string[3];
    //    if (txt_ReportTime.Text.Trim() == "")
    //    {
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clicktime", "请先填写收到时间！;", true);
    //    }
    //    else
    //    {
    //        if (back_flag.Checked)
    //        {

    //            if (txt_Remark.Text.Trim() == "")
    //            {
    //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickreason", "请填写回退原因！;", true);
    //            }
    //            else
    //            {//回退给中分室，虽然已填写日期，可是样品列表中不变色
    //                strSql = @"update t_M_MonitorItem set  ReportDate=null, Remark='" + txt_Remark.Text.Trim() + "记录时间：" + txt_ReportTime.Text.Trim() + "',flag=0 where id='" + strItemId + "'";
    //                arrlist.SetValue(strSql, i++);
    //                strSql = @"Insert into t_M_analysisBackinfo (MonitorID,CreateDate,Remark,UserID) values('" + strItemId + "',getdate(),'" + txt_Remark.Text.Trim() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
    //                arrlist.SetValue(strSql, i++);
    //                strSql = @"update t_M_SampleInfor set  StatusID=0 where id='" + strItemId + "'";
    //                arrlist.SetValue(strSql, i++);
    //                MyDataOp mdo = new MyDataOp(strSql);
    //                bool blSuccess = mdo.DoTran(i, arrlist);
    //                if (blSuccess == true)
    //                {
    //                    Log.SaveLog("报告编制中分析报告回退保存报告时间:" + strSampleId + "（" + strSelectedId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据编辑成功！');", true);
    //                }
    //                else
    //                {
    //                    Log.SaveLog("报告编制中分析报告回退保存报告时间:" + strSampleId + "（" + strSelectedId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据编辑失败！');", true);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            #region 编辑记录
    //            //if (lbl_AnalysisName.Text == "分析项目编辑")
    //            //{
    //            strSql = @"update t_M_MonitorItem set  ReportDate='" + txt_ReportTime.Text.Trim() +
    //                                 "',Remark='" + txt_Remark.Text.Trim() + "',flag=1 where id='" + strItemId + "'";
    //            arrlist.SetValue(strSql, i++);
    //            MyDataOp mdo = new MyDataOp(strSql);
    //            bool blSuccess = mdo.DoTran(i, arrlist);
    //            if (blSuccess == true)
    //            {
    //                Log.SaveLog("报告编制中保存报告时间:" + strSampleId + "（" + strSelectedId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据编辑成功！');", true);
    //            }
    //            else
    //            {
    //                Log.SaveLog("报告编制中保存报告时间:" + strSampleId + "（" + strSelectedId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据编辑失败！');", true);
    //            } 
    //            #endregion
    //        }

    //    }



    //    queryAnalysisItem();
       

    //}
    //填写某个分析报告的
    protected void grdvw_ListAnalysisItem_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);
    }
    protected void grdvw_ListAnalysisItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grdvw_ListAnalysisItem.PageIndex = e.NewPageIndex;
        queryAnalysisItem();


    }
    protected void btn_CancelAnalysis_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();", true);

        queryAnalysisItem();
    }
    //报告编制中的取消
    protected void btn_CancelReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();", true);

        queryAnalysisItem();
    }
    
    protected void grdvw_ListAnalysisItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        string strSql;
        string[] deletelist = new string[1];
        strSql = "DELETE FROM t_M_MonitorItem WHERE SampleID='" + strSelectedId + "'and id= '" + grdvw_ListAnalysisItem.Rows[e.RowIndex].Cells[1].Text + "'";
        //待修改，改项目删除后，相应要修改的信息

       
        deletelist.SetValue(strSql, 0);
        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(1, deletelist))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        queryAnalysisItem();
    }
    //#endregion

    protected void grdvw_ReportDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strSelectedId = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('attachment.aspx?kw=" + strSelectedId + "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);
    }
    //报告编制中的上传报告
    protected void grdvw_Report_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strSelectedId = grdvw_Report.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('attachment.aspx?kw=" + strSelectedId + "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);
    }
    //分析报告中的保存
    //protected void btn_Save_Click(object sender, EventArgs e)
    //{
       
        

    //    //strSql = @"update t_M_SampleInfor set ReportRemark='" + txt_doremark.Text.Trim() + "' where id='" + strSelectedId + "'";

    //    //分析报告未收到或有被退回到中分室，0-分析报告未完成，1-分析报告已完成
    //    string strQuery = "select * from t_M_MonitorItem where flag=0 and SampleID='" + strSelectedId + "'";
    //    DataSet ds = new MyDataOp(strQuery).CreateDataSet();
    //    string strSql = "";
    //    string[] strlist = new string[2];
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {

    //        //string strQuery1 = "select * from t_M_BackInfo where (flag=0) and SampleID=" + strSelectedId + "";
    //        //DataSet ds1 = new MyDataOp(strQuery1).CreateDataSet();
    //        //if (ds1.Tables[0].Rows.Count == 0)
    //        //{

    //        strSql = @"update t_M_ReporInfo set StatusID=1 where id='" + strReportId + "'";
    //        strlist.SetValue(strSql, 0);
    //        strSql = @"update t_M_SampleInfor set ReportRemark='" + txt_doremark.Text.Trim() + "',StatusID=1 where id='" + strSelectedId + "'";
    //        strlist.SetValue(strSql, 1);
    //        //}
    //        //else
    //        //{
    //        //    strSql = @"update t_M_BackInfo set flag=1 , updateUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',UpdateDate=getdate() where SampleID='" + strSelectedId + "'";
    //        //}
    //        MyDataOp mdo = new MyDataOp(strSql);
    //        bool blSuccess = mdo.DoTran(2, strlist);
    //        if (blSuccess == true)
    //        {
    //            Log.SaveLog("报告编制中分析报告处理保存:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存成功！');", true);
    //        }
    //        else
    //        {
    //            Log.SaveLog("报告编制中分析报告处理保存:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存失败！');", true);
    //        }
    //    }

    //    else
    //    {
    //        strSql = @"update t_M_SampleInfor set ReportRemark='" + txt_doremark.Text.Trim() + "' where id='" + strSelectedId + "'";
    //        strlist.SetValue(strSql, 0);

    //        //}
    //        //else
    //        //{
    //        //    strSql = @"update t_M_BackInfo set flag=1 , updateUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',UpdateDate=getdate() where SampleID='" + strSelectedId + "'";
    //        //}
    //        MyDataOp mdo = new MyDataOp(strSql);
    //        bool blSuccess = mdo.DoTran(1, strlist);
    //        if (blSuccess == true)
    //        {
    //            Log.SaveLog("报告编制中分析报告处理保存:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存成功！');", true);
    //        }
    //        else
    //        {
    //            Log.SaveLog("报告编制中分析报告处理保存:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存失败！');", true);
    //        }
    //    }
    //    ReportSelectQuery();

    //}
    //保存
    protected void btn_SaveReport_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string[] strlist = new string[2];
        if (txt_signtime.Text.Trim() != "")
        {
            string ddstr = "";
            if (txt_signremark.Text.Trim() != "")
                ddstr = "ReportProofRemark='" + txt_signremark.Text.Trim() + "记录时间：" + txt_signtime.Text.Trim() + "',";
            else
                ddstr = "ReportProofRemark=null,";
            //保存收到报告的时间
            strSql = @"update t_M_ReporInfo set  " + ddstr + "ReportCheckDate='" + txt_signtime.Text.Trim() + "',ReportCheckUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' where id='" + strReportId + "'";
            strlist.SetValue(strSql, 0);
            //strSql = @"update t_M_SampleInfor set StatusID=1 where id='" + strSelectedId + "'";
            //strlist.SetValue(strSql, 1);
            //}
            //else
            //{
            //    strSql = @"update t_M_BackInfo set flag=1 , updateUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',UpdateDate=getdate() where SampleID='" + strSelectedId + "'";
            //}
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.DoTran(1, strlist);
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLog("报告签发中保存:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 9);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('数据保存成功！');", true);
            }
            else
            {
                WebApp.Components.Log.SaveLog("报告签发中保存:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 9);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('数据保存失败！');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('请选择收到时间！');", true);
        }
        
       
        //else
        //{
        //   strSql = @"update t_M_ReporInfo set ReportRemark='" + txt_ReportRemark.Text.Trim() + "' where id='" + strReportId + "'";
        //    strlist.SetValue(strSql, 0);
           
        //    //}
        //    //else
        //    //{
        //    //    strSql = @"update t_M_BackInfo set flag=1 , updateUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',UpdateDate=getdate() where SampleID='" + strSelectedId + "'";
        //    //}
        //    MyDataOp mdo = new MyDataOp(strSql);
        //    bool blSuccess = mdo.DoTran(1, strlist);
        //    if (blSuccess == true)
        //    {
        //        Log.SaveLog("报告编制中保存:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('数据保存成功！');", true);
        //    }
        //    else
        //    {
        //        Log.SaveLog("报告编制中保存:(" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('数据保存成功！');", true);
        //    }
        //}
       // Query();
            ReportQuery();

    }
    //提交
    protected void btn_SampleReport_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string[] strlist = new string[2];
        if (txt_signtime.Text.Trim() != "")
        {
             string ddstr = "";
             if (txt_VerifyRemark.Text.Trim() != "")
                 ddstr = "ReportProofRemark='" + txt_signremark.Text.Trim() + "记录时间：" + txt_signtime.Text.Trim() + "',";
             else
                 ddstr = "ReportProofRemark=null,";
            //保存收到报告的时间
             strSql = @"update t_M_ReporInfo set StatusID=5, " + ddstr + "ReportCheckDate='" + txt_signtime.Text.Trim() + "',ReportCheckUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' where id='" + strReportId + "'";
            strlist.SetValue(strSql, 0);
            //strSql = @"update t_M_SampleInfor set StatusID=1 where id='" + strSelectedId + "'";
            //strlist.SetValue(strSql, 1);
            //}
            //else
            //{
            //    strSql = @"update t_M_BackInfo set flag=1 , updateUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',UpdateDate=getdate() where SampleID='" + strSelectedId + "'";
            //}
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.DoTran(1, strlist);
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLog("报告签发中保存:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 9);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('数据保存成功！');", true);
            }
            else
            {
                WebApp.Components.Log.SaveLog("报告签发中保存:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 9);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('数据保存失败！');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('请选择收到时间！');", true);
        }
        ReportQuery();
    }
    //回退
    protected void btn_BackReport_Click(object sender, EventArgs e)
    {
        string ReportStr = "update t_M_ReporInfo set ReportCheckDate=null,  ReportProofRemark='" + txt_signremark.Text.Trim() + "记录时间：" + txt_signtime.Text.Trim() + "',ReportCheckUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',StatusID=3 where id='" + strReportId + "'";
        string[] strlist = new string[2];
        strlist.SetValue(ReportStr, 0);
        ReportStr = @"Insert into t_M_Backinfo(SampleID,CreateDate,Remark,UserID,FuntionID) values('" + strReportId + "',getdate(),'" + txt_signremark.Text.Trim() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',4)";

        strlist.SetValue(ReportStr, 1);

        MyDataOp mdo = new MyDataOp(ReportStr);
        if (mdo.DoTran(2, strlist))
        {
            WebApp.Components.Log.SaveLog("报告签发中回退:（" + strReportId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 9);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert('数据保存成功！');", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("报告签发中回退:(" + strReportId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 9);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();alert(数据保存失败！');", true);
        }
        ReportQuery();
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        string strSample = "";
        string strDate = "";
        if (TextBox1.Text != "")
            strSample = "and SampleID like '%" + TextBox1.Text + "%'";

        if (TextBox2.Text != "")
            strDate = " and (year(AccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Day + "')";// or (StatusID=3 and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))
       // string strSql = "select ReportName 报告标识,t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ClientID,ClientName 委托单位,ReportAddress,urgent,ReportRemark 备注,StatusID,ReportWriteDate,ReportWriteRemark,ReportWriteUserID,ReportProofDate,ReportProofRemark,ReportProofRemark,ReportCheckDate  from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and  (StatusID=4) and t_M_ReporInfo.id in(Select distinct ReportID from t_M_SampleInfor where  (StatusID<=2) " + strSample + strDate + ") order by  t_M_ReporInfo.id ";// or ((StatusID=2)  and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))
        string strSql = "select ReportName 报告标识,t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ClientID,ClientName 区域,ReportAddress,urgent 备注,ReportRemark 报告备注,StatusID,ReportWriteDate,ReportWriteRemark,ReportWriteUserID,ReportProofDate,ReportProofRemark,ReportProofRemark,ReportCheckDate,ReportProofUserID,Ulevel,wtdepart 委托单位 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and  (StatusID=4) and t_M_ReporInfo.id in(Select distinct ReportID from t_M_SampleInfor where  (StatusID<=2) " + strSample + strDate + ") order by  t_M_ReporInfo.id";// or ((StatusID=2)  and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        //DataColumn dc = new DataColumn("是否上传报告");
        //ds.Tables[0].Columns.Add(dc);
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dccc = new DataColumn("报告审核状态");
        ds.Tables[0].Columns.Add(dccc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //if (dr["ReportAddress"].ToString() == "")
            //    dr["是否上传报告"] = "否";
            //else
            //    dr["是否上传报告"] = "是";
            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急"; 
            else
                dr["紧急程度"] = "一般";
            if (int.Parse(dr["StatusID"].ToString()) <= 1)
                dr["报告审核状态"] = "未完成";
            else
                dr["报告审核状态"] = "完成";



        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_Report.DataSource = ds;
            grdvw_Report.DataBind();
            int intColumnCount = grdvw_Report.Rows[0].Cells.Count;
            grdvw_Report.Rows[0].Cells.Clear();
            grdvw_Report.Rows[0].Cells.Add(new TableCell());
            grdvw_Report.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_Report.DataSource = ds;
            grdvw_Report.DataBind();

        }
    }
}