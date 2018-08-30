using System;
using System.Collections;
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

using WebApp.Components;

public partial class BaseData_HYItem : System.Web.UI.Page
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
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>行业管理</b></font>";
            #endregion
            Query();
        }
    }

    //查询并填入GridView中
    private void Query()
    {
        string strSql = "select id,code,descs from t_行业信息 order by descs";

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
            //DataSet dsbz=new MyDataOp("select * from t_标准字典").CreateDataSet();
            //DataColumn dc = new DataColumn("执行标准");
            //ds.Tables[0].Columns.Add(dc);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (dr["bz"].ToString() != "")
            //    {
            //        DataRow[] drs = dsbz.Tables[0].Select("id=" + int.Parse(dr["bz"].ToString()) + "");
            //        if (drs.Length > 0)
            //        {
            //            dr["执行标准"] = drs[0]["bz"].ToString();
            //        }
            //    }
            //}
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
            e.Row.Cells[2].Text = "行业代码";
            e.Row.Cells[3].Text = "行业";

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
            ////e.Row.Cells[0].Visible = false;//绑定数据后，隐藏0列 
            e.Row.Cells[1].Visible = false;
           // e.Row.Cells[4].Visible = false;
            //e.Row.Cells[3].Visible = false;

        }
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;

        strSql = "select * from t_行业信息 where id = '" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        string[] list = new string[2];
        int i=0;

        if (ds.Tables.Count == 1)
        {
            strSql = "update  t_委托单位 set 行业类别=12  WHERE 行业类别 = '" + strSelectedId + "'";
            list.SetValue(strSql, i++);
            strSql = "DELETE FROM t_行业信息 WHERE id = '" + strSelectedId + "'";
            list.SetValue(strSql, i++);
            MyDataOp mdo = new MyDataOp(strSql);
            if (mdo.DoTran(i,list))
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有该行业类型，数据删除失败！！')", true);
        }
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CleanAllText();
        lbl_Type.Text = "详细";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
        string strSql = @"select * from t_行业信息
                        where id='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        txt_hy.Text = ds.Tables[0].Rows[0]["descs"].ToString();
        txt_hyCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
        //if (ds.Tables[0].Rows[0]["bz"].ToString() != "")
        //{
        //    drop_bz.SelectedValue = ds.Tables[0].Rows[0]["bz"].ToString();
        //}
        //else
        //    drop_bz.SelectedIndex = drop_bz.Items.Count - 1;
        AllTxtreadOnly();
        btn_OK.Text = "编辑";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }

    #endregion

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        CleanAllText();
        //drop_bz.SelectedIndex = drop_bz.Items.Count - 1;
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

                string strSql = @"insert into t_行业信息
                        (descs,code)  
                        values('" + txt_hy.Text + "','" + txt_hyCode.Text + "')";
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
                txt_hy.Text = "";
                txt_hyCode.Text = "";
                Query();

            }

            //编辑记录
            if (lbl_Type.Text == "编辑")
            {
                string strSql = @"update t_行业信息 
                            set descs='" + txt_hy.Text + "',des='" + txt_hy.Text + "',code = '" + txt_hyCode.Text + "' where id = '" + strSelectedId + "'";
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
                txt_hy.Text = "";
                txt_hyCode.Text = "";
                Query();
            }
            #endregion
        }
    }

    #region 对TextBox以及DropDownList等控件进行的一些通用的重复性的操作，包括绑定事件和合法性校验
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
        txt_hy.Text = "";
        txt_hyCode.Text = "";
        //MyStaVoid.BindList("bz", "id", "select * from t_标准字典", drop_bz);
        //ListItem newItem = new ListItem("请选择", "");
        //drop_bz.Items.Add(newItem);
       
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        txt_hy.ReadOnly = true;
        txt_hyCode.ReadOnly = true;
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        txt_hy.ReadOnly = false;
        txt_hyCode.ReadOnly = false;
    }
    private void SetTxt()//设置详细页面中的TextBox的一些属性
    {
        txt_hy.MaxLength = 20;
        txt_hyCode.MaxLength = 20;
        
    }
    private void SetButton()//根据权限设置读写相关的按钮是否可用
    {
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
