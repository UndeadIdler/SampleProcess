using System;

namespace DataAccess
{
	/// <summary>
	/// IDbMessage 用于取代IDataBaseInfoMgr 。
	/// </summary>
	public interface IDbMessage
	{
		DataBaseType DataBaseType{get ; set ;}
		string		 ConnectString{get ; set ;}

		void		 ActivateDbConnChangedEvent() ; //外部通知IDataBaseInfoMgr数据库连接信息发生了变化		
		event		 EventHandler DbConfigChanged ; 
		bool         IsMultiDataBase{get ;set ;}
	}
}
