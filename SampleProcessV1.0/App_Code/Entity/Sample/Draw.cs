using System;
using System.Collections.Generic;

using System.Web;
namespace Entity{
/// <summary>
///Sample 的摘要说明
/// </summary>
    public class Draw
    {
        public Draw()
        {
        }
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
        /// 样品编号
        /// </summary>
        private string _SampleID;
        public string SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }
        /// <summary>
        /// 样品领用时间
        /// </summary>
        private DateTime _LyDate;
        public DateTime LyDate
        {
            get { return _LyDate; }
            set { _LyDate = value; }
        }
       
        /// <summary>
        /// 登记人
        /// </summary>
        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        /// <summary>
        /// 分析项目
        /// </summary>
        private string _itemlist;
        public string ItemList
        {
            get { return _itemlist; }
            set { _itemlist = value; }
        }
         /// <summary>
        /// 分析项目
        /// </summary>
        private string _itemvaluelist;
        public string ItemValueList
        {
            get { return _itemvaluelist; }
            set { _itemvaluelist = value; }
        }
        
        /// <summary>
        /// 登记时间
        /// </summary>
        private DateTime _createdate;
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        /// 还样时间
        /// </summary>
        private string _returndate;
        public string returndate
        {
            get { return _returndate; }
            set { _returndate = value; }
        }
        /// <summary>
        /// 还样说明
        /// </summary>
        private string _Remark;
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        /// <summary>
        /// 校核人
        /// </summary>
        private string _jhman;
        public string jhman
        {
            get { return _jhman; }
            set { _jhman = value; }
        }
        /// <summary>
        ///校核时间
        /// </summary>
        private DateTime _jhtime;
        public DateTime jhtime
        {
            get { return _jhtime; }
            set { _jhtime = value; }
        }
        /// <summary>
        /// 分析完成时间
        /// </summary>
        private DateTime _finishdate;
        public DateTime finishdate
        {
            get { return _finishdate; }
            set { _finishdate = value; }
        }
        /// <summary>
        /// 类型0-实验室，1-现场
        /// </summary>
        private int _type;
        public int type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 0-未分析，1-待交接；2-交接完
        /// </summary>
        private int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 分析人
        /// </summary>
        private string _fxman;
        public string fxman
        {
            get { return _fxman; }
            set { _fxman = value; }
        }
       
        public List<SampleItem> SampleItemList = new List<SampleItem>();


    }
}