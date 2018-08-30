using System ;
using System.Data ;

namespace DataAccess
{
	/// <summary>
	/// IADOBase 如果是简单的数据库操作，不涉及事务等，可以使用该接口。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public interface IADOBase
	{
		void DoCommand(string commandStr) ;
		DataSet DoQuery(string queryStr) ;
		void CommitDataSet(DataSet ds) ;//DataSet 必须是本IADOBase的DoQuery返回的DataSet
	}

	

}
