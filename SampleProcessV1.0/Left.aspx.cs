using System;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebApp.Components;
public partial class Left : System.Web.UI.Page
{
    static int pageflag=1;
    string fileName = "";
     string fileId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["Cookies"] != null)
        {

            AutoMenu(TreeView1);
            AutoTree(TreeView2);
            if (pageflag == 1)
            {
                panel_sample.Visible = true;
                panel_file.Visible=false;
            }
            else
            {
                panel_sample.Visible = false;
                panel_file.Visible = true;
            }

        }
    }
#region 文件目录
		 
	
    private void AutoTree(TreeView tree)
    {

        DataSet ds = new DataSet();
        string sqlStr = "select * from t_document where PARENTID is null or PARENTID = ''";

        try
        {
            ds = new MyDataOp(sqlStr).CreateDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode parent = new TreeNode();
                parent.Text = dr["NAME"].ToString();
                tree.Nodes.Add(parent);
                this.AddChildNode(parent, dr["ID"].ToString());

            }
        }
        catch (Exception msg)
        {   //修改：异常捕捉，并对异常记日志。修改人：WQ
            // logExpert.writOperateLog("专家系统目录树的加载" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");
            Response.Write("<script language='javascript'>alert('文件系统加载出错!');</script>");
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

        string str = "select * from t_document where PARENTID =" + "'" + ID + "'";

        try
        {//异常捕捉，并记错误日志。修改人：WQ
            dataSet = new MyDataOp(str).CreateDataSet();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TreeNode child = new TreeNode();
                child.Text = row["NAME"].ToString();
                parent.ChildNodes.Add(child);
                this.AddChildNode(child, row["ID"].ToString());
                string strContent = row["LINK"].ToString();
                //Regex urlregex = new Regex(@"(http:\/\/([\w.]+\/?)\S*)",
                //                 RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //strContent = urlregex.Replace(strContent,
                //             "<a href=\"\" target=\"_blank\">" + child.Text + "</a>");
            }
        }
        catch (Exception msg)
        {
            //logExpert.writOperateLog("专家系统加载目录" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");
            Response.Write("<script language='javascript'>alert('目录加载错误！');</script>");
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

    //根据所选的TreeNode节点加载文件，并将其读出
    protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    {
        pageflag = 2;
        string name = this.TreeView2.SelectedNode.Text.Trim();
        int point = 0;
        point = name.LastIndexOf('.');
        
        this.TreeView2.SelectedNode.Expand();
        if (point > 0)
        {
            DataSet ds = new DataSet();
            string str = "select * from t_document where NAME =" + "'" + name + "'";
           
            try
            {//异常捕捉，并记错误日志。修改人：WQ
                ds = new MyDataOp(str).CreateDataSet();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    fileName = row["FILEPATH"].ToString();
                    fileId = row["ID"].ToString();
                }
            }

            catch (Exception msg)
            {
                //logExpert.writOperateLog("专家系统点击相应目录显示相应节点中路径的获取" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;

                }
               
            }
            if (fileName == "")
            {
                Response.Write("<script>alert('数据库中没有文件路径！');</script>");
                return;
            }
            else
            {
                if (!File.Exists(fileName))
                {
                    int i = fileName.LastIndexOf('\\');
                    int j = fileName.LastIndexOf('.');
                    string message = string.Format("{0} 文件不存在！", fileName.Substring(i + 1, j - i - 1));
                    Response.Write("<script>alert('" + message + "');</script>");
                    return;
                }
                else
                {
                    int j = fileName.LastIndexOf('.');
                    string typeStr = fileName.Substring(j, 4);
                    switch (typeStr)
                    {
                        case ".txt":
                            // Response.Write(String.Format("<script>window.location='ExpertDetail.aspx?Option={0}';</script>", fileId));

                          
                            ////Encoding encoder = Encoding.GetEncoding("gb2312");  //定义编码方式
                            ////StreamReader read = new StreamReader(fileName, encoder);//定义StreamReader并用指定编码读取fileName中的内容
                            ////this.TextBox_Content.Text = doText.GetStr(read.ReadToEnd());//将读取的字符流用textbox显示
                            break;

                        default:
                            try
                            {
                                //string link1 = fileName.Substring(22);
                                //link1 = link1.Replace("\\", "/");
                                //openname = link1;
                                ////Stream readerDoc=Server.CreateObject("ADODB.Stream");
                                ////readerDoc.Read(
                                //Page.RegisterStartupScript("Link1", "<script language='javascript'>window.open('" + link1 + "');</script>");




                            }
                            catch (Exception msg)
                            {//修改：错误记日志。修改人：WQ
                                //logExpert.writOperateLog("专家系统点击相应目录显示相应节点内容，读取文件出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

                                Response.Write("<script>alert('读取文件时出错！');</script>");
                                return;
                            }
                            break;

                    }
                }
            }
        }
    }
     //添加目录
    protected void Button2_Click(object sender, EventArgs e)
    {
       

        if (this.TreeView1.SelectedNode == null)
        {
            Response.Write("<script>window.alert('请选择要添加到哪个目录！')</script>");
            return;
        }

        if(this.TextBox_Catalog.Text=="")
        {
            Response.Write("<script>window.alert('目录名不能为空！')</script>");
            return;
        }

        string name = this.TreeView1.SelectedNode.Text;
        int point = 0;
        point = name.LastIndexOf('.');
        //所选的是文件
        if(point >0)
        {
            string directoryPath="";
            string parentID = "";

            DataSet ds = new DataSet();
            string path= this.ReturnPath(this.TreeView1.SelectedNode.Parent,"");
           
            string str = String.Format("select * from t_document where FILEPATH like '%{0}'",path);
           
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
                //logExpert.writOperateLog("专家系统添加目录,保存实际目录路径时出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

            }
            finally
             {   if(ds!=null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
            if (directoryPath == "")
            {
                Response.Write("<script>alert('数据库中没有目录路径！');</script>");
                return;
            }
            else
            {
                if(Directory.Exists(directoryPath))
                {
                    string newPath = directoryPath+"\\"+this.TextBox_Catalog.Text;

                    //判断是否有同名的文件夹
                    if(!this.IsSameFolder(newPath))
                    {
                        Response.Write("<script>alert('已存在该文件夹')</script>");
                        return;
                    }

                    Directory.CreateDirectory(newPath);
                    //添加到数据库
                    string strCommand = String.Format("insert into t_document(ID,NAME,PARENTID,FILEPATH,SORT,TYPE,KEYWORDS) values(expertinfor_id.nextval,'{0}',{1},'{2}','{3}','{4}','{5}')"
                                             , this.TextBox_Catalog.Text, parentID, newPath, "", "", "", "");
                    MyDataOp dbdo = new MyDataOp(strCommand);

                    int i=0;
                    try
                    {
                        if (dbdo.ExecuteCommand())
                        { }
                        else
                        { };
                    }
                    catch (Exception msg)
                    {
                       // logExpert.writOperateLog("专家系统添加目录,保存目录路径等信息到数据库出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

                    }
                    finally
                    {
                       
                    }
                    if (i > 0)
                    {
                       
                        Response.Write("<script>alert('添加目录成功！');window.location.href='left.aspx';</script>");
                        return;
                    }
                    else
                    {

                        Response.Write("<script>alert('添加目录失败！');window.location.href='left.aspx';</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('服务器上没有该目录！');</script>");
                    return;
                }
            }
        }
        else//选的是文件夹
        {
            string directoryPath = "";
            string parentID = "";

            DataSet ds = new DataSet();
            string path = this.ReturnPath(this.TreeView1.SelectedNode, "");
            
            string str = String.Format("select * from t_document where FILEPATH like '%{0}'",path);
            
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
                }            }
            if (directoryPath == "")
            {
                Response.Write("<script>alert('数据库中没有目录路径！');</script>");
                return;
            }
            else
            {
                if (Directory.Exists(directoryPath))
                {
                    string newPath = directoryPath + "\\" + this.TextBox_Catalog.Text;

                    //判断是否有同名的文件夹
                    if (!this.IsSameFolder(newPath))
                    {
                        Response.Write("<script>alert('已存在该文件夹')</script>");
                        return;
                    }

                    Directory.CreateDirectory(newPath);
                    //添加到数据库
                    
                    string strCommand = String.Format("insert into t_document(NAME,PARENTID,FILEPATH,SORT,TYPE,KEYWORDS) values('{0}',{1},'{2}','{3}','{4}','{5}')"
                                             , this.TextBox_Catalog.Text, parentID, newPath, "", "", "", "");
                   
                    try
                    {
                        MyDataOp dbdo = new MyDataOp(strCommand);
                        if (dbdo.ExecuteCommand())
                        {
                            Response.Write("<script>alert('添加目录成功！');window.location.href='ExpertTree.aspx';</script>");
                            return;
                        }
                        else
                        {
                            Response.Write("<script>alert('添加目录失败！');window.location.href='ExpertTree.aspx';</script>");
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
                    Response.Write("<script>alert('服务器上没有该目录！');</script>");
                    return;
                }
            }
        }
    }


    //删除目录
    protected void Button3_Click(object sender, EventArgs e)
    {
       
        if (this.TreeView1.SelectedNode == null)
        {
            Response.Write("<script>window.alert('请选择要删除的目录或文件！')</script>");
            return;
        }

        string name = this.TreeView1.SelectedNode.Text;
        int point = 0;
        point = name.LastIndexOf('.');

        if (point > 0)//删除文件 (先删除文件再删除数据库中的信息)
        {
            string filePath = "";
            string truePath = "";
            string fileName = this.TreeView1.SelectedNode.Text;
          
            DataSet ds = new DataSet();
            string path = this.ReturnPath(this.TreeView1.SelectedNode.Parent, "");

            string str = String.Format("select * from t_document where FILEPATH like '%{0}'", path);
          

            try
            {
                ds = new MyDataOp(str).CreateDataSet();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    filePath = row["FILEPATH"].ToString();
                }
            }
            catch (Exception msg)
            {
                //logExpert.writOperateLog("专家系统删除文件,从数据库中读取信息时出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                
            }
            if (filePath == "")
            {
                Response.Write("<script>alert('数据库中没有文件路径！');</script>");
                return;
            }
            else
            {
                try
                {
                    truePath = filePath + "\\" + fileName;
                    File.Delete(truePath);
                }
                catch (Exception msg)
                {
                   // logExpert.writOperateLog("删除实际文件时出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "38");
                    Response.Write("<script>alert('删除文件时出错！');</script>");
                    return;
                }

                //删除数据库中的信息
                string delstr = String.Format("delete from t_document where FILEPATH = '{0}'", truePath);
               MyDataOp dbdo = new MyDataOp(delstr);
                int i = 0;
                try
                {
                    if(dbdo.ExecuteCommand())
                    {
                         Response.Write("<script>alert('删除文件成功！');window.location.href='ExpertTree.aspx';</script>");
                    return;
                }
                else
                {
                    Response.Write("<script>alert('删除文件失败！');window.location.href='ExpertTree.aspx';</script>");
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
        else//删除目录　　(先删除目录再删除数据库中的信息)
        {
            string directoryPath = "";
            string parentID = "";

            DataSet ds = new DataSet();
            string path = this.ReturnPath(this.TreeView1.SelectedNode, "");
            string str = String.Format("select * from t_document where FILEPATH like '%{0}'", path);
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
               // logExpert.writOperateLog("专家系统删除目录出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

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
                Response.Write("<script>alert('数据库中没有目录路径！');</script>");
                return;
            }
            else
            {
                try
                {
                    Directory.Delete(directoryPath, true);
                }
                catch
                {
                    Response.Write("<script>alert('删除目录时出错！');</script>");
                    return;
                }

               // 删除数据库中的信息
                string delstr = String.Format("delete from t_document where FILEPATH like '{0}%'", directoryPath);
               MyDataOp dbdo = new MyDataOp(delstr);
                try
                {
                    if (dbdo.ExecuteCommand())
                    {

                        Response.Write("<script>alert('删除目录成功！');window.location.href='ExpertTree.aspx';</script>");
                        return;
                    }
                    else
                    {
                        Response.Write("<script>alert('删除目录失败！');window.location.href='ExpertTree.aspx';</script>");
                        return;
                    }
                }
                catch (Exception msg)
                {
                    //logExpert.writOperateLog("专家系统删除目录出错" + msg.Message, Session.SessionID, Request.Cookies["Cookies"].Values["UserID"].ToString(), "0001");

                }
                finally
                {
                  
                }  

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

    private string ReturnPath(TreeNode node,string path)
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
            path = "\\"+currnet+path;
            return path;
        }
    }
#endregion

    //根据所选的TreeNode节
    private void AutoMenu(TreeView trvMenu)
    {
        trvMenu.Nodes.Clear();
        trvMenu.Font.Name="宋体";
        trvMenu.Font.Size=FontUnit.Parse("10");

        string strSql = "select t_R_RoleMenu.MenuID,MenuName,URL from t_R_Role,t_R_Menu,t_R_RoleMenu " +
            " where t_R_Role.RoleID=t_R_RoleMenu.RoleID and " +
            " t_R_RoleMenu.MenuID=t_R_Menu.ID and " +
            " t_R_Role.RoleID='" + Request.Cookies["Cookies"].Values["u_role"].ToString() + "' and " +
            "t_R_Menu.FatherID='0' and t_R_Menu.flag>=1 and t_R_RoleMenu.checked='1' order by t_R_Menu.OrderID";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        for(int i=0;i<ds.Tables[0].Rows.Count;i++)
        {
            TreeNode tnFather=new TreeNode();
            tnFather.Text = ds.Tables[0].Rows[i]["MenuName"].ToString();
            tnFather.NavigateUrl=ds.Tables[0].Rows[i]["URL"].ToString();
            if (tnFather.NavigateUrl=="#")
            tnFather.Expanded=true;
            else
                tnFather.Expanded = true;
            trvMenu.Nodes.Add(tnFather);
            CreateChildNodes(tnFather, Int16.Parse(ds.Tables[0].Rows[i]["MenuID"].ToString()));
        }
        ds.Dispose();
    }

    private void CreateChildNodes(TreeNode tnParentNode,int NodeId)
    {
        string strSql = "select t_R_RoleMenu.MenuID,MenuName,URL from t_R_Role,t_R_Menu,t_R_RoleMenu " +
            " where t_R_Role.RoleID=t_R_RoleMenu.RoleID and " +
            " t_R_RoleMenu.MenuID=t_R_Menu.ID and " +
            " t_R_Role.RoleID='" + Request.Cookies["Cookies"].Values["u_role"].ToString() + "' and " +
            "t_R_Menu.FatherID='" + NodeId + "' and t_R_Menu.flag>=1 and t_R_RoleMenu.checked='1' order by t_R_Menu.OrderID";
       
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode tnChild = new TreeNode();
            tnChild.Text = ds.Tables[0].Rows[i]["MenuName"].ToString();
            tnChild.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
            tnChild.Expanded = true;
            tnParentNode.ChildNodes.Add(tnChild);
            CreateChildNodes(tnChild, Int16.Parse(ds.Tables[0].Rows[i]["MenuID"].ToString()));
        }
        ds.Dispose();
    }
    public void Button_Command(Object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Expand":
                if (panel_sample.Visible == true)
                {
                    TreeView1.ExpandAll();
                }
                else
                {
                    TreeView2.ExpandAll();
                }

                break;
            case "Collapse":
                if (panel_sample.Visible == true)
                {
                    TreeView1.CollapseAll();
                }
                else
                    TreeView2.CollapseAll();
                break;
            default:
                // Do nothing.
                break;

        }
    }
   

    private void AutoCarMenu(TreeView trvMenu)
    {
        trvMenu.Nodes.Clear();
        trvMenu.Font.Name = "宋体";
        trvMenu.Font.Size = FontUnit.Parse("10");

        string strSql = "select t_R_RoleMenu.MenuID,MenuName,URL from t_R_Role,t_R_Menu,t_R_RoleMenu " +
            " where t_R_Role.RoleID=t_R_RoleMenu.RoleID and " +
            " t_R_RoleMenu.MenuID=t_R_Menu.ID and " +
            " t_R_Role.RoleID='" + Request.Cookies["Cookies"].Values["u_role"].ToString() + "' and " +
            "t_R_Menu.FatherID='0' and t_R_Menu.flag>=1 and t_R_RoleMenu.checked='1' and t_R_Menu.id=63 order by t_R_Menu.OrderID";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode tnFather = new TreeNode();
            tnFather.Text = ds.Tables[0].Rows[i]["MenuName"].ToString();
            tnFather.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
            if (tnFather.NavigateUrl == "#")
                tnFather.Expanded = true;
            else
                tnFather.Expanded = true;
            trvMenu.Nodes.Add(tnFather);
            CreateCarChildNodes(tnFather, Int16.Parse(ds.Tables[0].Rows[i]["MenuID"].ToString()));
        }
        ds.Dispose();
    }
    private void CreateCarChildNodes(TreeNode tnParentNode, int NodeId)
    {
        BLL.Car.Car car=new BLL.Car.Car();
        DataSet ds = car.Query("","");
      

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode tnChild = new TreeNode();
            tnChild.Text = ds.Tables[0].Rows[i]["carid"].ToString();
            tnChild.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString() + "?key='" + ds.Tables[0].Rows[i]["id"].ToString() + "'";
            tnChild.Expanded = true;
            tnParentNode.ChildNodes.Add(tnChild);
            CreateChildNodes(tnChild, Int16.Parse(ds.Tables[0].Rows[i]["id"].ToString()));
        }
        ds.Dispose();
    }
    protected void lb_btn_sample_Click(object sender, EventArgs e)
    {
        pageflag = 1;
        panel_file.Visible = false;
        panel_sample.Visible = true;
    }
    protected void lb_tbn_file_Click(object sender, EventArgs e)
    {
        pageflag = 2;
        panel_file.Visible =true ;
        panel_sample.Visible = false;
       // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "document", "<script language='javascript'>alert(window.parent.mainFrame);</script>", true);//.location.href='ExpertTree.aspx'
    }
}
