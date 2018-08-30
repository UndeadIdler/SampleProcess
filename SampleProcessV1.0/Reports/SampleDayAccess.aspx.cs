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

public partial class Reports_SampleDayAccess : System.Web.UI.Page
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
         // grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #3333FF;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>每日样品接收量统计</b></font>";
          // grdvw_List.Attributes.Add("style", "table-layout:fixed;");
        }
    }
   public void Query()
    {
        DateTime s = DateTime.Parse("2010-6-16 00:00:00");
        DateTime end = DateTime.Parse(DateTime.Now.ToString());
        if (txt_StartTime.Text.Trim() != "")
        {
            s = DateTime.Parse(txt_StartTime.Text.Trim() + " 0:00:00");

        }
        if (txt_EndTime.Text.Trim() != "")
        {
            end = DateTime.Parse(txt_EndTime.Text.Trim() + " 23:59:59");

        }
        // string strItem = "select id,AIName from t_M_AnalysisItemEx order by id";
        string strItem = "Select ItemID,ItemName from t_M_ItemInfo ";
        DataSet ds = new MyDataOp(strItem).CreateDataSet();
        DataSet ds_new = ds.Clone();
        ds_new.Tables[0].Columns.Remove("ItemID");
        ds_new.Tables[0].Columns.Remove("ItemName");
        DataColumn dcdate = new DataColumn("日期");
        ds_new.Tables[0].Columns.Add(dcdate);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DataColumn dc = new DataColumn(dr["ItemName"].ToString(), typeof(Int16));
            DataColumn dcid = new DataColumn(dr["ItemID"].ToString(), typeof(Int16));
            ds_new.Tables[0].Columns.Add(dc);
            ds_new.Tables[0].Columns.Add(dcid);

        }
        string strToday = @" select  count(t_M_SampleInfor.id) AS N, ItemName,ItemType, LEFT(CONVERT(varchar, AccessDate, 120), 10) AccessDate " +
       " FROM" +
           "   t_M_SampleInfor inner join t_M_ReporInfo on t_M_ReporInfo.id=t_M_SampleInfor.ReportID inner join t_M_ItemInfo on t_M_ItemInfo.ItemID=t_M_ReporInfo.ItemType" +
        " WHERE (AccessDate BETWEEN '" + s + "' AND '" + end + "') GROUP BY ItemName, AccessDate,ItemType" +
" ORDER BY AccessDate, ItemName";
        //将统计数据加入到DataSet中
       
       DataSet dsData = new MyDataOp(strToday).CreateDataSet();
        if (dsData.Tables[0].Rows.Count > 0)
        {
            for (DateTime dt = s; dt < end; )
            {
                DataRow dr = ds_new.Tables[0].NewRow();
                dr["日期"] = dt.Date.ToString("yyyy-MM-dd");
                for (int i =2; i < ds_new.Tables[0].Columns.Count; i=i+2)
                {
                    DataRow[] drdata = dsData.Tables[0].Select("AccessDate='" + dt.Date.ToString("yyyy-MM-dd") + "' and ItemType='" + ds_new.Tables[0].Columns[i].ColumnName + "'");

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
          
            ds_new.Tables[0].AcceptChanges();
            total[0] = "合计";
            for (int j = 1; j < ds_new.Tables[0].Columns.Count; j = j + 2 )
            {
                if (j%2==1)
                {
                    object temp = ds_new.Tables[0].Compute("sum([" + ds_new.Tables[0].Columns[j].ColumnName + "])", "");
                    total[j] = Int16.Parse(temp.ToString());
                }
               
            }
            ds_new.Tables[0].Rows.Add(total);
        }
      
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



            //TableCell headerset = new TableCell();
            //headerset.Text = "详细/编辑";
            //headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerset.Width = 60;
            //e.Row.Cells.Add(headerset);

            //TableCell headerUp = new TableCell();
            //headerUp.Text = "上传文件";
            //headerUp.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerUp.Width = 60;
            //e.Row.Cells.Add(headerUp);

            //TableCell headerDel = new TableCell();
            //headerDel.Text = "删除";
            //headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDel.Width = 30;
            //e.Row.Cells.Add(headerDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();



            //TableCell MenuSet = new TableCell();
            //MenuSet.Width = 60;
            //MenuSet.Style.Add("text-align", "center");
            //ImageButton btMenuSet = new ImageButton();
            //btMenuSet.ImageUrl = "~/images/Detail.gif";
            //btMenuSet.CommandName = "Edit";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuSet.Controls.Add(btMenuSet);
            //e.Row.Cells.Add(MenuSet);
            ////上传文件
            //TableCell MenuUp = new TableCell();
            //MenuUp.Width = 60;
            //MenuUp.Style.Add("text-align", "center");
            //ImageButton btMenuUp = new ImageButton();
            //btMenuUp.ImageUrl = "~/images/Detail.gif";
            //btMenuUp.CommandName = "Select";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuUp.Controls.Add(btMenuUp);
            //e.Row.Cells.Add(MenuUp);

            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/Images/Delete.gif";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            //tabcDel.Controls.Add(ibtnDel);
            //e.Row.Cells.Add(tabcDel);


        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            //e.Row.Cells[1].Visible = false;
            ////e.Row.Cells[7].Visible = false;
            ////e.Row.Cells[8].Visible = false;
         

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
    protected void btn_CreateReport_Click(object sender,EventArgs e)
    {
        Query();
    }

   
}
