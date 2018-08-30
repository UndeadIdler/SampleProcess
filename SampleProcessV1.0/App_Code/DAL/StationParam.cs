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
    /// StationParam 的摘要说明
    /// </summary>
    public class StationParam
    {
        public StationParam()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int add(Entity.stationP entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {
               
                IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@wrwtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.wrwtype.GetType().ToString()), entity.wrwtype, 50) ,              
                 dbFactory.MakeInParam("@qyid",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.qyid.GetType().ToString()),  entity.qyid , 50) ,
                 dbFactory.MakeInParam("@bz",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bz.GetType().ToString()), entity.bz, 50) ,              
                 dbFactory.MakeInParam("@item",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.item.GetType().ToString()), entity.item, 200) ,              
               
                 dbFactory.MakeInParam("@createdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createdate.GetType().ToString()), entity.createdate,0),  
                dbFactory.MakeInParam("@createuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createuser.GetType().ToString()), entity.createuser, 50),  
                 dbFactory.MakeOutReturnParam()
               };
               // string upstr = "update t_hyClassParam set flag=1,updatedate='" + entity.createdate + "',updateuser='" + entity.createuser + "' where wrwtype='" + entity.wrwtype + "' and hyid='"+entity.qyid+"' and flag=0";
               // iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Param_Add]", prams);
                if (iReturn < 0)
                    throw new Exception("保存失败！");
                entity.ID = int.Parse(prams[6].Value.ToString());
                foreach (Entity.Item item in entity.itemlist)
                {
                   
                    string instr = @"insert into t_hyItem(pid,itemid,fw) values('" + entity.ID + "','" + item.itemid + "','" + item.itemfw + "')";
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, instr, prams);
                    if (iReturn <= 0)
                    {
                        throw new Exception("配置保存失败！");
                    }
                }
                thelper.CommitTransaction(trans);
                iReturn = 1;
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
        public int update(Entity.stationP entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@wrwtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.wrwtype.GetType().ToString()), entity.wrwtype, 50) ,              
                 dbFactory.MakeInParam("@qyid",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.qyid.GetType().ToString()),  entity.qyid , 50) ,
                 dbFactory.MakeInParam("@bz",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bz.GetType().ToString()), entity.bz, 50) ,              
                 dbFactory.MakeInParam("@createdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createdate.GetType().ToString()), entity.createdate,0),  
                dbFactory.MakeInParam("@createuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createuser.GetType().ToString()), entity.createuser, 50),  
                 dbFactory.MakeOutReturnParam()
               };
                string upstr = "update t_hyClassParam set wrwtype='"+entity.wrwtype+"',hyid='"+entity.qyid+"',bz='"+entity.bz+"',updatedate='" + entity.createdate + "',updateuser='" + entity.createuser + "',itemlist='"+entity.item+"' where ID='"+entity.ID+"'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
                if (iReturn < 0)
                    throw new Exception("保存失败！");
                string delstr = "Delete from t_hyItem  where pid='" + entity.ID + "'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, delstr, prams);
                foreach (Entity.Item item in entity.itemlist)
                {

                    string instr = @"insert into t_hyItem(pid,itemid,fw) values('" + entity.ID + "','" + item.itemid + "','" + item.itemfw + "')";
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, instr, prams);
                    if (iReturn <= 0)
                    {
                        throw new Exception("配置保存失败！");
                    }
                }
                thelper.CommitTransaction(trans);
                iReturn = 1;
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

        public int addCompanyParam(Entity.stationP entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@wrwtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.wrwtype.GetType().ToString()), entity.wrwtype, 50) ,              
                 dbFactory.MakeInParam("@qyid",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.qyid.GetType().ToString()),  entity.qyid , 50) ,
                 dbFactory.MakeInParam("@bz",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bz.GetType().ToString()), entity.bz, 50) ,              
                 dbFactory.MakeInParam("@createdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createdate.GetType().ToString()), entity.createdate,0),  
                dbFactory.MakeInParam("@createuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createuser.GetType().ToString()), entity.createuser, 50),  
                dbFactory.MakeInParam("@itemtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.itemtype.GetType().ToString()), entity.itemtype, 50),  
               
                dbFactory.MakeOutReturnParam()
               };
                string upstr = "update t_CompabyBZ set flag=1,updatedate='" + entity.createdate + "',updateuser='" + entity.createuser + "' where wrwtype='" + entity.wrwtype + "' and qyid='" + entity.qyid + "' and itemtype='" + entity.itemtype + "' and flag=0";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_CompanyParam_Add]", prams);
                if (iReturn < 0)
                    throw new Exception("保存失败！");
               
                thelper.CommitTransaction(trans);
                iReturn = 1;
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
        public int updateCompanyParam(Entity.stationP entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@wrwtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.wrwtype.GetType().ToString()), entity.wrwtype, 50) ,              
                 dbFactory.MakeInParam("@qyid",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.qyid.GetType().ToString()),  entity.qyid , 50) ,
                 dbFactory.MakeInParam("@bz",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bz.GetType().ToString()), entity.bz, 50) ,              
                 dbFactory.MakeInParam("@createdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createdate.GetType().ToString()), entity.createdate,0),  
                dbFactory.MakeInParam("@createuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createuser.GetType().ToString()), entity.createuser, 50),  
                dbFactory.MakeInParam("@itemtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.itemtype.GetType().ToString()), entity.itemtype, 50),  
               
                dbFactory.MakeOutReturnParam()
               };
                string upstr = "update t_CompabyBZ set itemtype='"+entity.itemtype+"',wrwtype='" + entity.wrwtype + "',qyid='" + entity.qyid + "',bzid='" + entity.bz + "',updatedate='" + entity.createdate + "',updateuser='" + entity.createuser + "' where ID='" + entity.ID + "'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
               
                thelper.CommitTransaction(trans);
                iReturn = 1;
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


        public int addOUTNOParam(Entity.stationP entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@wrwtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.wrwtype.GetType().ToString()), entity.wrwtype, 50) ,              
                 dbFactory.MakeInParam("@OUTNO",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.bz.GetType().ToString()),  entity.bz , 50) ,
                   dbFactory.MakeInParam("@createdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createdate.GetType().ToString()), entity.createdate,0),  
                dbFactory.MakeInParam("@createuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createuser.GetType().ToString()), entity.createuser, 50),  
                 dbFactory.MakeOutReturnParam()
               };
                //string upstr = "update t_CompabyBZ set flag=1,updatedate='" + entity.createdate + "',updateuser='" + entity.createuser + "' where wrwtype='" + entity.wrwtype + "' and qyid='" + entity.qyid + "' and flag=0";
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_OUTNO_Add]", prams);
                if (iReturn < 0)
                    throw new Exception("保存失败！");
                //entity.ID = int.Parse(prams[4].Value.ToString());
                //foreach (Entity.Item item in entity.itemlist)
                //{

                //    string instr = @"insert into t_OUTNOItem(outid,itemid,fw) values('" + entity.ID + "','" + item.itemid + "','" + item.itemfw + "')";
                //    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, instr, prams);
                //    if (iReturn <= 0)
                //    {
                //        throw new Exception("配置保存失败！");
                //    }
                //}
                thelper.CommitTransaction(trans);
                iReturn = 1;
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
        public int updateOUTNOParam(Entity.stationP entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                                                dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 50) ,              
               
                  dbFactory.MakeInParam("@wrwtype",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.wrwtype.GetType().ToString()), entity.wrwtype, 50) ,              
                 dbFactory.MakeInParam("@OUTNO",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.bz.GetType().ToString()),  entity.bz , 50) ,
                   dbFactory.MakeInParam("@createdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createdate.GetType().ToString()), entity.createdate,0),  
                dbFactory.MakeInParam("@createuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createuser.GetType().ToString()), entity.createuser, 50) 
               };
                string upstr = "update t_OUTNO set type='" + entity.wrwtype + "',OUTNO='" + entity.bz + "',updatedate='" + entity.createdate + "',updateuser='" + entity.createuser + "' where ID='" + entity.ID + "'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
                //if (iReturn < 0)
                //    throw new Exception("保存失败！");
                //string delstr = "Delete from t_OUTNOItem  where outid='" + entity.ID + "'";
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, delstr, prams);
                //foreach (Entity.Item item in entity.itemlist)
                //{

                //    string instr = @"insert into t_OUTNOItem(outid,itemid,fw) values('" + entity.ID + "','" + item.itemid + "','" + item.itemfw + "')";
                //    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, instr, prams);
                //    if (iReturn <= 0)
                //    {
                //        throw new Exception("配置保存失败！");
                //    }
                //}
                if (iReturn < 0)
                        throw new Exception("保存失败！");
                thelper.CommitTransaction(trans);
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

        public int AddItemParam(Entity.stationP entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                  dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.ID.GetType().ToString()),  entity.ID , 50) ,
                 dbFactory.MakeInParam("@OUTID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.wrwtype.GetType().ToString()),  entity.wrwtype , 50) ,
                   dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bz.GetType().ToString()), entity.bz,50),  
                dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.createuser.GetType().ToString()), entity.createuser, 50),  
                 dbFactory.MakeOutReturnParam()
               };

                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_OUTNOParam_Add]", prams);

                int recordid = entity.ID;
                if (entity.ID == 0)
                {
                   recordid= int.Parse(prams[4].Value.ToString());
                    entity.ID = recordid;
                }
               
                string delstr = "Delete from t_OUTNOItem  where paramid='" + entity.ID + "'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, delstr, prams);
                foreach (Entity.Item item in entity.itemlist)
                {

                    string instr = @"insert into t_OUTNOItem(paramid,outid,itemid,fw) values('" +entity.ID+"','"+ entity.wrwtype + "','" + item.itemid + "','" + item.itemfw + "')";
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, instr, prams);
                    if (iReturn <= 0)
                    {
                        throw new Exception("配置保存失败！");
                    }
                }
                if (iReturn < 0)
                    throw new Exception("保存失败！");
                thelper.CommitTransaction(trans);
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