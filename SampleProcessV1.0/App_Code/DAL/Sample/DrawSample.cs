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
    ///DrawSample 的摘要说明
    /// </summary>
    public class DrawSample
    {
        public DrawSample()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public int AddDrawSample(Entity.Draw entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
               
                    IDbDataParameter[] prams = {
                                                     dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
               
                   dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@LyDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.LyDate.GetType().ToString()),  entity.LyDate , 50) ,
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@ItemList",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ItemList.GetType().ToString()), entity.ItemList, 50),                               
                 dbFactory.MakeInParam("@ItemValueList",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ItemValueList.GetType().ToString()), entity.ItemValueList, 50),                               
           dbFactory.MakeInParam("@fxflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.status.GetType().ToString()), entity.status, 50),                               

                 dbFactory.MakeOutReturnParam()
                                           };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Draw_Add]", prams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("样品领用保存失败！");
                    }
                    int recordid = int.Parse(prams[7].Value.ToString());
                    prams = null;
                    if (recordid > 0)
                    {
                        entity.ID = recordid;
                    }
                    IDbDataParameter[] itemprams = {
                                           };
                    foreach (Entity.SampleItem item in entity.SampleItemList)
                    {

                       
                            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set flag=1, lydate='" + entity.LyDate + "',fxDanID='" + entity.ID + "' where MonitorItem='" + item.MonitorID + "' and SampleID='" + item.SampleID + "'"), itemprams);

                            if (iReturn <= 0)
                            {
                                throw new Exception("领用样品分析项目更新失败！");
                            }
                            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("Insert into  t_DrawSampleDetail (DrawID,ItemID,method)values('" + entity.ID + "','" + item.MonitorID + "','" + item.Method + "')"), itemprams);
                            if (iReturn <= 0)
                            {
                                throw new Exception("领用样品分析项目更新失败！");
                            }
                        
                    }
                    iReturn=db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update t_M_SampleInfor set statusID=1 where SampleID='" + entity.SampleID + "'", itemprams);
                    if (iReturn <= 0)
                    {
                        throw new Exception("领用样品分析项目更新失败！");
                    }   
                itemprams = null;

               
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
        public int EditDrawSample(Entity.Draw entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = {
                                                     dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
               
                   dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@LyDate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.LyDate.GetType().ToString()),  entity.LyDate , 50) ,
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
                 dbFactory.MakeInParam("@ItemList",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ItemList.GetType().ToString()), entity.ItemList, 50),                               
                 dbFactory.MakeInParam("@ItemValueList",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ItemValueList.GetType().ToString()), entity.ItemValueList, 50),                               
           dbFactory.MakeOutReturnParam()
                                           };
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Draw_Edit]", prams);

                if (iReturn <= 0)
                {
                    throw new Exception("样品领用保存失败！");
                }
                int recordid = int.Parse(prams[6].Value.ToString());
               // prams = null;
                if (recordid > 0)
                {
                    entity.ID = recordid;
                }
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [t_DrawSampleDetail] where DrawID='" + entity.ID + "'", prams);
                if (iReturn < 0)
                {
                    throw new Exception("领用样品分析项目更新失败！");
                }
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set flag=0,fxDanID=0 where fxDanID='" + entity.ID + "'"), prams);
                if (iReturn < 0)
                {
                    throw new Exception("领用样品分析项目更新失败！");
                }
                foreach (Entity.SampleItem item in entity.SampleItemList)
                {
                    if (item.flag)
                    {
                        IDbDataParameter[] itemprams = {
                  dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
                   dbFactory.MakeInParam("@ItemID",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.MonitorID.GetType().ToString()), item.MonitorID, 50) ,              
                 dbFactory.MakeInParam("@Method",  DBTypeConverter.ConvertCsTypeToOriginDBType(item.Method.GetType().ToString()),  item.Method , 50) ,
        
                                           };
                       
                        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set flag=1, lydate='" + entity.LyDate + "',fxDanID='" + entity.ID + "' where MonitorItem='" + item.MonitorID + "' and SampleID='" + item.SampleID + "'"), itemprams);

                        if (iReturn < 0)
                        {
                            throw new Exception("领用样品分析项目更新失败！");
                        }
                      
                        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_DrawDetail_Add]", itemprams);
                        if (iReturn <= 0)
                        {
                            throw new Exception("领用样品分析项目更新失败！");
                        }
                        itemprams = null;
                    }
                   
                   
                }
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update t_M_SampleInfor set statusID=1 where SampleID='" + entity.SampleID + "'", itemprams);
                //if (iReturn <= 0)
                //{
                //    throw new Exception("领用样品分析项目更新失败！");
                //}
                


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
        public int ReturnSample(Entity.Draw entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {

                IDbDataParameter[] prams = {
                                                     dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
               
                   dbFactory.MakeInParam("@Remark",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.Remark.GetType().ToString()), entity.Remark, 50) ,              
                 dbFactory.MakeInParam("@returndate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.returndate.GetType().ToString()),  entity.returndate , 50)
                           
                                           };
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Draw_Update]", prams);

                if (iReturn <= 0)
                {
                    throw new Exception("样品领用保存失败！");
                }
                IDbDataParameter[] itemprams = {
                                           };
               
                //检查是否存在未做的
                db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update t_M_SampleInfor set ypstatus=0 where SampleID='" + entity.SampleID + "'", itemprams);
                itemprams = null;
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
        /// 现场分析数据保存：样品状态为可领用状态；分析状态为分析完成；数据状态为完成
        /// </summary>
        /// <param name="db"></param>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddDrawSampleTrans(DBOperatorBase db,IDbTransaction trans,Entity.Draw entity)
        {
            int iReturn = 0;
            //DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            //SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
               dbFactory.MakeInParam("@ID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4) ,              
               dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
               dbFactory.MakeInParam("@fxtime",  DBTypeConverter.ConvertCsTypeToOriginDBType(  entity.CreateDate.GetType().ToString()),  entity.CreateDate , 50) ,
               dbFactory.MakeInParam("@LyDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.LyDate.GetType().ToString()), entity.LyDate, 50),                               
               dbFactory.MakeInParam("@type",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.type.GetType().ToString()), entity.type, 4),                               
               dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
               dbFactory.MakeInParam("@ItemList",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ItemList.GetType().ToString()), entity.ItemList, 50),                               
               dbFactory.MakeInParam("@ItemValueList",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ItemValueList.GetType().ToString()), entity.ItemValueList, 50),                               
               dbFactory.MakeInParam("@fxman",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.UserID.GetType().ToString()), entity.UserID, 50),                               
              dbFactory.MakeInParam("@finishdate",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.finishdate.GetType().ToString()), entity.finishdate,4),  
               dbFactory.MakeOutReturnParam()
                                           };
                if(entity.type==1)//现场分析
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Draw_Add_xc]", prams);
                else
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_Draw_Add_fxc]", prams);

                if (iReturn <= 0)
                {
           
                    throw new Exception("样品领用保存失败！");
                }
                int recordid = int.Parse(prams[10].Value.ToString());
              
                if (recordid > 0)
                {
                    entity.ID = recordid;
                }
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "delete from t_DrawSampleDetail where DrawID='" + entity.ID + "'", prams);
                prams = null;
                IDbDataParameter[] itemprams = {
                                           };
                foreach (Entity.SampleItem item in entity.SampleItemList)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("Insert into  t_DrawSampleDetail (DrawID,ItemID,method,value,fxDate,fxuser)values('" + entity.ID + "','" + item.MonitorID + "','" + item.Method + "','"+item.Value+"','"+item.CreateDate+"','"+entity.UserID+"')"), itemprams);
                    if (iReturn <= 0)
                    {
                        throw new Exception("领用样品分析项目更新失败！");
                    }
                    else
                    {
                        IDbDataParameter[] itemprams2 = {
                 dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.SampleID.GetType().ToString()), entity.SampleID, 50) ,              
                 dbFactory.MakeInParam("@MonitorID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.MonitorID.GetType().ToString()), item.MonitorID,4),  
                 dbFactory.MakeInParam("@CreateDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.CreateDate.GetType().ToString()), item.CreateDate,50),  
                 dbFactory.MakeInParam("@UserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.UserID.GetType().ToString()), item.UserID, 50),
                 dbFactory.MakeInParam("@ckflag",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.ckflag.GetType().ToString()), item.ckflag,4),
                 dbFactory.MakeInParam("@AnalysisUserID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisUserID.GetType().ToString()), item.AnalysisUserID, 50),
                 dbFactory.MakeInParam("@AnalysisDate",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.AnalysisDate.GetType().ToString()), item.AnalysisDate,8),
                  dbFactory.MakeInParam("@Value",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Value.GetType().ToString()), item.Value, 50),
                 dbFactory.MakeInParam("@method",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.Method.GetType().ToString()), item.Method, 50),
                 dbFactory.MakeInParam("@statusID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.statusID.GetType().ToString()), item.statusID, 4),
                   dbFactory.MakeInParam("@fxDanID",  DBTypeConverter.ConvertCsTypeToOriginDBType( entity.ID.GetType().ToString()), entity.ID, 4)
                                                         };
                        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add_xc", itemprams2);
                        itemprams = null;
                        if (iReturn < 0)
                        {
                            throw new Exception("分析项目保存失败！");
                        }
                    }
                   

                }
                itemprams = null;
                iReturn = 1;
            }
            catch (Exception ex)
            {
                iReturn = 0;
            }
            finally
            {
            }
            return iReturn;
        }

        public int AddDraw(List<Entity.SampleItem> DrawList)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //保存领用日志记录
                foreach (Entity.SampleItem item in DrawList)
                {
                    IDbDataParameter[] prams = {
                      dbFactory.MakeInParam("@SampleID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.SampleID.GetType().ToString()), item.SampleID, 50) ,              
                     dbFactory.MakeInParam("@Lydate",  DBTypeConverter.ConvertCsTypeToOriginDBType(  item.Lydate.GetType().ToString()),  item.Lydate ,0) ,
                     dbFactory.MakeInParam("@LyUser",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.LyUser.GetType().ToString()), item.LyUser, 50),                               
                     dbFactory.MakeInParam("@MonitorRecordID",  DBTypeConverter.ConvertCsTypeToOriginDBType( item.ID.GetType().ToString()), item.ID,4),                                       
                     dbFactory.MakeOutReturnParam()
                                               };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "[proc_DrawSampleItem_Add]", prams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("样品领用保存失败！");
                    }
                    int recordid = int.Parse(prams[4].Value.ToString());
                    prams = null;
                    if (recordid > 0)
                    {
                        item.lyID = recordid.ToString();
                    }
                    IDbDataParameter[] itemprams = {
                                           };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set flag=2, lydate='" + item.Lydate + "',fxDanID='" + item.lyID + "',LyUser='"+item.LyUser+"' where id='" + item.ID + "'"), itemprams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("领用样品分析项目更新失败！");
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
        //分析数据保存
        public int DataSave(List<Entity.SampleItem> DrawList)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //保存
                foreach (Entity.SampleItem item in DrawList)
                {
                    IDbDataParameter[] prams = {
                                                               };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set experimentvalue='"+item.Value+"', fxdate='" + item.AnalysisDate + "',fxuser='" + item.AnalysisUserID + "',fxRemark='" + item.Remark + "',Method='"+item.Method+"' where id='" + item.ID + "'"), prams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("分析数据保存失败！");
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
        //分析数据提交
        public int DataSubmit(List<Entity.SampleItem> DrawList)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //保存
                foreach (Entity.SampleItem item in DrawList)
                {
                    IDbDataParameter[] prams = {
                                                               };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set experimentvalue='" + item.Value + "', fxdate='" + item.AnalysisDate + "',fxuser='" + item.AnalysisUserID + "',fxRemark='" + item.Remark + "',flag=3,Method='" + item.Method + "' where id='" + item.ID + "'"), prams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("分析数据提交保存失败！");
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
        //指派分析项目
        public int ZPSampleItem(List<Entity.SampleItem> entitylist)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            string upstr = "";
            try
            {
                //保存
                foreach (Entity.SampleItem item in entitylist)
                {
                    IDbDataParameter[] prams = {
                                                               };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set flag='" + item.statusID + "', zpdate='" + item.zpdate + "',zpcreateuser='" + item.zpcreateuser + "',zpto='" + item.zpto + "'  where id='" + item.ID + "'"), prams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("分析数据保存失败！");
                    }


                    //保存指派记录表
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into t_ZPInfo(SampleID,ToUser,zpdate,zpcreateuser,MonitorDetailID,CreateDate,CreateUser)values('" + item.SampleID + "','" + item.zpto + "','" + item.zpdate + "','" + item.zpcreateuser + "','" + item.ID + "',getdate(),'" + item.UserID + "')", prams);

                    if (iReturn < 0)
                    {
                        throw new Exception("分析项目保存失败！");
                    }
                }
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch
            {
                iReturn = 0;
                thelper.RollTransaction(trans);
            }

            return iReturn;
        }
        //分析项目退回分析
        public int SampleItemBack(List<Entity.SampleItem> entitylist)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            string upstr = "";
            try
            {
                //保存
                foreach (Entity.SampleItem item in entitylist)
                {
                    IDbDataParameter[] prams = {
                                                               };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set flag='" + item.statusID + "'  where id='" + item.ID + "'"), prams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("分析数据保存失败！");
                    }


                    //保存指派记录表
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into t_MonitorBackInfo(SampleID,Remark,zpdate,zpcreateuser,MonitorDetailID,CreateDate,CreateUser)values('" + item.SampleID + "','" + item.Remark + "','" + item.zpdate + "','" + item.zpcreateuser + "','" + item.ID + "',getdate(),'" + item.UserID + "')", prams);

                    if (iReturn < 0)
                    {
                        throw new Exception("分析项目保存失败！");
                    }
                }
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch
            {
                iReturn = 0;
                thelper.RollTransaction(trans);
            }

            return iReturn;
        }

        //监测项申领退回
        public int DataBack(List<Entity.SampleItem> DrawList)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //保存
                foreach (Entity.SampleItem item in DrawList)
                {
                    IDbDataParameter[] prams = {
                                                               };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_MonitorItemDetail set flag=0,zpdate=null,zpto=null,zpcreateuser=null,fxRemark='fxRemark+" + item .Remark+ "' where id='" + item.ID + "'"), prams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("监测项申领退回保存失败！");
                    }

                    IDbDataParameter[] itemprams = {
                                           };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, String.Format("update t_DrawSampleItem set flag=1, updateuser='" + item.AnalysisUserID + "',updatedate=getdate(),Remark='" + item.Remark + "' where id='" + item.lyID + "'"), itemprams);

                    if (iReturn <= 0)
                    {
                        throw new Exception("监测项申领退回更新失败！");
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
    }
}