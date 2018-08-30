using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPMoBan。
    /// </summary>
    public class ERPMoBan
    {
        public ERPMoBan()
        { }
        #region Model
        private int _id;
        private string _titlestr;
        private string _contentstr;
        private DateTime? _timestr;
        private string _username;
        private string _ifshare;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TitleStr
        {
            set { _titlestr = value; }
            get { return _titlestr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentStr
        {
            set { _contentstr = value; }
            get { return _contentstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IFShare
        {
            set { _ifshare = value; }
            get { return _ifshare; }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPMoBan");
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
            strSql.Append("insert into ERPMoBan(");
            strSql.Append("TitleStr,ContentStr,TimeStr,UserName,IFShare)");
            strSql.Append(" values (");
            strSql.Append("@TitleStr,@ContentStr,@TimeStr,@UserName,@IFShare)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@IFShare", SqlDbType.VarChar,50)};
            parameters[0].Value = TitleStr;
            parameters[1].Value = ContentStr;
            parameters[2].Value = TimeStr;
            parameters[3].Value = UserName;
            parameters[4].Value = IFShare;

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
            strSql.Append("update ERPMoBan set ");
            strSql.Append("TitleStr=@TitleStr,");
            strSql.Append("ContentStr=@ContentStr,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("IFShare=@IFShare");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@IFShare", SqlDbType.VarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = TitleStr;
            parameters[2].Value = ContentStr;
            parameters[3].Value = TimeStr;
            parameters[4].Value = UserName;
            parameters[5].Value = IFShare;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPMoBan ");
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
            strSql.Append("select ID,TitleStr,ContentStr,TimeStr,UserName,IFShare ");
            strSql.Append(" FROM ERPMoBan ");
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
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                IFShare = ds.Tables[0].Rows[0]["IFShare"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[TitleStr],[ContentStr],[TimeStr],[UserName],[IFShare] ");
            strSql.Append(" FROM ERPMoBan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}

