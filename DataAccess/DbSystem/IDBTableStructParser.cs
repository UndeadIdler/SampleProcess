using System;
using System.Collections ;
using System.Data;
using System.Collections.Generic;

namespace DataAccess
{
	/// <summary>
	/// IDBTableStructParser 用于展开某个数据库中任意一个表的详细结构信息。
	/// zhuweisky
	/// </summary>
	public interface IDBTableStructParser
	{
		void		  Initialize(string connStr ,DataBaseType dbType) ;				
		DBTableDetail GetTableStruct(string tableName) ;

		DBTableDetailEx GetTableStructEx(string tableName) ;
	}

	public class DBTableStructParser :IDBTableStructParser
	{
		private string connectionStr ;
		private IADOBase adoBase ;
		private IDBTypeElementFactory dbEleFactory ;
		
		#region IDBTableStructParser 成员

		#region Initialize
		public void Initialize(string connStr ,DataBaseType dbType)
		{
			this.connectionStr = connStr ;
			this.dbEleFactory  = DbElementFactoryGetter.GetDBTypeElementFactory(dbType) ;
			this.adoBase       = this.dbEleFactory.GetADOBase(connStr) ;
		}		
		#endregion	

		#region GetTableStruct
		//使用ADO.NET的GetSchemaTable方法
		public DBTableDetail GetTableStruct2(string tableName)
		{
			DataSet ds = new DataSet(); 
			IDbCommand cmd     = this.dbEleFactory.GetCommand();
			cmd.Connection     = this.dbEleFactory.GetConnection(this.connectionStr) ;				
			cmd.CommandText    = string.Format("Select * from {0}" ,tableName) ;

			cmd.Connection.Open() ;	
			IDataReader read = cmd.ExecuteReader(); 
			DataTable tb = read.GetSchemaTable();//注意这句话，得到表的架构信息 			
			read.Close(); 
			cmd.Connection.Close(); 
			
			DBTableDetail tableDetail = new DBTableDetail() ;
			tableDetail.TableName	  = tableName ;
			tableDetail.Columns       = new DBColumnInfo[tb.Rows.Count] ;		

			for(int i=0 ;i<tb.Rows.Count ;i++)
			{
				tableDetail.Columns[i] = new DBColumnInfo() ;
				tableDetail.Columns[i].ColumnName   = tb.Rows[i]["ColumnName"].ToString() ;
				tableDetail.Columns[i].ColumnType   = DBTypeConverter.ConvertCsTypeToOriginDBType(tb.Rows[i]["DataType"].ToString()) ;
				
				if(tableDetail.Columns[i].ColumnType.ToString() == "image")
				{
					tableDetail.Columns[i].Length = 16 ;
				}
				else
				{
					tableDetail.Columns[i].Length   = int.Parse(tb.Rows[i]["ColumnSize"].ToString()) ;
				}

				tableDetail.Columns[i].AllowNull    = bool.Parse(tb.Rows[i]["AllowDBNull"].ToString()) ;								
				tableDetail.Columns[i].IsAutoID     = bool.Parse(tb.Rows[i]["IsAutoIncrement"].ToString()) ;
				tableDetail.Columns[i].Description  = "" ;
				tableDetail.Columns[i].DefaultValue = "" ;
			}

			this.SetPKeys(tableDetail ,tableName) ;
			return tableDetail ;
		}

		//使用系统存储过程
		public DBTableDetail GetTableStruct(string tableName)
		{			
			IADOBase adoBase = new SqlADOBase(this.connectionStr);
			DataSet ds = adoBase.DoQuery("sp_columns " + tableName) ;
			DataTable tb = ds.Tables[0] ;

			DBTableDetail tableDetail = new DBTableDetail() ;
			tableDetail.TableName	  = tableName ;
			tableDetail.Columns       = new DBColumnInfo[tb.Rows.Count] ;		

			for(int i=0 ;i<tb.Rows.Count ;i++)
			{
				tableDetail.Columns[i] = new DBColumnInfo() ;
				tableDetail.Columns[i].ColumnName   = tb.Rows[i]["Column_Name"].ToString() ;
				tableDetail.Columns[i].ColumnType   = tb.Rows[i]["Type_Name"].ToString() ;
				if(tableDetail.Columns[i].ColumnType == "int identity")
				{
					tableDetail.Columns[i].ColumnType = "int" ;
				}

				tableDetail.Columns[i].Length		= int.Parse(tb.Rows[i]["Length"].ToString()) ;
				
				tableDetail.Columns[i].AllowNull    = (tb.Rows[i]["Nullable"].ToString() == "1") ;
				tableDetail.Columns[i].IsAutoID     = (tb.Rows[i]["SS_DATA_TYPE"].ToString() == "56") ;
				tableDetail.Columns[i].Description  = tb.Rows[i]["Remarks"].ToString();

				if((tableDetail.Columns[i].Description == null) || (tableDetail.Columns[i].Description == ""))
				{
					tableDetail.Columns[i].Description = tableDetail.Columns[i].ColumnName ;
				}

				string dv = tb.Rows[i]["Column_def"].ToString() ;
				if(dv == "")
				{
					tableDetail.Columns[i].DefaultValue = "" ;
				}
				else
				{
					string[] str   = dv.Split(new char[]{'(',')'}) ;

					string[] parts = str[1].Split('\'') ;
					if(parts.Length >= 2)
					{					
						tableDetail.Columns[i].DefaultValue = parts[1] ;
					}
					else
					{
						if(str.Length > 0)
						{
							tableDetail.Columns[i].DefaultValue = str[1] ;
						}
					}

				}
			}
			
			this.SetPKeys(tableDetail ,tableName) ;
			return tableDetail ;
		}

		private void SetPKeys(DBTableDetail tableDetail ,string tableName)
		{
			IADOBase adoBase = new SqlADOBase(this.connectionStr);
			DataSet ds = adoBase.DoQuery("sp_pkeys " + tableName) ;
			for(int i=0 ;i<ds.Tables[0].Rows.Count ;i++)
			{
				foreach(DBColumnInfo colInfo in tableDetail.Columns)
				{
					if(colInfo.ColumnName == ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString())
					{
						colInfo.IsPkey = true ;
						break ;
					}
				}
			}
		}
		#endregion

		#region GetTableStructEx
		public DBTableDetailEx GetTableStructEx(string tableName)
		{
			DBTableDetail detail = this.GetTableStruct(tableName) ;
			
			DBTableDetailEx detailEx = new DBTableDetailEx() ;
			detailEx.TableName = tableName ;
			detailEx.TableDescription = tableName + "表没有描述。" ;

			detailEx.dtColumns = DBTableStructParser.CreateDBTableColumnsStruct() ;

			for(int i=0 ;i<detail.Columns.Length ;i++)
			{
				DataRow row = detailEx.dtColumns.NewRow() ;
				row[0]      = detail.Columns[i].ColumnName ;
				row[1]      = detail.Columns[i].ColumnName ;
				row[2]      = detail.Columns[i].Description ;
				row[3]      = detail.Columns[i].ColumnType ;
				row[4]      = detail.Columns[i].Length ;
				row[5]      = detail.Columns[i].IsPkey ;
				row[6]      = detail.Columns[i].IsAutoID ;
				row[7]      = detail.Columns[i].DefaultValue ;

				detailEx.dtColumns.Rows.Add(row) ;
			}

			return detailEx ;
		}
		
		#endregion
		#endregion

		#region CreateDBTableColumnsStruct
		public static DataTable CreateDBTableColumnsStruct()//Name,ChineseName ,Type ,Description ,Length ,IsPKey ,AutoID ,DefaultValue
		{
			DataTable dt = new DataTable() ;

			dt.Columns.Add(new DataColumn("Name",Type.GetType("System.String"))) ;
			dt.Columns.Add(new DataColumn("ChineseName",Type.GetType("System.String"))) ; //added 2005.01.28
			dt.Columns.Add(new DataColumn("Description",Type.GetType("System.String"))) ; //added 2005.12.22
			dt.Columns.Add(new DataColumn("Type",Type.GetType("System.String"))) ;
			dt.Columns.Add(new DataColumn("Length",Type.GetType("System.Int32"))) ;
			dt.Columns.Add(new DataColumn("IsPKey",Type.GetType("System.Boolean"))) ;
			dt.Columns.Add(new DataColumn("AutoID",Type.GetType("System.Boolean"))) ;
			dt.Columns.Add(new DataColumn("DefaultValue",Type.GetType("System.String"))) ;

			return dt ;
		}
		#endregion

	}


	#region DBTableDetailEx
	[Serializable]
	public class DBTableDetailEx
	{
		public string TableName ;
		public string TableDescription ;
		public DataTable dtColumns ;//Name,ChineseName ,Description ,Type ,Length ,IsPKey ,AutoID ,DefaultValue
	}

	#endregion

	#region DBTableDetail ,DBColumnInfo
	[Serializable]
	public class DBTableDetail
	{
		public string TableName ;
		public DBColumnInfo[] Columns ;
	}

	[Serializable]
	public class DBColumnInfo
	{		
		public string ColumnName;		
		public string Description;
		public string ColumnType;
		public int    Length;
		public bool   IsPkey = false;
		public bool   IsAutoID;
		public bool   AllowNull;
		public string DefaultValue;
	}
	#endregion
}
