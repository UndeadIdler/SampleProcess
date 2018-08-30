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

public partial class ComponentReportMonitorPrt : System.Web.UI.Page 
{
    public string strTable = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();        
        PrintReport();       
    }
    protected void PrintReport()
    {
        string pDate = Request.QueryString["pDate"].ToString();
        string[] date = pDate.Split(',');
        DateTime dtStartTime, dtEndTime;
        DateTime dt = Convert.ToDateTime(date[0]);
        DateTime dt2 = Convert.ToDateTime(date[1]);
        dtStartTime = Convert.ToDateTime(dt.Year + "-" + dt.Month + "-1");
        dtEndTime = Convert.ToDateTime(dt2.Year + "-" + dt2.Month + "-1");
        dtEndTime = dtEndTime.AddMonths(1);

        int subMonth = int.Parse(dt2.Month.ToString()) - int.Parse(dt.Month.ToString()) + 1;
        Label_H.Text = "<font size='3'>" + DateTime.Parse(date[0]).ToString("yyyy年MM月") + "至" + DateTime.Parse(date[1]).ToString("yyyy年MM月") + " 监测报告数据组成表</font>";

        strTable = "<table class='listTable2' width='90%'><tbody><tr align='center'><th>月份</th><th>泵站考核</th><th>环境质量</th><th>环评监测</th><th>委托监测</th><th>污水处理厂监督</th><th>验收</th><th>执法</th><th>重点源</th><th>周报</th><th>其他</th><th>飞行</th></tr>";

        string strSql = "select m as [Date],";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 19 THEN n.num ELSE 0 END) AS 泵站考核,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 4 THEN n.num ELSE 0 END) AS 环境质量,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 9 THEN n.num ELSE 0 END) AS 环评监测,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 10 THEN n.num ELSE 0 END) AS 委托监测,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 7 THEN n.num ELSE 0 END) AS 环污水处理厂监督,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 8 THEN n.num ELSE 0 END) AS 验收,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 2 THEN n.num ELSE 0 END) AS 执法,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 5 THEN n.num ELSE 0 END) AS 重点源,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 14 THEN n.num ELSE 0 END) AS 周报,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType = 1 THEN n.num ELSE 0 END) AS 飞行,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ItemType <> '' THEN n.num ELSE 0 END) AS 总量 ";
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
            string bzkh = "";       //泵站考核
            string hjzl = "";       //环境质量      
            string hpjc = "";       //环评监测
            string wtjc = "";       //委托监测
            string wsc = "";       //环污水处理厂监督
            string ys = "";       //验收
            string zf = "";       //执法
            string zdy = "";       //重点源
            string zb = "";       //周报
            string fx = "";       //飞行
            string zl = "";       //总量            
            string qt = "";        //其他

            int bzkhN = 0;       //泵站考核
            int hjzlN = 0;       //环境质量      
            int hpjcN = 0;       //环评监测
            int wtjcN = 0;       //委托监测
            int wscN = 0;       //环污水处理厂监督
            int ysN = 0;       //验收
            int zfN = 0;       //执法
            int zdyN = 0;       //重点源
            int zbN = 0;       //周报
            int fxN = 0;       //飞行
            int qtN = 0;       //其他

            for (int i = 0; i < m; i++)
            {
                theMonths = ds.Tables[0].Rows[i][0].ToString() + "月份";
                bzkh = ds.Tables[0].Rows[i][1].ToString();
                hjzl = ds.Tables[0].Rows[i][2].ToString();
                hpjc = ds.Tables[0].Rows[i][3].ToString();
                wtjc = ds.Tables[0].Rows[i][4].ToString();
                wsc = ds.Tables[0].Rows[i][5].ToString();
                ys = ds.Tables[0].Rows[i][6].ToString();
                zf = ds.Tables[0].Rows[i][7].ToString();
                zdy = ds.Tables[0].Rows[i][8].ToString();
                zb = ds.Tables[0].Rows[i][9].ToString();
                fx = ds.Tables[0].Rows[i][10].ToString();
                zl = ds.Tables[0].Rows[i][11].ToString();

                qt = (int.Parse(zl) - (int.Parse(bzkh) + int.Parse(hjzl) + int.Parse(hpjc) + int.Parse(wtjc) + int.Parse(wsc) + int.Parse(ys) + int.Parse(zf) + int.Parse(zdy) + int.Parse(zb) + int.Parse(fx))).ToString();

                bzkhN += int.Parse(bzkh);
                hjzlN += int.Parse(hjzl);
                hpjcN += int.Parse(hpjc);
                wtjcN += int.Parse(wtjc);
                wscN += int.Parse(wsc);
                ysN += int.Parse(ys);
                zfN += int.Parse(zf);
                zdyN += int.Parse(zdy);
                zbN += int.Parse(zb);
                fxN += int.Parse(fx);
                qtN += int.Parse(qt);


                strTable += "<tr align='center'><td>" + theMonths +
                    "</td><td>" + bzkh +
                    "</td><td>" + hjzl +
                    "</td><td>" + hpjc +
                    "</td><td>" + wtjc +
                    "</td><td>" + wsc +
                    "</td><td>" + ys +
                    "</td><td>" + zf +
                    "</td><td>" + zdy +
                    "</td><td>" + zb +
                    "</td><td>" + qt +
                    "</td><td>" + fx + "</td></tr>";


            }
            strTable += "<tr align='center'><td>总计</td><td>" + bzkhN.ToString() +
                "</td><td>" + hjzlN.ToString() +
                "</td><td>" + hpjcN.ToString() +
                "</td><td>" + wtjcN.ToString() +
                "</td><td>" + wscN.ToString() +
                "</td><td>" + ysN.ToString() +
                "</td><td>" + zfN.ToString() +
                "</td><td>" + zdyN.ToString() +
                "</td><td>" + zbN.ToString() +
                "</td><td>" + qtN.ToString() +
                "</td><td>" + fxN.ToString() + "</td></tr>";

        }
        else
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='90%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(date[0]).ToString("yyyy年MM月") + "至" + DateTime.Parse(date[1]).ToString("yyyy年MM月") + " 监测数据统计表</b></font></caption><tbody><tr align='center'><th>月份</th><th>泵站考核</th><th>环境质量</th><th>环评监测</th><th>委托监测</th><th>污水处理厂监督</th><th>验收</th><th>执法</th><th>重点源</th><th>周报</th><th>其他</th><th>飞行</th></tr>";
            strTable += "<tr align='center'><td>总计</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td></tr>";
        }
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
