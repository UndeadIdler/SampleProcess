using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPProduct。
	/// </summary>
	public class ERPProduct
	{
		public ERPProduct()
		{}
		#region Model
		private int _id;
		private string _productname;
		private string _productserils;
		private string _gongyingshang;
		private string _producttype;
		private string _xinghao;
		private string _danwei;
		private string _chengben;
		private string _chushou;
		private decimal? _rukusum;
		private decimal? _chukusum;
		private decimal? _nowkucun;
		private decimal? _kucunbaojing;
		private string _cunchuweizhi;
		private string _chanpinmiaoshu;
		private string _backinfo;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 产品编码
		/// </summary>
		public string ProductSerils
		{
			set{ _productserils=value;}
			get{return _productserils;}
		}
		/// <summary>
		/// 产品供应商
		/// </summary>
		public string GongYingShang
		{
			set{ _gongyingshang=value;}
			get{return _gongyingshang;}
		}
		/// <summary>
		/// 产品类别
		/// </summary>
		public string ProductType
		{
			set{ _producttype=value;}
			get{return _producttype;}
		}
		/// <summary>
		/// 产品型号
		/// </summary>
		public string XingHao
		{
			set{ _xinghao=value;}
			get{return _xinghao;}
		}
		/// <summary>
		/// 计量单位
		/// </summary>
		public string DanWei
		{
			set{ _danwei=value;}
			get{return _danwei;}
		}
		/// <summary>
		/// 成本价
		/// </summary>
		public string ChengBen
		{
			set{ _chengben=value;}
			get{return _chengben;}
		}
		/// <summary>
		/// 出售价
		/// </summary>
		public string ChuShou
		{
			set{ _chushou=value;}
			get{return _chushou;}
		}
		/// <summary>
		/// 入库总量
		/// </summary>
		public decimal? RuKuSum
		{
			set{ _rukusum=value;}
			get{return _rukusum;}
		}
		/// <summary>
		/// 出库总量
		/// </summary>
		public decimal? ChuKuSum
		{
			set{ _chukusum=value;}
			get{return _chukusum;}
		}
		/// <summary>
		/// 当前库存
		/// </summary>
		public decimal? NowKuCun
		{
			set{ _nowkucun=value;}
			get{return _nowkucun;}
		}
		/// <summary>
		/// 库存报警量
		/// </summary>
		public decimal? KuCunBaoJing
		{
			set{ _kucunbaojing=value;}
			get{return _kucunbaojing;}
		}
		/// <summary>
		/// 存储位置
		/// </summary>
		public string CunChuWeiZhi
		{
			set{ _cunchuweizhi=value;}
			get{return _cunchuweizhi;}
		}
		/// <summary>
		/// 产品描述
		/// </summary>
		public string ChanPinMiaoShu
		{
			set{ _chanpinmiaoshu=value;}
			get{return _chanpinmiaoshu;}
		}
		/// <summary>
		/// 备注信息
		/// </summary>
		public string BackInfo
		{
			set{ _backinfo=value;}
			get{return _backinfo;}
		}
		#endregion Model


		#region  成员方法

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPProduct(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,ProductName,ProductSerils,GongYingShang,ProductType,XingHao,DanWei,ChengBen,ChuShou,RuKuSum,ChuKuSum,NowKuCun,KuCunBaoJing,CunChuWeiZhi,ChanPinMiaoShu,BackInfo ");
			strSql.Append(" FROM ERPProduct ");
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
				ProductName=ds.Tables[0].Rows[0]["ProductName"].ToString();
				ProductSerils=ds.Tables[0].Rows[0]["ProductSerils"].ToString();
				GongYingShang=ds.Tables[0].Rows[0]["GongYingShang"].ToString();
				ProductType=ds.Tables[0].Rows[0]["ProductType"].ToString();
				XingHao=ds.Tables[0].Rows[0]["XingHao"].ToString();
				DanWei=ds.Tables[0].Rows[0]["DanWei"].ToString();
				ChengBen=ds.Tables[0].Rows[0]["ChengBen"].ToString();
				ChuShou=ds.Tables[0].Rows[0]["ChuShou"].ToString();
				if(ds.Tables[0].Rows[0]["RuKuSum"].ToString()!="")
				{
					RuKuSum=decimal.Parse(ds.Tables[0].Rows[0]["RuKuSum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChuKuSum"].ToString()!="")
				{
					ChuKuSum=decimal.Parse(ds.Tables[0].Rows[0]["ChuKuSum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NowKuCun"].ToString()!="")
				{
					NowKuCun=decimal.Parse(ds.Tables[0].Rows[0]["NowKuCun"].ToString());
				}
				if(ds.Tables[0].Rows[0]["KuCunBaoJing"].ToString()!="")
				{
					KuCunBaoJing=decimal.Parse(ds.Tables[0].Rows[0]["KuCunBaoJing"].ToString());
				}
				CunChuWeiZhi=ds.Tables[0].Rows[0]["CunChuWeiZhi"].ToString();
				ChanPinMiaoShu=ds.Tables[0].Rows[0]["ChanPinMiaoShu"].ToString();
				BackInfo=ds.Tables[0].Rows[0]["BackInfo"].ToString();
			}
		}

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{

		return DbHelperSQL.GetMaxID("ID", "ERPProduct"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ERPProduct");
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
			strSql.Append("insert into ERPProduct(");
			strSql.Append("ProductName,ProductSerils,GongYingShang,ProductType,XingHao,DanWei,ChengBen,ChuShou,RuKuSum,ChuKuSum,NowKuCun,KuCunBaoJing,CunChuWeiZhi,ChanPinMiaoShu,BackInfo)");
			strSql.Append(" values (");
			strSql.Append("@ProductName,@ProductSerils,@GongYingShang,@ProductType,@XingHao,@DanWei,@ChengBen,@ChuShou,@RuKuSum,@ChuKuSum,@NowKuCun,@KuCunBaoJing,@CunChuWeiZhi,@ChanPinMiaoShu,@BackInfo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductName", SqlDbType.VarChar,50),
					new SqlParameter("@ProductSerils", SqlDbType.VarChar,50),
					new SqlParameter("@GongYingShang", SqlDbType.VarChar,50),
					new SqlParameter("@ProductType", SqlDbType.VarChar,50),
					new SqlParameter("@XingHao", SqlDbType.VarChar,50),
					new SqlParameter("@DanWei", SqlDbType.VarChar,50),
					new SqlParameter("@ChengBen", SqlDbType.VarChar,50),
					new SqlParameter("@ChuShou", SqlDbType.VarChar,50),
					new SqlParameter("@RuKuSum", SqlDbType.Decimal,9),
					new SqlParameter("@ChuKuSum", SqlDbType.Decimal,9),
					new SqlParameter("@NowKuCun", SqlDbType.Decimal,9),
					new SqlParameter("@KuCunBaoJing", SqlDbType.Decimal,9),
					new SqlParameter("@CunChuWeiZhi", SqlDbType.VarChar,50),
					new SqlParameter("@ChanPinMiaoShu", SqlDbType.VarChar,5000),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,5000)};
			parameters[0].Value = ProductName;
			parameters[1].Value = ProductSerils;
			parameters[2].Value = GongYingShang;
			parameters[3].Value = ProductType;
			parameters[4].Value = XingHao;
			parameters[5].Value = DanWei;
			parameters[6].Value = ChengBen;
			parameters[7].Value = ChuShou;
			parameters[8].Value = RuKuSum;
			parameters[9].Value = ChuKuSum;
			parameters[10].Value = NowKuCun;
			parameters[11].Value = KuCunBaoJing;
			parameters[12].Value = CunChuWeiZhi;
			parameters[13].Value = ChanPinMiaoShu;
			parameters[14].Value = BackInfo;

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
			strSql.Append("update ERPProduct set ");
			strSql.Append("ProductName=@ProductName,");
			strSql.Append("ProductSerils=@ProductSerils,");
			strSql.Append("GongYingShang=@GongYingShang,");
			strSql.Append("ProductType=@ProductType,");
			strSql.Append("XingHao=@XingHao,");
			strSql.Append("DanWei=@DanWei,");
			strSql.Append("ChengBen=@ChengBen,");
			strSql.Append("ChuShou=@ChuShou,");
			strSql.Append("RuKuSum=@RuKuSum,");
			strSql.Append("ChuKuSum=@ChuKuSum,");
			strSql.Append("NowKuCun=@NowKuCun,");
			strSql.Append("KuCunBaoJing=@KuCunBaoJing,");
			strSql.Append("CunChuWeiZhi=@CunChuWeiZhi,");
			strSql.Append("ChanPinMiaoShu=@ChanPinMiaoShu,");
			strSql.Append("BackInfo=@BackInfo");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ProductName", SqlDbType.VarChar,50),
					new SqlParameter("@ProductSerils", SqlDbType.VarChar,50),
					new SqlParameter("@GongYingShang", SqlDbType.VarChar,50),
					new SqlParameter("@ProductType", SqlDbType.VarChar,50),
					new SqlParameter("@XingHao", SqlDbType.VarChar,50),
					new SqlParameter("@DanWei", SqlDbType.VarChar,50),
					new SqlParameter("@ChengBen", SqlDbType.VarChar,50),
					new SqlParameter("@ChuShou", SqlDbType.VarChar,50),
					new SqlParameter("@RuKuSum", SqlDbType.Decimal,9),
					new SqlParameter("@ChuKuSum", SqlDbType.Decimal,9),
					new SqlParameter("@NowKuCun", SqlDbType.Decimal,9),
					new SqlParameter("@KuCunBaoJing", SqlDbType.Decimal,9),
					new SqlParameter("@CunChuWeiZhi", SqlDbType.VarChar,50),
					new SqlParameter("@ChanPinMiaoShu", SqlDbType.VarChar,5000),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,5000)};
			parameters[0].Value = ID;
			parameters[1].Value = ProductName;
			parameters[2].Value = ProductSerils;
			parameters[3].Value = GongYingShang;
			parameters[4].Value = ProductType;
			parameters[5].Value = XingHao;
			parameters[6].Value = DanWei;
			parameters[7].Value = ChengBen;
			parameters[8].Value = ChuShou;
			parameters[9].Value = RuKuSum;
			parameters[10].Value = ChuKuSum;
			parameters[11].Value = NowKuCun;
			parameters[12].Value = KuCunBaoJing;
			parameters[13].Value = CunChuWeiZhi;
			parameters[14].Value = ChanPinMiaoShu;
			parameters[15].Value = BackInfo;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ERPProduct ");
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
			strSql.Append("select  top 1 ID,ProductName,ProductSerils,GongYingShang,ProductType,XingHao,DanWei,ChengBen,ChuShou,RuKuSum,ChuKuSum,NowKuCun,KuCunBaoJing,CunChuWeiZhi,ChanPinMiaoShu,BackInfo ");
			strSql.Append(" FROM ERPProduct ");
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
				ProductName=ds.Tables[0].Rows[0]["ProductName"].ToString();
				ProductSerils=ds.Tables[0].Rows[0]["ProductSerils"].ToString();
				GongYingShang=ds.Tables[0].Rows[0]["GongYingShang"].ToString();
				ProductType=ds.Tables[0].Rows[0]["ProductType"].ToString();
				XingHao=ds.Tables[0].Rows[0]["XingHao"].ToString();
				DanWei=ds.Tables[0].Rows[0]["DanWei"].ToString();
				ChengBen=ds.Tables[0].Rows[0]["ChengBen"].ToString();
				ChuShou=ds.Tables[0].Rows[0]["ChuShou"].ToString();
				if(ds.Tables[0].Rows[0]["RuKuSum"].ToString()!="")
				{
					RuKuSum=decimal.Parse(ds.Tables[0].Rows[0]["RuKuSum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChuKuSum"].ToString()!="")
				{
					ChuKuSum=decimal.Parse(ds.Tables[0].Rows[0]["ChuKuSum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NowKuCun"].ToString()!="")
				{
					NowKuCun=decimal.Parse(ds.Tables[0].Rows[0]["NowKuCun"].ToString());
				}
				if(ds.Tables[0].Rows[0]["KuCunBaoJing"].ToString()!="")
				{
					KuCunBaoJing=decimal.Parse(ds.Tables[0].Rows[0]["KuCunBaoJing"].ToString());
				}
				CunChuWeiZhi=ds.Tables[0].Rows[0]["CunChuWeiZhi"].ToString();
				ChanPinMiaoShu=ds.Tables[0].Rows[0]["ChanPinMiaoShu"].ToString();
				BackInfo=ds.Tables[0].Rows[0]["BackInfo"].ToString();
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM ERPProduct ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
	}
}

