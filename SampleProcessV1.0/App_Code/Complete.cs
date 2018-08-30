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
public class Complete : WebService
{
    public Complete()
    {
    }
    /// <summary>
    /// ��Ŀ����
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 

        SqlDataReader myDR = new MyDataOp("select top " + count + " ItemName from t_M_ItemInfo where ItemCode like  '" + prefixText + "%'group by ItemName order by ItemName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ItemName"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetSampleTypeList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 

        SqlDataReader myDR = new MyDataOp("select top " + count + " ClassName from t_M_AnalysisMainClassEx where ClassCode like  '" + prefixText + "%'group by ClassName order by ClassName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ClassName"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
    /// <summary>
    /// ������Ŀ����
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetMainClassList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 

        SqlDataReader myDR = new MyDataOp("select top " + count + " ClassName from t_M_AnalysisMainClassEx where ClassCode like  '" + prefixText + "%'group by ClassName order by ClassName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ClassName"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
    /// <summary>
    /// ������Ŀ
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetClassList(string prefixText, int count,string contextKey)
    {
        List<string> items = new List<string>(count);//���� 
        string conditionstr = "";
        if (contextKey != "-1")
            conditionstr=" ClassID='" + contextKey + "' and";
        SqlDataReader myDR = new MyDataOp("select top " + count + " AIName from t_M_AnalysisItemEx where" + conditionstr + " AICode like  '" + prefixText + "%'group by AIName order by AIName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["AIName"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
    /// <summary>
    /// �����ʶ�б�
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetReportList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 
        //string conditionstr = "";
        SqlDataReader myDR = new MyDataOp("select top " + count + " ReportName from t_M_ReporInfo where ReportName like  '" + prefixText + "%' and (StatusID<=5) group by ReportName order by  ReportName ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["ReportName"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    } 
    
    /// <summary>
    /// ��Ŀ������
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetUserList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 
        SqlDataReader myDR = new MyDataOp("select top " + count + " Name from View_User where (Name like  '%" + prefixText + "%' or UserID like '%" + prefixText + "%'  )  order by num  desc").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["Name"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
    /// <summary>
    /// ��Ŀ������
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetUserList2(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 
        SqlDataReader myDR = new MyDataOp("select top " + count + " Name from View_User where (Name like  '%" + prefixText + "%' or UserID like '%" + prefixText + "%'  )  order by orderstr desc, num  desc").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["Name"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
    /// <summary>
    /// ��Ʒ��Դ
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetSampleSourceList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 
        SqlDataReader myDR = new MyDataOp("select top " + count + " ��λȫ�� from View_SampleSource where   (��λȫ�� like  '%" + prefixText + "%' or ��λ������ȫ�� like '%" + prefixText + "%' or ��ҵ��ƴ��� like '%" + prefixText + "%'  ) order by num  desc").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["��λȫ��"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }

    /// <summary>
    /// ��Ʒ��Դ
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetUserOtherList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 
        SqlDataReader myDR = new MyDataOp("select top " + count + " Name from t_R_UserInfo where (Name like  '%" + prefixText + "%' or UserID like '%" + prefixText + "%'  ) group by Name order by Name ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["Name"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
    /// <summary>
    /// ί�е�λ
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetClientList(string prefixText, int count)
    {
        List<string> items = new List<string>(count);//���� 
        //SqlDataReader myDR = new MyDataOp("select top " + count + " ��λȫ�� from t_ί�е�λ where (��λȫ�� like  '%" + prefixText + "%' or ��λ������ȫ�� like '%" + prefixText + "%' or ��ҵ��ƴ��� like '%" + prefixText + "%'  ) group by ��λȫ��  order by ��λȫ�� ").CreateReader();
        SqlDataReader myDR = new MyDataOp("select top " + count + " ��λȫ�� from View_wtdepart where   (��λȫ�� like  '%" + prefixText + "%' or ��λ������ȫ�� like '%" + prefixText + "%' or ��ҵ��ƴ��� like '%" + prefixText + "%'  ) order by num desc ").CreateReader();

        while (myDR.Read())
        {
            items.Add(myDR["��λȫ��"].ToString());
        }
        //myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
    }
}