using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccess;
using ESBasic.Logger;



namespace CommonLib.DAl.User
{
    public class Users
    {
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public int AddUsers(Entity.User.Users users)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(users.CName.GetType().ToString()),users.CName, 50),
                dbFactory.MakeInParam("@cPW",  DBTypeConverter.ConvertCsTypeToOriginDBType(users.CPW.GetType().ToString()), users.CPW,50),
                dbFactory.MakeInParam("@cMemo",   DBTypeConverter.ConvertCsTypeToOriginDBType(users.CMemo.GetType().ToString()), users.CMemo, 500),
                dbFactory.MakeInParam("@bStop",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.BStop.GetType().ToString()), users.BStop, 1),
                dbFactory.MakeInParam("@bAdmin",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.BAdmin.GetType().ToString()), users.BAdmin, 1),
                dbFactory.MakeInParam("@iRoleType",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.IRoleType.GetType().ToString()), users.IRoleType, 0),
                dbFactory.MakeInParam("@cRole",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.CRole.GetType().ToString()), users.CRole, 4000),
               
                dbFactory.MakeInParam("@cPhone",  DBTypeConverter.ConvertCsTypeToOriginDBType(users.CPhone.GetType().ToString()), users.CPhone,50),
                dbFactory.MakeInParam("@dActivityLastTime",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.DActivityLastTime.GetType().ToString()), users.DActivityLastTime, 0),
                dbFactory.MakeInParam("@cActivityIP",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.CActivityIP.GetType().ToString()), users.CActivityIP, 50),
                dbFactory.MakeInParam("@dCreateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.DCreateDate.GetType().ToString()), users.DCreateDate, 0),
                dbFactory.MakeInParam("@cCreateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.CCreateUser.GetType().ToString()), users.CCreateUser, 50),
                dbFactory.MakeInParam("@dUpdateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.DUpdateDate.GetType().ToString()), users.DUpdateDate, 0),
                dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(users.CUpdateUser.GetType().ToString()), users.CUpdateUser, 50),
                dbFactory.MakeOutReturnParam()
								   };
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_User_Add", prams);
                iReturn = int.Parse(prams[14].Value.ToString());
                string[] areaList = users.CAreaCode.Split(',');
                foreach (string str in areaList)
                {
                    string strinsert = "insert into t_R_UserArea(UserID,AreaID,cGradeBaseType) values('" + iReturn + "','"+str+"','01')";
                    db.ExecuteNonQueryTrans(trans, CommandType.Text, strinsert, prams);
                }
                thelper.CommitTransaction(trans);
                
            }
            catch (Exception ex)
            {
                iReturn = 0;
                thelper.RollTransaction(trans);
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }

        /// <summary>
        /// 删除用户-ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>int</returns>
        public int DeleteUsers(int ID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType(ID.GetType().ToString()),ID, 0)
                                   };
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_User_Delete", prams);
                db.ExecuteNonQueryTrans(trans, CommandType.Text, "delete from t_R_UserArea where UserID='" + ID + "'",prams);
                thelper.CommitTransaction(trans);


            }
            catch (Exception ex)
            {
                iReturn = 0;
                thelper.RollTransaction(trans);
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }

        /// <summary>
        /// 删除用户-用户名
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns>int</returns>
        public int DeleteUsersName(string UserName)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();

            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@UserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(UserName.GetType().ToString()),UserName, 50)
                                   };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "proc_User_DeleteName", prams);

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
        /// <summary>
        /// 根据CODE，角色id 获取单位名称

        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        protected string GetName(string iRoleType)
        {
            string str = "";
            switch (iRoleType)
            {
                case "0": str = "环保局"; break;
                case "1":
                    str = "企业用户";
                    break;
                case "2": str = "环评用户";
                    break;
            }
            return str;
        }
        private string ParseRole(string cRole)
        {
            string str = "";
            string[] arr = cRole.Split(',');
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] == "0")
                {
                    str = str + "企业档案,";
                }
                else if (arr[i] == "1")
                {
                    str = str + "废物管理,";
                }
            }
            return str;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Users</returns>
        public int CheckUsers(String cName, int userid)
        {
            int i = 0;
            Entity.User.Users entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();

            try
            {
                IDbDataParameter[] prams = {
               				   };

                string strSql = "SELECT * FROM [Users] where cName='" + cName + "'"; ;

                if (userid != 0)
                {
                    strSql = "SELECT * FROM [Users] where cName='" + cName + "' and ID!=" + userid + ""; ;
                }
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);

                while (dataReader.Read())
                {
                    i = 1;
                }

            }
            catch (Exception ex)
            {
                i = 0;
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {
                db.Conn.Close();
            }
            return i;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Users</returns>
        public Entity.User.Users GetUsers(String cName)
        {
            Entity.User.Users entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();

            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50)

								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_Get", prams);

                while (dataReader.Read())
                {
                    entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    entity.BStopName = dataReader["BStopName"].ToString();

                    entity.CRole = GetName(dataReader["IRoleType"].ToString());

                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());

                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    entity.BAdminName = dataReader["BAdminName"].ToString();

                    entity.CDepCode = dataReader["CDepCode"].ToString();
                     entity.CPhone = dataReader["CPhone"].ToString();

                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Users</returns>
        public Entity.User.Users GetUsersByID(int UserID)
        {
            Entity.User.Users entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();

            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType(UserID.GetType().ToString()),UserID,0)
								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_GetByID", prams);

                while (dataReader.Read())
                {
                    entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.CPW = dataReader["cPW"].ToString();
                    //entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    //entity.BStopName = dataReader["BStopName"].ToString();

                    entity.CRole = GetName(dataReader["IRoleType"].ToString());

                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());

                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    //entity.BAdminName = dataReader["BAdminName"].ToString();
                    entity.ILevel = DataHelper.ParseToInt(dataReader["ILevel"].ToString());
                    //entity.CDepCode = dataReader["CDepCode"].ToString();
                    entity.CPhone = dataReader["CPhone"].ToString();

                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public int UpdatePw(string cName, string oldPW, string newPW, DateTime dUpdateDate, string cUpdateUser)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();

            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                dbFactory.MakeInParam("@oldPW",         DBTypeConverter.ConvertCsTypeToOriginDBType(oldPW.GetType().ToString()), oldPW, 50),
                dbFactory.MakeInParam("@newPW",         DBTypeConverter.ConvertCsTypeToOriginDBType(newPW.GetType().ToString()), newPW, 50),
                dbFactory.MakeInParam("@dUpdateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(dUpdateDate.GetType().ToString()), dUpdateDate, 0),
                dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(cUpdateUser.GetType().ToString()), cUpdateUser, 50)
                                   };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "proc_User_UpdatePw", prams);

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
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public int UpdateUsers(string cName, string cDepCode, string cMemo, bool bStop, bool bAdmin, int IRoleType, string cPhone, string cRole, DateTime dUpdateDate, string cUpdateUser,int ID)
        {

            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
                                                dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType(ID.GetType().ToString()),ID, 0),
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                dbFactory.MakeInParam("@cDepCode",         DBTypeConverter.ConvertCsTypeToOriginDBType(cDepCode.GetType().ToString()), cDepCode,50),
                dbFactory.MakeInParam("@cMemo",  DBTypeConverter.ConvertCsTypeToOriginDBType(cMemo.GetType().ToString()),cMemo, 50),
                dbFactory.MakeInParam("@bStop",         DBTypeConverter.ConvertCsTypeToOriginDBType(bStop.GetType().ToString()), bStop,1),
                dbFactory.MakeInParam("@bAdmin",         DBTypeConverter.ConvertCsTypeToOriginDBType(bAdmin.GetType().ToString()), bAdmin,1),
                dbFactory.MakeInParam("@IRoleType",         DBTypeConverter.ConvertCsTypeToOriginDBType(IRoleType.GetType().ToString()), IRoleType, 0),
                dbFactory.MakeInParam("@cPhone",         DBTypeConverter.ConvertCsTypeToOriginDBType(cPhone.GetType().ToString()), cPhone,50),
                dbFactory.MakeInParam("@cRole",         DBTypeConverter.ConvertCsTypeToOriginDBType(cRole.GetType().ToString()), cRole,4000),
                dbFactory.MakeInParam("@dUpdateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(dUpdateDate.GetType().ToString()), dUpdateDate, 0),
                dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(cUpdateUser.GetType().ToString()), cUpdateUser, 50)
                                   };
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_User_Update", prams);
                db.ExecuteNonQueryTrans(trans, CommandType.Text, "delete from t_R_UserArea where UserID='" + ID + "'", prams);
                string [] areastr=cDepCode.Split(',');
                for (int i = 0; i < areastr.Length; i++)
                {
                    string strinsert = "insert into t_R_UserArea(UserID,AreaID,cGradeBaseType) values('" + ID + "','" + areastr[i] + "','01')";
                    db.ExecuteNonQueryTrans(trans, CommandType.Text, strinsert, prams);

                }
                    thelper.CommitTransaction(trans);
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }


        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="cName"></param>
        /// <returns></returns>
        public bool IsExistsUsers(string cName)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50)
								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_IsExists", prams);

                if (dataReader.Read())
                {
                    iReturn = 1;
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
            return iReturn == 1 ? true : false;
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public List<Entity.User.Users> QueryUsers(string cName, int bStop, string cDepCode, int iRole,int level)
        {
            List<Entity.User.Users> list = new List<Entity.User.Users>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                dbFactory.MakeInParam("@cDepCode",  DBTypeConverter.ConvertCsTypeToOriginDBType(cDepCode.GetType().ToString()),cDepCode,50),
                 dbFactory.MakeInParam("@bStop",  DBTypeConverter.ConvertCsTypeToOriginDBType(bStop.GetType().ToString()),bStop, 50),
                dbFactory.MakeInParam("@level",  DBTypeConverter.ConvertCsTypeToOriginDBType(level.GetType().ToString()),level, 0),
                 dbFactory.MakeInParam("@iRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(iRole.GetType().ToString()),iRole, 0)
              	 };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_QueryCondition", prams);

                while (dataReader.Read())
                {
                    Entity.User.Users entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    entity.BStopName = dataReader["BStopName"].ToString();

                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    entity.BAdminName = dataReader["BAdminName"].ToString();
                    entity.CRole = GetName(dataReader["IRoleType"].ToString());
                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());

                    entity.CDepCode = dataReader["CDepCode"].ToString();
                    //entity.CDepCodeName = GetName(entity.CDepCode, entity.IRoleType);

                    entity.CPhone = dataReader["CPhone"].ToString();
                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
                    list.Add(entity);
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

            return list;
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public DataTable QueryUsersDT(int userLevel,string cName, int bStop, string cDepCode, int iRole, int level)
        {
            DataTable dt = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                //                                dbFactory.MakeInParam("@ILevel",  DBTypeConverter.ConvertCsTypeToOriginDBType(userLevel.GetType().ToString()),userLevel, 0),
                //dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                //dbFactory.MakeInParam("@cDepCode",  DBTypeConverter.ConvertCsTypeToOriginDBType(cDepCode.GetType().ToString()),cDepCode,50),
                // dbFactory.MakeInParam("@bStop",  DBTypeConverter.ConvertCsTypeToOriginDBType(bStop.GetType().ToString()),bStop, 50),
                //dbFactory.MakeInParam("@level",  DBTypeConverter.ConvertCsTypeToOriginDBType(level.GetType().ToString()),level, 0),
                // dbFactory.MakeInParam("@iRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(iRole.GetType().ToString()),iRole, 0)
              	 };
                string constr="";
                if (cName != "")
                    constr += "and cName like '%"+cName+"%'";
                if (cDepCode != "")
                    constr += " and ID in (select UserID from t_R_UserArea where AreaID in (" + cDepCode + ") and cGradeBaseType='01')";
                if (level != 0)
                    constr += " and ILevel='" + level + "'";
                if (iRole != 0)
                    constr += " and RoleID=" + iRole + "";
                string str = " select * from  vUsers where   ILevel>" + userLevel+constr ;

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, str, prams);
                dt = new DataTable();
                dt.Load(dataReader);
              
              
            }
            catch (Exception ex)
            {
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {
                db.Conn.Close();
            }

            return dt;
        }
        /// <summary>
        /// 查询环评用户
        /// </summary>
        /// <returns></returns>
        public List<Entity.User.Users> QueryUsersAllHP(string cName, int bStop, string cDepCode, int iRole)
        {
            List<Entity.User.Users> list = new List<Entity.User.Users>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                dbFactory.MakeInParam("@cDepCode",  DBTypeConverter.ConvertCsTypeToOriginDBType(cDepCode.GetType().ToString()),cDepCode,50),
                 dbFactory.MakeInParam("@bStop",  DBTypeConverter.ConvertCsTypeToOriginDBType(bStop.GetType().ToString()),bStop, 50),
                dbFactory.MakeInParam("@iRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(iRole.GetType().ToString()),iRole, 0)
              	 };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "[proc_UserHP_QueryCondition]", prams);

                while (dataReader.Read())
                {
                    Entity.User.Users entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    entity.BStopName = dataReader["BStopName"].ToString();

                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    entity.BAdminName = dataReader["BAdminName"].ToString();
                    entity.CRole = GetName(dataReader["IRoleType"].ToString());
                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());

                    entity.CDepCode = dataReader["CDepCode"].ToString();
                    entity.CDepCodeName = dataReader["CDepCodeName"].ToString();

                    entity.CPhone = dataReader["CPhone"].ToString();
                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
                    list.Add(entity);
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

            return list;
        }
        /// <summary>
        /// 查询企业用户用户
        /// </summary>
        /// <returns></returns>
        public List<Entity.User.Users> QueryUsersAll(string cName, int bStop, string cDepCode, int iRole)
        {
            List<Entity.User.Users> list = new List<Entity.User.Users>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                dbFactory.MakeInParam("@cDepCode",  DBTypeConverter.ConvertCsTypeToOriginDBType(cDepCode.GetType().ToString()),cDepCode,50),
                 dbFactory.MakeInParam("@bStop",  DBTypeConverter.ConvertCsTypeToOriginDBType(bStop.GetType().ToString()),bStop, 50),
                dbFactory.MakeInParam("@iRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(iRole.GetType().ToString()),iRole, 0)
              	 };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_UserAll_QueryCondition", prams);

                while (dataReader.Read())
                {
                    Entity.User.Users entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    entity.BStopName = dataReader["BStopName"].ToString();

                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    entity.BAdminName = dataReader["BAdminName"].ToString();
                    entity.CRole = GetName(dataReader["IRoleType"].ToString());
                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());

                    entity.CDepCode = dataReader["CDepCode"].ToString();
                    entity.CDepCodeName = dataReader["cFullName"].ToString();

                    entity.CPhone = dataReader["CPhone"].ToString();
                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
                    list.Add(entity);
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

            return list;
        }


        public List<Entity.User.Users> QueryUsersGeneral(string cName, string cDepCode, int bStop, int iRole)
        {
            List<Entity.User.Users> list = new List<Entity.User.Users>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                dbFactory.MakeInParam("@cDepCode",  DBTypeConverter.ConvertCsTypeToOriginDBType(cDepCode.GetType().ToString()),cDepCode, 50),
                dbFactory.MakeInParam("@bStop",  DBTypeConverter.ConvertCsTypeToOriginDBType(bStop.GetType().ToString()),bStop, 0),
                dbFactory.MakeInParam("@iRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(iRole.GetType().ToString()),iRole, 0)
								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_QueryGeneral", prams);

                while (dataReader.Read())
                {
                    Entity.User.Users entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    entity.BStopName = dataReader["BStopName"].ToString();

                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    entity.BAdminName = dataReader["BAdminName"].ToString();

                    entity.CRole = ParseRole(dataReader["CRole"].ToString());
                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());
                    entity.CDepCode = dataReader["CDepCode"].ToString();
                    entity.CDepCodeName = dataReader["CDepCodeName"].ToString();
                    entity.CPhone = dataReader["CPhone"].ToString();
                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
                    list.Add(entity);
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

            return list;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<Entity.User.Users> GetAllUsers(int iRole)
        {
            List<Entity.User.Users> list = new List<Entity.User.Users>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@iRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(iRole.GetType().ToString()),iRole, 0)
								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_GetAll", prams);

                while (dataReader.Read())
                {
                    Entity.User.Users entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    entity.BStopName = dataReader["BStopName"].ToString();

                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    entity.BAdminName = dataReader["BAdminName"].ToString();

                    entity.CRole = ParseRole(dataReader["CRole"].ToString());
                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());

                    entity.CDepCode = dataReader["CDepCode"].ToString();
                    // entity.CDepCodeName = dataReader["CDepCodeName"].ToString();

                    entity.CPhone = dataReader["CPhone"].ToString();

                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
                    list.Add(entity);
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

            return list;
        }

        /// <summary>
        /// 查询用户登陆
        /// </summary>
        /// <param name="cName"></param>
        /// <returns></returns>
        public Entity.User.Users Login(string CName, string CPW, bool BStop)
        {
            Entity.User.Users entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(CName.GetType().ToString()),CName, 50),
                dbFactory.MakeInParam("@cPW",  DBTypeConverter.ConvertCsTypeToOriginDBType(CPW.GetType().ToString()),CPW, 50),
                dbFactory.MakeInParam("@bStop",  DBTypeConverter.ConvertCsTypeToOriginDBType(BStop.GetType().ToString()),BStop, 1)
								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_Login", prams);

                if (dataReader.Read())
                {
                    entity = new Entity.User.Users();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.CMemo = dataReader["cMemo"].ToString();
                    entity.ILevel = DataHelper.ParseToInt(dataReader["ILevel"].ToString());
                    entity.Guid = System.Guid.NewGuid().ToString();
                    entity.BStop = bool.Parse(dataReader["bStop"].ToString());
                    entity.BAdmin = bool.Parse(dataReader["BAdmin"].ToString());
                    entity.CRole = dataReader["CRole"].ToString();
                    entity.IRoleType = DataHelper.ParseToInt(dataReader["IRoleType"].ToString());
                    entity.CPhone = dataReader["CPhone"].ToString();
                    entity.CDepCode = dataReader["CDepCode"].ToString();
                    entity.DActivityLastTime = DateTime.Parse(dataReader["dActivityLastTime"].ToString());
                    entity.CActivityIP = dataReader["cActivityIP"].ToString();
                    entity.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    entity.CCreateUser = dataReader["cCreateUser"].ToString();
                    entity.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());

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

        /// <summary>
        /// 刷新用户登录信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public int RefreshUsers(string cName, DateTime dActivityLastTime, string cActivityIP, DateTime dUpdateDate, string cUpdateUser)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                //@cActivityIP,@dActivityLastTime,@dUpdateDate,@cUpdateUser,@cName
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
                dbFactory.MakeInParam("@dActivityLastTime",         DBTypeConverter.ConvertCsTypeToOriginDBType(dActivityLastTime.GetType().ToString()), dActivityLastTime, 0),
                dbFactory.MakeInParam("@cActivityIP",         DBTypeConverter.ConvertCsTypeToOriginDBType(cActivityIP.GetType().ToString()),cActivityIP, 50),
                dbFactory.MakeInParam("@dUpdateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(dUpdateDate.GetType().ToString()), dUpdateDate, 0),
                dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(cUpdateUser.GetType().ToString()), cUpdateUser, 50)
                                   };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "proc_User_Refresh", prams);

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

        /// <summary>
        /// 停用或启用用户
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <param name="BStop">是否停用</param>
        /// <returns></returns>
        public int StopUsers(int ID, bool bStop)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType(ID.GetType().ToString()),ID, 0),
                dbFactory.MakeInParam("@bStop",         DBTypeConverter.ConvertCsTypeToOriginDBType(bStop.GetType().ToString()), bStop, 1),
                dbFactory.MakeOutReturnParam()                  
                                           };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "proc_User_Stop", prams);
                iReturn = int.Parse(prams[2].Value.ToString());
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

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public List<Entity.User.UserRole> GetAllUserRole()
        {
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();

            List<Entity.User.UserRole> list = null;
            Entity.User.UserRole entity = null;

            try
            {
                IDbDataParameter[] prams = { };
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_User_GetAllRoles", prams);
                list = new List<Entity.User.UserRole>();
                while (dataReader.Read())
                {
                    entity = new Entity.User.UserRole();
                    entity.ID = int.Parse(dataReader["ID"].ToString());
                    entity.CName = dataReader["cName"].ToString();
                    entity.IRole = int.Parse(dataReader["iRole"].ToString());
                    entity.CImage = dataReader["cImage"].ToString();
                    list.Add(entity);
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
            return list;
        }
    }
}
