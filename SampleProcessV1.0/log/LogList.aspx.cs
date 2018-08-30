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

public partial class log_LogList : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string name//所选择操作列记录对应的id
    {
        get { return (string)ViewState["name"]; }
        set { ViewState["name"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面元素
            string sql = "select name from t_R_UserInfo where userid = '" + Request.Cookies["Cookies"].Values["u_id"].ToString().Trim() + "'";
            DataSet ds = new MyDataOp(sql).CreateDataSet();
            name = ds.Tables[0].Rows[0][0].ToString();

            //SetTxt();
           //SetButton();
            txts_time1.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txts_time1.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txts_time2.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txts_time2.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txt_date.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>工作日志列表</b></font>";
            #endregion
            Query();
        }
    }

    //查询并填入GridView中
    private void Query()
    {
        string str = "";
        if (txts_name.Text.Trim() != "") str += " and t_R_UserInfo.name like '%" + txts_name.Text.Trim() + "%'";
        if (txts_ulog.Text.Trim() != "") str += " and ulog like '%" + txts_ulog.Text.Trim() + "%'";
        if (txts_time1.Text.Trim() != "") str += " and dtime >= '" + txts_time1.Text.Trim() + "'";
        if (txts_time2.Text.Trim() != "") str += " and dtime <= '" + txts_time2.Text.Trim() + " 23:59:59'";

        string strSql = "select t_R_UserLog.id,t_R_UserLog.name as 工作人员,ulog as 工作内容,dtime as 工作时间,DepartName 所在部门,UserID from t_R_UserLog inner join t_R_UserInfo on t_R_UserLog.uid=t_R_UserInfo.UserID inner join t_M_DepartInfo on t_R_UserInfo.DepartID=t_M_DepartInfo.DepartID  where 1=1 " + str + " order by t_R_UserLog.id desc";
       
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
            for (int i = 0; i < grdvw_List.Rows.Count; i++)
            {
                if (grdvw_List.Rows[i].Cells[8].Text.Trim() != Request.Cookies["Cookies"].Values["u_id"].ToString())
                {
                    ImageButton fibtn = (ImageButton)grdvw_List.Rows[i].FindControl("editid");
                    fibtn.Visible = false;
                }               
            }
        }
    }

    #region GridView相关事件函数

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //修改字段标题
            //e.Row.Cells[4].Text = "样品类型";
            //e.Row.Cells[5].Text = "样品类型代码";

            //添加按钮列的标题
            TableCell tabcHeaderDetail = new TableCell();
            tabcHeaderDetail.Text = "详细/编辑";
            tabcHeaderDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDetail.Width = 60;
            e.Row.Cells.Add(tabcHeaderDetail);

            //TableCell tabcHeaderDel = new TableCell();
            //tabcHeaderDel.Text = "删除";
            //tabcHeaderDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //tabcHeaderDel.Width = 30;
            //e.Row.Cells.Add(tabcHeaderDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = id.ToString();
            e.Row.Cells[5].Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();            
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ID = "editid";
            ibtnDetail.ImageUrl = "~/Images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);           
            e.Row.Cells.Add(tabcDetail);         


            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/Images/Delete.gif";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            ////if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
           
            //tabcDel.Controls.Add(ibtnDel);
            //e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //e.Row.Cells[0].Visible = false;//绑定数据后，隐藏0列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[8].Visible = false;
            
        }
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    //protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[3].Text;
    //    string strSql;

    //    strSql = "select * from t_R_UserLog where TypeID = '" + strSelectedId + "'";
    //    DataSet ds = new MyDataOp(strSql).CreateDataSet();
   
    //    if (ds.Tables.Count == 1)
    //    {
    //        strSql = "DELETE FROM t_R_UserLog WHERE TypeID = '" + strSelectedId + "'";
    //        MyDataOp mdo = new MyDataOp(strSql);
    //        if (mdo.ExecuteCommand())
    //        {
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('日志删除成功!')", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('日志删除失败！')", true);
    //        }
    //        Query();
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有该日志，数据删除失败！！')", true);
    //    }
    //}
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        CleanAllText();
        lbl_Type.Text = "详细";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        string strSql = @"select * from t_R_UserLog 
                        where id='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        txt_name.Text = ds.Tables[0].Rows[0]["name"].ToString().Trim();
        txt_ulog.Text = ds.Tables[0].Rows[0]["ulog"].ToString().Trim();
        txt_date.Text = ds.Tables[0].Rows[0]["dtime"].ToString().Trim();
        AllTxtreadOnly();
        btn_OK.Text = "编辑";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }
    
    #endregion

    protected void btn_Add_Click(object sender, EventArgs e)
    {      
        lbl_Type.Text = "添加";
        CleanAllText();
        btn_OK.Text = "确定";
        //txt_name.Text = Request.Cookies["Cookies"].Values["u_id"].ToString().Trim();       
        txt_name.Text = name;
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

            if (txt_date.Text.Trim() == string.Empty || txt_ulog.Text.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('信息填写不完整！');", true);
                return;
            }

            //添加新纪录
            if (lbl_Type.Text == "添加")
            {

                string strSql = @"insert into t_R_UserLog
                        (name,ulog,utime,dtime,uid)  
                        values('" + txt_name.Text.Trim() + "','" + txt_ulog.Text.Trim() + "','" + DateTime.Now.ToString() + "','" + txt_date.Text.Trim() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
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
                Query();

            }

            //编辑记录
            if (lbl_Type.Text == "编辑")
            {
                string strSql = @"update t_R_UserLog 
                            set ulog='" + txt_ulog.Text.Trim() + "',dtime='" + txt_date.Text.Trim() + "' where id = '" + strSelectedId + "'";
                MyDataOp mdo = new MyDataOp(strSql);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据编辑失败！');", true);
                }
                //CleanAllText();
                Query();
            }
            #endregion
        }
    }

    #region 对TextBox以及DropDownList等控件进行的一些通用的重复性的操作，包括绑定事件和合法性校验
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
       
        txt_name.Text = "";
        txt_ulog.Text = "";
        txt_date.Text = "";
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        txt_date.Enabled = false;
        txt_ulog.ReadOnly = true;      
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        txt_date.Enabled = true;
        txt_ulog.ReadOnly = false;   
    }
    //private void SetTxt()//设置详细页面中的TextBox的一些属性
    //{
    //    txt_name.MaxLength = 100;
    //    txt_ulog.MaxLength = 100;      
    //}
    //private void SetButton()//根据权限设置读写相关的按钮是否可用
    //{
    //    //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
    //    if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            
    //    {
    //        btn_Add.Enabled = false;
    //        btn_OK.Enabled = false;
    //    }
    //}
    #endregion
   
    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }
}
