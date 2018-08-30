using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccess;
using ESBasic.Logger;


namespace DAl.Menu
{
    public class Menu
    {
        #region MyRegion
        
      
        ///// <summary>
        ///// 获取用户菜单
        ///// </summary>
        ///// <param name="cName">用户名</param>
        ///// <param name="iIsShow">是否显示</param>
        ///// <param name="iLevle">菜单级别</param>
        ///// <returns></returns>
        //public List<Entity.Menu> GetUserMenus(string cName, int iParentID, int iIsShow, int iLevle)
        //{
        //    List<Entity.Menu> list = new List<Entity.Menu>();
        //    Entity.Menu menu = null;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
                
        //         dbFactory.MakeInParam("@cUserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cName.GetType().ToString()),cName, 50),
        //        dbFactory.MakeInParam("@iIsShow",  DBTypeConverter.ConvertCsTypeToOriginDBType(iIsShow.GetType().ToString()),iIsShow, 0),
        //        dbFactory.MakeInParam("@iLevle",  DBTypeConverter.ConvertCsTypeToOriginDBType(iLevle.GetType().ToString()),iLevle, 0),
        //         dbFactory.MakeInParam("@iParentID",  DBTypeConverter.ConvertCsTypeToOriginDBType(iParentID.GetType().ToString()),iParentID, 0)
                
        //                           };

        //        IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_Menu_Query", prams);

        //        while (dataReader.Read())
        //        {
        //            menu = new Entity.Menu();
        //            menu.ID = int.Parse(dataReader["ID"].ToString());
        //            menu.CName = dataReader["CName"].ToString();
        //            menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
        //            menu.CDescription = dataReader["cDescription"].ToString();
        //            menu.CImage = dataReader["cImage"].ToString();
        //            menu.CUrlPath = dataReader["CUrlPath"].ToString();
        //            menu.CCode = dataReader["cCode"].ToString();
        //            menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
        //            menu.IOrderID = dataReader["iOrderID"].ToString();
        //            menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
        //            menu.CRole = dataReader["CRole"].ToString();
        //            menu.IRight = int.Parse(dataReader["IRight"].ToString());
        //            menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
        //            menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
        //            menu.CCreateUser = dataReader["cCreateUser"].ToString();
        //            menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
        //            list.Add(menu);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return list;
        //}

        
        //public List<Entity.Menu> GetMenuByRole(string cRole)
        //{
        //    List<Entity.Menu> list = new List<Entity.Menu>();
        //    Entity.Menu menu = null;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
                
        //        dbFactory.MakeInParam("@cRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(cRole.GetType().ToString()),cRole, 4000)
                
        //                           };

        //        IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_Menu_GetByRole", prams);

        //        while (dataReader.Read())
        //        {
        //            menu = new Entity.Menu();
        //            menu.ID = int.Parse(dataReader["ID"].ToString());
        //            menu.CName = dataReader["CName"].ToString();
        //            menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
        //            menu.CDescription = dataReader["cDescription"].ToString();
        //            menu.CImage = dataReader["cImage"].ToString();
        //            menu.CUrlPath = dataReader["cUrlPath"].ToString();
        //            menu.CCode = dataReader["cCode"].ToString();
        //            menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
        //            menu.IOrderID = dataReader["iOrderID"].ToString();
        //            menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
        //            menu.CRole = dataReader["CRole"].ToString();
        //            menu.IRight = int.Parse(dataReader["IRight"].ToString());
        //            menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
        //            menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
        //            menu.CCreateUser = dataReader["cCreateUser"].ToString();
        //            menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
        //            list.Add(menu);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 增加菜单
        ///// </summary>
        ///// <param name="menu">菜单</param>
        ///// <returns></returns>
        //public int AddMenu(Entity.Menu menu)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        //@cName ,@iParentID  ,@cDescription  ,@cImage ,@cCode  ,@iLevle  ,@iOrderID ,@iIsShow ,@dCreateDate ,@cCreateUser ,@dUpdateDate ,@cUpdateUser
        //        IDbDataParameter[] prams = {
        //        dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CName.GetType().ToString()),menu.CName, 50),
        //        dbFactory.MakeInParam("@iParentID",  DBTypeConverter.ConvertCsTypeToOriginDBType(menu.IParentID.GetType().ToString()), menu.IParentID,0),
        //        dbFactory.MakeInParam("@cDescription",   DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CDescription.GetType().ToString()), menu.CDescription, 0),
        //        dbFactory.MakeInParam("@cImage",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CImage.GetType().ToString()), menu.CImage,500),
        //        dbFactory.MakeInParam("@cUrlPath",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CUrlPath.GetType().ToString()), menu.CUrlPath,0),
        //        dbFactory.MakeInParam("@cCode",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CCode.GetType().ToString()), menu.CCode, 50),
        //        dbFactory.MakeInParam("@iLevle",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.ILevle.GetType().ToString()), menu.ILevle, 0),
        //        dbFactory.MakeInParam("@iOrderID",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.IOrderID.GetType().ToString()), menu.IOrderID, 50),
        //        dbFactory.MakeInParam("@iIsShow",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.IIsShow.GetType().ToString()), menu.IIsShow, 0),
        //        dbFactory.MakeInParam("@dCreateDate",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.DCreateDate.GetType().ToString()), menu.DCreateDate, 0),
        //        dbFactory.MakeInParam("@cCreateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CUpdateUser.GetType().ToString()), menu.CUpdateUser, 50),
        //        dbFactory.MakeInParam("@dUpdateDate ",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.DUpdateDate.GetType().ToString()), menu.DUpdateDate, 0),
        //        dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CUpdateUser.GetType().ToString()), menu.CUpdateUser, 50)

        //                                   };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "[proc_Menu_Add]", prams);

        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return iReturn;
        //}

        ///// <summary>
        ///// 删除菜单
        ///// </summary>
        ///// <param name="id">菜单id</param>
        ///// <returns></returns>
        //public int DeleteMenu(int id)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType(id.GetType().ToString()),id, 0)
        //                                   };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "[proc_Menu_Delete]", prams);

        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return iReturn;
        //}

        ///// <summary>
        ///// 修改菜单
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public int UpdateMenu(Entity.Menu menu)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        dbFactory.MakeInParam("@cName",  DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CName.GetType().ToString()),menu.CName, 50),
        //        dbFactory.MakeInParam("@iParentID",  DBTypeConverter.ConvertCsTypeToOriginDBType(menu.IParentID.GetType().ToString()), menu.IParentID,0),
        //        dbFactory.MakeInParam("@cDescription",   DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CDescription.GetType().ToString()), menu.CDescription, 0),
        //        dbFactory.MakeInParam("@cImage",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CImage.GetType().ToString()), menu.CImage,500),
        //        dbFactory.MakeInParam("@CUrlPath",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CUrlPath.GetType().ToString()), menu.CUrlPath,0),
        //        dbFactory.MakeInParam("@cCode",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CCode.GetType().ToString()), menu.CCode, 50),
        //        dbFactory.MakeInParam("@iLevle",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.ILevle.GetType().ToString()), menu.ILevle, 0),
        //        dbFactory.MakeInParam("@iOrderID",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.IOrderID.GetType().ToString()), menu.IOrderID, 50),
        //        dbFactory.MakeInParam("@iIsShow",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.IIsShow.GetType().ToString()), menu.IIsShow, 0),
        //        dbFactory.MakeInParam("@dUpdateDate ",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.DUpdateDate.GetType().ToString()), menu.DUpdateDate, 0),
        //        dbFactory.MakeInParam("@cUpdateUser",         DBTypeConverter.ConvertCsTypeToOriginDBType(menu.CUpdateUser.GetType().ToString()), menu.CUpdateUser, 50)

        //                                   };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.constr), true, CommandType.StoredProcedure, "[proc_Menu_Update]", prams);

        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return iReturn;
        //}

        ///// <summary>
        ///// 获取子菜单
        ///// </summary>
        ///// <param name="cUserName">用户名</param>
        ///// <param name="iLevle"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public List<Entity.Menu> GetMenu(string cUserName, int iLevle, int iParentID, string cRole)
        //{
        //    List<Entity.Menu> list = new List<Entity.Menu>();
        //    Entity.Menu menu = null;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        dbFactory.MakeInParam("@iLevle",  DBTypeConverter.ConvertCsTypeToOriginDBType(iLevle.GetType().ToString()),iLevle, 0),
        //        dbFactory.MakeInParam("@iParentID",  DBTypeConverter.ConvertCsTypeToOriginDBType(iParentID.GetType().ToString()),iParentID, 0),
        //        dbFactory.MakeInParam("@cRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(cRole.GetType().ToString()),cRole, 50)
                
        //                           };

        //        IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_Menu_Get", prams);

        //        while (dataReader.Read())
        //        {
        //            menu = new Entity.Menu();
        //            menu.ID = int.Parse(dataReader["ID"].ToString());
        //            menu.CName = dataReader["CName"].ToString();
        //            menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
        //            menu.CDescription = dataReader["cDescription"].ToString();
        //            menu.CImage = dataReader["cImage"].ToString();
        //            menu.CUrlPath = dataReader["CUrlPath"].ToString();
        //            menu.CCode = dataReader["cCode"].ToString();
        //            menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
        //            menu.IOrderID = dataReader["iOrderID"].ToString();
        //            menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
        //            menu.CRole = dataReader["CRole"].ToString();
        //            menu.IRight = int.Parse(dataReader["IRight"].ToString());
        //            menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
        //            menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
        //            menu.CCreateUser = dataReader["cCreateUser"].ToString();
        //            menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
        //            menu.BState = IsExistsUsers(cUserName, menu.ID, 0) == true ? 1 : 0;
        //            list.Add(menu);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return list;
        //}

        //public List<Entity.Menu> GetMenu(int iLevle, int iParentID)
        //{
        //    List<Entity.Menu> list = new List<Entity.Menu>();
        //    Entity.Menu menu = null;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        dbFactory.MakeInParam("@iLevle",  DBTypeConverter.ConvertCsTypeToOriginDBType(iLevle.GetType().ToString()),iLevle, 0),
        //        dbFactory.MakeInParam("@iParentID",  DBTypeConverter.ConvertCsTypeToOriginDBType(iParentID.GetType().ToString()),iParentID, 0)
                
        //                           };

        //        IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_Menu_Get", prams);

        //        while (dataReader.Read())
        //        {
        //            menu = new Entity.Menu();
        //            menu.ID = int.Parse(dataReader["ID"].ToString());
        //            menu.CName = dataReader["CName"].ToString();
        //            menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
        //            menu.CDescription = dataReader["cDescription"].ToString();
        //            menu.CImage = dataReader["cImage"].ToString();
        //            menu.CUrlPath = dataReader["CUrlPath"].ToString();
        //            menu.CCode = dataReader["cCode"].ToString();
        //            menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
        //            menu.IOrderID = dataReader["iOrderID"].ToString();
        //            menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
        //            menu.CRole = dataReader["CRole"].ToString();
        //            menu.IRight = int.Parse(dataReader["IRight"].ToString());
        //            menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
        //            menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
        //            menu.CCreateUser = dataReader["cCreateUser"].ToString();
        //            menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
        //            list.Add(menu);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return list;
        //}


        //public bool IsExistsUsers(string cUserName, int MenuID, int iType)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        dbFactory.MakeInParam("@cUserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cUserName.GetType().ToString()),cUserName, 50),
        //        dbFactory.MakeInParam("@MenuID",  DBTypeConverter.ConvertCsTypeToOriginDBType(MenuID.GetType().ToString()),MenuID, 0),
        //        dbFactory.MakeInParam("@iType",  DBTypeConverter.ConvertCsTypeToOriginDBType(iType.GetType().ToString()),iType, 0)
        //                           };

        //        IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_Menu_IsExists", prams);

        //        if (dataReader.Read())
        //        {
        //            iReturn = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return iReturn == 1 ? true : false;
        //}


        //public Entity.Menu GetUserMenu(string cUserName, string cCode)
        //{
        //    Entity.Menu menu = null;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        dbFactory.MakeInParam("@cUserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cUserName.GetType().ToString()),cUserName, 50),
        //        dbFactory.MakeInParam("@cCode",  DBTypeConverter.ConvertCsTypeToOriginDBType(cCode.GetType().ToString()),cCode, 50)
        //                           };

        //        IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_Menu_GetUser", prams);

        //        while (dataReader.Read())
        //        {
        //            menu = new Entity.Menu();
        //            menu.ID = int.Parse(dataReader["ID"].ToString());
        //            menu.CName = dataReader["CName"].ToString();
        //            menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
        //            menu.CDescription = dataReader["cDescription"].ToString();
        //            menu.CImage = dataReader["cImage"].ToString();
        //            menu.CUrlPath = dataReader["CUrlPath"].ToString();
        //            menu.CCode = dataReader["cCode"].ToString();
        //            menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
        //            menu.IOrderID = dataReader["iOrderID"].ToString();
        //            menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
        //            menu.CRole = dataReader["CRole"].ToString();
        //            menu.BLuRu = DataHelper.ParseToBoolean(dataReader["BLuRu"].ToString());
        //            menu.BCheck = DataHelper.ParseToBoolean(dataReader["BCheck"].ToString());
        //            menu.IRight = int.Parse(dataReader["IRight"].ToString());
        //            menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
        //            menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
        //            menu.CCreateUser = dataReader["cCreateUser"].ToString();
        //            menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Comm.EsbLogger.Log(ex.GetType().ToString(), ex.Message.ToString(), 0, ErrorLevel.Fatal);
        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return menu;
        //}
        #endregion
        #region 现用
        /// <summary>
        /// 根据用户名和用户角色获取用户菜单
        /// </summary>
        /// <param name="cName">用户名</param>
        /// <param name="iIsShow">是否显示</param>
        /// <param name="iLevle">菜单级别</param>
        /// <param name="iRole">用户角色</param>
        /// <returns></returns>
        public List<Entity.Menu> GetUserRoleMenus(string cUserName, int iParentID, int iIsShow, int iLevle, int iRole)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            Entity.Menu menu = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                
                dbFactory.MakeInParam("@cUserName",  DBTypeConverter.ConvertCsTypeToOriginDBType(cUserName.GetType().ToString()),cUserName, 50),
                dbFactory.MakeInParam("@iIsShow",  DBTypeConverter.ConvertCsTypeToOriginDBType(iIsShow.GetType().ToString()),iIsShow, 0),
                dbFactory.MakeInParam("@iLevle",  DBTypeConverter.ConvertCsTypeToOriginDBType(iLevle.GetType().ToString()),iLevle, 0),
                dbFactory.MakeInParam("@iParentID",  DBTypeConverter.ConvertCsTypeToOriginDBType(iParentID.GetType().ToString()),iParentID, 0),
                dbFactory.MakeInParam("@iRole",  DBTypeConverter.ConvertCsTypeToOriginDBType(iRole.GetType().ToString()),iRole, 0),
                
								   };

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.StoredProcedure, "proc_Menu_QueryRole", prams);

                while (dataReader.Read())
                {
                    menu = new Entity.Menu();
                    menu.ID = int.Parse(dataReader["ID"].ToString());
                    menu.CName = dataReader["CName"].ToString();
                    menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
                    menu.CDescription = dataReader["cDescription"].ToString();
                    menu.CImage = dataReader["cImage"].ToString();
                    menu.CUrlPath = dataReader["CUrlPath"].ToString();
                    menu.CCode = dataReader["cCode"].ToString();
                    menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
                    menu.IOrderID = dataReader["iOrderID"].ToString();
                    menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
                    menu.CUrlPath = dataReader["cUrlPath"].ToString();
                    menu.CRole = dataReader["CRole"].ToString();
                    menu.IRight = int.Parse(dataReader["IRight"].ToString());
                    menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
                    menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    menu.CCreateUser = dataReader["cCreateUser"].ToString();
                    menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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

        /// <summary>
        /// 根据用户名和用户角色获取用户菜单
        /// </summary>
        /// <param name="cName">用户名</param>
        /// <param name="iIsShow">是否显示</param>
        /// <param name="iLevle">菜单级别</param>
        /// <param name="iRole">用户角色</param>
        /// <returns></returns>
        public List<Entity.Menu> GetUserRoleListMenus(int iRole,int fatherid,int ishow,int checkstr)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            Entity.Menu menu = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
								   };
                string strSql = "select  t_R_Menu.*,t_R_Role.LevelID,t_R_RoleMenu.roleid from t_R_Role,t_R_Menu,t_R_RoleMenu " +
               " where t_R_Role.RoleID=t_R_RoleMenu.RoleID and " +
               " t_R_RoleMenu.MenuID=t_R_Menu.ID and " +
               " t_R_Role.RoleID='" + iRole + "' and " +
               "t_R_Menu.iParentID='" + fatherid + "' and t_R_Menu.iIsShow='" + ishow + "' and t_R_RoleMenu.checked='" + checkstr + "' order by t_R_Menu.iOrderID";

                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, strSql, prams);

                while (dataReader.Read())
                {
                    menu = new Entity.Menu();
                    menu.ID = int.Parse(dataReader["ID"].ToString());
                    menu.CName = dataReader["CName"].ToString();
                    menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
                    menu.CDescription = dataReader["cDescription"].ToString();
                    menu.CImage = dataReader["cImage"].ToString();
                    menu.CUrlPath = dataReader["CUrlPath"].ToString();
                    menu.CCode = dataReader["cCode"].ToString();
                    menu.ILevle = int.Parse(dataReader["LevelID"].ToString());
                    menu.IOrderID = dataReader["iOrderID"].ToString();
                    menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
                    menu.CUrlPath = dataReader["cUrlPath"].ToString();
                    menu.CRole = dataReader["roleid"].ToString();
                    menu.IRight = int.Parse(dataReader["IRight"].ToString());
                    //menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
                    //menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    //menu.CCreateUser = dataReader["cCreateUser"].ToString();
                    //menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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

      /// <summary>
      /// 根据父菜单获取菜单列表 
      /// </summary>
      /// <param name="fatherid"></param>
      /// <returns></returns>
        public List<Entity.Menu> GetMenusByFather(int fatherid)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            Entity.Menu menu = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                                          };

                string sqlStr = "select * from t_R_Menu where iParentID ='" + fatherid + "' order by iParentID,iOrderID ASC";
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, sqlStr, prams);

                while (dataReader.Read())
                {
                    menu = new Entity.Menu();
                    menu.ID = int.Parse(dataReader["ID"].ToString());
                    menu.CName = dataReader["CName"].ToString();
                    menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
                    menu.CDescription = dataReader["cDescription"].ToString();
                    menu.CImage = dataReader["cImage"].ToString();
                    menu.CUrlPath = dataReader["CUrlPath"].ToString();
                    menu.CCode = dataReader["cCode"].ToString();
                    menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
                    menu.IOrderID = dataReader["iOrderID"].ToString();
                    menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
                    menu.CUrlPath = dataReader["cUrlPath"].ToString();
                    //menu.CRole = dataReader["CRole"].ToString();
                    menu.IRight = int.Parse(dataReader["IRight"].ToString());
                    menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
                    menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    menu.CCreateUser = dataReader["cCreateUser"].ToString();
                    menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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
       /// <summary>
       /// 根据用户角色获取菜单列表 
       /// </summary>
       /// <param name="RoleID"></param>
       /// <returns></returns>
        public List<Entity.Menu> GetMenusByRole(int RoleID)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            Entity.Menu menu = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                                          };

                string sqlStr = "select * from t_R_Menu  inner join t_R_RoleMenu on t_R_RoleMenu.menuid=t_R_Menu.ID where t_R_RoleMenu.roleid ='" + RoleID + "' and checked=1 order by iParentID,iOrderID ASC";
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, sqlStr, prams);

                while (dataReader.Read())
                {
                    menu = new Entity.Menu();
                    menu.ID = int.Parse(dataReader["ID"].ToString());
                    menu.CName = dataReader["CName"].ToString();
                    menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
                    menu.CDescription = dataReader["cDescription"].ToString();
                    menu.CImage = dataReader["cImage"].ToString();
                    menu.CUrlPath = dataReader["CUrlPath"].ToString();
                    menu.CCode = dataReader["cCode"].ToString();
                    menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
                    menu.IOrderID = dataReader["iOrderID"].ToString();
                    menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
                    menu.CUrlPath = dataReader["cUrlPath"].ToString();
                   
                    menu.IRight = int.Parse(dataReader["IRight"].ToString());
                    menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
                    menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    menu.CCreateUser = dataReader["cCreateUser"].ToString();
                    menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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

        /// <summary>
        /// 根据用户角色获取菜单列表 
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public List<Entity.Menu> GetMenusByIsShow(int IsShow)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            Entity.Menu menu = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                                          };

                string sqlStr = "select * from t_R_Menu  where iIsShow ='" + IsShow + "' order by iParentID,iOrderID ASC";
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, sqlStr, prams);

                while (dataReader.Read())
                {
                    menu = new Entity.Menu();
                    menu.ID = int.Parse(dataReader["ID"].ToString());
                    menu.CName = dataReader["CName"].ToString();
                    menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
                    menu.CDescription = dataReader["cDescription"].ToString();
                    menu.CImage = dataReader["cImage"].ToString();
                    menu.CUrlPath = dataReader["CUrlPath"].ToString();
                    menu.CCode = dataReader["cCode"].ToString();
                    menu.IOrderID = dataReader["iOrderID"].ToString();
                    menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
                    menu.CUrlPath = dataReader["cUrlPath"].ToString();
                   
                    menu.IRight = int.Parse(dataReader["IRight"].ToString());
                    menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
                    menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    menu.CCreateUser = dataReader["cCreateUser"].ToString();
                    menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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

        /// <summary>
        /// 根据用户获取菜单可写权限列表 
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public List<Entity.Menu> GetMenusByUserID(int UserID)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            Entity.Menu menu = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                                          };

                string sqlStr = "select * from t_R_Menu  inner join [t_R_User_IsWrite] on [t_R_User_IsWrite].menuid=t_R_Menu.ID where [t_R_User_IsWrite].userid ='" + UserID + "' and isWrite=1 order by iParentID,iOrderID ASC";
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, sqlStr, prams);

                while (dataReader.Read())
                {
                    menu = new Entity.Menu();
                    menu.ID = int.Parse(dataReader["ID"].ToString());
                    menu.CName = dataReader["CName"].ToString();
                    menu.IParentID = int.Parse(dataReader["iParentID"].ToString());
                    menu.CDescription = dataReader["cDescription"].ToString();
                    menu.CImage = dataReader["cImage"].ToString();
                    menu.CUrlPath = dataReader["CUrlPath"].ToString();
                    menu.CCode = dataReader["cCode"].ToString();
                    menu.ILevle = int.Parse(dataReader["iLevle"].ToString());
                    menu.IOrderID = dataReader["iOrderID"].ToString();
                    menu.IIsShow = int.Parse(dataReader["iIsShow"].ToString());
                    menu.CUrlPath = dataReader["cUrlPath"].ToString();

                    menu.IRight = int.Parse(dataReader["IRight"].ToString());
                    menu.CUpdateUser = dataReader["cUpdateUser"].ToString();
                    menu.DCreateDate = DateTime.Parse(dataReader["dCreateDate"].ToString());
                    menu.CCreateUser = dataReader["cCreateUser"].ToString();
                    menu.DUpdateDate = DateTime.Parse(dataReader["dUpdateDate"].ToString());
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
          /// <summary>
        /// 检查用户可写权限是否配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckRight(int UserID)
        {
            bool ret = false;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
                                          };
                string sql_sel = "select count(*) from t_R_User_IsWrite where userid = " + UserID;
                //   string sqlStr = "select * from t_R_Menu  inner join [t_R_User_IsWrite] on [t_R_User_IsWrite].menuid=t_R_Menu.ID where [t_R_User_IsWrite].userid ='" + UserID + "' and checked=1 order by iParentID,iOrderID ASC";
                IDataReader dataReader = db.ExecuteReader(Config.constr, CommandType.Text, sql_sel, prams);

                while (dataReader.Read())
                {
                    ret = int.Parse(dataReader[0].ToString())==0?false:true;
                }
            }
            catch
            { ret = false; }
            finally
            {
                db.Conn.Close();
            }
            return ret;
        }
        /// <summary>
        /// SetUserMenuRight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int InsertUserMenuRight(int UserID, string MenuID, string NuMenuID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = { };
                string[] list = MenuID.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] != "")
                    {
                        db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("Insert into [t_R_User_IsWrite] (userid,menuid, isWrite,createdate) values('{0}',{1},'{2}',getdate())", UserID, list[i], '1'), prams);
                    }
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
         /// <summary>
        /// SetUserMenuRight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SetUserMenuRight(int UserID, string MenuID,string NuMenuID)
      {
          int iReturn = 0;
          DBOperatorBase db = new DataBase();

          IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
          SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
          IDbTransaction trans = thelper.StartTransaction();
          try
          {
              
                  IDbDataParameter[] prams = {};

                  if (MenuID != "")
                  {
                      db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update [t_R_User_IsWrite] set isWrite='1' where menuid in({0}) and userid='{1}'", MenuID, UserID), prams);
                  }
                  if (NuMenuID!=null)
                 {
                     db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update [t_R_User_IsWrite] set isWrite='0' where menuid in({0}) and userid='{1}'", NuMenuID, UserID), prams); 
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
        /// <summary>
        /// SetRoleMenu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SetRoleMenu(int RoleID, string MenuID, string NuMenuID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = { };

                if (MenuID != "")
                {
                    db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_R_RoleMenu set checked='1' where MenuID in({0}) and RoleID='{1}'", MenuID, RoleID), prams);
                }
                if (NuMenuID != null)
                {
                    db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_R_RoleMenu set checked='0' where MenuID in({0}) and RoleID='{1}'", NuMenuID, RoleID), prams);
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

        
        #endregion

    }
}
