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
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strFId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strFId"]; }
        set { ViewState["strFId"] = value; }
    }
    private string strRId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strRId"]; }
        set { ViewState["strRId"] = value; }
    }
    private string strSFile//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSFile"]; }
        set { ViewState["strSFile"] = value; }
    }
   //int fileaddress = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LabMessage1.Text = "";
            LabMessage2.Text = "";
            strFId = "";
            strRId = "";
            if (Request.QueryString["fid"] != null)
                strFId = Request.QueryString["fid"].ToString();
            if (Request.QueryString["rid"] != null)
                strRId = Request.QueryString["rid"].ToString();
            if (Request.QueryString["sfile"] != null)
                strSFile = HttpUtility.UrlDecode(Request.QueryString["sfile"].ToString());
            string str = "select t_Y_fileinfo.id,rid 项目编号,userid 上传者,fileName 文件名,address 文件地址,fid,name 工作环节,createdate 上传时间 from t_Y_fileinfo inner join t_Y_FlowDetail on t_Y_FlowDetail.id=t_Y_fileinfo.fid where rid='" + strRId + "' and fid<=" + strFId + "";

           // string str = "select id,rid 项目编号,userid 上传者,fileName 文件名,address 文件地址,fid from t_Y_fileinfo where rid='" + strRId + "' and fid<=" + strFId + "";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            string strtemp = "select Name,UserID from t_R_UserInfo";
            DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drr in ds_User.Tables[0].Rows)
                {
                    if (dr["上传者"].ToString() == drr["UserID"].ToString())
                        dr["上传者"] = drr["Name"].ToString();
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
                GridView_Report.DataSource = ds;
                GridView_Report.DataBind();
                int intColumnCount = GridView_Report.Rows[0].Cells.Count;
                GridView_Report.Rows[0].Cells.Clear();
                GridView_Report.Rows[0].Cells.Add(new TableCell());
                GridView_Report.Rows[0].Cells[0].ColumnSpan = intColumnCount;
            }
            else
            {
                GridView_Report.DataSource = ds;
                GridView_Report.DataBind();
            }
           
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
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[2].Visible = false;
            //e.Row.Cells[8].Visible = false;

        }
    }
    protected void GridView_Report_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = GridView_Report.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        int ddfid =int.Parse(GridView_Report.Rows[e.RowIndex].Cells[6].Text);
        if (strSelectedId != "" && (ddfid >= int.Parse(strFId) || (int.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) <= 6)))
        {


            string fileUrl = GridView_Report.Rows[e.RowIndex].Cells[5].Text;
            if (fileUrl != "&nbsp;")
            {
                string[] deletelist = new string[2];


                strSql = "Delete from t_Y_fileinfo WHERE id= '" + strSelectedId + "'";
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
                        WebApp.Components.Log.SaveLogY("删除文件:项目编号：" + strRId + "（" + strSelectedId + "）成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件删除成功!')", true);

                    }
                    catch (Exception deletemsg)
                    {
                        WebApp.Components.Log.SaveLogY("删除文件异常:项目编号：" + strRId + "（" + strSelectedId + "）" + deletemsg, Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));
                    }
                }
                else
                {
                    WebApp.Components.Log.SaveLogY("删除文件异常:项目编号：" + strRId + "（" + strSelectedId + "）失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件删除失败！')", true);
                }
                string str = "select id,rid 项目编号,userid 上传者,fileName 文件名,address 文件地址,fid from t_Y_fileinfo where rid='" + strRId + "' and fid<=" + strFId + "";
                DataSet ds = new MyDataOp(str).CreateDataSet();
                string strtemp = "select Name,UserID from t_R_UserInfo";
                DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    foreach (DataRow drr in ds_User.Tables[0].Rows)
                    {
                        if (dr["上传者"].ToString() == drr["UserID"].ToString())
                            dr["上传者"] = drr["Name"].ToString();
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
                    GridView_Report.DataSource = ds;
                    GridView_Report.DataBind();
                    int intColumnCount = GridView_Report.Rows[0].Cells.Count;
                    GridView_Report.Rows[0].Cells.Clear();
                    GridView_Report.Rows[0].Cells.Add(new TableCell());
                    GridView_Report.Rows[0].Cells[0].ColumnSpan = intColumnCount;
                }
                else
                {
                    GridView_Report.DataSource = ds;
                    GridView_Report.DataBind();
                }

            }
        }


        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你没有权限删除该文件!')", true);
        }

    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        //string str1 = "select * from t_M_SampleInfor where id='" + strSelectedId + "' and ReportAddress is Null";
        //DataSet dss = new MyDataOp(str1).CreateDataSet();
        //if (dss.Tables[0].Rows.Count > 0)
        //{
      
        string ServerPath = Server.MapPath("~/UpLoad/");
        DirectoryInfo TheFolderServer = new DirectoryInfo(ServerPath);
        if (!TheFolderServer.Exists)
        {
            Directory.CreateDirectory(ServerPath);
        }
            bool fileOK = false;
            string folder = strRId;// DateTime.Now.Date.ToString("yyyy-MM-dd");

            string path = ServerPath + folder + "\\";
           
        DirectoryInfo TheFolder = new DirectoryInfo(path);
            if (!TheFolder.Exists)
            {
                Directory.CreateDirectory(path);     
            }
              
            
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
                    string pathstr = path + FileUpload1.FileName;
                    try
                    {

                        if (!File.Exists(pathstr))
                        {
                            //string DBstr = "/UpLoad/" + folder + "/" + FileUpload1.FileName;
                           
                            FileUpload1.SaveAs(path + FileUpload1.FileName);

                            //LabMessage1.Text = "文件上传成功.";
                            //LabMessage2.Text = "<b>原文件路径：</b>" + FileUpload1.PostedFile.FileName + "<br />" +
                            //              "<b>文件大小：</b>" + FileUpload1.PostedFile.ContentLength + "字节<br />" +
                            //              "<b>文件类型：</b>" + FileUpload1.PostedFile.ContentType + "<br />";
                            string savestr = "Insert into t_Y_fileinfo(fid,rid,address,userid,createdate,filename)values(" + strFId + "," + strRId + ",'" + pathstr + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate(),'" + FileUpload1.FileName + "')";
                            MyDataOp doobj = new MyDataOp(savestr);
                            if (doobj.ExecuteCommand())
                            {
                                WebApp.Components.Log.SaveLogY("上传文件:项目编号：" + strRId + "（" + FileUpload1.PostedFile.FileName + "）成功！ " + LabMessage2.Text, Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));//window.opener.location.href = '" + strSFile + "';if(window.opener.progressWindow)window.opener.progressWindow.close();window.close();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.alert('文件上传成功！');", true);
                            }
                            else
                            {
                                WebApp.Components.Log.SaveLogY("上传文件:项目编号：" + strRId + "（" + FileUpload1.PostedFile.FileName + "）文件上传成功！路径保存失败！ " + LabMessage2.Text, Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传成功!路径保存失败！')", true);
                            }
                        }
                        else
                        {
                            WebApp.Components.Log.SaveLogY("上传文件:项目编号：" + strRId + "（" + FileUpload1.PostedFile.FileName + "）已存在同名文件！ " + LabMessage2.Text, Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('同名文件已存在，请重命名后上传！')", true);

                        }

                    }
                    catch (Exception ex)
                    {
                        WebApp.Components.Log.SaveLogY("上传文件:项目编号：" + strRId + "（" + FileUpload1.PostedFile.FileName + "）！存在异常： " + ex.Message.ToString(), Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));

                       // Log.SaveLog("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）！存在异常： " + ex.Message.ToString(), Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);
                        LabMessage1.Text = "文件上传失败！";
                    }



                }
                else
                {
                    WebApp.Components.Log.SaveLogY("上传文件:项目编号：" + strRId + "（" + FileUpload1.PostedFile.FileName + "）！存在异常：文件类型不匹配。 只能够Word和Excel文件.", Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));
               

                    LabMessage1.Text = "只能够Word和Excel文件.";
                }
            }
            else
            {
                WebApp.Components.Log.SaveLogY("上传文件:项目编号：" + strRId + "（" + FileUpload1.PostedFile.FileName + "）！存在异常：没有要上传的文件.", Request.Cookies["Cookies"].Values["u_id"].ToString(), int.Parse(strFId));

             //   Log.SaveLogY("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）！存在异常： 没有要上传的文件.", Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);

                LabMessage1.Text = " 没有要上传的文件."; }
            string str = "select id,rid 项目编号,userid 上传者,fileName 文件名,address 文件地址,fid from t_Y_fileinfo where rid='" + strRId + "' and fid<=" + strFId + "";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            string strtemp = "select Name,UserID from t_R_UserInfo";
            DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drr in ds_User.Tables[0].Rows)
                {
                    if (dr["上传者"].ToString() == drr["UserID"].ToString())
                        dr["上传者"] = drr["Name"].ToString();
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
                GridView_Report.DataSource = ds;
                GridView_Report.DataBind();
                int intColumnCount = GridView_Report.Rows[0].Cells.Count;
                GridView_Report.Rows[0].Cells.Clear();
                GridView_Report.Rows[0].Cells.Add(new TableCell());
                GridView_Report.Rows[0].Cells[0].ColumnSpan = intColumnCount;
            }
            else
            {
                GridView_Report.DataSource = ds;
                GridView_Report.DataBind();
            }
        //}
        //else
        //{
        //    Log.SaveLogY("报告编制中上传报告:（" + FileUpload1.PostedFile.FileName + "）已存在同名样品的报告文档！ " + LabMessage2.Text, Request.Cookies["Cookies"].Values["u_id"].ToString(), 6);

        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在同名样品的报告文档！')", true);
        //}



    }
    protected void GridView_Report_SelectedIndexChanged(object sender, EventArgs e)
    {
       int fileaddress = int.Parse(ConfigurationManager.AppSettings["count"].ToString());
      
        string  path = GridView_Report.SelectedRow.Cells[5].Text;
        string link1 = path;
        link1 = link1.Replace("\\", "/");

        int i = link1.IndexOf("/UpLoad");
        Uri uri = Request.Url;
        link1 = "http://" + uri.Authority.ToString() +  link1.Substring(i);
        //if (!File.Exists(link1))
        //{
        //    //int i = path.LastIndexOf('\\');
        //    //int j = path.LastIndexOf('.');
        //    string message ="文件不存在！";
        //    Response.Write("<script>alert('" + message + "');</script>");
        //    return;
        //}
        //else
        //{
            int j = path.LastIndexOf('.');
            string typeStr = path.Substring(j, 4);
            switch (typeStr)
            {
                //case ".txt":
                //    //DBOperate doText = new DBOperate();//字符串处理实例
                //    Encoding encoder = Encoding.GetEncoding("gb2312");  //定义编码方式
                //    StreamReader read = new StreamReader(path, encoder);//定义StreamReader并用指定编码读取fileName中的内容
                //    //streamStr = doText.GetStr(read.ReadToEnd());//将读取的字符流用textbox显示
                //    break;
                default:
                    try
                    {
                       
                        //openname = link1;
                        //Stream readerDoc=Server.CreateObject("ADODB.Stream");
                        //readerDoc.Read(
                        Page.RegisterStartupScript("Link1", "<script language='javascript'>window.open('" + link1 + "');</script>");
                    }
                    catch (Exception exmsg)
                    {
                        Response.Write("<script>alert('读取文件时出错！');</script>");
                        return;
                    }
                    break;
            }
        //}
    }
   
}
