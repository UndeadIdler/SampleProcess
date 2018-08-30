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
using System.Data.OleDb;

public partial class SampleData : System.Web.UI.Page
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
    private bool havedata//所选择操作列记录对应的id
    {
        get { return (bool)ViewState["havedata"]; }
        set { ViewState["havedata"] = value; }
    }
    private  DataTable expds//所选择操作列记录对应的id
    {
        get { return (DataTable)ViewState["expds"]; }
        set { ViewState["expds"] = value; }
    }
    private DataSet ds//所选择操作列记录对应的id
    {
        get { return (DataSet)ViewState["ds"]; }
        set { ViewState["ds"] = value; }
    }
     public DataSet dsitem_ZK
    {
        get { return (DataSet)ViewState["dsitem_ZK"]; }
        set { ViewState["dsitem_ZK"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            Int();
            Query();
        }
    }
    private void Int()
    {
       expds=new DataTable();
       
              DataColumn dc1=new DataColumn("监测项");
        expds.Columns.Add(dc1);
         DataColumn dc2=new DataColumn("样品编号");
        expds.Columns.Add(dc2);
        DataColumn dc4 = new DataColumn("采样点");
        expds.Columns.Add(dc4);
         DataColumn dc3=new DataColumn("采样时间");
        expds.Columns.Add(dc3);
        
         DataColumn dc11 = new DataColumn("执行标准");
         expds.Columns.Add(dc11);
         DataColumn dc12 = new DataColumn("标准值");
         expds.Columns.Add(dc12);
      
         DataColumn dc5=new DataColumn("接样时间");
        expds.Columns.Add(dc5);
         DataColumn dc6=new DataColumn("样品类型");
        expds.Columns.Add(dc6);
         DataColumn dc7=new DataColumn("样品性状");
        expds.Columns.Add(dc7);
         DataColumn dc8=new DataColumn("项目名称");
        expds.Columns.Add(dc8);
         DataColumn dc9=new DataColumn("项目负责人");
        expds.Columns.Add(dc9);
         DataColumn dc10=new DataColumn("紧急程度");
        expds.Columns.Add(dc10);
        DataColumn dc14 = new DataColumn("分析指标");
        expds.Columns.Add(dc14);
        DataColumn dc13 = new DataColumn("是否走绿色通道");
        expds.Columns.Add(dc13);
        DataColumn dc15 = new DataColumn("备注");
        expds.Columns.Add(dc15);
       
           
    }

    private void Query()
    {
        BindAnalysis();
        #region 质控
        BindZK();
 
        #endregion
    }
    private void BindAnalysis()
    {
        string constr = "";
        if (txt_Itemquery.Text.Trim() != "")
            constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or t_M_ANItemInf.AICode like '%" + txt_Itemquery.Text.Trim() + "%')";
        if (txt_QueryTime.Text.Trim() != "")
            constr += " and  (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";

        string strSql = "select t_MonitorItemDetail.id, t_MonitorItemDetail.fxDanID,t_MonitorItemDetail.MonitorItem,t_M_ANItemInf.AIName 监测项,t_M_SampleInfor.SampleID AS 样品编号,t_M_ReporInfo.Ulevel ,t_M_SampleInfor.SampleAddress 采样点 , CONVERT(VARCHAR(10),t_M_SampleInfor.SampleDate,120) AS 采样时间," +
      " CONVERT(VARCHAR(10),t_M_SampleInfor.AccessDate,120) AS 接样时间, " +
      " t_M_SampleInfor.TypeID, t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, t_M_ReporInfo.ProjectName 项目名称," +
     " t_M_ReporInfo.chargeman 项目负责人,t_MonitorItemDetail.experimentvalue,t_MonitorItemDetail.Method,t_M_SampleInfor.zxbz  " +
",t_M_ReporInfo.Green,t_MonitorItemDetail.Remark 备注,t_M_SampleInfor.SampleSource FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
     " t_M_AnalysisMainClassEx ON " +
    "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id and t_MonitorItemDetail.delflag=0  inner join t_M_ANItemInf on t_M_ANItemInf.ID=t_MonitorItemDetail.MonitorItem where t_MonitorItemDetail.flag=2 and t_M_SampleInfor.StatusID=1 and t_MonitorItemDetail.LyUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' " + constr + " ORDER BY t_M_ANItemInf.orderid,t_M_ANItemInf.AIName,t_M_SampleInfor.AccessDate,t_M_SampleInfor.SampleID";

        ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dcbz = new DataColumn("执行标准");
        ds.Tables[0].Columns.Add(dcbz);
        DataColumn dcbz1 = new DataColumn("标准值");
        ds.Tables[0].Columns.Add(dcbz1);

        DataColumn dcbz2 = new DataColumn("分析指标");
        ds.Tables[0].Columns.Add(dcbz2);
        DataColumn dcGreen = new DataColumn("是否走绿色通道");
        ds.Tables[0].Columns.Add(dcGreen);
        string str = "select * from t_R_UserInfo";
        DataSet dsuser = new MyDataOp(str).CreateDataSet();
        string strbz = "select t_hyClassParam.id,t_标准字典.bz,t_hyItem.itemid,t_hyItem.fw,单位全称,t_CompabyBZ.qyid from  t_CompabyBZ inner join t_hyClassParam on t_hyClassParam.id=t_CompabyBZ.bzid inner join t_标准字典 on t_标准字典.id=t_hyClassParam.bz inner join t_hyItem on t_hyItem.pid=t_hyClassParam.id inner join T_委托单位 on T_委托单位.id=t_CompabyBZ.qyid where t_CompabyBZ.flag=0";
        // string strbz = "select t_hyClassParam.id,t_标准字典.bz,t_hyItem.itemid,t_hyItem.fw from  t_CompabyBZ inner join t_hyClassParam on t_hyClassParam.id=t_CompabyBZ.bzid inner join t_标准字典 on t_标准字典.id=t_hyClassParam.bz inner join t_hyItem on t_hyItem.pid=t_hyClassParam.id where t_CompabyBZ.flag=0";
        DataSet dsbz = new MyDataOp(strbz).CreateDataSet();
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
            try
            {
                string zxstr = "";
                if (dr["zxbz"].ToString() != "")
                    zxstr = " and id='" + dr["zxbz"].ToString() + "'";
                DataRow[] drbz = dsbz.Tables[0].Select("单位全称='" + dr["SampleSource"].ToString() + "' and itemid='" + dr["MonitorItem"].ToString() + "'" + zxstr);
                if (drbz.Length > 0)
                {
                    dr["执行标准"] = drbz[0]["bz"].ToString().Trim();
                    dr["标准值"] = drbz[0]["fw"].ToString().Trim();
                }
            }
            catch { }
            string getitemstr = "select AIName,MonitorItem from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem  where  SampleID='" + dr["样品编号"].ToString() + "' and delflag=0";
            DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
            if (dsitem != null && dsitem.Tables.Count > 0)
            {
                foreach (DataRow drr in dsitem.Tables[0].Rows)
                {
                    dr["分析指标"] += drr[0].ToString() + ";";
                    //dr["分析项目编码"] += drr[1].ToString() + ",";
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

    }
    private void BindZK()
    {
        //      string strSql_zk = @"select distinct t_MonitorItemDetail.MonitorItem,t_M_ANItemInf.AIName 监测项,Name FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
        // " t_M_AnalysisMainClassEx ON " +
        //"  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.SampleID and t_MonitorItemDetail.delflag=0  inner join t_M_ANItemInf on t_M_ANItemInf.ID=t_MonitorItemDetail.MonitorItem inner join t_R_UserInfo on t_R_UserInfo.UserID=t_MonitorItemDetail.LyUser where t_MonitorItemDetail.flag=2 and t_M_SampleInfor.StatusID=1 and t_MonitorItemDetail.LyUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' " + constr + " ";
      string  constr = "";
        //if (txt_QueryTime.Text.Trim() != "")
        //    constr += " and  (year(createdate)= '" + DateTime.Now.Year + "' AND month(createdate)= '" + DateTime.Now.Month + "' and day(createdate)= '" + DateTime.Now.Day + "')";
   
        //    //constr += " and  (year(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(createdate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        //else
            constr += " and  (year(createdate)= '" + DateTime.Now.Year + "' AND month(createdate)= '" + DateTime.Now.Month + "' and day(createdate)= '" + DateTime.Now.Day + "')";


        string strSql_zk = @"select t_zkanalysisinfo.id, t_M_ANItemInf.id itemid,AIName,Name,analysisnum, scenejcnum, scenehgnum, experimentjcnum, experimenthgnum, jbhsjcnum, jbhshgnum, alljcnum, allhgnum, 
                      mmjcnum, mmhgnum, byjcnum, byhgnum, amount
 from t_zkanalysisinfo inner join t_M_ANItemInf on t_M_ANItemInf.id=t_zkanalysisinfo.itemid inner join t_R_UserInfo on t_R_UserInfo.UserID=t_zkanalysisinfo.userid where t_zkanalysisinfo.userid='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'" + constr+" order by createdate";

        dsitem_ZK = new MyDataOp(strSql_zk).CreateDataSet();

        //DataTable dt = new DataTable();
        //DataColumn dc1 = new DataColumn("MonitorItem");
        //DataColumn dc2 = new DataColumn("监测项");
        //dt.Columns.Add(dc1);
        //dt.Columns.Add(dc2);

       
       
        if (dsitem_ZK.Tables[0].Rows.Count == 0)
        {
            havedata = false;
            //没有记录仍保留表头
            dsitem_ZK.Tables[0].Rows.Add(dsitem_ZK.Tables[0].NewRow());
            grdvw_ZKList.DataSource = dsitem_ZK;
            grdvw_ZKList.DataBind();
            int intColumnCount = grdvw_ZKList.Rows[0].Cells.Count;
            grdvw_ZKList.Rows[0].Cells.Clear();
            grdvw_ZKList.Rows[0].Cells.Add(new TableCell());
            grdvw_ZKList.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            havedata = true;
            grdvw_ZKList.DataSource = dsitem_ZK;
            grdvw_ZKList.DataBind();

        }
       
       // dsitem_ZK.Dispose();
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
            //cbl.Text = "全选";
            cbl.CheckedChanged+=cbl_CheckedChanged;
            cbl.AutoPostBack = true;
            cbl.TabIndex = 1000;
            e.Row.Cells[0].Controls.Add(cbl);
            e.Row.Cells[0].Width =20;

            TableCell headerValue = new TableCell();
            headerValue.Text = "分析值";
            headerValue.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerValue.Width = 120;
            e.Row.Cells.Add(headerValue);
          
            TableCell headerMethod = new TableCell();
            headerMethod.Text = "分析方法";
            headerMethod.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerMethod.Width =400;
            e.Row.Cells.Add(headerMethod);
            //TableCell headerRemark= new TableCell();

            //headerRemark.Text = "备注";
            //headerRemark.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerRemark.Width = 60;
            //e.Row.Cells.Add(headerRemark);

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "保存";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 50;
            e.Row.Cells.Add(headerDetail);
            TableCell headerDetail1 = new TableCell();
            headerDetail1.Text = "提交";
            headerDetail1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail1.Width = 50;
            e.Row.Cells.Add(headerDetail1);

            //TableCell headerBack= new TableCell();
            //headerBack.Text = "退回";
            //headerBack.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerBack.Width = 50;
            //e.Row.Cells.Add(headerBack);
         
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = e.Row.RowIndex + 1;
            CheckBox cbl=new CheckBox();
            cbl.ID="cbl";
            cbl.Text = "";
            e.Row.Cells[0].Controls.Add(cbl);
            cbl.TabIndex = 1000; ;
            //手动添加详细和删除按钮-保存
            TableCell tabcValue = new TableCell();
            tabcValue.Width = 120;
            tabcValue.Style.Add("text-align", "center");
            TextBox txt_Value = new TextBox();
            txt_Value.ID = "txt_value";
            txt_Value.Width = 120;
            tabcValue.Controls.Add(txt_Value);
            e.Row.Cells.Add(tabcValue);
            txt_Value.TabIndex = 0;
            //手动添加详细和删除按钮-保存
            TableCell tabcMethod= new TableCell();
            tabcMethod.Width =400;
            tabcMethod.Style.Add("text-align", "center");
            RadioButtonList rbl_Method = new RadioButtonList();
            rbl_Method.ID = "rbl_Method";
            //rbl_Method.Width = 70;
            rbl_Method.RepeatDirection = RepeatDirection.Vertical;
            tabcMethod.Controls.Add(rbl_Method);
            e.Row.Cells.Add(tabcMethod);
            rbl_Method.TabIndex = 1000; ;
            ////手动添加详细和删除按钮-保存
            //TableCell tabcRemark = new TableCell();
            //tabcRemark.Width = 60;
            //tabcRemark.Style.Add("text-align", "center");
            //TextBox txt_Remark = new TextBox();
            //txt_Remark.ID = "txt_Remark";
            //txt_Remark.Height = 60;
            //txt_Remark.TextMode = TextBoxMode.MultiLine;
            //tabcRemark.Controls.Add(txt_Remark);
            //e.Row.Cells.Add(tabcRemark);
            //手动添加详细和删除按钮-保存
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 50;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ID = "Draw";
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
            ibtnDetail.TabIndex = 1000; ;
            ibtnDetail.CommandName = "Select";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            //手动添加详细和删除按钮-提交
            TableCell tabcDetail1 = new TableCell();
            tabcDetail1.Width = 50;
            tabcDetail1.Style.Add("text-align", "center");
            ImageButton ibtnDetail1 = new ImageButton();
            ibtnDetail1.ImageUrl = "~/images/Detail.gif";
            ibtnDetail1.TabIndex = 1000; ;
            ibtnDetail1.CommandName = "Edit";
           ibtnDetail1.Attributes.Add("OnClick", "if(!confirm('确定提交该项吗？')) return false;");
            tabcDetail1.Controls.Add(ibtnDetail1);
            e.Row.Cells.Add(tabcDetail1);

            ////手动添加详细和删除按钮-退回
            //TableCell tabcback = new TableCell();
            //tabcback.Width = 50;
            //tabcback.Style.Add("text-align", "center");
            //ImageButton ibtnback = new ImageButton();
            //ibtnback.ImageUrl = "~/images/Detail.gif";

            //ibtnback.CommandName = "Delete";
            //ibtnback.Attributes.Add("OnClick", "if(!confirm('确定退回该项吗？')) return false;");
            //tabcback.Controls.Add(ibtnback);
            //e.Row.Cells.Add(tabcback);
           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[18].Visible = false;
            for (int i = 0; i < cbl_choose.Items.Count; i++)
            {
                if (!cbl_choose.Items[i].Selected)
                {
                    try
                    {
                        int p = int.Parse(cbl_choose.Items[i].Value);
                        e.Row.Cells[p].Visible = false;
                    }
                    catch
                    { }
                }
            }
        }
    }

    protected void grdvw_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        List<Entity.SampleItem> entitylist = new List<Entity.SampleItem>();
        Entity.SampleItem entity = new Entity.SampleItem();
        entity.ID = int.Parse(grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().ToString());//监测项记录ID
        entity.lyID = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().ToString();//监测项记录ID
        //分析值
        TextBox txt_Value = grdvw_List.Rows[e.NewSelectedIndex].Cells[25].FindControl("txt_Value") as TextBox;
        if (txt_Value.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析值不能为空！')", true);
            return;
        }
        entity.Value = txt_Value.Text.Trim();
        ////分析备注
        //TextBox txt_Remark = grdvw_List.Rows[e.NewSelectedIndex].Cells[21].FindControl("txt_Remark") as TextBox;

        //entity.Remark = txt_Remark.Text.Trim();
        RadioButtonList rbl_Method = grdvw_List.Rows[e.NewSelectedIndex].Cells[26].FindControl("rbl_Method") as RadioButtonList;
        entity.Method = rbl_Method.SelectedValue;
        //分析人
        entity.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        //分析数据登记时间
        entity.AnalysisDate = DateTime.Now;
        entitylist.Add(entity);
        DAl.DrawSample itemObj = new DAl.DrawSample();
        if (itemObj.DataSave(entitylist) == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析数据保存成功！')", true);
            foreach (GridViewRow gvr in grdvw_List.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    int itemid = int.Parse(gvr.Cells[1].Text.Trim().ToString());
                    DataRow[] dr = ds.Tables[0].Select("id='" + itemid + "'");
                    if (dr.Length == 1)
                    {
                        TextBox txt_Value0 = gvr.Cells[25].FindControl("txt_Value") as TextBox;
                        if (txt_Value0.Text.Trim() != "")
                        {
                            RadioButtonList rbl_Method0 = gvr.Cells[26].FindControl("rbl_Method") as RadioButtonList;
                            dr[0]["experimentvalue"] = txt_Value0.Text;
                            dr[0]["Method"] = rbl_Method0.SelectedValue.ToString();
                        }
                    }
                    ds.AcceptChanges();
                }

            }

            //DataRow[] drsel = ds.Tables[0].Select("id='" + entity.ID + "'");
            //if (drsel.Length == 1)
            //{
            //    ds.Tables[0].Rows.Remove(drsel[0]);

            //}
            ds.AcceptChanges();
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
            BindZK();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析数据保存失败！')", true);
        }

    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<Entity.SampleItem> entitylist = new List<Entity.SampleItem>();
        Entity.SampleItem entity = new Entity.SampleItem();
        entity.ID = int.Parse(grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim().ToString());//监测项记录ID
        entity.lyID = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim().ToString();//监测项记录ID

        //分析值
        TextBox txt_Value = grdvw_List.Rows[e.NewEditIndex].Cells[25].FindControl("txt_Value") as TextBox;

        if (txt_Value.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析值不能为空！')", true);
            return;
        }
        DataRow[] drselflag = dsitem_ZK.Tables[0].Select("itemid='" + grdvw_List.Rows[e.NewEditIndex].Cells[3].Text.Trim().ToString() + "'");
        if (drselflag.Length == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请先登记质控记录再提交！')", true);
            return;
        }
        entity.Value = txt_Value.Text.Trim();
        //分析备注
        //TextBox txt_Remark = grdvw_List.Rows[e.NewEditIndex].Cells[21].FindControl("txt_Remark") as TextBox;

        entity.Remark = "";// txt_Remark.Text.Trim();
        RadioButtonList rbl_Method = grdvw_List.Rows[e.NewEditIndex].Cells[26].FindControl("rbl_Method") as RadioButtonList;
        entity.Method = rbl_Method.SelectedValue;
        //分析人
        entity.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        //分析数据登记时间
        entity.AnalysisDate = DateTime.Now;
        entitylist.Add(entity);
        DAl.DrawSample itemObj = new DAl.DrawSample();
        if (itemObj.DataSubmit(entitylist) == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析数据提交成功！')", true);
            foreach (GridViewRow gvr in grdvw_List.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    int itemid = int.Parse(gvr.Cells[1].Text.Trim().ToString());
                    DataRow[] dr = ds.Tables[0].Select("id='" + itemid + "'");
                    if (dr.Length == 1)
                    {
                        TextBox txt_Value0 = gvr.Cells[25].FindControl("txt_Value") as TextBox;
                        if (txt_Value0.Text.Trim() != "")
                        {
                            RadioButtonList rbl_Method0 = gvr.Cells[26].FindControl("rbl_Method") as RadioButtonList;
                            dr[0]["experimentvalue"] = txt_Value0.Text;
                            dr[0]["Method"] = rbl_Method0.SelectedValue.ToString();
                        }
                    }
                    ds.AcceptChanges();
                }

            }

            DataRow[] drsel = ds.Tables[0].Select("id='" + entity.ID + "'");
            if (drsel.Length == 1)
            {
                ds.Tables[0].Rows.Remove(drsel[0]);

            }
            ds.AcceptChanges();
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析数据提交失败！')", true);
        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<Entity.SampleItem> entitylist = new List<Entity.SampleItem>();
        Entity.SampleItem entity = new Entity.SampleItem();
        entity.ID = int.Parse(grdvw_List.Rows[e.RowIndex].Cells[1].Text.Trim().ToString());//监测项记录ID
        entity.lyID = grdvw_List.Rows[e.RowIndex].Cells[1].Text.Trim().ToString();//监测项记录ID
        //分析值
        TextBox txt_Value = grdvw_List.Rows[e.RowIndex].Cells[25].FindControl("txt_Value") as TextBox;
        entity.Value = txt_Value.Text.Trim();
        ////分析备注
        //TextBox txt_Remark = grdvw_List.Rows[e.RowIndex].Cells[26].FindControl("txt_Remark") as TextBox;
        if (txt_Value.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写退回备注！')", true);
            return;
        }
        entity.Remark = "";// txt_Remark.Text.Trim();
        RadioButtonList rbl_Method = grdvw_List.Rows[e.RowIndex].Cells[26].FindControl("rbl_Method") as RadioButtonList;
        entity.Method = rbl_Method.SelectedValue;
        //分析人
        entity.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        //分析数据登记时间
        entity.AnalysisDate = DateTime.Now;
        entitylist.Add(entity);
        DAl.DrawSample itemObj = new DAl.DrawSample();
        if (itemObj.DataBack(entitylist) == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('监测项申领退回成功！')", true);
            Query();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('监测项申领退回失败！')", true);
        }

    }
   



    protected void grdvw_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //初始化checkboxlist
            TextBox txt_Value = (TextBox)e.Row.FindControl("txt_Value");
            TextBox txt_Remark = (TextBox)e.Row.FindControl("txt_Remark");
            RadioButtonList rbl_Method = e.Row.FindControl("rbl_Method") as RadioButtonList;
            if (e.Row.Cells[15].Text.Trim() != "&nbsp;")
            {
                txt_Value.Text = e.Row.Cells[15].Text.Trim();
            }
            else
            {
                txt_Value.Text = "";
            }

            if (e.Row.Cells[3].Text.Trim() != "&nbsp;")
            {
                BindCheckBoxList(rbl_Method, e.Row.Cells[3].Text.Trim(), e.Row.Cells[10].Text.Trim());
            }
            if (e.Row.Cells[16].Text.Trim() != "&nbsp;")
                rbl_Method.SelectedValue = e.Row.Cells[16].Text.Trim();
            else
                rbl_Method.SelectedIndex = 0;

        }
    }
    #endregion
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
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        List<Entity.SampleItem> DrawList = new List<Entity.SampleItem>();
       
            foreach (GridViewRow gvr in grdvw_List.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbl = gvr.Cells[0].FindControl("cbl") as CheckBox;
                    Entity.SampleItem entity = new Entity.SampleItem();
                    if (cbl.Checked)
                    {


                        entity.ID = int.Parse(gvr.Cells[1].Text.Trim().ToString());//监测项记录ID
                        entity.lyID = gvr.Cells[1].Text.Trim().ToString();//领用ID


                        //分析值
                        TextBox txt_Value = gvr.Cells[25].FindControl("txt_Value") as TextBox;
                        RadioButtonList rbl_Method = gvr.Cells[26].FindControl("rbl_Method") as RadioButtonList;
                        if (txt_Value.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析值不能为空！')", true);
                            return;
                        }
                        entity.Value = txt_Value.Text.Trim();
                      
                       
                        entity.Method = rbl_Method.SelectedValue;


                        //分析人
                        entity.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
                        //分析数据登记时间
                        entity.AnalysisDate = DateTime.Now;
                        DrawList.Add(entity);
                        
                    }
                   
                   
                }

        }
            if (DrawList.Count > 0)
            {
                DAl.DrawSample drawobj = new DAl.DrawSample();
                if (drawobj.DataSave(DrawList) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！')", true);
                    Query();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存失败！')", true);
                }
               

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要保存的监测项！')", true);
                Query();
            }
       
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        List<Entity.SampleItem> DrawList = new List<Entity.SampleItem>();

        foreach (GridViewRow gvr in grdvw_List.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbl = gvr.Cells[0].FindControl("cbl") as CheckBox;
                if (cbl.Checked)
                {
                    Entity.SampleItem entity = new Entity.SampleItem();
                    entity.ID = int.Parse(gvr.Cells[1].Text.Trim().ToString());//监测项记录ID
                    entity.lyID = gvr.Cells[1].Text.Trim().ToString();//监测项记录ID
                    //分析值
                    TextBox txt_Value = gvr.Cells[25].FindControl("txt_Value") as TextBox;
                    if (txt_Value.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析值不能为空！')", true);
                        return;
                    }
                    entity.Value = txt_Value.Text.Trim();
                    //分析备注
                   // TextBox txt_Remark = gvr.Cells[26].FindControl("txt_Remark") as TextBox;

                    entity.Remark = "";// txt_Remark.Text.Trim();
                    RadioButtonList rbl_Method = gvr.Cells[26].FindControl("rbl_Method") as RadioButtonList;
                    entity.Method = rbl_Method.SelectedValue;
                    //分析人
                    entity.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
                    //分析数据登记时间
                    entity.AnalysisDate = DateTime.Now;
                    DrawList.Add(entity);
                }

            }

        }
        if (DrawList.Count > 0)
        {
            DAl.DrawSample drawobj = new DAl.DrawSample();
            if (drawobj.DataBack(DrawList) > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('退回保存成功！')", true);
                Query();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('退回保存失败！')", true);
            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要登记的监测项！')", true);
            Query();
        }

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {

        expds.Clear();

        foreach (GridViewRow gvr in grdvw_List.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                if (!cbl_check.Checked)
                {
                    if (gvr.Cells[15].Text.Trim() == "&nbsp;")
                    {
                        DataRow dr = expds.NewRow();
                        dr["监测项"] = gvr.Cells[4].Text.Trim().ToString();//监测项
                        dr["样品编号"] = gvr.Cells[5].Text.Trim();//样品编号
                        dr["采样时间"] = gvr.Cells[8].Text.Trim();//采样时间
                        dr["采样点"] = gvr.Cells[7].Text.Trim();//采样点
                        dr["接样时间"] = gvr.Cells[9].Text.Trim();//接样时间
                        dr["样品类型"] = gvr.Cells[11].Text.Trim();//样品类型 
                        dr["样品性状"] = gvr.Cells[12].Text.Trim();//样品性状  
                        dr["项目名称"] = gvr.Cells[13].Text.Trim();//项目名称  
                        dr["项目负责人"] = gvr.Cells[14].Text.Trim();//项目负责人
                        dr["备注"] = gvr.Cells[19].Text.Trim();//紧急程度
                        dr["紧急程度"] = gvr.Cells[21].Text.Trim();//紧急程度
                        dr["执行标准"] = gvr.Cells[22].Text.Trim();//执行标准
                        dr["标准值"] = gvr.Cells[23].Text.Trim();//标准值
                        if(gvr.Cells[18].Text.Trim()=="1")
                        dr["是否走绿色通道"] ="是";//是否走绿色通道
                        else
                            dr["是否走绿色通道"] = "否";
                        dr["分析指标"] = gvr.Cells[24].Text.Trim();//紧急程度
                        expds.Rows.Add(dr);
                        expds.AcceptChanges();
                    }
                }
                else
                {
                    CheckBox cbl = gvr.Cells[0].FindControl("cbl") as CheckBox;
                    if (cbl.Checked)
                    {
                        DataRow dr = expds.NewRow();
                        dr["监测项"] = gvr.Cells[4].Text.Trim().ToString();//监测项
                        dr["样品编号"] = gvr.Cells[5].Text.Trim();//样品编号
                        dr["采样时间"] = gvr.Cells[7].Text.Trim();//采样时间
                        dr["采样点"] = gvr.Cells[8].Text.Trim();//采样点
                        dr["接样时间"] = gvr.Cells[9].Text.Trim();//接样时间
                        dr["样品类型"] = gvr.Cells[11].Text.Trim();//样品类型 
                        dr["样品性状"] = gvr.Cells[12].Text.Trim();//样品性状  
                        dr["项目名称"] = gvr.Cells[13].Text.Trim();//项目名称  
                        dr["项目负责人"] = gvr.Cells[14].Text.Trim();//项目负责人
                        dr["备注"] = gvr.Cells[19].Text.Trim();//紧急程度
                        dr["紧急程度"] = gvr.Cells[21].Text.Trim();//紧急程度
                        dr["执行标准"] = gvr.Cells[22].Text.Trim();//执行标准
                        dr["标准值"] = gvr.Cells[23].Text.Trim();//标准值
                        if (gvr.Cells[18].Text.Trim() == "1")
                            dr["是否走绿色通道"] = "是";//是否走绿色通道
                        else
                            dr["是否走绿色通道"] = "否";
                        dr["分析指标"] = gvr.Cells[24].Text.Trim();//紧急程度
                        expds.Rows.Add(dr);
                        expds.AcceptChanges();
                    }
                }

            }

        }
        if (expds.Rows.Count > 0)
        {
            Random r=new Random();
            string fileName=r.Next().ToString()+".xls";
            string url = ConfigurationManager.AppSettings["path1"].ToString() + "Excel.xls";
            
            btn_Export_Click(expds, fileName);
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的监测项！')", true);
            Query();
        }
       
    }
    protected void btn_Export_Click(DataTable dt, string strFileName)
    {
        Microsoft.Office.Interop.Owc11.SpreadsheetClass xlSheet = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
        xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, dt.Columns.Count]).set_MergeCells(true);
        xlSheet.ActiveSheet.Cells[1, 1] = "分析任务单";

        int col=1;
        foreach (DataColumn dc in dt.Columns)
        { 
          
            xlSheet.ActiveSheet.Cells[2, col++] = dc.ColumnName.ToString(); 
        }


        for (int i = 0; i < dt.Rows.Count; i++)
        {

            int j = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                if (dt.Rows[i][dc.ColumnName].ToString() != "&nbsp;")
                {
                    xlSheet.ActiveSheet.Cells[3 + i, j] = dt.Rows[i][dc.ColumnName].ToString();
                      if(dc.ColumnName.Trim()=="标准值")
                          xlSheet.ActiveSheet.Cells[3 + i, j] = "'"+dt.Rows[i][dc.ColumnName].ToString();
                }

                j++;
            }
        }
        xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).Borders.set_LineStyle(Microsoft.Office.Interop.Owc11.XlLineStyle.xlContinuous);
        xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignCenter);
 
        try
        {
            xlSheet.Export(Server.MapPath(".") + "\\" + strFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.location.href='ShowXls.aspx?file_name=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "';", true);
            RemoveFiles(Server.MapPath("."));
        }
        catch
        {
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


    static public void TableToExcel(string excel, System.Data.DataTable tb, string tbname)     
    {           
        //if (System.IO.File.Exists(excel) == true)   
        //{                
        //    System.IO.File.Delete(excel); 
        //}           
        try        
        {
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + excel + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
            OleDbConnection connex = new OleDbConnection(strCon); //获取到excel文件的oledb连接       

          
            string ct = "CREATE TABLE " + tbname + " (";      //以下生成一个sql命令向excel中插入一个表          
            foreach (DataColumn clmn in tb.Columns)       
            {                    
                switch (clmn.DataType.Name)     
                    //根据不同数据类型分别处理     
                {                     
                    case "Decimal":    
                        ct += clmn.ColumnName + " Decimal,";         
                        break;           
                    case "Double":     
                        ct += clmn.ColumnName + " Double,";        
                        break;   
                    default:    
                        ct += clmn.ColumnName + " string,";
                        break;               
                }            
            }               
            ct = ct.Substring(0, ct.Length - 1) + ")";
            connex.Open(); 
            OleDbCommand cmd1 = new OleDbCommand(ct, connex);        
            cmd1.ExecuteNonQuery(); //向excel中插入一个表     
            foreach (DataRow r in tb.Rows)       
                //下面向excel中一行一行写入数据 
                {                   
                string fs = "", vs = "";   
                foreach (DataColumn clmn in tb.Columns)       
                {                     
                    fs += clmn.ColumnName + ",";    
                    if (r[clmn.ColumnName] == DBNull.Value)     
                    {                         
                        vs += "null,";       
                        continue;         
                    }              
                    switch (clmn.DataType.Name)     
                        //根据不同数据类型分别处理    
                    {                     
                        case "Decimal":   
                            vs += ((decimal)r[clmn.ColumnName]).ToString("0.00") + ",";  
                            break;                          
                        case "Double":      
                            vs += ((double)r[clmn.ColumnName]).ToString("0.00") + ",";     
                            break;                     
                        case "DateTime":      
                            vs += "'" + ((DateTime)r[clmn.ColumnName]).ToShortDateString() + "',";  
                            break;                   
                        default:     
                                vs += "'" + r[clmn.ColumnName].ToString() + "',";  
                                break;                       
                    }                 
                }             
                string sqlstr = "insert into [" + tbname + "$] (" + fs.Substring(0, fs.Length - 1) + ") values (" + vs.Substring(0, vs.Length - 1) + ")"; 
                OleDbCommand cmd = new OleDbCommand(sqlstr, connex);        
                cmd.ExecuteNonQuery();              
                //向excel中插入数据              
            }               
            connex.Close();   
        }          
        catch (Exception e)   
        {             
            throw new Exception(e.Message);  
        }    
    }
    public void CreateExcel(DataSet ds, string FileName)
    {
        HttpResponse resp;
        resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
        string colHeaders = "", ls_item = "";

        //定义表对象与行对象，同时用DataSet对其值进行初始化 
        DataTable dt = ds.Tables[0];
        DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
        int i = 0;
        int cl = dt.Columns.Count;


        //取得数据表各列标题，各标题之间以/t分割，最后一个列标题后加回车符 
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))//最后一列，加/n
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "/n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "/t";
            }

        }
        resp.Write(colHeaders);
        //向HTTP输出流中写入取得的数据信息 

        //逐行处理数据   
        foreach (DataRow row in myRow)
        {
            //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加/n
                {
                    ls_item += row[i].ToString() + "/n";
                }
                else
                {
                    ls_item += row[i].ToString() + "/t";
                }

            }
            resp.Write(ls_item);
            ls_item = "";

        }
        resp.End();
    } 



    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        List<Entity.SampleItem> DrawList = new List<Entity.SampleItem>();

        foreach (GridViewRow gvr in grdvw_List.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbl = gvr.Cells[0].FindControl("cbl") as CheckBox;
                if (cbl.Checked)
                {
                    Entity.SampleItem entity = new Entity.SampleItem();
                    entity.ID = int.Parse(gvr.Cells[1].Text.Trim().ToString());//监测项记录ID
                    entity.lyID = gvr.Cells[1].Text.Trim().ToString();//监测项记录ID
                    //分析值
                    TextBox txt_Value = gvr.Cells[25].FindControl("txt_Value") as TextBox;
                    if (txt_Value.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析值不能为空！')", true);
                        return;
                    }
                    entity.Value = txt_Value.Text.Trim();
                    ////分析备注
                    //TextBox txt_Remark = gvr.Cells[21].FindControl("txt_Remark") as TextBox;

                    entity.Remark ="";
                    RadioButtonList rbl_Method = gvr.Cells[26].FindControl("rbl_Method") as RadioButtonList;
                    entity.Method = rbl_Method.SelectedValue;
                    //分析人
                    entity.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
                    //分析数据登记时间
                    entity.AnalysisDate = DateTime.Now;
                    DrawList.Add(entity);

                    DataRow[] drsel = dsitem_ZK.Tables[0].Select("itemid='" + gvr.Cells[3].Text.Trim()+ "'");
                    if (drsel.Length == 0)
                    
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请先登记质控记录再提交！')", true);
                        return;
                    }
                }

            }
        }
        if (DrawList.Count > 0)
        {
            DAl.DrawSample drawobj = new DAl.DrawSample();
            if (drawobj.DataSubmit(DrawList) > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交保存成功！')", true);
                Query();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交保存失败！')", true);
            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要提交的监测项！')", true);
            Query();
        }
    }
   

    private void BindCheckBoxList(RadioButtonList rbl, string ItemID,string typeID)
    {
        DataSet ds = new MyDataOp("select t_M_AnStandard.id,t_M_AnStandard.Standard from  t_M_AIStandard inner join t_M_AnStandard on t_M_AnStandard.id=t_M_AIStandard.Standardid inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.ID=t_M_AIStandard.AIID  where t_M_AnalysisItemEx.AIID='" + ItemID + "' and t_M_AnalysisItemEx.ClassID='"+typeID+"'").CreateDataSet();

        //这里的方法根据你自己的取数据的方法      
        rbl.DataSource = ds;
        rbl.DataValueField = "id";
        rbl.DataTextField = "Standard";
        rbl.DataBind();
    }
    protected void lbn_choose_Click(object sender, EventArgs e)
    {
        if (cbl_choose.Visible)
        {
            cbl_choose.Visible = false;
        }
        else
            cbl_choose.Visible = true;
        Query();
    }
    protected void cbl_choose_SelectedIndexChanged(object sender, EventArgs e)
    {
        Query();
    }

    #region GridView相关事件响应函数
    protected void grdvw_ZKList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ZKList.PageIndex = e.NewPageIndex;
        Query();
    }

    protected void grdvw_ZKList_RowCreated(object sender, GridViewRowEventArgs e)
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
            tcHeader[5].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[5].Text = "现场平行样";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[6].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[6].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[6].Attributes.Add("colspan", "2"); //跨Column   
            tcHeader[6].Text = "实验室平行样";

            tcHeader.Add(new TableHeaderCell());
            tcHeader[7].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[7].Attributes.Add("rowspan", "0"); //跨Row   
            tcHeader[7].Attributes.Add("colspan", "2"); //跨Column   
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
            tcHeader[11].Text = "总检数";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[12].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[12].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[12].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[12].Text = "保存";
            tcHeader.Add(new TableHeaderCell());
            tcHeader[13].Attributes.Add("bgcolor", "#8E8E8E");
            tcHeader[13].Attributes.Add("rowspan", "2"); //跨Row   
            tcHeader[13].Attributes.Add("colspan", "0"); //跨Column   
            tcHeader[13].Text = "删除</tr><tr>";
            int n = 14;
            //第二行表头 
            for (int i = 0; i < 12; i = i + 2)
            {
                tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i].Attributes.Add("bgcolor", "#8E8E8E");
                tcHeader[n + i].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i].Text = "检查数";


                tcHeader.Add(new TableHeaderCell());
                tcHeader[n + i + 1].Attributes.Add("rowspan", "0"); //跨Row   
                tcHeader[n + i + 1].Attributes.Add("colspan", "0"); //跨Column 
                tcHeader[n + i + 1].Text = "合格数";
                tcHeader[n + i + 1].Attributes.Add("bgcolor", "#005EBB");
                if (i == 11)
                    tcHeader[n + i + 1].Text = "合格数</tr><tr>";
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            //手动添加详细和删除按钮-保存
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 50;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ID = "Draw";
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
            ibtnDetail.TabIndex = 1000; ;
            ibtnDetail.CommandName = "Select";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);
            for (int i = 18; i <= e.Row.Cells.Count -2; i++)
            {
                e.Row.Cells[i].Visible = false;
            }
            

            //手动添加详细和删除按钮
            TableCell tabcback = new TableCell();
            tabcback.Width = 50;
            tabcback.Style.Add("text-align", "center");
            ImageButton ibtnback = new ImageButton();
            ibtnback.ImageUrl = "~/images/Delete.gif";

            ibtnback.CommandName = "Delete";
            ibtnback.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            tabcback.Controls.Add(ibtnback);
            e.Row.Cells.Add(tabcback);

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
           
        }
    }
    protected void btn_SaveZK_Click(object sender, EventArgs e)
    {
        List<Entity.ZKItem> entitylist = new List<Entity.ZKItem>();
       
        foreach (GridViewRow gvr in grdvw_ZKList.Rows)
        {
            Entity.ZKItem entity = new Entity.ZKItem();
            if (gvr.Cells[0].Text.Trim().ToString() != "&nbsp;" && gvr.Cells[0].Text.Trim().ToString() != "")
                entity.ID = int.Parse(gvr.Cells[0].Text.Trim().ToString());//质控项ID
            else
                entity.ID = 0;
            DropDownList ddl = gvr.Cells[1].FindControl("drop_wrw") as DropDownList;
            entity.MonitorItem = ddl.SelectedValue.ToString().Trim();//监测项ID
            //现场平行样检查数
            TextBox txt_scenejcnum = gvr.FindControl("txt_scenejcnum") as TextBox;
            //entity.scenejcnum = txt_scenejcnum.Text.Trim();

            //现场平行样合格数
            TextBox txt_scenehgnum = gvr.FindControl("txt_scenehgnum") as TextBox;
            //entity.scenehgnum = txt_scenehgnum.Text.Trim();

            //实验平行样检查数
            TextBox txt_experimentjcnum = gvr.FindControl("txt_experimentjcnum") as TextBox;
           // entity.experimentjcnum = txt_experimentjcnum.Text.Trim();

            //实验平行样合格数
            TextBox txt_experimenthgnum = gvr.FindControl("txt_experimenthgnum") as TextBox;
            //entity.experimenthgnum = txt_experimenthgnum.Text.Trim();

            //加标回收检查数
            TextBox txt_jbhsjcnum = gvr.FindControl("txt_jbhsjcnum") as TextBox;
           // entity.jbhsjcnum = txt_jbhsjcnum.Text.Trim();

            //加标回收合格数
            TextBox txt_jbhshgnum = gvr.FindControl("txt_jbhshgnum") as TextBox;
            //entity.jbhshgnum = txt_jbhshgnum.Text.Trim();

            //全程序空白检查数
            TextBox txt_alljcnum = gvr.FindControl("txt_alljcnum") as TextBox;
            //entity.alljcnum = txt_alljcnum.Text.Trim();

            //全程序空白合格数
            TextBox txt_allhgnum = gvr.FindControl("txt_allhgnum") as TextBox;
            //entity.allhgnum = txt_allhgnum.Text.Trim();

            //密码样检查数
            TextBox txt_mmjcnum = gvr.FindControl("txt_mmjcnum") as TextBox;
            //entity.mmjcnum = txt_mmjcnum.Text.Trim();

            //密码样合格数
            TextBox txt_mmhgnum = gvr.FindControl("txt_mmhgnum") as TextBox;
            //entity.mmhgnum = txt_mmhgnum.Text.Trim();

            //标样检查数
            TextBox txt_byjcnum = gvr.FindControl("txt_byjcnum") as TextBox;
           // entity.byjcnum = txt_byjcnum.Text.Trim();

            //标样合格数
            TextBox txt_byhgnum = gvr.FindControl("txt_byhgnum") as TextBox;
            //entity.byhgnum = txt_byhgnum.Text.Trim();
            TextBox txt_amount = gvr.FindControl("txt_amount") as TextBox;
           // entity.amount = txt_amount.Text.Trim();
            //分析样品数
            TextBox txt_analysisnum = gvr.FindControl("txt_analysisnum") as TextBox;
            if (txt_analysisnum.Text.Trim() != "")
            {
                try
                {
                    entity.analysisnum = int.Parse(txt_analysisnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.analysisnum = 0;
            if (txt_scenejcnum.Text.Trim() != "")
            {
                try
                {
                    entity.scenejcnum = int.Parse(txt_scenejcnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.scenejcnum = 0;

            //现场平行样合格数
            //TextBox txt_scenehgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_scenehgnum") as TextBox;

            if (txt_scenehgnum.Text.Trim() != "")
            {
                try
                {
                    entity.scenehgnum = int.Parse(txt_scenehgnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.scenehgnum = 0;
            //实验平行样检查数
            //TextBox txt_experimentjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_experimentjcnum") as TextBox;

            if (txt_experimentjcnum.Text.Trim() != "")
            {
                try
                {
                    entity.experimentjcnum = int.Parse(txt_experimentjcnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.experimentjcnum = 0;
            //实验平行样合格数
            //TextBox txt_experimenthgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_experimenthgnum") as TextBox;

            if (txt_experimenthgnum.Text.Trim() != "")
            {
                try
                {
                    entity.experimenthgnum = int.Parse(txt_experimenthgnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.experimenthgnum = 0;
            //加标回收检查数
           // TextBox txt_jbhsjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_jbhsjcnum") as TextBox;

            if (txt_jbhsjcnum.Text.Trim() != "")
            {
                try
                {
                    entity.jbhsjcnum = int.Parse(txt_jbhsjcnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.jbhsjcnum = 0;
            //加标回收合格数
           // TextBox txt_jbhshgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_jbhshgnum") as TextBox;

            if (txt_jbhshgnum.Text.Trim() != "")
            {
                try
                {
                    entity.jbhshgnum = int.Parse(txt_jbhshgnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.jbhshgnum = 0;
            //全程序空白检查数
           // TextBox txt_alljcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_alljcnum") as TextBox;

            if (txt_alljcnum.Text.Trim() != "")
            {
                try
                {
                    entity.alljcnum = int.Parse(txt_alljcnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.alljcnum = 0;

            //全程序空白合格数
           // TextBox txt_allhgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_allhgnum") as TextBox;

            if (txt_allhgnum.Text.Trim() != "")
            {
                try
                {
                    entity.allhgnum = int.Parse(txt_allhgnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.allhgnum = 0;
            //密码样检查数
            //TextBox txt_mmjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_mmjcnum") as TextBox;

            if (txt_allhgnum.Text.Trim() != "")
            {
                try
                {
                    entity.mmjcnum = int.Parse(txt_mmjcnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.mmjcnum = 0;
            //密码样合格数
            //TextBox txt_mmhgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_mmhgnum") as TextBox;

            if (txt_mmhgnum.Text.Trim() != "")
            {
                try
                {
                    entity.mmhgnum = int.Parse(txt_mmhgnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.mmhgnum = 0;
            //标样检查数
           // TextBox txt_byjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_byjcnum") as TextBox;
            //entity.byjcnum = txt_byjcnum.Text.Trim();
            if (txt_byjcnum.Text.Trim() != "")
            {
                try
                {
                    entity.byjcnum = int.Parse(txt_byjcnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.byjcnum = 0;
            //标样合格数
           // TextBox txt_byhgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_byhgnum") as TextBox;
            // entity.byhgnum = txt_byhgnum.Text.Trim();
            if (txt_byhgnum.Text.Trim() != "")
            {
                try
                {
                    entity.byhgnum = int.Parse(txt_byhgnum.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.byhgnum = 0;
           // TextBox txt_amount = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_amount") as TextBox;
            // entity.amount = txt_amount.Text.Trim();
            if (txt_amount.Text.Trim() != "")
            {
                try
                {
                    entity.amount = int.Parse(txt_amount.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                    return;
                }
            }
            else
                entity.amount = 0;
            entity.CreateDate = DateTime.Now;
            entity.UserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
            entitylist.Add(entity);
        }
        DAl.ZKItem itemObj = new DAl.ZKItem();
        if (itemObj.Save(entitylist) == 1)
        {
            BindZK();
            BindAnalysis();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析数据保存成功！')", true);
            

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析数据保存失败！')", true);
        }
 
    }
    protected void btn_AddItem_Click(object sender, EventArgs e)
    {
        int i = 0;
        if (havedata)
        {
            foreach (GridViewRow gvr in grdvw_ZKList.Rows)
            {
                DataRow drdelete = dsitem_ZK.Tables[0].Rows[i];

                if (gvr.Cells[0].Text.Trim().ToString() != "&nbsp;" && gvr.Cells[0].Text.Trim().ToString() != "")
                    drdelete["id"] = int.Parse(gvr.Cells[0].Text.Trim().ToString());//质控项ID
                else
                    drdelete["id"] = 0;
                DropDownList ddl = gvr.Cells[1].FindControl("drop_wrw") as DropDownList;
                if (ddl.SelectedValue.ToString().Trim() != "")
                    drdelete["itemid"] = ddl.SelectedValue.ToString().Trim();//监测项ID
                else
                    drdelete["itemid"] = 0;
                //现场平行样检查数
                TextBox txt_scenejcnum = gvr.FindControl("txt_scenejcnum") as TextBox;
                if (txt_scenejcnum.Text.Trim() != "")
                    drdelete["scenejcnum"] = txt_scenejcnum.Text.Trim();
                else
                    drdelete["scenejcnum"] = 0;

                //现场平行样合格数
                TextBox txt_scenehgnum = gvr.FindControl("txt_scenehgnum") as TextBox;
                if (txt_scenehgnum.Text.Trim() != "")
                    drdelete["scenehgnum"] = txt_scenehgnum.Text.Trim();
                else
                    drdelete["scenehgnum"] = 0;

                //实验平行样检查数
                TextBox txt_experimentjcnum = gvr.FindControl("txt_experimentjcnum") as TextBox;
                if (txt_experimentjcnum.Text.Trim() != "")
                    drdelete["experimentjcnum"] = txt_experimentjcnum.Text.Trim();
                else
                    drdelete["experimentjcnum"] = 0;
                //实验平行样合格数
                TextBox txt_experimenthgnum = gvr.FindControl("txt_experimenthgnum") as TextBox;
                if (txt_experimenthgnum.Text.Trim() != "")
                    drdelete["experimenthgnum"] = txt_experimenthgnum.Text.Trim();
                else
                    drdelete["experimenthgnum"] = 0;

                //加标回收检查数
                TextBox txt_jbhsjcnum = gvr.FindControl("txt_jbhsjcnum") as TextBox;
                if (txt_jbhsjcnum.Text.Trim() != "")
                    drdelete["jbhsjcnum"] = txt_jbhsjcnum.Text.Trim();
                else
                    drdelete["jbhsjcnum"] = 0;

                //加标回收合格数
                TextBox txt_jbhshgnum = gvr.FindControl("txt_jbhshgnum") as TextBox;
                if (txt_jbhshgnum.Text.Trim() != "")
                    drdelete["jbhshgnum"] = txt_jbhshgnum.Text.Trim();
                else
                    drdelete["jbhshgnum"] = 0;

                //全程序空白检查数
                TextBox txt_alljcnum = gvr.FindControl("txt_alljcnum") as TextBox;
                if (txt_alljcnum.Text.Trim() != "")
                    drdelete["alljcnum"] = txt_alljcnum.Text.Trim();
                else
                    drdelete["alljcnum"] = 0;

                //全程序空白合格数
                TextBox txt_allhgnum = gvr.FindControl("txt_allhgnum") as TextBox;
                if (txt_allhgnum.Text.Trim() != "")
                    drdelete["allhgnum"] = txt_allhgnum.Text.Trim();
                else
                    drdelete["allhgnum"] = 0;

                //密码样检查数
                TextBox txt_mmjcnum = gvr.FindControl("txt_mmjcnum") as TextBox;
                if (txt_mmjcnum.Text.Trim() != "")
                    drdelete["mmjcnum"] = txt_mmjcnum.Text.Trim();
                else
                    drdelete["mmjcnum"] = 0;


                //密码样合格数
                TextBox txt_mmhgnum = gvr.FindControl("txt_mmhgnum") as TextBox;
                if (txt_mmhgnum.Text.Trim() != "")
                    drdelete["mmhgnum"] = txt_mmhgnum.Text.Trim();

                else
                    drdelete["mmhgnum"] = 0;
                //标样检查数
                TextBox txt_byjcnum = gvr.FindControl("txt_byjcnum") as TextBox;
                if (txt_byjcnum.Text.Trim() != "")
                    drdelete["byjcnum"] = txt_byjcnum.Text.Trim();
                else
                    drdelete["byjcnum"] = 0;

                //标样合格数
                TextBox txt_byhgnum = gvr.FindControl("txt_byhgnum") as TextBox;
                if (txt_byhgnum.Text.Trim() != "")
                    drdelete["byhgnum"] = txt_byhgnum.Text.Trim();
                else
                    drdelete["byhgnum"] = 0;
                TextBox txt_amount = gvr.FindControl("txt_amount") as TextBox;
                if (txt_amount.Text.Trim() != "")
                    drdelete["amount"] = txt_amount.Text.Trim();
                else
                    drdelete["amount"] = 0;
                //分析样品数
                TextBox txt_analysisnum = gvr.FindControl("txt_analysisnum") as TextBox;
                if (txt_analysisnum.Text != "")
                    drdelete["analysisnum"] = txt_analysisnum.Text;
                else
                    drdelete["analysisnum"] = 0;

                drdelete["Name"] = HttpUtility.UrlDecode(Request.Cookies["Cookies"].Values["Name"].ToString());
                i++;
                dsitem_ZK.AcceptChanges();
            }


            DataRow dr = dsitem_ZK.Tables[0].NewRow();
            dr["Name"] = HttpUtility.UrlDecode(Request.Cookies["Cookies"].Values["Name"].ToString());
            dsitem_ZK.Tables[0].Rows.Add(dr);
    }

        foreach (DataRow dr in dsitem_ZK.Tables[0].Rows)
        {
            dr["Name"]= HttpUtility.UrlDecode(Request.Cookies["Cookies"].Values["Name"].ToString());
            if (dr["itemid"].ToString() != "")
            {
                DataRow[] drselect = ds.Tables[0].Select("MonitorItem='" + dr["itemid"].ToString() + "'");
                dr["analysisnum"] = drselect.Length;
            }

        }
        dsitem_ZK.AcceptChanges();
        if (dsitem_ZK.Tables[0].Rows.Count == 0)
        {
            havedata = false;
            //没有记录仍保留表头
            dsitem_ZK.Tables[0].Rows.Add(dsitem_ZK.Tables[0].NewRow());
            grdvw_ZKList.DataSource = dsitem_ZK;
            grdvw_ZKList.DataBind();
            int intColumnCount = grdvw_ZKList.Rows[0].Cells.Count;
            grdvw_ZKList.Rows[0].Cells.Clear();
            grdvw_ZKList.Rows[0].Cells.Add(new TableCell());
            grdvw_ZKList.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            havedata = true;
            grdvw_ZKList.DataSource = dsitem_ZK;
            grdvw_ZKList.DataBind();

        }
        BindAnalysis();

    }
    protected void grdvw_ZKList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        List<Entity.ZKItem> entitylist = new List<Entity.ZKItem>();
        Entity.ZKItem entity = new Entity.ZKItem();
        if (grdvw_ZKList.Rows[e.NewSelectedIndex].Cells[0].Text.Trim().ToString() != "&nbsp;" && grdvw_ZKList.Rows[e.NewSelectedIndex].Cells[0].Text.Trim().ToString() != "")
            entity.ID = int.Parse(grdvw_ZKList.Rows[e.NewSelectedIndex].Cells[0].Text.Trim().ToString());//质控项ID
        else
            entity.ID = 0;
        DropDownList ddl = grdvw_ZKList.Rows[e.NewSelectedIndex].Cells[1].FindControl("drop_wrw") as DropDownList;
        entity.MonitorItem = ddl.SelectedValue.ToString().Trim();//监测项ID
        entity.CreateDate = DateTime.Now;
        entity.UpdateDate = DateTime.Now;
        entity.UserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        entity.UpdateUserID = Request.Cookies["Cookies"].Values["u_id"].ToString();
        //分析样品数
        TextBox txt_analysisnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_analysisnum") as TextBox;
        if (txt_analysisnum.Text.Trim() != "")
        {
            try
            {
                entity.analysisnum = int.Parse(txt_analysisnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.analysisnum = 0;
      //现场平行样检查数
        TextBox txt_scenejcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_scenejcnum") as TextBox;
        if (txt_scenejcnum.Text.Trim() != "")
        {
            try
            {
                entity.scenejcnum = int.Parse(txt_scenejcnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.scenejcnum = 0;

        //现场平行样合格数
        TextBox txt_scenehgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_scenehgnum") as TextBox;

        if (txt_scenehgnum.Text.Trim() != "")
        {
            try
            {
                entity.scenehgnum = int.Parse(txt_scenehgnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.scenehgnum = 0;
        //实验平行样检查数
        TextBox txt_experimentjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_experimentjcnum") as TextBox;

        if (txt_experimentjcnum.Text.Trim() != "")
        {
            try
            {
                entity.experimentjcnum = int.Parse(txt_experimentjcnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.experimentjcnum = 0;
        //实验平行样合格数
        TextBox txt_experimenthgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_experimenthgnum") as TextBox;

        if (txt_experimenthgnum.Text.Trim() != "")
        {
            try
            {
                entity.experimenthgnum =int.Parse( txt_experimenthgnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.experimenthgnum = 0;
        //加标回收检查数
        TextBox txt_jbhsjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_jbhsjcnum") as TextBox;

        if (txt_jbhsjcnum.Text.Trim() != "")
        {
            try
            {
                entity.jbhsjcnum =int.Parse( txt_jbhsjcnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.jbhsjcnum = 0;
        //加标回收合格数
        TextBox txt_jbhshgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_jbhshgnum") as TextBox;

        if (txt_jbhshgnum.Text.Trim() != "")
        {
            try
            {
                entity.jbhshgnum =  int.Parse(txt_jbhshgnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.jbhshgnum = 0;
        //全程序空白检查数
        TextBox txt_alljcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_alljcnum") as TextBox;

        if (txt_alljcnum.Text.Trim() != "")
        {
            try
            {
                entity.alljcnum = int.Parse(txt_alljcnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.alljcnum = 0;

        //全程序空白合格数
        TextBox txt_allhgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_allhgnum") as TextBox;

        if (txt_allhgnum.Text.Trim() != "")
        {
            try
            {
                entity.allhgnum =int.Parse( txt_allhgnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.allhgnum = 0;
        //密码样检查数
        TextBox txt_mmjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_mmjcnum") as TextBox;
        
        if (txt_allhgnum.Text.Trim() != "")
        {
            try
            {
                entity.mmjcnum = int.Parse(txt_mmjcnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.mmjcnum = 0;
        //密码样合格数
        TextBox txt_mmhgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_mmhgnum") as TextBox;
       
        if (txt_mmhgnum.Text.Trim() != "")
        {
            try
            {
                entity.mmhgnum = int.Parse(txt_mmhgnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.mmhgnum = 0;
        //标样检查数
        TextBox txt_byjcnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_byjcnum") as TextBox;
        //entity.byjcnum = txt_byjcnum.Text.Trim();
        if (txt_byjcnum.Text.Trim() != "")
        {
            try
            {
                entity.byjcnum = int.Parse(txt_byjcnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.byjcnum = 0;
        //标样合格数
        TextBox txt_byhgnum = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_byhgnum") as TextBox;
       // entity.byhgnum = txt_byhgnum.Text.Trim();
        if (txt_byhgnum.Text.Trim() != "")
        {
            try
            {
                entity.byhgnum = int.Parse(txt_byhgnum.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.byhgnum = 0;
        TextBox txt_amount = grdvw_ZKList.Rows[e.NewSelectedIndex].FindControl("txt_amount") as TextBox;
       // entity.amount = txt_amount.Text.Trim();
        if (txt_amount.Text.Trim() != "")
        {
            try
            {
                entity.amount = int.Parse(txt_amount.Text.Trim());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入数值型数据')", true);
                return;
            }
        }
        else
            entity.amount = 0;
        entitylist.Add(entity);
        DAl.ZKItem itemObj = new DAl.ZKItem();
        if (itemObj.Save(entitylist) == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('质控分析数据保存成功！')", true);
           

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析数据保存失败！')", true);
        }

    }
   
    protected void grdvw_ZKList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int delid = 0;
        if (grdvw_ZKList.Rows[e.RowIndex].Cells[0].Text.Trim().ToString() != "&nbsp;" && grdvw_ZKList.Rows[e.RowIndex].Cells[0].Text.Trim().ToString() != "")
            delid = int.Parse(grdvw_ZKList.Rows[e.RowIndex].Cells[0].Text.Trim().ToString());//质控项ID
        DAl.ZKItem zkitem = new DAl.ZKItem();
        if (delid != 0)
        {
            zkitem.Delete(delid);
        }
        GridViewRow gvr = grdvw_ZKList.Rows[e.RowIndex];
        DataRow drdelete = dsitem_ZK.Tables[0].NewRow();
#region 获取当前行的信息
        
           // drdelete["UpdateUserID"] = Request.Cookies["Cookies"].Values["u_id"].ToString();
	#endregion
            dsitem_ZK.Tables[0].Rows.RemoveAt(e.RowIndex);
         dsitem_ZK.AcceptChanges();
         if (dsitem_ZK.Tables[0].Rows.Count == 0)
         {
             havedata = false;
             //没有记录仍保留表头
             dsitem_ZK.Tables[0].Rows.Add(dsitem_ZK.Tables[0].NewRow());
             grdvw_ZKList.DataSource = dsitem_ZK;
             grdvw_ZKList.DataBind();
             int intColumnCount = grdvw_ZKList.Rows[0].Cells.Count;
             grdvw_ZKList.Rows[0].Cells.Clear();
             grdvw_ZKList.Rows[0].Cells.Add(new TableCell());
             grdvw_ZKList.Rows[0].Cells[0].ColumnSpan = intColumnCount;
         }
         else
         {
             havedata = true;
             grdvw_ZKList.DataSource = dsitem_ZK;
             grdvw_ZKList.DataBind();

         }
   

    }
    protected void grdvw_ZKList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList drop_wrw = e.Row.FindControl("drop_wrw") as DropDownList;
            BindItemList(drop_wrw);
            DataRowView row = e.Row.DataItem as DataRowView;

            string item = row["itemid"].ToString();
            drop_wrw.SelectedValue = item.ToString();
      }
    }
    private void BindItemList(DropDownList ddl_Item)
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("MonitorItem");
        DataColumn dc2 = new DataColumn("监测项");
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["MonitorItem"].ToString() != "")
            {
                DataRow[] drselect = dt.Select("MonitorItem='" + dr["MonitorItem"].ToString() + "'");
                if (drselect.Length == 0)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["MonitorItem"] = dr["MonitorItem"].ToString();
                    drnew["监测项"] = dr["监测项"].ToString();
                    dt.Rows.Add(drnew);
                    dt.AcceptChanges();
                }
            }
        }
        if (dt.Rows.Count > 0)
        {
            ddl_Item.DataSource = dt;
            ddl_Item.DataValueField = "MonitorItem";
            ddl_Item.DataTextField = "监测项";
            ddl_Item.DataBind();
            
        }
        else
        {
            string constr = "";
            if (txt_Itemquery.Text.Trim() != "")
                constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or t_M_ANItemInf.AICode like '%" + txt_Itemquery.Text.Trim() + "%')";
          
            constr += " and  (year(fxdate)= '" + DateTime.Now.Year + "' AND month(fxdate)= '" + DateTime.Now.Month + "' and '"+DateTime.Now.Day + "')";

            string getitemstr = "select distinct AIName 监测项,MonitorItem from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem  where  t_MonitorItemDetail.LyUser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' and delflag=0";

           DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
           ddl_Item.DataSource = dsitem;
            ddl_Item.DataValueField = "MonitorItem";
            ddl_Item.DataTextField = "监测项";
            ddl_Item.DataBind();
           
           
        }
      
    }

   
    
    #endregion
    protected void drop_wrw_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drop_wrw = sender as DropDownList;
        int i = 0;
        if (havedata)
        {
            foreach (GridViewRow gvr in grdvw_ZKList.Rows)
            {
                DataRow drdelete = dsitem_ZK.Tables[0].Rows[i];

                if (gvr.Cells[0].Text.Trim().ToString() != "&nbsp;" && gvr.Cells[0].Text.Trim().ToString() != "")
                    drdelete["id"] = int.Parse(gvr.Cells[0].Text.Trim().ToString());//质控项ID
                else
                    drdelete["id"] = 0;
                DropDownList ddl = gvr.Cells[1].FindControl("drop_wrw") as DropDownList;
                if (ddl.SelectedValue.ToString().Trim() != "")
                    drdelete["itemid"] = ddl.SelectedValue.ToString().Trim();//监测项ID
                else
                    drdelete["itemid"] = 0;
                //现场平行样检查数
                TextBox txt_scenejcnum = gvr.FindControl("txt_scenejcnum") as TextBox;
                if (txt_scenejcnum.Text.Trim() != "")
                    drdelete["scenejcnum"] = txt_scenejcnum.Text.Trim();
                else
                    drdelete["scenejcnum"] = 0;

                //现场平行样合格数
                TextBox txt_scenehgnum = gvr.FindControl("txt_scenehgnum") as TextBox;
                if (txt_scenehgnum.Text.Trim() != "")
                    drdelete["scenehgnum"] = txt_scenehgnum.Text.Trim();
                else
                    drdelete["scenehgnum"] = 0;

                //实验平行样检查数
                TextBox txt_experimentjcnum = gvr.FindControl("txt_experimentjcnum") as TextBox;
                if (txt_experimentjcnum.Text.Trim() != "")
                    drdelete["experimentjcnum"] = txt_experimentjcnum.Text.Trim();
                else
                    drdelete["experimentjcnum"] = 0;
                //实验平行样合格数
                TextBox txt_experimenthgnum = gvr.FindControl("txt_experimenthgnum") as TextBox;
                if (txt_experimenthgnum.Text.Trim() != "")
                    drdelete["experimenthgnum"] = txt_experimenthgnum.Text.Trim();
                else
                    drdelete["experimenthgnum"] = 0;

                //加标回收检查数
                TextBox txt_jbhsjcnum = gvr.FindControl("txt_jbhsjcnum") as TextBox;
                if (txt_jbhsjcnum.Text.Trim() != "")
                    drdelete["jbhsjcnum"] = txt_jbhsjcnum.Text.Trim();
                else
                    drdelete["jbhsjcnum"] = 0;

                //加标回收合格数
                TextBox txt_jbhshgnum = gvr.FindControl("txt_jbhshgnum") as TextBox;
                if (txt_jbhshgnum.Text.Trim() != "")
                    drdelete["jbhshgnum"] = txt_jbhshgnum.Text.Trim();
                else
                    drdelete["jbhshgnum"] = 0;

                //全程序空白检查数
                TextBox txt_alljcnum = gvr.FindControl("txt_alljcnum") as TextBox;
                if (txt_alljcnum.Text.Trim() != "")
                    drdelete["alljcnum"] = txt_alljcnum.Text.Trim();
                else
                    drdelete["alljcnum"] = 0;

                //全程序空白合格数
                TextBox txt_allhgnum = gvr.FindControl("txt_allhgnum") as TextBox;
                if (txt_allhgnum.Text.Trim() != "")
                    drdelete["allhgnum"] = txt_allhgnum.Text.Trim();
                else
                    drdelete["allhgnum"] = 0;

                //密码样检查数
                TextBox txt_mmjcnum = gvr.FindControl("txt_mmjcnum") as TextBox;
                if (txt_mmjcnum.Text.Trim() != "")
                    drdelete["mmjcnum"] = txt_mmjcnum.Text.Trim();
                else
                    drdelete["mmjcnum"] = 0;


                //密码样合格数
                TextBox txt_mmhgnum = gvr.FindControl("txt_mmhgnum") as TextBox;
                if (txt_mmhgnum.Text.Trim() != "")
                    drdelete["mmhgnum"] = txt_mmhgnum.Text.Trim();

                else
                    drdelete["mmhgnum"] = 0;
                //标样检查数
                TextBox txt_byjcnum = gvr.FindControl("txt_byjcnum") as TextBox;
                if (txt_byjcnum.Text.Trim() != "")
                    drdelete["byjcnum"] = txt_byjcnum.Text.Trim();
                else
                    drdelete["byjcnum"] = 0;

                //标样合格数
                TextBox txt_byhgnum = gvr.FindControl("txt_byhgnum") as TextBox;
                if (txt_byhgnum.Text.Trim() != "")
                    drdelete["byhgnum"] = txt_byhgnum.Text.Trim();
                else
                    drdelete["byhgnum"] = 0;
                TextBox txt_amount = gvr.FindControl("txt_amount") as TextBox;
                if (txt_amount.Text.Trim() != "")
                    drdelete["amount"] = txt_amount.Text.Trim();
                else
                    drdelete["amount"] = 0;
                //分析样品数
                TextBox txt_analysisnum = gvr.FindControl("txt_analysisnum") as TextBox;
                if (txt_analysisnum.Text != "")
                    drdelete["analysisnum"] = txt_analysisnum.Text;
                else
                    drdelete["analysisnum"] = 0;

                drdelete["Name"] = HttpUtility.UrlDecode(Request.Cookies["Cookies"].Values["Name"].ToString());
                i++;
                dsitem_ZK.AcceptChanges();
            }


            //DataRow dr = dsitem_ZK.Tables[0].NewRow();
            //dr["Name"] = HttpUtility.UrlDecode(Request.Cookies["Cookies"].Values["Name"].ToString());
            //dsitem_ZK.Tables[0].Rows.Add(dr);
        }

        foreach (DataRow dr in dsitem_ZK.Tables[0].Rows)
        {
            dr["Name"] = HttpUtility.UrlDecode(Request.Cookies["Cookies"].Values["Name"].ToString());
            if (dr["itemid"].ToString() != "")
            {
                DataRow[] drselect = ds.Tables[0].Select("MonitorItem='" + dr["itemid"].ToString() + "'");
                dr["analysisnum"] = drselect.Length;
            }

        }
        dsitem_ZK.AcceptChanges();
        if (dsitem_ZK.Tables[0].Rows.Count == 0)
        {
            havedata = false;
            //没有记录仍保留表头
            dsitem_ZK.Tables[0].Rows.Add(dsitem_ZK.Tables[0].NewRow());
            grdvw_ZKList.DataSource = dsitem_ZK;
            grdvw_ZKList.DataBind();
            int intColumnCount = grdvw_ZKList.Rows[0].Cells.Count;
            grdvw_ZKList.Rows[0].Cells.Clear();
            grdvw_ZKList.Rows[0].Cells.Add(new TableCell());
            grdvw_ZKList.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            havedata = true;
            grdvw_ZKList.DataSource = dsitem_ZK;
            grdvw_ZKList.DataBind();

        }
        BindAnalysis();
    }
}
