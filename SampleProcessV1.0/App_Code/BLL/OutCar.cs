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
using System.Collections.Generic;

namespace BLL.Car
{
    /// <summary>
    ///Car 的摘要说明
    /// </summary>
    public class OutCar
    {
        DAL.CarManager.OutManager outcar = new OutManager();
        DAL.CarManager.PersonManager person = new PersonManager();
        public int add(Entity.Car.OutCar entity)
        {
            int flag =0;
            if (!outcar.IsExist(entity, "0"))
            {
                int outid = outcar.add(entity);
                if (outid != 0)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;

                }
            }
            else
            {
                flag = 2;
            }
            return flag;
        }
        public void add(ref Entity.Car.OutCar entity)
        {
            entity.Flag = 1;
            if (!outcar.IsExist(entity, "0"))
            {
                int outid=outcar.add(entity);
                if (outid!=0)
                {
                    for (int i = 0; i < entity.RealNum; i++)
                    {
                        entity.Together[i].OutID = outid.ToString();
                        if (!person.IsExist(entity,entity.Together[i], "0"))
                        {
                            if (person.add(entity.Together[i]))
                                entity.retstr[i] = "1";//成功
                            else
                            {
                                entity.Flag = 0;
                                entity.retstr[i] = "0";//失败
                            }
                        }
                        else
                        {
                            entity.Flag = 0;
                            entity.retstr[i] = "2";//重复
                        }

                    }
                }
                else
                {
                    entity.Flag = 0;

                }
            }
            else
            {
                entity.Flag = 0;
            }
        }

        public int update( Entity.Car.OutCar entity)
        {
            int flag = 0;
            if (!outcar.IsExist(entity, "1"))
            {
                if (outcar.update(entity))
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;

                }
            }
            else
            {
                flag = 2;
            }
            return flag;
        }
        public void update(ref Entity.Car.OutCar entity)
        {
            entity.Flag = 1;
            if (!outcar.IsExist(entity, "1"))
            {
                if (outcar.update(entity))
                {
                    for (int i = 0; i < entity.RealNum; i++)
                    {
                        if (!person.IsExist(entity,entity.Together[i], "1"))
                        {
                            if (person.update(entity.Together[i]))
                                entity.retstr[i] = "1";//成功
                            else
                            {
                                entity.Flag = 0;
                                entity.retstr[i] = "0";//失败
                            }
                        }
                        else
                        {
                            entity.Flag =0;
                            entity.retstr[i] = "2";//重复
                        }

                    }
                }
                else
                {
                    entity.Flag = 0;

                }
            }
            else
            {
                entity.Flag = 0;
            }
        }
        public int  delete(string id)
        {
            if (!outcar.IsExistUnder(id))
            {
                return 2;//存在下属信息，不能删除
            }
            else
            {
                if (outcar.delete(id))
                    return 1;//删除成
                else
                    return 0;//删除失败
            }
        }
        public List<Entity.Car.OutCar> QueryList(string carid, string s, string e)
        {
           return outcar.QueryList(carid, s, e);
        }
        public DataSet Query(string carid, string s, string e)
        {
            return outcar.Query(carid, s, e);
        }
        public DataSet QueryA(string carid, string s, string e)
        {
            return outcar.QueryA(carid, s, e);
        }
    }
}
