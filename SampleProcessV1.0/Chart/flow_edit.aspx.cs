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


public partial class flow_edit : System.Web.UI.Page,System.Web.UI.ICallbackEventHandler
{
    private string strCondition
    {
        get { return (string)ViewState["strCondition"]; }
        set { ViewState["strCondition"] = value; }
    }
    private string strPointId
    {
        get { return (string)ViewState["strPointId"]; }
        set { ViewState["strPointId"] = value; }
    }
    string[][] arrstrInfo;
    int[][] LineArr;
    public int intPointSum;
    public int intLineSum;
    string strIsOK;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initalDropdownList();
            if (Request.QueryString["PointId"] != null)
            {
                strPointId = Request.QueryString["PointId"];
                drop_List.SelectedValue = strPointId;
               
            }
        }

        strCondition = " where t_第五级信息.上级id=t_第四级信息.id and t_设备信息.设备id=t_第五级信息.设备id and t_第四级信息.id='" + drop_List.SelectedValue + "'";
        //if (Request.Cookies["Cookies"].Values["FirstScale"] != null && Request.Cookies["Cookies"].Values["SecondScale"] != null && Request.Cookies["Cookies"].Values["ThirdScale"] != null)//单位级用户
        //{
        //    strCondition += "and t_第一级信息.id='" + Request.Cookies["Cookies"].Values["FirstScale"].ToString() + "' and t_第二级信息.id='" + Request.Cookies["Cookies"].Values["SecondScale"].ToString() + "' and t_第三级信息.id='" + Request.Cookies["Cookies"].Values["ThirdScale"].ToString() + "'";
        //}
        //else
        //{
        //    if (Request.Cookies["Cookies"].Values["FirstScale"] != null && Request.Cookies["Cookies"].Values["SecondScale"] != null)//县级用户
        //    {
        //        strCondition += "and t_第一级信息.id='" + Request.Cookies["Cookies"].Values["FirstScale"].ToString() + "' and t_第二级信息.id='" + Request.Cookies["Cookies"].Values["SecondScale"].ToString() + "'";
        //    }
        //    else
        //    {
        //        if (Request.Cookies["Cookies"].Values["FirstScale"] != null)//市级用户
        //        {
        //            strCondition += "and t_第一级信息.id='" + Request.Cookies["Cookies"].Values["FirstScale"].ToString() + "'";
        //        }
        //    }
        //}
        InitArray();//初始化数组
      
       
        //画图
        vmlDrawVml();
        Response.Write("<br />");
        Response.Write("<br />");
        Response.Write("<br />");
        vmlDrawGroup_start(1000, 450, 10000, 6000, true);
        DrawPoint();
        //DrawLine();
        vmlDrawBackGround(10000, 6000, "..\\Images\\cc.jpg");
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
       protected void btn_search_Click(object sender, EventArgs e)
       {
           //this.strPointId = drop_List.SelectedValue;
           //Response.Write("<script language='javascript'>window.location.href='../processflow/flow_edit.aspx?point_Id=" + drop_List.SelectedValue + "';</script>");
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
        string strVml = "<v:group ID=\"group1\" style=\"position:relative;left:0px;top:0px;WIDTH:" + iWidth + ";Height:" + iHeight +
           "pt;\" coordsize = \"" + cWidth + "," + cHeight + "\">\r\n";
        Response.Write(strVml);
        if (isRect)
        {
            strVml = "<v:rect style=\"position:relative;WIDTH:" + cWidth + ";HEIGHT:" + cHeight + "\" fillcolor=\"white\" strokecolor=\"black\" >\r\n";
            Response.Write(strVml);
            //Response.Write("<v:shadow on=\"t\" type=\"single\" color=\"silver\" offset=\"5pt,5pt\"></v:shadow>\r\n");
            Response.Write("</v:Rect>\r\n");
        }
    }
    private void vmlDrawGroup_end()
    {
        Response.Write("</v:group>\r\n");
    }
    private void vmlDrawBackGround(int cWidth, int cHeight, string url)
    {
        Response.Write("<v:Image id=\"img1\" style=\"Z-INDEX:3003;LEFT:0;WIDTH:" + cWidth + ";POSITION:absolute;TOP:0;HEIGHT:" + cHeight + ";\" src=\"" + url + "\" bilevel=\"f\"/>");
    }
    private void vmlDrawNewPoint(int intPx, int intPy, int intPw, int intPh, string strUrl, string strTitle, string strName,int i)
    {
        Response.Write("<v:Image id=p_" + i + " title=\"" + strTitle + "\" style=\"Z-INDEX:3009;LEFT:" + intPx + ";WIDTH:" + intPw + ";POSITION:absolute;TOP:" + intPy + ";HEIGHT:" + intPh * 1024 / 768 + ";  cursor:hand;\" onmousedown=\"mousedown(this);\" onmouseup=\"mouseup();\"  src=\"" + strUrl + "\" bilevel=\"f\"/>\r\n");
        Response.Write("<v:roundrect id=p_" + i + "_t strokeweight=0; stroked=\"t\" style=\"position:relative;left:" + (intPx - 200) + ";top:" + (intPy - 300 * 768 / 1024) + ";width:1000;height:1;z-index:3012\">\r\n");
        Response.Write("<v:stroke opacity=\"6553f\"/>");
        Response.Write("<v:textbox style=\"font-size:10pt;color:#45dffb;line-height:18px;TEXT-ALIGN:center\" inset=\"1,1,1,1\">" + strName + "</v:textbox>\r\n");
        Response.Write("</v:roundrect>\r\n");
    }

    //private void vmlDrawNewLine(int intPx, int intPy, int intPw, int intPh, string strUrl, string strTitle, string strName, int i)
    //{
    //    Response.Write("<v:Image id=l_" + i + " style=\"Z-INDEX:3009;LEFT:" + intPx + ";WIDTH:" + intPw + ";POSITION:absolute;TOP:" + intPy + ";HEIGHT:" + intPh * 1024 / 768 + ";  cursor:hand;\" onmousedown=\"mousedown(this);\" onmouseup=\"mouseup();\"  src=\"" + strUrl + "\" bilevel=\"f\"/>\r\n");
    //    Response.Write("<v:roundrect id=l_" + i +"_t strokecolor=\"white\"  strokeweight=0; stroked=\"t\" style=\"position:relative;left:" + intPx + ";top:" + (intPy - 300 * 768 / 1024) + ";width:800;height:1;z-index:3012\">\r\n");
    //    Response.Write("<v:stroke opacity=\"6553f\"/>");
    //    Response.Write("<v:textbox style=\"font-size:10pt;color:black;line-height:18px;TEXT-ALIGN:center\" inset=\"1,1,1,1\">" + strName + "</v:textbox>\r\n");
    //    Response.Write("</v:roundrect>\r\n");
    //}
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
            strTitle =  strName;
            sTrimage = arrstrInfo[i][6].ToString();
            vmlDrawNewPoint(Int32.Parse(arrstrInfo[i][2]), Int32.Parse(arrstrInfo[i][3]), Int32.Parse(arrstrInfo[i][4]), Int32.Parse(arrstrInfo[i][5]), sTrimage, strTitle, strName, i);
        }
        
    }
    
    private void InitArray()//初始化数组，包括读取数据库信息和从前置机取数
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

    #region ICallbackEventHandler 成员

    string ICallbackEventHandler.GetCallbackResult()
    {
        return strIsOK;
    }

    void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
    {
        string strSql,strTmp;
        strTmp = eventArgument;
        char[] sign ={ ',' };
        string[] arrstrPosition = strTmp.Split(sign);
        int j = 0;
        for (int i = 0; i < intPointSum; i++)
        {
            arrstrInfo[i][2] = arrstrPosition[j];
            arrstrInfo[i][3] = arrstrPosition[j + 1];
            j =j + 2;
        }
        

        for(int i=0;i<intPointSum;i++)
        {
            strSql = "update t_chart_main set x=" + arrstrInfo[i][2] + ",y=" + arrstrInfo[i][3] + " where id='" + arrstrInfo[i][1] + "'";
            MyDataOp mdo = new MyDataOp(strSql);
            mdo.ExecuteCommand();    
           
        }
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

        //int intRow = Convert.ToUInt16(ds.Tables[0].Rows[0][0].ToString());
        //if (intRow == 0)
        //{
        //    Response.Write("<script language='javascript'>alert('您没有权限进入本页！\\n请重新登录或与管理员联系！');parent.location='../login.aspx';</script>");
        //}
    }

    protected void drop_List_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.strPointId = drop_List.SelectedValue;
    }
}
