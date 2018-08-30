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
    public class Person
    {
        DAL.CarManager.PersonManager person = new PersonManager();
        public int add(Entity.Car.Person entity,Entity.Car.OutCar outcar)
        {
            if (person.MaxCheck(outcar.CarNO,outcar.ID))
            {
                if (!person.IsExist(outcar, entity, "0"))
                {
                    if (person.add(entity))
                        return 1;//成功
                    else
                        return 0;//失败
                }
                else
                    return 2;//已经存在
            }
            else
                return 3;//已经存在
        }
        public int update(Entity.Car.Person entity, Entity.Car.OutCar outcar)
        {
            if (!person.IsExist(outcar,entity, "1"))
            {
                if (person.update(entity))
                    return 1;//成功
                else
                    return 0;//失败
            }
            else
                return 2;//已经存在
        }
        public bool delete(string id)
        {
            return person.delete(id);
        }
        public DataSet Query(string outid)
        {
            return person.Query(outid);
        }
    }
}
