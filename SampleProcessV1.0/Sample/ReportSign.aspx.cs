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

public partial class Sample_ReportSign : System.Web.UI.Page
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
             
          // TextBox2.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");

           SetButton();
            ReportQuery();

            grdvw_Report.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>报告批准</b></font>"; 
        }
    }
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            Button3.Enabled = false;
           // Button4.Enabled = false;
            Button5.Enabled = false;
        }
        else
        {
            Button3.Enabled = true;
            //Button4.Enabled = true;
            Button5.Enabled = true;
            // btn_AddSample.Enabled = true;
        }
    }
    #region 样品列表
   
    //查询出报告列表
    private void ReportQuery()
    {
        string strSample = "";
        string strDate = "";
        if (TextBox1.Text != "")
            strSample = "and ReportNumber like '%" + TextBox1.Text + "%'";

        if (TextBox2.Text != "")
            strDate = " and (year(AccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(TextBox2.Text.Trim() + " 00:00:00").Day + "')";// or (StatusID=3 and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))

        string strSql = "select ReportName 报告标识,t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ClientID,ClientName 区域,ReportAddress,urgent 备注,ReportWriteRemark 报告备注,StatusID,ReportWriteDate,ReportWriteRemark,ReportWriteUserID,ReportCheckDate,ReportProofRemark,ReportSignRemark,ReportSignDate,ReportProofUserID,Ulevel,ReportNumber 报告编号, wtdepart 委托单位 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and  (StatusID=4) " + strSample + "and t_M_ReporInfo.id in(Select distinct ReportID from t_M_SampleInfor where  (StatusID=3) " + strDate + ") order by  t_M_ReporInfo.id ";// or ((StatusID=2)  and t_M_SampleInfor.id  in (select SampleID from t_M_BackInfo where flag=0))

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
                //if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportSignUserID"] = drr["Name"].ToString();
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
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
    }
    protected void grdvw_Report_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {



            TableCell headerset = new TableCell();
            headerset.Text = "报告批准";
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

    //选中某个报告，某个报告的样品单列表
    protected void grdvw_Report_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        string ReportID = grdvw_Report.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
              txt_ReportID.Text = "";
        if (grdvw_Report.Rows[e.NewSelectedIndex].Cells[1].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
          strReportId = grdvw_Report.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        txt_ReportID.ReadOnly = true;
        txt_CreateDate.ReadOnly = true;
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
         txt_VerifyTime.Text="";
        txt_VerifyRemark.Text="";
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
         txt_verfiyName.Text = grdvw_Report.Rows[e.NewSelectedIndex].Cells[19].Text.Trim();
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
        string sql = "select t_M_SampleInfor.SampleID,t_DrawSampleDetail.ItemID,AIName,t_DrawSampleDetail.*  from t_M_SampleInfor inner join t_DrawSample on t_DrawSample.SampleId=t_M_SampleInfor.id  inner join  t_DrawSampleDetail on t_DrawSampleDetail.DrawID=t_DrawSample.ID inner join  t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=ItemID where t_M_SampleInfor.ReportID=" + strReportId;
     
       // string sql = "select t_M_SampleInfor.SampleID,t_MonitorItemDetail.MonitorItem,AIName,t_MonitorItemDetail.*  from t_M_SampleInfor  inner join  t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.SampleID inner join  t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem where t_MonitorItemDetail.bz=0 and t_M_SampleInfor.ReportID=" + strReportId;
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
                    dr[dc.ColumnName] = dritemlist[0]["value"].ToString();
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
        ReportQuery();
    }
   
   
   
    //报告编制中的取消
    protected void btn_CancelReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();", true);

        ReportQuery();
    }
    
   
  
    //报告编制中的上传报告
    protected void grdvw_Report_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strSelectedId = grdvw_Report.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('attachment.aspx?kw=" + strSelectedId + "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);
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
                 ddstr = "ReportSignRemark='" + txt_signremark.Text.Trim() + "记录时间：" + txt_signtime.Text.Trim() + "',";
             else
                 ddstr = "ReportSignRemark=null,";
            //保存收到报告的时间
             strSql = @"update t_M_ReporInfo set StatusID=5, " + ddstr + "ReportSignDate='" + txt_signtime.Text.Trim() + "',ReportSignUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' where id='" + strReportId + "'";
            strlist.SetValue(strSql, 0);
           
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择收到时间！');", true);
        }
        ReportQuery();
    }
    //回退
    protected void btn_BackReport_Click(object sender, EventArgs e)
    {
        string ReportStr = "update t_M_ReporInfo set ReportSignDate=null,  ReportSignRemark='" + txt_signremark.Text.Trim() + "记录时间：" + txt_signtime.Text.Trim() + "',ReportSignUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',StatusID=3 where id='" + strReportId + "'";
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
        ReportQuery();
    }
}