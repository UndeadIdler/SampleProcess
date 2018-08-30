using System;
using System.Collections.Generic;

using System.Web;

namespace Entity{
/// <summary>
///Report 的摘要说明
/// </summary>
public class AccessReport
{
    public AccessReport()
	{
       
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// ID
    /// </summary>
    private int _id;
    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }
    /// <summary>
    /// 退回状态量
    /// </summary>
    private int _backflag;
    public int backflag
    {
        get { return _backflag; }
        set { _backflag = value; }
    }
    /// <summary>
    /// 任务类型
    /// </summary>
    private int _classID;
    public int classID
    {
        get { return _classID; }
        set { _classID = value; }
    }
    /// <summary>
    ///是否走绿色通道
    /// </summary>
    private int _Green;
    public int Green
    {
        get { return _Green; }
        set { _Green = value; }
    }
    /// <summary>
    /// 报告表示/委托编号
    /// </summary>
    private string _WTNO;
    public string WTNO
    {
        get { return _WTNO; }
        set { _WTNO = value; }
    }
    /// <summary>
    /// 委托日期
    /// </summary>
    private DateTime _WTDate;
    public DateTime WTDate
    {
        get { return _WTDate; }
        set { _WTDate = value; }
    }
    /// <summary>
    /// 项目名称
    /// </summary>
    private string _ProjectName;
    public string ProjectName
    {
        get { return _ProjectName; }
        set { _ProjectName = value; }
    }
    /// <summary>
    /// 项目名称
    /// </summary>
    private string _ProjectName2;
    public string ProjectName2
    {
        get { return _ProjectName2; }
        set { _ProjectName2 = value; }
    }
    /// <summary>
    ///函内容
    /// </summary>
    private string _Content;
    public string Content
    {
        get { return _Content; }
        set { _Content = value; }
    }
    /// <summary>
    ///函发文单位
    /// </summary>
    private string _hfwdw;
    public string hfwdw
    {
        get { return _hfwdw; }
        set { _hfwdw = value; }
    }

    /// <summary>
    ///函抄送
    /// </summary>
    private string _hcs;
    public string hcs
    {
        get { return _hcs; }
        set { _hcs = value; }
    }
    /// <summary>
    /// 紧急程度
    /// </summary>
    private string _level;
    public string level
    {
        get { return _level; }
        set { _level = value; }
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
    /// 单位地址
    /// </summary>
    private string _address;
    public string address
    {
        get { return _address; }
        set { _address = value; }
    }
    /// <summary>
    /// 联系人
    /// </summary>
    private string _lxMan;
    public string lxMan
    {
        get { return _lxMan; }
        set { _lxMan = value; }
    }
    /// <summary>
    /// 手机
    /// </summary>
    private string _lxtel;
    public string lxtel
    {
        get { return _lxtel; }
        set { _lxtel = value; }
    }
    /// <summary>
    /// 邮箱
    /// </summary>
    private string _lxEmail;
    public string lxEmail
    {
        get { return _lxEmail; }
        set { _lxEmail = value; }
    }
    /// <summary>
    /// 监测目的
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
    /// 监测方式
    /// </summary>
    private string _Mode;
    public string Mode
    {
        get { return _Mode; }
        set { _Mode = value; }
    }
   
    /// <summary>
    /// 任务创建时间
    /// </summary>
    private DateTime _CreateDate;
    public DateTime CreateDate
    {
        get { return _CreateDate; }
        set { _CreateDate = value; }
    }
    /// <summary>
    /// 任务创建人
    /// </summary>
    private string _CreateUser;
    public string CreateUser
    {
        get { return _CreateUser; }
        set { _CreateUser = value; }
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
     /// <summary>
    /// 负责人
    /// </summary>
    private string _chargeman;
    public string chargeman
    {
        get { return _chargeman; }
        set { _chargeman = value; }
    }
    /// <summary>
    /// 状态
    /// </summary>
    private double _StatusID;
    public double StatusID
    {
        get { return _StatusID; }
        set { _StatusID = value; }
    }

    /// <summary>
    /// 初审结果
    /// </summary>
    private int _csflag;
    public int csflag
    {
        get { return _csflag; }
        set { _csflag = value; }
    }

    /// <summary>
    /// 踏勘结果
    /// </summary>
    private int _tkflag;
    public int tkflag
    {
        get { return _tkflag; }
        set { _tkflag = value; }
    }

    /// <summary>
    /// 报告编号
    /// </summary>
    private string _ReportNO;
    public string ReportNO
    {
        get { return _ReportNO; }
        set { _ReportNO = value; }
    }
    /// <summary>
    /// 是否需要踏勘
    /// </summary>
    private string _hanwether;
    public string hanwether
    {
        get { return _hanwether; }
        set { _hanwether = value; }
    }
    public List<string> ItemList = new List<string>();
 public   List<Sample> SampleList = new List<Sample>();
}
}