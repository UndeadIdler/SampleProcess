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


public partial class Report_MonitorEf : System.Web.UI.Page
{
    public string strTable = "";
    public string strTableC = "";
    public string strTableP = "";
    private DataTable fxman//所选择操作列记录对应的id
    {
        get { return (DataTable)ViewState["fxman"]; }
        set { ViewState["fxman"] = value; }
    }

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
            for(int i=0;i<cbl_sampleType.Items.Count;i++)
            {
                cbl_sampleType.Items[i].Selected = true;
            }
            DAl.User.Users userobj = new DAl.User.Users();
            fxman = userobj.QueryUsersDT("", "48, 49, 50, 51, 52, 58");
            cbl_man.DataSource = fxman;
            cbl_man.DataTextField = "Name";
            cbl_man.DataValueField = "UserID";
            cbl_man.DataBind();
            //ListItem li = new ListItem("请选择", "0");
            //drop_man.Items.Add(li);
            //drop_man.SelectedIndex = drop_man.Items.Count - 1;
            Query(0);
            #endregion
        }
    }    
    private void Query(int Export)
    {
        //DataTable dtShow = new DataTable();

      
        string constr = "";
        string constr2 = "";
        for (int i = 0; i < cbl_sampleType.Items.Count; i++)
        {
            if (cbl_sampleType.Items[i].Selected)
            {
                constr += cbl_sampleType.Items[i].Value+",";
                constr2 = constr;
            }
        }
       
        if (constr != "")
        {
            constr = " and TypeID  in (" + constr.Substring(0, constr.Length - 1) + ")";
            constr2 = " and  ClassID in (" + constr2.Substring(0, constr2.Length - 1) + ")";
        }
        else
        { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择样品类型！');", true);
            return;
        }
        string sqlname = "";
        string sqlconstr = "";
        string sqlcroupstr = "";
        //按人员统计
        for (int i = 0; i < cbl_man.Items.Count; i++)
        {
            if (cbl_man.Items[i].Selected)
            {
                sqlconstr += "'"+cbl_man.Items[i].Value + "',";
            }
        }
        if (sqlconstr != "")
        {
            sqlconstr = " and fxuser  in (" + sqlconstr.Substring(0, sqlconstr.Length - 1) + ") and fxuser is not null";
            sqlname = ",fxuser as 分析人";
            sqlcroupstr = "fxuser";
            
        }
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选分析人员！');", true);
        //    return;
        //}
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
       // string strSql_Sample = @" select AIName 监测项, MonitorItem, count(MonitorItem) 监测项数,case when effectivedw=0 then (case when effectivetime!=0 then cast(effectivetime as real) else 0 end)*60  when effectivedw=1 then (case when effectivetime!=0 then cast(effectivetime as real) else 0 end) else 0 end  单位耗时, case when effectivedw=0 then (case when effectivetime!=0 then cast(effectivetime as real) else 0 end)*60  when effectivedw=1 then (case when effectivetime!=0 then cast(effectivetime as real) else 0 end) else 0 end*COUNT(*) 耗时  from (select MonitorItem  from t_M_ReporInfo r inner join t_M_SampleInfor s on r.id=s.ReportID inner join t_MonitorItemDetail p on p.SampleID=s.SampleID  where s.AccessDate >= '" + dtStartTime + "' and s.AccessDate < '" + dtEndTime + "'" + constr + ") a inner join t_M_AnalysisItemEx  b on b.AIID=a.MonitorItem  group by a.MonitorItem,b.AIName,b.effectivetime,effectivedw";
        string strSql_Sample = @" select s.SampleID, MonitorItem,method " + sqlname + " from t_M_ReporInfo r inner join t_M_SampleInfor s on r.id=s.ReportID inner join t_MonitorItemDetail p on p.SampleID=s.id   where s.AccessDate >= '" + dtStartTime + "' and s.AccessDate < '" + dtEndTime + "'" + constr + sqlconstr + "   and flag=3 and delflag=0 ";
        string item = @"select distinct AIName 监测项, AIID,ifmethod, case when a.effectivedw=0 then (case when cast(a.effectivetime as numeric(18, 2))!=0 then cast(a.effectivetime as numeric(18, 2)) else 0 end)*60  when a.effectivedw=1 then (case when cast(a.effectivetime as numeric(18, 2))!=0 then cast(a.effectivetime as numeric(18, 2)) else 0 end) else 0 end  单位耗时1,case when b.effectivedw=0 then (case when cast(b.effectivetime as numeric(18, 2))!=0 then cast(b.effectivetime as numeric(18, 2)) else 0 end)*60  when b.effectivedw=1 then (case when cast(b.effectivetime as numeric(18, 2))!=0 then cast(a.effectivetime as numeric(18, 2)) else 0 end) else 0 end  单位耗时2,b.method " + sqlname + "  from  t_M_SampleInfor s  inner join  t_MonitorItemDetail p on s.id=p.SampleID inner join t_M_AnalysisItemEx a on p.MonitorItem=a.AIID and s.TypeID=a.ClassID left join t_M_AItemEffectiveTime b on b.AID=a.AIID and SampleType=ClassID where a.effectivetime is not null " + constr2 + sqlconstr;
       
        //string item = "select distinct AIName 监测项, AIID,ifmethod, case when a.effectivedw=0 then (case when a.effectivetime!=0 then cast(a.effectivetime as real) else 0 end)*60  when a.effectivedw=1 then (case when a.effectivetime!=0 then cast(a.effectivetime as real) else 0 end) else 0 end  单位耗时1,case when b.effectivedw=0 then (case when b.effectivetime!=0 then cast(b.effectivetime as real) else 0 end)*60  when b.effectivedw=1 then (case when b.effectivetime!=0 then cast(b.effectivetime as real) else 0 end) else 0 end  单位耗时2,b.method  from t_M_AnalysisItemEx a left join t_M_AItemEffectiveTime b on b.AID=a.AIID and SampleType=ClassID where a.effectivetime is not null " + constr2;
        DataSet dtShow = new MyDataOp(item).CreateDataSet();
        DataSet dsSample = new MyDataOp(strSql_Sample).CreateDataSet();
       
        DataColumn dc2 = new DataColumn("数据量", typeof(int));
        DataColumn dc1 = new DataColumn("耗时");
        DataColumn dc3 = new DataColumn("备注");
        dtShow.Tables[0].Columns.Add(dc2);
        dtShow.Tables[0].Columns.Add(dc1);
        dtShow.Tables[0].Columns.Add(dc3);
        string upstr = "";
       
        foreach (DataRow dr in dtShow.Tables[0].Rows)
        {
            if (sqlconstr != "")
            {
                upstr = " and 分析人='" + dr["分析人"].ToString() + "'";
                DataRow[] druser = fxman.Select("UserID='" + dr["分析人"].ToString() + "'");
                if (druser.Length > 0)
                {
                    dr["分析人"] = druser[0]["Name"].ToString();
                }

            }
            if (dr["ifmethod"].ToString().Trim() == "0")
            {
                object num = dsSample.Tables[0].Compute("count(SampleID)", "MonitorItem='" + dr["AIID"].ToString().Trim() + "'" + upstr);
                dr["数据量"] = num.ToString();
                double mimutes =double.Parse(num.ToString()) * double.Parse(dr["单位耗时1"].ToString());
               
                dr["耗时"] = Math.Round(double.Parse((mimutes/60).ToString()), 2) + "小时";//小时
               
            }
            else
            {

                object num = dsSample.Tables[0].Compute("count(SampleID)", "MonitorItem='" + dr["AIID"].ToString().Trim() + "' and method='" + dr["method"].ToString() + "'" + upstr);
                dr["数据量"] = num.ToString();
                double mimutes =double.Parse(num.ToString()) * double.Parse(dr["单位耗时2"].ToString());
                TimeSpan t = TimeSpan.Parse(mimutes.ToString());
               
                dr["耗时"] = Math.Round(double.Parse((mimutes / 60).ToString()), 2) + "小时";//小时
                string str = "select Standard from t_M_AnStandard where id='" + dr["method"].ToString() + "'";
                DataSet dsmethod = new MyDataOp(str).CreateDataSet();
                if (dsmethod != null)
                {
                    if (dsmethod.Tables.Count > 0)
                    {
                        dr["备注"] = dsmethod.Tables[0].Rows[0][0].ToString();
                    }
                }
               

            }
        }
        if (dtShow.Tables[0].Rows.Count == 0)
            {
                //没有记录仍保留表头
                dtShow.Tables[0].Rows.Add(dtShow.Tables[0].NewRow());
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
          dsSample.Dispose();
      
       
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
 
              
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
           
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
    protected void cbl_all_CheckedChanged(object sender, EventArgs e)
    {
        if (cbl_all.Checked)
        {
            for (int i = 0; i < cbl_man.Items.Count; i++)
            {
                cbl_man.Items[i].Selected = true;
            }
        }
        else
            for (int i = 0; i < cbl_man.Items.Count; i++)
            {
                cbl_man.Items[i].Selected = false;
            }

    }
    protected void cbl_man_SelectedIndexChanged(object sender, EventArgs e)
    {
        int p=0;
        for (int i = 0; i < cbl_man.Items.Count; i++)
        {
            if (!cbl_man.Items[i].Selected)
                cbl_all.Checked = false;
            else
                p++;

        }
        if (p == cbl_man.Items.Count)
            cbl_all.Checked = true;
    }
}
