using System;
using System.Reflection;
using System.Data;
using System.Collections ;

namespace DataAccess
{
	/// <summary>
	/// DataEntrance 是使用DataAccess的基础入口，对XCodeFactory生成的数据层代码提供最大的支持，
	/// 简化用户对该类库的使用。
	/// (1)用户必须实现IDataBaseInfoMgr接口用于提供数据库类型和连接信息。
	/// (2)dbFactory 直接传入new XDBAccesserFactory() 即可。
	/// (3)请确保在主窗体的构造函数中或Main方法中调用了DataEntrance.Initialize() ;
	/// (4)凡是需要得到IDBAccesser实例的地方，请调用DataEntrance.CreateDBAccesser() ;
	/// (5)若是只需要使用IDBAccesser接口的query和order接口，可以使用DataEntrance中提供的更简单的方法
	/// (6)如果是多数据库的情况，则dataClassType的命名空间最后一字段就是对应的数据库名称。
	/// </summary>
	public class DataEntrance
	{		
		private static IDBAccesserFactory    DBFactory  ;
		private static IDataBaseInfoMgr      CurDbInfoMgr ;
		private static IDBTypeElementFactory CurElementFactory ;
		
		#region Initialize
		/// <summary>
		/// Initialize 方法用于初始化数据库连接信息。
		/// </summary>		
		public static bool Initialize(IDataBaseInfoMgr dbInfoMgr ,IDBAccesserFactory dbFactory )
		{
			if(dbInfoMgr == null)
			{
				return false ;
			}

			DataEntrance.CurDbInfoMgr = dbInfoMgr ;	
			DataEntrance.DBFactory    = dbFactory ;
			RefreshConfig() ;
			CurDbInfoMgr.DbConfigChanged += new EventHandler(CurDbInfoMgr_DbConfigChanged);		

			return true ;
		}

		private static void RefreshConfig()
		{
			string connStr = null ;
			DataBaseType dbType = CurDbInfoMgr.GetDbType() ;
			connStr = CurDbInfoMgr.GetConnString() ;
			
			DataEntrance.DBFactory.Initialize(dbType ,connStr ,true) ;	
			DataEntrance.CurElementFactory = DbElementFactoryGetter.GetDBTypeElementFactory(dbType) ;
		}		
		#endregion		

		#region CreateDBAccesser
		public static IDBAccesser CreateDBAccesser(Type dataClassType)
		{
			if(! DataEntrance.CurDbInfoMgr.IsMultiDataBase)
			{
				return DataEntrance.DBFactory.CreateDBAccesser(dataClassType ) ;
			}
			else
			{
				string[] parts = dataClassType.ToString().Split('.') ;
				if(parts.Length <3)
				{
					throw new Exception("The {0} dealer's namespace is invalid !") ;
				}

				string destDbName = parts[parts.Length-2] ;

				return DBFactory.CreateDBAccesser(CurDbInfoMgr.GetDbType() ,CurDbInfoMgr.GetConnString(destDbName) ,dataClassType ,null) ;
			}
		}		
		#endregion

		#region Query
		public static object GetFieldValue(Type objType ,string theID ,string fieldName)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.GetFieldValue(theID ,fieldName) ;
		}

		public static object GetFieldValueEx(Type objType ,string whereStr ,string fieldName)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.GetFieldValueEx(whereStr ,fieldName) ;
		}

		public static DataSet GetDataSet(Type objType ,string select_str) 
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.GetDataSet(select_str) ;
		}

		public static object GetAObject(Type objType ,string whereStr)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.GetAObject(whereStr) ;
		}

		public static object GetAObjectEspecial(Type objType ,string theID)
		{
			string whereStr = string.Format("Where ID = '{0}'" ,theID) ;

			return DataEntrance.GetAObject(objType ,whereStr) ;
		}

		public static object[] GetObjects(Type objType ,string whereStr)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.GetObjects(whereStr) ;
		}

		public static object[] GetObjectsWithoutBolb(Type objType ,string whereStr)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.GetObjectsWithoutBlob(whereStr) ;
		}

		public static bool FillBlobData(Type objType ,object obj) 
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.FillBlobData(obj) ;
		}
		#endregion

		#region Order
		#region Simplest
		public static void Insert(object obj ,IDbTransaction trans) 
		{
			Type objType = obj.GetType() ;
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			accesser.Insert(obj ,trans) ;
		}

		public static object InsertReturnIdentity(object obj ,IDbTransaction trans ) 
		{
			Type objType = obj.GetType() ;
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.InsertReturnIdentity(obj ,trans) ;
		}

		public static void InsertBatch(ArrayList objs ,IDbTransaction trans)
		{
			Type objType = objs[0].GetType() ;
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			accesser.InsertBatch(objs ,trans) ;
		}

		public static void InsertBatch(object[] objs ,IDbTransaction trans)
		{			
			ArrayList list = new ArrayList() ;
			for(int i=0 ;i<objs.Length ;i++)
			{
				list.Add(objs[i]) ;
			}

			DataEntrance.InsertBatch(list ,trans) ;
		}

		public static void Update(object obj ,IDbTransaction trans) 
		{
			Type objType = obj.GetType() ;
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			accesser.Update(obj ,trans) ;
		}
		#endregion

		#region Common
		public static void Insert(Type objType ,object obj ,IDbTransaction trans) 
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			accesser.Insert(obj ,trans) ;
		}

		public static object InsertReturnIdentity(Type objType ,object obj ,IDbTransaction trans ) 
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.InsertReturnIdentity(obj ,trans) ;
		}

		public static void InsertBatch(Type objType ,ArrayList objs ,IDbTransaction trans)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			accesser.InsertBatch(objs ,trans) ;
		}

		public static void InsertBatch(Type objType ,object[] objs ,IDbTransaction trans)
		{
			ArrayList list = new ArrayList() ;
			for(int i=0 ;i<objs.Length ;i++)
			{
				list.Add(objs[i]) ;
			}

			DataEntrance.InsertBatch(objType ,list ,trans) ;
		}

		public static void Update(Type objType ,object obj ,IDbTransaction trans) 
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			accesser.Update(obj ,trans) ;
		}
		#endregion

		#region shortCut
		public static bool UpdateFieldValue(Type objType ,object theID ,string fieldName ,object newVal ,IDbTransaction trans)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.UpdateFieldValue(theID ,fieldName ,newVal ,trans) ;
		}

		public static void Delete(Type objType ,object ID ,IDbTransaction trans)  //ID一般为int或string类型	
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			accesser.Delete(ID ,trans) ;
		}
		#endregion
		#endregion

		#region GetADOBase ,GetADOBaseEx
		public static IADOBase GetADOBase()
		{						
			string connStr = CurDbInfoMgr.GetConnString() ;
			return DataEntrance.CurElementFactory.GetADOBase(connStr) ;
		}

		public static IADOBase GetADOBaseEx(string destDbName)
		{						
			string connStr = CurDbInfoMgr.GetConnString(destDbName) ;
			return DataEntrance.CurElementFactory.GetADOBase(connStr) ;
		}		
		#endregion

		#region GetPaginationMgr
		public static IPaginationManager GetPaginationMgr(Type objType ,int page_size ,string whereStr ,string[] columns)
		{
			IDBAccesser accesser = DataEntrance.GetDestDealer(objType) ;
			return accesser.GetPaginationMgr(page_size ,whereStr ,columns) ;
		}
		#endregion

		#region GetTransactionHelper
		public static ITransactionHelper GetTransactionHelper()
		{
			string connStr = null ;
			DataBaseType dbType = CurDbInfoMgr.GetDbType() ;
			connStr = CurDbInfoMgr.GetConnString() ;
			return DataEntrance.CurElementFactory.GetTransactionHelper(connStr) ;
		}
		#endregion

		#region private
		private static IDBAccesser GetDestDealer(Type dataObjType)
		{
			return CreateDBAccesser(dataObjType) ; 	
		}

		private static void CurDbInfoMgr_DbConfigChanged(object sender, EventArgs e)
		{
			RefreshConfig() ;
		}
		#endregion		
	}

	/// <summary>
	/// IDataBaseInfoMgr 用于向DataEntrance提供数据库连接信息
	/// </summary>
	public interface IDataBaseInfoMgr
	{
		DataBaseType GetDbType() ;
		string		 GetConnString() ;
		string		 GetConnString(string dbName) ; //针对多数据库
		void		 ActivateDbConnChangeeEvent() ; //外部通知IDataBaseInfoMgr数据库连接信息发生了变化
		
		event		 EventHandler DbConfigChanged ; 

		bool         IsMultiDataBase{get ;}
	}
}
