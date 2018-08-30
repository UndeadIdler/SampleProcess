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

public partial class Report_ComponentReportTest : System.Web.UI.Page
{
    public string strTable = "";
    public string strTableC = "";
    public string strTableP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "测试报告数据组成表";
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
            xlSheet.ActiveSheet.Cells[1, 2] = "南湖区";
            xlSheet.ActiveSheet.Cells[1, 3] = "秀洲区";
            xlSheet.ActiveSheet.Cells[1, 4] = "联合污水";
            xlSheet.ActiveSheet.Cells[1, 5] = "五县";
            xlSheet.ActiveSheet.Cells[1, 6] = "其他";
           
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, 6]).Font.set_Bold(true);
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, 6]).Font.set_Size(10);
        }

        DateTime dtStartTime, dtEndTime;
        DateTime dt = Convert.ToDateTime(txt_StartTime.Text);
        DateTime dt2 = Convert.ToDateTime(txt_EndTime.Text);
        dtStartTime = Convert.ToDateTime(dt.Year + "-" + dt.Month + "-1");
        dtEndTime = Convert.ToDateTime(dt2.Year + "-" + dt2.Month + "-1");
        dtEndTime = dtEndTime.AddMonths(1);

        int subMonth = int.Parse(dt2.Month.ToString()) - int.Parse(dt.Month.ToString()) + 1;

        strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='90%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月") + " 测试报告数据组成表</b></font></caption><tbody><tr align='center'><th>月份</th><th>南湖区</th><th>秀洲区</th><th>联合污水</th><th>五县</th><th>其他</th></tr>";
        strTableP = "<table id='tableid'  style= 'BORDER-COLLAPSE:collapse' borderColor= '#00000' border= '1' width='95%'><caption><FONT style='size=3 HEIGHT: 50px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月") + " 测试报告数据组成表</b></font></caption><tbody><tr align='center'><th>月份</th><th>南湖区</th><th>秀洲区</th><th>联合污水</th><th>五县</th><th>其他</th></tr>";

        string strSql = "select m as [Date],";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID = 1 and r.ItemType = 13 THEN n.num ELSE 0 END) AS 南湖区,";
        strSql += "SUM(CASE WHEN datepart(month,n.ReportDate) = m and r.ClientID = 2 and r.ItemType = 13 THEN n.num ELSE 0 END) AS 秀洲区,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID = 6 and r.ItemType = 13 THEN n.num ELSE 0 END) AS 联合污水,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID = 5 and r.ItemType = 13 THEN n.num ELSE 0 END) AS 五县,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID <> '' and r.ItemType = 13 THEN n.num ELSE 0 END) AS 总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";
        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        int m = ds.Tables[0].Rows.Count;        
        if (m != 0)
        {
            string theMonths = "";
            string nh = "";
            string xz = "";
            string lh = "";
            string wx = "";
            string qt = "";

            int nhN = 0;
            int xzN = 0;
            int lhN = 0;
            int wxN = 0;
            int qtN = 0;

            for (int i = 0; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                nh = ds.Tables[0].Rows[i][1].ToString();
                xz = ds.Tables[0].Rows[i][2].ToString();
                lh = ds.Tables[0].Rows[i][3].ToString();
                wx = ds.Tables[0].Rows[i][4].ToString();
                qt = (int.Parse(ds.Tables[0].Rows[i][5].ToString()) - (int.Parse(nh) + int.Parse(xz) + int.Parse(lh) + int.Parse(wx))).ToString();

                nhN += int.Parse(nh);
                xzN += int.Parse(xz);
                lhN += int.Parse(lh);
                wxN += int.Parse(wx);
                qtN += int.Parse(qt);

                strTableC += "<tr align='center'><td>" + theMonths + "</td><td>" + nh + "</td><td>" + xz + "</td><td>" + lh + "</td><td>" + wx + "</td><td>" + qt + "</td></tr>";

                if (Export == 1)
                {
                    xlSheet.ActiveSheet.Cells[i + 2, 1] = theMonths;
                    xlSheet.ActiveSheet.Cells[i + 2, 2] = nh;
                    xlSheet.ActiveSheet.Cells[i + 2, 3] = xz;
                    xlSheet.ActiveSheet.Cells[i + 2, 4] = lh;
                    xlSheet.ActiveSheet.Cells[i + 2, 5] = wx;
                    xlSheet.ActiveSheet.Cells[i + 2, 6] = qt;
                }
            }
            strTableC += "<tr align='center'><td>总计</td><td>" + nhN.ToString() + "</td><td>" + xzN.ToString() + "</td><td>" + lhN.ToString() + "</td><td>" + wxN.ToString() + "</td><td>" + qtN.ToString() + "</td></tr>";
            if (Export == 1)
            {
                xlSheet.ActiveSheet.Cells[m + 2, 1] = "总计";
                xlSheet.ActiveSheet.Cells[m + 2, 2] = nhN.ToString();
                xlSheet.ActiveSheet.Cells[m + 2, 3] = xzN.ToString();
                xlSheet.ActiveSheet.Cells[m + 2, 4] = lhN.ToString();
                xlSheet.ActiveSheet.Cells[m + 2, 5] = wxN.ToString();
                xlSheet.ActiveSheet.Cells[m + 2, 6] = qtN.ToString();
                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[m + 2, 6]).Borders.set_LineStyle(Microsoft.Office.Interop.Owc11.XlLineStyle.xlContinuous);
                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[m + 2, 6]).set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignCenter);
            }
        }
        else
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='90%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月") + " 测试报告数据组成表</b></font></caption><tbody><tr align='center'><th>月份</th><th>南湖区</th><th>秀洲区</th><th>联合污水</th><th>五县</th><th>其他</th></tr>";
            strTableC += "<tr align='center'><td>总计</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td></tr>";
            if (Export == 1)
            {
                xlSheet.ActiveSheet.Cells[2, 1] = "总计";
                xlSheet.ActiveSheet.Cells[2, 2] = "-";
                xlSheet.ActiveSheet.Cells[2, 3] = "-";
                xlSheet.ActiveSheet.Cells[2, 4] = "-";
                xlSheet.ActiveSheet.Cells[2, 5] = "-";
                xlSheet.ActiveSheet.Cells[2, 6] = "-";
                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[2, 6]).Borders.set_LineStyle(Microsoft.Office.Interop.Owc11.XlLineStyle.xlContinuous);
                xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[2, 6]).set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignCenter);
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
                string strFileName = "测试报告数据组成表_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";
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
    protected void btn_ExportP_Click(object sender, EventArgs e)
    {
        string pDate = txt_StartTime.Text + "," + txt_EndTime.Text;
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('ComponentReportTestPrt.aspx?pDate=" + pDate + "');", true);
        Query(0);
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
