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
  public  class UserRole
    {
      public List<Entity.User.UserRole> RoleList(int ILevel)
      {
          Entity.User.UserRole entity = null;
          List<Entity.User.UserRole> list = new List<Entity.User.UserRole>();
          DBOperatorBase db = new DataBase();
          IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
          try
          {
              string constr = "";

              IDbDataParameter[] prams = {
                                   };
              string strSql = "select * from t_R_Role inner join t_R_Level on t_R_Level.id=t_R_Role.LevelID where  LevelID>'"+ILevel+"' ";

              IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);
              while (dataReader.Read())
              {
                  entity = new Entity.User.UserRole();
                  entity.ID = int.Parse(dataReader["RoleID"].ToString());
                  entity.CName = dataReader["RoleName"].ToString();
                  entity.ILevel = int.Parse(dataReader["LevelID"].ToString());
                  entity.ReadRight = int.Parse(dataReader["ReadRight"].ToString());
                  entity.WriteRight = int.Parse(dataReader["WriteRight"].ToString());
                  entity.ImportRight = int.Parse(dataReader["ImportRight"].ToString());
                  entity.ExportRight = int.Parse(dataReader["ExportRight"].ToString());
                  entity.UpLoadRight = int.Parse(dataReader["UpLoadRight"].ToString());
                  entity.CheckRight = int.Parse(dataReader["CheckRight"].ToString());
                  if (dataReader["bAdmin"].ToString() != "")
                      entity.Admin = int.Parse(dataReader["bAdmin"].ToString());
                  else
                      entity.Admin = 0;
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
        public DataTable ListDt(int searchlevel,string role,int level)
        {
            DataTable dt =new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string constr="";
                if(searchlevel!=0)
                 constr+=" and t_R_Level.id="+searchlevel+"";
                if(role!="")
                {
                    constr=" and RoleName like '%"+role+"%'";
                }
                IDbDataParameter[] prams = {
								   };
                string strSql = "select * from t_R_Role inner join t_R_Level on t_R_Role.LevelID=t_R_Level.id where t_R_Level.id>" + level + "" + constr;

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);
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
        public int AddRole(Entity.User.UserRole role)
        {
           // string sqlstr = "insert into t_R_Role(RoleName,LevelID,ReadRight,WriteRight,ImportRight,ExportRight,UpLoadRight,CheckRight)values('" + role.CName + "'," + role.ILevel + ",'" + role.ReadRight + "','" + role.WriteRight + "','" + role.ImportRight + "','" + role.ExportRight + "','" + role.UpLoadRight + "','" + role.CheckRight + "')";
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
                  dbFactory.MakeInParam("@RoleName",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.CName.GetType().ToString()),role.CName, 50),
                dbFactory.MakeInParam("@LevelID",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.ILevel.GetType().ToString()),role.ILevel, 0),
                 dbFactory.MakeInParam("@ReadRight",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.ReadRight.GetType().ToString()),role.ReadRight, 0),
                  dbFactory.MakeInParam("@WriteRight",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.WriteRight.GetType().ToString()),role.WriteRight, 0),
                   dbFactory.MakeInParam("@ImportRight",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.ImportRight.GetType().ToString()),role.ImportRight, 0),
                    dbFactory.MakeInParam("@ExportRight",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.ExportRight.GetType().ToString()),role.ExportRight, 0),
                     dbFactory.MakeInParam("@UpLoadRight",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.UpLoadRight.GetType().ToString()),role.UpLoadRight, 0), 
                     dbFactory.MakeInParam("@CheckRight",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.CheckRight.GetType().ToString()),role.CheckRight, 0),
               
                     dbFactory.MakeInParam("@bAdmin",  DBTypeConverter.ConvertCsTypeToOriginDBType(role.Admin.GetType().ToString()),role.Admin, 0),
        dbFactory.MakeOutReturnParam()
		                                    };
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AddRole", prams);
               int  roleid = int.Parse(prams[9].Value.ToString());
                DAl.Menu.Menu menu = new Menu.Menu();
             List<Entity.Menu> MenuList= menu.GetMenusByIsShow(1);
                foreach (Entity.Menu entity in MenuList)
                {
                    string str = @"insert into t_R_RoleMenu(MenuID,RoleID,checked,createuser,createdate)values('" + entity.ID + "','" + roleid + "','0','" + role.CreateUser + "','" + role.CreateDate + "')";
                     db.ExecuteNonQueryTrans(trans, CommandType.Text, str, prams);
                }
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
                iReturn = 0;
               
            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }

        public int UpdateRole(Entity.User.UserRole role)
        {
            string sqlstr = @"update t_R_Role set RoleName='" + role.CName + "'"+
                ",LevelID=" + role.ILevel+ 
                ",ReadRight='" + role.ReadRight + "'"+
                ",WriteRight='" + role.WriteRight + "'"+
                ",ImportRight='" + role.ImportRight + "'"+
                ",ExportRight='" + role.ExportRight + "'"+
                ",UpLoadRight='" + role.UpLoadRight + "'"+
                ",CheckRight='" + role.CheckRight + "'"+
                 ",bAdmin='" + role.Admin + "'" +
                
                 " where RoleID='"+role.ID+"'";
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
		                                    };
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.Text, sqlstr, prams);
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
        public int DeleteRole(int RoleID)
        {
            string sqlstr = @"Delete from  t_R_Role where RoleID='" + RoleID + "'";
            string sqlstr2 = @"Delete from  t_R_RoleMenu where roleid='" + RoleID + "'";
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
		                                    };
                db.ExecuteNonQueryTrans(trans, CommandType.Text, sqlstr, prams);
                db.ExecuteNonQueryTrans(trans, CommandType.Text, sqlstr2, prams);
                thelper.CommitTransaction(trans);
                iReturn = 1;
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
        public List<Entity.User.UserRole> ListUserRole(int searchlevel, string role, int level)
        {
            Entity.User.UserRole  entity= null;
            List<Entity.User.UserRole> list = new List<Entity.User.UserRole>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string constr = "";
                if (searchlevel != 0)
                    constr += " and t_R_Level.id=" + searchlevel + "";
                if (role != "")
                {
                    constr = " and RoleName like '%" + role + "%'";
                }
                IDbDataParameter[] prams = {
								   };
                string strSql = "select * from t_R_Role inner join t_R_Level on t_R_Role.LevelID=t_R_Level.id where t_R_Level.id>=" + level + "" + constr;

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);
                while (dataReader.Read())
                {
                    entity = new Entity.User.UserRole();
                    entity.ID= int.Parse(dataReader["RoleID"].ToString());
                    entity.CName = dataReader["RoleName"].ToString();
                    entity.ILevel = int.Parse(dataReader["ILevel"].ToString());
                    entity.ReadRight = int.Parse(dataReader["ReadRight"].ToString());
                    entity.WriteRight = int.Parse(dataReader["WriteRight"].ToString());
                    entity.ImportRight = int.Parse(dataReader["ImportRight"].ToString());
                    entity.ExportRight = int.Parse(dataReader["ExportRight"].ToString());
                    entity.UpLoadRight = int.Parse(dataReader["UpLoadRight"].ToString());
                    entity.CheckRight = int.Parse(dataReader["CheckRight"].ToString());
                    entity.Admin = int.Parse(dataReader["bAdmin"].ToString());
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
        public Entity.User.UserRole GetRole(int RoleID)
        {
            Entity.User.UserRole entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
               
                IDbDataParameter[] prams = {
								   };
                string strSql = "select * from t_R_Role inner join t_R_Level on t_R_Role.LevelID=t_R_Level.id where t_R_Role.RoleID='" + RoleID + "'"; ;

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);
                while (dataReader.Read())
                {
                    entity = new Entity.User.UserRole();
                    entity.ID = int.Parse(dataReader["RoleID"].ToString());
                    entity.CName = dataReader["RoleName"].ToString();
                    entity.ILevel = int.Parse(dataReader["LevelID"].ToString());
                    entity.ReadRight = int.Parse(dataReader["ReadRight"].ToString());
                    entity.WriteRight = int.Parse(dataReader["WriteRight"].ToString());
                    entity.ImportRight = int.Parse(dataReader["ImportRight"].ToString());
                    entity.ExportRight = int.Parse(dataReader["ExportRight"].ToString());
                    entity.UpLoadRight = int.Parse(dataReader["UpLoadRight"].ToString());
                    entity.CheckRight = int.Parse(dataReader["CheckRight"].ToString());
                    entity.Admin = int.Parse(dataReader["bAdmin"].ToString());
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
        public int  CheckRoleUserRole(string role,int RoleID)
        {
            int ret=0;
            Entity.User.UserRole entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {

                IDbDataParameter[] prams = {
                                           };
				  string strSql= "select * from t_R_Role inner join t_R_Level on t_R_Role.LevelID=t_R_Level.id where  RoleName='"+role+"'"; ;
				  
                if(RoleID!=0)
                {
                 strSql= "select * from t_R_Role inner join t_R_Level on t_R_Role.LevelID=t_R_Level.id where t_R_Role.RoleID!='" + RoleID + "' and RoleName='"+role+"'"; ;
                }
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);
                while (dataReader.Read())
                {
                    ret = 1;
                }
                
            }
            catch (Exception ex)
            {
                ret = 0;
                Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
            }
            finally
            {
                db.Conn.Close();
            }
            return ret;
        }
        public Entity.User.UserRole GetUserRole(int UserID)
        {
            Entity.User.UserRole entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {

                IDbDataParameter[] prams = {
								   };
                string strSql = "select * from Users inner join t_R_Role on t_R_Role.RoleID=Users.iRoleType where Users.ID='" + UserID + "'"; ;

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);
                while (dataReader.Read())
                {
                    entity = new Entity.User.UserRole();
                    entity.ID = int.Parse(dataReader["RoleID"].ToString());
                    entity.CName = dataReader["RoleName"].ToString();
                    entity.ILevel = int.Parse(dataReader["LevelID"].ToString());
                    entity.ReadRight = int.Parse(dataReader["ReadRight"].ToString());
                    entity.WriteRight = int.Parse(dataReader["WriteRight"].ToString());
                    entity.ImportRight = int.Parse(dataReader["ImportRight"].ToString());
                    entity.ExportRight = int.Parse(dataReader["ExportRight"].ToString());
                    entity.UpLoadRight = int.Parse(dataReader["UpLoadRight"].ToString());
                    entity.CheckRight = int.Parse(dataReader["CheckRight"].ToString());
                    entity.Admin = int.Parse(dataReader["bAdmin"].ToString());
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
    }
}
