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

public partial class Reports_MonitorItemReport : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_s.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_e.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_endtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            //txt_receivetime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_QueryEndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_ReportTime.Attributes.Add("OnFocus", "javascript:calendar()");
            //txt_AccessTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //txt_ReportTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
           
            ListItem choose = new ListItem("请选择", "-1");
            ListItem chose = new ListItem("全部", "-2");
           
            
            
            MyStaVoid.BindList("ItemName", "ItemID", "select ItemID, ItemName from t_M_ItemInfo  order by ItemName ", txt_item);
            txt_item.Items.Add(choose);
            txt_item.Items.Add(chose);
            txt_item.SelectedValue = "-1";
            MyStaVoid.BindList("SampleType", "TypeID", "select TypeID, SampleType from t_M_SampleType  order by SampleType ", txt_type);
            txt_type.Items.Add(choose);
            txt_type.Items.Add(chose);
            txt_type.SelectedValue = "-1";
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClass order by ClassCode asc", DropList_AnalysisMainItem);
            DropList_AnalysisMainItem.Items.Add(choose);
            DropList_AnalysisMainItem.SelectedValue = "-1";
            DropList_AnalysisMainItem_SelectedIndexChanged(null, null);
            //MyStaVoid.BindList("Status", "StatusID", "select StatusID, Status from t_M_StatusInfo  order by StatusID ", drop_status);
            //drop_status.Items.Add(choose);
            //drop_status.Items.FindByValue("-1").Selected = true;
            //MyStaVoid.BindList("ClientName", "id", "select id,ClientName from t_M_ClientInfo  order by id ", drop_client);
            //drop_client.Items.Add(choose);
            //drop_client.Items.FindByValue("-1").Selected = true;

            //RadioButtonList1.SelectedValue = "1";

            btn_query_Click(null,null);

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%;  COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>监测数据分析统计表</b></font>"; 
        }
    }
  

   
    
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        btn_query_Click(null, null);
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

            e.Row.Cells[1].Visible = false;
           
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

            e.Row.Cells[4].Visible = false;
        }
    }
    #endregion
   
 
    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
       //string strSample="";//样品编号
        string strDate="";//接样时间
        string strDateR = "";//接样时间
        //string strStatus = "";//样品状态
        string strItem="";//项目类型
        string strType="";//样品名称
        //string strClient="";//委托单位
        string strAnalysis = "";// 分析项目;
        //if(txt_samplequery.Text!="")//按样品编号
        //    strSample="and SampleID like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text.Trim() != "" && txt_QueryEndTime.Text.Trim() != "")//按采样时间
        {
            DateTime start=DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00");
            DateTime end=DateTime.Parse(txt_QueryEndTime.Text.Trim() + " 23:59:59");
            strDate = " and AccessDate between '"+ start+"' and '"+end+"'";
            //strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        }
        if (txt_s.Text.Trim() != "" && txt_e.Text.Trim() != "")//按采样时间
        {
            DateTime starts = DateTime.Parse(txt_s.Text.Trim() + " 00:00:00");
            DateTime ende = DateTime.Parse(txt_e.Text.Trim() + " 23:59:59");
            strDateR = " and t_M_MonitorItem.ReportDate between '" + starts + "' and '" + ende + "'";
            //strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        }
        //if(drop_status.SelectedValue!="-1")//按样品状态
        //{
        //    strStatus = " and t_M_SampleInfor.StatusID='" + drop_status.SelectedValue + "' ";
        //}
        if (txt_item.SelectedValue != "-1" && txt_item.SelectedValue != "-2")// 按项目类型
        {
            strItem = " and  t_M_ReporInfo.ItemType='" + txt_item.SelectedValue + "' ";
        }
        
        if (txt_type.SelectedValue != "-1" && txt_type.SelectedValue!="-2")//按样品类型
        {
            strType = " and t_M_SampleInfor.TypeID='" + txt_type.SelectedValue + "' ";
        }
        //if (drop_client.SelectedValue != "-1")//按委托单位
        //{
        //    strClient = " and ClientID='" + drop_client.SelectedValue + "' ";
        //}
        bool flag=false;
        int i = 0;
        //string flagtype ="";
        //if(RadioButtonList1.SelectedValue==1)
        //    flagtype = " and ";
        //else
        //flagtype = " or ";
        foreach (ListItem item in cb_analysisList.Items)
        {
            if (item.Selected)
            {
                
                flag=true;
                if (i == 0)
                strAnalysis += " and (MonitorItem='"+item.Value+"'";
                else
                    strAnalysis += " or MonitorItem='" + item.Value + "'";
                

                i++;
            }
        }
        //按选中的分析项目查询
      
        if (flag)
        {
            strAnalysis += ")";
            
        }
        string strSql="";
        DataSet ds;
    //    if ((txt_type.SelectedValue == "-2" && txt_item.SelectedValue == "-2") || (txt_item.SelectedValue == "-2" && txt_type.SelectedValue != "-1") || (txt_type.SelectedValue == "-2" && txt_item.SelectedValue != "-1"))
    //        {
    //            strSql = @"SELECT DISTINCT t_M_ItemInfo.ItemName AS 项目类型, t_M_SampleType.SampleType AS 样品类型,t_M_AnalysisItem.AIName AS 分析项目, SUM(temp1.数量) AS 数量 " +
    //    " FROM t_M_ItemInfo INNER JOIN (SELECT DISTINCT t_M_MonitorItem.MonitorItem, t_M_ReporInfo.ItemType, TypeID, sum(t_M_MonitorItem.Num) AS 数量  FROM t_M_ReporInfo inner join t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID INNER JOIN t_M_MonitorItem ON t_M_SampleInfor.id = t_M_MonitorItem.SampleID " + strDate + strItem + strType +
    //             " GROUP BY t_M_MonitorItem.MonitorItem, TypeID, t_M_ReporInfo.ItemType) temp1 ON" +
    //          " t_M_ItemInfo.ItemID = temp1.ItemType INNER JOIN" +
    //          " t_M_SampleType ON temp1.TypeID = t_M_SampleType.TypeID INNER JOIN" +
    //          " t_M_AnalysisItem ON temp1.MonitorItem = t_M_AnalysisItem.id" + strAnalysis + " GROUP BY t_M_ItemInfo.ItemName, t_M_SampleType.SampleType,t_M_AnalysisItem.AIName";

    //       }
    //    else if (txt_item.SelectedValue == "-2" && txt_type.SelectedValue == "-1")
    //    {
    //        strSql = @"SELECT DISTINCT t_M_ItemInfo.ItemName AS 项目类型,  t_M_AnalysisItem.AIName AS 分析项目, SUM(temp1.数量) AS 数量 " +
    //" FROM t_M_ItemInfo INNER JOIN (SELECT DISTINCT t_M_MonitorItem.MonitorItem, t_M_ReporInfo.ItemType,sum(t_M_MonitorItem.Num) AS 数量 FROM t_M_ReporInfo inner join t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID INNER JOIN t_M_MonitorItem ON t_M_SampleInfor.id = t_M_MonitorItem.SampleID " + strDate + strItem + strType +
    //         " GROUP BY t_M_MonitorItem.MonitorItem,  t_M_ReporInfo.ItemType) temp1 ON" +
    //      " t_M_ItemInfo.ItemID = temp1.ItemType INNER JOIN" +
    //      " t_M_AnalysisItem ON temp1.MonitorItem = t_M_AnalysisItem.id" + strAnalysis + " GROUP BY t_M_ItemInfo.ItemName, t_M_AnalysisItem.AIName";

    //    }

    //    else if (txt_item.SelectedValue == "-1" && txt_type.SelectedValue == "-2")
    //    {
    //        strSql = @"SELECT DISTINCT t_M_SampleType.SampleType AS 样品类型,  t_M_AnalysisItem.AIName AS 分析项目, SUM(temp1.数量) AS 数量 " +
    // " FROM (SELECT t_M_MonitorItem.MonitorItem,  TypeID, sum(t_M_MonitorItem.Num) AS 数量  FROM t_M_ReporInfo inner join t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID INNER JOIN t_M_MonitorItem ON t_M_SampleInfor.id = t_M_MonitorItem.SampleID " + strDate + strItem + strType +
    //          " GROUP BY t_M_MonitorItem.MonitorItem, TypeID) temp1 inner join " +

    //       " t_M_SampleType ON temp1.TypeID = t_M_SampleType.TypeID INNER JOIN" +
    //       " t_M_AnalysisItem ON temp1.MonitorItem = t_M_AnalysisItem.id" + strAnalysis + " GROUP BY  t_M_SampleType.SampleType,t_M_AnalysisItem.AIName";
    //    }

   
    //        else
    //        {
    //            strSql = @"SELECT DISTINCT" +
    //     " t_M_AnalysisItem.AIName AS 分析项目, SUM(temp1.数量) " +
    //     "AS 数量" +
    //" FROM (SELECT " +
    //             "MonitorItem, sum(t_M_MonitorItem.Num) AS 数量" +
    //       " FROM t_M_ReporInfo inner join t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID INNER JOIN " +
    //            " t_M_MonitorItem ON t_M_SampleInfor.id = t_M_MonitorItem.SampleID " + strDate + strItem + strType +
    //       " GROUP BY t_M_MonitorItem.MonitorItem, t_M_ReporInfo.ItemType) temp1 INNER JOIN " +
    //     "t_M_AnalysisItem ON t_M_AnalysisItem.id = temp1.MonitorItem " + strAnalysis + " GROUP BY t_M_AnalysisItem.AIName";

            //}



        string conditonstr="";

        if (txt_type.SelectedValue == "-2" && txt_item.SelectedValue == "-2")
        {
            if (strDate != "" || strType != "" || strItem != "" || strDateR!="")
            {
                conditonstr = "where t_M_ReporInfo.StatusID<>7 " + strDate + strType + strDateR;
            }
            strSql = @"SELECT MonitorItem, SUM(CASE WHEN t_M_MonitorItem.ReportDate IS not NULL THEN t_M_MonitorItem.num ELSE 0 END) AS 数量 ,t_M_ReporInfo.ItemType,t_M_SampleInfor.TypeID" +
    " FROM t_M_MonitorItem " +
    " inner join t_M_SampleInfor on t_M_MonitorItem.SampleID=t_M_SampleInfor.id INNER JOIN" +
                   " t_M_ReporInfo ON t_M_SampleInfor.ReportID = t_M_ReporInfo.id " + conditonstr + strAnalysis + strItem +
    " GROUP BY t_M_ReporInfo.ItemType,t_M_SampleInfor.TypeID,MonitorItem ORDER BY t_M_ReporInfo.ItemType,t_M_SampleInfor.TypeID, MonitorItem";
             ds = new MyDataOp(strSql).CreateDataSet();
            DataColumn dc1 = new DataColumn("项目类型");
            ds.Tables[0].Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("样品类型");
            ds.Tables[0].Columns.Add(dc2);
            DataColumn dc = new DataColumn("分析项目");
            ds.Tables[0].Columns.Add(dc);
            DataColumn dcc = new DataColumn("数据量");
            ds.Tables[0].Columns.Add(dcc);
            string str1 = "select id,AIName from t_M_AnalysisItemEx";
            string str2 = "select ItemID,ItemName from t_M_ItemInfo";
            string str3 = "select TypeID,SampleType from t_M_SampleType";
            
            
            DataSet ds1 = new MyDataOp(str1).CreateDataSet();
            DataSet ds2 = new MyDataOp(str2).CreateDataSet();
            DataSet ds3= new MyDataOp(str3).CreateDataSet();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drr in ds1.Tables[0].Rows)
                {
                    if (dr["MonitorItem"].ToString() == drr["id"].ToString())
                        dr["分析项目"] = drr["AIName"].ToString();
                }
                dr["数据量"] = dr["数量"].ToString();
                foreach (DataRow drr2 in ds2.Tables[0].Rows)
                {
                    if (dr["ItemType"].ToString() == drr2["ItemID"].ToString())
                        dr["项目类型"] = drr2["ItemName"].ToString();
                }
                foreach (DataRow drr3 in ds3.Tables[0].Rows)
                {
                    if (dr["TypeID"].ToString() == drr3["TypeID"].ToString())
                        dr["样品类型"] = drr3["SampleType"].ToString();
                }
            }
 
        }
        else if(txt_type.SelectedValue == "-2")
        {
            if (strDate != "" || strType != "" || strItem != "" ||strDateR!="")
            {
                conditonstr = "where t_M_ReporInfo.StatusID<>7 " + strDate + strType + strDateR;
            }
            strSql = @"SELECT MonitorItem,SUM(CASE WHEN t_M_MonitorItem.ReportDate IS not  NULL THEN t_M_MonitorItem.num ELSE 0 END) AS 数量量 ,t_M_SampleInfor.TypeID, MonitorItem " +
    " FROM t_M_MonitorItem " +
    " inner join t_M_SampleInfor on t_M_MonitorItem.SampleID=t_M_SampleInfor.id INNER JOIN" +
                   " t_M_ReporInfo ON t_M_SampleInfor.ReportID = t_M_ReporInfo.id " + conditonstr + strAnalysis + strItem +
    " GROUP BY t_M_SampleInfor.TypeID, MonitorItem ORDER BY t_M_SampleInfor.TypeID,MonitorItem";
             ds = new MyDataOp(strSql).CreateDataSet();
            DataColumn dc2 = new DataColumn("样品类型");
            ds.Tables[0].Columns.Add(dc2);
            DataColumn dc = new DataColumn("分析项目");
            ds.Tables[0].Columns.Add(dc);
            DataColumn dcc = new DataColumn("数据量");
            ds.Tables[0].Columns.Add(dcc);
         
            string str1 = "select id,AIName from t_M_AnalysisItemEx";
           // string str2 = "select ItemID,ItemName from t_M_ItemInfo";
            string str3 = "select TypeID,SampleType from t_M_SampleType";


            DataSet ds1 = new MyDataOp(str1).CreateDataSet();
            //DataSet ds2 = new MyDataOp(str2).CreateDataSet();
            DataSet ds3 = new MyDataOp(str3).CreateDataSet();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drr in ds1.Tables[0].Rows)
                {
                    if (dr["MonitorItem"].ToString() == drr["id"].ToString())
                        dr["分析项目"] = drr["AIName"].ToString();
                }
                dr["数据量"] = dr["数量"].ToString();
                //foreach (DataRow drr2 in ds2.Tables[0].Rows)
                //{
                //    if (dr["ItemType"].ToString() == drr2["ItemID"].ToString())
                //        dr["项目类型"] = drr2["ItemName"].ToString();
                //}
                foreach (DataRow drr3 in ds3.Tables[0].Rows)
                {
                    if (dr["TypeID"].ToString() == drr3["TypeID"].ToString())
                        dr["样品类型"] = drr3["SampleType"].ToString();
                }
            }
 
        }
        else if (txt_item.SelectedValue == "-2")
        {
            if (strDate != "" || strType != "" || strItem != ""||strDateR!="")
            {
                conditonstr = "where t_M_ReporInfo.StatusID<>7 " + strDate + strType + strDateR;
            }
            strSql = @"SELECT MonitorItem, SUM(CASE WHEN t_M_MonitorItem.ReportDate IS not NULL THEN t_M_MonitorItem.num ELSE 0 END) AS 数量,t_M_ReporInfo.ItemType, MonitorItem " +
    "FROM t_M_MonitorItem " +
    " inner join t_M_SampleInfor on t_M_MonitorItem.SampleID=t_M_SampleInfor.id INNER JOIN" +
                   " t_M_ReporInfo ON t_M_SampleInfor.ReportID = t_M_ReporInfo.id " + conditonstr + strAnalysis + strItem +
    " GROUP BY t_M_ReporInfo.ItemType, MonitorItem ORDER BY t_M_ReporInfo.ItemType, MonitorItem";
           ds = new MyDataOp(strSql).CreateDataSet();
            DataColumn dc1 = new DataColumn("项目类型");
            ds.Tables[0].Columns.Add(dc1);
            DataColumn dc = new DataColumn("分析项目");
            ds.Tables[0].Columns.Add(dc);
            DataColumn dcc = new DataColumn("数据量");
            ds.Tables[0].Columns.Add(dcc);
            string str1 = "select id,AIName from t_M_AnalysisItemEx";
            string str2 = "select ItemID,ItemName from t_M_ItemInfo";
           // string str3 = "select TypeID,SampleType from t_M_SampleType";


            DataSet ds1 = new MyDataOp(str1).CreateDataSet();
            DataSet ds2 = new MyDataOp(str2).CreateDataSet();
            //DataSet ds3 = new MyDataOp(str3).CreateDataSet();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drr in ds1.Tables[0].Rows)
                {
                    if (dr["MonitorItem"].ToString() == drr["id"].ToString())
                        dr["分析项目"] = drr["AIName"].ToString();
                }
                dr["数据量"] = dr["数量"].ToString();
                foreach (DataRow drr2 in ds2.Tables[0].Rows)
                {
                    if (dr["ItemType"].ToString() == drr2["ItemID"].ToString())
                        dr["项目类型"] = drr2["ItemName"].ToString();
                }
                //foreach (DataRow drr3 in ds3.Tables[0].Rows)
                //{
                //    if (dr["TypeID"].ToString() == drr3["TypeID"].ToString())
                //        dr["样品类型"] = drr3["SampleType"].ToString();
                //}
            }
 
        }
        else
        {
            if (strDate != "" || strType != "" || strItem != ""|| strDateR!="")
            {
                conditonstr = "where t_M_ReporInfo.StatusID<>7 " + strDate + strType + strDateR;
            }
            strSql = @"SELECT MonitorItem, SUM(CASE WHEN t_M_MonitorItem.ReportDate IS not NULL THEN t_M_MonitorItem.num ELSE 0 END) AS 数量, MonitorItem, MonitorItem " +
    "FROM t_M_MonitorItem " +
    " inner join t_M_SampleInfor on t_M_MonitorItem.SampleID=t_M_SampleInfor.id INNER JOIN" +
                   " t_M_ReporInfo ON t_M_SampleInfor.ReportID = t_M_ReporInfo.id " + conditonstr + strAnalysis + strItem +
    " GROUP BY MonitorItem ORDER BY MonitorItem";
            ds = new MyDataOp(strSql).CreateDataSet();
            DataColumn dc = new DataColumn("分析项目");
            ds.Tables[0].Columns.Add(dc);
            DataColumn dcc = new DataColumn("数据量");
            ds.Tables[0].Columns.Add(dcc);
            string str1 = "select id,AIName from t_M_AnalysisItemEx";
            DataSet ds1 = new MyDataOp(str1).CreateDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drr in ds1.Tables[0].Rows)
                {
                    if (dr["MonitorItem"].ToString() == drr["id"].ToString())
                        dr["分析项目"] = drr["AIName"].ToString();
                }
                dr["数据量"] = dr["数量"].ToString();
            }
        }
       

           
       
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
            int intColumnCount = grdvw_List.Rows[0].Cells.Count;
            grdvw_List.Rows[0].Cells.Clear();
            grdvw_List.Rows[0].Cells.Add(new TableCell());
            grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
        } 
    }
    protected void DropList_AnalysisMainItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds;
        string str;
        if (DropList_AnalysisMainItem.SelectedValue.ToString() == "-1")
        {
            str = "select id,AIName from t_M_AnalysisItemEx";
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        else
        {
            str = "select id,AIName from t_M_AnalysisItemEx where ClassID='" + DropList_AnalysisMainItem.SelectedValue.ToString() + "'";
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
       

    }
}