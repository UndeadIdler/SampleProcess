using System;
using System.Collections.Generic;

using System.Web;

namespace Entity{
/// <summary>
///Report 的摘要说明
/// </summary>
public class Report
{
	public Report()
	{
       
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 委托单位
    /// </summary>
    private string _WTMan;
    public string WTMan
    {
        get { return _WTMan; }
        set { _WTMan = value; }
    }
    /// <summary>
    /// 委托日期
    /// </summary>
    private string _WTDate;
    public string WTDate
    {
        get { return _WTDate; }
        set { _WTDate = value; }
    }
    /// <summary>
    /// 检测方法
    /// </summary>
    private string _JCMethod;
    public string JCMethod
    {
        get { return _JCMethod; }
        set { _JCMethod = value; }
    }
    /// <summary>
    /// 采样方
    /// </summary>
    private string _SampleMan;
    public string SampleMan
    {
        get { return _SampleMan; }
        set { _SampleMan = value; }
    }
    /// <summary>
    /// 检测日期
    /// </summary>
    private string _JCDate;
    public string JCDate
    {
        get { return _JCDate; }
        set { _JCDate = value; }
    }
    /// <summary>
    /// 采样日期
    /// </summary>
    private string _SampleDate;
    public string SampleDate
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
    /// 采样来源
    /// </summary>
    private string _SampleSource;
    public string SampleSource
    {
        get { return _SampleSource; }
        set { _SampleSource = value; }
    }
    /// <summary>
    /// 接样时间
    /// </summary>
    private string _AccessDate;
    public string AccessDate
    {
        get { return _AccessDate; }
        set { _AccessDate = value; }
    }

    /// <summary>
    /// 报告编制人
    /// </summary>
    private string _ReportMan;
    public string ReportMan
    {
        get { return _ReportMan; }
        set { _ReportMan = value; }
    }

    /// <summary>
    /// 报告编制时间
    /// </summary>
    private string _ReportTime;
    public string ReportTime
    {
        get { return _ReportTime; }
        set { _ReportTime = value; }
    }
    /// <summary>
    /// 报告审核人
    /// </summary>
    private string _CheckMan;
    public string CheckMan
    {
        get { return _CheckMan; }
        set { _CheckMan = value; }
    }
    /// <summary>
    /// 报告审核时间
    /// </summary>
    private string _CheckTime;
    public string CheckTime
    {
        get { return _CheckTime; }
        set { _CheckTime = value; }
    }

    /// <summary>
    /// 备注
    /// </summary>
    private string _Remark;
    public string Remark
    {
        get { return _Remark; }
        set { _Remark = value; }
    }

    public List<string> ItemList = new List<string>();
 public   List<Sample> SampleList = new List<Sample>();
}
}