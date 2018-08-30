using System;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess
{

    #region SqlBase
    /// <summary>
    /// SqlBase 用于SQL Server数据库 。
    /// 作者：朱伟 sky.zhuwei@163.com 
    /// </summary>
    public class SqlBase : DBOperatorBase
    {
        public override IDBTypeElementFactory GetDBTypeElementFactory()
        {
            return new SqlDBTypeElementFactory();
        }
    }
    #endregion

    #region OleBase
    public class OleBase : DBOperatorBase
    {
        public override IDBTypeElementFactory GetDBTypeElementFactory()
        {
            return new OleDBTypeElementFactory();
        }
    }
    #endregion

    #region OracleBase
    public class OracleBase : DBOperatorBase
    {
        public override IDBTypeElementFactory GetDBTypeElementFactory()
        {
            return new OracleDBTypeElementFactory();
        }
    }
    #endregion
}
