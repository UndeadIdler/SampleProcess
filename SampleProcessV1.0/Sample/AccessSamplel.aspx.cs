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

public partial class AccessSamplel : System.Web.UI.Page
{
    private string strSelectedId//样品单号
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private DataTable dt_Sample//样品列表
    {
        get { return (DataTable)ViewState["dt_Sample"]; }
        set { ViewState["dt_Sample"] = value; }
    }
    private DataTable dt_analysis//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis"]; }
        set { ViewState["dt_analysis"] = value; }
    }
    
    private int samplestatus//样品单状态
    {
        get { return (int)ViewState["samplestatus"]; }
        set { ViewState["samplestatus"] = value; }
    }
    //private static Hashtable itemlist1=new Hashtable();
    //private static Hashtable itemlist2 = new Hashtable();
    //private static Hashtable itemlist3 = new Hashtable();
    //private static Hashtable itemlist4 = new Hashtable();
    //private static Hashtable itemlist5 = new Hashtable();
    private int flag
    {
        get { return (int)ViewState["flag"]; }
        set { ViewState["flag"] = value; }
    }
   // private static int flag = 0;
    private string strReportId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportId"]; }
        set { ViewState["strReportId"] = value; }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
              txt_CreateDate.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_SampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_AccessSampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
           // tbTestTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            //txt_fxtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
           // txt_jhtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            Query();
            SetButton();
          

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>样品接收</b></font>";
        }
    }
    #region 报告相关
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            //btn_Add.Enabled = false;
            //btn_OK.Enabled = false;
            btn_AddSample.Enabled = false;
            //btn_Save.Enabled = false;
            btn_OKSample.Enabled = false;
            //btn_item.Enabled = false;
            //for (int i = 0; i < grdvw_List.Rows.Count; i++)
            //{
            //  ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
            //    if(btn!=null)
            //  btn.Visible = false;

            //}
        }
        else
        {
            //btn_Add.Enabled = true;
            //btn_OK.Enabled = true;
            //btn_Save.Enabled = true;
            btn_AddSample.Enabled = true;
            btn_OKSample.Enabled = true;
           // btn_item.Enabled = true;
            //for (int i = 0; i < grdvw_List.Rows.Count; i++)
            //{
            //    ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
            //    if(btn!=null)
            //    btn.Visible = true;
            //}
        }
    }
    private void Query()
    {
        //strSelectedId=txt_samplequery.Text;
        string strSample = "";
        string strDate = "";
        if (txt_samplequery.Text != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text != "")
            strDate = " and (year(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_M_ReporInfo.id,t_M_ReporInfo.ReportAccessDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,ClientID,ClientName 区域,urgent 备注,t_M_ReporInfo.Ulevel,t_委托单位.单位全称 委托单位,Projectname 项目名称,t_R_UserInfo.Name 项目负责人  from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo,t_委托单位,t_R_UserInfo where t_M_ClientInfo.id=ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=1) and wetherscance=0 and t_委托单位.id=wtdepart and chargeman=t_R_UserInfo.UserID  " + strSample + strDate + " order by t_M_ReporInfo.id";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急";
            else
                dr["紧急程度"] = "一般";


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
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string slevel="";
        txt_ReportID.Text = "";
        txt_CreateDate.Text = "";
        txt_Projectname.Text = "";
        txt_wtdepart.Text = "";
        txt_xmfzr.Text = "";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim() != "&nbsp;")
        txt_ReportID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();
       
        lbl_Type.Text = "编辑";

        txt_CreateDate.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim();//报告创建日期
        //txt_itemname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        //txt_itemname.ReadOnly = true;
        ListItem choose = new ListItem("请选择", "-1");
        //txt_itemname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
        drop_urgent.Text = "";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[8].Text != "&nbsp;")
            drop_urgent.Text = grdvw_List.Rows[e.NewEditIndex].Cells[8].Text;
        drop_level.SelectedValue = "0";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim()!="")
        {
            slevel = grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim();
            drop_level.SelectedValue = slevel;
        }
        MyStaVoid.BindList("ClientName", "id", "select * from t_M_ClientInfo order by id", DropList_client);

        string clientid = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text;
        DropList_client.Items.Add(choose);
        DropList_client.Items.FindByValue(clientid).Selected = true;
        MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);

        string itemtid = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue(itemtid).Selected = true;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim() != "")
        {
            txt_wtdepart.Text = grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[12].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[12].Text.Trim() != "")
        {
            txt_xmfzr.Text = grdvw_List.Rows[e.NewEditIndex].Cells[12].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim() != "")
        {
            txt_Projectname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim();
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();showAnalysis();hiddenDetailAnalysisAdd()", true);
        //lbl_Type.Text = "样品列表";
        //btn_Save.Visible = true;
       
        ReportSelectQuery();

    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "查看/编辑";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);

            TableCell headerset = new TableCell();
            headerset.Text = "提交";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1)=="1")
            //{
            //    TableCell headerDel = new TableCell();
            //    headerDel.Text = "删除";
            //    headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //    headerDel.Width = 30;
            //    e.Row.Cells.Add(headerDel);
            //}
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
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[12].Visible = false;
            

        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strReportId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string[] deletelist = new string[3];
        string str = "select id from t_M_SampleInfor where ReportID= '" + strReportId + "'";
        strSql = "DELETE FROM t_M_MonitorItem ";
        string condition = "";
        int i = 0;
        DataSet ds = new MyDataOp(str).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == 0)
            {
                condition = " WHERE SampleID= '" + dr[0].ToString() + "'";
            }
            else
            {
                condition += " or SampleID= '" + dr[0].ToString() + "'";
            }
            i++;

        }
       
       
        
        strSql = "DELETE FROM t_M_SampleInfor WHERE ReportID= '" + strReportId + "'";
        deletelist.SetValue(strSql, 0);
        strSql = "DELETE FROM t_M_ReporInfo WHERE id= '" + strReportId + "'";       
        deletelist.SetValue(strSql, 1);
        if (condition != "")
        {
            i = 4;
            strSql = "DELETE FROM t_M_MonitorItem " + condition;
            deletelist.SetValue(strSql, 2);
            strSql = "DELETE FROM t_MonitorItemDetail " + condition;
            deletelist.SetValue(strSql, 3);

        }
        else
            i = 2;
        
        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(i, deletelist))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + txt_ReportID.Text.Trim() + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
        }
        else
        {
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + txt_ReportID.Text.Trim() + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        Query();
    }
    #endregion
    protected void grdvw_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        strReportId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        string checkstr = "select count(*) from t_M_SampleInfor where ReportID='" + strReportId + "'";
        DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
        int count = 0;
        try
        {
            count = int.Parse(dscheck.Tables[0].Rows[0][0].ToString());
        }
        catch { }
        if (count != 0)
        {
            string strSqltmp = "";
            strSqltmp = @"update t_M_ReporInfo set StatusID=2 where id='" + strReportId + "'";
           
            MyDataOp mdo = new MyDataOp(strSqltmp);
            bool blSuccess = mdo.ExecuteCommand();
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLog("创建样品原单提交成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据提交成功！')", true);


            }
            else
            {
                WebApp.Components.Log.SaveLog("创建样品原单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添提交失败！')", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", ";alert('未添加样品无法提交')", true);
            ReportSelectQuery();
        }
        Query();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        //string strFlag = Verify();

        //if (strFlag != "")
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
        //    return;
        //}
        //else
        //{//获取样品来源编码
        //    string compay = "0";
        //    string cyman = "0";
        //    DataSet myDR2 = new MyDataOp("select id from t_委托单位 where 单位全称='" + txt_wtdepart.Text.Trim() + "'").CreateDataSet();
        //    if (myDR2.Tables[0].Rows.Count > 0)
        //    {
        //        compay = myDR2.Tables[0].Rows[0]["id"].ToString();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该委托单位，请核实！');", true);
        //        return;
        //    }
        //    //获取采样人，现场分析人
        //    DataSet myDR3 = new MyDataOp("select UserID from t_R_UserInfo where Name='" + txt_xmfzr.Text.Trim() + "'").CreateDataSet();
        //    if (myDR3.Tables[0].Rows.Count > 0)
        //    {
        //        cyman = myDR3.Tables[0].Rows[0]["UserID"].ToString();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在项目负责人/报告编制人，请核实！');", true);
        //        return;
        //    }
           
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        txt_ReportID.Text = "";
        Query();
    }



    //protected void btn_Add_Click(object sender, EventArgs e)
    //{
    //    lbl_Type.Text = "添加";
    //    //btn_OK.Text = "确定";
    //    btn_OKSample.Visible = true;
    //    txt_ReportID.Text = "";
    //    MyStaVoid.BindList("ClientName", "id", "select * from t_M_ClientInfo order by id", DropList_client);
    //    ListItem choose = new ListItem("请选择", "-1");
    //    DropList_client.Items.Add(choose);
    //    DropList_client.Items.FindByValue("-1").Selected = true;

    //    strReportId = "0";
    //    strReportName = "";
    //    txt_CreateDate.Text = "";
    //    txt_Projectname.Text = "";
    //    txt_wtdepart.Text = "";

    //    MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);
       
    //    drop_ItemList.Items.Add(choose);
    //    drop_ItemList.Items.FindByValue("-1").Selected = true;
    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();hiddenDetailAnalysis();hiddenDetailAnalysisAdd();", true);
    //    drop_urgent.Text = "";
    //    btn_Save.Visible = false;
       
    //    Query();
    //}
//    protected void btn_OK_Click(object sender, EventArgs e)
//    {
//        string strFlag = Verify();

//        if (strFlag != "")
//        {
//            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
//            // return;


//        }
//        else
//        { //获取样品来源编码
//            string compay = "0";
//            string cyman = "0";
//            DataSet myDR2 = new MyDataOp("select id from t_委托单位 where 单位全称='" + txt_wtdepart.Text.Trim() + "'").CreateDataSet();
//            if (myDR2.Tables[0].Rows.Count > 0)
//            {
//                compay = myDR2.Tables[0].Rows[0]["id"].ToString();
//            }
//            else
//            {
//                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该委托单位，请核实！');", true);
//                return;
//            }
//            //获取采样人，现场分析人
//            DataSet myDR3 = new MyDataOp("select UserID from t_R_UserInfo where Name='" + txt_xmfzr.Text.Trim() + "'").CreateDataSet();
//            if (myDR3.Tables[0].Rows.Count > 0)
//            {
//                cyman = myDR3.Tables[0].Rows[0]["UserID"].ToString();
//            }
//            else
//            {
//                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在项目负责人/报告编制人，请核实！');", true);
//                return;
//            }
//            #region 添加新纪录
//            if (strReportId == "" || strReportId=="0")
//            {
//                string clientname = DropList_client.SelectedValue.ToString();
//                string strSqltmp = "";

//                strSqltmp = @"insert into t_M_ReporInfo
//                    (CreateDate,ItemType,UserID,ClientID,ReportName,urgent,Ulevel,wtdepart,Projectname,StatusID,chargeman)  
//                    values('" + txt_CreateDate.Text.Trim() + "','" + drop_ItemList.SelectedValue.ToString() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "','" + clientname + "','" + txt_ReportID.Text.Trim() + "','" + drop_urgent.Text + "','" + drop_level.SelectedValue.ToString() + "','" +compay + "','" + txt_Projectname.Text.Trim() + "',0,'" + cyman + "')";
               
//                MyDataOp mdo = new MyDataOp(strSqltmp);
//                bool blSuccess = mdo.ExecuteCommand();
//                if (blSuccess == true)
//                {
//                    string getreportid = "select id from t_M_ReporInfo where ItemType='" + drop_ItemList.SelectedValue.ToString() + "' and ReportName='" + txt_ReportID.Text.Trim() + "' and Projectname='" + txt_Projectname.Text.Trim() + "' and StatusID=0 order by id desc";
//                    DataSet dsgetreportid = new MyDataOp(getreportid).CreateDataSet();
//                    if (dsgetreportid.Tables[0].Rows.Count >= 1)
//                    {
//                        strReportId = dsgetreportid.Tables[0].Rows[0][0].ToString();
//                    }
//                    ReportSelectQuery();
//                    btn_Save.Visible = true;
//                    WebApp.Components.Log.SaveLog("创建任务单添加成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
//                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAnalysis();", true);

//                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据添加成功！')", true);


//                }
//                else
//                {
//                    WebApp.Components.Log.SaveLog("创建任务单添加失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
//                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('数据添加失败！')", true);

//                }

//            }
//            #endregion
//            else 
//            {
//                string clientname = DropList_client.SelectedValue.ToString();
//                string strSqltmp = "";
//                strSqltmp = @"update t_M_ReporInfo set ReportName='" + txt_ReportID.Text.Trim() +
//                                                "',ItemType='" + drop_ItemList.SelectedValue.ToString() +
//                                                 "',ClientID='" + clientname +
//                                                 "',urgent='" + drop_urgent.Text +
//                                                 "',Ulevel='" + drop_level.SelectedValue.ToString() +
//                                                  "',wtdepart='" + compay +
//                                                  "',CreateDate='" + txt_CreateDate.Text +
//"',Projectname='" + txt_Projectname.Text +
//                                        "',StatusID=0,chargeman='"+cyman+"' where id='" + strReportId + "'";

//                MyDataOp mdo = new MyDataOp(strSqltmp);
//                bool blSuccess = mdo.ExecuteCommand();
//                if (blSuccess == true)
//                {
//                    WebApp.Components.Log.SaveLog("创建任务单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
//                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);


//                }
//                else
//                {
//                    WebApp.Components.Log.SaveLog("创建任务单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
//                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添保存失败！')", true);

//                }

//            }
          
//        }
//        Query();
//    }
//    private string Verify()
//    {
//        string strErrorInfo = "";

//        if (drop_ItemList.SelectedValue == "-1")
//            strErrorInfo += "请选择项目类型！\\n";
//        else if (txt_CreateDate.Text == "")
//            strErrorInfo += "请填写时间！\\n";

//        else if (txt_Projectname.Text.Trim() == "-1")
//            strErrorInfo += "请选择项目名称！\\n";
//        else if (txt_wtdepart.Text.Trim() == "-1")
//            strErrorInfo += "请选择委托单位！\\n";
//        else if (DropList_client.SelectedValue == "-1")
//            strErrorInfo += "请选择区域！\\n";
       
//        return strErrorInfo;
//    }
   
    #endregion
   
    /// <summary>
    /// 转换字符为m个字符长，不够前面补零
    /// </summary>
    /// <param name="strCode">待转换字符</param>
    /// <param name="m">结果字符长度</param>
    /// <returns>转换结果</returns>
    public static string CodeConvert(string strCode, int m)
    {
        string s = strCode;
        for (int i = 0; i < m; i++)
        {
            s = "0" + s;
        }
        s = s.Substring(s.Length - m, m);
        return s;
    }
   
   
   
    #region 样品单列表

    protected void btn_AddSample_Click(object sender, EventArgs e)
    {
        lbl_SampleDo.Text = "添加样品";
        InitialTable();
        DropList_SampleType.Text = ""; ;
        strSelectedId ="0";
        //hid_status.Value = "0";
        txt_SampleTime.Text ="";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);
        ReportSelectQuery();
        btn_OKSample.Visible = true;
       
    }
    private void newDt()
    { 
        dt_analysis=null;
        dt_analysis=new DataTable();
        DataColumn dc6 = new DataColumn("ID");
        DataColumn dc0 = new DataColumn("现场分析");
        DataColumn dc1 = new DataColumn("分析项目");
        DataColumn dc2 = new DataColumn("ItemID");
        DataColumn dc3 = new DataColumn("分析值");
        DataColumn dc70 = new DataColumn("分析方法");
        DataColumn dc4 = new DataColumn("Remark");
        DataColumn dc5 = new DataColumn("fxDanID");
            dt_analysis.Columns.Add((dc6));
            dt_analysis.Columns.Add((dc0));
            dt_analysis.Columns.Add((dc1));
            dt_analysis.Columns.Add((dc2));
            dt_analysis.Columns.Add((dc3));
            dt_analysis.Columns.Add((dc70));
            dt_analysis.Columns.Add((dc4));
            dt_analysis.Columns.Add((dc5)); 
           
    }
    private void InitialTable()
    {
        newDt();
       
        //hid_status.Value = "";

        txt_SampleSource.Text = "";
        txt_AccessSampleTime.Text = "";
        txt_SampleTime.Text = "";
        DropList_SampleType.Text = "";
        //tbTestTime.Text = "";
        tbSampleMan.Text = "";
        //tbTestPlace.Text = "";
        //txt_fxman.Text = "";
        //txt_fxtime.Text = "";
        //txt_jhman.Text = "";
        //txt_jhtime.Text = "";
        for (int i = 0; i < 5; i++)
        {
            DataRow dr = dt_Sample.NewRow();
            dt_Sample.Rows.Add(dr);
            dt_Sample.AcceptChanges();

        }
        RepeaterSample.DataSource = dt_Sample;
        RepeaterSample.DataBind();
        for (int i = 1; i < 5; i++)
        {
            //样品编号
            TextBox txt_SampleID = RepeaterSample.Items[i].FindControl("txt_SampleID") as TextBox;
           
            //采样点
            TextBox txt_SampleAddress = RepeaterSample.Items[i].FindControl("txt_SampleAddress") as TextBox;
           
            //样品列表
            TextBox txt_Item = RepeaterSample.Items[i].FindControl("txt_Item") as TextBox;
            
            //hid_Item1.Value = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
            //样品性质
            TextBox txt_xz = RepeaterSample.Items[i].FindControl("txt_xz") as TextBox;
            
            //样品数量
            TextBox txt_num = RepeaterSample.FindControl("txt_num") as TextBox;
            ////获取样品单监测项信息
            //GridView grv_Item = RepeaterSample.FindControl("grv_Item") as GridView;
            //CheckBox ck_xcflag = Panel_Sample.FindControl("ck_xcflag_" + i) as CheckBox;
            //TextBox txt_SampleID = Panel_Sample.FindControl("txt_SampleID" + i) as TextBox;
            //HiddenField hid_ID = Panel_Sample.FindControl("hid_ID" + i) as HiddenField;
            //GridView grv_Item = Panel_Sample.FindControl("grv_Item" + i) as GridView;
            //TextBox txt_SampleAddress = Panel_Sample.FindControl("txt_SampleAddress" + i) as TextBox;
            CheckBox ck_xcflag = RepeaterSample.Items[i].FindControl("ck_xcflag") as CheckBox;
            GridView grv_Item = RepeaterSample.Items[i].FindControl("grv_Item") as GridView;
            HiddenField hid_Item = RepeaterSample.Items[i].FindControl("hid_Item") as HiddenField;
            HiddenField hid_ID = RepeaterSample.Items[i].FindControl("hid_ID") as HiddenField;
            txt_Item.Text = "";
            hid_Item.Value = "";
            grv_Item.DataSource = null;
            grv_Item.DataBind();
            txt_SampleID.Text = "";
            hid_ID.Value = "";
            ck_xcflag.Checked = false;
            txt_SampleAddress.Text = "";
            txt_xz.Text = "";
           
        }
    }
    

    protected void grdvw_ReportDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList cbl = (CheckBoxList)e.Row.FindControl("cbl_item");

            if (cbl != null)
            {
               // BindCheckBoxList(cbl, e);
            }
        }
    }
    //检测样品单是否允许修改
    protected bool CheckSample(string sampleid)
    {
        bool ret = false;
        string checkstr = "select * from t_M_SampleInfor where SampleID='"+sampleid+"' and StatusID<3";
        DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
        if(dscheck!=null&& dscheck.Tables.Count>0)
            if (dscheck.Tables[0].Rows.Count > 0)
            {
                ret = true;
            }
        return ret;
    }
    protected void grdvw_ReportDetail_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        lbl_SampleDo.Text = "样品详单";
        InitialTable();
       
           DropList_SampleType.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[8].Text.Trim();
           strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
           txt_SampleSource.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
           txt_SampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[3].Text.Trim();
           txt_AccessSampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[6].Text.Trim();
           tbSampleMan.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[15].Text.Trim();

       #region 获取样品详细信息
        //样品编号
           TextBox txt_SampleID = RepeaterSample.Items[0].FindControl("txt_SampleID") as TextBox;
         txt_SampleID.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        //采样点
         TextBox txt_SampleAddress = RepeaterSample.Items[0].FindControl("txt_SampleAddress") as TextBox;
         if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[4].Text.Trim()!="&nbsp;")
           txt_SampleAddress.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
        //样品列表
           TextBox txt_Item = RepeaterSample.Items[0].FindControl("txt_Item") as TextBox;
           if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[16].Text.Trim() != "&nbsp;")
         txt_Item.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[16].Text.Trim();
           //hid_Item1.Value = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
        //样品性质
         TextBox txt_xz = RepeaterSample.Items[0].FindControl("txt_xz") as TextBox;
         if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[9].Text.Trim() != "&nbsp;")
           txt_xz.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[9].Text.Trim();
        //样品数量
           TextBox txt_num = RepeaterSample.Items[0].FindControl("txt_num") as TextBox;
           txt_num.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[14].Text.Trim();
           DAl.Sample sampleobj = new DAl.Sample();
           
            //获取样品单监测项信息
            GridView grv_Item = RepeaterSample.Items[0].FindControl("grv_Item") as GridView;
            DataTable dttemp = dt_analysis.Clone();
            DAl.Sample temp = new DAl.Sample();
            dttemp = temp.GetSampleDetail(txt_SampleID.Text);
            if (dttemp!=null&&dttemp.Rows.Count > 0)
            {
                grv_Item.DataSource = dttemp;
                grv_Item.DataBind();
                CheckBoxList cbl = RepeaterSample.Items[0].FindControl("cb_analysisList") as CheckBoxList;
                DataBindAll(cbl,grv_Item);
            }
            else
            {
                grv_Item.DataSource = null;
                grv_Item.DataBind();
            }
            CheckBox ck_xcflag = RepeaterSample.Items[0].FindControl("ck_xcflag") as CheckBox;
            if (ck_xcflag.Checked)
            {
                ck_xcflag.Checked = true;
                grv_Item.Visible = true;
            }
            else
            {
                ck_xcflag.Checked = false;
                grv_Item.Visible = false;
            }
           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);
           ReportSelectQuery();
           //根据样品状态来确定是否可以修改
           btn_OKSample.Visible = false;
           //}
           //else
           //{
           //     ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('交接完成的样品单不允许修改');", true);
           //}
#endregion
    }
    protected void grdvw_ReportDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ReportDetail.PageIndex = e.NewPageIndex;
        ReportSelectQuery();
    }
    protected void grdvw_ReportDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableCell headerset = new TableCell();
            headerset.Text = "查看/编辑";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);
            //TableCell headerDel = new TableCell();
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            //{
            //    headerDel.Text = "删除";
            //    headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //    headerDel.Width = 30;
            //    e.Row.Cells.Add(headerDel);

            //}
           
           
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
            ibtnDetail.CommandName = "Select";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);
            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/images/Delete.gif";
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
            //    ibtnDel.Enabled = false;
           


        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;

            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
        }
    }
    protected void btn_CancelReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();", true);
        Query();

    }
    /// <summary>
    /// 删除样品事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvw_ReportDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        strSelectedId = grdvw_ReportDetail.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string[] deletelist = new string[3];
        strSql = "DELETE FROM t_M_SampleInfor WHERE id= '" + strSelectedId + "'";
       
        deletelist.SetValue(strSql, 0);
        strSql = "DELETE FROM t_MonitorItemDetail WHERE SampleID= '" + grdvw_ReportDetail.Rows[e.RowIndex].Cells[5].Text.Trim() + "'";
        deletelist.SetValue(strSql, 1);
        strSql = "DELETE FROMt_DrawSample WHERE SampleID= '" + grdvw_ReportDetail.Rows[e.RowIndex].Cells[5].Text.Trim() + "'";
        deletelist.SetValue(strSql, 2);
        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(3, deletelist))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + grdvw_ReportDetail.Rows[e.RowIndex].Cells[5].Text + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
        }
        else
        {
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + grdvw_ReportDetail.Rows[e.RowIndex].Cells[5].Text + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        ReportSelectQuery();

    }
    //查询出选中的报告的样品列表
    private void ReportSelectQuery()
    {
        string strSql = "SELECT  t_M_SampleInfor.id,单位全称 样品来源,t_M_SampleInfor.SampleDate AS 采样时间,t_M_SampleInfor.SampleAddress 采样点 ,t_M_SampleInfor.SampleID AS 样品编号," +
      "t_M_SampleInfor.AccessDate AS 接样时间, " +
      " t_M_SampleInfor.TypeID,t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, " +
     " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID,t_M_SampleInfor.StatusID,t_M_SampleInfor.xcflag,t_M_SampleInfor.num 数量,cyman" +
" FROM  t_M_SampleInfor  INNER JOIN" +
     " t_M_AnalysisMainClassEx ON " +
    "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_委托单位 on t_委托单位.id=t_M_SampleInfor.SampleSource " +

" WHERE " +
     " t_M_SampleInfor.ReportID=" + strReportId +
" ORDER BY t_M_SampleInfor.id";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        DataColumn dccc = new DataColumn("分析项目");
        ds.Tables[0].Columns.Add(dccc);
        DataColumn dcc = new DataColumn("分析项目编码");
        ds.Tables[0].Columns.Add(dcc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string getitemstr = "select AIName,MonitorItem from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + dr["样品编号"].ToString() + "' and delflag=0";
            DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
            if (dsitem != null && dsitem.Tables.Count > 0)
            {
                foreach (DataRow drr in dsitem.Tables[0].Rows)
                {
                   dr["分析项目"] += drr[0].ToString() + ";";
                   dr["分析项目编码"] += drr[1].ToString() + ";";
                }
            }
       }
        dt_Sample = ds.Tables[0].Clone();
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
        ds.Dispose();
        Query();
       
    }
    protected void lbtn_chose_OnClick(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        RepeaterItem parent = lbtn_chose.Parent as RepeaterItem;
        CheckBoxList cbl = parent.FindControl("cb_analysisList") as CheckBoxList;
        GridView grv_Item = parent.FindControl("grv_Item") as GridView;
        cbl.AutoPostBack = false;
        if (cbl.Visible)
        {
            cbl.Visible = false;
        }
        else
        {
            cbl.Visible = true;
            DataBindAll(cbl, grv_Item);
        }
        cbl.AutoPostBack = true;
    }
    //protected void lbtn_chose1_OnClick(object sender, EventArgs e)
    //{
    //    flag = 1;
    //    DataBindAll(flag);
    //    ReportSelectQuery();
    //}
    //protected void lbtn_chose2_OnClick(object sender, EventArgs e)
    //{
    //    flag = 2;
    //    DataBindAll(flag);
    //    ReportSelectQuery();
    //}
    //protected void lbtn_chose3_OnClick(object sender, EventArgs e)
    //{
    //    flag = 3;
    //    DataBindAll(flag);
    //    ReportSelectQuery();
    //}
    //protected void lbtn_chose4_OnClick(object sender, EventArgs e)
    //{
    //    flag = 4;
    //    DataBindAll(flag);
    //    ReportSelectQuery();
    //}
    //protected void lbtn_chose5_OnClick(object sender, EventArgs e)
    //{
    //    flag = 5;
    //    DataBindAll(flag);
    //    ReportSelectQuery();
    //}
   
   
    #region 文本

    //protected void txt_Item1_OnTextChanged(object sender, EventArgs e)
    //{
    //    txt_Item_OnTextChanged(1);
    //    flag = 1;
    //}
    //protected void txt_Item2_OnTextChanged(object sender, EventArgs e)
    //{
    //    txt_Item_OnTextChanged(2);
    //    flag = 2;
    //}
    //protected void txt_Item3_OnTextChanged(object sender, EventArgs e)
    //{
    //    txt_Item_OnTextChanged(3);
    //    flag = 3;
    //}
    //protected void txt_Item4_OnTextChanged(object sender, EventArgs e)
    //{
    //    txt_Item_OnTextChanged(4);
    //    flag = 4;
    //}
    //protected void txt_Item5_OnTextChanged(object sender, EventArgs e)
    //{
    //    txt_Item_OnTextChanged(5);
    //    flag = 5;
    //}
   

    #endregion
  

    
  

 
    //  //绑定CheckBoxList的方法     
    //private void BindCheckBoxListItem(RadioButtonList cbl, string ItemID)
    //{
    //    DataSet ds = new MyDataOp("select CASE WHEN area IS NULL  THEN t_M_AIStandard.Standard ELSE t_M_AIStandard.Standard + ':' + area END AS Standard,t_M_AIStandard.id from t_M_AIStandard inner join t_M_AnalysisItemEx on t_M_AIStandard.AIID=t_M_AnalysisItemEx.id where t_M_AnalysisItemEx.id='" + ItemID + "'").CreateDataSet();
    //    //这里的方法根据你自己的取数据的方法      
    //    cbl.DataSource = ds;
    //    cbl.DataValueField = "id";
    //    cbl.DataTextField = "Standard";
    //    cbl.DataBind();
    //    cbl.SelectedIndex = 0;
    //}
    ////绑定分析人及方法    
    //private void BindCheckBoxListUser(RadioButtonList cbl, string ItemID)
    //{
    //    //DataSet ds = new MyDataOp("select CASE WHEN Name IS NULL  THEN Name+'('+t_M_AIStandard.Standard+')' ELSE t_M_AIStandard.Standard END AS Standard,t_M_AIStandard.id,t_MoniterUser.role,t_MoniterUser.userid from t_M_AIStandard left join t_MoniterUser  on itemid=AIID and t_M_AIStandard.id=t_MoniterUser.method  inner join t_R_UserInfo on t_R_UserInfo.UserID= t_MoniterUser.userid where t_MoniterUser.itemid='" + ItemID + "' and ifin=0").CreateDataSet();
    //    DataSet ds = new MyDataOp("select CASE WHEN Name IS not NULL  THEN Name+'('+t_M_AIStandard.Standard+')' ELSE Standard  END AS Standard,t_M_AIStandard.id,t_MoniterUser.role,t_MoniterUser.userid,[name] from t_M_AIStandard inner join t_MoniterUser  on itemid=AIID and t_M_AIStandard.id=t_MoniterUser.method  inner join t_R_UserInfo on t_R_UserInfo.UserID= t_MoniterUser.userid and ifin=0 where t_M_AIStandard.AIID='"+ItemID+"'" ).CreateDataSet();
    //    //这里的方法根据你自己的取数据的方法 
    //    DataRow[] dra = ds.Tables[0].Select("role='A'");
    //    DataSet tem=ds.Clone();
    //    if (dra.Length > 0)
    //    {
           
    //        DataRow[] drb = ds.Tables[0].Select("role='B'");
    //        if (drb.Length > 0)
    //        {
    //            foreach (DataRow dr in drb)
    //            {
    //                ds.Tables[0].Rows.Remove(dr);
    //                ds.Tables[0].AcceptChanges();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        DataRow[] drb = ds.Tables[0].Select("role='B'");
    //        if (drb.Length == 0)
    //        {
    //            DataSet dstemp = new MyDataOp("select  Standard,t_M_AIStandard.id from t_M_AIStandard where t_M_AIStandard.AIID='" + ItemID + "'").CreateDataSet();

    //            foreach (DataRow dr in dstemp.Tables[0].Rows)
    //            {
    //                DataRow dradd =ds.Tables[0].NewRow();
    //                dradd["Standard"] = dr["Standard"].ToString();
    //                dradd["id"] = dr["id"].ToString();
    //                ds.Tables[0].Rows.Add(dradd);
    //                ds.Tables[0].AcceptChanges();
    //            }

    //        }
           
 
    //    }
    //    cbl.DataSource = ds;
    //    cbl.DataValueField = "id";
    //    cbl.DataTextField = "Standard";
    //    cbl.DataBind();
    //    cbl.SelectedIndex = 0;
    //}
    
    
    #endregion

    #region new

    protected string getmethod(int ItemID)
    {

        string method = "";
        DataSet ds = new MyDataOp("select id from t_M_AIStandard where t_M_AIStandard.AIID='" + ItemID + "'").CreateDataSet();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
            method = ds.Tables[0].Rows[0][0].ToString();
        return method;

    }
    protected void btn_SampleSave_Click(object sender, EventArgs e)
    {
        string strFlag = VerifySample();//需要添加检查样品编号的有效性
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

            return;
        }
        else
        {
            string typename = "0";
            string compay = "0";
            string cyman = "";
            //获取样品类型编码
            DataSet myDR1 = new MyDataOp("select ClassID TypeID from t_M_AnalysisMainClassEx where ClassName='" + DropList_SampleType.Text.Trim() + "'").CreateDataSet();
            if (myDR1.Tables[0].Rows.Count > 0)
            {
                typename = myDR1.Tables[0].Rows[0]["TypeID"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该样品类型，请核实！');", true);
                return;
            }
            //获取样品来源编码
            DataSet myDR2 = new MyDataOp("select id from t_委托单位 where 单位全称='" + txt_SampleSource.Text.Trim() + "'").CreateDataSet();
            if (myDR2.Tables[0].Rows.Count > 0)
            {
                compay = myDR2.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该企业，请核实！');", true);
                return;
            }
            //获取采样人，现场分析人
            DataSet myDR3 = new MyDataOp("select UserID from t_R_UserInfo where Name='" + tbSampleMan.Text.Trim() + "'").CreateDataSet();
            if (myDR3.Tables[0].Rows.Count > 0)
            {
                cyman = myDR3.Tables[0].Rows[0]["UserID"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该采样人/现场分析人，请核实！');", true);
                return;
            }
            #region 保存纪录
            string ReportID = "";
            ReportID = strReportId;
            List<Entity.Sample> entityList = new List<Entity.Sample>();
            DAl.Sample Dalobj = new DAl.Sample();

            #region   //初始化样品信息


            for (int i = 0; i < 5; i++)
            {
                TextBox txt_SampleID = RepeaterSample.Items[i].FindControl("txt_SampleID") as TextBox;
                if (txt_SampleID.Text != "")
                {
                    #region 样品信息
                    Entity.Sample entity = new Entity.Sample();
                    if (strSelectedId != "0" && strSelectedId != "")//编辑信息则初始化记录id
                        entity.ID = int.Parse(strSelectedId);
                    else
                        entity.ID = 0;

                    entity.SampleID = txt_SampleID.Text;//样品ID
                    entity.ReportID = int.Parse(ReportID);//任务ID
                    entity.TypeID = int.Parse(typename);//样品类型
                    entity.SampleSource = compay;//样品来源
                    entity.SampleDate = DateTime.Parse(txt_SampleTime.Text.Trim());//采样时间
                    entity.AccessDate = DateTime.Parse(txt_AccessSampleTime.Text.Trim());//接样时间/现场检测时间
                    entity.CreateDate = DateTime.Now;//记录创建时间
                    entity.UserID = Request.Cookies["Cookies"].Values["u_id"].ToString();//记录创建人
                    entity.TestMan = cyman;//采样人/现场检测人
                    entity.datastatus = 0;//样品数据状态，待定
                    entity.StatusID =1;//样品单登记状态
                    entity.ypstatus = 0;//样品状态
                    CheckBox ck_xcflag = RepeaterSample.Items[i].FindControl("ck_xcflag") as CheckBox;//是否现场检测
                    if (ck_xcflag.Checked)
                        entity.xcflag = "1";
                    else
                        entity.xcflag = "0";
                    TextBox txt_SampleAddress = RepeaterSample.Items[i].FindControl("txt_SampleAddress") as TextBox;//采样点
                    entity.SampleAddress = txt_SampleAddress.Text.Trim();
                    TextBox txt_xz = RepeaterSample.Items[i].FindControl("txt_xz") as TextBox;//样品性质
                    entity.SampleProperty = txt_xz.Text.Trim();
                    TextBox txt_num = RepeaterSample.Items[i].FindControl("txt_num") as TextBox;//样品数量
                    entity.num = 1;
                    if (txt_num.Text.Trim() != "")
                        try
                        {
                            entity.num = int.Parse(txt_num.Text.Trim());
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品数量输入有误！');", true);
                            return;
                        }
                    if (entity.num <= 0)
                        entity.num = 1;

                    #region 初始化分析项目信息

                    GridView grv_Item = RepeaterSample.Items[i].FindControl("grv_Item") as GridView;
                    if (grv_Item.Rows.Count > 0)
                    {
                        for (int j = 0; j < grv_Item.Rows.Count; j++)
                        {
                            Entity.SampleItem temp = new Entity.SampleItem();
                            if (grv_Item.Rows[j].Cells[1].Text.Trim() != "" && grv_Item.Rows[j].Cells[1].Text.Trim() != "&nbsp;")
                                try
                                {
                                    temp.ID = int.Parse(grv_Item.Rows[j].Cells[1].Text.Trim());//分析项目记录ID
                                }
                                catch
                                { temp.ID = 0; }
                            temp.CreateDate = entity.CreateDate;//创建时间
                            temp.UserID = entity.UserID;//创建人;
                            temp.MonitorItem = grv_Item.Rows[j].Cells[3].Text.Trim();
                            temp.MonitorID = int.Parse(grv_Item.Rows[j].Cells[4].Text.Trim());
                            temp.SampleID = entity.SampleID;

                            CheckBox cb_flag = grv_Item.Rows[j].FindControl("cb_flag") as CheckBox;//是否现场检测
                            if (cb_flag.Checked)
                            {
                                temp.ckflag = 1;
                                TextBox txt_itemvalue = grv_Item.Rows[j].FindControl("itemvalue") as TextBox;//分析值
                                temp.Value = txt_itemvalue.Text.Trim();
                                temp.statusID = 3;//分析单状态待交接
                                temp.AnalysisDate = entity.AccessDate;//现场分析时间
                                temp.AnalysisUserID = entity.TestMan;//现场分析人
                               // temp.Lydate = entity.AccessDate;//领用时间
                                temp.LyUser = entity.UserID;//领用人
                                temp.AnalysisUserID = entity.TestMan;//现场分析人
                            }
                            else
                            {
                                //非现场分析
                                temp.ckflag = 0;
                                //获取分工信息
                                List<string[]> outlist = new List<string[]>();
                                outlist = getuserid(temp.MonitorID);
                                //有指派
                                if (outlist.Count == 1)
                                {
                                    string[] str = outlist[0];

                                    //指定人员分工,保存指定记录表信息
                                    temp.zpcreateuser = entity.UserID;
                                    temp.zpto = str[0];
                                    temp.zpdate = entity.CreateDate;
                                    temp.statusID = 1;
                                }
                                //无指派
                                else
                                {
                                    temp.zpto = "";
                                    temp.zpcreateuser = "";
                                    temp.statusID = 0;

                                }
                            }

                            TextBox txt_ItemRemark = grv_Item.Rows[j].FindControl("txt_ItemRemark") as TextBox;//备注信息
                            temp.Remark = "";
                            if (txt_ItemRemark.Text.Trim() != "")
                                temp.Remark = txt_ItemRemark.Text.Trim();

                            entity.ItemValueList += temp.MonitorID + ";";
                            entity.ItemList += temp.MonitorItem + ";";
                            entity.SampleItemList.Add(temp);
                    #endregion
                    #endregion

                        }
                        entityList.Add(entity);
                    }
                }
            }
            //判断样品是否添加
            //TBD
            //数据入库
            int ret = 0;
            if (lbl_SampleDo.Text.Trim() == "添加样品")
            {
                if (entityList.Count > 0)
                {
                    ret = Dalobj.AddSample(entityList);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请添加样品！！');", true);
                }
            }
            //else
            //    ret = Dalobj.AddSample(entityList);

            if (ret == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号重复!');", true);
            }
            if (ret == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据保存成功!');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);

            #endregion


            #endregion
            ReportSelectQuery();
        }
    }
    protected void ck_xcflag_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ck_xcflag = sender as CheckBox;
    RepeaterItem parent=   ck_xcflag.Parent as RepeaterItem;
    GridView grv_Item = parent.FindControl("grv_Item") as GridView;

        if (ck_xcflag.Checked)
        {
            grv_Item.Visible = true;
        }
        else
            grv_Item.Visible = false;
       
    }
    protected void btn_CancelSample_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();", true);
        ReportSelectQuery();
    }
    private string VerifySample()
    {
        string strErrorInfo = "";


        if (DropList_SampleType.Text == "")
            strErrorInfo += "样品类型不能为空！\\n";
        else if (txt_SampleTime.Text == "")
        {
            strErrorInfo += "请输入接样时间！\\n";


        }
        if (tbSampleMan.Text.Trim() == "")
        {
            strErrorInfo += "采样人/现场分析人不能为空！";


        }
        if (txt_AccessSampleTime.Text != "")
        {

            try
            {
                DateTime testtime = DateTime.Parse(txt_AccessSampleTime.Text.Trim());

            }
            catch
            {
                strErrorInfo += "采样时间/现场分析时间填写有误!";


            }
        }
        else
        {
            strErrorInfo += "采样时间/现场分析时间不能为空";


        }
        return strErrorInfo;
    }
   //获取在岗负责人员信息
    protected List<string[]> getuserid(int ItemID)
    {
        List<string[]> outlist = new List<string[]>();
        string[] usermethod = new string[2];
        DataSet ds = new MyDataOp("select t_MoniterUser.role,t_MoniterUser.userid,[name] from  t_MoniterUser inner join t_R_UserInfo on t_R_UserInfo.UserID= t_MoniterUser.userid  where t_MoniterUser.itemid='" + ItemID + "' and t_R_UserInfo.UserID not in (select userid from  t_R_OUTUserLog where convert(varchar(10),dtime,120)=convert(varchar(10),getdate(),120))").CreateDataSet();

        DataSet checkds = new MyDataOp("SELECT     userid FROM t_R_OUTUserLog WHERE CONVERT(varchar(10), dtime, 120) = CONVERT(varchar(10), GETDATE(), 120)").CreateDataSet();
     
           //这里的方法根据你自己的取数据的方法 
        DataRow[] dra = ds.Tables[0].Select("role='A'");
        DataSet tem = ds.Clone();
        if (dra.Length > 0)
        {
            bool flag = false;
            foreach (DataRow drr in checkds.Tables[0].Rows)
            {
                string b=dra[0]["userid"].ToString().Trim()+",";
              if( drr["userid"].ToString().LastIndexOf(b)>-1)
              {
                  flag = true; break;
              }
            }
            if (!flag)
            {
                DataRow[] drb = ds.Tables[0].Select("role='B'");
                if (drb.Length > 0)
                {
                    foreach (DataRow dr in drb)
                    {
                        ds.Tables[0].Rows.Remove(dr);
                        ds.Tables[0].AcceptChanges();
                    }
                }
            }
            else
            {
                foreach (DataRow dr in dra)
                {
                    ds.Tables[0].Rows.Remove(dr);
                    ds.Tables[0].AcceptChanges();
                }
            }
        }
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            usermethod[0] = dr["userid"].ToString();
            //usermethod[1] = dr["method"].ToString();
            outlist.Add(usermethod);
        }
        return outlist;

    }
    protected void DataBindAll(CheckBoxList cb_analysisList,GridView grv)
    {
        if (DropList_SampleType.Text.Trim().ToString().Trim() != "")
        {
            string str = "select  t_M_AnalysisItemEx.id,AIName from t_M_AnalysisItemEx,t_M_AnalysisMainClassEx where t_M_AnalysisItemEx.ClassID=t_M_AnalysisMainClassEx.ClassID and t_M_AnalysisMainClassEx.ClassName='" + DropList_SampleType.Text.Trim().ToString() + "'";
            //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
            if (ds.Tables[0].Rows.Count < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('没有该样品类型的分析项目，请先在分析项目管理中添加');", true);

                return;
            }
            else
            {
                for (int i = 0; i < cb_analysisList.Items.Count; i++)
                {
                    foreach (GridViewRow gvr in grv.Rows)
                    {
                        if (gvr.Cells[4].Text.Trim() == cb_analysisList.Items[i].Value.ToString().Trim())
                        {
                            cb_analysisList.Items[i].Selected = true;
                        }  
                    }
                } 
            }
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请先选择样品类型');", true);

        return;

    }
   
  protected void txt_Item_OnTextChanged(object sender, EventArgs e)
    {
        string s = DropList_SampleType.Text.Trim();
        TextBox txt_Item =sender as  TextBox;
        RepeaterItem parent = txt_Item.Parent as RepeaterItem;
       string[] list = txt_Item.Text.Trim().Split(',');
       CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
       DataTable tempds = dt_analysis.Clone();
       string valustr = "";
       string itemstr = "";
        foreach (string tem in list)
        {
            if (tem.Trim() != "")
            {
                DataRow[] dr = tempds.Select("分析项目='" + tem.Trim() + "'");
                if (dr.Length>0)
                {
                    if (!valustr.Contains(dr[0]["itemid"].ToString().Trim() + ";"))
                    {
                        valustr += dr[0]["itemid"].ToString().Trim()
                            + ";";
                        itemstr += tem.Trim() + ";";
                    }
                }
                else
                {
                    string code = getitemid(tem.Trim());
                    if (code != "")
                    {
                        DataRow drnew = tempds.NewRow();
                        drnew["itemid"] = code;
                        drnew["分析项目"] = tem.Trim();
                        tempds.Rows.Add(drnew);
                        valustr += code + ";";
                        itemstr += tem.Trim() + ";";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统中不存在该分析项目，请确认！');", true);

                        return;
                    }

                }
            }

        }
        GridView grv_Item = parent.FindControl("grv_Item") as GridView;
        grv_Item.DataSource = tempds;
        grv_Item.Visible = ck_xcflag.Checked;
        grv_Item.DataBind();

    }
  protected void Change(RepeaterItem parent)
  {
      CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
      GridView grv_Item = parent.FindControl("grv_Item") as GridView;
      TextBox txt_Item = parent.FindControl("txt_Item") as TextBox;
      HiddenField hid_Item = parent.FindControl("hid_Item") as HiddenField;
      DataTable tempds;
      CheckBoxList cb_analysisList = parent.FindControl("cb_analysisList") as CheckBoxList;
      txt_Item.Text = null;
      hid_Item.Value = null;
      tempds = dt_analysis.Clone();
      foreach (ListItem LI in cb_analysisList.Items)
      {
          if (LI.Selected)
          {
              DataRow dr = tempds.NewRow();

              dr["分析项目"] = LI.Text;
              dr["ItemID"] = LI.Value;
              tempds.Rows.Add(dr);
              txt_Item.Text += LI.Text.Trim() + ";";
              hid_Item.Value += LI.Value.Trim() + ";";
          }
      }
      grv_Item.DataSource = tempds;
      grv_Item.DataBind();
      grv_Item.Visible = ck_xcflag.Checked;
     // ReportSelectQuery();
  }
    private string getitemid(string item)
    {
        string code = "";
        if (item != "")
        {
            string sql = "select id from t_M_AnalysisItemEx where AIName='" + item + "'";
            DataSet ds = new MyDataOp(sql).CreateDataSet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                code = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        return code;
    }
    #region 分析项目编辑

    protected void grv_Item_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Width = 20;
            e.Row.Cells[2].Width = 150;
            TableCell headerset0 = new TableCell();
            headerset0.Text = "现场检测";
            headerset0.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset0.Width = 60;
            e.Row.Cells.Add(headerset0);
            TableCell headerset1 = new TableCell();
            headerset1.Text = "分析值";
            headerset1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset1.Width = 60;

            e.Row.Cells.Add(headerset1);

            // TableCell headerset11 = new TableCell();
            // headerset11.Text = "分析方法";
            // headerset11.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            // headerset11.Width = 400;
            TableCell headerRemark = new TableCell();
            headerRemark.Text = "备注";
            headerRemark.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerRemark.Width = 100;
            e.Row.Cells.Add(headerRemark);
           


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();


            //是否现场检测
            TableCell tabcDetail0 = new TableCell();
            tabcDetail0.Width = 60;
            tabcDetail0.Style.Add("text-align", "center");
            CheckBox cb_flag = new CheckBox();
            cb_flag.ID = "cb_flag";
            cb_flag.Text = "是";
            tabcDetail0.Controls.Add(cb_flag);
            e.Row.Cells.Add(tabcDetail0);

            //分析值
            TableCell tabcDetail1 = new TableCell();
            tabcDetail1.Width = 60;
            tabcDetail1.Style.Add("text-align", "center");
            TextBox itemvalue = new TextBox();
            itemvalue.ID = "itemvalue";
            itemvalue.Width = 75;
            tabcDetail1.Controls.Add(itemvalue);
            e.Row.Cells.Add(tabcDetail1);

            ////分析方法
            //TableCell tabcDetail11 = new TableCell();
            //tabcDetail11.Width =400;
            //tabcDetail11.Style.Add("text-align", "center");
            //RadioButtonList rbl = new RadioButtonList();
            //rbl.ID = "rbl";
            //rbl.Width = 400;
            //tabcDetail11.Controls.Add(rbl);
            //e.Row.Cells.Add(tabcDetail11);

            //备注
            TableCell tab_remark = new TableCell();

            tab_remark.Style.Add("text-align", "center");
            TextBox txt_ItemRemark = new TextBox();
            txt_ItemRemark.ID = "txt_ItemRemark";
            txt_ItemRemark.Width = 200;
            tab_remark.Controls.Add(txt_ItemRemark);
            e.Row.Cells.Add(tab_remark);

            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/images/Delete.gif";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");

            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            //{
            //    tabcDel.Controls.Add(ibtnDel);
            //    e.Row.Cells.Add(tabcDel);

            //}
            //else
            //    ibtnDel.Enabled = false;



        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
    }
    
    
    protected void grv_Item_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView item_grv = sender as GridView;
        string grvid = item_grv.ID;
        //flag = int.Parse(grvid.Substring(grvid.Length - 1, 1));
        string itemid =e.Row.Cells[4].Text.Trim();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txt_value = (TextBox)e.Row.FindControl("itemvalue");
            if (e.Row.Cells[5].Text.Trim() != "&nbsp;")
            {
                txt_value.Text = e.Row.Cells[5].Text.Trim();
            }
           
            CheckBox cb_xcfx = (CheckBox)e.Row.FindControl("cb_flag");
                if (e.Row.Cells[2].Text.Trim() == "1")
                    cb_xcfx.Checked = true;
                else
                    cb_xcfx.Checked = false;

                TextBox txt_ItemRemark = (TextBox)e.Row.FindControl("txt_ItemRemark");
                if (e.Row.Cells[7].Text.Trim() != "&nbsp;")
                txt_ItemRemark.Text = e.Row.Cells[7].Text.Trim();
            //RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("rbl");
            //if (rbl != null)
            //{
                
            //    BindCheckBoxListUser(rbl, e.Row.Cells[3].Text.Trim());
            //     if (e.Row.Cells[5].Text.Trim() != "&nbsp;")
            //     {
            //         rbl.SelectedValue = e.Row.Cells[5].Text.Trim();
            //        }
               
            //}
        }
    }
    
    #endregion
    #endregion



    protected void cb_analysisList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string s = DropList_SampleType.Text.Trim();
        CheckBoxList cb_analysisList = sender as CheckBoxList;
        RepeaterItem parent = cb_analysisList.Parent as RepeaterItem;
        Change(parent);

    }

    
}
