using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPSource。
    /// </summary>
    public class ERPSource
    {
        public ERPSource()
        { }
        #region Model
        private int _id;
        private string _sourcename;
        private string _shengqinguser;
        private string _shiyonguser;
        private string _shiyongshixian;
        private string _sourcemiaoshu;
        private string _nowstate;
        private string _username;
        private DateTime? _timestr;
        private string _backinfo;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string SourceName
        {
            set { _sourcename = value; }
            get { return _sourcename; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string ShengQingUser
        {
            set { _shengqinguser = value; }
            get { return _shengqinguser; }
        }
        /// <summary>
        /// 使用人
        /// </summary>
        public string ShiYongUser
        {
            set { _shiyonguser = value; }
            get { return _shiyonguser; }
        }
        /// <summary>
        /// 使用时限
        /// </summary>
        public string ShiYongShiXian
        {
            set { _shiyongshixian = value; }
            get { return _shiyongshixian; }
        }
        /// <summary>
        /// 资源描述
        /// </summary>
        public string SourceMiaoShu
        {
            set { _sourcemiaoshu = value; }
            get { return _sourcemiaoshu; }
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
        /// <summary>
        /// 备注说明
        /// </summary>
        public string BackInfo
        {
            set { _backinfo = value; }
            get { return _backinfo; }
        }
        #endregion Model


        #region  成员方法

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPSource(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SourceName,ShengQingUser,ShiYongUser,ShiYongShiXian,SourceMiaoShu,NowState,UserName,TimeStr,BackInfo ");
            strSql.Append(" FROM ERPSource ");
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
                SourceName = ds.Tables[0].Rows[0]["SourceName"].ToString();
                ShengQingUser = ds.Tables[0].Rows[0]["ShengQingUser"].ToString();
                ShiYongUser = ds.Tables[0].Rows[0]["ShiYongUser"].ToString();
                ShiYongShiXian = ds.Tables[0].Rows[0]["ShiYongShiXian"].ToString();
                SourceMiaoShu = ds.Tables[0].Rows[0]["SourceMiaoShu"].ToString();
                NowState = ds.Tables[0].Rows[0]["NowState"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPSource");
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
            strSql.Append("insert into ERPSource(");
            strSql.Append("SourceName,ShengQingUser,ShiYongUser,ShiYongShiXian,SourceMiaoShu,NowState,UserName,TimeStr,BackInfo)");
            strSql.Append(" values (");
            strSql.Append("@SourceName,@ShengQingUser,@ShiYongUser,@ShiYongShiXian,@SourceMiaoShu,@NowState,@UserName,@TimeStr,@BackInfo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SourceName", SqlDbType.VarChar,50),
					new SqlParameter("@ShengQingUser", SqlDbType.VarChar,50),
					new SqlParameter("@ShiYongUser", SqlDbType.VarChar,50),
					new SqlParameter("@ShiYongShiXian", SqlDbType.VarChar,100),
					new SqlParameter("@SourceMiaoShu", SqlDbType.VarChar,5000),
					new SqlParameter("@NowState", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,5000)};
            parameters[0].Value = SourceName;
            parameters[1].Value = ShengQingUser;
            parameters[2].Value = ShiYongUser;
            parameters[3].Value = ShiYongShiXian;
            parameters[4].Value = SourceMiaoShu;
            parameters[5].Value = NowState;
            parameters[6].Value = UserName;
            parameters[7].Value = TimeStr;
            parameters[8].Value = BackInfo;

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
            strSql.Append("update ERPSource set ");
            strSql.Append("SourceName=@SourceName,");
            strSql.Append("ShengQingUser=@ShengQingUser,");
            strSql.Append("ShiYongUser=@ShiYongUser,");
            strSql.Append("ShiYongShiXian=@ShiYongShiXian,");
            strSql.Append("SourceMiaoShu=@SourceMiaoShu,");
            strSql.Append("NowState=@NowState,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("BackInfo=@BackInfo");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@SourceName", SqlDbType.VarChar,50),
					new SqlParameter("@ShengQingUser", SqlDbType.VarChar,50),
					new SqlParameter("@ShiYongUser", SqlDbType.VarChar,50),
					new SqlParameter("@ShiYongShiXian", SqlDbType.VarChar,100),
					new SqlParameter("@SourceMiaoShu", SqlDbType.VarChar,5000),
					new SqlParameter("@NowState", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,5000)};
            parameters[0].Value = ID;
            parameters[1].Value = SourceName;
            parameters[2].Value = ShengQingUser;
            parameters[3].Value = ShiYongUser;
            parameters[4].Value = ShiYongShiXian;
            parameters[5].Value = SourceMiaoShu;
            parameters[6].Value = NowState;
            parameters[7].Value = UserName;
            parameters[8].Value = TimeStr;
            parameters[9].Value = BackInfo;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPSource ");
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
            strSql.Append("select  top 1 ID,SourceName,ShengQingUser,ShiYongUser,ShiYongShiXian,SourceMiaoShu,NowState,UserName,TimeStr,BackInfo ");
            strSql.Append(" FROM ERPSource ");
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
                SourceName = ds.Tables[0].Rows[0]["SourceName"].ToString();
                ShengQingUser = ds.Tables[0].Rows[0]["ShengQingUser"].ToString();
                ShiYongUser = ds.Tables[0].Rows[0]["ShiYongUser"].ToString();
                ShiYongShiXian = ds.Tables[0].Rows[0]["ShiYongShiXian"].ToString();
                SourceMiaoShu = ds.Tables[0].Rows[0]["SourceMiaoShu"].ToString();
                NowState = ds.Tables[0].Rows[0]["NowState"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}

