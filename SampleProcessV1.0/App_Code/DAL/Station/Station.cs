using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccess;
using System.Configuration;


namespace DAl
{
/// <summary>
///Report 的摘要说明
/// </summary>
    public class Station
    {
        public Station()
        {
        }
        /// <summary>
        /// 添加企业信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddStation(Entity.Station entity)
        {
           int iReturn = 0;
              DBOperatorBase db = new DataBase();
            
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop=new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                                               };
                string instr = @"insert into t_委托单位(单位全称,单位法人代码,单位曾用名全称,单位详细地址,所属镇街道,邮政编码,法定代表人,tel1,mobile1,市府网1,环保分管人,tel2,mobile2,市府网2,环保负责人,tel3,mobile3,市府网3,传真号码,电子邮箱,行业类别,kzlevel) values('"
                            + entity.qymc + "','"
                            +entity.frdm+"','"
                              + entity.cname + "','"
                              + entity.dz + "','"
                              //+ entity.jgdm + "','"
                              + entity.sd + "','"
                              + entity.yzbm + "','"
                              + entity.frdb + "','"
                              + entity.tel1 + "','"
                              + entity.mobile1 + "','"
                              + entity.zfw1 + "','"
                               + entity.hbfg + "','"
                              + entity.tel2 + "','"
                              + entity.mobile2 + "','"
                              + entity.zfw2 + "','"
                              + entity.hbfz + "','"
                              + entity.tel3 + "','"
                              + entity.mobile3 + "','"
                              + entity.zfw3 + "','"
                            + entity.czhm + "','" + entity.email + "','" + entity.industry + "'," + entity.control + ")";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, instr, prams);
                    if(iReturn<=0)
                        throw new Exception("单位保存失败！");
                   
                    thelper.CommitTransaction(trans);
                    iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);

                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
        /// <summary>
        /// 编辑企业信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateStation(Entity.Station entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            DrawSample drawop = new DrawSample();
            try
            {

                IDbDataParameter[] prams = {
                        
                
               };
                string checkstr="select 1 from [dbo].[t_委托单位] where 单位全称='"+ entity.qymc + "' and ID!='"+entity.ID+"'";
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, checkstr, prams);
                int i = 0;
                while (idr.Read())
                {
                    i++;
                    break;
                }

                if (i > 0)
                    iReturn =0;
                else
                {
                    string upstrwt = "update t_委托单位 set 单位全称='" + entity.qymc + "',单位法人代码='"+entity.frdm+"',单位曾用名全称='"
                               + entity.cname + "',单位详细地址='"
                               + entity.dz + "',所属镇街道='"
                               + entity.sd + "',邮政编码='"
                               + entity.yzbm + "',法定代表人='"
                               + entity.frdb + "',tel1='"
                               + entity.tel1 + "',mobile1='"
                               + entity.mobile1 + "',市府网1='"
                               + entity.zfw1 + "',环保分管人='"
                                 + entity.hbfg + "',tel2='"

                               + entity.tel2 + "',mobile2='"
                               + entity.mobile2 + "',市府网2='"
                               + entity.zfw2 + "',环保负责人='"
                               + entity.hbfz + "',tel3='"
                               + entity.tel3 + "',mobile3='"
                               + entity.mobile3 + "',市府网3='"
                               + entity.zfw3 + "',传真号码='" + entity.czhm
                               + "',电子邮箱='" + entity.email + "',行业类别='"
                               + entity.industry + "',kzlevel="
                               + entity.control
                               
                                + " where id = '" + entity.ID+"'";
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, upstrwt, prams);
                    if (iReturn < 0)
                    {
                        throw new Exception("编辑保存失败！");
                    }
                    thelper.CommitTransaction(trans);
                    iReturn = 1;
                }
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
        /// 获取企业信息
        /// </summary>
        /// <param name="ID">企业编号</param>
        /// <returns></returns>
        public DataTable GetStationByID(string ID)
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
                    Constr = " and  ID='" + ID + "'";
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_企业信息  where 1=1 " + Constr, prams);

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
        /// 获取企业信息
        /// </summary>
        /// <param name="ID">企业编号</param>
        /// <returns></returns>
        public DataTable GetWTByName(string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (Name != "")
                    Constr = " and  单位全称='" + Name + "'";
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from View_wtdepart  where 1=1 " + Constr + "", prams);
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
        }/// <summary>
        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <param name="ID">企业编号</param>
        /// <returns></returns>
        public DataTable GetStationByName(string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (Name != "")
                    Constr = " and  单位全称='" + Name + "'";
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_委托单位  where 1=1 " + Constr + "", prams);
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
        /// 获取企业信息
        /// </summary>
        /// <param name="ID">企业编号</param>
        /// <returns></returns>
        public DataTable GetBzByName(string Name,string Typeid)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (Name != "")
                    Constr = " and  单位全称='" + Name + "'";
                IDbDataParameter[] prams = {
                // dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType(sampleid.GetType().ToString()), sampleid, 50)            
                                                };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select t_hyClassParam.id, t_标准字典.bz  from t_hyClassParam inner join t_标准字典 on t_标准字典.id=t_hyClassParam.bz inner join t_CompabyBZ on t_CompabyBZ.bzid=t_hyClassParam.id inner join t_委托单位 on t_委托单位.id=t_CompabyBZ.qyid  where t_CompabyBZ.wrwtype='" + Typeid + "'  and t_CompabyBZ.flag=0 " + Constr + "", prams);
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
        /// 获取企业信息
        /// </summary>
        /// <param name="ID">企业编号</param>
        /// <returns></returns>
        public DataTable GetJKByName(string Name, string Typeid)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string Constr = "";
                if (Name != "")
                    Constr = " and  单位全称='" + Name + "'";
                IDbDataParameter[] prams = {
                                           };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_委托单位  where type=1 " + Constr + "", prams);
                dt.Load(idr);
              

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