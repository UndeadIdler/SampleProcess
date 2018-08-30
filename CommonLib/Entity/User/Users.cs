using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib.Entity.User//修改名字空间
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
        private string cName;
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string cPW;
        public string CPW
        {
            get { return cPW; }
            set { cPW = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string cMemo;
        public string CMemo
        {
            get { return cMemo; }
            set { cMemo = value; }
        }

        /// <summary>
        /// 是否停用
        /// </summary>
        private bool bStop;
        public bool BStop
        {
            get { return bStop; }
            set { bStop = value; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        private int iLevel;
        public int ILevel
        {
            get { return iLevel; }
            set { iLevel = value; }
        }
        /// <summary>
        /// 是否停用文字描述
        /// </summary>
        private string bStopName;
        public string BStopName
        {
            get { return bStopName; }
            set { bStopName = value; }
        }

        /// <summary>
        /// 是否管理员
        /// </summary>
        private bool bAdmin;
        public bool BAdmin
        {
            get { return bAdmin; }
            set { bAdmin = value; }
        }


        /// <summary>
        /// 系统类别
        /// </summary>
        private string cRole;
        public string CRole
        {
            get { return cRole; }
            set { cRole = value; }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        private int iRoleType;
        public int IRoleType
        {
            get { return iRoleType; }
            set { iRoleType = value; }
        }

        /// <summary>
        ///  角色文字描述
        /// </summary>
        private string bAdminName;
        public string BAdminName
        {
            get { return bAdminName; }
            set { bAdminName = value; }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        private string cPhone;
        public string CPhone
        {
            get { return cPhone; }
            set { cPhone = value; }
        }

        /// <summary>
        /// 区域
        /// </summary>
        private string cArea;
        public string CArea
        {
            get { return cArea; }
            set { cArea = value; }
        }


        /// <summary>
        /// 区域代码
        /// </summary>
        private string cAreaCode;
        public string CAreaCode
        {
            get { return cAreaCode; }
            set { cAreaCode = value; }
        }

        /// <summary>
        /// 部门
        /// </summary>
        private string cDepCode;
        public string CDepCode
        {
            get { return cDepCode; }
            set { cDepCode = value; }
        }

        /// <summary>
        /// 部门文字描述
        /// </summary>
        private string cDepCodeName;
        public string CDepCodeName
        {
            get { return cDepCodeName; }
            set { cDepCodeName = value; }
        }

        /// <summary>
        /// 最近活动时间
        /// </summary>
        private DateTime dActivityLastTime;
        public DateTime DActivityLastTime
        {
            get { return dActivityLastTime; }
            set { dActivityLastTime = value; }
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
    }
}