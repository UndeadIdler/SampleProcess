using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using WebApp.Components;

public partial class Content_ExpertTree : System.Web.UI.Page
{
    public string openname = "";
    
    public string filedirectory = ConfigurationManager.AppSettings["filedirectory"];
    public string Bakfiledirectory = ConfigurationManager.AppSettings["Bakfiledirectory"];
    //查询数据库读出记录，加载TreeView
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["Cookies"].Values["u_id"] != null)
        {
            if (!this.IsPostBack)
            {
                AutoTree(TreeView1);
                Query("", "");
                SetButton();
            }
        }
        else
            Response.Write("<script language='javascript'>alert('你没有权限查看该模块！或者登陆已经过期！');window.parent.location.href=\"../Login.aspx\";</script>");
    }
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            Button_AddCatalog.Enabled = false;
            Panel1.Visible = false;
            Button_Delete.Visible = false;
            
        }
        else
        {
            Button_AddCatalog.Enabled = true;
            Panel1.Visible = true;
            Button_Delete.Visible = true;
        }
    }
    #region GridView相关事件函数

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //修改字段标题
            //e.Row.Cells[2].Text = "车牌号";
            //e.Row.Cells[3].Text = "限载人数";


            ////添加按钮列的标题
            //TableCell tabcHeaderEdit = new TableCell();
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(3, 1) == "0")
            //{
            //    tabcHeaderEdit.Text = "详细";
            //}
            //else
            //    tabcHeaderEdit.Text = "编辑";
            //tabcHeaderEdit.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //tabcHeaderEdit.Width = 60;
            //e.Row.Cells.Add(tabcHeaderEdit);
            //添加按钮列的标题
            TableCell tabcHeaderDetail = new TableCell();
            tabcHeaderDetail.Text = "下载";
            tabcHeaderDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDetail.Width = 60;
            e.Row.Cells.Add(tabcHeaderDetail);

            TableCell tabcHeaderDel = new TableCell();
            tabcHeaderDel.Text = "删除";
            tabcHeaderDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDel.Width = 30;
            e.Row.Cells.Add(tabcHeaderDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = id.ToString();

            ////手动添加详细和删除按钮
            //TableCell tabcEdit = new TableCell();
            //tabcEdit.Width = 60;
            //tabcEdit.Style.Add("text-align", "center");
            //ImageButton ibtnEdit = new ImageButton();
            //ibtnEdit.ImageUrl = "~/Images/Detail.gif";
            //ibtnEdit.CommandName = "Edit";
            //tabcEdit.Controls.Add(ibtnEdit);
            //e.Row.Cells.Add(tabcEdit);
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/Images/Detail.gif";
            ibtnDetail.CommandName = "Select";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/Images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
            {
                ibtnDel.Visible = false;
                //ibtnEdit.Visible = false;
            }
            else
            {
                tabcDel.Visible = true;
                //ibtnEdit.Visible = true;
            }

            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(3, 1) == "0")
            {
                e.Row.Cells[4].Visible = false;
            }
            else
                e.Row.Cells[4].Visible = true;
            for (int i = 5; i < e.Row.Cells.Count - 2; i++)
                e.Row.Cells[i].Visible = false;//绑定数据后，隐藏0列 


        }
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query("", "");
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[5].Text;
        string strSql;

        string name = grdvw_List.Rows[e.RowIndex].Cells[7].Text;
        int point = 0;
        point = name.LastIndexOf('.');

        if (point > 0)//删除文件 (先删除文件再删除数据库中的信息)
        {
            string filePath = "";
            string truePath = "";
            string fileName = name;

            DataSet ds = new DataSet();
            string path = grdvw_List.Rows[e.RowIndex].Cells[6].Text;//this.ReturnPath(this.TreeView1.SelectedNode.Parent, "");


            if (path == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据库中没有文件路径！');", true);
               // Response.Write("<script>alert('数据库中没有文件路径！');</script>");
                return;
            }
            else
            {
                try
                {
                    truePath = path;
                    File.Delete(filedirectory+truePath);
                }
                catch (Exception msg)
                {
                    // logExpert.writOperateLog("删除实际文件时出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "38");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除文件时出错！');", true);
                    //Response.Write("<script>alert('删除文件时出错！');</script>");
                    return;
                }

                //删除数据库中的信息
                string delstr = String.Format("delete from t_document where id = '{0}'", strSelectedId);
                MyDataOp dbdo = new MyDataOp(delstr);
                int i = 0;
                try
                {
                    if (dbdo.ExecuteCommand())
                    {
                        Query("", "");
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除文件成功！');", true);
                       // Response.Write("<script>alert('删除文件成功！');window.location.href='ExpertTree.aspx';</script>");
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除文件失败！');", true);
                        //Response.Write("<script>alert('删除文件失败！');window.location.href='ExpertTree.aspx';</script>");
                        return;
                    }
                }
                catch (Exception msg)
                {
                    //logExpert.writOperateLog("删除文件时删除数据库中信息时出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "38");

                }
                finally
                {

                }

            }
        }


        Query("", txt_keyword.Text.Trim());

    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string url = grdvw_List.Rows[e.NewEditIndex].Cells[12].Text.Trim();
        url = url.Replace("\\", "/");
        // int i = url.LastIndexOf("/");
        int i = url.IndexOf("/filemanagement");
        Uri uri = Request.Url;
        string str = "/file";
        url = "http://" + uri.Authority.ToString() + str + url.Substring(i);
        url = HttpUtility.HtmlEncode(url);
        url = " ../DsoFramer/EditFile.aspx?FilePath=" + url;//FilePath=634945367023750000.doc

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('" + url + "','文档','toolbar=no')", true);
    }

    #endregion
    private void VisibleControls(bool isTrue)
    {
        //根据权限显示页面控件
        this.Label2.Visible = isTrue;
        TextBox_Catalog.Visible = isTrue;
        Button_AddCatalog.Visible = isTrue;
        Button_Delete.Visible = isTrue;
        Label1.Visible = isTrue;
        TextBox_KeyWord.Visible = isTrue;
        FileUpload1.Visible = isTrue;
        Button_AddFile.Visible = isTrue;
    }

    #region 文件目录


    private void AutoTree(TreeView tree)
    {

        tree.Nodes.Clear();
        DataSet ds = new DataSet();
        string sqlStr = "select * from t_directory where PARENTID is null or PARENTID = ''";

        try
        {
            ds = new MyDataOp(sqlStr).CreateDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode parent = new TreeNode();
                parent.Text = dr["NAME"].ToString();
                parent.Value = dr["ID"].ToString();
                tree.Nodes.Add(parent);
                this.AddChildNode(parent, dr["ID"].ToString());

            }
        }
        catch (Exception msg)
        {   //修改：异常捕捉，并对异常记日志。修改人：WQ
           // Response.Write("<script language='javascript'>alert('文件系统加载出错!');</script>");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件系统加载出错！');", true);
             
        }
        finally
        {//wq2007-10-30
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }

    }
    private void AddChildNode(TreeNode parent, string ID)
    {

        DataSet dataSet = new DataSet();

        string str = "select * from t_directory where PARENTID =" + "'" + ID + "'";

        try
        {//异常捕捉，并记错误日志。修改人：WQ
            dataSet = new MyDataOp(str).CreateDataSet();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TreeNode child = new TreeNode();
                child.Text = row["NAME"].ToString();
                child.Value = row["ID"].ToString();
                parent.ChildNodes.Add(child);
                this.AddChildNode(child, row["ID"].ToString());
                string strContent = row["LINK"].ToString();

            }
        }
        catch (Exception msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('目录加载错误！');", true);
           // Response.Write("<script language='javascript'>alert('目录加载错误！');</script>");
        }
        finally
        {
            if (dataSet != null)
            {
                dataSet.Dispose();
                dataSet = null;
            }

        }
    }
    protected void Query(string fatherID, string keyword)
    {
        string str = "select * from t_document where type=1";
        if (fatherID != "")
        { str += " and parentid='" + fatherID + "'"; }
        if (keyword != "")
        {
            str += " and  KEYWORDS like '%" + keyword + "%'";
        }
        str += " order by uploaddate desc";
        DataSet ds = new MyDataOp(str).CreateDataSet();
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

    //根据所选的TreeNode节点加载文件，并将其读出
    protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    {
        string name = this.TreeView1.SelectedNode.Text.Trim();
        string selectid = this.TreeView1.SelectedNode.Value.Trim();
        int point = 0;
        point = name.LastIndexOf('.');

        this.TreeView1.SelectedNode.Expand();

        Query(selectid, "");

    }


    //保存文件
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

        if (this.TextBox_KeyWord.Text == "")
        {
            Response.Write("<script>window.alert('请选择添加关键字！')</script>");
            return;
        }
        if (this.TreeView1.SelectedNode == null)
        {
            Response.Write("<script>window.alert('请选择要保存到哪个目录！')</script>");
            return;
        }
        if (FileUpload1.PostedFile.FileName == "")
        {
            Response.Write("<script>window.alert('请选择文件后点击上传！')</script>");
            return;
        }

        //并要取得 唯一的文件路径
        //判断是否有同名的文件

        string fileName = FileUpload1.PostedFile.FileName;
        int i = fileName.LastIndexOf('\\');
        fileName = fileName.Substring(i + 1);

        string name = this.TreeView1.SelectedNode.Text;
        string link = "";
        string path = "";
        string type = "";
        int point = 0;
        point = name.LastIndexOf('.');
        int start = fileName.LastIndexOf('.');
        type = fileName.Substring(start);
        string addfilename = fileName.Substring(0, start - 1);
        if (point > 0)
        {
            path = this.ReturnPath(this.TreeView1.SelectedNode.Parent, "");
            link = path + "\\" + fileName;// +guid + type;
        }
        else
        {
            path = this.ReturnPath(this.TreeView1.SelectedNode, "");
            link = path + "\\" + fileName;// +guid + type;
        }

        string parentID = "";
        string allPath = "";
        DataSet ds = new DataSet();
        string filePath = "";
        string str = String.Format("select * from t_directory where FILEPATH like '%{0}'", path);
        try
        {//修改：捕捉异常，错误记日志。修改人：WQ

            ds = new MyDataOp(str).CreateDataSet();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                parentID = row["ID"].ToString();
                filePath = row["FILEPATH"].ToString() + '\\' + fileName;
                allPath = filedirectory +  filePath;//+ guid + type;
            }
        }
        catch (Exception msg)
        {
            // logExpert.writOperateLog("专家系统添加文件，获取文件路径出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");
            Response.Write("<script>alert('保存文件时，文件路径获取出错！');</script>");
            return;
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }

        if (File.Exists(allPath))
        {
            Response.Write("<script>alert('已存在该文件!');</script>");
            return;
        }
        else
        {
            try
            {
                Stream inStream = FileUpload1.FileContent;
                this.FileUpload1.SaveAs(allPath);
                this.InsertRow(fileName, parentID, filePath, link, inStream, addfilename, type);
            }
            catch (Exception msg)
            {
                Response.Write("<script>alert('保存文件时出错!')</script>");
                return;
            }
        }
       

    }

    private void InsertRow(string insertFileName, string parentID, string allPath, string path, Stream file, string guid, string type)
    {
        byte[] bytelist = new byte[file.Length];
        file.Read(bytelist, 0, int.Parse(file.Length.ToString()));
        string strsql = String.Format("insert into t_document(NAME,PARENTID,FILEPATH,SORT,TYPE,KEYWORDS,LINK,guid,cFileType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", insertFileName, parentID, allPath, "", "1", this.TextBox_KeyWord.Text.Trim(), path, guid, type);


        StringBuilder str = new StringBuilder();
        str.Append(String.Format("insert into t_document(NAME,PARENTID,FILEPATH,SORT,TYPE,KEYWORDS,LINK,guid,cContent) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',@content)", insertFileName, parentID, allPath, "", "1", this.TextBox_KeyWord.Text.Trim(), path, guid));

        try
        {

            MyDataOp dbdo = new MyDataOp(strsql);
            if (dbdo.ExecuteCommandpara(bytelist, str))
            {
                Response.Write("<script>window.alert('保存文件成功！'); window.location.href ='ExpertTree.aspx';</script>");
                return;
            }
            else
            {
                if (dbdo.ExecuteCommand())
                {
                    Response.Write("<script>window.alert('保存文件成功！'); window.location.href ='ExpertTree.aspx';</script>");
                    return;
                }
                else
                {
                    Response.Write("<script>window.alert('保存文件失败！'); window.location.href ='ExpertTree.aspx';</script></script>");
                    return;
                }
            }
        }
        catch (Exception msg)
        {

        }
        finally
        {

        }

    }

    //搜索
    protected void Button1_Click(object sender, EventArgs e)
    {
        Query("", txt_keyword.Text.Trim());
    }

    //添加目录
    protected void Button2_Click(object sender, EventArgs e)
    {


        if (this.TreeView1.SelectedNode == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择要添加到哪个目录！');", true);
           // Response.Write("<script>window.alert('请选择要添加到哪个目录！')</script>");
            return;
        }

        if (this.TextBox_Catalog.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('目录名不能为空！');", true);
           // Response.Write("<script>window.alert('目录名不能为空！')</script>");
            return;
        }

        string name = this.TreeView1.SelectedNode.Text;
        int point = 0;
        point = name.LastIndexOf('.');
        //所选的是文件
        string directoryPath = "";
        string parentID = "";

        DataSet ds = new DataSet();
        string path = this.ReturnPath(this.TreeView1.SelectedNode, "");

        string str = String.Format("select * from t_directory where FILEPATH like '%{0}'", path);

        try
        {
            ds = new MyDataOp(str).CreateDataSet();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                directoryPath = row["FILEPATH"].ToString();
                parentID = row["ID"].ToString();
            }
        }
        catch (Exception msg)
        {
            //logExpert.writOperateLog("专家系统添加文件,从数据库中读取信息时出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
        }
        if (directoryPath == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据库中没有目录路径！');", true);
            //Response.Write("<script>alert('数据库中没有目录路径！');</script>");
            return;
        }
        else
        {

            string guid = TextBox_Catalog.Text.Trim();// Guid.NewGuid().ToString();
            if (Directory.Exists(filedirectory + directoryPath))
            {
                string newPath = directoryPath + "\\" + guid;

                //判断是否有同名的文件夹
                if (!this.IsSameFolder(filedirectory + newPath))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已存在该文件夹');", true);
          
                    //Response.Write("<script>alert('已存在该文件夹')</script>");
                    return;
                }

                Directory.CreateDirectory(filedirectory + newPath);
                //添加到数据库

                string strCommand = String.Format("insert into t_directory(NAME,PARENTID,FILEPATH,SORT,TYPE) values('{0}','{1}','{2}','{3}','{4}')"
                                         , this.TextBox_Catalog.Text, parentID, newPath, "", "");

                try
                {
                    MyDataOp dbdo = new MyDataOp(strCommand);
                    if (dbdo.ExecuteCommand())
                    {
                        AutoTree(TreeView1);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('添加目录成功!');", true);
                       // Response.Write("<script>alert('添加目录成功！');window.location.href='ExpertTree.aspx';</script>");
                        return;
                    }
                    else
                    {
                       
                        try
                        {
                            Directory.Delete(filedirectory + directoryPath, true);
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('添加目录失败!删除目录时出错!');", true);

                            //Response.Write("<script>alert('删除目录时出错！');</script>");
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('添加目录失败!');", true);
                        return;
                    }
                }
                catch (Exception msg)
                {
                    //logExpert.writOperateLog("专家系统添加文件,保存文件路径等信息到数据库出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

                }
                finally
                {

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('服务器上没有该目录!');", true);
               // Response.Write("<script>alert('服务器上没有该目录！');</script>");
                return;
            }
           
        }

    }
    #endregion

    //待修改,删除该目录的子目录及文件
    private void DeleteChildNode(string ID, ref string strIDlist)
    {

        DataSet dataSet = new DataSet();

        string str = "select * from t_directory where PARENTID =" + "'" + ID + "'";

        try
        {//异常捕捉，并记错误日志。修改人：WQ
            dataSet = new MyDataOp(str).CreateDataSet();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                strIDlist += "','" + row["ID"].ToString();
                this.DeleteChildNode(row["ID"].ToString(), ref strIDlist);
            }
        }
        catch (Exception msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('目录加载错误!');", true);
            //Response.Write("<script language='javascript'>alert('目录加载错误！');</script>");
        }
        finally
        {
            if (dataSet != null)
            {
                dataSet.Dispose();
                dataSet = null;
            }
           
        }
    }
    //删除目录
    protected void Button3_Click(object sender, EventArgs e)
    {

        if (this.TreeView1.SelectedNode == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择要删除的目录或文件!');", true);
            //Response.Write("<script>window.alert('请选择要删除的目录或文件！')</script>");
            return;
        }

        string name = this.TreeView1.SelectedNode.Value;
        string fileName = this.TreeView1.SelectedNode.Value;

        string directoryPath = "";
        string parentID = "";

        DataSet ds = new DataSet();
        string path = this.ReturnPath(this.TreeView1.SelectedNode, "");
        try
        {
            string str = String.Format("select * from t_directory where id='{0}' and parentid!=''", fileName);
            ds = new MyDataOp(str).CreateDataSet();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                directoryPath = row["FILEPATH"].ToString();
                parentID = row["ID"].ToString();
            }
        }
        catch (Exception msg)
        {
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }
        if (directoryPath == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('你所操作的目录为根目录或者数据库中不存在该目录!');", true);
            //Response.Write("<script>alert('你所操作的目录为根目录或者数据库中不存在该目录！');</script>");
            return;
        }
        else
        {
            try
            {
                Directory.Delete(filedirectory + directoryPath, true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除目录时出错!');", true);
           
                //Response.Write("<script>alert('删除目录时出错！');</script>");
                return;
            }

            // 删除数据库中的信息
            string IDlist = fileName;
            DeleteChildNode(fileName, ref IDlist);
            //删除数据库中的信息
            string[] strList = new string[3];

            string delstr0 = String.Format("delete from t_directory where ID='{0}'", fileName);
            string delstr = String.Format("delete from t_directory where PARENTID  in ('{0}')", IDlist);
            string delstr1 = String.Format("delete from t_document where PARENTID  in ('{0}')", IDlist);
            strList.SetValue(delstr0, 0);
            strList.SetValue(delstr, 1);
            strList.SetValue(delstr1, 2);
            MyDataOp dbdo = new MyDataOp(delstr);
            try
            {
                if (dbdo.DoTran(3, strList))
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除目录成功!');", true);
           
                
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除目录失败!');", true);
                  
                }
            }
            catch (Exception msg)
            {
                //logExpert.writOperateLog("专家系统删除目录出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

            }
            finally
            {
                Query("", "");
                AutoTree(TreeView1);
            }


        }




    }

    private bool IsSameFolder(string path)
    {
        if (Directory.Exists(path))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private string ReturnPath(TreeNode node, string path)
    {
        string currnet = node.Text;
        if (node.Parent != null)
        {
            TreeNode parentNode = node.Parent;
            path = "\\" + currnet + path;
            path = this.ReturnPath(parentNode, path);
            return path;
        }
        else
        {
            path = "\\" + currnet + path;
            return path;
        }
    }

    protected void grdvw_List_SelectedIndexChanged(object sender, EventArgs e)
    {
        string url = grdvw_List.Rows[grdvw_List.SelectedIndex].Cells[12].Text.Trim();
        url = url.Replace("\\", "/");
        int i = url.IndexOf("/文档管理");
        Uri uri = Request.Url;
        string str = "/file";
        url = "http://" + uri.Authority.ToString() + str + url.Substring(i);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('" + url + "','文档','')", true);

    }
}
