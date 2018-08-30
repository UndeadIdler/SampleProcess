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


public partial class Report_ZKSample : System.Web.UI.Page
{
    public string strTable = "";
    public string strTableC = "";
    public string strTableP = "";
    private DataSet ds//所选择操作列记录对应的id
    {
        get { return (DataSet)ViewState["ds"]; }
        set { ViewState["ds"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Title = "监测报告数据组成表";
        if (!IsPostBack)
        {
            #region 初始化页面   
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
           
     
            Query(0);
            #endregion
        }
    }    
    private void Query(int Export)
    {
        string constr = "";
        if (txt_Itemquery.Text.Trim() != "")
            constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or t_M_ANItemInf.AICode like '%" + txt_Itemquery.Text.Trim() + "%')";

        if (txt_QueryTime.Text.Trim() != "")
            constr += " and  (year(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        else
            constr += " and  (year(createdate)= '" + DateTime.Now.Year + "' AND month(createdate)= '" + DateTime.Now.Month + "' and day(createdate)= '" + DateTime.Now.Day + "')";


        string strSql_zk = @"select t_zkanalysisinfo.id, t_M_ANItemInf.id itemid,AIName,Name,analysisnum, scenejcnum,case when analysisnum!=0 then  CONVERT(decimal(18, 2), scenejcnum*1.0/analysisnum*1.0*100) else null end,scenehgnum,case when scenejcnum!=0 then CONVERT(decimal(18, 2),scenejcnum*1.0/scenejcnum*1.0*100) else null end, experimentjcnum,case when analysisnum!=0 then CONVERT(decimal(18, 2),experimentjcnum*1.0/analysisnum*1.0*100) else null  end, experimenthgnum,case when experimentjcnum!=0 then CONVERT(decimal(18, 2),experimenthgnum*1.0/experimentjcnum*1.0*100) else null end , jbhsjcnum, case when analysisnum!=0 then CONVERT(decimal(18, 2),jbhsjcnum*1.0/analysisnum*1.0*100) else null end ,jbhshgnum, case when jbhsjcnum!=0 then CONVERT(decimal(18, 2),  jbhshgnum*1.0/jbhsjcnum*1.0*100) else null end , alljcnum,allhgnum, 
                      mmjcnum, mmhgnum, byjcnum, byhgnum, amount
 from t_zkanalysisinfo inner join t_M_ANItemInf on t_M_ANItemInf.id=t_zkanalysisinfo.itemid inner join t_R_UserInfo on t_R_UserInfo.UserID=t_zkanalysisinfo.userid where 1=1" + constr + " order by createdate";

        ds = new MyDataOp(strSql_zk).CreateDataSet();
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


            tcHeader.Clear();
            tcHeader.Add(new TableHeaderCell());
            tcHeader[0].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[0].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[0].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[0].Text = "ID";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[1].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[1].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[1].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[1].Text = "分析项目ID";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[2].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[2].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[2].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[2].Text = "分析项目";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[3].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[3].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[3].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[3].Text = "分析者";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[4].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[4].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[4].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[4].Text = "分析样品数";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[5].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[5].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[5].Attributes.Add("colspan", "4"); //跨Column   
            tcHeader[5].Text = "现场平行样";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[6].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[6].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[6].Attributes.Add("colspan", "4"); //跨Column   
            tcHeader[6].Text = "实验室平行样";

            tcHeader.Add(new TableHeaderCell());
            tcHeader[7].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[7].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[7].Attributes.Add("colspan", "4"); //跨Column   
            tcHeader[7].Text = "加标回收";

            tcHeader.Add(new TableHeaderCell());
            tcHeader[8].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[8].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[8].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[8].Text = "全程序空白";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[9].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[9].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[9].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[9].Text = "密码样";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[10].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[10].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[10].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[10].Text = "标样";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[11].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[11].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[11].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[11].Text = "总检数</tr><tr>";

            int n = 12;
            //第二行表头 
            int p = 0;
            for (int i = 0; i < 6; i++)
            {
                tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i + p].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[n + i + p].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i + p].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i + p].Text = "检查数";
                if (i < 3)
                {
                    p++;
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[n + i + p].Attributes.Add("bgcolor", "#8E8E8E");
                    tcHeader[n + i + p].Attributes.Add("rowspan", "0"); //跨Row   
                    tcHeader[n + i + p].Attributes.Add("colspan", "0"); //跨Column 
                    tcHeader[n + i + p].Text = "检查率%";
                }
                p++;
                tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i + p].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i + p].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i + p].Text = "合格数";
                tcHeader[n + i + p].Attributes.Add("bgcolor", "#005EBB");
                if (i < 3)
                {
                    p++;
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[n + i + p].Attributes.Add("rowspan", "0"); //跨Row   
                    tcHeader[n + i + p].Attributes.Add("colspan", "0"); //跨Column 
                    tcHeader[n + i + p].Text = "合格率%";
                    tcHeader[n + i + p].Attributes.Add("bgcolor", "#005EBB");
                }
                if (n + i + p == 17)
                    tcHeader[n + i + p].Text = "合格数</tr><tr>";

            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {



        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[12].Visible = false;
            // e.Row.Cells[18].Visible = false;
            //for (int i = 8; i <= e.Row.Cells.Count-1; i++)
            //{
            //    e.Row.Cells[i].Visible = false;
            //}

            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
            //e.Row.Cells[20].Visible = false;
            //e.Row.Cells[21].Visible = false;
            //e.Row.Cells[18].Visible = false;
            //for (int i = 0; i < cbl_choose.Items.Count; i++)
            //{
            //    if (!cbl_choose.Items[i].Selected)
            //    {
            //        try
            //        {
            //            int p = int.Parse(cbl_choose.Items[i].Value);
            //            e.Row.Cells[p].Visible = false;
            //        }
            //        catch
            //        { }
            //    }
            //}
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
