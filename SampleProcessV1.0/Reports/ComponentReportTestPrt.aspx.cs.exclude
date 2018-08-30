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

public partial class ComponentReportTestPrt : System.Web.UI.Page 
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
        Label_H.Text = "<font size='3'>" + DateTime.Parse(date[0]).ToString("yyyy年MM月") + "至" + DateTime.Parse(date[1]).ToString("yyyy年MM月") + " 测试报告数据组成表</font>";

        strTable = "<table id='tableid' class='listTable2'><tr align='center'><th>月份</th><th>南湖区</th><th>秀洲区</th><th>联合污水</th><th>五县</th><th>其他</th></tr>";

        string strSql = "select m as [Date],";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID = 1 THEN 1 ELSE 0 END) AS 南湖区,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID = 2 THEN 1 ELSE 0 END) AS 秀洲区,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID = 6 THEN 1 ELSE 0 END) AS 联合污水,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID = 5 THEN 1 ELSE 0 END) AS 五县,";
        strSql += "SUM(CASE WHEN datepart(month, n.ReportDate) = m and r.ClientID <> '' THEN 1 ELSE 0 END) AS 总量 ";
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

                strTable += "<tr align='center'><td>" + theMonths + "</td><td>" + nh + "</td><td>" + xz + "</td><td>" + lh + "</td><td>" + wx + "</td><td>" + qt + "</td></tr>";
            }
            strTable += "<tr align='center'><td>总计</td><td>" + nhN.ToString() + "</td><td>" + xzN.ToString() + "</td><td>" + lhN.ToString() + "</td><td>" + wxN.ToString() + "</td><td>" + qtN.ToString() + "</td></tr>";
            
        }
        else
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='90%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(date[0]).ToString("yyyy年MM月") + "至" + DateTime.Parse(date[1]).ToString("yyyy年MM月") + " 测试报告数据组成表</b></font></caption><tbody><tr align='center'><th>月份</th><th>南湖区</th><th>秀洲区</th><th>联合污水</th><th>五县</th><th>其他</th></tr>";
            strTable += "<tr align='center'><td>总计</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td></tr>";           
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
