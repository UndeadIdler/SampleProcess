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


public partial class Report_ComponentReportMonitor : System.Web.UI.Page
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

           // txt_StartTime.Attributes.Add("OnFocus", "javascript:calendar()");
           // txt_EndTime.Attributes.Add("OnFocus", "javascript:calendar()");
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
        DataColumn dc1_0 = new DataColumn("1_10_48,77_0", typeof(int));//地表水样品数 
        DataColumn dc1_1 = new DataColumn("2_10_48,77_0", typeof(int));//地表水数据量
        DataColumn dc2_0 = new DataColumn("1_1_45,46,47,79,74_0", typeof(int));//重点源废水样品数
        DataColumn dc2_1 = new DataColumn("2_1_45,46,47,79,74_0", typeof(int));//重点源废水数据量
        DataColumn dc3_0 = new DataColumn("1_1_7,56,59_1", typeof(int));//大队委托水样品数
        DataColumn dc3_1 = new DataColumn("2_1_7,56,59_1", typeof(int));//大队委托水数据量
        DataColumn dc4_0 = new DataColumn("1_1,10,11_8,49_1", typeof(int));//自送样水样品数 
        DataColumn dc4_1 = new DataColumn("2_1,10,11_8,49_1", typeof(int));//自送样水数据量
        DataColumn dc5_0 = new DataColumn("1_1_1,5,75_1", typeof(int));//三同时水样品数 
        DataColumn dc5_1 = new DataColumn("2_1_1,5,75_1", typeof(int));//三同时水数据量
        DataColumn dc6_0 = new DataColumn("1_1,10,11_2,3,4,6,9,53,55,57,58,60,61,62,68,69,72,78,71_1", typeof(int));//其他委托水样品数 
        DataColumn dc6_1 = new DataColumn("2_1,10,11_2,3,4,6,9,53,55,57,58,60,61,62,68,69,72,78,71_1", typeof(int));//其他委托水数据量

        DataColumn dc7_0 = new DataColumn("1_2,12_52,54_0", typeof(int));//气常规样品数 
        DataColumn dc7_1 = new DataColumn("2_2,12_52,54_0", typeof(int));//气常规气数据量
        DataColumn dc8_0 = new DataColumn("1_2_63,70_0", typeof(int));//气重点源样品数 
        DataColumn dc8_1 = new DataColumn("2_2_63,70_0", typeof(int));//气重点源气数据量
        DataColumn dc9_0 = new DataColumn("1_2_1,5,76_1", typeof(int));//气三同时样品数 
        DataColumn dc9_1 = new DataColumn("2_2_1,5,76_1", typeof(int));//气三同时数据量
        DataColumn dc10_0 = new DataColumn("1_2,12_2,3,4,6,9,53,55,57,58,60,61,62,68,69,72,78,56,8,59,71_1", typeof(int));//其他委托气样品数 
        DataColumn dc10_1 = new DataColumn("2_2,12_2,3,4,6,9,53,55,57,58,60,61,62,68,69,72,78,56,8,59,71_1", typeof(int));//其他委托气数据量

        DataColumn dc11_0 = new DataColumn("1_4,6,15,16_66,65,73,68,69_0", typeof(int));//土壤、底质、煤样样品数 
        DataColumn dc11_1 = new DataColumn("2_4,6,15,16_66,65,73,68,69_0", typeof(int));//土壤、底质、煤样数据量
        DataColumn dc14_0 = new DataColumn("1_7_0_0", typeof(int));//噪声样品量
        DataColumn dc14_1 = new DataColumn("2_7_0_0", typeof(int));//噪声数据量
        DataColumn dc12_0 = new DataColumn("1_5_49,9_0", typeof(int));//桑叶氟化物样品数 
        DataColumn dc12_1 = new DataColumn("2_5_49,9_0", typeof(int));//桑叶氟化物数据量
        DataColumn dc13_0 = new DataColumn("Samplesum", typeof(int));//样品数合计
        DataColumn dc13_1 = new DataColumn("sum", typeof(int));//数据量合计
        dtShow.Columns.Add(dc0);
        dtShow.Columns.Add(dc1_0);
        dtShow.Columns.Add(dc1_1);
        dtShow.Columns.Add(dc2_0);
        dtShow.Columns.Add(dc2_1);
        dtShow.Columns.Add(dc3_0);
        dtShow.Columns.Add(dc3_1);
        dtShow.Columns.Add(dc4_0);
        dtShow.Columns.Add(dc4_1);
        dtShow.Columns.Add(dc5_0);
        dtShow.Columns.Add(dc5_1);
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
        dtShow.Columns.Add(dc14_0);
        dtShow.Columns.Add(dc14_1);
        dtShow.Columns.Add(dc12_0);
        dtShow.Columns.Add(dc12_1);
        dtShow.Columns.Add(dc13_0);
        dtShow.Columns.Add(dc13_1);
       
        DateTime dtStartTime, dtEndTime;
        DateTime dt = Convert.ToDateTime(txt_StartTime.Text);
        DateTime dt2 = Convert.ToDateTime(txt_EndTime.Text);
        dtStartTime = dt; //Convert.ToDateTime(dt.Year + "-" + dt.Month + "-1");
        dtEndTime = dt2;// Convert.ToDateTime(dt2.Year + "-" + dt2.Month + "-1");
        //dtEndTime = dtEndTime.AddMonths(1);
        if (dt.Year != dt2.Year)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不能跨年查询！');", true);
            return;
        }
        int subMonth = int.Parse(dt2.Month.ToString()) - int.Parse(dt.Month.ToString()) + 1;
        string strSql_Sample = "select *,month(AccessDate) m   from t_M_ReporInfo r inner join t_M_SampleInfor s on r.id=s.ReportID ";
        strSql_Sample += "where s.AccessDate >= '" + dtStartTime + "' and s.AccessDate < '" + dtEndTime + "'";
          DataSet dsSample = new MyDataOp(strSql_Sample).CreateDataSet();
          string strSql = "select *, month(AccessDate) m  from t_M_ReporInfo r inner join t_M_SampleInfor s on r.id=s.ReportID inner join t_MonitorItemDetail n on n.SampleID=s.id ";
        strSql += "where s.AccessDate >= '" + dtStartTime + "' and s.AccessDate < '" + dtEndTime + "' and n.delflag=0 ";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
       
      
        for (int mth =0; mth < subMonth; mth++)
        {
            DataRow dradd = dtShow.NewRow();
            dradd[0] = int.Parse(dt.Month.ToString()) + mth;
           
            int j = 1;
            int yps = 0;
            int sjl = 0;
            for (; j < dtShow.Columns.Count-2; j=j+2)
            {
               
                string col = dtShow.Columns[j].ColumnName;
                switch (col)
                {
                    case "sum":
                    case "Samplesum":
                        break;
                    default: string[] list = col.Split('_'); //0：1-样品数，2-数据量；1-样品类型；2-项目类型；3-任务类型：0-例行监测，1-委托监测

                        string constr = "";
                        if (list[1] != "0")//样品类型
                        { constr += " and TypeID in(" + list[1] + ")"; }

                        if (list[2] != "0")//项目类型
                        {
                            constr += " and ItemType in (" + list[2] + ")";
                        }
                        
                        // if (list[1] == "5")
                        //{
                        //    constr += " and ItemType ='5'";// and SampleID in ( select SampleID  from t_MonitorItemDetail  where MonitorItem='25' )";
                        //}
                        if(list[3]!="0")//任务类型
                        {
                            constr+=" and rwclass='" + list[3] + "'";
                        }
                        object ypobj = dsSample.Tables[0].Compute("sum(num)", " m='" + (dradd[0].ToString()) + "'" + constr);
                        //DataRow[] drsel = dsSample.Tables[0].Select(" m='" + (dradd[0].ToString()) + "'" + constr);
                        if (ypobj.ToString() != "")
                        {
                            dradd[j] = ypobj.ToString();
                            yps += int.Parse(ypobj.ToString());
                        }
                        else
                             dradd[j]="0";
                           
                            DataRow[] drselnum = ds.Tables[0].Select(" m='" + (dradd[0].ToString()) + "' "+constr);
                                dradd[j + 1] = drselnum.Length;
                                sjl += drselnum.Length;
                        break;
                }
            }
            dradd[j] = yps;
            dradd[j + 1] = sjl;
            dtShow.Rows.Add(dradd);
           // strSql += " union all select " + (int.Parse(dt.Month.ToString()) + mth).ToString();
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
    }
    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query(0);
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

     private void NAR(object o)
     {
         try
         {
             System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
         }
         catch
         { }
         finally
         {
             o = null;
         }
     }
     #endregion
   
    

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
 
                //第一行表头   
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                  tcHeader[0].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[0].Attributes.Add("rowspan", "3"); //跨Row   
                tcHeader[0].Attributes.Add("colspan", "0"); //跨Column   
                tcHeader[0].Text = "月分";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[1].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[1].Attributes.Add("colspan", "12"); //跨Column   
                tcHeader[1].Text = "水质监测数据";
                tcHeader.Add(new TableHeaderCell());
                 tcHeader[2].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[2].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[2].Attributes.Add("colspan", "8"); //跨Column   
                tcHeader[2].Text = "大气监测数据";
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[3].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[3].Attributes.Add("rowspan", "2"); //跨Row   
                tcHeader[3].Attributes.Add("colspan", "2"); //跨Column   
                tcHeader[3].Text = "土壤、底质、煤样";
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[4].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[4].Attributes.Add("rowspan", "2"); //跨Row   
                tcHeader[4].Attributes.Add("colspan", "2"); //跨Column   
                tcHeader[4].Text = "噪声监测";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[5].Attributes.Add("rowspan", "2"); //跨Row   
                tcHeader[5].Attributes.Add("colspan", "2"); //跨Column   
                tcHeader[5].Text = "桑叶氟化物";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[6].Attributes.Add("rowspan", "2"); //跨Row   
                tcHeader[6].Attributes.Add("colspan", "2"); //跨Column   
                tcHeader[6].Text = "合计</tr><tr>";
                //第二行表头   
                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[7].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[7].Attributes.Add("colspan", "2"); //跨Column 
                tcHeader[7].Text = "地表水";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[8].Attributes.Add("colspan", "2"); //跨Column 
                tcHeader[8].Text = "污染源";
                tcHeader[8].Attributes.Add("bgcolor", "#005EBB");

                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[9].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[9].Attributes.Add("colspan", "2"); //跨Column 
                 tcHeader[9].Text = "大队";
                tcHeader[9].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[10].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[10].Attributes.Add("colspan", "2"); //跨Column 
                 tcHeader[10].Text = "自送样";
                tcHeader[10].Attributes.Add("bgcolor", "#005EBB");
               

                 tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Attributes.Add("bgcolor", "#008A23");
                tcHeader[11].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[11].Attributes.Add("colspan", "2"); //跨Column 
                tcHeader[11].Text = "三同时";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Text = "其他委托";
                tcHeader[12].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[12].Attributes.Add("colspan", "2"); //跨Column
                tcHeader[12].Attributes.Add("bgcolor", "#005EBB");

                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[13].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[13].Attributes.Add("colspan", "2"); //跨Column
                 tcHeader[13].Text = "气常规";
                tcHeader[13].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[14].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[14].Attributes.Add("colspan", "2"); //跨Column
                 tcHeader[14].Text = "污染源";
                tcHeader[14].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[15].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[15].Attributes.Add("colspan", "2"); //跨Column
                 tcHeader[15].Text = "三同时";
                tcHeader[15].Attributes.Add("bgcolor", "#005EBB");
                 tcHeader.Add(new TableHeaderCell());
                 tcHeader[16].Attributes.Add("rowspan", "0"); //跨Row   
                 tcHeader[16].Attributes.Add("colspan", "2"); //跨Column
                 tcHeader[16].Text = "其他委托</tr><tr>";
                tcHeader[16].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());

                tcHeader[17].Text = "样品数";
                tcHeader[17].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[18].Text = "数据量";
                tcHeader[18].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[19].Text = "样品数";
                tcHeader[19].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[20].Text = "数据量";
                tcHeader[20].Attributes.Add("bgcolor", "#005EBB");

               
               

                tcHeader.Add(new TableHeaderCell());
                tcHeader[21].Text = "样品数";
                tcHeader[21].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[22].Text = "数据量";
                tcHeader[22].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[23].Text = "样品数";
                tcHeader[23].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[24].Text = "数据量";
                tcHeader[24].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[25].Text = "样品数";
                tcHeader[25].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[26].Text = "数据量";
                tcHeader[26].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[27].Text = "样品数";
                tcHeader[27].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[28].Text = "数据量";
                tcHeader[28].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[29].Text = "样品数";
                tcHeader[29].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[30].Text = "数据量";
                tcHeader[30].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[31].Text = "样品数";
                tcHeader[31].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[32].Text = "数据量";
                tcHeader[32].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[33].Text = "样品数";
                tcHeader[33].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[34].Text = "数据量";
                tcHeader[34].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[35].Text = "样品数";
                tcHeader[35].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[36].Text = "数据量";
                tcHeader[36].Attributes.Add("bgcolor", "#005EBB");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[37].Text = "样品数";
                tcHeader[37].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[38].Text = "数据量";
                tcHeader[38].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[39].Text = "样品数";
                tcHeader[39].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[40].Text = "数据量";
                tcHeader[40].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[41].Text = "样品数";
                tcHeader[41].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[42].Text = "数据量";
                tcHeader[42].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[43].Text = "样品数";
                tcHeader[43].Attributes.Add("bgcolor", "#005EBB");
                tcHeader.Add(new TableHeaderCell());
                tcHeader[44].Text = "数据量</tr>";
                tcHeader[44].Attributes.Add("bgcolor", "#005EBB");
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
           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            
        }
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
