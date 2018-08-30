using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DAL.CarManager;
using Entity.Car;

namespace BLL.Car
{
    /// <summary>
    ///Car 的摘要说明
    /// </summary>
    public class Car
    {
        DAL.CarManager.CarManager car = new CarManager();
        public int add(Entity.Car.Car entity)
        {
            if (!car.IsExist(entity,"0"))
            {
           if(car.add(entity))
               return 1;//成功
           else
               return 0;//失败
            }else
                return 2;//已经存在
        }
        public int update(Entity.Car.Car entity)
        {
            if (!car.IsExist(entity, "1"))
            {
                if (car.update(entity))
                    return 1;//成功
                else
                    return 0;//失败
            }
            else
                return 2;//已经存在
        }
        public bool delete(string id)
        {
            return car.delete(id);
        }
        public DataSet Query(string id, string carid)
        {
            return car.Query(id, carid);
        }
    }
}
