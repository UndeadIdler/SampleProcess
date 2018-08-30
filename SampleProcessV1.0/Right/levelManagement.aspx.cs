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

public partial class BaseData_levelManagement : System.Web.UI.Page
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
            Query();            
        }
    }

    private void Query()
    {
        string strSql = "select t_操作员级别.级别 级别id,级别名称 from t_操作员级别";

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
            //translateTheIDToName();
        } 
    }
    
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_Type.Text = "编辑";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
       txt_levelName.Text=grdvw_List.Rows[e.NewEditIndex].Cells[2].Text;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "详细/编辑";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);


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
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDel.Enabled = false;
            }
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                //绑定数据后，隐藏4,5,6,7列 
                // e.Row.Cells[4].Visible = false;
               // e.Row.Cells[1].Visible = false;
                //e.Row.Cells[6].Visible = false;
                //e.Row.Cells[7].Visible = false;
                //e.Row.Cells[8].Visible = false;
            }
        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string[] deletelist = new string[4];
        strSql = "DELETE FROM t_操作员级别 WHERE 级别 = '" + strSelectedId + "'";
        deletelist.SetValue("delete from t_操作员信息 where 所属角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 0);
        deletelist.SetValue("delete from t_角色菜单关系表 where 角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 1);
         deletelist.SetValue("DELETE FROM t_角色信息 WHERE 级别id = '" + strSelectedId + "'",2);

        deletelist.SetValue(strSql, 3);
        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(4,deletelist))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        Query();
    }
    #endregion

    #region 其它函数


    private string Verify()
    {
        string strErrorInfo = "";
        if (txt_levelName.Text.Trim() == "")
        {
            strErrorInfo += "操作员级别不能为空！\\n";
        }
        else
        {
            string str = "select * from t_操作员级别 where 级别名称='" + txt_levelName.Text.Trim() + "'";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
               strErrorInfo= "已经存在该级别!";
            else
                strErrorInfo= "";
        }
        return strErrorInfo;
    }
   
    #endregion

    protected void btn_OK_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

        }
        else
        {
            #region 添加新纪录
            if (lbl_Type.Text == "添加")
            {
                string strSql = @"insert into t_操作员级别
                    (级别名称)  
                    values('" + txt_levelName.Text.Trim() + "')";
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
            }
            #endregion

            #region 编辑记录
            if (lbl_Type.Text == "编辑")
            {
                string strSql = @"update t_操作员级别 
                        set 级别名称='" + txt_levelName.Text.Trim() +
                                "' where 级别='" + strSelectedId + "'";
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
            }

            #endregion
            Query();
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        txt_levelName.Text = "";
        Query();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        btn_OK.Text = "确定";
        txt_levelName.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        Query();
    }
}