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
public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Request.Cookies["Cookies"].Values["u_id"] == null)
        {
            Response.Write("<script language='javascript'>alert('您没有权限进入本页或当前登录用户已过期！\\n请重新登录或与管理员联系！');parent.location='../login.aspx';</script>");
        }

        string strPageName = Request.Url.AbsolutePath;
        strPageName = strPageName.Substring(strPageName.LastIndexOf("/") + 1);

        string strSql = "select count(*) from t_R_Role,t_R_RoleMenu,t_R_Menu " +
            "where t_R_Role.RoleID='" + Request.Cookies["Cookies"].Values["u_role"] +
            "' and t_R_Menu.RelativeFile like '%" + strPageName +
            "%' and t_R_Role.RoleID=t_R_RoleMenu.RoleID and t_R_RoleMenu.checked='1' and t_R_RoleMenu.MenuID=t_R_Menu.ID";
        MyDataOp mdo = new MyDataOp(strSql);
        DataSet ds = mdo.CreateDataSet();

        int intRow = Convert.ToUInt16(ds.Tables[0].Rows[0][0].ToString());
        if (intRow == 0)
        {
            Response.Write("<script language='javascript'>alert('您没有权限进入本页！\\n请重新登录或与管理员联系！');history.back();</script>");
        }
    }
}
