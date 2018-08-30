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

public partial class Right_UserTree : System.Web.UI.Page
{
    string FunctionID = "";
    string NoFunctionID = "";
    private string Option//所选择操作列记录对应的id
    {
        get { return (string)ViewState["Option"]; }
        set { ViewState["Option"] = value; }
    }
    private string RoleID//所选择操作列记录对应的id
    {
        get { return (string)ViewState["RoleID"]; }
        set { ViewState["RoleID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {


            Option = Request.Params["kw"].ToString();
            DataSet ds_rolename = new MyDataOp("select Name,RoleID from  t_R_UserInfo where id='" +Option + "'").CreateDataSet();
            Label1.Text = ds_rolename.Tables[0].Rows[0]["Name"].ToString() + "的可写权限配置";
            RoleID = ds_rolename.Tables[0].Rows[0][1].ToString();
            string sqlStr = "select t_R_Menu.* from t_R_Menu inner join t_R_RoleMenu on t_R_RoleMenu.MenuID=t_R_Menu.ID where FatherID ='0' and RoleID='" + RoleID + "' order by FatherID ASC";


            DataSet ds = new MyDataOp(sqlStr).CreateDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode parent = new TreeNode();
                parent.Text = dr["MenuName"].ToString();
                parent.Value = dr["ID"].ToString();
                this.TreeView1.Nodes.Add(parent);
                this.AddChildNode(parent, dr["ID"].ToString());
            }

            this.SelectFunctionID(Option);
        }
    }
    protected void SetButton()
    {
        if (MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            Button1.Enabled =true ;
           
        }
        else
        {
            Button1.Enabled = false;
           
        }
    }
    private void SelectFunctionID(string roleID)
    {
        try
        {
            //string commandStr = String.Format("select * from t_R_RoleMenu where RoleID='{0}' and checked='1'", roleID);
            string commandStr = String.Format("select * from t_R_User_IsWrite where userid='{0}' and iswrite='1'", roleID);
            DataSet ds = new MyDataOp(commandStr).CreateDataSet();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.SelectTrue(this.TreeView1.Nodes, ds.Tables[0].Rows[i]["MenuID"].ToString());
            }
        }
        catch
        {
            Response.Write("<script>alert('操作数据库失败！');</script>");
            return;
        }
    }

    private void SelectTrue(TreeNodeCollection nodes ,string functionID)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Value == functionID)
            {
                nodes[i].Checked = true;
                return;
            }
            else
            {
                if (nodes[i].ChildNodes.Count != 0)
                {
                    this.SelectTrue(nodes[i].ChildNodes, functionID);
                }
            }
        }
    }

    private void AddChildNode(TreeNode parent, string ID)
    {
       
        //string str = String.Format("select * from t_R_Menu where FatherID ='{0}' order by id ASC", ID);
        string str = String.Format("select t_R_Menu.* from t_R_Menu inner join t_R_RoleMenu on t_R_RoleMenu.MenuID=t_R_Menu.ID where FatherID ='{0}' and RoleID='" + RoleID + "' order by t_R_Menu.OrderID ASC", ID);
        DataSet ds = new MyDataOp(str).CreateDataSet();
       

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode child = new TreeNode();
            child.Text = dr["MenuName"].ToString();
            child.Value = dr["ID"].ToString();
            parent.ChildNodes.Add(child);
            this.AddChildNode(child, dr["ID"].ToString());
        }
    }

    private void NodeCheck(TreeNodeCollection nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Checked == true)
            {
                FunctionID = FunctionID + "'" + nodes[i].Value + "',";
            }
            else
            {
                NoFunctionID = NoFunctionID + "'" + nodes[i].Value + "',";
            }
            if (nodes[i].ChildNodes.Count != 0)
            {
                this.NodeCheck(nodes[i].ChildNodes);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //string sql_sel = "select count(*) from t_R_User_IsWrite where userid = " + Option;
        //DataSet ds_sel = new MyDataOp(sql_sel).CreateDataSet();
        //if (ds_sel.Tables[0].Rows[0][0].ToString() == "0")
        //{

        string sql = "select t_R_Menu.id from  t_R_Menu inner join t_R_RoleMenu on t_R_RoleMenu.MenuID=t_R_Menu.ID  where RoleID='" + RoleID + "' order by t_R_Menu.OrderID";
            DataSet ds = new MyDataOp(sql).CreateDataSet();
            string[] list = new string[ds.Tables[0].Rows.Count+1];
            list.SetValue("Delete from t_R_User_IsWrite where userid='"+Option+ "'", 0);
          
            for (int j = 0, lenth = ds.Tables[0].Rows.Count; j < lenth; j++)
            {
                list.SetValue("insert into t_R_User_IsWrite(userid,menuid,isWrite) values(" + Option + "," + ds.Tables[0].Rows[j][0].ToString() + "," + 0 + ")", j+1);
            }
            string sqlstr = "";
            MyDataOp mdo = new MyDataOp(sqlstr);
            mdo.DoTran(ds.Tables[0].Rows.Count, list);
        //}

            string[] strlist = new string[2];
        int i = 0;
        this.NodeCheck(this.TreeView1.Nodes);
        if (FunctionID != "")
        {
            i++;
            FunctionID = FunctionID.Substring(0, FunctionID.Length - 1);
            strlist.SetValue(String.Format("update t_R_User_IsWrite set isWrite='1' where menuid in({0}) and userid='{1}'", FunctionID, Option), 0);
        }
        if (NoFunctionID != "")
        {
            i++;
            NoFunctionID = NoFunctionID.Substring(0, NoFunctionID.Length - 1);
            strlist.SetValue(String.Format("update t_R_User_IsWrite set isWrite='0' where menuid in({0}) and userid='{1}'", NoFunctionID, Option), 1);
        }

        bool flag = true;
        for (int j = 0; j < strlist.Length; j++)
        {
            if (strlist[j] != null && strlist[j] != "")
            {
                MyDataOp cmd = new MyDataOp(strlist[j]);
                flag = cmd.ExecuteCommand() && flag;
            }
        }

        if (flag == true)
        {
            Response.Write("<script>alert('保存成功！');window.close();</script>");
        }
        else
        {
            Response.Write("<script>alert('保存失败！');</script>");
        }
       // string[] strlist = new string[2];
       // int i = 0;
       // this.NodeCheck(this.TreeView1.Nodes);
       // if (FunctionID != "")
       // {
       //     i++;
       //     FunctionID = FunctionID.Substring(0, FunctionID.Length - 1);
       //     strlist.SetValue(String.Format("update t_R_RoleMenu set checked='1' where MenuID in({0}) and RoleID='{1}'", FunctionID, Request.Cookies["Cookies"].Values["Option"].ToString()), 0);

       // }
       // if (NoFunctionID != "")
       // {
       //     i++;
       //     NoFunctionID = NoFunctionID.Substring(0, NoFunctionID.Length - 1);
       //     strlist.SetValue(String.Format("update t_R_RoleMenu set checked='0' where MenuID in({0}) and RoleID='{1}'", NoFunctionID, Request.Cookies["Cookies"].Values["Option"].ToString()), 1);

       // }

       
                 

       // string str = "";      
       //MyDataOp cmd=new MyDataOp(str);
       //bool flag= cmd.DoTran(i,strlist);
       // if(flag==true)
       // {
       //             Response.Write("<script>alert('保存成功！');window.close();</script>");
       //  }
       // else
       // {
       //      Response.Write("<script>alert('保存失败！');</script>");
       // }
            
    }

    protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        try
        {
            if (e.Node.ChildNodes.Count == 0)
            {
                if (e.Node.Checked == true)
                {
                    if (e.Node.Parent != null)
                    {
                        e.Node.Parent.Checked = true;
                        this.ChildChecked(e.Node.Parent);
                        return;
                    }
                }
                else
                {
                    if (e.Node.Parent != null)
                    {
                        this.CheckParent(e.Node);
                    }
                }
            }

            if (e.Node.ChildNodes.Count != 0 && e.Node.Parent != null)
            {
                this.ParentChildChecked(e.Node);
                return;
            }

            if (e.Node.ChildNodes.Count != 0)
            {
                this.ParentChecked(e.Node);
                return;
            }
        }
        catch (Exception err)
        {
 
        }
    }

    protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
    {
        this.TreeView1.SelectedNode.Checked = !this.TreeView1.SelectedNode.Checked;

        if(this.TreeView1.SelectedNode.ChildNodes.Count == 0)
        {
            if (this.TreeView1.SelectedNode.Checked == true)
            {
                this.TreeView1.SelectedNode.Parent.Checked = true;
                this.ChildChecked(this.TreeView1.SelectedNode.Parent);
                return;
            }
            else
            {
                if (this.TreeView1.SelectedNode.Parent != null)
                {
                    this.CheckParent(this.TreeView1.SelectedNode);
                }
            }
        }

        if (this.TreeView1.SelectedNode.ChildNodes.Count != 0 && this.TreeView1.SelectedNode.Parent != null)
        {
            this.ParentChildChecked(this.TreeView1.SelectedNode);
            return;
        }

        if (this.TreeView1.SelectedNode.ChildNodes.Count != 0)
        {
            this.ParentChecked(this.TreeView1.SelectedNode);
            return;
        }
    }

    private void ParentChecked(TreeNode node)
    {
        if (node.ChildNodes.Count != 0)
        {
            if (node.Checked == true)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    node.ChildNodes[i].Checked = true;
                    this.ParentChecked(node.ChildNodes[i]);
                }
            }
            else
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    node.ChildNodes[i].Checked = false;
                    this.ParentChecked(node.ChildNodes[i]);
                }
            }
        }
    }

    private void ParentChildChecked(TreeNode node)
    {
        if (node.Parent != null && node.ChildNodes.Count != 0)
        {
            if (node.Checked == true)
            {
                node.Parent.Checked = true;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    node.ChildNodes[i].Checked = true;
                    this.ParentChildChecked(node.ChildNodes[i]);
                }
                this.ParentChildChecked(node.Parent);
            }
            else
            {
                this.CheckParent(node);
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    node.ChildNodes[i].Checked = false;
                    this.ParentChildChecked(node.ChildNodes[i]);
                }
            }
        }
    }

    private void CheckParent(TreeNode node)
    {
        int sum = 0;
        for (int j = 0; j < node.Parent.ChildNodes.Count; j++)
        {
            if (node.Parent.ChildNodes[j].Checked == true)
            {
                sum = 1;
            }
        }
        if (sum > 0)
        {
            node.Parent.Checked = true;
        }
        else
        {
            node.Parent.Checked = false;
            if (node.Parent.Parent != null)
            {
                this.CheckParent(node.Parent);
            }
        }
    }

    private void ChildChecked(TreeNode child)
    {
        if (child.Parent != null)
        {
            child.Parent.Checked = true;
        }
        else
        {
            return;
        }
        this.ChildChecked(child.Parent);
    }
}
