//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
//using System.Data.SqlClient;
//using System.Text;
//using System.Text.RegularExpressions;
////using System.Diagnostics;
////using OWC11;
//using Microsoft.Office.Interop.Word;
//using Word = Microsoft.Office.Interop.Word;
//using WebApp.Components;
//using System.IO;
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
using System.Collections.Generic;
using OWC11;
using System.IO;
using System.Text;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using WebApp.Components;
using Entity;
using System.Text.RegularExpressions;


public partial class Sample_Access4 : System.Web.UI.Page
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
    private string strReportNO//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strReportNO"]; }
        set { ViewState["strReportNO"] = value; }
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
            DAl.Station stationobj = new DAl.Station();
            DataTable dtJK = stationobj.GetJKByName("", "1");
            cbl_cs.DataSource = dtJK;
            cbl_cs.DataTextField = "单位全称";
            cbl_cs.DataValueField = "id";
            cbl_cs.DataBind();
           // MyStaVoid.BindList("单位全称", "id", "select id,单位全称 from t_委托单位 where type=1", cbl_cs);
            SetButton();// txt_QueryTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");


            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>编制整改函</b></font>";
        }
    }


    #region 报告相关
    private void initial()
    {
         foreach (ListItem li in cbl_cs.Items)
        {
            li.Selected = false;
            }
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

        string id = strReportId;
        string sql_sel = "SELECT hanNO,hContent,hcreatedate,wtdepart 单位全称,Projectname,hfwdw,hcs  FROM t_Y_FlowInfo  where  t_Y_FlowInfo.id= '" + id + "'";
      
       // string sql_sel = "SELECT hanNO,hContent,hcreatedate,单位全称,Projectname,hfwdw,hcs  FROM t_Y_FlowInfo inner join t_委托单位 on t_委托单位.type=0 and t_委托单位.id=wtdepart where  t_Y_FlowInfo.id= '" + id + "'";
        DataSet ds_sel = new MyDataOp(sql_sel).CreateDataSet();
        if (ds_sel.Tables[0].Rows.Count > 0)
        {

            int i = ds_sel.Tables[0].Rows[0]["hanNO"].ToString().LastIndexOf('〕');
            string xmbh_1 = ds_sel.Tables[0].Rows[0]["hanNO"].ToString().Substring(i-4,4);
            string xmbh_2 = ds_sel.Tables[0].Rows[0]["hanNO"].ToString().Substring(i+1);
            string zgyj = ds_sel.Tables[0].Rows[0]["hContent"].ToString();
            string xmmc = ds_sel.Tables[0].Rows[0]["Projectname"].ToString();
            string dwmc = ds_sel.Tables[0].Rows[0]["单位全称"].ToString();
            string fwrq = string.Empty;
            string fwrqChinese = string.Empty;
            if (ds_sel.Tables[0].Rows[0][2].ToString().Trim() != null && ds_sel.Tables[0].Rows[0][2].ToString().Trim() != "")
            {
                fwrq = DateTime.Parse(ds_sel.Tables[0].Rows[0][2].ToString()).ToString("yyyy年MM月dd日");
                fwrqChinese = DateConvert(DateTime.Parse(ds_sel.Tables[0].Rows[0][2].ToString()).ToString("yyyy-M-d"));
            }
            string fwr = ds_sel.Tables[0].Rows[0]["hfwdw"].ToString();
            string cs = ds_sel.Tables[0].Rows[0]["hcs"].ToString();
            //string ztc = ds_sel.Tables[0].Rows[0][7].ToString();
            //string fwjg = ds_sel.Tables[0].Rows[0][8].ToString();
            //string fsdx = ds_sel.Tables[0].Rows[0][9].ToString();

            try
            {

                string DocPath = Server.MapPath("./");// ConfigurationManager.AppSettings["DocPath"].ToString();
                //string DocPath = "E:\\桐乡环保审批\\WebApp\\txsp\\Query\\";

                Word.Application app = new Word.Application();
                string TemplateFile = DocPath + "\\template\\template_zg.doc";

                //生成的具有模板样式的新文件
                string FileName = DocPath + "\\temp\\" + oid.ToString() + ".doc";

                File.Copy(TemplateFile, FileName);

                Word.Document doc = new Word.Document();

                object Obj_FileName = FileName;

                object Visible = false;

                object ReadOnly = false;

                object missing = System.Reflection.Missing.Value;

                //打开文件  
                object IsSave = true;
                try
                {
                    doc = app.Documents.Open(ref Obj_FileName,

                    ref missing, ref ReadOnly, ref missing,

                    ref missing, ref missing, ref missing, ref missing,

                    ref missing, ref missing, ref missing, ref Visible,

                    ref missing, ref missing, ref missing,

                    ref missing);

                    doc.Activate();
                // throw new Exception("test");
                }
                catch (Exception exp)
                {
                    Log.log a = new Log.log();
                    a.Log("文档打开"+exp.ToString());
                    doc.Close(ref IsSave, ref missing, ref missing);
                    app.Application.Quit(ref missing, ref missing, ref missing);
                    NAR(doc);
                    NAR(app);
                    return;
                }

                string[] sqArr = { "sq_xmbh_1", "sq_xmbh_2", "sq_xmmc", "sq_zgyj", "sq_date", "sq_cs", "sq_fsdx","sq_fwdw" };
                string[] replaceArr = { xmbh_1, xmbh_2, xmmc, zgyj, fwrq, cs, dwmc, fwr };


                foreach (Word.Bookmark bm in doc.Bookmarks)
                {
                    switch (bm.Name)
                    {
                        case "sq_xmbh_1": bm.Select(); bm.Range.Text = replaceArr[0].ToString(); break;
                        case "sq_xmbh_2": bm.Select(); bm.Range.Text = replaceArr[1].ToString(); break;
                        case "sq_xmmc": bm.Select(); bm.Range.Text = replaceArr[2].ToString(); break;
                        case "sq_zgyj": bm.Select(); bm.Range.Text = replaceArr[3].ToString(); break;                       
                        case "sq_date": bm.Select(); bm.Range.Text = replaceArr[4].ToString(); break;
                        case "sq_cs": bm.Select(); bm.Range.Text = replaceArr[5].ToString(); break;
                        case "sq_fsdx": bm.Select(); bm.Range.Text = replaceArr[6].ToString(); break;
                        case "sq_fwdw": bm.Select(); bm.Range.Text = replaceArr[7].ToString(); break;
                    }
                }

               
                doc.Close(ref IsSave, ref missing, ref missing);
                app.Application.Quit(ref missing, ref missing, ref missing);
                NAR(doc);
                NAR(app);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('temp/" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);

            }
          catch (Exception exp)
                {
                    Log.log a = new Log.log();
                    a.Log("标签替换"+exp.ToString());
                   
                }
        }  
    }

    private void NAR(object o)
    {
        try
        {//System.Runtime.InteropServices.Marshal.ReleaseComObject
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject
            while (System.Runtime.InteropServices.Marshal.FinalReleaseComObject(o) > 0) ;
        }
        catch { }
        finally
        {
            o = null;
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
    protected void btn_back_Click(object sender, EventArgs e)
    { //保存项目信息项目
        Entity.AccessReport entity = new Entity.AccessReport();
        DAl.Report reportobj = new DAl.Report();

        entity.ReportNO = strReportNO;
        entity.CreateDate = DateTime.Now;//创建时间
        entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
        entity.StatusID = 2;//保存
        entity.backflag = 1;
        entity.Remark = txt_Content.Text.TrimEnd();
        entity.ID = int.Parse(strReportId);
        entity.hcs = txt_cs.Text.Trim();
        entity.hfwdw = txt_fwdw.Text.Trim();
        if (reportobj.UpateYShan(entity) == 1)
        {
            WebApp.Components.Log.SaveLog("函编制退回踏勘保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('函编制退回踏勘成功！')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("函编制退回踏勘保存失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('函编制退回踏勘失败！')", true);

        }
        Query();
    }
    
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        //保存项目信息项目
        Entity.AccessReport entity = new Entity.AccessReport();
        DAl.Report reportobj = new DAl.Report();

        entity.ReportNO = strReportNO;
        entity.CreateDate = DateTime.Now;//创建时间
        entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
        entity.StatusID = 3;//保存
        entity.backflag = 0;
        entity.Content = txt_Content.Text.TrimEnd();
        entity.ID = int.Parse(strReportId);
        entity.hcs = txt_cs.Text.Trim();
        entity.hfwdw = txt_fwdw.Text.Trim();
        if (reportobj.UpateYShan(entity) == 1)
        {
            WebApp.Components.Log.SaveLog("函编制保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('函保存成功！')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("函编制保存失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('函保存失败！')", true);

        }
        Query();
    }
    
    protected void btn_Save_Click(object sender, EventArgs e)
    { 
       
        //提交项目
        //保存项目信息项目
        Entity.AccessReport entity = new Entity.AccessReport();
        DAl.Report reportobj = new DAl.Report();
        //生成文号
        entity.ReportNO ="";//t_报告编号
        entity.CreateDate = DateTime.Now;//创建时间
        entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();//创建人
        entity.StatusID = 5;//提交
        entity.backflag = 0;
        entity.Content = txt_Content.Text.TrimEnd();
        entity.ID = int.Parse(strReportId);
        entity.hcs = txt_cs.Text.Trim();
        entity.hfwdw = txt_fwdw.Text.Trim();
        if (reportobj.UpateYShanFinish(entity) == 1)
        {
            WebApp.Components.Log.SaveLog("函编制保存成功！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddSuccess", "hiddenDetail();alert('函保存成功！')", true);
        }
        else
        {
            WebApp.Components.Log.SaveLog("函编制保存失败！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickAddFail", "alert('函保存失败！')", true);

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
        //strSelectedId=txt_samplequery.Text;
        string strSample = "";
        string strDate = "";
        if (txt_samplequery.Text != "")
            strSample = "and ReportName like'%" + txt_samplequery.Text + "%'";
        strSample += " and tkwether='1'";
        if (drop_tkwether.SelectedValue.ToString() == "3")
        {
            strSample += " and (statusID='3')";
            btn_OK.Visible = true;
            btn_Save.Visible = true;
            panel_han.Visible = true;
            btn_print.Visible = false;
            btn_back.Visible = true;
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>编制整改函</b></font>";

        }
        else
        {
            strSample += " and (statusID='5')";

            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) != "1")
            {
                btn_OK.Visible = false;
            }
            else
                btn_OK.Visible = true;
            btn_back.Visible = false;
            btn_Save.Visible = false;
            panel_han.Visible = true;
            btn_print.Visible = true;
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR:#2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>编制整改函</b></font>";

        }
        if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) != "1")
            strSample += " and chargeman='" + Request.Cookies["Cookies"].Values["u_id"].ToString() + "'";
        if (txt_QueryTime.Text != "")
            strDate = " and (year(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Year + "' AND month(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Month + "' and day(ReportAccessDate)= '" + DateTime.Parse(txt_QueryTime.Text.Trim() + " 00:00:00").Day + "')";
        string strSql = "select t_Y_FlowInfo.id,t_Y_FlowInfo.ReportAccessDate 时间,t_Y_FlowInfo.ItemType,ItemName 项目类型,ReportName 报告标识,urgent 备注,t_Y_FlowInfo.Ulevel,Projectname 项目名称,t_R_UserInfo.Name 项目负责人,rwclass,jcmethod,address,lxman,lxtel,lxemail,wtdepart,wether,varman1,vardate1,varremark1,t_Y_FlowInfo.chargeman,varman2,vardate2,varremark2,tkwether,hcontent,hfwdw,hcs,hanNO 函文号  from t_Y_FlowInfo,t_M_ItemInfo,t_R_UserInfo where  t_Y_FlowInfo.ItemType=t_M_ItemInfo.ItemID and chargeman=t_R_UserInfo.UserID  and hanwether=1   " + strSample + strDate + " order by t_Y_FlowInfo.ReportAccessDate";

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

            //if (dr["wtdepart"].ToString().Trim() != "")
            //{
            //    DataRow[] drdep = dtstation.Select("id=" + dr["wtdepart"].ToString().Trim() + "");
            //    if (drdep.Length == 1)
            //        dr["委托单位"] = drdep[0]["单位全称"].ToString();

            //    else
            dr["委托单位"] = dr["wtdepart"].ToString().Trim();
           // }

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
        entity.StatusID = 4;
        entity.backflag = 0;
        entity.WTDate = DateTime.Parse(dt.Rows[0]["ReportAccessDate"].ToString());//委托日期
        entity.classID = int.Parse(dt.Rows[0]["rwclass"].ToString());//委托任务
       // entity.ReportNO = dt.Rows[0]["ReportNO"].ToString(); 
        entity.WTMan = dt.Rows[0]["wtdepart"].ToString(); ;//委托单位
        entity.lxEmail = dt.Rows[0]["lxemail"].ToString();
        entity.lxMan = dt.Rows[0]["lxman"].ToString();
        entity.lxtel = dt.Rows[0]["lxtel"].ToString();
        entity.address = dt.Rows[0]["address"].ToString();

        entity.chargeman = dt.Rows[0]["chargeman"].ToString();//项目负责人
        entity.level = dt.Rows[0]["Ulevel"].ToString();//紧急程度
        entity.Mode = dt.Rows[0]["jcmethod"].ToString();//监测方式
        entity.Remark = dt.Rows[0]["urgent"].ToString();//备注
        entity.WTNO = dt.Rows[0]["ReportName"].ToString();//委托协议编码，报告标识
        entity.ProjectName = dt.Rows[0]["Projectname"].ToString();//项目名称
        entity.TypeID = int.Parse(dt.Rows[0]["ItemType"].ToString());
        //rwclass,CreateDate,UserID,ReportAccessDate,ItemType,ReportName,Ulevel,urgent,wtdepart,lxman,lxtel,lxemail,address,jcmethod,Projectname,StatusID,chargeman
        entity.ID = int.Parse(strReportId);
        if (reportobj.UpateYSfa(entity) == 1)
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
        cbl_cs.Visible = false;
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
       
            if (grdvw_List.Rows[e.NewEditIndex].Cells[33].Text.Trim() != "&nbsp;" && grdvw_List.Rows[e.NewEditIndex].Cells[33].Text.Trim() != "")
            {
                txt_wtdepart.Text = grdvw_List.Rows[e.NewEditIndex].Cells[33].Text.Trim();
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
            txt_remark1.Text = grdvw_List.Rows[e.NewEditIndex].Cells[20].Text;
        if (grdvw_List.Rows[e.NewEditIndex].Cells[25].Text.Trim() != "1")
        {
            rbl_tkwether.SelectedValue = "0";
        }
        else
        {
            rbl_tkwether.SelectedValue = "1";
            if (grdvw_List.Rows[e.NewEditIndex].Cells[26].Text.Trim() != "&nbsp;")
                txt_Content.Text = grdvw_List.Rows[e.NewEditIndex].Cells[26].Text;
            if (grdvw_List.Rows[e.NewEditIndex].Cells[27].Text.Trim() != "&nbsp;")
                txt_fwdw.Text = grdvw_List.Rows[e.NewEditIndex].Cells[27].Text.Trim();
            if (grdvw_List.Rows[e.NewEditIndex].Cells[28].Text.Trim() != "&nbsp;")
                txt_cs.Text = grdvw_List.Rows[e.NewEditIndex].Cells[28].Text.Trim();
             if (grdvw_List.Rows[e.NewEditIndex].Cells[29].Text.Trim() != "&nbsp;")
                 strReportNO = grdvw_List.Rows[e.NewEditIndex].Cells[29].Text.Trim();
            
        }
        if (grdvw_List.Rows[e.NewEditIndex].Cells[24].Text.Trim() != "&nbsp;")
            txt_remark2.Text = grdvw_List.Rows[e.NewEditIndex].Cells[24].Text.Trim();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
        if (rbl_wether.SelectedIndex == 0)
            lbl_remark.Text = "踏勘情况";
        else
            lbl_remark.Text = "整改意见";



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
            e.Row.Cells[32].Visible = false;
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


    protected void lb_cs_Click(object sender, EventArgs e)
    {
        if (cbl_cs.Visible == true)
            cbl_cs.Visible = false;
        else
            cbl_cs.Visible = true;
    }
    protected void cbl_cs_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_cs.Text = "";
        foreach (ListItem li in cbl_cs.Items)
        {
            if (li.Selected)
            {
                txt_cs.Text += li.Text + ",";
            }
        }
        if (txt_cs.Text.Substring(txt_cs.Text.Length-1, 1) == ",")
        { txt_cs.Text = txt_cs.Text.Substring(0,txt_cs.Text.Length - 1); }
    }
}
