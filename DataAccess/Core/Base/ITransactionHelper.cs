using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient ;

namespace DataAccess
{
	/// <summary>
	/// ITransactionHelper 主要用于简化ADO.NET事务编程，它可以处理同一数据库内部的事务。通常将其与IDBAccesser结合起来使用 。
	/// 如果是跨数据库的事务处理，则需要使用COM+事务。
	/// 
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// 2004.04.18
	/// </summary>
	public interface ITransactionHelper
	{
		//打开数据库连接，并启动事务
		IDbTransaction StartTransaction() ; //返回的结果可作为IDBAccesser的支持事务的方法（如Insert）的参数

		//提交事务，并关闭数据库连接
		void CommitTransaction(IDbTransaction trans) ;

		//回滚事务，并关闭数据库连接
		void RollTransaction(IDbTransaction trans) ;
	}

	#region BaseTransactionHelper
	// 同一个BaseTransactionHelper 对象不可穿插在两个事务中，最好是一个BaseTransactionHelper 对象对应一个事务。
	public abstract class BaseTransactionHelper :ITransactionHelper
	{
		protected string connectStr ;
		protected IDbConnection connection ;

		public BaseTransactionHelper(string conStr)
		{
			this.connectStr = conStr ;
			this.InitializeConnectionPool() ;
		}

		protected abstract void InitializeConnectionPool() ;

		#region ITransactionHelper 成员

		public IDbTransaction StartTransaction()
		{
			if(this.connection == null)
			{
				return null ;
			}

			this.connection.Open() ;
			return this.connection.BeginTransaction() ;
		}

		public void CommitTransaction(IDbTransaction trans)
		{
			if((this.connection == null) ||(trans == null))
			{
				return ;
			}

			trans.Commit() ;
			this.connection.Close() ;
		}

		public void RollTransaction(IDbTransaction trans)
		{
			if((this.connection == null) ||(trans == null))
			{
				return ;
			}

			trans.Rollback() ;
			this.connection.Close() ;
		}

		#endregion
	}
	#endregion

	#region SqlTransactionHelper
	//SQLServer数据库的TransactionHelper
	public class SqlTransactionHelper : BaseTransactionHelper
	{	
		public SqlTransactionHelper(string conStr) : base(conStr)
		{						
		}

		protected override void InitializeConnectionPool()
		{
			this.connection = new SqlConnection(this.connectStr) ;
		}
		
	}
	#endregion

	#region OleTransactionHelper
	//OLE Access数据库的TransactionHelper
	public class OleTransactionHelper : BaseTransactionHelper
	{	
		public OleTransactionHelper(string conStr) : base(conStr)
		{						
		}

		protected override void InitializeConnectionPool()
		{
			this.connection = new OleDbConnection(this.connectStr) ;
		}		
	}
	#endregion

	#region OracleTransactionHelper
	public class OracleTransactionHelper : BaseTransactionHelper
	{	
		public OracleTransactionHelper(string conStr) : base(conStr)
		{						
		}

		protected override void InitializeConnectionPool()
		{
			this.connection = new OracleConnection(this.connectStr) ;
		}		
	}
	#endregion
}
