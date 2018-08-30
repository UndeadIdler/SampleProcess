using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPWorkFlowJieDian。
    /// </summary>
    public class ERPWorkFlowJieDian
    {
        public ERPWorkFlowJieDian()
        { }
        #region Model
        private int _id;
        private int? _workflowid;
        private string _jiedianserils;
        private string _jiedianname;
        private string _jiedianaddr;
        private string _nextjiedianserils;
        private string _pingshenmoshi;
        private string _shengpiusersetting;
        private int? _jieshuhours;
        private string _ifeditfile;
        private string _ifdelfile;
        private string _tiaojiantiaozhuan;
        private string _jingbanrenyuanlist;
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
        public int? WorkFlowID
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }
        /// <summary>
        /// 步骤编号
        /// </summary>
        public string JieDianSerils
        {
            set { _jiedianserils = value; }
            get { return _jiedianserils; }
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
        /// 节点所处位置
        /// </summary>
        public string JieDianAddr
        {
            set { _jiedianaddr = value; }
            get { return _jiedianaddr; }
        }
        /// <summary>
        /// 下一节点序号(列表)
        /// </summary>
        public string NextJieDianSerils
        {
            set { _nextjiedianserils = value; }
            get { return _nextjiedianserils; }
        }
        /// <summary>
        /// 评审模式
        /// </summary>
        public string PingShenMoShi
        {
            set { _pingshenmoshi = value; }
            get { return _pingshenmoshi; }
        }
        /// <summary>
        /// 审批人员选择
        /// </summary>
        public string ShengPiUserSetting
        {
            set { _shengpiusersetting = value; }
            get { return _shengpiusersetting; }
        }
        /// <summary>
        /// 多长时间未审批自动结束
        /// </summary>
        public int? JieShuHours
        {
            set { _jieshuhours = value; }
            get { return _jieshuhours; }
        }
        /// <summary>
        /// 是否允许编辑附件
        /// </summary>
        public string IFEditFile
        {
            set { _ifeditfile = value; }
            get { return _ifeditfile; }
        }
        /// <summary>
        /// 是否允许删除附件
        /// </summary>
        public string IFDelFile
        {
            set { _ifdelfile = value; }
            get { return _ifdelfile; }
        }
        /// <summary>
        /// 条件跳转
        /// </summary>
        public string TiaoJianTiaoZhuan
        {
            set { _tiaojiantiaozhuan = value; }
            get { return _tiaojiantiaozhuan; }
        }
        /// <summary>
        /// 经办人列表
        /// </summary>
        public string JingBanRenYuanList
        {
            set { _jingbanrenyuanlist = value; }
            get { return _jingbanrenyuanlist; }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPWorkFlowJieDian");
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
            strSql.Append("insert into ERPWorkFlowJieDian(");
            strSql.Append("WorkFlowID,JieDianSerils,JieDianName,JieDianAddr,NextJieDianSerils,PingShenMoShi,ShengPiUserSetting,JieShuHours,IFEditFile,IFDelFile,TiaoJianTiaoZhuan,JingBanRenYuanList)");
            strSql.Append(" values (");
            strSql.Append("@WorkFlowID,@JieDianSerils,@JieDianName,@JieDianAddr,@NextJieDianSerils,@PingShenMoShi,@ShengPiUserSetting,@JieShuHours,@IFEditFile,@IFDelFile,@TiaoJianTiaoZhuan,@JingBanRenYuanList)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@JieDianSerils", SqlDbType.VarChar,50),
					new SqlParameter("@JieDianName", SqlDbType.VarChar,50),
					new SqlParameter("@JieDianAddr", SqlDbType.VarChar,50),
					new SqlParameter("@NextJieDianSerils", SqlDbType.VarChar,50),
					new SqlParameter("@PingShenMoShi", SqlDbType.VarChar,500),
					new SqlParameter("@ShengPiUserSetting", SqlDbType.VarChar,50),
					new SqlParameter("@JieShuHours", SqlDbType.Int,4),
					new SqlParameter("@IFEditFile", SqlDbType.VarChar,50),
					new SqlParameter("@IFDelFile", SqlDbType.VarChar,50),
					new SqlParameter("@TiaoJianTiaoZhuan", SqlDbType.VarChar,8000),
					new SqlParameter("@JingBanRenYuanList", SqlDbType.VarChar,8000)};
            parameters[0].Value = WorkFlowID;
            parameters[1].Value = JieDianSerils;
            parameters[2].Value = JieDianName;
            parameters[3].Value = JieDianAddr;
            parameters[4].Value = NextJieDianSerils;
            parameters[5].Value = PingShenMoShi;
            parameters[6].Value = ShengPiUserSetting;
            parameters[7].Value = JieShuHours;
            parameters[8].Value = IFEditFile;
            parameters[9].Value = IFDelFile;
            parameters[10].Value = TiaoJianTiaoZhuan;
            parameters[11].Value = JingBanRenYuanList;

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
            strSql.Append("update ERPWorkFlowJieDian set ");
            strSql.Append("WorkFlowID=@WorkFlowID,");
            strSql.Append("JieDianSerils=@JieDianSerils,");
            strSql.Append("JieDianName=@JieDianName,");
            strSql.Append("JieDianAddr=@JieDianAddr,");
            strSql.Append("NextJieDianSerils=@NextJieDianSerils,");
            strSql.Append("PingShenMoShi=@PingShenMoShi,");
            strSql.Append("ShengPiUserSetting=@ShengPiUserSetting,");
            strSql.Append("JieShuHours=@JieShuHours,");
            strSql.Append("IFEditFile=@IFEditFile,");
            strSql.Append("IFDelFile=@IFDelFile,");
            strSql.Append("TiaoJianTiaoZhuan=@TiaoJianTiaoZhuan,");
            strSql.Append("JingBanRenYuanList=@JingBanRenYuanList");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@JieDianSerils", SqlDbType.VarChar,50),
					new SqlParameter("@JieDianName", SqlDbType.VarChar,50),
					new SqlParameter("@JieDianAddr", SqlDbType.VarChar,50),
					new SqlParameter("@NextJieDianSerils", SqlDbType.VarChar,50),
					new SqlParameter("@PingShenMoShi", SqlDbType.VarChar,500),
					new SqlParameter("@ShengPiUserSetting", SqlDbType.VarChar,50),
					new SqlParameter("@JieShuHours", SqlDbType.Int,4),
					new SqlParameter("@IFEditFile", SqlDbType.VarChar,50),
					new SqlParameter("@IFDelFile", SqlDbType.VarChar,50),
					new SqlParameter("@TiaoJianTiaoZhuan", SqlDbType.VarChar,8000),
					new SqlParameter("@JingBanRenYuanList", SqlDbType.VarChar,8000)};
            parameters[0].Value = ID;
            parameters[1].Value = WorkFlowID;
            parameters[2].Value = JieDianSerils;
            parameters[3].Value = JieDianName;
            parameters[4].Value = JieDianAddr;
            parameters[5].Value = NextJieDianSerils;
            parameters[6].Value = PingShenMoShi;
            parameters[7].Value = ShengPiUserSetting;
            parameters[8].Value = JieShuHours;
            parameters[9].Value = IFEditFile;
            parameters[10].Value = IFDelFile;
            parameters[11].Value = TiaoJianTiaoZhuan;
            parameters[12].Value = JingBanRenYuanList;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPWorkFlowJieDian ");
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
            strSql.Append("select ID,WorkFlowID,JieDianSerils,JieDianName,JieDianAddr,NextJieDianSerils,PingShenMoShi,ShengPiUserSetting,JieShuHours,IFEditFile,IFDelFile,TiaoJianTiaoZhuan,JingBanRenYuanList ");
            strSql.Append(" FROM ERPWorkFlowJieDian ");
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
                if (ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                JieDianSerils = ds.Tables[0].Rows[0]["JieDianSerils"].ToString();
                JieDianName = ds.Tables[0].Rows[0]["JieDianName"].ToString();
                JieDianAddr = ds.Tables[0].Rows[0]["JieDianAddr"].ToString();
                NextJieDianSerils = ds.Tables[0].Rows[0]["NextJieDianSerils"].ToString();
                PingShenMoShi = ds.Tables[0].Rows[0]["PingShenMoShi"].ToString();
                ShengPiUserSetting = ds.Tables[0].Rows[0]["ShengPiUserSetting"].ToString();
                if (ds.Tables[0].Rows[0]["JieShuHours"].ToString() != "")
                {
                    JieShuHours = int.Parse(ds.Tables[0].Rows[0]["JieShuHours"].ToString());
                }
                IFEditFile = ds.Tables[0].Rows[0]["IFEditFile"].ToString();
                IFDelFile = ds.Tables[0].Rows[0]["IFDelFile"].ToString();
                TiaoJianTiaoZhuan = ds.Tables[0].Rows[0]["TiaoJianTiaoZhuan"].ToString();
                JingBanRenYuanList = ds.Tables[0].Rows[0]["JingBanRenYuanList"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[WorkFlowID],[JieDianSerils],[JieDianName],[JieDianAddr],[NextJieDianSerils],[PingShenMoShi],[ShengPiUserSetting],[JieShuHours],[IFEditFile],[IFDelFile],[TiaoJianTiaoZhuan],[JingBanRenYuanList] ");
            strSql.Append(" FROM ERPWorkFlowJieDian ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}