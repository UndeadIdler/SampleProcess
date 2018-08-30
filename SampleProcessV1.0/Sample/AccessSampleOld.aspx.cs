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

public partial class AccessSampleOld : System.Web.UI.Page
{
    private string strSelectedId//样品单号
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }

    private DataTable dt_analysis//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis"]; }
        set { ViewState["dt_analysis"] = value; }
    }
    private DataTable dt_analysis1//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis1"]; }
        set { ViewState["dt_analysis1"] = value; }
    }
    private DataTable dt_analysis2//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis2"]; }
        set { ViewState["dt_analysis2"] = value; }
    }
    private DataTable dt_analysis3//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis3"]; }
        set { ViewState["dt_analysis3"] = value; }
    }
    private DataTable dt_analysis4//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis4"]; }
        set { ViewState["dt_analysis4"] = value; }
    }
    private DataTable dt_analysis5//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis5"]; }
        set { ViewState["dt_analysis5"] = value; }
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
            btn_Add.Enabled = false;
            btn_OK.Enabled = false;
            btn_AddSample.Enabled = false;
            btn_Save.Enabled = false;
            btn_OKSample.Enabled = false;
            btn_item.Enabled = false;
            //for (int i = 0; i < grdvw_List.Rows.Count; i++)
            //{
            //  ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
            //    if(btn!=null)
            //  btn.Visible = false;

            //}
        }
        else
        {
            btn_Add.Enabled = true;
            btn_OK.Enabled = true;
            btn_Save.Enabled = true;
            btn_AddSample.Enabled = true;
            btn_OKSample.Enabled = true;
            btn_item.Enabled = true;
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
        string strSql = "select t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,ClientID,ClientName 区域,urgent 备注,Ulevel,wtdepart 委托单位,Projectname 项目名称 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=0) and wetherscance=0 order by t_M_ReporInfo.id";

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
        if (grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim() != "")
        {
            txt_Projectname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim();
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();showAnalysis();", true);
        //lbl_Type.Text = "样品列表";
        btn_Save.Visible = true;
       
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

            //TableCell headerset = new TableCell();
            //headerset.Text = "样品列表";
            //headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerset.Width = 60;
            //e.Row.Cells.Add(headerset);
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1)=="1")
            {
                TableCell headerDel = new TableCell();
                headerDel.Text = "删除";
                headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                headerDel.Width = 30;
                e.Row.Cells.Add(headerDel);
            }
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

            //TableCell MenuSet = new TableCell();
            //MenuSet.Width = 60;
            //MenuSet.Style.Add("text-align", "center");
            //ImageButton btMenuSet = new ImageButton();
            //btMenuSet.ImageUrl = "~/images/Detail.gif";
            //btMenuSet.CommandName = "Select";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuSet.Controls.Add(btMenuSet);
            //e.Row.Cells.Add(MenuSet);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.ID = "btn_delete";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            {
                tabcDel.Controls.Add(ibtnDel);
                e.Row.Cells.Add(tabcDel);
               
            }
            else
            {
                ibtnDel.Visible = false;
            }
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[9].Visible = false;
            

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

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        string strFlag = Verify();

        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
            return;
        }
        else
        {
            string checkstr = "select count(*) from t_M_SampleInfor where ReportID='" + strReportId + "'";
            DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
            int count=0;
            try{
                count=int.Parse(dscheck.Tables[0].Rows[0][0].ToString());
            }
            catch{}
            if (count != 0)
            {
                string clientname = DropList_client.SelectedValue.ToString();
                string strSqltmp = "";
                strSqltmp = @"update t_M_ReporInfo set ReportName='" + txt_ReportID.Text.Trim() +
                                                "',ItemType='" + drop_ItemList.SelectedValue.ToString() +
                                                 "',ClientID='" + clientname +
                                                 "',urgent='" + drop_urgent.Text +
                                                 "',Ulevel='" + drop_level.SelectedValue.ToString() +
                                                  "',wtdepart='" + txt_wtdepart.Text.Trim() +
                                                  "',CreateDate='" + txt_CreateDate.Text +
    "',Projectname='" + txt_Projectname.Text +
                                        "',StatusID=1 where id='" + strReportId + "'";

                MyDataOp mdo = new MyDataOp(strSqltmp);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    WebApp.Components.Log.SaveLog("创建样品原单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);


                }
                else
                {
                    WebApp.Components.Log.SaveLog("创建样品原单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添保存失败！')", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", ";alert('未添加样品无法提交')", true);
            ReportSelectQuery();
        }
        }
        Query();
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
        string strSample = "";
        string strDate = "";
        if (txt_samplequery.Text != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text != "")
            strDate = " and (year(CreateDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(CreateDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(CreateDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,ClientID,ClientName 区域,urgent 备注,Ulevel,wtdepart 委托单位,Projectname 项目名称 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=0) and wetherscance=0 " + strSample + strDate + " order by t_M_ReporInfo.id";

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
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        txt_ReportID.Text = "";
        Query();
    }



    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        btn_OK.Text = "确定";
        txt_ReportID.Text = "";
        MyStaVoid.BindList("ClientName", "id", "select * from t_M_ClientInfo order by id", DropList_client);
        ListItem choose = new ListItem("请选择", "-1");
        DropList_client.Items.Add(choose);
        DropList_client.Items.FindByValue("-1").Selected = true;

        strReportId = "0";
        strReportName = "";
        txt_CreateDate.Text = "";
        txt_Projectname.Text = "";
        txt_wtdepart.Text = "";

        MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);
       
        drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue("-1").Selected = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();hiddenDetailAnalysis();hiddenDetailAnalysis();hiddenDetailAnalysisAdd();unshowchose()", true);
        drop_urgent.Text = "";
        btn_Save.Visible = false;
       
        Query();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();

        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);
            // return;


        }
        else
        {
            #region 添加新纪录
            if (strReportId == "" || strReportId=="0")
            {
                string clientname = DropList_client.SelectedValue.ToString();
                string strSqltmp = "";

                strSqltmp = @"insert into t_M_ReporInfo
                    (CreateDate,ItemType,UserID,ClientID,ReportName,urgent,Ulevel,wtdepart,Projectname,StatusID,chargeman)  
                    values('" + txt_CreateDate.Text.Trim() + "','" + drop_ItemList.SelectedValue.ToString() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "','" + clientname + "','" + txt_ReportID.Text.Trim() + "','" + drop_urgent.Text + "','" + drop_level.SelectedValue.ToString() + "','" + txt_wtdepart.Text.Trim() + "','" + txt_Projectname.Text.Trim() + "',0,'" + txt_xmfzr.Text.Trim() + "')";
               
                MyDataOp mdo = new MyDataOp(strSqltmp);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    string getreportid = "select id from t_M_ReporInfo where ItemType='" + drop_ItemList.SelectedValue.ToString() + "' and ReportName='" + txt_ReportID.Text.Trim() + "' and Projectname='" + txt_Projectname.Text.Trim() + "' and StatusID=0 order by id desc";
                    DataSet dsgetreportid = new MyDataOp(getreportid).CreateDataSet();
                    if (dsgetreportid.Tables[0].Rows.Count >= 1)
                    {
                        strReportId = dsgetreportid.Tables[0].Rows[0][0].ToString();
                    }
                    ReportSelectQuery();
                    btn_Save.Visible = true;
                    WebApp.Components.Log.SaveLog("创建任务单添加成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAnalysis();", true);

                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据添加成功！')", true);


                }
                else
                {
                    WebApp.Components.Log.SaveLog("创建任务单添加失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('数据添加失败！')", true);

                }

            }
            #endregion
            else 
            {
                string clientname = DropList_client.SelectedValue.ToString();
                string strSqltmp = "";
                strSqltmp = @"update t_M_ReporInfo set ReportName='" + txt_ReportID.Text.Trim() +
                                                "',ItemType='" + drop_ItemList.SelectedValue.ToString() +
                                                 "',ClientID='" + clientname +
                                                 "',urgent='" + drop_urgent.Text +
                                                 "',Ulevel='" + drop_level.SelectedValue.ToString() +
                                                  "',wtdepart='" + txt_wtdepart.Text.Trim() +
                                                  "',CreateDate='" + txt_CreateDate.Text +
"',Projectname='" + txt_Projectname.Text +
                                        "',StatusID=0,chargeman='"+txt_xmfzr.Text.Trim()+"' where id='" + strReportId + "'";

                MyDataOp mdo = new MyDataOp(strSqltmp);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    WebApp.Components.Log.SaveLog("创建任务单编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);


                }
                else
                {
                    WebApp.Components.Log.SaveLog("创建任务单编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添保存失败！')", true);

                }

            }
          
        }
        Query();
    }
    private string Verify()
    {
        string strErrorInfo = "";

        if (drop_ItemList.SelectedValue == "-1")
            strErrorInfo += "请选择项目类型！\\n";
        else if (txt_CreateDate.Text == "")
            strErrorInfo += "请填写时间！\\n";

        else if (txt_Projectname.Text.Trim() == "-1")
            strErrorInfo += "请选择项目名称！\\n";
        else if (txt_wtdepart.Text.Trim() == "-1")
            strErrorInfo += "请选择委托单位！\\n";
        else if (DropList_client.SelectedValue == "-1")
            strErrorInfo += "请选择区域！\\n";
        return strErrorInfo;
    }
   
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
       
    }
    private void newDt()
    { 
        dt_analysis=null;
        dt_analysis=new DataTable();
        DataColumn dc0 = new DataColumn("现场分析");
        DataColumn dc1 = new DataColumn("分析项目");
        DataColumn dc2 = new DataColumn("ItemID");
        DataColumn dc3 = new DataColumn("分析值");
        DataColumn dc70 = new DataColumn("分析方法");
            dt_analysis.Columns.Add((dc0));
            dt_analysis.Columns.Add((dc1));
            dt_analysis.Columns.Add((dc2));
            dt_analysis.Columns.Add((dc3));
            dt_analysis.Columns.Add((dc70)); 
            dt_analysis1 = dt_analysis.Clone();
            dt_analysis2 = dt_analysis.Clone();
            dt_analysis3 = dt_analysis.Clone();
            dt_analysis4 = dt_analysis.Clone();
            dt_analysis5 = dt_analysis.Clone();
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
        for (int i = 1; i <= 5; i++)
        {
            CheckBox ck_xcflag = Panel_Sample.FindControl("ck_xcflag_" + i) as CheckBox;
            TextBox txt_SampleID = Panel_Sample.FindControl("txt_SampleID" + i) as TextBox;
            HiddenField hid_ID = Panel_Sample.FindControl("hid_ID" + i) as HiddenField;
            GridView grv_Item = Panel_Sample.FindControl("grv_Item" + i) as GridView;
            TextBox txt_SampleAddress = Panel_Sample.FindControl("txt_SampleAddress" + i) as TextBox;
            TextBox txt_xz = Panel_Sample.FindControl("txt_xz" + i) as TextBox;
            TextBox txt_item = Panel_Sample.FindControl("txt_item" + i) as TextBox;
            HiddenField hid_Item = Panel_Sample.FindControl("hid_Item" + i) as HiddenField;
            txt_item.Text = "";
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
        txt_SampleID1.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        if (CheckSample(txt_SampleID1.Text))
       {

           
           txt_Item1.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[16].Text.Trim();
           hid_Item1.Value = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[17].Text.Trim();
           DropList_SampleType.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[8].Text.Trim();
           strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
           txt_SampleSource.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
          
           txt_SampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[3].Text.Trim();
           txt_SampleAddress1.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
           txt_AccessSampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[6].Text.Trim();
           tbSampleMan.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[15].Text.Trim();
           //samplestatus = int.Parse(grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[6].Text.Trim());
           txt_xz1.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[9].Text.Trim();
           txt_num1.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[14].Text.Trim();
           DAl.Sample sampleobj = new DAl.Sample();
           
            //获取样品单监测项信息
            GridView grv_Item = Panel_Sample.FindControl("grv_Item1") as GridView;
            DataTable dttemp = dt_analysis.Clone();
            DAl.Sample temp = new DAl.Sample();
            dttemp = temp.GetSampleDetail(txt_SampleID1.Text);
            if (dttemp!=null&&dttemp.Rows.Count > 0)
            {
                grv_Item.DataSource = dttemp;
                grv_Item.DataBind();
                dt_analysis1 = dttemp.Copy();
            }
            else
            {
                grv_Item.DataSource = null;
                grv_Item.DataBind();
            }
            if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[13].Text.Trim() == "1")
            {
                ck_xcflag_1.Checked = true;
                grv_Item.Visible = true;
            }
            else
            {
                ck_xcflag_1.Checked = false;
                grv_Item.Visible = false;
            }
           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);
           ReportSelectQuery();
           }
           else
           {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('交接完成的样品单不允许修改');", true);
           }
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
            TableCell headerDel = new TableCell();
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            {
                headerDel.Text = "删除";
                headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                headerDel.Width = 30;
                e.Row.Cells.Add(headerDel);

            }
           
           
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
            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            {
                tabcDel.Controls.Add(ibtnDel);
                e.Row.Cells.Add(tabcDel);
               
            }
            else
                ibtnDel.Enabled = false;
           


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
        string strSql = "SELECT  t_M_SampleInfor.id,t_M_SampleInfor.SampleSource 样品来源,t_M_SampleInfor.SampleDate AS 采样时间,t_M_SampleInfor.SampleAddress 采样点 ,t_M_SampleInfor.SampleID AS 样品编号," +
      "t_M_SampleInfor.AccessDate AS 接样时间, " +
      " t_M_SampleInfor.TypeID, t_M_SampleType.SampleType AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, " +
     " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID,t_M_SampleInfor.StatusID,t_M_SampleInfor.xcflag,t_M_SampleInfor.num 数量,cyman" +
" FROM  t_M_SampleInfor  INNER JOIN" +
     " t_M_SampleType ON " +
    "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID "+

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
                   dr["分析项目"] += drr[0].ToString() + ",";
                   dr["分析项目编码"] += drr[1].ToString() + ",";
                }
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
        ds.Dispose();
        Query();
       
    }
    protected List<string []>  getuserid(int ItemID)
    {
        List<string[]> outlist = new List<string[]>();
        string[] usermethod = new string[2];
        DataSet ds = new MyDataOp("select CASE WHEN Name IS not NULL  THEN Name+'('+t_M_AIStandard.Standard+')' ELSE Standard  END AS Standard,t_M_AIStandard.id,t_MoniterUser.role,t_MoniterUser.userid,[name],method from t_M_AIStandard inner join t_MoniterUser  on itemid=AIID and t_M_AIStandard.id=t_MoniterUser.method  inner join t_R_UserInfo on t_R_UserInfo.UserID= t_MoniterUser.userid and ifin=0 where t_M_AIStandard.AIID='" + ItemID + "'").CreateDataSet();
        //这里的方法根据你自己的取数据的方法 
        DataRow[] dra = ds.Tables[0].Select("role='A'");
        DataSet tem = ds.Clone();
        if (dra.Length > 0)
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
       foreach(DataRow dr in ds.Tables[0].Rows)
        {
            usermethod[0] = dr["userid"].ToString();
            usermethod[1] = dr["method"].ToString();
            outlist.Add(usermethod);
        }
       return outlist;
       
    }
    protected string getmethod(int ItemID)
    {
       
        string method ="";
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
            //获取样品类型编码
            DataSet myDR1 = new MyDataOp("select TypeID from t_M_SampleType where SampleType='" + DropList_SampleType.Text.Trim() + "'").CreateDataSet();
            if (myDR1.Tables[0].Rows.Count > 0)
            {
                typename = myDR1.Tables[0].Rows[0]["TypeID"].ToString();
            }
           
            #region 保存纪录
            string ReportID = "";
            ReportID = strReportId;
            List<Entity.Sample> entityList = new List<Entity.Sample>();
            DAl.Sample Dalobj = new DAl.Sample();
            //初始化样品信息
            for (int i = 1; i <= 5; i++)
            {
                TextBox txt_SampleID = Panel_Sample.FindControl("txt_SampleID" + i) as TextBox;
                if (txt_SampleID.Text != "")
                {
                    Entity.Sample entity = new Entity.Sample();
                    if (strSelectedId != "0" && strSelectedId!="")
                        entity.ID = int.Parse(strSelectedId);
                    else
                        entity.ID = 0;
                  
                    entity.SampleID = txt_SampleID.Text;
                    entity.ReportID = int.Parse(ReportID);
                    entity.TypeID = int.Parse(typename);
                    entity.SampleSource = txt_SampleSource.Text.Trim();
                    entity.SampleDate = DateTime.Parse(txt_SampleTime.Text.Trim());
                    entity.AccessDate = DateTime.Parse( txt_AccessSampleTime.Text.Trim());
                    entity.CreateDate = DateTime.Now;
                    entity.UserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
                    
                    entity.TestMan = tbSampleMan.Text.Trim();
                    entity.datastatus = 0;
                    entity.StatusID = 0;
                    entity.ypstatus = 0;
                    CheckBox ck_xcflag = Panel_Sample.FindControl("ck_xcflag_" + i.ToString()) as CheckBox;
                    if(ck_xcflag.Checked)
                        entity.xcflag="1";
                    else
                        entity.xcflag="0";
                    HiddenField hid_ID = Panel_Sample.FindControl("hid_ID" + i.ToString()) as HiddenField;
                    if (hid_ID.Value != "")
                        entity.ID = int.Parse(hid_ID.Value);
                    TextBox txt_SampleAddress = Panel_Sample.FindControl("txt_SampleAddress" + i) as TextBox;
                    entity.SampleAddress = txt_SampleAddress.Text.Trim();
                    TextBox txt_xz = Panel_Sample.FindControl("txt_xz" + i) as TextBox;
                    TextBox txt_num = Panel_Sample.FindControl("txt_num" + i) as TextBox;
                    entity.num = 1;
                    if (txt_num.Text.Trim()!="")
                        try
                        {
                            entity.num = int.Parse(txt_num.Text.Trim());
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品数量输入有误！');", true);
                            return;
                        }
                    if (entity.num<=0)
                        entity.num = 1;
                    entity.SampleProperty = txt_xz.Text.Trim();

                   //获取分析项目信息
                    entity.DrawList = new List<Entity.Draw>();
                    GridView grv_Item = Panel_Sample.FindControl("grv_Item" + i) as GridView;
                    if (grv_Item.Rows.Count > 0)
                    {
                        for (int j = 0; j < grv_Item.Rows.Count; j++)
                        {
                            Entity.Draw drawentity = new Entity.Draw();
                            Entity.SampleItem temp = new Entity.SampleItem();
                            if (grv_Item.Rows[j].Cells[7].Text.Trim() != "")
                                drawentity.ID = int.Parse(grv_Item.Rows[j].Cells[7].Text.Trim());
                            else
                                drawentity.ID = 0;

                            temp.CreateDate = DateTime.Now;
                            temp.AnalysisDate = temp.CreateDate;
                            temp.MonitorItem = grv_Item.Rows[j].Cells[2].Text.Trim();
                            temp.MonitorID = int.Parse(grv_Item.Rows[j].Cells[3].Text.Trim());
                            temp.SampleID = entity.SampleID;
                            drawentity.SampleID = entity.SampleID;
                            CheckBox cb_xcfx = grv_Item.Rows[j].FindControl("cb_flag") as CheckBox;
                            TextBox txt_itemvalue = grv_Item.Rows[j].FindControl("itemvalue") as TextBox;
                            TextBox txt_ItemRemark = grv_Item.Rows[j].FindControl("txt_ItemRemark") as TextBox;
                            temp.Remark = "";
                            drawentity.finishdate = DateTime.Now;
                            if (txt_ItemRemark.Text.Trim() != "")
                                temp.Remark = txt_ItemRemark.Text.Trim();
                           
                           
                            entity.ItemValueList += temp.MonitorID+",";
                            temp.UserID = entity.UserID;
                            drawentity.SampleID = temp.SampleID;
                            drawentity.ItemList += temp.MonitorItem + ",";
                            drawentity.ItemValueList += temp.MonitorID + ",";
                            drawentity.LyDate = temp.CreateDate;
                            drawentity.CreateDate = DateTime.Now;
                            drawentity.UserID = tbSampleMan.Text.Trim();
                            drawentity.fxman = drawentity.UserID;
                            temp.AnalysisUserID = drawentity.fxman;
                            if (cb_xcfx.Checked)
                            {
                                //现场分析项目
                                temp.ckflag = 1;
                                //if (hid_status.Value != "")
                                //    drawentity.status = int.Parse(hid_status.Value);
                               
                               
                                drawentity.type = 1;
                                temp.statusID = 1;
                                if (tbSampleMan.Text.Trim() == "")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('现场分析人不能为空！');", true);

                                    return;
                                }
                                if (txt_AccessSampleTime.Text != "")
                                {

                                    try
                                    {
                                        drawentity.CreateDate = DateTime.Parse(txt_AccessSampleTime.Text.Trim());
                                        drawentity.LyDate = drawentity.CreateDate;
                                        drawentity.finishdate = DateTime.Now;

                                    }
                                    catch
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('现场分析需填写有误！');", true);

                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('现场分析需填写分析时间！');", true);

                                    return;
                                }
                                if (txt_itemvalue.Text.Trim() != "")
                                {
                                    try
                                    {
                                        temp.Value =txt_itemvalue.Text.Trim();
                                    }
                                    catch
                                    { 
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('现场分析需填写分析数据！！');", true);

                                    return;
                                }
                                //TBD根据监测项获取分析方法
                                string method = getmethod(temp.MonitorID);
                                temp.Method = method;
                                //现场分析项目,样品的数据状态为已登记;样品单状态为待交接
                                entity.datastatus = 1;
                                entity.StatusID = 2;
                            }
                            else
                            {
                               
                                //非现场分析,根据员工分工表找到人员
                                temp.ckflag = 0;
                                drawentity.type =0;
                                //TBD根据分工表找到对应的分析人员与分析方法,AB角
                                List<string[]> outlist = new List<string[]>();
                                 outlist=getuserid(temp.MonitorID);
                                 if (outlist.Count == 1)
                                 {
                                     string[] str = outlist[0];
                                     temp.AnalysisUserID = str[0];
                                     temp.Method = str[1];
                                 }
                                 else
                                 {
                                     temp.AnalysisUserID = "";
                                     temp.Method = "";
                                 }
                                 temp.Value = "";
                                 //RadioButtonList rbl = grv_Item.Rows[j].FindControl("rbl") as RadioButtonList;
                                 //if (rbl.SelectedValue != null)
                                 //{
                                 //    temp.Method = rbl.SelectedValue;
                                 //}
                                 if (temp.AnalysisUserID != "")
                                 {
                                     temp.statusID = 1;//分工表中找到员工,则MonitorItem的状态为被分配
                                     drawentity.fxman = temp.AnalysisUserID;
                                     drawentity.UserID = temp.AnalysisUserID;
                                     drawentity.SampleItemList.Add(temp);
                                 }
                                 else
                                 {
                                     temp.statusID = 0;//分工表中未找到员工,则MonitorItem的状态为未分配
                                     drawentity.UserID = "";
                                     drawentity.fxman = "";
                                 }
                            }

                                
                            ////编辑,增分析单ID不为0
                            //if (hid_drawid.Value != "")
                            //    drawentity.ID = int.Parse(hid_drawid.Value);
                            //else
                            //    drawentity.ID = 0;
                           
                            //若已确定分析人员,怎生成分析单
                            if (drawentity.fxman != "")
                            {
                                entity.DrawList.Add(drawentity);
                                drawentity.SampleItemList.Add(temp);
                            }
                            else
                                entity.SampleItemList.Add(temp);
                            
                        }
                        if (entity.ItemValueList.Length > 0)
                            entity.ItemValueList = entity.ItemValueList.Substring(0, entity.ItemValueList.Length - 1);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择样品的分析项目！');", true);

                        return;
                    }

                    
                    entityList.Add(entity);
                }
            }

            //数据入库
            if (Dalobj.AddSample(entityList) > 0)
            {
                //if(int.Parse(hid_status.Value)>=2)
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据保存成功！现场检测数据已移交，无法修改！');", true);
                //else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据保存成功!');", true);
   
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
        }
               #endregion

        //}
        ReportSelectQuery();


    }
    protected void ck_xcflag_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ck_xcflag =sender as CheckBox;
        int i=int.Parse( ck_xcflag.ID.Substring(ck_xcflag.ID.Length - 1, 1));
        GridView grv_Item = Panel_Sample.FindControl("grv_Item" + i) as GridView;

        if (ck_xcflag.Checked)
        {
            grv_Item.Visible = true;
        }
        else
            grv_Item.Visible = false;
        DataTable tempds = dt_analysis.Clone();
        switch (i.ToString())
        {
            case "1": tempds = dt_analysis1.Copy(); break;
            case "2": tempds = dt_analysis2.Copy(); break;
            case "3": tempds = dt_analysis3.Copy(); break;
            case "4": tempds = dt_analysis4.Copy(); break;
            case "5": tempds = dt_analysis5.Copy(); break;
            default: tempds = dt_analysis1; break;
        }
        if (tempds.Rows.Count > 0)
        {
            grv_Item.DataSource = tempds;
            grv_Item.DataBind();
        }
        else
        {
            grv_Item.DataSource = tempds;
            grv_Item.DataBind();
        }
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
        else if (txt_SampleTime.Text=="")
        {
            strErrorInfo += "请输入接样时间！\\n";


        }
        if (tbSampleMan.Text.Trim() == "")
        {
            strErrorInfo += "采样人/现场分析人不能为空！";

            
        }
        if (txt_AccessSampleTime.Text!= "")
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
   
    /// <summary>
    /// 分析项目绑定
    /// </summary>
    protected void DataBindAll(int flag)
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('没有该样品类型的分析项目，请先在分析项目管理中添加');unshowchose();", true);

return;
            }
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请先选择样品类型');unshowchose();", true);

 return;
       
    }
   
    #endregion
    protected void lbtn_chose1_OnClick(object sender, EventArgs e)
    {
        flag = 1;
        DataBindAll(flag);
        ReportSelectQuery();
    }
    protected void lbtn_chose2_OnClick(object sender, EventArgs e)
    {
        flag = 2;
        DataBindAll(flag);
        ReportSelectQuery();
    }
    protected void lbtn_chose3_OnClick(object sender, EventArgs e)
    {
        flag = 3;
        DataBindAll(flag);
        ReportSelectQuery();
    }
    protected void lbtn_chose4_OnClick(object sender, EventArgs e)
    {
        flag = 4;
        DataBindAll(flag);
        ReportSelectQuery();
    }
    protected void lbtn_chose5_OnClick(object sender, EventArgs e)
    {
        flag = 5;
        DataBindAll(flag);
        ReportSelectQuery();
    }
   
    protected void btn_item_Click(object sender, EventArgs e)
    {
        string s = DropList_SampleType.Text.Trim();
        CheckBox xc_flag = Panel_Sample.FindControl("ck_xcflag_" + flag) as CheckBox;
        GridView grv_Item = Panel_Sample.FindControl("grv_Item" + flag) as GridView;
        TextBox txt_Item = Panel_Sample.FindControl("txt_Item" + flag) as TextBox;
        HiddenField hid_Item = Panel_Sample.FindControl("hid_Item" + flag) as HiddenField;
        DataTable tempds = dt_analysis.Clone();
        switch (flag.ToString())
        {
            case "1": tempds = dt_analysis1.Copy(); break;
            case "2": tempds = dt_analysis2.Copy(); break;
            case "3": tempds = dt_analysis3.Copy(); break;
            case "4": tempds = dt_analysis4.Copy(); break;
            case "5": tempds = dt_analysis5.Copy(); break;
            default:tempds = dt_analysis1; break;
        }
        foreach (ListItem LI in cb_analysisList.Items)
        {
            if (LI.Selected)
            {
                DataRow[] drselect = tempds.Select("ItemID='" + LI.Value + "'");
                if (drselect.Length == 0)
                {
                    DataRow dr = tempds.NewRow();

                    dr["分析项目"] = LI.Text;
                    dr["ItemID"] = LI.Value;
                    tempds.Rows.Add(dr);
                    txt_Item.Text += LI.Text.Trim() + ",";
                    hid_Item.Value += LI.Value.Trim() + ",";
                }
               
            }
        }
        //if (tempds.Rows.Count > 0)
        //{
            grv_Item.DataSource = tempds;
            grv_Item.DataBind();
            switch (flag.ToString())
            {
                case "1": dt_analysis1=tempds.Copy() ; break;
                case "2": dt_analysis2 = tempds.Copy(); break;
                case "3": dt_analysis3=tempds.Copy(); break;
                case "4": dt_analysis4= tempds.Copy(); break;
                case "5": dt_analysis5=tempds.Copy(); break;
                default:  dt_analysis1= tempds.Copy(); break;
            }
        //}

        grv_Item.Visible = xc_flag.Checked;
        ReportSelectQuery(); 
    
    }
    #region 文本

    protected void txt_Item1_OnTextChanged(object sender, EventArgs e)
    {
        txt_Item_OnTextChanged(1);
        flag = 1;
    }
    protected void txt_Item2_OnTextChanged(object sender, EventArgs e)
    {
        txt_Item_OnTextChanged(2);
        flag = 2;
    }
    protected void txt_Item3_OnTextChanged(object sender, EventArgs e)
    {
        txt_Item_OnTextChanged(3);
        flag = 3;
    }
    protected void txt_Item4_OnTextChanged(object sender, EventArgs e)
    {
        txt_Item_OnTextChanged(4);
        flag = 4;
    }
    protected void txt_Item5_OnTextChanged(object sender, EventArgs e)
    {
        txt_Item_OnTextChanged(5);
        flag = 5;
    }
    protected void txt_Item_OnTextChanged(int i)
    {
        TextBox txt_Item = Panel_Sample.FindControl("txt_Item" + i) as TextBox;
        CheckBox ck_xcflag = Panel_Sample.FindControl("ck_xcflag_" + i) as CheckBox;
        GridView grv_Item = Panel_Sample.FindControl("grv_Item" + i) as GridView;
        HiddenField hid_Item = Panel_Sample.FindControl("hid_Item" + i) as HiddenField;
        string[] list = txt_Item.Text.Trim().Split(',');
        DataTable tempds = dt_analysis.Clone();
        if (ck_xcflag.Checked)
        { grv_Item.Visible = true; }
        else
            grv_Item.Visible = false;
        switch (i.ToString())
        {
            case "1": dt_analysis1 = tempds.Copy(); break;
            case "2": dt_analysis2 = tempds.Copy(); break;
            case "3": dt_analysis3 = tempds.Copy(); break;
            case "4": dt_analysis4 = tempds.Copy(); break;
            case "5": dt_analysis5 = tempds.Copy(); break;
            default: dt_analysis1 = tempds.Copy(); break;
        }
        string valustr = "";
        string itemstr = "";
        foreach (string tem in list)
        {
            if (tem.Trim() != "")
            {
                DataRow[] dr = tempds.Select("分析项目='" + tem.Trim() + "'");
                if (dr.Length>0)
                {
                    if (!valustr.Contains(dr[0]["itemid"].ToString().Trim() + ","))
                    {
                        valustr += dr[0]["itemid"].ToString().Trim()
                            + ",";
                        itemstr += tem.Trim() + ",";
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
                        valustr += code + ",";
                        itemstr += tem.Trim() + ",";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统中不存在该分析项目，请确认！');", true);

                        return;
                    }

                }
            }

        }
        if (tempds.Rows.Count > 0)
        {
            grv_Item.DataSource = tempds;
            grv_Item.DataBind();
            switch (i.ToString())
            {
                case "1": dt_analysis1 = tempds.Copy(); break;
                case "2": dt_analysis2 = tempds.Copy(); break;
                case "3": dt_analysis3 = tempds.Copy(); break;
                case "4": dt_analysis4 = tempds.Copy(); break;
                case "5": dt_analysis5 = tempds.Copy(); break;
                default: dt_analysis1 = tempds.Copy(); break;
            }
        }
        else
        {
            grv_Item.DataSource = tempds;
            grv_Item.DataBind();
            switch (i.ToString())
            {
                case "1": dt_analysis1 = tempds.Copy(); break;
                case "2": dt_analysis2 = tempds.Copy(); break;
                case "3": dt_analysis3 = tempds.Copy(); break;
                case "4": dt_analysis4 = tempds.Copy(); break;
                case "5": dt_analysis5 = tempds.Copy(); break;
                default: dt_analysis1 = tempds.Copy(); break;
            }
        }
        
    
        txt_Item.Text = itemstr;
        hid_Item.Value = valustr;
        ReportSelectQuery();

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

    #endregion
    protected void txt_SampleType_OnTextChanged(object sender, EventArgs e)
   {
       DataSet ds;
       string str;
       if (DropList_SampleType.Text.Trim().ToString() == "")
       {
           str = "select id,AIName from t_M_AnalysisItemEx order by id";
           ds = new MyDataOp(str).CreateDataSet();
           cb_analysisList.DataSource = ds;
           cb_analysisList.DataValueField = "id";
           cb_analysisList.DataTextField = "AIName";
           cb_analysisList.DataBind();
       }
       else
       {
           str = "select  t_M_AnalysisItemEx.id,AIName from t_M_AnalysisItemEx,t_M_AnalysisMainClassEx where t_M_AnalysisItemEx.ClassID=t_M_AnalysisMainClassEx.ClassID and t_M_AnalysisMainClassEx.ClassName='" + DropList_SampleType.Text.Trim().ToString() + "'";
           ds = new MyDataOp(str).CreateDataSet();
           cb_analysisList.DataSource = ds;
           cb_analysisList.DataValueField = "id";
           cb_analysisList.DataTextField = "AIName";
           cb_analysisList.DataBind();
       }
       if (ds.Tables[0].Rows.Count > 0)
       {

           str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' ";
           DataSet dscheck = new MyDataOp(str).CreateDataSet();
           if (dscheck.Tables[0].Rows.Count > 0)
               foreach (ListItem LI in cb_analysisList.Items)
               {
                   foreach (DataRow dr in dscheck.Tables[0].Rows)
                   {
                       if (dr[0].ToString() == LI.Value)
                           LI.Selected = true;
                   }
               }
           dscheck.Dispose();

       }
       ds.Dispose();
       ReportSelectQuery();
       Query();

   }

    #region 分析项目编辑
    
     protected void grv_Item_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Width =20;
            e.Row.Cells[2].Width = 60;
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
            //TableCell headerset = new TableCell();
            //headerset.Text = "编辑";
            //headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerset.Width = 60;
            //e.Row.Cells.Add(headerset);
            TableCell headerDel = new TableCell();
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            {
                headerDel.Text = "删除";
                headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                headerDel.Width = 30;
                e.Row.Cells.Add(headerDel);

            }
           
           
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

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
           
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            {
                tabcDel.Controls.Add(ibtnDel);
                e.Row.Cells.Add(tabcDel);
               
            }
            else
                ibtnDel.Enabled = false;
           


        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
    }
    
   protected void grv_Item_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {
       GridView item_grv = sender as GridView;
      string grvid= item_grv.ID;
       flag=int.Parse(grvid.Substring(grvid.Length-1,1));
       string itemid = item_grv.Rows[e.RowIndex].Cells[3].Text.Trim();
        DataTable tempds = dt_analysis.Clone();
       
        switch (flag.ToString())
        {
            case "1": tempds = dt_analysis1.Copy(); break;
            case "2": tempds = dt_analysis2.Copy(); break;
            case "3": tempds = dt_analysis3.Copy(); break;
            case "4": tempds = dt_analysis4.Copy(); break;
            case "5": tempds = dt_analysis5.Copy(); break;
            default:tempds = dt_analysis1; break;
        }
        DataRow[] dr = tempds.Select("ItemID='" + itemid + "'");
        if (dr.Length > 0)
        {
            tempds.Rows.Remove(dr[0]);
            tempds.AcceptChanges();
            //if (tempds.Rows.Count > 0)
            //{
                item_grv.DataSource = tempds;
                item_grv.DataBind();
                switch (flag.ToString())
                {
                    case "1": dt_analysis1 = tempds.Copy();break;
                    case "2": dt_analysis2 = tempds.Copy(); break;
                    case "3": dt_analysis3 = tempds.Copy(); break;
                    case "4": dt_analysis4 = tempds.Copy(); break;
                    case "5": dt_analysis5 = tempds.Copy(); break;
                    default: dt_analysis1 = tempds.Copy(); break;
                }
                TextBox txt_Item = Panel_Sample.FindControl("txt_Item" +flag) as TextBox;
                HiddenField hid_Item = Panel_Sample.FindControl("hid_Item" + flag) as HiddenField;
             txt_Item.Text = "";
             hid_Item.Value = "";
                for (int p = 0; p < tempds.Rows.Count; p++)
                { 
                    txt_Item.Text+=tempds.Rows[p]["分析项目"].ToString()+",";
                    hid_Item.Value+=tempds.Rows[p]["itemid"].ToString()+",";
                }
                if (tempds.Rows.Count == 0)
                {
                    txt_Item.Text = "";
                    hid_Item.Value = "";
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
        cbl.SelectedIndex = 0;
    }
    //绑定分析人及方法    
    private void BindCheckBoxListUser(RadioButtonList cbl, string ItemID)
    {
        //DataSet ds = new MyDataOp("select CASE WHEN Name IS NULL  THEN Name+'('+t_M_AIStandard.Standard+')' ELSE t_M_AIStandard.Standard END AS Standard,t_M_AIStandard.id,t_MoniterUser.role,t_MoniterUser.userid from t_M_AIStandard left join t_MoniterUser  on itemid=AIID and t_M_AIStandard.id=t_MoniterUser.method  inner join t_R_UserInfo on t_R_UserInfo.UserID= t_MoniterUser.userid where t_MoniterUser.itemid='" + ItemID + "' and ifin=0").CreateDataSet();
        DataSet ds = new MyDataOp("select CASE WHEN Name IS not NULL  THEN Name+'('+t_M_AIStandard.Standard+')' ELSE Standard  END AS Standard,t_M_AIStandard.id,t_MoniterUser.role,t_MoniterUser.userid,[name] from t_M_AIStandard inner join t_MoniterUser  on itemid=AIID and t_M_AIStandard.id=t_MoniterUser.method  inner join t_R_UserInfo on t_R_UserInfo.UserID= t_MoniterUser.userid and ifin=0 where t_M_AIStandard.AIID='"+ItemID+"'" ).CreateDataSet();
        //这里的方法根据你自己的取数据的方法 
        DataRow[] dra = ds.Tables[0].Select("role='A'");
        DataSet tem=ds.Clone();
        if (dra.Length > 0)
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
            DataRow[] drb = ds.Tables[0].Select("role='B'");
            if (drb.Length == 0)
            {
                DataSet dstemp = new MyDataOp("select  Standard,t_M_AIStandard.id from t_M_AIStandard where t_M_AIStandard.AIID='" + ItemID + "'").CreateDataSet();

                foreach (DataRow dr in dstemp.Tables[0].Rows)
                {
                    DataRow dradd =ds.Tables[0].NewRow();
                    dradd["Standard"] = dr["Standard"].ToString();
                    dradd["id"] = dr["id"].ToString();
                    ds.Tables[0].Rows.Add(dradd);
                    ds.Tables[0].AcceptChanges();
                }

            }
           
 
        }
        cbl.DataSource = ds;
        cbl.DataValueField = "id";
        cbl.DataTextField = "Standard";
        cbl.DataBind();
        cbl.SelectedIndex = 0;
    }
    protected void grv_Item_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView item_grv = sender as GridView;
        string grvid = item_grv.ID;
        flag = int.Parse(grvid.Substring(grvid.Length - 1, 1));
        string itemid =e.Row.Cells[3].Text.Trim();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txt_value = (TextBox)e.Row.FindControl("itemvalue");
            if (e.Row.Cells[4].Text.Trim() != "&nbsp;")
            {
                txt_value.Text = e.Row.Cells[4].Text.Trim();
            }
           
            CheckBox cb_xcfx = (CheckBox)e.Row.FindControl("cb_flag");
                if (e.Row.Cells[1].Text.Trim() == "1")
                    cb_xcfx.Checked = true;
                else
                    cb_xcfx.Checked = false;

                TextBox txt_ItemRemark = (TextBox)e.Row.FindControl("txt_ItemRemark");
                if (e.Row.Cells[6].Text.Trim() != "&nbsp;")
                txt_ItemRemark.Text = e.Row.Cells[6].Text.Trim();
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
  
     
}
