using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient ;
using System.Data.OracleClient ;

namespace DataAccess
{
	/// <summary>
	/// IDBTypeElementFactory 用于集中管理与《数据库类型》相关的所有信息。对于很多DataAccess中的类，
	/// 不同类型的数据库需要不同实现的只有该接口。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// 2005.04.18
	/// </summary>	
	public interface IDBTypeElementFactory
	{
		IDbCommand      GetCommand() ;
		IDbConnection   GetConnection(string conStr) ;
		IDbDataAdapter  GetDataAdapter() ;

		IADOBase		GetADOBase(string connStr) ;
		IDBOperator     GetDBOperator() ;

		ITransactionHelper GetTransactionHelper(string connStr) ;

        /// <summary>
        /// 过时方法
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="originDbType"></param>
        /// <param name="paraLen"></param>
        /// <returns></returns>
		IDbDataParameter   GetDbDataParameter(string paraName ,string originDbType ,int paraLen) ;//如果不指定长度，paraLen=0

        IDbDataParameter MakeInParam(string ParamName, string originDbType, object Value, int Size);
        IDbDataParameter MakeOutParam(string ParamName, string originDbType, int Size);
        IDbDataParameter MakeOutReturnParam();



		//根据adapter的select语句自动产生其它sql语句
		void BuildCommandForAdapter(IDbDataAdapter adapter) ;		
	}

	#region SqlDBTypeElementFactory

	public class SqlDBTypeElementFactory : IDBTypeElementFactory
	{
		#region IDBTypeElementFactory 成员
        
		public IDbCommand GetCommand()
		{
			return new SqlCommand() ;
		}

		public IDbConnection GetConnection(string conStr)
		{
			return new SqlConnection(conStr) ;
		}

		public IDbDataAdapter GetDataAdapter()
		{
			return new SqlDataAdapter() ;
		}

		public IADOBase GetADOBase(string connStr) 
		{
			return new SqlADOBase(connStr) ;
		}

		public IDBOperator GetDBOperator()
		{
			return new SqlBase() ;
		}

		public ITransactionHelper GetTransactionHelper(string connStr)
		{
			return new SqlTransactionHelper(connStr) ;
		}

		public void BuildCommandForAdapter(IDbDataAdapter adapter)
		{			
			SqlDataAdapter sqlAdapter = adapter as SqlDataAdapter ;
			if(sqlAdapter == null)
			{
				throw new Exception("The adapter is not SqlDataAdapter !") ;
			}

			SqlCommandBuilder builder = new SqlCommandBuilder(sqlAdapter) ;
		}

		public IDbDataParameter GetDbDataParameter(string paraName ,string originDbType ,int paraLen)
		{
			if(paraLen == 0)
			{
				return new SqlParameter(paraName, DBTypeConverter.GetSqlDbTypeByOriginName(originDbType)) ;
			}

			return new SqlParameter(paraName, DBTypeConverter.GetSqlDbTypeByOriginName(originDbType) ,paraLen) ;
		}


        public IDbDataParameter MakeInParam(string ParamName, string originDbType, object Value,int Size)
        {
            return MakeParam(ParamName, DBTypeConverter.GetSqlDbTypeByOriginName(originDbType), Size, ParameterDirection.Input, Value);
        }

        public IDbDataParameter MakeOutParam(string ParamName, string originDbType, int Size)
        {
            return MakeParam(ParamName, DBTypeConverter.GetSqlDbTypeByOriginName(originDbType), Size, ParameterDirection.Output, null);
        }

        public IDbDataParameter MakeOutReturnParam()
        {
            return new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0,
                string.Empty, DataRowVersion.Default, null);
        }


        /// <summary>
        /// 生成存储过程参数
        /// </summary>
        /// <param name="ParamName">存储过程名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }
		#endregion
	}

	#endregion

	#region OleDBTypeElementFactory
	public class OleDBTypeElementFactory : IDBTypeElementFactory
	{
		#region IDBTypeElementFactory 成员

		public IDbCommand GetCommand()
		{
			return new OleDbCommand() ;
		}

		public IDbConnection GetConnection(string conStr)
		{
			return new OleDbConnection(conStr) ;
		}

		public IDbDataAdapter  GetDataAdapter()
		{
			return new OleDbDataAdapter() ;
		}

		public IADOBase GetADOBase(string connStr) 
		{
			return new OleADOBase(connStr) ;
		}

		public IDBOperator GetDBOperator()
		{
			return new OleBase() ;
		}

		public void BuildCommandForAdapter(IDbDataAdapter adapter)
		{
			OleDbDataAdapter oleAdapter = adapter as OleDbDataAdapter ;
			if(oleAdapter == null)
			{
				throw new Exception("The adapter is not OleDbDataAdapter !") ;
			}

			OleDbCommandBuilder builder = new OleDbCommandBuilder(oleAdapter) ;
		}

		public ITransactionHelper GetTransactionHelper(string connStr)
		{
			return new OleTransactionHelper(connStr) ;
		}

		public IDbDataParameter GetDbDataParameter(string paraName ,string originDbType ,int paraLen)
		{
			if(paraLen == 0)
			{
				return new OleDbParameter(paraName, DBTypeConverter.GetOleDbTypeByOriginName(originDbType)) ;
			}

			return new OleDbParameter(paraName, DBTypeConverter.GetOleDbTypeByOriginName(originDbType) ,paraLen) ;
		}


        public IDbDataParameter MakeInParam(string ParamName, string originDbType, object Value, int Size)
        {
            return MakeParam(ParamName, DBTypeConverter.GetOleDbTypeByOriginName(originDbType), Size, ParameterDirection.Input, Value);
        }

        public IDbDataParameter MakeOutParam(string ParamName, string originDbType, int Size)
        {
            return MakeParam(ParamName, DBTypeConverter.GetOleDbTypeByOriginName(originDbType), Size, ParameterDirection.Output, null);
        }

        public IDbDataParameter MakeOutReturnParam()
        {
            return new OleDbParameter("ReturnValue", OleDbType.Integer, 4,
                ParameterDirection.ReturnValue, false, 0, 0,
                string.Empty, DataRowVersion.Default, null);
        }


        /// <summary>
        /// 生成存储过程参数
        /// </summary>
        /// <param name="ParamName">存储过程名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public OleDbParameter MakeParam(string ParamName, OleDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            OleDbParameter param;

            if (Size > 0)
                param = new OleDbParameter(ParamName, DbType, Size);
            else
                param = new OleDbParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }
		#endregion
	}
	#endregion

	#region OracleDBTypeElementFactory

	public class OracleDBTypeElementFactory : IDBTypeElementFactory
	{
		#region IDBTypeElementFactory 成员

		public IDbCommand GetCommand()
		{
			return new OracleCommand() ;
		}

		public IDbConnection GetConnection(string conStr)
		{
			return new OracleConnection(conStr) ;
		}

		public IDbDataAdapter GetDataAdapter()
		{
			return new OracleDataAdapter() ;
		}

		public IADOBase GetADOBase(string connStr)
		{			
			return new OracleADOBase(connStr);
		}

		public IDBOperator GetDBOperator()
		{			
			return new OracleBase();
		}

		public ITransactionHelper GetTransactionHelper(string connStr)
		{
			return new OracleTransactionHelper(connStr) ;
		}

		public IDbDataParameter GetDbDataParameter(string paraName, string originDbType, int paraLen)
		{
			if(paraLen == 0)
			{
				return new OracleParameter(paraName, DBTypeConverter.GetOracleTypeByOriginName(originDbType)) ;
			}

			return new OracleParameter(paraName, DBTypeConverter.GetOracleTypeByOriginName(originDbType) ,paraLen) ;
		}

		public void BuildCommandForAdapter(IDbDataAdapter adapter)
		{
			OracleDataAdapter oracleAdapter = adapter as OracleDataAdapter;
			if(oracleAdapter == null)
			{
				throw new Exception("The adapter is not OleDbDataAdapter !") ;
			}

			OracleCommandBuilder builder = new OracleCommandBuilder(oracleAdapter) ;
		}


        public IDbDataParameter MakeInParam(string ParamName, string originDbType, object Value, int Size)
        {
            return MakeParam(ParamName, DBTypeConverter.GetOracleTypeByOriginName(originDbType), Size, ParameterDirection.Input, Value);
        }

        public IDbDataParameter MakeOutParam(string ParamName, string originDbType, int Size)
        {
            return MakeParam(ParamName, DBTypeConverter.GetOracleTypeByOriginName(originDbType), Size, ParameterDirection.Output, null);
        }

        public IDbDataParameter MakeOutReturnParam()
        {
            return new OracleParameter("ReturnValue", OracleType.UInt32, 4,
                ParameterDirection.ReturnValue, false, 0, 0,
                string.Empty, DataRowVersion.Default, null);
        }


        /// <summary>
        /// 生成存储过程参数
        /// </summary>
        /// <param name="ParamName">存储过程名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public OracleParameter MakeParam(string ParamName, OracleType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            OracleParameter param;

            if (Size > 0)
                param = new OracleParameter(ParamName, DbType, Size);
            else
                param = new OracleParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }

		#endregion

	}

	#endregion
}
