using System;
using System.Reflection ;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// IDBAccesserFactory 用于产生针对不同（类型数据库）的访问者Accesser 。	
	/// 约定：
	/// (1)数据库表名与DataClass类同名。
	/// (2)数据库访问类的名字类似于 DataClassName + "SqlDealer" ,如StudentSqlDealer 表示访问对应的SQL数据库中的Student表。	
	/// 
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// 2005.05.25
	/// </summary>
	public interface IDBAccesserFactory
	{		
		//如果connStr经常变化，可传入null 。如果是Ole，则connStr为access文件的路径。cachAccesser表示是否缓存创建的实例，
		void Initialize(DataBaseType dbType ,string connStr ,bool cachAccesser) ;
		
		//采用dataClassType作为参数是为了在编译时发现类型不存在的错误
		IDBAccesser CreateDBAccesser(Type dataClassType) ;			
		IDBAccesser CreateDBAccesser(DataBaseType dbType , string connStr , Type dataClassType ,string assemblyName);//assemblyName是访问类所属的程序集名称	
	}
	
	public class DBAccesserFactory :IDBAccesserFactory
	{
		private DataBaseType curDbType      = DataBaseType.SqlServer ;	
		private string       curConnStr     = null ;
		private bool         accesserCached = false ;
		private Hashtable    htableCached   = null ;
		
		#region IDBAccesserFactory 成员
		#region Initialize
		public void Initialize(DataBaseType dbType ,string connStr ,bool cachAccesser)
		{
			this.curDbType      = dbType ;			
			this.curConnStr     = connStr ;
			this.accesserCached = cachAccesser ;
			if(cachAccesser)
			{
				this.htableCached = Hashtable.Synchronized(new Hashtable()) ;
			}
		}
		#endregion

		#region public CreateDBAccesser
		public IDBAccesser CreateDBAccesser(Type dataClassType )
		{
			if(this.curConnStr == null)
			{
				throw new Exception("Paras Information is not enough to Create instance !") ;
			}			

			return this.CreateDBAccesser(this.curDbType ,this.curConnStr ,dataClassType ,null) ;			
		}

		public IDBAccesser CreateDBAccesser(DataBaseType dbType , string connStr , Type dataClassType ,string assemblyName)
		{
			string tarTypeName = dataClassType.FullName + DBAccesserFactory.GetAppendixOfDBType(dbType) ;

			return this.CreateDBAccesser(tarTypeName ,connStr ,dataClassType ,assemblyName) ;						
		}	
		#endregion

		#region private CreateDBAccesser
		private IDBAccesser CreateDBAccesser(string dealerTypeName , string connStr , Type dataClassType ,string assemblyName)
		{
			if(this.accesserCached)
			{
				if(this.htableCached[dataClassType] != null)
				{
					return (IDBAccesser)this.htableCached[dataClassType] ;
				}
			}
			
			Type tarType = AdvancedFunction.GetType(dealerTypeName ,assemblyName) ;

			if(tarType == null)
			{
				throw new Exception("The target type is not exit !") ;
			}

			Type supType = typeof(DataAccess.IDBAccesser)  ;

			// 不可用IsSubclassOf，IsSubclassOf 检测不了接口类型。
			if(! supType.IsAssignableFrom(tarType))
			{
				throw new Exception("The target DbDealer Type is not subclass of IDBAccesser !") ;
			}

			object[] args = {connStr} ;			
			IDBAccesser accesser = (IDBAccesser)Activator.CreateInstance(tarType ,args) ;

			if(this.accesserCached)
			{
				if(accesser != null)
				{
					this.htableCached.Add(dataClassType ,accesser) ;
				}
			}

			return accesser ;
		}
		#endregion

		#endregion

		#region GetAppendixOfDBType
		private static string GetAppendixOfDBType(DataBaseType dbType)
		{
			switch(dbType)
			{
				case DataBaseType.SqlServer :
				{
					return "SqlDealer" ;
				}
				case DataBaseType.Ole :
				{
					return "OleDealer" ;
				}
				case DataBaseType.Oracle :
				{
					return "OracleDealer" ;
				}
				default:
				{
					throw new Exception("Error DataBase Type !") ;
				}
			}
		}
		#endregion
	}

	public enum DataBaseType
	{
		SqlServer ,Ole ,Oracle
	}
}
