using System;
using System.Collections;
using System.Text ;
using System.Data;

namespace DataAccess
{
	/// <summary>
	/// DBSystemHelper 用于提供数据库系统自身的信息，并提供对数据库自身的操作。目前主要针对SqlServer数据库。
	/// 朱伟 2005.08.17
	/// </summary>
	public class DBSystemHelper
	{
		public DBSystemHelper()
		{			
		}

		#region GetAllDbSysInfos ,IsDbExist ,GetDbSysInformation
		/// <summary>
		/// 得到所有数据库的系统信息
		/// </summary>		
		public static DbSysInformation[] GetAllDbSysInfos(string dbIP , string user ,string pwd)
		{
			string connStr = string.Format("Server = {0} ;User = {1} ;Pwd = {2}" ,dbIP ,user ,pwd) ;
			IADOBase adoBase = new SqlADOBase(connStr);
			
			try
			{
				DataSet ds = adoBase.DoQuery("sp_helpdb") ;
				DbSysInformation[] dbInfos = new DbSysInformation[ds.Tables[0].Rows.Count] ;
				for(int i=0; i<dbInfos.Length ;i++)
				{
					dbInfos[i]		 = new DbSysInformation() ;
					dbInfos[i].ID    = int.Parse(ds.Tables[0].Rows[i]["DbID"].ToString()) ;
					dbInfos[i].Name  = ds.Tables[0].Rows[i]["Name"].ToString() ;
					dbInfos[i].Owner = ds.Tables[0].Rows[i]["Owner"].ToString() ;
					dbInfos[i].TimeCreated = DateTime.Parse(ds.Tables[0].Rows[i]["Created"].ToString()) ;
				}

				return dbInfos ;
			}
			catch(Exception ee)
			{
				throw ee ;
			}
		}
		
		/// <summary>
		/// 判断某个数据库是否存在
		/// </summary>		
		public static bool IsDbExist(string dbIP , string user ,string pwd ,string dbName)
		{
			DbSysInformation[] dbInfos = DBSystemHelper.GetAllDbSysInfos(dbIP ,user ,pwd) ;
			for(int i=0 ;i<dbInfos.Length ;i++)
			{
				if(dbInfos[i].Name == dbName)
				{
					return true ;
				}
			}

			return false ;
		}
		
		/// <summary>
		/// 得到指定数据库的系统信息
		/// </summary>		
		public static DbSysInformation GetDbSysInformation(string dbIP , string user ,string pwd ,string dbName)
		{
			DbSysInformation[] dbInfos = DBSystemHelper.GetAllDbSysInfos(dbIP ,user ,pwd) ;
			for(int i=0 ;i<dbInfos.Length ;i++)
			{
				if(dbInfos[i].Name == dbName)
				{
					return dbInfos[i] ;
				}
			}

			return null ;
		}
		#endregion

		#region GetAllUserTableNames ,IsTableExist
		/// <summary>
		/// 得到指定数据库中所有用户表的名字
		/// </summary>		
		public static string[] GetAllUserTableNames(string connStr)
		{
			IADOBase adoBase = new SqlADOBase(connStr);

			string query = "select name from sysobjects where OBJECTPROPERTY(id ,'IsUserTable')=1" ;
			DataSet ds = adoBase.DoQuery(query) ;

			ArrayList list = new ArrayList() ;
			for(int i=0 ;i<ds.Tables[0].Rows.Count ;i++)
			{	
				string name = ds.Tables[0].Rows[i][0].ToString() ;
				if(name.ToLower() != "dtproperties")
				{
					list.Add(name) ;
				}
			}

			string[] names = new string[list.Count] ;

			for(int i=0 ;i<names.Length ;i++)
			{
				names[i] = list[i].ToString() ;
			}

			return names ;
		}
		
		/// <summary>
		/// 判断特定的数据库中是否存在某个表
		/// </summary>		
		public static bool IsTableExist(string connStr ,string tableName)
		{
			string[] names = DBSystemHelper.GetAllUserTableNames(connStr) ;
			for(int i=0 ;i<names.Length ;i++)
			{
				if(names[i] == tableName)
				{
					return true ;
				}
			}

			return false ;
		}
		#endregion

		#region GetTableStruct ,GetTableStructEx
		public static DBTableDetail GetTableStruct(string connStr ,string tableName ,DataBaseType dbType)
		{
			IDBTableStructParser dbParser = new DBTableStructParser() ;
			dbParser.Initialize(connStr ,dbType) ;
			return dbParser.GetTableStruct(tableName) ;
		}

		public static DBTableDetailEx GetTableStructEx(string connStr ,string tableName ,DataBaseType dbType)
		{
			IDBTableStructParser dbParser = new DBTableStructParser() ;
			dbParser.Initialize(connStr ,dbType) ;
			return dbParser.GetTableStructEx(tableName) ;
		}
		#endregion

		#region CreateDb ,RemoveDb
		/// <summary>
		/// 创建一个新的数据库
		/// </summary>		
		public static void CreateDb(string dbIP , string user ,string pwd ,string newDbName)
		{
			string connStr = string.Format("Server = {0} ;User = {1} ;Pwd = {2}" ,dbIP ,user ,pwd) ;
			IADOBase adoBase = new SqlADOBase(connStr);

			string command = string.Format("Create database {0}" ,newDbName ) ;
			adoBase.DoCommand(command) ;
		}
		
		/// <summary>
		/// 删除指定数据库
		/// </summary>		
		public static void RemoveDb(string dbIP , string user ,string pwd ,string dbName)
		{
			string connStr = string.Format("Server = {0} ;User = {1} ;Pwd = {2}" ,dbIP ,user ,pwd) ;
			IADOBase adoBase = new SqlADOBase(connStr);

			string command = string.Format("Drop database {0}" ,dbName ) ;
			adoBase.DoCommand(command) ;
		}
		#endregion

		#region BackUpDb ,RestoreDb
		/// <summary>
		/// 备份指定数据库
		/// </summary>		
		public static void BackUpDb(string dbIP , string user ,string pwd ,string dbName ,string bakFilePath)
		{
			string connStr = string.Format("Server = {0} ;User = {1} ;Pwd = {2}" ,dbIP ,user ,pwd) ;
			IADOBase adoBase = new SqlADOBase(connStr);

			string command = string.Format("use {0} backup database {0} to disk = '{1}'" ,dbName ,bakFilePath) ;
			adoBase.DoCommand(command) ;
		}

		/// <summary>
		/// 还原指定数据库
		/// </summary>	
		public static void RestoreDb(string dbIP , string user ,string pwd ,string bakFilePath ,string dbName )
		{
			string connStr = string.Format("Server = {0} ;User = {1} ;Pwd = {2}" ,dbIP ,user ,pwd) ;
			IADOBase adoBase = new SqlADOBase(connStr);

			string command = string.Format("use {0} restore database {0} from disk = '{1}'" ,dbName ,bakFilePath) ;
			adoBase.DoCommand(command) ;
		}
		#endregion

		#region CreateTable ,RemoveTable		
		/// <summary>
		/// 在数据库中创建指定表
		/// </summary>		
		public void CreateTable(string connStr ,DBTableDetail tableInfo)
		{
			//形成SQL语句
			StringBuilder strBuilder = new StringBuilder() ;
			string str_create = string.Format("Create Table {0}" ,tableInfo.TableName) ;
			strBuilder.Append(str_create) ;
			strBuilder.Append(" (") ;

			int start =0 ;
			if(tableInfo.Columns[0].ColumnType == "int")
			{				
				if(tableInfo.Columns[0].IsAutoID)
				{
					strBuilder.Append(string.Format("{0} int PRIMARY KEY IDENTITY ," ,tableInfo.Columns[0].ColumnName)) ;
					start =1 ;
				}
			}			
			
			for(int i=start ;i<tableInfo.Columns.Length ;i++)
			{
				bool length_fixed = DBSystemHelper.DBType_lengthFixed(tableInfo.Columns[i].ColumnType) ;
				string item ;
				if(length_fixed)
				{
					item = string.Format("{0} {1} " ,tableInfo.Columns[i].ColumnName ,tableInfo.Columns[i].ColumnType) ;
				}
				else
				{
					item = string.Format("{0} {1}({2}) " ,tableInfo.Columns[i].ColumnName ,tableInfo.Columns[i].ColumnType) ;
				}
			
				if((tableInfo.Columns[i].DefaultValue != null) && (tableInfo.Columns[i].DefaultValue != ""))
				{
					item += string.Format("DEFAULT {0}" ,tableInfo.Columns[i].DefaultValue) ;
				}
				
				if(tableInfo.Columns[i].IsPkey)
				{
					item += "PRIMARY KEY" ;
				}

				if(i != tableInfo.Columns.Length-1)
				{
					item += " , " ;
				}

				strBuilder.Append(item) ;
				
			}
			
			strBuilder.Append(" )") ;		
	
			//插入数据库
			IADOBase adoBase = new SqlADOBase(connStr);			
			adoBase.DoCommand(strBuilder.ToString()) ;			
		}

		/// <summary>
		/// 删除数据库中的指定表
		/// </summary>
		public void RemoveTable(string connStr ,string tableName)
		{
			IADOBase adoBase = new SqlADOBase(connStr);
			string deleteStr = string.Format("Drop Table {0}" ,tableName) ;
			adoBase.DoCommand(deleteStr) ;
		}

		private static bool DBType_lengthFixed(string DB_type)
		{
			if(DB_type == "nvarchar" || DB_type == "varbinary")
			{
				return false ;
			}
			
			return true ;
		}
		#endregion
	}

	/// <summary>
	/// 某个数据库的系统信息
	/// </summary>
	public class DbSysInformation
	{
		public int    ID    ;
		public string Name = "";
		public string Owner ;
		public DateTime TimeCreated ;	
	
		public override string ToString()
		{
			return this.Name ;
		}

	}
}
