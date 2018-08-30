using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class business
{
  
    private string _jyid;
    public string jyid//交易id
    {
        get { return _jyid; }
        set { _jyid = value; }
    }
    private string _htid;
    public string htid//合同编号
    {
        get { return _htid; }
        set { _htid = value; }
    }
    private string _jyhj;
    public string jyhj//交易合计
    {
        get { return _jyhj; }
        set { _jyhj = value; }
    }
    private string _sxf;
    public string sxf//手续费
    {
        get { return _sxf; }
        set { _sxf = value; }
    }
    private string _hj;
    public string hj//合计
    {
        get { return _hj; }
        set { _hj = value; }
    }
    private string _jydate;
    public string jydate//交易时间
    {
        get { return _jydate; }
        set { _jydate = value; }
    }

    private string _jytype;
    public string jytype//交易类型
    {
        get { return _jytype; }
        set { _jytype = value; }
    }
    public jybakobject[] jy;
    public business()
    {
     
    }
}

/// <summary>
///jyobject 的摘要说明
/// </summary>
public class jybakobject
{
   
    private string _classtype;
    public string classtype//污染物种类
    {
        get { return _classtype; }
        set { _classtype = value; }
    }
   
    private double _jyl;
    public double jyl//交易量
    {
        get { return _jyl; }
        set { _jyl = value; }
    }
    private double _jyf;
    public double jyf//交易费
    {
        get { return _jyf; }
        set { _jyf = value; }
    }
    private double _real;
    public double real//实际购得指标
    {
        get { return _real; }
        set { _real = value; }
    }
    private double _xk;
    public double xk//许可
    {
        get { return _xk; }
        set { _xk = value; }
    }
    private double _fsxk;
    public double fsxk//许可
    {
        get { return _fsxk; }
        set { _fsxk = value; }
    }
    private double _fs;
    public double fs//许可
    {
        get { return _fs; }
        set { _fs = value; }
    }
    private string _date;
    public string date//排污权有效期
    {
        get { return _date; }
        set { _date = value; }
    }

    public jybakobject()
    {

    }
}
