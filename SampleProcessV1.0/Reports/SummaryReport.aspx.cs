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
using Microsoft.Office.Interop.Owc11;

public partial class Report_SummaryReport : System.Web.UI.Page
{
    public string strTable = "";
    public string strTableC = "";
    public string strTableP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "监测数据统计表";
        if (!IsPostBack)
        {
            #region 初始化页面               
            txt_StartTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-01-01");            
            txt_EndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");          
            Query(0);
            #endregion
        }
    }    
    private void Query(int Export)
    {
        Microsoft.Office.Interop.Owc11.SpreadsheetClass xlSheet = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
        if (Export == 1)
        {           
            xlSheet.ActiveSheet.Cells[1, 1] = "月份";
            xlSheet.ActiveSheet.Cells[1, 2] = "例行监测";
            xlSheet.ActiveSheet.Cells[1, 3] = "委托监测";
            xlSheet.ActiveSheet.Cells[1, 4] = "数据总量";
           
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, 4]).Font.set_Bold(true);
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, 4]).Font.set_Size(10);
        }

        DateTime dtStartTime, dtEndTime;
        DateTime dt = Convert.ToDateTime(txt_StartTime.Text);
        DateTime dt2 = Convert.ToDateTime(txt_EndTime.Text);
        dtStartTime = Convert.ToDateTime(dt.Year + "-" + dt.Month + "-1");
        dtEndTime = Convert.ToDateTime(dt2.Year + "-" + dt2.Month + "-1");
        dtEndTime = dtEndTime.AddMonths(1);

        int subMonth = int.Parse(dt2.Month.ToString()) - int.Parse(dt.Month.ToString()) + 1;

        strTable = "<table id='tableid' class='listTable' border='0' cellspacing='1' width='90%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月") + " 监测数据统计表</b></font></caption><tbody><tr align='center'><th>月份</th><th>监测报告</th><th>测试报告</th><th>数据总量</th></tr>";
        strTableP = "<table id='tableid'  style= 'BORDER-COLLAPSE:collapse' borderColor= '#00000' border= '1' width='95%'><caption><FONT style='size=3 HEIGHT: 50px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月") + " 监测数据统计表</b></font></caption><tbody><tr align='center'><th>月份</th><th>监测报告</th><th>测试报告</th><th>数据总量</th></tr>";

        //string strSql = "select m as [Date],";
        //strSql += "SUM(CASE WHEN datepart(month, AccessDate) = m and ItemType <> 13 THEN 1 ELSE 0 END) AS 监测报告,";
        //strSql += "SUM(CASE WHEN datepart(month, AccessDate) = m and ItemType = 13 THEN 1 ELSE 0 END) AS 测试报告, ";
        //strSql += "SUM(CASE WHEN datepart(month, AccessDate) = m and ItemType <> '' THEN 1 ELSE 0 END) AS 数据总量 ";
        //strSql += "from t_M_SampleInfor c,( select month('" + dtStartTime + "') m ";
        string strSql = "select m as [Date],";
        strSql += "SUM(CASE WHEN datepart(month, n.fxdate) = m and r.rwclass =0 THEN 1 ELSE 0 END) AS 例行监测,";
        strSql += "SUM(CASE WHEN datepart(month, n.fxdate) = m and  r.rwclass =1 THEN 1 ELSE 0 END)  AS 委托监测,";
        strSql += "SUM(CASE WHEN datepart(month, n.fxdate) = m and r.ItemType <> '' THEN 1 ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_MonitorItemDetail n,( select month('" + dtStartTime + "') m ";
       
        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.fxdate >= '" + dtStartTime + "' and n.fxdate < '" + dtEndTime + "' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.SampleID = n.sampleid ";
        strSql += "GROUP BY m";
       

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        int m = ds.Tables[0].Rows.Count;        
        if (m != 0)
        {
            string theMonths = "";
            string jcReportsN = "";
            string csReportsN = "";
            string sumReportsN = "";
            int jcSum = 0;
            int csSum = 0;

            for (int i = 0; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][1].ToString();
                csReportsN = ds.Tables[0].Rows[i][2].ToString();
                sumReportsN = ds.Tables[0].Rows[i][3].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);

                strTableC += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";

                if (Export == 1)
                {
                    xlSheet.ActiveSheet.Cells[i + 2, 1] = theMonths;
                    xlSheet.ActiveSheet.Cells[i + 2, 2] = jcReportsN;
                    xlSheet.ActiveSheet.Cells[i + 2, 3] = csReportsN;
                    xlSheet.ActiveSheet.Cells[i + 2, 4] = sumReportsN;
                }
            }
            strTableC += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
            if (Export == 1)
            {
                xlSheet.ActiveSheet.Cells[m + 2, 1] = "总计";
                xlSheet.ActiveSheet.Cells[m + 2, 2] = jcSum.ToString();
                xlSheet.ActiveSheet.Cells[m + 2, 3] = csSum.ToString();
                xlSheet.ActiveSheet.Cells[m + 2, 4] = (jcSum + csSum).ToString();
                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[m + 2, 4]).Borders.set_LineStyle(Microsoft.Office.Interop.Owc11.XlLineStyle.xlContinuous);
                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[m + 2, 4]).set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignCenter);
            }
        }
        else
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='90%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + txt_StartTime.Text.Trim() + " 00时至" + txt_EndTime.Text.Trim() + " 24时 监测数据统计表</b></font></caption><tbody><tr align='center'><th>月份</th><th>监测报告</th><th>测试报告</th><th>数据总量</th></tr>";
            strTableC += "<tr align='center'><td>总计</td><td>-</td><td>-</td><td>-</td></tr>";
            if (Export == 1)
            {
                xlSheet.ActiveSheet.Cells[2, 1] = "总计";
                xlSheet.ActiveSheet.Cells[2, 2] = "-";
                xlSheet.ActiveSheet.Cells[2, 3] = "-";
                xlSheet.ActiveSheet.Cells[2, 4] = "-";

                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[2, 4]).Borders.set_LineStyle(Microsoft.Office.Interop.Owc11.XlLineStyle.xlContinuous);
                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[2, 4]).set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignCenter);

            }
        }
        strTableC += "</tbody></table>";
        strTable = strTable + strTableC;
        strTableP = strTableP + strTableC;
        if (Export == 1)
        {
            //导出报表
            try
            {
                RemoveFiles(Server.MapPath("."));
                string strFileName = "监测数据统计表_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";
                xlSheet.Export(Server.MapPath(".") + "\\" + strFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('ShowXls.aspx?file_name=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "');", true);
            }
            catch
            {
            }
        }
    }
    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query(0);
    }

    protected void btn_ExportR_Click(object sender, EventArgs e)
    {         
        Query(1);
    }
    //protected void btn_ExportP_Click(object sender, EventArgs e)
    //{
    //    string pDate = txt_StartTime.Text + "," + txt_EndTime.Text;
    //    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('SummaryReportPrt.aspx?pDate=" + pDate + "');", true);
    //    Query(0);
    //}
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
            if (fi.Extension.ToString() == ".xls")
            {
                // if file is older than 2 minutes, we'll clean it up
                TimeSpan min = new TimeSpan(0, 0, 2, 0, 0);
                if (fi.CreationTime < DateTime.Now.Subtract(min))
                {
                    fi.Delete();
                }
            }
        }
    }   
}
