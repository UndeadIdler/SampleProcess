using System;
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

using WebApp.Components;

public partial class ComponentReportTypePrt : System.Web.UI.Page 
{
    public string strTable = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();        
        PrintReport();       
    }
    protected void PrintReport()
    {
        int totalM = 0;
        string strSql = "";
        string pDate = Request.QueryString["pDate"].ToString();
        string[] date = pDate.Split(',');
        DateTime dtStartTime, dtEndTime;
        DateTime dt = Convert.ToDateTime(date[0]);
        DateTime dt2 = Convert.ToDateTime(date[1]);
        dtStartTime = Convert.ToDateTime(dt.Year + "-" + dt.Month + "-1");
        dtEndTime = Convert.ToDateTime(dt2.Year + "-" + dt2.Month + "-1");
        dtEndTime = dtEndTime.AddMonths(1);

        int subMonth = int.Parse(dt2.Month.ToString()) - int.Parse(dt.Month.ToString()) + 1;
        Label_H.Text = "<font size='3'>" + DateTime.Parse(date[0]).ToString("yyyy年MM月") + "至" + DateTime.Parse(date[1]).ToString("yyyy年MM月") + " 监测数据组成表</font>";

        strTable = "<table id='tableid' class='listTable3'><tbody><tr align='center'><th>样品类型</th><th>月份</th><th>监测报告</th><th>测试报告</th><th>数据总量</th></tr>";

        #region 地表水

        strTable += "<tr align='center'><td rowspan = '" + (subMonth + 1).ToString() + "'>地表水</td>";
        strSql = "select m as [Date],s.typeid,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> 13 THEN n.num ELSE 0 END) AS 监测报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 13 THEN n.num ELSE 0 END) AS 测试报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";

        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and s.typeid = '2' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m,s.typeid";


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

            strTable += "<td>"
                     + ds.Tables[0].Rows[0][0].ToString() + "月份</td><td>"
                     + ds.Tables[0].Rows[0][2].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][3].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][4].ToString() + "</tr>";

            jcSum = int.Parse(ds.Tables[0].Rows[0][2].ToString());
            csSum = int.Parse(ds.Tables[0].Rows[0][3].ToString());

            for (int i = 1; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][2].ToString();
                csReportsN = ds.Tables[0].Rows[i][3].ToString();
                sumReportsN = ds.Tables[0].Rows[i][4].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);

                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
         
        }
        else
        {
            strTable += "<td>" + dt.Month.ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            for (int mth = 1; mth < subMonth; mth++)
            {
                strTable += "<tr align='center'><td>" + (int.Parse(dt.Month.ToString()) + mth).ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>0</td><td>0</td><td>0</td></tr>";
        }
        totalM += subMonth;
        #endregion

        #region 臭气浓度

        strTable += "<tr align='center'><td rowspan = '" + (subMonth + 1).ToString() + "'>臭气浓度</td>";

        strSql = "select m as [Date],s.typeid,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> 13 THEN n.num ELSE 0 END) AS 监测报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 13 THEN n.num ELSE 0 END) AS 测试报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";

        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and s.typeid = '8' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m,s.typeid";


        ds = new MyDataOp(strSql).CreateDataSet();
        m = ds.Tables[0].Rows.Count;
        if (m != 0)
        {
            string theMonths = "";
            string jcReportsN = "";
            string csReportsN = "";
            string sumReportsN = "";
            int jcSum = 0;
            int csSum = 0;

            strTable += "<td>"
                     + ds.Tables[0].Rows[0][0].ToString() + "月份</td><td>"
                     + ds.Tables[0].Rows[0][2].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][3].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][4].ToString() + "</tr>";

            jcSum = int.Parse(ds.Tables[0].Rows[0][2].ToString());
            csSum = int.Parse(ds.Tables[0].Rows[0][3].ToString());
            for (int i = 1; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][2].ToString();
                csReportsN = ds.Tables[0].Rows[i][3].ToString();
                sumReportsN = ds.Tables[0].Rows[i][4].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);

                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
          
        }
        else
        {
            strTable += "<td>" + dt.Month.ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            for (int mth = 1; mth < subMonth; mth++)
            {
                strTable += "<tr align='center'><td>" + (int.Parse(dt.Month.ToString()) + mth).ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>0</td><td>0</td><td>0</td></tr>";
        }
        totalM += subMonth;
        #endregion

        #region 废气

        strTable += "<tr align='center'><td rowspan = '" + (subMonth + 1).ToString() + "'>废气</td>";

        strSql = "select m as [Date],s.typeid,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> 13 THEN n.num ELSE 0 END) AS 监测报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 13 THEN n.num ELSE 0 END) AS 测试报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";

        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and s.typeid = '3' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m,s.typeid";


        ds = new MyDataOp(strSql).CreateDataSet();
        m = ds.Tables[0].Rows.Count;
        if (m != 0)
        {
            string theMonths = "";
            string jcReportsN = "";
            string csReportsN = "";
            string sumReportsN = "";
            int jcSum = 0;
            int csSum = 0;

            strTable += "<td>"
                     + ds.Tables[0].Rows[0][0].ToString() + "月份</td><td>"
                     + ds.Tables[0].Rows[0][2].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][3].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][4].ToString() + "</tr>";

            jcSum = int.Parse(ds.Tables[0].Rows[0][2].ToString());
            csSum = int.Parse(ds.Tables[0].Rows[0][3].ToString());
            for (int i = 1; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][2].ToString();
                csReportsN = ds.Tables[0].Rows[i][3].ToString();
                sumReportsN = ds.Tables[0].Rows[i][4].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);

                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";              
            }
            strTable += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
           
        }
        else
        {
            strTable += "<td>" + dt.Month.ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            for (int mth = 1; mth < subMonth; mth++)
            {
                strTable += "<tr align='center'><td>" + (int.Parse(dt.Month.ToString()) + mth).ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>0</td><td>0</td><td>0</td></tr>";           
        }
        totalM += subMonth;
        #endregion

        #region 废水

        strTable += "<tr align='center'><td rowspan = '" + (subMonth + 1).ToString() + "'>废水</td>";

        strSql = "select m as [Date],s.typeid,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> 13 THEN n.num ELSE 0 END) AS 监测报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 13 THEN n.num ELSE 0 END) AS 测试报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";

        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and s.typeid = '1' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m,s.typeid";


        ds = new MyDataOp(strSql).CreateDataSet();
        m = ds.Tables[0].Rows.Count;
        if (m != 0)
        {
            string theMonths = "";
            string jcReportsN = "";
            string csReportsN = "";
            string sumReportsN = "";
            int jcSum = 0;
            int csSum = 0;

            strTable += "<td>"
                     + ds.Tables[0].Rows[0][0].ToString() + "月份</td><td>"
                     + ds.Tables[0].Rows[0][2].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][3].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][4].ToString() + "</tr>";

            jcSum = int.Parse(ds.Tables[0].Rows[0][2].ToString());
            csSum = int.Parse(ds.Tables[0].Rows[0][3].ToString());

            for (int i = 1; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][2].ToString();
                csReportsN = ds.Tables[0].Rows[i][3].ToString();
                sumReportsN = ds.Tables[0].Rows[i][4].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);

                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
        }
        else
        {
            strTable += "<td>" + dt.Month.ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            for (int mth = 1; mth < subMonth; mth++)
            {
                strTable += "<tr align='center'><td>" + (int.Parse(dt.Month.ToString()) + mth).ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";               
            }
            strTable += "<tr align='center'><td>总计</td><td>0</td><td>0</td><td>0</td></tr>";
        }
        totalM += subMonth;
        #endregion

        #region 环境空气

        strTable += "<tr align='center'><td rowspan = '" + (subMonth + 1).ToString() + "'>环境空气</td>";

        strSql = "select m as [Date],s.typeid,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> 13 THEN n.num ELSE 0 END) AS 监测报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 13 THEN n.num ELSE 0 END) AS 测试报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";

        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and s.typeid = '4' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m,s.typeid";


        ds = new MyDataOp(strSql).CreateDataSet();
        m = ds.Tables[0].Rows.Count;
        if (m != 0)
        {
            string theMonths = "";
            string jcReportsN = "";
            string csReportsN = "";
            string sumReportsN = "";
            int jcSum = 0;
            int csSum = 0;

            strTable += "<td>"
                     + ds.Tables[0].Rows[0][0].ToString() + "月份</td><td>"
                     + ds.Tables[0].Rows[0][2].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][3].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][4].ToString() + "</tr>";

            jcSum = int.Parse(ds.Tables[0].Rows[0][2].ToString());
            csSum = int.Parse(ds.Tables[0].Rows[0][3].ToString());

            for (int i = 1; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][2].ToString();
                csReportsN = ds.Tables[0].Rows[i][3].ToString();
                sumReportsN = ds.Tables[0].Rows[i][4].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);

                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";                
            }
            strTable += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
           
        }
        else
        {
            strTable += "<td>" + dt.Month.ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            for (int mth = 1; mth < subMonth; mth++)
            {
                strTable += "<tr align='center'><td>" + (int.Parse(dt.Month.ToString()) + mth).ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";              
            }
            strTable += "<tr align='center'><td>总计</td><td>0</td><td>0</td><td>0</td></tr>";          
        }
        totalM += subMonth;
        #endregion

        #region 土壤

        strTable += "<tr align='center'><td rowspan = '" + (subMonth + 1).ToString() + "'>土壤</td>";

        strSql = "select m as [Date],s.typeid,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> 13 THEN n.num ELSE 0 END) AS 监测报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 13 THEN n.num ELSE 0 END) AS 测试报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";

        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and s.typeid = '6' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m,s.typeid";


        ds = new MyDataOp(strSql).CreateDataSet();
        m = ds.Tables[0].Rows.Count;
        if (m != 0)
        {
            string theMonths = "";
            string jcReportsN = "";
            string csReportsN = "";
            string sumReportsN = "";
            int jcSum = 0;
            int csSum = 0;

            strTable += "<td>"
                     + ds.Tables[0].Rows[0][0].ToString() + "月份</td><td>"
                     + ds.Tables[0].Rows[0][2].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][3].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][4].ToString() + "</tr>";

            jcSum = int.Parse(ds.Tables[0].Rows[0][2].ToString());
            csSum = int.Parse(ds.Tables[0].Rows[0][3].ToString());
            for (int i = 1; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][2].ToString();
                csReportsN = ds.Tables[0].Rows[i][3].ToString();
                sumReportsN = ds.Tables[0].Rows[i][4].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);
                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";             
            }
            strTable += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
            
        }
        else
        {
            strTable += "<td>" + dt.Month.ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            for (int mth = 1; mth < subMonth; mth++)
            {
                strTable += "<tr align='center'><td>" + (int.Parse(dt.Month.ToString()) + mth).ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>0</td><td>0</td><td>0</td></tr>";
        }
        totalM += subMonth;
        #endregion

        #region 噪声

        strTable += "<tr align='center'><td rowspan = '" + (subMonth + 1).ToString() + "'>噪声</td>";

        strSql = "select m as [Date],s.typeid,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> 13 THEN n.num ELSE 0 END) AS 监测报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 13 THEN n.num ELSE 0 END) AS 测试报告,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 数据总量 ";
        strSql += "from t_M_ReporInfo r,t_m_sampleinfor s,t_m_monitoritem n,( select month('" + dtStartTime + "') m ";

        for (int mth = 1; mth < subMonth; mth++)
        {
            strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
        }

        strSql += ") aa ";
        strSql += "where n.ReportDate >= '" + dtStartTime + "' and n.ReportDate < '" + dtEndTime + "' ";
        strSql += "and s.typeid = '12' ";
        strSql += "and r.id = s.reportid ";
        strSql += "and s.id = n.sampleid ";
        strSql += "GROUP BY m,s.typeid";


        ds = new MyDataOp(strSql).CreateDataSet();
        m = ds.Tables[0].Rows.Count;
        if (m != 0)
        {
            string theMonths = "";
            string jcReportsN = "";
            string csReportsN = "";
            string sumReportsN = "";
            int jcSum = 0;
            int csSum = 0;

            strTable += "<td>"
                     + ds.Tables[0].Rows[0][0].ToString() + "月份</td><td>"
                     + ds.Tables[0].Rows[0][2].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][3].ToString() + "</td><td>"
                     + ds.Tables[0].Rows[0][4].ToString() + "</tr>";

            jcSum = int.Parse(ds.Tables[0].Rows[0][2].ToString());
            csSum = int.Parse(ds.Tables[0].Rows[0][3].ToString());

            for (int i = 1; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                jcReportsN = ds.Tables[0].Rows[i][2].ToString();
                csReportsN = ds.Tables[0].Rows[i][3].ToString();
                sumReportsN = ds.Tables[0].Rows[i][4].ToString();

                jcSum += int.Parse(jcReportsN);
                csSum += int.Parse(csReportsN);

                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + jcReportsN + "</td><td>" + csReportsN + "</td><td>" + sumReportsN + "</td></tr>";            
            }
            strTable += "<tr align='center'><td>总计</td><td>" + jcSum.ToString() + "</td><td>" + csSum.ToString() + "</td><td>" + (jcSum + csSum).ToString() + "</td></tr>";
            
        }
        else
        {
            strTable += "<td>" + dt.Month.ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";           
            for (int mth = 1; mth < subMonth; mth++)
            {
                strTable += "<tr align='center'><td>" + (int.Parse(dt.Month.ToString()) + mth).ToString() + "月份</td><td>0</td><td>0</td><td>0</td></tr>";               
            }
            strTable += "<tr align='center'><td>总计</td><td>0</td><td>0</td><td>0</td></tr>";           
        }
        totalM += subMonth;
        #endregion

        strTable += "</tbody></table>";
    }

    protected void CheckLogin()
    {
        if (Request.Cookies["Cookies"].Values["u_id"] == null)
        {
            Response.Write("<script language='javascript'>alert('您没有权限进入本页或当前登录用户已过期！\\n请重新登录或与管理员联系！');parent.location='../login.aspx';</script>");
        }
        string strPageName = Request.Url.AbsolutePath;
        strPageName = strPageName.Substring(strPageName.LastIndexOf("/") + 1);

        string strSql = "select count(*) from t_R_Role,t_R_RoleMenu,t_R_Menu " +
           "where t_R_Role.RoleID='" + Request.Cookies["Cookies"].Values["u_role"] +
           "' and t_R_Menu.RelativeFile like '%" + strPageName +
           "%' and t_R_Role.RoleID=t_R_RoleMenu.RoleID and t_R_RoleMenu.checked='1' and t_R_RoleMenu.MenuID=t_R_Menu.ID";
        MyDataOp mdo = new MyDataOp(strSql);
        DataSet ds = mdo.CreateDataSet();

        int intRow = Convert.ToUInt16(ds.Tables[0].Rows[0][0].ToString());
        if (intRow == 0)
        {
            Response.Write("<script language='javascript'>alert('您没有权限进入本页！\\n请重新登录或与管理员联系！');parent.location='../login.aspx';</script>");
        }
    }
}
