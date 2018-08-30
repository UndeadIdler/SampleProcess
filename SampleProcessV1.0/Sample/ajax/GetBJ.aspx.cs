using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;

using WebApp.Components;

public partial class maintaininfo_GetBJ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Request.QueryString["key"].Trim();
        string[] temp = key.Split(',');
        string cstr="";
        if(temp.Length>1)
        {
            cstr = " and type='" + temp[0].ToString() + "'";
          key = temp[1].ToString();
        }
        string sql = "select accessoriesname from t_e_AccessoriesInfo inner join t_e_type on t_e_type.id=t_e_AccessoriesInfo.modelid where  accessoriesname like '%" + key + "%'" + cstr;
        DataSet ds = new MyDataOp(sql).CreateDataSet();        
       

        StringBuilder items = new StringBuilder();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string str = dr[0].ToString();
            items.Append(str + "\n");
        }
        Response.Write(items.ToString());
        Response.End();
    }
}
