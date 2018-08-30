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

    public class Person
    {
        /// <summary>
        ///自增id
        /// </summary>
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 人员
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
       /// <summary>
       ///  出车ID
       /// </summary>
        private string outid;
        public string OutID
        {
            get { return outid; }
            set { outid = value; }
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
    }
}
