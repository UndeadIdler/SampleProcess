<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Sample_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>样品列表</title>
      <link href="../App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />
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
    .t_r{width:99%;}     
    .t_r table{width:900px;} 
    .t_r_title{width:900px;}/*宽度比 t_r table 大20px （至少大20，小了滚动条滑到右侧显示有问题）*/ 
    .t_r_t{width:99%; overflow:hidden;} 
    .bordertop{ border-top:0px;} 
    .AutoNewline {word-break: break-all;} 
</style>

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

//所属区改变调用的ajax
function Update()
{

//area=encodeURIComponent(document.all("DropDownList_Area").value);
var url = "GetNewData.aspx?kw="+Math.random();

xmlHttpArea=createXMLHttpRequest();
xmlHttpArea.onreadystatechange=AreahandleStateChange;

xmlHttpArea.open('GET', url, true);
xmlHttpArea.send(null);
//initialize();
}

 //setTimeout("Update()",30000);
////所属区改变调用的回调函数
function AreahandleStateChange()
{
//alert(xmlHttpArea.readyState );
    if(xmlHttpArea.readyState == 4) 
    {
    //alert(xmlHttpArea.xmlHttpArea.status);

        if(xmlHttpArea.status == 200) 
        {
       
        var areaDepart=xmlHttpArea.responseText;
       
       
        document.getElementById("design1").innerHTML=areaDepart;
        }
    }
}


 document.ondblclick=initialize;  
  document.onclick=sc;   
  var   timer;   
  function   initialize()
  { 
      
      timer=setInterval('scrollwindow()',50); 
       
  }     
  function sc()
  {   
    clearInterval(timer);   
  }   
  function   scrollwindow()     
  {     
      currentpos=document.body.scrollTop;     
      window.scroll(0,++currentpos);     
     if   (currentpos   !=   document.body.scrollTop)  
     { //Update();
//     //scrollwindow();  
    //window.location.reload(true);
    }
       
  }     

    </script>
    </head>

<body  class="body" style=" width:900px" onload="Update()">
    <form id="form1" runat="server">
    <div id="design1">
    <%=outputStr%>
        </div>
    </form>
</body >
</html>
