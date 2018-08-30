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

public partial class Query_SampleListQuery1 : System.Web.UI.Page
{
    //public string outputStr;
    public string outputSum;
    public int kk = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面   
            txt_StartTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:calendar()");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-MM-01");
            txt_EndTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            Query();
          // SummaryQuery();
            #endregion 
        }       
    }
    public void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query();
    }
   
    private void Query()
    {
        DataSet ds;
        StringBuilder outputStr = new StringBuilder();

        outputStr.Append("<table>");
        DateTime start =DateTime.Parse( txt_StartTime.Text.Trim() + " 0:00:00");
        DateTime end = DateTime.Parse(txt_EndTime.Text.Trim() + " 23:59:59");

        string strSql = "SELECT t_M_ReporInfo.ReportName, t_M_ReporInfo.ReportRemark,t_M_ItemInfo.ItemName, t_M_SampleInfor.id, t_M_SampleInfor.AccessDate, t_M_SampleType.SampleType, t_M_SampleInfor.SampleID, t_M_SampleInfor.UserID,t_M_SampleInfor.CreateDate, t_M_ReporInfo.ReportWriteDate,t_M_ReporInfo.ReportWriteUserID,t_M_ReporInfo.ReportWriteRemark,t_M_ReporInfo.ReportWriteUserID, t_M_ReporInfo.ReportWriteRemark,t_M_ReporInfo.ReportDataDate, t_M_ReporInfo.ReportDataUser,t_M_ReporInfo.ReportDataRemark, t_M_ReporInfo.ReportCheckDate,t_M_ReporInfo.ReportSignUserID, t_M_ReporInfo.ReportProofRemark,t_M_ReporInfo.ReportSignDate,t_M_ReporInfo.ReportSignUserID,t_M_ReporInfo.ReportSignRemark, t_M_ReporInfo.ReportBindDate,t_M_ReporInfo.StatusID,t_M_ReporInfo.urgent  FROM t_M_ReporInfo INNER JOIN  t_M_SampleInfor ON t_M_ReporInfo.id = t_M_SampleInfor.ReportID INNER JOIN t_M_ItemInfo ON t_M_ReporInfo.ItemType = t_M_ItemInfo.ItemID INNER JOIN t_M_SampleType ON t_M_SampleInfor.TypeID = t_M_SampleType.TypeID WHERE (t_M_ReporInfo.StatusID >=6) and  t_M_SampleInfor.AccessDate between '" + start + "' and '" + end + "'  order by t_M_SampleInfor.AccessDate desc";
        string ItemSql = "";
        ds = new MyDataOp(strSql).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportWriteUserID"] = drr["Name"].ToString();
                if (dr["ReportDataUser"].ToString() == drr["UserID"].ToString())
                    dr["ReportDataUser"] = drr["Name"].ToString();
                if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportSignUserID"] = drr["Name"].ToString();
                if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportSignUserID"] = drr["Name"].ToString();
            }
        }
        kk = ds.Tables[0].Rows.Count;
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                outputStr.Append("<tr>");
                outputStr.Append("<td  rowspan='3' width='5%' class='AutoNewline'>" + (i + 1).ToString() + "</td>");
                if (ds.Tables[0].Rows[i]["AccessDate"].ToString() != "")
                {
                    string strurgent = "";
                    strurgent = ds.Tables[0].Rows[i]["urgent"].ToString();
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["ReportName"].ToString() + "</font></td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "</td>");
                    outputStr.Append("<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["AccessDate"].ToString() + "</td>");
                    outputStr.Append("<td  rowspan='3' width='5%' style='background-color:#9AFF9A' class='AutoNewline'>" + strurgent + "</td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["SampleType"].ToString() + "</td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["SampleID"].ToString() + "</font></td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportRemark"].ToString() + "</td>");
                }
                else
                {
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                    outputStr.Append("<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'></td>");
                    outputStr.Append("<td  rowspan='3' width='5%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'></td>");
                }

                //if (ds.Tables[0].Rows[i]["ReportRemark"] != null)
                //{


                //报告校核
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
                {
                    if (ds.Tables[0].Rows[i]["ReportdataDate"].ToString() != "")
                    {
                        outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportdataDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                    }
                    else
                    {

                        outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                    }

                }
                else
                {

                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                }
                //}
                //else
                //{
                //    if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
                //    {
                //       outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'></td>");

                //    }
                //    else
                //    {
                //        outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69'  class='AutoNewline'></td>");
                //    }

                //}、
                //报告编制
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3 && ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                {
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportWriteDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                }
                else
                {

                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                }

                //报告审核
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
                    // if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportCheckDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
                //报告签发
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                    //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportCheckDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");

                // if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 6)
                //报告装订
                if (ds.Tables[0].Rows[i]["ReportSignDate"].ToString() != "" && int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 6)
                    outputStr.Append("<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignDate"].ToString() + "</td>");
                else
                    outputStr.Append("<td rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>");
                //if (ds.Tables[0].Rows[i]["ReportBindDate"].ToString() != "" && int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >=6)
                //    outputStr.Append( "<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportBindDate"].ToString() + "</td>");
                //else
                //    outputStr.Append( "<td width='8%' rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>");
                outputStr.Append("</tr>");


                outputStr.Append("<tr>");
                //报告校核
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
                    //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportDataUser"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportDataUser"].ToString() + "</td>");
                //报告编制
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
                    //if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteUserID"].ToString() + "</td>");
                else
                    outputStr.Append("<td style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteUserID"].ToString() + "</td>");
                //报告审核
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
                    //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                //报告签发
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                    //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                outputStr.Append("</tr>");
                outputStr.Append("<tr>");
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
                    //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportdataRemark"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportdataRemark"].ToString() + "</td>");
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
                    //if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteRemark"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteRemark"].ToString() + "</td>");
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignRemark"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignRemark"].ToString() + "</td>");

                outputStr.Append("</tr>");
                //----分析项目----//
                ItemSql = "select t_MonitorItemDetail.id,t_M_AnalysisItemEx.AIName,t_MonitorItemDetail.FxDate,Remark,Name from t_MonitorItemDetail,t_M_AnalysisItemEx,t_R_UserInfo ";
                ItemSql += "where t_MonitorItemDetail.SampleID = '" + ds.Tables[0].Rows[i]["SampleID"].ToString() + "' and t_MonitorItemDetail.MonitorItem = t_M_AnalysisItemEx.id  and fxuser=t_R_UserInfo.UserID ";
                ItemSql += "order by t_MonitorItemDetail.id";
                DataSet Ids = new MyDataOp(ItemSql).CreateDataSet();
                outputStr.Append("<tr><td colspan='13'><table width='100%' class='listTable2'><tr><td colspan='10' align='center'>分析项目</td></tr>");

                // outputStr.Append( "<tr align='center'>";
                //outputStr.Append( "<td rowspan='4' width='8%'>分析项目</td>";


                if (Ids.Tables[0].Rows.Count > 0 && Ids.Tables[0].Rows.Count <= 10)
                {
                    outputStr.Append("<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                        {
                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                        }
                        else
                        {
                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append("<td width='7%' ></td>");
                    }
                    outputStr.Append("</tr>");
                    //数量
                    outputStr.Append("<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        //if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                        //{
                        outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Name"].ToString() + "</td>");
                        //}
                        //else
                        //{
                        //    outputStr.Append("<td width='7%' style='background-color:#FF8C69'>1</td>");
                        //}
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append("<td width='7%' ></td>");
                    }
                    outputStr.Append("</tr>");
                    //日期
                    outputStr.Append("<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                        {
                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["FxDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                        }
                        else
                        {
                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append("<td width='7%' ></td>");
                    }
                    outputStr.Append("</tr>");
                    outputStr.Append("<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                        {
                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                        }
                        else
                        {
                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append("<td width='7%'></td>");
                    }
                    outputStr.Append("</tr>");


                }
                else
                {
                    int num = Ids.Tables[0].Rows.Count / 10 + 1;
                    int ni = 0;
                    for (ni = 0; ni < num - 1; ni++)
                    {
                        // outputStr.Append( "</tr>";
                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }

                        }
                        outputStr.Append("</tr>");

                        //数量
                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Name"].ToString() + "</td>");
                       
                            //if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            //{
                            //    outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>1</td>");
                            //}
                            //else
                            //{
                            //    outputStr.Append("<td width='7%' style='background-color:#FF8C69'>1</td>");
                            //}

                        }
                        outputStr.Append("</tr>");

                        //日期
                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["FxDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                            }
                            else
                            {
                                outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
                            }

                        }
                        outputStr.Append("</tr>");

                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                            }

                        }
                        outputStr.Append("</tr>");

                    }

                    if (ni == num - 1)
                    {
                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append("<td width='7%' ></td>");
                        }
                        outputStr.Append("</tr>");
                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Name"].ToString() + "</td>");
                       
                            //if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            //{
                            //    outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>1</td>");
                            //}
                            //else
                            //{
                            //    outputStr.Append("<td width='7%' style='background-color:#FF8C69'>1</td>");
                            //}
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append("<td width='7%' ></td>");
                        }
                        outputStr.Append("</tr>");
                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["FxtDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                            }
                            else
                            {
                                outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append("<td width='7%' ></td>");
                        }
                        outputStr.Append("</tr>");
                        outputStr.Append("<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append("<td width='7%' ></td>");
                        }
                        outputStr.Append("</tr>");




                    }
                    Ids.Dispose();


                }

                //将分析项目分行显示
                outputStr.Append("</tr>");
                outputStr.Append("</table></td></tr>");
                //----分析项目----//
            }
        }
        
       ds.Dispose();
       outputStr.Append( "</table>");
       Samplelist.Text = outputStr.ToString();
    }

  
    
}
