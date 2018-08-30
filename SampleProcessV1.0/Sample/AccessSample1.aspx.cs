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

public partial class AccessSample1 : System.Web.UI.Page
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
    private int strReportId//所选择操作列记录对应的id
    {
        get { return (int)ViewState["strReportId"]; }
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
            //txt_SampleType.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_CreateDate.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
           // txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
           txt_SampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
           //// txt_SampleTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            Query();
            SetButton();// txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
          

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>样品接收</b></font>";
        }
    }


    protected void SetButton()
    {
        //if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        //{
        //    btn_Add.Enabled = false;
        //    //btn_OK.Enabled = false;
        //    btn_AddSample.Enabled = false;
        //    for (int i = 0; i < grdvw_List.Rows.Count; i++)
        //    {
        //      ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
        //      btn.Visible = false;

        //    }
        //}
        //else
        //{
        //    btn_Add.Enabled = true;
        //    //btn_OK.Enabled = true;
        //    btn_AddSample.Enabled = true;
        //    for (int i = 0; i < grdvw_List.Rows.Count; i++)
        //    {
        //        ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
        //        btn.Visible = true;

        //    }
        //}
    }
    private void Query()
    {
        string strSql = "select t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,ClientID,ClientName 区域,urgent 备注,Ulevel,wtdepart 委托单位 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=0 or StatusID=1) order by t_M_ReporInfo.id";

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
        strReportId = int.Parse(grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim());
       
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
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showDetail();", true);

    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "编辑";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);

            TableCell headerset = new TableCell();
            headerset.Text = "编辑";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

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
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[9].Visible = false;

        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strReportId = int.Parse(grdvw_List.Rows[e.RowIndex].Cells[1].Text);
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
            i = 3;
            strSql = "DELETE FROM t_M_MonitorItem " + condition;
            deletelist.SetValue(strSql, 2);

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
    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
        string strSample = "";
        string strDate = "";
        if (txt_samplequery.Text != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text != "")
            strDate = " and (year(CreateDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(CreateDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(CreateDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_M_ReporInfo.id,CreateDate 时间,t_M_ReporInfo.ItemType,ItemName 项目类型,ReportName 报告标识,ClientID,ClientName 区域,urgent 备注,Ulevel,wtdepart 委托单位 from t_M_ReporInfo,t_M_ItemInfo,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID=0 or StatusID=1) " + strSample + strDate + " order by t_M_ReporInfo.id";

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
        strReportId =0;
        lbl_Type.Text = "添加";
        btn_OKSample.Text = "保存";
        txt_ReportID.Text = "";
        MyStaVoid.BindList("ClientName", "id", "select * from t_M_ClientInfo order by id", DropList_client);
        ListItem choose = new ListItem("请选择", "-1");
        DropList_client.Items.Add(choose);
        DropList_client.Items.FindByValue("-1").Selected = true;
       
        MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);
       
        drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue("-1").Selected = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "show();", true);
        Panel3.Visible = false;
        Panel4.Visible = false;
        MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClass order by ClassCode asc", DropList_AnalysisMainItem);
        //ListItem choose = new ListItem("全部", "-1");
        DropList_AnalysisMainItem.SelectedItem.Selected = false;
        DropList_AnalysisMainItem.Items.Add(choose);
        DropList_AnalysisMainItem.Items.FindByValue("-1").Selected = true;
        ReportSelectQuery(); 
        clean();
        DataBindAll();
        Query();
    }
    private void clean()
    {
        drop_urgent.Text = "";
        txt_ReportID.Text = "";
        txt_CreateDate.Text = "";
        DropList_client.SelectedIndex = DropList_client.Items.Count - 1;
        drop_ItemList.SelectedIndex = drop_ItemList.Items.Count - 1;
        drop_level.SelectedIndex = 0;
        txt_wtdepart.Text = "";
        txt_SampleID.Text = "";
        DropList_SampleType.Text = "";
        txt_SampleTime.Text = "";
        txt_num.Text = "0";
        txt_own.Text = "";
        txt_send.Text = "";
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
  
  
    private string Verify()
    {
       string strErrorInfo = ""; 
       
        if (drop_ItemList.SelectedValue == "-1")
            strErrorInfo += "请选择项目类型！\\n";
        else if (txt_CreateDate.Text == "")
            strErrorInfo += "请填写时间！\\n";

        else if (DropList_client.SelectedValue == "-1")
            strErrorInfo += "请选择区域！\\n";
       
        return strErrorInfo;
    }
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        Panel3.Visible = true;
        //txt_SReportID.Text = "";
        clean();
        string slevel = "";
        txt_ReportID.Text = "";
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        strReportId = int.Parse(grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim());

        lbl_Type.Text = "编辑";

        txt_CreateDate.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();//报告创建日期
        //txt_itemname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        //txt_itemname.ReadOnly = true;
        ListItem choose = new ListItem("请选择", "-1");
        //txt_itemname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
        drop_urgent.Text = "";
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[8].Text != "&nbsp;")
            drop_urgent.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[8].Text;
        drop_level.SelectedValue = "0";
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[9].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewSelectedIndex].Cells[9].Text.Trim() != "")
        {
            slevel = grdvw_List.Rows[e.NewSelectedIndex].Cells[9].Text.Trim();
            drop_level.SelectedValue = slevel;
        }
        MyStaVoid.BindList("ClientName", "id", "select * from t_M_ClientInfo order by id", DropList_client);

        string clientid = grdvw_List.Rows[e.NewSelectedIndex].Cells[6].Text;
        DropList_client.Items.Add(choose);
        DropList_client.Items.FindByValue(clientid).Selected = true;
        MyStaVoid.BindList("ItemName", "ItemID", "select * from t_M_ItemInfo order by ItemID", drop_ItemList);

        string itemtid = grdvw_List.Rows[e.NewSelectedIndex].Cells[3].Text;
        drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue(itemtid).Selected = true;
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[10].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewSelectedIndex].Cells[10].Text.Trim() != "")
        {
            txt_wtdepart.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[10].Text.Trim();
        }
       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "show();", true);
        ReportSelectQuery();
        DataBindAll();
    }
    #region 样品单列表

    protected void btn_AddSample_Click(object sender, EventArgs e)
    {
        lbl_SampleDo.Text = "添加样品";

        DropList_SampleType.Text = "";
        txt_SampleID.Text = "";

        txt_SampleTime.Text = "";


        //ListItem choose = new ListItem("请选择", "-1");

        //MyStaVoid.BindList("ReportName", "id", "select * from t_M_ReporInfo where StatusID<2 order by id", txt_report);
        //txt_report.Items.Add(choose);
        //txt_report.Items.FindByValue(strReportId).Selected = true;

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();", true);


        ReportSelectQuery();
    }
    protected void grdvw_ReportDetail_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        lbl_SampleDo.Text = "编辑样品";
        txt_SampleID.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
       
        DropList_SampleType.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[7].Text.Trim();
        strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        strSampleId = txt_SampleID.Text;
        txt_SampleTime.Text = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
       
        MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClass order by ClassCode asc", DropList_AnalysisMainItem);
        ListItem choose = new ListItem("全部", "-1");
        DropList_AnalysisMainItem.SelectedItem.Selected = false;
        DropList_AnalysisMainItem.Items.Add(choose);
        DropList_AnalysisMainItem.Items.FindByValue("-1").Selected = true;
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();hiddenDetail();", true);
        if (grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[12].Text.Trim() != "&nbsp;")
            txt_send.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[12].Text.Trim();
      
        string retstr = "";
        List<string> namelist = new List<string>();

        namelist = getnum(lbl_sample.Text, ref retstr);
        if (retstr != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + retstr + "');", true);
        }
        txt_num.Text = namelist.Count.ToString();

        //if (grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text != "&nbsp;")
        //    txt_num.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text;
        DataBindAll();
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
            //////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[16].Visible = false;
        }
    }
    protected void btn_CancelReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
       //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        Query();

    }
    protected void grdvw_ReportDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        strSelectedId = grdvw_ReportDetail.Rows[e.RowIndex].Cells[1].Text;
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
        string strSql = "select t_M_SampleInfor.id,AccessDate 接样时间,t_M_ReporInfo.ItemType,ItemName 项目类型,SampleID 样品编号,t_M_SampleInfor.TypeID,t_M_SampleType.SampleType 样品类型,t_M_ReporInfo.ClientID,ClientName 区域,t_M_ReporInfo.ReportName 报告标识,t_M_ReporInfo.urgent 备注,t_M_SampleInfor.ReportRemark 报告备注,Ulevel,snum,wtdepart 委托单位,t_M_ReporInfo.ID from t_M_ReporInfo, t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType,t_M_ClientInfo where t_M_ClientInfo.id=t_M_ReporInfo.ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID and t_M_ReporInfo.StatusID<2 and t_M_SampleInfor.ReportID=t_M_ReporInfo.id  and t_M_SampleInfor.StatusID=0 order by t_M_SampleInfor.id ";

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
//        string strSql = "SELECT t_M_ReporInfo.ReportName AS 报告标识, t_M_SampleInfor.id," +
//      "t_M_SampleInfor.AccessDate AS 接样时间, t_M_ReporInfo.ItemType," +
//     " ItemName AS 项目类型, t_M_SampleInfor.SampleID AS 样品编号, " +
//      " t_M_SampleInfor.TypeID, t_M_SampleType.SampleType AS 样品类型, " +
//      " t_M_ReporInfo.ClientID, t_M_ClientInfo.ClientName AS 委托单位, " +
//     " t_M_ReporInfo.urgent 备注, t_M_SampleInfor.ReportRemark AS 报告备注, " +
//     " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID" +
//" FROM t_M_ReporInfo inner join t_M_SampleInfor on t_M_SampleInfor.ReportID = t_M_ReporInfo.id INNER JOIN" +
//     " t_M_ItemInfo ON t_M_ReporInfo.ItemType = t_M_ItemInfo.ItemID INNER JOIN" +
//     " t_M_SampleType ON " +
//    "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID INNER JOIN" +

//     " t_M_ClientInfo ON t_M_ReporInfo.ClientID = t_M_ClientInfo.id " +

//" WHERE " +
//     " t_M_SampleInfor.ReportID='" + strReportId +
//"' ORDER BY t_M_SampleInfor.id";
        //DataSet ds = new MyDataOp(strSql).CreateDataSet();
       
        //DataColumn dccc = new DataColumn("样品报告状态");
        //ds.Tables[0].Columns.Add(dccc);
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
           
        //    if (int.Parse(dr["StatusID"].ToString()) <= 1)
        //        dr["样品报告状态"] = "未完成";
        //    else
        //        dr["样品报告状态"] = "完成";



        //}
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
            if ( strReportId == 0 || strReportId==-1)
            { 
           
                string clientname = DropList_client.SelectedValue.ToString();
                string strSqltmp = "";
                strSqltmp = @"insert into t_M_ReporInfo
                    (CreateDate,ItemType,UserID,ClientID,ReportName,urgent,Ulevel,wtdepart)  
                    values('" + txt_CreateDate.Text.Trim() + "','" + drop_ItemList.SelectedValue.ToString() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "','" + clientname + "','" + txt_ReportID.Text.Trim() + "','" + drop_urgent.Text + "','" + drop_level.SelectedValue.ToString() + "','" + txt_wtdepart.Text.Trim() + "')";


                MyDataOp mdo = new MyDataOp(strSqltmp);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    string str = "select id from t_M_ReporInfo where CreateDate='" + txt_CreateDate.Text.Trim() + "' and ItemType='" + drop_ItemList.SelectedValue.ToString() + "' and Ulevel='" + drop_level.SelectedValue.ToString() + "'";
                    DataSet dscheck = new MyDataOp(str).CreateDataSet();
                    if (dscheck != null)
                    {
                        strReportId = int.Parse(dscheck.Tables[0].Rows[0][0].ToString());
                    }
                    WebApp.Components.Log.SaveLog("任务添加成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "alert('任务添加成功！')", true);
                    Panel3.Visible = true;

                }
                else
                {
                    WebApp.Components.Log.SaveLog("任务添加失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('任务添加失败！')", true);

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

                                                  "',Projectname='" + txt_itemname.Text.Trim() +
                                        "' where id='" + strReportId + "'";

                MyDataOp mdo = new MyDataOp(strSqltmp);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    WebApp.Components.Log.SaveLog("任务编辑成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "alert('任务编辑成功！')", true);


                }
                else
                {
                    WebApp.Components.Log.SaveLog("任务编辑失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('任务编辑失败！')", true);

                }

            }
            //}
        }
        Query();
    }
   
    protected void btn_OKSample_Click(object sender, EventArgs e)
    {
        string strFlag = VerifySample();//需要添加检查样品编号的有效性

        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

           return;
        }
        else
        {
            string retstr = "";
            List<string> namelist = new List<string>();
            namelist = getnum(lbl_sample.Text, ref retstr);
            if (retstr != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + retstr + "');", true);
                return;
            }
            int sampleNum = namelist.Count; ;

            //自动通过样品编号计算
            if (sampleNum == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

            }
            else
            {
                try
                {

                    if (int.Parse(txt_num.Text.Trim()) != sampleNum)
                    {
                        if (int.Parse(txt_num.Text.Trim()) != 0)
                        {
                            sampleNum = int.Parse(txt_num.Text.Trim());
                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                            return;
                        }
                    }
                    else
                    {
                        int selectnum = 0;
                        string str = "";
                        int M = cb_analysisList.Items.Count;
                        string[] liststr = new string[M + 1 + M * sampleNum];
                        int i = 0;
                        //更新样品单表
                        liststr.SetValue("update t_M_SampleInfor set snum='" + sampleNum + "' where id='" + strSelectedId + "'", i++);
                        foreach (ListItem LI in cb_analysisList.Items)
                        {
                            if (LI.Selected)
                            {
                                selectnum++;
                                //分析项目表
                                str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                                DataSet dscheck = new MyDataOp(str).CreateDataSet();
                                if (dscheck.Tables[0].Rows.Count <= 0)
                                {
                                    str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + LI.Value + "','" + sampleNum + "')";
                                    liststr.SetValue(str, i++);


                                }
                                else
                                {
                                    str = "update t_M_MonitorItem set Num='" + sampleNum + "' where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                    liststr.SetValue(str, i++);
                                }
                                dscheck.Dispose();
                                //string strcheckup = "Delete from t_MonitorItemDetail where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";
                                //liststr.SetValue(strcheckup, i++);
                                for (int j = 0; j < sampleNum; j++)
                                {
                                    string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[j] + "' and MonitorItem='" + LI.Value + "' ";
                                    DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                    DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                    if (seldr.Length <= 0)
                                    {
                                        DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                        if (checkdr.Length <= 0)
                                        {
                                            str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + LI.Value + "','" + namelist[j] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                            liststr.SetValue(str, i++);
                                        }

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                        WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                                DataSet dscheck = new MyDataOp(str).CreateDataSet();
                                if (dscheck.Tables[0].Rows.Count > 0)
                                {
                                    str = "delete from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                    liststr.SetValue(str, i++);
                                    string strcheckup = "Delete from t_MonitorItemDetail where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                    liststr.SetValue(strcheckup, i++);


                                }
                                dscheck.Dispose();
                            }
                        }
                        if (selectnum == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择分析项目！');", true);
                            return;
                        }
                        else
                        {
                            MyDataOp OpObj = new MyDataOp(str);
                            if (OpObj.DoTran(i, liststr))
                            {
                                bool detailret = true;
                                if (txt_own.Text.Trim() != "")
                                {
                                    string strown = txt_own.Text.Trim();
                                    string[] strlist = strown.Split(',', '，');
                                    string[] liststr2 = new string[strlist.Length + 1];
                                    for (int j = 0; j < strlist.Length; j++)
                                    {
                                        if (strlist[j].ToString().Trim() != "")
                                        {
                                            str = "select id,AIName from t_M_AnalysisItem where AIName='" + strlist[j].ToString() + "'";

                                            DataSet dscheck = new MyDataOp(str).CreateDataSet();
                                            if (dscheck.Tables[0].Rows.Count > 0)
                                            {
                                                str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + sampleNum + "')";
                                                liststr2.SetValue(str, i++);
                                                for (int m = 0; m < sampleNum; m++)
                                                {
                                                    string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[m] + "' and MonitorItem='" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "' ";
                                                    DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                                    DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                                    if (seldr.Length <= 0)
                                                    {
                                                        DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                                        if (checkdr.Length <= 0)
                                                        {
                                                            str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + namelist[m] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                                            liststr2.SetValue(str, i++);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                                        WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                str = @"insert into t_M_AnalysisItem
                    (ClassID,AIName,AICode)  
                    values('1','" + strlist[j] + "','" + strlist[j] + "')";
                                                MyDataOp mdo = new MyDataOp(str);
                                                bool blSuccess = mdo.ExecuteCommand();
                                                if (blSuccess == true)
                                                {
                                                    DataSet myDR = new MyDataOp("select id from t_M_AnalysisItem where AIName='" + strlist[j] + "'").CreateDataSet();

                                                    if (myDR.Tables[0].Rows.Count > 0)
                                                    {
                                                        string itemname = myDR.Tables[0].Rows[0]["id"].ToString();
                                                        myDR.Dispose();
                                                        str = @"insert into t_M_MonitorItem
                    (SampleID,MonitorItem,Num,UserID,CreateDate)  
                    values('" + strSelectedId + "','" + itemname + "','" + sampleNum + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate())";
                                                        liststr2.SetValue(str, i++);
                                                        for (int m = 0; m < sampleNum; m++)
                                                        {
                                                            string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[m] + "' and MonitorItem='" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "' ";
                                                            DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                                            DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                                            if (seldr.Length <= 0)
                                                            {
                                                                DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                                                if (checkdr.Length <= 0)
                                                                {
                                                                    str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + namelist[m] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                                                    liststr2.SetValue(str, i++);
                                                                }

                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                                                WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }
                                            }


                                            dscheck.Dispose();
                                        }

                                    }
                                    MyDataOp OpObj2 = new MyDataOp(str);
                                    detailret = OpObj.DoTran(i, liststr);
                                }
                                if (detailret)
                                {
                                    WebApp.Components.Log.SaveLog("样品接收添加分析项目" + strSampleId + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存成功！');", true);
                                    DataBindAll();
                                    txt_own.Text = "";

                                }
                                else
                                {
                                    WebApp.Components.Log.SaveLog("样品接收添加分析项目" + strSampleId + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                    WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                }
                Query();
            }
        }
    }
   
    
    protected void btn_CancelSample_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
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
    #region 分析项目添加
    protected void btn_OKAnalysis_Click(object sender, EventArgs e)
    {
        string retstr = "";
        List<string> namelist = new List<string>();
        namelist = getnum(lbl_sample.Text, ref retstr);
        if (retstr != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + retstr + "');", true);
            return;
        }
        int sampleNum = namelist.Count; ;

        //自动通过样品编号计算
        if (sampleNum == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

        }
        else
        {
            try
            {

                if (int.Parse(txt_num.Text.Trim()) != sampleNum)
                {
                    if (int.Parse(txt_num.Text.Trim()) != 0)
                    {
                        sampleNum = int.Parse(txt_num.Text.Trim());
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                        WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                        return;
                    }
                }
                else
                {
                    int selectnum = 0;
                    string str = "";
                    int M = cb_analysisList.Items.Count;
                    string[] liststr = new string[M + 1 + M * sampleNum];
                    int i = 0;
                    //更新样品单表
                    liststr.SetValue("update t_M_SampleInfor set snum='" + sampleNum + "' where id='" + strSelectedId + "'", i++);
                    foreach (ListItem LI in cb_analysisList.Items)
                    {
                        if (LI.Selected)
                        {
                            selectnum++;
                            //分析项目表
                            str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                            DataSet dscheck = new MyDataOp(str).CreateDataSet();
                            if (dscheck.Tables[0].Rows.Count <= 0)
                            {
                                str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + LI.Value + "','" + sampleNum + "')";
                                liststr.SetValue(str, i++);


                            }
                            else
                            {
                                str = "update t_M_MonitorItem set Num='" + sampleNum + "' where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                liststr.SetValue(str, i++);
                            }
                            dscheck.Dispose();
                            //string strcheckup = "Delete from t_MonitorItemDetail where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";
                            //liststr.SetValue(strcheckup, i++);
                            for (int j = 0; j < sampleNum; j++)
                            {
                                string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[j] + "' and MonitorItem='" + LI.Value + "' ";
                                DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                if (seldr.Length <= 0)
                                {
                                    DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                    if (checkdr.Length <= 0)
                                    {
                                        str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + LI.Value + "','" + namelist[j] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                        liststr.SetValue(str, i++);
                                    }

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                    WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                            DataSet dscheck = new MyDataOp(str).CreateDataSet();
                            if (dscheck.Tables[0].Rows.Count > 0)
                            {
                                str = "delete from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                liststr.SetValue(str, i++);
                                string strcheckup = "Delete from t_MonitorItemDetail where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                liststr.SetValue(strcheckup, i++);


                            }
                            dscheck.Dispose();
                        }
                    }
                    if (selectnum == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择分析项目！');", true);
                        return;
                    }
                    else
                    {
                        MyDataOp OpObj = new MyDataOp(str);
                        if (OpObj.DoTran(i, liststr))
                        {
                            bool detailret = true;
                            if (txt_own.Text.Trim() != "")
                            {
                                string strown = txt_own.Text.Trim();
                                string[] strlist = strown.Split(',', '，');
                                string[] liststr2 = new string[strlist.Length*sampleNum + 1];
                                for (int j = 0; j < strlist.Length; j++)
                                {
                                    if (strlist[j].ToString().Trim() != "")
                                    {
                                        str = "select id,AIName from t_M_AnalysisItem where AIName='" + strlist[j].ToString() + "'";

                                        DataSet dscheck = new MyDataOp(str).CreateDataSet();
                                        if (dscheck.Tables[0].Rows.Count > 0)
                                        {
                                            str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + sampleNum + "')";
                                            liststr2.SetValue(str, i++);
                                            for (int m = 0; m < sampleNum; m++)
                                            {
                                                string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[m] + "' and MonitorItem='" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "' ";
                                                DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                                DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                                if (seldr.Length <= 0)
                                                {
                                                    DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                                    if (checkdr.Length <= 0)
                                                    {
                                                        str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + namelist[m] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                                        liststr2.SetValue(str, i++);
                                                    }

                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                                    WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            str = @"insert into t_M_AnalysisItem
                    (ClassID,AIName,AICode)  
                    values('1','" + strlist[j] + "','" + strlist[j] + "')";
                                            MyDataOp mdo = new MyDataOp(str);
                                            bool blSuccess = mdo.ExecuteCommand();
                                            if (blSuccess == true)
                                            {
                                                DataSet myDR = new MyDataOp("select id from t_M_AnalysisItem where AIName='" + strlist[j] + "'").CreateDataSet();

                                                if (myDR.Tables[0].Rows.Count > 0)
                                                {
                                                    string itemname = myDR.Tables[0].Rows[0]["id"].ToString();
                                                    myDR.Dispose();
                                                    str = @"insert into t_M_MonitorItem
                    (SampleID,MonitorItem,Num,UserID,CreateDate)  
                    values('" + strSelectedId + "','" + itemname + "','" + sampleNum + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate())";
                                                    liststr2.SetValue(str, i++);
                                                    for (int m = 0; m < sampleNum; m++)
                                                    {
                                                        string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[m] + "' and MonitorItem='" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "' ";
                                                        DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                                        DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                                        if (seldr.Length <= 0)
                                                        {
                                                            DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                                            if (checkdr.Length <= 0)
                                                            {
                                                                str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + namelist[m] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                                                liststr2.SetValue(str, i++);
                                                            }

                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                                            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                                            return;
                                                        }
                                                    }
                                                   
                                                }
                                            }
                                        }


                                        dscheck.Dispose();
                                    }

                                }
                                MyDataOp OpObj2 = new MyDataOp(str);
                                detailret = OpObj.DoTran(i, liststr);
                            }
                            if (detailret)
                            {
                                WebApp.Components.Log.SaveLog("样品接收添加分析项目" + strSampleId + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存成功！');", true);
                                DataBindAll();
                                txt_own.Text = "";

                            }
                            else
                            {
                                WebApp.Components.Log.SaveLog("样品接收添加分析项目" + strSampleId + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
                            }
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            }
            Query();
        }


    }


   
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

    #endregion
}
