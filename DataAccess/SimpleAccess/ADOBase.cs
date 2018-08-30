using System;
using System.Data ;
using System.Data.SqlClient ;


namespace DataAccess
{
	/// <summary>
	/// 如果是简单的数据库操作，不涉及事务等，可以使用这个简单类
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public abstract class ADOBase : IADOBase  ,IDBTypeElementFactoryGetter
	{
		private IDBTypeElementFactory dbElementFactory ;
		private IDbCommand command ;
		private IDbConnection connection ;
		private IDbDataAdapter adapter ;		

		public ADOBase(string connectStr)
		{
			this.dbElementFactory = this.GetDBTypeElementFactory() ;

			this.connection			= this.dbElementFactory.GetConnection(connectStr) ;
			this.command			= this.dbElementFactory.GetCommand() ;
			this.command.Connection = this.connection ;
			this.adapter			= this.dbElementFactory.GetDataAdapter() ;			
		}

		#region IADOBase 成员

		public void DoCommand(string commandStr)
		{
			this.command.CommandText = commandStr ;
			try
			{				
				this.connection.Open() ;
				this.command.ExecuteNonQuery() ;
			}
			finally
			{
				this.connection.Close() ;
			}
		}

		public DataSet DoQuery(string queryStr)
		{
			this.command.CommandText = queryStr ;
			this.adapter.SelectCommand = this.command ;
			DataSet data_set_result = new DataSet() ;
			
			this.adapter.Fill(data_set_result)  ;
			return data_set_result ;
		}

		//注意：this.ds_groupInfo.Tables[0].Rows[row_num].Delete();语句将第row_num行标记为“delete” 。
		//   而 this.ds_groupInfo.Tables[0].Rows.RemoveAt(row_num) ;将彻底从DataTable中删除行。这样
		//   在 adapter.Update(ds)时就不会对数据库中的对应行作delete操作 
		//   所以如果要使用adapter更新数据库，在对DataTable中的记录进行删除操作时，请采用Rows[row_num].Delete()，
		//   而不是Rows.RemoveAt(row_num)
		public void CommitDataSet(DataSet ds)
		{
			//根据adapter的select语句自动产生其它sql语句
			this.dbElementFactory.BuildCommandForAdapter(this.adapter) ;			
			this.adapter.Update(ds) ;
			ds.AcceptChanges() ;	
		}
		#endregion

		#region IDBTypeAssistantGetter 成员

		public abstract IDBTypeElementFactory GetDBTypeElementFactory() ;

		#endregion
	}

}
