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

public partial class Sample_ReportQuery : System.Web.UI.Page
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
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_endtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_receivetime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_QueryEndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_ReportTime.Attributes.Add("OnFocus", "javascript:calendar()");
            //txt_AccessTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //txt_ReportTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClassEx order by ClassCode asc", DropList_AnalysisMainItem);
            ListItem choose = new ListItem("全部", "-1");
            DropList_AnalysisMainItem.SelectedItem.Selected = false;
            DropList_AnalysisMainItem.Items.Add(choose);
            DropList_AnalysisMainItem.Items.FindByValue("-1").Selected = true;
            DropList_AnalysisMainItem_SelectedIndexChanged(null, null);
            MyStaVoid.BindList("ItemName", "ItemID", "select ItemID, ItemName from t_M_ItemInfo  order by ItemName ", txt_item);
            txt_item.Items.Add(choose);
            txt_item.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("SampleType", "TypeID", "select TypeID, SampleType from t_M_SampleType  order by SampleType ", txt_type);
            txt_type.Items.Add(choose);
            txt_type.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("Status", "StatusID", "select StatusID, Status from t_M_StatusInfo  order by StatusID ", drop_status);
            drop_status.Items.Add(choose);
            drop_status.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("ClientName", "id", "select id,ClientName from t_M_ClientInfo  order by id ", drop_client);
            drop_client.Items.Add(choose);
            drop_client.Items.FindByValue("-1").Selected = true;

            //RadioButtonList1.SelectedValue = "1";

            btn_query_Click(null, null);
        }
    }
    #region 样品列表
   

    #region GridView相关事件响应函数
    protected void grdvw_ReportDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ReportDetail.PageIndex = e.NewPageIndex;
        ReportSelectQuery();
    }
    protected void grdvw_Report_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_Report.PageIndex = e.NewPageIndex;
        btn_query_Click(null,null);
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
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[11].Visible = false;
           
        }
    }
    protected void grdvw_Report_RowCreated(object sender, GridViewRowEventArgs e)
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
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[22].Visible = false;
            e.Row.Cells[23].Visible = false;
            e.Row.Cells[26].Visible = false;
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
                strErrorInfo = "样品编号不能重复!";
            else
                strErrorInfo = "";
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
        //txt_ReportID.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
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
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[25].Text.Trim() != "&nbsp;")
            txt_Client.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[25].Text.Trim();

        else
            txt_Client.Text = "";
        txt_Client.ReadOnly = true;

        txt_ReportRemark.Text = "";
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[10].Text.Trim() != "&nbsp;")
            txt_ReportRemark.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[10].Text.Trim();
        //txt_CheckRemark.Text = "";
        //txt_checktime.Text = "";
        txt_VerifyTime.Text = "";
        txt_VerifyRemark.Text = "";
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
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[17].Text.Trim() != "&nbsp;")
            txt_signremark.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
        txt_signtime.Text = "";
        txt_sign.Text = "";
        txt_sign.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[23].Text.Trim();
        txt_verfiyName.Text = "";
        txt_verfiyName.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[22].Text.Trim();
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[19].Text.Trim() != "&nbsp;")
            txt_signtime.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[19].Text.Trim();
        txt_endtime.Text = "";
        txt_receivetime.Text = "";
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[20].Text.Trim() != "&nbsp;")
            txt_receivetime.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[20].Text.Trim();
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[26].Text.Trim() != "&nbsp;")
            txt_endtime.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[26].Text.Trim();

        ReportSelectQuery();


    }


    #endregion

    //查询出选中的报告的样品列表
    private void ReportSelectQuery()
    {
        string strSql = "SELECT  t_M_SampleInfor.id,t_M_SampleInfor.SampleSource 样品来源,t_M_SampleInfor.SampleDate AS 采样时间,t_M_SampleInfor.SampleAddress 采样点 ,t_M_SampleInfor.SampleID AS 样品编号," +
     "t_M_SampleInfor.AccessDate AS 接样时间, " +
     " t_M_SampleInfor.TypeID, t_M_SampleType.SampleType AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, " +
    " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID,ReportRemark" +
" FROM  t_M_SampleInfor  INNER JOIN" +
    " t_M_SampleType ON " +
   "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID " +

" WHERE " +
    " t_M_SampleInfor.ReportID=" + strReportId +
" ORDER BY t_M_SampleInfor.id";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        string sql = "select t_M_SampleInfor.SampleID,t_MonitorItemDetail.MonitorItem,AIName,t_MonitorItemDetail.*  from t_M_SampleInfor  inner join  t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id inner join  t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem where t_MonitorItemDetail.bz=0 and t_M_SampleInfor.ReportID=" + strReportId;
        DataSet dsitem = new MyDataOp(sql).CreateDataSet();

        if (dsitem != null && dsitem.Tables.Count > 0)
        {
            foreach (DataRow drr in dsitem.Tables[0].Rows)
            {
                if (!ds.Tables[0].Columns.Contains(drr[2].ToString()))
                    ds.Tables[0].Columns.Add(drr[2].ToString());
            }
        }
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            for (int i = 12; i < ds.Tables[0].Columns.Count; i++)
            {
                DataColumn dc = ds.Tables[0].Columns[i];


                DataRow[] dritemlist = dsitem.Tables[0].Select("SampleID='" + dr["样品编号"].ToString() + "' and AIName='" + dc.ColumnName + "'");
                if (dritemlist.Length > 0)
                {
                    dr[dc.ColumnName] = dritemlist[0]["experimentvalue"].ToString();
                }
                else
                    dr[dc.ColumnName] = "-";


            }
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
        btn_query_Click(null,null);
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
        string sql = "select t_M_MonitorItemEx.id,MonitorItem,t_M_MonitorItemEx.SampleID,t_M_SampleInfor.SampleID 样品编号,AIName 分析项目 ,Num 数量,ReportDate  收到时间,flag,Remark 备注 from t_M_MonitorItemEx,t_M_AnalysisItem,t_M_SampleInfor where t_M_SampleInfor.id=t_M_MonitorItemEx.SampleID and t_M_MonitorItemEx.MonitorItem=t_M_AnalysisItem.id  and t_M_SampleInfor.id='" + strSelectedId + "' order by t_M_SampleInfor.SampleID";
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
        else
        {
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

   
    //分析项目列表退出
    protected void btn_ExitAnalysisItem_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);

        queryAnalysisItem();
    }

    #endregion
   
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

        btn_query_Click(null,null);
    }

   
   

    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
        string strSample = "";//样品编号
        string strDate = "";//接样时间
        string strStatus = "";//样品状态
        string strItem = "";//项目类型
        string strType = "";//样品名称
        string strClient = "";//委托单位
        string strReportName = "";// 报告标识
        string strAanlysisStatus = "";//样品分析状态
        string strUrgent = "";//按紧急程度
        if (Drop_Urgent.SelectedValue != "-1")
        {
            if (Drop_Urgent.SelectedValue == "1")
                strUrgent = "and t_M_ReporInfo.Ulevel=1";
            else
                strUrgent = "and (t_M_ReporInfo.Ulevel<>1 or t_M_ReporInfo.Ulevel is null)";
        }
        if (txt_samplequery.Text != "")//按样品编号

            strSample = "and t_M_SampleInfor.SampleID like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text.Trim() != "" && txt_QueryEndTime.Text.Trim() != "")//按采样时间
        {
            DateTime start = DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00");
            DateTime end = DateTime.Parse(txt_QueryEndTime.Text.Trim() + " 23:59:59");
            strDate = " and AccessDate between '" + start + "' and '" + end + "'";
            //strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        }

        if (txt_ReportName.Text.Trim() != "")
        { strReportName = " and t_M_ReporInfo.ReportName like'%" + txt_ReportName.Text.Trim() + "%'"; }
        if (drop_status.SelectedValue != "-1")//按样品状态
        {
            strStatus = " and t_M_ReporInfo.StatusID='" + drop_status.SelectedValue + "' ";
        }
        if (txt_item.SelectedValue != "-1")// 按项目类型
        {
            strItem = " and t_M_ReporInfo.ItemType='" + txt_item.SelectedValue + "' ";
        }
        if (txt_type.SelectedValue != "-1")//按样品类型
        {
            strType = " and t_M_SampleInfor.TypeID='" + txt_type.SelectedValue + "' ";
        }
        if (drop_client.SelectedValue != "-1")//按委托单位
        {
            strClient = " and t_M_ReporInfo.ClientID='" + drop_client.SelectedValue + "' ";
        }
        if (drop_analysisstatus.SelectedValue != "-1")
        {
            strAanlysisStatus = " and t_M_SampleInfor.id in (select SampleID from  t_M_MonitorItem where flag=" + drop_analysisstatus.SelectedValue + ")";

        }
       //ReportName 报告标识,t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ClientID,ClientName 区域,ReportAddress,urgent 备注,ReportWriteRemark 报告备注,StatusID,ReportWriteDate,ReportWriteRemark,ReportWriteUserID,ReportCheckDate,ReportProofRemark,ReportSignRemark,ReportSignDate,ReportProofUserID,Ulevel,ReportNumber 报告编号, wtdepart 委托单位

        string strSql = "select distinct t_M_ReporInfo.ReportName 报告标识,t_M_ReporInfo.id,t_M_ReporInfo.CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,t_M_ReporInfo.ClientID,ClientName 区域,t_M_ReporInfo.ReportAddress,t_M_ReporInfo.urgent 备注,t_M_ReporInfo.ReportWriteRemark 报告备注,t_M_ReporInfo.StatusID,t_M_ReporInfo.ReportWriteDate,t_M_ReporInfo.ReportWriteRemark,t_M_ReporInfo.ReportWriteUserID,t_M_ReporInfo.ReportProofDate,t_M_ReporInfo.ReportProofRemark,ReportSignRemark,t_M_ReporInfo.ReportSignRemark,t_M_ReporInfo.ReportCheckDate,t_M_ReporInfo.ReportSignDate,t_M_ReporInfo.ReportBindDate,t_M_ReporInfo.ReportProofUserID,t_M_ReporInfo.ReportSignUserID,Status 报告状态,wtdepart  委托单位,ReportBindReceiveDate from t_M_ReporInfo,t_M_SampleInfor,  t_M_ItemInfo,t_M_ClientInfo,t_M_StatusInfo where t_M_SampleInfor.ReportID=t_M_ReporInfo.id and t_M_StatusInfo.StatusID=t_M_ReporInfo.StatusID and t_M_ClientInfo.id=t_M_ReporInfo.ClientID and  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID  " + strUrgent + strSample + strDate + strStatus + strItem + strType + strClient + strAanlysisStatus + strReportName + " order by t_M_ReporInfo.id  desc ";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();

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
                if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportSignUserID"] = drr["Name"].ToString();
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
    protected void DropList_AnalysisMainItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds;
        string str;
        if (DropList_AnalysisMainItem.SelectedValue.ToString() == "-1")
        {
            str = "select id,AIName from t_M_AnalysisItemEx";
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        else
        {
            str = "select id,AIName from t_M_AnalysisItemEx where ClassID='" + DropList_AnalysisMainItem.SelectedValue.ToString() + "'";
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
       

    }
   
   
}