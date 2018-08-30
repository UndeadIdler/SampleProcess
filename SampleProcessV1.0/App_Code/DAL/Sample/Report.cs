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
/// Report 的摘要说明
/// </summary>
public class Report
{
	public Report()
	{
    }
		//
		// TODO: 在此处添加构造函数逻辑
		//
         /// <summary>
        /// 添加样品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
   
        public int AddYS(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            //IDbTransaction trans = thelper.StartTransaction();
            
            
            try
            {
                                       IDbDataParameter[] prams = {
                 };
                
                 iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "insert into t_Y_FlowInfo (rwclass,CreateDate,UserID,ReportAccessDate,ItemType,ReportName,urgent,wtdepart,lxman,lxtel,lxemail,address,jcmethod,Projectname,StatusID) values('" + entity.classID + "','" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName + "','" + entity.StatusID + "')", prams);
                   
              
               
            }
            catch (Exception ex)
            {
              
                iReturn =-1;
            }
            finally
            {
                if (db.Conn!=null)
                db.Conn.Close();
            }
            return iReturn;
        }
        public int UpateYS(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            
            
            try
            {

                IDbDataParameter[] prams = {
                
                                               };

                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set backflag=0,wether=0,  rwclass='" + entity.classID + "', updatedate=getdate(),updateuser='" + entity.CreateUser + "',ReportAccessDate='" + entity.WTDate + "',ItemType='" + entity.TypeID + "',ReportName='" + entity.WTNO + "',urgent='" + entity.Remark + "',wtdepart='" + entity.WTMan + "',lxman='" + entity.lxMan + "',lxtel='" + entity.lxtel + "',lxemail='" + entity.lxEmail + "',address='" + entity.address + "',jcmethod='" + entity.Mode + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "' where id='" + entity.ID + "'", prams);
                //. + " values('" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.level + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName + "','" + entity.StatusID + "','" + entity.chargeman + "')", prams);


            }
            catch (Exception ex)
            {
                

                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
    //项目初审
        public int UpateYScs(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            
            try
            {

                IDbDataParameter[] prams = {
                
                                               };
                if (entity.csflag == 1)// 初审不通过
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set backflag='" + entity.backflag + "',  hanwether='" + entity.hanwether + "',csdate=getdate(), updatedate=getdate(),updateuser='" + entity.CreateUser + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',kschargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "',varman1='" + entity.CreateUser + "',vardate1='" + entity.CreateDate + "',varremark1='" + entity.Remark + "',wether='" + entity.csflag + "' where id='" + entity.ID + "'", prams);

                else if (entity.backflag == 1)//一般的退回
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set backflag='" + entity.backflag + "', hanwether='" + entity.hanwether + "',csdate=getdate(), updatedate=getdate(),updateuser='" + entity.CreateUser + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',kschargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "',varman1='" + entity.CreateUser + "',vardate1='" + entity.CreateDate + "',varremark1='" + entity.Remark + "' where id='" + entity.ID + "'", prams);
               
                else if(entity.StatusID ==1.5) 
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set hanwether='" + entity.hanwether + "' ,  updatedate=getdate(),csdate=getdate(),updateuser='" + entity.CreateUser + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',kschargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "',varman1='" + entity.CreateUser + "',vardate1='" + entity.CreateDate + "',varremark1='" + entity.Remark + "',wether='" + entity.csflag + "' where id='" + entity.ID + "'", prams);
                else
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set hanwether='" + entity.hanwether + "' ,  updatedate=getdate(),csdate=getdate(),updateuser='" + entity.CreateUser + "',Projectname='" + entity.ProjectName + "',kschargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "',varman1='" + entity.CreateUser + "',vardate1='" + entity.CreateDate + "',varremark1='" + entity.Remark + "',wether='" + entity.csflag + "' where id='" + entity.ID + "'", prams);

                if (iReturn == 1)
                {
                    thelper.CommitTransaction(trans);
                    if (entity.backflag == 1)
                    {
                        ItemObj backobj = new ItemObj(entity.CreateUser, entity.ID.ToString(), "初审退回综合室受理", entity.CreateDate.ToString(), entity.Remark);
                        backobj.save();
                    }

                    else if (entity.StatusID == 0)
                    {
                        ItemObj backobj = new ItemObj(entity.CreateUser, entity.ID.ToString(), "初审不通过退回综合室受理", entity.CreateDate.ToString(), entity.Remark);
                        backobj.save();
                    }
                }
                else
                    thelper.RollTransaction(trans);
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
       //项目指派
    public int UpateYSzp(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();

            try
            {

                IDbDataParameter[] prams = {
                
                                               };
                string upstr = "";
                if (entity.StatusID == 2 || entity.StatusID == 3.5 || entity.StatusID == 1)
                    upstr = "StatusID='" + entity.StatusID + "',";
               
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set backflag='"+entity.backflag+"', hanwether='" + entity.hanwether + "' ,zpremark='"+entity.Remark+"',  updatedate=getdate(),updateuser='" + entity.CreateUser + "',"+upstr+" chargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "',zpdate=getdate() where id='" + entity.ID + "'", prams);

                if (iReturn == 1)
                {
                    //提交
                    if (entity.StatusID == 2 || entity.StatusID == 3.5)
                    {
                        IDataReader sr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_M_ReporInfo where syID='" + entity.ID + "'", prams);
                        DataTable dt = new DataTable();
                        dt.Load(sr);
                        if (dt.Rows.Count > 0)
                        {
                            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_M_ReporInfo set rwclass='" + entity.classID + "', CreateDate='" + entity.CreateDate + "',UserID='" + entity.CreateUser + "',ReportAccessDate='" + entity.WTDate + "',ItemType='" + entity.TypeID + "',ReportName='" + entity.WTNO + "',Ulevel='" + entity.level + "',urgent='" + entity.Remark + "',wtdepart='" + entity.WTMan + "',lxman='" + entity.lxMan + "',lxtel='" + entity.lxtel + "',lxemail='" + entity.lxEmail + "',address='" + entity.address + "',jcmethod='" + entity.Mode + "',Projectname='" + entity.ProjectName + "',Green='" + entity.Green + "',chargeman='" + entity.chargeman + "' where syID='" + entity.ID + "'", prams);
                        }
                        else
                        {
                            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "insert into t_M_ReporInfo (rwclass,CreateDate,UserID,ReportAccessDate,ItemType,ReportName,Ulevel,urgent,wtdepart,lxman,lxtel,lxemail,address,jcmethod,Projectname,StatusID,syID,Green,chargeman)values('" + entity.classID + "','" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.level + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName2 + "','1','" + entity.ID + "','" + entity.Green + "','" + entity.chargeman + "')", prams);
                        }
                    }
                    if (iReturn == 1)
                    {
                        thelper.CommitTransaction(trans);
                        if (entity.backflag == 1)
                        {
                            ItemObj backobj = new ItemObj(entity.CreateUser, entity.ID.ToString(), "指派退回初审", entity.CreateDate.ToString(), entity.Remark);
                            backobj.save();
                        }
                    }
                    else
                        thelper.RollTransaction(trans);
                }
                else
                    thelper.RollTransaction(trans);

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
        public int UpateYStk(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           
            
            try
            {

                IDbDataParameter[] prams = {
                
                                               };

                if (entity.StatusID != 2)
                {
                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set backflag='" + entity.backflag + "', updatedate=getdate(),updateuser='" + entity.CreateUser + "',StatusID='" + entity.StatusID + "',varman2='" + entity.CreateUser + "',vardate2='" + entity.CreateDate + "',varremark2='" + entity.Remark + "',tkwether='" + entity.tkflag + "' where id='" + entity.ID + "'", prams);
                }
                else
                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set  updatedate=getdate(),updateuser='" + entity.CreateUser + "',StatusID='" + entity.StatusID + "',varman2='" + entity.CreateUser + "',vardate2='" + entity.CreateDate + "',varremark2='" + entity.Remark + "',tkwether='" + entity.tkflag + "' where id='" + entity.ID + "'", prams);                 
            }
            catch (Exception ex)
            {
                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }
        public int UpateYShan(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string bh = "";
               
                IDbDataParameter[] prams = {
                
                                       };
                string constr = "";
                if (entity.backflag == 1)
                    constr = " ,backflag='" + entity.backflag + "',StatusID='" + entity.StatusID + "' ,varremark3='" + entity.Remark + "'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set huser='" + entity.CreateUser + "',hcreatedate=getdate(),hContent='" + entity.Content + "',updatedate=getdate(),updateuser='" + entity.CreateUser + "',varman3='" + entity.CreateUser + "',vardate3='" + entity.CreateDate + "',hfwdw='" + entity.hfwdw + "',hcs='" + entity.hcs + "'" + constr + " where id='" + entity.ID + "'", prams);
                if (iReturn == 1)
                { thelper.CommitTransaction(trans); }
                else
                    thelper.RollTransaction(trans);
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
        private int Newtkbh(string type, string Date, ref string bh)
        {
            //生成编号
            int newbh = 1;

            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           
            string sql = "select LastBh from t_报告编号 where BhType = '" + type + "' and  Date ='" + Date + "'";

            DataTable dt = new DataTable();
            IDbDataParameter[] prams = {
                                                };
            IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, sql, prams);
            dt.Load(idr);
            if (dt.Rows.Count > 0)
            {
                string num = dt.Rows[0][0].ToString();

                newbh = int.Parse(num) + 1;
                //string newbhs = "00" + newbh.ToString();
                bh = type + "〔" + Date + "〕" + newbh.ToString() + "号";

            }
            else
            {
               
                newbh = 1;
                bh = type + "〔" + Date + "〕" + newbh.ToString()+"号";
                string inSql = "insert into t_报告编号(LastBh,BhType,Date) values('" + newbh + "','" + type + "','" + Date + "')";
                int iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, inSql, prams);
            }
            return newbh;
        }
        public int UpateYShanFinish(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                string bh="";
                if (entity.ReportNO == "")
                {
                    string type = "桐环监测";
                    int newbh = Newtkbh(type, DateTime.Now.ToString("yyyy"), ref bh);
                    entity.ReportNO = bh;
                    IDbDataParameter[] otherprams = {
                                                     };
                    string inSql = "update  t_报告编号 set LastBh='" + newbh + "' where BhType='" + type + "' and Date='" + DateTime.Now.ToString("yyyy") + "'";
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, inSql, otherprams);
                    if (iReturn <= 0)
                    {
                        throw new Exception("分析项目保存失败！");
                    }
                }
                   
                IDbDataParameter[] prams = {
                
                                               };
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set StatusID='" + entity.StatusID + "', hanNO='" + entity.ReportNO + "', huser='" + entity.CreateUser + "',hcreatedate=getdate(),hContent='" + entity.Content + "',updatedate=getdate(),updateuser='" + entity.CreateUser + "',varman3='" + entity.CreateUser + "',vardate3='" + entity.CreateDate + "',hfwdw='" + entity.hfwdw + "',hcs='" + entity.hcs + "' where id='" + entity.ID + "'", prams);
                if (iReturn == 1)
                { thelper.CommitTransaction(trans); }
                else
                    thelper.RollTransaction(trans);

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
    //报告编制
        public int UpateYSfa(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();
           
            try
            {

                IDbDataParameter[] prams = {
                
                                               };


                //iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set  rwclass='" + entity.classID + "', updatedate=getdate(),updateuser='" + entity.CreateUser + "',ReportAccessDate='" + entity.WTDate + "',ItemType='" + entity.TypeID + "',ReportName='" + entity.WTNO + "',urgent='" + entity.Remark + "',wtdepart='" + entity.WTMan + "',lxman='" + entity.lxMan + "',lxtel='" + entity.lxtel + "',lxemail='" + entity.lxEmail + "',address='" + entity.address + "',jcmethod='" + entity.Mode + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',,chargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "' where id='" + entity.ID + "'", prams);

                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set backflag='" + entity.backflag + "',StatusID='" + entity.StatusID + "' ,updatedate=getdate(),updateuser='" + entity.CreateUser + "',varman3='" + entity.CreateUser + "',vardate3='" + entity.CreateDate + "' where id='" + entity.ID + "'", prams);
                //. + " values('" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.level + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName + "','" + entity.StatusID + "','" + entity.chargeman + "')", prams);

                if (iReturn == 1)
                { thelper.CommitTransaction(trans); }
                else
                    thelper.RollTransaction(trans);

               
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
       //验收方案编写保存相关
        public int UpateYSfanAn(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            IDbTransaction trans = thelper.StartTransaction();

            try
            {

                IDbDataParameter[] prams = {
                
                                               };


                //iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set  rwclass='" + entity.classID + "', updatedate=getdate(),updateuser='" + entity.CreateUser + "',ReportAccessDate='" + entity.WTDate + "',ItemType='" + entity.TypeID + "',ReportName='" + entity.WTNO + "',urgent='" + entity.Remark + "',wtdepart='" + entity.WTMan + "',lxman='" + entity.lxMan + "',lxtel='" + entity.lxtel + "',lxemail='" + entity.lxEmail + "',address='" + entity.address + "',jcmethod='" + entity.Mode + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',,chargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "' where id='" + entity.ID + "'", prams);
                string backstr = "";
                if (entity.StatusID == 2)
                    backstr = ",fanbackremark='" + entity.Content + "'";
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "update t_Y_FlowInfo set backflag='" + entity.backflag + "', fanbackflag='" + entity.backflag + "',StatusID='" + entity.StatusID + "' ,updatedate=getdate(),updateuser='" + entity.CreateUser + "',varman3='" + entity.CreateUser + "',vardate3='" + entity.CreateDate + "',varremark3='" + entity.Content + "'" + backstr + " where id='" + entity.ID + "'", prams);
                //. + " values('" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.level + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName + "','" + entity.StatusID + "','" + entity.chargeman + "')", prams);

                if (iReturn == 1)
                { thelper.CommitTransaction(trans);
                   
                     if (entity.StatusID == 2)
                    {
                        ItemObj backobj = new ItemObj(entity.CreateUser, entity.ID.ToString(), "验收方案编制退回踏勘", entity.CreateDate.ToString(), entity.Content);
                        backobj.save();
                    }
                }
                else
                    thelper.RollTransaction(trans);


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
       
        public int Add(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
           
           
            try
            {
              
                    IDbDataParameter[] prams = {
                
                                               };

                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "insert into t_M_ReporInfo (rwclass,CreateDate,UserID,ReportAccessDate,ItemType,ReportName,Ulevel,urgent,wtdepart,lxman,lxtel,lxemail,address,jcmethod,Projectname,StatusID,chargeman,Green)"
               + " values('" + entity.classID + "','" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.level + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName + "','" + entity.StatusID + "','" + entity.chargeman + "','"+entity.Green+"')", prams); 
            }
            catch (Exception ex)
            {
                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }

        public DataTable Get(int id)
        {
       
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
             try
            {
               
               
                IDbDataParameter[] prams = {
                                                   };
                IDataReader idr = db.ExecuteReader(Config.constr, CommandType.Text, "select * from t_Y_FlowInfo  where id='" + id + "'", prams);

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
        public int Upate(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);
            
            try
            {

                IDbDataParameter[] prams = {
                
                                               };
       iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_M_ReporInfo set  rwclass='" + entity.classID + "', updatedate=getdate(),updateuser='" + entity.CreateUser + "',ReportAccessDate='" + entity.WTDate + "',ItemType='" + entity.TypeID + "',ReportName='" + entity.WTNO + "',Ulevel='" + entity.level + "',urgent='" + entity.Remark + "',wtdepart='" + entity.WTMan + "',lxman='" + entity.lxMan + "',lxtel='" + entity.lxtel + "',lxemail='" + entity.lxEmail + "',address='" + entity.address + "',jcmethod='" + entity.Mode + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',chargeman='" + entity.chargeman + "',Green='"+entity.Green+"' where id='" + entity.ID + "'", prams);
                          }
            catch (Exception ex)
            {
                

                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }

        public int UpateXCJC(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);


            try
            {

                IDbDataParameter[] prams = {
                
                  
                                           };
                string backstr = "";
                if (entity.StatusID == 3||entity.StatusID==1.5)
                    backstr = ", xcjcbackremark='" + entity.Remark + "'";
                if (entity.StatusID != 4 && entity.StatusID != 3&&entity.StatusID!=1.5)
                {
                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set  updatedate=getdate(),updateuser='" + entity.CreateUser + "',varman5='" + entity.CreateUser + "',vardate5='" + entity.CreateDate + "',varremark5='" + entity.Remark + "'" + backstr + " where id='" + entity.ID + "'", prams);
                    //. + " values('" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.level + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName + "','" + entity.StatusID + "','" + entity.chargeman + "')", prams);

                }
                else
                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set xcjcbackflag='" + entity.backflag + "',  updatedate=getdate(),updateuser='" + entity.CreateUser + "',StatusID='" + entity.StatusID + "',varman5='" + entity.CreateUser + "',vardate5='" + entity.CreateDate + "',varremark5='" + entity.Remark + "' " + backstr + " where id='" + entity.ID + "'", prams);

                //iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set  rwclass='" + entity.classID + "', updatedate=getdate(),updateuser='" + entity.CreateUser + "',ReportAccessDate='" + entity.WTDate + "',ItemType='" + entity.TypeID + "',ReportName='" + entity.WTNO + "',urgent='" + entity.Remark + "',wtdepart='" + entity.WTMan + "',lxman='" + entity.lxMan + "',lxtel='" + entity.lxtel + "',lxemail='" + entity.lxEmail + "',address='" + entity.address + "',jcmethod='" + entity.Mode + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',,chargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "' where id='" + entity.ID + "'", prams);


                if (iReturn == 1&& (entity.StatusID==3||entity.StatusID==1.5))
                {
                    string str="";
                    if (entity.StatusID == 3)
                        str = "现场监测退回方案编写";
                    else if( entity.StatusID == 1.5)
                        str = "现场监测退回项目指派";
                   ItemObj backobj = new ItemObj(entity.CreateUser, entity.ID.ToString(), str, entity.CreateDate.ToString(), entity.Content);
                    backobj.save();
                    
                }

            }
            catch (Exception ex)
            {


                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }

    //报告编制
        public int UpateYSReport(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);


            try
            {

                IDbDataParameter[] prams = {
                
                  
                                           };
                string backstr = "";
                if (entity.StatusID == 3.5)
                    backstr = ", reportbackremark='" + entity.Remark + "'";
                if (entity.StatusID != 5 && entity.StatusID != 3.5)
                {
                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set  updatedate=getdate(),updateuser='" + entity.CreateUser + "',varman4='" + entity.CreateUser + "',vardate4='" + entity.CreateDate + "',varremark4='" + entity.Remark + "',ReportNO='" + entity.ReportNO + "'" + backstr + " where id='" + entity.ID + "'", prams);
                    //. + " values('" + entity.CreateDate + "','" + entity.CreateUser + "','" + entity.WTDate + "','" + entity.TypeID + "','" + entity.WTNO + "','" + entity.level + "','" + entity.Remark + "','" + entity.WTMan + "','" + entity.lxMan + "','" + entity.lxtel + "','" + entity.lxEmail + "','" + entity.address + "','" + entity.Mode + "','" + entity.ProjectName + "','" + entity.StatusID + "','" + entity.chargeman + "')", prams);

                }
                else
                    iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set reportbackflag='" + entity.backflag + "',  updatedate=getdate(),updateuser='" + entity.CreateUser + "',StatusID='" + entity.StatusID + "',varman4='" + entity.CreateUser + "',vardate4='" + entity.CreateDate + "',varremark4='" + entity.Remark + "',ReportNO='" + entity.ReportNO + "' " + backstr + " where id='" + entity.ID + "'", prams);

                //iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set  rwclass='" + entity.classID + "', updatedate=getdate(),updateuser='" + entity.CreateUser + "',ReportAccessDate='" + entity.WTDate + "',ItemType='" + entity.TypeID + "',ReportName='" + entity.WTNO + "',urgent='" + entity.Remark + "',wtdepart='" + entity.WTMan + "',lxman='" + entity.lxMan + "',lxtel='" + entity.lxtel + "',lxemail='" + entity.lxEmail + "',address='" + entity.address + "',jcmethod='" + entity.Mode + "',Projectname='" + entity.ProjectName + "',StatusID='" + entity.StatusID + "',,chargeman='" + entity.chargeman + "' ,ulevel='" + entity.level + "' where id='" + entity.ID + "'", prams);


                if (iReturn == 1 && entity.StatusID == 3.5)
                {
                    ItemObj backobj = new ItemObj(entity.CreateUser, entity.ID.ToString(), "验收方案编制退回现场监测", entity.CreateDate.ToString(), entity.Content);
                    backobj.save();
                }

            }
            catch (Exception ex)
            {


                iReturn = -1;
            }
            finally
            {
                if (db.Conn != null)
                    db.Conn.Close();
            }
            return iReturn;
        }

        public int UpateBack(Entity.AccessReport entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.constr);


            try
            {

                IDbDataParameter[] prams = {
                
                                               };


                iReturn = db.ExecuteNonQuery(Config.constr, CommandType.Text, "update t_Y_FlowInfo set backflag='" + entity.backflag + "',StatusID='" + entity.StatusID + "',tkwether=null, updatedate=getdate(),updateuser='" + entity.CreateUser + "',varremark2='"+entity.Remark+"' where id='" + entity.ID + "'", prams);

                    if (iReturn==1)
                    {
                        ItemObj backobj = new ItemObj(entity.CreateUser, entity.ID.ToString(), "踏勘退回指派", entity.CreateDate.ToString(), entity.Remark);
                        backobj.save();
                    }


            }
            catch (Exception ex)
            {


                iReturn = -1;
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
