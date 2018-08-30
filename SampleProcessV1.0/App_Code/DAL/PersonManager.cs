using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Entity.Car;
using WebApp.Components;
namespace DAL.CarManager
{
    /// <summary>
    ///CarManager 的摘要说明
    /// </summary>
    public class PersonManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool add(Person entity)
        {
            string sqlstr = String.Format(@"insert into t_c_outdetail(name,destn,remark,createdate,createuser,outid) values('{0}','{1}','{2}','{3}','{4}','{5}')", entity.Name, entity.Destn, entity.Remark, entity.CreateDate, entity.CreateUser, entity.OutID);

            MyDataOp db = new MyDataOp(sqlstr);
            return db.ExecuteCommand();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool update(Person entity)
        {
            string sqlstr = String.Format(@"update t_c_outdetail set name='{0}',updatedate='{1}',updateuser='{2}',destn='{3}',remark='{4}' where id='{5}'", entity.Name, entity.CreateDate, entity.CreateUser, entity.Destn,entity.Remark,entity.ID);

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
            string sqlstr = String.Format(@"delete from t_c_outdetail where id='{0}'",id);
            MyDataOp db = new MyDataOp(sqlstr);
            return db.ExecuteCommand();
        }
        public bool IsExist(OutCar entity,Person person, string flag)
        {
            bool Existflag = false;

            string constr = "";
            if (flag == "1")//0-新增；1-修改
            {
                constr = " and id!='" + person.ID + "'";
            }
            string sqlstr = String.Format(@"select * from t_c_outinfo inner join t_c_outdetail on t_c_outinfo.id=t_c_outdetail.outid where name='{0}' and (outstart between '{1}' and '{2}' or outend between '{3}' and '{4}')" + constr, person.Name, entity.OutStart, entity.OutEnd, entity.OutStart, entity.OutEnd);
            DataSet ds = new MyDataOp(sqlstr).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Existflag = true;
            }
            return Existflag;
        }
        public DataSet Query(string outid)
        {
            string strsql = "select * from t_c_outdetail where outid='" + outid + "'";
            return new MyDataOp(strsql).CreateDataSet();
 
        }
        public bool MaxCheck(string carid,string outid)
        {
            string checkstr = "select count (*) from t_c_outdetail inner join t_c_outinfo on t_c_outinfo.id=t_c_outdetail.outid  where t_c_outinfo.carid='" + carid + "' and t_c_outinfo.id='"+outid+"'  ";
            DataSet ds = new MyDataOp(checkstr).CreateDataSet();
            BLL.Car.Car car = new BLL.Car.Car();
           DataSet dscar= car.Query("", carid);
           if (int.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1 < int.Parse(dscar.Tables[0].Rows[0]["num"].ToString()))
                return true;
            else
                return false;
        }
    }
}
