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

public partial class ExamineReport_access14 : System.Web.UI.Page
{
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
            //txt_date1.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            Query();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>办公室装订</b></font>";

        }
    }
    protected void grdvw_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('attachment.aspx?rid=" + strSelectedId + "&&fid=15&&sfile=" + HttpUtility.UrlEncode("access1.aspx") + "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);

    }

    private void Query()
    {
        string strSql = "select  t_Y_FlowInfo.id 编号,ItemName 项目类型,accessman 受理人员,accessdate 受理日期,accessremark 备注 from t_Y_FlowInfo where StatusID=15";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["受理人员"].ToString() == drr["UserID"].ToString())
                    dr["受理人员"] = drr["Name"].ToString();
                //if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportProofUserID"] = drr["Name"].ToString();
                //if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportSignUserID"] = drr["Name"].ToString();
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

        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
        txt_ItemName.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim() != "&nbsp;")
            txt_AccessTime.Text = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
        else
            txt_AccessTime.Text = "";
        txt_person0.Text = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;

        txt_ItemName.ReadOnly = true;
        txt_person0.ReadOnly = true;


        //备注绑定
        //项目接收备注显示
        string remarkstr = "select CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='1' order by t_Y_Detail.id";
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
        string remarkstr2 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='2' order by t_Y_Detail.id";
        DataSet ds_Remark2 = new MyDataOp(remarkstr2).CreateDataSet();
        foreach (DataRow dr in ds_Remark2.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView2.DataSource = ds_Remark2;
        GridView2.DataBind();
        string remarkstr3 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='3' order by t_Y_Detail.id";
        DataSet ds_Remark3 = new MyDataOp(remarkstr3).CreateDataSet();
        foreach (DataRow dr in ds_Remark3.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView3.DataSource = ds_Remark3;
        GridView3.DataBind();
        string remarkstr4 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='4' order by t_Y_Detail.id";
        DataSet ds_Remark4 = new MyDataOp(remarkstr4).CreateDataSet();
        foreach (DataRow dr in ds_Remark4.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView4.DataSource = ds_Remark4;
        GridView4.DataBind();
        string remarkstr5 = "select CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='5' order by t_Y_Detail.id";
        DataSet ds_Remark5 = new MyDataOp(remarkstr5).CreateDataSet();
        foreach (DataRow dr in ds_Remark5.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView5.DataSource = ds_Remark5;
        GridView5.DataBind();
        string remarkstr6 = "select CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='6' order by t_Y_Detail.id";
        DataSet ds_Remark6 = new MyDataOp(remarkstr6).CreateDataSet();
        foreach (DataRow dr in ds_Remark6.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView6.DataSource = ds_Remark6;
        GridView6.DataBind();
        string remarkstr7 = "select CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='7' order by t_Y_Detail.id";
        DataSet ds_Remark7 = new MyDataOp(remarkstr7).CreateDataSet();
        foreach (DataRow dr in ds_Remark7.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView7.DataSource = ds_Remark7;
        GridView7.DataBind();
        string remarkstr8 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='8' order by t_Y_Detail.id";
        DataSet ds_Remark8 = new MyDataOp(remarkstr8).CreateDataSet();
        foreach (DataRow dr in ds_Remark8.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView8.DataSource = ds_Remark8;
        GridView8.DataBind();
        string remarkstr9 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='9' order by t_Y_Detail.id";
        DataSet ds_Remark9 = new MyDataOp(remarkstr9).CreateDataSet();
        foreach (DataRow dr in ds_Remark9.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView9.DataSource = ds_Remark9;
        GridView9.DataBind();
        string remarkstr10 = "select CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='10' order by t_Y_Detail.id";
        DataSet ds_Remark10 = new MyDataOp(remarkstr10).CreateDataSet();
        foreach (DataRow dr in ds_Remark10.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView10.DataSource = ds_Remark10;
        GridView10.DataBind();
        string remarkstr11 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='11' order by t_Y_Detail.id";
        DataSet ds_Remark11 = new MyDataOp(remarkstr11).CreateDataSet();
        foreach (DataRow dr in ds_Remark11.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView11.DataSource = ds_Remark11;
        GridView11.DataBind();
        string remarkstr12 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='12' order by t_Y_Detail.id";
        DataSet ds_Remark12 = new MyDataOp(remarkstr12).CreateDataSet();
        foreach (DataRow dr in ds_Remark12.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView12.DataSource = ds_Remark12;
        GridView12.DataBind();
        string remarkstr13 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='13' order by t_Y_Detail.id";
        DataSet ds_Remark13 = new MyDataOp(remarkstr13).CreateDataSet();
        foreach (DataRow dr in ds_Remark13.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView13.DataSource = ds_Remark13;
        GridView13.DataBind();
        string remarkstr14 = "select  CreateDate 备注时间,bz 备注及意见,userid 用户名 from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='14' order by t_Y_Detail.id";
        DataSet ds_Remark14 = new MyDataOp(remarkstr14).CreateDataSet();
        foreach (DataRow dr in ds_Remark14.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView14.DataSource = ds_Remark14;
        GridView14.DataBind();
        //备注绑定
        //有回退的，则显示回退备注，非回退，不显示
        //string backremarkstr = "select  createdate 备注时间,t_Y_BackInfo.remark 备注及意见,userid 用户名 from t_Y_BackInfo inner join t_Y_FlowInfo on t_Y_BackInfo.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_BackInfo.functionid where t_Y_BackInfo.itemid='" + strSelectedId + "' and t_Y_BackInfo.functionid='15' order by t_Y_BackInfo.id";

        //// string backremarkstr = "select * from t_Y_BackInfo where itemid='" + strSelectedId + "' and functionid='2'";
        //DataSet ds_Remark_back = new MyDataOp(backremarkstr).CreateDataSet();
        //foreach (DataRow dr in ds_Remark_back.Tables[0].Rows)
        //{
        //    foreach (DataRow drr in ds_User.Tables[0].Rows)
        //    {
        //        if (dr["用户名"].ToString() == drr["UserID"].ToString())
        //            dr["用户名"] = drr["Name"].ToString();

        //    }
        //}
        //GridView_back.DataSource = ds_Remark_back;
        //GridView_back.DataBind();
        //回退的数据编辑，显示前面的备注信息，只读
        string remarkstr_now = "select ItemName 项目类型, name 阶段,CreateDate 备注时间,bz 备注及意见,userid 用户名,flag,t_Y_Detail.id from t_Y_Detail inner join t_Y_FlowInfo on t_Y_Detail.itemid=t_Y_FlowInfo.id inner join t_Y_FlowDetail on t_Y_FlowDetail.id= t_Y_Detail.statusid where t_Y_Detail.itemid='" + strSelectedId + "' and t_Y_Detail.statusid='15' order by t_Y_Detail.id";
        DataSet ds_Remark_now = new MyDataOp(remarkstr_now).CreateDataSet();
        foreach (DataRow dr in ds_Remark_now.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["用户名"].ToString() == drr["UserID"].ToString())
                    dr["用户名"] = drr["Name"].ToString();

            }
        }
        GridView_now.DataSource = ds_Remark_now;
        GridView_now.DataBind();
        DataRow[] dr_remark = ds_Remark_now.Tables[0].Select("flag=0");
        if (dr_remark.Length > 0)
        {
            txt_Remark_now.Text = dr_remark[0][3].ToString();
            SelectedId = dr_remark[0][6].ToString();
        }
        //if (ds_Remark_back.Tables[0].Rows.Count > 0)
        //{


        //    Panel_back.Visible = true;


        //}
        //else
        //{

        //    Panel_back.Visible = false;
        //}
        ds_Remark1.Dispose();
        ds_Remark2.Dispose();
        ds_Remark3.Dispose();
        ds_Remark4.Dispose();
        ds_Remark5.Dispose();
        ds_Remark6.Dispose();
        ds_Remark7.Dispose();
        ds_Remark8.Dispose();
        ds_Remark9.Dispose();
        ds_Remark10.Dispose();
        ds_Remark11.Dispose();
        ds_Remark12.Dispose();
        ds_Remark13.Dispose();
        ds_Remark14.Dispose();
       // ds_Remark_back.Dispose();
        ds_Remark_now.Dispose();



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

            //TableCell headerDel = new TableCell();
            //headerDel.Text = "删除";
            //headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDel.Width = 30;
            //e.Row.Cells.Add(headerDel);
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

            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/Images/Delete.gif";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            //tabcDel.Controls.Add(ibtnDel);
            //e.Row.Cells.Add(tabcDel);


        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[8].Visible = false;
            //e.Row.Cells[9].Visible = false;

        }
    }


    #endregion

    #region 其它函数


    private string Verify()
    {
        string strErrorInfo = "";
        if (txt_Remark_now.Text.Trim() == "")
        {
            strErrorInfo += "请填写备注！\\n";
        }
        return strErrorInfo;
    }

    #endregion
    //保存
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

        }
        else
        {


            #region 编辑记录
            string strSql = "";
            //判断是否为第一次保存
            string selectidstr = "select * from t_Y_Detail where itemid='" + strSelectedId + "' and flag='0' and statusid='15'";
            DataSet dsid = new MyDataOp(selectidstr).CreateDataSet();
            //编辑
            if (dsid.Tables[0].Rows.Count > 0)
            {
                strSql = @"update t_Y_Detail 
                        set bz='" + txt_Remark_now.Text + "',CreateDate=getdate() where itemid='" + strSelectedId + "' and statusid='15' and id='" + SelectedId + "'";


            }
            //新加备注
            else
            {
                strSql = @"insert into t_Y_Detail
                                    (itemid,statusid,userid,CreateDate,bz)  
                                    values('" + strSelectedId + "','15','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark_now.Text + "')";
            }
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.ExecuteCommand();
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLogY("编辑保存（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 15);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存成功！');", true);
            }
            else
            {
                WebApp.Components.Log.SaveLogY("编辑保存（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 15);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存失败！');", true);
            }



            #endregion
            Query();
        }
    }
    //提交
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

        }
        else
        {


            #region 编辑记录


            string[] updatestr = new string[4];
            int i = 0;
            string strSql = "";
            //判断是否为第一次保存
            string selectidstr = "select * from t_Y_Detail where itemid='" + strSelectedId + "' and flag='0' and statusid='15'";
            DataSet dsid = new MyDataOp(selectidstr).CreateDataSet();
            //编辑
            if (dsid.Tables[0].Rows.Count > 0)
            {
                strSql = @"update t_Y_FlowInfo 
                        set StatusID=16  where id='" + strSelectedId + "'";
                updatestr.SetValue(strSql, i++);
                strSql = @"update t_Y_Detail 
                        set bz='" + txt_Remark_now.Text + "',CreateDate=getdate(),flag='1' where itemid='" + strSelectedId + "' and statusid='15' and id='" + SelectedId + "' ";
                updatestr.SetValue(strSql, i++);

            }
            //新加备注
            else
            {
                strSql = @"update t_Y_FlowInfo 
                        set StatusID=16  where id='" + strSelectedId + "'";
                updatestr.SetValue(strSql, i++);
                strSql = @"insert into t_Y_Detail
                                    (itemid,statusid,userid,CreateDate,bz,flag)  
                                    values('" + strSelectedId + "','15','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark_now.Text + "','1')";
                updatestr.SetValue(strSql, i++);
            }


            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.DoTran(i, updatestr);
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLogY("编辑提交（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 15);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存成功！');", true);
            }
            else
            {
                WebApp.Components.Log.SaveLogY("编辑提交（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 15);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存失败！');", true);
            }


            #endregion
            Query();
        }

    }
    //回退
    protected void btn_back_Click(object sender, EventArgs e)
    {
        string strFlag = Verify();
        if (strFlag != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + strFlag + "');", true);

        }
        else
        {


            #region 编辑记录

            int i = 0;
            //项目状态修改
            string strSql = @"update t_Y_FlowInfo 
                        set StatusID=14  where id='" + strSelectedId + "'";
            string[] strList = new string[5];
            strList.SetValue(strSql, i++);

            //判断是否为第一次保存
            string selectidstr = "select * from t_Y_Detail where itemid='" + strSelectedId + "' and flag='0' and statusid='15'";
            DataSet dsid = new MyDataOp(selectidstr).CreateDataSet();
            //编辑
            if (dsid.Tables[0].Rows.Count > 0)
            {
                strSql = @"update t_Y_Detail 
                        set bz='" + txt_Remark_now.Text + "',CreateDate=getdate(),flag='1' where itemid='" + strSelectedId + "' and statusid='15' and id='" + SelectedId + "'";


            }
            //新加备注
            else
            {
                strSql = @"insert into t_Y_Detail
                                    (itemid,statusid,userid,CreateDate,bz,flag)  
                                    values('" + strSelectedId + "','15','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + txt_Remark_now.Text + "','1')";
            }
            strList.SetValue(strSql, i++);
            //            //修改
            //            strSql = @"update t_Y_Detail 
            //                                    set flag='1' where itemid='" + strSelectedId + "' and statusid='2'";
            //strList.SetValue(strSql, i++);
            strSql = "Insert into t_Y_BackInfo(itemid,functionid,remark,userid,createdate)values(" + strSelectedId + ",15,'" + txt_Remark_now.Text + "记录时间：" + DateTime.Now.ToString() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate())";
            strList.SetValue(strSql, i++);
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.DoTran(i, strList);
            if (blSuccess == true)
            {
                WebApp.Components.Log.SaveLogY("回退（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 15);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存成功！');", true);
            }
            else
            {
                WebApp.Components.Log.SaveLogY("回退（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 15);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据保存失败！');", true);
            }


            #endregion
            Query();
        }

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;

        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        txt_ItemName.Text = "";
        txt_AccessTime.Text = "";
        // txt_date1.Text = "";
        txt_Remark_now.Text = "";
        Query();
    }

}