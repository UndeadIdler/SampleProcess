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
using DAL;
using System.IO;
using System.Text;
using System.Data.OleDb;

public partial class ZKSample1 : System.Web.UI.Page
{
    private string strSelectedId//样品单号
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }

    private string strDrawId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strDrawId"]; }
        set { ViewState["strDrawId"] = value; }
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
    private string AnalysisIdList//所选择操作列记录对应的id
    {
        get { return (string)ViewState["AnalysisIdList"]; }
        set { ViewState["AnalysisIdList"] = value; }
    }
    private string AnalysisNameList//所选择操作列记录对应的id
    {
        get { return (string)ViewState["AnalysisNameList"]; }
        set { ViewState["AnalysisNameList"] = value; }
    }
    private bool havedata//所选择操作列记录对应的id
    {
        get { return (bool)ViewState["havedata"]; }
        set { ViewState["havedata"] = value; }
    }
    private  DataTable expds//所选择操作列记录对应的id
    {
        get { return (DataTable)ViewState["expds"]; }
        set { ViewState["expds"] = value; }
    }
    private DataSet ds//所选择操作列记录对应的id
    {
        get { return (DataSet)ViewState["ds"]; }
        set { ViewState["ds"] = value; }
    }
     public DataSet dsitem_ZK
    {
        get { return (DataSet)ViewState["dsitem_ZK"]; }
        set { ViewState["dsitem_ZK"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            Query();
        }
    }
    

    private void Query()
    {
       
        #region 质控
        BindZK();
 
        #endregion
    }
    private void BindAnalysis()
    {
//        string constr = "";
//        if (txt_Itemquery.Text.Trim() != "")
//            constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or t_M_ANItemInf.AICode like '%" + txt_Itemquery.Text.Trim() + "%')";
//        if (txt_QueryTime.Text.Trim() != "")
//            constr += " and  (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";

//        string strSql = "select t_MonitorItemDetail.id, t_MonitorItemDetail.fxDanID,t_MonitorItemDetail.MonitorItem,t_M_ANItemInf.AIName 监测项,t_M_SampleInfor.SampleID AS 样品编号,t_M_ReporInfo.Ulevel ,t_M_SampleInfor.SampleAddress 采样点 , CONVERT(VARCHAR(10),t_M_SampleInfor.SampleDate,120) AS 采样时间," +
//      " CONVERT(VARCHAR(10),t_M_SampleInfor.AccessDate,120) AS 接样时间, " +
//      " t_M_SampleInfor.TypeID, t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, t_M_ReporInfo.ProjectName 项目名称," +
//     " t_M_ReporInfo.chargeman 项目负责人,t_MonitorItemDetail.experimentvalue,t_MonitorItemDetail.Method,t_M_SampleInfor.zxbz  " +
//",t_M_ReporInfo.Green,t_MonitorItemDetail.Remark 备注,t_M_SampleInfor.SampleSource FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
//     " t_M_AnalysisMainClassEx ON " +
//    "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.SampleID and t_MonitorItemDetail.delflag=0  inner join t_M_ANItemInf on t_M_ANItemInf.ID=t_MonitorItemDetail.MonitorItem where t_MonitorItemDetail.flag=2 and t_M_SampleInfor.StatusID=1 and t_MonitorItemDetail.LyUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' " + constr + " ORDER BY t_M_ANItemInf.orderid,t_M_ANItemInf.AIName,t_M_SampleInfor.AccessDate,t_M_SampleInfor.SampleID";

//        ds = new MyDataOp(strSql).CreateDataSet();
//        DataColumn dcc = new DataColumn("紧急程度");
//        ds.Tables[0].Columns.Add(dcc);
//        DataColumn dcbz = new DataColumn("执行标准");
//        ds.Tables[0].Columns.Add(dcbz);
//        DataColumn dcbz1 = new DataColumn("标准值");
//        ds.Tables[0].Columns.Add(dcbz1);

//        DataColumn dcbz2 = new DataColumn("分析指标");
//        ds.Tables[0].Columns.Add(dcbz2);
//        DataColumn dcGreen = new DataColumn("是否走绿色通道");
//        ds.Tables[0].Columns.Add(dcGreen);
//        string str = "select * from t_R_UserInfo";
//        DataSet dsuser = new MyDataOp(str).CreateDataSet();
//        string strbz = "select t_hyClassParam.id,t_标准字典.bz,t_hyItem.itemid,t_hyItem.fw,单位全称,t_CompabyBZ.qyid from  t_CompabyBZ inner join t_hyClassParam on t_hyClassParam.id=t_CompabyBZ.bzid inner join t_标准字典 on t_标准字典.id=t_hyClassParam.bz inner join t_hyItem on t_hyItem.pid=t_hyClassParam.id inner join T_委托单位 on T_委托单位.id=t_CompabyBZ.qyid where t_CompabyBZ.flag=0";
//        // string strbz = "select t_hyClassParam.id,t_标准字典.bz,t_hyItem.itemid,t_hyItem.fw from  t_CompabyBZ inner join t_hyClassParam on t_hyClassParam.id=t_CompabyBZ.bzid inner join t_标准字典 on t_标准字典.id=t_hyClassParam.bz inner join t_hyItem on t_hyItem.pid=t_hyClassParam.id where t_CompabyBZ.flag=0";
//        DataSet dsbz = new MyDataOp(strbz).CreateDataSet();
//        foreach (DataRow dr in ds.Tables[0].Rows)
//        {

//            DataRow[] druser = dsuser.Tables[0].Select("userid='" + dr["项目负责人"] + "'");
//            if (druser.Length > 0)
//            { dr["项目负责人"] = druser[0]["Name"].ToString(); }

//            if (dr["Ulevel"].ToString() == "1")
//                dr["紧急程度"] = "紧急";
//            else
//                dr["紧急程度"] = "一般";
//            if (dr["Green"].ToString() == "1")
//                dr["是否走绿色通道"] = "是";
//            else
//                dr["是否走绿色通道"] = "否";
//            try
//            {
//                string zxstr = "";
//                if (dr["zxbz"].ToString() != "")
//                    zxstr = " and id='" + dr["zxbz"].ToString() + "'";
//                DataRow[] drbz = dsbz.Tables[0].Select("单位全称='" + dr["SampleSource"].ToString() + "' and itemid='" + dr["MonitorItem"].ToString() + "'" + zxstr);
//                if (drbz.Length > 0)
//                {
//                    dr["执行标准"] = drbz[0]["bz"].ToString().Trim();
//                    dr["标准值"] = drbz[0]["fw"].ToString().Trim();
//                }
//            }
//            catch { }
//            string getitemstr = "select AIName,MonitorItem from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem  where  SampleID='" + dr["样品编号"].ToString() + "' and delflag=0";
//            DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
//            if (dsitem != null && dsitem.Tables.Count > 0)
//            {
//                foreach (DataRow drr in dsitem.Tables[0].Rows)
//                {
//                    dr["分析指标"] += drr[0].ToString() + ";";
//                    //dr["分析项目编码"] += drr[1].ToString() + ",";
//                }
//            }
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

    }
    private void BindZK()
    {
        //      string strSql_zk = @"select distinct t_MonitorItemDetail.MonitorItem,t_M_ANItemInf.AIName 监测项,Name FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
        // " t_M_AnalysisMainClassEx ON " +
        //"  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.SampleID and t_MonitorItemDetail.delflag=0  inner join t_M_ANItemInf on t_M_ANItemInf.ID=t_MonitorItemDetail.MonitorItem inner join t_R_UserInfo on t_R_UserInfo.UserID=t_MonitorItemDetail.LyUser where t_MonitorItemDetail.flag=2 and t_M_SampleInfor.StatusID=1 and t_MonitorItemDetail.LyUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' " + constr + " ";
     
      string constr = "";
      if (txt_Itemquery.Text.Trim() != "")
          constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or t_M_ANItemInf.AICode like '%" + txt_Itemquery.Text.Trim() + "%')";

        if (txt_QueryTime.Text.Trim() != "")
            constr += " and  (year(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        else
            constr += " and  (year(createdate)= '" + DateTime.Now.Year + "' AND month(createdate)= '" + DateTime.Now.Month + "' and day(createdate)= '" + DateTime.Now.Day + "')";


        string strSql_zk = @"select t_zkanalysisinfo.id, t_M_ANItemInf.id itemid,AIName,Name,analysisnum, scenejcnum,case when analysisnum!=0 then  CONVERT(decimal(18, 2), scenejcnum*1.0/analysisnum*1.0*100) else null end,scenehgnum,case when scenejcnum!=0 then CONVERT(decimal(18, 2),scenejcnum*1.0/scenejcnum*1.0*100) else null end, experimentjcnum,case when analysisnum!=0 then CONVERT(decimal(18, 2),experimentjcnum*1.0/analysisnum*1.0*100) else null  end, experimenthgnum,case when experimentjcnum!=0 then CONVERT(decimal(18, 2),experimenthgnum*1.0/experimentjcnum*1.0*100) else null end , jbhsjcnum, case when analysisnum!=0 then CONVERT(decimal(18, 2),jbhsjcnum*1.0/analysisnum*1.0*100) else null end ,jbhshgnum, case when jbhsjcnum!=0 then CONVERT(decimal(18, 2),  jbhshgnum*1.0/jbhsjcnum*1.0*100) else null end , alljcnum,allhgnum, 
                      mmjcnum, mmhgnum, byjcnum, byhgnum, amount
 from t_zkanalysisinfo inner join t_M_ANItemInf on t_M_ANItemInf.id=t_zkanalysisinfo.itemid inner join t_R_UserInfo on t_R_UserInfo.UserID=t_zkanalysisinfo.userid where 1=1" + constr+" order by createdate";

        dsitem_ZK = new MyDataOp(strSql_zk).CreateDataSet();
        if (dsitem_ZK.Tables[0].Rows.Count == 0)
        {
            havedata = false;
            //没有记录仍保留表头
            dsitem_ZK.Tables[0].Rows.Add(dsitem_ZK.Tables[0].NewRow());
            grdvw_ZKList.DataSource = dsitem_ZK;
            grdvw_ZKList.DataBind();
            int intColumnCount = grdvw_ZKList.Rows[0].Cells.Count;
            grdvw_ZKList.Rows[0].Cells.Clear();
            grdvw_ZKList.Rows[0].Cells.Add(new TableCell());
            grdvw_ZKList.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            havedata = true;
            grdvw_ZKList.DataSource =dsitem_ZK ;
            grdvw_ZKList.DataBind();

        }
       
       // dsitem_ZK.Dispose();
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }

    protected void btn_ExportR_Click(object sender, EventArgs e)
    {
        ExcelOut(this.grdvw_ZKList);
    }
    public void ExcelOut(GridView gv)
    {
        if (gv.Rows.Count > 0)
        {
            Response.Clear();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            Response.Write("没有数据");
        }
    }
    
    protected void btn_Export_Click(object sender, EventArgs e)
    {

        //expds.Clear();

        //foreach (GridViewRow gvr in grdvw_ZKList.Rows)
        //{
        //    if (gvr.RowType == DataControlRowType.DataRow)
        //    {
               
        //            if (gvr.Cells[15].Text.Trim() == "&nbsp;")
        //            {
        //                DataRow dr = expds.NewRow();
        //                dr["监测项"] = gvr.Cells[4].Text.Trim().ToString();//监测项
        //                dr["样品编号"] = gvr.Cells[5].Text.Trim();//样品编号
        //                dr["采样时间"] = gvr.Cells[8].Text.Trim();//采样时间
        //                dr["采样点"] = gvr.Cells[7].Text.Trim();//采样点
        //                dr["接样时间"] = gvr.Cells[9].Text.Trim();//接样时间
        //                dr["样品类型"] = gvr.Cells[11].Text.Trim();//样品类型 
        //                dr["样品性状"] = gvr.Cells[12].Text.Trim();//样品性状  
        //                dr["项目名称"] = gvr.Cells[13].Text.Trim();//项目名称  
        //                dr["项目负责人"] = gvr.Cells[14].Text.Trim();//项目负责人
        //                dr["备注"] = gvr.Cells[19].Text.Trim();//紧急程度
        //                dr["紧急程度"] = gvr.Cells[21].Text.Trim();//紧急程度
        //                dr["执行标准"] = gvr.Cells[22].Text.Trim();//执行标准
        //                dr["标准值"] = gvr.Cells[23].Text.Trim();//标准值
        //                if(gvr.Cells[18].Text.Trim()=="1")
        //                dr["是否走绿色通道"] ="是";//是否走绿色通道
        //                else
        //                    dr["是否走绿色通道"] = "否";
        //                dr["分析指标"] = gvr.Cells[24].Text.Trim();//紧急程度
        //                expds.Rows.Add(dr);
        //                expds.AcceptChanges();
        //            }
               

        //    }

        //}
        if (dsitem_ZK.Tables[0].Rows.Count > 0)
        {
            Random r=new Random();
            string fileName=r.Next().ToString()+".xls";
            string url = ConfigurationManager.AppSettings["path1"].ToString() + "Excel.xls";
            
            btn_Export_Click(dsitem_ZK.Tables[0], fileName);
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有可导出的数据！')", true);
            Query();
        }
       
    }
    protected void btn_Export_Click(DataTable dt, string strFileName)
    {
        Microsoft.Office.Interop.Owc11.SpreadsheetClass xlSheet = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
        xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, dt.Columns.Count]).set_MergeCells(true);
        xlSheet.ActiveSheet.Cells[1, 1] = "分析任务单";

        int col=1;
        foreach (DataColumn dc in dt.Columns)
        { 
          
            xlSheet.ActiveSheet.Cells[2, col++] = dc.ColumnName.ToString(); 
        }


        for (int i = 0; i < dt.Rows.Count; i++)
        {

            int j = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                if (dt.Rows[i][dc.ColumnName].ToString() != "&nbsp;")
                {
                    xlSheet.ActiveSheet.Cells[3 + i, j] = dt.Rows[i][dc.ColumnName].ToString();
                      if(dc.ColumnName.Trim()=="标准值")
                          xlSheet.ActiveSheet.Cells[3 + i, j] = "'"+dt.Rows[i][dc.ColumnName].ToString();
                }

                j++;
            }
        }
        xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).Borders.set_LineStyle(Microsoft.Office.Interop.Owc11.XlLineStyle.xlContinuous);
        xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignCenter);
 
        try
        {
            xlSheet.Export(Server.MapPath(".") + "\\" + strFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.location.href='ShowXls.aspx?file_name=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "';", true);
            RemoveFiles(Server.MapPath("."));
        }
        catch
        {
        }


    }
     //<summary>
     //删除超时文件
     //</summary>
     //<param name="strPath"></param>
    private void RemoveFiles(string strPath)
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] fiArr = di.GetFiles();
        foreach (System.IO.FileInfo fi in fiArr)
        {
            if (fi.Extension.ToString() == ".xls")
            {
                 //if file is older than 2 minutes, we'll clean it up
                TimeSpan min = new TimeSpan(0, 0, 2, 0, 0);
                if (fi.CreationTime < DateTime.Now.Subtract(min))
                {
                    fi.Delete();
                }
            }
        }
    } 

    #region GridView相关事件响应函数
    protected void grdvw_ZKList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ZKList.PageIndex = e.NewPageIndex;
        Query();
    }

    protected void grdvw_ZKList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //第一行表头  
            TableCellCollection tcHeader = e.Row.Cells;
            tcHeader.Clear();


            tcHeader.Clear();
            tcHeader.Add(new TableHeaderCell());
            tcHeader[0].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[0].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[0].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[0].Text = "ID";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[1].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[1].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[1].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[1].Text = "分析项目ID";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[2].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[2].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[2].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[2].Text = "分析项目";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[3].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[3].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[3].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[3].Text = "分析者";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[4].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[4].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[4].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[4].Text = "分析样品数";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[5].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[5].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[5].Attributes.Add("colspan", "4"); //跨Column   
            tcHeader[5].Text = "现场平行样";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[6].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[6].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[6].Attributes.Add("colspan", "4"); //跨Column   
            tcHeader[6].Text = "实验室平行样";

            tcHeader.Add(new TableHeaderCell());
            tcHeader[7].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[7].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[7].Attributes.Add("colspan", "4"); //跨Column   
            tcHeader[7].Text = "加标回收";

            tcHeader.Add(new TableHeaderCell());
            tcHeader[8].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[8].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[8].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[8].Text = "全程序空白";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[9].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[9].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[9].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[9].Text = "密码样";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[10].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[10].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[10].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[10].Text = "标样";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[11].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[11].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[11].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[11].Text = "总检数</tr><tr>";
           
            int n = 12;
            //第二行表头 
            int p=0;
            for (int i = 0; i <6;i++ )
            {
                tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i+p].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[n + i + p].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i + p].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i + p].Text = "检查数";
                if(i<3)
                {
                    p++;
                 tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i+p].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[n + i+p].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i+p].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i+p].Text = "检查率%";
                }
               p++;
                tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i+p].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i+p].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i+p].Text = "合格数";
                tcHeader[n + i+p].Attributes.Add("bgcolor", "#005EBB");
                 if(i<3)
                {
                   p++;
                      tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i+p].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i+p].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i+p].Text = "合格率%";
                tcHeader[n + i+p].Attributes.Add("bgcolor", "#005EBB");
                 }
                 if (n + i + p==17)
                    tcHeader[n + i+p].Text = "合格数</tr><tr>";
               
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
           

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[12].Visible = false;
           // e.Row.Cells[18].Visible = false;
            //for (int i = 8; i <= e.Row.Cells.Count-1; i++)
            //{
            //    e.Row.Cells[i].Visible = false;
            //}

            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
            //e.Row.Cells[20].Visible = false;
            //e.Row.Cells[21].Visible = false;
            //e.Row.Cells[18].Visible = false;
            //for (int i = 0; i < cbl_choose.Items.Count; i++)
            //{
            //    if (!cbl_choose.Items[i].Selected)
            //    {
            //        try
            //        {
            //            int p = int.Parse(cbl_choose.Items[i].Value);
            //            e.Row.Cells[p].Visible = false;
            //        }
            //        catch
            //        { }
            //    }
            //}
        }
    }
   
    protected void grdvw_ZKList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

      //  if (e.Row.RowType == DataControlRowType.DataRow)
      //  {
      //      DropDownList drop_wrw = e.Row.FindControl("drop_wrw") as DropDownList;
      //      BindItemList(drop_wrw);
      //      DataRowView row = e.Row.DataItem as DataRowView;

      //      string item = row["itemid"].ToString();
      //      drop_wrw.SelectedValue = item.ToString();
      //}
    }
    private void BindItemList(DropDownList ddl_Item)
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("MonitorItem");
        DataColumn dc2 = new DataColumn("监测项");
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["MonitorItem"].ToString() != "")
            {
                DataRow[] drselect = dt.Select("MonitorItem='" + dr["MonitorItem"].ToString() + "'");
                if (drselect.Length == 0)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["MonitorItem"] = dr["MonitorItem"].ToString();
                    drnew["监测项"] = dr["监测项"].ToString();
                    dt.Rows.Add(drnew);
                    dt.AcceptChanges();
                }
            }
        }
        if (dt.Rows.Count > 0)
        {
            ddl_Item.DataSource = dt;
            ddl_Item.DataValueField = "MonitorItem";
            ddl_Item.DataTextField = "监测项";
            ddl_Item.DataBind();
        }
        else
        {
            string constr = "";
            if (txt_Itemquery.Text.Trim() != "")
                constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or t_M_ANItemInf.AICode like '%" + txt_Itemquery.Text.Trim() + "%')";
            if (txt_QueryTime.Text.Trim() != "")
                constr += " and  (year(fxdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(fxdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(fxdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";

            string getitemstr = "select distinct AIName 监测项,MonitorItem from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem  where  t_MonitorItemDetail.LyUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' and delflag=0";

           DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
           ddl_Item.DataSource = dsitem;
            ddl_Item.DataValueField = "MonitorItem";
            ddl_Item.DataTextField = "监测项";
            ddl_Item.DataBind();
        }
       
    }

   
    
    #endregion
}
