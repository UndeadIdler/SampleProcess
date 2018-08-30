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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;


public partial class carinfo_OutCarChart : System.Web.UI.Page
{

   
    protected BLL.Car.OutCar outcar = new BLL.Car.OutCar();
    protected BLL.Car.Car car = new BLL.Car.Car();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txt_s.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_s.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_e.Attributes.Add("OnFocus", "javascript:WdatePicker({dateFmt:'yyyy-MM-dd'})");
            txt_e.Text = DateTime.Now.ToString("yyyy-MM-dd");
             DataSet dscar = car.Query("", "");
             drop_car.DataSource = dscar;
             drop_car.DataValueField = "carid";
             drop_car.DataTextField = "carid";
             drop_car.DataBind();
             ListItem li = new ListItem("所有", ""); drop_car.Items.Add(li);
             drop_car.SelectedIndex = drop_car.Items.Count - 1;
            Query();
        }
       

  }
    private void Query()
    {
        DataSet dscar = car.Query("", drop_car.SelectedValue);
        DataSet ds = outcar.Query(drop_car.SelectedValue, txt_s.Text.Trim(),  DateTime.Parse(txt_e.Text.Trim()).AddDays(1).ToString());


           Chart1.Height =500;

           ArrayList yValues = new ArrayList();
           ArrayList tValues = new ArrayList();
             ArrayList xValues = new ArrayList();
           ArrayList nValues = new ArrayList();
         
           for (int i = 0; i < dscar.Tables[0].Rows.Count; i++)
           {
               DataRow[] dr = ds.Tables[0].Select("carid='" + dscar.Tables[0].Rows[i]["carid"].ToString() + "'");
              
                   int j=0;
                   int m = 0;
                   tValues.Clear();
                   yValues.Clear();
                   xValues.Clear();
                   nValues.Clear();
                 
                       tValues.Add(dscar.Tables[0].Rows[i]["carid"].ToString());
                       xValues.Add(0);
                       yValues.Add((i + 1));
                       nValues.Add(2);
                       tValues.Add(double.NaN);
                       xValues.Add(++j);
                       yValues.Add(double.NaN);
                       nValues.Add(0);
                  
                   if (dr.Length > 0)
                   {
                  foreach(DataRow drr in dr)
                  {
                      //if (m == 0)
                      //{
                      //    tValues.Add(dscar.Tables[0].Rows[i]["carid"].ToString());
                      //    xValues.Add(0);
                      //    yValues.Add((i+1));
                      //    nValues.Add(2);
                      //    tValues.Add(double.NaN);
                      //    xValues.Add(++j);
                      //    yValues.Add(double.NaN);
                      //    nValues.Add(0);
                      //}
                     
                       tValues.Add(DateTime.Parse(drr["outstart"].ToString()).ToString());
                       xValues.Add(++j);
                       yValues.Add((i + 1));
                       nValues.Add(1);
                       tValues.Add(DateTime.Parse(drr["outend"].ToString()).ToString());
                       yValues.Add((i + 1));
                       xValues.Add(++j);
                       nValues.Add(1);
                       string temp = DateTime.Parse(dr[m]["outend"].ToString()).ToString();
                       if (m != dr.Length-1)
                           if (DateTime.Parse(dr[m + 1]["outstart"].ToString()).ToString() != temp)
                           {
                               yValues.Add(double.NaN);
                               xValues.Add(++j);
                               nValues.Add(0);
                               tValues.Add(DateTime.Parse(drr["outstart"].ToString()).AddMinutes(5).ToString());
                           }
                       m++;
                       
                   }
                   } 
                  
                 Chart1.Series.Add("Default" + i);
                 Chart1.Series["Default" + i].BorderWidth = 2;
                 Chart1.Series["Default" + i].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                 Chart1.Series["Default" + i].BorderColor = System.Drawing.Color.CadetBlue;
                 Chart1.Series["Default" + i].ShadowOffset = 1;
                // Chart1.Series["Default" + i].Color = System.Drawing.Color.AliceBlue;

                 //Chart1.Series["Default" + i].EmptyPointStyle.BorderDashStyle = ChartDashStyle.Solid;
                 //Chart1.Series["Default" + i].EmptyPointStyle.BorderWidth = 2;
                 //Chart1.Series["Default" + i].EmptyPointStyle.Color = System.Drawing.Color.Red;
                 //Chart1.Series["Default" + i].EmptyPointStyle.MarkerStyle = MarkerStyle.None;

                  Chart1.Series["Default" + i].Points.DataBindXY(xValues, yValues);
               
                  Chart1.Series["Default" + i].ChartType = SeriesChartType.Line;
                 
                  for (int q = 0; q < Chart1.Series["Default" + i].Points.Count; q++)
                  {
                    
                      Chart1.Series["Default" + i].Points[q].Label = tValues[q].ToString();
                      if (nValues[q].ToString() == "0")

                          Chart1.Series["Default" + i].Points[q].Label = "";
                  }
                  Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
                 Chart1.Series["Default" + i].Name= dscar.Tables[0].Rows[i]["carid"].ToString();// nValues[i].ToString();

                

          
           }
     
           Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = dscar.Tables[0].Rows.Count+1;
           Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0.0;

           Chart1.ChartAreas["ChartArea1"].BorderWidth =1;
 
           
    }

    protected void drop_car_SelectedIndexChanged(object sender, EventArgs e)
    {
        Query();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Query();
    }
}
