using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Entity.Car;

using WebApp.Components;
using System.Collections.Generic;
namespace DAL.CarManager
{
    /// <summary>
    ///OutManager 的摘要说明
    /// </summary>
    public class OutManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int add(OutCar entity)
        {
            int id = 0;
            string sqlstr = String.Format(@"insert into t_c_outinfo(carid,destn,remark,createdate,createuser,outstart,outend,driver) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", entity.CarNO, entity.Destn, entity.Remark, entity.CreateDate, entity.CreateUser, entity.OutStart, entity.OutEnd, entity.Driver);

            MyDataOp db = new MyDataOp(sqlstr);
            if (db.ExecuteCommand())
            {
                db=null;
                string selectstr =String.Format(@"select * from t_c_outinfo  where carid='{0}' and outstart='{1}' and outend='{2}'",entity.CarNO,entity.OutStart,entity.OutEnd);
                SqlDataReader sdr = new MyDataOp(selectstr).CreateReader();
                while (sdr.Read())
                {
                    id = int.Parse(sdr["id"].ToString());
                }
            }
            return id;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool update(OutCar entity)
        {
            string sqlstr = String.Format(@"update t_c_outinfo set carid='{0}',updatedate='{1}',updateuser='{2}',destn='{3}',remark='{4}',outstart='{5}',outend='{6}',driver='{7}' where id='{8}'", entity.CarNO, entity.UpdateDate, entity.UpdateUser, entity.Destn, entity.Remark,entity.OutStart,entity.OutEnd,entity.Driver, entity.ID);

            MyDataOp db = new MyDataOp(sqlstr);
            return db.ExecuteCommand();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool delete(string id)
        {
            string sqlstr = String.Format(@"delete from t_c_outinfo where id='{0}'", id);
            MyDataOp db = new MyDataOp(sqlstr);
            return db.ExecuteCommand();
        }
        /// <summary>
        /// 新增、修改时的重复性检查
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool IsExist(OutCar entity,string flag)
        {
            bool Existflag = false;
             
            string  constr="";
            if(flag=="1")//0-新增；1-修改
            {
                constr=" and id!='"+entity.ID+"'";
            }
            string sqlstr = String.Format(@"select * from t_c_outinfo where carid='{0}' and (outstart between '{1}' and '{2}' or outend between '{3}' and '{4}')" + constr,entity.CarNO,entity.OutStart,entity.OutEnd,entity.OutStart,entity.OutEnd);
            DataSet ds = new MyDataOp(sqlstr).CreateDataSet();
            if (ds.Tables[0].Rows.Count>0)
            {
                Existflag = true;
            }
            return Existflag;
        }
        /// <summary>
        /// 新增、修改时的重复性检查
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool IsExistUnder(string id)
        {
            bool Existflag = false;

            string sqlstr = String.Format(@"select * from t_c_outdetail where outid='{0}'",id);
            DataSet ds = new MyDataOp(sqlstr).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Existflag = true;
            }
            return Existflag;

        }
        public DataSet Query(string carid,string s,string e)
        {
            List<OutCar> carList = new List<OutCar>();
            string str="";
            if (carid != "")
            {
                str += " and t_c_outinfo.carid='" + carid + "'";
            }
            if (s != "" && e != "")
            {
                str += " and outstart between '"+s+"' and '"+e+"'";
            }
            else if (s != "" && e == "")
            {
                str += " and '" + s + "'  between outstart and  outend";
            }

            string sqlstr = "select t_c_outinfo.*,t_c_carinfo.num from t_c_outinfo inner join t_c_carinfo on t_c_outinfo.carid=t_c_carinfo.carid where 1=1" + str + "";
            string sql = "select t_c_outdetail.* from t_c_outdetail inner join t_c_outinfo on t_c_outinfo.id=t_c_outdetail.outid where 1=1 " + str;
            DataSet ds = new MyDataOp(sqlstr).CreateDataSet();
            DataSet dsperson = new MyDataOp(sql).CreateDataSet();
            DataColumn dc = new DataColumn("realnum");
            ds.Tables[0].Columns.Add(dc);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //OutCar temp = new OutCar();
                    //temp.CarNO = dr["carid"].ToString();
                    //temp.CreateDate = DateTime.Parse(dr["createdate"].ToString());
                    //temp.CreateUser = dr["createuser"].ToString();
                    //temp.Destn = dr["deston"].ToString();
                    //temp.Driver = dr["driver"].ToString();
                    //temp.ID = dr["id"].ToString();
                    //temp.RealNum = dr["realnum"].ToString();
                   
                    //temp.Num = int.Parse(dr["num"].ToString());
                    DataRow[] drr = dsperson.Tables[0].Select("outid='"+dr["id"].ToString()+"'");
                    //temp.Together = new List<Person>();

                    if (drr.Length > 0)
                    {
                        dr["realnum"] = drr.Length.ToString();
                        //temp.RealNum = drr.Length;
                        //for(int i=0;i<drr.Length;i++)
                        //{
                        //    Person tperson=new Person();
                        //    tperson.ID=drr["id"].ToString();
                        //    tperson.Destn=drr["destn"].ToString();
                        //    tperson.Name=drr["name"].ToString();
                        //    tperson.OutID=drr["outid"].ToString();
                        //    temp.Together.Add(tperson);
                        //}

                    }
                    else
                        dr["realnum"] ="0";
                   
                }
            }
            return ds;
        }
        public List<OutCar> QueryList(string carid, string s, string e)
        {
            List<OutCar> carList = new List<OutCar>();
            string str = "";
            if (carid != "")
            {
                str += " and t_c_outinfo.carid='" + carid + "'";
            }
            if (s != "" && e != "")
            {
                str += " and outstart between '" + s + "' and '" + e + "'";
            }
            string sqlstr = "select t_c_outinfo.*,t_c_carinfo.num from t_c_outinfo inner join t_c_carinfo on t_c_outinfo.carid=t_c_carinfo.carid where 1=1" + str + "";
            string sql = "select t_c_outdetail.* from t_c_outdetail inner join t_c_outinfo on t_c_outinfo.id=t_c_outdetail.outid where 1=1 " + str;
            DataSet ds = new MyDataOp(sqlstr).CreateDataSet();
            DataSet dsperson = new MyDataOp(sql).CreateDataSet();
            DataColumn dc = new DataColumn("realnum");
            ds.Tables[0].Columns.Add(dc);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //OutCar temp = new OutCar();
                    //temp.CarNO = dr["carid"].ToString();
                    //temp.CreateDate = DateTime.Parse(dr["createdate"].ToString());
                    //temp.CreateUser = dr["createuser"].ToString();
                    //temp.Destn = dr["deston"].ToString();
                    //temp.Driver = dr["driver"].ToString();
                    //temp.ID = dr["id"].ToString();
                    //temp.RealNum = dr["realnum"].ToString();

                    //temp.Num = int.Parse(dr["num"].ToString());
                    DataRow[] drr = dsperson.Tables[0].Select("outid='" + dr["id"].ToString() + "'");
                    //temp.Together = new List<Person>();

                    if (drr.Length > 0)
                    {
                        dr["realnum"] = drr.Length.ToString();
                        //temp.RealNum = drr.Length;
                        //for(int i=0;i<drr.Length;i++)
                        //{
                        //    Person tperson=new Person();
                        //    tperson.ID=drr["id"].ToString();
                        //    tperson.Destn=drr["destn"].ToString();
                        //    tperson.Name=drr["name"].ToString();
                        //    tperson.OutID=drr["outid"].ToString();
                        //    temp.Together.Add(tperson);
                        //}

                    }
                    else
                        dr["realnum"] = "0";

                }
            }
            return carList;
        }

        public DataSet QueryA(string carid, string s, string e)
        {
            List<OutCar> carList = new List<OutCar>();
            string str = "";
            if (carid != "")
            {
                str += " and t_c_outinfo.carid='" + carid + "'";
            }
            if (s != "" && e != "")
            {
                str += " and outstart between '" + s + "' and '" + e + "'";
            }
            string sqlstr = "select t_c_outinfo.id, CONVERT(varchar(12) , t_c_outinfo.outstart, 108 ) outstart,CONVERT(varchar(12) , t_c_outinfo.outend, 108 ) outend,t_c_carinfo.num from t_c_outinfo inner join t_c_carinfo on t_c_outinfo.carid=t_c_carinfo.carid where 1=1" + str + "";
            string sql = "select t_c_outdetail.* from t_c_outdetail inner join t_c_outinfo on t_c_outinfo.id=t_c_outdetail.outid where 1=1 " + str;
            DataSet ds = new MyDataOp(sqlstr).CreateDataSet();
            DataSet dsperson = new MyDataOp(sql).CreateDataSet();
            DataColumn dc = new DataColumn("realnum");
            ds.Tables[0].Columns.Add(dc);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                    DataRow[] drr = dsperson.Tables[0].Select("outid='" + dr["id"].ToString() + "'");
                   

                    if (drr.Length > 0)
                    {
                        dr["realnum"] = drr.Length.ToString();
                    }
                    else
                        dr["realnum"] = "0";

                }
            }
            return ds;
        }
        
    }
}
