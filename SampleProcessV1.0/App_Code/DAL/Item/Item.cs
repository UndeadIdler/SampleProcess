using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccess;
using System.Configuration;


namespace DAl
{
/// <summary>
/// Item 的摘要说明
/// </summary>
    public class Item
    {
        public Item()
        {
        }
        /// <summary>
        /// 获取样品类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetType(string TypeID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            DataTable dtItem = new DataTable();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@Type",  DBTypeConverter.ConvertCsTypeToOriginDBType(TypeID.GetType().ToString()), TypeID, 50)            
                                          };
                string constr="";    
                //水t_M_AnalysisMainClassEx增加Class字段，用于水组区分
                if(TypeID!="")
                {
                    constr = " and Class='" + TypeID + "'";
                }
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select ClassID TypeID,ClassName 样品类型 from t_M_AnalysisMainClassEx where 1=1 " + constr, prams);

                dt.Load(idr);


            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// 获取样品监测项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetItem(string classId)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = {
                                };
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select t_M_ANItemInf.id,t_M_ANItemInf.AIName from t_M_AnalysisItemEx inner join t_M_ANItemInf on t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID where ClassID='" + classId + "'", prams);
                dt.Load(idrItem);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 按类型获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetABRole(string userid, string role)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            DataTable dtItem = new DataTable();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);

            try
            {

                IDbDataParameter[] prams = {
                                };
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select distinct t_M_AnalysisItemEx.ClassID,t_M_AnalysisMainClassEx.ClassName from t_MoniterUser inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.AIID=t_MoniterUser.itemid and t_MoniterUser.typeid=t_M_AnalysisItemEx.ClassID inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_M_AnalysisItemEx.ClassID where userid='" + userid + "' and role='" + role + "' ", prams);
                //IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select t_M_ANItemInf.id,AIName,role,t_MoniterUser.method from t_MoniterUser inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MoniterUser.itemid  where userid='" + userid + "' and role='" + role + "' ", prams);

                dtItem.Load(idrItem);

            }
            catch (Exception ex)
            {
                dtItem = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dtItem;
        }
        /// <summary>
        /// 获取样品监测项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetAB(string userid, string role)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            DataTable dtItem = new DataTable();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);

            try
            {

                IDbDataParameter[] prams = {
                                };
                string condstr = "";
              
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select t_M_AnalysisItemEx.id,t_M_ANItemInf.AIName+'('+t_M_ANItemInf.ClassName+')' as AIName,role,t_M_AnalysisItemEx.ClassID from t_MoniterUser inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=t_MoniterUser.itemid inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_M_AnalysisItemEx.ClassID where userid='" + userid + "' and role='" + role + "' " + condstr, prams);
                //IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select t_M_ANItemInf.id,AIName,role,t_MoniterUser.method from t_MoniterUser inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MoniterUser.itemid  where userid='" + userid + "' and role='" + role + "' ", prams);

                dtItem.Load(idrItem);

            }
            catch (Exception ex)
            {
                dtItem = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dtItem;
        }
        /// <summary>
        /// 获取样品监测项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetAB(string userid,string typeid,string role)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            DataTable dtItem = new DataTable();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);

            try
            {

                IDbDataParameter[] prams = {
                                };
                string condstr="";
                if (typeid != "")
                    condstr = " and t_M_AnalysisItemEx.ClassID='" + typeid + "'";
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select t_M_AnalysisItemEx.AIID as id,t_M_ANItemInf.AIName,role,t_M_AnalysisItemEx.ClassID from t_MoniterUser inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.AIID=t_MoniterUser.itemid and t_MoniterUser.typeid=t_M_AnalysisItemEx.ClassID  inner join t_M_AnalysisMainClassEx on t_M_AnalysisMainClassEx.ClassID=t_M_AnalysisItemEx.ClassID inner join t_M_ANItemInf on t_M_ANItemInf.id=t_M_AnalysisItemEx.AIID where userid='" + userid + "' and role='" + role + "' " + condstr, prams);
                //IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select t_M_ANItemInf.id,AIName,role,t_MoniterUser.method from t_MoniterUser inner join t_M_ANItemInf on t_M_ANItemInf.id=t_MoniterUser.itemid  where userid='" + userid + "' and role='" + role + "' ", prams);
          
                dtItem.Load(idrItem);

            }
            catch (Exception ex)
            {
                dtItem = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dtItem;
        }


        /// <summary>
        /// 获取样品监测项
        /// </summary>
        /// <param name="ItemId">样品分类与监测项对应中的ID</param>
        /// <returns></returns>
        public DataTable GetMethod(string ItemId,string TypeID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = {
                                };
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select t_M_AnStandard.id,t_M_AnStandard.Standard from  t_M_AIStandard inner join t_M_AnStandard on t_M_AnStandard.id=t_M_AIStandard.Standardid inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.ID=t_M_AIStandard.AIID  where t_M_AnalysisItemEx.AIID='" + ItemId + "' and t_M_AnalysisItemEx.ClassID='" + TypeID + "'", prams);
                dt.Load(idrItem);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 获取监测项字典中的信息
        /// </summary>
        /// <param name="ItemId">监测项的ID</param>
        /// <returns></returns>
        public DataTable GetDictionary(string ItemName)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = {
                                };
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_M_ANItemInf  where AIName='" + ItemName + "'", prams);
                dt.Load(idrItem);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
        

        /// <summary>
        /// 获取监测项与样品对应中的信息
        /// </summary>
        /// <param name="typeid">样品类型ID</param>
        /// <param name="ItemId">监测项的ID</param>
        /// <returns></returns>
        public DataTable GetTypeItem(int typeid,int ItemId)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           
            try
            {

                IDbDataParameter[] prams = {
                                };
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_M_AnalysisItemEx where AIID ='" + ItemId + "' and ClassID='" + typeid + "'", prams);
                dt.Load(idrItem);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// 获取监测项与样品对应中的信息
        /// </summary>
        /// <param name="typeid">样品类型ID</param>
        /// <param name="ItemId">监测项的ID</param>
        /// <returns></returns>
        public DataTable GetItemList(string typeid)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);

            try
            {

                IDbDataParameter[] prams = {
                                };
                IDataReader idrItem = db.ExecuteReader(Config.constr, CommandType.Text, "select  t_M_AnalysisItemEx.AIID id,t_M_ANItemInf.AIName from t_M_ANItemInf inner join  t_M_AnalysisItemEx on   AIID=t_M_ANItemInf.id where  ClassID in (" + typeid + ") order by  t_M_ANItemInf.orderid,t_M_ANItemInf.AIName", prams);
                dt.Load(idrItem);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
    }
}
