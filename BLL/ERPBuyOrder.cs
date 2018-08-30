using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPBuyOrder。
    /// </summary>
    public class ERPBuyOrder
    {
        public ERPBuyOrder()
        { }
        #region Model
        private int _id;
        private string _ordername;
        private string _gongyingshangname;
        private string _serils;
        private string _dingdanleixing;
        private string _dingdanmiaoshu;
        private DateTime? _createdate;
        private DateTime? _laihuodate;
        private DateTime? _tixingdate;
        private string _chuangjianren;
        private string _fuzeren;
        private string _fujianlist;
        private string _nowstate;
        private string _shenpitongguoren;
        private string _backinfo;
        private string _username;
        private DateTime? _timestr;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName
        {
            set { _ordername = value; }
            get { return _ordername; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GongYingShangName
        {
            set { _gongyingshangname = value; }
            get { return _gongyingshangname; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Serils
        {
            set { _serils = value; }
            get { return _serils; }
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string DingDanLeiXing
        {
            set { _dingdanleixing = value; }
            get { return _dingdanleixing; }
        }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string DingDanMiaoShu
        {
            set { _dingdanmiaoshu = value; }
            get { return _dingdanmiaoshu; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 来货日期
        /// </summary>
        public DateTime? LaiHuoDate
        {
            set { _laihuodate = value; }
            get { return _laihuodate; }
        }
        /// <summary>
        /// 提醒日期
        /// </summary>
        public DateTime? TiXingDate
        {
            set { _tixingdate = value; }
            get { return _tixingdate; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string ChuangJianRen
        {
            set { _chuangjianren = value; }
            get { return _chuangjianren; }
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public string FuZeRen
        {
            set { _fuzeren = value; }
            get { return _fuzeren; }
        }
        /// <summary>
        /// 附件文件
        /// </summary>
        public string FuJianList
        {
            set { _fujianlist = value; }
            get { return _fujianlist; }
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string NowState
        {
            set { _nowstate = value; }
            get { return _nowstate; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ShenPiTongGuoRen
        {
            set { _shenpitongguoren = value; }
            get { return _shenpitongguoren; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string BackInfo
        {
            set { _backinfo = value; }
            get { return _backinfo; }
        }
        /// <summary>
        /// 录入人
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        #endregion Model


        #region  成员方法

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPBuyOrder(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,OrderName,GongYingShangName,Serils,DingDanLeiXing,DingDanMiaoShu,CreateDate,LaiHuoDate,TiXingDate,ChuangJianRen,FuZeRen,FuJianList,NowState,ShenPiTongGuoRen,BackInfo,UserName,TimeStr ");
            strSql.Append(" FROM ERPBuyOrder ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                OrderName = ds.Tables[0].Rows[0]["OrderName"].ToString();
                GongYingShangName = ds.Tables[0].Rows[0]["GongYingShangName"].ToString();
                Serils = ds.Tables[0].Rows[0]["Serils"].ToString();
                DingDanLeiXing = ds.Tables[0].Rows[0]["DingDanLeiXing"].ToString();
                DingDanMiaoShu = ds.Tables[0].Rows[0]["DingDanMiaoShu"].ToString();
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LaiHuoDate"].ToString() != "")
                {
                    LaiHuoDate = DateTime.Parse(ds.Tables[0].Rows[0]["LaiHuoDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TiXingDate"].ToString() != "")
                {
                    TiXingDate = DateTime.Parse(ds.Tables[0].Rows[0]["TiXingDate"].ToString());
                }
                ChuangJianRen = ds.Tables[0].Rows[0]["ChuangJianRen"].ToString();
                FuZeRen = ds.Tables[0].Rows[0]["FuZeRen"].ToString();
                FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                NowState = ds.Tables[0].Rows[0]["NowState"].ToString();
                ShenPiTongGuoRen = ds.Tables[0].Rows[0]["ShenPiTongGuoRen"].ToString();
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPBuyOrder");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPBuyOrder(");
            strSql.Append("OrderName,GongYingShangName,Serils,DingDanLeiXing,DingDanMiaoShu,CreateDate,LaiHuoDate,TiXingDate,ChuangJianRen,FuZeRen,FuJianList,NowState,ShenPiTongGuoRen,BackInfo,UserName,TimeStr)");
            strSql.Append(" values (");
            strSql.Append("@OrderName,@GongYingShangName,@Serils,@DingDanLeiXing,@DingDanMiaoShu,@CreateDate,@LaiHuoDate,@TiXingDate,@ChuangJianRen,@FuZeRen,@FuJianList,@NowState,@ShenPiTongGuoRen,@BackInfo,@UserName,@TimeStr)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderName", SqlDbType.VarChar,50),
					new SqlParameter("@GongYingShangName", SqlDbType.VarChar,50),
					new SqlParameter("@Serils", SqlDbType.VarChar,50),
					new SqlParameter("@DingDanLeiXing", SqlDbType.VarChar,50),
					new SqlParameter("@DingDanMiaoShu", SqlDbType.VarChar,5000),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@LaiHuoDate", SqlDbType.DateTime),
					new SqlParameter("@TiXingDate", SqlDbType.DateTime),
					new SqlParameter("@ChuangJianRen", SqlDbType.VarChar,50),
					new SqlParameter("@FuZeRen", SqlDbType.VarChar,50),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,500),
					new SqlParameter("@NowState", SqlDbType.VarChar,50),
					new SqlParameter("@ShenPiTongGuoRen", SqlDbType.VarChar,50),
					new SqlParameter("@BackInfo", SqlDbType.Text),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
            parameters[0].Value = OrderName;
            parameters[1].Value = GongYingShangName;
            parameters[2].Value = Serils;
            parameters[3].Value = DingDanLeiXing;
            parameters[4].Value = DingDanMiaoShu;
            parameters[5].Value = CreateDate;
            parameters[6].Value = LaiHuoDate;
            parameters[7].Value = TiXingDate;
            parameters[8].Value = ChuangJianRen;
            parameters[9].Value = FuZeRen;
            parameters[10].Value = FuJianList;
            parameters[11].Value = NowState;
            parameters[12].Value = ShenPiTongGuoRen;
            parameters[13].Value = BackInfo;
            parameters[14].Value = UserName;
            parameters[15].Value = TimeStr;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPBuyOrder set ");
            strSql.Append("OrderName=@OrderName,");
            strSql.Append("GongYingShangName=@GongYingShangName,");
            strSql.Append("Serils=@Serils,");
            strSql.Append("DingDanLeiXing=@DingDanLeiXing,");
            strSql.Append("DingDanMiaoShu=@DingDanMiaoShu,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("LaiHuoDate=@LaiHuoDate,");
            strSql.Append("TiXingDate=@TiXingDate,");
            strSql.Append("ChuangJianRen=@ChuangJianRen,");
            strSql.Append("FuZeRen=@FuZeRen,");
            strSql.Append("FuJianList=@FuJianList,");
            strSql.Append("NowState=@NowState,");
            strSql.Append("ShenPiTongGuoRen=@ShenPiTongGuoRen,");
            strSql.Append("BackInfo=@BackInfo,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TimeStr=@TimeStr");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@OrderName", SqlDbType.VarChar,50),
					new SqlParameter("@GongYingShangName", SqlDbType.VarChar,50),
					new SqlParameter("@Serils", SqlDbType.VarChar,50),
					new SqlParameter("@DingDanLeiXing", SqlDbType.VarChar,50),
					new SqlParameter("@DingDanMiaoShu", SqlDbType.VarChar,5000),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@LaiHuoDate", SqlDbType.DateTime),
					new SqlParameter("@TiXingDate", SqlDbType.DateTime),
					new SqlParameter("@ChuangJianRen", SqlDbType.VarChar,50),
					new SqlParameter("@FuZeRen", SqlDbType.VarChar,50),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,500),
					new SqlParameter("@NowState", SqlDbType.VarChar,50),
					new SqlParameter("@ShenPiTongGuoRen", SqlDbType.VarChar,50),
					new SqlParameter("@BackInfo", SqlDbType.Text),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
            parameters[0].Value = ID;
            parameters[1].Value = OrderName;
            parameters[2].Value = GongYingShangName;
            parameters[3].Value = Serils;
            parameters[4].Value = DingDanLeiXing;
            parameters[5].Value = DingDanMiaoShu;
            parameters[6].Value = CreateDate;
            parameters[7].Value = LaiHuoDate;
            parameters[8].Value = TiXingDate;
            parameters[9].Value = ChuangJianRen;
            parameters[10].Value = FuZeRen;
            parameters[11].Value = FuJianList;
            parameters[12].Value = NowState;
            parameters[13].Value = ShenPiTongGuoRen;
            parameters[14].Value = BackInfo;
            parameters[15].Value = UserName;
            parameters[16].Value = TimeStr;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPBuyOrder ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,OrderName,GongYingShangName,Serils,DingDanLeiXing,DingDanMiaoShu,CreateDate,LaiHuoDate,TiXingDate,ChuangJianRen,FuZeRen,FuJianList,NowState,ShenPiTongGuoRen,BackInfo,UserName,TimeStr ");
            strSql.Append(" FROM ERPBuyOrder ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                OrderName = ds.Tables[0].Rows[0]["OrderName"].ToString();
                GongYingShangName = ds.Tables[0].Rows[0]["GongYingShangName"].ToString();
                Serils = ds.Tables[0].Rows[0]["Serils"].ToString();
                DingDanLeiXing = ds.Tables[0].Rows[0]["DingDanLeiXing"].ToString();
                DingDanMiaoShu = ds.Tables[0].Rows[0]["DingDanMiaoShu"].ToString();
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LaiHuoDate"].ToString() != "")
                {
                    LaiHuoDate = DateTime.Parse(ds.Tables[0].Rows[0]["LaiHuoDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TiXingDate"].ToString() != "")
                {
                    TiXingDate = DateTime.Parse(ds.Tables[0].Rows[0]["TiXingDate"].ToString());
                }
                ChuangJianRen = ds.Tables[0].Rows[0]["ChuangJianRen"].ToString();
                FuZeRen = ds.Tables[0].Rows[0]["FuZeRen"].ToString();
                FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                NowState = ds.Tables[0].Rows[0]["NowState"].ToString();
                ShenPiTongGuoRen = ds.Tables[0].Rows[0]["ShenPiTongGuoRen"].ToString();
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPBuyOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}

