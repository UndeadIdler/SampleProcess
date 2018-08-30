using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
/// <summary>
/// stationP 的摘要说明
/// </summary>
    public class stationP
    {
        public stationP()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
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
        ///污染物类型
        /// </summary>
        private string _wrwtype;
        public string wrwtype
        {
            get { return _wrwtype; }
            set { _wrwtype = value; }
        }
        ///执行标准
        /// </summary>
        private string _bz;
        public string bz
        {
            get { return _bz; }
            set { _bz = value; }
        }
        ///企业ID
        /// </summary>
        private string _qyid;
        public string qyid
        {
            get { return _qyid; }
            set { _qyid = value; }
        }
        ///时间
        /// </summary>
        private DateTime _createdate;
        public DateTime createdate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        ///操作人
        /// </summary>
        private string _createuser;
        public string createuser
        {
            get { return _createuser; }
            set { _createuser = value; }
        }
        ///监测指标
        /// </summary>
        private string _item;
        public string item
        {
            get { return _item; }
            set { _item = value; }
        }
        ///项目类型
        /// </summary>
        private string _itemtype;
        public string itemtype
        {
            get { return _itemtype; }
            set { _itemtype = value; }
        }
        public List<Entity.Item> itemlist = new List<Entity.Item>();
    }

}