using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPCustomInfo。
	/// </summary>
	public class ERPCustomInfo
	{
		public ERPCustomInfo()
		{}
		#region Model
		private int _id;
		private string _customname;
		private string _customserils;
		private string _chargeman;
		private string _address;
		private string _urllink;
		private string _youbian;
		private string _telstr;
		private string _xingzhi;
		private string _laiyuan;
		private string _quyu;
		private string _zhuangtai;
		private string _renshu;
		private string _bangongmianji;
		private string _leibie;
		private string _jibie;
		private string _yewufanwei;
		private string _hangye;
		private string _muqianwenti;
		private string _yujidingdandate;
		private string _backinfoa;
		private string _backinfob;
		private string _backinfoc;
		private string _backinfod;
		private DateTime? _timestr;
		private string _username;
		private string _dingdancount;
		private string _yujitixing;
		private string _ifshare;
		private string _cusbaka;
		private string _cusbakb;
		private string _cusbakc;
		private string _cusbakd;
		private string _cusbake;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomName
		{
			set{ _customname=value;}
			get{return _customname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomSerils
		{
			set{ _customserils=value;}
			get{return _customserils;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChargeMan
		{
			set{ _chargeman=value;}
			get{return _chargeman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UrlLink
		{
			set{ _urllink=value;}
			get{return _urllink;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YouBian
		{
			set{ _youbian=value;}
			get{return _youbian;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TelStr
		{
			set{ _telstr=value;}
			get{return _telstr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XingZhi
		{
			set{ _xingzhi=value;}
			get{return _xingzhi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LaiYuan
		{
			set{ _laiyuan=value;}
			get{return _laiyuan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QuYu
		{
			set{ _quyu=value;}
			get{return _quyu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhuangTai
		{
			set{ _zhuangtai=value;}
			get{return _zhuangtai;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RenShu
		{
			set{ _renshu=value;}
			get{return _renshu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BanGongMianJi
		{
			set{ _bangongmianji=value;}
			get{return _bangongmianji;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LeiBie
		{
			set{ _leibie=value;}
			get{return _leibie;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JiBie
		{
			set{ _jibie=value;}
			get{return _jibie;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YeWuFanWei
		{
			set{ _yewufanwei=value;}
			get{return _yewufanwei;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HangYe
		{
			set{ _hangye=value;}
			get{return _hangye;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MuQianWenTi
		{
			set{ _muqianwenti=value;}
			get{return _muqianwenti;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YuJiDingDanDate
		{
			set{ _yujidingdandate=value;}
			get{return _yujidingdandate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BackInfoA
		{
			set{ _backinfoa=value;}
			get{return _backinfoa;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BackInfoB
		{
			set{ _backinfob=value;}
			get{return _backinfob;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BackInfoC
		{
			set{ _backinfoc=value;}
			get{return _backinfoc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BackInfoD
		{
			set{ _backinfod=value;}
			get{return _backinfod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TimeStr
		{
			set{ _timestr=value;}
			get{return _timestr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DingDanCount
		{
			set{ _dingdancount=value;}
			get{return _dingdancount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YuJiTiXing
		{
			set{ _yujitixing=value;}
			get{return _yujitixing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IFShare
		{
			set{ _ifshare=value;}
			get{return _ifshare;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CusBakA
		{
			set{ _cusbaka=value;}
			get{return _cusbaka;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CusBakB
		{
			set{ _cusbakb=value;}
			get{return _cusbakb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CusBakC
		{
			set{ _cusbakc=value;}
			get{return _cusbakc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CusBakD
		{
			set{ _cusbakd=value;}
			get{return _cusbakd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CusBakE
		{
			set{ _cusbake=value;}
			get{return _cusbake;}
		}
		#endregion Model


		#region  成员方法

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPCustomInfo(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CustomName,CustomSerils,ChargeMan,Address,UrlLink,YouBian,TelStr,XingZhi,LaiYuan,QuYu,ZhuangTai,RenShu,BanGongMianJi,LeiBie,JiBie,YeWuFanWei,HangYe,MuQianWenTi,YuJiDingDanDate,BackInfoA,BackInfoB,BackInfoC,BackInfoD,TimeStr,UserName,DingDanCount,YuJiTiXing,IFShare,CusBakA,CusBakB,CusBakC,CusBakD,CusBakE ");
			strSql.Append(" FROM ERPCustomInfo ");
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
				CustomName=ds.Tables[0].Rows[0]["CustomName"].ToString();
				CustomSerils=ds.Tables[0].Rows[0]["CustomSerils"].ToString();
				ChargeMan=ds.Tables[0].Rows[0]["ChargeMan"].ToString();
				Address=ds.Tables[0].Rows[0]["Address"].ToString();
				UrlLink=ds.Tables[0].Rows[0]["UrlLink"].ToString();
				YouBian=ds.Tables[0].Rows[0]["YouBian"].ToString();
				TelStr=ds.Tables[0].Rows[0]["TelStr"].ToString();
				XingZhi=ds.Tables[0].Rows[0]["XingZhi"].ToString();
				LaiYuan=ds.Tables[0].Rows[0]["LaiYuan"].ToString();
				QuYu=ds.Tables[0].Rows[0]["QuYu"].ToString();
				ZhuangTai=ds.Tables[0].Rows[0]["ZhuangTai"].ToString();
				RenShu=ds.Tables[0].Rows[0]["RenShu"].ToString();
				BanGongMianJi=ds.Tables[0].Rows[0]["BanGongMianJi"].ToString();
				LeiBie=ds.Tables[0].Rows[0]["LeiBie"].ToString();
				JiBie=ds.Tables[0].Rows[0]["JiBie"].ToString();
				YeWuFanWei=ds.Tables[0].Rows[0]["YeWuFanWei"].ToString();
				HangYe=ds.Tables[0].Rows[0]["HangYe"].ToString();
				MuQianWenTi=ds.Tables[0].Rows[0]["MuQianWenTi"].ToString();
				YuJiDingDanDate=ds.Tables[0].Rows[0]["YuJiDingDanDate"].ToString();
				BackInfoA=ds.Tables[0].Rows[0]["BackInfoA"].ToString();
				BackInfoB=ds.Tables[0].Rows[0]["BackInfoB"].ToString();
				BackInfoC=ds.Tables[0].Rows[0]["BackInfoC"].ToString();
				BackInfoD=ds.Tables[0].Rows[0]["BackInfoD"].ToString();
				if(ds.Tables[0].Rows[0]["TimeStr"].ToString()!="")
				{
					TimeStr=DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
				}
				UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				DingDanCount=ds.Tables[0].Rows[0]["DingDanCount"].ToString();
				YuJiTiXing=ds.Tables[0].Rows[0]["YuJiTiXing"].ToString();
				IFShare=ds.Tables[0].Rows[0]["IFShare"].ToString();
				CusBakA=ds.Tables[0].Rows[0]["CusBakA"].ToString();
				CusBakB=ds.Tables[0].Rows[0]["CusBakB"].ToString();
				CusBakC=ds.Tables[0].Rows[0]["CusBakC"].ToString();
				CusBakD=ds.Tables[0].Rows[0]["CusBakD"].ToString();
				CusBakE=ds.Tables[0].Rows[0]["CusBakE"].ToString();
			}
		}

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{

		return DbHelperSQL.GetMaxID("ID", "ERPCustomInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ERPCustomInfo");
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
			strSql.Append("insert into ERPCustomInfo(");
			strSql.Append("CustomName,CustomSerils,ChargeMan,Address,UrlLink,YouBian,TelStr,XingZhi,LaiYuan,QuYu,ZhuangTai,RenShu,BanGongMianJi,LeiBie,JiBie,YeWuFanWei,HangYe,MuQianWenTi,YuJiDingDanDate,BackInfoA,BackInfoB,BackInfoC,BackInfoD,TimeStr,UserName,DingDanCount,YuJiTiXing,IFShare,CusBakA,CusBakB,CusBakC,CusBakD,CusBakE)");
			strSql.Append(" values (");
			strSql.Append("@CustomName,@CustomSerils,@ChargeMan,@Address,@UrlLink,@YouBian,@TelStr,@XingZhi,@LaiYuan,@QuYu,@ZhuangTai,@RenShu,@BanGongMianJi,@LeiBie,@JiBie,@YeWuFanWei,@HangYe,@MuQianWenTi,@YuJiDingDanDate,@BackInfoA,@BackInfoB,@BackInfoC,@BackInfoD,@TimeStr,@UserName,@DingDanCount,@YuJiTiXing,@IFShare,@CusBakA,@CusBakB,@CusBakC,@CusBakD,@CusBakE)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomName", SqlDbType.VarChar,200),
					new SqlParameter("@CustomSerils", SqlDbType.VarChar,100),
					new SqlParameter("@ChargeMan", SqlDbType.VarChar,100),
					new SqlParameter("@Address", SqlDbType.VarChar,500),
					new SqlParameter("@UrlLink", SqlDbType.VarChar,200),
					new SqlParameter("@YouBian", SqlDbType.VarChar,50),
					new SqlParameter("@TelStr", SqlDbType.VarChar,100),
					new SqlParameter("@XingZhi", SqlDbType.VarChar,50),
					new SqlParameter("@LaiYuan", SqlDbType.VarChar,50),
					new SqlParameter("@QuYu", SqlDbType.VarChar,50),
					new SqlParameter("@ZhuangTai", SqlDbType.VarChar,50),
					new SqlParameter("@RenShu", SqlDbType.VarChar,50),
					new SqlParameter("@BanGongMianJi", SqlDbType.VarChar,50),
					new SqlParameter("@LeiBie", SqlDbType.VarChar,50),
					new SqlParameter("@JiBie", SqlDbType.VarChar,50),
					new SqlParameter("@YeWuFanWei", SqlDbType.VarChar,200),
					new SqlParameter("@HangYe", SqlDbType.VarChar,100),
					new SqlParameter("@MuQianWenTi", SqlDbType.VarChar,500),
					new SqlParameter("@YuJiDingDanDate", SqlDbType.VarChar,100),
					new SqlParameter("@BackInfoA", SqlDbType.VarChar,8000),
					new SqlParameter("@BackInfoB", SqlDbType.VarChar,8000),
					new SqlParameter("@BackInfoC", SqlDbType.VarChar,8000),
					new SqlParameter("@BackInfoD", SqlDbType.VarChar,8000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@DingDanCount", SqlDbType.VarChar,50),
					new SqlParameter("@YuJiTiXing", SqlDbType.VarChar,50),
					new SqlParameter("@IFShare", SqlDbType.VarChar,5000),
					new SqlParameter("@CusBakA", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakB", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakC", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakD", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakE", SqlDbType.VarChar,8000)};
			parameters[0].Value = CustomName;
			parameters[1].Value = CustomSerils;
			parameters[2].Value = ChargeMan;
			parameters[3].Value = Address;
			parameters[4].Value = UrlLink;
			parameters[5].Value = YouBian;
			parameters[6].Value = TelStr;
			parameters[7].Value = XingZhi;
			parameters[8].Value = LaiYuan;
			parameters[9].Value = QuYu;
			parameters[10].Value = ZhuangTai;
			parameters[11].Value = RenShu;
			parameters[12].Value = BanGongMianJi;
			parameters[13].Value = LeiBie;
			parameters[14].Value = JiBie;
			parameters[15].Value = YeWuFanWei;
			parameters[16].Value = HangYe;
			parameters[17].Value = MuQianWenTi;
			parameters[18].Value = YuJiDingDanDate;
			parameters[19].Value = BackInfoA;
			parameters[20].Value = BackInfoB;
			parameters[21].Value = BackInfoC;
			parameters[22].Value = BackInfoD;
			parameters[23].Value = TimeStr;
			parameters[24].Value = UserName;
			parameters[25].Value = DingDanCount;
			parameters[26].Value = YuJiTiXing;
			parameters[27].Value = IFShare;
			parameters[28].Value = CusBakA;
			parameters[29].Value = CusBakB;
			parameters[30].Value = CusBakC;
			parameters[31].Value = CusBakD;
			parameters[32].Value = CusBakE;

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
			strSql.Append("update ERPCustomInfo set ");
			strSql.Append("CustomName=@CustomName,");
			strSql.Append("CustomSerils=@CustomSerils,");
			strSql.Append("ChargeMan=@ChargeMan,");
			strSql.Append("Address=@Address,");
			strSql.Append("UrlLink=@UrlLink,");
			strSql.Append("YouBian=@YouBian,");
			strSql.Append("TelStr=@TelStr,");
			strSql.Append("XingZhi=@XingZhi,");
			strSql.Append("LaiYuan=@LaiYuan,");
			strSql.Append("QuYu=@QuYu,");
			strSql.Append("ZhuangTai=@ZhuangTai,");
			strSql.Append("RenShu=@RenShu,");
			strSql.Append("BanGongMianJi=@BanGongMianJi,");
			strSql.Append("LeiBie=@LeiBie,");
			strSql.Append("JiBie=@JiBie,");
			strSql.Append("YeWuFanWei=@YeWuFanWei,");
			strSql.Append("HangYe=@HangYe,");
			strSql.Append("MuQianWenTi=@MuQianWenTi,");
			strSql.Append("YuJiDingDanDate=@YuJiDingDanDate,");
			strSql.Append("BackInfoA=@BackInfoA,");
			strSql.Append("BackInfoB=@BackInfoB,");
			strSql.Append("BackInfoC=@BackInfoC,");
			strSql.Append("BackInfoD=@BackInfoD,");
			strSql.Append("TimeStr=@TimeStr,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("DingDanCount=@DingDanCount,");
			strSql.Append("YuJiTiXing=@YuJiTiXing,");
			strSql.Append("IFShare=@IFShare,");
			strSql.Append("CusBakA=@CusBakA,");
			strSql.Append("CusBakB=@CusBakB,");
			strSql.Append("CusBakC=@CusBakC,");
			strSql.Append("CusBakD=@CusBakD,");
			strSql.Append("CusBakE=@CusBakE");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CustomName", SqlDbType.VarChar,200),
					new SqlParameter("@CustomSerils", SqlDbType.VarChar,100),
					new SqlParameter("@ChargeMan", SqlDbType.VarChar,100),
					new SqlParameter("@Address", SqlDbType.VarChar,500),
					new SqlParameter("@UrlLink", SqlDbType.VarChar,200),
					new SqlParameter("@YouBian", SqlDbType.VarChar,50),
					new SqlParameter("@TelStr", SqlDbType.VarChar,100),
					new SqlParameter("@XingZhi", SqlDbType.VarChar,50),
					new SqlParameter("@LaiYuan", SqlDbType.VarChar,50),
					new SqlParameter("@QuYu", SqlDbType.VarChar,50),
					new SqlParameter("@ZhuangTai", SqlDbType.VarChar,50),
					new SqlParameter("@RenShu", SqlDbType.VarChar,50),
					new SqlParameter("@BanGongMianJi", SqlDbType.VarChar,50),
					new SqlParameter("@LeiBie", SqlDbType.VarChar,50),
					new SqlParameter("@JiBie", SqlDbType.VarChar,50),
					new SqlParameter("@YeWuFanWei", SqlDbType.VarChar,200),
					new SqlParameter("@HangYe", SqlDbType.VarChar,100),
					new SqlParameter("@MuQianWenTi", SqlDbType.VarChar,500),
					new SqlParameter("@YuJiDingDanDate", SqlDbType.VarChar,100),
					new SqlParameter("@BackInfoA", SqlDbType.VarChar,8000),
					new SqlParameter("@BackInfoB", SqlDbType.VarChar,8000),
					new SqlParameter("@BackInfoC", SqlDbType.VarChar,8000),
					new SqlParameter("@BackInfoD", SqlDbType.VarChar,8000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@DingDanCount", SqlDbType.VarChar,50),
					new SqlParameter("@YuJiTiXing", SqlDbType.VarChar,50),
					new SqlParameter("@IFShare", SqlDbType.VarChar,5000),
					new SqlParameter("@CusBakA", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakB", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakC", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakD", SqlDbType.VarChar,8000),
					new SqlParameter("@CusBakE", SqlDbType.VarChar,8000)};
			parameters[0].Value = ID;
			parameters[1].Value = CustomName;
			parameters[2].Value = CustomSerils;
			parameters[3].Value = ChargeMan;
			parameters[4].Value = Address;
			parameters[5].Value = UrlLink;
			parameters[6].Value = YouBian;
			parameters[7].Value = TelStr;
			parameters[8].Value = XingZhi;
			parameters[9].Value = LaiYuan;
			parameters[10].Value = QuYu;
			parameters[11].Value = ZhuangTai;
			parameters[12].Value = RenShu;
			parameters[13].Value = BanGongMianJi;
			parameters[14].Value = LeiBie;
			parameters[15].Value = JiBie;
			parameters[16].Value = YeWuFanWei;
			parameters[17].Value = HangYe;
			parameters[18].Value = MuQianWenTi;
			parameters[19].Value = YuJiDingDanDate;
			parameters[20].Value = BackInfoA;
			parameters[21].Value = BackInfoB;
			parameters[22].Value = BackInfoC;
			parameters[23].Value = BackInfoD;
			parameters[24].Value = TimeStr;
			parameters[25].Value = UserName;
			parameters[26].Value = DingDanCount;
			parameters[27].Value = YuJiTiXing;
			parameters[28].Value = IFShare;
			parameters[29].Value = CusBakA;
			parameters[30].Value = CusBakB;
			parameters[31].Value = CusBakC;
			parameters[32].Value = CusBakD;
			parameters[33].Value = CusBakE;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete ERPCustomInfo ");
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
			strSql.Append("select ID,CustomName,CustomSerils,ChargeMan,Address,UrlLink,YouBian,TelStr,XingZhi,LaiYuan,QuYu,ZhuangTai,RenShu,BanGongMianJi,LeiBie,JiBie,YeWuFanWei,HangYe,MuQianWenTi,YuJiDingDanDate,BackInfoA,BackInfoB,BackInfoC,BackInfoD,TimeStr,UserName,DingDanCount,YuJiTiXing,IFShare,CusBakA,CusBakB,CusBakC,CusBakD,CusBakE ");
			strSql.Append(" FROM ERPCustomInfo ");
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
				CustomName=ds.Tables[0].Rows[0]["CustomName"].ToString();
				CustomSerils=ds.Tables[0].Rows[0]["CustomSerils"].ToString();
				ChargeMan=ds.Tables[0].Rows[0]["ChargeMan"].ToString();
				Address=ds.Tables[0].Rows[0]["Address"].ToString();
				UrlLink=ds.Tables[0].Rows[0]["UrlLink"].ToString();
				YouBian=ds.Tables[0].Rows[0]["YouBian"].ToString();
				TelStr=ds.Tables[0].Rows[0]["TelStr"].ToString();
				XingZhi=ds.Tables[0].Rows[0]["XingZhi"].ToString();
				LaiYuan=ds.Tables[0].Rows[0]["LaiYuan"].ToString();
				QuYu=ds.Tables[0].Rows[0]["QuYu"].ToString();
				ZhuangTai=ds.Tables[0].Rows[0]["ZhuangTai"].ToString();
				RenShu=ds.Tables[0].Rows[0]["RenShu"].ToString();
				BanGongMianJi=ds.Tables[0].Rows[0]["BanGongMianJi"].ToString();
				LeiBie=ds.Tables[0].Rows[0]["LeiBie"].ToString();
				JiBie=ds.Tables[0].Rows[0]["JiBie"].ToString();
				YeWuFanWei=ds.Tables[0].Rows[0]["YeWuFanWei"].ToString();
				HangYe=ds.Tables[0].Rows[0]["HangYe"].ToString();
				MuQianWenTi=ds.Tables[0].Rows[0]["MuQianWenTi"].ToString();
				YuJiDingDanDate=ds.Tables[0].Rows[0]["YuJiDingDanDate"].ToString();
				BackInfoA=ds.Tables[0].Rows[0]["BackInfoA"].ToString();
				BackInfoB=ds.Tables[0].Rows[0]["BackInfoB"].ToString();
				BackInfoC=ds.Tables[0].Rows[0]["BackInfoC"].ToString();
				BackInfoD=ds.Tables[0].Rows[0]["BackInfoD"].ToString();
				if(ds.Tables[0].Rows[0]["TimeStr"].ToString()!="")
				{
					TimeStr=DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
				}
				UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				DingDanCount=ds.Tables[0].Rows[0]["DingDanCount"].ToString();
				YuJiTiXing=ds.Tables[0].Rows[0]["YuJiTiXing"].ToString();
				IFShare=ds.Tables[0].Rows[0]["IFShare"].ToString();
				CusBakA=ds.Tables[0].Rows[0]["CusBakA"].ToString();
				CusBakB=ds.Tables[0].Rows[0]["CusBakB"].ToString();
				CusBakC=ds.Tables[0].Rows[0]["CusBakC"].ToString();
				CusBakD=ds.Tables[0].Rows[0]["CusBakD"].ToString();
				CusBakE=ds.Tables[0].Rows[0]["CusBakE"].ToString();
			}
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select [ID],[CustomName],[CustomSerils],[ChargeMan],[Address],[UrlLink],[YouBian],[TelStr],[XingZhi],[LaiYuan],[QuYu],[ZhuangTai],[RenShu],[BanGongMianJi],[LeiBie],[JiBie],[YeWuFanWei],[HangYe],[MuQianWenTi],[YuJiDingDanDate],[BackInfoA],[BackInfoB],[BackInfoC],[BackInfoD],[TimeStr],[UserName],[DingDanCount],[YuJiTiXing],[IFShare],[CusBakA],[CusBakB],[CusBakC],[CusBakD],[CusBakE] ");
			strSql.Append(" FROM ERPCustomInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
	}
}

