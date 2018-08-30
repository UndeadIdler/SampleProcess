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

public partial class Sample_ReportSignSend : System.Web.UI.Page
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
            //txt_ReportTime.Attributes.Add("OnFocus", "javascript:calendar()");
            //txt_AccessTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //txt_ReportTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            Query();
            SetButton();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>报告装订</b></font>"; 
        }
    }
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            btn_OKAnalysis.Enabled = false;
           
        }
        else
        {
            btn_OKAnalysis.Enabled = true;
           
            // btn_AddSample.Enabled = true;
        }
    }
    #region 样品列表

    private void Query()
    {
        string strSql = "select t_M_SampleInfor.id,AccessDate 接样时间,t_M_SampleInfor.ItemType,ItemName 项目类型,SampleID 样品编号,t_M_SampleInfor.TypeID,t_M_SampleType.SampleType 样品类型,ClientID,ClientName 委托单位,ReportWriteDate,ReportWriteUserID,ReportWriteRemark,ReportProofDate,ReportProofUserID,ReportProofRemark,ReportCheckDate,ReportSignUserID,ReportProofRemark from t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType ,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and  t_M_SampleInfor.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID and StatusID=5 order by t_M_SampleInfor.id ";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportWriteUserID"] = drr["Name"].ToString();
                if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportProofUserID"] = drr["Name"].ToString();
                if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportSignUserID"] = drr["Name"].ToString();
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
    
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    //protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    lbl_Type.Text = "编辑";
    //    strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
    //    txt_SampleID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text;
    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    //    ListItem choose = new ListItem("请选择", "-1");
    //    MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemCode asc", DropList_ItemList);
    //    DropList_ItemList.SelectedItem.Selected = false;
    //    DropList_ItemList.Items.Add(choose);
    //    DropList_ItemList.Items.FindByValue(grdvw_List.Rows[e.NewEditIndex].Cells[3].Text).Selected = true;
    //    MyStaVoid.BindList("SampleType", "TypeID", "select * from t_M_SampleType order by SampleTypeCode asc", DropList_SampleType);
    //    DropList_SampleType.SelectedItem.Selected = false;
    //    DropList_SampleType.Items.Add(choose);
    //    DropList_SampleType.Items.FindByValue(grdvw_List.Rows[e.NewEditIndex].Cells[6].Text).Selected = true;
    //    //MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClass order by ClassCode asc", DropList_AnalysisMainItem);
    //    //DropList_AnalysisMainItem.SelectedItem.Selected = false;
    //    //DropList_AnalysisMainItem.Items.Add(choose);
    //    //string str = "select ClassID from t_M_MonitorItem inner join t_M_AnalysisItem on t_M_MonitorItem.MonitorItem=t_M_AnalysisItem.id where SampleID='" + strSelectedId + "'";
    //    //DataSet dscheck = new MyDataOp(str).CreateDataSet();
    //    //if (dscheck.Tables[0].Rows.Count > 0)
    //    //    DropList_AnalysisMainItem.Items.FindByValue(dscheck.Tables[0].Rows[0][0].ToString()).Selected = true;
    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();hiddenDetailAnalysis();", true);
        

    //    //DropList_AnalysisMainItem_SelectedIndexChanged(null,null);
    //}
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "详细/编辑";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);

            TableCell headerset = new TableCell();
            headerset.Text = "报告装订";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

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

            TableCell MenuSet = new TableCell();
            MenuSet.Width = 60;
            MenuSet.Style.Add("text-align", "center");
            ImageButton btMenuSet = new ImageButton();
            btMenuSet.ImageUrl = "~/images/Detail.gif";
            btMenuSet.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuSet.Controls.Add(btMenuSet);
            e.Row.Cells.Add(MenuSet);

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
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[18].Visible = false;
            
        }
    }
    //protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
    //    string strSql;
    //    string[] deletelist = new string[2];
    //    strSql = "DELETE FROM t_M_SampleInfor WHERE id= '" + strSelectedId + "'";
    //    //待修改，改项目删除后，相应要修改的信息

    //    //deletelist.SetValue("delete from t_操作员信息 where 所属角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 0);
    //    //deletelist.SetValue("delete from t_角色菜单关系表 where 角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 1);
    //    // deletelist.SetValue("DELETE FROM t_角色信息 WHERE 级别id = '" + strSelectedId + "'",2);

    //    deletelist.SetValue(strSql, 0);
    //    MyDataOp mdo = new MyDataOp(strSql);
    //    if (mdo.DoTran(1,deletelist))
    //    {
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
    //    }
    //    Query();
    //}
   
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
        lbl_Type.Text = "报告签发";
        txt_AccessTime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
        txt_ItemList.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
        txt_ItemList.ReadOnly = true;
        txt_SampleType.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[7].Text.Trim();
        txt_SampleType.ReadOnly = true;
        txt_checktime.Text = "";
        txt_person.Text = "";
        txt_remark.Text = "";
        //txt_VerifyTime.Text = "";
        //txt_VerifyMan.Text = "";
        //txt_VerifyRemark.Text = "";
        txt_signtime.Text = "";
        txt_signremark.Text = "";
        txt_user.Text = "";
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[10].Text.Trim() != "&nbsp;")
            txt_checktime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[10].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[11].Text.Trim() != "&nbsp;")
            txt_person.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[11].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[12].Text.Trim() != "&nbsp;")
            txt_remark.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[12].Text.Trim();
        //if (grdvw_List.Rows[e.NewSelectedIndex].Cells[13].Text.Trim() != "&nbsp;")
        //    txt_VerifyTime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[13].Text.Trim();
        //if (grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text.Trim() != "&nbsp;")
        //    txt_VerifyMan.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text.Trim();
        //if (grdvw_List.Rows[e.NewSelectedIndex].Cells[15].Text.Trim() != "&nbsp;")
        //    txt_VerifyRemark.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[15].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[16].Text.Trim() != "&nbsp;")
            txt_signtime.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[16].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[18].Text.Trim() != "&nbsp;")
            txt_signremark.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[18].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[17].Text.Trim() != "&nbsp;")
            txt_user.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
        txt_endtime.Text = "";
        txt_receivetime.Text = "";

        //ListItem choose = new ListItem("请选择", "-1");
        //MyStaVoid.BindList("Name", "JobID", "select * from t_userinfo order by Name asc", drop_user);
        //drop_user.SelectedItem.Selected = false;
        //drop_user.Items.Add(choose);
        //drop_user.Items.FindByValue("-1").Selected = true;
        //ListItem choose = new ListItem("请选择", "-1");
        //MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemCode asc", DropList_ItemList);
        //DropList_ItemList.SelectedItem.Selected = false;
        //DropList_ItemList.Items.Add(choose);
        //DropList_ItemList.Items.FindByValue(grdvw_List.Rows[e.NewSelectedIndex].Cells[3].Text).Selected = true;
        //MyStaVoid.BindList("SampleType", "TypeID", "select * from t_M_SampleType order by SampleTypeCode asc", DropList_SampleType);
        //DropList_SampleType.SelectedItem.Selected = false;
        //DropList_SampleType.Items.Add(choose);
        //DropList_SampleType.Items.FindByValue(grdvw_List.Rows[e.NewSelectedIndex].Cells[6].Text).Selected = true;
        queryAnalysisItem();

    }
    protected void queryAnalysisItem()
    {
        string sql = "select t_M_MonitorItem.id,MonitorItem,t_M_MonitorItem.SampleID,t_M_SampleInfor.SampleID 样品编号,AIName 分析项目 ,Num 数量,ReportDate  收到时间,flag,Remark 备注 from t_M_MonitorItem,t_M_AnalysisItem,t_M_SampleInfor where t_M_SampleInfor.id=t_M_MonitorItem.SampleID and t_M_MonitorItem.MonitorItem=t_M_AnalysisItem.id  and t_M_SampleInfor.id='" + strSelectedId + "' order by t_M_SampleInfor.SampleID";
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
        grdvw_ListAnalysisItem.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #3333FF;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>" + strSampleId + "的分析项目列表</b></font>";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        Query();
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
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[8].Visible = false;

        }
    }
   
    
   
    protected void btn_ExitAnalysisItem_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
       
        queryAnalysisItem();
    }
    
    #endregion
    #region 分析项目添加
    protected void btn_OKAnalysis_Click(object sender, EventArgs e)
    { if (txt_endtime.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请填写收到时间！');", true);
        }
    else if (txt_receivetime.Text.Trim() == "")
       {
           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请填写装订时间！');", true);

       }
       else
       {
       

        #region 编辑记录


           string strSql = @"update t_M_SampleInfor set ReportSignDate='" + txt_receivetime.Text.Trim() +
                            "',StatusID=6,ReportBindDate='" + txt_endtime.Text.Trim() + "' where id='" + strSelectedId + "'";
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.ExecuteCommand();
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLog("报告装订中保存提交:" + strSampleId + "（" + strSelectedId + "）！成功", Request.Cookies["Cookies"].Values["u_id"].ToString(), 10);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
            }
            else
            {
                WebApp.Components.Log.SaveLog("报告装订中保存提交:" + strSampleId + "（" + strSelectedId + "）！失败", Request.Cookies["Cookies"].Values["u_id"].ToString(), 10);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
            }
       


            #endregion
       }
        queryAnalysisItem();
       

    }
    
    protected void grdvw_ListAnalysisItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grdvw_ListAnalysisItem.PageIndex = e.NewPageIndex;
        queryAnalysisItem();


    }
  
   
    #endregion
    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
       string strSample="";
        string strDate="";
        if(txt_samplequery.Text!="")
            strSample="and SampleID like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text!= "")
            strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_M_SampleInfor.id,AccessDate 接样时间,t_M_SampleInfor.ItemType,ItemName 项目类型,SampleID 样品编号,t_M_SampleInfor.TypeID,t_M_SampleType.SampleType 样品类型,ClientID,ClientName 委托单位,ReportWriteDate,ReportWriteUserID,ReportWriteRemark,ReportProofDate,ReportProofUserID,ReportProofRemark,ReportCheckDate,ReportSignUserID,ReportProofRemark from t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType ,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and  t_M_SampleInfor.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID " + strSample + strDate + " and StatusID=5 order by t_M_SampleInfor.id ";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportWriteUserID"] = drr["Name"].ToString();
                if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportProofUserID"] = drr["Name"].ToString();
                if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportSignUserID"] = drr["Name"].ToString();
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
}