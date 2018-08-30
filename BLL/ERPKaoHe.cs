using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPKaoHe。
	/// </summary>
	public class ERPKaoHe
	{
		public ERPKaoHe()
		{}
		#region Model
		private int _id;
		private string _kaohetitle;
		private string _kaoheuser;
		private string _kaohetype;
		private string _kaohedate;
		private string _xiangmufen;
		private string _renwufen;
		private string _kaohejielun;
		private string _backinfo;
		private string _username;
		private DateTime? _timestr;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 考核主题
		/// </summary>
		public string KaoHeTitle
		{
			set{ _kaohetitle=value;}
			get{return _kaohetitle;}
		}
		/// <summary>
		/// 考核员工
		/// </summary>
		public string KaoHeUser
		{
			set{ _kaoheuser=value;}
			get{return _kaoheuser;}
		}
		/// <summary>
		/// 考核类别
		/// </summary>
		public string KaoHeType
		{
			set{ _kaohetype=value;}
			get{return _kaohetype;}
		}
		/// <summary>
		/// 考核日期
		/// </summary>
		public string KaoHeDate
		{
			set{ _kaohedate=value;}
			get{return _kaohedate;}
		}
		/// <summary>
		/// 考核项目总分
		/// </summary>
		public string XiangMuFen
		{
			set{ _xiangmufen=value;}
			get{return _xiangmufen;}
		}
		/// <summary>
		/// 考核任务总分
		/// </summary>
		public string RenWuFen
		{
			set{ _renwufen=value;}
			get{return _renwufen;}
		}
		/// <summary>
		/// 考核结论
		/// </summary>
		public string KaoHeJieLun
		{
			set{ _kaohejielun=value;}
			get{return _kaohejielun;}
		}
		/// <summary>
		/// 备注说明
		/// </summary>
		public string BackInfo
		{
			set{ _backinfo=value;}
			get{return _backinfo;}
		}
		/// <summary>
		/// 录入人
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 录入时间
		/// </summary>
		public DateTime? TimeStr
		{
			set{ _timestr=value;}
			get{return _timestr;}
		}
		#endregion Model


		#region  成员方法

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPKaoHe(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,KaoHeTitle,KaoHeUser,KaoHeType,KaoHeDate,XiangMuFen,RenWuFen,KaoHeJieLun,BackInfo,UserName,TimeStr ");
			strSql.Append(" FROM ERPKaoHe ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				KaoHeTitle=ds.Tables[0].Rows[0]["KaoHeTitle"].ToString();
				KaoHeUser=ds.Tables[0].Rows[0]["KaoHeUser"].ToString();
				KaoHeType=ds.Tables[0].Rows[0]["KaoHeType"].ToString();
				KaoHeDate=ds.Tables[0].Rows[0]["KaoHeDate"].ToString();
				XiangMuFen=ds.Tables[0].Rows[0]["XiangMuFen"].ToString();
				RenWuFen=ds.Tables[0].Rows[0]["RenWuFen"].ToString();
				KaoHeJieLun=ds.Tables[0].Rows[0]["KaoHeJieLun"].ToString();
				BackInfo=ds.Tables[0].Rows[0]["BackInfo"].ToString();
				UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				if(ds.Tables[0].Rows[0]["TimeStr"].ToString()!="")
				{
					TimeStr=DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
				}
			}
		}

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{

		return DbHelperSQL.GetMaxID("ID", "ERPKaoHe"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ERPKaoHe");
			strSql.Append(" where ID=@ID ");

			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ERPKaoHe(");
			strSql.Append("KaoHeTitle,KaoHeUser,KaoHeType,KaoHeDate,XiangMuFen,RenWuFen,KaoHeJieLun,BackInfo,UserName,TimeStr)");
			strSql.Append(" values (");
			strSql.Append("@KaoHeTitle,@KaoHeUser,@KaoHeType,@KaoHeDate,@XiangMuFen,@RenWuFen,@KaoHeJieLun,@BackInfo,@UserName,@TimeStr)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@KaoHeTitle", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeUser", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeType", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeDate", SqlDbType.VarChar,50),
					new SqlParameter("@XiangMuFen", SqlDbType.VarChar,50),
					new SqlParameter("@RenWuFen", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeJieLun", SqlDbType.VarChar,5000),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,5000),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
			parameters[0].Value = KaoHeTitle;
			parameters[1].Value = KaoHeUser;
			parameters[2].Value = KaoHeType;
			parameters[3].Value = KaoHeDate;
			parameters[4].Value = XiangMuFen;
			parameters[5].Value = RenWuFen;
			parameters[6].Value = KaoHeJieLun;
			parameters[7].Value = BackInfo;
			parameters[8].Value = UserName;
			parameters[9].Value = TimeStr;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ERPKaoHe set ");
			strSql.Append("KaoHeTitle=@KaoHeTitle,");
			strSql.Append("KaoHeUser=@KaoHeUser,");
			strSql.Append("KaoHeType=@KaoHeType,");
			strSql.Append("KaoHeDate=@KaoHeDate,");
			strSql.Append("XiangMuFen=@XiangMuFen,");
			strSql.Append("RenWuFen=@RenWuFen,");
			strSql.Append("KaoHeJieLun=@KaoHeJieLun,");
			strSql.Append("BackInfo=@BackInfo,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("TimeStr=@TimeStr");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@KaoHeTitle", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeUser", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeType", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeDate", SqlDbType.VarChar,50),
					new SqlParameter("@XiangMuFen", SqlDbType.VarChar,50),
					new SqlParameter("@RenWuFen", SqlDbType.VarChar,50),
					new SqlParameter("@KaoHeJieLun", SqlDbType.VarChar,5000),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,5000),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
			parameters[0].Value = ID;
			parameters[1].Value = KaoHeTitle;
			parameters[2].Value = KaoHeUser;
			parameters[3].Value = KaoHeType;
			parameters[4].Value = KaoHeDate;
			parameters[5].Value = XiangMuFen;
			parameters[6].Value = RenWuFen;
			parameters[7].Value = KaoHeJieLun;
			parameters[8].Value = BackInfo;
			parameters[9].Value = UserName;
			parameters[10].Value = TimeStr;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ERPKaoHe ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public void GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,KaoHeTitle,KaoHeUser,KaoHeType,KaoHeDate,XiangMuFen,RenWuFen,KaoHeJieLun,BackInfo,UserName,TimeStr ");
			strSql.Append(" FROM ERPKaoHe ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				KaoHeTitle=ds.Tables[0].Rows[0]["KaoHeTitle"].ToString();
				KaoHeUser=ds.Tables[0].Rows[0]["KaoHeUser"].ToString();
				KaoHeType=ds.Tables[0].Rows[0]["KaoHeType"].ToString();
				KaoHeDate=ds.Tables[0].Rows[0]["KaoHeDate"].ToString();
				XiangMuFen=ds.Tables[0].Rows[0]["XiangMuFen"].ToString();
				RenWuFen=ds.Tables[0].Rows[0]["RenWuFen"].ToString();
				KaoHeJieLun=ds.Tables[0].Rows[0]["KaoHeJieLun"].ToString();
				BackInfo=ds.Tables[0].Rows[0]["BackInfo"].ToString();
				UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				if(ds.Tables[0].Rows[0]["TimeStr"].ToString()!="")
				{
					TimeStr=DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
				}
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM ERPKaoHe ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
	}
}

