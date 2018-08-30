using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;


namespace WebApp.Components
{
    ///<summary>
    ///MyDataOp 的摘要说明
    ///数据库操作类 支持操作：查询，添加，删除，修改，事务
    ///建立对象：MyDataOp(strSql) 其中strSql为SQL语句
    ///查询：CreatReader()，CreatDataSet()
    ///添加，删除，修改：ExecuteCommand()
    ///操作完成后自动关闭数据库连接
    ///</summary>
    
    public class MyDataOp
    {   
        private string strSql;//存放Sql语句
       private string strConn;//存放连接数据库的参数
        private SqlConnection sqlConn;
        
        public MyDataOp(string strSqlPra)
        {
            strSql = strSqlPra;
            strConn =  System.Configuration.ConfigurationManager.AppSettings["connString"]; 
        }
        
        /// <summary>
        /// 执行查询，将查询结果以DataReader的形式返回
        /// </summary>
        /// <returns>DataReader</returns>
        public SqlDataReader CreateReader()
        {
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand(strSql, sqlConn);
            sqlConn.Open();
            SqlDataReader sqlReader = sqlComm.ExecuteReader();
            return sqlReader;
        }
        
        /// <summary>
        /// 执行查询操做，将结果以DataSet的形式返回
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet CreateDataSet()
        {
            sqlConn = new SqlConnection(strConn);
            SqlDataAdapter sqlAdpt = new SqlDataAdapter(strSql, sqlConn);
            DataSet ds = new DataSet();
            sqlAdpt.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 执行查询操做，将结果以DataSet的形式返回
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet CreateDataSetP(string str)
        {
            sqlConn = new SqlConnection(strConn);
            SqlDataAdapter sqlAdpt = new SqlDataAdapter();
            sqlAdpt.SelectCommand = new SqlCommand();
            sqlAdpt.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdpt.SelectCommand.CommandText = str;
            sqlAdpt.SelectCommand.Connection = sqlConn;
            DataSet ds = new DataSet();
            sqlAdpt.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行添加、删除、修改等操作
        /// </summary>
        /// <returns>操作是否成功,成功则返回true</returns>
        public bool ExecuteCommand(string sql)
        {
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand(sql, sqlConn);
            sqlConn.Open();
            try
            {
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 执行添加、删除、修改等操作
        /// </summary>
        /// <returns>操作是否成功,成功则返回true</returns>
        public bool ExecuteCommand()
        {
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand(strSql, sqlConn);
            sqlConn.Open();
            try
            {
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 执行添加、删除、修改等操作
        /// </summary>
        /// <returns>操作是否成功,成功则返回true</returns>
        public int ExecuteCommandpro(Dictionary<string, string> bytepara, string sqlstr)
        {
            int ret =-1;
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.CommandText = sqlstr;
            foreach(KeyValuePair<string,string> key in bytepara)
            {
                SqlParameter para = new SqlParameter(key.Key, key.Value);
                sqlComm.Parameters.Add(para);
            }

            sqlConn.Open();
            try
            {
                sqlComm.Connection = sqlConn;
                ret = sqlComm.ExecuteNonQuery(); 
               
            }
            catch
            {
                ret = -1; ;
            }
            finally
            {
                sqlConn.Close();
            }
            return ret;
        }
        /// <summary>
        /// 执行添加、删除、修改等操作
        /// </summary>
        /// <returns>操作是否成功,成功则返回true</returns>
        public bool ExecuteCommandpara(byte[] bytepara,StringBuilder sqlstr)
        {
            bool ret = false;
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand();
            SqlParameter para=new SqlParameter("@content",SqlDbType.Binary);
            para.Value=bytepara;
           
            sqlComm.CommandText = sqlstr.ToString();
            sqlComm.Parameters.Add(para);

            sqlConn.Open();
            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.ExecuteNonQuery();

                ret = true; ;
            }
            catch
            {
                ret=false;
            }
            finally
            {
                sqlConn.Close();
            }
            return ret;
        }
        /// <summary>
        /// 执行事务处理
        /// </summary>
        /// <param name="n">同时操作的任务数量</param>
        /// <param name="arr_strSql">存放用于操作的所有Sql语句</param>
        /// <returns>操作是否成功，成功则返回true</returns>
        public bool DoTran(int n,string[] arr_strSql)
        {
            bool blSuccess = false;
            //建立连接并打开
            sqlConn = new SqlConnection(strConn);
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand();

            //SqlTransaction sqlTran=new SqlTransaction();
            //注意，SqlTransaction类无公开的构造函数
            SqlTransaction sqlTran;
            //创建一个事务
            sqlTran = sqlConn.BeginTransaction();
            //从此开始，基于该连接的数据操作都被认为是事务的一部分

            try
            {
                //下面绑定连接和事务对象
                sqlComm.Connection = sqlConn;
                sqlComm.Transaction = sqlTran;

                //在每次事务执行之前都检查其有效性显得代价太高——绝大多数的情况下这种耗时的检查是不必要的。
                //事务存储点提供了一种机制，用于回滚部分事务。因此，我们可以不必在更新之前检查更新的有效性，
                //而是预设一个存储点，在更新之后，如果没有出现错误，就继续执行，否则回滚到更新之前的存储点。
                //存储点的作用就在于此。要注意的是，更新和回滚代价很大，只有在遇到错误的可能性很小，
                //而且预先检查更新的有效性的代价相对很高的情况下，使用存储点才会非常有效。

                //设定存储点
                sqlTran.Save("NoUpdate");

                //更新数据
                for (int i = 0; i < n; i++)
                {
                    sqlComm.CommandText = arr_strSql[i];
                    sqlComm.ExecuteNonQuery();
                }
                 
                //提交事务
                sqlTran.Commit();
                blSuccess = true;
            }
            catch (Exception err)
            {
                //不使用存储点
                //sqlTran.Rollback(); 
                //更新错误，回滚到指定存储点
                sqlTran.Rollback("NoUpdate");

                blSuccess = false;
            }
            finally
            {
                sqlConn.Close();      
            }
            return blSuccess;
        }
    }


    /// <summary>
    /// 此类为一些通用静态函数的集合
    /// </summary>
    public class MyStaVoid
    {
        public MyStaVoid()
        {
        }

        /// <summary>
        /// 将查询的结果绑定到指定的DropDownList中，
        /// </summary>
        /// <param name="strText">DropDownList中前台列表中显示的文字所对应的字段</param>
        /// <param name="strValue">DropDownList中后台对应的值所对应的字段</param>
        /// <param name="strSqlPra">获得绑定内容的查询语句</param>
        /// <param name="drop_TobeBundled">待绑定的DropDownList控件</param>
        /// <returns>返回绑定结果，成功则为true</returns>
        public static bool BindList(string strText, string strValue, string strSqlPra, System.Web.UI.WebControls.DropDownList drop_TobeBundled)
        {
            try
            {
                string strSql = strSqlPra;

                MyDataOp mdo = new MyDataOp(strSql);
                DataSet ds = mdo.CreateDataSet();
                drop_TobeBundled.DataTextField = strText;
                drop_TobeBundled.DataValueField = strValue;
                drop_TobeBundled.DataSource = ds;
                drop_TobeBundled.DataBind();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 将查询的结果绑定到指定的DropDownList中，
        /// </summary>
        /// <param name="strText">DropDownList中前台列表中显示的文字所对应的字段</param>
        /// <param name="strValue">DropDownList中后台对应的值所对应的字段</param>
        /// <param name="strSqlPra">获得绑定内容的查询语句</param>
        /// <param name="drop_TobeBundled">待绑定的DropDownList控件</param>
        /// <returns>返回绑定结果，成功则为true</returns>
        public static bool BindList(string strText, string strValue, string strSqlPra, System.Web.UI.WebControls.CheckBoxList drop_TobeBundled)
        {
            try
            {
                string strSql = strSqlPra;

                MyDataOp mdo = new MyDataOp(strSql);
                DataSet ds = mdo.CreateDataSet();
                drop_TobeBundled.DataTextField = strText;
                drop_TobeBundled.DataValueField = strValue;
                drop_TobeBundled.DataSource = ds;
                drop_TobeBundled.DataBind();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 获得系统表中每级所对应的名称
        /// </summary>
        /// <param name="i">级别数，1～4</param>
        /// <returns>返回该级别的名称</returns>
        public static string getScaleName(int i)
        {
            string strSql = "select * from t_系统信息";
            DataSet ds = new MyDataOp(strSql).CreateDataSet();
            string strTmp = ds.Tables[0].Rows[0][i].ToString();
            return strTmp;
        }
        /// <summary>
        /// 将水井代码变为6位的固定长度字符串，不足6位前面补零
        /// </summary>
        /// <param name="strCode">水井代码</param>
        /// <returns>长度为6位的水井代码</returns>
        public static string CodeConvet(string strCode)
        {
            string s = strCode;
            s = "000000" + s;
            s = s.Substring(s.Length - 6, 6);
            return s;
        }

        public static string CodeConvet(string strCode, int m)
        {
            string s = strCode;
            for (int i = 0; i < m; i++)
            {
                s = "0" + s;
            }
            s = s.Substring(s.Length - m, m);
            return s;
        }


        /// <summary>
        /// 字节数组转浮点数
        /// </summary>
        /// <param name="bytBuf">字节数组</param>
        /// <param name="uiStartIndex">起始下标</param>
        /// <param name="bytSubScriptOrder">读取方向</param>
        /// <returns>转换后的浮点数</returns>
        public static Single ByteArrayToFloat(byte[] bytBuf, uint uiStartIndex, byte bytSubScriptOrder)
        {
            byte[] bytBuf_ = new byte[4];
            int j = 0;
            if (bytSubScriptOrder == 0)
            {
                for (j = 3; j >= 0; j--)
                {
                    bytBuf_[j] = bytBuf[uiStartIndex++];
                }
            }
            else
            {
                for (j = 0; j < 4; j++)
                {
                    bytBuf_[j] = bytBuf[j];
                }
            }
            return System.BitConverter.ToSingle(bytBuf_, 0);
        }

        /// <summary>
        /// 浮点数转字节数组
        /// </summary>
        /// <param name="bytOutBuf">转换后的字节数组</param>
        /// <param name="flValue">待转换的浮点数</param>
        /// <param name="uiStartIndex">存数的起始下标</param>
        /// <param name="bytSubScriptOrder">存储方向</param>
        public static void FloatToByteArray(ref byte[] bytOutBuf, float flValue, uint uiStartIndex, byte bytSubScriptOrder)
        {
            byte[] bytBuf_ = new byte[4];
            int j = 0;
            bytBuf_ = System.BitConverter.GetBytes(flValue);
            if (bytSubScriptOrder == 0)
            {
                for (j = 3; j >= 0; j--)
                {
                    bytOutBuf[uiStartIndex++] = bytBuf_[j];
                }
            }
            else
            {
                for (j = 0; j < 4; j++)
                {
                    bytOutBuf[uiStartIndex++] = bytBuf_[j];
                }
            }
        }
        //public Array stringChange(string str)
        //{
        //    string temp = str.Split(',');

        //}
        public static bool isWrite(string UserID, string PageName)
        {
            bool Write = false;
            string sql = "select count(*) from t_R_User_IsWrite where userid = " + UserID + " and (isWrite = 1) and menuid in (select id from t_R_Menu where RelativeFile = '" + PageName + "')";
            DataSet ds = new MyDataOp(sql).CreateDataSet();
            if (ds.Tables[0].Rows[0][0].ToString() != "0")
            {
                Write = true;
            }
            return Write;
        }     
    }
         /// <summary>
    /// 此类为日志保存类
    /// </summary>
    public static class Log
    {
      
      public static void SaveLog(string msgstr,string userid,int functionid)
        {
            string str = "insert into t_L_LogInfo(OperateID,LogMessage,UserID,LogDate)values(" + functionid + ",'" + msgstr + "','" + userid + "',getdate())";

            MyDataOp doobj = new MyDataOp(str);
            doobj.ExecuteCommand();

        }
      public static void SaveLogY(string msgstr, string userid, int functionid)
      {
          string str = "insert into t_Y_LogInfo(functionid,LogMessage,UserID,LogDate)values(" + functionid + ",'" + msgstr + "','" + userid + "',getdate())";

          MyDataOp doobj = new MyDataOp(str);
          doobj.ExecuteCommand();

      }

    }
}

