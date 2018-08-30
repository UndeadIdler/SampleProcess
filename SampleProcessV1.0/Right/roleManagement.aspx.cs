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

public partial class BaseData_roleManagement : System.Web.UI.Page
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

            MyStaVoid.BindList("LevelName", "LevelID", "select * from t_R_UserLevel where LevelID>"+Request.Cookies["Cookies"].Values["u_level"].ToString(), drop_UserLevel);
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>角色列表</b></font>";
            //SetButton();  
             #endregion
            SetButton();
            Query(); 
        }
    }


    private void Query()
    {
        string strSql = "select t_R_Role.RoleID,t_R_Role.RoleName 角色名称,t_R_UserLevel.LevelName 级别名称 " +
            " from t_R_UserLevel,t_R_Role " +
            " where t_R_UserLevel.LevelID=t_R_Role.LevelID and t_R_Role.LevelID='" + drop_UserLevel.SelectedValue + "' order by RoleID";

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
    
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void ItemBind()
    {
        cbl_itemlist.Items.Clear();
        string strSql = "select ItemID,ItemName,classid from t_M_ItemInfo where bzw=0  order by classid";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        cbl_itemlist.DataSource = ds;
        cbl_itemlist.DataTextField = "ItemName";
        cbl_itemlist.DataValueField = "ItemID";
        cbl_itemlist.DataBind();
    }

    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_Type.Text = "编辑";
        clear();
        ItemBind();
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
        DataSet ds_CheckPur = new MyDataOp("Select * from t_R_Role where t_R_Role.RoleID='" + strSelectedId + "'").CreateDataSet();
        //DataSet ds_Check = new MyDataOp("select * from t_角色菜单关系表 where t_角色菜单关系表.id='" + strSelectedId + "'").CreateDataSet();


        txt_roleName.Text = ds_CheckPur.Tables[0].Rows[0]["RoleName"].ToString();
        TextBox_reflashTime.Text = ds_CheckPur.Tables[0].Rows[0]["RefreshRight"].ToString();
        if (ds_CheckPur.Tables[0].Rows[0]["ReadRight"].ToString() == "1")
            CheckBox_readwrite.Checked = true;
        else
            CheckBox_readwrite.Checked = false;
        if (ds_CheckPur.Tables[0].Rows[0]["RefreshRight"].ToString() == "1")
            CheckBox_reflash.Checked = true;
        else
            CheckBox_reflash.Checked = false;
        if (ds_CheckPur.Tables[0].Rows[0]["WriteRight"].ToString() == "1")
            CheckBox_set.Checked = true;
        else
            CheckBox_set.Checked = false;
        if (ds_CheckPur.Tables[0].Rows[0]["dataflag"].ToString() == "1")
            CheckBox_Data.Checked = true;
        else
            CheckBox_Data.Checked = false;
        if (ds_CheckPur.Tables[0].Rows[0]["FileRight"].ToString() == "1")
            CheckBox_control.Checked = true;
        else CheckBox_control.Checked = false;
        string strSql = "select roleid,itemid from t_roleItem where roleid='"+strSelectedId+"'";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        foreach (ListItem li in cbl_itemlist.Items)
        {
            DataRow[] dr = ds.Tables[0].Select("itemid='" + li.Value.Trim() + "'");
            if (dr.Length > 0)
            {
                li.Selected = true;
            }
        }   
       
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

            TableCell headerset = new TableCell();
            headerset.Text = "菜单配置";
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

            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/Images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            TableCell MenuSet = new TableCell();
            MenuSet.Width = 60;
            MenuSet.Style.Add("text-align", "center");
            ImageButton btMenuSet = new ImageButton();
            btMenuSet.ImageUrl="~/Images/Detail.gif";
            btMenuSet.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuSet.Controls.Add(btMenuSet);
            e.Row.Cells.Add(MenuSet);

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
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        strSql = "DELETE FROM t_R_Role WHERE RoleID = '" + strSelectedId + "'";
        string[] deletelist = new string[4];
        deletelist.SetValue(strSql, 0);
        deletelist.SetValue("delete from t_R_RoleMenu where RoleID='" + strSelectedId + "'", 1);
        deletelist.SetValue("delete from t_R_UserInfo where RoleID='" + strSelectedId + "'", 2);
        deletelist.SetValue("delete from t_roleitem where roleid='" + strSelectedId + "'", 3);
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
    private void btMenuSet_Click(object sender, GridViewSelectEventArgs e)
    {
        grdvw_List_RowSelecting(sender,e);
    }
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('RoleTree.aspx?kw=" + strSelectedId + "','theNewWindow','width=400,height=700,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);
    }
    
    #endregion

    protected void drop_UserLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        Query();
    }
    protected void drop_Level_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        Query();
    }
   

    #region 其它函数
    
    //将各个id分别翻译成相对应的名称
    private void translateTheIDToName()
    {
        for (int i = 0; i < grdvw_List.Rows.Count; i++)
        {
            string strSql = "";
            DataSet ds = new DataSet();

            //翻译操作员id为用户名
            strSql="select * from t_R_UserInfo where id='"+grdvw_List.Rows[i].Cells[4].Text+"'";
            ds = new MyDataOp(strSql).CreateDataSet();
            grdvw_List.Rows[i].Cells[0].Text = ds.Tables[0].Rows[0]["UserID"].ToString();

            strSql = "select * from t_R_Level where LevelID='" + grdvw_List.Rows[i].Cells[5].Text + "'";
            ds = new MyDataOp(strSql).CreateDataSet();
            grdvw_List.Rows[i].Cells[1].Text = ds.Tables[0].Rows[0]["LevelName"].ToString();

            strSql="select * from t_R_Role where RoleID='"+grdvw_List.Rows[i].Cells[6].Text+"'";
            ds = new MyDataOp(strSql).CreateDataSet();
            grdvw_List.Rows[i].Cells[2].Text = ds.Tables[0].Rows[0]["RoleName"].ToString();

            if (grdvw_List.Rows[i].Cells[5].Text == "6")
            {
                grdvw_List.Rows[i].Cells[3].Text = "";
            }
            //else
            //{
            //    //strSql = "select * from ";
            //    //switch (grdvw_List.Rows[i].Cells[5].Text)
            //    //{
            //    //    case "4":
            //    //        strSql += " t_第一级信息 ";
            //    //        break;
            //    //    case "5":
            //    //        strSql += " t_第二级信息 ";
            //    //        break;
            //    //    case "12":
            //    //        strSql += " t_第三级信息 ";
            //    //        break;
            //    //    default:
            //    //        strSql += " t_第三级信息 ";
            //    //        break;
            //    }
                //strSql += " where ID='" + grdvw_List.Rows[i].Cells[7].Text + "'";
                //ds = new MyDataOp(strSql).CreateDataSet();
                //grdvw_List.Rows[i].Cells[3].Text = ds.Tables[0].Rows[0]["名称"].ToString();
            //}
        }
    }
    private string Verify()
    {
        string strErrorInfo = "";
        //if (txt_UserName.Text == "")
        //{
        //    strErrorInfo+="用户名不能为空！\\n";
        //}
        //if (drop_Role.Items.Count == 0)
        //{
        //    strErrorInfo+="请先添加该级别的角色！\\n";
        //}
        //if (drop_FirSca_Name.Items.Count == 0&&drop_FirSca_Name.Visible==true)
        //{
        //    strErrorInfo+="请添加"+MyStaVoid.getScaleName(1)+"！\\n";
        //}
        //if (drop_SecSca_Name.Items.Count == 0 && drop_SecSca_Name.Visible == true)
        //{
        //    strErrorInfo+="请添加"+MyStaVoid.getScaleName(2)+"！\\n";
        //}
        //if (drop_ThrSca_Name.Items.Count == 0 && drop_ThrSca_Name.Visible == true)
        //{
        //    strErrorInfo+="请添加" + MyStaVoid.getScaleName(3) + "！\\n";
        //}
        return strErrorInfo;
    }
    //private void BindDrop()
    //{
    //    //-----FirstScale----------
    //    lbl_FirSca_Title.Text = MyStaVoid.getScaleName(1) + "名称：";
    //    MyStaVoid.BindList("名称", "id", "select * from t_第一级信息", drop_FirSca_Name);
    //    //-----SecondScale---------
    //    lbl_SecSca_Title.Text = MyStaVoid.getScaleName(2) + "名称：";
    //    MyStaVoid.BindList("名称", "id", "select * from t_第二级信息 where 上级id='" + drop_FirSca_Name.SelectedValue + "'", drop_SecSca_Name);
    //    //-----ThirdScale----------
    //    lbl_ThrSca_Title.Text = MyStaVoid.getScaleName(3) + "名称：";
    //    MyStaVoid.BindList("名称", "id", "select * from t_第三级信息 where 上级id='" + drop_SecSca_Name.SelectedValue + "'", drop_ThrSca_Name);
    //    //------Other-------------
    //    MyStaVoid.BindList("级别名称", "级别", "select * from t_操作员级别 where 级别>'"+Request.Cookies["Cookies"].Values["u_level"].ToString()+"'", drop_Level);
    //    MyStaVoid.BindList("角色名称", "id", "select * from t_角色信息 where 级别id='" + drop_Level.SelectedValue + "' order by id ", drop_Role);
    //}
    //private void SetButton()
    //{
    //    if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
    //    {
    //        btn_Add.Enabled = false;
    //        btn_OK.Enabled = false;
    //    }
    //}

    #endregion

    protected void btn_OK_Click(object sender, EventArgs e)
    {
        string strErrorInfo=Verify();
        if (strErrorInfo!="")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('"+strErrorInfo+"');", true);
            Query();
            return;
        }
       
        int vreadwrite = 0, vreflash = 0, vset = 0, vcontrol = 0,vdata=0;
        if(CheckBox_readwrite.Checked)
            vreadwrite=1;
        if(CheckBox_reflash.Checked)
            vreflash=1;
        if(CheckBox_set.Checked)
            vset=1;
        if (CheckBox_control.Checked)
            vcontrol = 1;
        if (CheckBox_Data.Checked)
            vdata = 1;
        #region 添加新纪录
        if (lbl_Type.Text == "添加")
        {
            string strSql = @"insert into t_R_Role(RoleName,LevelID,ReadRight,RefreshRight,WriteRight,FileRight,dataflag)values('" + txt_roleName.Text.Trim() + "','" + drop_UserLevel.SelectedValue + "','" + vreadwrite + "','" + vreflash + "','" + vset + "','" + vcontrol + "','"+vdata+"')";  
 
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.ExecuteCommand();
            if (blSuccess == true)
            {
                string str = "select RoleID from t_R_Role where RoleName='" + txt_roleName.Text.Trim() + "' and LevelID='" + drop_UserLevel.SelectedValue + "'";
                DataSet ds = new MyDataOp(str).CreateDataSet();
                DataSet ds_menu = new MyDataOp("select * from t_R_Menu").CreateDataSet();
                int n=ds_menu.Tables[0].Rows.Count+cbl_itemlist.Items.Count+1;
                string[] list = new string[n];
                int i=0;
                foreach(DataRow dr in ds_menu.Tables[0].Rows)
                {
                    list.SetValue("insert into t_R_RoleMenu(MenuID,RoleID,checked)values('" + dr["ID"].ToString() + "','" + ds.Tables[0].Rows[0][0].ToString() + "','0')", i++);
                }

                list.SetValue("delete from t_roleitem where roleid='" + strSelectedId + "'", i++);
                foreach (ListItem li in cbl_itemlist.Items)
                {
                    if (li.Selected)
                        list.SetValue("insert into t_roleitem(roleid,itemid,createdate)values('" + strSelectedId + "','" + li.Value.Trim() + "',getdate()", i++);
                }
               
                blSuccess=mdo.DoTran(i,list);
                if(blSuccess==true)
                {
                     ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('角色添加成功！');", true);
                }
                else
                {
                    string deleteRollBack = "delete from t_R_Role  where RoleID='" + ds.Tables[0].Rows[0][0].ToString() + "'";
                    MyDataOp deletedo=new MyDataOp(deleteRollBack);
                    blSuccess=deletedo.ExecuteCommand();
                    if(blSuccess==true)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('角色添加失败！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('角色添加失败！');", true);
            }
        }
        #endregion

        #region 编辑记录
        if (lbl_Type.Text == "编辑")
        {
            int i = 0;
            string[] strlist = new string[cbl_itemlist.Items.Count + 2];
            string strSql = @"update t_R_Role  set RoleName='" + this.txt_roleName.Text.Trim() +
                             "',ReadRight='" + vreadwrite +
                             "',RefreshRight='" + vreflash +
                             "',WriteRight='" + vset +
                             "',FileRight='"+vcontrol+
                             "',dataflag='"+vdata+
                             "' where RoleID='" + strSelectedId + "'";
            strlist.SetValue(strSql, i++);
            strlist.SetValue("delete from t_roleitem where roleid='"+strSelectedId+"'", i++);
            foreach (ListItem li in cbl_itemlist.Items)
            {
               if( li.Selected)
                strlist.SetValue("insert into t_roleitem(roleid,itemid,createdate)values('" + strSelectedId + "','"+li.Value.Trim()+"',getdate())", i++);
            }
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.DoTran(i, strlist);
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
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        txt_roleName.Text = "";
        Query();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        clear();
        
        btn_OK.Text = "确定";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        Query();
    }
    private void clear()
    {
        txt_roleName.Text = "";
        CheckBox_reflash.Checked = false;
        CheckBox_set.Checked = false;
        CheckBox_readwrite.Checked = false;
        //CheckBox_control.Checked = false;
        TextBox_reflashTime.Text = "0";
    }
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            btn_Add.Enabled = false;
            btn_OK.Enabled = false;
        }
        else
        {
            btn_Add.Enabled = true;
            btn_OK.Enabled = true;
        }
    }

}