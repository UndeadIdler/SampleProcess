using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Collections.Generic;

namespace Entity.Car
{
    /// <summary>
    ///Car 的摘要说明
    /// </summary>

    public class OutCar
    {
        /// <summary>
        /// id
        /// </summary>
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        private string carNO;
        public string CarNO
        {
            get { return carNO; }
            set { carNO = value; }
        }
       /// <summary>
       /// 司机
       /// </summary>
        private string driver;
        public string Driver
        {
            get { return driver; }
            set { driver = value; }
        }
       
       /// <summary>
       /// 目的地
       /// </summary>
        private string destn;
        public string Destn
        {
            get { return destn; }
            set { destn = value; }
        }
        /// <summary>
        /// 出发时间
        /// </summary>
        private DateTime outStart;
        public DateTime OutStart
        {
            get { return outStart; }
            set { outStart = value; }
        }
        /// <summary>
        /// 用车结束时间
        /// </summary>
        private DateTime outEnd;
        public DateTime OutEnd
        {
            get { return outEnd; }
            set { outEnd = value; }
        }
       /// <summary>
       /// 备注
       /// </summary>
        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        
        private string createuser;
        public string CreateUser
        {
            get { return createuser; }
            set { createuser = value; }
        }
        private string updateuser;
        public string UpdateUser
        {
            get { return updateuser; }
            set { updateuser = value; }
        }
        private DateTime createdate;
        public DateTime CreateDate
        {
            get { return createdate; }
            set { createdate = value; }
        }
        private DateTime updatedate;
        public DateTime UpdateDate
        {
            get { return updatedate; }
            set { updatedate = value; }
        }
        private int realnum;
        public int RealNum
        {
            get { return realnum; }
            set { realnum = value; }
        }
        private int num;
        public int Num
        {
            get { return num; }
            set {num = value; }
        }
        private string all;
        public string All
        {
            get { return all; }
            set { all = value; }
        }
        public List<Person> Together = new List<Person>();
        //public Car CarEntity = new Car();
        public List<string> retstr = new List<string>();
        private int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
    }
}
