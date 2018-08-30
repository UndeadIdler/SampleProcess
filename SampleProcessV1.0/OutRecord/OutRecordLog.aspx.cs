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
using System.IO;
using System.Text;

using WebApp.Components;
using Microsoft.Office.Interop.Owc11;

public partial class OutRecordLog : System.Web.UI.Page
{
    //public string strTable = "";
    //public string strTableC = "";
    public int num
    {
        get { return (int)ViewState["num"]; }
        set { ViewState["num"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "日志列表";
        if (!IsPostBack)
        {
            try
            {
                num = int.Parse(System.Configuration.ConfigurationManager.AppSettings["num"]);

            }
            catch
            {
                num = 8; 
            }
            #region 初始化页面               
            txt_StartTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");            
            txt_EndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            MyStaVoid.BindList("DepartName", "DepartID","select * from t_M_DepartInfo where DepartID!='0'", ddl_depart);
            ListItem li = new ListItem("所有", "-1");
            ddl_depart.Items.Add(li);
            ddl_depart.SelectedIndex = ddl_depart.Items.Count - 1;
            Query(0);
            #endregion
        }
    }    
    private void Query(int Export)
    {
        StringBuilder strshow = new StringBuilder();
        //Microsoft.Office.Interop.Owc11.SpreadsheetClass xlSheet = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
        //if (Export == 1)
        //{
        //    xlSheet.ActiveSheet.Cells[1, 1] = "日期";
        //    xlSheet.ActiveSheet.Cells[1, 2] = "项目类型";
        //    xlSheet.ActiveSheet.Cells[1, 3] = "受理";
        //    xlSheet.ActiveSheet.Cells[1, 4] = "外业室确认";
        //    xlSheet.ActiveSheet.Cells[1, 5] = "踏勘";
        //    xlSheet.ActiveSheet.Cells[1, 6] = "方案室内审核";
        //    xlSheet.ActiveSheet.Cells[1, 7] = "方案上传";
        //    xlSheet.ActiveSheet.Cells[1, 8] = "综合室确认";
        //    xlSheet.ActiveSheet.Cells[1, 9] = "签订协议";
        //    xlSheet.ActiveSheet.Cells[1, 10] = "现场监测";
        //    xlSheet.ActiveSheet.Cells[1, 11] = "移交监测数据";
        //    xlSheet.ActiveSheet.Cells[1, 12] = "报告上传";
        //   xlSheet.ActiveSheet.Cells[1, 13] = "室内审核";
        //    xlSheet.ActiveSheet.Cells[1, 14] = "外业室报告修改";
        //    xlSheet.ActiveSheet.Cells[1, 15] = "综合室审定";
        //    xlSheet.ActiveSheet.Cells[1, 16] = "外业室修改报告";
        //    xlSheet.ActiveSheet.Cells[1, 17] = "装订";

        //    xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, 14]).Font.set_Bold(true);
        //    xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, 14]).Font.set_Size(10);
        //}
        string str1="";
        if (ddl_depart.SelectedIndex != ddl_depart.Items.Count - 1)
            str1 = " and DepartID='" + ddl_depart.SelectedValue.ToString() + "'";
        string strtemp = "select Name,UserID,DepartID from t_R_UserInfo where DepartID<>0 and flag=0 " + str1 + " order by orderstr";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        int usernum = ds_User.Tables[0].Rows.Count / num + 1;
        int usernums = ds_User.Tables[0].Rows.Count % num;
        strshow.Append("<table id='tableid' class='listTable' boder='0' cellspacing='1' width='98%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月dd日") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月dd日") + " 工作日志列表</b></font></caption><tbody><tr align='center'>");
         strshow.Append("<th><b>日期</b></th>");
         strshow.Append("<th><b>部门</b></th>");
         strshow.Append("<th colspan = '" + num + "' ><b>工作日志</b></th>");
        strshow.Append("</tr>");
        //dayin
        //strTableP = "<table id='tableid' width='98%' style= 'BORDER-COLLAPSE:collapse' borderColor= '#00000' border= '1'> <caption><FONT style='size=3 HEIGHT: 50px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月dd日") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月dd日") + " 受理验收流程列表</b></font></caption><tbody><tr align='center'>";
        //strTableP += "<th>序号</th>";
        //strTableP += "<th>项目类型</th>";
        //strTableP += "<th>受理</th>";
        //strTableP += "<th>外业室确认</th>";
        //strTableP += "<th>踏勘</th>";
        //strTableP += "<th>方案室内审核</th>";
        //strTableP += "<th>方案上传</th>";
        //strTableP += "<th>综合室确认</th>";
        //strTableP += "<th>签订协议</th>";
        //strTableP += "<th>现场监测</th>";
        //strTableP += "<th>移交监测数据</th>";
        //strTableP += "<th>报告上传</th>";
        //strTableP += "<th>室内审核</th>";
        //strTableP += "<th>外业室修改</th>";
        //strTableP += "<th>综合室审核</th>";
        //strTableP += "<th>外业室修改</th>";
        //strTableP += "<th>装订</th>";
       

        //strTableP += "</tr>";
        #region 工作日志         

        string str = "";
        DateTime dt_s=DateTime.Parse(txt_StartTime.Text.Trim()+" 0:00:00");
        DateTime dt_e=DateTime.Parse(txt_EndTime.Text.Trim()+" 23:59:59");
        if (txt_StartTime.Text.Trim() != "") str += " and dtime >= '" + dt_s + "'";
        if (txt_EndTime.Text.Trim() != "") str += " and dtime <= '" + dt_e+"'";

        string strSql = "select t_R_OTUserLog.id,t_R_OTUserLog.name as 加班人员,ulog as 加班内容,dtime as 加班时间,DepartID,uid from t_R_OTUserLog inner join t_R_UserInfo on t_R_OTUserLog.uid=t_R_UserInfo.UserID where  1=1 " + str + " order by t_R_OTUserLog.id desc";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        string strsql2 = "select DepartID,DepartName from t_M_DepartInfo where DepartID!='0' "+str1;
        DataSet dsdepart=new MyDataOp (strsql2).CreateDataSet();
        int m = ds.Tables[0].Rows.Count;        
       
            for (DateTime dd = dt_s; dd <= dt_e; )
            {
                StringBuilder sb = new StringBuilder();
                
                //string showstr1 = "";
                //string showstr2 = "";
                int ndate = 0;
               
                foreach(DataRow dr in dsdepart.Tables[0].Rows)
                {
                    
                    DataRow[] drset = ds_User.Tables[0].Select("DepartID='" + dr[0].ToString() + "'");
                    int usern = drset.Length;
                    int usernum1 = usern / num + 1;
                    int usernums1 = usern % num;
                    ndate += usernum1;
                    string showstr1 = "";
                    string showstr2 = "";
                    StringBuilder sb1 = new StringBuilder();
                    for (int row = 0; row < usernum1; row++)
                    {
                       // sb1.Remove(0,sb1.Length);
                        showstr1 = "";
                        showstr2 = "";
                        for (int i = 0; i < num && row * num + i < num * (usernum1 - 1) + usernums1; i++)
                        {

                            DataRow[] drselect = ds.Tables[0].Select("uid='" + drset[num * row + i]["UserID"].ToString() + "' and  DepartID='" + dr[0].ToString() + "'  and 加班时间 >='" + dd + "' and 加班时间<='" + dd.AddDays(1).AddSeconds(-1) + "' ");
                            if (drselect.Length > 0)
                            {

                                showstr1 += "<th>" + drselect[0]["加班人员"].ToString() + "</th>";
                                showstr2 += "<td width='8%'>";
                                for (int p = 0; p < drselect.Length; p++)
                                {
                                    if (drselect.Length == 1)
                                        showstr2 += drselect[p]["加班内容"].ToString();
                                    else
                                        showstr2 += drselect[p]["加班内容"].ToString() + ";<br/>";

                                }
                                showstr2 += "</td>";


                            }
                            else
                            {
                                showstr1 += "<th>" + drset[num * row + i]["Name"].ToString() + "</th>";
                                showstr2 += "<td height='100px' width='8%'></td>";
                            }
                        }
                        if (row == usernum1 - 1)
                        {
                            for (int j = 0; j < num - usernums1; j++)
                            {
                                showstr1 += "<td></td>";
                                showstr2 += "<td></td>";
                            }
                        }
                        showstr1 += "</tr>";
                        showstr2 += "</tr>";
                    
                   
                    sb1.Append(showstr1);
                    sb1.Append(showstr2);
                    }
                    sb.Append("<th rowspan='" + usernum1 * 2 + "' width='8%'>" + dr[1].ToString() + "</th>");
                    sb.Append(sb1.ToString());
                    }
                strshow.Append("<tr><th rowspan='" + ndate*2 + "' width='8%'>" + dd.ToString("yyyy-MM-dd") + "</th>");
               
                strshow.Append(sb.ToString());
                //}
                dd = dd.AddDays(1);
            }
           
        #endregion

       strshow.Append("</tbody></table>");
       loglist.Text = strshow.ToString();
        //strTable = strTable + strTableC;
        //strTableP = strTableP + strTableC;
        //if (Export == 1)
        //{
        //    //导出报表
        //    try
        //    {
        //        RemoveFiles(Server.MapPath("."));
        //        string strFileName = "工作日志_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";
        //        xlSheet.Export(Server.MapPath(".") + "\\" + strFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
        //        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('ShowXls.aspx?file_name=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "');", true);
        //    }
        //    catch
        //    {
        //    }
        //}
        
    }
    //private string BgStyle(int s,int i)
    //{
    //    string bgStyleY = "style='background-color:#9AFF9A'";
    //    string bgStyleN = "style='background-color:#FF8C69'";

    //    if (s<=i)
    //    {
    //        return bgStyleN;
    //    }
    //    else
    //    {
    //        return bgStyleY;
    //    }
    //}

    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query(0);
    }

    //protected void btn_ExportR_Click(object sender, EventArgs e)
    //{         
    //    Query(1);
    //}   
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
            if (fi.Extension.ToString() == ".xls")
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
}
