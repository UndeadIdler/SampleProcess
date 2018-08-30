using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.User
{
    public class UserRole
    {
        /// <summary>
        /// 子增长
        /// </summary>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        private string cName;
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }

        /// <summary>
        /// 值
        /// </summary>
        private int iRole;
        public int IRole
        {
            get { return iRole; }
            set { iRole = value; }
        }
        /// <summary>
        /// 所属级别
        /// </summary>
        private int iLevel;
        public int ILevel
        {
            get { return iLevel; }
            set { iLevel = value; }
        }

        /// <summary>
        /// 。。
        /// </summary>
        private string cImage;
        public string CImage
        {
            get { return cImage; }
            set { cImage = value; }
        }
        private int _readright;
        public int ReadRight
        {
            get { return _readright; }
            set { _readright = value; }
        }
         private int _writeright;
           public int WriteRight
        {
            get { return _writeright; }
            set { _writeright = value; }
        }
           private int _ImportRight;
           public int ImportRight
        {
            get { return _ImportRight; }
            set { _ImportRight = value; }
        }
           private int _ExportRight;
           public int ExportRight
           {
               get { return _ExportRight; }
               set { _ExportRight = value; }
           }
           private int _UpLoadRight;
           public int UpLoadRight
           {
               get { return _UpLoadRight; }
               set { _UpLoadRight = value; }
           }
           private int _CheckRight;
           public int CheckRight
           {
               get { return _CheckRight; }
               set { _CheckRight = value; }
           }
           private int _Admin;
           public int Admin
           {
               get { return _Admin; }
               set { _Admin = value; }
           }
           private string _createuser;
           public string CreateUser
           {
               get { return _createuser; }
               set { _createuser = value; }
           }
           private DateTime _createdate;
           public DateTime CreateDate
           {
               get { return _createdate; }
               set { _createdate = value; }
           }

           private string _udateuser;
           public string UpdateUser
           {
               get { return _udateuser; }
               set { _udateuser = value; }
           }
           private DateTime _updatedate;
           public DateTime UpdateDate
           {
               get { return _updatedate; }
               set { _updatedate = value; }
           }

    }
}
