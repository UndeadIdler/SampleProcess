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

public partial class drawSample1 : System.Web.UI.Page
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

        string strSql = "select t_M_SampleInfor.id, t_M_SampleInfor.SampleID AS 样品编号,t_M_ReporInfo.Ulevel ,t_M_SampleInfor.SampleDate AS 采样时间,t_M_SampleInfor.SampleAddress 采样点 ," +
      "t_M_SampleInfor.AccessDate AS 接样时间, " +
      " t_M_SampleInfor.TypeID, t_M_SampleType.SampleType AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, " +
     " t_M_SampleInfor.ypstatus, t_M_SampleInfor.ReportID,t_M_ReporInfo.chargeman 项目负责人" +
" FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
     " t_M_SampleType ON " +
    "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID   where ( t_M_SampleInfor.SampleID in(select SampleID from t_MonitorItemDetail where flag <1) or t_M_SampleInfor.SampleID in(select SampleID from t_DrawSample  where  returndate is null and type=0 ) ) " + constr + " ORDER BY t_M_SampleInfor.AccessDate";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
          DataColumn dcs = new DataColumn("样品状态");
        ds.Tables[0].Columns.Add(dcs);
        DataColumn dcitem = new DataColumn("分析项目");
        ds.Tables[0].Columns.Add(dcitem);
        DataColumn dcitemvalue = new DataColumn("分析项目编码");
        ds.Tables[0].Columns.Add(dcitemvalue);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急"; 
            else
                dr["紧急程度"] = "一般";
            if (dr["ypstatus"].ToString() == "1")
                dr["样品状态"] = "领用中";
            else if (dr["ypstatus"].ToString() == "0")

                dr["样品状态"] = "可领用";
            else
                dr["样品状态"] = "领用中";
           
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
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysis();", true);
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        DrawReList();
        Query();
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        DrawList();
       
        Query();
    }

    protected void DrawReList()
    {
        DataSet ds = new MyDataOp("select t_DrawSample.id, t_DrawSample.SampleID 样品编号,CreateDate 领样时间,Name 领用人,Itemlist 分析项目,t_DrawSample.flag,returndate 还样时间  from t_DrawSample inner join t_R_UserInfo on LyUserID=UserID   where t_DrawSample.type=0 and   t_DrawSample.SampleID='" + strSelectedId + "' and yxflag=0 order by t_DrawSample.CreateDate desc").CreateDataSet();
        DataColumn dc = new DataColumn("状态");
        ds.Tables[0].Columns.Add(dc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["flag"].ToString() == "0")
            {
                dr["状态"] = "未还";
            }
            else
            {
                dr["状态"] = "已还";
            }

        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_ReportDetail.DataSource = ds;
            grdvw_ReportDetail.DataBind();
            int intColumnCount = grdvw_ReportDetail.Rows[0].Cells.Count;
            grdvw_ReportDetail.Rows[0].Cells.Clear();
            grdvw_ReportDetail.Rows[0].Cells.Add(new TableCell());
            grdvw_ReportDetail.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_ReportDetail.DataSource = ds;
            grdvw_ReportDetail.DataBind();

        }
    }

    protected void DrawList()
    {
        DataSet ds = new MyDataOp("select t_DrawSample.id, t_DrawSample.SampleID 样品编号,CreateDate 领样时间,Name 领用人,Itemlist 分析项目,t_DrawSample.flag,returndate 还样时间  from t_DrawSample inner join t_R_UserInfo on LyUserID=UserID   where t_DrawSample.type=0 and  t_DrawSample.SampleID='" + strSelectedId + "' and yxflag=0 order by t_DrawSample.CreateDate desc").CreateDataSet();
        DataColumn dc = new DataColumn("状态");
        ds.Tables[0].Columns.Add(dc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["flag"].ToString() == "0")
            {
                dr["状态"] = "未还";
            }
            else
            {
                dr["状态"] = "已还";
            }

        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_drawList.DataSource = ds;
            grdvw_drawList.DataBind();
            int intColumnCount = grdvw_drawList.Rows[0].Cells.Count;
            grdvw_drawList.Rows[0].Cells.Clear();
            grdvw_drawList.Rows[0].Cells.Add(new TableCell());
            grdvw_drawList.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_drawList.DataSource = ds;
            grdvw_drawList.DataBind();

        }
}
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableCell headerDetail = new TableCell();
            headerDetail.Text = "领样/修改";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);
            TableCell headerDetail1 = new TableCell();
            headerDetail1.Text = "还样";
            headerDetail1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail1.Width = 60;
            e.Row.Cells.Add(headerDetail1);
         
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = id.ToString();
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ID = "Draw";
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
           
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            //手动添加详细和删除按钮
            TableCell tabcDetail1 = new TableCell();
            tabcDetail1.Width = 60;
            tabcDetail1.Style.Add("text-align", "center");
            ImageButton ibtnDetail1 = new ImageButton();
            ibtnDetail1.ImageUrl = "~/images/Detail.gif";

            ibtnDetail1.CommandName = "Select";
            tabcDetail1.Controls.Add(ibtnDetail1);
            e.Row.Cells.Add(tabcDetail1);

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            //e.Row.Cells[12].Visible = false;
            e.Row.Cells[16].Visible = false;

        }
    }
    protected void grdvw_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        bool flag = true;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
         
            if (e.Row.Cells[2].Text.Trim() != "&nbsp;")
            {
                string getitemstr = "select AIName,MonitorItem,flag from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + e.Row.Cells[2].Text.Trim() + "' and delflag=0";
                DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
                if (dsitem != null && dsitem.Tables.Count > 0)
                {
                    DataRow[] dr = dsitem.Tables[0].Select("flag<=0");
                    if (dr.Length == 0)
                    { flag = false; }
                    foreach (DataRow drr in dsitem.Tables[0].Rows)
                    {

                        if (drr[2].ToString().Trim() == "0")
                        {
                            e.Row.Cells[15].Text += drr[0].ToString() + ",";
                            e.Row.Cells[16].Text += drr[1].ToString() + ",";
                        }
                        else if (drr[2].ToString().Trim() == "1")
                        {
                            e.Row.Cells[15].Text += "#" + drr[0].ToString() + "#,";
                            e.Row.Cells[16].Text += drr[1].ToString() + ",";
                        }
                        else
                        {
                            e.Row.Cells[15].Text += "[" + drr[0].ToString() + "],";
                            e.Row.Cells[16].Text += drr[1].ToString() + ",";
                        }

                    }
                }
            }
           
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //初始化checkboxlist


            bool flag1 = false;
            bool flag2 = false;
            ImageButton draw = (ImageButton)e.Row.FindControl("draw");
            getlyrecord(e.Row.Cells[2].Text.Trim(), out flag1);

            //若样品已被领用则处于不能领样状态

            if (e.Row.Cells[10].Text.Trim() == "1")
            {
                flag2 = false;

            }
            else
            {
                flag2 = true;
            }


            if (flag)
            {
               // draw.Enabled = flag1 || flag2;
                if (!(flag1 || flag2))
                    e.Row.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                //draw.Enabled = flag;
                e.Row.BackColor = System.Drawing.Color.Salmon;
            }
        }
       
    }     
    //绑定CheckBoxList的方法     
    private void BindCheckBoxList(CheckBoxList cbl, string SampleID)     
    {
        //string[] name = e.Row.Cells[14].Text.Trim().Split(',');
        //string[] namevalue = e.Row.Cells[15].Text.Trim().Split(',');      
        DataSet ds = new MyDataOp("select MonitorItem,AIName from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_MonitorItemDetail.MonitorItem=t_M_AnalysisItemEx.id where t_MonitorItemDetail.flag=0 and bz=0 and SampleID='" + SampleID + "'").CreateDataSet();
        
            //这里的方法根据你自己的取数据的方法      
        cbl.DataSource = ds;
        cbl.DataValueField = "MonitorItem";
        cbl.DataTextField = "AIName";       
        cbl.DataBind();     
    }
    private string getlyrecord(string SampleID,out bool flag)
    {
        string retstr = "";
        DataSet ds = new MyDataOp("select t_DrawSample.CreateDate,MonitorItem,AIName,LyUserID,Name  from t_DrawSample inner join t_R_UserInfo on LyUserID=UserID  inner join t_MonitorItemDetail on fxDanID=t_DrawSample.id inner join t_M_AnalysisItemEx  on t_M_AnalysisItemEx.id=MonitorItem where t_DrawSample.SampleID='" + SampleID + "' order by t_DrawSample.CreateDate desc").CreateDataSet();
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                retstr += dr["Name"].ToString() + "于" + dr["CreateDate"].ToString() + "分析项目： " + dr["AIName"].ToString() + "\r\n";
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LyUserID"].ToString() == Request.Cookies["Cookies"].Values["u_id"].ToString())
                    flag = true;
                else
                    flag = false;
            }
            else flag = true;
        }
        else
            flag = true;
       return retstr;
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }
   
   
    #endregion

    #region 领样 
    protected void btn_draw_OnClick(object sender, EventArgs e)
    {
        strDrawId = "0";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showDraw", "DrawShow();", true);
        GetDetail(0);
        DrawList();
        Query();
    }
    protected void GetDetail( int flag)
    {
        string getitemstr = "";
             getitemstr = " select t_M_AnalysisItemEx.ID,AIName 监测项 , value 分析值 ,method 分析方法,fxDanID DrawID from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem left join t_DrawSampleDetail on t_DrawSampleDetail.DrawID=t_MonitorItemDetail.fxDanID and ItemID=MonitorItem where SampleID='" + strSelectedId + "' and delflag=0 and flag=0";
      

        DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
        if (dsitem.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            dsitem.Tables[0].Rows.Add(dsitem.Tables[0].NewRow());
            grv_itemlist.DataSource = dsitem;
            grv_itemlist.DataBind();
            int intColumnCount = grv_itemlist.Rows[0].Cells.Count;
            grv_itemlist.Rows[0].Cells.Clear();
            grv_itemlist.Rows[0].Cells.Add(new TableCell());
            grv_itemlist.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grv_itemlist.DataSource = dsitem;
            grv_itemlist.DataBind();
        }
    }
    //选择领样记录
    protected void grdvw_drawList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strDrawId = grdvw_drawList.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showDraw", "DrawShow();", true);
        GetDetail(1);
        DrawList();
        Query();
    }
   
    protected void grdvw_drawList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
           
            TableCell headerDetail = new TableCell();
            headerDetail.Text = "查看/编辑";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);
            //TableCell headerdel = new TableCell();
            //headerdel.Text = "撤销";
            //headerdel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerdel.Width = 60;
            //e.Row.Cells.Add(headerdel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();
           
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
           
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);
            ////手动添加详细和删除按钮
            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/Images/Delete.gif";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            ////if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            ////{
            ////    ibtnDel.Enabled = false;
            ////}
            //tabcDel.Controls.Add(ibtnDel);
            //e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Visible = false;
          
        }
    }

    protected void grdvw_drawList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_drawList.Rows[e.RowIndex].Cells[2].Text;
        strDrawId = grdvw_drawList.Rows[e.RowIndex].Cells[1].Text;
        string namelist=grdvw_drawList.Rows[e.RowIndex].Cells[5].Text;
        string strSql;
        string[] deletelist = new string[4];
        int i = 0;
        strSql = "DELETE FROM t_DrawSample WHERE id = '" + strDrawId + "'";
        deletelist.SetValue(strSql, i++);
        strSql = "DELETE FROM t_DrawSampleDetail WHERE DrawID = '" + strDrawId + "'";
        deletelist.SetValue(strSql, i++);
        strSql = "update t_MonitorItemDetail set flag=0,fxDanID=0 where fxDanID='" + strDrawId + "'";     
        deletelist.SetValue(strSql, i++);

        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(i, deletelist))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        string logstr = "insert into t_Y_LogInfo(functionid,LogMessage,LogDate,UserID)values('79','删除样品领用记录" + strSelectedId + strDrawId + namelist + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
        mdo.ExecuteCommand(logstr);
        DrawList();
        Query();
    }
    #endregion
    #region analysisitem


    protected void grv_itemlist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableCell headerDetail1 = new TableCell();
            headerDetail1.Text = "分析方法选择";
            headerDetail1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
          
            e.Row.Cells.Add(headerDetail1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

           CheckBox cb_id= e.Row.Cells[0].FindControl("cb_id") as CheckBox;
           cb_id.Text = id.ToString();
           //手动添加分析项目添加
           TableCell tabcSelect = new TableCell();

           tabcSelect.Style.Add("text-align", "left");
           RadioButtonList cbl = new RadioButtonList();
           cbl.ID = "cbl_item";
          
           tabcSelect.Controls.Add(cbl);
           e.Row.Cells.Add(tabcSelect);     
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
    }
    protected void grv_itemlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //初始化checkboxlist
            CheckBox cb = (CheckBox)e.Row.FindControl("cb_id");
            RadioButtonList cbl = (RadioButtonList)e.Row.FindControl("cbl_item");
           
            if (cbl != null)
            {
                if (e.Row.Cells[1].Text.Trim() != "&nbsp;")
                {
                    BindCheckBoxListItem(cbl, e.Row.Cells[1].Text.Trim());
                    if (strDrawId=="0"||strDrawId != e.Row.Cells[4].Text.Trim())
                    {
                        cbl.SelectedIndex = 0;
                    }
                    else
                    {

                        if (e.Row.Cells[3].Text.Trim() != "&nbsp;")
                        {
                            cbl.SelectedValue = e.Row.Cells[3].Text.Trim();
                            cb.Checked = true;
                        }
                    }
                    btn_OK.Visible = true; 
                }
                else
                {
                    btn_OK.Visible = false; 
                }
            }

        }
    }
    //绑定CheckBoxList的方法     
    private void BindCheckBoxListItem(RadioButtonList cbl, string ItemID)
    {
        DataSet ds = new MyDataOp("select CASE WHEN area IS NULL  THEN t_M_AIStandard.Standard ELSE t_M_AIStandard.Standard + ':' + area END AS Standard,t_M_AIStandard.id from t_M_AIStandard inner join t_M_AnalysisItemEx on t_M_AIStandard.AIID=t_M_AnalysisItemEx.id where t_M_AnalysisItemEx.id='" + ItemID + "'").CreateDataSet();
        //这里的方法根据你自己的取数据的方法      
        cbl.DataSource = ds;
        cbl.DataValueField = "id";
        cbl.DataTextField = "Standard";
        cbl.DataBind();
    }

    
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        Query();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
      
        string[] strlist = new string[3 + grv_itemlist.Rows.Count];
        string sampleID = strSelectedId;
        string itemlist = "";
        string itemValuelist = "";
        int j = 0;

        DateTime lydate = DateTime.Now;
        bool flag = false;
        Entity.Draw entity = new Entity.Draw();
        entity.ID = int.Parse(strDrawId);
        entity.SampleID = sampleID;
        entity.LyDate = DateTime.Now;
        entity.UserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        //string getitemstr = "select AIName,MonitorItem from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=t_MonitorItemDetail.MonitorItem inner join t_DrawSample  on t_DrawSample.id=t_MonitorItemDetail.FxDanID where t_DrawSample.SampleID='" + sampleID + "' and fxuser='" + entity.UserID + "' and t_DrawSample.flag=0";
        //DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
        //foreach (DataRow dr in dsitem.Tables[0].Rows)
        //{
        //    itemlist += dr["AIName"].ToString() + ",";
        //    itemValuelist += dr["MonitorItem"].ToString() + ",";
        //}
        //更新分析项目状态为分析中
        for (int i = 0; i < grv_itemlist.Rows.Count; i++)
        {
            CheckBox cbl = (CheckBox)grv_itemlist.Rows[i].FindControl("cb_id");
            RadioButtonList rbl = (RadioButtonList)grv_itemlist.Rows[i].FindControl("cbl_item");
            string aiid = grv_itemlist.Rows[i].Cells[1].Text.Trim();
            string ainame = grv_itemlist.Rows[i].Cells[2].Text.Trim();
            Entity.SampleItem item = new Entity.SampleItem();
            item.MonitorID = int.Parse(aiid);
            item.SampleID = sampleID;
            if (cbl.Checked)
            {
                flag = true;
                if (!itemlist.Contains(ainame + ","))
                {
                    itemlist += ainame + ",";
                    itemValuelist += aiid + ",";
                }
               
                item.flag = true;

                entity.status = 0;
                item.Method = rbl.SelectedValue;
                entity.SampleItemList.Add(item);
                item = null;
          }
           
        }
        entity.ItemList = itemlist;
        entity.ItemValueList = itemValuelist;
        if (flag)
        {
      
            DAl.DrawSample DrawSampleobj = new DAl.DrawSample();
            int returnID=0;
            if (strDrawId == "0")
                returnID = DrawSampleobj.AddDrawSample(entity);
            else
                returnID = DrawSampleobj.EditDrawSample(entity);
            if (returnID > 0)
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "alert('样品领用保存成功！');unshowDraw();", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddUnSuccess", "alert('样品领用保存失败！')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddUnSuccess", "alert('请选择本次领样要分析的项目！')", true);
        }
        DrawList();
        Query();
    }
    #endregion

    #region 还样

    //修改
    protected void grdvw_ReportDetail_RowSelecting(object sender, GridViewSelectEventArgs e)
    { 
         //strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
         //string SampleId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
         //TextBox txt_value = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[15].FindControl("txt_value") as TextBox;
       
         //   string upstr = "update t_MonitorItemDetail set flag=0,fxDanID=null where id='" + strSelectedId + "'";
         //   MyDataOp sqlobj = new MyDataOp(upstr);
         //   if (sqlobj.ExecuteCommand())
         //       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('申领退回成功！')", true);
         //   else
         //       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('申领退回失败！')", true);
         //   Query();
    }
   
    //还样
    protected void grdvw_ReportDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strDrawId = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        Entity.Draw entity = new Entity.Draw();
        entity.ID = int.Parse(strDrawId);
        entity.SampleID =strSelectedId;
        entity.returndate = DateTime.Now.ToString();
        entity.UserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        TextBox txt_remark= grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[7].FindControl("txt_Remark") as TextBox;
        entity.Remark = txt_remark.Text.Trim();
       

            DAl.DrawSample DrawSampleobj = new DAl.DrawSample();
            if (DrawSampleobj.ReturnSample(entity) > 0)
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "alert('还样保存成功！');hiddenDetail();", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddUnSuccess", "alert('还样保存失败！')", true);
            DrawReList();
        Query();
    }
   
    protected void grdvw_ReportDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableCell headerDetail1 = new TableCell();
            headerDetail1.Text = "备注";
            headerDetail1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail1.Width = 60;
            e.Row.Cells.Add(headerDetail1);
            TableCell headerDetail = new TableCell();
            headerDetail.Text = "还样";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();
            //手动添加分析项目添加
            TableCell tabcSelect = new TableCell();
            tabcSelect.Width = 100;
            tabcSelect.Style.Add("text-align", "center");
            TextBox txt_value = new TextBox();
            txt_value.ID = "txt_Remark";
            tabcSelect.Controls.Add(txt_value);
            e.Row.Cells.Add(tabcSelect);
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ID = "Return";
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
           
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Visible = false;
           // e.Row.Cells[3].Visible = false;
           // e.Row.Cells[5].Visible = false;
           // e.Row.Cells[6].Visible = false;
           //// e.Row.Cells[10].Visible = false;
           // e.Row.Cells[11].Visible = false;
           // e.Row.Cells[12].Visible = false;
        }
    }
    //protected void grdvw_ReportDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //初始化checkboxlist
    //        TextBox txt_value = (TextBox)e.Row.FindControl("txt_value");
    //       // txt_value.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:dd'})");
    //        //if (txt_value != null)
    //        //{
    //        //    if( e.Row.Cells[12].Text.Trim()!="&nbsp;")
    //        //        txt_value.Text = e.Row.Cells[12].Text.Trim();
    //        //}
    //    }
    //}     
    #endregion


}
