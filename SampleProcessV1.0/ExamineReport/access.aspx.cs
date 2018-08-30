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

public partial class ExamineReport_access : System.Web.UI.Page
{
   //项目受理状态为：1
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string SelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["SelectedId"]; }
        set { ViewState["SelectedId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            Query();
            SetButton();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>综合室项目受理</b></font>";
           // GridView1.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>备注记录</b></font>"; 
        }
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
    private void Query()
    {
        string strSql = "select  t_Y_FlowInfo.id 编号,wtdw 委托单位,ItemName 项目类型,lxman 联系人,lxtel 联系电话,accessman 受理人员,accessdate 受理日期,accessremark 备注,varremark1,ckflag from t_Y_FlowInfo where StatusID='1'";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["受理人员"].ToString() == drr["UserID"].ToString())
                    dr["受理人员"] = drr["Name"].ToString();
                
            }
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
        initial();
        //cb_if.Checked = false;
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
        drop_ItemName.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;

        txt_wtdw.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text;
        txt_man.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
        txt_tel.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text;
        //if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim() != "&nbsp;")
        //    if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim() == "1")
        //        cb_if.Checked = true;
        //    else
        //        cb_if.Checked = false;
        //else
        //    cb_if.Checked = false;
        //备注绑定ItemName 项目类型, name 阶段,
        string remarkstr = "select wtdw 委托单位,ItemName 项目类型, name 阶段, CreateDate 备注时间,bz 备注及意见,userid 用户名,flag,t_Y_Detail.id from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='1' order by t_Y_Detail.id ";
        DataSet ds_Remark1 = new MyDataOp(remarkstr).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds_Remark1.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();
               
            }
        }
        GridView1.DataSource = ds_Remark1;
        GridView1.DataBind();
       
        DataRow[] dr_remark = ds_Remark1.Tables[0].Select("flag=0");
        if (dr_remark.Length > 0)
        {
            txt_Remark.Text = dr_remark[0][4].ToString();
            SelectedId = dr_remark[0][7].ToString();
        }
        ds_Remark1.Dispose();
        //有回退的，则显示回退备注，非回退，不显示
        // string backremarkstr = "select * from t_Y_BackInfo where itemid='" + strSelectedId + "' and functionid='1'";
        string backremarkstr = "select ItemName 项目类型, name 阶段,createdate 备注时间,t_Y_BackInfo.remark 备注及意见,userid 用户名 from t_Y_BackInfo inner join t_Y_FlowInfo on t_Y_BackInfo.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_BackInfo.functionid where t_Y_BackInfo.itemid='" + strSelectedId + "' and t_Y_BackInfo.functionid='2' order by t_Y_BackInfo.id";
 
        DataSet ds_Remark_back = new MyDataOp(backremarkstr).CreateDataSet();

        foreach (DataRow dr in ds_Remark_back.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();
                //if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportProofUserID"] = drr["Name"].ToString();
                //if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportSignUserID"] = drr["Name"].ToString();
            }
        }
        GridView_back.DataSource = ds_Remark_back;
        GridView_back.DataBind();
      
         //回退的数据编辑，显示前面的备注信息，只读
        if (ds_Remark_back.Tables[0].Rows.Count > 0)
         {
            
             Panel_back.Visible = true;
             

         }
         else
         {
            Panel_back.Visible = false; }

        ds_Remark_back.Dispose();


        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {



            TableCell headerset = new TableCell();
            headerset.Text = "详细/编辑";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

            TableCell headerUp = new TableCell();
            headerUp.Text = "上传文件";
            headerUp.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerUp.Width = 60;
            e.Row.Cells.Add(headerUp);

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



            TableCell MenuSet = new TableCell();
            MenuSet.Width = 60;
            MenuSet.Style.Add("text-align", "center");
            ImageButton btMenuSet = new ImageButton();
            btMenuSet.ImageUrl = "~/images/Detail.gif";
            btMenuSet.CommandName = "Edit";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuSet.Controls.Add(btMenuSet);
            e.Row.Cells.Add(MenuSet);
            //上传文件
            TableCell MenuUp = new TableCell();
            MenuUp.Width = 60;
            MenuUp.Style.Add("text-align", "center");
            ImageButton btMenuUp = new ImageButton();
            btMenuUp.ImageUrl = "~/images/Detail.gif";
            btMenuUp.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuUp.Controls.Add(btMenuUp);
            e.Row.Cells.Add(MenuUp);

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
           // e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;

        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

           
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            //e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[6].Visible = false;
           

       }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string[] deletelist = new string[4];
        strSql = "DELETE FROM t_Y_FlowInfo WHERE id = '" + strSelectedId + "'";
        

        deletelist.SetValue(strSql, 0);
        strSql = "DELETE FROM t_Y_Detail WHERE itemid = '" + strSelectedId + "'";
         deletelist.SetValue(strSql, 1);
        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.DoTran(2,deletelist))
        {
            WebApp.Components.Log.SaveLogY("项目受理中删除项目信息（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 0);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLogY("项目受理中删除项目信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 0);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        Query();
    }
    #endregion

    #region 其它函数


    private string Verify()
    {
        string strErrorInfo = "";
        if (drop_ItemName.SelectedIndex == 0)
        {
            strErrorInfo += "项目类型不能为空！\\n";
        }
        else if (txt_Remark.Text.Trim() == "")
        {
            strErrorInfo += "请在备注中说明项目审批部门！\\n";
        }
        //string sqlstr = "select * from ";
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
            int flag=0;
            //if (cb_if.Checked)
            //    flag = 1;
            string strSql = "";
            #region 添加新纪录
            if (lbl_Type.Text == "添加")
            {
             
                strSql = @"insert into t_Y_FlowInfo
                                    (itemname,accessman,accessdate,accessremark,StatusID,wtdw,lxman,lxtel,ckflag)  
                                    values('" + drop_ItemName.SelectedValue.ToString().Trim() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark.Text + "','1','" + txt_wtdw.Text + "','" + txt_man.Text.Trim() + "','" + txt_tel.Text.Trim() + "'," + flag + ")";
              
                MyDataOp mdo = new MyDataOp(strSql);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    string selectidstr = "select max(id) from t_Y_FlowInfo where itemname='" + drop_ItemName.SelectedValue.ToString().Trim() + "' and accessman='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'";
                    DataSet dsid = new MyDataOp(selectidstr).CreateDataSet();
                    if (dsid.Tables[0].Rows.Count > 0)
                    {
                        strSelectedId = dsid.Tables[0].Rows[0][0].ToString();
                        strSql = @"insert into t_Y_Detail
                                    (itemid,statusid,userid,CreateDate,bz)  
                                    values('" + strSelectedId + "','1','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark.Text + "')";
                        MyDataOp mdo1 = new MyDataOp(strSql);
                        if (mdo1.ExecuteCommand())
                        {
                            WebApp.Components.Log.SaveLogY("项目受理中添加项目信息" + drop_ItemName.SelectedItem.Text+ "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加成功！');", true);
                        }
                        else
                        {
                            WebApp.Components.Log.SaveLogY("项目受理中添加项目信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                        }
                    }
                    else
                    {
                        WebApp.Components.Log.SaveLogY("项目受理中添加项目信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                    }
                }
            }
            #endregion

                #region 编辑记录
                if (lbl_Type.Text == "编辑")
                {
                    string[] updatestr = new string[4];
                    int i = 0;
                    //判断是否为第一次保存
                    string selectidstr = "select * from t_Y_Detail where itemid='" + strSelectedId + "' and flag='0' and statusid='1'";
                    DataSet dsid = new MyDataOp(selectidstr).CreateDataSet();
                    //编辑
                    if (dsid.Tables[0].Rows.Count > 0)
                    {
                        strSql = @"update t_Y_FlowInfo 
                        set itemname='" + drop_ItemName.SelectedValue + "',accessremark='" + txt_Remark.Text + "',accessdate=getdate(),wtdw='" + txt_wtdw.Text.Trim() + "',lxman='" + txt_man.Text.Trim() + "',lxtel='" + txt_tel.Text.Trim() + "',ckflag="+flag+" where id='" + strSelectedId + "'";
                        updatestr.SetValue(strSql, i++);
                        strSql = @"update t_Y_Detail 
                        set bz='" + txt_Remark.Text + "',CreateDate=getdate() where itemid='" + strSelectedId + "' and statusid='1' and id='" + SelectedId + "'";
                        updatestr.SetValue(strSql, i++);
                    }
                    //新备注
                    else
                    {
                        strSql = @"update t_Y_FlowInfo 
                        set itemname='" + drop_ItemName.SelectedValue + "',accessremark='" + txt_Remark.Text + "' ,wtdw='" + txt_wtdw.Text.Trim() + "',lxman='" + txt_man.Text.Trim() + "',lxtel='" + txt_tel.Text.Trim() + "',ckflag=" + flag + " where id='" + strSelectedId + "'";
                        updatestr.SetValue(strSql, i++);
                        strSql = @"insert into t_Y_Detail
                                    (itemid,statusid,userid,CreateDate,bz)  
                                    values('" + strSelectedId + "','1','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark.Text + "')";
                        updatestr.SetValue(strSql, i++);

                    }
                    MyDataOp mdo2 = new MyDataOp(strSql);
                    bool blSuccess = mdo2.DoTran(i, updatestr);
                    if (blSuccess == true)
                    {
                        WebApp.Components.Log.SaveLogY("项目受理中编辑项目信息（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
                    }
                    else
                    {
                        WebApp.Components.Log.SaveLogY("项目受理中编辑项目信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
                    }
                }
            }

            #endregion
            Query();
       
    }
    /// <summary>
    /// 提交于编辑确定的区别在于项目的状态是否改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

        }
        else
        {
            int flag = 0;
            //if(cb_if.Checked)
            //    flag = 1;
            string strSql = "";
            #region 添加新纪录
            if (lbl_Type.Text == "添加")
            {

               strSql = @"insert into t_Y_FlowInfo
                                    (itemname,accessman,accessdate,accessremark,StatusID,wtdw,lxman,lxtel,ckflag)  
                                    values('" + drop_ItemName.SelectedValue + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark.Text + "','2','"+txt_wtdw.Text.Trim()+"','"+txt_man.Text.Trim()+"','"+txt_tel.Text.Trim()+"',"+flag+")";

                MyDataOp mdo = new MyDataOp(strSql);
                bool blSuccess = mdo.ExecuteCommand();
                if (blSuccess == true)
                {
                    string selectidstr = "select max(id) from t_Y_FlowInfo where itemname='" + drop_ItemName.SelectedValue + "' and accessman='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'";
                    DataSet dsid = new MyDataOp(selectidstr).CreateDataSet();
                    if (dsid.Tables[0].Rows.Count > 0)
                    {
                        strSelectedId = dsid.Tables[0].Rows[0][0].ToString();
                        strSql = @"insert into t_Y_Detail
                                    (itemid,statusid,userid,CreateDate,bz,flag)  
                                    values('" + strSelectedId + "','1','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark.Text + "','1')";
                        MyDataOp mdo3 = new MyDataOp(strSql);
                        if (mdo3.ExecuteCommand())
                        {
                            WebApp.Components.Log.SaveLogY("项目受理中添加项目信息" + drop_ItemName.SelectedItem.Text + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加成功！');", true);
                        }
                        else
                        {
                            WebApp.Components.Log.SaveLogY("项目受理中添加项目信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                        }
                    }
                    else
                    {
                        WebApp.Components.Log.SaveLogY("项目受理中添加项目信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                    }
                }
            }
            #endregion

                #region 编辑记录
                if (lbl_Type.Text == "编辑")
                {
                    string[] updatestr = new string[4];
                    int i = 0;
                    //判断是否为第一次保存
                    string selectidstr = "select * from t_Y_Detail where itemid='" + strSelectedId + "' and flag='0' and statusid='1'";
                    DataSet dsid = new MyDataOp(selectidstr).CreateDataSet();
                    //编辑
                    if (dsid.Tables[0].Rows.Count > 0)
                    {
                       strSql = @"update t_Y_FlowInfo 
                        set itemname='" + drop_ItemName.SelectedValue + "',accessremark='" + txt_Remark.Text + "',StatusID='2' ,wtdw='" + txt_wtdw.Text.Trim() + "',lxman='" + txt_man.Text.Trim() + "',lxtel='" + txt_tel.Text.Trim() + "' ,ckflag=" + flag + " where id='" + strSelectedId + "'";
                        updatestr.SetValue(strSql, i++);
                        strSql = @"update t_Y_Detail 
                        set bz='" + txt_Remark.Text + "',CreateDate=getdate(),flag='1' where itemid='" + strSelectedId + "' and statusid='1' and id='" + SelectedId + "'";
                        updatestr.SetValue(strSql, i++);
                    }
                    //新备注
                    else
                    {
                       strSql = @"update t_Y_FlowInfo 
                        set itemname='" + drop_ItemName.SelectedValue + "',accessremark='" + txt_Remark.Text + "',StatusID='2' ,wtdw='" + txt_wtdw.Text.Trim() + "',lxman='" + txt_man.Text.Trim() + "',lxtel='" + txt_tel.Text.Trim() + "' ,ckflag=" + flag + "   where id='" + strSelectedId + "'";
                        updatestr.SetValue(strSql, i++);
                        strSql = @"insert into t_Y_Detail
                                    (itemid,statusid,userid,CreateDate,bz,flag)  
                                    values('" + strSelectedId + "','1','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark.Text + "','1')";
                        updatestr.SetValue(strSql, i++);

                    }
                    MyDataOp mdo4 = new MyDataOp(strSql);
                    bool  blSuccess = mdo4.DoTran(i, updatestr);
                    if (blSuccess == true)
                    {
                        WebApp.Components.Log.SaveLogY("项目受理中编辑项目信息（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
                    }
                    else
                    {
                        WebApp.Components.Log.SaveLogY("项目受理中编辑项目信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 1);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
                    }
                }
            }

            #endregion
            Query();
        

    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        drop_ItemName.SelectedIndex = 0;
        txt_Remark.Text = "";
        txt_wtdw.Text = "";
        txt_man.Text = "";
        txt_tel.Text = "";
        Query();
    }
    protected void initial()
    {
        DAl.Sample typeobj=new DAl.Sample();
        DataTable dt = typeobj.GetSampleType("");
        DataTable dtItem = typeobj.GetPurpose("",1);
        drop_ItemName.DataSource = dtItem;
        drop_ItemName.DataTextField = "ItemName";
        drop_ItemName.DataValueField = "ItemID";
        drop_ItemName.DataBind();
        cb_ItemList.DataSource = dt;
        cb_ItemList.DataTextField = "ClassName";
        cb_ItemList.DataValueField = "ClassID";
        cb_ItemList.DataBind();

    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        btn_OK.Text = "确定";
        initial();
        drop_ItemName.SelectedIndex = 0;
        txt_Remark.Text = "";
        txt_wtdw.Text = "";
        txt_man.Text = "";
        txt_tel.Text = "";
        //cb_if.Checked = false;
        Panel_back.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        //txt_AccessTime.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
        Query();
    }
   
    protected void grdvw_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('attachment.aspx?rid=" + strSelectedId + "&&fid=1&&sfile=" + HttpUtility.UrlEncode("access.aspx")+ "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);

    }
    //protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    //{

    //}
}