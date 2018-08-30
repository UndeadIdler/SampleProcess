using System;
using System.Collections.Generic;
using System.Text;

namespace Entity //修改名字空间
{
    public class UserPower
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
        private string cUserName;
        public string CUserName
        {
            get { return cUserName; }
            set { cUserName = value; }
        }


        /// <summary>
        /// 菜单ID，表Menu-ID
        /// </summary>
        private int iMenuId;
        public int IMenuId
        {
            get { return iMenuId; }
            set { iMenuId = value; }
        }

        /// <summary>
        /// 编辑功能
        /// </summary>
        private bool bLuRu;
        public bool BLuRu
        {
            get { return bLuRu; }
            set { bLuRu = value; }
        }

        /// <summary>
        /// 审核功能
        /// </summary>
        private bool bCheck;
        public bool BCheck
        {
            get { return bCheck; }
            set { bCheck = value; }
        }

        /// <summary>
        /// 上报功能
        /// </summary>
        private bool bUp;
        public bool BUp
        {
            get { return bUp; }
            set { bUp = value; }
        }

        private DateTime dCreateDate;
        public DateTime DCreateDate
        {
            get { return dCreateDate; }
            set { dCreateDate = value; }
        }

        private string cCreateUser;
        public string CCreateUser
        {
            get { return cCreateUser; }
            set { cCreateUser = value; }
        }

        private DateTime dUpdateDate;
        public DateTime DUpdateDate
        {
            get { return dUpdateDate; }
            set { dUpdateDate = value; }
        }

        private string cUpdateUser;
        public string CUpdateUser
        {
            get { return cUpdateUser; }
            set { cUpdateUser = value; }
        }
    }
}