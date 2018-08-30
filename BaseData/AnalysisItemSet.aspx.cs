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

public partial class BaseData_AnalysisItemSet : System.Web.UI.Page
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
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClass order by ClassID", dropList_type);
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClass order by ClassID", dropList_type_S);
            ListItem townList = new ListItem("请选择", "");
            dropList_type_S.Items.Add(townList);
            dropList_type_S.SelectedValue = "";
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>分析项目管理</b></font>";
            #endregion
            Query();
           
        }        
    }

    //查询并填入GridView中
    private void Query()
    {
        string strS = "";
        if (dropList_type_S.SelectedValue != "")
        {
            strS += " and t_M_AnalysisItem.ClassID = '" + dropList_type_S.SelectedValue + "'";
        }
        if (txt_AIName_S.Text.Trim() != "")
        {
            strS += " and t_M_AnalysisItem.AICode like '" + txt_AIName_S.Text.Trim() + "%'";       
        }
        strS += "  order by t_M_AnalysisItem.ClassID";

        string strSql = "select id,t_M_AnalysisMainClass.ClassName,AIName,AICode from t_M_AnalysisItem,t_M_AnalysisMainClass where t_M_AnalysisItem.ClassID = t_M_AnalysisMainClass.ClassID";
        strSql += strS;

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
            e.Row.Cells[4].Text = "分析项目类别";
            e.Row.Cells[5].Text = "分析项目类型";
            e.Row.Cells[6].Text = "分析项目代码";

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

        strSql = "select * from t_M_AnalysisItem where id = '" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
   
        if (ds.Tables.Count == 1)
        {
            strSql = "DELETE FROM t_M_AnalysisItem WHERE id = '" + strSelectedId + "'";
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
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有该项目类型，数据删除失败！！')", true);
        }
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CleanAllText();
        lbl_Type.Text = "详细";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        string strSql = @"select * from t_M_AnalysisItem 
                        where id='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        dropList_type.SelectedValue = ds.Tables[0].Rows[0]["ClassID"].ToString();
        txt_AIName.Text = ds.Tables[0].Rows[0]["AIName"].ToString();
        txt_AICode.Text = ds.Tables[0].Rows[0]["AICode"].ToString();
        AllTxtreadOnly();
        btn_OK.Text = "编辑";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }

    protected void dropList_type_S_SelectedIndexChanged(object sender, EventArgs e)
    {
         Query();       
    }
    #endregion

    protected void btn_Add_Click(object sender, EventArgs e)
    {      
        lbl_Type.Text = "添加";
        CleanAllText();
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

    protected void btn_Query_Click(object sender, EventArgs e)
    {
        Query();
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

            //添加新纪录
            if (lbl_Type.Text == "添加")
            {
              string str = "select id,AIName from t_M_AnalysisItem where AIName='" + txt_AIName.Text + "'";

                                    DataSet dscheck = new MyDataOp(str).CreateDataSet();
                                    if (dscheck.Tables[0].Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已存在该分析项目！请直接在原有分析项目上修改信息！');", true);

                                    }
                                    else
                                    {
                                        string strSql = @"insert into t_M_AnalysisItem
                        (ClassID,AIName,AICode)  
                        values('" + dropList_type.SelectedValue.ToString() + "','" + txt_AIName.Text + "','" + txt_AICode.Text + "')";
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
                                        txt_AIName.Text = "";
                                        txt_AICode.Text = "";
                                   
                Query();
                                    }

            }

            //编辑记录
            if (lbl_Type.Text == "编辑")
            {
               string str = "select id,AIName from t_M_AnalysisItem where AIName='" + txt_AIName.Text + "' and id!='" + strSelectedId + "'";

                                    DataSet dscheck = new MyDataOp(str).CreateDataSet();
                                    if (dscheck.Tables[0].Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已存在同名称的分析项目！请直接在原有分析项目上修改信息！');", true);

                                    }
                                    else
                                    {
                                        string strSql = @"update t_M_AnalysisItem 
                            set ClassID = '" + dropList_type.SelectedValue.ToString() + "',AIName='" + txt_AIName.Text + "',AICode = '" + txt_AICode.Text + "' where id = '" + strSelectedId + "'";
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
                                        txt_AIName.Text = "";
                                        txt_AICode.Text = "";
                                        Query();
                                    }
            }
            #endregion
        }
    }

    #region 对TextBox以及DropDownList等控件进行的一些通用的重复性的操作，包括绑定事件和合法性校验
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
        //dropList_type.SelectedValue = "";
        txt_AIName.Text = "";
        txt_AICode.Text = "";         
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        dropList_type.Enabled = false;
        txt_AIName.ReadOnly = true;
        txt_AICode.ReadOnly = true;      
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        dropList_type.Enabled = true;
        txt_AIName.ReadOnly = false;
        txt_AICode.ReadOnly = false;   
    }
    private void SetTxt()//设置详细页面中的TextBox的一些属性
    {
        txt_AIName.MaxLength = 20;
        txt_AICode.MaxLength = 20;      
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
    protected void txt_AIName_S_TextChanged(object sender, EventArgs e)
    {
        Query();
    }
}
