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
    public class CarManager
    {
        public bool add(Car entity)
        {
            string sqlstr = String.Format(@"insert into t_c_carinfo(carid,num,createdate,createuser) values('{0}','{1}','{2}','{3}')", entity.CarNO, entity.Num, entity.CreateDate, entity.CreateUser);

            MyDataOp db = new MyDataOp(sqlstr);
            return db.ExecuteCommand();
        }
        public bool update(Car entity)
        {
            string sqlstr = String.Format(@"update t_c_carinfo set num='{0}',updatedate='{1}',updateuser='{2}' where carid='{3}'",  entity.Num, entity.UpdateDate, entity.UpdateUser,entity.CarNO);

            MyDataOp db = new MyDataOp(sqlstr);
            return db.ExecuteCommand();
        }
        public bool delete(string id)
        {
            string sqlstr = String.Format(@"delete from t_c_carinfo where carid='{0}'", id);

            MyDataOp db = new MyDataOp(sqlstr);
            return db.ExecuteCommand();
        }
        public bool IsExist(Car entity, string flag)
        {
            bool Existflag = false;

            string constr = "";
            if (flag == "1")//0-新增；1-修改
            {
                constr = " and id!='" + entity.ID + "'";
            }
            string sqlstr = String.Format(@"select * from t_c_carinfo  where carid='{0}'" + constr, entity.CarNO);
            DataSet ds = new MyDataOp(sqlstr).CreateDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Existflag = true;
            }
            return Existflag;
        }
        public DataSet Query(string id,string carid)
        {
            string str = "";
            if (id != "")
            {
                str = " and id='" + id + "'";
            }
            if (carid != "")
            {
                str += " and carid='" + carid + "'";
            }
            string sqlstr = "select * from t_c_carinfo where 1=1 "+ str;
            return new MyDataOp(sqlstr).CreateDataSet();
        }
    }
}
