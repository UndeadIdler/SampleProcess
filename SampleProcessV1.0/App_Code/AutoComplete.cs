// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


using System;
using System.Collections.Generic;
using System.Web.Services;
using WebApp.Components;
using System.Data;
using System.Data.SqlClient;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : WebService
{
    public AutoComplete()
    {
    }
    /// <summary>
    /// 项目类型
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 

        SqlDataReader myDR = new MyDataOp("select top " + count + " ItemName from t_M_ItemInfo where ItemCode like  '" + prefixText + "%'group by ItemName order by ItemName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ItemName"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }
    /// <summary>
    /// 样品类型
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetSampleTypeList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 

       SqlDataReader myDR = new MyDataOp("select top " + count + " ClassName from t_M_AnalysisMainClassEx where ClassCode like  '" + prefixText + "%'group by ClassName,orderid order by orderid ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ClassName"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }
    /// <summary>
    /// 分析项目大类
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetMainClassList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 

        SqlDataReader myDR = new MyDataOp("select top " + count + " ClassName from t_M_AnalysisMainClassEx where ClassCode like  '" + prefixText + "%'group by ClassName order by ClassName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ClassName"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }
    /// <summary>
    /// 分析项目
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetClassList(string prefixText, int count,string contextKey)
    {
        List<string> items = new List<string>(count);//泛型 
        string conditionstr = "";
        if (contextKey != "-1")
            conditionstr=" ClassID='" + contextKey + "' and";
        SqlDataReader myDR = new MyDataOp("select top " + count + " AIName from t_M_AnalysisItemEx where" + conditionstr + " AICode like  '" + prefixText + "%'group by AIName order by AIName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["AIName"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }
    /// <summary>
    /// 报告标识列表
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetReportList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 
        //string conditionstr = "";
        SqlDataReader myDR = new MyDataOp("select top " + count + " ReportName from t_M_ReporInfo where ReportName like  '" + prefixText + "%' and (StatusID<=5) group by ReportName order by  ReportName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ReportName"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    } 
    
    /// <summary>
    /// 项目负责人
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetUserList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 
        //SqlDataReader myDR = new MyDataOp("select top " + count + " Name from t_R_UserInfo where (Name like  '%" + prefixText + "%' or UserID like '%" + prefixText + "%'  ) group by Name order by Name ").CreateReader();
        SqlDataReader myDR = new MyDataOp("select top " + count + " Name from View_User where (Name like  '%" + prefixText + "%' or UserID like '%" + prefixText + "%'  )  order by num desc ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["Name"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }

    /// <summary>
    /// 样品来源
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetSampleSourceList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 
       //SqlDataReader myDR = new MyDataOp("select top " + count + " 单位全称 from t_委托单位 where type='0' and  (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称 order by 单位全称 ").CreateReader();
        SqlDataReader myDR = new MyDataOp("select top " + count + " 单位全称 from View_SampleSource where   (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) order by num desc").CreateReader();

       
        while (myDR.Read())
        {
            items.Add(myDR["单位全称"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }

    /// <summary>
    /// 样品来源
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetUserOtherList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 
        SqlDataReader myDR = new MyDataOp("select top " + count + " Name from t_R_UserInfo where (Name like  '%" + prefixText + "%' or UserID like '%" + prefixText + "%'  ) group by Name order by Name ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["Name"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }
    /// <summary>
    /// 委托单位
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetClientList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 
       // SqlDataReader myDR = new MyDataOp("select top " + count + " 单位全称 from t_企业信息 where (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称  union (select top " + count + " 单位全称 from t_other where (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称) order by 单位全称 ").CreateReader();
       // SqlDataReader myDR = new MyDataOp("select top " + count + " 单位全称 from t_委托单位 where (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称  order by 单位全称 ").CreateReader();
        SqlDataReader myDR = new MyDataOp("select top " + count + " 单位全称 from View_wtdepart where   (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) order by num desc ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["单位全称"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }

    /// <summary>
    /// 排放口
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] Getcyd(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 
        // SqlDataReader myDR = new MyDataOp("select top " + count + " 单位全称 from t_企业信息 where (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称  union (select top " + count + " 单位全称 from t_other where (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称) order by 单位全称 ").CreateReader();
        SqlDataReader myDR = new MyDataOp("select top " + count + " outNO from t_OUTNO where (outNO like  '%" + prefixText + "%' or code like '%" + prefixText + "%'  ) group by outNO   order by outNO ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["outNO"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }
    /// <summary>
    /// 性状
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] Getxz(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//泛型 
        // SqlDataReader myDR = new MyDataOp("select top " + count + " 单位全称 from t_企业信息 where (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称  union (select top " + count + " 单位全称 from t_other where (单位全称 like  '%" + prefixText + "%' or 单位曾用名全称 like '%" + prefixText + "%' or 企业简称代码 like '%" + prefixText + "%'  ) group by 单位全称) order by 单位全称 ").CreateReader();
        SqlDataReader myDR = new MyDataOp("select top " + count + " xz from t_xz where (xz like  '%" + prefixText + "%' or code like '%" + prefixText + "%'  ) group by xz   order by xz ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["xz"].ToString());
        }
        //myCon.Close();//关闭数据库连接 
        return items.ToArray();
    }
}