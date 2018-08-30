using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entity
{
/// <summary>
/// Station 的摘要说明
/// </summary>
    public class Station
    {
        public Station()
        {
        }

        /// <summary>
        /// id
        /// </summary>
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        ///_qymc
        /// </summary>
        private string _qymc;
        public string qymc
        {
            get { return _qymc; }
            set { _qymc = value; }
        }
        /// <summary>
        ///_法人代码
        /// </summary>
        private string _frdm;
        public string frdm
        {
            get { return _frdm; }
            set { _frdm = value; }
        }
        ///_cname
        /// </summary>
        private string _cname;
        public string cname
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        ///_dz
        /// </summary>
        private string _dz;
        public string dz
        {
            get { return _dz; }
            set { _dz = value; }
        }
        /// <summary>
        ///_jgdm
        /// </summary>
        private string _jgdm;
        public string jgdm
        {
            get { return _jgdm; }
            set { _jgdm = value; }
        }
        /// <summary>
        ///_sd
        /// </summary>
        private string _sd;
        public string sd
        {
            get { return _sd; }
            set { _sd = value; }
        }
        /// <summary>
        ///_yzbm
        /// </summary>
        private string _yzbm;
        public string yzbm
        {
            get { return _yzbm; }
            set { _yzbm = value; }
        }
        /// <summary>
        ///_frdb
        /// </summary>
        private string _frdb;
        public string frdb
        {
            get { return _frdb; }
            set { _frdb = value; }
        }
        /// <summary>
        ///_tel1
        /// </summary>
        private string _tel1;
        public string tel1
        {
            get { return _tel1; }
            set { _tel1 = value; }
        }
        /// <summary>
        ///_mobile1
        /// </summary>
        private string _mobile1;
        public string mobile1
        {
            get { return _mobile1; }
            set { _mobile1 = value; }
        }
        /// <summary>
        ///zfw1
        /// </summary>
        private string _zfw1;
        public string zfw1
        {
            get { return _zfw1; }
            set { _zfw1 = value; }
        }
        /// <summary>
        ///hbfg
        /// </summary>
        private string _hbfg;
        public string hbfg
        {
            get { return _hbfg; }
            set { _hbfg = value; }
        }
        /// <summary>
        ///_tel2
        /// </summary>
        private string _tel2;
        public string tel2
        {
            get { return _tel2; }
            set { _tel2 = value; }
        }
        /// <summary>
        ///_mobile2
        /// </summary>
        private string _mobile2;
        public string mobile2
        {
            get { return _mobile2; }
            set { _mobile2 = value; }
        }
        /// <summary>
        ///zfw2
        /// </summary>
        private string _zfw2;
        public string zfw2
        {
            get { return _zfw2; }
            set { _zfw2 = value; }
        }

        /// <summary>
        ///hbfz
        /// </summary>
        private string _hbfz;
        public string hbfz
        {
            get { return _hbfz; }
            set { _hbfz = value; }
        }
        /// <summary>
        ///_tel3
        /// </summary>
        private string _tel3;
        public string tel3
        {
            get { return _tel3; }
            set { _tel3 = value; }
        }
        /// <summary>
        ///_mobile3
        /// </summary>
        private string _mobile3;
        public string mobile3
        {
            get { return _mobile3; }
            set { _mobile3 = value; }
        }
        /// <summary>
        ///zfw3
        /// </summary>
        private string _zfw3;
        public string zfw3
        {
            get { return _zfw3; }
            set { _zfw3 = value; }
        }
        /// <summary>
        ///wsrw
        /// </summary>
        private string _wsrw;
        public string wsrw
        {
            get { return _wsrw; }
            set { _wsrw = value; }
        }
        /// <summary>
        ///wstime
        /// </summary>
        private string _wstime;
        public string wstime
        {
            get { return _wstime; }
            set { _wstime = value; }
        }
        /// <summary>
        ///grrw
        /// </summary>
        private string _grrw;
        public string grrw
        {
            get { return _grrw; }
            set { _grrw = value; }
        }
        /// <summary>
        ///grtime
        /// </summary>
        private string _grtime;
        public string grtime
        {
            get { return _grtime; }
            set { _grtime = value; }
        }
        /// <summary>
        ///czhm
        /// </summary>
        private string _czhm;
        public string czhm
        {
            get { return _czhm; }
            set { _czhm = value; }
        }


        /// <summary>
        ///email
        /// </summary>
        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        ///cp
        /// </summary>
        private string _cp;
        public string cp
        {
            get { return _cp; }
            set { _cp = value; }
        }
        /// <summary>
        ///industry
        /// </summary>
        private string _industry;
        public string industry
        {
            get { return _industry; }
            set { _industry = value; }
        }
        /// <summary>
        ///  status
        /// </summary>
        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 标准
        /// </summary>
        private string _bz;
        public string bz
        {
            get { return _bz; }
            set { _bz = value; }
        }
        /// <summary>
        /// 控制级别
        /// </summary>
        private string _control;
        public string control
        {
            get { return _control; }
            set { _control = value; }
        }
        /// <summary>
        ///企业简称代码
        /// </summary>
        private string _qyjcdm;
        public string qyjcdm
        {
            get { return _qyjcdm; }
            set { _qyjcdm = value; }
        }
        
        /// <summary>
        /// 类型
        /// </summary>
        private int _type;
        public int type
        {
            get { return _type; }
            set { _type = value; }
        }
         /// <summary>
        ///企业建立时间
        /// </summary>
        private string _createdate;
        public string createdate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        ///企业工商营业范围
        /// </summary>
        private string _other;
        public string other
        {
            get { return _other; }
            set { _other = value; }
        }
        /// <summary>
        ///  statusID
        /// </summary>
        private int _statusID;
        public int statusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }
    }
}