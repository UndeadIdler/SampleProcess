using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebApp.Components;

public partial class Getdata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["searchText"] != null)
        {
            if (Request.QueryString["searchText"].ToString().Trim().Length > 0)
            {
                DataSet ds = new MyDataOp(string.Format("select AIName from t_M_AnalysisItem where AICode like'{0}%'", Request.QueryString["searchText"])).CreateDataSet();

                string returnText = "";
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        returnText += dr[0].ToString() + "\n";
                    }
                }

                Response.Write(returnText);
            }
        }
       
    }
}
