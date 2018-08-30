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
using System.Collections.Generic;
using OWC11;
using System.IO;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using WebApp.Components;
using Entity;

public partial class Sample_ReportSampleData : System.Web.UI.Page
{
   
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    
    private DataTable fxman//所选择操作列记录对应的id
    {
        get { return (DataTable)ViewState["fxman"]; }
        set { ViewState["fxman"] = value; }
    }
    private DataSet ds//所选择操作列记录对应的id
    {
        get { return (DataSet)ViewState["ds"]; }
        set { ViewState["ds"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
           DAl.User.Users userobj=new DAl.User.Users();
           DAl.User.UserRole userrole= new DAl.User.UserRole();
           string strrole = userrole.RoleListListStr(1);
            
            fxman = userobj.QueryUsersDT("", strrole);
          
            ReportQuery();

            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>分工指派</b></font>"; 
        }
    }
    #region 样品列表
    private void ReportQuery()
    {
        string constr = "";
        if (txt_sampleQuery.Text.Trim() != "")
            constr += " and t_M_SampleInfor.SampleID like '%" + txt_sampleQuery.Text.Trim() + "%'";
        if (txt_QueryTime.Text.Trim() != "")
            constr += " and  (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        if (txt_Itemquery.Text.Trim() != "")
            constr += " and (t_M_ANItemInf.AIName like '%" + txt_Itemquery.Text.Trim() + "%' or LOWER(t_M_ANItemInf.AICode) like '%LOWER(" + txt_Itemquery.Text.Trim() + ")%')";
   
        string strSql = "select t_MonitorItemDetail.id, t_MonitorItemDetail.MonitorItem,t_M_ANItemInf.AIName 监测项,t_M_SampleInfor.SampleID AS 样品编号,t_M_ReporInfo.Ulevel ,t_M_SampleInfor.SampleAddress 采样点 ,t_M_SampleInfor.SampleDate AS 采样时间," +
      "t_M_SampleInfor.AccessDate AS 接样时间, " +
      " t_M_SampleInfor.TypeID, t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状,ProjectName 项目名称," +
     " t_M_ReporInfo.chargeman 项目负责人,t_MonitorItemDetail.zpto,t_MonitorItemDetail.flag,t_M_SampleInfor.id " +
" FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
     " t_M_AnalysisMainClassEx ON " +
    "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id and t_MonitorItemDetail.delflag=0  inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem where t_MonitorItemDetail.flag=" + drop_status.SelectedValue.ToString() + " and t_M_SampleInfor.StatusID=1  " + constr + " ORDER BY t_M_SampleInfor.AccessDate";

      ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
       
       
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
           
               
                //if (dr["StatusID"].ToString() == "0")
                //    dr["样品状态"] = "登记中";
                //else
                //    dr["样品状态"] = "已提交";
          

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
        ReportQuery();
    }
    
   
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            System.Web.UI.WebControls.CheckBox cbl = new System.Web.UI.WebControls.CheckBox();
            cbl.ID = "cbl_All";
            cbl.Text = "全选";
            cbl.CheckedChanged += cbl_CheckedChanged;
            cbl.AutoPostBack = true;
            e.Row.Cells[0].Controls.Add(cbl);
            e.Row.Cells[0].Width = 60;

            TableCell headerset = new TableCell();
            headerset.Text = "指派给";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);
           

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "指派";
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
            System.Web.UI.WebControls.CheckBox cbl = new System.Web.UI.WebControls.CheckBox();
            cbl.ID = "cbl";
            e.Row.Cells[0].Controls.Add(cbl);

            //手动添加详细和删除按钮
            TableCell tabcSelect = new TableCell();
            tabcSelect.Width = 60;
            tabcSelect.Style.Add("text-align", "center");
            DropDownList drp_zpto = new DropDownList();
            drp_zpto.ID = "drp_zpto";
            drp_zpto.Width = 75;
            tabcSelect.Controls.Add(drp_zpto);

            e.Row.Cells.Add(tabcSelect);
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
            ibtnDetail.CommandName = "Select";
            ibtnDetail.Attributes.Add("OnClick", "if(!confirm('确定要指派吗？')) return false;else return true;");
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[9].Visible = false;

            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
        }
    }

    protected void cbl_CheckedChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox cbl_all = sender as System.Web.UI.WebControls.CheckBox;
        foreach (GridViewRow gvr in grdvw_List.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.CheckBox cbl = gvr.Cells[0].FindControl("cbl") as System.Web.UI.WebControls.CheckBox;
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
    #endregion

    #region 其它函数


  
   
    #endregion
   
    //选中某个报告，某个报告的样品单列表
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    { //指派给谁
        DropDownList drp_zpto = grdvw_List.Rows[e.NewSelectedIndex].FindControl("drp_zpto") as DropDownList;
        if (drp_zpto.SelectedValue.ToString() != "0")
        {
            List<Entity.SampleItem> entitylist = new List<Entity.SampleItem>();
            Entity.SampleItem entity = new Entity.SampleItem();
            entity.SampleID = grdvw_List.Rows[e.NewSelectedIndex].Cells[16].Text.Trim().ToString();//样品编号
            entity.ID = int.Parse(grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().ToString());//监测项记录ID


            entity.zpto = drp_zpto.SelectedValue.ToString();
            entity.statusID = 1;
            //指派人
            entity.zpcreateuser = Request.Cookies["Cookies"].Values["u_id"].ToString();
            //指派时间
            entity.zpdate = DateTime.Now;
            entitylist.Add(entity);
            DAl.DrawSample itemObj = new DAl.DrawSample();
            if (itemObj.ZPSampleItem(entitylist) == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('指派保存成功！')", true);
                ReportQuery();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('指派保存失败！')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分析人未指定！')", true);
        }

    }

    protected void btn_zp_Click(object sender, EventArgs e)
    {
        int i = 0;
        int j = 0;
        foreach (GridViewRow gvr in grdvw_List.Rows)
        {
            System.Web.UI.WebControls.CheckBox cbl= gvr.FindControl("cbl") as System.Web.UI.WebControls.CheckBox;
            if (cbl.Checked)
            {
                i++;
                //指派给谁
                DropDownList drp_zpto = gvr.FindControl("drp_zpto") as DropDownList;
                if (drp_zpto.SelectedValue.ToString() != "0")
                {
                    List<Entity.SampleItem> entitylist = new List<Entity.SampleItem>();
                    Entity.SampleItem entity = new Entity.SampleItem();
                    entity.SampleID = gvr.Cells[16].Text.Trim().ToString();//样品编号
                    entity.ID = int.Parse(gvr.Cells[1].Text.Trim().ToString());//监测项记录ID


                    entity.zpto = drp_zpto.SelectedValue.ToString();
                    entity.statusID = 1;
                    //指派人
                    entity.zpcreateuser = Request.Cookies["Cookies"].Values["u_id"].ToString();
                    //指派时间
                    entity.zpdate = DateTime.Now;
                    entitylist.Add(entity);
                    DAl.DrawSample itemObj = new DAl.DrawSample();
                    if (itemObj.ZPSampleItem(entitylist) == 1)
                    {
                        j++;
                    }
                   
                }
               
            }
        }
        if (i == j&&j>0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('指派保存成功！')", true);
            ReportQuery();
        }
        else if (i > j && j > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('部分指派保存成功！请查看分析人员是否选择？')", true);
            ReportQuery();
        }
        else if (i == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选指派记录！')", true);
        }
        else if (j == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请查看分析人员是否选择？')", true);
        }
    }
  
    protected void btn_query_Click(object sender, EventArgs e)
    {
        ReportQuery();
       
    }


    protected void grdvw_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList drp_zpto = e.Row.FindControl("drp_zpto") as DropDownList;
            //DataRow[] dr=fxman.Select("")
            drp_zpto.DataSource = fxman;
            drp_zpto.DataTextField = "Name";
            drp_zpto.DataValueField = "UserID";
            drp_zpto.DataBind();
            ListItem li = new ListItem("请选择", "0");
            drp_zpto.Items.Add(li);
             drp_zpto.SelectedIndex = drp_zpto.Items.Count - 1;
            if (e.Row.Cells[14].Text != "0" && e.Row.Cells[14].Text!="")
            {
                drp_zpto.SelectedValue = e.Row.Cells[14].Text.Trim();
            }
        }
    }
    #endregion
}