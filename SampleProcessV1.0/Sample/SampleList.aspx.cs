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

public partial class Query_SampleList : System.Web.UI.Page
{
  //  public string outputStr;
   // public string outputSum;
    public int kk = 0;
    private string flagRelash
    {
        get { return (string)ViewState["flagRelash"]; }
        set { ViewState["flagRelash"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面          
            //if (Request.QueryString["vvkw"]!=null&& Request.QueryString["kw"] != null)
            //{

            //    flagRelash = Request.QueryString["vvkw"].ToString();
            //}
           SummaryQuery();
           Query1();
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
        lbl_sum.Text = outputSum.ToString();
        dss.Dispose();
       // if (flagRelash == "1")
       //{

       //    Response.ContentType = "text/xml";
       //    Response.Write(outputSum.ToString());
       //    Response.End();
       //    flagRelash = "";
       //}
    }
    private void Query1()
    {
        StringBuilder outputStr = new StringBuilder();
       
        outputStr.Append("<table>");
        string strSql = "";
        string ItemSql = "";
        string sql = "select * from view_sstj";
        DataSet ds = new MyDataOp(strSql).CreateDataSetP("ssxs");


        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DataRow[] dataUser = ds_User.Tables[0].Select("UserID='" + dr["ReportWriteUserID"].ToString() + "'");
            foreach (DataRow drr in dataUser)
            {
                dr["ReportWriteUserID"] = drr["Name"].ToString();
            }

            DataRow[] dataUser2 = ds_User.Tables[0].Select("UserID='" + dr["ReportdataUser"].ToString() + "'");
            foreach (DataRow drr in dataUser2)
            {
                dr["ReportdataUser"] = drr["Name"].ToString();
            }
            DataRow[] dataUser3 = ds_User.Tables[0].Select("UserID='" + dr["ReportSignUserID"].ToString() + "'");
            foreach (DataRow drr in dataUser3)
            {
                dr["ReportSignUserID"] = drr["Name"].ToString();
            }
            DataRow[] dataUser4 = ds_User.Tables[0].Select("UserID='" + dr["ReportSignUserID"].ToString() + "'");
            foreach (DataRow drr in dataUser4)
            {
                dr["ReportSignUserID"] = drr["Name"].ToString();
            }
            //foreach (DataRow drr in ds_User.Tables[0].Rows)
            //{
            //if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
            //    dr["ReportWriteUserID"] = drr["Name"].ToString();
            //if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
            //    dr["ReportProofUserID"] = drr["Name"].ToString();
            //if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
            //    dr["ReportSignUserID"] = drr["Name"].ToString();
            //}

        }
        
        ds_User.Dispose();
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
                    outputStr.Append( "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["SampleType"].ToString() + "</td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["SampleID"].ToString() + "</font></td>");
                    outputStr.Append("<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportRemark"].ToString() + "</td>");
                }
                else
                {
                   outputStr.Append( "<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>");
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
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >=3&&ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
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
                    outputStr.Append( "<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignDate"].ToString() + "</td>");
                else
                    outputStr.Append( "<td rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>");
                //if (ds.Tables[0].Rows[i]["ReportBindDate"].ToString() != "" && int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >=6)
                //    outputStr.Append( "<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportBindDate"].ToString() + "</td>");
                //else
                //    outputStr.Append( "<td width='8%' rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>");
                outputStr.Append( "</tr>");


                outputStr.Append( "<tr>");
                //报告校核
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
                    //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                    outputStr.Append( "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportDataUser"].ToString() + "</td>");
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
                    outputStr.Append( "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                //报告签发
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                    //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>");
                outputStr.Append( "</tr>");
                outputStr.Append( "<tr>");
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
                    outputStr.Append( "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>");
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                    outputStr.Append("<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignRemark"].ToString() + "</td>");
                else
                    outputStr.Append("<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignRemark"].ToString() + "</td>");
           
                outputStr.Append( "</tr>");
                //----分析项目----//
                ItemSql = "select t_MonitorItemDetail.id,t_M_AnalysisItemEx.AIName,t_DrawSampleDetail.fxDate,t_DrawSampleDetail.Remark,Name from t_MonitorItemDetail  inner join t_M_AnalysisItemEx on t_MonitorItemDetail.MonitorItem = t_M_AnalysisItemEx.id  left join  t_DrawSampleDetail on   t_MonitorItemDetail.fxDanID=t_DrawSampleDetail.DrawID and  t_DrawSampleDetail.ItemID=t_MonitorItemDetail.MonitorItem left join t_DrawSample on t_DrawSample.id=t_DrawSampleDetail.DrawID left join t_R_UserInfo  on t_DrawSample.fxman=t_R_UserInfo.UserID  ";
                ItemSql += "where t_MonitorItemDetail.SampleID = '" + ds.Tables[0].Rows[i]["id"].ToString() + "'   ";
                ItemSql += "order by  t_MonitorItemDetail.id";
                DataSet Ids = new MyDataOp(ItemSql).CreateDataSet();
                outputStr.Append( "<tr><td colspan='13'><table width='100%' class='listTable2'><tr><td colspan='10' align='center'>分析项目</td></tr>");

                // outputStr.Append( "<tr align='center'>";
                //outputStr.Append( "<td rowspan='4' width='8%'>分析项目</td>";


                if (Ids.Tables[0].Rows.Count > 0 && Ids.Tables[0].Rows.Count <= 10)
                {
                    outputStr.Append( "<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["fxDate"].ToString() != "")
                        {
                            outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                        }
                        else
                        {
                            outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append( "<td width='7%' ></td>");
                    }
                    outputStr.Append( "</tr>");
                    //数量
                    outputStr.Append( "<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Name"].ToString() + "</td>");
                       
                        //if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                        //{
                        //    outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>1</td>");
                        //}
                        //else
                        //{
                        //    outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>1</td>");
                        //}
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append( "<td width='7%' ></td>");
                    }
                    outputStr.Append( "</tr>");
                    //日期
                    outputStr.Append( "<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                        {
                            outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["FxDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                        }
                        else
                        {
                            outputStr.Append( "<td width='7%' style='background-color:#FF8C69'></td>");
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append( "<td width='7%' ></td>");
                    }
                    outputStr.Append( "</tr>");
                    outputStr.Append( "<tr>");
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                        {
                            outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                        }
                        else
                        {
                            outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr.Append( "<td width='7%'></td>");
                    }
                    outputStr.Append( "</tr>");


                }
                else
                {
                    int num = Ids.Tables[0].Rows.Count / 10 + 1;
                    int ni = 0;
                    for (ni = 0; ni < num - 1; ni++)
                    {
                        // outputStr.Append( "</tr>";
                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }

                        }
                        outputStr.Append( "</tr>");

                        //数量
                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Name"].ToString() + "</td>");
                       
                            //if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            //{
                            //    outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>1</td>");
                            //}
                            //else
                            //{
                            //    outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>1</td>");
                            //}

                        }
                        outputStr.Append( "</tr>");

                        //日期
                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["FxDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                            }
                            else
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#FF8C69'></td>");
                            }

                        }
                        outputStr.Append( "</tr>");

                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                            }

                        }
                        outputStr.Append( "</tr>");

                    }

                    if (ni == num - 1)
                    {
                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>");
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append( "<td width='7%' ></td>");
                        }
                        outputStr.Append( "</tr>");
                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            //if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            //{
                            outputStr.Append("<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Name"].ToString() + "</td>");
                            //}
                            //else
                            //{
                            //    outputStr.Append( "<td width='7%' style='background-color:#FF8C69'>1</td>");
                            //}
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append( "<td width='7%' ></td>");
                        }
                        outputStr.Append( "</tr>");
                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["FxDate"].ToString()).ToString("MM-dd HH:mm") + "</td>");
                            }
                            else
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#FF8C69'></td>");
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append( "<td width='7%' ></td>");
                        }
                        outputStr.Append( "</tr>");
                        outputStr.Append( "<tr>");
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["FxDate"].ToString() != "")
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>");
                            }
                            else
                            {
                                outputStr.Append( "<td width='7%' style='background-color:#FF8C69'></td>");
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr.Append( "<td width='7%' ></td>");
                        }
                        outputStr.Append( "</tr>");




                    }
                     Ids.Dispose();


                }

                //将分析项目分行显示
                outputStr.Append( "</tr>");
                outputStr.Append( "</table></td></tr>");
                //----分析项目----//
            }

        }
        outputStr.Append( "</table>");

        Samplelist.Text = outputStr.ToString();
        
       ds.Dispose();
       //if (flagRelash == "2")
       //{
         
       //    Response.ContentType = "text/xml";
       //    Response.Write(outputStr.ToString());
       //    Response.End();
       //    flagRelash = "";
       //}
       
    }
   
    
}
