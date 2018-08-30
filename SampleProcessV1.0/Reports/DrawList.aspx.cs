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

public partial class Reports_DrawList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txt_StartTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_EndTime.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_StartTime.Text = DateTime.Now.Date.ToString("yyyy-MM-01");
            txt_EndTime.Text = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");

            Query();
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #3333FF;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>样品流转记录单</b></font>";

        }
    }
   public void Query()
    {
       string str="";
       DateTime s=DateTime.Parse(DateTime.Now.ToString());
       DateTime end = DateTime.Parse(DateTime.Now.ToString());
        if (txt_StartTime.Text.Trim() != "")
        {
            s=DateTime.Parse(txt_StartTime.Text.Trim()+" 0:00:00");
            str += " and  CreateDate >='" + s + "'";
        }
       if( txt_EndTime.Text.Trim() != "")
       {
           end = DateTime.Parse(txt_EndTime.Text.Trim() + " 23:59:59");
           str += " and  CreateDate <='" + end + "'";
       }
       if (txt_sampleID.Text.Trim() != "")
       {
           str += " and  SampleID like '%" + txt_sampleID.Text.Trim() + "%'";
       }
       
        //将统计数据加入到DataSet中
       string strData = "Select SampleID 样品编号,CreateDate 领用时间,ItemList 分析项目,returndate 还样时间,LyUserID 分析人 from t_DrawSample inner join t_R_UserInfo on t_R_UserInfo.UserID=LyUserID where [type]=0  and yxflag=0  " + str + " order by CreateDate desc ";
        DataSet ds_new = new MyDataOp(strData).CreateDataSet();
       
          
        if (ds_new.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds_new.Tables[0].Rows.Add(ds_new.Tables[0].NewRow());
            grdvw_List.DataSource = ds_new;
            grdvw_List.DataBind();
            int intColumnCount = grdvw_List.Rows[0].Cells.Count;
            grdvw_List.Rows[0].Cells.Clear();
            grdvw_List.Rows[0].Cells.Add(new TableCell());
            grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_List.DataSource = ds_new;
            grdvw_List.DataBind();
        }
            

    }

    //导出成Word文件 
  protected void btnToWord_Click(object sender, EventArgs e)
   {
       Response.Clear();
       Response.BufferOutput = true;
       //设定输出的字符集 
       Response.Charset = "GB2312";
       //假定导出的文件名为FileName.doc 
       Response.AppendHeader("Content-Disposition", "attachment;filename=样品流转记录单.doc");
       Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
       ////设置导出文件的格式 
       Response.ContentType = "application/ms-word";
       //关闭ViewState 
       grdvw_List.EnableViewState = false;
       System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ZH-CN", true);
       System.IO.StringWriter stringWriter = new System.IO.StringWriter(cultureInfo);
       System.Web.UI.HtmlTextWriter textWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
       grdvw_List.RenderControl(textWriter);
       // //把HTML写回浏览器 
       Response.Write(stringWriter.ToString());
       Response.End();

   }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
          
           }
      
           

        }
    protected void btn_CreateReport_Click(object sender, EventArgs e)
    {
        Query();
    }

   
}
