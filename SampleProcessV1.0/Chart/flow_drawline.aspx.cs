using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebApp.Components;


public partial class flow_drawline : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
{
    private string strCondition
    {
        get { return (string)ViewState["strCondition"]; }
        set { ViewState["strCondition"] = value; }
    }
    private string strNum
    {
        get { return (string)ViewState["strNum"]; }
        set { ViewState["strNum"] = value; }
    }
    private string strDept
    {
        get { return (string)ViewState["strDept"]; }
        set { ViewState["strDept"] = value; }
    }
    public string strPointId
    {
        get { return (string)ViewState["strPointId"]; }
        set { ViewState["strPointId"] = value; }
    }
    public string linepoint;
    string[][] arrstrInfo;
    public int intPointSum;
    public int intLineSum;
    string strIsOK;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           initalDropdownList();
      
       }
        strPointId = drop_List.SelectedValue;
            InitArray();//初始化数组
            //画图
            vmlDrawVml();
            Response.Write("<br />");
            Response.Write("<br />");
            Response.Write("<br />");
            vmlDrawGroup_start(1000,600, 1000, 600, true);
            DrawPoint();
            DrawLine();
            vmlDrawBackGround(1000, 600, "..\\Images\\cc.jpg");
            vmlDrawGroup_end();
        

    }
    private void initalDropdownList()
    {
        string strSql = "select t_chart_type.id,charttype from t_chart_type";
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
        drop_List.DataSource = ds;
        drop_List.DataValueField = "id";
        drop_List.DataTextField = "charttype";
        drop_List.DataBind();
    }
    protected void drop_List_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.strPointId = drop_List.SelectedValue;
    }
   
    

    #region 画图相关函数
    private void vmlDrawVml()
    {
        Response.Write("<HTML xmlns:v>\r\n");
        Response.Write("<HEAD>\r\n");
        Response.Write("<STYLE>\r\n");
        Response.Write("v\\:*{behavior:url(#default#VML);}\r\n");
        Response.Write("</STYLE>\r\n");
        Response.Write("</HEAD>\r\n");
    }
    private void vmlDrawGroup_start(int iWidth, int iHeight, int cWidth, int cHeight, bool isRect)
    {
        string strVmll=" <v:group id=\"toolbar\" coordorigin=\"0,0\" coordsize=\"300,300\" style=\"width:300px;height:30px;position:relative;\" onclick=\"selectShape()\">\r\n";
  strVmll+="<v:roundRect style=\"position:relative;width:300px;height:300px;\">\r\n";
      strVmll+="<v:stroke color=\"black\" weight=\"2px\" arcsize=\"1\"/>\r\n";
      strVmll += "<v:fill color=\"black\" opacity=\"0.5\"/>\r\n";
   strVmll+="</v:roundRect>\r\n";
   
   strVmll+="<v:line from=\"20,50\" to=\"20,250\" style=\"position:relative;\" strokeColor=\"red\" strokeWeight=\"2px\" />\r\n";

 strVmll+=" </v:group>\r\n";
 Response.Write(strVmll);
        string strVml = "<v:group ID=\"map\" style=\"position:relative;left:0px;top:0px;WIDTH:" + iWidth + ";Height:" + iHeight +
           "px;\" coordsize = \"" + cWidth + "," + cHeight + "\">\r\n";
      //strVml+= "<v:roundRect style=\"position:relative;width:1000px;height:600px;z-index:2\"";
      //strVml += " strokeColor=\"#000000\" strokeweight=\"2px\" arcsize=\"0.02\">\r\n";
      //strVml += " <v:fill color=\"#000000\" opacity=\"0.8\"/>\r\n";
      //strVml += "</v:roundRect>\r\n";
       
        //<v:group id="map" coordorigin="0,0" coordsize="800,500" style="width:800px;height:500px;position:relative;">
        if (isRect)
        {
            //<!-- 线 -->
 strVml+="<v:line id=\"line\" from=\"0,0\" to=\"20,250\" style=\"position:relative;display:none;\">\r\n";
     strVml+="<v:stroke opacity=\"0.5\" Color=\"red\" Weight=\"2px\"/>\r\n";
  strVml+="</v:line>\r\n";

        }
             Response.Write(strVml);
    }
    private void vmlDrawGroup_end()
    {
        Response.Write("</v:group>\r\n");
    }
    private void vmlDrawBackGround(int cWidth, int cHeight, string url)
    {
      Response.Write("<v:Image id=\"img1\" style=\"Z-INDEX:-1;LEFT:0;WIDTH:" + cWidth + ";POSITION:absolute;TOP:0;HEIGHT:" + cHeight + ";\" src=\"" + url + "\" bilevel=\"f\"/>");
    }
    private void vmlDrawNewPoint(int intPx, int intPy, int intPw, int intPh, string strUrl, string strTitle, string strName,int i)
    {
        Response.Write("<v:Image id=p_" + i + " title=\"" + strTitle + "\" style=\"Z-INDEX:1;LEFT:" + intPx + ";WIDTH:" + intPw + ";POSITION:absolute;TOP:" + intPy + ";HEIGHT:" + intPh * 1024 / 768 + ";  cursor:hand;\" src=\"" + strUrl + "\" bilevel=\"f\"/>\r\n");
        //Response.Write("<v:roundrect id=p_" + i + "_t strokecolor=\"black\"  strokeweight=0; stroked=\"t\" style=\"position:relative;left:" + (intPx-200) + ";top:" + (intPy - 300 * 768 / 1024) + ";width:800;height:0;z-index:3012\">\r\n");
      // Response.Write("<v:stroke opacity=\"6553f\"/>");
        //Response.Write("<v:textbox style=\"font-size:10pt;color:#45dffb;line-height:18px;TEXT-ALIGN:center\" inset=\"1,1,1,1\">" + strName + "</v:textbox>\r\n");
        //Response.Write("</v:roundrect>\r\n");
    }
     private void vmlDrawNewLine(string from,string to)
    {
        Response.Write("<v:Line style=\"Z-INDEX:1;POSITION:absolute;\" strokecolor=\"#45dffb\" strokeWeight=\"1px\"  from=" + from + " to=" + to + ">\r\n");
         Response.Write("<v:fill opacity=\"0.1\"/>");
         Response.Write("</v:line>\r\n");
       
    }
     
    #endregion
    #region ICallbackEventHandler 成员

    string ICallbackEventHandler.GetCallbackResult()
    {
        return strIsOK;
    }

    void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
    {
        string strSql, strTmp;
        strTmp = eventArgument;




        strSql = "update  t_chart_type set line='" + linepoint + strTmp + "' where id=" + drop_List.SelectedValue + "";
            MyDataOp mdo = new MyDataOp(strSql);
            mdo.ExecuteCommand();

        
    }

    #endregion
    #region 获得和组织数据函数

    private void DrawPoint()//根据不同情况画点
    {
        string strTitle = "";
        string sTrimage;
        string strName = "";



        for (int i = 0; i < intPointSum; i++)
        {
            strName = arrstrInfo[i][0].ToString();
            strTitle = strName;
            sTrimage = arrstrInfo[i][6].ToString();

            vmlDrawNewPoint(Int32.Parse(arrstrInfo[i][2]) / 10, Int32.Parse(arrstrInfo[i][3]) / 10, Int32.Parse(arrstrInfo[i][4]) / 10, Int32.Parse(arrstrInfo[i][5]) / 10, sTrimage, strTitle, strName, i);
        }
        
    }
    private void DrawLine()//根据不同情况画线
    {
        string strTmp = "";


        string strSql = "select t_chart_type.id,line from t_chart_type where id="+drop_List.SelectedValue+"";
        DataSet ds;
        ds = new MyDataOp(strSql).CreateDataSet();
        if (ds.Tables[0].Rows[0]["line"] != null && ds.Tables[0].Rows[0]["line"].ToString() != "")
        {
            strTmp = ds.Tables[0].Rows[0]["line"].ToString();
        }
        linepoint = strTmp;
        char[] sign = { ';' };
        string[] arrstrPosition = strTmp.Split(sign);
        for (int i = 0; i < arrstrPosition.Length; i++)
        {
            char[] sign2 = { ',' };
            string[] arrstrPosition2 = arrstrPosition[i].Split(sign2);

            for (int j = 0; j < 3 && arrstrPosition2.Length > 1; )
            {
                string from = arrstrPosition2[j] + "," + arrstrPosition2[j + 1];
                string to = arrstrPosition2[j + 2] + "," + arrstrPosition2[j + 3];
                vmlDrawNewLine(from, to);
                j = j + 3;
            }
        }

    }
   
    private void InitArray()//初始化数组，包括读取数据库信息
    {
        string strSql = "select t_chart_main.* from t_chart_main where type="+drop_List.SelectedValue+" order by id";
        DataSet ds;
        try
        {

            //读取该单位的流程图的设备信息
            ds = new MyDataOp(strSql).CreateDataSet();
            intPointSum = ds.Tables[0].Rows.Count;
            arrstrInfo = new string[intPointSum][];
            for (int i = 0; i < intPointSum; i++)
            {
                arrstrInfo[i] = new string[11];

                arrstrInfo[i][0] = ds.Tables[0].Rows[i]["name"].ToString();
                arrstrInfo[i][1] = ds.Tables[0].Rows[i]["id"].ToString();
                arrstrInfo[i][2] = ds.Tables[0].Rows[i]["x"].ToString();
                arrstrInfo[i][3] = ds.Tables[0].Rows[i]["y"].ToString();

                arrstrInfo[i][4] = ds.Tables[0].Rows[i]["h"].ToString();
                arrstrInfo[i][5] = ds.Tables[0].Rows[i]["w"].ToString();
                arrstrInfo[i][6] = ds.Tables[0].Rows[i]["address"].ToString();


            }
        }
        catch
        { }
    }

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        //if (Request.Cookies["Cookies"].Values["u_id"] == null)
        //{
        //    Response.Write("<script language='javascript'>alert('您没有权限进入本页或当前登录用户已过期！\\n请重新登录或与管理员联系！');parent.location='../login.aspx';</script>");
        //}

        //string strPageName = Request.Url.AbsolutePath;
        //strPageName = strPageName.Substring(strPageName.LastIndexOf("/") + 1);

        //string strSql = "select count(*) from t_角色信息,t_角色菜单关系表,t_菜单项管理 " +
        //    "where t_角色信息.id='" + Request.Cookies["Cookies"].Values["u_role"] +
        //    "' and t_菜单项管理.相关文件 like '%" + strPageName +
        //    "%' and t_角色信息.id=t_角色菜单关系表.角色id and t_角色菜单关系表.菜单id=t_菜单项管理.id";
        //MyDataOp mdo = new MyDataOp(strSql);
        //DataSet ds = mdo.CreateDataSet();

        //int introw = Convert.ToUInt16(ds.Tables[0].Rows[0][0].ToString());
        //if (introw == 0)
        //{
        //    Response.Write("<script language='javascript'>alert('您没有权限进入本页！\\n请重新登录或与管理员联系！');parent.location='../login.aspx';</script>");
        //}
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
    
    }

    
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string strSql, strTmp;
        strTmp = "";
        strSql = "update t_chart_type set line='" + strTmp + "' where id=" + drop_List.SelectedValue+ "";
        MyDataOp mdo = new MyDataOp(strSql);
        if (mdo.ExecuteCommand())
        {
            btn_search_Click(null, null);
            Response.Write("<script language='javascript'>alert('删除成功！');</script>");
        }
    }
}
