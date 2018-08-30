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

public partial class BaseData_OUTNOItemParam : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strHyId//行业类别
    {
        get { return (string)ViewState["strHyId"]; }
        set { ViewState["strHyId"] = value; }
    }
    private DataTable dt_analysis//分析项目列表
    {
        get { return (DataTable)ViewState["dt_analysis"]; }
        set { ViewState["dt_analysis"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面元素
            SetTxt();
            newDt();
            SetButton();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>采样点配置</b></font>";
            #endregion
            Query();
        }
    }

    //查询并填入GridView中
    private void Query()
    {
      //  string strSql = "select  t_CompabyBZ.id,t_委托单位.id 企业id,单位全称,t_CompabyBZ.wrwtype,className 污染物类型,行业类别 hyid,descs 行业类别,t_CompabyBZ.bzid from t_委托单位 inner join t_行业信息 on t_行业信息.id=行业类别 inner join t_CompabyBZ on t_CompabyBZ.qyid=t_委托单位.id inner join t_hyClassParam on t_hyClassParam.id=t_CompabyBZ.bzid inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_CompabyBZ.wrwtype and t_CompabyBZ.flag=0 order by 单位全称";
       // string strsql = "select t_OUTNO.outNO, t_M_ANItemInf.id, AIName 分析项目, t_OUTNOItem.itemid ItemID from t_OUTNOItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_OUTNOItem.itemid inner join  t_OUTNO on t_OUTNO.id=t_OUTNOItem.outid  where outNO='" + txt_SampleSource.Text.Trim() + "' ";
        //where outNO='" + txt_SampleSource.Text.Trim() + "'
        string strSql = "select t_OUTNO.id,t_M_AnalysisMainClassEx.ClassName 污染物类型, t_OUTNO.outNO 采样点, t_OUTNO.type from  t_OUTNO inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_OUTNO.type  ";

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

            //DataColumn dc = new DataColumn("监测指标");
            //ds.Tables[0].Columns.Add(dc);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (dr["id"].ToString() != "")
            //    {

            //        strSql = @"select t_M_ANItemInf.id, AIName 分析项目, t_OUTNOItem.itemid ItemID from t_OUTNOItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_OUTNOItem.itemid where t_OUTNOItem.outid='"+dr["id"].ToString()+"' ";
                       
            //        DataSet dsitem = new MyDataOp(strSql).CreateDataSet();
            //       foreach (DataRow drr in dsitem.Tables[0].Rows)
            //        {
            //            dr["监测指标"] += drr["分析项目"].ToString() + ",";
            //        }
            //    }
            //}
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
        }
    }

    #region GridView相关事件函数

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)

    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ////修改字段标题
            //e.Row.Cells[2].Text = "行业";
            //e.Row.Cells[3].Text = "行业代码";

            //添加按钮列的标题
            TableCell tabcHeaderDetail = new TableCell();
            tabcHeaderDetail.Text = "详细/编辑";
            tabcHeaderDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDetail.Width = 60;
            e.Row.Cells.Add(tabcHeaderDetail);

            TableCell tabcHeaderDel = new TableCell();
            tabcHeaderDel.Text = "删除";
            tabcHeaderDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDel.Width = 30;
            e.Row.Cells.Add(tabcHeaderDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = id.ToString();
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/Images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/Images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDel.Enabled = false;
            }

            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //e.Row.Cells[6].Visible = false;//绑定数据后，隐藏0列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[8].Visible = false;

        }
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        string strSql;
        string[] strlist = new string[3];
       
        int i=0;
        strSql = "DELETE FROM t_OUTNOItem WHERE outid = '" + strSelectedId + "'";
        strlist.SetValue(strSql, i++);
        strSql = "DELETE FROM t_OUTNOParam WHERE outid = '" + strSelectedId + "'";
        strlist.SetValue(strSql, i++);
        strSql = "DELETE FROM t_OUTNO WHERE id = '" + strSelectedId + "'";
         strlist.SetValue(strSql,i++);
            MyDataOp mdo = new MyDataOp(strSql);
            if (mdo.DoTran(i,strlist))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
            }
            Query();
      
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CleanAllText();
        lbl_Type.Text = "详细";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
       // strHyId = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text;
        txt_SampleSource.Text = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        drop_wrw.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
        txt_Item.Text = "";
        ItemParam();
        AllTxtreadOnly();
        btn_OK.Text = "编辑";
       
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }

    #endregion
    protected void ItemParam()
    {
        string strsql = "select id,outid,remark 说明 ,list  监测项 from  t_OUTNOParam  where outid='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strsql).CreateDataSet();
        string strsql2 = "select t_M_ANItemInf.id, AIName 分析项目, t_OUTNOItem.itemid ItemID,t_OUTNOItem.fw,paramid from t_OUTNOItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_OUTNOItem.itemid  where t_OUTNOItem.outid='" + strSelectedId + "'";
        DataSet ds2 = new MyDataOp(strsql2).CreateDataSet();

       // txt_Item.Enabled = false;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow[] drs = ds2.Tables[0].Select("paramid='" + dr["id"].ToString() + "'");
                        foreach (DataRow drr in drs)
                        {
                            dr["监测项"] += drr["分析项目"].ToString() + ",";
                        }
                    }
                }
                else
                {
                    DataRow dr = ds.Tables[0].NewRow();

                    foreach (DataRow drr in ds2.Tables[0].Rows)
                    {

                        dr["监测项"] += drr["分析项目"].ToString() + ",";

                    }
                    ds.Tables[0].Rows.Add(dr);
                }
            }
        }
        grv_Item.DataSource = ds;
        grv_Item.DataBind();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        CleanAllText();
        grv_Item.DataSource = null;
        grv_Item.DataBind();
        btn_OK.Text = "确定";
        Query();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);

    }
    protected void drop_FirSca_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        AllTxtCanWrite();
        Query();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        if (btn_OK.Text == "编辑")
        {
            AllTxtCanWrite();
            btn_OK.Text = "确定";
            lbl_Type.Text = "编辑";
        }
        else
        {
            #region 当按钮文字为“确定”时，执行添加或编辑操作
            DAl.StationParam doobj = new DAl.StationParam();
            Entity.stationP entity = new Entity.stationP();
            //初始化信息
             //获取样品类型编码
            string typename = drop_wrw.SelectedValue.Trim();
            entity.wrwtype = typename;

             //获取企业编号
            string strcheck = "";
             if (lbl_Type.Text == "添加")
            {
                 strcheck="select id from t_OUTNO where outNO='" + txt_SampleSource.Text.Trim() + "'";
             }
             else
             {
                 strcheck = "select id from t_OUTNO where outNO='" + txt_SampleSource.Text.Trim() + "' and id!='" + strSelectedId + "'";
             }
            DataSet myDR2 = new MyDataOp(strcheck).CreateDataSet();
            if (myDR2.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统已存在改采样点！');", true);
                return;
            }
            else
            {
            entity.bz = txt_SampleSource.Text.Trim();
            entity.createdate = DateTime.Now;
            entity.createuser = Request.Cookies["Cookies"].Values["u_id"].ToString();

            }
            //添加新纪录
            if (lbl_Type.Text == "添加")
            {
                if (doobj.addOUTNOParam(entity) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加成功！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                }

                Query();

            }

            //编辑记录
            if (lbl_Type.Text == "编辑")
            {
                entity.ID =int.Parse(strSelectedId);
                if (doobj.updateOUTNOParam(entity) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
                }

                Query();
            }
            }
            #endregion
        }
   

    #region 对TextBox以及DropDownList等控件进行的一些通用的重复性的操作，包括绑定事件和合法性校验
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
        MyStaVoid.BindList("ClassName", "ClassID", "select ClassID, ClassName from t_M_AnalysisMainClassEx", drop_wrw);
        ListItem li = new ListItem("请选择");
        drop_wrw.Items.Add(li);
        drop_wrw.SelectedIndex = drop_wrw.Items.Count - 1;
            txt_SampleSource.Text ="";
            panel_Item.Visible = false;
         txt_Item.Text = "";
       
       
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        txt_SampleSource.ReadOnly = true;
        drop_wrw.Enabled  = false;
        //txt_Item.ReadOnly = true;
        //drop_bz.Enabled = false;
       
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        txt_SampleSource.ReadOnly = false;
        drop_wrw.Enabled = true;
        txt_Item.ReadOnly = false;
        //drop_bz.Enabled = true;
    }
    private void SetTxt()//设置详细页面中的TextBox的一些属性
    {
        //txt_hy.MaxLength = 20;
        //txt_hyCode.MaxLength = 20;
        
    }
    private void SetButton()//根据权限设置读写相关的按钮是否可用
    {
       if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
        {
            btn_Add.Enabled = false;
            btn_OK.Enabled = false;
        }
    }
    #endregion
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Query();
    }

    protected void grv_Item_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Width = 20;
            e.Row.Cells[2].Width = 150;
            TableCell headerset0 = new TableCell();
            headerset0.Text = "编辑";
            headerset0.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset0.Width = 60;
            e.Row.Cells.Add(headerset0);
            TableCell tabcHeaderDel = new TableCell();
            tabcHeaderDel.Text = "删除";
            tabcHeaderDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDel.Width = 30;
            e.Row.Cells.Add(tabcHeaderDel);

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();



            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/Images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);
            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/Images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDel.Enabled = false;
            }

            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
           
            
        }
    }
    protected void grv_Item_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strHyId = grv_Item.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;

        Panel panel_other = panel_Item.FindControl("panel_other") as Panel;
        CheckBoxList cbl_other = panel_other.FindControl("cb_other") as CheckBoxList;
        TextBox txt_Item = panel_Item.FindControl("txt_Item") as TextBox;
        txt_Item.Text = grv_Item.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        if (grv_Item.Rows[e.NewEditIndex].Cells[3].Text != "&nbsp;")
            txt_Remark.Text = grv_Item.Rows[e.NewEditIndex].Cells[3].Text;
        else
            txt_Remark.Text = "";
        if (drop_wrw.SelectedIndex != drop_wrw.Items.Count - 1)
        {
            panel_Item.Visible = true;
            txt_Item.Visible = true;
            DataBindAll(cbl, txt_Item.Text.Trim(), 1,true);
            DataBindAll(cbl_other, txt_Item.Text.Trim(), 0,true);
            string itemname = "";
            string itemvalue = "";
            getinf(cb_analysisList,  ref itemname, ref itemvalue);
            getinf(cb_other, ref itemname, ref itemvalue);
            txt_Item.Text = itemname;
        }
       

    }
    protected void grv_Item_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strHyId = grv_Item.Rows[e.RowIndex].Cells[1].Text.Trim();
        string[] delstrlist = new string[3];
        int i=0;
        string delstr = "delete from t_OUTNOItem where  paramid='" + strHyId + "'";
        delstrlist.SetValue(delstr, i++);
       delstr = "delete from t_OUTNOParam where  id='" + strHyId + "'";
       delstrlist.SetValue(delstr, i++);
       MyDataOp mydo = new MyDataOp(delstr);
      if( mydo.DoTran(i, delstrlist))
      {
          ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据删除成功！');", true);
      }
      else
      {
          ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据删除失败！');", true);
      }
      ItemParam();
    }
    protected void lbtn_chose_OnTextChanged(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;

        Panel panel_other = panel_Item.FindControl("panel_other") as Panel;
        CheckBoxList cbl_other = panel_other.FindControl("cb_other") as CheckBoxList;
        TextBox txt_Item = panel_Item.FindControl("txt_Item") as TextBox;
       
            if (drop_wrw.SelectedIndex != drop_wrw.Items.Count - 1)
            {
                 panel_Item.Visible = true;
                 txt_Item.Visible = true;
                 txt_Item.Text = "";
                DataBindAll(cbl, txt_Item.Text.Trim(), 1,false);
                DataBindAll(cbl_other, txt_Item.Text.Trim(), 0,false);
            }
            strHyId = "";
            txt_Remark.Text = "";
       

    }
    //其他
    protected void btn_other_Onclick(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        //RepeaterItem parent = lbtn_chose.Parent.Parent as RepeaterItem;
        Panel panel_item = lbtn_chose.Parent as Panel;

        Panel panel_other = panel_item.FindControl("panel_other") as Panel;
        CheckBoxList cbl = panel_other.FindControl("cb_other") as CheckBoxList;
        TextBox txt_Item = panel_item.FindControl("txt_Item") as TextBox;
        GridView grv_Item = panel_other.FindControl("grv_Item") as GridView;
        cbl.AutoPostBack = false;
        if (panel_other.Visible)
        {
            panel_other.Visible = false;
        }
        else
        {
            panel_other.Visible = true;
            //DataBindAll(cbl, txt_Item.Text.Trim(), 0);
        }
        cbl.AutoPostBack = true;
    }
    protected void btn_cg_Onclick(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        //RepeaterItem parent = lbtn_chose.Parent.Parent as RepeaterItem;
        Panel panel_item = lbtn_chose.Parent as Panel;
        Panel panel_cg = panel_item.FindControl("panel_cg") as Panel;
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
        TextBox txt_Item = panel_item.FindControl("txt_Item") as TextBox;
        GridView grv_Item = panel_cg.FindControl("grv_Item") as GridView;
        cbl.AutoPostBack = false;
        if (panel_cg.Visible)
        {
            panel_cg.Visible = false;
        }
        else
        {
            panel_cg.Visible = true;
            //DataBindAll(cbl, txt_Item.Text.Trim(), 1);
        }
        cbl.AutoPostBack = true;
    }
    protected void DataBindAll(CheckBoxList cb, string itemvaluelsit, int type,bool flag)
    {
        if (drop_wrw.Text.Trim().ToString().Trim() != "")
        {
            string str = "select  t_M_AnalysisItemEx.AIID id,t_M_ANItemInf.AIName from t_M_AnalysisItemEx,t_M_AnalysisMainClassEx,t_M_ANItemInf where t_M_AnalysisItemEx.ClassID=t_M_AnalysisMainClassEx.ClassID and t_M_ANItemInf.ID=t_M_AnalysisItemEx.AIID and type='" + type + "' and t_M_AnalysisMainClassEx.ClassID='" + drop_wrw.SelectedValue.Trim().ToString() + "'";
            //string str = "select id,AIName from t_M_AnalysisItemEx order by ClassID asc";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            cb.DataSource = ds;
            cb.DataValueField = "id";
            cb.DataTextField = "AIName";
            cb.DataBind();
            if (flag)
            {
                if (ds.Tables[0].Rows.Count < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('没有该样品类型的分析项目，请先在分析项目管理中添加');", true);
                    return;
                }
                else
                {
                    string[] list = itemvaluelsit.Split(',');
                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        foreach (string item in list)
                        {
                            if (item.Trim() == cb.Items[i].Text.ToString().Trim())
                            {
                                cb.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            panel_Item.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请先选择样品类型');", true);

            return;
        }

    }
    //protected void RepeaterSample_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{

    //    RepeaterItem seleItem = e.Item;
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {

    //        Panel panel_Item = seleItem.FindControl("panel_Item") as Panel;
    //        grv_ItemBind(panel_Item);
    //    }

    //}
    //protected void grv_ItemBind(Panel parent)
    //{

    //    // CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
    //   // GridView grv_Item = parent.FindControl("grv_Item") as GridView;
    //    TextBox txt_Item = parent.FindControl("txt_Item") as TextBox;
    //    HiddenField hid_Item = parent.FindControl("hid_Item") as HiddenField;
    //    DataTable tempds;
    //    Panel panel_cg = parent.FindControl("panel_cg") as Panel;
    //    Panel panel_other = parent.FindControl("panel_other") as Panel;
    //    CheckBoxList cb_analysisList = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
    //    CheckBoxList cb_other = panel_other.FindControl("cb_other") as CheckBoxList;
    //    DataBindAll(cb_analysisList, txt_Item.Text, 1);
    //    DataBindAll(cb_other, txt_Item.Text, 0);
    //    tempds = dt_analysis.Clone();

    //    //string itemname = "";
    //    //string itemvalue = "";
    //    //getinf(cb_analysisList, grv_Item, ref tempds, ref itemname, ref itemvalue);
    //    //getinf(cb_other, grv_Item, ref tempds, ref itemname, ref itemvalue);
    //    //txt_Item.Text = itemname;
    //    //hid_Item.Value = itemvalue;
    //    //grv_Item.DataSource = tempds;
    //    //grv_Item.DataBind();
    //    // grv_Item.Visible = ck_xcflag.Checked;


    //}
   
    protected void txt_Item_OnTextChanged(object sender, EventArgs e)
    {
        string s = drop_wrw.Text.Trim();
        TextBox txt_Item = sender as TextBox;
        // RepeaterItem parent = txt_Item.Parent as RepeaterItem;
        string[] list = txt_Item.Text.Trim().Split(',');

        DataBindAll(cb_analysisList, txt_Item.Text.Trim(), 1, true);
        DataBindAll(cb_other, txt_Item.Text.Trim(), 0, true);
    }
    protected void cb_analysisList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string s = drop_wrw.Text.Trim();
        CheckBoxList cb_analysisList = sender as CheckBoxList;
        Panel parent = cb_analysisList.Parent as Panel;
        Change(parent);

    }
    protected void cb_cg_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb_cg = sender as CheckBox;
        Panel panel_cg = cb_cg.Parent as Panel;
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
        if (cb_cg.Checked)
        {
            //RepeaterItem panel_cg = cb_cg.Parent as RepeaterItem;

            foreach (ListItem li in cbl.Items)
            { li.Selected = true; }
            Change(panel_cg);

        }
        else
        {
            foreach (ListItem li in cbl.Items)
            { li.Selected = false; }
            Change(panel_cg);
        }
    }
    private void newDt()
    {
        dt_analysis = null;
        dt_analysis = new DataTable();
        DataColumn dc6 = new DataColumn("ID");
       
        DataColumn dc1 = new DataColumn("分析项目");
        DataColumn dc2 = new DataColumn("ItemID");
        DataColumn dc3 = new DataColumn("标准");
        
        dt_analysis.Columns.Add((dc6));
      
        dt_analysis.Columns.Add((dc1));
        dt_analysis.Columns.Add((dc2));
        dt_analysis.Columns.Add((dc3));
       

    }
    protected void getinf(CheckBoxList cbl,  ref  string itemname, ref string itemvalue)
    {

        foreach (ListItem LI in cbl.Items)
        {
            if (LI.Selected)
            {
                        itemname += LI.Text.Trim() + ",";
                        itemvalue += LI.Value.Trim() + ",";

               
            }

        }
    }
 
   
    protected void Change(Panel parent)
    {
        GridView grv_Item = parent.FindControl("grv_Item") as GridView;
        TextBox txt_Item = parent.FindControl("txt_Item") as TextBox;
        HiddenField hid_Item = parent.FindControl("hid_Item") as HiddenField;
        DataTable tempds;
        Panel temp = parent.FindControl("panel_cg") as Panel;
        Panel temp2 = parent.FindControl("panel_other") as Panel;
        CheckBoxList cb_analysisList = temp.FindControl("cb_analysisList") as CheckBoxList;
        CheckBoxList cb_other = temp2.FindControl("cb_other") as CheckBoxList;

        tempds = dt_analysis.Clone();
        string itemname = "";
        string itemvalue = "";
        getinf(cb_analysisList, ref itemname, ref itemvalue);
        getinf(cb_other,  ref itemname, ref itemvalue);
        txt_Item.Text = itemname;
       
    }

    protected void drop_wrw_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAl.Station stationobj = new DAl.Station();
       DataTable dtcompanay= stationobj.GetStationByName(txt_SampleSource.Text.Trim());
        if(dtcompanay!=null)
            if (dtcompanay.Rows.Count > 0)
            {
        
            }
    }
    protected void drop_bz_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }


    protected void btn_saveitem_Click(object sender, EventArgs e)
    {
        Entity.stationP entity = new Entity.stationP();
        entity.wrwtype = strSelectedId;
        entity.createuser = "";
        entity.bz = txt_Remark.Text.Trim();
        if (strHyId != "&nbsp;" && strHyId != "")
            entity.ID = int.Parse(strHyId);
        else
            entity.ID = 0;

        foreach (ListItem LI in cb_analysisList.Items)
        {
            if (LI.Selected)
            {
                Entity.Item temp = new Entity.Item();
                temp.itemid = LI.Value;
                temp.itemfw = "";
                entity.itemlist.Add(temp);
            }

        }
        foreach (ListItem LI in cb_other.Items)
        {
            if (LI.Selected)
            {
                Entity.Item temp = new Entity.Item();
                temp.itemid = LI.Value;
                temp.itemfw = "";
                entity.itemlist.Add(temp);
            }

        }
        DAl.StationParam doobj = new DAl.StationParam();
        if (doobj.AddItemParam(entity) > 0)
        {
            panel_Item.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存成功！');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
        }

        ItemParam();
    }
}
