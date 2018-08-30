using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPForm。
    /// </summary>
    public class ERPForm
    {
        public ERPForm()
        { }
        #region Model
        private int _id;
        private string _formname;
        private string _formtype;
        private string _shiyonguserlist;
        private DateTime? _timestr;
        private string _username;
        private string _tiaojianlist;
        private string _contentstr;
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
        public string FormName
        {
            set { _formname = value; }
            get { return _formname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormType
        {
            set { _formtype = value; }
            get { return _formtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShiYongUserList
        {
            set { _shiyonguserlist = value; }
            get { return _shiyonguserlist; }
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
        public string TiaoJianList
        {
            set { _tiaojianlist = value; }
            get { return _tiaojianlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentStr
        {
            set { _contentstr = value; }
            get { return _contentstr; }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPForm");
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
            strSql.Append("insert into ERPForm(");
            strSql.Append("FormName,FormType,ShiYongUserList,TimeStr,UserName,TiaoJianList,ContentStr)");
            strSql.Append(" values (");
            strSql.Append("@FormName,@FormType,@ShiYongUserList,@TimeStr,@UserName,@TiaoJianList,@ContentStr)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@FormName", SqlDbType.VarChar,50),
					new SqlParameter("@FormType", SqlDbType.VarChar,50),
					new SqlParameter("@ShiYongUserList", SqlDbType.VarChar,8000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TiaoJianList", SqlDbType.VarChar,8000),
					new SqlParameter("@ContentStr", SqlDbType.Text)};
            parameters[0].Value = FormName;
            parameters[1].Value = FormType;
            parameters[2].Value = ShiYongUserList;
            parameters[3].Value = TimeStr;
            parameters[4].Value = UserName;
            parameters[5].Value = TiaoJianList;
            parameters[6].Value = ContentStr;

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
            strSql.Append("update ERPForm set ");
            strSql.Append("FormName=@FormName,");
            strSql.Append("FormType=@FormType,");
            strSql.Append("ShiYongUserList=@ShiYongUserList,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TiaoJianList=@TiaoJianList,");
            strSql.Append("ContentStr=@ContentStr");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@FormName", SqlDbType.VarChar,50),
					new SqlParameter("@FormType", SqlDbType.VarChar,50),
					new SqlParameter("@ShiYongUserList", SqlDbType.VarChar,8000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TiaoJianList", SqlDbType.VarChar,8000),
					new SqlParameter("@ContentStr", SqlDbType.Text)};
            parameters[0].Value = ID;
            parameters[1].Value = FormName;
            parameters[2].Value = FormType;
            parameters[3].Value = ShiYongUserList;
            parameters[4].Value = TimeStr;
            parameters[5].Value = UserName;
            parameters[6].Value = TiaoJianList;
            parameters[7].Value = ContentStr;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPForm ");
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
            strSql.Append("select ID,FormName,FormType,ShiYongUserList,TimeStr,UserName,TiaoJianList,ContentStr ");
            strSql.Append(" FROM ERPForm ");
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
                FormName = ds.Tables[0].Rows[0]["FormName"].ToString();
                FormType = ds.Tables[0].Rows[0]["FormType"].ToString();
                ShiYongUserList = ds.Tables[0].Rows[0]["ShiYongUserList"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                TiaoJianList = ds.Tables[0].Rows[0]["TiaoJianList"].ToString();
                ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[FormName],[FormType],[ShiYongUserList],[TimeStr],[UserName],[TiaoJianList],[ContentStr] ");
            strSql.Append(" FROM ERPForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}