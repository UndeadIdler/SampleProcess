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

public partial class maintaininfo_GetYQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Request.QueryString["key"].Trim();
      // string sql = "select equipment from t_e_equipment where equipment like '%" + key + "%'";

        string sql = "select t_e_factory.name+'#'+t_e_type.type+'#'+t_e_model.name as equipmentname,t_e_type.type,t_e_factory.name,t_e_model.name standard from t_e_equipment inner join t_e_factory on t_e_factory.id=t_e_equipment.factory inner join t_e_type on t_e_type.id=t_e_equipment.classid inner join t_e_model on t_e_model.id=t_e_equipment.standard where t_e_factory.name+'#'+t_e_type.type+'#'+t_e_model.name like '%" + key + "%'";
        DataSet ds = new MyDataOp(sql).CreateDataSet();        
      
        StringBuilder items = new StringBuilder();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string str = dr["equipmentname"].ToString();
           // string str = dr["name"].ToString() + "#" + dr["type"].ToString();
           // if (dr["standard"].ToString().Trim() != "")
           //     str += "#" + dr["standard"].ToString();
            items.Append(str + "\n");
        }
        Response.Write(items.ToString());
        Response.End();
    }
}
