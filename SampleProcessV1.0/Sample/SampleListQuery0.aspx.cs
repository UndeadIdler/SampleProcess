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
using System.Text;

using System.Management;
using System.Collections.Generic;
using OWC11;
using System.IO;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using WebApp.Components;


using WebApp.Components;

public partial class Sample_SampleListQuery0 : System.Web.UI.Page
{
    private int strSelectedId//所选择操作列记录对应的id
    {
        get { return (int)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strSampleId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSampleId"]; }
        set { ViewState["strSampleId"] = value; }
    }
    private DataSet ds_dayin//所选择操作列记录对应的id
    {
        get { return (DataSet)ViewState["ds_dayin"]; }
        set { ViewState["ds_dayin"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_endtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
           // txt_receivetime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_QueryEndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_QueryTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_QueryEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //txt_ReportTime.Attributes.Add("OnFocus", "javascript:calendar()");
            //txt_AccessTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //txt_ReportTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClassEx order by ClassCode asc", txt_type);
            ListItem choose = new ListItem("全部", "-1");
            txt_type.SelectedItem.Selected = false;
            txt_type.Items.Add(choose);
            txt_type.Items.FindByValue("-1").Selected = true;
            //DropList_AnalysisMainItem_SelectedIndexChanged(null, null);
            MyStaVoid.BindList("ItemName", "ItemID", "select ItemID, ItemName from t_M_ItemInfo  order by ItemName ", txt_item);
            txt_item.Items.Add(choose);
            txt_item.Items.FindByValue("-1").Selected = true;
            //MyStaVoid.BindList("SampleType", "TypeID", "select TypeID, SampleType from t_M_SampleType  order by SampleType ", txt_type);
            //txt_type.Items.Add(choose);
            //txt_type.Items.FindByValue("-1").Selected = true;
            //MyStaVoid.BindList("Status", "StatusID", "select StatusID, Status from t_M_StatusInfo  order by StatusID ", drop_status);
            //drop_status.Items.Add(choose);
            //drop_status.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("ClientName", "id", "select id,ClientName from t_M_ClientInfo  order by id ", drop_client);
            drop_client.Items.Add(choose);
            drop_client.Items.FindByValue("-1").Selected = true;


            btn_query_Click(null,null);

        }
    }
    #region 样品列表

   
    
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        btn_query_Click(null, null);
    }
   
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            //CheckBox cb = new CheckBox();
            //cb.ID = "cb";
           //e.Row.Cells[0].Controls.Add(cb);
            e.Row.Cells[0].Text = id.ToString();
           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 

            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            //e.Row.Cells[15].Visible = false;

            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
            //e.Row.Cells[19].Visible = false;
           
        }
    }
   
   
    #endregion

    #region 其它函数


    
   
    #endregion

    
    
    

    #endregion
    protected void grv_dayin_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {

            System.Web.UI.WebControls.CheckBox cbl_Item = new System.Web.UI.WebControls.CheckBox();
            cbl_Item.ID = "cb_all";
            cbl_Item.Text = "序号";
            cbl_Item.CheckedChanged += cbl_Item_CheckedChanged;
            cbl_Item.AutoPostBack = true;
            e.Row.Cells[0].Controls.Add(cbl_Item);

            TableCell headerset1 = new TableCell();
            headerset1.Text = "标签打印项选择";
            headerset1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset1.Width = 60;
            e.Row.Cells.Add(headerset1);
            TableCell headerset = new TableCell();
            headerset.Text = "追加";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

            TableCell headerDel = new TableCell();

            headerDel.Text = "移除";
                headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                headerDel.Width = 30;
                e.Row.Cells.Add(headerDel);

           


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

           
            //TableCell tabcDetail1 = new TableCell();
            //tabcDetail1.Width = 60;
            //tabcDetail1.Style.Add("text-align", "center");
            System.Web.UI.WebControls.CheckBox cbl_Item = new System.Web.UI.WebControls.CheckBox();
            cbl_Item.ID = "cb";
            cbl_Item.Text = id.ToString();
           // tabcDetail1.Controls.Add(cbl_Item);
            e.Row.Cells[0].Controls.Add(cbl_Item);

           

            //手动添加详细和删除按钮
            TableCell tabcDetail2 = new TableCell();
            tabcDetail2.Width = 60;
            tabcDetail2.Style.Add("text-align", "center");
            ImageButton ibtnDetail2 = new ImageButton();
            ibtnDetail2.ImageUrl = "~/images/Detail.gif";
            ibtnDetail2.CommandName = "Edit";
            tabcDetail2.Controls.Add(ibtnDetail2);
            e.Row.Cells.Add(tabcDetail2);
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
            ibtnDetail.CommandName = "Select";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定移除该项吗？')) return false;");
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[7].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[8].Visible = false;

            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
            //e.Row.Cells[19].Visible = false;
        }
    }

    protected void cbl_Item_CheckedChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox cbl_all = sender as System.Web.UI.WebControls.CheckBox;
        foreach (GridViewRow gvr in grv_dayin.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox cbl = gvr.Cells[0].FindControl("cb") as System.Web.UI.WebControls.CheckBox;
                    if (cbl_all.Checked)
                    {
                        cbl.Checked = true;
                    }
                    else
                    {
                        cbl.Checked = false;
                    }
                }

            }
        
    }
    protected void grv_dayin_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
      // strSelectedId = grv_dayin.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
       DataRow  dr= ds_dayin.Tables[0].NewRow();
       for (int i = 0; i < ds_dayin.Tables[0].Columns.Count;i++ )
       {
           if (grv_dayin.Rows[e.NewSelectedIndex].Cells[i + 1].Text.Trim() != "&nbsp;")
               dr[i] = grv_dayin.Rows[e.NewSelectedIndex].Cells[i + 1].Text;
           else
               dr[i] = "";
       }
        
       ds_dayin.Tables[0].Rows.Add(dr);
       ds_dayin.Tables[0].AcceptChanges();
       DataView dv = new DataView();
       dv.Table = ds_dayin.Tables[0];
       
       dv.Sort = "样品编号";
       grv_dayin.DataSource = dv;
       grv_dayin.DataBind();
       ds_dayin.Tables[0].Clear();
       ds_dayin.Clear();
      // DataTable dt= grv_dayin.DataSource as DataTable;
       for (int j = 0; j < grv_dayin.Rows.Count; j++)
       {
           DataRow dradd = ds_dayin.Tables[0].NewRow();
           for (int i = 0; i < ds_dayin.Tables[0].Columns.Count; i++)
           {
               if (grv_dayin.Rows[j].Cells[i + 1].Text.Trim() != "&nbsp;")
                   dradd[i] = grv_dayin.Rows[j].Cells[i + 1].Text;
               else
                   dradd[i] = "";
           }
           ds_dayin.Tables[0].Rows.Add(dradd);
       }
       ds_dayin.Tables[0].AcceptChanges();

    }
    protected void grv_dayin_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ds_dayin.Tables[0].Rows.RemoveAt(e.RowIndex);
        ds_dayin.Tables[0].AcceptChanges();
        DataView dv = new DataView();
        dv.Table = ds_dayin.Tables[0];
        dv.Sort = "样品编号";
       
       
        grv_dayin.DataSource = dv;
        grv_dayin.DataBind();

    }
    protected void grv_dayin_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lbl_sample.Text = grv_dayin.Rows[e.NewEditIndex].Cells[1].Text.Trim();
           strSelectedId = e.NewEditIndex;
           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);
             string getitemstr = "select Case when AICode!='' then AICode else AIName end as AIName,MonitorItem,xcflag from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + grv_dayin.Rows[e.NewEditIndex].Cells[1].Text.Trim()+ "' and delflag=0";
            DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
            cbl_Item.DataSource = dsitem;
            cbl_Item.DataTextField = "AIName";
            cbl_Item.DataValueField = "MonitorItem";
            cbl_Item.DataBind();
            cbl_Item.RepeatDirection = RepeatDirection.Horizontal;
            cbl_Item.RepeatColumns = 6;
            if (cbl_Item.Items.Count <= 12)
            {
                for (int i = 0; i < cbl_Item.Items.Count; i++)
                {
                    cbl_Item.Items[i].Selected = true;
                }
            }
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        ds_dayin.Tables[0].Rows[strSelectedId]["标签打印项"] = "";
        int j = 0;
        for(int i=0;i<cbl_Item.Items.Count;i++)
        {
            if(cbl_Item.Items[i].Selected)
            {
                j++;
                ds_dayin.Tables[0].Rows[strSelectedId]["标签打印项"] += cbl_Item.Items[i].Text + ",";
            }
        }
        if (j == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择监测项！');", true);
            return;
        }
        ds_dayin.AcceptChanges();
        grv_dayin.DataSource = ds_dayin;
        grv_dayin.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);


    }
    protected void grv_dayin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //CheckBoxList cbl_Item = e.Row.Cells[7].FindControl("cbl_Item") as CheckBoxList;
        //    //string getitemstr = "select Case when AICode!='' then AICode else AIName end as AIName,MonitorItem,xcflag from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + e.Row.Cells[1].Text.Trim() + "' and delflag=0";
        //    //DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
        //    //cbl_Item.DataSource = dsitem;
        //    //cbl_Item.DataTextField = "AIName";
        //    //cbl_Item.DataValueField = "MonitorItem";
        //    //cbl_Item.DataBind();
        //    //cbl_Item.RepeatDirection = RepeatDirection.Horizontal;
        //    //cbl_Item.RepeatColumns = 6;
        //    //if (cbl_Item.Items.Count <= 6)
        //    //{
        //    //    for (int i = 0; i < cbl_Item.Items.Count; i++)
        //    //    {
        //    //        cbl_Item.Items[i].Selected = true;
        //    //    }
        //    //}

        //}
    }
   

    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
       string strSample="";//样品编号
        string strDate="";//接样时间
        string strStatus = "";//样品状态
        string strItem="";//项目类型
        string strType="";//样品名称
        string strClient="";//委托单位
       
        string strAanlysisStatus = "";//样品分析状态
        string strUrgent = "";//按紧急程度
        if (Drop_Urgent.SelectedValue != "-1")
        {
            if (Drop_Urgent.SelectedValue=="1")
            strUrgent = "and t_M_ReporInfo.Ulevel=1";
            else
                strUrgent = "and (t_M_ReporInfo.Ulevel<>1 or t_M_ReporInfo.Ulevel is null)";
        }
        if(txt_samplequery.Text!="")//按样品编号

            strSample = "and t_M_SampleInfor.SampleID like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text.Trim() != "" && txt_QueryEndTime.Text.Trim() != "")//按采样时间
        {
            DateTime start=DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00");
            DateTime end=DateTime.Parse(txt_QueryEndTime.Text.Trim() + " 23:59:59");
            strDate = " and t_M_SampleInfor.AccessDate between '" + start + "' and '" + end + "'";
            //strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        }

        if(txt_item.SelectedValue!="-1")// 按项目类型
        {
            strItem = " and t_M_ReporInfo.ItemType='" + txt_item.SelectedValue + "' ";
        }
        if (txt_type.SelectedValue != "-1")//按样品类型
        {
            strType = " and t_M_SampleInfor.TypeID='" + txt_type.SelectedValue + "' ";
        }
        if (drop_client.SelectedValue != "-1")//按委托单位
        {
            strClient = " and t_M_ReporInfo.ClientID='" + drop_client.SelectedValue + "' ";
        }
       
       
      
        ////按选中的分析项目查询
        string strtitle = "";
      
        string cstr = "";
       
            if (cstr == "")
            { 
                cstr = " and 1=1";
            }
           
            cstr = strUrgent + strSample + strDate + strStatus + strItem + strType + strClient + strAanlysisStatus + strtitle;
            string strSql = "SELECT t_M_SampleInfor.SampleID AS 样品编号, t_M_ReporInfo.projectname 项目名称,t_M_SampleInfor.SampleDate AS 采样时间,t_M_SampleInfor.SampleProperty 样品性状,t_M_SampleInfor.SampleAddress 采样点 ," +
             "t_M_AnalysisMainClassEx.ClassName AS 样品类型, " +
            " t_M_SampleInfor.xcflag,t_M_SampleInfor.num 数量" +
       " FROM  t_M_SampleInfor  INNER JOIN" +
            " t_M_AnalysisMainClassEx ON " +
           "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_M_ReporInfo on t_M_ReporInfo.id=t_M_SampleInfor.ReportID " +

       " WHERE 1=1 " + cstr+
       " ORDER BY t_M_SampleInfor.SampleID";
            DataSet ds = new MyDataOp(strSql).CreateDataSet();

            DataColumn dccc = new DataColumn("分析项目");
            ds.Tables[0].Columns.Add(dccc);
            DataColumn dcc = new DataColumn("现场分析项目");
            ds.Tables[0].Columns.Add(dcc);
            DataColumn dcc2 = new DataColumn("分析项目代码");
            ds.Tables[0].Columns.Add(dcc2);
           
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string getitemstr = "select AIName,AICode,MonitorItem,xcflag from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MonitorItemDetail.MonitorItem where  SampleID='" + dr["样品编号"].ToString() + "' and delflag=0";
                DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
                if (dsitem != null && dsitem.Tables.Count > 0)
                {
                    foreach (DataRow drr in dsitem.Tables[0].Rows)
                    {
                        dr["分析项目"] += drr[0].ToString() + ",";
                        if (drr[3].ToString() == "1")
                            dr["现场分析项目"] += drr[0].ToString() + ",";
                        else
                        {
                            if (drr[1].ToString() != "")
                            {
                                dr["分析项目代码"] += drr[1].ToString() + ",";
                            }
                            else
                                dr["分析项目代码"] += drr[0].ToString() + ",";
                        }
                    }
                }
               
            }
           
            ds_dayin = ds.Copy();
            DataColumn dc = new DataColumn("标签打印项");
            ds_dayin.Tables[0].Columns.Add(dc);
            //string specialstr = "select AIName,AICode,MonitorItem from t_SpecialItem";
              
           
            foreach (DataRow dr in ds_dayin.Tables[0].Rows)
            {
               
                    dr["标签打印项"] = dr["分析项目代码"].ToString();

                    int n = int.Parse(dr["数量"].ToString());
                    if (n > 1)
                    {
                        for (int i = 1; i < n; i++)
                        {
                            DataRow drnew = ds.Tables[0].NewRow();
                            foreach (DataColumn dcname in ds.Tables[0].Columns)
                            {
                                drnew[dcname.ColumnName] = dr[dcname.ColumnName];
                            }
                            ds.Tables[0].Rows.Add(drnew);
                        }
                    }
               
            }
         string specialstr = "select case when AICode is not null then AICode else AIName end 分析项目,relatedItem from  t_M_ANItemInf  inner join t_SpecialItem on t_SpecialItem.itemid=t_M_ANItemInf.id";
                DataSet ds_sitem = new MyDataOp(specialstr).CreateDataSet();
                
            DataRow[] drsele = ds_dayin.Tables[0].Select("数量<>1");
            foreach (DataRow dr in drsele)
            {
               
                //    string[] list = dr["分析项目代码"].ToString().Split(',');
                //    string[] strlist = new string[list.Length];
                //    int m = 0;
                //    int j = 0;
                //foreach(DataRow drspecial in ds_sitem.Tables[0].Rows)
                //{
                //    foreach (string str in list)
                //    {
                //        if (str != "")
                //       {
                //            if(str==drspecial[0].ToString())
                //            {
                //                for (int k=0;k< list.Length;k++)
                //                {
                //                   if(str==drspecial[1].ToString())
                //                   {  strlist.SetValue(drspecial[0]+","+drspecial[1], m++);
                //                      list.SetValue("", k);   
                //                    }
                //                   else
                //                   {
                //                     strlist.SetValue(drspecial[0], m++);
                //                        list.SetValue("", j);
                //                   }
                               
                //            }
                //        }
                //    }
                //}
                //    int n = int.Parse(dr["数量"].ToString());
                //    if (n > 1)
                //    {
                //        for (int i = 1; i < n; i++)
                //        {
                //            if (i < strlist.Length)
                //            {
                //                if (strlist[i] != "")
                //                {
                //                    DataRow drnew = ds_dayin.Tables[0].NewRow();
                //                    foreach (DataColumn dcname in ds_dayin.Tables[0].Columns)
                //                    {
                //                        if (dcname.ColumnName != "标签打印项")
                //                            drnew[dcname.ColumnName] = dr[dcname.ColumnName];
                //                        else
                //                        {
                //                            if (i < strlist.Length)
                //                            {
                //                                if (strlist[i] != "")
                //                                    drnew[dcname.ColumnName] = strlist[i - 1];

                //                            }
                //                            else
                //                                drnew[dcname.ColumnName] = dr[dcname.ColumnName];
                //                        }
                //                    }

                //                    DataRow[] checkrow = ds_dayin.Tables[0].Select("标签打印项='" + drnew["标签打印项"] + "'");
                //                    if (checkrow.Length <= 0)
                //                    {
                //                        ds_dayin.Tables[0].Rows.Add(drnew);
                //                    }
                //                }
                //                else
                //                    continue;
                //            }
                //        }
                //    }
                ////}
                //else
                //{

                    int n = int.Parse(dr["数量"].ToString());
                    if (n > 1)
                    {
                        for (int i = 1; i < n; i++)
                        {
                            DataRow drnew = ds_dayin.Tables[0].NewRow();
                            foreach (DataColumn dcname in ds_dayin.Tables[0].Columns)
                            {
                                drnew[dcname.ColumnName] = dr[dcname.ColumnName];
                            }
                            ds_dayin.Tables[0].Rows.Add(drnew);
                        }
                    }
                
            }
            ds_dayin.AcceptChanges();
            grv_dayin.Visible = true;
            if (ds_dayin.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds_dayin.Tables[0].Rows.Add(ds_dayin.Tables[0].NewRow());
            grv_dayin.DataSource = ds_dayin;
            grv_dayin.DataBind();
            int intColumnCount = grv_dayin.Rows[0].Cells.Count;
            grv_dayin.Rows[0].Cells.Clear();
            grv_dayin.Rows[0].Cells.Add(new TableCell());
            grv_dayin.Rows[0].Cells[0].ColumnSpan = intColumnCount;
            
        }
        else
        {
            grv_dayin.Visible = true;
            DataView dv = new DataView();
            dv.Table = ds_dayin.Tables[0];
            dv.Sort = "样品编号";


            grv_dayin.DataSource = dv;
            grv_dayin.DataBind();
            
        }
        ds.Dispose();
    }
    protected void btn_print_Click(object sender, EventArgs e)
    {
          int NO=1;
        if(txt_NO.Text.Trim()!="")
      try
      {
          NO = int.Parse(txt_NO.Text);
          if (NO > 8)
          {
              ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", " alert('标签开始项输入有误');", true);
              return;
          }
      }
        catch
      {
          ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", " alert('标签开始项输入有误');", true);
          return;
        }
        List<Entity.Sample> SampleList = new List<Entity.Sample>();
        int num =48;
        foreach (GridViewRow gvr in grv_dayin.Rows)
        {
            System.Web.UI.WebControls.CheckBox cb = gvr.Cells[0].FindControl("cb") as System.Web.UI.WebControls.CheckBox;
            if (cb.Checked)
            {
               
                    Entity.Sample entity = new Entity.Sample();
                    if ( gvr.Cells[1].Text.Trim()!="&nbsp;")
                    entity.SampleID = gvr.Cells[1].Text.Trim();
                    if (gvr.Cells[2].Text.Trim() != "&nbsp;")
                    entity.projectname = gvr.Cells[2].Text.Trim();
                    if (gvr.Cells[3].Text.Trim() != "&nbsp;")
                    entity.SampleDate = DateTime.Parse(gvr.Cells[3].Text.Trim());
                    //if (gvr.Cells[4].Text.Trim() != "&nbsp;")
                    //    entity.SampleProperty = gvr.Cells[4].Text.Trim();
                    //else

                    //    entity.SampleProperty = "";
                    if (gvr.Cells[5].Text.Trim() != "&nbsp;")
                    {
                        //if (entity.SampleProperty != "")
                        //    entity.SampleProperty += ";";
                        entity.SampleProperty += gvr.Cells[5].Text.Trim();

                    }
                    else
                        entity.SampleProperty = "";
                    string itemstr = gvr.Cells[12].Text.Trim();
                    string[] itemlist = itemstr.Split(',');
                    if (itemlist.Length > 13)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", " alert('监测项数不能大于12项');", true);
                        return;
                    }
                    for (int i = 0; i < itemlist.Length; i++)
                    {
                        if (itemlist[i] != "")
                        {
                            Entity.SampleItem item = new Entity.SampleItem();
                            item.Remark = itemlist[i];
                            entity.SampleItemList.Add(item);
                        }
                    }
                    if (entity.SampleItemList.Count > 0)
                    {
                        SampleList.Add(entity);
                    }
            }
        }
        if (SampleList.Count > 0)
        {
            num = SampleList.Count;
            if (NO-1+ num<=48)
                print(SampleList,NO-1);
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", " alert('标签打印不能超过6页纸！');", true);
                return;
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", " alert('请选择需要打印的标签！');", true);
            return;
        }
    }
    private void print(List<Entity.Sample> samplelist,int num)
    {
        try
        {
            RemoveFiles(Server.MapPath(".") + "\\temp\\");
        }
        catch (Exception e)
        {
            Log.log alog = new Log.log();
            alog.Log(e.Message.ToString() + DateTime.Now.ToString());
        }

        try
        {
            Random rd = new Random();
            int oid = rd.Next(10000);
            Word.Application app = new Word.Application();
            Word.Document doc = new Document();
            object missing = System.Reflection.Missing.Value;


            object IsSave = true;
            try
            {
                string DocPath = Server.MapPath("../");



                string TemplateFile = "";

                TemplateFile = DocPath + "Sample\\template\\SampleTemplate.doc";


                //生成的具有模板样式的新文件
                string FileName = DocPath + "Sample\\temp\\" + oid.ToString() + ".doc";

                File.Copy(TemplateFile, FileName);


                object Obj_FileName = FileName;

                object Visible = false;

                object ReadOnly = false;



                //打开文件  

                doc = app.Documents.Open(ref Obj_FileName,

                ref missing, ref ReadOnly, ref missing,

                ref missing, ref missing, ref missing, ref missing,

                ref missing, ref missing, ref missing, ref Visible,

                ref missing, ref missing, ref missing,

                ref missing);

                doc.Activate();

                for (int j = 1; j <= samplelist.Count; j++)
                {
                    //找书签，赋值
                     //    //样品编号
                      object name = "SampleID1_" + (j + num);
                        if (doc.Bookmarks.Exists(name.ToString()) == true)
                        {
                          Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref name);
                            itemstr.Select();
                            itemstr.Range.Text = samplelist[j - 1].SampleID;
                         }

                        //项目名称
                        object ProjectName = "ProjectName1_" + (j + num);
                        if (doc.Bookmarks.Exists(ProjectName.ToString()) == true)
                        {
                            Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref ProjectName);
                            itemstr.Select();
                            itemstr.Range.Text = samplelist[j - 1].projectname;
                            // break;
                        }
                        //样品性状
                        object Profile = "Profile1_" + (j + num);
                        if (doc.Bookmarks.Exists(Profile.ToString()) == true)
                        {
                            Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref Profile);
                            itemstr.Select();
                            itemstr.Range.Text = samplelist[j - 1].SampleProperty;
                            //break;
                        }
                        //样品性状
                        object SampleDate = "SampleDate1_" + (j + num);
                        if (doc.Bookmarks.Exists(SampleDate.ToString()) == true)
                        {
                            Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref SampleDate);
                            itemstr.Select();
                            itemstr.Range.Text = samplelist[j - 1].SampleDate.ToString("yyyy-MM-dd");
                            // break;
                        }
                        for (int p = 1; p <= samplelist[j - 1].SampleItemList.Count; p++)
                        {
                            object Item = "Item" + (j + num) + "_" + p;
                            if (doc.Bookmarks.Exists(Item.ToString()) == true)
                            {
                                Word.Bookmark itemstr = doc.Bookmarks.get_Item(ref Item);
                                itemstr.Select();
                                itemstr.Range.Text = samplelist[j - 1].SampleItemList[p - 1].Remark;
                            }
                        } 
                }
                doc.Save();
               
                doc.Close(ref IsSave, ref missing, ref missing);
                app.Application.Quit(ref missing, ref missing, ref missing);
                
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('temp/" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);
            }
            catch (Exception mes)
            {
                doc.Close(ref IsSave, ref missing, ref missing);
                app.Application.Quit(ref missing, ref missing, ref missing);
                Log.log alog = new Log.log();
                alog.Log(mes.Message.ToString() + DateTime.Now.ToString());
            }
        }
        catch (Exception mes)
        {
            Log.log alog = new Log.log();
            alog.Log(mes.Message.ToString() + DateTime.Now.ToString());
        }

        
    }
    /// <summary>
    /// 删除超时文件
    /// </summary>
    /// <param name="strPath"></param>
    private void RemoveFiles(string strPath)
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] fiArr = di.GetFiles();
        foreach (System.IO.FileInfo fi in fiArr)
        {
            if (fi.Extension.ToString() == ".xls" || fi.Extension.ToString() == ".doc")
            {
                // if file is older than 2 minutes, we'll clean it up
                TimeSpan min = new TimeSpan(0, 0, 2, 0, 0);
                if (fi.CreationTime < DateTime.Now.Subtract(min))
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
                }
            }
        }
    }
   

    
}