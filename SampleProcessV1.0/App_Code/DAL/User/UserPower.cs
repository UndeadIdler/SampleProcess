using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccess;
using ESBasic.Logger;

namespace DAl.User
{
    public class UserPower
    {
        public Entity.UserPower GetUserPower(string cUserName, int iMenuID)
        {
            Entity.UserPower entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cUserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cUserName.GetType().ToString()),cUserName, 50),
                dbFactory.MakeInParam("@iMenuID",  DBTypeConverter.ConvertCsTypeToOriginDBType(iMenuID.GetType().ToString()),iMenuID, 0)
                
								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_UserPower_Get", prams);

                while (dataReader.Read())
                {
                    entity = new Entity.UserPower();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CUserName = dataReader["CUserName"].ToString();
                    entity.IMenuId = int.Parse(dataReader["IMenuId"].ToString());
                    entity.BLuRu = bool.Parse(dataReader["BLuRu"].ToString());
                    entity.BUp = bool.Parse(dataReader["BUp"].ToString());
                    entity.BCheck = bool.Parse(dataReader["BCheck"].ToString());
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
                    entity.CUpdateUser = dataReader["CUpdateUser"].ToString();
                }
            }
            catch (Exception ex)
            {
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {
                db.Conn.Close();
            }

            return entity;
        }

        public int AddUserPower(string cUserName, int iMenuId, bool bLuRu, bool bCheck, DateTime dCreateDate, string cCreateUser,
            DateTime dUpdateDate, string cUpdateUser)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cUserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cUserName.GetType().ToString()),cUserName,50),
                dbFactory.MakeInParam("@iMenuId",         DBTypeConverter.ConvertCsTypeToOriginDBType(iMenuId.GetType().ToString()), iMenuId, 0),
                dbFactory.MakeInParam("@bLuRu",         DBTypeConverter.ConvertCsTypeToOriginDBType(bLuRu.GetType().ToString()), bLuRu, 1),
                dbFactory.MakeInParam("@bCheck",         DBTypeConverter.ConvertCsTypeToOriginDBType(bCheck.GetType().ToString()), bCheck, 1),
                dbFactory.MakeInParam("@dCreateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(dCreateDate.GetType().ToString()), dCreateDate, 0),
                dbFactory.MakeInParam("@cCreateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(cCreateUser.GetType().ToString()), cCreateUser, 50),
                dbFactory.MakeInParam("@dUpdateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(dUpdateDate.GetType().ToString()), dUpdateDate, 0),
                dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(cUpdateUser.GetType().ToString()), cUpdateUser, 50)
								   };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "proc_UserPower_Add", prams);

            }
            catch (Exception ex)
            {
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {

                db.Conn.Close();

            }
            return iReturn;
        }

        public int UpdateUserPower(string cUserName, int iMenuId, bool bLuRu, bool bCheck,bool bUp,
           DateTime dUpdateDate, string cUpdateUser)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cUserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cUserName.GetType().ToString()),cUserName,50),
                dbFactory.MakeInParam("@iMenuId",         DBTypeConverter.ConvertCsTypeToOriginDBType(iMenuId.GetType().ToString()), iMenuId, 0),
                dbFactory.MakeInParam("@bLuRu",         DBTypeConverter.ConvertCsTypeToOriginDBType(bLuRu.GetType().ToString()), bLuRu, 1),
                dbFactory.MakeInParam("@bCheck",         DBTypeConverter.ConvertCsTypeToOriginDBType(bCheck.GetType().ToString()), bCheck, 1),
                 dbFactory.MakeInParam("@bUp",         DBTypeConverter.ConvertCsTypeToOriginDBType(bUp.GetType().ToString()), bUp, 1),
                dbFactory.MakeInParam("@dUpdateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(dUpdateDate.GetType().ToString()), dUpdateDate, 0),
                dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(cUpdateUser.GetType().ToString()), cUpdateUser, 50)
								   };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "proc_UserPower_Update", prams);

            }
            catch (Exception ex)
            {
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {

                db.Conn.Close();

            }
            return iReturn;
        }


        public int DeleteUserPower(int iMenuId, string cUserName)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@iMenuId",         DBTypeConverter.ConvertCsTypeToOriginDBType(iMenuId.GetType().ToString()), iMenuId, 0),
                dbFactory.MakeInParam("@cUserName",         DBTypeConverter.ConvertCsTypeToOriginDBType(cUserName.GetType().ToString()), cUserName, 50)
								   };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "proc_UserPower_Delete", prams);

            }
            catch (Exception ex)
            {
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {

                db.Conn.Close();

            }
            return iReturn;
        }
    }
}
