using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.SqlClient;
 using DataAccess;
using WebApp.Components;
using DAL;

public partial class drawSample : System.Web.UI.Page
{
    private string strSelectedId//样品单号
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }

    private string strDrawId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strDrawId"]; }
        set { ViewState["strDrawId"] = value; }
    }
    private string strReportName//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportName"]; }
        set { ViewState["strReportName"] = value; }
    }
    private string strAnalysisId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strAnalysisId"]; }
        set { ViewState["strAnalysisId"] = value; }
    }
    private string AnalysisIdList//所选择操作列记录对应的id
    {
        get { return (string)ViewState["AnalysisIdList"]; }
        set { ViewState["AnalysisIdList"] = value; }
    }
    private string AnalysisNameList//所选择操作列记录对应的id
    {
        get { return (string)ViewState["AnalysisNameList"]; }
        set { ViewState["AnalysisNameList"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                       txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
             Query();
           // SetButton();
          

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>分析领样</b></font>";
        }
    }
    private void Query()
    {
        string constr = "";
        if (txt_QueryTime.Text.Trim() != "")
            constr = " and  (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        if (txt_Itemquery.Text.Trim() != "")
            constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or LOWER(t_M_ANItemInf.AICode) like '%" + txt_Itemquery.Text.Trim().ToLower() + "%')";
        string strSql = "select t_MonitorItemDetail.id, t_MonitorItemDetail.MonitorItem,t_M_ANItemInf.AIName 监测项,t_M_SampleInfor.SampleID AS 样品编号,t_M_ReporInfo.Ulevel ,t_M_SampleInfor.SampleAddress 采样点 ,t_M_SampleInfor.SampleDate AS 采样时间," +
      "t_M_SampleInfor.AccessDate AS 接样时间, " +
      " t_M_SampleInfor.TypeID, t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, ProjectName 项目名称," +
     " t_M_ReporInfo.chargeman 项目负责人,case when t_M_AnalysisItemEx.num!=0 then t_M_AnalysisItemEx.num else 1000 end as num,t_M_SampleInfor.CreateDate,case when t_M_AnalysisItemEx.num!=0 then datediff(hour, t_M_SampleInfor.CreateDate,getdate()) else 0 end as sxx," +
"t_M_ReporInfo.Green,t_MonitorItemDetail.Remark 备注,t_MonitorItemDetail.SampleID sID FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +

    " t_M_AnalysisMainClassEx ON t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id and t_MonitorItemDetail.delflag=0  inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.ClassID=t_M_SampleInfor.TypeID and t_M_AnalysisItemEx.AIID=t_MonitorItemDetail.MonitorItem where t_MonitorItemDetail.flag=1 and t_M_SampleInfor.StatusID=1 and t_MonitorItemDetail.zpto='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' " + constr + " ORDER BY t_M_ANItemInf.orderid,t_M_ANItemInf.AIName,t_M_SampleInfor.AccessDate,t_M_SampleInfor.SampleID,sxx desc";//t_M_AnalysisItemEx.num asc,sxx desc";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dcc1 = new DataColumn("分析指标");
        ds.Tables[0].Columns.Add(dcc1);
        DataColumn dcc2 = new DataColumn("是否走绿色通道");
        ds.Tables[0].Columns.Add(dcc2);
        string str = "select * from t_R_UserInfo";
        DataSet dsuser = new MyDataOp(str).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            DataRow[] druser = dsuser.Tables[0].Select("userid='" + dr["项目负责人"] + "'");
            if (druser.Length > 0)
            { dr["项目负责人"] = druser[0]["Name"].ToString(); }
            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急"; 
            else
                dr["紧急程度"] = "一般";
            if (dr["Green"].ToString() == "1")
                dr["是否走绿色通道"] = "是";
            else
                dr["是否走绿色通道"] = "否";
            string getitemstr = "select AIName,MonitorItem from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + dr["sID"].ToString() + "' and delflag=0";
            DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
            if (dsitem != null && dsitem.Tables.Count > 0)
            {
                foreach (DataRow drr in dsitem.Tables[0].Rows)
                {
                    dr["分析指标"] += drr[0].ToString() + ",";
                   
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

    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox cbl = new CheckBox();
            cbl.ID = "cbl_All";
            cbl.Text = "全选";
            cbl.CheckedChanged+=cbl_CheckedChanged;
            cbl.AutoPostBack = true;
            e.Row.Cells[0].Controls.Add(cbl);
            e.Row.Cells[0].Width = 60;
            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "领样/修改";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);
            //TableCell headerDetail1 = new TableCell();
            //headerDetail1.Text = "还样";
            //headerDetail1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail1.Width = 60;
            //e.Row.Cells.Add(headerDetail1);
         
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = e.Row.RowIndex + 1;
            CheckBox cbl=new CheckBox();
            cbl.ID="cbl";
            cbl.Text = "领样";
            e.Row.Cells[0].Controls.Add(cbl);
            ////手动添加详细和删除按钮
            //TableCell tabcDetail = new TableCell();
            //tabcDetail.Width = 60;
            //tabcDetail.Style.Add("text-align", "center");
            //ImageButton ibtnDetail = new ImageButton();
            //ibtnDetail.ID = "Draw";
            //ibtnDetail.ImageUrl = "~/images/Detail.gif";
           
            //ibtnDetail.CommandName = "Edit";
            //tabcDetail.Controls.Add(ibtnDetail);
            //e.Row.Cells.Add(tabcDetail);

            ////手动添加详细和删除按钮
            //TableCell tabcDetail1 = new TableCell();
            //tabcDetail1.Width = 60;
            //tabcDetail1.Style.Add("text-align", "center");
            //ImageButton ibtnDetail1 = new ImageButton();
            //ibtnDetail1.ImageUrl = "~/images/Detail.gif";

            //ibtnDetail1.CommandName = "Select";
            //tabcDetail1.Controls.Add(ibtnDetail1);
            //e.Row.Cells.Add(tabcDetail1);

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[19].Visible = false;

        }
    }
    protected void cbl_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbl_all=sender as CheckBox;
            foreach (GridViewRow gvr in grdvw_List.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbl = gvr.Cells[0].FindControl("cbl") as CheckBox;
                    if (cbl_all.Checked)
                    {
                        cbl.Checked = true;
                    }
                    else
                    {
                        cbl.Checked = false;
                    }
                }

            }
        
    }
   
    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_Draw_Click(object sender, EventArgs e)
    {
        List<Entity.SampleItem> DrawList = new List<Entity.SampleItem>();
       
            foreach (GridViewRow gvr in grdvw_List.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbl = gvr.Cells[0].FindControl("cbl") as CheckBox;
                    if (cbl.Checked)
                    {
                        Entity.SampleItem temp = new Entity.SampleItem();
                        temp.Lydate = DateTime.Now;
                        temp.LyUser = Request.Cookies["Cookies"].Values["u_id"].ToString();
                        temp.SampleID = gvr.Cells[19].Text.Trim();
                        temp.ID = int.Parse(gvr.Cells[1].Text.Trim());
                        temp.MonitorID = int.Parse(gvr.Cells[2].Text.Trim());
                        DrawList.Add(temp);
                    }
                    
                }

        }
            if (DrawList.Count > 0)
            {
                DAl.DrawSample drawobj = new DAl.DrawSample();
                if (drawobj.AddDraw(DrawList) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('领样成功！')", true);
                    Query();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('领样失败！')", true);
                }
               

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要领取的监测项！')", true);
            }
       
    }
   
    #endregion
    protected void grdvw_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        bool flag = true;
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //初始化checkboxlist


           
                // draw.Enabled = flag1 || flag2;
            if (e.Row.Cells[14].Text.Trim() != "1000" && e.Row.Cells[14].Text.Trim() != "&nbsp;")
            {
                int sxx = int.Parse(e.Row.Cells[14].Text.Trim());
                DateTime jytime = DateTime.Parse(e.Row.Cells[15].Text.Trim());
                TimeSpan ts = DateTime.Now - jytime;
                if (ts.TotalHours > sxx)
                    e.Row.BackColor = System.Drawing.Color.Red;
                if (ts.TotalHours <= sxx && ts.TotalHours > sxx / 2)
                    e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }

    }     
}
