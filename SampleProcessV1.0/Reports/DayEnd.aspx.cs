using System;
using System.Collections;
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

public partial class Reports_DayEnd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txt_StartTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-MM-01");
            txt_EndTime.Text = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");

            Query();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #3333FF;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>每日样品完成量统计</b></font>";

        }
    }
   public void Query()
    {
       DateTime s=DateTime.Parse("2010-6-16 00:00:00");
       DateTime end=DateTime.Parse(DateTime.Now.ToString());
        if (txt_StartTime.Text.Trim() != "")
        {
            s=DateTime.Parse(txt_StartTime.Text.Trim()+" 0:00:00");

        }
       if( txt_EndTime.Text.Trim() != "")
       {
           end = DateTime.Parse(txt_EndTime.Text.Trim() + " 23:59:59");

       }
       string strItem = "Select  distinct MonitorItem,AIName from View_MonitorItemEnd where fxdate BETWEEN '" + s + "' AND '" + end + "' ";
       DataSet ds = new MyDataOp(strItem).CreateDataSet();
       DataSet ds_new = ds.Clone();
       DataColumn dcdate = new DataColumn("日期");
       ds_new.Tables[0].Columns.Add(dcdate);
       foreach (DataRow dr in ds.Tables[0].Rows)
       {
           DataColumn dc = new DataColumn(dr["AIName"].ToString() + "(" + dr["MonitorItem"].ToString() + ")",typeof(Int16));
           DataColumn dcid = new DataColumn(dr["MonitorItem"].ToString());
           ds_new.Tables[0].Columns.Add(dc);
           ds_new.Tables[0].Columns.Add(dcid);

       }
        //将统计数据加入到DataSet中
       string strToday = @"SELECT SUM(N) AS Expr1, MonitorItem, fxdate " +
"FROM (SELECT count(MonitorItem) AS N, MonitorItem, LEFT(CONVERT(varchar, AccessDate, 120), 10) fxdate " +
      " FROM t_MonitorItemDetail INNER JOIN" +
          "   t_M_SampleInfor ON " +
            " t_M_SampleInfor.id = t_MonitorItemDetail.SampleID" +
       " WHERE (fxdate BETWEEN '" + s + "' AND '" + end + "') and t_MonitorItemDetail.flag=3  GROUP BY MonitorItem, AccessDate) a" +
" GROUP BY MonitorItem, fxdate" +
" ORDER BY fxdate, MonitorItem";
        //string strData = "Select num,itemid,cdate from t_M_EndDayInfo order by itemid,cdate";
       DataSet dsData = new MyDataOp(strToday).CreateDataSet();
       
            for (DateTime dt = s; dt < end; )
            {
                DataRow dr = ds_new.Tables[0].NewRow();
                dr["日期"] = dt.Date.ToString("yyyy-MM-dd");
                for (int i =4; i < ds_new.Tables[0].Columns.Count; i=i+2)
                {
                    DataRow[] drdata = dsData.Tables[0].Select("fxdate='" + dt.Date.ToString("yyyy-MM-dd") + "' and MonitorItem='" + ds_new.Tables[0].Columns[i].ColumnName + "'");

                    if (drdata.Length > 0)
                    {
                       
                        dr[i-1] = drdata[0][0].ToString();
                    }
                    else
                        dr[i-1] = "0";
                  
               }
                 
                ds_new.Tables[0].Rows.Add(dr);
                dt = dt.AddDays(1);
               
               
               
        }
            DataRow total = ds_new.Tables[0].NewRow();
            ds_new.Tables[0].Columns.Remove("MonitorItem");
            ds_new.Tables[0].Columns.Remove("AIName");
            ds_new.Tables[0].AcceptChanges();
           total[0] = "合计";
            for (int j = 1; j < ds_new.Tables[0].Columns.Count; j = j + 2)
            {
                if (j % 2 == 1)
                {
                    object temp = ds_new.Tables[0].Compute("sum([" + ds_new.Tables[0].Columns[j].ColumnName + "])", "");
                    total[j] = Int16.Parse(temp.ToString());
                }

            }
            ds_new.Tables[0].Rows.Add(total);
        if (ds_new.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds_new.Tables[0].Rows.Add(ds_new.Tables[0].NewRow());
            grdvw_List.DataSource = ds_new;
            grdvw_List.DataBind();
            int intColumnCount = grdvw_List.Rows[0].Cells.Count;
            grdvw_List.Rows[0].Cells.Clear();
            grdvw_List.Rows[0].Cells.Add(new TableCell());
            grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_List.DataSource = ds_new;
            grdvw_List.DataBind();
        }
            

    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
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
           
            foreach (TableCell ce in e.Row.Cells)
            {
                int j = e.Row.Cells.GetCellIndex(ce);
                //for (int j = 0; j < e.Row.Cells.GetCellIndex; j++)
                //{
                if (j % 2 == 1)
                    e.Row.Cells[j].Visible = false;
            }
            e.Row.Cells[1].Visible = true;
            }
      
           

        }
    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query();
    }

   
}
