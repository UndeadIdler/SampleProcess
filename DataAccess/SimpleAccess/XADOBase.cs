using System;

namespace DataAccess
{
	#region SqlADOBase
	/// <summary>
	/// SqlADOBase ADOBase在Sql数据库上实现。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public class SqlADOBase : ADOBase
	{
		public SqlADOBase(string connectStr) : base(connectStr)
		{
		}

		public override IDBTypeElementFactory GetDBTypeElementFactory()
		{
			return new SqlDBTypeElementFactory() ;
		}
	}
	#endregion

	#region OracleADOBase
	/// <summary>
	/// OracleADOBase ADOBase在Oracle数据库上实现。
	/// </summary>
	public class OracleADOBase  : ADOBase
	{
		public OracleADOBase(string connectStr) : base(connectStr)
		{
		}

		public override IDBTypeElementFactory GetDBTypeElementFactory()
		{
			return new OracleDBTypeElementFactory() ;
		}
	}
	#endregion

	#region OleADOBase
	/// <summary>
	/// OleADOBase ADOBase在Ole数据库上实现。。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public class OleADOBase : ADOBase
	{
		public OleADOBase(string connectStr) : base(connectStr)
		{
		}

		public override IDBTypeElementFactory GetDBTypeElementFactory()
		{
			return new OleDBTypeElementFactory() ;
		}
	}
	#endregion
}
