using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient ;
using System.Data.OracleClient ;

namespace DataAccess
{
	/// <summary>
	/// DBTypeConverter 主要用于（SqlServer原始数据类型、C#数据库类型、C#类型）之间的相互转换 。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// 2004.04.18
	/// </summary>
	public sealed class DBTypeConverter
	{
		public DBTypeConverter()
		{			
		}

		#region ConvertOriginDBTypeToCsType  // DBTypeToCsType
	
		#region ConvertOriginDBTypeToCsType  
		public static string ConvertOriginDBTypeToCsType(string origin_dbType)
		{
			switch(origin_dbType)
			{					
				case "bit":
				{
					return "bool" ;					
				}
				case "datetime":
				{
					return "DateTime" ;		
				}
				case "decimal":
				{
					return "decimal" ;		
				}
				case "float":
				{
					return "float" ;	
				}
				case "image":
				{
					return "byte[]" ;	
				}
				case "int":
				{
					return "int" ;	
				}
				case "money":
				{
					return "decimal" ;	
				}
				case "numeric":
				{
					return "decimal" ;	
				}
				case "nvarchar":
				{
					return "string" ;	
				}
				case "varchar":
				{
					return "string" ;	
				}
				case "smallint":
				{
					return "short" ;	
				}
				case "tinyint":
				{
					return "byte" ;	
				}
				case "text":
				{
					return "string" ;	
				}
				case "varbinary":
				{
					return "byte[]" ;	
				}
				default:
				{
					throw new Exception("DB type error !") ;
					
				}

			}
		}

		#endregion  

		public static string[] ConvertOriginDBTypeToCsType(string[] origin_dbTypes)
		{
			int num = origin_dbTypes.Length ;
			string[] results = new string[num] ;

			for(int i=0 ;i<num ;i++)
			{
				results[i] = ConvertOriginDBTypeToCsType(origin_dbTypes[i]) ;
			}

			return results ;
		}
		#endregion  
  
		#region ConvertCsTypeToOriginDBType
		//CsTypeToDBType
		public static string ConvertCsTypeToOriginDBType(string csType)
		{
			switch(csType)
			{					
				case "bool" :
				{
					return "bit" ;					
				}
				case "DateTime" :
				{
					return "datetime" ;		
				}
				case "decimal" :
				{
					return "decimal" ;		
				}
				case "float" :
				{
					return "float" ;	
				}
				case "byte[]" :
				{
					return "image" ;	
				}
				case "int" :
				{
					return "int" ;	
				}				
				case "string" :
				{
					return "nvarchar" ;	
				}
				case "short" :
				{
					return "smallint" ;	
				}
				case "byte" :
				{
					return "tinyint" ;	
				}	

				case "System.Boolean":
				{
					return "bit" ;					
				}
				case "System.DateTime":
				{
					return "datetime" ;		
				}
				case "System.Decimal":
				{
					return "decimal" ;		
				}
				case "System.Single":
				{
					return "float" ;	
				}
				case "System.Double" :
				{
					return "float" ;	
				}
				case "System.Byte[]":
				{
					return "image" ;	
				}
				case "System.Int32":
				{
					return "int" ;	
				}				
				case "System.String":
				{
					return "nvarchar" ;	
				}
				case "System.Int16":
				{
					return "smallint" ;	
				}
				case "System.Byte":
				{
					return "tinyint" ;	
				}			
				default:
				{
					throw new Exception("DB type error !") ;					
				}

			}
		}
		#endregion
		
		#region ConvertOriginDBTypeToCsDBType 将数据库类型转换为C#中的DBType类型 ,如 "SqlDbType.Bit" 
		public static string ConvertOriginDBTypeToCsDBType(string origin_dbType ,DataBaseType dbType)
		{
			if(dbType == DataBaseType.SqlServer)
			{
				#region SqlServer
				switch(origin_dbType)
				{					
					case "bit":
					{
						return "SqlDbType.Bit" ;					
					}
					case "datetime":
					{
						return "SqlDbType.DateTime" ;		
					}
					case "decimal":
					{
						return "SqlDbType.Decimal" ;		
					}
					case "float":
					{
						return "SqlDbType.Float" ;	
					}
					case "image":
					{
						return "SqlDbType.Image" ;	
					}
					case "int":
					{
						return "SqlDbType.Int" ;	
					}
					case "money":
					{
						return "SqlDbType.Money" ;	
					}
					case "numeric":
					{
						return "SqlDbType.Int" ;	
					}
					case "nvarchar":
					{
						return "SqlDbType.NVarChar" ;							
					}
					case "varchar":
					{
						return "SqlDbType.VarChar" ;							
					}
					case "smallint":
					{
						return "SqlDbType.SmallInt" ;	
					}
					case "tinyint":
					{
						return "SqlDbType.TinyInt" ;	
					}
					case "text":
					{
						return "SqlDbType.Text" ;	
					}
					case "varbinary":
					{
						return "SqlDbType.VarBinary" ;	
					}
					default:
					{
						throw new Exception("DB type error !") ;
					
					}
				}
				#endregion
			} 
			else if(dbType == DataBaseType.Ole)
			{
				#region Ole
				switch(origin_dbType)
				{					
					case "bit":
					{
						return "OleDbType.Boolean" ;					
					}
					case "datetime":
					{
						return "OleDbType.Date" ;		
					}
					case "decimal":
					{
						return "OleDbType.Decimal" ;		
					}
					case "float":
					{
						return "OleDbType.Decimal" ;	
					}
					case "image":
					{
						return "OleDbType.Variant" ;	
					}
					case "int":
					{
						return "OleDbType.Integer" ;	
					}
					case "money":
					{
						return "OleDbType.Decimal" ;	
					}
					case "numeric":
					{
						return "OleDbType.Integer" ;	
					}
					case "nvarchar":
					{
						return "OleDbType.VarChar" ;	
					}
					case "varchar":
					{
						return "OleDbType.VarChar" ;								
					}
					case "smallint":
					{
						return "OleDbType.SmallInt" ;	
					}
					case "tinyint":
					{
						return "OleDbType.TinyInt" ;	
					}
					case "text":
					{
						return "OleDbType.Variant" ;	
					}
					case "varbinary":
					{
						return "OleDbType.VarBinary" ;	
					}
					default:
					{
						throw new Exception("DB type error !") ;
					
					}
				}
				#endregion
			}
			else if(dbType == DataBaseType.Oracle)
			{
				#region Oracle
				switch(origin_dbType)
				{					
					case "bit":
					{
						return "OracleType.Number" ;					
					}
					case "datetime":
					{
						return "OracleType.DateTime" ;		
					}
					case "decimal":
					{
						return "OracleType.Float" ;		
					}
					case "float":
					{
						return "OracleType.Float" ;	
					}
					case "image":
					{
						return "OracleType.Blob" ;	
					}
					case "int":
					{
						return "OracleType.Int32" ;	
					}
					case "money":
					{
						return "OracleType.Float" ;	
					}
					case "numeric":
					{
						return "OracleType.Number" ;	
					}
					case "nvarchar":
					{
						return "OracleType.NVarChar" ;	
					}
					case "varchar":
					{
						return "OracleType.NChar" ;								
					}
					case "smallint":
					{
						return "OracleType.Int16" ;	
					}
					case "tinyint":
					{
						return "OracleType.Byte" ;	
					}
					case "text":
					{
						return "OracleType.LongVarChar" ;	
					}
					case "varbinary":
					{
						return "OracleType.LongRaw" ;	
					}
					default:
					{
						throw new Exception("DB type error !") ;
					
					}
				}
				#endregion
			}
			else
			{
				throw new Exception("DataBaseType error !") ;	
			}
          

		}
		#endregion

		#region GetOleDbTypeByOriginName ,GetSqlDbTypeByOriginName ,GetOracleTypeByOriginName
		#region GetOleDbTypeByOriginName
		public static OleDbType GetOleDbTypeByOriginName(string origin_dbType)
		{
			switch(origin_dbType)
			{					
				case "bit":
				{
					return OleDbType.Boolean ;					
				}
				case "datetime":
				{
					return OleDbType.Date ;		
				}
				case "decimal":
				{
					return OleDbType.Decimal ;		
				}
				case "float":
				{
					return OleDbType.Decimal ;	
				}
				case "image":
				{
					return OleDbType.Variant ;	
				}
				case "int":
				{
					return OleDbType.Integer ;	
				}
				case "money":
				{
					return OleDbType.Decimal ;	
				}
				case "numeric":
				{
					return OleDbType.Integer ;	
				}
				case "nvarchar":
				{
					return OleDbType.VarChar ;	
				}
				case "smallint":
				{
					return OleDbType.SmallInt ;	
				}
				case "tinyint":
				{
					return OleDbType.TinyInt ;	
				}
				case "text":
				{
					return OleDbType.Variant ;	
				}
				case "varbinary":
				{
					return OleDbType.VarBinary ;	
				}
				default:
				{
					throw new Exception("DB type error !") ;					
				}
			}
		}
		#endregion
    
		#region GetSqlDbTypeByOriginName
		public static SqlDbType GetSqlDbTypeByOriginName(string origin_dbType)
		{
			switch(origin_dbType)
			{					
				case "bit":
				{
					return SqlDbType.Bit ;					
				}

				case "datetime":
				{
					return SqlDbType.DateTime ;		
				}
				case "decimal":
				{
					return SqlDbType.Decimal ;		
				}
				case "float":
				{
					return SqlDbType.Float ;	
				}
				case "image":
				{
					return SqlDbType.Image ;	
				}
				case "int":
				{
					return SqlDbType.Int ;	
				}
				case "money":
				{
					return SqlDbType.Money ;	
				}
				case "numeric":
				{
					return SqlDbType.Int ;	
				}
				case "nvarchar":
				{
					return SqlDbType.NVarChar ;	
				}
				case "smallint":
				{
					return SqlDbType.SmallInt ;	
				}
				case "tinyint":
				{
					return SqlDbType.TinyInt ;	
				}
				case "text":
				{
					return SqlDbType.Text ;	
				}
				case "varbinary":
				{
					return SqlDbType.VarBinary ;	
				}
				default:
				{
					throw new Exception("DB type error !") ;					
				}
			}
		}
		#endregion

		#region GetOracleTypeByOriginName
		public static OracleType GetOracleTypeByOriginName(string origin_dbType)
		{
			switch(origin_dbType)
			{					
				case "bit":
				{
					return OracleType.Number ;					
				}
				case "datetime":
				{
					return OracleType.DateTime ;		
				}
				case "decimal":
				{
					return OracleType.Float;		
				}
				case "float":
				{
					return OracleType.Float ;	
				}
				case "image":
				{
					return OracleType.Blob ;	
				}
				case "int":
				{
					return OracleType.Int32 ;	
				}
				case "money":
				{
					return OracleType.Float ;	
				}
				case "numeric":
				{
					return OracleType.Number ;	
				}
				case "nvarchar":
				{
					return OracleType.NVarChar ;	
				}
				case "smallint":
				{
					return OracleType.Int16 ;	
				}
				case "tinyint":
				{
					return OracleType.Byte ;	
				}
				case "text":
				{
					return OracleType.LongVarChar ;	
				}
				case "varbinary":
				{
					return OracleType.LongRaw ;	
				}
				default:
				{
					throw new Exception("DB type error !") ;					
				}
			}
		}
		#endregion
		#endregion

		#region GetOriginDBTypeLength
		public static int GetOriginDBTypeLength(string dbType)
		{
			switch(dbType)
			{
				case "bit":
				{
					return 1 ;
				}
				case "datetime":
				{
					return 8 ;
				}
				case "decimal":
				{
					return 9 ;
				}
				case "float":
				{
					return 8 ;
				}
				case "image":
				{
					return 16 ;
				}
				case "int":
				{
					return 4 ;
				}
				case "money":
				{
					return 8 ;
				}
				case "numeric":
				{
					return 9 ;
				}
				case "nvarchar":
				{
					return 50 ;
				}
				case "smallint":
				{
					return 2 ;
				}
				case "tinyint":
				{
					return 1 ;
				}
				case "text":
				{
					return 16 ;
				}
				case "varbinary":
				{
					return 50 ;
				}
				default:
				{
					return 50 ;
				}

			}
		}
		#endregion
		                                                              
		#region 根据类型名称得到该类型的默认值 －－GetDefaultValue
		public static string GetDefaultValue(string s_type)
		{
			if(DBTypeConverter.IsSubstringOf("[]" ,s_type)) //数组为引用类型
			{
				return "null" ;
			}
			else if(DBTypeConverter.IsSubstringOf("string" ,s_type)) 
			{
				return "\"\"" ;
			}
			else if(DBTypeConverter.IsSubstringOf("int" ,s_type) || DBTypeConverter.IsSubstringOf("int" ,s_type)   ||DBTypeConverter.IsSubstringOf("decimal" ,s_type) 
				|| DBTypeConverter.IsSubstringOf("float" ,s_type) ||DBTypeConverter.IsSubstringOf("double" ,s_type)
				|| DBTypeConverter.IsSubstringOf("short" ,s_type) ||DBTypeConverter.IsSubstringOf("byte" ,s_type))
			{
				return "0" ;
			}
			else if(DBTypeConverter.IsSubstringOf("bool" ,s_type))
			{
				return "false" ;
			}
			else if(DBTypeConverter.IsSubstringOf("DateTime" ,s_type))
			{
				return "DateTime.Now" ;
			}
			else
			{
				return "null" ;
			}
		}

		private static bool IsSubstringOf(string inner_str ,string outter_str)
		{
			int index =  outter_str.IndexOf(inner_str) ;
			
			return (index >-1) ;
		}
		#endregion			
	}
}
