using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccess;
using System.Configuration;
using WebApp.Components;

namespace DAl
{
    /// <summary>
    /// ZKItem 的摘要说明
    /// </summary>
    public class ZKItem
    {
        public ZKItem()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 添加质控样信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Save(List<Entity.ZKItem> entityList)
        {
            int iReturn = 0;

            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           // IDbTransaction trans = thelper.StartTransaction();
           
            try
            {


                foreach (Entity.ZKItem entity in entityList)
                {
                    if (entity.ID != 0)
                    {
                        IDbDataParameter[] prams = {
                         dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                         dbFactory.MakeInParam("@MonitorItem",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.MonitorItem.GetType().ToString()), entity.MonitorItem, 50) ,              
                         dbFactory.MakeInParam("@UpdateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.UpdateDate.GetType().ToString()),  entity.UpdateDate , 0) ,
                         dbFactory.MakeInParam("@UpdateUserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UpdateUserID.GetType().ToString()), entity.UpdateUserID, 50) ,              
                         dbFactory.MakeInParam("@analysisnum",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.analysisnum.GetType().ToString()),  entity.analysisnum , 50) ,
                         dbFactory.MakeInParam("@scenejcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.scenejcnum.GetType().ToString()), entity.scenejcnum,50),  
                         dbFactory.MakeInParam("@scenehgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.scenehgnum.GetType().ToString()), entity.scenehgnum,50),  
                         dbFactory.MakeInParam("@experimentjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.experimentjcnum.GetType().ToString()), entity.experimentjcnum, 50), 
                         dbFactory.MakeInParam("@experimenthgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.experimenthgnum.GetType().ToString()), entity.experimenthgnum, 50), 
                         dbFactory.MakeInParam("@jbhsjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.jbhsjcnum.GetType().ToString()), entity.jbhsjcnum, 50),                               
                         dbFactory.MakeInParam("@jbhshgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.jbhshgnum.GetType().ToString()), entity.jbhshgnum, 50),  
                         dbFactory.MakeInParam("@alljcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.alljcnum.GetType().ToString()), entity.alljcnum, 50),  
                         dbFactory.MakeInParam("@allhgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.allhgnum.GetType().ToString()), entity.allhgnum, 50),                               
                         dbFactory.MakeInParam("@mmjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.mmjcnum.GetType().ToString()), entity.mmjcnum, 50),  
                         dbFactory.MakeInParam("@mmhgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.mmhgnum.GetType().ToString()), entity.mmhgnum,50),  
                        dbFactory.MakeInParam("@byjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.byjcnum.GetType().ToString()), entity.byjcnum,50),  
                       dbFactory.MakeInParam("@byhgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.byhgnum.GetType().ToString()), entity.byhgnum,50),  
                       dbFactory.MakeInParam("@amount",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.amount.GetType().ToString()), entity.amount,50),
 
                       };

                        string updatestr = @"update t_zkanalysisinfo set itemid='" + prams[1].Value + "',analysisnum='" + prams[4].Value
                            + "',UpdateDate='" + prams[2].Value + "',UpdateUserID='" + prams[3].Value
                            + "',scenejcnum='" + prams[5].Value + "',scenehgnum='" + prams[6].Value
                            + "',experimentjcnum='" + prams[7].Value + "',experimenthgnum='" + prams[8].Value
                            + "',jbhsjcnum='" + prams[9].Value + "',jbhshgnum='" + prams[10].Value

                            + "',alljcnum='" + prams[11].Value + "',allhgnum='" + prams[12].Value
                            + "',mmjcnum='" + prams[13].Value + "',mmhgnum='" + prams[14].Value
                            + "',byjcnum='" + prams[15].Value + "',byhgnum='" + prams[16].Value
                            + "',amount='" + prams[17].Value + "' where id='" + prams[0].Value + "'";
                        iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, updatestr, prams);

                    }
                    else
                    {
                        IDbDataParameter[] prams = {
                         dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                         dbFactory.MakeInParam("@MonitorItem",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.MonitorItem.GetType().ToString()), entity.MonitorItem, 50) ,              
                         dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.CreateDate.GetType().ToString()),  entity.CreateDate , 0) ,
                         dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50) ,              
                         dbFactory.MakeInParam("@analysisnum",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.analysisnum.GetType().ToString()),  entity.analysisnum , 50) ,
                         dbFactory.MakeInParam("@scenejcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.scenejcnum.GetType().ToString()), entity.scenejcnum,50),  
                         dbFactory.MakeInParam("@scenehgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.scenehgnum.GetType().ToString()), entity.scenehgnum,50),  
                         dbFactory.MakeInParam("@experimentjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.experimentjcnum.GetType().ToString()), entity.experimentjcnum, 50), 
                         dbFactory.MakeInParam("@experimenthgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.experimenthgnum.GetType().ToString()), entity.experimenthgnum, 50), 
                         dbFactory.MakeInParam("@jbhsjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.jbhsjcnum.GetType().ToString()), entity.jbhsjcnum, 50),                               
                         dbFactory.MakeInParam("@jbhshgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.jbhshgnum.GetType().ToString()), entity.jbhshgnum, 50),  
                         dbFactory.MakeInParam("@alljcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.alljcnum.GetType().ToString()), entity.alljcnum, 50),  
                         dbFactory.MakeInParam("@allhgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.allhgnum.GetType().ToString()), entity.allhgnum, 50),                               
                         dbFactory.MakeInParam("@mmjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.mmjcnum.GetType().ToString()), entity.mmjcnum, 50),  
                         dbFactory.MakeInParam("@mmhgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.mmhgnum.GetType().ToString()), entity.mmhgnum,50),  
                        dbFactory.MakeInParam("@byjcnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.byjcnum.GetType().ToString()), entity.byjcnum,50),  
                       dbFactory.MakeInParam("@byhgnum",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.byhgnum.GetType().ToString()), entity.byhgnum,50),  
                       dbFactory.MakeInParam("@amount",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.amount.GetType().ToString()), entity.amount,50),
 
                       };

                        string insertstr = @"insert into t_zkanalysisinfo( itemid,analysisnum,CreateDate,UserID"+
                           " ,scenejcnum,scenehgnum,experimentjcnum,experimenthgnum,jbhsjcnum,jbhshgnum,alljcnum,allhgnum,mmjcnum,mmhgnum,byjcnum,byhgnum,amount) "+
" values ('" + prams[1].Value + "','" + prams[4].Value
                            + "','" + prams[2].Value + "','" + prams[3].Value
                            + "','" + prams[5].Value + "','" + prams[6].Value
                            + "','" + prams[7].Value + "','" + prams[8].Value
                            + "','" + prams[9].Value + "','" + prams[10].Value
                            + "','" + prams[11].Value + "','" + prams[12].Value
                            + "','" + prams[13].Value + "','" + prams[14].Value
                            + "','" + prams[15].Value + "','" + prams[16].Value
                            + "','" + prams[17].Value + "')";
                        iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, insertstr, prams);
                    }   
                }
            }
            catch (Exception ex)
            {
                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
        /// <summary>
        /// 删除质控样信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            int iReturn = 0;

            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            try
            {

                        IDbDataParameter[] prams = {
                       };

                        string deletestr = @"delete from  t_zkanalysisinfo  where id='" +id + "'";
                        iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, deletestr, prams);
            }
            catch (Exception ex)
            {
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