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
using System.Data.SqlClient;
using WebApp.Components;
using System.Collections.Generic;

public partial class carinfo_OutCar : System.Web.UI.Page
{
    private string strSelectedId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strSelectedId"]; }
        set { ViewState["strSelectedId"] = value; }
    }
    private string strPersonId//所选择操作列记录对应的id
    {
        get { return (string)ViewState["strPersonId"]; }
        set { ViewState["strPersonId"] = value; }
    }
    protected BLL.Car.OutCar outcar = new BLL.Car.OutCar();
    BLL.Car.Person objperson = new BLL.Car.Person();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            txt_s.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_e.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            btn_query_Click(null, null);
        }


    }

    protected void btn_query_Click(object sender, EventArgs e)
    {
      
        List<Entity.Car.OutCar> outList = new List<Entity.Car.OutCar>();
       DataSet  ds = outcar.Query("", txt_s.Text.Trim(), txt_e.Text.Trim());
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
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        lbl_Type.Text = "添加";
        btn_OK.Text = "添加";
        CleanAllText();
        AllTxtCanWrite();
        btn_adddetail.Visible = false;
        panel_detail.Visible = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);
        btn_query_Click(null, null);

    }

    protected void btn_OK_Click(object sender, EventArgs e)
    {

        if (btn_OK.Text == "编辑")
        {
            AllTxtCanWrite();
            btn_OK.Text = "确定";
            lbl_Type.Text = "编辑";
        }
        else
        {
           
            if (txt_start.Text.Trim() == "" ||txt_end.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('前填写出车时间！');", true);
                return;
            }
            if(drop_carno.SelectedValue=="-1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择车辆！');", true);
                return;
            }
            if (txt_destn.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请填写目的地！');", true);
                return;
            }
            if (txt_driver.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请填写出车人！');", true);
                return;
            }
           
           
            #region 当按钮文字为“确定”时，执行添加或编辑操作
            Entity.Car.OutCar entity = new Entity.Car.OutCar();
            entity.CarNO = drop_carno.SelectedValue.ToString();
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();
            entity.CreateDate = DateTime.Now;
            entity.Num = int.Parse(lbl_num.Text.Trim());
            entity.Destn = txt_destn.Text.Trim();
            entity.OutStart = DateTime.Parse(txt_start.Text.Trim());
            entity.OutEnd = DateTime.Parse(txt_end.Text.Trim());
            entity.Driver = txt_driver.Text.Trim();
            if (entity.OutStart >= entity.OutEnd)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('出车时间填写有误！');", true);
                return;
            }
            TimeSpan t = entity.OutEnd - entity.OutStart;
            if (t.TotalMinutes<=10)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('出车时间填写有误！');", true);
                return;
            }
            //添加新纪录
            if (lbl_Type.Text == "添加")
            {
                int retstr = outcar.add(entity);
                switch (retstr.ToString())
                {
                    case "1":

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加成功！');", true);
                        break;
                    case "0":
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据添加失败！');", true);
                        break;
                    case "2": ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已存在该时段该车的出车记录，不能重复！');", true);
                        break;

                }
            }

            //编辑记录
            else if (lbl_Type.Text == "编辑")
            {
                entity.ID = strSelectedId;
                entity.UpdateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();
                entity.UpdateDate = DateTime.Now;
                int retstr = outcar.update(entity);
                switch (retstr.ToString())
                {
                    case "1":
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "hiddenDetail();alert('数据编辑成功！');", true);
                        break;
                    case "0":
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('数据编辑失败！');", true); break;

                    case "2": ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('已存在该时段该车的出车记录，不能重复！');", true);
                        break;
                }



            }

            btn_query_Click(null, null);
            #endregion
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "hiddenDetail();", true);
        btn_query_Click(null, null);
    }
    
     
    #region GridView相关事件响应函数
    private void CleanAllText()//清空详细页面中所有的TextBox
    {
        lbl_num.Text = "";
        txt_destn.Text = "";
        txt_start.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
        txt_start.Text = "";
        txt_end.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
        txt_end.Text = "";
        txt_remark.Text = "";
        BLL.Car.Car car = new BLL.Car.Car();
        DataSet dscar = car.Query("", "");
        drop_carno.DataSource = dscar;
        drop_carno.DataValueField = "carid";
        drop_carno.DataTextField = "carid";
        drop_carno.DataBind();
        ListItem li = new ListItem("请选择", "-1");
        drop_carno.Items.Add(li);
        drop_carno.SelectedValue = "-1";
    }
    private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
    {
        txt_destn.ReadOnly = true;
        txt_start.ReadOnly = true;
        txt_end.ReadOnly = true;
        txt_remark.ReadOnly = true;
        drop_carno.Enabled = false;
    }
    private void AllTxtCanWrite()//设置详细页面中所有的TextBox为可写
    {
        txt_destn.ReadOnly = false;
        txt_start.ReadOnly = false;
        txt_end.ReadOnly = false;
        txt_remark.ReadOnly = false;
        drop_carno.Enabled = true;
    }
    protected void grdvw_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        btn_query_Click(null,null);
    }
    protected void grdvw_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        lbl_Type.Text = "编辑";
        btn_OK.Text = "编辑";
        btn_adddetail.Visible = true;
        CleanAllText();
       lbl_num.Text= grdvw_List.Rows[e.NewEditIndex].Cells[7].Text.Trim();//车牌号
        drop_carno.SelectedValue= grdvw_List.Rows[e.NewEditIndex].Cells[4].Text.Trim();//车牌号
        txt_destn.Text = grdvw_List.Rows[e.NewEditIndex].Cells[5].Text.Trim();//目的地
        txt_start.Text = grdvw_List.Rows[e.NewEditIndex].Cells[2].Text.Trim();//出发时间
        txt_end.Text = grdvw_List.Rows[e.NewEditIndex].Cells[3].Text.Trim();//结束时间
        txt_driver.Text = grdvw_List.Rows[e.NewEditIndex].Cells[6].Text.Trim();//结束时间
        AllTxtreadOnly();
        QueryPerson();
       panel_detail.Visible = false;
       
       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "showAddEdit();", true);

    }
    protected void QueryPerson()
    {
        BLL.Car.Person person = new BLL.Car.Person();
        DataSet dsdetail = person.Query(strSelectedId);
        grdvw_Detail.DataSource = dsdetail;
        grdvw_Detail.DataBind();
    }
    protected void grdvw_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "查看";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);

            //TableCell headerset = new TableCell();
            //headerset.Text = "同车人员";
            //headerset.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            //headerset.Width = 60;
            //e.Row.Cells.Add(headerset);

            TableCell headerDel = new TableCell();
            headerDel.Text = "删除";
            headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDel.Width = 30;
            e.Row.Cells.Add(headerDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

            //TableCell MenuSet = new TableCell();
            //MenuSet.Width = 60;
            //MenuSet.Style.Add("text-align", "center");
            //ImageButton btMenuSet = new ImageButton();
            //btMenuSet.ImageUrl = "~/images/Detail.gif";
            //btMenuSet.CommandName = "Select";
            ////btMenuSet.Click+=new ImageClickEventHandler(btMenuSet_Click);
            //MenuSet.Controls.Add(btMenuSet);
            //e.Row.Cells.Add(MenuSet);

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");
           
            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDel.Enabled = false;
            }
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {   //绑定数据后，隐藏列 
            e.Row.Cells[1].Visible = false;
            for (int i =9; i < e.Row.Cells.Count - 2; i++)
                e.Row.Cells[i].Visible = false;
        }
    }
   

    protected void grdvw_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strSelectedId = grdvw_List.Rows[e.RowIndex].Cells[1].Text;
        if(outcar.delete(strSelectedId)==1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
            WebApp.Components.Log.SaveLog("删除出车信息（" + strSelectedId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
        }
        else if (outcar.delete(strSelectedId) == 0)
        {
            WebApp.Components.Log.SaveLog("删除出车信息（" + strSelectedId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
        }
        else if (outcar.delete(strSelectedId) == 2)
        {
          
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在同车人员，无法删除数据！')", true);
        }
        btn_query_Click(null,null);
    }
    #endregion
    protected void grdvw_List_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drop_carno_SelectedIndexChanged(object sender, EventArgs e)
    {
        string carid = drop_carno.SelectedValue;
            
          BLL.Car.Car car = new BLL.Car.Car();
          DataSet dscar = car.Query("", carid);
        if (dscar.Tables[0].Rows.Count > 0)
            lbl_num.Text = dscar.Tables[0].Rows[0]["num"].ToString();
        else
            lbl_num.Text = "";
    }
    protected void btn_adddetail_Click(object sender, EventArgs e)
    {

        lbl_DteatilType.Text = "添加";
        btn_Save.Text = "添加";
        panel_detail.Visible = true;
       
        CleanAllTextDetail();
        AllTxtCanWriteDetail();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (btn_Save.Text == "编辑")
        {
            btn_Save.Text = "确定";
            lbl_DteatilType.Text = "编辑";
        }
        else
        {
            if (txt_name.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写同行人员名字！')", true);
                return;
            }
          
            Entity.Car.OutCar outcar = new Entity.Car.OutCar();
            outcar.OutStart = DateTime.Parse(txt_start.Text.Trim());
            outcar.OutEnd = DateTime.Parse(txt_end.Text.Trim());
            outcar.CarNO = drop_carno.SelectedValue.ToString();
            Entity.Car.Person entity = new Entity.Car.Person();
            entity.Name = txt_name.Text.Trim();
            entity.OutID = strSelectedId;
            entity.Remark = txt_detail.Text.Trim();
            entity.CreateDate = DateTime.Now;
            entity.CreateUser = Request.Cookies["Cookies"].Values["u_id"].ToString();
            entity.Destn = txt_destndetail.Text.Trim();
            if (lbl_DteatilType.Text.Trim() == "添加")
            {
                int flag=objperson.add(entity,outcar);
                if (flag == 1)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加成功！')", true);
                else if (flag == 0)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加失败！')", true);
                else if (flag == 2)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该人员已在同一时间约定其他车辆同行！')", true);
                else if (flag ==3)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该车已满员！')", true);
                QueryPerson();
            }
            if (lbl_DteatilType.Text.Trim() == "编辑")
            {
                entity.ID = strPersonId;
                int flag = objperson.update(entity, outcar);
                if (flag == 1)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('编辑成功！')", true);
                else if (flag == 0)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('编辑失败！')", true);
                else if (flag == 2)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该人员已在同一时间约定其他车辆同行！')", true);
                QueryPerson();
            }
            btn_query_Click(null, null);

        }
    }

    #region GridView相关事件响应函数
    private void CleanAllTextDetail()//清空详细页面中所有的TextBox
    {
        txt_name.Text = "";
        txt_destndetail.Text = "";
        txt_detail.Text = "";
    }
    private void AllTxtreadOnlyDetail()//设置详细页面中所有的TextBox为只读
    {
        txt_name.ReadOnly = true;
        txt_destndetail.ReadOnly = true;
        txt_detail.ReadOnly = true;
    }
    private void AllTxtCanWriteDetail()//设置详细页面中所有的TextBox为可写
    {
        txt_name.ReadOnly = false;
        txt_destndetail.ReadOnly = false;
        txt_detail.ReadOnly = false;
    }
    protected void grdvw_Detail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvw_List.PageIndex = e.NewPageIndex;
        btn_query_Click(null, null);
    }
    protected void grdvw_Detail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        strPersonId = grdvw_Detail.Rows[e.NewEditIndex].Cells[1].Text.Trim();
       lbl_DteatilType.Text = "编辑";
       btn_Save.Text = "编辑";
       
       CleanAllTextDetail();
       panel_detail.Visible = true;
       AllTxtreadOnly();
       txt_name.Text = grdvw_Detail.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        if(grdvw_Detail.Rows[e.NewEditIndex].Cells[3].Text.Trim()!="&nbsp;")
       txt_destndetail.Text = grdvw_Detail.Rows[e.NewEditIndex].Cells[3].Text.Trim();
        if (grdvw_Detail.Rows[e.NewEditIndex].Cells[4].Text.Trim() != "&nbsp;")
       txt_detail.Text = grdvw_Detail.Rows[e.NewEditIndex].Cells[4].Text.Trim();

    }
    protected void grdvw_Detail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell headerDetail = new TableCell();
            headerDetail.Text = "编辑";
            headerDetail.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDetail.Width = 60;
            e.Row.Cells.Add(headerDetail);

          

            TableCell headerDel = new TableCell();
            headerDel.Text = "删除";
            headerDel.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
            headerDel.Width = 30;
            e.Row.Cells.Add(headerDel);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到每项时颜色交替效果
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#00ffee'");
            int id = e.Row.RowIndex + 1;

            e.Row.Cells[0].Text = id.ToString();

            //手动添加详细和删除按钮
            TableCell tabcDetail = new TableCell();
            tabcDetail.Width = 60;
            tabcDetail.Style.Add("text-align", "center");
            ImageButton ibtnDetail = new ImageButton();
            ibtnDetail.ImageUrl = "~/images/Detail.gif";
            ibtnDetail.CommandName = "Edit";
            tabcDetail.Controls.Add(ibtnDetail);
            e.Row.Cells.Add(tabcDetail);

         

            TableCell tabcDel = new TableCell();
            tabcDel.Width = 30;
            tabcDel.Style.Add("text-align", "center");
            ImageButton ibtnDel = new ImageButton();
            ibtnDel.ImageUrl = "~/images/Delete.gif";
            ibtnDel.CommandName = "Delete";
            ibtnDel.Attributes.Add("OnClick", "if(!confirm('确定删除该项吗？')) return false;");

            if (Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(1, 1) == "0")
            {
                ibtnDel.Enabled = false;
            }
            tabcDel.Controls.Add(ibtnDel);
            e.Row.Cells.Add(tabcDel);
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {   //绑定数据后，隐藏列 
            e.Row.Cells[1].Visible = false;
            for (int i =5; i < e.Row.Cells.Count - 2; i++)
                e.Row.Cells[i].Visible = false;
        }
    }


    protected void grdvw_Detail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        strPersonId = grdvw_Detail.Rows[e.RowIndex].Cells[1].Text;
      if( objperson.delete(strPersonId))

      {
              ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除成功!')", true);
              WebApp.Components.Log.SaveLog("删除同行人员信息（" + strPersonId + "）成功！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
      }
      else{
          WebApp.Components.Log.SaveLog("删除同行人员信息（" + strPersonId + "）失败！！", Request.Cookies["Cookies"].Values["u_id"].ToString(), 5);
              ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据删除失败！')", true);
          }

      QueryPerson();
      btn_query_Click(null, null);
    }
    #endregion
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        panel_detail.Visible = false;
    }
}
