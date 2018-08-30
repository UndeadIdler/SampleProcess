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
using WebApp.Components;
using System.IO;
using System.Text;

public partial class Sample_UpLoad : System.Web.UI.Page
{
    public string filedirectory = ConfigurationManager.AppSettings["filedirectory"];
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string filestr//所选择操作列记录对应的id
    {
        get { return (string)ViewState["filestr"]; }
        set { ViewState["filestr"] = value; }
    }
   //int fileaddress = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LabMessage1.Text = "";
            LabMessage2.Text = "";
            if (Request.QueryString["kw"] != null && Request.QueryString["file"] != null)
            {
                strSelectedId = Request.QueryString["kw"].ToString();
                filestr = Request.QueryString["file"].ToString();
            }
            string str = "select id,ReportNumber 报告编号,ReportUploadDate 上传日期,ReportAddress 报告地址 from t_M_ReporInfo where id='" + strSelectedId + "' and fileflag=1";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            GridView_Report.DataSource = ds;
            GridView_Report.DataBind();
            ds.Dispose();
           
        }

    }
    protected void GridView_Report_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //TableCell headerDetail = new TableCell();
            //headerDetail.Text = "编辑";
            //headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDetail.Width = 60;
            //e.Row.Cells.Add(headerDetail);

            TableCell headerset = new TableCell();
            headerset.Text = "查看";
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

            ////手动添加详细和删除按钮
            //TableCell tabcDetail = new TableCell();
            //tabcDetail.Width = 60;
            //tabcDetail.Style.Add("text-align", "center");
            //ImageButton ibtnDetail = new ImageButton();
            //ibtnDetail.ImageUrl = "~/images/Detail.gif";
            //ibtnDetail.CommandName = "Edit";
            //tabcDetail.Controls.Add(ibtnDetail);
            //e.Row.Cells.Add(tabcDetail);

            TableCell MenuSet = new TableCell();
            MenuSet.Width = 60;
            MenuSet.Style.Add("text-align", "center");
            ImageButton btMenuSet = new ImageButton();
            btMenuSet.ImageUrl = "~/images/Detail.gif";
            btMenuSet.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuSet.Controls.Add(btMenuSet);
            e.Row.Cells.Add(MenuSet);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
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
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[8].Visible = false;

        }
    }
    protected void GridView_Report_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = GridView_Report.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string fileUrl = GridView_Report.Rows[e.RowIndex].Cells[4].Text;
        if (fileUrl != "&nbsp;")
        {
            string[] deletelist = new string[2];


            strSql = "update t_M_ReporInfo set ReportAddress=null,fileflag=0,ReportUploadDate=null WHERE id= '" + strSelectedId + "'";
            //待修改，改项目删除后，相应要修改的信息

            //deletelist.SetValue("delete from t_操作员信息 where 所属角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 0);
            //deletelist.SetValue("delete from t_角色菜单关系表 where 角色id=(select id from t_角色信息 where 级别id='" + strSelectedId + "')", 1);
            // deletelist.SetValue("DELETE FROM t_角色信息 WHERE 级别id = '" + strSelectedId + "'",2);

            deletelist.SetValue(strSql, 0);
            MyDataOp mdo = new MyDataOp(strSql);
            if (mdo.DoTran(1, deletelist))
            {
                try
                {
                    fileUrl = fileUrl.Replace("\\", "/");
                    int kkp = fileUrl.LastIndexOf('/');

                    //string link1 = fileUrl.Substring(kkp-1, fileUrl.Length - kkp);
                    FileInfo file = new FileInfo(fileUrl);
                    file.Delete();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('报告删除成功!')", true);

                }
                catch (Exception deletemsg)
                {
                    WebApp.Components.Log.SaveLog("报告编制中删除报告异常:（" + strSelectedId + "）" + deletemsg, Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                }
            }
            else
            {
                WebApp.Components.Log.SaveLog("报告编制中删除报告:（" + strSelectedId + "）失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('报告删除失败！')", true);
            }
            string str = "select id,ReportNumber 报告编号,ReportUploadDate 报告上传日期,ReportAddress 报告地址 from t_M_ReporInfo where id='" + strSelectedId + "' and fileflag=1";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            GridView_Report.DataSource = ds;
            GridView_Report.DataBind();

        }
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        string str1 = "select * from t_M_ReporInfo where id='" + strSelectedId + "' and ReportAddress is Null";
        DataSet dss = new MyDataOp(str1).CreateDataSet();
        if (dss.Tables[0].Rows.Count > 0)
        {
            bool fileOK = false;
            string path = filedirectory;
            if (FileUpload1.FileName != "")
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                String[] allowedExtensions = { ".doc", ".xls" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }

                if (fileOK)
                {
                    string pathstr = path + "\\Report\\" + FileUpload1.FileName;
                    try
                    {

                        FileUpload1.SaveAs(pathstr);

                        LabMessage1.Text = "文件上传成功.";
                        LabMessage2.Text = "<b>原文件路径：</b>" + FileUpload1.PostedFile.FileName + "<br />" +
                                      "<b>文件大小：</b>" + FileUpload1.PostedFile.ContentLength + "字节<br />" +
                                      "<b>文件类型：</b>" + FileUpload1.PostedFile.ContentType + "<br />";
                        string savestr = "update t_M_ReporInfo set ReportAddress='" + pathstr + "', ReportUploadDate=getdate(),fileflag=1 where id='" + strSelectedId + "'";
                        MyDataOp doobj = new MyDataOp(savestr);
                        if (doobj.ExecuteCommand())
                        {
                            WebApp.Components.Log.SaveLog("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）成功！ " + LabMessage2.Text, Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.alert('文件上传成功！');window.opener.location.href = '" + filestr + "';if(window.opener.progressWindow)window.opener.progressWindow.close();window.close();", true);
                        }
                        else
                        {
                            WebApp.Components.Log.SaveLog("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）文件上传成功!路径保存失败！ " + LabMessage2.Text, Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传成功!路径保存失败！')", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        WebApp.Components.Log.SaveLog("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）！存在异常： " + ex.Message.ToString(), Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                        LabMessage1.Text = "文件上传不成功.";
                    }



                }
                else
                {
                    WebApp.Components.Log.SaveLog("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）！存在异常： 只能够Word和Excel文件.", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);

                    LabMessage1.Text = "只能够Word和Excel文件.";
                }
            }
            else
            {
                WebApp.Components.Log.SaveLog("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）！存在异常： 没有要上传的文件.", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);

                LabMessage1.Text = " 没有要上传的文件."; }
            string str = "select id,ReportNumber 报告编号,ReportUploadDate 报告上传日期,ReportAddress 报告地址 from t_M_ReporInfo where id='" + strSelectedId + "' and fileflag=1";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            GridView_Report.DataSource = ds;
            GridView_Report.DataBind();
        }
        else
        {
            WebApp.Components.Log.SaveLog("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）已存在同名样品的报告文档！ " + LabMessage2.Text, Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在改报告的监测报告文档！')", true);
        }
        dss.Dispose();



    }
    protected void GridView_Report_SelectedIndexChanged(object sender, EventArgs e)
    {
        string url = GridView_Report.Rows[GridView_Report.SelectedIndex].Cells[4].Text.Trim();

        int i = url.LastIndexOf('\\');
        url = url.Substring(i + 1);

       
      
        string type = "";
       
        int start = url.LastIndexOf('.');
        type = url.Substring(start);
        string addfilename = url.Substring(0, start - 1);
        url = url.Replace("\\", "/");
        
        Uri uri = Request.Url;
        
        string str = "/file/Report";
        url = "http://" + uri.Authority.ToString() + str +"/"+ url;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('" + url + "','文档','')", true);
       
    }
   
}
