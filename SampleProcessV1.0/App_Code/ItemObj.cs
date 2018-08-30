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
///ItemObj 的摘要说明
/// </summary>
public class ItemObj
{
    public FlowItem FlowItemObj = new FlowItem();
	public ItemObj(string UserID,string flowid,string title,string CreateDate,String Remark)
	{
       
        FlowItemObj.title = title;
        FlowItemObj.flowid = flowid;
        FlowItemObj.UserID = UserID;
        FlowItemObj.CreateDate = CreateDate;
        FlowItemObj.Remark = Remark;
	}
   public struct FlowItem
    {
        public string UserID;
        public string CreateDate;
        public string Remark;
        public string title;
        public string flowid;
    }
    public int save()
    { 
         int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();

            try
            {

                IDbDataParameter[] prams = {
                
                                               };

                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "insert into t_Y_BackInfo ([itemid] ,[title] ,[remark],[userid],[createdate]) values('" + FlowItemObj.flowid + "','" + FlowItemObj.title + "','" + FlowItemObj.Remark + "','" + FlowItemObj.UserID + "','" + FlowItemObj.CreateDate + "')", prams);
               
                    if (iReturn == 1)
                    {
                        thelper.CommitTransaction(trans);
                    }
                    else
                        thelper.RollTransaction(trans);
               

            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);

                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
    }

}

