<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="True" 
    CodeFile="SampleList.aspx.cs" Inherits="Query_SampleList" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style type="text/css"> 
    table{border-collapse:collapse;border-spacing:0px; width:100%; border:#4F7FC9 solid 1px;} 
    table td{border:1px solid #4F7FC9;height:25px; text-align:center; font-size:12px;} 
    table th{ background-color:#E3EFFF; border:#4F7FC9 solid 1px; white-space:nowrap; height:22px; border-top:0px;border-left:1px; font-size:14px;}   
            /*t_r_content和cl_freeze高度相差20px， 高度为外观显示高度，可根据情况调整*/ 
    .t_r_content{width:80%;  background:#EEF8FF; } 
    .cl_freeze{overflow:hidden; width:100%;} 
    /* width 调整左边标题列宽度（左侧外观显示宽度）; 指定为width:auto 在Opera下显示有问题; height比 t_r_content 高度小20px*/ 
    /* width 宽度为 右侧外观显示宽度 实际显示宽度大小为“t_r”宽度加上“cl_freeze”宽度 */ 
              /* 如果显示不正常，调整 t_r的width 使其和t_left的width之和小于100%；等于100%时会有问题*/ 
    .t_r{ width:99%;}     
    .t_r table{width:900px;} 
    .t_r_title{width:900px;}/*宽度比 t_r table 大20px （至少大20，小了滚动条滑到右侧显示有问题）*/ 
   
    .bordertop{ border-top:0px;} 
    .AutoNewline {word-break: break-all;} 
  
    .scroll {   
        width: 900px;                                     /*宽度*/   
        height: 450px;                                  /*高度*/   
        overflow-y: auto;                             /*横向滚动条(scroll:始终出现;auto:必要时出现;具体参考CSS文档)*/   
       
    }   
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
  var xmlHttp;
  var xmlHttpObject; 
  var xmlHttpGet;
  var xmlHttpArea;
  var dd;
  var area;
   function createXMLHttpRequest()
  {  

  try
    {    // Firefox, Opera 8.0+, Safari 
       xmlHttp=new XMLHttpRequest(); 
       return xmlHttp;  
    }
  catch (e)
    {    // Internet Explorer   
     try
      {     
       xmlHttp=new ActiveXObject("Msxml2.XMLHTTP"); 
        return xmlHttp;   
       }
    catch (e)
      {     
       try
        {       
         xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");  
          return xmlHttp;     
        }
      catch (e)
        {        
            alert("Your browser does not support AJAX!");      
            return false;    
         }      
      }  
    } 
}
setTimeout(load,1000);
function load(){
 setInterval("changeArea()",60000);
  setInterval("changesum()",60000);
 }
 
 //所属区改变调用的ajax
function changesum()
{

var url = "GetNewData.aspx?kw="+Math.random();

xmlHttpArea=createXMLHttpRequest();
xmlHttpArea.onreadystatechange=handleStateChange;

xmlHttpArea.open('GET', url, true);
xmlHttpArea.send(null);
}
////所属区改变调用的回调函数
function handleStateChange()
{
if(xmlHttpArea.readyState == 4) {
if(xmlHttpArea.status == 200) {
var sum=xmlHttpArea.responseText;

var dd=document.getElementById("TotalSum");

dd.innerHTML=sum;

}
}
}
//所属区改变调用的ajax
function changeArea()
{

var url = "GetListData.aspx?kw="+Math.random();

xmlHttpGet=createXMLHttpRequest();
xmlHttpGet.onreadystatechange=AreahandleStateChange;

xmlHttpGet.open('GET', url, true);
xmlHttpGet.send(null);
}
////所属区改变调用的回调函数
function AreahandleStateChange()
{
if(xmlHttpGet.readyState == 4) {
if(xmlHttpGet.status == 200) {
var areaDepart=xmlHttpGet.responseText;

var dd2=document.getElementById("design");
dd2.innerHTML=areaDepart;




}
}
}
</script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <table style="width: 321px">
                <tr>
                    <td style="height: 7px" colspan="3">
                        <span style="font-size: 10pt">
                            <img src="../Images/minipro.gif" />通讯中，请稍等....</span>
                    </td>
                </tr>
            </table>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: center; position: inherit; top: 1px; width: 800px; height: 15px;">
                <font style='width: 102.16%; color: #2292DD; font-size: 14pt; line-height: 150%;
                    font-family: 楷体_GB2312; height: 35px'><b>样品列表</b></font></div>
            <div id="TotalSum" style="text-align: center; position: inherit; top: 1px; width: 900px; height: 15px;">
             <asp:Label ID="lbl_sum" runat="server" Text=""></asp:Label>
                </div>
            <br>
            <div class="t_r" style="text-align:center;">
                <div id="t_r_t"  style="text-align:left; width:900px;">
                    <div class="t_r_title">
                    
                        <table>
                            <tr>
                                <th width="5%">
                                    序号
                                </th>
                                <th width="8%">
                                    报告标识
                                </th>
                                <th width="8%">
                                    项目类型
                                </th>
                                <th width="10%">
                                    接样时间
                                </th>
                                <th width="5%">
                                    备注
                                </th>
                                <th width="8%">
                                    样品类型
                                </th>
                                <th width="8%">
                                    样品编号
                                </th>
                                <th width="8%">
                                    样品状态
                                </th>
                                <th width="8%">
                                  数据交接
                                </th>
                                <th width="8%">
                                    报告编制
                                </th>
                                <th width="8%">
                                    报告审核
                                </th>
                                <th width="8%">
                                    报告签发
                                </th>
                                <th width="8%">
                                    报告装订
                                </th>
                            </tr>
                        </table>
                    </div>
               
                <div class="t_r_content" id="t_r_content"> 
<div id="design"  class="scroll" style=" text-align:center;" >
    <asp:Label ID="Samplelist" runat="server" Text=""></asp:Label>
    <%--<asp:Table ID="tb_list" runat="server">
    </asp:Table>--%>
  

</div>
 </div>
</div>
</div>

<%--<script  type="text/javascript">
var speed=2;

design2.innerHTML=design1.innerHTML;


function Marquee2()
{
if(design2.offsetTop-design.scrollTop<=0)
{
//Reflash();
design.scrollTop-=design1.offsetHeight;
}
else
{
design.scrollTop++;
}
}
var MyMar2=setInterval(Marquee2,speed);
if(design2.offsetHeight<=580)
{
design2.innerHTML="";}
design.onmouseover=function()
{
clearInterval(MyMar2);
}
design.onmouseout=function()
{
MyMar2=setInterval(Marquee2,speed);
}
</script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
