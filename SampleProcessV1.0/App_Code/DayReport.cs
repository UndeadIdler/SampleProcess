using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using WebApp.Components;

/// <summary>
///DayReport 的摘要说明
/// </summary>
public class DayReport
{
	public DayReport()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public void analysis(DateTime s, DateTime e)
    {

       
        DateTime dt_s = s;//DateTime.Parse(DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd")+" 00:00:00");//DateTime.Parse("2009-11-01 00:00:00");
        DateTime dt_e = e; //DateTime.Parse(DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd")+" 23:59:59");//DateTime.Parse("2009-11-01 23:59:59");//
        for (DateTime dt = s; dt < e; )
        {
            if (dt.AddHours(24) == e)
            {
                dt_s = s; dt_e = e;
                dt = dt.AddHours(24);
            }
            else
            {
                dt_s = dt;
                dt_e = dt.AddDays(1).AddSeconds(-1);
                dt = dt.AddDays(1);
            }

            string str = @"SELECT SUM(N) AS Expr1, MonitorItem, CreateDate " +
"FROM (SELECT SUM(num) AS N, MonitorItem, LEFT(CONVERT(varchar, AccessDate, 120), 10) CreateDate " +
       " FROM t_M_MonitorItem INNER JOIN" +
           "   t_M_SampleInfor ON " +
             " t_M_SampleInfor.id = t_M_MonitorItem.SampleID" +
        " WHERE (AccessDate BETWEEN '" + dt_s + "' AND '" + dt_e + "')" +
        " GROUP BY MonitorItem, AccessDate) a" +
" GROUP BY MonitorItem, CreateDate" +
" ORDER BY CreateDate, MonitorItem";
        DataSet ds = new MyDataOp(str).CreateDataSet();
        string str2 = @"SELECT SUM(N) AS Expr1, MonitorItem, ReportDate " +
"FROM (SELECT SUM(num) AS N, MonitorItem, LEFT(CONVERT(varchar, t_M_MonitorItem.ReportDate, 120), 10) ReportDate " +
       " FROM t_M_MonitorItem INNER JOIN" +
           "   t_M_SampleInfor ON " +
             " t_M_SampleInfor.id = t_M_MonitorItem.SampleID inner join t_M_ReporInfo ON t_M_SampleInfor.ReportID = t_M_ReporInfo.id where " +
        " (t_M_MonitorItem.ReportDate BETWEEN '" + dt_s + "' AND '" + dt_e + "') " +
        " GROUP BY MonitorItem, t_M_MonitorItem.ReportDate) a" +
" GROUP BY MonitorItem, ReportDate" +
" ORDER BY ReportDate, MonitorItem";
            //"select sum(N),MonitorItem,ReportDate from (select sum(num) as N,MonitorItem,LEFT(CONVERT(varchar, ReportDate, 120), 10) ReportDate from t_M_MonitorItem where (ReportDate between '" + dt_s + "' and '" + dt_e + "') group by MonitorItem,ReportDate) as b group by MonitorItem,ReportDate ";
        DataSet ds2 = new MyDataOp(str2).CreateDataSet();
        int i=0;
        int m=1;
        m=ds.Tables[0].Rows.Count+ds2.Tables[0].Rows.Count;
         string[] ListStr=new string[m];
        foreach (DataRow dr in ds.Tables[0].Rows)
        {//检查统计的日期的数据是否已存在
            string checkstr = "select cdate from t_M_AccessDayInfo where cdate='" + dr[2].ToString() + "' and itemid='" + dr[1].ToString() + "'";
            DataSet checkds = new MyDataOp(checkstr).CreateDataSet();
            if(checkds.Tables[0].Rows.Count==0)

            ListStr.SetValue("Insert into t_M_AccessDayInfo(itemid,num,cdate,ttdate)values('" + dr[1].ToString() + "','" + dr[0].ToString() + "','" + dr[2].ToString() + "',getdate())", i++);
            else
                ListStr.SetValue("update t_M_AccessDayInfo set num='" + dr[0].ToString() + "',ttdate=getdate() where cdate='" + dr[2].ToString() + "' and itemid='" + dr[1].ToString() + "'", i++);


        }

        
         foreach (DataRow dr in ds2.Tables[0].Rows)
         {
             string checkstr = "select cdate from t_M_EndDayInfo where cdate='" + dr[2].ToString() + "'";
             DataSet checkds = new MyDataOp(checkstr).CreateDataSet();
             if (checkds.Tables[0].Rows.Count == 0)

                 ListStr.SetValue("Insert into t_M_EndDayInfo(itemid,num,cdate,ttdate)values('" + dr[1].ToString() + "','" + dr[0].ToString() + "','" + dr[2].ToString() + "',getdate())", i++);
             else
                 ListStr.SetValue("update t_M_EndDayInfo set num='" + dr[0].ToString() + "',ttdate=getdate()) where cdate='" + dr[2].ToString() + "' and itemid='" + dr[1].ToString() + "'", i++);
         }
         if (i > 0)
         {
             MyDataOp doOp = new MyDataOp(str);
             bool status = doOp.DoTran(i, ListStr);
             if (!status)
             {
                 WebApp.Components.Log.SaveLog("自动统计日报失败！" + DateTime.Now.ToString(), "1", 0);
             }
         }
        }
    }
}
