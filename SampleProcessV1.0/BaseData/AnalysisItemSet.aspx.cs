﻿using System;
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

public partial class BaseData_AnalysisItemSet : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private DataSet dsmethod//所选择操作列记录对应的id
    {
        get { return (DataSet)ViewState["dsmethod"]; }
        set { ViewState["dsmethod"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面元素

            SetTxt();
            SetButton();
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClassEx order by ClassID", dropList_type);
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClassEx order by ClassID", dropList_type_S);
            ListItem townList = new ListItem("请选择", "");
            dropList_type_S.Items.Add(townList);
            dropList_type_S.SelectedValue = "";
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 100%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>分析项目管理</b></font>";
            #endregion
            Query();
           
        }        
    }

    //查询并填入GridView中
    private void Query()
    {
        string strS = "";
        if (dropList_type_Query.SelectedValue!="")
        {
            strS += " and t_M_AnalysisItemEx.type = '" + dropList_type_Query.SelectedValue + "'";
        }
        if (dropList_type_S.SelectedValue != "")
        {
            strS += " and t_M_AnalysisItemEx.ClassID = '" + dropList_type_S.SelectedValue + "'";
        }
        if (txt_AIName_S.Text.Trim() != "")
        {
            strS += " and (t_M_ANItemInf.AIName like '%" + txt_AIName_S.Text.Trim() + "%' or t_M_ANItemInf.AICode like '%" + txt_AIName_S.Text.Trim() + "%')";       
        }
        strS += "  order by t_M_AnalysisItemEx.ClassID";

        string strSql = "select t_M_AnalysisItemEx.id,t_M_AnalysisMainClassEx.ClassName, case when  t_M_ANItemInf.AICode is not null or  t_M_ANItemInf.AICode!=' ' then t_M_ANItemInf.AIName+'('+t_M_ANItemInf.AICode+')' else  t_M_ANItemInf.AIName end  AIName,t_M_AnalysisItemEx.type from t_M_AnalysisItemEx,t_M_AnalysisMainClassEx ,t_M_ANItemInf   where t_M_AnalysisItemEx.ClassID = t_M_AnalysisMainClassEx.ClassID and t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID";
        strSql += strS;

        DataColumn dc = new DataColumn("组别");
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        ds.Tables[0].Columns.Add(dc);
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
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["type"].ToString().Trim() == "0")
                    dr["组别"] = "其他";
                else
                    dr["组别"] = "常规";
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
            //修改字段标题
            e.Row.Cells[4].Text = "检测的类别";
            e.Row.Cells[5].Text = "检测项目";
            //e.Row.Cells[6].Text = "检测标准（方法）名称及编号（含年号）";

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
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[3].Text;
        string strSql;

        strSql = "select * from t_M_AnalysisItemEx where id = '" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
   
        if (ds.Tables.Count == 1)
        {
            strSql = "DELETE FROM t_M_AnalysisItemEx WHERE id = '" + strSelectedId + "'";
            MyDataOp mdo = new MyDataOp(strSql);
            if (mdo.ExecuteCommand())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
            }
            Query();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有该项目类型，数据删除失败！！')", true);
        }
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CleanAllText();
        lbl_Type.Text = "详细";
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        string strSql = @"select t_M_AnalysisItemEx.ClassID,t_M_AnalysisItemEx.num,t_M_ANItemInf.AICode,t_M_ANItemInf.AIName,t_M_ANItemInf.dw,t_M_AnalysisItemEx.type,t_M_AnalysisItemEx.dose,t_M_AnalysisItemEx.effectivetime,t_M_AnalysisItemEx.effectivedw from t_M_AnalysisItemEx  inner join t_M_ANItemInf on t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID
                        where t_M_AnalysisItemEx.id='" + strSelectedId + "'";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        dropList_type.SelectedValue = ds.Tables[0].Rows[0]["ClassID"].ToString();
        txt_AIName.Text = ds.Tables[0].Rows[0]["AIName"].ToString();
        txt_AICode.Text = ds.Tables[0].Rows[0]["AICode"].ToString();
        if (ds.Tables[0].Rows[0]["num"].ToString() != "&nbsp;" && ds.Tables[0].Rows[0]["num"].ToString() != "0")
        txt_num.Text = ds.Tables[0].Rows[0]["num"].ToString();
        if (ds.Tables[0].Rows[0]["dose"].ToString() != "&nbsp;")
        txt_dose.Text = ds.Tables[0].Rows[0]["dose"].ToString();
        if (ds.Tables[0].Rows[0]["dw"].ToString()!="")
        rbl_dw.SelectedIndex = int.Parse(ds.Tables[0].Rows[0]["dw"].ToString());
        drop_type.SelectedValue = ds.Tables[0].Rows[0]["type"].ToString();

        if (ds.Tables[0].Rows[0]["effectivetime"].ToString() != "&nbsp;" && ds.Tables[0].Rows[0]["effectivetime"].ToString() != "0")
            txt_Effnum.Text = ds.Tables[0].Rows[0]["effectivetime"].ToString();

        if (ds.Tables[0].Rows[0]["effectivedw"].ToString() != "")
            rbl_Effdw.SelectedIndex = int.Parse(ds.Tables[0].Rows[0]["effectivedw"].ToString()); 
        string str = "select distinct t_M_AnStandard.id,t_M_AnStandard.Standard from t_M_AIStandard inner join t_M_AnStandard on t_M_AnStandard.id=t_M_AIStandard.Standardid where AIID='" + strSelectedId + "'";
        dsmethod = new MyDataOp(str).CreateDataSet();
        cbl_Method.DataSource = dsmethod;
        cbl_Method.DataValueField = "id";
        cbl_Method.DataTextField = "Standard";
        cbl_Method.DataBind();
        for (int i = 0; i < cbl_Method.Items.Count; i++)
        {
            cbl_Method.Items[i].Selected = true;
        }
        AllTxtreadOnly();
        btn_OK.Text = "编辑";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
    }

    protected void dropList_type_S_SelectedIndexChanged(object sender, EventArgs e)
    {
         Query();       
    }
    #endregion

    protected void btn_Add_Click(object sender, EventArgs e)
    {      
        lbl_Type.Text = "添加";
        CleanAllText();
        btn_OK.Text = "确定";
        Query();
        string str = "select  top 1 t_M_AnStandard.id,t_M_AnStandard.Standard from t_M_AIStandard inner join t_M_AnStandard on t_M_AnStandard.Standard=t_M_AIStandard.Standard where AIID='" + strSelectedId + "'";
        DataSet tep = new MyDataOp(str).CreateDataSet();
        dsmethod = tep.Clone();
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

    protected void btn_Query_Click(object sender, EventArgs e)
    {
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

            int num = 0;
            int dw = 0;
            int hours=0;
            int Effnum = 0;
            int Effdw = 0;
            if (txt_num.Text.Trim() != "")
            {
                try
                {
                    num = int.Parse(txt_num.Text.Trim());
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请正确填写样品的有效性!');", true);
                }
                dw = rbl_dw.SelectedIndex;
                if (dw == 0)
                    hours = num;
                else
                    hours = num * 24;
            }
            //添加新纪录
            if (lbl_Type.Text == "添加")
            {
                //检查是否在监测项目表存在
                //TBD
                string strItem = "select id,AIName from t_M_ANItemInf where AIName='" + txt_AIName.Text.Trim() + "'";
                DataSet dscheckItem = new MyDataOp(strItem).CreateDataSet();
                if (dscheckItem.Tables[0].Rows.Count > 0)
                {
                    string AIID = dscheckItem.Tables[0].Rows[0][0].ToString().Trim();
                    //检查是否在分类表中存在
                    string str = "select id from t_M_AnalysisItemEx where AIID='" + AIID + "' and ClassID='" + dropList_type.SelectedValue.ToString().Trim() + "'";

                    DataSet dscheck = new MyDataOp(str).CreateDataSet();
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已存在该分析项目！请直接在原有分析项目上修改信息！');", true);

                    }
                    else
                    {
                        string name = "ClassID,AIName,num,dw,hours,AIID,type,dose";
                        string values = "'" + dropList_type.SelectedValue.ToString() + "','" + txt_AIName.Text + "','" + num + "','" + dw + "','" + hours + "','" + AIID + "','" + drop_type.SelectedValue.ToString() + "','" + txt_dose.Text.Trim() + "'";
                        if (txt_AICode.Text.Trim() != "")
                        {
                            name += @"AICode,";
                            values += ",'" + txt_AICode.Text + "'";
                        }
                        if (txt_Effnum.Text.Trim() != "")
                        {
                            name += ",effectivetime,effectivedw";
                            values += ",'" + txt_Effnum.Text.Trim() + "','" + rbl_Effdw.SelectedValue.Trim() + "'";
                        }
                        if (txt_Remark.Text.Trim() != "")
                        {
                            name += ",Remark";
                            values += ",'" + txt_Remark.Text.Trim() + "'";
                        }
                        string strSql = @"insert into t_M_AnalysisItemEx("+name+")  values("+values+")";
                    
                        MyDataOp mdo = new MyDataOp(strSql);
                        bool blSuccess = mdo.ExecuteCommand();
                        if (blSuccess == true)
                        {
                            string strc = "select id,AIName from t_M_AnalysisItemEx where AIID='" + AIID + "' and ClassID='" + dropList_type.SelectedValue.ToString().Trim() + "'";
                            DataSet dsc = new MyDataOp(strc).CreateDataSet();
                            strSelectedId = dsc.Tables[0].Rows[0][0].ToString().Trim();
                            string[] strlist = new string[cbl_Method.Items.Count + 1];
                            int j = 0;
                            strlist.SetValue("delete from t_M_AIStandard where AIID='" + strSelectedId + "'", j++);
                            for (int i = 0; i < cbl_Method.Items.Count; i++)
                            {
                                if (cbl_Method.Items[i].Selected)
                                {
                                    string temp = " insert into t_M_AIStandard(AIID,Standardid,Standard)values('" + strSelectedId + "','" + cbl_Method.Items[i].Value.ToString().Trim() + "','" + cbl_Method.Items[i].Text.Trim() + "')";
                                    strlist.SetValue(temp, j++);
                                }
                            }
                            if (j > 0)
                            {
                                if (mdo.DoTran(j, strlist))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加成功！');", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('分析项目添加成功，分析方法配置失败！');", true);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                        }
                        txt_AIName.Text = "";
                        txt_AICode.Text = "";

                        Query();
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('分析项目字段中没有改项，请先添加！');", true);
                    return;
                }
            }

            //编辑记录
            if (lbl_Type.Text == "编辑")
            {
                 //检查是否在监测项目表存在
                //TBD
                string strItem = "select id,AIName from t_M_ANItemInf where AIName='" + txt_AIName.Text.Trim() + "'";
                DataSet dscheckItem = new MyDataOp(strItem).CreateDataSet();
                if (dscheckItem.Tables[0].Rows.Count > 0)
                {
                    string AIID = dscheckItem.Tables[0].Rows[0][0].ToString().Trim();
                  
                string str = "select id,AIName from t_M_AnalysisItemEx where AIID='" + AIID + "' and ClassID='" + dropList_type.SelectedValue.ToString().Trim() + "' and id!='" + strSelectedId + "'";

                    DataSet dscheck = new MyDataOp(str).CreateDataSet();
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已存在同名称的分析项目！请直接在原有分析项目上修改信息！');", true);

                    }
                    else
                    {
                        string strUpdate =" ClassID = '" + dropList_type.SelectedValue.ToString() + "',AIName='" + txt_AIName.Text + "',num='" + num + "',dw='" + dw + "',hours='" + hours + "',AIID='"+AIID+"',type='"+drop_type.SelectedValue.ToString()+"',dose='"+txt_dose.Text.Trim()+"'";
                       
                        if (txt_AICode.Text.Trim() != "")
                        {
                            strUpdate += ",AICode='" + txt_AICode.Text + "'";
                            
                        }
                        else
                            strUpdate += ",AICode=null";
                        if (txt_Effnum.Text.Trim() != "")
                        {
                            strUpdate += ",effectivetime='" + txt_Effnum.Text + "',effectivedw='" + rbl_Effdw.SelectedValue.Trim() + "'";
                           
                        }
                        else
                            strUpdate += ",effectivetime=null,effectivedw=null";
                          
                        if (txt_Remark.Text.Trim() != "")
                        {
                            strUpdate += ",Remark='" + txt_Remark.Text.Trim() + "'";

                        }
                        else
                            strUpdate += ",Remark=null";
                       
                        string strSql = @" update t_M_AnalysisItemEx 
                            set " + strUpdate + " where id = '" + strSelectedId + "'";               
                        MyDataOp mdo = new MyDataOp(strSql);
                        bool blSuccess = mdo.ExecuteCommand();
                        if (blSuccess == true)
                        {
                            string[] strlist = new string[cbl_Method.Items.Count + 1];
                            int j = 0;
                            strlist.SetValue("delete from t_M_AIStandard where AIID='" + strSelectedId + "'", j++);
                            for (int i = 0; i < cbl_Method.Items.Count; i++)
                            {
                                if (cbl_Method.Items[i].Selected)
                                {
                                    string temp = " insert into t_M_AIStandard(AIID,Standardid,Standard)values('" + strSelectedId + "','" + cbl_Method.Items[i].Value.ToString().Trim() + "','" + cbl_Method.Items[i].Text.Trim() + "')";
                                    strlist.SetValue(temp, j++);
                                }
                            }
                            if (j > 0)
                            {
                                if (mdo.DoTran(j, strlist))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('分析项目编辑成功！');", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('分析项目编辑成功，分析方法配置失败！');", true);
                                }
                            }
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('分析方法必须填写！');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑失败！');", true);
                        }
                        txt_AIName.Text = "";
                        txt_AICode.Text = "";
                        Query();
 }
            }
            
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('分析项目字段中没有该项，请先添加！');", true);
                return;
            }
        }
            #endregion
        }
    }

    #region 对TextBox以及DropDownList等控件进行的一些通用的重复性的操作，包括绑定事件和合法性校验
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
        //dropList_type.SelectedValue = "";
        txt_AIName.Text = "";
        txt_AICode.Text = "";
        txt_num.Text = "";
        rbl_dw.SelectedIndex = 0;
        txt_dose.Text = "";
        txt_Effnum.Text = "";
        rbl_Effdw.SelectedIndex = 0;

    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        dropList_type.Enabled = false;
        txt_AIName.ReadOnly = true;
        txt_AICode.ReadOnly = true;
        txt_dose.ReadOnly = true;
         txt_Effnum.ReadOnly =true;
         rbl_Effdw.Enabled = false;
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        dropList_type.Enabled = true;
        txt_AIName.ReadOnly = false;
        txt_AICode.ReadOnly = false;
        txt_dose.ReadOnly = false;
        txt_Effnum.ReadOnly =false ;
        rbl_Effdw.Enabled = true;
    }
    private void SetTxt()//设置详细页面中的TextBox的一些属性
    {
        txt_AIName.MaxLength = 20;
        txt_AICode.MaxLength = 20;
        txt_dose.MaxLength = 20;
    }
    private void SetButton()//根据权限设置读写相关的按钮是否可用
    {
        //if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) >6 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
        if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
        {
            btn_Add.Enabled = false;
            btn_OK.Enabled = false;
        }
    }
    #endregion
    protected void txt_AIName_S_TextChanged(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_choose_Click(object sender, EventArgs e)
    {
        panel_choose.Visible = true;
        //string str = "select * from t_M_AnStandard order by standard";
        //DataSet ds = new MyDataOp(str).CreateDataSet();
        //cbl_choose.DataSource = ds;
        //cbl_choose.DataValueField = "id";
        //cbl_choose.DataTextField = "Standard";
        //cbl_choose.DataBind();
        //foreach (ListItem li in cbl_choose.Items)
        //{

        //    DataRow[] dr = dsmethod.Tables[0].Select("id='" + li.Value + "'");

        //        if (dr.Length>0)
        //        {
        //           li.Selected=true;

        //        }
           
        //    else
        //    {
        //        li.Selected = false;

        //    }
           
        //}

    }

    protected void btn_Close_Click(object sender, ImageClickEventArgs e)
    {
        panel_choose.Visible = false;
    }
    //方法选择
    //protected void cbl_choose_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    foreach (ListItem li in cbl_choose.Items)
    //    {

    //        DataRow[] dr = dsmethod.Tables[0].Select("id='" + li.Value + "'");
    //        if (li.Selected)
    //        {

    //            if (dr.Length == 0)
    //            {
    //                DataRow dradd = dsmethod.Tables[0].NewRow();
    //                dradd["id"] = li.Value;
    //                dradd["Standard"] = li.Text;
    //                dsmethod.Tables[0].Rows.Add(dradd);

    //            }
    //        }
    //        else
    //        {
    //            if (dr.Length > 0)
    //            {

    //                dsmethod.Tables[0].Rows.Remove(dr[0]);

    //            }
    //        }
    //        dsmethod.AcceptChanges();
    //    }

    //    cbl_Method.DataSource = dsmethod;
    //    cbl_Method.DataValueField = "id";
    //    cbl_Method.DataTextField = "Standard";
    //    cbl_Method.DataBind();
    //    for (int i = 0; i < cbl_Method.Items.Count; i++)
    //    {
    //        cbl_Method.Items[i].Selected = true;
    //    }
    //}
    //protected void cbl_Method_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    foreach (ListItem li in cbl_choose.Items)
    //    {

    //        DataRow[] dr = dsmethod.Tables[0].Select("id='" + li.Value + "'");
    //        if (li.Selected)
    //        {

    //            if (dr.Length == 0)
    //            {
    //                DataRow dradd = dsmethod.Tables[0].NewRow();
    //                dradd["id"] = li.Value;
    //                dradd["Standard"] = li.Text;
    //                dsmethod.Tables[0].Rows.Add(dradd);

    //            }
    //        }
    //        else
    //        {
    //            if (dr.Length > 0)
    //            {

    //                dsmethod.Tables[0].Rows.Remove(dr[0]);

    //            }
    //        }
    //        dsmethod.AcceptChanges();
    //    }

    //    cbl_Method.DataSource = dsmethod;
    //    cbl_Method.DataValueField = "id";
    //    cbl_Method.DataTextField = "Standard";
    //    cbl_Method.DataBind();
    //    for (int i = 0; i < cbl_Method.Items.Count; i++)
    //    {
    //        cbl_Method.Items[i].Selected = true;
    //    }
    //}
    protected void btn_addm_Click(object sender, EventArgs e)
    {

        string str = "SELECT    id ,  Standard FROM  t_M_AnStandard WHERE     (Standard ='" + txt_method.Text.Trim() + "') ";
        DataSet dsm = new MyDataOp(str).CreateDataSet();
        if (dsm.Tables[0].Rows.Count > 0)
        {
            DataRow[] dr = dsmethod.Tables[0].Select("id='" + dsm.Tables[0].Rows[0][0].ToString() + "'");

            if (dr.Length == 0)
            {
                DataRow dradd = dsmethod.Tables[0].NewRow();
                dradd["id"] = dsm.Tables[0].Rows[0][0].ToString();
                dradd["Standard"] = dsm.Tables[0].Rows[0][1].ToString();
                dsmethod.Tables[0].Rows.Add(dradd);

            }


            cbl_Method.DataSource = dsmethod;
            cbl_Method.DataValueField = "id";
            cbl_Method.DataTextField = "Standard";
            cbl_Method.DataBind();
            for (int i = 0; i < cbl_Method.Items.Count; i++)
            {
                cbl_Method.Items[i].Selected = true;
            }
        }
    }
}
