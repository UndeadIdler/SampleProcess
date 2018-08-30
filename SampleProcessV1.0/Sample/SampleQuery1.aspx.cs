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

public partial class Sample_SampleQuery1 : System.Web.UI.Page
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
            txt_endtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_receivetime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_QueryEndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_ReportTime.Attributes.Add("OnFocus", "javascript:calendar()");
            //txt_AccessTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //txt_ReportTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClassEx order by ClassCode asc", txt_type);
            ListItem choose = new ListItem("全部", "-1");
            txt_type.SelectedItem.Selected = false;
            txt_type.Items.Add(choose);
            txt_type.Items.FindByValue("-1").Selected = true;
            DropList_AnalysisMainItem_SelectedIndexChanged(null, null);
            MyStaVoid.BindList("ItemName", "ItemID", "select ItemID, ItemName from t_M_ItemInfo  order by ItemName ", txt_item);
            txt_item.Items.Add(choose);
            txt_item.Items.FindByValue("-1").Selected = true;
            //MyStaVoid.BindList("SampleType", "TypeID", "select TypeID, SampleType from t_M_SampleType  order by SampleType ", txt_type);
            //txt_type.Items.Add(choose);
            //txt_type.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("Status", "StatusID", "select StatusID, Status from t_M_StatusInfo  order by StatusID ", drop_status);
            drop_status.Items.Add(choose);
            drop_status.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("ClientName", "id", "select id,ClientName from t_M_ClientInfo  order by id ", drop_client);
            drop_client.Items.Add(choose);
            drop_client.Items.FindByValue("-1").Selected = true;


            btn_query_Click(null,null);

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
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 

            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
           
        }
    }
   
   
    #endregion

    #region 其它函数


    private string Verify()
    {
        string strErrorInfo = "";
        if (txt_SampleID.Text.Trim() == "")
        {
            strErrorInfo += "样品编码不能为空！\\n";
        }
        else
        {
            string str = "select * from t_M_SampleInfor where SampleID='" + txt_SampleID.Text.Trim() + "'";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
               strErrorInfo= "样品编号不能重复!";
            else
                strErrorInfo= "";
        }
        return strErrorInfo;
    }
   
    #endregion

    
    
    

    #endregion
   
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        strSampleId = grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        txt_SampleID.Text = strSampleId;
        txt_SampleID.ReadOnly = true;
        txt_AccessTime.ReadOnly = true;
       
        txt_AccessTime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
        txt_ItemList.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
        txt_ItemList.ReadOnly = true;
        txt_SampleType.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[7].Text.Trim();
        txt_SampleType.ReadOnly = true;
        txt_checktime.Text = "";
        txt_person.Text = "";
        txt_remark.Text = "";
        txt_VerifyTime.Text = "";
        txt_VerifyMan.Text = "";
        txt_VerifyRemark.Text = "";
        txt_signtime.Text = "";
        txt_signremark.Text = "";
        txt_user.Text = "";
        txt_endtime.Text = "";
        txt_receivetime.Text = "";
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[13].Text.Trim() != "&nbsp;")
            txt_checktime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[13].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text.Trim() != "&nbsp;")
            txt_person.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[15].Text.Trim() != "&nbsp;")
            txt_remark.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[15].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[16].Text.Trim() != "&nbsp;")
            txt_VerifyTime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[16].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[17].Text.Trim() != "&nbsp;")
            txt_VerifyMan.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[18].Text.Trim() != "&nbsp;")
            txt_VerifyRemark.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[18].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[19].Text.Trim() != "&nbsp;")

            txt_signtime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[19].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[21].Text.Trim() != "&nbsp;")
            txt_signremark.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[21].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[20].Text.Trim() != "&nbsp;")
            txt_user.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[20].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[22].Text.Trim() != "&nbsp;")
            txt_endtime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[22].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[23].Text.Trim() != "&nbsp;")
            txt_receivetime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[23].Text.Trim();
       
        queryAnalysisItem();

    }
    protected void queryAnalysisItem()
    {
        string sql = "select t_M_MonitorItem.id,MonitorItem,t_MonitorItemDetail.SampleID,t_M_SampleInfor.SampleID 样品编号,AIName 分析项目 ,Num 数量,ReportDate  收到时间,flag,Remark 备注 from t_M_MonitorItem,t_M_AnalysisItemEx,t_M_SampleInfor where t_M_SampleInfor.SampleID=t_MonitorItemDetail.SampleID and t_MonitorItemDetail.MonitorItem=t_M_AnalysisItemEx.id  and t_M_SampleInfor.id='" + strSelectedId + "' order by t_M_SampleInfor.SampleID";
        DataSet ds = new MyDataOp(sql).CreateDataSet();
        DataColumn dc = new DataColumn("状态");
        ds.Tables[0].Columns.Add(dc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["flag"].ToString() == "0")
                dr["状态"] = "未完成";
            else
                dr["状态"] = "已完成";

        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_ListAnalysisItem.DataSource = ds;
            grdvw_ListAnalysisItem.DataBind();
            int intColumnCount = grdvw_ListAnalysisItem.Rows[0].Cells.Count;
            grdvw_ListAnalysisItem.Rows[0].Cells.Clear();
            grdvw_ListAnalysisItem.Rows[0].Cells.Add(new TableCell());
            grdvw_ListAnalysisItem.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else {
            grdvw_ListAnalysisItem.DataSource = ds;
            grdvw_ListAnalysisItem.DataBind();
        }
        ds.Dispose();
        grdvw_ListAnalysisItem.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #3333FF;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>" + strSampleId + "的分析项目列表</b></font>";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        btn_query_Click(null, null);
    }
    #region 项目列表

    protected void grdvw_ListAnalysisItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "填写报告时间";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);

            

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
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
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
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[4].Visible = false;
            //e.Row.Cells[9].Visible = false;
        }
    }
   
    
   
    protected void btn_ExitAnalysisItem_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
       
        queryAnalysisItem();
    }
    
    #endregion
    #region 分析项目添加
    
    
    protected void grdvw_ListAnalysisItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grdvw_ListAnalysisItem.PageIndex = e.NewPageIndex;
        queryAnalysisItem();


    }
  
   
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
        if(txt_samplequery.Text!="")//按样品编号

            strSample = "and t_M_SampleInfor.SampleID like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text.Trim() != "" && txt_QueryEndTime.Text.Trim() != "")//按采样时间
        {
            DateTime start=DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00");
            DateTime end=DateTime.Parse(txt_QueryEndTime.Text.Trim() + " 23:59:59");
            strDate = " and AccessDate between '"+ start+"' and '"+end+"'";
            //strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        }

        if(drop_status.SelectedValue!="-1")//按样品状态
        {
            strStatus = " and t_M_ReporInfo.StatusID='" + drop_status.SelectedValue + "' ";
        }
        if(txt_item.SelectedValue!="-1")// 按项目类型
        {
            strItem = " and t_M_ReporInfo.ItemType='" + txt_item.SelectedValue + "' ";
        }
        if (txt_type.SelectedValue != "-1")//按样品类型
        {
            strType = " and t_M_SampleInfor.TypeID='" + txt_type.SelectedValue + "' ";
        }
        if (drop_client.SelectedValue != "-1")//按委托单位
        {
            strClient = " and t_M_ReporInfo.ClientID='" + drop_client.SelectedValue + "' ";
        }
        if (drop_analysisstatus.SelectedValue != "-1")
        {
            strAanlysisStatus = "  and t_MonitorItemDetail.flag=" + drop_analysisstatus.SelectedValue + "";
 
        }
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
                        strAnalysis = " t_MonitorItemDetail.MonitorItem='" + item.Value + "' ";
                    else
                        strAnalysis += " or t_MonitorItemDetail.MonitorItem='" + item.Value + "' ";
                //}

                i++;
            }
        }
        //按选中的分析项目查询
        string strtitle = "";
        string strtitle2 = "";
        if (flag)
        {
            strtitle = " and (" + strAnalysis + ")";
            strtitle2 = " and  t_M_SampleInfor.SampleID in (select SampleID from t_MonitorItemDetail where 1=1  and (" + strAnalysis + "))";
        }
        string cstr = "";
        cstr = strUrgent + strSample + strDate + strStatus + strItem + strType + strClient + strAanlysisStatus + strtitle2;
            if (cstr == "")
            { 
                cstr = " and 1=1";
            }
            //  string strSql = "select  t_M_ReporInfo.ReportName 报告标识, t_M_SampleInfor.id,AccessDate 接样时间,t_M_ReporInfo.ItemType,ItemName 项目类型,t_M_SampleInfor.SampleID 样品编号,t_M_SampleInfor.TypeID,t_M_SampleType.SampleType 样品类型,t_M_ReporInfo.ClientID,ClientName 委托单位,t_M_ReporInfo.urgent 备注,t_M_ReporInfo.StatusID,Status 报告状态,t_M_ReporInfo.ReportWriteDate,t_M_ReporInfo.ReportWriteUserID,t_M_ReporInfo.ReportWriteRemark,t_M_ReporInfo.ReportProofDate,t_M_ReporInfo.ReportProofUserID,t_M_ReporInfo.ReportProofRemark,t_M_ReporInfo.ReportCheckDate,t_M_ReporInfo.ReportSignUserID,t_M_ReporInfo.ReportProofRemark,t_M_ReporInfo.ReportSignDate,t_M_ReporInfo.ReportBindDate,MonitorItem,AIName 分析项目,t_MonitorItemDetail.bz,t_M_ReporInfo.urgent,t_MonitorItemDetail.flag from t_M_ReporInfo, t_MonitorItemDetail,t_M_AnalysisItem, t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType ,t_M_ClientInfo,t_M_StatusInfo where t_M_ReporInfo.id=t_M_SampleInfor.ReportID and t_M_StatusInfo.StatusID=t_M_ReporInfo.StatusID and t_MonitorItemDetail.SampleID=t_M_SampleInfor.SampleID and t_MonitorItemDetail.MonitorItem=t_M_AnalysisItem.id and t_M_ClientInfo.id=t_M_ReporInfo.ClientID and  t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID " + cstr + " and t_MonitorItemDetail.bz=0 order by t_MonitorItemDetail.id Asc ";

            string strSql = "SELECT  t_M_SampleInfor.id,t_M_ReporInfo.Projectname 项目名称,t_M_SampleInfor.SampleID AS 样品编号,t_M_SampleInfor.SampleAddress 采样点 ,t_M_SampleInfor.AccessDate AS 接样时间," +
         "t_M_ReporInfo.chargeman AS 项目负责人, " +
         " t_M_SampleInfor.TypeID, t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, " +
        " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID,t_M_SampleInfor.ReportRemark" +
      " FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
        " t_M_AnalysisMainClassEx ON " +
       "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID " +

      " WHERE " +
        " 1=1"+ cstr+
      " ORDER BY t_M_SampleInfor.id";
            DataSet ds = new MyDataOp(strSql).CreateDataSet();
            foreach (ListItem item in cb_analysisList.Items)
            {
                if (item.Selected)
                {
                    ds.Tables[0].Columns.Add(item.Text.Trim());
                }
            }
            string sql = "select t_M_SampleInfor.SampleID,t_MonitorItemDetail.MonitorItem,AIName,t_MonitorItemDetail.*  from t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID inner join  t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.SampleID inner join  t_M_ANItemInf on t_M_ANItemInf.id=MonitorItem where 1=1  " + cstr + strtitle;
            DataSet dsitem = new MyDataOp(sql).CreateDataSet();

            //if (dsitem != null && dsitem.Tables.Count > 0)
            //{
            //    foreach (DataRow drr in dsitem.Tables[0].Rows)
            //    {
            //        if (!ds.Tables[0].Columns.Contains(drr[2].ToString()))
            //            ds.Tables[0].Columns.Add(drr[2].ToString());
            //    }
            //}

             string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    DataRow[] drs = ds_User.Tables[0].Select("UserID='" + dr["指派给"].ToString() + "'");
        //    if(drs.Length>0)
        //    {

        //        dr["指派给"] = drs[0]["Name"].ToString();
        //     }
        //    DataRow[] drchargeman = ds_User.Tables[0].Select("UserID='" + dr["项目负责人"].ToString() + "'");
        //    if (drchargeman.Length > 0)
        //    {

        //        dr["项目负责人"] = drchargeman[0]["Name"].ToString();
        //    }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow[] drchargeman = ds_User.Tables[0].Select("UserID='" + dr["项目负责人"].ToString() + "'");
                if (drchargeman.Length > 0)
                {

                    dr["项目负责人"] = drchargeman[0]["Name"].ToString();
                }
                for (int j = 12; j < ds.Tables[0].Columns.Count; j++)
                {
                    DataColumn dc = ds.Tables[0].Columns[j];


                    DataRow[] dritemlist = dsitem.Tables[0].Select("SampleID='" + dr["样品编号"].ToString() + "' and AIName='" + dc.ColumnName + "'");
                    if (dritemlist.Length > 0)
                    {
                        if (dritemlist[0]["experimentvalue"] != null && dritemlist[0]["experimentvalue"].ToString() != "")
                        {
                            if(dritemlist[0]["flag"].ToString()=="3")
                            dr[dc.ColumnName] = dritemlist[0]["experimentvalue"].ToString();
                        }
                    }
                    else
                        dr[dc.ColumnName] = "\\";


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
    protected void DropList_AnalysisMainItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds;
        string str;
        string constr="";
        if (txt_type.SelectedIndex != txt_type.Items.Count - 1)
        {
            constr = " and ClassID='" + txt_type.SelectedValue.Trim() + "'";
        }
        if (DropList_AnalysisMainItem.SelectedValue.ToString() == "-1")
        {
            str = "select distinct t_M_ANItemInf.id,t_M_ANItemInf.AIName from t_M_AnalysisItemEx inner join t_M_ANItemInf on t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID where 1=1 " + constr;
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        else
        {
            str = "select distinct t_M_ANItemInf.id,t_M_ANItemInf.AIName from t_M_AnalysisItemEx inner join t_M_ANItemInf on t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID where type='" + DropList_AnalysisMainItem.SelectedValue.ToString() + "'" + constr;
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        ds.Dispose();
       
    }   
}