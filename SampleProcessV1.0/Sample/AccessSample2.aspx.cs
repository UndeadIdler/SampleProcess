using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using WebApp.Components;

public partial class Sample_AccessSample2 : System.Web.UI.Page
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
    private string strAnalysisId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strAnalysisId"]; }
        set { ViewState["strAnalysisId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_SampleType.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            //txt_AccessTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
           // MyStaVoid.BindList("ClientName", "id", ""select top " + count + " ReportName from t_M_ReporInfo where" + conditionstr + " ReportName like  '" + prefixText + "%' and StatusID=0 or StatusID=1 group by ReportName order by  ReportName "", DropList_client);
           // txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            Query();
            SetButton();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>添加分析项目</b></font>"; 
        }
    }
    protected void SetButton()
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            btn_OKAnalysis.Enabled = false;
            btn_SampleSave.Enabled = false;
           // btn_AddSample.Enabled = false;
        }
        else
        {
            btn_OKAnalysis.Enabled = true;
            btn_SampleSave.Enabled = true;
           // btn_AddSample.Enabled = true;
        }
    }
    //private void MainQuery()
    //{
    //    string strSql = "select t_M_ReporInfo.id,CreateDate 时间,t_M_SampleInfor.ItemType,ItemName 项目类型,ClientID,ClientName 委托单位,ReportName 报告标识 from t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType,t_M_ClientInfo where t_M_ClientInfo.id=ClientID and t_M_SampleInfor.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID and (StatusID=0 or StatusID=1) order by t_M_SampleInfor.id ";

    //    DataSet ds = new MyDataOp(strSql).CreateDataSet();

    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        //没有记录仍保留表头
    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        grdvw_List.DataSource = ds;
    //        grdvw_List.DataBind();
    //        int intColumnCount = grdvw_List.Rows[0].Cells.Count;
    //        grdvw_List.Rows[0].Cells.Clear();
    //        grdvw_List.Rows[0].Cells.Add(new TableCell());
    //        grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
    //    }
    //    else
    //    {
    //        grdvw_List.DataSource = ds;
    //        grdvw_List.DataBind();
    //    }
    //}

    private void Query()
    {
        string strSql = "select t_M_SampleInfor.id,AccessDate 接样时间,t_M_ReporInfo.ItemType,ItemName 项目类型,SampleID 样品编号,t_M_SampleInfor.TypeID,t_M_SampleType.SampleType 样品类型,t_M_ReporInfo.ClientID,ClientName 区域,t_M_ReporInfo.ReportName 报告标识,t_M_ReporInfo.urgent 备注,t_M_SampleInfor.ReportRemark 报告备注,Ulevel,snum,wtdepart 委托单位 from t_M_ReporInfo, t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType,t_M_ClientInfo where t_M_ClientInfo.id=t_M_ReporInfo.ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID and t_M_ReporInfo.StatusID<2 and t_M_SampleInfor.ReportID=t_M_ReporInfo.id  and t_M_SampleInfor.StatusID=0 order by t_M_SampleInfor.id ";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急";
            else
                dr["紧急程度"] = "一般";


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
    }
    
    #region GridView相关事件响应函数
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
   
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            

            TableCell headerset = new TableCell();
            headerset.Text = "分析项目";
            headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerset.Width = 60;
            e.Row.Cells.Add(headerset);

        
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

           

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
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[8].Visible = false;

            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            
        }
    }
   
    #endregion

    #region 其它函数


    //private string Verify()
    //{
    //    string strErrorInfo = "";
    //    if (txt_SampleID.Text.Trim() == "")
    //    {
    //        strErrorInfo += "样品编码不能为空！\\n";
    //    }
        
    //    else if (DropList_SampleType.Text== "")
    //        strErrorInfo += "样品类型不能为空！\\n";

    //    else if (DropList_client.SelectedValue == "-1")
    //        strErrorInfo += "请选择委托单位！\\n";
    //    else if (txt_ReportName.Text.Trim()=="")
    //    {
    //        strErrorInfo += "请输入报告编号！\\n";
    //        //if (lbl_Type.Text == "添加")
    //        //{

    //        //    string str = "select * from t_M_SampleInfor where SampleID='" + txt_SampleID.Text.Trim() + "'";
    //        //    DataSet ds = new MyDataOp(str).CreateDataSet();
    //        //    if (ds.Tables[0].Rows.Count > 0)
    //        //        strErrorInfo = "样品编号不能重复!";
    //        //    else
    //        //        strErrorInfo = "";
    //        //}
            
    //    }
    //    //else if(txt_num.Text.Trim())//判断是否为数值型
    //    return strErrorInfo;
    //}
    private string VerifyItem()
    {
        string strErrorInfo = "";
        //if (DropList_AnalysisItem.Text=="")
        //{
        //    strErrorInfo += "请选择分析项目！\\n";
        //}
        //else if (txt_detailNum.Text == "")//TBD数字的判断
        //{
        //    strErrorInfo += "请填写正确的数量！\\n";
        //}
        
        return strErrorInfo;
    }
    
   
    #endregion
    protected void grdvw_List_RowSelecting(object sender, GridViewSelectEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        strSampleId = grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        lbl_sample.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        MyStaVoid.BindList("ClassName", "ClassID", "select * from t_M_AnalysisMainClass order by ClassCode asc", DropList_AnalysisMainItem);
        ListItem choose = new ListItem("全部", "-1");
        DropList_AnalysisMainItem.SelectedItem.Selected = false;
        DropList_AnalysisMainItem.Items.Add(choose);
        DropList_AnalysisMainItem.Items.FindByValue("-1").Selected=true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEditAnalysisAdd();hiddenDetail();", true);
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[12].Text.Trim() != "&nbsp;")
        txt_send.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[12].Text.Trim();
        // if (grdvw_List.Rows[e.NewSelectedIndex].Cells[15].Text.Trim() != "&nbsp;")
        //txt_wtdepart.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[15].Text.Trim();
        string retstr = "";
        List<string> namelist = new List<string>();

        namelist = getnum(lbl_sample.Text, ref retstr);
        if (retstr != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + retstr + "');", true);
        }
       txt_num.Text= namelist.Count.ToString() ;
            
        if (grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text != "&nbsp;")
            txt_num.Text = grdvw_List.Rows[e.NewSelectedIndex].Cells[14].Text;
        DataBindAll();
       

    }
    ////查询出某个样品的分析项目列表
    //protected void queryAnalysisItem()
    //{
    //    string sql = "select t_M_SampleInfor.SampleID 样品单, t_MonitorItemDetail.SampleID, t_MonitorItemDetail.SampleNO 样品编号,t_MonitorItemDetail.MonitorItem,AIName 分析项目,experimentvalue 分析值 from t_M_MonitorItem,t_M_AnalysisItem,t_M_SampleInfor,t_MonitorItemDetail where t_M_SampleInfor.id=t_M_MonitorItem.SampleID and t_M_MonitorItem.MonitorItem=t_M_AnalysisItem.id and t_MonitorItemDetail.SampleID=t_M_SampleInfor.ID  and t_M_SampleInfor.id='" + strSelectedId + "' order by t_M_SampleInfor.SampleID";
    //    DataSet ds = new MyDataOp(sql).CreateDataSet();

    //    if (ds.Tables[0].Rows.Count == 0)
    //    {
    //        //没有记录仍保留表头
    //        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //        grdvw_ListAnalysisItem.DataSource = ds;
    //        grdvw_ListAnalysisItem.DataBind();
    //        int intColumnCount = grdvw_ListAnalysisItem.Rows[0].Cells.Count;
    //        grdvw_ListAnalysisItem.Rows[0].Cells.Clear();
    //        grdvw_ListAnalysisItem.Rows[0].Cells.Add(new TableCell());
    //        grdvw_ListAnalysisItem.Rows[0].Cells[0].ColumnSpan = intColumnCount;
    //    }
    //    else
    //    {
    //        grdvw_ListAnalysisItem.DataSource = ds;
    //        grdvw_ListAnalysisItem.DataBind();
    //    }
    //    grdvw_ListAnalysisItem.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #3333FF;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>" + strSampleId + "的分析项目列表</b></font>";

    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);

    //    ReportSelectQuery();
    //}
    //protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    //{

    //}
   
   #region 项目列表

  
    
    #endregion
    private List<string> getnum(string str,ref string  retstr)
    {
        List<string> namelist = new List<string>(); 
        string[] sampleList = str.Split(',', '，');
        int sampleNum = 0;
       sampleNum = sampleList.Length;
        for (int k = 0; k < sampleList.Length; k++)
        {
            string[] range = sampleList[k].Split('~');
            if (range.Length == 2)
            {
                if (range[1].Trim() != "" && range[0].Trim() != "")
                {
                    try
                    {
                        sampleNum = sampleNum - 1;
                        if (range[1].Substring(0, range[1].Length - 3) == range[0].Substring(0, range[0].Length - 3))
                        {
                            int p=int.Parse(range[1].Substring(range[1].Length - 3, 3));
                            int q=int.Parse(range[0].Substring(range[0].Length - 3, 3));
                            int temp = p- q + 1;
                            if (temp <= 0)
                                throw new Exception("编号有错！");
                            else
                            {
                                sampleNum = sampleNum + temp;
                                for (int j = q; j <=p; j++)
                                {
                                    string strtemp = range[1].Substring(0, range[1].Length - 3) + CodeConvert(j.ToString(),3);
                                    namelist.Add(strtemp);
                                }
                            }
                        }
                        else
                            throw new Exception("编号不统一！");
                    }
                    catch (Exception msg)
                    {
                       retstr=msg.Message.ToString();
                        break;
                    }
                }
            }
            else if (range.Length > 2)
            {
                retstr = "编号填写有误！";
                break;
            }
            else
                namelist.Add(sampleList[k]);


        }
        
        return namelist;
    }

    /// <summary>
    /// 转换字符为m个字符长，不够前面补零
    /// </summary>
    /// <param name="strCode">待转换字符</param>
    /// <param name="m">结果字符长度</param>
    /// <returns>转换结果</returns>
    public static string CodeConvert(string strCode, int m)
    {
        string s = strCode;
        for (int i = 0; i < m; i++)
        {
            s = "0" + s;
        }
        s = s.Substring(s.Length - m, m);
        return s;
    }

    #region 分析项目添加
    protected void btn_OKAnalysis_Click(object sender, EventArgs e)
    {
          string retstr = "";
          List<string> namelist = new List<string>();
          namelist = getnum(lbl_sample.Text, ref retstr);
          if (retstr != "")
          {
              ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + retstr + "');", true); 
              return;
          }
          int sampleNum = namelist.Count; ;
            
            //自动通过样品编号计算
        if (sampleNum == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

        }
        else
        {
            try
            {

                if (int.Parse(txt_num.Text.Trim()) != sampleNum)
                {
                    if (int.Parse(txt_num.Text.Trim()) != 0)
                    {
                        sampleNum = int.Parse(txt_num.Text.Trim());
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                        WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                        return;
                    }
                }
                else
                {
                    int selectnum = 0;
                    string str = "";
                    int M = cb_analysisList.Items.Count;
                    string[] liststr = new string[M + 1 + M * sampleNum];
                    int i = 0;
                    //更新样品单表
                    liststr.SetValue("update t_M_SampleInfor set snum='" + sampleNum + "' where id='" + strSelectedId + "'", i++);
                    foreach (ListItem LI in cb_analysisList.Items)
                    {
                        if (LI.Selected)
                        {
                            selectnum++;
                            //分析项目表
                            str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                            DataSet dscheck = new MyDataOp(str).CreateDataSet();
                            if (dscheck.Tables[0].Rows.Count <= 0)
                            {
                                str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + LI.Value + "','" + sampleNum + "')";
                                liststr.SetValue(str, i++);


                            }
                            else
                            {
                                str = "update t_M_MonitorItem set Num='" + sampleNum + "' where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                liststr.SetValue(str, i++);
                            }
                            dscheck.Dispose();
                            //string strcheckup = "Delete from t_MonitorItemDetail where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";
                            //liststr.SetValue(strcheckup, i++);
                            for (int j = 0; j < sampleNum; j++)
                            {
                                string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[j] + "' and MonitorItem='" + LI.Value + "' ";
                                DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                if (seldr.Length <= 0)
                                {
                                    DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                    if (checkdr.Length <= 0)
                                    {
                                        str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + LI.Value + "','" + namelist[j] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                        liststr.SetValue(str, i++);
                                    }
                                   
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                    WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                            DataSet dscheck = new MyDataOp(str).CreateDataSet();
                            if (dscheck.Tables[0].Rows.Count > 0)
                            {
                                str = "delete from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                liststr.SetValue(str, i++);
                                string strcheckup = "Delete from t_MonitorItemDetail where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                                liststr.SetValue(strcheckup, i++);


                            }
                            dscheck.Dispose();
                        }
                    }
                    if (selectnum == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择分析项目！');", true);
                        return;
                    }
                    else
                    {
                        MyDataOp OpObj = new MyDataOp(str);
                        if (OpObj.DoTran(i, liststr))
                        {
                            bool detailret = true;
                            if (txt_own.Text.Trim() != "")
                            {
                                string strown = txt_own.Text.Trim();
                                string[] strlist = strown.Split(',', '，');
                                string[] liststr2 = new string[strlist.Length + 1];
                                for (int j = 0; j < strlist.Length; j++)
                                {
                                    if (strlist[j].ToString().Trim() != "")
                                    {
                                        str = "select id,AIName from t_M_AnalysisItem where AIName='" + strlist[j].ToString() + "'";

                                        DataSet dscheck = new MyDataOp(str).CreateDataSet();
                                        if (dscheck.Tables[0].Rows.Count > 0)
                                        {
                                            str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + sampleNum + "')";
                                            liststr2.SetValue(str, i++);
                                            for (int m = 0; m < sampleNum; m++)
                                            {
                                                string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[m] + "' and MonitorItem='" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "' ";
                                                DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                                DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                                if (seldr.Length <= 0)
                                                {
                                                    DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                                    if (checkdr.Length <= 0)
                                                    {
                                                        str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + namelist[m] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                                        liststr2.SetValue(str, i++);
                                                    }

                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                                    WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                                    return;
                                                }
                                            }
                                            //for (int m = 0; m < sampleNum; m++)
                                            //{
                                            //    string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[m] + "' and  SampleID!='" + strSelectedId + "'";
                                            //    DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                            //    if (dsdetialcheck.Tables[0].Rows.Count <= 0)
                                            //    {
                                            //        string strcheckup = "Delete from t_MonitorItemDetail where SampleID='" + strSelectedId + "'";
                                            //        liststr2.SetValue(strcheckup, i++);

                                            //        str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + namelist[m] + "')";
                                            //        liststr2.SetValue(str, i++);

                                            //    }
                                            //    else
                                            //    {
                                            //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不能重复！');", true);
                                            //        Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不能重复！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                            //        return;
                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            str = @"insert into t_M_AnalysisItem
                    (ClassID,AIName,AICode)  
                    values('1','" + strlist[j] + "','" + strlist[j] + "')";
                                            MyDataOp mdo = new MyDataOp(str);
                                            bool blSuccess = mdo.ExecuteCommand();
                                            if (blSuccess == true)
                                            {
                                                DataSet myDR = new MyDataOp("select id from t_M_AnalysisItem where AIName='" + strlist[j] + "'").CreateDataSet();

                                                if (myDR.Tables[0].Rows.Count > 0)
                                                {
                                                    string itemname = myDR.Tables[0].Rows[0]["id"].ToString();
                                                    myDR.Dispose();
                                                    str = @"insert into t_M_MonitorItem
                    (SampleID,MonitorItem,Num,UserID,CreateDate)  
                    values('" + strSelectedId + "','" + itemname + "','" + sampleNum + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate())";
                                                    liststr2.SetValue(str, i++);
                                                    for (int m = 0; m < sampleNum; m++)
                                                    {
                                                        string strcheck = "select SampleNo,SampleID from t_MonitorItemDetail where SampleNo='" + namelist[m] + "' and MonitorItem='" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "' ";
                                                        DataSet dsdetialcheck = new MyDataOp(strcheck).CreateDataSet();
                                                        DataRow[] seldr = dsdetialcheck.Tables[0].Select(" SampleID<>'" + strSelectedId + "'");
                                                        if (seldr.Length <= 0)
                                                        {
                                                            DataRow[] checkdr = dsdetialcheck.Tables[0].Select(" SampleID='" + strSelectedId + "'");
                                                            if (checkdr.Length <= 0)
                                                            {
                                                                str = "insert into t_MonitorItemDetail(SampleID,MonitorItem,SampleNo,createdate,createuser) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + namelist[m] + "',getdate(),'" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "')";
                                                                liststr2.SetValue(str, i++);
                                                            }

                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品重复，请检查！');", true);
                                                            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品重复，请检查！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }


                                        dscheck.Dispose();
                                    }

                                }
                                MyDataOp OpObj2 = new MyDataOp(str);
                                detailret = OpObj.DoTran(i, liststr);
                            }
                            if (detailret)
                            {
                                WebApp.Components.Log.SaveLog("样品接收添加分析项目" + strSampleId + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存成功！');", true);
                                DataBindAll();
                                txt_own.Text = "";

                            }
                            else
                            {
                                WebApp.Components.Log.SaveLog("样品接收添加分析项目" + strSampleId + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
                            }
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            }
                   

          

            Query();
        }
   

   }
   

    protected void btn_CancelAnalysis_Click(object sender, EventArgs e)
    {
       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetailAnalysisAdd();", true);
       Query();

    }

    #endregion
    protected void btn_query_Click(object sender, EventArgs e)
    {
        //strSelectedId=txt_samplequery.Text;
       string strSample="";
        string strDate="";
        if(txt_samplequery.Text!="")
            strSample="and SampleID like'%" + txt_samplequery.Text + "%'";

        if (txt_QueryTime.Text!= "")
            strDate = " and (year(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(AccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_M_SampleInfor.id,AccessDate 接样时间,t_M_ReporInfo.ItemType,ItemName 项目类型,SampleID 样品编号,t_M_SampleInfor.TypeID,t_M_SampleType.SampleType 样品类型,t_M_ReporInfo.ClientID,ClientName 区域,t_M_ReporInfo.ReportName 报告标识,t_M_ReporInfo.urgent 备注,t_M_SampleInfor.ReportRemark 报告备注,Ulevel,snum,wtdepart 委托单位 from t_M_ReporInfo, t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType,t_M_ClientInfo where t_M_ClientInfo.id=t_M_ReporInfo.ClientID and t_M_ReporInfo.ItemType=t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID=t_M_SampleType.TypeID and t_M_ReporInfo.StatusID<2 and t_M_SampleInfor.ReportID=t_M_ReporInfo.id  and t_M_SampleInfor.StatusID=0 " + strSample + strDate + "  order by t_M_SampleInfor.id  ";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();

        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急";
            else
                dr["紧急程度"] = "一般";


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
    protected void btn_SampleSave_Click(object sender, EventArgs e)
    {
        string retstr = "";
        List<string> namelist = new List<string>();

        namelist = getnum(lbl_sample.Text, ref retstr);
        if (retstr != "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + retstr + "');", true);
            return;
        }
        int sampleNum = namelist.Count; ; //自动通过样品编号计算
        try
        {

             if (int.Parse(txt_num.Text.Trim()) != sampleNum){
                    if (int.Parse(txt_num.Text.Trim()) != 0)
                    {
                        sampleNum = int.Parse(txt_num.Text.Trim());
                    }
                    
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);
                        WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    }
                }
                 
       
        

            
            string str = "";
            int M = cb_analysisList.Items.Count;
            string[] liststr = new string[M+1];
            int i = 0;
            liststr.SetValue("update t_M_SampleInfor set snum='" + sampleNum + "' where id='"+strSelectedId+"'", i++);
            foreach (ListItem LI in cb_analysisList.Items)
            {
                if (LI.Selected)
                {
                    str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                    DataSet dscheck = new MyDataOp(str).CreateDataSet();
                    if (dscheck.Tables[0].Rows.Count <= 0)
                    {
                        str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + LI.Value + "','" + sampleNum + "')";
                        liststr.SetValue(str, i++);


                    }
                    else
                    {
                        str = "update t_M_MonitorItem set Num='" + sampleNum + "' where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                        liststr.SetValue(str, i++);
                    }
                    dscheck.Dispose();
                }
                else
                {
                    str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "' ";

                    DataSet dscheck = new MyDataOp(str).CreateDataSet();
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        str = "delete from t_M_MonitorItem where SampleID='" + strSelectedId + "' and MonitorItem='" + LI.Value + "'";
                        liststr.SetValue(str, i++);


                    }
                    dscheck.Dispose();
                }
            }
            if (txt_own.Text.Trim() != "")
            {
                string strown = txt_own.Text.Trim();
                string[] strlist = strown.Split(',', '，');
                for (int j = 0; j < strlist.Length; j++)
                {
                    if (strlist[j].ToString().Trim() != "")
                    {
                        str = "select id,AIName from t_M_AnalysisItem where AIName='" + strlist[j].ToString() + "'";

                        DataSet dscheck = new MyDataOp(str).CreateDataSet();
                        if (dscheck.Tables[0].Rows.Count > 0)
                        {
                            str = "insert into t_M_MonitorItem(SampleID,MonitorItem,Num) values('" + strSelectedId + "','" + dscheck.Tables[0].Rows[0][0].ToString().Trim() + "','" + sampleNum + "')";
                            liststr.SetValue(str, i++);
                        }
                        else
                        {
                            str = @"insert into t_M_AnalysisItem
                    (ClassID,AIName,AICode)  
                    values('1','" + strlist[j] + "','" + strlist[j] + "')";
                            MyDataOp mdo = new MyDataOp(str);
                            bool blSuccess = mdo.ExecuteCommand();
                            if (blSuccess == true)
                            {
                                DataSet myDR = new MyDataOp("select id from t_M_AnalysisItem where AIName='" + strlist[j] + "'").CreateDataSet();

                                if (myDR.Tables[0].Rows.Count > 0)
                                {
                                    string itemname = myDR.Tables[0].Rows[0]["id"].ToString();
                                    myDR.Dispose();
                                    str = @"insert into t_M_MonitorItem
                    (SampleID,MonitorItem,Num,UserID,CreateDate)  
                    values('" + strSelectedId + "','" + itemname + "','" + sampleNum + "','" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "',getdate())";
                                    liststr.SetValue(str, i++);
                                }
                            }
                        }
                        dscheck.Dispose();
                    }

                }

            }
            str = "select * from t_M_MonitorItem where SampleID='" + strSelectedId + "'";
            DataSet ds = new MyDataOp(str).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = "update t_M_SampleInfor set StatusID=1 where id='" + strSelectedId + "'";

                liststr.SetValue(str, i++);
                MyDataOp OpObj = new MyDataOp(str);
                if (OpObj.DoTran(i, liststr))
                {
                    WebApp.Components.Log.SaveLog("样品接收添加分析项目" + lbl_sample.Text + "（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存成功！');", true);
                    DataBindAll();
                    txt_own.Text = "";

                }
                else
                {
                    WebApp.Components.Log.SaveLog("样品接收添加分析项目" + lbl_sample.Text + "（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据保存失败！');", true);
                }
                ds.Dispose();
            }
            else
            {
                WebApp.Components.Log.SaveLog("样品接收添加分析项目" + lbl_sample.Text + "（" + strSelectedId + "）未保存分析项目时提交！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请添加分析项目！再提交');", true);

            }
            
       
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('样品编号不符合规定！');", true);

            WebApp.Components.Log.SaveLog("样品接收编辑样品" + strSampleId + "（" + strSelectedId + "）样品编号不符合规定！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);

        }
        Query();
        //queryAnalysisItem();

    }
    protected void DropList_AnalysisMainItem_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataSet ds;
        string str;
        if (DropList_AnalysisMainItem.SelectedValue.ToString() == "-1")
        {
            str = "select id,AIName from t_M_AnalysisItem";
            ds = new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        else
        {
            str = "select id,AIName from t_M_AnalysisItem where ClassID='" + DropList_AnalysisMainItem.SelectedValue.ToString() + "'";
            ds= new MyDataOp(str).CreateDataSet();
            cb_analysisList.DataSource = ds;
            cb_analysisList.DataValueField = "id";
            cb_analysisList.DataTextField = "AIName";
            cb_analysisList.DataBind();
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            
                str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' "; 
                DataSet dscheck = new MyDataOp(str).CreateDataSet();
                if (dscheck.Tables[0].Rows.Count > 0)
                    foreach (ListItem LI in cb_analysisList.Items)
                    {
                        foreach (DataRow dr in dscheck.Tables[0].Rows)
                        {
                            if (dr[0].ToString() == LI.Value)
                                LI.Selected = true;
                        }
                    }
                dscheck.Dispose();
           
        }
        ds.Dispose();
        
        Query();

    }
     protected void DataBindAll()

{
    

     string str = "select id,AIName from t_M_AnalysisItem order by ClassID asc";
        DataSet ds = new MyDataOp(str).CreateDataSet();
        cb_analysisList.DataSource = ds;
        cb_analysisList.DataValueField = "id";
        cb_analysisList.DataTextField = "AIName";
        cb_analysisList.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            
            str = "select MonitorItem from t_M_MonitorItem where SampleID='" + strSelectedId + "' ";
                DataSet dscheck = new MyDataOp(str).CreateDataSet();
                if (dscheck.Tables[0].Rows.Count > 0)
                    foreach (ListItem LI in cb_analysisList.Items)
                    {
                        foreach (DataRow dr in dscheck.Tables[0].Rows)
                        {
                            if (dr[0].ToString() == LI.Value)
                                LI.Selected = true;
                        }
                    
            }
        }
        //Query();
}

    
     protected void txt_ReportName_TextChanged(object sender, EventArgs e)
     {

     }
}