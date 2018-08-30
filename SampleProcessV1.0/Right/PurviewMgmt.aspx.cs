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

public partial class BaseData_PurviewMgmt1 : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strUserId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strUserId"]; }
        set { ViewState["strUserId"] = value; }
    }
    private DataTable dtItemA //监测项目表
    {
        get { return (DataTable)ViewState["dtItemA"]; }
        set { ViewState["dtItemA"] = value; }
    }
    private DataTable dtItemB //监测项目表
    {
        get { return (DataTable)ViewState["dtItemB"]; }
        set { ViewState["dtItemB"] = value; }
    }
    private DataTable dt_analysisA//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysisA"]; }
        set { ViewState["dt_analysisA"] = value; }
    }
  private  DAl.Item itemObj = new DAl.Item();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面元素
          
           // dtItem = itemObj.GetItem();
            MyStaVoid.BindList("LevelName", "LevelID", "select * from t_R_UserLevel where LevelID>'" + Request.Cookies["Cookies"].Values["u_level"].ToString() + "'", drop_UserLevel);
          
            ////-----FirstScale----------
            //lbl_FirSca_Title.Text = MyStaVoid.getScaleName(2) + "名称：";
            ////-----SecondScale---------
            //lbl_SecSca_Title.Text = MyStaVoid.getScaleName(3) + "名称：";
            ////-----ThirdScale----------
            //lbl_ThrSca_Title.Text = "所属部门：";
            //------Other-------------
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>用户管理</b></font>";

            SetButton();
            #endregion

            Query();            
        }
    }
    protected void btn_Query_Click(object sender, EventArgs e) 
    {Query();}
    private void Query()
    {
        string str = "";
        if (txt_Query.Text.Trim() != "")
        { str = " and Name like '%" + txt_Query.Text.Trim() + "%'"; }
        string strSql = "select t_R_UserInfo.id,t_R_UserInfo.UserID,Name,t_R_Role.LevelID,t_R_UserLevel.LevelName,t_R_Role.RoleID,t_R_Role.RoleName,t_R_UserInfo.DepartID,DepartName" +
            " from t_R_UserInfo ,t_R_Role,t_M_DepartInfo,t_R_UserLevel " +
            "where t_R_UserInfo.RoleID=t_R_Role.RoleID and t_R_UserInfo.DepartID=t_M_DepartInfo.DepartID and t_R_UserLevel.LevelID=t_R_Role.LevelID";

        strSql += " and t_R_Role.LevelID='" + drop_UserLevel.SelectedValue + "'" + str + " and t_R_UserInfo.flag=0 order by t_R_UserInfo.DepartID ";
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
       
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text;
        txt_name.Text= grdvw_List.Rows[e.NewEditIndex].Cells[2].Text;
        DataSet ds = new MyDataOp("select * from t_R_UserInfo,t_R_Role where t_R_UserInfo.RoleID=t_R_Role.RoleID and t_R_UserInfo.id='" +
            strSelectedId + "'").CreateDataSet();
        MyStaVoid.BindList("DepartName", "DepartID", "select * from t_M_DepartInfo order by DepartID ", drop_ThrSca_Name);
        drop_ThrSca_Name.SelectedItem.Selected = false;
        drop_ThrSca_Name.Items.FindByValue(ds.Tables[0].Rows[0]["DepartID"].ToString()).Selected = true;
        txt_UserName.Text = ds.Tables[0].Rows[0]["UserID"].ToString();
        strUserId = txt_UserName.Text.Trim();
        if (ds.Tables[0].Rows[0]["Analysisflag"].ToString() == "1")
        {
          panel_role.Visible = true;
          panel_a.Visible = false;
          panel_b.Visible = false;
          ABRoleGroup(strUserId);
        }
        else
        {
            panel_role.Visible = false;
            panel_a.Visible = false;
            panel_b.Visible = false;
        }
        BindDrop();
       // MyStaVoid.BindList("LevelName", "LevelID", "select * from t_R_UserLevel where LevelID>'" + Request.Cookies["Cookies"].Values["u_level"].ToString() + "'", drop_Level);
        drop_Level.SelectedItem.Selected=false;
        drop_Level.Items.FindByValue(ds.Tables[0].Rows[0]["LevelID"].ToString()).Selected = true;
       // MyStaVoid.BindList("RoleName", "RoleID", "select * from t_R_Role where LevelID='" + ds.Tables[0].Rows[0]["LevelID"].ToString()+ "' order by RoleID ", drop_Role);
        drop_Role.SelectedItem.Selected = false;
        drop_Role.Items.FindByValue(ds.Tables[0].Rows[0]["RoleID"].ToString()).Selected = true;
       
        Txt_pwd.Text = ds.Tables[0].Rows[0]["PWD"].ToString();

        
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

            TableCell headerSel= new TableCell();
            headerSel.Text = "可写权限";
            headerSel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerSel.Width = 60;
            e.Row.Cells.Add(headerSel);

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
            //手动添加详细和删除按钮
            TableCell tabcSel = new TableCell();
            tabcSel.Width = 60;
            tabcSel.Style.Add("text-align", "center");
            ImageButton ibtnsel = new ImageButton();
            ibtnsel.ImageUrl = "~/Images/Detail.gif";
            ibtnsel.CommandName = "Select";
            tabcSel.Controls.Add(ibtnsel);
            e.Row.Cells.Add(tabcSel);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/Images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");

            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDetail.Enabled = false;
                ibtnsel.Enabled = false;
                ibtnDel.Enabled = false;
            }
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[13].Visible = false;
            //e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[14].Visible = false;
            
        }
    }
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[6].Text;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('UserTree.aspx?kw=" + strSelectedId + "','theNewWindow','width=400,height=700,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);
    }
    
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[6].Text;
        string strSql;
        strSql = "update t_R_UserInfo set flag=1 WHERE id = '" + strSelectedId + "'";
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

    protected void drop_UserLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Query();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
    }
    protected void drop_Level_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyStaVoid.BindList("RoleName", "RoleID", "select * from t_R_Role where LevelID='" + drop_Level.SelectedValue + "'", drop_Role);
        
        Query();
    }

    #region 其它函数
    
   
    private string Verify(string checkStr)
    {
        string strErrorInfo = "";
        if (txt_UserName.Text.Trim() == "")
        {
            strErrorInfo+="用户名不能为空！\\n";
        }
        if(txt_name.Text.Trim()=="")
        {
            strErrorInfo += "姓名不能为空！\\n";
        }
        if (drop_Role.Items.Count == 0)
        {
            strErrorInfo+="请先添加该级别的角色！\\n";
        }
        
        if (drop_ThrSca_Name.Items.Count == 0 && drop_ThrSca_Name.Visible == true)
        {
            strErrorInfo += "请添加";
            //strErrorInfo+="请添加" + MyStaVoid.getScaleName(4) + "！\\n";
        }
        //if (checkStr == "添加")
        //{
            string str = "select * from t_R_UserInfo where UserID='" + txt_UserName.Text.Trim() + "' and id!='"+strSelectedId+"'";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strErrorInfo += "该用户名已经存在！\\n";
            }
        //}
        return strErrorInfo;
    }
    private void BindDrop()
    {

        MyStaVoid.BindList("LevelName", "LevelID", "select * from t_R_UserLevel where LevelID>'" + Request.Cookies["Cookies"].Values["u_level"].ToString() + "'", drop_Level);
        MyStaVoid.BindList("RoleName", "RoleID", "select * from t_R_Role where LevelID='" + drop_UserLevel.SelectedValue + "' order by RoleID ", drop_Role);
    }
       
   

    #endregion

    protected void btn_OK_Click(object sender, EventArgs e)
    {
        string strErrorInfo = Verify(lbl_Type.Text);
        if (strErrorInfo!="")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('"+strErrorInfo+"');", true);
           
            return;
        }
        //获得所属地id
        string  strAttribtionID="";
        strAttribtionID = drop_ThrSca_Name.SelectedValue;
        DateTime nowTime = DateTime.Parse(DateTime.Now.ToString());
        DAl.User.Users userobj = new DAl.User.Users();
        Entity.User.Users userentity = new Entity.User.Users();
       
        userentity.UserID = txt_UserName.Text;
        userentity.PWD = Txt_pwd.Text.Trim();
        userentity.DepartID = int.Parse(strAttribtionID);
        userentity.RoleID = int.Parse(drop_Role.SelectedValue);
        userentity.PWDModifyTime = nowTime;
        userentity.Name = txt_name.Text.Trim();
        
        #region 添加新纪录
        if (lbl_Type.Text == "添加")
        {
            int ret=userobj.AddUsers(userentity);
            if (ret >= 1)
            {strSelectedId=ret.ToString();
            panel_role.Visible = true;
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
             userentity.ID = int.Parse(strSelectedId);
            if (userobj.EditUsers(userentity) == 1)
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
        txt_UserName.Text = "";
        Query();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        panel_role.Visible = false;
        lbl_Type.Text = "添加";
        txt_UserName.Text = "";
        BindDrop();
        Txt_pwd.Text = "";
        drop_Level.SelectedValue = drop_UserLevel.SelectedValue;
        MyStaVoid.BindList("DepartName", "DepartID", "select * from t_M_DepartInfo order by DepartID ", drop_ThrSca_Name);
        btn_OK.Text = "确定";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        Query();
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
     #region 化验员则显示化验员AB角色设定
    protected void drop_ThrSca_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drop_ThrSca_Name.SelectedValue.ToString() == "4")
        {
            panel_role.Visible = true;
        }
        else
            panel_role.Visible = false;
    }

    private void intial(DropDownList drop_type_temp)
    {
        DataTable dttype = itemObj.GetType("");
        drop_type_temp.DataSource = dttype;
        drop_type_temp.DataValueField = "TypeID";
        drop_type_temp.DataTextField = "样品类型";
        drop_type_temp.DataBind();
        if (drop_type_temp.ID == "drop_type")
            drop_type_SelectedIndexChanged(null, null);
        else
            drop_type_b_SelectedIndexChanged(null, null);

    }

         #region A角
         
        
       
         protected void grv_a_RowCreated(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.Header)
             {
                 e.Row.Cells[1].Text = "样品类型";
              
                 TableCell headerDetail = new TableCell();
                 headerDetail.Text = "监测项目";
                 headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                 headerDetail.Width = 400;
                 e.Row.Cells.Add(headerDetail);
                 TableCell headerEdit = new TableCell();
                 headerEdit.Text = "编辑";
                 headerEdit.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                 headerEdit.Width = 30;
                 e.Row.Cells.Add(headerEdit);

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
                 //int id = e.Row.RowIndex + 1;

                 //e.Row.Cells[0].Text = id.ToString();

                 //手动添加详细和删除按钮
                 TableCell tabcDetail = new TableCell();
                
                 tabcDetail.Style.Add("text-align", "center");
                 //CheckBoxList ibtnDetail = new CheckBoxList();
                 //ibtnDetail.ID = "cbl_Check";
                
                 //tabcDetail.Controls.Add(ibtnDetail);
                 e.Row.Cells.Add(tabcDetail);

                 //手动添加详细和删除按钮
                 TableCell tabcSel = new TableCell();
                 tabcSel.Width = 30;
                 tabcSel.Style.Add("text-align", "center");
                 ImageButton ibtnsel = new ImageButton();
                 ibtnsel.ImageUrl = "~/Images/Detail.gif";
                 ibtnsel.CommandName = "Edit";
                 tabcSel.Controls.Add(ibtnsel);
                 e.Row.Cells.Add(tabcSel);

                 TableCell tabcDel = new TableCell();
                 tabcDel.Width = 30;
                 tabcDel.Style.Add("text-align", "center");
                 ImageButton ibtnDel = new ImageButton();
                 ibtnDel.ImageUrl = "~/Images/Delete.gif";
                 ibtnDel.CommandName = "Delete";
                 ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
                 tabcDel.Controls.Add(ibtnDel);
                 e.Row.Cells.Add(tabcDel);

             }
             if (e.Row.RowType != DataControlRowType.Pager)
             {
                 ////绑定数据后，隐藏4,5,6,7列 
                 e.Row.Cells[0].Visible = false;
              
             }
         }
         protected void grv_a_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;")
                {
                    DataTable Method = itemObj.GetAB(strUserId, e.Row.Cells[0].Text.Trim(), "A");//dtItem.Select("ClassID='" + e.Row.Cells[1].Text.Trim() + "'");
                   
                    foreach (DataRow dr in Method.Rows)
                    {
                        e.Row.Cells[2].Text += dr["AIName"].ToString() +",";
                    }
                }
            } 
        }


        

    protected void drop_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        //根据人员所属水气组邦定对应的指标
        inGrv(cbl_ItemlistA,drop_type.SelectedValue.ToString());
      
    }
   
    protected void btn_save_a_OnClick(object sender, EventArgs e)
    {
        DAl.User.Users userobj = new DAl.User.Users();
        Entity.User.Users userentity = new Entity.User.Users();

        userentity.UserID = txt_UserName.Text;
            //TBD AB角信息初始化
            for (int i = 0; i < cbl_ItemlistA.Items.Count; i++)
            {

                if (cbl_ItemlistA.Items[i].Selected)
                {
                    Entity.SampleItem item = new Entity.SampleItem();
                    item.TypeID = int.Parse(drop_type.SelectedValue.ToString().Trim());
                    item.MonitorID = int.Parse(cbl_ItemlistA.Items[i].Value);
                    item.MonitorItem = cbl_ItemlistA.Items[i].Text.Trim();
                    userentity.AitemList.Add(item);
                }

            }
       
        //保存用户AB角
            if (userobj.SaveAB(userentity, "A", drop_type.SelectedValue.ToString().Trim()) == 1)
        {
           
          
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('A角设定成功！');", true);
            panel_a.Visible = false;
            ABRoleGroup(userentity.UserID);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('A角设定失败！');", true);
        }
 
    }
    protected void ABRoleGroup(string userid)
    {
        //用户A角绑定
        dtItemA = itemObj.GetABRole(userid, "A");
        grv_a.DataSource = dtItemA;
        grv_a.DataBind();

        //用户A角绑定
        dtItemB = itemObj.GetABRole(userid, "B");
        grv_b.DataSource = dtItemB;
        grv_b.DataBind();
       
    }
    protected void grv_a_RowEditing(object sender, GridViewEditEventArgs e)
    {
        panel_a.Visible = true;
        intial(drop_type);
        drop_type.SelectedValue = grv_a.Rows[e.NewEditIndex].Cells[0].Text.Trim();
        txt_a_Item.Text = grv_a.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        txt_a_Item_TextChanged(null, null);

    }
    protected void grv_a_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DAl.User.Users userobj = new DAl.User.Users();
        Entity.User.Users userentity = new Entity.User.Users();

        userentity.UserID = txt_UserName.Text;
        string type = grv_a.Rows[e.RowIndex].Cells[0].Text.Trim();
        if (userobj.DeleteAB(userentity, "A", type) == 1)

        { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除成功！');", true); }
        else
        { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除失败！');", true); }
        ABRoleGroup(userentity.UserID);
    }
    protected void btn_a_Add_Click(object sender, EventArgs e)
    {
        intial(drop_type);
        txt_a_Item.Text = "";
        panel_a.Visible = true;
    }

    protected void cbl_ItemlistA_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_a_Item.Text = getItem(cbl_ItemlistA);
    }
    protected void txt_a_Item_TextChanged(object sender, EventArgs e)
    {
        string stritem = txt_a_Item.Text.Trim();
        txt_a_Item.Text = DataBindAll(cbl_ItemlistA, stritem,drop_type.SelectedValue.ToString().Trim());
    }
         #endregion
    #region B角



    protected void grv_b_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "样品类型";

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "监测项目";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 400;
            e.Row.Cells.Add(headerDetail);
            TableCell headerEdit = new TableCell();
            headerEdit.Text = "编辑";
            headerEdit.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerEdit.Width = 30;
            e.Row.Cells.Add(headerEdit);

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
            //int id = e.Row.RowIndex + 1;

            //e.Row.Cells[0].Text = id.ToString();

            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();

            tabcDetail.Style.Add("text-align", "center");
            //CheckBoxList ibtnDetail = new CheckBoxList();
            //ibtnDetail.ID = "cbl_Check";

            //tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            //手动添加详细和删除按钮
            TableCell tabcSel = new TableCell();
            tabcSel.Width = 30;
            tabcSel.Style.Add("text-align", "center");
            ImageButton ibtnsel = new ImageButton();
            ibtnsel.ImageUrl = "~/Images/Detail.gif";
            ibtnsel.CommandName = "Edit";
            tabcSel.Controls.Add(ibtnsel);
            e.Row.Cells.Add(tabcSel);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/Images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
           e.Row.Cells[0].Visible = false;
            


        }
    }
    protected void grv_b_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Trim() != "&nbsp;")
            {
                DataTable Method = itemObj.GetAB(strUserId, e.Row.Cells[0].Text.Trim(), "B");//dtItem.Select("ClassID='" + e.Row.Cells[1].Text.Trim() + "'");

                foreach (DataRow dr in Method.Rows)
                {
                    e.Row.Cells[2].Text += dr["AIName"].ToString() + ",";
                }
            }
        }
    }




    protected void drop_type_b_SelectedIndexChanged(object sender, EventArgs e)
    {
        //根据人员所属水气组邦定对应的指标
       
        inGrv(cbl_ItemlistB,drop_type_b.SelectedValue.ToString());

    }

    protected void btn_save_b_OnClick(object sender, EventArgs e)
    {
        DAl.User.Users userobj = new DAl.User.Users();
        Entity.User.Users userentity = new Entity.User.Users();

        userentity.UserID = txt_UserName.Text;
        //TBD AB角信息初始化
        for (int i = 0; i < cbl_ItemlistB.Items.Count; i++)
        {

            if (cbl_ItemlistB.Items[i].Selected)
            {
                Entity.SampleItem item = new Entity.SampleItem();
                item.TypeID = int.Parse(drop_type_b.SelectedValue.ToString().Trim());
                item.MonitorID = int.Parse(cbl_ItemlistB.Items[i].Value);
                item.MonitorItem = cbl_ItemlistB.Items[i].Text.Trim();
                userentity.AitemList.Add(item);
            }

        }

        //保存用户AB角
        if (userobj.SaveAB(userentity, "B", drop_type_b.SelectedValue.ToString().Trim()) == 1)
        {


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('B角设定成功！');", true);
            panel_b.Visible = false;
            ABRoleGroup(userentity.UserID);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('B角设定失败！');", true);
        }

    }
   
    protected void grv_b_RowEditing(object sender, GridViewEditEventArgs e)
    {
        panel_b.Visible = true;
        intial(drop_type_b);
        drop_type_b.SelectedValue = grv_b.Rows[e.NewEditIndex].Cells[0].Text.Trim();
        txt_b_Item.Text = grv_b.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        txt_b_Item_TextChanged(null, null);

    }
    protected void grv_b_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DAl.User.Users userobj = new DAl.User.Users();
        Entity.User.Users userentity = new Entity.User.Users();

         userentity.UserID = txt_UserName.Text;
        string type=grv_b.Rows[e.RowIndex].Cells[0].Text.Trim();
        if (userobj.DeleteAB(userentity, "B", type) == 1)

        { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除成功！');", true); }
        else
        { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除失败！');", true); }
        ABRoleGroup(userentity.UserID);

    }
    protected void btn_b_Add_Click(object sender, EventArgs e)
    {
        intial(drop_type_b);
        txt_b_Item.Text = "";
        panel_b.Visible = true;
    }

    protected void cbl_ItemlistB_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_b_Item.Text = getItem(cbl_ItemlistB);
    }
    protected void txt_b_Item_TextChanged(object sender, EventArgs e)
    {
        string stritem = txt_b_Item.Text.Trim();
        txt_b_Item.Text = DataBindAll(cbl_ItemlistB, stritem,drop_type_b.SelectedValue.ToString().Trim());
    }

    #endregion
     #endregion


 
     #region Common
     
   
     private string getItem(CheckBoxList cb_analysisList)
     {
         
         string stritem = "";
         foreach (ListItem LI in cb_analysisList.Items)
         {
             if (LI.Selected)
             {
                 stritem += LI.Text.Trim() + ",";
             }
         }
         return stritem;
     }
     protected void inGrv(CheckBoxList cbl_Itemlist,string type)
     {

         DataTable dt = itemObj.GetItemList(type);
         cbl_Itemlist.DataSource = dt;
         cbl_Itemlist.DataTextField = "AIName";
         cbl_Itemlist.DataValueField = "id";
         cbl_Itemlist.DataBind();


     }
     protected string DataBindAll(CheckBoxList cb, string itemvaluelsit,string type)
     {
         string retstr = "";
         if (type != "")
         {
             string str = "select  t_M_AnalysisItemEx.AIID id,t_M_ANItemInf.AIName,lower(t_M_ANItemInf.AICode) AICode from t_M_AnalysisItemEx,t_M_AnalysisMainClassEx,t_M_ANItemInf where t_M_AnalysisItemEx.ClassID=t_M_AnalysisMainClassEx.ClassID and t_M_ANItemInf.ID=t_M_AnalysisItemEx.AIID  and t_M_AnalysisMainClassEx.ClassID='" + type + "' order by t_M_ANItemInf.orderid,t_M_ANItemInf.AIName ";
             //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
             DataSet ds = new MyDataOp(str).CreateDataSet();
             cb.DataSource = ds;
             cb.DataValueField = "id";
             cb.DataTextField = "AIName";
             cb.DataBind();
             if (ds.Tables[0].Rows.Count < 0)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('没有该样品类型的分析项目，请先在分析项目管理中添加');", true);

                 return itemvaluelsit;
             }
             else
             {
                 string[] list = itemvaluelsit.Split(',');
                 for (int i = 0; i < cb.Items.Count; i++)
                 {
                     foreach (string item in list)
                     {
                         if (item.Trim() == cb.Items[i].Text.ToString().Trim())
                         {
                             cb.Items[i].Selected = true;
                         }
                         else
                         {
                             DataRow[] drsell = ds.Tables[0].Select("AICode='" + item.Trim().ToLower() + "'");
                             if (drsell.Length > 0)
                             {
                                 for (int j = 0; j < drsell.Length; j++)
                                 {
                                     if (drsell[j]["AIName"].ToString().Trim() == cb.Items[i].Text.ToString().Trim())
                                     {
                                         cb.Items[i].Selected = true;
                                     }
                                 }
                             }
                         }
                     }
                     if (cb.Items[i].Selected == true)
                     { retstr += cb.Items[i].Text.Trim()+","; }


                 }
                
                return     retstr;
             }
         }
         else
         {
             ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请先选择样品类型');", true);

             return itemvaluelsit;
         }

     }
     #endregion
}