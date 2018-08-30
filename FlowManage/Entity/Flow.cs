using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowManage
{
    public class Flow
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
        /// 项目名称,三同时验收
        /// </summary>
        private string _itemname;
        public string itemname
        {
            get { return _itemname; }
            set { _itemname = value; }
        }

        /// <summary>
        /// 受理人
        /// </summary>
        private string _accessman;
        public string accessman
        {
            get { return _accessman; }
            set { _accessman = value; }
        }

        /// <summary>
        /// 委托日期
        /// </summary>
        private DateTime _accessdate;
        public DateTime accessdate
        {
            get { return _accessdate; }
            set { _accessdate = value; }
        }

        /// <summary>
        /// 委托单位
        /// </summary>
        private bool _wtdw;
        public bool wtdw
        {
            get { return _wtdw; }
            set { _wtdw = value; }
        }
        /// <summary>
        ///联系人
        /// </summary>
        private int _lxman;
        public int lxman
        {
            get { return _lxman; }
            set { _lxman = value; }
        }
        /// <summary>
        /// 联系人电话
        /// </summary>
        private string _lxtel;
        public string lxtel
        {
            get { return _lxtel; }
            set { _lxtel = value; }
        }

        /// <summary>
        /// 单位地址
        /// </summary>
        private bool _address;
        public bool address
        {
            get { return _address; }
            set { _address = value; }
        }


        /// <summary>
        /// 验收状态
        /// </summary>
        private string _StatusID;
        public string StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        /// <summary>
        /// 备注说明
        /// </summary>
        private int _accessremark;
        public int accessremark
        {
            get { return _accessremark; }
            set { _accessremark = value; }
        }

    }
}
