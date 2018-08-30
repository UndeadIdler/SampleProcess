using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class depart
{
   // 单位全称;单位详细地址;法定代表人;单位法人代码
     private string _departid;
    public string departid//企业id
    {
        get { return _departid; }
        set { _departid = value; }
    }
     private string _departname;
    public string departname//单位全称
    {
        get { return _departname; }
        set { _departname = value; }
    }
    private string _address;
     public string address//单位详细地址
    {
        get { return _address; }
        set { _address = value; }
    }
    
     private string _fddb;
     public string fddb//法定代表人
    {
        get { return _fddb; }
        set { _fddb = value; }
    }

     private string _frdm;
     public string frdm//单位法人代码
    {
        get { return _frdm; }
        set { _frdm = value; }
    }
     private string _remark;
     public string remark//下次换证时间
     {
         get { return _remark; }
         set { _remark = value; }
     }
     private string _remarkall;
     public string remarkall//下次换证时间
     {
         get { return _remarkall; }
         set { _remarkall = value; }
     }
     private string _sskjd;
     public string ssjd//所属街道
     {
         get { return _sskjd; }
         set { _sskjd = value; }
     }


     private string _czhm;
     public string czhm//传真号码
     {
         get { return _czhm; }
         set { _czhm = value; }
     }

     private string _yzbm;
     public string yzbm//邮政编码
     {
         get { return _yzbm; }
         set { _yzbm = value; }
     }

     private string _dzyx;
     public string dzyx//电子邮箱
     {
         get { return _dzyx; }
         set { _dzyx = value; }
     }

     private string _sczt;
     public string sczt//生产状态
     {
         get { return _sczt; }
         set { _sczt = value; }
     }
     private string _hy;
     public string hy//生产状态
     {
         get { return _hy; }
         set { _hy = value; }
     }

     private string _frdh;
     public string frdh//电话
     {
         get { return _frdh; }
         set { _frdh = value; }
     }
     private string _frsj;
     public string frsj//手机
     {
         get { return _frsj; }
         set { _frsj = value; }
     }
     private string _frsfw;
     public string frsfw//市府网
     {
         get { return _frsfw; }
         set { _frsfw = value; }
     }
     private string _fgr;
     public string fgr//分管人
     {
         get { return _fgr; }
         set { _fgr = value; }
     }
     private string _fgdh;
     public string fgdh//电话
     {
         get { return _fgdh; }
         set { _fgdh = value; }
     }
     private string _fgsj;
     public string fgsj//手机
     {
         get { return _fgsj; }
         set { _fgsj = value; }
     }
     private string _fgsfw;
     public string fgsfw//市府网
     {
         get { return _fgsfw; }
         set { _fgsfw = value; }
     }

     private string _fzr;
     public string fzr//分管人
     {
         get { return _fzr; }
         set { _fzr = value; }
     }
     private string _fzrdh;
     public string fzdh//电话
     {
         get { return _fzrdh; }
         set { _fzrdh = value; }
     }
     private string _fzrsj;
     public string fzrsj//手机
     {
         get { return _fzrsj; }
         set { _fzrsj = value; }
     }
     private string _fzrsfw;
     public string fzrsfw//市府网
     {
         get { return _fzrsfw; }
         set { _fzrsfw = value; }
     }

     private string _wsrw;
     public string wsrw//行业
     {
         get { return _wsrw; }
         set { _wsrw = value; }
     }
     private string _rwsj;
     public string rwsj//行业
     {
         get { return _rwsj; }
         set { _rwsj = value; }
     }
     private string _jzgr;
     public string jzgr//行业
     {
         get { return _jzgr; }
         set { _jzgr = value; }
     }
     private string _grsj;
     public string grsj//行业
     {
         get { return _grsj; }
         set { _grsj = value; }
     }
     private string _yyzzfw;
     public string yyzzfw//行业
     {
         get { return _yyzzfw; }
         set { _yyzzfw = value; }
     }
     private string _jncp;
     public string jncp//行业
     {
         get { return _jncp; }
         set { _jncp = value; }
     }
     private string _jcsj;
     public string jcsj//行业
     {
         get { return _jcsj; }
         set { _jcsj = value; }
     }
     private string _cym;
     public string cym//行业
     {
         get { return _cym; }
         set { _cym = value; }
     }
    

    public departoneinfectant [] infectantlist;
    public jyobject jy;
    public depart()
    {
        address = "";
        cym = "";
        jncp = "";
        jcsj = "";
        yyzzfw = "";
        fgr = "";

        fgdh = "";
        fgsj = "";
        fgsfw = "";
        fzr = "";
        fzdh = "";
        fzrsfw = "";
        fzrsj = "";
        fddb = "";
        frdh = "";
        frsj = "";
        frsfw = "";
        yzbm = "";
        czhm="";
        dzyx = "";
        sczt = "";



      //infectantlist=new departoneinfectant[p];
    }
}

public class departoneinfectant
{
    private string _departid;
    public string departid//企业id
    {
        get { return _departid; }
        set { _departid = value; }
    }
    private string _infectant;
    public string infectant//污染物
    {
        get { return _infectant; }
        set { _infectant = value; }
    }
    private double _pwl;
    public double pwl//排污量
    {
        get { return _pwl; }
        set { _pwl = value; }
    }
    private double _pwnd;
    public double pwnd//排污浓度
    {
        get { return _pwnd; }
        set { _pwnd = value; }
    }
    private double _flpf;
    public double flpf//废水排放量
    {
        get { return _flpf; }
        set { _flpf = value; }
    }
    //public jyobject detaillist;
    public departoneinfectant()
    {
       //detaillist =new Detailobject[q];
    }
}
/// <summary>
///jyobject 的摘要说明
/// </summary>
public class jyobject
{
    public Detailobject[] jyobj;
    public Detailobject[] buyobj;
    public Detailobject[] saleobj;


}


/// <summary>
///jyobject 的摘要说明
/// </summary>
public class Detailobject
{
    private string _jytype;
    public string jytype//交易类型
    {
        get { return _jytype; }
        set { _jytype = value; }
    }
    private string _jytypeid;
    public string jytypeid//交易类型
    {
        get { return _jytypeid; }
        set { _jytypeid = value; }
    }
    private string _classtype;
    public string classtype//污染物种类
    {
        get { return _classtype; }
        set { _classtype = value; }
    }
    private string _classname;
    public string classname//污染物种类
    {
        get { return _classname; }
        set { _classname = value; }
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
    private string _jydate;
    public string jydate//交易时间
    {
        get { return _jydate; }
        set { _jydate = value; }
    }
    private string _ht;
    public string ht//合同
    {
        get { return _ht; }
        set { _ht = value; }
    }
    public Detailobject()
    {

    }
}
