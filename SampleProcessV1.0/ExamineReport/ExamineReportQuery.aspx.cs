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
using Microsoft.Office.Interop.Owc11;

public partial class ExamineReportQuery : System.Web.UI.Page
{
    public string strTable = "";
    public string strTableC = "";
    public string strTableP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "委托监测列表";
        if (!IsPostBack)
        {
            #region 初始化页面   
            txt_StartTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            //txt_StartTime.Attributes.Add("OnFocus", "javascript:calendar()");
            //txt_EndTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");            
            txt_EndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");  
            MyStaVoid.BindList("name","code"," select name,code from t_dictinf where type='status' order by code",drop_statusQuery);
            ListItem li = new ListItem("所有", "");
            drop_statusQuery.Items.Add(li);
            drop_statusQuery.SelectedIndex = drop_statusQuery.Items.Count - 1;

            Query(0);
            #endregion
        }
    }
    private void Query(int Export)
    {


       string strtitle = "select * from t_R_Menu inner join t_chart_main on uid=t_R_Menu.id where  type=3 order by orderid";
       // string strtitle = "select * from t_R_Menu  where  fatherID=30 and id!=49 order by OrderID";
        DataSet dstitle = new MyDataOp(strtitle).CreateDataSet();
        Microsoft.Office.Interop.Owc11.SpreadsheetClass xlSheet = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
        if (Export == 1)
        {
            xlSheet.ActiveSheet.Cells[1, 1] = "序号";
            xlSheet.ActiveSheet.Cells[1, 2] = "报告标识";
            xlSheet.ActiveSheet.Cells[1, 3] = "项目名称";
            xlSheet.ActiveSheet.Cells[1, 4] = "委托单位";
        }
        int i = 5;
        if (txt_StartTime.Text.Trim() != "" && txt_EndTime.Text.Trim() != "")
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='98%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月dd日") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月dd日") + " 受理委托监测列表</b></font></caption><tbody><tr align='center'>";
            strTableP = strTable;
        }
        else if (txt_StartTime.Text.Trim() != "")
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='98%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月dd日") + "之后受理委托监测列表</b></font></caption><tbody><tr align='center'>";
            strTableP = strTable;
        }
        else if (txt_EndTime.Text.Trim() != "")
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='98%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月dd日") + "之前受理委托监测列表</b></font></caption><tbody><tr align='center'>";
            strTableP = strTable;
        }
        else
        {
            strTable = "<table id='tableid' class='listTable' boder='0' cellspacing='1' width='98%'><caption><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:12pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 30px'><b>受理委托监测列表</b></font></caption><tbody><tr align='center'>";
            strTableP = strTable;
        }
        strTable += "<th>序号</th>";
        strTable += "<th>报告标识</th>";
        strTable += "<th>项目名称</th>";
        strTable += "<th>委托单位</th>";
       // strTableP = "<table id='tableid' width='98%' style= 'BORDER-COLLAPSE:collapse' borderColor= '#00000' border= '1'> <caption><FONT style='size=3 HEIGHT: 50px'><b>" + DateTime.Parse(txt_StartTime.Text.Trim()).ToString("yyyy年MM月dd日") + "至" + DateTime.Parse(txt_EndTime.Text.Trim()).ToString("yyyy年MM月dd日") + " 受理委托监测列表</b></font></caption><tbody><tr align='center'>";
        strTableP += "<th>序号</th>";
        strTableP += "<th>报告标识</th>";
        strTableP += "<th>项目名称</th>";
        strTableP += "<th>委托单位</th>";
        foreach (DataRow dr in dstitle.Tables[0].Rows)
        {
            if (Export == 1)
            {
                xlSheet.ActiveSheet.Cells[1, i++] = dr["Name"].ToString();
            }
            strTable += "<th>" + dr["Name"].ToString() + "</th>";
            strTableP += "<th>" + dr["Name"].ToString() + "</th>";
        }

        if (Export == 1)
        {
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, i]).Font.set_Bold(true);
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, i]).Font.set_Size(10);
        }
        strTable += "</tr>";

        strTableP += "</tr>";
        #region 受理流程         
        string condstr="";
        if(txt_StartTime.Text.Trim()!="")
            condstr += " and t.ReportAccessDate >= '" + txt_StartTime.Text.Trim() + " 0:00:00" + "'";
        if (txt_EndTime.Text.Trim() != "")
            condstr += " and t.ReportAccessDate <= '" + txt_EndTime.Text.Trim() + " 23:59:59" + "'";
        if (txt_wtQuery.Text.Trim() != "")
            condstr += " and wtdepart like '%" + txt_wtQuery.Text.Trim() + "%'";
        if (txt_bsQuery.Text.Trim() != "")
            condstr += " and ReportName like '%" + txt_bsQuery.Text.Trim() + "%'";
        if (drop_fanganQuery.SelectedValue.ToString().Trim() != "")
        {
            condstr += " and hanwether='" + drop_fanganQuery.SelectedValue.ToString().Trim() + "' and statusID>'0'";
        }
       
        
        if (drop_statusQuery.SelectedIndex != drop_statusQuery.Items.Count - 1)
        {
            switch (drop_statusQuery.SelectedValue.ToString())
            {
                case "0":
                    condstr += " and statusID='0' and  wether=0";//受理中
                    break;
                case "1":
                    condstr += " and statusID='1'";//初审中
                    break;
                case "1.5":
                    condstr += " and statusID='1.5' "; break;//指派中（不出方案）
               
                  
                case "1.7":
                    condstr +=  " and statusID='0' and wether=1";//初审不通过
                    break;
                case "2":
                    condstr +=  " and statusID='2' and wether=0 and hanwether=1";//踏勘中
                    break;
                case "2.5":
                    condstr +=  " and statusID='3' and tkwether=1 and hanwether=1";//函编写
                    break;
                case "3":
                    condstr +=  " and statusID='3' and tkwether=0 and hanwether=1";//方案编写中
                    break;
                case "3.5":
                    condstr += " and statusID='3.5'";//报告编制中
                    break;
                case "4":
                    condstr += " and statusID='4'";//报告编制中
                    break;
                case "5":
                    condstr +=" and statusID='5'  ";//报告编制完成
                    break;
                case "6":
                    condstr +=" and statusID='5' and tkwether=1";//函编写完成
                    break;
            }
        }
          string strSql = "select * from t_Y_FlowInfo t  where 1=1 " + condstr + " order by t.ReportAccessDate desc";

        if (txt_chargeman.Text.Trim() != "")
        {
            condstr += " and Name like '%" + txt_chargeman.Text.Trim() + "%'";
            strSql = "select * from t_Y_FlowInfo t inner join t_R_UserInfo on chargeman=t_R_UserInfo.UserID where 1=1 " + condstr + " order by t.ReportAccessDate desc";

        }

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        int m = ds.Tables[0].Rows.Count;
        if (m != 0)
        {
            string wtdw = "";
                string itemname = "";
                string accessman = "";
                string accessdate = "";
                string accessremark = "";
                string varman1 = "";
                string vardate1 = "";
                string varremark1 = "";
                string varman2 = "";
                string vardate2 = "";
                string varremark2 = "";
                string varman3 = "";
                string vardate3 = "";
                string varremark3 = "";
                string varman4 = "";
                string vardate4 = "";
                string varremark4 = "";
               string reportbs= "";
               string varman5 = "";
               string vardate5 = "";
               string varremark5 = "";
               string varman0 = "";
                string vardate0 = "";
                string varremark0 = "";


               
           
               double status = 0;
                string strtemp = "select Name,UserID from t_R_UserInfo";
                DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
                for (int j = 0; j < m; j++)
             {
                 wtdw = "";
                 reportbs = "";
                    itemname = "";
                     accessman = "";
                    accessdate = "";
                    accessremark = "";
                    varman0 = "";
                     varman1 = "";
                     vardate1 = "";
                    varremark1 = "";
                     varman2 = "";
                    vardate2 = "";
                     varremark2 = "";
                    varman3 = "";
                    vardate3 = "";
                     varremark3 = "";
                    varman4 = "";
                     vardate4 = "";
                     varremark4 = "";

                     varman5 = "";
                     vardate5 = "";
                     varremark5 = "";
                    
                    vardate0 = "";
                    varremark0 = "";
                     int flag = 1;//是否出方案
                     int wether = 1;//初审是否通过
                     int tkwether = 1;//踏勘是否通过
                     try
                     {
                         flag = int.Parse(ds.Tables[0].Rows[j]["hanwether"].ToString());
                         wether = int.Parse(ds.Tables[0].Rows[j]["wether"].ToString());
                         tkwether = int.Parse(ds.Tables[0].Rows[j]["tkwether"].ToString());
                     }
                     catch
                     { }
                    
           
                     DataRow[] drs = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["UserID"].ToString() + "'");
                   
                    if(drs.Length==1)
                        accessman = drs[0]["Name"].ToString();
                    DataRow[] drvarman0 = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["kschargeman"].ToString() + "'");
                    if (drvarman0.Length == 1)
                        varman0 = drvarman0[0]["Name"].ToString();
                    DataRow[] drvarman1 = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["chargeman"].ToString() + "'");
                    if (drvarman1.Length == 1)
                        varman1 = drvarman1[0]["Name"].ToString();
                    DataRow[] drvarman2 = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["varman2"].ToString() + "'");
                    if (drvarman2.Length == 1)
                        varman2 = drvarman2[0]["Name"].ToString();
                    DataRow[] drvarman3 = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["varman3"].ToString() + "'");
                    if (drvarman3.Length == 1)
                        varman3 = drvarman3[0]["Name"].ToString();
                        if (ds.Tables[0].Rows[j]["varman4"].ToString() != "")
                        {
                           
                            DataRow[] drvarman4 = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["varman4"].ToString() + "'");

                            if (drvarman4.Length > 0)
                            {
                                varman4 += " 报告编制：" + drvarman4[0]["Name"].ToString(); ;
                            }
                        }
                        DataRow[] drvarman5 = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["varman5"].ToString() + "'");
                        if (drvarman5.Length == 1)
                            varman5 = drvarman5[0]["Name"].ToString();
                        if (ds.Tables[0].Rows[j]["ReportdataUser"].ToString() != "")
                        {
                            DataRow[] dryj4 = ds_User.Tables[0].Select("UserID='" + ds.Tables[0].Rows[j]["ReportdataUser"].ToString() + "'");

                            if (dryj4.Length > 0)
                            {
                                varman4 += "数据移交：" + dryj4[0]["Name"].ToString(); ; ;
                            }
                        }

                        wtdw = ds.Tables[0].Rows[j]["wtdepart"].ToString();
                    itemname = ds.Tables[0].Rows[j]["Projectname"].ToString();
                    reportbs = ds.Tables[0].Rows[j]["ReportName"].ToString();
                    accessdate = ds.Tables[0].Rows[j]["ReportAccessDate"].ToString();
                    accessremark = ds.Tables[0].Rows[j]["urgent"].ToString();
                    vardate1 = ds.Tables[0].Rows[j]["vardate1"].ToString();
                    vardate1 = ds.Tables[0].Rows[j]["vardate1"].ToString();
                    varremark1 = ds.Tables[0].Rows[j]["varremark1"].ToString();
                   // varman2 = ds.Tables[0].Rows[j]["varman2"].ToString();
                    vardate2 = ds.Tables[0].Rows[j]["vardate2"].ToString();
                    varremark2 = ds.Tables[0].Rows[j]["varremark2"].ToString();
                   // varman3 = ds.Tables[0].Rows[j]["varman3"].ToString();
                    vardate3 = ds.Tables[0].Rows[j]["vardate3"].ToString();
                    varremark3 = ds.Tables[0].Rows[j]["varremark3"].ToString();
                    vardate5 = ds.Tables[0].Rows[j]["vardate5"].ToString();
                    varremark5 = ds.Tables[0].Rows[j]["varremark5"].ToString();
                    if (ds.Tables[0].Rows[j]["ReportdataDate"].ToString()!="")
                    vardate4 += "数据移交：" + ds.Tables[0].Rows[j]["ReportdataDate"].ToString();
                    if (ds.Tables[0].Rows[j]["vardate4"].ToString()!="")
                        vardate4 += " 报告编制：" + ds.Tables[0].Rows[j]["vardate4"].ToString();
                    varremark4 += ds.Tables[0].Rows[j]["varremark4"].ToString();
                
                  status =double.Parse(ds.Tables[0].Rows[j]["StatusID"].ToString());

                  strTableC += "<tr><td rowspan = '3'>" + (j + 1).ToString() + "</td><td rowspan = '3' " + BgStyle(status, 0, flag, wether, tkwether) + ">"
                      + reportbs + "</td><td rowspan = '3' " + BgStyle(status, 0, flag, wether, tkwether) + ">"
                      + itemname + "</td><td rowspan = '3' " + BgStyle(status, 0, flag, wether, tkwether) + ">"
                      + wtdw + "</td><td " + BgStyle(status, 0, flag, wether, tkwether) + ">"
                  + accessman + "</td><td " + BgStyle(status, 1, flag, wether, tkwether) + ">"
                   + varman0 + "</td><td " + BgStyle(status, 1.5, flag, wether, tkwether) + ">"
                  + varman1 + "</td><td " + BgStyle(status, 2, flag, wether, tkwether) + ">"
                  + varman2 + "</td><td " + BgStyle(status, 3, flag, wether, tkwether) + ">"
                  + varman3 + "</td><td " + BgStyle(status, 3.5, flag, wether, tkwether) + ">"
                  + varman5 + "</td>";
                  strTableC += "<td " + BgStyle(status, 4, flag, wether, tkwether) + ">"
                  + varman4 + "</td>";

                  strTableC += "</td></tr><tr><td " + BgStyle(status, 0, flag, wether, tkwether) + ">"
                    + accessdate + "</td><td " + BgStyle(status,1, flag, wether, tkwether) + ">"
                      + vardate0 + "</td><td " + BgStyle(status, 1.5, flag, wether, tkwether) + ">"
                    + vardate1 + "</td><td " + BgStyle(status, 2, flag, wether, tkwether) + ">"
                    + vardate2 + "</td><td " + BgStyle(status, 3, flag, wether, tkwether) + ">"
                    + vardate3 + "</td><td " + BgStyle(status, 3.5, flag, wether, tkwether) + ">"
                    + vardate5 + "</td><td " + BgStyle(status, 4, flag, wether, tkwether) + ">"
                       + vardate4 + "</td>";

                  strTableC += "</td></tr><tr><td " + BgStyle(status, 0, flag, wether, tkwether) + ">"
                    + accessremark + "</td><td " + BgStyle(status, 1, flag, wether, tkwether) + ">"
                     + varremark0 + "</td><td " + BgStyle(status, 1.5, flag, wether, tkwether) + ">"
                    + varremark1 + "</td><td " + BgStyle(status, 2, flag, wether, tkwether) + ">"
                    + varremark2 + "</td><td " + BgStyle(status,3, flag, wether, tkwether) + ">"

                    + varremark3 + "</td><td " + BgStyle(status, 3.5, flag, wether, tkwether) + ">"

                    + varremark5 + "</td>";
                      strTableC +=
                   "<td " + BgStyle(status, 4, flag, wether, tkwether) + ">"
                    + varremark4 + "</td>";
                   
                    strTableC +="</tr>";

                    if (Export == 1)
                    {

                        xlSheet.get_Range(xlSheet.Cells[j * 3 + 2, 1], xlSheet.Cells[j * 3 + 4, 1]).set_MergeCells(true);
                        xlSheet.get_Range(xlSheet.Cells[j * 3 + 2, 2], xlSheet.Cells[j * 3 + 4, 2]).set_MergeCells(true);
                        xlSheet.get_Range(xlSheet.Cells[j * 3 + 2, 3], xlSheet.Cells[j * 3 + 4, 3]).set_MergeCells(true);
                        xlSheet.get_Range(xlSheet.Cells[j * 3 + 2, 4], xlSheet.Cells[j * 3 + 4, 4]).set_MergeCells(true);
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 1] = (j + 1).ToString();
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 2] = reportbs;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 3] = itemname;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 4] =wtdw;
                        xlSheet.ActiveSheet.Cells[j* 3 + 2, 5] = accessman;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 6] = varman0;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 7] = varman1;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 8] = varman2;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 9] = varman3;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 10] = varman5;
                        xlSheet.ActiveSheet.Cells[j * 3 + 2, 11] = varman4;
                      

                        xlSheet.ActiveSheet.Cells[j*3 + 3, 5] = accessdate;
                        xlSheet.ActiveSheet.Cells[j * 3 + 3, 6] = vardate0;
                        xlSheet.ActiveSheet.Cells[j*3 + 3, 7] = vardate1;
                        xlSheet.ActiveSheet.Cells[j*3 + 3, 8] = vardate2;
                        xlSheet.ActiveSheet.Cells[j*3 + 3, 9] = vardate3;
                        xlSheet.ActiveSheet.Cells[j*3 + 3, 10] = vardate5;
                        xlSheet.ActiveSheet.Cells[j * 3 + 3, 11] = vardate4;
                       

                        xlSheet.ActiveSheet.Cells[j * 3 + 4, 5] = accessremark;
                        xlSheet.ActiveSheet.Cells[j * 3 + 4, 6] = varremark0;
                        xlSheet.ActiveSheet.Cells[j * 3 + 4, 7] = varremark1;
                        xlSheet.ActiveSheet.Cells[j * 3 + 4,8] = varremark2;
                        xlSheet.ActiveSheet.Cells[j * 3 + 4, 9] = varremark3;
                        xlSheet.ActiveSheet.Cells[j * 3 + 4,10] = varremark5;
                       xlSheet.ActiveSheet.Cells[j * 3 + 4, 11] = varremark4;
                       
                }   
        }
        if (Export == 1)
        {
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[m * 3 + 1, 11]).Borders.set_LineStyle(Microsoft.Office.Interop.Owc11.XlLineStyle.xlContinuous);
            xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[m * 3 + 1, 11]).set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignCenter);
        }
        }
        else
        {
            strTableC += "<tr>";
                for(int p=0;p<=10;p++)
                    strTableC+="<td>-</td>";
                strTableC +="</tr>";
            if (Export == 1)
            {
                xlSheet.ActiveSheet.Cells[2, 1] = "-";
                xlSheet.ActiveSheet.Cells[2, 2] = "-";
                xlSheet.ActiveSheet.Cells[2, 3] = "-";
                xlSheet.ActiveSheet.Cells[2, 4] = "-";
                xlSheet.ActiveSheet.Cells[2, 5] = "-";
                xlSheet.ActiveSheet.Cells[2, 6] = "-";
                xlSheet.ActiveSheet.Cells[2, 7] = "-";
                xlSheet.ActiveSheet.Cells[2, 8] = "-";
                xlSheet.ActiveSheet.Cells[2, 9] = "-";
                xlSheet.ActiveSheet.Cells[2, 10] = "-";
                xlSheet.ActiveSheet.Cells[2, 11] = "-";
                
       
        }            
       }        
       #endregion

        strTableC += "</tbody></table>";
        strTable = strTable + strTableC;
        strTableP = strTableP + strTableC;
        if (Export == 1)
        {
            //导出报表
            try
            {
                RemoveFiles(Server.MapPath("."));
                string strFileName = "委托监测列表_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";
                xlSheet.Export(Server.MapPath(".") + "\\" + strFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('ShowXls.aspx?file_name=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + "');", true);
            }
            catch
            {
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s">状态</param>
    /// <param name="i"></param>
    /// <param name="f">是否出方案</param>
    /// <param name="wether">初审是否通过</param>
    /// <param name="tkwether">踏勘是否通过</param>
    /// <returns></returns>
    private string BgStyle(double s,double i,int f,int wether,int tkwether)
    {
        string bgStyleY = "style='background-color:#9AFF9A'";
        string bgStyleN = "style='background-color:#FF8C69'";
        string bgStyleEnd = "style='background-color:#FF8C69'";//background-color:#66c2ca

                    if (s <= i)
                    {
                        if (wether== 1&&i<=1)//1-初审不通过
                            return bgStyleY;
                        else if (wether == 1 && s >= 1)//1-初审不通过
                            return bgStyleN;
                        else if (wether != 1)//0-初审通过
                        {
                           
                            if (f==0&&i <2)//0-不出方案
                            {
                                return bgStyleN;
                            }
                            else if (f == 0 && i >=2)
                            {
                                return bgStyleEnd;
                            }
                            else//出方案
                            {
                                if(tkwether==1&&s==3&&i>3)//踏勘不通过
                                    return bgStyleEnd;
                                else 
                                   return bgStyleN;
                            }
                            
                        }
                        else
                            return bgStyleEnd;
                        

                    }
                    else
                    {
                        if (f == 0)
                        {
                            if (s == 5)
                                return bgStyleY;
                            else if(s<=i)
                                return bgStyleN;
                            else 
                                return
                                     bgStyleY;

                        }
                        else
                        {
                            if (tkwether == 1 && s == 5 && i > 3)//踏勘不通过
                                return bgStyleEnd;
                            else
                                return bgStyleY;
                        }
                       
                    }
               
           
    }
    private string BgStyle1(double s, double i, int f, int wether, int tkwether)
    {
        string bgStyleY = "style='background-color:#9AFF9A'";
        string bgStyleN = "style='background-color:#FF8C69'";
        string bgStyleEnd = "style='background-color:#66c2ca'";
        if (f == 0)
        {
            if (i < 2)
            {
                if (s <= i)
                {
                    return bgStyleN;
                }
                else
                {
                    return bgStyleY;
                }
            }
            else
                return bgStyleEnd;
        }
        else
        {
            if (s <= i)
            {
                return bgStyleN;
            }
            else
            {
                return bgStyleY;
            }
        }
    }
    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query(0);
    }

    protected void btn_ExportR_Click(object sender, EventArgs e)
    {         
        Query(1);
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
