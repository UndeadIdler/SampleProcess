using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace Entity.Car
{
    /// <summary>
    ///Car 的摘要说明
    /// </summary>

    public class Car
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
       /// 限载人数
       /// </summary>
        private string num;
        public string Num
        {
            get { return num; }
            set { num = value; }
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
    }
}
