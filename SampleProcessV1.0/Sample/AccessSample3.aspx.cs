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

using WebApp.Components;

public partial class AccessSample3 : System.Web.UI.Page
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
    private string strReportId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportId"]; }
        set { ViewState["strReportId"] = value; }
    }
    private string strAnalysisId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strAnalysisId"]; }
        set { ViewState["strAnalysisId"] = value; }
    }
    private string strReportName//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportName"]; }
        set { ViewState["strReportName"] = value; }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_SampleType.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_CreateDate.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_SampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
           // txt_SampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            Query();
            SetButton();// txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
          

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>样品接收</b></font>";
        }
    }


    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
          
            btn_OK.Enabled = false;
            //btn_AddSample.Enabled = false;
            for (int i = 0; i < grdvw_List.Rows.Count; i++)
            {
              ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
              if (btn != null)
              btn.Visible = false;

            }
        }
        else
        {
            
            btn_OK.Enabled = true;
            //btn_AddSample.Enabled = true;
            //for (int i = 0; i < grdvw_List.Rows.Count; i++)
            //{
            //    ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
            //    btn.Visible = true;

            //}
        }
    }
    private void Query()
    {
        string strSql = "select t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,ClientID,ClientName 区域,urgent 备注,Ulevel,wtdepart 委托单位 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=1) order by t_M_ReporInfo.id";

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
        if (grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();
       
        lbl_Type.Text = "样品列表";

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
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAnalysisAdd();", true);
        ReportSelectQuery();

    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {   
            TableCell headerDetail = new TableCell();
            headerDetail.Text = "任务编辑";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);
            TableCell headerset = new TableCell();
            headerset.Text = "样品接收";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);
            TableCell headerDel = new TableCell();
            headerDel.Text = "样品列表";
            headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDel.Width = 30;
            e.Row.Cells.Add(headerDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

           
            TableCell MenuSet = new TableCell();
            MenuSet.Width = 60;
            MenuSet.Style.Add("text-align", "center");
            ImageButton btMenuSet = new ImageButton();
            btMenuSet.ImageUrl = "~/images/Detail.gif";
            btMenuSet.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuSet.Controls.Add(btMenuSet);
            e.Row.Cells.Add(MenuSet);
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/images/Detail.gif";

            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.ID = "btn_delete";
            ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[9].Visible = false;

        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string slevel = "";
        txt_ReportID.Text = "";
        if (grdvw_List.Rows[e.RowIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.RowIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.RowIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.RowIndex].Cells[1].Text.Trim();

        lbl_Type.Text = "样品列表";

        txt_CreateDate.Text = grdvw_List.Rows[e.RowIndex].Cells[2].Text.Trim();//报告创建日期
        //txt_itemname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        //txt_itemname.ReadOnly = true;
        ListItem choose = new ListItem("请选择", "-1");
        //txt_itemname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
        drop_urgent.Text = "";
        if (grdvw_List.Rows[e.RowIndex].Cells[8].Text != "&nbsp;")
            drop_urgent.Text = grdvw_List.Rows[e.RowIndex].Cells[8].Text;
        drop_level.SelectedValue = "0";
        if (grdvw_List.Rows[e.RowIndex].Cells[9].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.RowIndex].Cells[9].Text.Trim() != "")
        {
            slevel = grdvw_List.Rows[e.RowIndex].Cells[9].Text.Trim();
            drop_level.SelectedValue = slevel;
        }
        MyStaVoid.BindList("ClientName", "id", "select * from t_M_ClientInfo order by id", DropList_client);

        string clientid = grdvw_List.Rows[e.RowIndex].Cells[6].Text;
        DropList_client.Items.Add(choose);
        DropList_client.Items.FindByValue(clientid).Selected = true;
        MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);

        string itemtid = grdvw_List.Rows[e.RowIndex].Cells[3].Text;
        drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue(itemtid).Selected = true;
        if (grdvw_List.Rows[e.RowIndex].Cells[10].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.RowIndex].Cells[10].Text.Trim() != "")
        {
            txt_wtdepart.Text = grdvw_List.Rows[e.RowIndex].Cells[10].Text.Trim();
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showDetailAnalysis();", true);
        ReportSelectQuery();
    }
    #endregion

    protected void btn_Save_Click(object sender, EventArgs e)
    {
         
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
        string strSql = "select t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,ClientID,ClientName 区域,urgent 备注,Ulevel,wtdepart 委托单位 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=1) " + strSample + strDate + " order by t_M_ReporInfo.id";

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
      
        //DropList_SampleType.Text= "";
        //DropList_AnalysisMainItem.SelectedValue = "-1";
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
       
        MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);
       
        drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue("-1").Selected = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();hiddenDetailAnalysis();", true);
        drop_urgent.Text = "";

        Query();
    }
    private List<string> getnum(string str, ref string retstr)
    {
        List<string> namelist = new List<string>();
        string[] sampleList = str.Split(',', '，');
        int sampleNum = 0;
        sampleNum = sampleList.Length;
        for (int k = 0; k < sampleList.Length; k++)
        {
            string[] range = sampleList[k].Split('~');
            if (range.Length == 2)
            {
                if (range[1].Trim() != "" && range[0].Trim() != "")
                {
                    try
                    {
                        sampleNum = sampleNum - 1;
                        if (range[1].Substring(0, range[1].Length - 3) == range[0].Substring(0, range[0].Length - 3))
                        {
                            int p = int.Parse(range[1].Substring(range[1].Length - 3, 3));
                            int q = int.Parse(range[0].Substring(range[0].Length - 3, 3));
                            int temp = p - q + 1;
                            if (temp <= 0)
                                throw new Exception("编号有错！");
                            else
                            {
                                sampleNum = sampleNum + temp;
                                for (int j = q; j <= p; j++)
                                {
                                    string strtemp = range[1].Substring(0, range[1].Length - 3) + CodeConvert(j.ToString(), 3);
                                    namelist.Add(strtemp);
                                }
                            }
                        }
                        else
                            throw new Exception("编号不统一！");
                    }
                    catch (Exception msg)
                    {
                        retstr = msg.Message.ToString();
                        break;
                    }
                }
            }
            else if (range.Length > 2)
            {
                retstr = "编号填写有误！";
                break;
            }
            else
                namelist.Add(sampleList[k]);


        }
        return namelist;
    }

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
            //int num = getnum(txt_ReportID.Text);
            //if (num == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
            //  //  return;
            //}
            //else
            //{
                #region 添加新纪录
                if (lbl_Type.Text == "添加")
                {
                    string clientname = DropList_client.SelectedValue.ToString();
                    string strSqltmp = "";
                        strSqltmp = @"insert into t_M_ReporInfo
                    (CreateDate,ItemType,UserID,ClientID,ReportName,urgent,Ulevel,wtdepart)  
                    values('" + txt_CreateDate.Text.Trim() + "','" + drop_ItemList.SelectedValue.ToString() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "','" + clientname + "','" + txt_ReportID.Text.Trim() + "','" + drop_urgent.Text + "','" + drop_level.SelectedValue.ToString() + "','" + txt_wtdepart.Text.Trim()+ "')";


                        MyDataOp mdo = new MyDataOp(strSqltmp);
                        bool blSuccess = mdo.ExecuteCommand();
                        if (blSuccess == true)
                        {
                            WebApp.Components.Log.SaveLog("创建样品原单添加成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据添加成功！')", true);


                        }
                        else
                        {
                            WebApp.Components.Log.SaveLog("创建样品原单添加失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添加失败！')", true);

                        }

                }
                #endregion
                else if (lbl_Type.Text == "编辑")
                {
                    string clientname = DropList_client.SelectedValue.ToString();
                    string strSqltmp = "";
                    strSqltmp = @"update t_M_ReporInfo set ReportName='" + txt_ReportID.Text.Trim() +
                                                    "',ItemType='" + drop_ItemList.SelectedValue.ToString() +
                                                     "',ClientID='" + clientname +
                                                     "',urgent='" + drop_urgent.Text +
                                                     "',Ulevel='"+drop_level.SelectedValue.ToString()+
                                                      "',wtdepart='" + txt_wtdepart.Text.Trim() +

  "',Projectname='" + txt_Projectname.Text +
                                            "' where id='" + strReportId + "'";

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
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "hiddenDetail();alert('数据添保存败！')", true);

                    }

                }
            //}
        }
        Query();
    }
    private string Verify()
    {
       string strErrorInfo = ""; 
        // if (txt_ReportID.Text.Trim() == "")
        //{
        //    strErrorInfo += "报告标识不能为空！\\n";
        //}

        //else
        if (drop_ItemList.SelectedValue == "-1")
            strErrorInfo += "请选择项目类型！\\n";
        else if (txt_CreateDate.Text == "")
            strErrorInfo += "请填写时间！\\n";

        else if (DropList_client.SelectedValue == "-1")
            strErrorInfo += "请选择委托单位！\\n";
       
        return strErrorInfo;
    }
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        txt_SReportID.Text = "";
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim()!="&nbsp;")
        txt_SReportID.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
       
        lbl_Type.Text = "任务信息";
        btn_Save.Visible = false;
        txt_SCreateDate.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();//报告创建日期
       
        txt_itemname.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[10].Text.Trim() != "&nbsp;")
            txt_Client.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[10].Text.Trim();
        else
            txt_Client.Text = "";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "show();", true);
        ReportSelectQuery();
    }
    #region 样品单列表

    protected void btn_AddSample_Click(object sender, EventArgs e)
    {
        lbl_SampleDo.Text = "添加样品";
       
        DropList_SampleType.Text = "";
        txt_SampleID.Text = "";
        
        txt_SampleTime.Text = "";
       
        ListItem choose = new ListItem("请选择", "-1");
        
        //MyStaVoid.BindList("ReportName", "id", "select * from t_M_ReporInfo where StatusID<2 order by id", txt_report);
        //txt_report.Items.Add(choose);
        //txt_report.Items.FindByValue(strReportId).Selected = true;
       
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);


        ReportSelectQuery();
    }
    protected void grdvw_ReportDetail_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        lbl_SampleDo.Text = "编辑样品";
        txt_SampleID.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[6].Text.Trim();
       
        DropList_SampleType.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[8].Text.Trim();
        strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
       
        strReportId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[14].Text.Trim();
        strSampleId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
        txt_SampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[3].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);

        //MyStaVoid.BindList("ReportName", "id", "select * from t_M_ReporInfo where StatusID<2 order by id", txt_report);
        //txt_report.Items.FindByValue(strReportId).Selected = true;
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
            headerset.Text = "编辑";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "报告上传";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);
            TableCell headerDel = new TableCell();
            headerDel.Text = "删除";
            headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDel.Width = 30;
            e.Row.Cells.Add(headerDel);
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
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDel.Enabled = false;
            }
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);


        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;

            //e.Row.Cells[11].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
        }
    }
    protected void btn_CancelReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysis();", true);
        Query();

    }
    protected void grdvw_ReportDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        strSelectedId = grdvw_ReportDetail.Rows[e.RowIndex].Cells[2].Text;
        string strSql;
        string[] deletelist = new string[2];
        strSql = "DELETE FROM t_M_SampleInfor WHERE id= '" + strSelectedId + "'";
        //待修改，改项目删除后，相应要修改的信息

        //deletelist.SetValue("delete from t_操作员信息 where 所属角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 0);
        //deletelist.SetValue("delete from t_角色菜单关系表 where 角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 1);
        // deletelist.SetValue("DELETE FROM t_角色信息 WHERE 级别id = '" + strSelectedId + "'",2);

        deletelist.SetValue(strSql, 0);
        strSql = "DELETE FROM t_M_MonitorItem WHERE SampleID= '" + strSelectedId + "'";
        deletelist.SetValue(strSql, 1);

        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(2, deletelist))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + txt_SampleID.Text.Trim() + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
        }
        else
        {
            WebApp.Components.Log.SaveLog("样品接收中删除样品信息" + txt_SampleID.Text.Trim() + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        ReportSelectQuery();

    }
    //查询出选中的报告的样品列表
    private void ReportSelectQuery()
    {
        string strSql = "SELECT t_M_ReporInfo.ReportName AS 报告标识, t_M_SampleInfor.id," +
      "t_M_SampleInfor.AccessDate AS 接样时间, t_M_ReporInfo.ItemType," +
     " ItemName AS 项目类型, t_M_SampleInfor.SampleID AS 样品编号, " +
      " t_M_SampleInfor.TypeID, t_M_SampleType.SampleType AS 样品类型, " +
      " t_M_ReporInfo.ClientID, t_M_ClientInfo.ClientName AS 委托单位, " +
     " t_M_ReporInfo.urgent 备注, t_M_SampleInfor.ReportRemark AS 报告备注, " +
     " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID" +
" FROM t_M_ReporInfo inner join t_M_SampleInfor on t_M_SampleInfor.ReportID = t_M_ReporInfo.id INNER JOIN" +
     " t_M_ItemInfo ON t_M_ReporInfo.ItemType = t_M_ItemInfo.ItemID INNER JOIN" +
     " t_M_SampleType ON " +
    "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID INNER JOIN" +

     " t_M_ClientInfo ON t_M_ReporInfo.ClientID = t_M_ClientInfo.id " +

" WHERE " +
     " t_M_SampleInfor.ReportID=" + strReportId +
" ORDER BY t_M_SampleInfor.id";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
       
        DataColumn dccc = new DataColumn("样品报告状态");
        ds.Tables[0].Columns.Add(dccc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
           
            if (int.Parse(dr["StatusID"].ToString()) <= 1)
                dr["样品报告状态"] = "未完成";
            else
                dr["样品报告状态"] = "完成";



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
    protected void btn_OKSample_Click(object sender, EventArgs e)
    {
        string strFlag = VerifySample();//需要添加检查样品编号的有效性

        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

           // return;
        }
        else
        {
            List<string> namelist = new List<string>();
            string retstr = "";
            namelist = getnum(txt_SampleID.Text, ref retstr);
            int num = namelist.Count; //= getnum(txt_SampleID.Text);
            if (retstr!="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + retstr + "');", true);
               // return;

            }
            else
            {
                string name = "";
                for (int i = 0; i < num; i++)
                {
                    name += namelist[i] + ",";
                }
                    #region 添加新纪录
                    if (lbl_SampleDo.Text == "添加样品")
                    {
                        string strSqltmp = "";

                        DataSet myDR1 = new MyDataOp("select TypeID from t_M_SampleType where SampleType='" + DropList_SampleType.Text.Trim() + "'").CreateDataSet();
                        if (myDR1.Tables[0].Rows.Count > 0)
                        {


                            string typename = myDR1.Tables[0].Rows[0]["TypeID"].ToString();

                            string ReportID = "";

                            ReportID =strReportId;
                            strSqltmp = @"insert into t_M_SampleInfor
                    (AccessDate,TypeID,SampleID,UserID,CreateDate,ReportName,ReportID,StatusID,snum)  
                    values('" + txt_SampleTime.Text.Trim() + "','" + typename + "','" + txt_SampleID.Text.Trim() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + strReportId + "','" + ReportID + "',0,'" + num + "')";
                           

                            MyDataOp mdo = new MyDataOp(strSqltmp);
                            bool blSuccess = mdo.ExecuteCommand();
                            if (blSuccess == true)
                            {
                                WebApp.Components.Log.SaveLog("样品接收添加样品成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据添加成功！');", true);

                            }
                            else
                            {

                                WebApp.Components.Log.SaveLog("样品接收添加样品失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据添加失败！');", true);

                            }

                        }
                        else
                        {
                            WebApp.Components.Log.SaveLog("样品接收添加样品不存在该样品名称！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('不存在该样品类型，请联系样品管理员！');", true);

                        }
                    }
                #endregion

                #region 编辑记录
                if (lbl_SampleDo.Text == "编辑样品")
                {
                    DataSet myDR1 = new MyDataOp("select TypeID from t_M_SampleType where SampleType='" + DropList_SampleType.Text.Trim() + "'").CreateDataSet();
                    if (myDR1.Tables[0].Rows.Count > 0)
                    {

                        string typename = myDR1.Tables[0].Rows[0]["TypeID"].ToString();

                        string ReportID = "";

                        ReportID = strReportId;// myDR3.Tables[0].Rows[0]["id"].ToString();
                        string strSql = @"update t_M_SampleInfor set AccessDate='" + txt_SampleTime.Text.Trim() +
                            "',SampleID='" + txt_SampleID.Text.Trim() +

                                                 "',TypeID='" + typename +
                                                 "',ReportName='" + strReportName + "',ReportID='" + ReportID + "'" +
                                                 ",snum='"+num+"'"+
                                        " where id='" + strSelectedId + "'";
                        string[] arr = new string[2];
                        arr.SetValue(strSql, 0);

                        strSql = "update t_M_MonitorItem set Num='" + num + "' where SampleID='" + strSelectedId + "'";
                        arr.SetValue(strSql, 1);
                        MyDataOp mdo = new MyDataOp(strSql);
                        bool blSuccess = mdo.DoTran(2, arr);
                        if (blSuccess == true)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据编辑成功！');", true);
                            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('数据编辑失败！');", true);
                            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();alert('不存在该样品类型，请联系样品管理员！');", true);
                        WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）不存在该样品类型！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

                    }
                }

                #endregion


            }

        }
        ReportSelectQuery();

    }
    protected void btn_CancelSample_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();", true);
        ReportSelectQuery();
    }
    private string VerifySample()
    {
        string strErrorInfo = "";
        if (txt_SampleID.Text.Trim() == "")
        {
            strErrorInfo += "样品编码不能为空！\\n";
        }

        else if (DropList_SampleType.Text == "")
            strErrorInfo += "样品类型不能为空！\\n";

        //else if (txt_report.SelectedValue == "-1")
        //{
        //    strErrorInfo += "请输入报告编号！\\n";
           

        //}
        else if (txt_SampleTime.Text=="")
        {
            strErrorInfo += "请输入接样时间！\\n";


        }
        return strErrorInfo;
    }
    #endregion
    protected void DropList_AnalysisMainItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds;
        string str;
        if (DropList_AnalysisMainItem.SelectedValue.ToString() == "-1")
        {
            str = "select id,AIName from t_M_AnalysisItem";
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        else
        {
            str = "select id,AIName from t_M_AnalysisItem where ClassID='" + DropList_AnalysisMainItem.SelectedValue.ToString() + "'";
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

        Query();

    }
    protected void DataBindAll()
    {


        string str = "select id,AIName from t_M_AnalysisItem order by ClassID asc";
        DataSet ds = new MyDataOp(str).CreateDataSet();
        cb_analysisList.DataSource = ds;
        cb_analysisList.DataValueField = "id";
        cb_analysisList.DataTextField = "AIName";
        cb_analysisList.DataBind();
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
        }
        //Query();
    }

}
