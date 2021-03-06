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
using System.IO;
using System.Text;
using WebApp.Components;

public partial class Sample_GetNewData : System.Web.UI.Page
{
    //  public string outputStr;
    // public string outputSum;
    public int kk = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面
            if ( Request.QueryString["kw"] != null)
            {
                SummaryQuery();               
            }
            if (Request.QueryString["id"] != null)
            {               
                LoadLog();
            }
            
            //Query1();
            #endregion
        }
    }
    private void SummaryQuery()
    {
        StringBuilder outputSum = new StringBuilder();

        //string sql = "SELECT t_M_AnalysisItem.AIName,";
        //sql += "SUM(CASE WHEN t_M_MonitorItem.ReportDate IS NULL THEN t_M_MonitorItem.num ELSE 0 END) AS undone,";
        //sql += "SUM(CASE WHEN t_M_MonitorItem.ReportDate IS NOT NULL THEN t_M_MonitorItem.num ELSE 0 END) AS done ";
        //sql += "FROM t_M_MonitorItem INNER JOIN t_M_AnalysisItem ON t_M_MonitorItem.MonitorItem = t_M_AnalysisItem.id INNER JOIN t_M_SampleInfor ON t_M_MonitorItem.SampleID = t_M_SampleInfor.id inner join t_M_ReporInfo on t_M_ReporInfo.id=t_M_SampleInfor.ReportID ";
        //sql += "WHERE (t_M_ReporInfo.StatusID < 6) and SUM(CASE WHEN t_M_MonitorItem.ReportDate IS NULL THEN t_M_MonitorItem.num ELSE 0 END)>0 GROUP BY t_M_AnalysisItem.AIName";
        string sql = "select * from view_ywc";
        //DataSet dss = new MyDataOp(sql).CreateDataSetP("tj");

        DataSet dss = new MyDataOp(sql).CreateDataSet();

        outputSum.Append("<table><tr><td>分析项目</td>");
        if (dss.Tables[0].Rows.Count > 0)
        {
            string AIName = "";
            string UnDone = "";
            string Done = "";
            for (int m = 0; m < dss.Tables[0].Rows.Count; m++)
            {
                AIName = dss.Tables[0].Rows[m][0].ToString();
                outputSum.Append("<td>" + AIName + "</td>");
            }
            outputSum.Append("</tr><tr><td>已完成</td>");
            for (int n = 0; n < dss.Tables[0].Rows.Count; n++)
            {
                Done = dss.Tables[0].Rows[n][2].ToString();
                outputSum.Append("<td>" + Done + "</td>");
            }
            outputSum.Append("</tr><tr><td>未完成</td>");
            for (int l = 0; l < dss.Tables[0].Rows.Count; l++)
            {
                UnDone = dss.Tables[0].Rows[l][1].ToString();
                outputSum.Append("<td>" + UnDone + "</td>");
            }
        }
        outputSum.Append("</tr></table>");

        dss.Dispose();
       

            Response.ContentType = "text/xml";
            Response.Write(outputSum.ToString());
            Response.End();
           
       
    }
    private void LoadLog()
    {
        string outputlog = "";
        string date1 = DateTime.Now.ToString("yyyy-MM-dd");
        string date2 = date1 + " 23:59:59";
        string strSql = "select id,name as 工作人员,ulog as 工作内容,utime as 登记时间 from t_R_UserLog where utime > '" + date1 + "' and utime < '" + date2 + "' order by id desc";

        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                outputlog += "[<font color='green'>" + ds.Tables[0].Rows[i][1].ToString() + "</font>]  " + ds.Tables[0].Rows[i][2].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color='#6c6f6f'>" + ds.Tables[0].Rows[i][3].ToString() + "</font> <br>";
            }
        }
        else
        {
            outputlog += "<font color='red'>今日无工作日志！</font>";
        }

        ds.Dispose();

        Response.ContentType = "text/xml";
        Response.Write(outputlog);
        Response.End();
    }
    //private void Query1()
    //{
    //    StringBuilder outputStr = new StringBuilder();

    //    outputStr.Append("<table>");
    //    string strSql = "";
    //    string ItemSql = "";
    //    string sql = "select * from view_sstj";
    //    DataSet ds = new MyDataOp(strSql).CreateDataSetP("ssxs");


    //    string strtemp = "select Name,UserID from t_R_UserInfo";
    //    DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();

    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        DataRow[] dataUser = ds_User.Tables[0].Select("UserID='" + dr["ReportWriteUserID"].ToString() + "'");
    //        foreach (DataRow drr in dataUser)
    //        {
    //            dr["ReportWriteUserID"] = drr["Name"].ToString();
    //        }

    //        DataRow[] dataUser2 = ds_User.Tables[0].Select("UserID='" + dr["ReportProofUserID"].ToString() + "'");
    //        foreach (DataRow drr in dataUser2)
    //        {
    //            dr["ReportProofUserID"] = drr["Name"].ToString();
    //        }
    //        DataRow[] dataUser3 = ds_User.Tables[0].Select("UserID='" + dr["ReportSignUserID"].ToString() + "'");
    //        foreach (DataRow drr in dataUser3)
    //        {
    //            dr["ReportSignUserID"] = drr["Name"].ToString();
    //        }
    //        //foreach (DataRow drr in ds_User.Tables[0].Rows)
    //        //{
    //        //if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
    //        //    dr["ReportWriteUserID"] = drr["Name"].ToString();
    //        //if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
    //        //    dr["ReportProofUserID"] = drr["Name"].ToString();
    //        //if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
    //        //    dr["ReportSignUserID"] = drr["Name"].ToString();
    //        //}

    //    }

    //    ds_User.Dispose();
    //    kk = ds.Tables[0].Rows.Count;
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            outputStr.Append("<tr>");
    //            outputStr.Append("<td  rowspan='3' width='5%' class='AutoNewline'>" + (i + 1).ToString() + "</td>");
    //            if (ds.Tables[0].Rows[i]["AccessDate"].ToString() != "")
    //            {
    //                string strurgent = "";
    //                strurgent = ds.Tables[0].Rows[i]["urgent"].ToString();
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["ReportName"].ToString() + "</font></td>");
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "</td>");
    //                outputStr.Append("<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["AccessDate"].ToString() + "</td>");
    //                outputStr.Append("<td  rowspan='3' width='5%' style='background-color:#9AFF9A' class='AutoNewline'>" + strurgent + "</td>");
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["SampleType"].ToString() + "</td>");
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["SampleID"].ToString() + "</font></td>");

    //            }
    //            else
    //            {
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //                outputStr.Append("<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'></td>");
    //                outputStr.Append("<td  rowspan='3' width='5%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //                outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");

    //            }

    //            if (ds.Tables[0].Rows[i]["ReportRemark"] != null)
    //            {



    //                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
    //                {
    //                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportRemark"].ToString() + "</td>");

    //                }
    //                else
    //                {
    //                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69'  class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportRemark"].ToString() + "</td>");
    //                }
    //            }
    //            else
    //            {
    //                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
    //                {
    //                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'></td>");

    //                }
    //                else
    //                {
    //                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#FF8C69'  class='AutoNewline'></td>");
    //                }

    //            }
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
    //            //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
    //            {
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportWriteDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
    //            }
    //            else
    //            {

    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //            }


    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
    //                // if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportProofDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
    //                //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportCheckDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");

    //            // if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 6)
    //            if (ds.Tables[0].Rows[i]["ReportSignDate"].ToString() != "" && int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
    //                outputStr.Append("<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignDate"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //            if (ds.Tables[0].Rows[i]["ReportBindDate"].ToString() != "" && int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
    //                outputStr.Append("<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportBindDate"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //            outputStr.Append("</tr>");


    //            outputStr.Append("<tr>");
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
    //                //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteUserID"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
    //                //if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofUserID"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td style='background-color:#FF8C69'></td>");
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
    //                //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
    //            outputStr.Append("</tr>");
    //            outputStr.Append("<tr>");
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
    //                //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteRemark"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteRemark"].ToString() + "</td>");
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
    //                //if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
    //            if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
    //                //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
    //                outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
    //            else
    //                outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
    //            outputStr.Append("</tr>");
    //            //----分析项目----//
    //            ItemSql = "select t_M_MonitorItem.id,t_M_AnalysisItem.AIName,t_M_MonitorItem.Num,t_M_MonitorItem.ReportDate,Remark from t_M_MonitorItem,t_M_AnalysisItem ";
    //            ItemSql += "where t_M_MonitorItem.SampleID = '" + ds.Tables[0].Rows[i]["id"].ToString() + "' and t_M_MonitorItem.MonitorItem = t_M_AnalysisItem.id ";
    //            ItemSql += "order by t_M_MonitorItem.id";
    //            DataSet Ids = new MyDataOp(ItemSql).CreateDataSet();
    //            outputStr.Append("<tr><td colspan='13'><table width='100%' class='listTable2'><tr><td colspan='10' align='center'>分析项目</td></tr>");

    //            // outputStr.Append( "<tr align='center'>";
    //            //outputStr.Append( "<td rowspan='4' width='8%'>分析项目</td>";


    //            if (Ids.Tables[0].Rows.Count > 0 && Ids.Tables[0].Rows.Count <= 10)
    //            {
    //                outputStr.Append("<tr>");
    //                for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //                {
    //                    if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
    //                    }
    //                    else
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
    //                    }
    //                }

    //                for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
    //                {
    //                    outputStr.Append("<td width='7%' ></td>");
    //                }
    //                outputStr.Append("</tr>");
    //                //数量
    //                outputStr.Append("<tr>");
    //                for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //                {
    //                    if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>");
    //                    }
    //                    else
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>");
    //                    }
    //                }

    //                for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
    //                {
    //                    outputStr.Append("<td width='7%' ></td>");
    //                }
    //                outputStr.Append("</tr>");
    //                //日期
    //                outputStr.Append("<tr>");
    //                for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //                {
    //                    if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
    //                    }
    //                    else
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
    //                    }
    //                }

    //                for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
    //                {
    //                    outputStr.Append("<td width='7%'></td>");
    //                }
    //                outputStr.Append("</tr>");
    //                outputStr.Append("<tr>");
    //                for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //                {
    //                    if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
    //                    }
    //                    else
    //                    {
    //                        outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
    //                    }
    //                }

    //                for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
    //                {
    //                    outputStr.Append("<td width='7%'></td>");
    //                }
    //                outputStr.Append("</tr>");


    //            }
    //            else
    //            {
    //                int num = Ids.Tables[0].Rows.Count / 10 + 1;
    //                int ni = 0;
    //                for (ni = 0; ni < num - 1; ni++)
    //                {
    //                    // outputStr.Append( "</tr>";
    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < (ni + 1) * 10; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
    //                        }

    //                    }
    //                    outputStr.Append("</tr>");

    //                    //数量
    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < (ni + 1) * 10; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>");
    //                        }

    //                    }
    //                    outputStr.Append("</tr>");

    //                    //日期
    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < (ni + 1) * 10; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
    //                        }

    //                    }
    //                    outputStr.Append("</tr>");

    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < (ni + 1) * 10; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
    //                        }

    //                    }
    //                    outputStr.Append("</tr>");

    //                }

    //                if (ni == num - 1)
    //                {
    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
    //                        }
    //                    }
    //                    for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
    //                    {
    //                        outputStr.Append("<td width='7%' ></td>");
    //                    }
    //                    outputStr.Append("</tr>");
    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>");
    //                        }
    //                    }
    //                    for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
    //                    {
    //                        outputStr.Append("<td width='7%' ></td>");
    //                    }
    //                    outputStr.Append("</tr>");
    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
    //                        }
    //                    }
    //                    for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
    //                    {
    //                        outputStr.Append("<td width='7%' ></td>");
    //                    }
    //                    outputStr.Append("</tr>");
    //                    outputStr.Append("<tr>");
    //                    for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
    //                    {
    //                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            outputStr.Append("<td width='7%' style='background-color:#FF8C69'></td>");
    //                        }
    //                    }
    //                    for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
    //                    {
    //                        outputStr.Append("<td width='7%' ></td>");
    //                    }
    //                    outputStr.Append("</tr>");




    //                }
    //                Ids.Dispose();


    //            }

    //            //将分析项目分行显示
    //            outputStr.Append("</tr>");
    //            outputStr.Append("</table></td></tr>");
    //            //----分析项目----//
    //        }

    //    }
    //    outputStr.Append("</table>");

    //    //Samplelist.Text = DateTime.Now.ToString();

    //    ds.Dispose();
    //    if (flagRelash == "2")
    //    {

    //        Response.ContentType = "text/xml";
    //        Response.Write(outputStr.ToString());
    //        Response.End();
    //        flagRelash = "";
    //    }

    //}

   
}
