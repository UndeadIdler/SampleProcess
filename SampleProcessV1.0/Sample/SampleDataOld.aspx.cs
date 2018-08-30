using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.SqlClient;
 using DataAccess;
using WebApp.Components;
using DAL;

public partial class SampleDataOld : System.Web.UI.Page
{
    private string strSelectedId//分析项号
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strSampleID//样品单号
    {
        get { return (string)ViewState["strSampleID"]; }
        set { ViewState["strSampleID"] = value; }
    }
    private string strNowLyID//领用单号
    {
        get { return (string)ViewState["strNowLyID"]; }
        set { ViewState["strNowLyID"] = value; }
    }
    private string fxlist//领用单号
    {
        get { return (string)ViewState["fxlist"]; }
        set { ViewState["fxlist"] = value; }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                       txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
             Query();
           // SetButton();


             grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>分析登记</b></font>";
        }
    }
    private void Query()
    {
        string constr = "";
        if (txt_QueryTime.Text.Trim() != "")
            constr = " and  (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";

        string strSql = "select t_DrawSample.ID, t_M_ReporInfo.Ulevel , t_M_SampleInfor.SampleID AS 样品编号," +
      "t_M_SampleInfor.AccessDate AS 接样时间, " +
      " t_M_SampleInfor.TypeID, t_M_AnalysisMainClassEx.ClassName AS 样品类型, t_DrawSample.CreateDate 领用时间,LyUserID,t_DrawSample.fxflag,jhtime,jhman " +
" FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN" +
     " t_M_AnalysisMainClassEx ON " +
    "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_DrawSample on t_DrawSample.SampleID=t_M_SampleInfor.SampleID   where  LyUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' and t_M_SampleInfor.StatusID<=2 " + constr + " and t_DrawSample.fxflag=0 and t_DrawSample.type=0  and yxflag=0  ORDER BY t_M_SampleInfor.SampleID";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dcs = new DataColumn("分析项目");
        ds.Tables[0].Columns.Add(dcs);
        DataColumn dcs1 = new DataColumn("领用分析项目");
        ds.Tables[0].Columns.Add(dcs1);
        DataColumn dcs2 = new DataColumn("分析单状态");
        ds.Tables[0].Columns.Add(dcs2);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急";
            else
                dr["紧急程度"] = "一般";
            string getitemstr = "select AIName,MonitorItem,fxDanID from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=t_MonitorItemDetail.MonitorItem  where   SampleID='" + dr["样品编号"].ToString() + "' and delflag=0";
            DataSet dsitem = new MyDataOp(getitemstr).CreateDataSet();
            if (dsitem != null && dsitem.Tables.Count > 0)
            {
                DataRow[] drselect =dsitem.Tables[0].Select("fxDanID='"+ dr["id"].ToString()+"'");
                foreach (DataRow drr in dsitem.Tables[0].Rows)
                {
                    dr["分析项目"] += drr[0].ToString() + ",";                   
                }
                foreach (DataRow drr in drselect)
                {
                    dr["领用分析项目"] += drr[0].ToString() + ",";
                }
            }

            if (dr["fxflag"].ToString() == "0")
                dr["分析单状态"] = "分析中";
            else if (dr["fxflag"].ToString() == "1")
                dr["分析单状态"] = "未提交";
            else if (dr["fxflag"].ToString() == "2")
                dr["分析单状态"] = "待交接";
            else if (dr["fxflag"].ToString() == "3")
                dr["分析单状态"] = "交接完成";
           
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

        }
        ds.Dispose();
    }
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
       strSampleID= grdvw_List.Rows[e.NewEditIndex].Cells[3].Text.Trim();
       strNowLyID = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();
       fxlist = grdvw_List.Rows[e.NewEditIndex].Cells[14].Text.Trim();
       if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim() != "&nbsp;")
       txt_jhdate.Text = grdvw_List.Rows[e.NewEditIndex].Cells[10].Text.Trim();
       if (grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim() != "&nbsp;")
       txt_jhman.Text = grdvw_List.Rows[e.NewEditIndex].Cells[11].Text.Trim();
       txt_jhdate.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAnalysis();", true);
      
        ReportQuery(); ;

    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "查看/编辑";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);

            //TableCell headerset = new TableCell();
            //headerset.Text = "样品列表";
            //headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerset.Width = 60;
            //e.Row.Cells.Add(headerset);

          
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
            ibtnDetail.ImageUrl = "~/images/Detail.gif";

            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            //TableCell MenuSet = new TableCell();
            //MenuSet.Width = 60;
            //MenuSet.Style.Add("text-align", "center");
            //ImageButton btMenuSet = new ImageButton();
            //btMenuSet.ImageUrl = "~/images/Detail.gif";
            //btMenuSet.CommandName = "Select";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuSet.Controls.Add(btMenuSet);
            //e.Row.Cells.Add(MenuSet);

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
    } 
    #endregion
    //提交分析项目,登记分析单数据，更细领用分析标志位
    protected void btn_Commit_Click(object sender, EventArgs e)
    {
           
        if(txt_jhdate.Text.Trim()=="")
        {
                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('校核时间不能为空！');", true);
                       return;
        }
        else if (txt_jhman.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('校核人员不能为空！');", true);
        }
        else
        {
            Entity.Draw draw = new Entity.Draw();
            draw.SampleID = strSampleID;
            draw.ID = int.Parse(strNowLyID);
            draw.finishdate = DateTime.Now;
            draw.jhman = txt_jhman.Text;
            draw.jhtime = DateTime.Parse(txt_jhdate.Text.Trim());
            draw.fxman = Request.Cookies["Cookies"].Values["u_id"].ToString().Trim();

            foreach (GridViewRow gr in grdvw_ReportDetail.Rows)
            {
                Entity.SampleItem item = new Entity.SampleItem();
                if (gr.Cells[1].Text.Trim() != "")
                {
                    item.ID = int.Parse(gr.Cells[1].Text.Trim());

                    TextBox txt_value = gr.Cells[7].FindControl("txt_value") as TextBox;

                    if (txt_value.Text.Trim() != "")
                    {
                        DateTime time = DateTime.Now;
                        if (gr.Cells[6].Text.Trim() != "&nbsp;" && gr.Cells[6].Text.Trim() != "")
                            item.AnalysisDate = DateTime.Parse(gr.Cells[6].Text.Trim());
                        //item.jhdate = DateTime.Parse(txt_jhdate.Text.Trim());
                        item.Value = txt_value.Text.Trim();
                        item.statusID = 1;
                        item.lyID = strNowLyID;
                        item.AnalysisUserID = Request.Cookies["Cookies"].Values["u_id"].ToString().Trim();
                        //item.jhman = txt_jhman.Text;
                        draw.SampleItemList.Add(item);
                    }
                }
                else
                    item.ID = 0;
            }

            if (fxlist == "&nbsp;" || draw.SampleItemList.Count == grdvw_ReportDetail.Rows.Count)
            {
                DAl.Sample sampleobj = new DAl.Sample();

                if (sampleobj.ExChangeSample(draw) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('分析数据提交成功！');hiddenDetailAnalysis();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('分析数据提交失败！')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('分析数据登记未全！不能成功提交')", true);
            }
        }
        ReportQuery();
        Query();
    }
    //protected void SetButton()
    //{
    //    if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
    //    {
    //        btn_Add.Enabled = false;
    //        btn_OK.Enabled = false;
    //        btn_AddSample.Enabled = false;
    //        for (int i = 0; i < grdvw_List.Rows.Count; i++)
    //        {
    //          ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
    //            if(btn!=null)
    //          btn.Visible = false;

    //        }
    //    }
    //    else
    //    {
    //        btn_Add.Enabled = true;
    //        btn_OK.Enabled = true;
    //        btn_AddSample.Enabled = true;
    //        for (int i = 0; i < grdvw_List.Rows.Count; i++)
    //        {
    //            ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
    //            if(btn!=null)
    //            btn.Visible = true;
    //        }
    //    }
    //}
    private void ReportQuery()
    {
       
        string strSql = "select t_DrawSampleDetail.id,  t_M_SampleInfor.SampleID AS 样品编号," +
     " t_DrawSampleDetail.CreateDate 领用时间,t_MonitorItemDetail.MonitorItem, t_M_AnalysisItemEx.AIName 分析项目 ,t_DrawSampleDetail.fxDate 数据登记时间,t_DrawSampleDetail.value" +
" FROM t_M_SampleInfor    INNER JOIN" +
     " t_M_SampleType ON " +
    "  t_M_SampleInfor.TypeID = t_M_SampleType.TypeID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id inner join t_DrawSample on t_DrawSample.SampleID=t_M_SampleInfor.SampleID and t_DrawSample.id=t_MonitorItemDetail.fxDanID inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=t_MonitorItemDetail.MonitorItem inner join t_DrawSampleDetail  on DrawID=t_DrawSample.id  where t_M_SampleInfor.SampleID='" + strSampleID + "' and t_DrawSample.id='" + strNowLyID + "' and   t_DrawSample.LyUserID='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' and fxflag=0  ORDER BY t_M_SampleInfor.SampleID";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_ReportDetail.DataSource = ds;
            grdvw_ReportDetail.DataBind();
            int intColumnCount = grdvw_ReportDetail.Rows[0].Cells.Count;
            grdvw_ReportDetail.Rows[0].Cells.Clear();
            grdvw_ReportDetail.Rows[0].Cells.Add(new TableCell());
            grdvw_ReportDetail.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_ReportDetail.DataSource = ds;
            grdvw_ReportDetail.DataBind();
            
        }
        ds.Dispose();
    }

    #region GridView相关事件响应函数
    protected void grdvw_ReportDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_ReportDetail.PageIndex = e.NewPageIndex;
        ReportQuery();
    }
    protected void grdvw_ReportDetail_RowSelecting(object sender, GridViewSelectEventArgs e)
    { 
         strSelectedId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
         string SampleId = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
         string[] str = new string[3];
         int i = 0;
         string monitorid = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[4].Text.Trim();
        TextBox txt_value = grdvw_ReportDetail.Rows[e.NewSelectedIndex].Cells[7].FindControl("txt_value") as TextBox;
       
            string upstr = "update t_MonitorItemDetail set flag=0,fxDanID=null where fxDanID='" + strNowLyID + "' and MonitorItem='" + monitorid + "'";

            str.SetValue(upstr, i++);

            upstr = "update t_DrawSampleDetail set updatedate=getdate(),updateuser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',yxflag=1 where id='" + strSelectedId + "'";
            str.SetValue(upstr, i++);
            if (checkDraw(strNowLyID, strSelectedId) == 1)
            {
                upstr = "update t_DrawSample set updatedate=getdate(),updateuser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',yxflag=1 where id='" + strNowLyID + "'";
                str.SetValue(upstr, i++);
            }
            MyDataOp sqlobj = new MyDataOp(upstr);
            if (sqlobj.DoTran(i, str))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('申领退回成功！')", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('申领退回失败！')", true);
            ReportQuery();
    }
    //撤销监测项领用时，检测本次领用是否存在有效的监测项，是-不更改领用记录；否-则更改领用记录为无效！
    protected int checkDraw(string LyID,string strID)
    {
        int ret = 0;
        string checkstr = "select count(*) from t_DrawSampleDetail where yxflag=0 and DrawID='"+LyID+"' and id!='"+strID+"'";
        DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
        if (dscheck != null)
        {
            if (dscheck.Tables[0].Rows[0][0].ToString() == "0")
                ret = 1;
        }
        return ret;

    }
    protected void grdvw_ReportDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strSelectedId = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        string SampleId = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[2].Text.Trim();
         string monitorid = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        TextBox txt_value = grdvw_ReportDetail.Rows[e.NewEditIndex].Cells[7].FindControl("txt_value") as TextBox;
        string[] str = new string[2];
        int i=0;
        if (txt_value.Text.Trim() != "")
        {
            string upstr = "update t_DrawSampleDetail set value='" + txt_value.Text + "',fxDate=getdate(),DataFlag=1,fxuser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' where id='" + strSelectedId + "'";
             str.SetValue(upstr, i++);
            MyDataOp sqlobj = new MyDataOp(upstr);
               if (sqlobj.DoTran(i,str))
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('分析数据登记成功！')", true);
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Success", "alert('分析数据登记失败！')", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddUnSuccess", "alert('请填写分析数据后再保存！')", true);
        
       ReportQuery();
    }
   
    protected void grdvw_ReportDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableCell headerDetail1 = new TableCell();
            headerDetail1.Text = "分析值";
            headerDetail1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail1.Width = 60;
            e.Row.Cells.Add(headerDetail1);
            TableCell headerDetail = new TableCell();
            headerDetail.Text = "保存";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);


            TableCell headerDel = new TableCell();
            headerDel.Text = "申领撤销";
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

            e.Row.Cells[0].Text = id.ToString();
            //手动添加分析项目添加
            TableCell tabcSelect = new TableCell();
            tabcSelect.Width = 100;
            tabcSelect.Style.Add("text-align", "center");
            TextBox txt_value = new TextBox();
            txt_value.ID = "txt_value";
            tabcSelect.Controls.Add(txt_value);
            e.Row.Cells.Add(tabcSelect);
            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ID = "Draw";
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
           
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            TableCell MenuSet = new TableCell();
            MenuSet.Width = 60;
            MenuSet.Style.Add("text-align", "center");
            ImageButton btMenuSet = new ImageButton();
            btMenuSet.ImageUrl = "~/images/Detail.gif";
            btMenuSet.CommandName = "Select";
            //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            MenuSet.Controls.Add(btMenuSet);
            e.Row.Cells.Add(MenuSet);

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;

            e.Row.Cells[4].Visible = false;
            //e.Row.Cells[8].Visible = false;
            e.Row.Cells[7].Visible = false;
          
        }
    }
    protected void grdvw_ReportDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //初始化checkboxlist
            TextBox txt_value = (TextBox)e.Row.FindControl("txt_value");

            if (txt_value != null)
            {
                if (e.Row.Cells[7].Text.Trim() != "&nbsp;")
                    txt_value.Text = e.Row.Cells[7].Text.Trim();
            }
        }
    }     
    //绑定CheckBoxList的方法     
    //private void BindCheckBoxList(CheckBoxList cbl, string SampleID)     
    //{
    //    //string[] name = e.Row.Cells[14].Text.Trim().Split(',');
    //    //string[] namevalue = e.Row.Cells[15].Text.Trim().Split(',');      
    //    DataSet ds = new MyDataOp("select MonitorItem,AIName from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_MonitorItemDetail.MonitorItem=t_M_AnalysisItemEx.id where t_MonitorItemDetail.flag=0 and SampleID='" + SampleID + "'").CreateDataSet();
        
    //        //这里的方法根据你自己的取数据的方法      
    //    cbl.DataSource = ds;
    //    cbl.DataValueField = "MonitorItem";
    //    cbl.DataTextField = "AIName";       
    //    cbl.DataBind();     
    //}
    //private string getlyrecord(string SampleID,out bool flag)
    //{
    //    string retstr = "";
    //    DataSet ds = new MyDataOp("select CreateDate,ItemList,Name,LyUserID  from t_DrawSample inner join t_R_UserInfo on LyUserID=UserID  where SampleID='" + SampleID + "' order by CreateDate desc").CreateDataSet();
    //    if (ds != null && ds.Tables.Count > 0)
    //    {
    //        foreach (DataRow dr in ds.Tables[0].Rows)
    //        {
    //            retstr += dr["Name"].ToString() + "于" + dr["CreateDate"].ToString() + "分析项目： " + dr["ItemList"].ToString() + "\r\n";
    //        }
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows[0]["LyUserID"].ToString() == Request.Cookies["Cookies"].Values["u_id"].ToString())
    //                flag = true;
    //            else
    //                flag = false;
    //        }
    //        else flag = true;
    //    }
    //    else
    //        flag = true;
    //   return retstr;
    //}
    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }
   
   
    #endregion

}
