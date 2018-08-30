using System;

namespace DataAccess
{
	/// <summary>
	/// DbElementFactoryGetter 用于获取数据库元素工厂。
	/// </summary>
	public interface IDBTypeElementFactoryGetter
	{
		IDBTypeElementFactory GetDBTypeElementFactory() ;
	}

	public class DbElementFactoryGetter 
	{	
		public static IDBTypeElementFactory GetDBTypeElementFactory(DataBaseType dbType)
		{
			switch(dbType)
			{
				case DataBaseType.SqlServer :
				{
					return new SqlDBTypeElementFactory() ;
				}
				case DataBaseType.Ole :
				{
					return new OleDBTypeElementFactory() ;
				}
				case DataBaseType.Oracle:
				{
					return new OracleDBTypeElementFactory() ;
				}
				default:
				{
					return null ;
				}
			}
		}
	}

}
