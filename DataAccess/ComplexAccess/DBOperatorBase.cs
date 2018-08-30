using System;
using System.Collections;
using System.Data;


namespace DataAccess
{
    /// <summary>
	/// DBOperatorBase 用于底层封装基本的数据库操作，不同数据库类型的派生类只需要重写IDBTypeElementFactory接口即可。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
    public abstract class DBOperatorBase : IDBOperator, IDBTypeElementFactoryGetter
    {
        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        private IDBTypeElementFactory dbElementFactory = null;

        public DBOperatorBase()
        {
            this.dbElementFactory = this.GetDBTypeElementFactory();
        }

        private IDbConnection conn;
        public IDbConnection Conn
        {
            get { return this.conn; }
        }

        #region 新增成员
        protected void PrepareCommand(IDbCommand cmd, IDbConnection conn, IDbTransaction trans, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms)
        {
            this.conn = conn;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (IDbDataParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region IDBOperator 成员

        #region ExecuteNonQuery
        public int ExecuteNonQuery(string connString, System.Data.CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms)
        {
            this.conn = this.dbElementFactory.GetConnection(connString);
            return this.ExecuteNonQuery(conn, true, cmdType, cmdText, cmdParms);
        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(IDbConnection connection, bool closeConnection, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms)
        {
            try
            {
                this.conn = connection;
                IDbCommand cmd = this.dbElementFactory.GetCommand();
                this.PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
                return val;
            }
            catch(Exception ex)
            {
                ex = ex;
                return -1;
            }
            finally
            {
                if (closeConnection)
                {
                    connection.Close();
                }
            }
        }
        #endregion

        #region ExecuteNonQueryTrans
        public int ExecuteNonQueryTrans(IDbTransaction trans, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms)
        {
            IDbCommand cmd = this.dbElementFactory.GetCommand();
            this.PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region ExecuteReader
        public IDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms)
        {
            IDbCommand cmd = this.dbElementFactory.GetCommand();
            this.conn = this.dbElementFactory.GetConnection(connString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                IDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection表明关闭DataReader对象，则关联的Connection 对象自动关闭
                //cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteScalar
        public object ExecuteScalar(string connString, System.Data.CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms, IDbTransaction trans)
        {
            IDbCommand cmd = this.dbElementFactory.GetCommand();
            this.conn = this.dbElementFactory.GetConnection(connString);

            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                ex = ex;
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region CacheParameters
        public void CacheParameters(string cacheKey, IDbDataParameter[] cmdParms)
        {
            this.parmCache[cacheKey] = cmdParms;
        }
        #endregion

        #region GetCachedParameters
        public IDbDataParameter[] GetCachedParameters(string cacheKey)
        {
            IDbDataParameter[] cachedParms = (IDbDataParameter[])this.parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            IDbDataParameter[] clonedParms = new IDbDataParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (IDbDataParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }
        #endregion

        #endregion

        #region IDBTypeAssistantGetter 成员

        public abstract IDBTypeElementFactory GetDBTypeElementFactory();
        #endregion
    }
}
