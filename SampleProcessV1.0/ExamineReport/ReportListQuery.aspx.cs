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

public partial class ReportListQuery : System.Web.UI.Page
{
    public string outputStr;
    public string outputSum;
    public int kk = 0;
    private DataSet ds
    {
        get { return (DataSet)ViewState["ds"]; }
        set { ViewState["ds"] = value; }
    }
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面
            txt_StartTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-01-01");
            txt_EndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");       
            Query();
          
            #endregion 
        }       
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    private void Query()
    {
        string strSql = "select t.id 编号,ItemName 项目类型,accessman 受理人员,accessdate 受理日期,accessremark 备注,varman1,vardate1,varremark1,varman2,vardate2,varremark2,varman3,vardate3,varremark3,varman4,vardate4,varremark4,varman5,vardate5,varremark5,varman6,vardate6,varremark6 from t_Y_FlowInfo t where t.accessdate >= '" + txt_StartTime.Text.Trim() + " 0:00:00" + "' and t.accessdate <= '" + txt_EndTime.Text.Trim() + " 23:59:59" + "' order by id desc";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["受理人员"].ToString() == drr["UserID"].ToString())
                    dr["受理人员"] = drr["Name"].ToString();
                //if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportProofUserID"] = drr["Name"].ToString();
                //if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                //    dr["ReportSignUserID"] = drr["Name"].ToString();
            }
        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
            int intColumnCount = grdvw_List.Rows[0].Cells.Count;
            grdvw_List.Rows[0].Cells.Clear();
            grdvw_List.Rows[0].Cells.Add(new TableCell());
            grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
            //translateTheIDToName();
        } 


    }
    protected void grdvw_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('Seeattachment.aspx?rid=" + strSelectedId + "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);

    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {



            TableCell headerset = new TableCell();
            headerset.Text = "报告查看";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

            //TableCell headerUp = new TableCell();
            //headerUp.Text = "上传文件";
            //headerUp.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerUp.Width = 60;
            //e.Row.Cells.Add(headerUp);

            //TableCell headerDel = new TableCell();
            //headerDel.Text = "删除";
            //headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerDel.Width = 30;
            //e.Row.Cells.Add(headerDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();



            //TableCell MenuSet = new TableCell();
            //MenuSet.Width = 60;
            //MenuSet.Style.Add("text-align", "center");
            //ImageButton btMenuSet = new ImageButton();
            //btMenuSet.ImageUrl = "~/images/Detail.gif";
            //btMenuSet.CommandName = "Edit";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuSet.Controls.Add(btMenuSet);
            //e.Row.Cells.Add(MenuSet);
            //上传文件
            TableCell MenuUp = new TableCell();
            MenuUp.Width = 60;
            MenuUp.Style.Add("text-align", "center");
            ImageButton btMenuUp = new ImageButton();
            btMenuUp.ImageUrl = "~/images/Detail.gif";
            btMenuUp.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuUp.Controls.Add(btMenuUp);
            e.Row.Cells.Add(MenuUp);

            //TableCell tabcDel = new TableCell();
            //tabcDel.Width = 30;
            //tabcDel.Style.Add("text-align", "center");
            //ImageButton ibtnDel = new ImageButton();
            //ibtnDel.ImageUrl = "~/Images/Delete.gif";
            //ibtnDel.CommandName = "Delete";
            //ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            //{
            //    ibtnDel.Enabled = false;
            //}
            //tabcDel.Controls.Add(ibtnDel);
            //e.Row.Cells.Add(tabcDel);


        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[22].Visible = false;
            e.Row.Cells[23].Visible = false;
            //e.Row.Cells[24].Visible = false;
            //e.Row.Cells[25].Visible = false;
            //e.Row.Cells[26].Visible = false;
            //e.Row.Cells[27].Visible = false;
            //e.Row.Cells[28].Visible = false;
            //e.Row.Cells[29].Visible = false;
            //e.Row.Cells[30].Visible = false;
            //e.Row.Cells[31].Visible = false;
            //e.Row.Cells[32].Visible = false;
            //e.Row.Cells[33].Visible = false;
            //e.Row.Cells[34].Visible = false;
            //e.Row.Cells[35].Visible = false;
            //e.Row.Cells[36].Visible = false;
            //e.Row.Cells[37].Visible = false;
            //e.Row.Cells[38].Visible = false;

        }
    }



    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query();
    }
}
