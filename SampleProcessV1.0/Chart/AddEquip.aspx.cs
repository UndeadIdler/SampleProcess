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
using System.Text.RegularExpressions;//使用正则表达式
using WebApp.Components;

public partial class BaseData_AddEquip : System.Web.UI.Page
{
    private string strScaName//本级别名称
    {
        get { return (string)ViewState["strScaName"]; }
        set { ViewState["strScaName"] = value; }
    }
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    { 
        
       // showDetail("");
        if (!IsPostBack)
        {
            //string receiveStr="";
            //if (Request.Params["kw"] != "" && Request.Params["kw"]!=null)
            //{
            //    receiveStr = Request.Params["kw"].ToString();
            //   txt_ForSca_Code_forSearch.Text = receiveStr;
            //}
            #region 初始化页面元素
           
          
            #endregion
            
            Query();
        }

    }
    //private void SetTxt()//设置详细页面中的TextBox的一些属性
    //{

    //    txt_ForSca_Code_forSearch.MaxLength = 20;
    //    txt_ForSca_Name_forSearch.MaxLength = 20;
    //    txt_ThrSca_Code_forSearch.MaxLength = 20;
    //    txt_ThrSca_Name_forSearch.MaxLength = 20;

    //}
    private void Query()
    {
        string strSql = "select id,charttype 流程图 from t_chart_type ";
        string strCondition = "";//查询语句的条件部分，condition--条件

        
            //如果选择的是全部，则读出所有的可用value组织查询条件，否则只取当前value参与查询
            
      

        strSql += strCondition;
        DataSet ds = new MyDataOp(strSql).CreateDataSet();

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
        }

    }

    protected void btn_Query_Click(object sender, EventArgs e)
    {
        Query();
    }
    #region GridView相关事件函数

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //修改字段标题
            //e.Row.Cells[3].Text = MyStaVoid.getScaleName(4) + "编码";
            //e.Row.Cells[4].Text = MyStaVoid.getScaleName(4) + "名称";
            //e.Row.Cells[5].Text = MyStaVoid.getScaleName(5) + "编码";
            //e.Row.Cells[6].Text = "监测点";

            //插入按钮列的标题
            TableCell headerDetail = new TableCell();
            headerDetail.Text = "图片选择";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Style.Add("BACKGROUND-IMAGE ", "url(../Images/bgi.gif)"); 
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);
            ////插入按钮列的标题
            //TableCell headerPipeLine = new TableCell();
            //headerPipeLine.Text = "泵选择";
            //headerPipeLine.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerPipeLine.Style.Add("BACKGROUND-IMAGE ", "url(../Images/bgi.gif)"); 
            //headerPipeLine.Width = 60;
            //e.Row.Cells.Add(headerPipeLine);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#c1ebff'");

            ////手动添加详细按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/Images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            //////手动添加管道选择按钮
            //TableCell tabcPipeLine = new TableCell();
            //tabcPipeLine.Width = 60;
            //tabcPipeLine.Style.Add("text-align", "center");
            //ImageButton ibtnPipeLine = new ImageButton();
            //ibtnPipeLine.ImageUrl = "~/Images/Detail.gif";
            //ibtnPipeLine.CommandName = "Select";
            //tabcPipeLine.Controls.Add(ibtnPipeLine);
            //e.Row.Cells.Add(tabcPipeLine);

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;//绑定数据后，隐藏0列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
       
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text;
      
       
       lbl_Type.Text ="流程图图片选择";
       lbl_Type.Width = System.Web.UI.WebControls.Unit.Parse("300px");
       string strSql = @"select t_chart_item.* from t_chart_item";

       DataSet ds = new MyDataOp(strSql).CreateDataSet();
       GridView1.DataSource = ds;
       GridView1.DataBind();
       CleanData();
       showDetail(strSelectedId);
        //AllTxtReadOnly();
        btn_OK.Text = "确定";//showAddEdit()
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }
    #endregion
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        Query();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {

        #region 当按钮文字为“确定”时，执行添加或编辑操作
        //合法性校验
        //string strErrorInfo = Verify();
        //if (strErrorInfo != "")
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert(\"" + strErrorInfo + "\");", true);
        //    return;
        //}
        string strSql="";
        #region 
        string[] strInsert = new string[100];
        string sql = "";
        int j = 0;
        
            string strEquipment = @"select * from t_chart_item";
            DataSet dse = new MyDataOp(strEquipment).CreateDataSet();
            int level = 0;

            foreach (GridViewRow gr in GridView1.Rows)
            {
                DataRow [] dr=dse.Tables[0].Select("id='"+gr.Cells[1].Text.Trim()+"'");
                if (dr.Length > 0)
                {
                    //foreach (DataRow dr in dse.Tables[0].Rows)
                    //{
                    int Flag = 0;
                    string straddress = dr[0]["address"].ToString();

                    CheckBox savecheckbox = (CheckBox)gr.FindControl("CheckBox");
                    TextBox savetextbox = (TextBox)gr.FindControl("TextBox");
                    //选中的设备
                    if (savecheckbox.Checked)
                    {
                        int x;
                        int y;
                        x = int.Parse(dr[0]["x"].ToString());
                        y = int.Parse(dr[0]["y"].ToString());
                        int h = int.Parse(dr[0]["h"].ToString());
                       int w = int.Parse(dr[0]["w"].ToString());
                       string address = dr[0]["address"].ToString();
                        sql = "select * from t_chart_main where  type='" + strSelectedId + "' and pid='" + gr.Cells[1].Text.Trim() + "'";
                        DataSet ds = new MyDataOp(sql).CreateDataSet();
                        if (ds.Tables[0].Rows.Count <= 0)
                        {

                            strSql = @"Insert into t_chart_main(type,pid,name,x,y,h,w,address)values('" + strSelectedId + "','" + gr.Cells[1].Text.Trim() + "','" + gr.Cells[2].Text.Trim() + "','" + x + "','" + y + "','" + h + "','" + w + "','" + address + "')";
                            strInsert.SetValue(strSql, j++);

                        }
                    }
                    //  没有选中的设备
                    else
                    {
                        sql = "select * from t_chart_main where  type='" + strSelectedId + "' and pid='" + gr.Cells[1].Text.Trim() + "'";
                        DataSet ds = new MyDataOp(sql).CreateDataSet();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            sql = "Delete from t_chart_main where type='" + strSelectedId + "' and pid='" + gr.Cells[1].Text.Trim() + "' ";
                            strInsert.SetValue(sql, j++);
                        }
                    }

                }
            }
        
        if (j > 0)
        {
            MyDataOp mdo = new MyDataOp(strSql);
            bool blSuccess = mdo.DoTran(j, strInsert);
            if (blSuccess == true)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('保存成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('保存失败！');", true);
            }
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择图片！');", true);
       
        #endregion

       
        #endregion
    }



    protected void grdvbs_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    //修改字段标题
            //    e.Row.Cells[3].Text = "别名";
            //    if (lbl_Type.Text.Trim() == "流程图设备选择")
            //    {
            //        e.Row.Cells[6].Text = "数量";
            //        //e.Row.Cells[5].Text = "设备类别";
            //    }
            //    else
            //    {
            //        e.Row.Cells[4].Text = "方向";
            //        e.Row.Cells[5].Text = "设备类别";
            //    }
            //    e.Row.Cells[7].Text = "图片大小";

            //}
            //if (e.Row.RowType != DataControlRowType.Pager)
            //{
            //    if (lbl_Type.Text.Trim() != "流程图设备选择")
            //    {
            //        e.Row.Cells[6].Visible = false;
            //        e.Row.Cells[4].Visible = true;
            //        e.Row.Cells[5].Visible = true;
            //        //e.Row.Cells[5].Text = "设备类别";
            //    }
            //    else
            //    {
            //        e.Row.Cells[4].Visible = false;
            //        e.Row.Cells[5].Visible = false;
            //        e.Row.Cells[6].Visible = true;
            //    }
            //    //e.Row.Cells[0].Visible = false;//绑定数据后，隐藏0列 
            //    //e.Row.Cells[1].Visible = false;
            //    //e.Row.Cells[2].Visible = false;
            //}
        

    }
    private void showDetail(string strselected)
    {
        string str = "select * from t_chart_main where type='"+strselected+"'";
        DataSet ds = new MyDataOp(str).CreateDataSet();
        if (ds != null)
        {
            foreach (GridViewRow gr in GridView1.Rows)//也可以循环Table 
            {
                DataRow[] dr = ds.Tables[0].Select("pid='" + gr.Cells[1].Text.Trim() + "'");
                if (dr.Length > 0)
                {
                    CheckBox cb = (CheckBox)gr.FindControl("CheckBox");
                    cb.Checked = true;
                }
            }
        }
    }
    private void CleanData()
    {
        if(GridView1.Rows.Count>0)
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox cb =(CheckBox)gr.FindControl("CheckBox");
            cb.Checked = false;
        }
    }
   
}
