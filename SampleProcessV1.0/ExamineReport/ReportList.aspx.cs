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

public partial class ExamineReport_ReportList : System.Web.UI.Page
{
    public string outputStr;
    public string outputSum;
    public int kk = 0;
    private DataSet ds
    {
        get { return (DataSet)ViewState["ds"]; }
        set { ViewState["ds"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化页面          
            Query();
            SummaryQuery();
            #endregion 
        }       
    }
    private void SummaryQuery()
    {
        string sql = "SELECT t_M_AnalysisItem.AIName,";
        sql += "SUM(CASE WHEN t_M_MonitorItem.ReportDate IS NULL THEN t_M_MonitorItem.num ELSE 0 END) AS undone,";
        sql += "SUM(CASE WHEN t_M_MonitorItem.ReportDate IS NOT NULL THEN t_M_MonitorItem.num ELSE 0 END) AS done ";
        sql += "FROM t_M_MonitorItem INNER JOIN t_M_AnalysisItem ON t_M_MonitorItem.MonitorItem = t_M_AnalysisItem.id INNER JOIN t_M_SampleInfor ON t_M_MonitorItem.SampleID = t_M_SampleInfor.id inner join t_M_ReporInfo on t_M_ReporInfo.id=t_M_SampleInfor.ReportID ";
        sql += "WHERE (t_M_ReporInfo.StatusID < 6) GROUP BY t_M_AnalysisItem.AIName";
        DataSet dss = new MyDataOp(sql).CreateDataSet();
        
        outputSum = "<table><tr><td>分析项目</td>";
        if (dss.Tables[0].Rows.Count > 0)
        {
            string AIName = "";
            string UnDone = "";
            string Done = "";
            for (int m = 0; m < dss.Tables[0].Rows.Count; m++)
            {
                AIName = dss.Tables[0].Rows[m][0].ToString();
                outputSum += "<td>" + AIName + "</td>";
            }
            outputSum += "</tr><tr><td>已完成</td>";
            for (int n = 0; n < dss.Tables[0].Rows.Count; n++)
            {              
                Done = dss.Tables[0].Rows[n][2].ToString();
                outputSum += "<td>" + Done + "</td>";
            }
            outputSum += "</tr><tr><td>未完成</td>";
            for (int l = 0; l < dss.Tables[0].Rows.Count; l++)
            {
                UnDone = dss.Tables[0].Rows[l][1].ToString();
                outputSum += "<td>" + UnDone + "</td>";
            }
        }
        outputSum += "</tr></table>";
    }
    private void Query()
    {
        outputStr = "";
        outputStr = "<table>";
        // outputStr = "<table class='listTable' boder='0' cellspacing='1' width='100%'><tbody><tr align='center'><th>序号</th><th>接样时间</th><th>项目类型</th><th>样品类型</th><th>样品编号</th>";
        string strSql = "";
        string ItemSql = "";
        strSql = @" SELECT t_M_ReporInfo.ReportName, t_M_ReporInfo.ReportRemark," +
       "t_M_ItemInfo.ItemName, t_M_SampleInfor.id, t_M_SampleInfor.AccessDate, " +
       "t_M_SampleType.SampleType, t_M_SampleInfor.SampleID, t_M_SampleInfor.UserID," +
       "t_M_SampleInfor.CreateDate, t_M_ReporInfo.ReportWriteDate," +
       "t_M_ReporInfo.ReportWriteUserID, t_M_ReporInfo.ReportWriteRemark," +
       "t_M_ReporInfo.ReportProofDate, t_M_ReporInfo.ReportProofUserID," +
       "t_M_ReporInfo.ReportProofRemark, t_M_ReporInfo.ReportCheckDate," +
       "t_M_ReporInfo.ReportSignUserID, t_M_ReporInfo.ReportProofRemark," +
       "t_M_ReporInfo.ReportSignDate, t_M_ReporInfo.ReportBindDate," +
       "t_M_ReporInfo.StatusID,t_M_ReporInfo.urgent " +
 " FROM t_M_ReporInfo INNER JOIN" +
      "  t_M_SampleInfor ON t_M_ReporInfo.id = t_M_SampleInfor.ReportID INNER JOIN" +
      " t_M_ItemInfo ON t_M_ReporInfo.ItemType = t_M_ItemInfo.ItemID INNER JOIN" +
      " t_M_SampleType ON t_M_SampleInfor.TypeID = t_M_SampleType.TypeID" +
 " WHERE (t_M_ReporInfo.StatusID <> 6)"; 

        //strSql += "select t_M_SampleInfor.id,t_M_SampleInfor.AccessDate,t_M_ItemInfo.ItemName,t_M_SampleType.SampleType,t_M_SampleInfor.SampleID,t_M_SampleInfor.UserID,t_M_SampleInfor.CreateDate,";
        //strSql += "t_M_SampleInfor.ReportWriteDate,t_M_SampleInfor.ReportWriteUserID,t_M_SampleInfor.ReportWriteRemark,t_M_SampleInfor.ReportProofDate,t_M_SampleInfor.ReportProofUserID,t_M_SampleInfor.ReportProofRemark,t_M_SampleInfor.ReportCheckDate,t_M_SampleInfor.ReportSignUserID,t_M_SampleInfor.ReportProofRemark,t_M_SampleInfor.ReportSignDate,t_M_SampleInfon.ReportDate,t_M_SampleInfor.StatusID";
        //strSql += " from t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType where t_M_SampleInfor.ItemType = t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID = t_M_SampleType.TypeID and t_M_SampleInfor.StatusID <> 6";
        //存在要查询的数据，添加排序条件
        if (strSql != "")
        {
            strSql += " order by t_M_SampleInfor.AccessDate desc";
        }
        //outputStr += "<th>报告编制</th><th>报告校核</th><th>报告审核</th><th>报告签发</th><th>报告装订</th></tr>";
        ds = new MyDataOp(strSql).CreateDataSet();
        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            foreach (DataRow drr in ds_User.Tables[0].Rows)
            {
                if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportWriteUserID"] = drr["Name"].ToString();
                if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportProofUserID"] = drr["Name"].ToString();
                if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
                    dr["ReportSignUserID"] = drr["Name"].ToString();
            }
        }
        kk = ds.Tables[0].Rows.Count;
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                outputStr += "<tr>";
                outputStr += "<td  rowspan='3' width='5%' class='AutoNewline'>" + (i + 1).ToString() + "</td>";
                if (ds.Tables[0].Rows[i]["AccessDate"].ToString() != "")
                {
                    string strurgent = "";
                    strurgent = ds.Tables[0].Rows[i]["urgent"].ToString();
                    //if ( == "1")
                    //{
                    //    ;
                    //}
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["ReportName"].ToString() + "</font></td>";
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "</td>";
                    outputStr += "<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["AccessDate"].ToString() + "</td>";
                    outputStr += "<td  rowspan='3' width='5%' style='background-color:#9AFF9A' class='AutoNewline'>" + strurgent + "</td>";
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["SampleType"].ToString() + "</td>";
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["SampleID"].ToString() + "</font></td>";

                }
                else
                {
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                    outputStr += "<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'></td>";
                    outputStr += "<td  rowspan='3' width='5%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                    outputStr += "<td  rowspan='3' width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";

                }
               
                if (ds.Tables[0].Rows[i]["ReportRemark"] != null)
                {
                   
                   
                    
                        if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
                        {
                            outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportRemark"].ToString() + "</td>";

                        }
                        else
                        {
                            outputStr += "<td  rowspan='3' width='8%' style='background-color:#FF8C69'  class='AutoNewline'>"+ ds.Tables[0].Rows[i]["ReportRemark"].ToString() + "</td>";
                        }
                }
                else
                {
                    if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 2)
                    {
                        outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'></td>";

                    }
                    else
                    {
                        outputStr += "<td  rowspan='3' width='8%' style='background-color:#FF8C69'  class='AutoNewline'></td>";
                    }

                }
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >=3)
                //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                {
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportWriteDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
                }
                else
                {
                   
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                }


                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >=4)
               // if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportProofDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
                else
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportCheckDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
                else
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";

               // if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 6)
                if (ds.Tables[0].Rows[i]["ReportSignDate"].ToString() != "" && int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                    outputStr += "<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignDate"].ToString() + "</td>";
                else
                    outputStr += "<td rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>";
                if (ds.Tables[0].Rows[i]["ReportBindDate"].ToString() != "" && int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                    outputStr += "<td width='8%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportBindDate"].ToString() + "</td>";
                else
                    outputStr += "<td width='8%' rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>";
                outputStr += "</tr>";


                outputStr += "<tr>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
                //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteUserID"].ToString() + "</td>";
                else
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
                //if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofUserID"].ToString() + "</td>";
                else
                    outputStr += "<td style='background-color:#FF8C69'></td>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >=5)
                //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>";
                else
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                outputStr += "</tr>";
                outputStr += "<tr>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
                //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteRemark"].ToString() + "</td>";
                else
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>"+ds.Tables[0].Rows[i]["ReportWriteRemark"].ToString()+"</td>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >=4)
                //if (ds.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>";
                else
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
                //if (ds.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>";
                else
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>";
                outputStr += "</tr>";
                //----分析项目----//
                ItemSql = "select t_M_MonitorItem.id,t_M_AnalysisItem.AIName,t_M_MonitorItem.Num,t_M_MonitorItem.ReportDate,Remark from t_M_MonitorItem,t_M_AnalysisItem ";
                ItemSql += "where t_M_MonitorItem.SampleID = '" + ds.Tables[0].Rows[i]["id"].ToString() + "' and t_M_MonitorItem.MonitorItem = t_M_AnalysisItem.id ";
                ItemSql += "order by t_M_MonitorItem.id";
                DataSet Ids = new MyDataOp(ItemSql).CreateDataSet();
                outputStr += "<tr><td colspan='13'><table width='100%' class='listTable2'>";

                outputStr += "<tr align='center'>";
                outputStr += "<td rowspan='4' width='8%'>分析项目</td>";

                if (Ids.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                        {
                            outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>";
                        }
                        else
                        {
                            outputStr += "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>";
                        }
                    }
                    outputStr += "<td></td>";
                }
                outputStr += "</tr>";
                outputStr += "<tr>";
                if (Ids.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                        { outputStr += "<td style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>"; }
                        else
                        { outputStr += "<td style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>"; }
                    }
                    outputStr += "<td></td>";
                }
                outputStr += "</tr>";
                outputStr += "<tr>";
                if (Ids.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                        { outputStr += "<td style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>"; }
                        else
                        { outputStr += "<td  style='background-color:#FF8C69'></td>"; }
                    }
                    outputStr += "<td></td>";
                }
                outputStr += "</tr>";
                outputStr += "<tr>";
                if (Ids.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                        { outputStr += "<td style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>"; }
                        else
                        { outputStr += "<td  style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>"; }
                    }
                    outputStr += "<td></td>";
                }
                outputStr += "</tr>";
                outputStr += "</table></td></tr>";
                //----分析项目----//
            }
        }
        outputStr += "</table>";
    }

    //private void Query()
    //{
    //    outputStr = "";
    //    outputStr = "<table>";
    //    string ReportSql="";
    //    string strSql = "";
    //    string ItemSql = "";
    //    ReportSql += "select t_M_ReporInfo.id,t_M_ReporInfo.ReportName,t_M_ReporInfo.ReportRemark,t_M_ItemInfo.ItemName, ";
    //    ReportSql += "t_M_ReporInfo.ReportWriteDate,t_M_ReporInfo.ReportWriteUserID,t_M_ReporInfo.ReportWriteRemark,t_M_ReporInfo.ReportProofDate,t_M_ReporInfo.ReportProofUserID,t_M_ReporInfo.ReportProofRemark,t_M_ReporInfo.ReportCheckDate,t_M_ReporInfo.ReportSignUserID,t_M_ReporInfo.ReportProofRemark,t_M_ReporInfo.ReportSignDate,t_M_ReporInfo.ReportBindDate,t_M_ReporInfo.StatusID";
    //        ReportSql += " from t_M_ReporInfo, t_M_ItemInfo where t_M_ReporInfo.ItemType = t_M_ItemInfo.ItemID and t_M_ReporInfo.StatusID <> 6";
    //        //存在要查询的数据，添加排序条件
    //        if (ReportSql != "")
    //        {
    //            ReportSql += " order by t_M_ReporInfo.id desc";
    //        }    
    //    DataSet dsReport = new MyDataOp(ReportSql).CreateDataSet();
    //     if (dsReport.Tables[0].Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
    //        {
    //            strSql = "";
    //             strSql += " select t_M_ReporInfo.ReportName,t_M_ReporInfo.ReportRemark,t_M_ItemInfo.ItemName, t_M_SampleInfor.id,t_M_SampleInfor.AccessDate,t_M_SampleType.SampleType,t_M_SampleInfor.SampleID,t_M_SampleInfor.UserID,t_M_SampleInfor.CreateDate,";
    //             strSql += "t_M_ReporInfo.ReportWriteDate,t_M_ReporInfo.ReportWriteUserID,t_M_ReporInfo.ReportWriteRemark,t_M_ReporInfo.ReportProofDate,t_M_ReporInfo.ReportProofUserID,t_M_ReporInfo.ReportProofRemark,t_M_ReporInfo.ReportCheckDate,t_M_SampleInfor.ReportSignUserID,t_M_SampleInfor.ReportProofRemark,t_M_SampleInfor.ReportSignDate,t_M_SampleInfon.ReportDate,t_M_SampleInfor.StatusID";
    //             strSql += " from t_M_ReporInfo, t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType where t_M_ReporInfo.id=t_M_SampleInfor.ReportID and t_M_SampleInfor.ItemType = t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID = t_M_SampleType.TypeID and t_M_SampleInfor.StatusID <> 6 and  t_M_SampleInfor.ReportID='"+dsReport.Tables[0].Rows[i]["id"].ToString()+"'";

    //            if (strSql != "")
    //            {
    //                strSql += " order by t_M_SampleInfor.AccessDate desc";
    //            }   
    //            DataSet dsSample=new MyDataOp(strSql).CreateDataSet();
    //            string strtemp = "select Name,UserID from t_R_UserInfo";
    //            DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
    //            foreach (DataRow dr in dsSample.Tables[0].Rows)
    //            {
    //                foreach (DataRow drr in ds_User.Tables[0].Rows)
    //                {
    //                    if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
    //                        dr["ReportWriteUserID"] = drr["Name"].ToString();
    //                    if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
    //                        dr["ReportProofUserID"] = drr["Name"].ToString();
    //                    if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
    //                        dr["ReportSignUserID"] = drr["Name"].ToString();
    //                }
    //            }
    //            outputStr += "<tr>";
    //            outputStr += "<td  rowspan='3' width='4%' class='AutoNewline'>" + (i + 1).ToString() + "</td>";
    //             if(dsSample.Tables[0].Rows.Count>0)
    //            {


    //                outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportName"].ToString() + "</td>";
    //                outputStr += "<td  rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ItemName"].ToString() + "</td>";
                   
                
    //                //outputStr += "<td  rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["AccessDate"].ToString() + "</td>";
    //              //每个样品单列出
    //                 outputStr+="<td rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>";
    //                  outputStr+="<table width='200px'>";
    //                foreach (DataRow dr in dsSample.Tables[0].Rows)
    //                {
    //                    outputStr += "<tr><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'> " + dr["SampleID"].ToString() + "</td><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'>" + dr["AccessDate"].ToString() + "</td><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'>" + dr["SampleType"].ToString() + "</td>";
    //                    ItemSql = "select t_M_MonitorItem.id,t_M_AnalysisItem.AIName,t_M_MonitorItem.Num,t_M_MonitorItem.ReportDate,Remark from t_M_MonitorItem,t_M_AnalysisItem ";
    //                    ItemSql += "where t_M_MonitorItem.SampleID = '" + dr["id"].ToString() + "' and t_M_MonitorItem.MonitorItem = t_M_AnalysisItem.id ";
    //                    ItemSql += "order by t_M_MonitorItem.id";
    //                    DataSet Ids = new MyDataOp(ItemSql).CreateDataSet();
    //                    outputStr += "<td><table>";
    //                    foreach (DataRow dr_Item in Ids.Tables[0].Rows)
    //                    {
    //                        outputStr += "<tr><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'>" + dr_Item["AIName"].ToString() + "</td><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'>" + dr_Item["Num"].ToString() + "</td><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'>" + dr_Item["ReportDate"].ToString() + "</td><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'>" + dr_Item["Remark"].ToString() + "</td></tr>";

    //                    }
    //                    outputStr += " </td></tr>";
    //                }
    //                outputStr += "</table>";

    //                // outputStr += "<td  rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["SampleType"].ToString() + "</td>";
    //               // outputStr += "<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["SampleID"].ToString() + "</font></td>";
    //            }
    //            else
    //            {
    //                outputStr += "<td  rowspan='3' width='8%' style='background-color:#9AFF9A' class='AutoNewline'></td>";
    //                outputStr += "<td  rowspan='3' width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //                //每个样品单列出
    //                outputStr += "<td rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>";
    //                outputStr += "<table width='200px'>";
    //                //foreach (DataRow dr in dsSample.Tables[0].Rows)
    //                //{
    //                    outputStr += "<tr><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'></td><td width='70px' style='background-color:#9AFF9A' class='AutoNewline'></td><tr>";
    //                //}
    //                outputStr += "</table>";
    //               // outputStr += "<td  rowspan='3' width='5%' class='AutoNewline'>" + (i + 1).ToString() + "</td>";
    //               // outputStr += "<td  rowspan='3' width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                  
    //                //outputStr += "<td  rowspan='3' width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //               // outputStr += "<td  rowspan='3' width='10%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //            }

    //    //    }
    //    //    }
    //    //// outputStr = "<table class='listTable' boder='0' cellspacing='1' width='100%'><tbody><tr align='center'><th>序号</th><th>接样时间</th><th>项目类型</th><th>样品类型</th><th>样品编号</th>";
    //    //string strSql = "";
    //    //string ItemSql = "";
    //    //strSql += "select t_M_ReporInfo.ReportName,t_M_ReporInfo.ReportRemark,t_M_ItemInfo.ItemName, t_M_SampleInfor.id,t_M_SampleInfor.AccessDate,t_M_SampleType.SampleType,t_M_SampleInfor.SampleID,t_M_SampleInfor.UserID,t_M_SampleInfor.CreateDate,";
    //    //strSql += "t_M_ReporInfo.ReportWriteDate,t_M_ReporInfo.ReportWriteUserID,t_M_ReporInfo.ReportWriteRemark,t_M_ReporInfo.ReportProofDate,t_M_ReporInfo.ReportProofUserID,t_M_ReporInfo.ReportProofRemark,t_M_ReporInfo.ReportCheckDate,t_M_SampleInfor.ReportSignUserID,t_M_SampleInfor.ReportProofRemark,t_M_SampleInfor.ReportSignDate,t_M_SampleInfon.ReportDate,t_M_SampleInfor.StatusID";
    //    //strSql += " from t_M_ReporInfo, t_M_SampleInfor,t_M_ItemInfo,t_M_SampleType where t_M_ReporInfo.id=t_M_SampleInfor.ReportID and t_M_SampleInfor.ItemType = t_M_ItemInfo.ItemID and t_M_SampleInfor.TypeID = t_M_SampleType.TypeID and t_M_SampleInfor.StatusID <> 6";
        
    //    ////outputStr += "<th>报告编制</th><th>报告校核</th><th>报告审核</th><th>报告签发</th><th>报告装订</th></tr>";
       
    //    //ds = new MyDataOp(strSql).CreateDataSet();
    //    //string strtemp = "select Name,UserID from t_R_UserInfo";
    //    //DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();
    //    //foreach (DataRow dr in ds.Tables[0].Rows)
    //    //{
    //    //    foreach (DataRow drr in ds_User.Tables[0].Rows)
    //    //    {
    //    //        if (dr["ReportWriteUserID"].ToString() == drr["UserID"].ToString())
    //    //            dr["ReportWriteUserID"] = drr["Name"].ToString();
    //    //        if (dr["ReportProofUserID"].ToString() == drr["UserID"].ToString())
    //    //            dr["ReportProofUserID"] = drr["Name"].ToString();
    //    //        if (dr["ReportSignUserID"].ToString() == drr["UserID"].ToString())
    //    //            dr["ReportSignUserID"] = drr["Name"].ToString();
    //    //    }
    //    ////}
    //    //kk = ds.Tables[0].Rows.Count;
    //    //if (ds.Tables[0].Rows.Count > 0)
    //    //{
    //    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    //    {
    //    //        outputStr += "<tr>";
    //    //        outputStr += "<td  rowspan='3' width='5%' class='AutoNewline'>" + (i + 1).ToString() + "</td>";
    //    //        if (ds.Tables[0].Rows[i]["AccessDate"].ToString() != "")
    //    //        {

    //    //            outputStr += "<td  rowspan='3' width='5%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportName"].ToString() + "</td>";
    //    //            outputStr += "<td  rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "</td>";
    //    //       // 
                
    //    //            outputStr += "<td  rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["AccessDate"].ToString() + "</td>";
                  
    //    //            outputStr += "<td  rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + ds.Tables[0].Rows[i]["SampleType"].ToString() + "</td>";
    //    //            outputStr += "<td  rowspan='3' width='10%' style='background-color:#9AFF9A' class='AutoNewline'><font color=red>" + ds.Tables[0].Rows[i]["SampleID"].ToString() + "</font></td>";
    //    //        }
    //    //        else
    //    //        {
    //    //            outputStr += "<td  rowspan='3' width='9%' style='background-color:#9AFF9A' class='AutoNewline'></td>";
    //    //            outputStr += "<td  rowspan='3' width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //    //           // outputStr += "<td  rowspan='3' width='5%' class='AutoNewline'>" + (i + 1).ToString() + "</td>";
    //    //            outputStr += "<td  rowspan='3' width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                  
    //    //            outputStr += "<td  rowspan='3' width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //    //            outputStr += "<td  rowspan='3' width='10%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //    //        }
    //             if (dsReport.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
    //                 outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(dsReport.Tables[0].Rows[i]["ReportWriteDate"].ToString()).ToString("MM-dd HH:mm") + "</td>"; 
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";

    //             if (dsReport.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
    //                 outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(dsReport.Tables[0].Rows[i]["ReportProofDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";

    //             if (dsReport.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
    //                 outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(dsReport.Tables[0].Rows[i]["ReportCheckDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";


    //             if (dsReport.Tables[0].Rows[i]["ReportSignDate"].ToString() != "")
    //                 outputStr += "<td width='9%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportSignDate"].ToString() + "</td>";
    //            else
    //                outputStr += "<td rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //             if (dsReport.Tables[0].Rows[i]["ReportBindDate"].ToString() != "")
    //                 outputStr += "<td width='9%'  rowspan='3' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportBindDate"].ToString() + "</td>";
    //            else
    //                outputStr += "<td width='9%' rowspan='3' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //            outputStr += "</tr>";

               
    //            outputStr += "<tr>";
    //            if (dsReport.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
    //                outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportWriteUserID"].ToString() + "</td>";
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //            if (dsReport.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
    //                outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportProofUserID"].ToString() + "</td>";
    //            else
    //                outputStr += "<td style='background-color:#FF8C69'></td>";
    //            if (dsReport.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
    //                outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportSignUserID"].ToString() + "</td>";
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //            outputStr += "</tr>";
    //            outputStr += "<tr>";
    //            if (dsReport.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
    //                outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportWriteRemark"].ToString() + "</td>";
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //            if (dsReport.Tables[0].Rows[i]["ReportProofDate"].ToString() != "")
    //                outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>";
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //            if (dsReport.Tables[0].Rows[i]["ReportCheckDate"].ToString() != "")
    //                outputStr += "<td width='9%' style='background-color:#9AFF9A' class='AutoNewline'>" + dsReport.Tables[0].Rows[i]["ReportProofRemark"].ToString() + "</td>";
    //            else
    //                outputStr += "<td width='9%' style='background-color:#FF8C69' class='AutoNewline'></td>";
    //            outputStr += "</tr>";
    //            ////----分析项目----//
    //            //ItemSql = "select t_M_MonitorItem.id,t_M_AnalysisItem.AIName,t_M_MonitorItem.Num,t_M_MonitorItem.ReportDate,Remark from t_M_MonitorItem,t_M_AnalysisItem ";
    //            //ItemSql += "where t_M_MonitorItem.SampleID = '" + dsReport.Tables[0].Rows[i]["id"].ToString() + "' and t_M_MonitorItem.MonitorItem = t_M_AnalysisItem.id ";
    //            //ItemSql += "order by t_M_MonitorItem.id";
    //            //DataSet Ids = new MyDataOp(ItemSql).CreateDataSet();
    //            //outputStr += "<tr><td colspan='9'><table width='100%' class='listTable2'>";

    //            //outputStr += "<tr align='center'>";
    //            //outputStr += "<td rowspan='4' width='5%'>分析项目</td>";
                
    //            //if (Ids.Tables[0].Rows.Count > 0)
    //            //{
    //            //    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //            //    {
    //            //        if(Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //            //        {
    //            //            outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>";
    //            //        }else
    //            //        {
    //            //            outputStr += "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["AIName"].ToString() + "</td>";
    //            //        }
    //            //    }
    //            //    outputStr += "<td></td>";
    //            //}
    //            //outputStr += "</tr>";
    //            //outputStr += "<tr>";
    //            //if (Ids.Tables[0].Rows.Count > 0)
    //            //{
    //            //    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //            //    {
    //            //        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //            //        { outputStr += "<td style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>"; }
    //            //        else
    //            //        { outputStr += "<td style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>"; }
    //            //    }
    //            //    outputStr += "<td></td>";
    //            //}
    //            //outputStr += "</tr>";
    //            //outputStr += "<tr>";
    //            //if (Ids.Tables[0].Rows.Count > 0)
    //            //{
    //            //    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //            //    {
    //            //        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //            //        { outputStr += "<td style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>"; }
    //            //        else
    //            //        { outputStr += "<td  style='background-color:#FF8C69'></td>"; }
    //            //    }
    //            //    outputStr += "<td></td>";
    //            //}
    //            //outputStr += "</tr>";
    //            //outputStr += "<tr>";
    //            //if (Ids.Tables[0].Rows.Count > 0)
    //            //{
    //            //    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
    //            //    {
    //            //        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
    //            //        { outputStr += "<td style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>"; }
    //            //        else
    //            //        { outputStr += "<td  style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>"; }
    //            //    }
    //            //    outputStr += "<td></td>";
    //            //}
    //            //outputStr += "</tr>";
    //            //outputStr += "</table></td></tr>";
    //            //outputStr+="</td></tr>";
    //            //----分析项目----//
    //        }        
    //    }
    //    outputStr += "</table>";
    //}

    
}
