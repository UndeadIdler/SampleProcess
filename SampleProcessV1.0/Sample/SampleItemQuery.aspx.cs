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

public partial class Sample_SampleQuery : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strSampleId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSampleId"]; }
        set { ViewState["strSampleId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_endtime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            //txt_receivetime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            txt_QueryEndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_ReportTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-01-01");
            txt_QueryEndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClassEx order by ClassCode asc", txt_type);
            ListItem choose = new ListItem("全部", "-1");
            txt_type.SelectedItem.Selected = false;
            txt_type.Items.Add(choose);
            txt_type.Items.FindByValue("-1").Selected = true;
            DropList_AnalysisMainItem_SelectedIndexChanged(null, null);
            MyStaVoid.BindList("ItemName", "ItemID", "select ItemID, ItemName from t_M_ItemInfo  order by ItemName ", txt_item);
            txt_item.Items.Add(choose);
            txt_item.Items.FindByValue("-1").Selected = true;
            //MyStaVoid.BindList("SampleType", "TypeID", "select TypeID, SampleType from t_M_SampleType  order by SampleType ", txt_type);
            //txt_type.Items.Add(choose);
            //txt_type.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("Status", "StatusID", "select StatusID, Status from t_M_StatusInfo  order by StatusID ", drop_status);
            drop_status.Items.Add(choose);
            drop_status.Items.FindByValue("-1").Selected = true;
            MyStaVoid.BindList("ClientName", "id", "select id,ClientName from t_M_ClientInfo  order by id ", drop_client);
            drop_client.Items.Add(choose);
            drop_client.Items.FindByValue("-1").Selected = true;


            Query(0);

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

            e.Row.Cells[0].Text = id.ToString();

           
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //绑定数据后，隐藏4,5,6,7列 

            e.Row.Cells[1].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[17].Visible = false;
            if (Request.Cookies["Cookies"].Values["u_level"].ToString() == "12")
                e.Row.Cells[5].Visible = false;
           
        }
    }
   
   
    #endregion

    #endregion
   
  
    
    protected void Query(int type)
        {
        //strSelectedId=txt_samplequery.Text;
       string strSample="";//样品编号
        string strDate="";//接样时间
        string strStatus = "";//样品状态
        string strItem="";//项目类型
        string strType="";//样品名称
        string strClient="";//委托单位
        string strAnalysis = "";// 分析项目;
        string strAanlysisStatus = "";//样品分析状态
        string strUrgent = "";//按紧急程度
        if (Drop_Urgent.SelectedValue != "-1")
        {
            if (Drop_Urgent.SelectedValue=="1")
            strUrgent = "and t_M_ReporInfo.Ulevel=1";
            else
                strUrgent = "and (t_M_ReporInfo.Ulevel<>1 or t_M_ReporInfo.Ulevel is null)";
        }
        if (txt_QuerySource.Text != "")//按样品来源

            strSample += "and t_M_SampleInfor.SampleSource like'%" + txt_QuerySource.Text + "%'";

        if (txt_QueryBS.Text != "")//按报告标识

            strSample += "and t_M_ReporInfo.ReportName like'%" + txt_QueryBS.Text + "%'";
        if (txt_samplequery.Text != "")//按样品编号

            strSample += "and t_M_SampleInfor.SampleID like'%" + txt_samplequery.Text + "%'";
        if (txt_QueryTime.Text.Trim() != "" && txt_QueryEndTime.Text.Trim() != "")//按采样时间
        {
            DateTime start=DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00");
            DateTime end=DateTime.Parse(txt_QueryEndTime.Text.Trim() + " 23:59:59");
            strDate = " and AccessDate between '"+ start+"' and '"+end+"'";
            //strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        }

        if(drop_status.SelectedValue!="-1")//按样品状态
        {
            strStatus = " and t_M_ReporInfo.StatusID='" + drop_status.SelectedValue + "' ";
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
        if (drop_analysisstatus.SelectedValue != "-1")
        {
            strAanlysisStatus = "  and t_MonitorItemDetail.flag=" + drop_analysisstatus.SelectedValue + "";
 
        }
        bool flag=false;
        int i = 0;
        //string flagtype ="";
        //if(RadioButtonList1.SelectedValue==1)
        //    flagtype = " and ";
        //else
        //flagtype = " or ";
        foreach (ListItem item in cb_analysisList.Items)
        {
            if (item.Selected)
            {
                
                flag=true;
                
                    if (i == 0)
                        strAnalysis = " t_MonitorItemDetail.MonitorItem='" + item.Value + "' ";
                    else
                        strAnalysis += " or t_MonitorItemDetail.MonitorItem='" + item.Value + "' ";
                //}

                i++;
            }
        }
       
        //按选中的分析项目查询
        string strtitle = "";
        if (flag)
        {
            strtitle += " and (" + strAnalysis + ")";
        }
        string cstr = "";
       
            if (cstr == "")
            { 
                cstr = " and 1=1";
            }

            if (Request.Cookies["Cookies"].Values["u_level"].ToString() == "12" && Request.Cookies["Cookies"].Values["u_role"].ToString() != "48")//zpto='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' or 
           {
                    strtitle += " and (chargeman ='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' or t_MonitorItemDetail.fxuser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";

           }
           
            //if (Request.Cookies["Cookies"].Values["u_level"].ToString() == "12")//zpto='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' or 
            //{
            //    strtitle += " and (chargeman ='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "' or t_MonitorItemDetail.fxuser='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
              
            //}
            cstr = strUrgent + strSample + strDate + strStatus + strItem + strType + strClient + strAanlysisStatus + strtitle;

            if (type == 0)
                cstr += " and 1<>1";
            string strSql = "SELECT  t_M_SampleInfor.id,t_M_ReporInfo.ReportName 报告标识,t_M_ReporInfo.Projectname 项目名称,t_M_SampleInfor.SampleID AS 样品编号,t_M_SampleInfor.SampleSource 样品来源,t_M_SampleInfor.SampleAddress 采样点 ," +
         "t_M_SampleInfor.AccessDate AS 接样时间,t_M_ReporInfo.chargeman 项目负责人, " +
         " t_M_SampleInfor.TypeID, t_M_AnalysisMainClassEx.ClassName AS 样品类型,t_M_SampleInfor.SampleProperty 样品性状, " +
        " t_M_SampleInfor.StatusID, t_M_SampleInfor.ReportID,t_M_SampleInfor.ReportRemark,AIName 监测项,experimentvalue 分析值, t_MonitorItemDetail.flag,zpto 指派给, zpto 状态" +
      " FROM t_M_ReporInfo inner join  t_M_SampleInfor on t_M_ReporInfo.id=t_M_SampleInfor.ReportID  INNER JOIN  " +
        " t_M_AnalysisMainClassEx ON " +
       "  t_M_SampleInfor.TypeID = t_M_AnalysisMainClassEx.ClassID inner join t_MonitorItemDetail on t_MonitorItemDetail.SampleID=t_M_SampleInfor.id inner join  t_M_ANItemInf on t_M_ANItemInf.id=MonitorItem " +

      " WHERE " +
        " 1=1"+ cstr+
      " ORDER BY t_M_SampleInfor.id";
       
            DataSet ds = new MyDataOp(strSql).CreateDataSet();
           
           
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DataRow[] drs = ds_User.Tables[0].Select("UserID='" + dr["指派给"].ToString() + "'");
            if(drs.Length>0)
            {

                dr["指派给"] = drs[0]["Name"].ToString();
             }
            DataRow[] drchargeman = ds_User.Tables[0].Select("UserID='" + dr["项目负责人"].ToString() + "'");
            if (drchargeman.Length > 0)
            {

                dr["项目负责人"] = drchargeman[0]["Name"].ToString();
            }
            switch (dr["flag"].ToString())
            {
                case "1": dr["状态"] = "未领用";
                    break;
                case "2": dr["状态"] = "分析中";
                    break;
                case"0":
                    dr["状态"]="未指派";
                    break;

                default: dr["状态"] = "完成";
                    break;
            }
            if (dr["flag"].ToString() != "3")
                dr["分析值"] = "";
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

    protected void btn_query_Click(object sender, EventArgs e)

    { Query(1); }
    protected void DropList_AnalysisMainItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds;
        string str;
        string constr="";
        if (txt_type.SelectedIndex != txt_type.Items.Count - 1)
        {
            constr = " and ClassID='" + txt_type.SelectedValue.Trim() + "'";
        }
        if (DropList_AnalysisMainItem.SelectedValue.ToString() == "-1")
        {
            str = "select distinct t_M_ANItemInf.id,t_M_ANItemInf.AIName from t_M_AnalysisItemEx inner join t_M_ANItemInf on t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID where 1=1 " + constr;
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        else
        {
            str = "select distinct t_M_ANItemInf.id,t_M_ANItemInf.AIName from t_M_AnalysisItemEx inner join t_M_ANItemInf on t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID where type='" + DropList_AnalysisMainItem.SelectedValue.ToString() + "'" + constr;
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        ds.Dispose();
       
    }

    protected void lb_show_Click(object sender, EventArgs e)
    {
        
    if (Panel_Other.Visible)
        Panel_Other.Visible = false;
    else
        Panel_Other.Visible = true;
    }
}