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

public partial class BaseData_PasswordMgmtOld: System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
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

    private void Query()
    {
        string strSql = "select t_R_UserInfo.id,t_R_UserInfo.UserID,Name,t_R_Role.LevelID,t_R_UserLevel.LevelName,t_R_Role.RoleID,t_R_Role.RoleName,t_R_UserInfo.DepartID,DepartName" +
            " from t_R_UserInfo ,t_R_Role,t_M_DepartInfo,t_R_UserLevel " +
            "where t_R_UserInfo.RoleID=t_R_Role.RoleID and t_R_UserInfo.DepartID=t_M_DepartInfo.DepartID and t_R_UserLevel.LevelID=t_R_Role.LevelID";

        strSql += " and t_R_Role.LevelID='" + drop_UserLevel.SelectedValue + "' order by t_R_UserInfo.DepartID ";
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
        if (drop_ThrSca_Name.SelectedValue.ToString().Trim() == "4")
        {
            panel_role.Visible = true;
            panel_a.Visible = false;
            lbtn_xk.Text = "+分析员角色设定";
            btn_a_add.Text = "+分析员A角设定";
            btn_b_add.Text = "+分析员B角设定";
            panel_b.Visible = false;
            drop_type.SelectedValue = ds.Tables[0].Rows[0]["grouptype"].ToString();
            ABRoleGroup(ds.Tables[0].Rows[0]["UserID"].ToString());
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
        strSql = "DELETE FROM t_R_UserInfo WHERE id = '" + strSelectedId + "'";
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
        MyStaVoid.BindList("RoleName", "RoleID", "select * from t_R_Role where LevelID='" + drop_UserLevel.SelectedValue+ "' order by RoleID ", drop_Role);
        DataTable dt = itemObj.GetType(drop_type.SelectedValue.ToString());
        Repeater_A.DataSource = dt;
        Repeater_A.DataBind();
      
        Repeater_B.DataSource = dt;
       
        Repeater_B.DataBind();
        inGrv(dt);
        //grv_a.DataSource = dt;
        //grv_a.DataBind();
        //dt_analysisA = null;
        //dt_analysisA = new DataTable();
        //DataColumn dc1 = new DataColumn("id");
        //DataColumn dc2 = new DataColumn("Standard");
        //DataColumn dc3 = new DataColumn("Method");
        //dt_analysisA.Columns.Add(dc1);
        //dt_analysisA.Columns.Add(dc2);
        //dt_analysisA.Columns.Add(dc3);
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
    
		 protected void lbtn_xk_Click(object sender, EventArgs e)
         {
             if (panel_a.Visible == true)
             {
                 lbtn_xk.Text = "+分析员角色设定";
                 panel_a.Visible = false;
                 Repeater_A.Visible = false;
                 btn_a_add.Text = "+分析员A角设定";
                 panel_b.Visible = false;
                 Repeater_B.Visible = false;
                 btn_b_add.Text = "+分析员B角设定";
             }
             else
             {
                 panel_a.Visible = true;
                 Repeater_A.Visible = false;

                 panel_b.Visible = true;
                 Repeater_B.Visible = false;
                 btn_a_add.Text = "+分析员B角设定";
                 btn_a_add.Text = "+分析员A角设定";
                 lbtn_xk.Text = "-分析员角色设定";
             }

         }

         #region A角
         
        
         protected void btn_a_add_OnClick(object sender, EventArgs e)
         {
             if (Repeater_A.Visible == false)
             {
                 Repeater_A.Visible = true;
                 btn_a_add.Text = "-分析员A角设定";
                
                 //根据人员所属水气组邦定对应的指标
                 for(int i=0;i<Repeater_A.Items.Count;i++)
                 {
                      Label lbl_sampletypeID = (Label)Repeater_A.Items[i].FindControl("sampletypeID");
                      GridView grv_a = Repeater_A.Items[i].FindControl("grv_a") as GridView;
                      for (int j = 0; j < grv_a.Rows.Count; j++)
                      {
                          CheckBox autoid = (CheckBox)grv_a.Rows[j].FindControl("autoid");

                          DataRow[] dra = dtItemA.Select("ClassID='" + lbl_sampletypeID.Text + "' and id='" + grv_a.Rows[j].Cells[1].Text.Trim()+ "'");
                          if (dra.Length > 0)
                          {
                              autoid.Checked = true;
                              RadioButtonList RBL = grv_a.Rows[j].FindControl("cbl") as RadioButtonList;
                              for (int p = 0; p < RBL.Items.Count; p++)
                              {
                                  if (RBL.Items[p].Value == dra[0]["method"].ToString().Trim())
                                      RBL.Items[p].Selected = true;
                                  else
                                      RBL.Items[p].Selected = false;
                              }
                          }
                          //else
                          //    autoid.Checked = false;
                      }
                 }  
             }
             else
             {
                 btn_a_add.Text = "+分析员A角设定";
                 Repeater_A.Visible = false;
             }
         }
         protected void grv_a_RowCreated(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.Header)
             {
                 e.Row.Cells[3].Text = "监测项目";
              
                 TableCell headerDetail = new TableCell();
                 headerDetail.Text = "分析方法选择";
                 headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                 headerDetail.Width = 600;
                 e.Row.Cells.Add(headerDetail);
                
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
                 RadioButtonList ibtnDetail = new RadioButtonList();
                 ibtnDetail.ID = "cbl";
                 ibtnDetail.SelectedIndex = 0;
                 tabcDetail.Controls.Add(ibtnDetail);
                 e.Row.Cells.Add(tabcDetail);
             }
             if (e.Row.RowType != DataControlRowType.Pager)
             {
                 ////绑定数据后，隐藏4,5,6,7列 
               e.Row.Cells[1].Visible = false;
               e.Row.Cells[2].Visible = false;
                 

             }
         }
         protected void grv_a_RowDataBound(object sender, GridViewRowEventArgs e)
        { 
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (e.Row.Cells[1].Text.Trim() != "&nbsp;")
            //    {
            //        DataTable Method = itemObj.GetMethod(e.Row.Cells[1].Text.Trim());//dtItem.Select("ClassID='" + e.Row.Cells[1].Text.Trim() + "'");
            //        RadioButtonList cbl_temp = e.Row.Cells[4].FindControl("cbl") as RadioButtonList;
            //        cbl_temp.RepeatDirection = RepeatDirection.Vertical;
            //        cbl_temp.DataSource = Method;
            //        cbl_temp.DataValueField = "id";
            //        cbl_temp.DataTextField = "Standard";
            //        cbl_temp.DataBind(); 
            //    }
            //} 
        }

        
    
    protected void drop_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = itemObj.GetType(drop_type.SelectedValue.ToString());
       Repeater_A.DataSource = dt;
       Repeater_A.DataBind();
       inGrv(dt);
    }
    protected void inGrv(DataTable dt)
    {
        for (int j = 0; j < dt.Rows.Count; j++)
        {
          
            //选中框
            Label lbl_sampletypeID = (Label)Repeater_A.Items[j].FindControl("sampletypeID");

            //污染物
            Label lbl_sampletype = (Label)Repeater_A.Items[j].FindControl("sampletype");

            lbl_sampletypeID.Text = dt.Rows[j]["TypeID"].ToString().Trim();
            lbl_sampletype.Text = dt.Rows[j]["样品类型"].ToString().Trim();

            GridView grv_a = Repeater_A.Items[j].FindControl("grv_a") as GridView;
            grv_a.DataSource = itemObj.GetItem(dt.Rows[j]["TypeID"].ToString().Trim());
            grv_a.DataBind();
            for (int i = 0; i < grv_a.Rows.Count; i++)
            {
                CheckBox autoid = grv_a.Rows[i].FindControl("autoid") as CheckBox;
                RadioButtonList cbl = grv_a.Rows[i].FindControl("cbl") as RadioButtonList;
                autoid.Checked = false;
                cbl.SelectedIndex = 0;
            }

            //选中框
            Label lbl_sampletypeIDB = (Label)Repeater_B.Items[j].FindControl("sampletypeID");

            //污染物
            Label lbl_sampletypeB = (Label)Repeater_B.Items[j].FindControl("sampletype");

            lbl_sampletypeIDB.Text = dt.Rows[j]["TypeID"].ToString().Trim();
            lbl_sampletypeB.Text = dt.Rows[j]["样品类型"].ToString().Trim();

            GridView grv_b = Repeater_B.Items[j].FindControl("grv_b") as GridView;
            grv_b.DataSource = itemObj.GetItem(dt.Rows[j]["TypeID"].ToString().Trim());
            grv_b.DataBind();
            for (int i = 0; i < grv_b.Rows.Count; i++)
            {
                CheckBox autoid = grv_b.Rows[i].FindControl("autoid") as CheckBox;
                RadioButtonList cbl = grv_b.Rows[i].FindControl("cbl") as RadioButtonList;
                autoid.Checked = false;
                cbl.SelectedIndex = 0;
            }

        }

    }
    protected void btn_save_a_OnClick(object sender, EventArgs e)
    {
        DAl.User.Users userobj = new DAl.User.Users();
        Entity.User.Users userentity = new Entity.User.Users();

        userentity.UserID = txt_UserName.Text;
        for (int p = 0; p < Repeater_A.Items.Count; p++)
        {
            GridView grv_a = Repeater_A.Items[p].FindControl("grv_a") as GridView;
            //TBD AB角信息初始化
            for (int i = 0; i < grv_a.Rows.Count; i++)
            {
                CheckBox cb = grv_a.Rows[i].Cells[0].FindControl("autoid") as CheckBox;
                if (cb.Checked)
                {
                    Entity.SampleItem item = new Entity.SampleItem();
                    item.MonitorID = int.Parse(grv_a.Rows[i].Cells[1].Text.Trim());
                    item.MonitorItem = grv_a.Rows[i].Cells[3].Text.Trim();
                    RadioButtonList cbl = grv_a.Rows[i].Cells[4].FindControl("cbl") as RadioButtonList;
                    for (int j = 0; j < cbl.Items.Count; j++)
                    {
                        if (cbl.Items[j].Selected)
                        {
                            item.Method = cbl.Items[j].Value.Trim();

                        }
                    }
                    userentity.AitemList.Add(item);
                }

            }
        }
        //保存用户AB角
        if (userobj.SaveAB(userentity,"A") == 1)
        {
            Repeater_A.Visible = false;
            btn_a_add.Text = "+分析员A角设定";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('A角设定成功！');", true);
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
             dtItemA = itemObj.GetAB(userid,"A");
            
                 cbl_a.DataSource = dtItemA;
                 cbl_a.DataValueField = "id";
                 cbl_a.DataTextField = "AIName";
                 cbl_a.DataBind();
            
            for (int i = 0; i < cbl_a.Items.Count; i++)
            { cbl_a.Items[i].Selected=true; }

            dtItemB = itemObj.GetAB(userid, "B");

            cbl_b.DataSource = dtItemB;
            cbl_b.DataValueField = "id";
            cbl_b.DataTextField = "AIName";
            cbl_b.DataBind();
            for (int i = 0; i < cbl_b.Items.Count; i++)
            { cbl_b.Items[i].Selected = true; }
    }

         #endregion
    #region B角
    protected void btn_b_add_OnClick(object sender, EventArgs e)
    {
        if (Repeater_B.Visible == false)
        {
            Repeater_B.Visible = true;
            btn_b_add.Text = "-分析员B角设定";

            //根据人员所属水气组邦定对应的指标
            for (int i = 0; i < Repeater_B.Items.Count; i++)
            {
                Label lbl_sampletypeID = (Label)Repeater_B.Items[i].FindControl("sampletypeID");
                GridView grv_b = Repeater_B.Items[i].FindControl("grv_b") as GridView;
                for (int j = 0; j < grv_b.Rows.Count; j++)
                {
                    CheckBox autoid = (CheckBox)grv_b.Rows[j].FindControl("autoid");

                    DataRow[] dra = dtItemB.Select("ClassID='" + lbl_sampletypeID.Text + "' and id='" + grv_b.Rows[j].Cells[1].Text.Trim() + "'");
                    if (dra.Length > 0)
                    {
                        autoid.Checked = true;
                        RadioButtonList RBL = grv_b.Rows[j].FindControl("cbl") as RadioButtonList;
                        for (int p = 0; p < RBL.Items.Count; p++)
                        {
                            if (RBL.Items[p].Value == dra[0]["method"].ToString().Trim())
                                RBL.Items[p].Selected = true;
                            else
                                RBL.Items[p].Selected = false;
                        }
                    }
                    //else
                    //    autoid.Checked = false;
                }
            }
        }
        else
        {
            btn_b_add.Text = "+分析员B角设定";
            Repeater_B.Visible = false;
        }
    }
    protected void grv_b_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = "监测项目";

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "分析方法选择";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 600;
            e.Row.Cells.Add(headerDetail);

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
            RadioButtonList ibtnDetail = new RadioButtonList();
            ibtnDetail.ID = "cbl";
            ibtnDetail.SelectedIndex = 0;
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //////绑定数据后，隐藏4,5,6,7列 
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;


        }
    }
    protected void grv_b_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text.Trim() != "&nbsp;")
            {
                DataTable Method = itemObj.GetMethod(e.Row.Cells[1].Text.Trim(), e.Row.Cells[13].Text.Trim());//dtItem.Select("ClassID='" + e.Row.Cells[1].Text.Trim() + "'");
                RadioButtonList cbl_temp = e.Row.Cells[4].FindControl("cbl") as RadioButtonList;
                cbl_temp.RepeatDirection = RepeatDirection.Vertical;
                cbl_temp.DataSource = Method;
                cbl_temp.DataValueField = "id";
                cbl_temp.DataTextField = "Standard";
                cbl_temp.DataBind();
            }
        }
    }


     protected void btn_save_b_OnClick(object sender, EventArgs e)
    {
        DAl.User.Users userobj = new DAl.User.Users();
        Entity.User.Users userentity = new Entity.User.Users();

        userentity.UserID = txt_UserName.Text;
        for (int p = 0; p < Repeater_B.Items.Count; p++)
        {
            GridView grv_b = Repeater_B.Items[p].FindControl("grv_b") as GridView;
            //TBD AB角信息初始化
            for (int i = 0; i < grv_b.Rows.Count; i++)
            {
                CheckBox cb = grv_b.Rows[i].Cells[0].FindControl("autoid") as CheckBox;
                if (cb.Checked)
                {
                    Entity.SampleItem item = new Entity.SampleItem();
                    item.MonitorID = int.Parse(grv_b.Rows[i].Cells[1].Text.Trim());
                    item.MonitorItem = grv_b.Rows[i].Cells[3].Text.Trim();
                    RadioButtonList cbl = grv_b.Rows[i].Cells[4].FindControl("cbl") as RadioButtonList;
                    for (int j = 0; j < cbl.Items.Count; j++)
                    {
                        if (cbl.Items[j].Selected)
                        {
                            item.Method = cbl.Items[j].Value.Trim();

                        }
                    }
                    userentity.AitemList.Add(item);
                }

            }
        }
        //保存用户AB角
        if (userobj.SaveAB(userentity,"B") == 1)
        {
            Repeater_B.Visible = false;
            btn_b_add.Text = "+分析员B角设定";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('B角设定成功！');", true);
            ABRoleGroup(userentity.UserID);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('B角设定失败！');", true);
        }
 
    }

    #endregion
     #endregion


}