using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPWorkToDo。
    /// </summary>
    public class ERPWorkToDo
    {
        public ERPWorkToDo()
        { }
        #region Model
        private int _id;
        private string _workname;
        private string _formname;
        private string _username;
        private DateTime? _timestr;
        private int? _workflowid;
        private string _formcontent;
        private string _fujianlist;
        private string _shenpiyijian;
        private int? _jiedianid;
        private string _jiedianname;
        private string _shenpirenlist;
        private string _tongguorenlist;
        private string _statenow;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 本次工作名称
        /// </summary>
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }
        /// <summary>
        /// 所用表单
        /// </summary>
        public string FormName
        {
            set { _formname = value; }
            get { return _formname; }
        }
        /// <summary>
        /// 发起人名称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime? TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        /// <summary>
        /// 采用的流程ID
        /// </summary>
        public int? WorkFlowID
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }
        /// <summary>
        /// 表单内容
        /// </summary>
        public string FormContent
        {
            set { _formcontent = value; }
            get { return _formcontent; }
        }
        /// <summary>
        /// 附件列表
        /// </summary>
        public string FuJianList
        {
            set { _fujianlist = value; }
            get { return _fujianlist; }
        }
        /// <summary>
        /// 审批意见（可含附件）
        /// </summary>
        public string ShenPiYiJian
        {
            set { _shenpiyijian = value; }
            get { return _shenpiyijian; }
        }
        /// <summary>
        /// 流转到的节点ID
        /// </summary>
        public int? JieDianID
        {
            set { _jiedianid = value; }
            get { return _jiedianid; }
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string JieDianName
        {
            set { _jiedianname = value; }
            get { return _jiedianname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShenPiRenList
        {
            set { _shenpirenlist = value; }
            get { return _shenpirenlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TongGuoRenList
        {
            set { _tongguorenlist = value; }
            get { return _tongguorenlist; }
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string StateNow
        {
            set { _statenow = value; }
            get { return _statenow; }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPWorkToDo");
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
            strSql.Append("insert into ERPWorkToDo(");
            strSql.Append("WorkName,FormName,UserName,TimeStr,WorkFlowID,FormContent,FuJianList,ShenPiYiJian,JieDianID,JieDianName,ShenPiRenList,TongGuoRenList,StateNow)");
            strSql.Append(" values (");
            strSql.Append("@WorkName,@FormName,@UserName,@TimeStr,@WorkFlowID,@FormContent,@FuJianList,@ShenPiYiJian,@JieDianID,@JieDianName,@ShenPiRenList,@TongGuoRenList,@StateNow)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkName", SqlDbType.VarChar,200),
					new SqlParameter("@FormName", SqlDbType.VarChar,200),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@FormContent", SqlDbType.Text),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
					new SqlParameter("@ShenPiYiJian", SqlDbType.Text),
					new SqlParameter("@JieDianID", SqlDbType.Int,4),
					new SqlParameter("@JieDianName", SqlDbType.VarChar,200),
					new SqlParameter("@ShenPiRenList", SqlDbType.VarChar,8000),
					new SqlParameter("@TongGuoRenList", SqlDbType.VarChar,8000),
					new SqlParameter("@StateNow", SqlDbType.VarChar,50)};
            parameters[0].Value = WorkName;
            parameters[1].Value = FormName;
            parameters[2].Value = UserName;
            parameters[3].Value = TimeStr;
            parameters[4].Value = WorkFlowID;
            parameters[5].Value = FormContent;
            parameters[6].Value = FuJianList;
            parameters[7].Value = ShenPiYiJian;
            parameters[8].Value = JieDianID;
            parameters[9].Value = JieDianName;
            parameters[10].Value = ShenPiRenList;
            parameters[11].Value = TongGuoRenList;
            parameters[12].Value = StateNow;

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
            strSql.Append("update ERPWorkToDo set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("FormName=@FormName,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("WorkFlowID=@WorkFlowID,");
            strSql.Append("FormContent=@FormContent,");
            strSql.Append("FuJianList=@FuJianList,");
            strSql.Append("ShenPiYiJian=@ShenPiYiJian,");
            strSql.Append("JieDianID=@JieDianID,");
            strSql.Append("JieDianName=@JieDianName,");
            strSql.Append("ShenPiRenList=@ShenPiRenList,");
            strSql.Append("TongGuoRenList=@TongGuoRenList,");
            strSql.Append("StateNow=@StateNow");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@WorkName", SqlDbType.VarChar,200),
					new SqlParameter("@FormName", SqlDbType.VarChar,200),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@FormContent", SqlDbType.Text),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
					new SqlParameter("@ShenPiYiJian", SqlDbType.Text),
					new SqlParameter("@JieDianID", SqlDbType.Int,4),
					new SqlParameter("@JieDianName", SqlDbType.VarChar,200),
					new SqlParameter("@ShenPiRenList", SqlDbType.VarChar,8000),
					new SqlParameter("@TongGuoRenList", SqlDbType.VarChar,8000),
					new SqlParameter("@StateNow", SqlDbType.VarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = WorkName;
            parameters[2].Value = FormName;
            parameters[3].Value = UserName;
            parameters[4].Value = TimeStr;
            parameters[5].Value = WorkFlowID;
            parameters[6].Value = FormContent;
            parameters[7].Value = FuJianList;
            parameters[8].Value = ShenPiYiJian;
            parameters[9].Value = JieDianID;
            parameters[10].Value = JieDianName;
            parameters[11].Value = ShenPiRenList;
            parameters[12].Value = TongGuoRenList;
            parameters[13].Value = StateNow;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateBD()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPWorkToDo set ");
            strSql.Append("FormContent=@FormContent ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@FormContent", SqlDbType.Text)};
            parameters[0].Value = ID;
            parameters[1].Value = FormContent;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPWorkToDo ");
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
            strSql.Append("select ID,WorkName,FormName,UserName,TimeStr,WorkFlowID,FormContent,FuJianList,ShenPiYiJian,JieDianID,JieDianName,ShenPiRenList,TongGuoRenList,StateNow ");
            strSql.Append(" FROM ERPWorkToDo ");
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
                WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                FormName = ds.Tables[0].Rows[0]["FormName"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                FormContent = ds.Tables[0].Rows[0]["FormContent"].ToString();
                FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                ShenPiYiJian = ds.Tables[0].Rows[0]["ShenPiYiJian"].ToString();
                if (ds.Tables[0].Rows[0]["JieDianID"].ToString() != "")
                {
                    JieDianID = int.Parse(ds.Tables[0].Rows[0]["JieDianID"].ToString());
                }
                JieDianName = ds.Tables[0].Rows[0]["JieDianName"].ToString();
                ShenPiRenList = ds.Tables[0].Rows[0]["ShenPiRenList"].ToString();
                TongGuoRenList = ds.Tables[0].Rows[0]["TongGuoRenList"].ToString();
                StateNow = ds.Tables[0].Rows[0]["StateNow"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[WorkName],[FormName],[UserName],[TimeStr],[WorkFlowID],[FormContent],[FuJianList],[ShenPiYiJian],[JieDianID],[JieDianName],[ShenPiRenList],[TongGuoRenList],[StateNow] ");
            strSql.Append(" FROM ERPWorkToDo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}

