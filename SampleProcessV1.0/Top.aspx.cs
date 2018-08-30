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

public partial class Top : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["Cookies"] != null)
        {

            lbl_nowuser.Text = "当前用户："+ HttpUtility.UrlDecode(Request.Cookies["Cookies"].Values["Name"].ToString());//)";" + Request.Cookies["Cookies"].Values["u_id"].ToString();// +"(" 
        }
        else
        {
            Response.Write("<script language='javascript'>window.parent.location.href=\"login.aspx\";</script>");

        }
    }
    
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.Cookies["Cookies"] != null)
        {
            HttpCookie mycookies = new HttpCookie("Cookies");
            mycookies["u_id"] = null;
            mycookies.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(mycookies);
            SSOHelper.GlobalSessionEnd();
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.Cookies["Cookies"] != null)
        {
            HttpCookie mycookies = new HttpCookie("Cookies");
            mycookies["u_id"] = null;
            mycookies.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(mycookies);
            SSOHelper.GlobalSessionEnd();
        }
    }
}
