using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
/// <summary>
/// Item 的摘要说明
/// </summary>
    public class Item
    {
        public Item()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        ///监测项Id
        /// </summary>
        private string _itemid;
        public string itemid
        {
            get { return _itemid; }
            set { _itemid = value; }
        }
        ///监测项范围
        /// </summary>
        private string _itemfw;
        public string itemfw
        {
            get { return _itemfw; }
            set { _itemfw = value; }
        }
    }
}