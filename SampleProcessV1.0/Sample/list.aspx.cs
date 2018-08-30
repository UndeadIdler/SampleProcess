using System;
using System.Collections;
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
using WebApp.Components;

public partial class Sample_list : System.Web.UI.Page
{
    public string outputStr;
    // public static int intnum = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    Query1();
        //    //intnum++;
        //}

    }
    private void Query1()
    {
        //int topnum=10;
        //string sql1 = "select count(*) from view_ssxs";
        //DataSet ds1 = new MyDataOp(sql1).CreateDataSet();
        //int kk = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
        //if (intnum * 10 >= kk)
        //{ topnum = kk;
        //intnum = 1;
        //}
        //else
        //{
        //    topnum = intnum * 10;
        //}
       outputStr = "";
       outputStr = "<table>";
        //string strSql = "";
        string ItemSql = "";//SELECT TOP 10 *

       // string sql = "select top 10 * from ( select top " + topnum + " * from view_ssxs order by id asc) DERIVEDTBL order by id desc";
        string sql = "select  top 10 * from  view_ssxs  order by id desc";
        DataSet ds = new MyDataOp(sql).CreateDataSet();


        string strtemp = "select Name,UserID from t_R_UserInfo";
        DataSet ds_User = new MyDataOp(strtemp).CreateDataSet();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DataRow[] dataUser = ds_User.Tables[0].Select("UserID='" + dr["ReportWriteUserID"].ToString() + "'");
            foreach (DataRow drr in dataUser)
            {
                dr["ReportWriteUserID"] = drr["Name"].ToString();
            }

            DataRow[] dataUser2 = ds_User.Tables[0].Select("UserID='" + dr["ReportProofUserID"].ToString() + "'");
            foreach (DataRow drr in dataUser2)
            {
                dr["ReportProofUserID"] = drr["Name"].ToString();
            }
            DataRow[] dataUser3 = ds_User.Tables[0].Select("UserID='" + dr["ReportSignUserID"].ToString() + "'");
            foreach (DataRow drr in dataUser3)
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
        //kk = ds.Tables[0].Rows.Count;
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
                        outputStr += "<td  rowspan='3' width='8%' style='background-color:#FF8C69'  class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportRemark"].ToString() + "</td>";
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
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 3)
                //if (ds.Tables[0].Rows[i]["ReportWriteDate"].ToString() != "")
                {
                    outputStr += "<td width='8%' style='background-color:#9AFF9A' class='AutoNewline'>" + DateTime.Parse(ds.Tables[0].Rows[i]["ReportWriteDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
                }
                else
                {

                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'></td>";
                }


                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
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
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 5)
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
                    outputStr += "<td width='8%' style='background-color:#FF8C69' class='AutoNewline'>" + ds.Tables[0].Rows[i]["ReportWriteRemark"].ToString() + "</td>";
                if (int.Parse(ds.Tables[0].Rows[i]["StatusID"].ToString()) >= 4)
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
                outputStr += "<tr><td colspan='13'><table width='100%' class='listTable2'><tr><td colspan='10' align='center'>分析项目</td></tr>";

                // outputStr += "<tr align='center'>";
                //outputStr += "<td rowspan='4' width='8%'>分析项目</td>";


                if (Ids.Tables[0].Rows.Count > 0 && Ids.Tables[0].Rows.Count <= 10)
                {
                    outputStr += "<tr>";
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

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr += "<td width='7%' ></td>";
                    }
                    outputStr += "</tr>";
                    //数量
                    outputStr += "<tr>";
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                        {
                            outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>";
                        }
                        else
                        {
                            outputStr += "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>";
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr += "<td width='7%' ></td>";
                    }
                    outputStr += "</tr>";
                    //日期
                    outputStr += "<tr>";
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                        {
                            outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
                        }
                        else
                        {
                            outputStr += "<td width='7%' style='background-color:#FF8C69'></td>";
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr += "<td width='7%'></td>";
                    }
                    outputStr += "</tr>";
                    outputStr += "<tr>";
                    for (int j = 0; j < Ids.Tables[0].Rows.Count; j++)
                    {
                        if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                        {
                            outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>";
                        }
                        else
                        {
                            outputStr += "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>";
                        }
                    }

                    for (int mi = 0; mi < 10 - Ids.Tables[0].Rows.Count; mi++)
                    {
                        outputStr += "<td width='7%'></td>";
                    }
                    outputStr += "</tr>";


                }
                else
                {
                    int num = Ids.Tables[0].Rows.Count / 10 + 1;
                    int ni = 0;
                    for (ni = 0; ni < num - 1; ni++)
                    {
                        // outputStr += "</tr>";
                        outputStr += "<tr>";
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
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
                        outputStr += "</tr>";

                        //数量
                        outputStr += "<tr>";
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                            {
                                outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>";
                            }
                            else
                            {
                                outputStr += "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>";
                            }

                        }
                        outputStr += "</tr>";

                        //日期
                        outputStr += "<tr>";
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                            {
                                outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
                            }
                            else
                            {
                                outputStr += "<td width='7%' style='background-color:#FF8C69'></td>";
                            }

                        }
                        outputStr += "</tr>";

                        outputStr += "<tr>";
                        for (int j = ni * 10; j < (ni + 1) * 10; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                            {
                                outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>";
                            }
                            else
                            {
                                outputStr += "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>";
                            }

                        }
                        outputStr += "</tr>";

                    }

                    if (ni == num - 1)
                    {
                        outputStr += "<tr>";
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
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
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr += "<td width='7%' ></td>";
                        }
                        outputStr += "</tr>";
                        outputStr += "<tr>";
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                            {
                                outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>";
                            }
                            else
                            {
                                outputStr += "<td width='7%' style='background-color:#FF8C69'>" + Ids.Tables[0].Rows[j]["Num"].ToString() + "</td>";
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr += "<td width='7%' ></td>";
                        }
                        outputStr += "</tr>";
                        outputStr += "<tr>";
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                            {
                                outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + DateTime.Parse(Ids.Tables[0].Rows[j]["ReportDate"].ToString()).ToString("MM-dd HH:mm") + "</td>";
                            }
                            else
                            {
                                outputStr += "<td width='7%' style='background-color:#FF8C69'></td>";
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr += "<td width='7%' ></td>";
                        }
                        outputStr += "</tr>";
                        outputStr += "<tr>";
                        for (int j = ni * 10; j < Ids.Tables[0].Rows.Count; j++)
                        {
                            if (Ids.Tables[0].Rows[j]["ReportDate"].ToString() != "")
                            {
                                outputStr += "<td width='7%' style='background-color:#9AFF9A'>" + Ids.Tables[0].Rows[j]["Remark"].ToString() + "</td>";
                            }
                            else
                            {
                                outputStr += "<td width='7%' style='background-color:#FF8C69'></td>";
                            }
                        }
                        for (int mi = 0; mi < (ni + 1) * 10 - Ids.Tables[0].Rows.Count; mi++)
                        {
                            outputStr += "<td width='7%' ></td>";
                        }
                        outputStr += "</tr>";




                    }
                    Ids.Dispose();


                }

                //将分析项目分行显示
                outputStr += "</tr>";
                outputStr += "</table></td></tr>";
                //----分析项目----//
            }

        }
        outputStr += "</table>";
        ds.Dispose();
        //Response.Write(outputStr);
        //Response.End();//停止该页的执行
    }
   
}
