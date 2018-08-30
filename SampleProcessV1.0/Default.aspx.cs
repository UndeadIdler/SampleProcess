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
using System.Net;

using System.Timers;
using WebApp.Components;

public partial class Content_Default : System.Web.UI.Page
{
    public string extensionNum;
    public string userID;
    public string serverIP;
    public string num;
    //static double interval = 6000;
    //public System.Timers.Timer FindNetMessage = new System.Timers.Timer(interval);
    


    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.Cookies["Cookies"].Values["User"] != null)
        //{
        //    if (!this.IsPostBack)
        //    {

                
        //            //OracleConnection cnn = new OracleConnection();
        //            //string connectionStr = ConfigurationManager.ConnectionStrings["CallCenterConnectionString"].ConnectionString;
        //            //cnn.ConnectionString = connectionStr;
        //            ////加载菜单 
        //        string commandString = "select t_R_RoleMenu.MenuID,MenuName,URL,MenuPosition from t_R_Role,t_R_Menu,t_R_RoleMenu " +
        //" where t_R_Role.RoleID=t_R_RoleMenu.RoleID and " +
        //" t_R_RoleMenu.MenuID=t_R_Menu.ID and " +
        //" t_R_Role.RoleID='" + Request.Cookies["Cookies"].Values["u_role"].ToString() + "' and " +
        //"t_R_Menu.FatherID='0' and t_R_Menu.flag=1 and t_R_RoleMenu.checked='1' order by t_R_Menu.OrderID";
        //            //OracleDataAdapter dataAdapter = new OracleDataAdapter(commandString, cnn);
        //            //DataSet ds = new DataSet();
        //            DataSet ds = new MyDataOp(commandString).CreateDataSet();
        //            try
        //            {
                       
        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    if (dr["FatherID"].ToString() == "" && dr["MenuID"].ToString() == "0")
        //                    {
        //                        MenuItem root = new MenuItem(dr["MenuName"].ToString(), dr["MenuID"].ToString());
        //                        root.NavigateUrl = dr["URL"].ToString();
        //                        root.Target = dr["MenuPosition"].ToString();
        //                        this.Menu1.Items.Add(root);
        //                    }

        //                    if (dr["FatherID"].ToString() == "" && dr["MenuID"].ToString() == "1")
        //                    {
        //                        MenuItem root = new MenuItem(dr["MenuID"].ToString(), dr["MenuID"].ToString());
        //                        root.NavigateUrl = dr["URL"].ToString();
        //                        root.Target = dr["MenuPosition"].ToString();
        //                        this.Menu2.Items.Add(root);
        //                    }

        //                    if (dr["FatherID"].ToString() == "" && dr["MenuID"].ToString() == "2")
        //                    {
        //                        MenuItem root = new MenuItem(dr["MenuID"].ToString(), dr["MenuID"].ToString());
        //                        root.NavigateUrl = dr["URL"].ToString();
        //                        root.Target = dr["MenuPosition"].ToString();
        //                        this.Menu3.Items.Add(root);
        //                    }

        //                    if (dr["FatherID"].ToString() == "" && dr["MenuID"].ToString() == "3")
        //                    {
        //                        MenuItem root = new MenuItem(dr["MenuID"].ToString(), dr["MenuID"].ToString());
        //                        root.NavigateUrl = dr["URL"].ToString();
        //                        root.Target = dr["MenuPosition"].ToString();
        //                        this.Menu4.Items.Add(root);
        //                    }

        //                    if (dr["FatherID"].ToString() == "" && dr["MenuID"].ToString() == "4")
        //                    {
        //                        MenuItem root = new MenuItem(dr["MenuID"].ToString(), dr["MenuID"].ToString());
        //                        root.NavigateUrl = dr["URL"].ToString();
        //                        root.Target = dr["MenuPosition"].ToString();
        //                        this.Menu5.Items.Add(root);
        //                    }

        //                    //加载地图菜单
        //                    if (dr["FatherID"].ToString() == "" && dr["MenuID"].ToString() == "58")
        //                    {
        //                        MenuItem root = new MenuItem(dr["MenuID"].ToString(), dr["MenuID"].ToString());
        //                        root.NavigateUrl = dr["URL"].ToString();
        //                        root.Target = dr["MenuPosition"].ToString();
        //                        this.Menu7.Items.Add(root);
        //                    }

        //                    if (dr["FatherID"].ToString() == "" && dr["MenuID"].ToString() == "33")
        //                    {
        //                        MenuItem root = new MenuItem(dr["MenuID"].ToString(), dr["MenuID"].ToString());
        //                        root.NavigateUrl = dr["URL"].ToString();
        //                        root.Target = dr["MenuPosition"].ToString();
        //                        this.Menu6.Items.Add(root);
        //                    }
        //                }
        //                this.FillMenu(ds);

        //            }
        //            catch (Exception msg)
        //            {
                       
        //            }
        //            finally
        //            {
                        
        //            }
               
        //    }
        //}
        //else
        //{
        //    Response.Write("<script language='javascript'>window.location.href=\"../login.aspx\";</script>");

        //}

    }

    
    //private void FillMenu(DataSet ds)
    //{
    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        if (ds.Tables[0].Rows[i]["FatherID"].ToString() == "0")
    //        {
    //            MenuItem child = new MenuItem(ds.Tables[0].Rows[i]["MenuID"].ToString(), ds.Tables[0].Rows[i]["MenuID"].ToString());
    //            child.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
    //            child.Target = ds.Tables[0].Rows[i]["MenuPosition"].ToString();
    //            this.Menu1.Items[0].ChildItems.Add(child);
    //        }

    //        if (ds.Tables[0].Rows[i]["FatherID"].ToString() == "1")
    //        {
    //            MenuItem child = new MenuItem(ds.Tables[0].Rows[i]["MenuID"].ToString(), ds.Tables[0].Rows[i]["MenuID"].ToString());
    //            child.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
    //            child.Target = ds.Tables[0].Rows[i]["MenuPosition"].ToString();
    //            this.Menu2.Items[0].ChildItems.Add(child);
    //        }

    //        if (ds.Tables[0].Rows[i]["FatherID"].ToString() == "2")
    //        {
    //            MenuItem child = new MenuItem(ds.Tables[0].Rows[i]["MenuID"].ToString(), ds.Tables[0].Rows[i]["MenuID"].ToString());
    //            child.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
    //            child.Target = ds.Tables[0].Rows[i]["MenuPosition"].ToString();
    //            this.Menu3.Items[0].ChildItems.Add(child);
    //        }

    //        if (ds.Tables[0].Rows[i]["FatherID"].ToString() == "3")
    //        {
    //            MenuItem child = new MenuItem(ds.Tables[0].Rows[i]["MenuID"].ToString(), ds.Tables[0].Rows[i]["MenuID"].ToString());
    //            child.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
    //            child.Target = ds.Tables[0].Rows[i]["MenuPosition"].ToString();
    //            this.Menu4.Items[0].ChildItems.Add(child);
    //        }

    //        if (ds.Tables[0].Rows[i]["FatherID"].ToString() == "4")
    //        {
    //            MenuItem child = new MenuItem(ds.Tables[0].Rows[i]["MenuID"].ToString(), ds.Tables[0].Rows[i]["MenuID"].ToString());
    //            child.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
    //            child.Target = ds.Tables[0].Rows[i]["MenuPosition"].ToString();
    //            this.Menu5.Items[0].ChildItems.Add(child);
    //        }

    //        if (ds.Tables[0].Rows[i]["FatherID"].ToString() == "58")
    //        {
    //            MenuItem child = new MenuItem(ds.Tables[0].Rows[i]["MenuID"].ToString(), ds.Tables[0].Rows[i]["MenuID"].ToString());
    //            child.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
    //            child.Target = ds.Tables[0].Rows[i]["MenuPosition"].ToString();
    //            this.Menu7.Items[0].ChildItems.Add(child);
    //        }

    //        if (ds.Tables[0].Rows[i]["FatherID"].ToString() == "33")
    //        {
    //            MenuItem child = new MenuItem(ds.Tables[0].Rows[i]["MenuID"].ToString(), ds.Tables[0].Rows[i]["MenuID"].ToString());
    //            child.NavigateUrl = ds.Tables[0].Rows[i]["URL"].ToString();
    //            child.Target = ds.Tables[0].Rows[i]["MenuPosition"].ToString();
    //            this.Menu6.Items[0].ChildItems.Add(child);
    //        }
    //    }
    //}

    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{
    //    Page.RegisterStartupScript("LogOut", "<script>myExit();</script>");
    //}

    //protected string check(string Num)
    //{
    //    int temp = Int32.Parse(Num);
    //    if (temp < 10)
    //        return "000" + Num;
    //    else if (temp > 9 && temp < 100)
    //        return "00" + Num;
    //    else if (temp > 99 && temp < 1000)
    //        return "0" + Num;
    //    else
    //        return Num;
    //}

    ////打开窗口如果放到这里操作就会导致页面刷新，从而系统自动退出
    //protected void Menu7_MenuItemClick(object sender, MenuEventArgs e)
    //{
    //    Response.Write("<script language='javascript'>window.open('http://www.sina.com.cn', '新窗口', 'fullscreen=no,scrollbars=yes,resizable=yes,top=5,left=5,width=1000,height=680,toolbar=no,directories=no,menubar=yes,location=yes,status=yes');</script>");
    //}
}
