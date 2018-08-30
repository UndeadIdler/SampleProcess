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

public partial class Reports_DefaultA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string strFileName = HttpUtility.UrlDecode(Request.QueryString["file_name"]);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.WriteFile((Server.MapPath(".")) + "\\" + strFileName);
            Response.End();
        }
    }
}
