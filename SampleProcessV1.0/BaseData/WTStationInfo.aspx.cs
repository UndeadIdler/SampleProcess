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
using OWC11;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using WebApp.Components;
using System.IO;
using System.Drawing.Printing;


public partial class BaseData_WTStationInfo : System.Web.UI.Page
{

    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string Editid//所选择操作列记录对应的id
    {
        get { return (string)ViewState["Editid"]; }
        set { ViewState["Editid"] = value; }
    }
    private bool Editflag//所选择操作列记录对应的id
    {
        get { return (bool)ViewState["Editflag"]; }
        set { ViewState["Editflag"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            #region 初始化页面元素           
          
            grdvw_List.Caption = "<FONT style='WIDTH: 102.16%; COLOR: #50598d;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>委托单位信息</b></font>";
            initial();
                 
            SetButton();            
            #endregion    
            Query();
        }
    }

    private void initial()
    {
        MyStaVoid.BindList("des", "id", "select rtrim(ClientName) des,id from t_M_ClientInfo", drop_area_query);
        //MyStaVoid.BindList("des", "id", "select rtrim(des) des,id from t_生产状态", drop_status);
        //MyStaVoid.BindList("des", "id", "select rtrim(des) des,id from t_行业信息", drop_industry);
       ListItem listsd = new ListItem("所有", "0");
        //drp_sd.Items.Add(listsd);
        //drop_status.Items.Add(listsd);
       drop_area_query.Items.Add(listsd);
       drop_area_query.SelectedIndex = drop_area_query.Items.Count - 1;
        
    }
    private void Query()
    {
         //string strSql = "select t_委托单位.id,单位全称,单位详细地址,邮政编码,单位曾用名全称 from t_委托单位";

        string strSql = "select t_委托单位.id,单位全称,单位详细地址,单位法人代码,所属镇街道,邮政编码,法定代表人,tel1 电话,mobile1 手机,环保负责人, tel3 联系电话,单位曾用名全称 from t_委托单位 where 1=1   ";

        
        string strCondition="";

        if (drop_area_query.SelectedValue.ToString()!="0")
            strCondition += "and (所属镇街道 = '" + drop_area_query.SelectedValue.ToString() + "')";                  


            if (txt_StationName_forSearch.Text != "")
        {
            strCondition += "and (单位全称 like '%" + txt_StationName_forSearch.Text.Trim() + "%' or 单位曾用名全称 like '%" + txt_StationName_forSearch.Text.Trim() + "%')";                  

            //strCondition += "and (单位全称 like '%" + txt_StationName_forSearch.Text.Trim() + "%'";                  
        }
        strCondition += " order by t_委托单位.单位全称";
      

        strSql += strCondition;
        DataSet ds = new MyDataOp(strSql).CreateDataSet();
       
           
        
        if (ds.Tables[0].Rows.Count == 0)
        {
            //没有记录仍保留表头
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
            int intColumnCount = grdvw_List.Rows[0].Cells.Count;
            grdvw_List.Rows[0].Cells.Clear();
            grdvw_List.Rows[0].Cells.Add(new TableCell());
            grdvw_List.Rows[0].Cells[0].ColumnSpan = intColumnCount;
        }
        else
        {
            grdvw_List.DataSource = ds;
            grdvw_List.DataBind();
        }
        
    }

    #region GridView相关事件函数

    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          
            //修改字段标题
            //e.Row.Cells[2].Text = MyStaVoid.getScaleName(4) + "编码";
            //e.Row.Cells[3].Text = MyStaVoid.getScaleName(4) + "名称";

            ////添加按钮列的标题
            //TableCell tabcHeaderExport = new TableCell();
            //tabcHeaderExport.Text = "详细";
            //tabcHeaderExport.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //tabcHeaderExport.Width = 60;
            //e.Row.Cells.Add(tabcHeaderExport);

            TableCell tabcHeaderDetail = new TableCell();
            tabcHeaderDetail.Text = "修改";
            tabcHeaderDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDetail.Style.Add("BACKGROUND-IMAGE ", "url(../Images/gr.jpg)"); 
            tabcHeaderDetail.Width = 60;
            e.Row.Cells.Add(tabcHeaderDetail);           

            TableCell tabcHeaderDel = new TableCell();
            tabcHeaderDel.Text = "删除";
            tabcHeaderDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            tabcHeaderDel.Style.Add("BACKGROUND-IMAGE ", "url(../Images/gr.jpg)"); 
            tabcHeaderDel.Width = 30;
            e.Row.Cells.Add(tabcHeaderDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#d7d9e4'");
                       
            int id = this.grdvw_List.PageIndex * this.grdvw_List.PageSize + e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = id.ToString();  

            //手动添加详细和删除按钮
          
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/Images/Edit.gif";           
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);


            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            LinkButton ibtnDel = new LinkButton();
            ibtnDel.Text = "删除";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
            //if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(2, 1) == "1")
            //{
            ibtnDel.Visible = Editflag;
            //}
            //else
            //    ibtnDel.Visible = false;

            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Visible = false;
            //if (drop_type.SelectedValue.ToString().Trim() == "0")
            //{
            //    e.Row.Cells[5].Visible =true;
            //}
            //else
            //    e.Row.Cells[5].Visible =false ;
        }
    }
  
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        Query();
    }
    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;

        string[] strlist = new string[2];
        int i = 0;
        string strSql = "delete from t_委托单位  where id='" + strSelectedId + "'";
       
        strlist.SetValue(strSql,i++);
       MyDataOp mdo = new MyDataOp(strSql);
       bool success = mdo.DoTran(i, strlist);

        if (success)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            Query();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
    }
    
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "new", "showAddEdit();", true);  
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
        Editid = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text;
        InitDropDownList("");
        
        AllTxtCanWrite();
        InitData(Editid);
        btn_print.Visible = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "new", "showAddEdit();", true);  
        SetButton();            
    }
    #endregion
    protected void btn_Query_Click(object sender, EventArgs e)
    {
        Query();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        InitDropDownList("");
        AllTxtClean();
        AllTxtCanWrite();
       // drop_bz.SelectedValue = "0";
        Editid =null;
        btn_print.Visible = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "showAddEdit();", true);  
    }

    private void SetButton()//根据权限设置读写相关的按钮是否可用
    {
        if (!MyStaVoid.isWrite(Request.Cookies["Cookies"].Values["uid"].ToString(), this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString()))
        {
            btn_Add.Visible = false;
            btn_Save.Visible = false;
            Editflag = false;
        }
        else
        {
            Editflag = true;
            btn_Add.Visible = true;
            btn_Save.Visible = true;
        }
    }

    #region 详细页面
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();", true);
        AllTxtCanWrite();
        //Query();
    }
    //    protected void Page_Load(object sender, EventArgs e)
    //{
    //    this.Title = "企业信息";
    //    if (!IsPostBack)
    //    {
    //        #region 初始化页面元素           
          
    //        if (Request.QueryString["edit"] != null)
    //        {
    //            InitDropDownList("1");
    //            Editid = Request.QueryString["edit"].ToString();
    //            InitData(Editid);
    //            txt_qymc.Enabled = false;
    //            btn_Add.Text = "编 辑";
    //            AllTxtreadOnly();
    //            btn_print.Visible = true;

               
    //        }
    //        else
    //        {
    //            InitDropDownList("");
    //            AllTxtCanWrite();
    //            btn_print.Visible = false;
    //        }
    //        txt_createdate.Attributes.Add("OnFocus", "javascript:WdatePicker()");
    //        //txt_wstime.Attributes.Add("OnFocus", "javascript:WdatePicker()");
    //        //txt_grtime.Attributes.Add("OnFocus", "javascript:WdatePicker()");

    //        SetButton();            
    //        #endregion 
    //    }
    //}

    private void InitDropDownList(string id)//清空详细页面中所有的TextBox
    {
        MyStaVoid.BindList("des", "id", "select rtrim(ClientName) des,id from t_M_ClientInfo", drp_sd);
        //MyStaVoid.BindList("des", "id", "select rtrim(des) des,id from t_生产状态", drop_status);
        MyStaVoid.BindList("des", "id", "select rtrim(des) des,id from t_行业信息", drop_industry);
        ListItem listsd = new ListItem("请选择", "0");
        drp_sd.Items.Add(listsd);
      //  drop_status.Items.Add(listsd);
        drop_industry.Items.Add(listsd);
        //MyStaVoid.BindList("bz", "id", "select * from t_标准字典", drop_bz);


        if (id == "")
        {
            drp_sd.SelectedValue = "0";
            //drop_status.SelectedValue = "0";
            drop_industry.SelectedValue = "0";
            //drop_bz.SelectedValue = "7";

        }
        
        
    }
    private void AllTxtClean()
    {
        txt_qymc.Text = "";
        txt_cname.Text = "";
        txt_dz.Text = "";
       
        drp_sd.SelectedIndex = 0;
       
        txt_yzbm.Text = "";
        txt_frdb.Text = "";
        txt_tel1.Text = "";
        txt_mobile1.Text = "";
        txt_zfw1.Text = "";
        txt_hbfg.Text = "";
        txt_tel2.Text = "";
        txt_mobile2.Text = "";
        txt_zfw2.Text = "";
        txt_hbfz.Text = "";
        txt_tel3.Text = "";
        txt_mobile3.Text = "";
        txt_zfw3.Text = "";
        txt_czhm.Text = "";
        txt_email.Text = "";
        txt_frdm.Text = "";
      
        //drop_bz.SelectedValue = "0";
        drop_control.SelectedValue = "0";
    }
    private void InitData(string id)//清空详细页面中所有的TextBox
    {
        string sql = "select * from t_委托单位 where id = " + id;
        DataSet ds = new MyDataOp(sql).CreateDataSet();
        if (ds.Tables[0].Rows.Count == 1)
        {
            txt_qymc.Text = ds.Tables[0].Rows[0]["单位全称"].ToString();
            //txt_pwNO.Text = ds.Tables[0].Rows[0]["排污权证号"].ToString();
            txt_cname.Text = ds.Tables[0].Rows[0]["单位曾用名全称"].ToString();
            txt_dz.Text = ds.Tables[0].Rows[0]["单位详细地址"].ToString();
           
            if (ds.Tables[0].Rows[0]["所属镇街道"].ToString() != "")
            {
                drp_sd.SelectedValue = ds.Tables[0].Rows[0]["所属镇街道"].ToString();
            }
            else
            {
                drp_sd.Items.FindByText("请选择").Selected = true;
            }
            if (ds.Tables[0].Rows[0]["行业类别"].ToString() != "")
            {
                drop_industry.SelectedValue = ds.Tables[0].Rows[0]["行业类别"].ToString();
            }
            //else
            //{
            //    drop_industry.Items.FindByText("请选择").Selected = true;
            //}
          
          
            if (ds.Tables[0].Rows[0]["kzlevel"].ToString() != "")
            {
                drop_control.SelectedValue = ds.Tables[0].Rows[0]["kzlevel"].ToString();
            }
            else
                drop_control.SelectedIndex = drop_control.Items.Count - 1;

            txt_yzbm.Text = ds.Tables[0].Rows[0]["邮政编码"].ToString();
            txt_frdb.Text = ds.Tables[0].Rows[0]["法定代表人"].ToString();
            txt_frdm.Text = ds.Tables[0].Rows[0]["单位法人代码"].ToString();
            txt_czhm.Text = ds.Tables[0].Rows[0]["传真号码"].ToString();
            txt_email.Text = ds.Tables[0].Rows[0]["电子邮箱"].ToString();
            txt_tel1.Text = ds.Tables[0].Rows[0]["tel1"].ToString();
            txt_mobile1.Text = ds.Tables[0].Rows[0]["mobile1"].ToString();
            txt_zfw1.Text = ds.Tables[0].Rows[0]["市府网1"].ToString();
            txt_hbfg.Text = ds.Tables[0].Rows[0]["环保分管人"].ToString();
            txt_tel2.Text = ds.Tables[0].Rows[0]["tel2"].ToString();
            txt_mobile2.Text = ds.Tables[0].Rows[0]["mobile2"].ToString();
            txt_zfw2.Text = ds.Tables[0].Rows[0]["市府网2"].ToString().Trim();
            txt_hbfz.Text = ds.Tables[0].Rows[0]["环保负责人"].ToString().Trim();
            txt_tel3.Text = ds.Tables[0].Rows[0]["tel3"].ToString();
            txt_mobile3.Text = ds.Tables[0].Rows[0]["mobile3"].ToString();
            txt_zfw3.Text = ds.Tables[0].Rows[0]["市府网3"].ToString();
        }
    }
 private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        //txt_pwNO.Enabled = true;
       
       // txt_qymc.Enabled = true;
       // txt_cname.Enabled = true;
       // txt_dz.Enabled = true;
       //// txt_jgdm.Enabled = true;
     
       // txt_yzbm.Enabled = true;
      
       // txt_hbfz.Enabled = true;
       // txt_tel3.Enabled = true;
       // txt_mobile3.Enabled = true;
        //txt_pwNO.Enabled = true;
        drop_industry.Enabled = true;
        //drop_status.Enabled = true;
        txt_qymc.Enabled = true;
        txt_cname.Enabled = true;
        txt_dz.Enabled = true;
        txt_frdm.Enabled = true;
        drp_sd.Enabled = true;
        txt_yzbm.Enabled = true;
        txt_frdb.Enabled = true;
        txt_tel1.Enabled = true;
        txt_mobile1.Enabled = true;
        txt_zfw1.Enabled = true;
        txt_hbfg.Enabled = true;
        txt_tel2.Enabled = true;
        txt_mobile2.Enabled = true;
        txt_zfw2.Enabled = true;
        txt_hbfz.Enabled = true;
        txt_tel3.Enabled = true;
        txt_mobile3.Enabled = true;
        txt_zfw3.Enabled = true;
        txt_czhm.Enabled = true;
        txt_email.Enabled = true;
       
       
       
    }
 private string checkInput()
 {

     string msg = "";

     if (txt_qymc.Text.Trim() == "") msg += "请输入企业名称！";
     if (drp_sd.SelectedValue.ToString() == "0") msg += "请选择企业所属镇街道！";
   
     if (drop_industry.SelectedValue.ToString() == "0") msg += "请选择企业所属行业！";

     if (Editid == null)
     {
         string checkstr = "select * from t_委托单位 where 单位全称='" + txt_qymc.Text.Trim() + "'";
         DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
         if (dscheck != null)
             if (dscheck.Tables[0].Rows.Count > 0)
             {
                 msg += "该企业已存在！";
             }
     }
     else
     {
         string checkstr = "select * from t_委托单位 where 单位全称='" + txt_qymc.Text.Trim() + "' and id!='" + Editid + "'";
         DataSet dscheck = new MyDataOp(checkstr).CreateDataSet();
         if (dscheck != null)
             if (dscheck.Tables[0].Rows.Count > 0)
             {
                 msg += "单位全称不能重复！";
             }

     }

     return msg;
 }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (btn_Add.Text == "编 辑")
        {
            AllTxtCanWrite();
            btn_Add.Text = "确定";

        }
        else
        {
            string msg = checkInput();
            if (msg != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + msg + "')", true);
                return;
            }
            else
            {
                Entity.Station entity = new Entity.Station();
                entity.type = 0;
                entity.statusID = 1;
                entity.qymc = txt_qymc.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.cname = txt_cname.Text.Trim();// = ds.Tables[0].Rows[0]["单位曾用名全称"].ToString();
                entity.dz = txt_dz.Text.Trim();// = ds.Tables[0].Rows[0]["单位详细地址"].ToString();
                entity.frdm = txt_frdm.Text.Trim();
                entity.sd = drp_sd.SelectedValue.ToString();// = ds.Tables[0].Rows[0]["所属镇街道"].ToString();
                entity.yzbm = txt_yzbm.Text.Trim();// = ds.Tables[0].Rows[0]["邮政编码"].ToString();
                entity.frdb = txt_frdb.Text.Trim();// = ds.Tables[0].Rows[0]["法定代表人"].ToString();
                entity.tel1 = txt_tel1.Text.Trim();// = ds.Tables[0].Rows[0]["tel1"].ToString();
                entity.mobile1 = txt_mobile1.Text.Trim();// = ds.Tables[0].Rows[0]["mobile1"].ToString();
                entity.zfw1 = txt_zfw1.Text.Trim();// = ds.Tables[0].Rows[0]["市府网1"].ToString();
                entity.hbfg = txt_hbfg.Text.Trim();//= ds.Tables[0].Rows[0]["环保分管人"].ToString();
                entity.tel2 = txt_tel2.Text.Trim();// = ds.Tables[0].Rows[0]["tel2"].ToString();
                entity.mobile2 = txt_mobile2.Text.Trim();// = ds.Tables[0].Rows[0]["mobile2"].ToString();
                entity.zfw2 = txt_zfw2.Text.Trim();// = ds.Tables[0].Rows[0]["市府网2"].ToString().Trim();
                entity.hbfz = txt_hbfz.Text.Trim();// = ds.Tables[0].Rows[0]["环保负责人"].ToString().Trim();
                entity.tel3 = txt_tel3.Text.Trim();// = ds.Tables[0].Rows[0]["tel3"].ToString();
                entity.mobile3 = txt_mobile3.Text.Trim();// = ds.Tables[0].Rows[0]["mobile3"].ToString();
                entity.zfw3 = txt_zfw3.Text.Trim();// =
                entity.czhm = txt_czhm.Text.Trim();
                entity.email = txt_email.Text.Trim();
               
                entity.control = drop_control.SelectedValue.Trim();
                if (drop_industry.SelectedValue.Trim() != "" && drop_industry.SelectedValue.Trim() != "0")
                    entity.industry = drop_industry.SelectedValue.Trim();
                else
                    entity.industry = "12";
                entity.qyjcdm = "";
                DAl.Station stationbj = new DAl.Station();
                if (Editid == null)
                {

                    if (stationbj.AddStation(entity) > 0)
                    {

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据添加成功！');hiddenDetail();", true);

                        Query();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据添加失败！');", true);
                    }
                }
                else
                {//单位全称,单位曾用名全称,单位详细地址,单位法人代码,所属镇街道,邮政编码,法定代表人,tel1,mobile1,市府网1,环保分管人,tel2,mobile2,市府网2,环保负责人,tel3,mobile3,市府网3,污水是否入网,污水入网时间,集中供热是否入网,集中供热入网时间
                    entity.ID = int.Parse(Editid);
                    if (stationbj.UpdateStation(entity) > 0)
                    {

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据编辑成功！');hiddenDetail();", true);
                        Query();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据编辑失败！');", true);
                    }

                }
            }
        }
        btn_Add.Enabled = true;

    }

    /// <summary>
    /// 删除超时文件
    /// </summary>
    /// <param name="strPath"></param>
    private void RemoveFiles(string strPath)
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] fiArr = di.GetFiles();
        foreach (System.IO.FileInfo fi in fiArr)
        {
            if (fi.Extension.ToString() == ".xls" || fi.Extension.ToString() == ".doc")
            {
                // if file is older than 2 minutes, we'll clean it up
                TimeSpan min = new TimeSpan(0, 0, 2, 0, 0);
                if (fi.CreationTime < DateTime.Now.Subtract(min))
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
                }
            }
        }
    }
    #region 打印


    protected void btn_print_Click(object sender, EventArgs e)
    {

        try
        {
            RemoveFiles(Server.MapPath(".") + "\\");
        }
        catch (Exception exp)
        {
           Log.log alog = new Log.log();
            alog.Log(exp.Message.ToString() + DateTime.Now.ToString());
        }

        Random rd = new Random();
        int oid = rd.Next(10000);
        Word.Application app = new Word.Application();
        Word.Document doc = new Word.Document();
        object missing = System.Reflection.Missing.Value;
        object IsSave = true;
        try
        {
            DataSet ds_depart = new MyDataOp("select * from t_委托单位 inner join t_M_ClientInfo on 所属镇街道=t_M_ClientInfo.id inner join t_行业信息 on t_行业信息.id=行业类别  where t_委托单位.id='" + Editid + "'").CreateDataSet();
            depart departobj = new depart();
            if (ds_depart.Tables[0].Rows.Count > 0)
            {
                departobj.departid = ds_depart.Tables[0].Rows[0]["id"].ToString().Trim();
                departobj.departname = ds_depart.Tables[0].Rows[0]["单位全称"].ToString().Trim();
                departobj.address = ds_depart.Tables[0].Rows[0]["单位详细地址"].ToString().Trim();
                departobj.fddb = ds_depart.Tables[0].Rows[0]["法定代表人"].ToString().Trim();
               // departobj.frdm = ds_depart.Tables[0].Rows[0]["单位法人代码"].ToString().Trim();
                departobj.frdh = ds_depart.Tables[0].Rows[0]["tel1"].ToString().Trim();
                departobj.frsj = ds_depart.Tables[0].Rows[0]["mobile1"].ToString().Trim();
                departobj.fzrsfw = ds_depart.Tables[0].Rows[0]["市府网1"].ToString().Trim();

                departobj.fgr = ds_depart.Tables[0].Rows[0]["环保分管人"].ToString().Trim();
                departobj.fgdh = ds_depart.Tables[0].Rows[0]["tel2"].ToString().Trim();
                departobj.fgsj = ds_depart.Tables[0].Rows[0]["mobile2"].ToString().Trim();
                departobj.fgsfw = ds_depart.Tables[0].Rows[0]["市府网2"].ToString().Trim();
                departobj.fzr = ds_depart.Tables[0].Rows[0]["环保负责人"].ToString().Trim();
                departobj.fzdh = ds_depart.Tables[0].Rows[0]["tel3"].ToString().Trim();
                departobj.fzrsj = ds_depart.Tables[0].Rows[0]["mobile3"].ToString().Trim();
                departobj.fzrsfw = ds_depart.Tables[0].Rows[0]["市府网3"].ToString().Trim();
                departobj.sczt = ds_depart.Tables[0].Rows[0]["des2"].ToString().Trim();
                departobj.ssjd = ds_depart.Tables[0].Rows[0]["des"].ToString().Trim();
                departobj.yzbm = ds_depart.Tables[0].Rows[0]["邮政编码"].ToString().Trim();
                departobj.dzyx = ds_depart.Tables[0].Rows[0]["电子邮箱"].ToString().Trim();
                departobj.czhm = ds_depart.Tables[0].Rows[0]["传真号码"].ToString().Trim();
                departobj.hy = ds_depart.Tables[0].Rows[0]["des1"].ToString().Trim();
                //if (ds_depart.Tables[0].Rows[0]["污水是否入网"].ToString().Trim() == "1")
                //    departobj.wsrw = "是";
                //else
                //    departobj.wsrw = "否";
                //if (ds_depart.Tables[0].Rows[0]["污水入网时间"].ToString() != "1900-1-1 0:00:00" && ds_depart.Tables[0].Rows[0]["污水入网时间"].ToString() != "")
                //    departobj.rwsj = ds_depart.Tables[0].Rows[0]["污水入网时间"].ToString().Trim();
                //else
                //    departobj.rwsj = "";
                //if (ds_depart.Tables[0].Rows[0]["集中供热是否入网"].ToString().Trim() == "1")
                //    departobj.jzgr = "是";
                //else
                //    departobj.jzgr = "否";
                //if (ds_depart.Tables[0].Rows[0]["集中供热入网时间"].ToString() != "1900-1-1 0:00:00" && ds_depart.Tables[0].Rows[0]["集中供热入网时间"].ToString() != "")
                //    departobj.grsj = ds_depart.Tables[0].Rows[0]["集中供热入网时间"].ToString().Trim();
                //else
                //    departobj.grsj = "";
                //departobj.yyzzfw = ds_depart.Tables[0].Rows[0]["工商营业执照经营范围"].ToString().Trim();
                //if (ds_depart.Tables[0].Rows[0]["单位设立时间"].ToString() != "1900-1-1 0:00:00" && ds_depart.Tables[0].Rows[0]["单位设立时间"].ToString() != "")
                //    departobj.jcsj = DateTime.Parse(ds_depart.Tables[0].Rows[0]["单位设立时间"].ToString().Trim()).ToString("yyyy-mm-dd");
                //else

                //    departobj.jcsj = "";
                //departobj.jncp = ds_depart.Tables[0].Rows[0]["企业近年产品产量"].ToString().Trim();
                departobj.cym = ds_depart.Tables[0].Rows[0]["单位曾用名全称"].ToString().Trim();






                int j = 0;

                //string DocPath = ConfigurationManager.AppSettings["DocPath"].ToString();
                string DocPath = Server.MapPath("../");



                string TemplateFile = "";
                //if (hpxs == "环评书")
                TemplateFile = DocPath + "BaseData\\template\\qydjtemplate.doc";
                //else if (hpxs == "环评表")
                //    TemplateFile = DocPath + "\\template\\template_pf_nocs.doc";

                //生成的具有模板样式的新文件
                string FileName = DocPath + "BaseData\\" + oid.ToString() + ".doc";

                File.Copy(TemplateFile, FileName);


                object Obj_FileName = FileName;

                object Visible = false;

                object ReadOnly = false;

                //PageSetupDialog dlgPageSetup = new PageSetupDialog();

                //打开文件 
                //WORD程序不可见
                app.Visible = false;
                //不弹出警告框
                app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

                //先保存默认的打印机
                string defaultPrinter = app.ActivePrinter;



                doc = app.Documents.Open(ref Obj_FileName,

                ref missing, ref ReadOnly, ref missing,

                ref missing, ref missing, ref missing, ref missing,

                ref missing, ref missing, ref missing, ref Visible,

                ref missing, ref missing, ref missing,

                ref missing);

                doc.Activate();
                // 单位全称，企业地址，法人代表，机构代码，试用类型，COD，废水排放量，
                //string[] sqArr = { "departname", "address", "frdb", "jgdm", "sytype", "infectant1", "water", "infectant1nd", "infectant2", "infectant2nd", "remark" };


                foreach (Word.Bookmark bm in doc.Bookmarks)
                {
                    switch (bm.Name)
                    {
                        case "c_dwmc": bm.Select(); bm.Range.Text = departobj.departname.ToString(); break;
                        case "c_xxdz": bm.Select(); bm.Range.Text = departobj.address.ToString(); break;
                        case "c_frdm": bm.Select(); bm.Range.Text = departobj.frdm.ToString(); break;
                        case "c_ssjd": bm.Select(); bm.Range.Text = departobj.ssjd.ToString(); break;
                        case "c_yzbm": bm.Select(); bm.Range.Text = departobj.yzbm.ToString(); break;
                        case "c_czhm": bm.Select(); bm.Range.Text = departobj.czhm.ToString(); break;
                        case "c_dzyx": bm.Select(); bm.Range.Text = departobj.dzyx.ToString(); break;
                        case "c_sczt": bm.Select(); bm.Range.Text = departobj.sczt.ToString(); break;
                        case "c_hy": bm.Select(); bm.Range.Text = departobj.hy.ToString(); break;

                        case "c_frdb": bm.Select(); bm.Range.Text = departobj.fddb.ToString(); break;
                        case "c_frdh": bm.Select(); bm.Range.Text = departobj.frdh.ToString(); break;
                        case "c_frsj": bm.Select(); bm.Range.Text = departobj.frsj.ToString(); break;
                        case "c_frsfw": bm.Select(); bm.Range.Text = departobj.frsfw.ToString(); break;

                        case "c_fgr": bm.Select(); bm.Range.Text = departobj.fgr.ToString(); break;
                        case "c_fgrdh": bm.Select(); bm.Range.Text = departobj.fgdh.ToString(); break;
                        case "c_fgrsj": bm.Select(); bm.Range.Text = departobj.fgsj.ToString(); break;
                        case "c_fgrsfw": bm.Select(); bm.Range.Text = departobj.fgsfw.ToString(); break;

                        case "c_zrr": bm.Select(); bm.Range.Text = departobj.fzr.ToString(); break;
                        case "c_zrrdh": bm.Select(); bm.Range.Text = departobj.fzdh.ToString(); break;
                        case "c_zrrsj": bm.Select(); bm.Range.Text = departobj.fzrsj.ToString(); break;
                        case "c_zrrsfw": bm.Select(); bm.Range.Text = departobj.fzrsfw.ToString(); break;

                        case "c_wsrw": bm.Select(); bm.Range.Text = departobj.wsrw.ToString(); break;
                        case "c_wsrwsj": bm.Select(); bm.Range.Text = departobj.rwsj.ToString(); break;

                        case "c_jzgr": bm.Select(); bm.Range.Text = departobj.jzgr.ToString(); break;
                        case "c_jzgrsj": bm.Select(); bm.Range.Text = departobj.grsj.ToString(); break;

                        case "c_yyzzfw": bm.Select(); bm.Range.Text = departobj.yyzzfw.ToString(); break;
                        case "c_jncp": bm.Select(); bm.Range.Text = departobj.jncp.ToString(); break;
                        case "c_jcsj": bm.Select(); bm.Range.Text = departobj.jcsj.ToString(); break;
                        case "c_cym": bm.Select(); bm.Range.Text = departobj.cym.ToString(); break;

                    }
                }





                doc.Save();



                object Background = true;
                //doc.PrintPreview();
                //doc.PrintOut(ref Background, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);



              ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('" + oid.ToString() + ".doc','theNewWindow',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')", true);

            }
        }
        catch (Exception mes)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('打印机连接失败！请先测试打印机连接状态！')", true);



            Log.log alog = new Log.log();
            alog.Log(mes.Message.ToString() + DateTime.Now.ToString());
        }
        finally
        {
            doc.Close(ref IsSave, ref missing, ref missing);
            app.Application.Quit(ref missing, ref missing, ref missing);
            doc = null;
            app = null;

        }



    }
    #endregion
    #endregion
}
