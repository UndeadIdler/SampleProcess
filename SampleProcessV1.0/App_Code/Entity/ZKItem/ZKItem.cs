using System;
using System.Collections.Generic;

using System.Web;
namespace Entity
{
/// <summary>
///Sample 的摘要说明
/// </summary>
    public class ZKItem
    {
        public ZKItem()
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
        private DateTime _CreateDate;
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// 分析结果登记人
        /// </summary>
        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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
        /// 分析样品数
        /// </summary>
        private int _analysisnum;
        public int analysisnum
        {
            get { return _analysisnum; }
            set { _analysisnum = value; }
        }
        /// <summary>
        /// 现场平行样检查数
        /// </summary>
        private int _scenejcnum;
        public int scenejcnum
        {
            get { return _scenejcnum; }
            set { _scenejcnum = value; }
        }
        /// <summary>
        /// 现场平行样合格数
        /// </summary>
        private int _scenehgnum;
        public int scenehgnum
        {
            get { return _scenehgnum; }
            set { _scenehgnum = value; }
        }
        /// <summary>
        /// 实验室平行检查数
        /// </summary>
        private int _experimentjcnum;
        public int experimentjcnum
        {
            get { return _experimentjcnum; }
            set { _experimentjcnum = value; }
        }
        /// <summary>
        /// 实验室平行合格数
        /// </summary>
        private int _experimenthgnum;
        public int experimenthgnum
        {
            get { return _experimenthgnum; }
            set { _experimenthgnum = value; }
        }
        /// <summary>
        /// 加标回收检查数
        /// </summary>
        private int _jbhsjcnum;
        public int jbhsjcnum
        {
            get { return _jbhsjcnum; }
            set { _jbhsjcnum = value; }
        }
        /// <summary>
        /// 加标回收合格数
        /// </summary>
        private int _jbhshgnum;
        public int jbhshgnum
        {
            get { return _jbhshgnum; }
            set { _jbhshgnum = value; }
        }

        /// <summary>
        ///全程序空白检查数
        /// </summary>
        private int _alljcnum;
        public int alljcnum
        {
            get { return _alljcnum; }
            set { _alljcnum = value; }
        }
        /// <summary>
        /// 全程序空白合格数
        /// </summary>
        private int _allhgnum;
        public int allhgnum
        {
            get { return _allhgnum; }
            set { _allhgnum = value; }
        }

        /// <summary>
        ///密码样检查数
        /// </summary>
        private int _mmjcnum;
        public int mmjcnum
        {
            get { return _mmjcnum; }
            set { _mmjcnum = value; }
        }
        /// <summary>
        /// 密码样合格数
        /// </summary>
        private int _mmhgnum;
        public int mmhgnum
        {
            get { return _mmhgnum; }
            set { _mmhgnum = value; }
        }

        /// <summary>
        ///标样检查数
        /// </summary>
        private int _byjcnum;
        public int byjcnum
        {
            get { return _byjcnum; }
            set { _byjcnum = value; }
        }
        /// <summary>
        /// 标样合格数
        /// </summary>
        private int _byhgnum;
        public int byhgnum
        {
            get { return _byhgnum; }
            set { _byhgnum = value; }
        }
        /// <summary>
        ///总检查数
        /// </summary>
        private int _amount;
        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        /// <summary>
        /// 分析结果修改日期
        /// </summary>
        private DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        /// <summary>
        /// 分析结果修改人
        /// </summary>
        private string _UpdateUserID;
        public string UpdateUserID
        {
            get { return _UpdateUserID; }
            set { _UpdateUserID = value; }
        }
    }
}