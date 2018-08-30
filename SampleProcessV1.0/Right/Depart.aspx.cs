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

public partial class BaseData_Depart : System.Web.UI.Page
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
        string strSql = "select DepartID 部门ID ,DepartName 部门名称,Contact 联系人,Tel 联系电话 from t_M_DepartInfo where flag=1";

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
       txt_DepartName.Text=grdvw_List.Rows[e.NewEditIndex].Cells[2].Text;
       txt_Contact.Text = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
       txt_tel.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
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
               //e.Row.Cells[1].Visible = false;
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
        strSql = "DELETE FROM t_M_DepartInfo WHERE DepartID = '" + strSelectedId + "'";
      
      
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
    #endregion

    #region 其它函数


    private string Verify()
    {
        string strErrorInfo = "";
        if (txt_DepartName.Text.Trim() == "")
        {
            strErrorInfo += "单位名称不能为空！\\n";
        }
        else
        {
            string con="";
            if(lbl_Type.Text=="编辑")
                con=" and DepartID!='"+strSelectedId+"'";
            string str = "select * from t_M_DepartInfo where DepartName='" + txt_DepartName.Text.Trim() + "' " + con;
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
               strErrorInfo= "已经存在该级别!";
            else
                strErrorInfo= "";
        }
        return strErrorInfo;
    }
   
    #endregion

    protected int GetID()
    {
        int id = 0;
        string str = "select max(DepartID) from t_M_DepartInfo";
        DataSet ds = new MyDataOp(str).CreateDataSet();
        if (ds != null)
            id = int.Parse(ds.Tables[0].Rows[0][0].ToString())+1;
        return id;
    }
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
                string strSql = @"insert into t_M_DepartInfo
                    (DepartID,DepartName,Contact,Tel)  
                    values('"+GetID()+"','" + txt_DepartName.Text.Trim() + "','" + txt_Contact.Text.Trim() + "','" + txt_tel.Text.Trim() + "')";
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
                string strSql = @"update t_M_DepartInfo
                        set DepartName='" + txt_DepartName.Text.Trim() +"',Contact='" + txt_Contact.Text.Trim()  +"',Tel='" + txt_tel.Text.Trim()  +           
                                "' where DepartID='" + strSelectedId + "'";
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
        txt_DepartName.Text = "";
        txt_Contact.Text = "";
        txt_tel.Text = "";
        Query();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        btn_OK.Text = "确定";
        txt_DepartName.Text = "";
        txt_Contact.Text = "";
        txt_tel.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        Query();
    }
}