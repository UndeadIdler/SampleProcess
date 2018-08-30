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
  public   class Level
    {
      public List<Entity.User.UserLevel> LevelList(int level)
      {
           List<Entity.User.UserLevel> list = new List<Entity.User.UserLevel>();
            Entity.User.UserLevel menu = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
								   };
                string strSql = "select * from t_R_Level where id>="+level+"";

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);

                while (dataReader.Read())
                {
                    menu = new Entity.User.UserLevel();
                    menu.ILevelID = int.Parse(dataReader["id"].ToString());
                    menu.ILevel = dataReader["ILevel"].ToString();
                   
                    list.Add(menu);
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
       public DataTable LevelListDt(int level)
      {
          DataTable dt = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
								   };
                string strSql = "select * from t_R_Level where id>"+level+"";

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
     
      
    }
}
