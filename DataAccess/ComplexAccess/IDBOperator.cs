using System;
using System.Collections;
using System.Data ;

namespace DataAccess
{
    /// <summary>							
	/// IDBOperator 用于底层封装基本的数据库操作，IDBOperator主要为IDBAccesser所用 。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
    public interface IDBOperator
    {
        //执行单纯的sql命令
        int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms);

        /// <summary>
        /// 可以在某个连接上执行批命令或单个命令，如果是批命令，closeConnection为false，
        /// 并且请在外部打开连接－执行批处理－关闭连接
        /// </summary>		
        int ExecuteNonQuery(IDbConnection connection, bool closeConnection, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms);

        /// <summary>
        /// 用于在事务中执行命令，请确保trans不为null
        /// </summary>		
        int ExecuteNonQueryTrans(IDbTransaction trans, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms);

        IDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms);
        object ExecuteScalar(string connString, CommandType cmdType, string cmdText, IDbDataParameter[] cmdParms, IDbTransaction trans);
        void CacheParameters(string cacheKey, IDbDataParameter[] cmdParms);
    }
		
}
