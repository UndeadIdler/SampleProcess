using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPWorkFlow。
    /// </summary>
    public class ERPWorkFlow
    {
        public ERPWorkFlow()
        { }
        #region Model
        private int _id;
        private string _workflowname;
        private string _formname;
        private string _userlist;
        private string _guidangtype;
        private string _username;
        private string _jianjie;
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
        public string WorkFlowName
        {
            set { _workflowname = value; }
            get { return _workflowname; }
        }
        /// <summary>
        /// 对应表单名称
        /// </summary>
        public string FormName
        {
            set { _formname = value; }
            get { return _formname; }
        }
        /// <summary>
        /// 允许哪些人使用
        /// </summary>
        public string UserList
        {
            set { _userlist = value; }
            get { return _userlist; }
        }
        /// <summary>
        /// 归档类型(完成后归档)
        /// </summary>
        public string GuiDangType
        {
            set { _guidangtype = value; }
            get { return _guidangtype; }
        }
        /// <summary>
        /// 制定人
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 流程介绍
        /// </summary>
        public string JianJie
        {
            set { _jianjie = value; }
            get { return _jianjie; }
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPWorkFlow");
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
            strSql.Append("insert into ERPWorkFlow(");
            strSql.Append("WorkFlowName,FormName,UserList,GuiDangType,UserName,JianJie,TimeStr)");
            strSql.Append(" values (");
            strSql.Append("@WorkFlowName,@FormName,@UserList,@GuiDangType,@UserName,@JianJie,@TimeStr)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkFlowName", SqlDbType.VarChar,50),
					new SqlParameter("@FormName", SqlDbType.VarChar,50),
					new SqlParameter("@UserList", SqlDbType.VarChar,8000),
					new SqlParameter("@GuiDangType", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@JianJie", SqlDbType.VarChar,2000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
            parameters[0].Value = WorkFlowName;
            parameters[1].Value = FormName;
            parameters[2].Value = UserList;
            parameters[3].Value = GuiDangType;
            parameters[4].Value = UserName;
            parameters[5].Value = JianJie;
            parameters[6].Value = TimeStr;

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
            strSql.Append("update ERPWorkFlow set ");
            strSql.Append("WorkFlowName=@WorkFlowName,");
            strSql.Append("FormName=@FormName,");
            strSql.Append("UserList=@UserList,");
            strSql.Append("GuiDangType=@GuiDangType,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("JianJie=@JianJie,");
            strSql.Append("TimeStr=@TimeStr");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowName", SqlDbType.VarChar,50),
					new SqlParameter("@FormName", SqlDbType.VarChar,50),
					new SqlParameter("@UserList", SqlDbType.VarChar,8000),
					new SqlParameter("@GuiDangType", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@JianJie", SqlDbType.VarChar,2000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime)};
            parameters[0].Value = ID;
            parameters[1].Value = WorkFlowName;
            parameters[2].Value = FormName;
            parameters[3].Value = UserList;
            parameters[4].Value = GuiDangType;
            parameters[5].Value = UserName;
            parameters[6].Value = JianJie;
            parameters[7].Value = TimeStr;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPWorkFlow ");
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
            strSql.Append("select ID,WorkFlowName,FormName,UserList,GuiDangType,UserName,JianJie,TimeStr ");
            strSql.Append(" FROM ERPWorkFlow ");
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
                WorkFlowName = ds.Tables[0].Rows[0]["WorkFlowName"].ToString();
                FormName = ds.Tables[0].Rows[0]["FormName"].ToString();
                UserList = ds.Tables[0].Rows[0]["UserList"].ToString();
                GuiDangType = ds.Tables[0].Rows[0]["GuiDangType"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                JianJie = ds.Tables[0].Rows[0]["JianJie"].ToString();
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
            strSql.Append("select [ID],[WorkFlowName],[FormName],[UserList],[GuiDangType],[UserName],[JianJie],[TimeStr] ");
            strSql.Append(" FROM ERPWorkFlow ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}