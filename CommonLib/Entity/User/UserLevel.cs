using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib.Entity.User
{
    public class UserLevel
    {
        /// <summary>
        /// 所属级别
        /// </summary>
        private int iLevelID;
        public int ILevelID
        {
            get { return iLevelID; }
            set { iLevelID = value; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        private string  iLevel;
        public string ILevel
        {
            get { return iLevel; }
            set { iLevel = value; }
        }
    }
}
