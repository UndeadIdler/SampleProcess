using System;
using System.Collections.Generic;

using System.Web;
namespace Entity{
/// <summary>
///Sample 的摘要说明
/// </summary>
    public class Sample
    {
        public Sample()
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
        /// 样品编号ID
        /// </summary>
        private int _NO;
        public int NO
        {
            get { return _NO; }
            set { _NO = value; }
        }
        /// <summary>
        /// 报告ID
        /// </summary>
        private int _reportid;
        public int ReportID
        {
            get { return _reportid; }
            set { _reportid = value; }
        }
        /// <summary>
        /// 接样日期
        /// </summary>
        private DateTime _AccessDate;
        public DateTime AccessDate
        {
            get { return _AccessDate; }
            set { _AccessDate = value; }
        }

        /// <summary>
        /// 采样日期
        /// </summary>
        private DateTime _SampleDate;
        public DateTime SampleDate
        {
            get { return _SampleDate; }
            set { _SampleDate = value; }
        }
        /// <summary>
        /// 样品类型
        /// </summary>
        private int _TypeID;
        public int TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }
        /// <summary>
        /// 样品类型
        /// </summary>
        private string _SampleType;
        public string SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
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
        /// 样品来源
        /// </summary>
        private string _SampleSource;
        public string SampleSource
        {
            get { return _SampleSource; }
            set { _SampleSource = value; }
        }
        /// <summary>
        /// 样品性状
        /// </summary>
        private string _SampleProperty;
        public string SampleProperty
        {
            get { return _SampleProperty; }
            set { _SampleProperty = value; }
        }
        /// <summary>
        /// 采样点
        /// </summary>
        private string _SampleAddress;
        public string SampleAddress
        {
            get { return _SampleAddress; }
            set { _SampleAddress = value; }
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
        /// 测试地点
        /// </summary>
        private String _TestPlace;
        public String TestPlace
        {
            get { return _TestPlace; }
            set { _TestPlace = value; }
        }

        /// <summary>
        /// 检测日期
        /// </summary>
        private String _TestDate;
        public String TestDate
        {
            get { return _TestDate; }
            set { _TestDate = value; }
        }

        /// <summary>
        /// 检测人
        /// </summary>
        private String _TestMan;
        public String TestMan
        {
            get { return _TestMan; }
            set { _TestMan = value; }
        }
        /// <summary>
        /// 是否现场分析
        /// </summary>
        private String _xcflag;
        public String xcflag
        {
            get { return _xcflag; }
            set { _xcflag = value; }
        }

        /// <summary>
        /// 样品状态
        /// </summary>
        private int _ypstatus;
        public int ypstatus
        {
            get { return _ypstatus; }
            set { _ypstatus = value; }
        }

        /// <summary>
        /// 样品单状态
        /// </summary>
        private int _statusid;
        public int StatusID
        {
            get { return _statusid; }
            set { _statusid = value; }
        }
        /// <summary>
        /// 数据状态
        /// </summary>
        private int _datastatus;
        public int datastatus
        {
            get { return _datastatus; }
            set { _datastatus = value; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        private int _num;
        public int num
        {
            get { return _num; }
            set { _num = value; }
        }
        /// <summary>
        /// bz
        /// </summary>
        private string _bzid;
        public string bzid
        {
            get { return _bzid; }
            set { _bzid = value; }
        }
        /// <summary>
        /// bzid
        /// 
        /// </summary>
        private string _bz;
        public string bz
        {
            get { return _bz; }
            set { _bz = value; }
        }
        /// <summary>
        /// bzfw
        /// </summary>
        private string _bzfw;
        public string bzfw
        {
            get { return _bzfw; }
            set { _bzfw = value; }
        }
        /// <summary>
        /// 降水开始时间
        /// </summary>
        private DateTime _starttime;
        public DateTime starttime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }
        /// <summary>
        /// 降水结束时间
        /// </summary>
        private DateTime _endtime;
        public DateTime endtime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }
        private string  _projectname;
        public string projectname
        {
            get { return _projectname; }
            set { _projectname = value; }
        }
        public List<SampleItem> SampleItemList = new List<SampleItem>();
        public List<Draw> DrawList = new List<Draw>();


    }
}