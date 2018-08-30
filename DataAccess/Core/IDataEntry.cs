using System;
using System.Collections ;
using System.Data ;

namespace DataAccess
{
	/// <summary>
	/// IDataEntry 数据访问基础接口。
	/// </summary>
	public interface IDataEntry
	{
		void Initialize() ;

		IDBAccesser CreateDBAccesser(Type dataClassType) ;		

		#region Order
		void	Insert(object obj ,IDbTransaction trans) ;
		object  InsertReturnIdentity(object obj ,IDbTransaction trans );
		void	InsertBatch(ArrayList objs ,IDbTransaction trans);
		void	InsertBatch(object[] objs ,IDbTransaction trans);
		void	Update(object obj ,IDbTransaction trans) ;
		bool	UpdateFieldValue(Type objType ,object theID ,string fieldName ,object newVal ,IDbTransaction trans) ;
		void	Delete(Type objType ,object ID ,IDbTransaction trans) ;
		#endregion

		#region Query
		object   GetFieldValue(Type objType ,string theID ,string fieldName) ;
		object   GetFieldValueEx(Type objType ,string whereStr ,string fieldName) ;
		DataSet	 GetDataSet(Type objType ,string select_str) ;
		object	 GetAObject(Type objType ,string whereStr) ;
		object	 GetAObjectEspecial(Type objType ,string theID) ;
		object[] GetObjects(Type objType ,string whereStr) ;
		object[] GetObjectsWithoutBolb(Type objType ,string whereStr) ;
		bool	 FillBlobData(Type objType ,object obj) ;
		#endregion

		#region Relation
		IADOBase		   GetADOBase() ;
		IPaginationManager GetPaginationMgr(Type objType ,int page_size ,string whereStr ,string[] columns) ;
		ITransactionHelper GetTransactionHelper();
		#endregion
	}

	/// <summary>
	/// DataEntry 针对一个指定的单数据库进行数据访问操作
	/// </summary>
	public class DataEntry :IDataEntry
	{
		#region members
		private DataBaseType dataBaseType = DataBaseType.SqlServer ;
		private string connString = "" ;
		private string dealerAssemName = null ;

		private IDBTypeElementFactory curElementFactory ;
		private IDBAccesserFactory    dBAccesserFactory ;
		#endregion

		#region Ctor
		public DataEntry()
		{
		}

		/// <summary>
		/// 如果Dealer位于当前程序集中，dealerAssem_Name可传入null
		/// </summary>		
		public DataEntry(DataBaseType dataBase_Type ,string conn_String ,string dealerAssem_Name)
		{
			this.dataBaseType	 = dataBase_Type ;
			this.connString		 = conn_String ;
			this.dealerAssemName = dealerAssem_Name ;
		}
		#endregion

		#region IDataEntry 成员

		#region property
		public DataBaseType DataBaseType
		{
			set
			{
				this.dataBaseType = value ;
			}
		}

		public string ConnectString
		{
			set
			{
				this.connString = value ;
			}
		}

		public string  DealerAssemName
		{
			set
			{
				this.dealerAssemName = value ;
			}
		}
		#endregion

		public void Initialize()
		{
			this.curElementFactory = DbElementFactoryGetter.GetDBTypeElementFactory(this.dataBaseType) ;
			this.dBAccesserFactory = new DBAccesserFactory() ;
			this.dBAccesserFactory.Initialize(this.dataBaseType ,this.connString ,true) ;
		}

		public IDBAccesser CreateDBAccesser(Type dataClassType)
		{		
			if((this.dealerAssemName == null) || (this.dealerAssemName == ""))
			{
				return this.dBAccesserFactory.CreateDBAccesser(dataClassType) ;
			}

			return this.dBAccesserFactory.CreateDBAccesser(this.dataBaseType ,this.connString ,dataClassType ,this.dealerAssemName);
		}	

		#region Order
		public void Insert(object obj, IDbTransaction trans)
		{
			Type objType = obj.GetType() ;
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			accesser.Insert(obj ,trans) ;
		}

		public object InsertReturnIdentity(object obj, IDbTransaction trans)
		{
			Type objType = obj.GetType() ;
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.InsertReturnIdentity(obj ,trans) ;
		}

		public void InsertBatch(ArrayList objs, IDbTransaction trans)
		{
			Type objType = objs[0].GetType() ;
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			accesser.InsertBatch(objs ,trans) ;
		}

		public void InsertBatch(object[] objs, IDbTransaction trans)
		{
			ArrayList list = new ArrayList() ;
			for(int i=0 ;i<objs.Length ;i++)
			{
				list.Add(objs[i]) ;
			}

			this.InsertBatch(list ,trans) ;
		}

		public void Update(object obj, IDbTransaction trans)
		{
			Type objType = obj.GetType() ;
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			accesser.Update(obj ,trans) ;
		}

		public bool UpdateFieldValue(Type objType, object theID, string fieldName, object newVal, IDbTransaction trans)
		{			
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.UpdateFieldValue(theID ,fieldName ,newVal ,trans) ;
		}

		public void Delete(Type objType, object ID, IDbTransaction trans)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			accesser.Delete(ID ,trans) ;
		}
		#endregion

		#region Query
		public object GetFieldValue(Type objType, string theID, string fieldName)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetFieldValue(theID ,fieldName) ;
		}

		public object GetFieldValueEx(Type objType, string whereStr, string fieldName)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetFieldValueEx(whereStr ,fieldName) ;
		}

		public DataSet GetDataSet(Type objType, string select_str)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetDataSet(select_str) ;
		}

		public object GetAObject(Type objType, string whereStr)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetAObject(whereStr) ;
		}

		public object GetAObjectEspecial(Type objType, string theID)
		{
			string whereStr = string.Format("Where ID = '{0}'" ,theID) ;
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetAObject(whereStr) ;
		}

		public object[] GetObjects(Type objType, string whereStr)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetObjects(whereStr) ;
		}

		public object[] GetObjectsWithoutBolb(Type objType, string whereStr)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetObjectsWithoutBlob(whereStr) ;
		}

		public bool FillBlobData(Type objType, object obj)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.FillBlobData(obj) ;
		}
		#endregion

		#region Relation
		public IADOBase GetADOBase()
		{			
			return this.curElementFactory.GetADOBase(this.connString) ;
		}

		public IPaginationManager GetPaginationMgr(Type objType, int page_size, string whereStr, string[] columns)
		{
			IDBAccesser accesser = this.CreateDBAccesser(objType) ;
			return accesser.GetPaginationMgr(page_size ,whereStr ,columns) ;
		}

		public ITransactionHelper GetTransactionHelper()
		{			
			return this.curElementFactory.GetTransactionHelper(this.connString) ;
		}
		#endregion

		#endregion

	}

}
