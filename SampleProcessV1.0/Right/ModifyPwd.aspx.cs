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
public partial class BaseData_ModifyPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetTxt();
    }
   
    private void CleanAllTxt()
    {
        txt_OrigPwd.Text = "";
        txt_NewPwd.Text = "";
        txt_RetypePwd.Text = "";
    }
    private void SetTxt()
    {
        txt_OrigPwd.MaxLength = 12;
        txt_NewPwd.MaxLength = 12;
        txt_RetypePwd.MaxLength = 12;
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        CleanAllTxt();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        DateTime pswdTime = DateTime.Parse(DateTime.Now.ToString()); 
        string strSql = "select * from t_R_UserInfo where UserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'";
        DataSet ds=new MyDataOp(strSql).CreateDataSet();

        if (txt_OrigPwd.Text.Trim()!= ds.Tables[0].Rows[0]["PWD"].ToString())
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('原密码不正确！');", true);
            txt_OrigPwd.Text = "";
            return;
        }

        if (txt_NewPwd.Text.Trim() != txt_RetypePwd.Text.Trim())
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('新密码与确认密码不匹配！');", true);
            txt_NewPwd.Text = "";
            txt_RetypePwd.Text = "";
            return;
        }

        strSql = "update t_R_UserInfo " +
            " set PWD='" + txt_NewPwd.Text.Trim() + "',PWDModifyTime = '" + pswdTime + "' where UserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'";
        MyDataOp mdo = new MyDataOp(strSql);
        bool blSuccess = mdo.ExecuteCommand();
        if (blSuccess == true)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('密码修改成功！请重新登陆');parent.location='../login.aspx';", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('密码修改失败！');", true);
            return;
        }
    }
}
