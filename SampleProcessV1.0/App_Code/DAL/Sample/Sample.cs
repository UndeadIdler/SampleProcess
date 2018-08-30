using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccess;
using System.Configuration;
using WebApp.Components;

namespace DAl
{
/// <summary>
///Report 的摘要说明
/// </summary>
    public class Sample
    {
        public Sample()
        {
        }
        public string GetSampleID()
        {
            string NO = "";

            return NO;

        }
        public string getType(string type)
        {
            //类型
            string typeclass = "";
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           
            string sql = "select NOClass from t_M_AnalysisMainClassEx where   ClassID ='" + type + "'";

            DataTable dt = new DataTable();
            IDbDataParameter[] prams = {
                                                };
            IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, sql, prams);
            dt.Load(idr);
            if (dt.Rows.Count > 0)
            {
               typeclass = dt.Rows[0][0].ToString();
            }
            return typeclass;
        }
        /// <summary>
        /// 检查样品编号是否唯一
        /// </summary>
        /// <param name="SampleID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CheckNumber(string SampleID,int id)
        {
            int ret = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);

            string constr="";
            if (id != 0)
                constr += " and id!='" + id + "'";
            string sql = "select count(*) from t_M_SampleInfor where   SampleID ='" + SampleID + "'" + constr;

            DataTable dt = new DataTable();
            IDbDataParameter[] prams = {
                                                };
            IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, sql, prams);
            dt.Load(idr);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                    ret = 1;
                else
                    ret = 0;
            }
            else
                ret = -1;

            return ret;
        }
        private int Newtkbh(string type,string Date, ref string bh)
        {
            //生成编号
             int newbh =1;
           
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           // string Date = DateTime.Now.ToString("yyMMdd");
           string sql = "select LastBh from t_最新编号 where BhType = '" + type + "' and  Date ='" + Date + "'";

            DataTable dt = new DataTable();
            IDbDataParameter[] prams = {
                                                };
            IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, sql, prams);
            dt.Load(idr);
            if (dt.Rows.Count > 0)
            {
                string num = dt.Rows[0][0].ToString();

                newbh = int.Parse(num) + 1;
                string newbhs = "00" + newbh.ToString();
                bh = type + Date + newbhs.Substring(newbhs.Length-3,3);
                if (type == "降")
                    bh = type + Date;
            }
            else
            {
                bh = type + Date + "001";
                newbh   = 1;
                if (type == "降")
                    bh = type + Date;
                string inSql = "insert into t_最新编号(LastBh,BhType,Date) values('" + newbh + "','"+type+"','"+Date+"')";
              int iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, inSql, prams);
            }
            return newbh;
        }
        /// <summary>
        /// 添加样品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddSample(List<Entity.Sample> entityList)
        {
            int iReturn = 0;
           
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            // DrawSample drawop=new DrawSample();
            try
            {


                foreach (Entity.Sample entity in entityList)
                {
                    //int checkid = CheckNumber(entity.SampleID, entity.ID);
                    //if (checkid == 1)
                    //{

                        //entity.NO = int.Parse(entity.SampleID.Substring(entity.SampleID.Length - 3, 3));
                        IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                 dbFactory.MakeInParam("@AccessDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.AccessDate.GetType().ToString()), entity.AccessDate, 50) ,              
                 dbFactory.MakeInParam("@ReportID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.ReportID.GetType().ToString()),  entity.ReportID , 50) ,
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@SampleDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.SampleDate .GetType().ToString()),  entity.SampleDate , 50) ,
                 dbFactory.MakeInParam("@TypeID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TypeID.GetType().ToString()), entity.TypeID,4),  
                 dbFactory.MakeInParam("@SampleSource",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleSource.GetType().ToString()), entity.SampleSource,50),  
                 dbFactory.MakeInParam("@SampleAddress",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleAddress.GetType().ToString()), entity.SampleAddress, 50), 
                 dbFactory.MakeInParam("@SampleProperty",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleProperty.GetType().ToString()), entity.SampleProperty, 50), 
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.CreateDate.GetType().ToString()), entity.CreateDate, 50),  
                 dbFactory.MakeInParam("@xcflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.xcflag.GetType().ToString()), entity.xcflag, 50),  
                 dbFactory.MakeInParam("@ypstatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ypstatus.GetType().ToString()), entity.ypstatus, 4),                               
                 dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.StatusID.GetType().ToString()), entity.StatusID, 4),  
                 dbFactory.MakeInParam("@datastatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.datastatus.GetType().ToString()), entity.datastatus,4),  
                dbFactory.MakeInParam("@TestMan",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TestMan.GetType().ToString()), entity.TestMan,0),  
               dbFactory.MakeInParam("@bzid",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bzid.GetType().ToString()), entity.bzid,50),  
               dbFactory.MakeInParam("@num",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.num.GetType().ToString()), entity.num,4),
                     dbFactory.MakeInParam("@starttime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.starttime.GetType().ToString()), entity.starttime,50),  
               dbFactory.MakeInParam("@endtime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.endtime.GetType().ToString()), entity.endtime,50),  
              
               
                 dbFactory.MakeOutReturnParam()
               };

                        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Sample_Add]", prams);

                        int recordid = int.Parse(prams[20].Value.ToString());

                        if (recordid > 0)
                        {
                            entity.ID = recordid;
                        } if (iReturn == 0)
                        {
                            return 0;
                        }
                        else if (iReturn < 0)
                            throw new Exception("样品保存失败！");
                        foreach (Entity.SampleItem item in entity.SampleItemList)
                        {
                            if (item.ckflag == 0)//非现场
                            {
                                if (item.zpto != "")//有指派 
                                {
                                    IDbDataParameter[] itempramszp = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 0),
                                     dbFactory.MakeInParam("@zpcreateuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpcreateuser.GetType().ToString()), item.zpcreateuser, 0),
                                     dbFactory.MakeInParam("@zpdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpdate.GetType().ToString()), item.zpdate, 0),
                                     dbFactory.MakeInParam("@zpto",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpto.GetType().ToString()), item.zpto, 0),
                                     dbFactory.MakeInParam("@flag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 0),
                                     dbFactory.MakeOutReturnParam()
                                                                 };
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_zp", itempramszp);

                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                    item.ID = int.Parse(itempramszp[9].Value.ToString());
                                    //保存指派记录表
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into t_ZPInfo(SampleID,ToUser,zpdate,zpcreateuser,MonitorDetailID,CreateDate,CreateUser)values('" + entity.SampleID + "','" + item.zpto + "','" + item.CreateDate + "','" + item.UserID + "','" + item.ID + "',getdate(),'" + item.UserID + "')", itempramszp);
                                    itempramszp = null;
                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                }
                                else//无指派
                                {
                                    IDbDataParameter[] itemprams = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 4)
                                                               };
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add", itemprams);
                                    itemprams = null;
                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                }
                            }
                            else//现场分析单登记
                            {
                                IDbDataParameter[] itemprams = {
                                                                 
                                    dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@ckflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.ckflag.GetType().ToString()), item.ckflag,4),
                                     dbFactory.MakeInParam("@AnalysisUserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisUserID.GetType().ToString()), item.AnalysisUserID, 50),
                                     dbFactory.MakeInParam("@AnalysisDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisDate.GetType().ToString()), item.AnalysisDate,8),
                                     dbFactory.MakeInParam("@Value",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Value.GetType().ToString()), item.Value, 50),
                                    
                                     dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 4)
                                    
                                                                             };
                                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_xc", itemprams);
                                itemprams = null;
                                if (iReturn < 0)
                                {
                                    throw new Exception("分析项目保存失败！");
                                }
                            }
                        }
                   //}
                   // //else if (checkid == 0)
                   // //{
                   // //    iReturn = 0;
                   // //    throw new Exception("样品编号重复！");
                   // //}
                   // else
                   // {
                   //     iReturn = -1;
                   //     throw new Exception("样品保存失败（1）！");
                   // }
                }
                    thelper.CommitTransaction(trans);
                    iReturn = 1;
                
                    
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
        /// <summary>
        ///修改样品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateSample(List<Entity.Sample> entityList)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
           
            bool flag = false;
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {
              
                foreach (Entity.Sample entity in entityList)
                {
                        if (entity.ID != 0)
                        {
                            IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                 dbFactory.MakeInParam("@AccessDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.AccessDate.GetType().ToString()), entity.AccessDate, 50) ,              
                 dbFactory.MakeInParam("@ReportID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.ReportID.GetType().ToString()),  entity.ReportID , 50) ,
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@SampleDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.SampleDate .GetType().ToString()),  entity.SampleDate , 50) ,
                 dbFactory.MakeInParam("@TypeID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TypeID.GetType().ToString()), entity.TypeID,4),  
                 dbFactory.MakeInParam("@SampleSource",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleSource.GetType().ToString()), entity.SampleSource,50),  
                 dbFactory.MakeInParam("@SampleAddress",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleAddress.GetType().ToString()), entity.SampleAddress, 50), 
                 dbFactory.MakeInParam("@SampleProperty",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleProperty.GetType().ToString()), entity.SampleProperty, 50), 
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.CreateDate.GetType().ToString()), entity.CreateDate, 50),  
                 dbFactory.MakeInParam("@xcflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.xcflag.GetType().ToString()), entity.xcflag, 50),  
                 dbFactory.MakeInParam("@ypstatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ypstatus.GetType().ToString()), entity.ypstatus, 4),                               
                 dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.StatusID.GetType().ToString()), entity.StatusID, 4),  
                 dbFactory.MakeInParam("@datastatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.datastatus.GetType().ToString()), entity.datastatus,4),  
                dbFactory.MakeInParam("@TestMan",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TestMan.GetType().ToString()), entity.TestMan,0),  
  dbFactory.MakeInParam("@bzid",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bzid.GetType().ToString()), entity.bzid,50),  
           dbFactory.MakeInParam("@num",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.num.GetType().ToString()), entity.num,4),
                  dbFactory.MakeInParam("@starttime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.starttime.GetType().ToString()), entity.starttime,50),  
               dbFactory.MakeInParam("@endtime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.endtime.GetType().ToString()), entity.endtime,50)
                 
               };

                            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Sample_Update]", prams);

                        }
                        else
                        {
                            IDbDataParameter[] pramsadd = {
                              dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                 dbFactory.MakeInParam("@AccessDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.AccessDate.GetType().ToString()), entity.AccessDate, 50) ,              
                 dbFactory.MakeInParam("@ReportID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.ReportID.GetType().ToString()),  entity.ReportID , 50) ,
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@SampleDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.SampleDate .GetType().ToString()),  entity.SampleDate , 50) ,
                 dbFactory.MakeInParam("@TypeID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TypeID.GetType().ToString()), entity.TypeID,4),  
                 dbFactory.MakeInParam("@SampleSource",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleSource.GetType().ToString()), entity.SampleSource,50),  
                 dbFactory.MakeInParam("@SampleAddress",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleAddress.GetType().ToString()), entity.SampleAddress, 50), 
                 dbFactory.MakeInParam("@SampleProperty",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleProperty.GetType().ToString()), entity.SampleProperty, 50), 
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.CreateDate.GetType().ToString()), entity.CreateDate, 50),  
                 dbFactory.MakeInParam("@xcflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.xcflag.GetType().ToString()), entity.xcflag, 50),  
                 dbFactory.MakeInParam("@ypstatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ypstatus.GetType().ToString()), entity.ypstatus, 4),                               
                 dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.StatusID.GetType().ToString()), entity.StatusID, 4),  
                 dbFactory.MakeInParam("@datastatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.datastatus.GetType().ToString()), entity.datastatus,4),  
                dbFactory.MakeInParam("@TestMan",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TestMan.GetType().ToString()), entity.TestMan,0),
                          dbFactory.MakeInParam("@bzid",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bzid.GetType().ToString()), entity.bzid,50) , 
                    dbFactory.MakeInParam("@num",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.num.GetType().ToString()), entity.num,4),
                       dbFactory.MakeInParam("@starttime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.starttime.GetType().ToString()), entity.starttime,50),  
               dbFactory.MakeInParam("@endtime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.endtime.GetType().ToString()), entity.endtime,50),  
              
                    dbFactory.MakeOutReturnParam()                             };

                            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Sample_Add]", pramsadd);
                            entity.ID = int.Parse(pramsadd[20].Value.ToString());
                            if (iReturn == 0)
                            {
                                return 0;
                            }
                        }


                        if (iReturn < 0)
                        {
                            throw new Exception("样品保存失败！");
                        }
                        int recordid = entity.ID;

                        if (recordid > 0)
                        {
                            entity.ID = recordid;
                        }
                        if (entity.ItemValueList.Length > 0)
                        {
                            entity.ItemValueList = entity.ItemValueList.Substring(0, entity.ItemValueList.Length - 1);
                        }
                        if (entity.ItemValueList.Length > 0)
                        {
                            IDbDataParameter[] prams = {
                                                      };
                            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_MonitorItemDetail set delflag=1 where SampleID='" + entity.ID + "' and MonitorItem not in (" + entity.ItemValueList + ")", prams);

                        }
                        foreach (Entity.SampleItem item in entity.SampleItemList)
                        {
                            if (item.ckflag == 0)//非现场
                            {
                                if (item.zpto != "")//有指派 
                                {
                                    IDbDataParameter[] itempramszp = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 0),
                                     dbFactory.MakeInParam("@zpcreateuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpcreateuser.GetType().ToString()), item.zpcreateuser, 0),
                                     dbFactory.MakeInParam("@zpdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpdate.GetType().ToString()), item.zpdate, 0),
                                     dbFactory.MakeInParam("@zpto",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpto.GetType().ToString()), item.zpto, 0),
                                dbFactory.MakeInParam("@flag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 0),
                                     dbFactory.MakeOutReturnParam()
                                                                 };
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_zp", itempramszp);

                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                    if (iReturn > 0)
                                    {
                                        item.ID = int.Parse(itempramszp[9].Value.ToString());
                                        //保存指派记录表
                                        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into t_ZPInfo(SampleID,ToUser,zpdate,zpcreateuser,MonitorDetailID,CreateDate,CreateUser)values('" + entity.ID + "','" + item.zpto + "','" + item.CreateDate + "','" + item.UserID + "','" + item.ID + "',getdate(),'" + item.UserID + "')", itempramszp);
                                        itempramszp = null;
                                        if (iReturn < 0)
                                        {
                                            throw new Exception("分析项目保存失败！");
                                        }
                                    }
                                }
                                else//无指派
                                {
                                    IDbDataParameter[] itemprams = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 4)
                                                               };
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add", itemprams);
                                    itemprams = null;
                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                }
                            }
                            else//现场分析单登记
                            {
                                IDbDataParameter[] itemprams = {
                                    dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@ckflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.ckflag.GetType().ToString()), item.ckflag,4),
                                     dbFactory.MakeInParam("@AnalysisUserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisUserID.GetType().ToString()), item.AnalysisUserID, 50),
                                     dbFactory.MakeInParam("@AnalysisDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisDate.GetType().ToString()), item.AnalysisDate,8),
                                     dbFactory.MakeInParam("@Value",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Value.GetType().ToString()), item.Value, 50),
                                    
                                     dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 4)
                                    
                                                                             };
                                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_xc", itemprams);
                                itemprams = null;
                                if (iReturn < 0)
                                {
                                    throw new Exception("分析项目保存失败！");
                                }
                            }
                        }
                    //}
                    //else if (checkid == 0)
                    //{
                    //    iReturn = 0;
                    //    throw new Exception("样品编号不能重复！");
                      
                    //}
                    //else
                    //{
                    //    iReturn = -1;
                    //    throw new Exception("样品保存失败（2）！");
                    //   }
                    

                }
               
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
        /// <summary>
        /// 添加样品信息（自动编号）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddSampleAutoNumber(List<Entity.Sample> entityList)
        {
           int iReturn = 0;
           int num = 0;
              DBOperatorBase db = new DataBase();
            
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
           // DrawSample drawop=new DrawSample();
            try
            {
             
                string bh = "";
                int newbh = 1;

             
                string type = getType(entityList[0].TypeID.ToString());
                    
                foreach (Entity.Sample entity in entityList)
                {
                   
                    if (type != "")
                    {
                        //获取样品编号TBD 

                        string Date = entity.AccessDate.ToString("yyMMdd");
                       
                        if (num == 0)
                        {
                            num++;
                                newbh = Newtkbh(type, Date, ref bh);
                                if (type == "降")
                                if(newbh ==1)
                                    bh = type + Date;
                                else
                                { return 0; }
                        }
                        else
                        {
                            if (type == "降")
                            { return 0; }
                            newbh += 1;
                            string newbhs = "00" + newbh.ToString();
                            bh = type + Date + newbhs.Substring(newbhs.Length - 3, 3);
                        }
                            entity.SampleID = bh;
                        entity.NO = int.Parse(entity.SampleID.Substring(entity.SampleID.Length - 3, 3));
                        IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                 dbFactory.MakeInParam("@AccessDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.AccessDate.GetType().ToString()), entity.AccessDate, 50) ,              
                 dbFactory.MakeInParam("@ReportID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.ReportID.GetType().ToString()),  entity.ReportID , 50) ,
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@SampleDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.SampleDate .GetType().ToString()),  entity.SampleDate , 50) ,
                 dbFactory.MakeInParam("@TypeID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TypeID.GetType().ToString()), entity.TypeID,4),  
                 dbFactory.MakeInParam("@SampleSource",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleSource.GetType().ToString()), entity.SampleSource,50),  
                 dbFactory.MakeInParam("@SampleAddress",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleAddress.GetType().ToString()), entity.SampleAddress, 50), 
                 dbFactory.MakeInParam("@SampleProperty",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleProperty.GetType().ToString()), entity.SampleProperty, 50), 
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.CreateDate.GetType().ToString()), entity.CreateDate, 50),  
                 dbFactory.MakeInParam("@xcflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.xcflag.GetType().ToString()), entity.xcflag, 50),  
                 dbFactory.MakeInParam("@ypstatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ypstatus.GetType().ToString()), entity.ypstatus, 4),                               
                 dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.StatusID.GetType().ToString()), entity.StatusID, 4),  
                 dbFactory.MakeInParam("@datastatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.datastatus.GetType().ToString()), entity.datastatus,4),  
                dbFactory.MakeInParam("@TestMan",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TestMan.GetType().ToString()), entity.TestMan,0),  
               dbFactory.MakeInParam("@bzid",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bzid.GetType().ToString()), entity.bzid,50),  
               dbFactory.MakeInParam("@num",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.num.GetType().ToString()), entity.num,4),
                     dbFactory.MakeInParam("@starttime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.starttime.GetType().ToString()), entity.starttime,50),  
               dbFactory.MakeInParam("@endtime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.endtime.GetType().ToString()), entity.endtime,50),  
              
               
                 dbFactory.MakeOutReturnParam()
               };
                       
                        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Sample_Add]", prams);
                    
                        int recordid = int.Parse(prams[20].Value.ToString());

                        if (recordid > 0)
                        {
                            entity.ID = recordid;
                        }  if (iReturn == 0)
                        {
                            return 0;
                        }
                        else if (iReturn < 0)
                            throw new Exception("样品保存失败！");
                        foreach (Entity.SampleItem item in entity.SampleItemList)
                        {
                            if (item.ckflag == 0)//非现场
                            {
                                if (item.zpto != "")//有指派 
                                {
                                    IDbDataParameter[] itempramszp = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 0),
                                     dbFactory.MakeInParam("@zpcreateuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpcreateuser.GetType().ToString()), item.zpcreateuser, 0),
                                     dbFactory.MakeInParam("@zpdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpdate.GetType().ToString()), item.zpdate, 0),
                                     dbFactory.MakeInParam("@zpto",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpto.GetType().ToString()), item.zpto, 0),
                                     dbFactory.MakeInParam("@flag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 0),
                                     dbFactory.MakeOutReturnParam()
                                                                 };
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_zp", itempramszp);

                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                    item.ID = int.Parse(itempramszp[9].Value.ToString());
                                    //保存指派记录表
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into t_ZPInfo(SampleID,ToUser,zpdate,zpcreateuser,MonitorDetailID,CreateDate,CreateUser)values('" + entity.SampleID + "','" + item.zpto + "','" + item.CreateDate + "','" + item.UserID + "','" + item.ID + "',getdate(),'" + item.UserID + "')", itempramszp);
                                    itempramszp = null;
                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                }
                                else//无指派
                                {
                                    IDbDataParameter[] itemprams = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 4)
                                                               };
                                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add", itemprams);
                                    itemprams = null;
                                    if (iReturn < 0)
                                    {
                                        throw new Exception("分析项目保存失败！");
                                    }
                                }
                            }
                            else//现场分析单登记
                            {
                                IDbDataParameter[] itemprams = {
                                                                 
                                    dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@ckflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.ckflag.GetType().ToString()), item.ckflag,4),
                                     dbFactory.MakeInParam("@AnalysisUserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisUserID.GetType().ToString()), item.AnalysisUserID, 50),
                                     dbFactory.MakeInParam("@AnalysisDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisDate.GetType().ToString()), item.AnalysisDate,8),
                                     dbFactory.MakeInParam("@Value",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Value.GetType().ToString()), item.Value, 50),
                                    
                                     dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 4)
                                    
                                                                             };
                                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_xc", itemprams);
                                itemprams = null;
                                if (iReturn < 0)
                                {
                                    throw new Exception("分析项目保存失败！");
                                }
                            }
                        }

                    }
                    else
                    {
                        throw new Exception("分析项目保存失败！");
                    }
                }
                if (num > 0)
                {
                    IDbDataParameter[] otherprams = {
                                                     };
                    string inSql = "update  t_最新编号 set LastBh='" + newbh + "' where BhType='" + type + "' and Date='" + entityList[0].AccessDate.ToString("yyMMdd") + "'";
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, inSql, otherprams);
                      if(iReturn<=0)
                      {
                          throw new Exception("分析项目保存失败！");
                      }
                }
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);

                iReturn =-1;
            }
            finally
            {
                if (db.Conn!=null)
                db.Conn.Close();
            }
            return iReturn;
        }
        /// <summary>
        /// 添加样品信息（自动编号）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateSampleAutoNumber(List<Entity.Sample> entityList)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            int num = 0;
            bool flag = false;
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {
                int newbh = 1;
                string bh = "";
                string type = getType(entityList[0].TypeID.ToString());
                foreach (Entity.Sample entity in entityList)
                {
                   

                     if (type != "")
                     {
                         string className = "";
                         switch (entityList[0].TypeID.ToString())
                         {
                             case "7":
                             case "8":
                             case "9": className = entity.SampleID.Substring(0, 2);
                                 break;
                             default: className = entity.SampleID.Substring(0, 1);
                                 break;
                         }

                         string[] strsqllist = new string[3];
                         int i = 0;
                         if (className != type)
                         {
                             string sampleid = entity.SampleID.ToString();
                            
                             string strSql = "DELETE FROM t_M_SampleInfor WHERE id= '" + entity.ID.ToString() + "'";
                             strsqllist.SetValue(strSql, i++);


                             strSql = "DELETE FROM t_MonitorItemDetail WHERE SampleID= '" + sampleid + "'";
                             strsqllist.SetValue(strSql, i++);
                              int NO = 1;
                             if (className == "降")
                                 NO = 1;
                             else
                                 NO = int.Parse(sampleid.Substring(sampleid.Length - 3, 3));
                             string checkstr = "select * from t_最新编号 where Date='" + entity.SampleDate.ToString("yyMMdd") + "' and BhType='" + className + "' and LastBh='" + NO + "' and LastBh>0";
                             DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
                             if (dscheck.Tables[0].Rows.Count != 0)
                             {
                                 strSql = "update t_最新编号  set LastBh=LastBh-1 where Date='" + entity.SampleDate.ToString("yyMMdd") + "' and BhType='" + className + "'";
                                 strsqllist.SetValue(strSql, i++);
                             }
                             entity.SampleID = "";
                         }
                         if (entity.SampleID == "")
                         {
                             flag = true;
                             //获取样品编号TBD 
                             string Date = entity.AccessDate.ToString("yyMMdd");

                             if (num == 0)
                             {
                                 num++;
                                 newbh = Newtkbh(type, Date, ref bh);
                                 if (type == "降")
                                if( newbh == 1)
                                     bh = type + Date;
                                 else
                                 { return 0; }
                             }
                             else
                             {
                                 if (type == "降")
                                 { return 0; }
                                 newbh += 1;
                                 string newbhs = "00" + newbh.ToString();
                                 bh = type + Date + newbhs.Substring(newbhs.Length - 3, 3);
                             }
                             entity.SampleID = bh;
                             entity.NO = 0;
                         }
                         else
                             entity.NO = 1;
                         foreach(string str in strsqllist)
                         {
                             if (str !=null)
                             {
                                 IDbDataParameter[] tempprams = {
                                                    };
                                 iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, str, tempprams);
                             }

                         }
                       
                         IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                 dbFactory.MakeInParam("@AccessDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.AccessDate.GetType().ToString()), entity.AccessDate, 50) ,              
                 dbFactory.MakeInParam("@ReportID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.ReportID.GetType().ToString()),  entity.ReportID , 50) ,
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@SampleDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.SampleDate .GetType().ToString()),  entity.SampleDate , 50) ,
                 dbFactory.MakeInParam("@TypeID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TypeID.GetType().ToString()), entity.TypeID,4),  
                 dbFactory.MakeInParam("@SampleSource",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleSource.GetType().ToString()), entity.SampleSource,50),  
                 dbFactory.MakeInParam("@SampleAddress",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleAddress.GetType().ToString()), entity.SampleAddress, 50), 
                 dbFactory.MakeInParam("@SampleProperty",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleProperty.GetType().ToString()), entity.SampleProperty, 50), 
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.CreateDate.GetType().ToString()), entity.CreateDate, 50),  
                 dbFactory.MakeInParam("@xcflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.xcflag.GetType().ToString()), entity.xcflag, 50),  
                 dbFactory.MakeInParam("@ypstatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ypstatus.GetType().ToString()), entity.ypstatus, 4),                               
                 dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.StatusID.GetType().ToString()), entity.StatusID, 4),  
                 dbFactory.MakeInParam("@datastatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.datastatus.GetType().ToString()), entity.datastatus,4),  
                dbFactory.MakeInParam("@TestMan",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TestMan.GetType().ToString()), entity.TestMan,0),  
  dbFactory.MakeInParam("@bzid",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bzid.GetType().ToString()), entity.bzid,50),  
           dbFactory.MakeInParam("@num",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.num.GetType().ToString()), entity.num,4),
                  dbFactory.MakeInParam("@starttime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.starttime.GetType().ToString()), entity.starttime,50),  
               dbFactory.MakeInParam("@endtime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.endtime.GetType().ToString()), entity.endtime,50)
                 
               };
                         if (!flag)
                         {
                             iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Sample_Update]", prams);

                         }
                         else
                         {
                             IDbDataParameter[] pramsadd = {
                              dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                 dbFactory.MakeInParam("@AccessDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.AccessDate.GetType().ToString()), entity.AccessDate, 50) ,              
                 dbFactory.MakeInParam("@ReportID",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.ReportID.GetType().ToString()),  entity.ReportID , 50) ,
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@SampleDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.SampleDate .GetType().ToString()),  entity.SampleDate , 50) ,
                 dbFactory.MakeInParam("@TypeID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TypeID.GetType().ToString()), entity.TypeID,4),  
                 dbFactory.MakeInParam("@SampleSource",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleSource.GetType().ToString()), entity.SampleSource,50),  
                 dbFactory.MakeInParam("@SampleAddress",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleAddress.GetType().ToString()), entity.SampleAddress, 50), 
                 dbFactory.MakeInParam("@SampleProperty",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleProperty.GetType().ToString()), entity.SampleProperty, 50), 
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.CreateDate.GetType().ToString()), entity.CreateDate, 50),  
                 dbFactory.MakeInParam("@xcflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.xcflag.GetType().ToString()), entity.xcflag, 50),  
                 dbFactory.MakeInParam("@ypstatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ypstatus.GetType().ToString()), entity.ypstatus, 4),                               
                 dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.StatusID.GetType().ToString()), entity.StatusID, 4),  
                 dbFactory.MakeInParam("@datastatus",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.datastatus.GetType().ToString()), entity.datastatus,4),  
                dbFactory.MakeInParam("@TestMan",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.TestMan.GetType().ToString()), entity.TestMan,0),
                          dbFactory.MakeInParam("@bzid",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.bzid.GetType().ToString()), entity.bzid,50) , 
                    dbFactory.MakeInParam("@num",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.num.GetType().ToString()), entity.num,4),
                       dbFactory.MakeInParam("@starttime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.starttime.GetType().ToString()), entity.starttime,50),  
               dbFactory.MakeInParam("@endtime",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.endtime.GetType().ToString()), entity.endtime,50),  
              
                    dbFactory.MakeOutReturnParam()                             };
                            
                             iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Sample_Add]", pramsadd);
                              entity.ID = int.Parse(pramsadd[20].Value.ToString());
                              if (iReturn == 0)
                              {
                                  return 0;
                              } 
                         }
                        
                         if (iReturn < 0)
                         {
                             throw new Exception("样品保存失败！");
                         }
                         int recordid = entity.ID;
                        
                         if (recordid > 0)
                         {
                             entity.ID = recordid;
                         }
                         if (entity.ItemValueList.Length > 0)
                         {
                             entity.ItemValueList = entity.ItemValueList.Substring(0, entity.ItemValueList.Length - 1);
                         }
                         if (entity.ItemValueList.Length > 0)
                         {
                             iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_MonitorItemDetail set delflag=1 where SampleID='" + entity.SampleID + "' and MonitorItem not in (" + entity.ItemValueList + ")", prams);

                         }
                         foreach (Entity.SampleItem item in entity.SampleItemList)
                         {
                                 if (item.ckflag == 0)//非现场
                                 {
                                     if (item.zpto != "")//有指派 
                                     {
                                         IDbDataParameter[] itempramszp = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 0),
                                     dbFactory.MakeInParam("@zpcreateuser",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpcreateuser.GetType().ToString()), item.zpcreateuser, 0),
                                     dbFactory.MakeInParam("@zpdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpdate.GetType().ToString()), item.zpdate, 0),
                                     dbFactory.MakeInParam("@zpto",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.zpto.GetType().ToString()), item.zpto, 0),
                                dbFactory.MakeInParam("@flag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 0),
                                     dbFactory.MakeOutReturnParam()
                                                                 };
                                         iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_zp", itempramszp);

                                         if (iReturn < 0)
                                         {
                                             throw new Exception("分析项目保存失败！");
                                         }
                                         if (iReturn > 0)
                                         {
                                             item.ID = int.Parse(itempramszp[9].Value.ToString());
                                             //保存指派记录表
                                             iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into t_ZPInfo(SampleID,ToUser,zpdate,zpcreateuser,MonitorDetailID,CreateDate,CreateUser)values('" + entity.SampleID + "','" + item.zpto + "','" + item.CreateDate + "','" + item.UserID + "','" + item.ID + "',getdate(),'" + item.UserID + "')", itempramszp);
                                             itempramszp = null;
                                             if (iReturn < 0)
                                             {
                                                 throw new Exception("分析项目保存失败！");
                                             }
                                         }
                                     }
                                     else//无指派
                                     {
                                         IDbDataParameter[] itemprams = {
                                     dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Remark.GetType().ToString()), item.Remark, 4)
                                                               };
                                         iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add", itemprams);
                                         itemprams = null;
                                         if (iReturn < 0)
                                         {
                                             throw new Exception("分析项目保存失败！");
                                         }
                                     }
                                 }
                                 else//现场分析单登记
                                 {
                                     IDbDataParameter[] itemprams = {
                                    dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                                     dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                                     dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                                     dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                                     dbFactory.MakeInParam("@ckflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.ckflag.GetType().ToString()), item.ckflag,4),
                                     dbFactory.MakeInParam("@AnalysisUserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisUserID.GetType().ToString()), item.AnalysisUserID, 50),
                                     dbFactory.MakeInParam("@AnalysisDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisDate.GetType().ToString()), item.AnalysisDate,8),
                                     dbFactory.MakeInParam("@Value",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Value.GetType().ToString()), item.Value, 50),
                                    
                                     dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 4)
                                    
                                                                             };
                                     iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_xc", itemprams);
                                     itemprams = null;
                                     if (iReturn < 0)
                                     {
                                         throw new Exception("分析项目保存失败！");
                                     }
                                 }
                         }
                     }
                     else
                     {
                         throw new Exception("分析项目保存失败！");
                     }
                    
                }
                if(num>0)
                {
                    IDbDataParameter[] otherprams = {
                                                     };
                    string inSql = "update  t_最新编号 set LastBh='" + newbh + "' where BhType='" + type + "' and Date='" + entityList[0].AccessDate.ToString("yyMMdd") + "'";
                               iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, inSql, otherprams);
                               if (iReturn<=0)
                                {
                                    throw new Exception("分析项目保存失败！");
                                }
                }
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);

                iReturn = 0;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
        /// <summary>
        /// 获取样品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetSampleDetail(string sampleid)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
               
                    IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                    IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select t_MonitorItemDetail.ID, xcflag 现场分析,AIName 分析项目 ,MonitorItem ItemID, experimentvalue 分析值 ,method 分析方法,t_MonitorItemDetail.Remark,t_MonitorItemDetail.fxDanID,showid from t_MonitorItemDetail inner join t_M_ANItemInf on t_M_ANItemInf.id=MonitorItem  where SampleID='" + sampleid + "' and delflag=0", prams);
              
                dt.Load(idr);
                // idr = db.ExecuteReader(Config.constr, CommandType.Text, "select xcflag 现场分析,AIName 分析项目 ,MonitorItem ItemID from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem inner join t_DrawSampleDetail on t_DrawSampleDetail.DrawID=t_MonitorItemDetail.fxDanID and ItemID=MonitorItem where SampleID='" + sampleid + "' and delflag=0", prams);
                
               
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// 添加样品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity.Draw GetScence(string sampleid)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            Entity.Draw entity = null;
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           // IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = {
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select *  from t_DrawSample  where t_DrawSample.SampleID='" + sampleid + "' and [type]=1 ", prams);
                while (idr.Read())
                { entity= new Entity.Draw();
                    entity.ID = int.Parse(idr["id"].ToString());
                    entity.CreateDate = DateTime.Parse(idr["fxtime"].ToString());
                    entity.UserID = idr["fxman"].ToString();
                    //entity.jhman = idr["jhman"].ToString();
                    //entity.jhtime = DateTime.Parse(idr["jhtime"].ToString());
                    entity.status = int.Parse(idr["fxflag"].ToString());
                }
              
             
              


            }
            catch (Exception ex)
            {
                entity = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return entity;
        }
        //提交分析报告
        public int ExChangeSample(Entity.Draw entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            string upstr = "";
            try
            {
                foreach (Entity.SampleItem item in entity.SampleItemList)
                {
                    IDbDataParameter[] prams = { 
                dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.ID.GetType().ToString()),item.ID, 50) ,           
                dbFactory.MakeInParam("@ItemID",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.MonitorID.GetType().ToString()), item.MonitorID, 50) ,  
                  dbFactory.MakeInParam("@AnalysisUserID",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.AnalysisUserID.GetType().ToString()), item.AnalysisUserID, 50) ,  
               
                //  dbFactory.MakeInParam("@jhman",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.jhman.GetType().ToString()), item.jhman, 50) ,  
                //dbFactory.MakeInParam("@jhdate",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.jhdate.GetType().ToString()), item.jhdate, 50) ,  
                dbFactory.MakeInParam("@Value",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.Value.GetType().ToString()), item.Value, 50) ,  
                dbFactory.MakeInParam("@DrawID",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.lyID.GetType().ToString()), item.lyID, 50) ,  
              
                                          };
                    //upstr = " update t_MonitorItemDetail set fxuser=@AnalysisUserID,fxdate=getdate(),jhman=@jhman,jhdate=@jhdate,experimentvalue=@Value where id=@ID";
                    //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
                    upstr = " update t_DrawSampleDetail set updatedate=getdate(),updateuser=@AnalysisUserID,value=@Value where ItemID=@ItemID and DrawID=@DrawID";
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams);
                    prams = null;
                }
                IDbDataParameter[] prams0 = { };
                upstr = " update t_DrawSample set fxtime='" + entity.finishdate + "',fxman='" + entity.fxman + "',finishdate='" + entity.finishdate + "',jhman='" + entity.jhman + "',jhtime='" + entity.jhtime + "',fxflag=1 where  ID='"+entity.ID+"'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstr, prams0);
                prams0 = null;
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch
            { iReturn =0;
            thelper.RollTransaction(trans);
            }
           
           return iReturn;
        }
        /// <summary>
        /// 获取样品类型
        /// </summary>
        /// <param name="sampleid"></param>
        /// <returns></returns>
        public DataTable GetSampleType(string ID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (ID != "")
                    Constr = " and  ClassID='" + ID + "'";
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_M_AnalysisMainClassEx  where 1=1 "+Constr, prams);

                dt.Load(idr);
                // idr = db.ExecuteReader(Config.constr, CommandType.Text, "select xcflag 现场分析,AIName 分析项目 ,MonitorItem ItemID from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem inner join t_DrawSampleDetail on t_DrawSampleDetail.DrawID=t_MonitorItemDetail.fxDanID and ItemID=MonitorItem where SampleID='" + sampleid + "' and delflag=0", prams);


            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 获取监测目的
        /// </summary>
        /// <param name="sampleid"></param>
        /// <returns></returns>
        public DataTable GetPurpose(string ID,int classid)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (ID != "")
                    Constr = " and  ItemID='" + ID + "'";
                if (classid != -1)
                    Constr += " and  classid='" + classid + "'";
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_M_ItemInfo  where bzw=0 " + Constr, prams);

                dt.Load(idr);
                // idr = db.ExecuteReader(Config.constr, CommandType.Text, "select xcflag 现场分析,AIName 分析项目 ,MonitorItem ItemID from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem inner join t_DrawSampleDetail on t_DrawSampleDetail.DrawID=t_MonitorItemDetail.fxDanID and ItemID=MonitorItem where SampleID='" + sampleid + "' and delflag=0", prams);


            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 获取监测方式
        /// </summary>
        /// <param name="sampleid"></param>
        /// <returns></returns>
        public DataTable GetMode(string ID,string type,string noin)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (ID != "")
                    Constr = " and  code='" + ID + "'";
                if (noin != "")
                    Constr += " and  code not in('" + noin + "')";
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_dictinf  where  type='" + type + "' " + Constr, prams);

                dt.Load(idr);
                // idr = db.ExecuteReader(Config.constr, CommandType.Text, "select xcflag 现场分析,AIName 分析项目 ,MonitorItem ItemID from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem inner join t_DrawSampleDetail on t_DrawSampleDetail.DrawID=t_MonitorItemDetail.fxDanID and ItemID=MonitorItem where SampleID='" + sampleid + "' and delflag=0", prams);


            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <param name="sampleid"></param>
        /// <returns></returns>
        public DataTable GetArea(string areaid)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (areaid != "")
                    Constr = " and  id='" + areaid + "'";
               
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_M_ClientInfo  where id='" + areaid + "' " + Constr, prams);

                dt.Load(idr);
                // idr = db.ExecuteReader(Config.constr, CommandType.Text, "select xcflag 现场分析,AIName 分析项目 ,MonitorItem ItemID from t_MonitorItemDetail inner join t_M_AnalysisItemEx on t_M_AnalysisItemEx.id=MonitorItem inner join t_DrawSampleDetail on t_DrawSampleDetail.DrawID=t_MonitorItemDetail.fxDanID and ItemID=MonitorItem where SampleID='" + sampleid + "' and delflag=0", prams);


            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return dt;
        }
    }
}