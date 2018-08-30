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

public partial class DsoFramer_EditFile : System.Web.UI.Page
{
    public string url;
    public string Serverurl;
    public string filaname;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(3, 1) == "1")
            {
                lbl_type.Text = "文件在线编辑";

            }
            
            url = HttpUtility.HtmlDecode(Request.QueryString["FilePath"].ToString());
             
                int j= url.LastIndexOf("/");
                filaname = url.Substring(j);
            string FileType = url.Remove(0, Request.QueryString["FilePath"].ToString().LastIndexOf('.') + 1);
            if (FileType.ToLower().Trim() == "doc" || FileType.ToLower().Trim() == "xls" || FileType.ToLower().Trim() == "ppt")
            {
                int i = url.IndexOf("/filemanagement");
                Serverurl = System.Configuration.ConfigurationManager.AppSettings["OARoot"].ToString() + url.Substring(i);
            }
            else
            {
                Response.Write("<script>alert('该文件格式不能进行在线编辑！');window.close();</script>");
            }
        }
        catch
        {
            Response.Write("<script>alert('该文件格式不能进行在线编辑！');window.close();</script>");
        }
    }
}
