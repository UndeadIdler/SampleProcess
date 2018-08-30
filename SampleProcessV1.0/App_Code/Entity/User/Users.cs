using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.User //修改名字空间
{
    public class Users
    {
        /// <summary>
        /// 自增长
        /// </summary>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string _PWD;
        public string PWD
        {
            get { return _PWD; }
            set { _PWD = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        private int _RoleID;
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        /// <summary>
        ///部门ID
        /// </summary>
        private int _DepartID;
        public int DepartID
        {
            get { return _DepartID; }
            set { _DepartID = value; }
        }
        /// <summary>
        /// 密码最后修改时间
        /// </summary>
        private DateTime _PWDModifyTime;
        public DateTime PWDModifyTime
        {
            get { return _PWDModifyTime; }
            set { _PWDModifyTime = value; }
        }

        /// <summary>
        /// 所属分组
        /// </summary>
        private bool _grouptype;
        public bool grouptype
        {
            get { return _grouptype; }
            set { _grouptype = value; }
        }

        /// <summary>
        /// 最近活动IP
        /// </summary>
        private string cActivityIP;
        public string CActivityIP
        {
            get { return cActivityIP; }
            set { cActivityIP = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime dCreateDate;
        public DateTime DCreateDate
        {
            get { return dCreateDate; }
            set { dCreateDate = value; }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        private string cCreateUser;
        public string CCreateUser
        {
            get { return cCreateUser; }
            set { cCreateUser = value; }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime dUpdateDate;
        public DateTime DUpdateDate
        {
            get { return dUpdateDate; }
            set { dUpdateDate = value; }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        private string cUpdateUser;
        public string CUpdateUser
        {
            get { return cUpdateUser; }
            set { cUpdateUser = value; }
        }


        /// <summary>
        /// 动态分配唯一标识
        /// </summary>
        private string guid;
        public string Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        public List<SampleItem> AitemList = new List<SampleItem>();
        public List<SampleItem> BitemList = new List<SampleItem>();

    }
}