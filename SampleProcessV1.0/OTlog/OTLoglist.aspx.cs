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
using Microsoft.Office.Interop.Owc11;

using WebApp.Components;

public partial class OTlog_OTLogList : System.Web.UI.Page
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
        if (Request.Cookies["Cookies"] != null)
        {
            if (!IsPostBack)
            {
                #region 初始化页面元素
                string sql = "select name from t_R_UserInfo where userid = '" + Request.Cookies["Cookies"].Values["u_id"].ToString().Trim() + "'";
                DataSet ds = new MyDataOp(sql).CreateDataSet();
                name = ds.Tables[0].Rows[0][0].ToString();
                MyStaVoid.BindList("DepartName", "DepartID", "select DepartName,DepartID from t_M_DepartInfo where flag='1'", drop_depart);
                ListItem li = new ListItem("所有", "-1");
                drop_depart.Items.Add(li);
                drop_depart.SelectedIndex = drop_depart.Items.Count - 1;
                //SetTxt();
                //SetButton();
                txts_time1.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
                txts_time1.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                txts_time2.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
                txts_time2.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                txt_date.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
                grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>加班记录列表</b></font>";

                txt_time1.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'HH:mm'})");
                txt_time2.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'HH:mm'})");
                #endregion
                Query();
            }
        }
        else
        {
            Response.Write("<script language='javascript'>alert('您没有权限进入本页或当前登录用户已过期！\\n请重新登录或与管理员联系！');parent.location='../login.aspx';</script>");

        }
    }

    protected void txt_time1_TextChanged(object sender, EventArgs e)
    {
        if (txt_time1.Text.Trim() != string.Empty && txt_time2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime t2 = DateTime.Parse(txt_date.Text.Trim() + " " + txt_time2.Text.Trim() + ":00");
                DateTime t1 = DateTime.Parse(txt_date.Text.Trim() + " " + txt_time1.Text.Trim() + ":00");
                TimeSpan ts = t2 - t1;
                if (double.Parse(ts.TotalHours.ToString()) > 0)
                {
                    txt_num.Text = Math.Round(double.Parse(ts.TotalHours.ToString()),2).ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('时间段填写有误!');", true);
                    return;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('时间段填写有误!');", true);
                    return;
            }
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Owc11.SpreadsheetClass xlSheet = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
        xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, 7]).set_MergeCells(true);
        xlSheet.ActiveSheet.Cells[1, 1] = "加班记录(" + txts_time1.Text + "至" + txts_time2.Text + ")";

        //xlSheet.get_Range(xlSheet.Cells[2, 13], xlSheet.Cells[2, 20]).set_MergeCells(true);
        //xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[2, 1]).set_MergeCells(true);
        //xlSheet.get_Range(xlSheet.Cells[1, 2], xlSheet.Cells[1, 4]).set_MergeCells(true);
        //xlSheet.get_Range(xlSheet.Cells[1, 5], xlSheet.Cells[1, 7]).set_MergeCells(true);
        //xlSheet.get_Range(xlSheet.Cells[1, 8], xlSheet.Cells[1, 10]).set_MergeCells(true);
        //xlSheet.get_Range(xlSheet.Cells[1, 11], xlSheet.Cells[1, 13]).set_MergeCells(true);
        //xlSheet.get_Range(xlSheet.Cells[1, 14], xlSheet.Cells[1, 16]).set_MergeCells(true);
        xlSheet.ActiveSheet.Cells[2, 1] = "序号";
        xlSheet.ActiveSheet.Cells[2, 2] = "部门";
        xlSheet.ActiveSheet.Cells[2, 3] = "加班日期";
       
        xlSheet.ActiveSheet.Cells[2, 4] = "加班人员";
        xlSheet.ActiveSheet.Cells[2, 5] = "加班时间段";
        xlSheet.ActiveSheet.Cells[2, 6] = "加班小时数";
        xlSheet.ActiveSheet.Cells[2, 7] = "工作内容";
       
        for (int i = 0; i < grdvw_List.Rows.Count; i++)
        {

            if (grdvw_List.Rows[i].Cells[0].Text.Trim() != "&nbsp;")
                xlSheet.ActiveSheet.Cells[3 + i, 1] = grdvw_List.Rows[i].Cells[0].Text;
            if (grdvw_List.Rows[i].Cells[2].Text.Trim() != "&nbsp;")
                xlSheet.ActiveSheet.Cells[3 + i, 2] = grdvw_List.Rows[i].Cells[2].Text;
            if (grdvw_List.Rows[i].Cells[3].Text.Trim() != "&nbsp;")
                xlSheet.ActiveSheet.Cells[3 + i, 3] = grdvw_List.Rows[i].Cells[3].Text;
            if (grdvw_List.Rows[i].Cells[4].Text.Trim() != "&nbsp;")
                xlSheet.ActiveSheet.Cells[3 + i, 4] = grdvw_List.Rows[i].Cells[4].Text;
            if (grdvw_List.Rows[i].Cells[5].Text.Trim() != "&nbsp;")
                xlSheet.ActiveSheet.Cells[3 + i, 5] = grdvw_List.Rows[i].Cells[5].Text;
            if (grdvw_List.Rows[i].Cells[6].Text.Trim() != "&nbsp;")
                xlSheet.ActiveSheet.Cells[3 + i, 6] = grdvw_List.Rows[i].Cells[6].Text;
            if (grdvw_List.Rows[i].Cells[7].Text.Trim() != "&nbsp;")
                xlSheet.ActiveSheet.Cells[3 + i, 7] = grdvw_List.Rows[i].Cells[7].Text;

            
        }
        try
        {
            string strFileName = "加班记录(" + txts_time1.Text + "至" + txts_time2.Text + ").xls";
            xlSheet.Export(Server.MapPath(".") + "\\" + strFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.location.href='ShowXls.aspx?file_name=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "';", true);

            RemoveFiles(Server.MapPath("."));
        }
        catch
        {
        }


    }
    /// <summary>
    /// 删除超时文件
    /// </summary>
    /// <param name="strPath"></param>
    private void RemoveFiles(string strPath)
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] fiArr = di.GetFiles();
        foreach (System.IO.FileInfo fi in fiArr)
        {
            if (fi.Extension.ToString() == ".xls")
            {
                // if file is older than 2 minutes, we'll clean it up
                TimeSpan min = new TimeSpan(0, 0, 2, 0, 0);
                if (fi.CreationTime < DateTime.Now.Subtract(min))
                {
                    fi.Delete();
                }
            }
        }
    } 
    //查询并填入GridView中
    private void Query()
    {
        string str = "";
        if(drop_depart.SelectedValue!="-1")
            str += " and t_R_UserInfo.DepartID='" + drop_depart.SelectedValue + "'";
        if (txts_name.Text.Trim() != "") str += " and t_R_UserInfo.name like '%" + txts_name.Text.Trim() + "%'";
        if (txts_ulog.Text.Trim() != "") str += " and ulog like '%" + txts_ulog.Text.Trim() + "%'";
        if (txts_time1.Text.Trim() != "") str += " and dtime >= '" + txts_time1.Text.Trim() + "'";
        if (txts_time2.Text.Trim() != "") str += " and dtime <= '" + txts_time2.Text.Trim() + " 23:59:59'";
        //convert(varchar(10),getdate(),120) 
        if(drop_status_query.SelectedValue!="")
            str += " and t_R_OTUserLog.flag='" + drop_status_query.SelectedValue + "'";
        string strSql = "select t_R_OTUserLog.id,DepartName 所在部门,convert(varchar(10),dtime,120) as 加班日期,t_R_OTUserLog.name as 加班人员,period 加班时间段,num 加班小时数,ulog as 加班内容,UserID,case when t_R_OTUserLog.flag='0' then '待审核'   when t_R_OTUserLog.flag='1' then '通过' when t_R_OTUserLog.flag ='2' then '未通过' end as 状态 from t_R_OTUserLog inner join t_R_UserInfo on t_R_OTUserLog.uid=t_R_UserInfo.UserID inner join t_M_DepartInfo on t_R_UserInfo.DepartID=t_M_DepartInfo.DepartID  where 1=1 " + str + " order by t_M_DepartInfo.DepartID,t_R_OTUserLog.dtime asc";
       
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
                // 根据审核权限信息进行修改 
                //if (grdvw_List.Rows[i].Cells[9].Text.Trim() == "待审核")
                //{
                //    ImageButton fibtn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_Select");
                //    fibtn.Visible = true;

                //}
                //else
                //{
                //    ImageButton fibtn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_Select");
                //    fibtn.Visible = false;
                //}

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


            ////添加按钮列的标题
            //TableCell tabcHeaderSelect = new TableCell();
            //tabcHeaderSelect.Text = "审核";
            //tabcHeaderSelect.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //tabcHeaderSelect.Width = 60;
            //e.Row.Cells.Add(tabcHeaderSelect);
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

            ////手动添加详细和删除按钮
            //TableCell tabcSelect = new TableCell();
            //tabcSelect.Width = 60;
            //tabcSelect.Style.Add("text-align", "center");
            //ImageButton ibtnSelect = new ImageButton();
            //ibtnSelect.ID = "btn_Select";
            //ibtnSelect.ImageUrl = "~/Images/Detail.gif";
            //ibtnSelect.CommandName = "Select";
            //tabcSelect.Controls.Add(ibtnSelect);
            //e.Row.Cells.Add(tabcSelect);         
            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/Images/Delete.gif";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            ////if (Int16.Parse(Request.Cookies["Cookies"].["u_level"].ToString()) >6 || Request.Cookies["Cookies"].["u_purview"].ToString().Substring(0, 1) == "0")
            //if (Request.Cookies["Cookies"].["u_purview"].ToString().Substring(1, 1) == "0")
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
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[3].Visible = false;
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

    //    strSql = "select * from t_R_OTUserLog where TypeID = '" + strSelectedId + "'";
    //    DataSet ds = new MyDataOp(strSql).CreateDataSet();
   
    //    if (ds.Tables.Count == 1)
    //    {
    //        strSql = "DELETE FROM t_R_OTUserLog WHERE TypeID = '" + strSelectedId + "'";
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
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        CleanAllText();
        lbl_Type.Text = "详细";
        panel_sh.Visible = true;
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text;
        string strSql = @"select * from t_R_OTUserLog 
                        where id='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        txt_name.Text = ds.Tables[0].Rows[0]["name"].ToString().Trim();
        txt_ulog.Text = ds.Tables[0].Rows[0]["ulog"].ToString().Trim();
        txt_date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtime"].ToString().Trim()).ToString("yyyy-MM-dd");
        string period = ds.Tables[0].Rows[0]["period"].ToString().Trim();
        string[] timelist = period.Split('-');
        if (timelist.Length == 2)
        {
            txt_time1.Text = timelist[0];
            txt_time2.Text = timelist[1];
        }
        txt_num.Text = ds.Tables[0].Rows[0]["num"].ToString().Trim();
        AllTxtreadOnly();
        btn_OK.Visible = false;
        //btn_sh.Visible = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);

    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        CleanAllText();
        lbl_Type.Text = "详细";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
        string strSql = @"select * from t_R_OTUserLog 
                        where id='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        txt_name.Text = ds.Tables[0].Rows[0]["name"].ToString().Trim();
        txt_ulog.Text = ds.Tables[0].Rows[0]["ulog"].ToString().Trim();
        txt_date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtime"].ToString().Trim()).ToString("yyyy-MM-dd");
        string period = ds.Tables[0].Rows[0]["period"].ToString().Trim();
        string status = ds.Tables[0].Rows[0]["flag"].ToString().Trim();
        if (status.Trim() == "0")
        {
            btn_OK.Enabled = true;
            panel_sh.Visible = false;
           
            //btn_sh.Visible = false;
        }
            else if(status.Trim()=="2")
        {
           
            btn_OK.Enabled = true;
            panel_sh.Visible = true;
            drop_flag.Enabled = false;
            txt_remark.Enabled = false;
            drop_flag.SelectedValue = status;
            txt_remark.Text = ds.Tables[0].Rows[0]["remark"].ToString().Trim();
            //btn_sh.Visible = false;
        }
        else
        {
            drop_flag.SelectedValue = status;
            btn_OK.Enabled = false;
            panel_sh.Visible = true;
            drop_flag.Enabled = false;
            txt_remark.Enabled = false;
            txt_remark.Text = ds.Tables[0].Rows[0]["remark"].ToString().Trim();
            //btn_sh.Visible = false;
        }
        string[] timelist = period.Split('-');
        if (timelist.Length == 2)
        {
            txt_time1.Text = timelist[0];
            txt_time2.Text = timelist[1];
        }
        txt_num.Text = ds.Tables[0].Rows[0]["num"].ToString().Trim();
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
        panel_sh.Visible = false;
        btn_OK.Enabled = true;
        //btn_sh.Visible = false;
        //txt_name.Text = Request.Cookies["Cookies"].["u_id"].ToString().Trim();       
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
    ////审核
    //protected void btn_sh_Click(object sender, EventArgs e)
    //{
    //    btn_OK.Visible = true;
    //    btn_Cancel.Visible = true;
    //}
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
            if (txt_date.Text.Trim()==string.Empty||txt_time1.Text.Trim() == string.Empty || txt_time2.Text.Trim() == string.Empty || txt_num.Text.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('信息填写不完整！');", true);
                return;
            }
            try
            {
                double num = double.Parse(txt_num.Text);
            }
            catch {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('小时数必须为数值型！');", true);

            }
            string period = txt_time1.Text + "-" + txt_time2.Text;
            //添加新纪录
            if (lbl_Type.Text == "添加")
            {

                string strSql = @"insert into t_R_OTUserLog
                        (name,ulog,utime,dtime,uid,period,num)  
                        values('" + txt_name.Text.Trim() + "','" + txt_ulog.Text.Trim() + "','" + DateTime.Now.ToString() + "','" + txt_date.Text.Trim() + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "','"+period+"','"+txt_num.Text.Trim()+"')";
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
                string strSql = @"update t_R_OTUserLog 
                            set period='"+period+"',num='"+txt_num.Text.Trim()+"', ulog='" + txt_ulog.Text.Trim() + "',dtime='" + txt_date.Text.Trim() + "' where id = '" + strSelectedId + "'";
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
        txt_num.Text = "";
        txt_time1.Text = "";
        txt_time2.Text = "";
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        txt_date.Enabled = false;
        txt_ulog.ReadOnly = true;
        txt_num.ReadOnly = true;
        txt_time1.Enabled = false;
        txt_time2.Enabled = false;
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        txt_date.Enabled = true;
        txt_ulog.ReadOnly = false;
        txt_time1.Enabled = true;
        txt_time2.Enabled = true;
    }
    //private void SetTxt()//设置详细页面中的TextBox的一些属性
    //{
    //    txt_name.MaxLength = 100;
    //    txt_ulog.MaxLength = 100;      
    //}
    //private void SetButton()//根据权限设置读写相关的按钮是否可用
    //{
    //    //if (Int16.Parse(Request.Cookies["Cookies"].["u_level"].ToString()) >6 || Request.Cookies["Cookies"].["u_purview"].ToString().Substring(0, 1) == "0")
    //    if (Request.Cookies["Cookies"].["u_purview"].ToString().Substring(1, 1) == "0")
            
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
