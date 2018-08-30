using System;
using System.Collections ;
using System.Data;

namespace DataAccess
{
	/// <summary>
	/// IDBAccesser 是最核心的数据访问接口	
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public interface IDBAccesser :IDBAccesserQuery ,IDBAccesserOrder ,IDBAccesserRelation
	{
		//property
		string		 ConnectString{get ;}
		string		 DbTableName {get ;}
		DataBaseType DataBaseType{get ;}			

		//others
		IPaginationManager GetPaginationMgr(int page_size ,string whereStr ,string[] columns ) ;
	}

	/// <summary>
	/// IDBAccesserRelation 为执行sql关系型接口
	/// </summary>
	public interface IDBAccesserRelation
	{
		//RelationAction
		int         GetRecordsCount() ;//得到表中记录的总数
		object      GetFieldValue  (string theID ,string fieldName) ;
		object		GetFieldValueEx(string whereStr ,string fieldName) ;
		bool        UpdateFieldValue(object theID ,string fieldName ,object newVal ,IDbTransaction trans) ;
		object      ExecuteScalar(string command) ;
		IDataReader GetReader(string select_str) ; //IDataReader用完后要及时关闭
	}

	/// <summary>
	/// IDBAccesserQuery 为执行sql查询接口
	/// </summary>
	public interface IDBAccesserQuery
	{
		bool	 ReviseAObject(string where_str ,object target )  ;//使用数据库内容来更新当前对象
		object   GetAObject(string where_str) ;//if there is no condition clause ,please input ""
		object[] GetObjects(string where_str) ;		
		object[] GetObjectsWithoutBlob(string where_str) ; //获取的对象中凡是Blob字段都未填充
		bool	 FillBlobData(object obj) ;				   //填充某个对象的所有Blob字段
		DataSet  GetDataSet(string select_str) ;	
	}

	/// <summary>
	/// IDBAccesserOrder 为执行sql命令接口
	/// </summary>
	public interface IDBAccesserOrder
	{
		//如果不需要事务，trans以null传入
		void Insert(object obj ,IDbTransaction trans) ;
		void Update(object obj ,IDbTransaction trans) ;		
		void Delete(object ID ,IDbTransaction trans) ; //ID一般为int或string类型	
		void InsertBatch(ArrayList objs ,IDbTransaction trans) ;//批插入

		//插入对象并返回自动编号标志
		object InsertReturnIdentity(object obj ,IDbTransaction trans) ;
	}	
}
