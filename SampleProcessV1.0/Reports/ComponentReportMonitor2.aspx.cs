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
using System.IO;
using System.Text;
using WebApp.Components;


public partial class Report_ComponentReportMonitor2 : System.Web.UI.Page
{
    public string strTable = "";
    public string strTableC = "";
    public string strTableP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Title = "监测报告数据组成表";
        if (!IsPostBack)
        {
            #region 初始化页面               
            txt_StartTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-01-01");            
            txt_EndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");          
            Query(0);
            #endregion
        }
    }    
    private void Query(int Export)
    {
        DataTable dtShow = new DataTable();
        DataColumn dc0 = new DataColumn("月份");
        //样品数_样品类型_项目类型
        DataColumn dc1_0 = new DataColumn("0_1_0", typeof(int));//三同时
        DataColumn dc1_1 = new DataColumn("0_8,49_0", typeof(int));//自送样 
        DataColumn dc2_0 = new DataColumn("0_5,79_0", typeof(int));//限期治理
        DataColumn dc12_0 = new DataColumn("0_79_1", typeof(int));//废水自动设备复测（厂次）
        DataColumn dc2_1 = new DataColumn("0_59_0", typeof(int));//涉刑涉法（厂次）
        DataColumn dc3_0 = new DataColumn("0_3_0", typeof(int));//体系认证
        DataColumn dc3_1 = new DataColumn("0_2_0", typeof(int));//环评监测
        DataColumn dc4_0 = new DataColumn("0_78_0", typeof(int));//治水办 
        DataColumn dc4_1 = new DataColumn("0_6_0", typeof(int));//清洁生产
        DataColumn dc5_0 = new DataColumn("0_4_0", typeof(int));//生态系列 
        DataColumn dc5_1 = new DataColumn("0_7_0", typeof(int));//执法检查
        DataColumn dc15_1 = new DataColumn("0_9_0", typeof(int));//其他委托//1,7,49,5,0,3,2,6,4
        DataColumn dc6_0 = new DataColumn("0_46_0", typeof(int));//污水厂监督监测 
        DataColumn dc6_1 = new DataColumn("0_45,74_0", typeof(int));//省控重点源（水：厂次）

        DataColumn dc7_0 = new DataColumn("0_63_0", typeof(int));//省控重点源（气：厂次） 
        DataColumn dc7_1 = new DataColumn("0_70_0", typeof(int));//重点源、烟尘在线比对 （厂次）
        DataColumn dc8_0 = new DataColumn("0_47_0", typeof(int));//TOC在线比对监测厂次（水 
        DataColumn dc8_1 = new DataColumn("0_75_0", typeof(int));//TOC在线验收厂次（水）
        DataColumn dc9_0 = new DataColumn("0_53_1", typeof(int));//减排   （水：厂次） 
        DataColumn dc9_1 = new DataColumn("0_53_2", typeof(int));//减排   （气：厂次）
        DataColumn dc10_0 = new DataColumn("0_55_0", typeof(int));//调查监测（样品数）
        DataColumn dc10_1 = new DataColumn("0_57_0", typeof(int));//污染事故监测（样品数）

        DataColumn dc11_0 = new DataColumn("0_56_0", typeof(int));//信访监测（样品数）
        DataColumn dc11_1 = new DataColumn("0_58_0", typeof(int));//自动站  （样品数）
        
        dtShow.Columns.Add(dc0);
        dtShow.Columns.Add(dc1_0);
        dtShow.Columns.Add(dc1_1);
        dtShow.Columns.Add(dc2_0);
        dtShow.Columns.Add(dc12_0);
        dtShow.Columns.Add(dc2_1);
        dtShow.Columns.Add(dc3_0);
        dtShow.Columns.Add(dc3_1);
        dtShow.Columns.Add(dc4_0);
        dtShow.Columns.Add(dc4_1);
        dtShow.Columns.Add(dc5_0);
        dtShow.Columns.Add(dc5_1);
        dtShow.Columns.Add(dc15_1);
        dtShow.Columns.Add(dc6_0);
        dtShow.Columns.Add(dc6_1);
        dtShow.Columns.Add(dc7_0);
        dtShow.Columns.Add(dc7_1);
        dtShow.Columns.Add(dc8_0);
        dtShow.Columns.Add(dc8_1);
        dtShow.Columns.Add(dc9_0);
        dtShow.Columns.Add(dc9_1);
        dtShow.Columns.Add(dc10_0);

        dtShow.Columns.Add(dc10_1);
        dtShow.Columns.Add(dc11_0);
        dtShow.Columns.Add(dc11_1);
       
       
        DateTime dtStartTime, dtEndTime;
        DateTime dt = Convert.ToDateTime(txt_StartTime.Text);
        DateTime dt2 = Convert.ToDateTime(txt_EndTime.Text);
        dtStartTime = dt;// Convert.ToDateTime(dt.Year + "-" + dt.Month + "-1");
        dtEndTime = dt2;// Convert.ToDateTime(dt2.Year + "-" + dt2.Month + "-1");
        dtEndTime = dtEndTime.AddMonths(1);
        if (dt.Year != dt2.Year)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不能跨年查询！');", true);
            return;
        }
        int subMonth = int.Parse(dt2.Month.ToString()) - int.Parse(dt.Month.ToString()) + 1;
        string strSql_Sample = "select *,month(AccessDate) m   from t_M_ReporInfo r inner join t_M_SampleInfor s on r.id=s.ReportID ";
        strSql_Sample += "where s.AccessDate >= '" + dtStartTime + "' and s.AccessDate < '" + dtEndTime + "' ";
          DataSet dsSample = new MyDataOp(strSql_Sample).CreateDataSet();
          string strSql = "select distinct r.ItemType,s.SampleSource, month(s.AccessDate) m,s.AccessDate,s.TypeID  from t_M_ReporInfo r inner join t_M_SampleInfor s on r.id=s.ReportID";
          strSql += " where s.AccessDate >= '" + dtStartTime + "' and s.AccessDate < '" + dtEndTime + "' ";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        //string strSql1 = "select distinct r.*, month(r.CreateDate) m from t_M_ReporInfo r";
        //strSql1 += " where r.CreateDate >= '" + dtStartTime + "' and r.CreateDate < '" + dtEndTime + "' and r.rwclass=1";
        //DataSet ds1 = new MyDataOp(strSql1).CreateDataSet();
       
      
        for (int mth =0; mth < subMonth; mth++)
        {
            DataRow dradd = dtShow.NewRow();
            dradd[0] = int.Parse(dt.Month.ToString()) + mth;
            int j = 1;
           
            //for (; j < 13; j++)
            //{
            //    string col = dtShow.Columns[j].ColumnName;
            //    string[] list = col.Split('_'); //0：0-int,1-out;1-项目类型；2-样品类型

            //    string constr = "";
            //    if (list[0] == "0")//项目类型
            //    {
            //        constr += " and ItemType in (" + list[1] + ")";
            //    }
            //    else
            //        constr += " and ItemType not in (" + list[1] + ")";
            //      DataRow[] drselnum = ds1.Tables[0].Select(" m='" + (dradd[0].ToString()) + "' "+constr);
            //      dradd[j ] = drselnum.Length;
            //}
            for (j=1; j <=20; j++)
            {
                string col = dtShow.Columns[j].ColumnName;
                string[] list = col.Split('_'); //0：0-int,1-out;1-项目类型；2-样品类型

                string constr = "";
                if (list[0] == "0")//项目类型
                {
                    constr += " and ItemType in (" + list[1] + ")";
                }
                else
                    constr += " and ItemType not in (" + list[1] + ")";
                if (list[2] != "0")
                    constr += " and TypeID  in (" + list[2] + ")";
                DataRow[] drselnum = ds.Tables[0].Select(" m='" + (dradd[0].ToString()) + "' " + constr);
                dradd[j] = drselnum.Length;
            }
            for (j = 21; j <= 24; j++)
            {
                string col = dtShow.Columns[j].ColumnName;
                string[] list = col.Split('_'); //0：0-int,1-out;1-项目类型；2-样品类型

                string constr = "";
                if (list[0] == "0")//项目类型
                {
                    constr += " and ItemType in (" + list[1] + ")";
                }
                else
                    constr += " and ItemType not in (" + list[1] + ")";
                object ypobj = dsSample.Tables[0].Compute("sum(num)", " m='" + (dradd[0].ToString()) + "'"+constr);
               if (ypobj.ToString() != "")
                {
                    dradd[j] = ypobj.ToString();
                   
                }
                else
                    dradd[j] = "0";
                
            }
                dtShow.Rows.Add(dradd);
        }
        DataRow drhj = dtShow.NewRow();
        drhj[0] = "合计";
        for (int p = 1; p < dtShow.Columns.Count; p++)
        {
            object sum = dtShow.Compute("sum([" + dtShow.Columns[p].ColumnName + "])", "");
            drhj[p] = sum.ToString();
        }
        dtShow.Rows.Add(drhj);
        dtShow.AcceptChanges();
            if (dtShow.Rows.Count == 0)
            {
                //没有记录仍保留表头
                dtShow.Rows.Add(dtShow.NewRow());
                grdvw_List.DataSource = dtShow;
                grdvw_List.DataBind();
                int intColumnCount = grdvw_List.Rows[0].Cells.Count;
                grdvw_List.Rows[0].Cells.Clear();
                grdvw_List.Rows[0].Cells.Add(new TableCell());
                grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
            }
            else
            {
                grdvw_List.DataSource = dtShow;
                grdvw_List.DataBind();
            }
        ds.Dispose();
      
        //if (Export == 1)
        //{
        //    //导出报表
        //    try
        //    {
        //        RemoveFiles(Server.MapPath("."));
        //        string strFileName = "监测报告数据组成表_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";
        //        xlSheet.Export(Server.MapPath(".") + "\\" + strFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
        //        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('ShowXls.aspx?file_name=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "');", true);
        //    }
        //    catch
        //    {
        //    }
        //}
    }
    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query(0);
    }
    protected void btn_print_Click(object sender, EventArgs e)
    {
        Query(0);

    }
    

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
 
                //第一行表头   
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                  tcHeader[0].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[0].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[0].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[0].Text = "月分";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[1].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[1].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[1].Text = "三同时验收（厂次）";
                tcHeader.Add(new TableHeaderCell());
                 tcHeader[2].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[2].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[2].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[2].Text = "自送样(自查)厂次";
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[3].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[3].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[3].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[3].Text = "限期治理验收（厂次）";
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[4].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[4].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[4].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[4].Text = "废水自动设备复测（厂次）";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[5].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[5].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[5].Text = "涉刑涉法（厂次）";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[6].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[6].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[6].Text = "体系认证监测(厂次）";
              
                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[7].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[7].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[7].Text = "环评监测(厂次）";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[8].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[8].Text = "治水办(厂次）";
                tcHeader[8].Attributes.Add("bgcolor", "#005EBB");

                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[9].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[9].Attributes.Add("colspan", "0"); //跨Column 
                 tcHeader[9].Text = "清洁生产(厂次）";
                tcHeader[9].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[10].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[10].Text = "生态系列监测(厂次）";
                tcHeader[10].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[11].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[11].Attributes.Add("colspan", "0"); //跨Column 
                 tcHeader[11].Text = "执法检查(厂次）";
                tcHeader[11].Attributes.Add("bgcolor", "#005EBB");
               

                 tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Attributes.Add("bgcolor", "#008A23");
                tcHeader[12].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[12].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[12].Text = "其他(厂次）";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[13].Text = "污水厂监督监测(厂次）";
                tcHeader[13].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[13].Attributes.Add("colspan", "0"); //跨Column
                tcHeader[13].Attributes.Add("bgcolor", "#005EBB");

                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[14].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[14].Attributes.Add("colspan", "0"); //跨Column
                 tcHeader[14].Text = "废水重点源、重金属重点企业(厂次）";
                tcHeader[14].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[15].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[15].Attributes.Add("colspan", "0"); //跨Column
                 tcHeader[15].Text = "废气重点源(厂次）";
                tcHeader[15].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[16].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[16].Attributes.Add("colspan", "0"); //跨Column
                 tcHeader[16].Text = "废气自动设备比对监测(厂次）";
                tcHeader[16].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[17].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[17].Attributes.Add("colspan", "0"); //跨Column
                 tcHeader[17].Text = "废水自动设备比对监测(厂次）";
                tcHeader[17].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());

                tcHeader[18].Text = "废水自动设备验收(厂次）";
                tcHeader[18].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[19].Text = "减排   （水：厂次）";
                tcHeader[19].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[20].Text = "减排   （气：厂次）";
                tcHeader[20].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[21].Text = "调查监测（样品数）";
                tcHeader[21].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[22].Text = "污染事故监测（样品数）";
                tcHeader[22].Attributes.Add("bgcolor", "#005EBB");

               
               

                tcHeader.Add(new TableHeaderCell());
                tcHeader[23].Text = "信访监测（样品数）";
                tcHeader[23].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[24].Text = "自动站  （样品数）</tr>";
                tcHeader[24].Attributes.Add("bgcolor", "#005EBB");
            ////数据行 内容自适应 不换行   
            //case DataControlRowType.DataRow:
            //    TableCellCollection cells1 = e.Row.Cells;
            //    for (int i = 0; i < cells1.Count; i++)
            //    {
            //        cells1[i].Wrap = false; //设置此项切记 不要设置前台GridView宽度  
 
            //    }
            //    e.Row.Cells[1].Visible = false;//绑定数据后，隐藏0列
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            //int id = e.Row.RowIndex + 1;

            //e.Row.Cells[0].Text = id.ToString();

            ////手动添加详细和删除按钮
            //TableCell tabcDetail = new TableCell();
            //tabcDetail.Width = 60;
            //tabcDetail.Style.Add("text-align", "center");
            //ImageButton ibtnDetail = new ImageButton();
            //ibtnDetail.ImageUrl = "~/images/Detail.gif";

            //ibtnDetail.CommandName = "Edit";
            //tabcDetail.Controls.Add(ibtnDetail);
            //e.Row.Cells.Add(tabcDetail);


            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/images/Delete.gif";
            //ibtnDel.ID = "btn_delete";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            ////if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            ////{
            ////    ibtnDel.Enabled = false;
            ////}
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            //{
            //    tabcDel.Controls.Add(ibtnDel);
            //    e.Row.Cells.Add(tabcDel);

            //}
            //else
            //{
            //    ibtnDel.Visible = false;
            //}
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
            //e.Row.Cells[15].Visible = false;

            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
            //e.Row.Cells[18].Visible = false;
        }
    }
    protected void btn_ExportR_Click(object sender, EventArgs e)
    {
        ExcelOut(this.grdvw_List);
    }
    //Query(0);
    #region 导出



    public void ExcelOut(GridView gv)
    {
        if (gv.Rows.Count > 0)
        {
            Response.Clear();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            Response.Write("没有数据");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control);
    }
    #endregion
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
