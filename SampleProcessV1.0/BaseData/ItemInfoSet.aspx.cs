﻿using System;
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

public partial class BaseData_ItemInfoSet : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面元素
            SetTxt();
           SetButton();
           initialAll();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>项目类型管理</b></font>";
            #endregion

            Query();
        }
    }
     private void initialAll()
    {
        dropList_type_Query.Items.Clear();
        DAl.Sample sampleobj = new DAl.Sample();
        DataTable dttype = sampleobj.GetMode("", "rwtype", "");
        dropList_type_Query.DataSource = dttype;
        dropList_type_Query.DataTextField = "name";
        dropList_type_Query.DataValueField = "code";
        dropList_type_Query.DataBind();
        ListItem li = new ListItem("请选择");
        dropList_type_Query.Items.Add(li);
        dropList_type_Query.SelectedIndex = dropList_type_Query.Items.Count - 1;


    }
    

    private void initial()
    {
        drop_rwtype.Items.Clear();
        DAl.Sample sampleobj = new DAl.Sample();
        DataTable dttype = sampleobj.GetMode("", "rwtype", "");
        drop_rwtype.DataSource = dttype;
        drop_rwtype.DataTextField = "name";
        drop_rwtype.DataValueField = "code";
        drop_rwtype.DataBind();
        ListItem li = new ListItem("请选择");
        drop_rwtype.Items.Add(li);
        drop_rwtype.SelectedIndex = drop_rwtype.Items.Count - 1;


    }
    protected void btn_Query_Click(object sender, EventArgs e)
    {Query();}
    //查询并填入GridView中
    private void Query()
    {
        string constr="";
        if (dropList_type_Query.SelectedIndex != dropList_type_Query.Items.Count - 1)
        {
            //if (dropList_type_Query.SelectedValue.ToString().Trim() == "0")
            //    constr = " and classid='2'";
            //else

                constr = " and classid='" + dropList_type_Query.SelectedValue.ToString().Trim() + "'";
        }
        string strSql = "select ItemID,ItemName,ItemCode from t_M_ItemInfo where bzw=0 " + constr + " order by ItemID";
       
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

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

    #region GridView相关事件函数

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //修改字段标题
            e.Row.Cells[4].Text = "项目类型";
            e.Row.Cells[5].Text = "项目代码";

            //添加按钮列的标题
            TableCell tabcHeaderDetail = new TableCell();
            tabcHeaderDetail.Text = "详细/编辑";
            tabcHeaderDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDetail.Width = 60;
            e.Row.Cells.Add(tabcHeaderDetail);

            TableCell tabcHeaderDel = new TableCell();
            tabcHeaderDel.Text = "删除";
            tabcHeaderDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDel.Width = 30;
            e.Row.Cells.Add(tabcHeaderDel);
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
            ibtnDetail.ImageUrl = "~/Images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/Images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDel.Enabled = false;
            }
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //e.Row.Cells[0].Visible = false;//绑定数据后，隐藏0列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[3].Text;
        string strSql;
       

            strSql = "select * from t_M_ItemInfo where  bzw=0  and ItemID = '" + strSelectedId + "'";
            DataSet ds = new MyDataOp(strSql).CreateDataSet();

            if (ds.Tables.Count == 1)
            {
                strSql = "update t_M_ItemInfo set bzw=1 WHERE ItemID = '" + strSelectedId + "'";
                MyDataOp mdo = new MyDataOp(strSql);
                if (mdo.ExecuteCommand())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
                }
                Query();
           
        }
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CleanAllText();
        initial();
        lbl_Type.Text = "详细";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        //if (strSelectedId != "" && int.Parse(strSelectedId) <= 20)
        //{
        //    btn_OK.Enabled = false;

        //}
        string strSql = @"select * from t_M_ItemInfo 
                        where ItemID='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        txt_ItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
        txt_ItemCode.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString();
        if (ds.Tables[0].Rows[0]["ClassID"] != null && ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
            //if (ds.Tables[0].Rows[0]["ClassID"].ToString().Trim() == "2")
            //    drop_rwtype.SelectedValue = "0";
            //else

                drop_rwtype.SelectedValue = ds.Tables[0].Rows[0]["ClassID"].ToString();
        AllTxtreadOnly();
        btn_OK.Text = "编辑";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }
    
    #endregion

    protected void btn_Add_Click(object sender, EventArgs e)
    {      
        lbl_Type.Text = "添加";
        CleanAllText();
        AllTxtCanWrite();
        initial();
        btn_OK.Enabled = true;
        btn_OK.Text = "确定";
        Query();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
       
    }
    protected void drop_FirSca_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        AllTxtCanWrite();
        Query();
    }
    private string CheckInfo()
    {
        string ret = "";
        if (drop_rwtype.SelectedItem.Text.ToString().Trim() == "请选择")
        {
            ret = "请选择项目类型！";
        }
        return ret; 

    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        if (btn_OK.Text == "编辑")
        {
            AllTxtCanWrite();
            btn_OK.Text = "确定";
            lbl_Type.Text = "编辑";
        }
        else
        {
            #region 当按钮文字为“确定”时，执行添加或编辑操作
            if (CheckInfo() == "")
            {
                string rwtype = drop_rwtype.SelectedValue.ToString().Trim();
                //if (drop_rwtype.SelectedValue.ToString().Trim() == "0")
                //    rwtype = "2";
                //添加新纪录
                if (lbl_Type.Text == "添加")
                {

                  
                    string strSql = @"insert into t_M_ItemInfo
                        (ItemName,ItemCode,classid)  
                        values('" + txt_ItemName.Text + "','" + txt_ItemCode.Text + "','" + rwtype + "')";
                    MyDataOp mdo = new MyDataOp(strSql);
                    bool blSuccess = mdo.ExecuteCommand();
                    if (blSuccess == true)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加成功！');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                    }
                    txt_ItemName.Text = "";
                    txt_ItemCode.Text = "";
                    Query();

                }

                //编辑记录
                if (lbl_Type.Text == "编辑")
                {
                    string strSql = @"update t_M_ItemInfo 
                            set ItemName='" + txt_ItemName.Text + "',ItemCode = '" + txt_ItemCode.Text + "',classid='" + rwtype + "' where ItemID = '" + strSelectedId + "'";
                    MyDataOp mdo = new MyDataOp(strSql);
                    bool blSuccess = mdo.ExecuteCommand();
                    if (blSuccess == true)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
                    }
                    txt_ItemName.Text = "";
                    txt_ItemCode.Text = "";
                    Query();
                }
            #endregion
            }
           
                 else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择项目类型！');", true);
                    }
        }
    }

    #region 对TextBox以及DropDownList等控件进行的一些通用的重复性的操作，包括绑定事件和合法性校验
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
        txt_ItemName.Text = "";
        txt_ItemCode.Text = "";         
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        txt_ItemName.ReadOnly = true;
        txt_ItemCode.ReadOnly = true;      
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        txt_ItemName.ReadOnly = false;
        txt_ItemCode.ReadOnly = false; 
  
    }
    private void SetTxt()//设置详细页面中的TextBox的一些属性
    {
        txt_ItemName.MaxLength = 20;
        txt_ItemCode.MaxLength = 20;      
    }
    private void SetButton()//根据权限设置读写相关的按钮是否可用
    {
        //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
        if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
        {
            btn_Add.Enabled = false;
            btn_OK.Enabled = false;
        }
    }
    #endregion
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Query();
    }
}
