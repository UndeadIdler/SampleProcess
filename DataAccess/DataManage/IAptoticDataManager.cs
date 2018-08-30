using System;
using System.Collections ;
using System.Data ;

namespace DataAccess
{
	/// <summary>
	/// IAptoticDataManager 用于管理在整个系统运行过程中经常使用但又不易发生变化的数据，通常这些数据的量大，
	/// 它们存放于数据库中，如果每次需要时就访问数据库，势必降低系统的整个性能。
	/// (1)IAptoticDataManager 一般在系统启动时就载入部分数据，而另外一些数据在第一次使用时才载入，
	/// (2)IAptoticDataManager 还可以在运行的过程中新添其它的Aptotic数据。
	/// (3)如果系统的特征是长时间运行，那么可以设定自动更新时间，以使IAptoticDataManager中保存的数据与数据库中的同步。
	/// (4)请不要访问由GetData返回对象的UnAptotic部分。
	/// (5)被缓存的数据对应的Table要求必须有一个字段为"ID"表示唯一索引 。 
	/// 
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// 2005.05.30
	/// </summary>
	public interface IAptoticDataManager :IDisposable
	{
		//aptDataInfos 中是AptoticDataInformation集合
		void Initialize(ArrayList aptDataInfos  ,DataBaseType dbType ,int updateMinutes) ; //updateMinutes为-1表示不更新		
		void ClearAllData() ;
		void UpdateData() ;

		void     ClearData(Type dataClassType) ; //如果目标程序修改了部分不易变数据，应调用此方法通知IAptoticDataManager。
		object   GetData(Type dataClassType ,string ID) ;
		object[] GetAllData(Type dataClassType) ;
	}
	

	public class AptoticDataInformation
	{		
		public string ConnStr    ;
		public Type DataClassType ; //类型全名，包括命名空间部分
		public bool LoadNow ;
	}
}
