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
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
//using OWC11;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using WebApp.Components;
using System.IO;

using WebApp.Components;

public partial class Sample_Access3 : System.Web.UI.Page
{
    public static string process = System.Configuration.ConfigurationManager.AppSettings["ProcessName"].ToString();
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
    private string strReportId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportId"]; }
        set { ViewState["strReportId"] = value; }
    }
    private string strReportName//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportName"]; }
        set { ViewState["strReportName"] = value; }
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
            txt_CreateDate.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");//onclick="SetDate(this,'yyyy-MM-dd hh:mm:ss')" readonly="readonly" 
            txt_QueryTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");

            Query();
            
            SetButton();// txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");


            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>编制验收方案</b></font>";
        }
    }


    #region 报告相关
    private void initial()
    {
        DAl.Sample sampleobj = new DAl.Sample();
        drop_rwtype.SelectedIndex = 0;

        lbl_reportNO.Text = "报告标识";
        lbl_AccessTime.Text = "委托时间";
        panel_wtdw.Visible = true;

        DataTable dtmode = sampleobj.GetMode("", "mode", "");
        drop_mode.DataSource = dtmode;
        drop_mode.DataTextField = "name";
        drop_mode.DataValueField = "code";
        drop_mode.DataBind();



        DataTable dtpurpose = sampleobj.GetPurpose("",1);
        drop_ItemList.DataSource = dtpurpose;
        drop_ItemList.DataTextField = "ItemName";
        drop_ItemList.DataValueField = "ItemID";
        drop_ItemList.DataBind();
        txt_lxtel.Text = "";
        txt_lxman.Text = "";
        txt_lxemail.Text = "";
        txt_address.Text = "";
        txt_ReportID.Text = "";
        txt_CreateDate.Text = "";
        txt_Projectname.Text = "";
        txt_wtdepart.Text = "";
        txt_xmfzr.Text = "";
        drop_urgent.Text = "";
        txt_Content.Text = "";



    }
    protected void btn_print_Click(object sender, EventArgs e)
    {
        try
        {
            RemoveFiles(Server.MapPath(".") + "\\");
        }
        catch { }

        Random rd = new Random();
        int oid = rd.Next(10000);

        string id = strSelectedId;
        string sql_sel = "SELECT t1.验收编号,t1.环保意见,t1.原审批建设内容,t1.企业名称,t2.发文日期,t2.发文人,t2.抄送,t2.主题词,t2.发文机构,t1.实际建设内容 as 发送对象  FROM t_项目验收 t1,t_项目发文信息 t2 where t1.验收编号 = t2.项目编号 and t2.发文类型 = '其他函件' and t1.验收编号 = '" + id + "'";
        DataSet ds_sel = new MyDataOp(sql_sel).CreateDataSet();
        if (ds_sel.Tables[0].Rows.Count > 0)
        {
            string[] xmbhTmp = ds_sel.Tables[0].Rows[0][0].ToString().Split('-');
            string xmbh1 = "20" + xmbhTmp[0].ToString();
            string xmbh2 = xmbhTmp[1].ToString();
            string spyj = ds_sel.Tables[0].Rows[0][1].ToString();
            string xmmc = ds_sel.Tables[0].Rows[0][2].ToString();
            string dwmc = ds_sel.Tables[0].Rows[0][3].ToString();
            string fwrq = string.Empty;
            string fwrqChinese = string.Empty;
            if (ds_sel.Tables[0].Rows[0][4].ToString().Trim() != null && ds_sel.Tables[0].Rows[0][4].ToString().Trim() != "")
            {
                fwrq = DateTime.Parse(ds_sel.Tables[0].Rows[0][4].ToString()).ToString("yyyy年MM月dd日");
                fwrqChinese = DateConvert(DateTime.Parse(ds_sel.Tables[0].Rows[0][4].ToString()).ToString("yyyy-M-d"));
            }
            string fwr = ds_sel.Tables[0].Rows[0][5].ToString();
            string cs = ds_sel.Tables[0].Rows[0][6].ToString();
            string ztc = ds_sel.Tables[0].Rows[0][7].ToString();
            string fwjg = ds_sel.Tables[0].Rows[0][8].ToString();
            string fsdx = ds_sel.Tables[0].Rows[0][9].ToString();

            try
            {

                string DocPath = ConfigurationManager.AppSettings["DocPath"].ToString();
                //string DocPath = "E:\\桐乡环保审批\\WebApp\\txsp\\Query\\";

                Word.Application app = new Word.Application();
                string TemplateFile = DocPath + "\\template\\template_zg.doc";

                //生成的具有模板样式的新文件
                string FileName = DocPath + "\\" + oid.ToString() + ".doc";

                File.Copy(TemplateFile, FileName);

                Word.Document doc = new Word.Document();

                object Obj_FileName = FileName;

                object Visible = false;

                object ReadOnly = false;

                object missing = System.Reflection.Missing.Value;

                //打开文件  

                doc = app.Documents.Open(ref Obj_FileName,

                ref missing, ref ReadOnly, ref missing,

                ref missing, ref missing, ref missing, ref missing,

                ref missing, ref missing, ref missing, ref Visible,

                ref missing, ref missing, ref missing,

                ref missing);

                doc.Activate();

                string[] sqArr = { "sq_xmbh_1", "sq_xmbh_2", "sq_xmmc", "sq_ysyj", "sq_date", "sq_cs", "sq_fsdx" };
                string[] replaceArr = { xmbh1, xmbh2, xmmc, spyj, fwrq, cs, fsdx };


                foreach (Word.Bookmark bm in doc.Bookmarks)
                {
                    switch (bm.Name)
                    {
                        case "sq_xmbh_1": bm.Select(); bm.Range.Text = replaceArr[0].ToString(); break;
                        case "sq_xmbh_2": bm.Select(); bm.Range.Text = replaceArr[1].ToString(); break;
                        case "sq_xmmc": bm.Select(); bm.Range.Text = replaceArr[2].ToString(); break;
                        //case "sq_ysyj": bm.Select(); bm.Range.Text = replaceArr[3].ToString(); break;                       
                        case "sq_date": bm.Select(); bm.Range.Text = replaceArr[4].ToString(); break;
                        case "sq_cs": bm.Select(); bm.Range.Text = replaceArr[5].ToString(); break;
                        case "sq_fsdx": bm.Select(); bm.Range.Text = replaceArr[6].ToString(); break;
                    }
                }

                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                app.Application.Quit(ref missing, ref missing, ref missing);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);

            }
            catch { }
        }  
    }
    #endregion
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
                    fi.Delete();
                }
            }
        }
    }

    /// <summary>
    /// 把字符串日期转换成相应的 中文日期
    /// </summary>
    /// <param name="date">字符串日期</param>
    /// <returns>中文日期</returns>
    public static string DateConvert(string date)
    {
        StringBuilder result = new StringBuilder();
        Regex theReg = new Regex(@"(d{2}|d{4})(/|-)(d{1,2})(/|-)(d{1,2})");

        //if (theReg.IsMatch(date))
        //{
        string[] str = null;
        if (date.Contains("-"))
        {
            str = date.Split('-');
        }
        else if (date.Contains("/"))
        {
            str = date.Split('/');
        }
        // str[0]中为年，将其各个字符转换为相应的汉字
        for (int i = 0; i < str[0].Length; i++)
        {
            result.Append(strChinese(int.Parse(str[0][i].ToString())));
        }
        result.Append("年");
        // str[1]中为月，将其各个字符转换成相应的汉字
        int month = int.Parse(str[1]);
        int MN1 = month / 10;
        int MN2 = month % 10;
        if (MN1 == 1)
        {
            result.Append("十");
            if (MN2 != 0)
            {
                result.Append(strChinese(MN2));
            }
        }
        if (MN1 == 0)
        {
            result.Append(strChinese(month));
        }

        result.Append("月");
        //str[2]中为日，将其各个字符转换成相应的汉字
        int day = int.Parse(str[2]);
        int DN1 = day / 10;
        int DN2 = day % 10;
        if (DN1 > 1)
        {
            result.Append(strChinese(DN1) + "十");
        }
        if (DN1 == 1)
        {
            result.Append("十");
        }
        if (DN2 != 0)
        {
            result.Append(strChinese(DN2));
        }
        result.Append("日");
        return result.ToString();

        //}
        //else
        //{
        //    return "日期格式错误！";
        //}

    }

    /// <summary>
    /// 把阿拉伯数字转换成中文数字
    /// </summary>
    /// <param name="nub">阿拉伯数字</param>
    /// <returns>中文数字</returns>


    public static string strChinese(int nub)
    {
        string[] arr = new string[] { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        char[] nubarr = nub.ToString().ToCharArray();
        string str = "";
        for (int i = 0, len = nubarr.Length; i < len; i++)
        {
            switch (nubarr[i])
            {
                case '0':
                    str += arr[0];
                    break;
                case '1':
                    str += arr[1];
                    break;
                case '2':
                    str += arr[2];
                    break;
                case '3':
                    str += arr[3];
                    break;
                case '4':
                    str += arr[4];
                    break;
                case '5':
                    str += arr[5];
                    break;
                case '6':
                    str += arr[6];
                    break;
                case '7':
                    str += arr[7];
                    break;
                case '8':
                    str += arr[8];
                    break;
                case '9':
                    str += arr[9];
                    break;
            }
        }
        return str;
    }
    /// <summary>
    /// 结束进程
    /// </summary>
    private void KillPreDevProcess()
    {
        Process[] p = Process.GetProcessesByName(process);
        if (p.Length != 0)
        {
            foreach (Process pCur in p)
            {
                pCur.Kill();
            }
        }
       
    }
    //退回到踏勘
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        //保存项目信息项目
        Entity.AccessReport entity = new Entity.AccessReport();
        DAl.Report reportobj = new DAl.Report();
       
        entity.CreateDate = DateTime.Now;//创建时间
        entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
        entity.StatusID = 2;//保存
        entity.backflag = 1;
        entity.Content = txt_Content.Text.Trim();
        entity.ID = int.Parse(strReportId);
        if (reportobj.UpateYSfanAn(entity) == 1)
        {
            WebApp.Components.Log.SaveLog("方案编写退回保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 3);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('方案编写退回保存成功！')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("方案编写退回保存失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 3);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('方案编写退回保存失败！')", true);

        }
        Query();
    }

    //方案编写保存
    protected void btn_Save_Click(object sender, EventArgs e)
    { 
        //保存项目信息项目
        Entity.AccessReport entity = new Entity.AccessReport();
        DAl.Report reportobj = new DAl.Report();
       
        //方案编写保存
        entity.ReportNO = "";//
        entity.CreateDate = DateTime.Now;//创建时间
        entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
        entity.StatusID = 3;//保存
        entity.backflag = 0;
        entity.Content = txt_Content.Text.Trim();
        entity.ID = int.Parse(strReportId);

        if (reportobj.UpateYSfanAn(entity) == 1)
        {
            WebApp.Components.Log.SaveLog("方案编写保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(),4);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('方案编写保存成功！')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("方案编写保存失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 4);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('方案编写保存失败！')", true);

        }
        Query();
    }
    protected void drop_rwtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAl.Sample sampleobj = new DAl.Sample();

        lbl_reportNO.Text = "报告标识";
        lbl_AccessTime.Text = "委托时间";
        panel_wtdw.Visible = true;
        DataTable dtmode = sampleobj.GetMode("", "mode", "");
        drop_mode.DataSource = dtmode;
        drop_mode.DataTextField = "name";
        drop_mode.DataValueField = "code";
        drop_mode.DataBind();

    }
    protected void SetButton()
    {
        //if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        //{
        //    btn_Add.Enabled = false;
        //    btn_OK.Enabled = false;

        //    btn_Save.Enabled = false;

        //}
        //else
        //{
        //    btn_Add.Enabled = true;
        //    btn_OK.Enabled = true;
        //    btn_Save.Enabled = true;
        // btn_AddSample.Enabled = true;
        //  btn_OKSample.Enabled = true;
        // btn_item.Enabled = true;
        //for (int i = 0; i < grdvw_List.Rows.Count; i++)
        //{
        //    ImageButton btn = (ImageButton)grdvw_List.Rows[i].FindControl("btn_delete");
        //    if(btn!=null)
        //    btn.Visible = true;
        //}
        //    }
    }
    private void Query()
    {
        string strSample = "";
        string strDate = "";
        if (txt_samplequery.Text != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text + "%'";
        if (drop_tkwether.SelectedIndex == 0)
        {
            strSample += " and (tkwether='0')";
           
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>编制验收方案</b></font>";

        }
        
        if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) != "1")
            strSample += " and chargeman='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'";
        if (txt_QueryTime.Text != "")
            strDate = " and (year(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_Y_FlowInfo.id,t_Y_FlowInfo.ReportAccessDate 时间,t_Y_FlowInfo.ItemType,ItemName 项目类型,ReportName 报告标识,urgent 备注,t_Y_FlowInfo.Ulevel,Projectname 项目名称,t_R_UserInfo.Name 项目负责人,rwclass,jcmethod,address,lxman,lxtel,lxemail,wtdepart,wether,varman1,vardate1,varremark1,t_Y_FlowInfo.chargeman,varman2,vardate2,varremark2,tkwether,varremark3,xcjcbackflag,xcjcbackremark  from t_Y_FlowInfo,t_M_ItemInfo,t_R_UserInfo where  t_Y_FlowInfo.ItemType=t_M_ItemInfo.ItemID and (StatusID='3') and chargeman=t_R_UserInfo.UserID  and hanwether=1   " + strSample + strDate + " order by t_Y_FlowInfo.ReportAccessDate";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        DataColumn dcc = new DataColumn("紧急程度");
        ds.Tables[0].Columns.Add(dcc);
        DataColumn dc = new DataColumn("监测方式");
        ds.Tables[0].Columns.Add(dc);
        DataColumn dc0 = new DataColumn("任务类型");
        ds.Tables[0].Columns.Add(dc0);
        DataColumn dc1 = new DataColumn("委托单位");
        ds.Tables[0].Columns.Add(dc1);

        DAl.Sample getobj = new DAl.Sample();
        DataTable dtmode = getobj.GetMode("", "mode", "");
        DAl.Station get = new DAl.Station();
        DataTable dtstation = get.GetWTByName("");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["Ulevel"].ToString() == "1")
                dr["紧急程度"] = "紧急";
            else
                dr["紧急程度"] = "一般";
            if (dr["rwclass"].ToString() == "1")
                dr["任务类型"] = "委托监测";
            else
                dr["任务类型"] = "例行监测";

            DataRow[] drsel = dtmode.Select("code='" + dr["jcmethod"].ToString() + "'");
            if (drsel.Length == 1)
                dr["监测方式"] = drsel[0]["name"].ToString();

            else
                dr["监测方式"] = "";

           
            dr["委托单位"] = dr["wtdepart"].ToString().Trim();
           
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
    //提交
    protected void grdvw_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        strReportId = grdvw_List.Rows[e.NewSelectedIndex].Cells[1].Text.Trim();
        Entity.AccessReport entity = new Entity.AccessReport();
        DAl.Report reportobj = new DAl.Report();
        DataTable dt = reportobj.Get(int.Parse(strReportId));
        entity.CreateDate = DateTime.Now;//创建时间
        entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
        entity.StatusID = 3.5;
        entity.backflag = 0;
        entity.Green = 0;
        entity.WTDate = DateTime.Parse(dt.Rows[0]["ReportAccessDate"].ToString());//委托日期
        entity.classID = int.Parse(dt.Rows[0]["rwclass"].ToString());//委托任务

        entity.WTMan = dt.Rows[0]["wtdepart"].ToString(); ;//委托单位
        entity.lxEmail = dt.Rows[0]["lxemail"].ToString();
        entity.lxMan = dt.Rows[0]["lxman"].ToString();
        entity.lxtel = dt.Rows[0]["lxtel"].ToString();
        entity.address = dt.Rows[0]["address"].ToString();

        entity.chargeman = dt.Rows[0]["chargeman"].ToString();//项目负责人
        entity.level = dt.Rows[0]["Ulevel"].ToString();//紧急程度
        entity.Mode = dt.Rows[0]["jcmethod"].ToString();//监测方式
        entity.Content = dt.Rows[0]["varremark3"].ToString();//备注
        entity.WTNO = dt.Rows[0]["ReportName"].ToString();//委托协议编码，报告标识
        entity.ProjectName = dt.Rows[0]["Projectname"].ToString();//项目名称
        entity.TypeID = int.Parse(dt.Rows[0]["ItemType"].ToString());
        //rwclass,CreateDate,UserID,ReportAccessDate,ItemType,ReportName,Ulevel,urgent,wtdepart,lxman,lxtel,lxemail,address,jcmethod,Projectname,StatusID,chargeman
        entity.ID = int.Parse(strReportId);
        if (reportobj.UpateYSfanAn(entity) == 1)
        {
            WebApp.Components.Log.SaveLog("方案编制保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('数据保存成功！')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("方案编制保存失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('数据添保存失败！')", true);

        }
        Query();
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        initial();
        string slevel = "";

        //btn_Save.Visible = true;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim() != "&nbsp;")
            txt_ReportID.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//报告标识
        strReportName = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        strReportId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();

        lbl_Type.Text = "查看";

        txt_CreateDate.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim();//报告创建日期

        ListItem choose = new ListItem("请选择", "-1");
        if (grdvw_List.Rows[e.NewEditIndex].Cells[10].Text != "&nbsp;")//任务类型
            drop_rwtype.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[10].Text;

        panel_wtdw.Visible = drop_rwtype.SelectedValue == "1";


        drop_urgent.Text = "";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[6].Text != "&nbsp;")//备注
            drop_urgent.Text = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text;



        string itemtid = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text;
        //drop_ItemList.Items.Add(choose);
        drop_ItemList.Items.FindByValue(itemtid).Selected = true;
        if (drop_rwtype.SelectedValue == "1")//委托监测
        {
            if (grdvw_List.Rows[e.NewEditIndex].Cells[16].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[16].Text.Trim() != "")
            {
                txt_wtdepart.Text = grdvw_List.Rows[e.NewEditIndex].Cells[16].Text.Trim();
            }
            if (grdvw_List.Rows[e.NewEditIndex].Cells[11].Text != "&nbsp;")//监测方式
                drop_mode.SelectedValue = grdvw_List.Rows[e.NewEditIndex].Cells[11].Text;
            if (grdvw_List.Rows[e.NewEditIndex].Cells[12].Text != "&nbsp;")//地址
                txt_address.Text = grdvw_List.Rows[e.NewEditIndex].Cells[12].Text;
            if (grdvw_List.Rows[e.NewEditIndex].Cells[13].Text != "&nbsp;")//联系人
                txt_lxman.Text = grdvw_List.Rows[e.NewEditIndex].Cells[13].Text;
            if (grdvw_List.Rows[e.NewEditIndex].Cells[14].Text != "&nbsp;")//联系人手机
                txt_lxtel.Text = grdvw_List.Rows[e.NewEditIndex].Cells[14].Text;
            if (grdvw_List.Rows[e.NewEditIndex].Cells[15].Text != "&nbsp;")//联系人邮箱
                txt_lxemail.Text = grdvw_List.Rows[e.NewEditIndex].Cells[15].Text;
        }

        if (grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim() != "")
        {
            txt_Projectname.Text = grdvw_List.Rows[e.NewEditIndex].Cells[8].Text.Trim();
        }

        if (grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim() != "")
        {
            txt_xmfzr.Text = grdvw_List.Rows[e.NewEditIndex].Cells[9].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim() != "")
        {
            slevel = grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim();
            drop_level.SelectedValue = slevel;
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[17].Text.Trim() != "1")
            rbl_wether.SelectedValue = "0";
        else
            rbl_wether.SelectedValue = "1";
        if (grdvw_List.Rows[e.NewEditIndex].Cells[20].Text.Trim() != "&nbsp;")
            txt_remark1.Text = grdvw_List.Rows[e.NewEditIndex].Cells[20].Text.Trim();
        if (grdvw_List.Rows[e.NewEditIndex].Cells[25].Text.Trim() != "1")
        {
            rbl_tkwether.SelectedValue = "0";
        }
        else
        {
            rbl_tkwether.SelectedValue = "1";
            if (grdvw_List.Rows[e.NewEditIndex].Cells[26].Text.Trim() != "&nbsp;")
                txt_Content.Text = grdvw_List.Rows[e.NewEditIndex].Cells[26].Text.Trim();
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[24].Text.Trim() != "&nbsp;")
            txt_remark2.Text = grdvw_List.Rows[e.NewEditIndex].Cells[24].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
        if (rbl_wether.SelectedIndex == 0)
            lbl_remark.Text = "踏勘情况";
        else
            lbl_remark.Text = "整改意见";
        //方案编写备注
        if (grdvw_List.Rows[e.NewEditIndex].Cells[26].Text.Trim() != "&nbsp;")
            txt_Content.Text = grdvw_List.Rows[e.NewEditIndex].Cells[26].Text.Trim();
        //根据是否现场监测返回来确定是否现实退回意见
        if (grdvw_List.Rows[e.NewEditIndex].Cells[27].Text.Trim() == "1")
        {
            panel_back.Visible = true;
            if (grdvw_List.Rows[e.NewEditIndex].Cells[28].Text.Trim() != "&nbsp;")
            {
                txt_back.Text = grdvw_List.Rows[e.NewEditIndex].Cells[28].Text.Trim();

            }
           
        }
        else
        {
            panel_back.Visible = false;
            

        }


    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "查看";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);
            if (drop_tkwether.SelectedIndex == 0)
            {
                TableCell headerDetail1 = new TableCell();
                headerDetail1.Text = "方案上传";
                headerDetail1.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                headerDetail1.Width = 60;
                e.Row.Cells.Add(headerDetail1);
                TableCell headerDetail2 = new TableCell();
                headerDetail2.Text = "提交";
                headerDetail2.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                headerDetail2.Width = 60;
                e.Row.Cells.Add(headerDetail2);
            }
           
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

            if (drop_tkwether.SelectedIndex == 0)
            {
                TableCell MenuSet = new TableCell();
                MenuSet.Width = 60;
                MenuSet.Style.Add("text-align", "center");
                ImageButton btMenuSet = new ImageButton();
                btMenuSet.ImageUrl = "~/images/Upload.gif";
                btMenuSet.CommandName = "Delete";
                //btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
                MenuSet.Controls.Add(btMenuSet);
                e.Row.Cells.Add(MenuSet);

                TableCell tabcDel = new TableCell();
                tabcDel.Width = 30;
                tabcDel.Style.Add("text-align", "center");
                ImageButton ibtnDel = new ImageButton();
                ibtnDel.ImageUrl = "~/images/Detail.gif";
                ibtnDel.ID = "btn_delete";
                ibtnDel.CommandName = "Select";
                ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定提交该项吗？')) return false;");
                ////if (Int16.Parse(Request.Cookies["Cookies"].Values["u_level"].ToString()) > 2 || Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(0, 1) == "0")
                ////{
                ////    ibtnDel.Enabled = false;
                ////}
                //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
                //{
                tabcDel.Controls.Add(ibtnDel);
                e.Row.Cells.Add(tabcDel);
            }

            //}
            //else
            //{
            //    ibtnDel.Visible = false;
            //}
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            ////绑定数据后，隐藏4,5,6,7列 
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[7].Visible = false;
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
            e.Row.Cells[24].Visible = false;
            e.Row.Cells[25].Visible = false;
            e.Row.Cells[26].Visible = false;
            e.Row.Cells[27].Visible = false;
            e.Row.Cells[28].Visible = false;
            e.Row.Cells[30].Visible = false;
            e.Row.Cells[31].Visible = false;
        }
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "window.open('attachment.aspx?rid=" + strSelectedId + "&&fid=1&&sfile=" + HttpUtility.UrlEncode("access.aspx") + "','theNewWindow','width=850,height=400,location=no,menubar=no,screenX=175,screenY=175,status=no,toolbar=no')", true);

        Query();
    }
    #endregion


    protected void btn_query_Click(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        txt_ReportID.Text = "";
        Query();
    }



  
  
    private string Verify()
    {
        string strErrorInfo = "";

        //if (drop_ItemList.SelectedValue == "-1")
        //    strErrorInfo += "请选择项目类型！\\n";
        //else if (txt_CreateDate.Text == "")
        //    strErrorInfo += "请填写时间！\\n";

        //else if (txt_Projectname.Text.Trim() == "-1")
        //    strErrorInfo += "请选择项目名称！\\n";
        //else if (txt_wtdepart.Text.Trim() == "-1")
        //    strErrorInfo += "请选择委托单位！\\n";
        //else if (DropList_client.SelectedValue == "-1")
        //    strErrorInfo += "请选择区域！\\n";

        return strErrorInfo;
    }

    protected void txt_wtdepart_TextChanged(object sender, EventArgs e)
    {
        DAl.Station StationObj = new DAl.Station();
        DataTable dt = StationObj.GetStationByName(txt_wtdepart.Text.Trim());
        if (dt != null)
        {
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["单位详细地址"].ToString() != "&nbsp;")
                    txt_address.Text = dt.Rows[0]["单位详细地址"].ToString();
                if (dt.Rows[0]["环保负责人"].ToString() != "&nbsp;")
                    txt_lxman.Text = dt.Rows[0]["环保负责人"].ToString();
                if (dt.Rows[0]["mobile3"].ToString() != "&nbsp;")
                    txt_lxtel.Text = dt.Rows[0]["mobile3"].ToString();
                if (dt.Rows[0]["电子邮箱"].ToString() != "&nbsp;")
                    txt_lxemail.Text = dt.Rows[0]["电子邮箱"].ToString();
            }
        }
    }
    protected void rbl_tkwether_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_tkwether.SelectedValue.ToString() == "0")
            lbl_remark.Text = "踏勘情况";
        else
            lbl_remark.Text = "整改意见";
    }


}
