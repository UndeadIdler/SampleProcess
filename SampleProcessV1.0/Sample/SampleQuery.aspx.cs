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
using System.Text;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Word;
using WebApp.Components;

public partial class Sample_SampleListQuery3 : System.Web.UI.Page
{
   
    private int strSelectedId//所选择操作列记录对应的id
    {
        get { return (int)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strSampleId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSampleId"]; }
        set { ViewState["strSampleId"] = value; }
    }
    private DataSet ds_dayin//所选择操作列记录对应的id
    {
        get { return (DataSet)ViewState["ds_dayin"]; }
        set { ViewState["ds_dayin"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_endtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
           // txt_receivetime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_QueryEndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_ReportTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txt_QueryEndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClassEx order by ClassCode asc", cbl_type);
           ListItem choose = new ListItem("全部", "-1");
            //cbl_type.SelectedItem.Selected = false;
            //cbl_type.Items.Add(choose);
            //txt_type.Items.FindByValue("-1").Selected = true;
            //DropList_AnalysisMainItem_SelectedIndexChanged(null, null);
            MyStaVoid.BindList("ItemName", "ItemID", "select ItemID, ItemName from t_M_ItemInfo  order by ItemName ", txt_item);
            txt_item.Items.Add(choose);
            txt_item.Items.FindByValue("-1").Selected = true;
            //MyStaVoid.BindList("SampleType", "TypeID", "select TypeID, SampleType from t_M_SampleType  order by SampleType ", txt_type);
            //txt_type.Items.Add(choose);
            //txt_type.Items.FindByValue("-1").Selected = true;
            //MyStaVoid.BindList("Status", "StatusID", "select StatusID, Status from t_M_StatusInfo  order by StatusID ", drop_status);
            //drop_status.Items.Add(choose);
            //drop_status.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("ClientName", "id", "select id,ClientName from t_M_ClientInfo  order by id ", drop_client);
            drop_client.Items.Add(choose);
            drop_client.Items.FindByValue("-1").Selected = true;


            btn_query_Click(null,null);

            //grdvw_List.Caption = "<FONT style='WIDTH: 102.16%;font-size:22pt; LINE-HEIGHT: 150%; FONT-FAMILY: SimSun_GB2312; HEIGHT: 35px'><b>样 品 登 记 表</b></font>";

        }
    }
    #region 样品列表

   
    
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        btn_query_Click(null, null);
    }
   
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            TableCellCollection cells1 = e.Row.Cells;
            for (int i = 0; i < cells1.Count; i++)
            {
              //  cells1[i].Wrap = false; //设置此项切记 不要设置前台GridView宽度  
                cells1[i].Width = 75;
                if (i == 6)
                    cells1[i].Width = 220;
                else if (i ==7)
                    cells1[i].Width =30;
            }
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[0].Visible = false;
        }
    }
   
   
    #endregion

    #region 其它函数


    
   
    #endregion

    
    
    

    #endregion
   
   
   
   
    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
       string strSample="";//样品编号
        string strDate="";//接样时间
        string strStatus = "";//样品状态
        string strItem="";//项目类型
        string strType="";//样品名称
        string strClient="";//委托单位
        string strAnalysis = "";// 分析项目;
        string strAanlysisStatus = "";//样品分析状态
        string strUrgent = "";//按紧急程度
        if (Drop_Urgent.SelectedValue != "-1")
        {
            if (Drop_Urgent.SelectedValue=="1")
            strUrgent = "and t_M_ReporInfo.Ulevel=1";
            else
                strUrgent = "and (t_M_ReporInfo.Ulevel<>1 or t_M_ReporInfo.Ulevel is null)";
        }
        //样品原标识
        if (txt_pfk_query.Text.Trim() != "")
        { strSample += "and t_M_SampleInfor.SampleAddress like'%" + txt_pfk_query.Text + "%'"; }
        //报告标识
        if (txt_WTNO_query.Text.Trim() != "")
        { strSample += "and t_M_ReporInfo.ReportName like'%" + txt_WTNO_query.Text + "%'"; }
        //样品来源
        if (txt_source_query.Text.Trim() != "")
        {
            strSample += "and t_M_SampleInfor.SampleSource like'%" + txt_source_query.Text + "%'";
        }
        if(txt_samplequery.Text!="")//按样品编号

            strSample += "and t_M_SampleInfor.SampleID like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text.Trim() != "" && txt_QueryEndTime.Text.Trim() != "")//按采样时间
        {
            DateTime start=DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00");
            DateTime end=DateTime.Parse(txt_QueryEndTime.Text.Trim() + " 23:59:59");
            strDate = " and AccessDate between '"+ start+"' and '"+end+"'";
            //strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        }

        //if(drop_status.SelectedValue!="-1")//按样品状态
        //{
        //    strStatus = " and t_M_ReporInfo.StatusID='" + drop_status.SelectedValue + "' ";
        //}
        if(txt_item.SelectedValue!="-1")// 按项目类型
        {
            strItem = " and t_M_ReporInfo.ItemType='" + txt_item.SelectedValue + "' ";
        }
        int i = 0;
        foreach (ListItem li in cbl_type.Items)
        {
            if (li.Selected)
            {
                if (i != 0)
                    strType += "  or ";
                strType += " t_M_SampleInfor.TypeID='" + li.Value.ToString().Trim() + "' ";
                i++;
            }
        }
        if(strType!="")
        strType = " and (" + strType + ")";
        //if (txt_type.SelectedValue != "-1")//按样品类型
        //{
        //    strType = " and t_M_SampleInfor.TypeID='" + txt_type.SelectedValue + "' ";
        //}
        if (drop_client.SelectedValue != "-1")//按委托单位
        {
            strClient = " and t_M_ReporInfo.ClientID='" + drop_client.SelectedValue + "' ";
        }
       
        //bool flag=false;
        //int i = 0;
      
        ////按选中的分析项目查询
        string strtitle = "";
      
        string cstr = "";
       
            if (cstr == "")
            { 
                cstr = " and 1=1";
            }
           
            cstr = strUrgent + strSample + strDate + strStatus + strItem + strType + strClient + strAanlysisStatus + strtitle;
            string strSql = "SELECT t_M_SampleInfor.id, t_M_ReporInfo.ReportName 报告标识,t_M_ReporInfo.projectname 项目名称,CONVERT(varchar,t_M_SampleInfor.AccessDate,23) AS 送样时间,t_M_SampleInfor.SampleSource 样品来源,t_M_SampleInfor.SampleAddress  样品原标识,t_M_SampleInfor.SampleID AS 样品编号,t_M_SampleInfor.SampleProperty 样品性状,SourceBs 监测项目,t_M_SampleInfor.num 数量, CONVERT(varchar,t_M_SampleInfor.SampleDate,23) AS 采样日期,t_R_UserInfo.Name 收样人" +
       " FROM  t_M_SampleInfor  INNER JOIN" +
            " t_R_UserInfo ON " +
           "  t_R_UserInfo.UserID = t_M_SampleInfor.UserID inner join t_M_ReporInfo on t_M_ReporInfo.id=t_M_SampleInfor.ReportID " +

       " WHERE 1=1 " + cstr+
       " ORDER BY t_M_SampleInfor.AccessDate";
            DataSet ds = new MyDataOp(strSql).CreateDataSet();

            //DataColumn dccc = new DataColumn("分析项目");
            //ds.Tables[0].Columns.Add(dccc);

            DataColumn dcc2 = new DataColumn("监测后样品处理情况");
            ds.Tables[0].Columns.Add(dcc2);
           
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string getitemstr = "select AIName,AICode,MonitorItem,xcflag from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + dr["id"].ToString() + "' and delflag=0";
                DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
                if (dsitem != null && dsitem.Tables.Count > 0)
                {
                    foreach (DataRow drr in dsitem.Tables[0].Rows)
                    {
                        dr["监测项目"] += drr[0].ToString() + ",";
                       
                    }
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
        ds.Dispose();
    }

    //public void ExcelOut(GridView gv)
    //{
    //    if (gv.Rows.Count > 0)
    //    {
    //        grdvw_List.Caption = "<FONT style='WIDTH: 102.16%;font-size:22pt; LINE-HEIGHT: 150%; FONT-FAMILY: SimSun_GB2312; HEIGHT: 35px'><b>样 品 登 记 表</b></font>";

    //        Response.Clear();
    //        Response.ClearContent();
    //        Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".doc");
    //        Response.ContentEncoding = System.Text.Encoding.UTF8;
    //        Response.ContentType = "application/ms-word";
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        gv.RenderControl(htw);
    //        Response.Write("TXHJ / ZJ39" + "\n\r" + sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //        grdvw_List.Caption = "";
    //    }
    //    else
    //    {
    //        Response.Write("没有数据");
    //    }
    //}
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    // base.VerifyRenderingInServerForm(control);
    //}
    protected void btn_print_Click(object sender, EventArgs e)
    {
        OutReport report = new OutReport();


        try
        {
            string DocPath = Server.MapPath("../");
            string TemplateFile = "";
            TemplateFile = DocPath + "Sample\\template\\SampleInOut.doc";
            report.CreateNewDocument(TemplateFile);
            int row =2;
            string[] heads = new string[12];
            int i = 0;
           
            foreach (GridViewRow gvr in grdvw_List.Rows)
            {
                report.AddRow(1,1);
                string[] values = new string[12];
                i = 0;
                int j = 0;
                foreach (TableCell tc in gvr.Cells)
                {
                    if (j > 0)
                    {
                        if (tc.Text != "&nbsp;")
                            values.SetValue(tc.Text, i++);
                        else
                            values.SetValue("", i++);
                    }
                    j++;
                }
               // values.SetValue("", i++);
                report.InsertCell(1, row++, 12, values); //给模板中第一个表格的第二行的5列分别插入数据

            }
            Random rd = new Random();
            int oid = rd.Next(10000);
            //生成的具有模板样式的新文件
            string FileName = DocPath + "Sample\\temp\\" + oid.ToString() + ".doc";
            report.SaveDocument(FileName);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('temp/" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);
 
        }
        catch
        { report.killWinWordProcess(); }
    }
   
    

    
}