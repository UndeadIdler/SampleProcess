using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPDengJi。
    /// </summary>
    public class ERPDengJi
    {
        public ERPDengJi()
        { }
        #region Model
        private int _id;
        private string _username;
        private string _shenpiren;
        private DateTime? _shenqingtime;
        private string _backinfo;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private string _statenow;
        private string _typename;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 审批人
        /// </summary>
        public string ShenPiRen
        {
            set { _shenpiren = value; }
            get { return _shenpiren; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ShenQingTime
        {
            set { _shenqingtime = value; }
            get { return _shenqingtime; }
        }
        /// <summary>
        /// 详细说明
        /// </summary>
        public string BackInfo
        {
            set { _backinfo = value; }
            get { return _backinfo; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string StateNow
        {
            set { _statenow = value; }
            get { return _statenow; }
        }
        /// <summary>
        /// 登记类型
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPDengJi");
            strSql.Append(" where ID=" + ID + " ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPDengJi(");
            strSql.Append("UserName,ShenPiRen,ShenQingTime,BackInfo,StartTime,EndTime,StateNow,TypeName)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@ShenPiRen,@ShenQingTime,@BackInfo,@StartTime,@EndTime,@StateNow,@TypeName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@ShenPiRen", SqlDbType.VarChar,50),
					new SqlParameter("@ShenQingTime", SqlDbType.DateTime),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,8000),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@StateNow", SqlDbType.VarChar,50),
					new SqlParameter("@TypeName", SqlDbType.VarChar,50)};
            parameters[0].Value = UserName;
            parameters[1].Value = ShenPiRen;
            parameters[2].Value = ShenQingTime;
            parameters[3].Value = BackInfo;
            parameters[4].Value = StartTime;
            parameters[5].Value = EndTime;
            parameters[6].Value = StateNow;
            parameters[7].Value = TypeName;

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
            strSql.Append("update ERPDengJi set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("ShenPiRen=@ShenPiRen,");
            strSql.Append("ShenQingTime=@ShenQingTime,");
            strSql.Append("BackInfo=@BackInfo,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("StateNow=@StateNow,");
            strSql.Append("TypeName=@TypeName");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@ShenPiRen", SqlDbType.VarChar,50),
					new SqlParameter("@ShenQingTime", SqlDbType.DateTime),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,8000),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@StateNow", SqlDbType.VarChar,50),
					new SqlParameter("@TypeName", SqlDbType.VarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = UserName;
            parameters[2].Value = ShenPiRen;
            parameters[3].Value = ShenQingTime;
            parameters[4].Value = BackInfo;
            parameters[5].Value = StartTime;
            parameters[6].Value = EndTime;
            parameters[7].Value = StateNow;
            parameters[8].Value = TypeName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPDengJi ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserName,ShenPiRen,ShenQingTime,BackInfo,StartTime,EndTime,StateNow,TypeName ");
            strSql.Append(" FROM ERPDengJi ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                ShenPiRen = ds.Tables[0].Rows[0]["ShenPiRen"].ToString();
                if (ds.Tables[0].Rows[0]["ShenQingTime"].ToString() != "")
                {
                    ShenQingTime = DateTime.Parse(ds.Tables[0].Rows[0]["ShenQingTime"].ToString());
                }
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                if (ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
                {
                    StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                StateNow = ds.Tables[0].Rows[0]["StateNow"].ToString();
                TypeName = ds.Tables[0].Rows[0]["TypeName"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[UserName],[ShenPiRen],[ShenQingTime],[BackInfo],[StartTime],[EndTime],[StateNow],[TypeName] ");
            strSql.Append(" FROM ERPDengJi ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}

