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

public partial class BaseData_QYItemParam : System.Web.UI.Page
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
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>企业监测项配置</b></font>";
            #endregion
            Query();
        }
    }

    //查询并填入GridView中
    private void Query()
    {
        string strconstr = "";
        if (dropList_type_Query.SelectedValue.Trim()!="-1")
            strconstr += "  and itemid = '" + dropList_type_Query.SelectedValue.Trim() + "' ";
        if (txt_qyQuery.Text.Trim() != "")
            strconstr += "  and 单位全称 like '%" + txt_qyQuery.Text.Trim() + "%' ";
        string strSql = "select  t_CompabyBZ.id,t_委托单位.id 企业id,单位全称,t_CompabyBZ.wrwtype,className 污染物类型,行业类别 hyid,descs 行业类别,t_CompabyBZ.bzid,t_M_ItemInfo.itemid,t_M_ItemInfo.itemname 项目类型 from t_委托单位 inner join t_行业信息 on t_行业信息.id=行业类别 inner join t_CompabyBZ on t_CompabyBZ.qyid=t_委托单位.id inner join t_hyClassParam on t_hyClassParam.id=t_CompabyBZ.bzid inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_CompabyBZ.wrwtype and t_CompabyBZ.flag=0  inner join t_M_ItemInfo on  itemtype =t_M_ItemInfo.itemid where 1=1 " + strconstr + " order by 单位全称";

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

            DataColumn dc = new DataColumn("监测指标");
            ds.Tables[0].Columns.Add(dc);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["bzid"].ToString() != "")
                {

                    strSql = @"select t_hyItem.id,t_M_ANItemInf.AIName 分析项目,ItemID,fw 标准 from t_hyItem inner join t_M_ANItemInf on t_M_ANItemInf.id=t_hyItem.itemid
                        where pid='" + int.Parse(dr["bzid"].ToString()) + "'";
                    DataSet dsitem = new MyDataOp(strSql).CreateDataSet();
                   foreach (DataRow drr in dsitem.Tables[0].Rows)
                    {
                        dr["监测指标"] += drr["分析项目"].ToString() + ",";
                    }
                }
            }
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
            e.Row.Cells[6].Visible = false;//绑定数据后，隐藏0列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;

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
        string[] strlist = new string[2];
       
        int i=0;
       
        strSql = "DELETE FROM t_CompabyBZ WHERE id = '" + strSelectedId + "'";
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
        strHyId = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text;
        txt_SampleSource.Text = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        drop_wrw.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[4].Text;
        drop_bz.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[8].Text;
        MyStaVoid.BindList("bz", "id", "select t_hyClassParam.id,t_标准字典.bz+'('+t_hyClassParam.itemlist+')' as bz from t_hyClassParam inner join t_标准字典 on t_hyClassParam.bz=t_标准字典.id where hyid='" + strHyId + "'", drop_bz);
        txt_Item.Text = "";
        string strsql = "select t_M_ANItemInf.id, AIName 分析项目, t_hyItem.itemid ItemID,t_hyItem.fw 标准 from t_hyItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_hyItem.itemid where pid='" + drop_bz.SelectedValue.ToString().Trim() + "'";
        DataSet ds = new MyDataOp(strsql).CreateDataSet();
     // MyStaVoid.BindList("bz", "id", "select t_hyClassParam.id,t_标准字典.bz+'('+t_hyClassParam.itemlist+')' as bz from t_hyClassParam inner join t_标准字典 on t_hyClassParam.bz=t_标准字典.id where hyid='" + strHyId + "'", drop_itemtype);

        drop_itemtype.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim();
        txt_Item.Enabled = false;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                //DataColumn dc = new DataColumn("ID");
                //ds.Tables[0].Columns.Add(dc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txt_Item.Text += dr["分析项目"].ToString() + ",";
                    }
                    grv_Item.DataSource = ds;
                    grv_Item.DataBind();

                }
            }
        }
        AllTxtreadOnly();
        btn_OK.Text = "编辑";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }

    #endregion

    protected void btn_Query_Click(object sender, EventArgs e)
    {
        Query();
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
            //DataSet myDR1 = new MyDataOp("select ClassID TypeID from t_M_AnalysisMainClassEx where ClassID='" + drop_wrw.SelectedValue.Trim() + "'").CreateDataSet();
            //if (myDR1.Tables[0].Rows.Count > 0)
            //{
            //    typename = myDR1.Tables[0].Rows[0]["TypeID"].ToString();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该样品类型，请核实！');", true);
            //    return;
            //}
            entity.wrwtype = typename;

             //获取企业编号
            string  compay ="";
            DataSet myDR2 = new MyDataOp("select id from t_委托单位 where 单位全称='" + txt_SampleSource.Text.Trim() + "'").CreateDataSet();
            if (myDR2.Tables[0].Rows.Count > 0)
            {
                compay = myDR2.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('系统不存在该企业，请核实！');", true);
                return;
            }
            entity.bz = drop_bz.SelectedValue.Trim();
            entity.qyid = compay;
            entity.createdate = DateTime.Now;
            entity.createuser = Request.Cookies["Cookies"].Values["u_id"].ToString();
            entity.itemtype = drop_itemtype.SelectedValue.ToString().Trim();
        //    for (int j = 0; j < grv_Item.Rows.Count; j++)
        //    {
        //        Entity.Item temp = new Entity.Item();
        //        if (grv_Item.Rows[j].Cells[3].Text.Trim() != "" && grv_Item.Rows[j].Cells[3].Text.Trim() != "&nbsp;")
        //        {
        //            try
        //            {
        //                temp.itemid = grv_Item.Rows[j].Cells[3].Text.Trim();//分析项目ID
        //                TextBox txt_fw = grv_Item.Rows[j].Cells[5].FindControl("itemvalue") as TextBox;

        //                try
        //                {
        //                    temp.itemfw = txt_fw.Text.Trim();//分析项目范围
        //                }
        //                catch
        //                { temp.itemfw = ""; }
        //                entity.itemlist.Add(temp);
        //            }
        //            catch
        //            { temp.itemid = ""; }
                    
        //        }
               

                
        //    }
            //添加新纪录
            if (lbl_Type.Text == "添加")
            {
                if (doobj.addCompanyParam(entity) > 0)
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
                entity.ID = int.Parse(strSelectedId);
                if (doobj.updateCompanyParam(entity) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
                }

                Query();
            }
            #endregion
        }
    }

    #region 对TextBox以及DropDownList等控件进行的一些通用的重复性的操作，包括绑定事件和合法性校验
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
        MyStaVoid.BindList("ClassName", "ClassID", "select ClassID, ClassName from t_M_AnalysisMainClassEx", drop_wrw);
        ListItem li = new ListItem("请选择");
        drop_wrw.Items.Add(li);
        drop_wrw.SelectedIndex = drop_wrw.Items.Count - 1;
            txt_SampleSource.Text ="";
        
         txt_Item.Text = "";
         MyStaVoid.BindList("ItemName", "ItemID", "select ItemID,ItemName,ItemCode from t_M_ItemInfo  order by ItemID", drop_itemtype);
       
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        txt_SampleSource.ReadOnly = true;
        drop_wrw.Enabled  = false;
        txt_Item.ReadOnly = true;
        drop_bz.Enabled = false;
        drop_itemtype.Enabled = false;
       
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        txt_SampleSource.ReadOnly = false;
        drop_wrw.Enabled = true;
        txt_Item.ReadOnly = false;
        drop_bz.Enabled = true;
        drop_itemtype.Enabled = true;
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
            headerset0.Text = "标准";
            headerset0.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset0.Width = 60;
            e.Row.Cells.Add(headerset0);

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();



            //分析值
            TableCell tabcDetail1 = new TableCell();
            tabcDetail1.Width = 60;
            tabcDetail1.Style.Add("text-align", "center");
            TextBox itemvalue = new TextBox();
            itemvalue.ID = "itemvalue";
            itemvalue.Width = 75;
            tabcDetail1.Controls.Add(itemvalue);
            e.Row.Cells.Add(tabcDetail1);

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            
        }
    }
    protected void grv_Item_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView item_grv = sender as GridView;
        string grvid = item_grv.ID;
        //flag = int.Parse(grvid.Substring(grvid.Length - 1, 1));
        string itemid = e.Row.Cells[3].Text.Trim();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txt_value = (TextBox)e.Row.FindControl("itemvalue");
            if (e.Row.Cells[4].Text.Trim() != "&nbsp;")
            {
                txt_value.Text = e.Row.Cells[4].Text.Trim();
            }
           
        }
    }
    protected void lbtn_chose_OnTextChanged(object sender, EventArgs e)
    {

        LinkButton lbtn_chose = sender as LinkButton;
        CheckBoxList cbl = panel_cg.FindControl("cb_analysisList") as CheckBoxList;

        Panel panel_other = panel_Item.FindControl("panel_other") as Panel;
        CheckBoxList cbl_other = panel_other.FindControl("cb_other") as CheckBoxList;
        TextBox txt_Item = panel_Item.FindControl("txt_Item") as TextBox;
        if (panel_Item.Visible)
        {
            panel_Item.Visible = false;

        }
        else
        {
            panel_Item.Visible = true;
            DataBindAll(cbl, txt_Item.Text.Trim(), 1);
            DataBindAll(cbl_other, txt_Item.Text.Trim(), 0);
        }

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
            DataBindAll(cbl, txt_Item.Text.Trim(), 0);
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
            DataBindAll(cbl, txt_Item.Text.Trim(), 1);
        }
        cbl.AutoPostBack = true;
    }
    protected void DataBindAll(CheckBoxList cb, string itemvaluelsit, int type)
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
        else
        {
            panel_Item.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请先选择样品类型');", true);

            return;
        }

    }
    protected void RepeaterSample_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        RepeaterItem seleItem = e.Item;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Panel panel_Item = seleItem.FindControl("panel_Item") as Panel;
            grv_ItemBind(panel_Item);
        }

    }
    protected void grv_ItemBind(Panel parent)
    {

       // CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
        GridView grv_Item = parent.FindControl("grv_Item") as GridView;
        TextBox txt_Item = parent.FindControl("txt_Item") as TextBox;
        HiddenField hid_Item = parent.FindControl("hid_Item") as HiddenField;
        DataTable tempds;
        Panel panel_cg = parent.FindControl("panel_cg") as Panel;
        Panel panel_other = parent.FindControl("panel_other") as Panel;
        CheckBoxList cb_analysisList = panel_cg.FindControl("cb_analysisList") as CheckBoxList;
        CheckBoxList cb_other = panel_other.FindControl("cb_other") as CheckBoxList;
        DataBindAll(cb_analysisList, txt_Item.Text, 1);
        DataBindAll(cb_other, txt_Item.Text, 0);
        tempds = dt_analysis.Clone();

        string itemname = "";
        string itemvalue = "";
        getinf(cb_analysisList, grv_Item, ref tempds, ref itemname, ref itemvalue);
        getinf(cb_other, grv_Item, ref tempds, ref itemname, ref itemvalue);
        txt_Item.Text = itemname;
        hid_Item.Value = itemvalue;
        grv_Item.DataSource = tempds;
        grv_Item.DataBind();
       // grv_Item.Visible = ck_xcflag.Checked;

      
    }
    protected void txt_Item_OnTextChanged(object sender, EventArgs e)
    {
        string s = drop_wrw.Text.Trim();
        TextBox txt_Item = sender as TextBox;
       // RepeaterItem parent = txt_Item.Parent as RepeaterItem;
        string[] list = txt_Item.Text.Trim().Split(',');
        //CheckBox ck_xcflag = parent.FindControl("ck_xcflag") as CheckBox;
       // Panel panel_Item = parent.FindControl("panel_Item") as Panel;
        grv_ItemBind(panel_Item);
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

        MyStaVoid.BindList("ItemName", "ItemID", "select ItemID,ItemName,ItemCode from t_M_ItemInfo  order by ItemID", dropList_type_Query);
        ListItem li = new ListItem("所有", "-1");
        dropList_type_Query.Items.Add(li);
        dropList_type_Query.SelectedValue = "-1";

    }
    protected void getinf(CheckBoxList cbl, GridView grv_Item, ref DataTable tempds, ref  string itemname, ref string itemvalue)
    {

        foreach (ListItem LI in cbl.Items)
        {
            if (LI.Selected)
            {
                int i = 0;
                foreach (GridViewRow grv in grv_Item.Rows)
                {
                    if (grv.Cells[2].Text.Trim() == LI.Text.ToString().Trim())
                    {
                        i++;
                        DataRow drnew = tempds.NewRow();
                        drnew["ID"] = grv.Cells[1].Text.Trim();
                        drnew["分析项目"] = LI.Text;
                        drnew["ItemID"] = LI.Value;
                        

                        if (grv.Cells[5].Text.Trim() != "&nbsp;")
                            drnew["标准"] = grv.Cells[4].Text.Trim();//分析
                        else
                            drnew["标准"] = "";

                        tempds.Rows.Add(drnew);
                        tempds.AcceptChanges();
                        itemname += LI.Text.Trim() + ",";
                        itemvalue += LI.Value.Trim() + ",";

                    }
                }

                if (i == 0)
                {
                    DataRow drnew = tempds.NewRow();
                    drnew["ID"] = "0";
                    drnew["分析项目"] = LI.Text;
                    drnew["ItemID"] = LI.Value;
                    tempds.Rows.Add(drnew);
                    tempds.AcceptChanges();
                    itemname += LI.Text.Trim() + ",";
                    itemvalue += LI.Value.Trim() + ",";
                }

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
        getinf(cb_analysisList, grv_Item, ref tempds, ref itemname, ref itemvalue);
        getinf(cb_other, grv_Item, ref tempds, ref itemname, ref itemvalue);
        txt_Item.Text = itemname;
        hid_Item.Value = itemvalue;
        grv_Item.DataSource = tempds;
        grv_Item.DataBind();
        //grv_Item.Visible = ck_xcflag.Checked;
    }

    protected void drop_wrw_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAl.Station stationobj = new DAl.Station();
       DataTable dtcompanay= stationobj.GetStationByName(txt_SampleSource.Text.Trim());
        if(dtcompanay!=null)
            if (dtcompanay.Rows.Count > 0)
            {
               // MyStaVoid.BindList("bz", "id", "select t_hyClassParam.id,t_标准字典.bz+'('+t_hyClassParam.itemlst+')' as bz from t_hyClassParam inner join t_标准字典 on t_hyClassParam.bz=t_标准字典.id where hyid='" + strHyId + "'", drop_bz);

                string strSql = @"select t_hyClassParam.id,t_标准字典.bz+'('+t_hyClassParam.itemlist+')' as bz from t_hyClassParam inner join t_标准字典 on t_标准字典.id=t_hyClassParam.bz
                                where  wrwtype='"+drop_wrw.SelectedValue.ToString().Trim()+"' and flag=0";
                     DataSet ds = new MyDataOp(strSql).CreateDataSet();
                     drop_bz.DataSource = ds;
                     drop_bz.DataTextField = "bz";
                     drop_bz.DataValueField = "id";
                     drop_bz.DataBind();
                     ListItem li = new ListItem("请选择","");
                     drop_bz.Items.Add(li);
                     drop_bz.SelectedIndex = drop_bz.Items.Count - 1;
            }
    }
    protected void drop_bz_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Item.Text = "";
        string strsql = "select t_M_ANItemInf.id, AIName 分析项目, t_hyItem.itemid ItemID,t_hyItem.fw 标准 from t_hyItem inner join t_M_ANItemInf on  t_M_ANItemInf.id=t_hyItem.itemid where pid='" + drop_bz.SelectedValue.ToString().Trim() + "'";
        DataSet ds = new MyDataOp(strsql).CreateDataSet();
      
        
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                //DataColumn dc = new DataColumn("ID");
                //ds.Tables[0].Columns.Add(dc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txt_Item.Text += dr["分析项目"].ToString() + ",";
                    }
                    grv_Item.DataSource = ds;
                    grv_Item.DataBind();
                   
                }
            }
        }
    }
}
