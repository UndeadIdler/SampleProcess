using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebApp.Components;
public partial class Chart_Index2 : System.Web.UI.Page
{
    string[][] arrstrInfo;
    public string linepoint;
    public int intPointSum;
    public int intLineSum;
    protected void Page_Load(object sender, EventArgs e)
    {

       InitArray();//初始化数组
       vmlDrawVml();
       Response.Write("<br />");
       //Response.Write("<br />");
       //Response.Write("<br />");
       vmlDrawGroup_start(1000, 450, 1000, 600, true);
       DrawPoint();
       DrawLine();
       vmlDrawBackGround(1000, 600, "..\\Images\\cc.jpg");
       vmlDrawGroup_end();
    }
    private void vmlDrawNewLine(string from, string to)
    {
        Response.Write("<v:Line style=\"Z-INDEX:3009;POSITION:absolute;\" strokecolor=\"#45dffb\" strokeWeight=\"1px\"  from=" + from + " to=" + to + ">\r\n");
        Response.Write("<v:fill opacity=\"0.1\"/>");
        Response.Write("</v:line>\r\n");
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
            Response.Write("<v:shadow on=\"t\" type=\"single\" color=\"silver\" offset=\"5pt,5pt\"></v:shadow>\r\n");
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
    private void vmlDrawNewPoint(int intPx, int intPy, int intPw, int intPh, string strUrl, string strTitle, string strName, int i,string Url)
    {
        Response.Write("<v:Image id=p_" + i + " title=\"" + strTitle + "\" style=\"Z-INDEX:3009;LEFT:" + intPx + ";WIDTH:" + intPw + ";POSITION:absolute;TOP:" + intPy + ";HEIGHT:" + intPh * 1024 / 768 + ";  cursor:hand;\" onmousedown=\"mousedown(this);\" onmouseup=\"mouseup();\"  src=\"" + strUrl + "\"\" onclick=\"location='"+Url+"'\" bilevel=\"f\"/>");
        Response.Write("<v:roundrect id=p_" + i + "_t strokecolor=\"black\"  strokeweight=0; stroked=\"t\" style=\"position:relative;left:" + (intPx - 20) + ";top:" + (intPy - 30 * 768 / 1024) + ";width:80;height:0;z-index:3012\">\r\n");
        Response.Write("<v:stroke opacity=\"cae7f9\"/>");
        Response.Write("<v:textbox style=\"font-size:10pt;color:#0000;line-height:18px;TEXT-ALIGN:center\" inset=\"1,1,1,1\">" + strName + "</v:textbox>\r\n");
        Response.Write("</v:roundrect>\r\n");
    }
    #endregion
    private void DrawPoint()//根据不同情况画点
    {
        string strTitle = "";
        string sTrimage;
        string strName = "";



        for (int i = 0; i < intPointSum; i++)
        {
            strName = arrstrInfo[i][0].ToString();
            strTitle = strName+"\r\n" ;
            sTrimage = arrstrInfo[i][6].ToString();

            vmlDrawNewPoint(Int32.Parse(arrstrInfo[i][2])/10 , Int32.Parse(arrstrInfo[i][3])/10, Int32.Parse(arrstrInfo[i][4])/10, Int32.Parse(arrstrInfo[i][5])/10 , sTrimage, strTitle, strName, i, arrstrInfo[i][7].ToString());
        }

    }
    private void InitArray()//初始化数组，包括读取数据库信息
    {


        string strSql = "select t_chart_main.* from t_chart_main where type=2 order by id";
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
                arrstrInfo[i][7] = ds.Tables[0].Rows[i]["url"].ToString();


            }
        }
        catch
        { }
    }
    private void DrawLine()//根据不同情况画线
    {
        string strTmp = "";


        string strSql = "select t_chart_type.id,line from t_chart_type where id=2";
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
}
