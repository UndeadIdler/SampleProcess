using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;


using WebApp.Components;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_UserName.Focus();
        
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["function"] != null)
            {
                string code = "";
                string codecompare = System.Configuration.ConfigurationManager.AppSettings["LoginCode"].ToString();
                try
                {
                    code = DESEncrypt.Decrypt(Request.QueryString["function"]).ToString();

                }
                catch
                {
                    string encode = DESEncrypt.Encrypt(codecompare);
                }
                if (code == codecompare)
                {
                    string strSql = "select DepartID,UserID,t_R_UserInfo.RoleID,PWDModifyTime,Name,LevelID,ReadRight,WriteRight,RefreshRight,FileRight,id from t_R_UserInfo inner join t_R_Role on t_R_Role.RoleID=t_R_UserInfo.RoleID where UserID='user' and PWD='user'";

                    //
                    // string strSql = "select id,UserID,t_R_UserInfo.RoleID,Name,RealName,PWDModifyTime,LevelID,ReadRight,WriteRight,RefreshRight,id from t_R_UserInfo inner join t_R_Role on t_R_Role.RoleID=t_R_UserInfo.RoleID where UserID='user' and PWD='user'";
                    MyDataOp mdo = new MyDataOp(strSql);
                    DataSet ds = mdo.CreateDataSet();

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        Response.Write("<script language='javascript'>alert('帐户或密码输入错误！请重新输入！');</script>");
                    }
                    else
                    {
                        HttpCookie Cookie = new HttpCookie("Cookies");

                        DateTime dt = DateTime.Now;
                        TimeSpan ts = new TimeSpan(0, 10, 0, 0); //有效期10小时；
                        Cookie.Expires = dt.Add(ts);
                        Cookie.Values.Add("uid", ds.Tables[0].Rows[0]["id"].ToString());
                        Cookie.Values.Add("u_id", ds.Tables[0].Rows[0]["UserID"].ToString());
                        Cookie.Values.Add("Name", HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["Name"].ToString()));
                        // Cookie.Values.Add("RealName", HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["RealName"].ToString()));
                        Cookie.Values.Add("u_role", ds.Tables[0].Rows[0]["RoleID"].ToString());
                        Cookie.Values.Add("u_level", ds.Tables[0].Rows[0]["LevelID"].ToString());
                        Cookie.Values.Add("u_purview", ds.Tables[0].Rows[0]["ReadRight"].ToString() +//读权限
                        ds.Tables[0].Rows[0]["WriteRight"].ToString() +//写权限
                        ds.Tables[0].Rows[0]["RefreshRight"].ToString() +
                        ds.Tables[0].Rows[0]["FileRight"].ToString());//刷新权限;

                        Response.AppendCookie(Cookie);
                        Response.Redirect("~/main.htm");
                    }
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                txt_UserName.Focus();
              
                if (!Page.IsPostBack)
                {
                    if (Request.Cookies["Cookies"] != null)
                    {
                        HttpCookie mycookies = new HttpCookie("Cookies");
                        mycookies["u_id"] = null;
                        mycookies.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(mycookies);
                        txt_Pwd.Text = "";
                        SSOHelper.GlobalSessionEnd();
                    }
                }
            }
        }


    }
    #region  Public static void OpenNewFullScreenPage( Page page, string pageUrl, bool isCloseOldPage, string scriptName )
    /// <summary>
    /// 打开一个全屏页面并关闭当前页面
    /// </summary>
    /// <param name="page">当前页面的指针，一般为this</param>
    /// <param name="pageUrl">新页面的URL</param>
    public static void OpenNewFullScreenPage(Page page, string pageUrl, bool isCloseOldPage, string scriptName)
    {
        StringBuilder StrScript = new StringBuilder();
        StrScript.Append("<script language=javascript>");
        StrScript.Append("width=screen.width-10;");
        StrScript.Append("height=screen.height-69;");
        StrScript.Append("var name=Math.random();");
       // StrScript.Append("alert(name);" + "\n");
        StrScript.Append("name=window.open('" + pageUrl + "','_blank','toolbar=0,location=0,directories=0,menubar=0,scrollbars=1,resizable=0,top=0,depended=0,left=0,height='+ height +',width='+ width +'');");

        if (isCloseOldPage)
        {
           
            StrScript.Append("window.focus();");
            StrScript.Append("if(name==null){alert('您的浏览器禁止弹出窗口，请查看浏览器设置！');}");

           StrScript.Append("else{window.opener=null;");
         
            StrScript.Append("window.open('','_self');");
           StrScript.Append("window.close(); }");
        }
        StrScript.Append("</script>");
        if (!page.IsStartupScriptRegistered(scriptName))
        {
            page.RegisterStartupScript(scriptName, StrScript.ToString());
        }
     
    }
    #endregion
    protected void btn_Login_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_UserName.Text.Trim() != "user")
        {
            if (SSOHelper.CheckOnline(txt_UserName.Text.Trim()))
            {
                string strSql = "select DepartID,UserID,t_R_UserInfo.RoleID,PWDModifyTime,Name,LevelID,ReadRight,WriteRight,RefreshRight,FileRight,id,ManageRight,dataflag from t_R_UserInfo inner join t_R_Role on t_R_Role.RoleID=t_R_UserInfo.RoleID where  t_R_UserInfo.flag=0 and UserID='" + txt_UserName.Text.Trim() + "' and PWD='" + txt_Pwd.Text.Trim() + "'";
                MyDataOp mdo = new MyDataOp(strSql);
                DataSet ds = mdo.CreateDataSet();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    Response.Write("<script language='javascript'>alert('帐户或密码输入错误！请重新输入！');</script>");
                }
                else
                {
                    HttpCookie Cookie = new HttpCookie("Cookies");

                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(0, 10, 0, 0); //有效期10小时；
                    Cookie.Expires = dt.Add(ts);
                    Cookie.Values.Add("uid", ds.Tables[0].Rows[0]["id"].ToString());
                    Cookie.Values.Add("u_id", ds.Tables[0].Rows[0]["UserID"].ToString());
                    Cookie.Values.Add("u_role", ds.Tables[0].Rows[0]["RoleID"].ToString());
                    Cookie.Values.Add("u_flag", ds.Tables[0].Rows[0]["dataflag"].ToString());
                    Cookie.Values.Add("u_pswdtime", ds.Tables[0].Rows[0]["PWDModifyTime"].ToString());
                    Cookie.Values.Add("Name", HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["Name"].ToString()));
                    //Cookie.Values.Add("u_Name",ds.Tables[0].Rows[0]["Name"].ToString());//用户名
                    // Cookie.Values.Add("RealName", HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["RealName"].ToString()));

                    Cookie.Values.Add("u_level", ds.Tables[0].Rows[0]["LevelID"].ToString());
                    Cookie.Values.Add("u_purview", ds.Tables[0].Rows[0]["ReadRight"].ToString() +//读权限
                    ds.Tables[0].Rows[0]["WriteRight"].ToString() +//写权限
                    ds.Tables[0].Rows[0]["ManageRight"].ToString() +
                    ds.Tables[0].Rows[0]["FileRight"].ToString());//刷新权限;
                    Cookie.Values.Add("departid", ds.Tables[0].Rows[0]["DepartID"].ToString());
                    Response.AppendCookie(Cookie);
                    SSOHelper sso = new SSOHelper();
                    sso.LoginRegister(txt_UserName.Text.Trim());

                    Response.Redirect("~/main.htm");

                }

            }
            else
            {
                Response.Write("<script language='javascript'>alert('您的帐户已在别处登陆！');</script>");

            }
        }
       
    }
    protected void btn_Clean_Click(object sender, EventArgs e)
    {
        txt_UserName.Text = "";
        txt_Pwd.Text = "";
    } 
}
