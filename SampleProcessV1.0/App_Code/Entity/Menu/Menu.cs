using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Menu
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
        /// 菜单名称
        /// </summary>
        private string cName;
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }

        /// <summary>
        /// 父节点编号
        /// </summary>
        private int iParentID;
        public int IParentID
        {
            get { return iParentID; }
            set { iParentID = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string cDescription;
        public string CDescription
        {
            get { return cDescription; }
            set { cDescription = value; }
        }

        /// <summary>
        /// 图片
        /// </summary>
        private string cImage;
        public string CImage
        {
            get { return cImage; }
            set { cImage = value; }
        }

        /// <summary>
        /// 页面路径
        /// </summary>
        private string cUrlPath;
        public string CUrlPath
        {
            get { return cUrlPath; }
            set { cUrlPath = value; }
        }

        /// <summary>
        /// 事件编号（functionID）
        /// </summary>
        private string cCode;
        public string CCode
        {
            get { return cCode; }
            set { cCode = value; }
        }

        /// <summary>
        /// 树结构层级
        /// </summary>
        private int iLevle;
        public int ILevle
        {
            get { return iLevle; }
            set { iLevle = value; }
        }

        /// <summary>
        /// 排列顺序
        /// </summary>
        private string iOrderID;
        public string IOrderID
        {
            get { return iOrderID; }
            set { iOrderID = value; }
        }

        /// <summary>
        /// 是否显示,0隐藏，1显示
        /// </summary>
        private int iIsShow;
        public int IIsShow
        {
            get { return iIsShow; }
            set { iIsShow = value; }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        private string cRole;
        public string CRole
        {
            get { return cRole; }
            set { cRole = value; }
        }

        /// <summary>
        /// 状态，UserPower中是否存在
        /// </summary>
        private int bState;
        public int BState
        {
            get { return bState; }
            set { bState = value; }
        }

        private int iRight;
        public int IRight
        {
            get { return iRight; }
            set { iRight = value; }
        }

        /// <summary>
        /// 是否可录入
        /// </summary> 
        private bool bLuRu = false;
        public bool BLuRu
        {
            get { return bLuRu; }
            set { bLuRu = value; }
        }


        /// <summary>
        /// 是否有上报
        /// </summary>
        private bool bUp = false;
        public bool BUp
        {
            get { return bUp; }
            set { bUp = value; }
        }


        /// <summary>
        /// 是否有审核
        /// </summary>
        private bool bCheck = false;
        public bool BCheck
        {
            get { return bCheck; }
            set { bCheck = value; }
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
        /// 创建人
        /// </summary> 
        private string cCreateUser;
        public string CCreateUser
        {
            get { return cCreateUser; }
            set { cCreateUser = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary> 
        private DateTime dUpdateDate;
        public DateTime DUpdateDate
        {
            get { return dUpdateDate; }
            set { dUpdateDate = value; }
        }

        /// <summary>
        /// 更新人
        /// </summary> 
        private string cUpdateUser;
        public string CUpdateUser
        {
            get { return cUpdateUser; }
            set { cUpdateUser = value; }
        }

        public List<Menu> children = null;
    }
}