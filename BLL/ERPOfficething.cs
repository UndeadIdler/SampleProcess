using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPOfficething。
    /// </summary>
    public class ERPOfficething
    {
        public ERPOfficething()
        { }
        #region Model
        private int _id;
        private string _shenqingthing;
        private string _typestr;
        private string _shengqingnum;
        private string _nowstate;
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
        /// 
        /// </summary>
        public string ShenQingThing
        {
            set { _shenqingthing = value; }
            get { return _shenqingthing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeStr
        {
            set { _typestr = value; }
            get { return _typestr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShengQingNum
        {
            set { _shengqingnum = value; }
            get { return _shengqingnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NowState
        {
            set { _nowstate = value; }
            get { return _nowstate; }
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
        public ERPOfficething(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ShenQingThing,TypeStr,ShengQingNum,NowState,UserName,TimeStr ");
            strSql.Append(" FROM ERPOfficething ");
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
                ShenQingThing = ds.Tables[0].Rows[0]["ShenQingThing"].ToString();
                TypeStr = ds.Tables[0].Rows[0]["TypeStr"].ToString();
                ShengQingNum = ds.Tables[0].Rows[0]["ShengQingNum"].ToString();
                NowState = ds.Tables[0].Rows[0]["NowState"].ToString();
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
            strSql.Append("select count(1) from ERPOfficething");
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
            strSql.Append("insert into ERPOfficething(");
            strSql.Append("ShenQingThing,TypeStr,ShengQingNum,NowState,UserName,TimeStr)");
            strSql.Append(" values (");
            strSql.Append("@ShenQingThing,@TypeStr,@ShengQingNum,@NowState,@UserName,@TimeStr)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ShenQingThing", SqlDbType.VarChar,50),
					new SqlParameter("@TypeStr", SqlDbType.VarChar,50),
					new SqlParameter("@ShengQingNum", SqlDbType.VarChar,50),
					new SqlParameter("@NowState", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
            parameters[0].Value = ShenQingThing;
            parameters[1].Value = TypeStr;
            parameters[2].Value = ShengQingNum;
            parameters[3].Value = NowState;
            parameters[4].Value = UserName;
            parameters[5].Value = TimeStr;

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
            strSql.Append("update ERPOfficething set ");
            strSql.Append("ShenQingThing=@ShenQingThing,");
            strSql.Append("TypeStr=@TypeStr,");
            strSql.Append("ShengQingNum=@ShengQingNum,");
            strSql.Append("NowState=@NowState,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TimeStr=@TimeStr");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ShenQingThing", SqlDbType.VarChar,50),
					new SqlParameter("@TypeStr", SqlDbType.VarChar,50),
					new SqlParameter("@ShengQingNum", SqlDbType.VarChar,50),
					new SqlParameter("@NowState", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
            parameters[0].Value = ID;
            parameters[1].Value = ShenQingThing;
            parameters[2].Value = TypeStr;
            parameters[3].Value = ShengQingNum;
            parameters[4].Value = NowState;
            parameters[5].Value = UserName;
            parameters[6].Value = TimeStr;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPOfficething ");
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
            strSql.Append("select  top 1 ID,ShenQingThing,TypeStr,ShengQingNum,NowState,UserName,TimeStr ");
            strSql.Append(" FROM ERPOfficething ");
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
                ShenQingThing = ds.Tables[0].Rows[0]["ShenQingThing"].ToString();
                TypeStr = ds.Tables[0].Rows[0]["TypeStr"].ToString();
                ShengQingNum = ds.Tables[0].Rows[0]["ShengQingNum"].ToString();
                NowState = ds.Tables[0].Rows[0]["NowState"].ToString();
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
            strSql.Append(" FROM ERPOfficething ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}

