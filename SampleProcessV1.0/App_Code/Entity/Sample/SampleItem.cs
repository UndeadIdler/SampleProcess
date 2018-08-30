using System;
using System.Collections.Generic;

using System.Web;
namespace Entity
{
/// <summary>
///Sample 的摘要说明
/// </summary>
    public class SampleItem
    {
        public SampleItem()
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
        /// 分析结果登记日期
        /// </summary>
        private DateTime _AnalysisDate;
        public DateTime AnalysisDate
        {
            get { return _AnalysisDate; }
            set { _AnalysisDate = value; }
        }

        /// <summary>
        /// 分析结果登记人
        /// </summary>
        private string _AnalysisUserID;
        public string AnalysisUserID
        {
            get { return _AnalysisUserID; }
            set { _AnalysisUserID = value; }
        }

        /// <summary>
        /// 监测项所属类别
        /// </summary>
        private int _TypeID;
        public int TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }
        /// <summary>
        /// 分析项目编码
        /// </summary>
        private int _MonitorID;
        public int MonitorID
        {
            get { return _MonitorID; }
            set { _MonitorID = value; }
        }
        /// <summary>
        /// 分析项目
        /// </summary>
        private string _MonitorItem;
        public string MonitorItem
        {
            get { return _MonitorItem; }
            set { _MonitorItem = value; }
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
        /// 分析值
        /// </summary>
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
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
        /// 分析方法
        /// </summary>
        private string _Method;
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
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
        /// 是否选中
        /// </summary>
        private bool _flag;
        public bool flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        /// <summary>
        /// 指派人
        /// </summary>
        private string _zpcreateuser;
        public string zpcreateuser
        {
            get { return _zpcreateuser; }
            set { _zpcreateuser = value; }
        }
        /// <summary>
        /// 指派给谁
        /// </summary>
        private string _zpto;
        public string zpto
        {
            get { return _zpto; }
            set { _zpto = value; }
        }
        /// <summary>
        /// 指派时间
        /// </summary>
        private DateTime _zpdate;
        public DateTime zpdate
        {
            get { return _zpdate; }
            set { _zpdate = value; }
        }
        /// <summary>
        /// 是否现场监测
        /// </summary>
        private int _ckflag;
        public int ckflag
        {
            get { return _ckflag; }
            set { _ckflag = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        private int _statusID;
        public int statusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }
        /// <summary>
        ///领用ID
        /// </summary>
        private string _lyID;
        public string lyID
        {
            get { return _lyID; }
            set { _lyID = value; }
        }
        /// <summary>
        ///备注
        /// </summary>
        private string _Remark;
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        /// <summary>
        /// 领用人
        /// </summary>
        private string _LyUser;
        public string LyUser
        {
            get { return _LyUser; }
            set { _LyUser = value; }
        }
        /// <summary>
        /// 领用时间
        /// </summary>
        private DateTime _Lydate;
        public DateTime Lydate
        {
            get { return _Lydate; }
            set { _Lydate = value; }
        }

    }
}