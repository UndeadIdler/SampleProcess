using System;
using System.EnterpriseServices ;

namespace DataAccess.Distributed
{
	/// <summary>
	/// EsTransactionScope 用于支持分布式事务（可跨数据库）。
	/// 通过 using( EsTransactionScope ts = new EsTransactionScope())使用EsTransactionScope类。
	/// 注：此种方案来自Florin Lazar，http://blogs.msdn.com/florinlazar/archive/2004/07/24/194199.aspx
	/// </summary>
	public class EsTransactionScope : IDisposable
	{
		//提交事务时，设置为true		
		private bool consistent = false;

		#region ctor
		public EsTransactionScope()
		{  
			this.EnterTxContext(TransactionOption.Required);
		} 

		public EsTransactionScope(TransactionOption txOption)
		{
			this.EnterTxContext(txOption);
		}

		//进入事务上下文
		private void EnterTxContext(TransactionOption txOption)
		{
			ServiceConfig config = new ServiceConfig(); 
			config.Transaction = txOption; 
			ServiceDomain.Enter(config);   
		} 
		#endregion

		#region Dispose 取消事务或完成事务
		public void Dispose()
		{     
			if(!this.consistent)
			{
				//取消事务
				ContextUtil.SetAbort();
			}

			ServiceDomain.Leave();
		}
		#endregion
 
		#region Complete 提交事务
		//在事务方法执行后，必须调用此方法来提交事务，否则将会认为放弃事务，所有操作将回滚。
		public void Complete()
		{
			this.consistent = true;
		} 
		#endregion
	}

	#region Example
	/*
	 using( EsTransactionScope ts = new EsTransactionScope())
	 {
		DistributedMethod() ;
		ts.Complete() ;
	 }
	 
	 如果事务失败，操作将回滚，并抛出异常！
	*/
	#endregion
}

