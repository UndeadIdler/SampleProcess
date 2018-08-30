<%@ Page Language="C#" AutoEventWireup="true" CodeFile="flow_drawline.aspx.cs" Inherits="flow_drawline" Theme="Default" StylesheetTheme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>工艺流程图</title>
    <link href="../App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<script language="javascript" type="text/javascript">
 var sum=<%=intPointSum %>;
 //var linePonits=<%=linepoint %>;
 var position=new Array(sum*2);
 var point="";
 var i=0;
  var $ = function(o){
      return document.getElementById(o);
  };
  var shapeTag  = 'line';
  var weight = '1px';
  var color  = '#45dffb';//
  var mouseDown = false;
  var polyline = false;
  var lines = new Array();
  var startX,startY;//鼠标按下的初始坐标
  window.onload = function(){
      //画板背景
   var rectStr =  ' <v:roundRect style="position:relative;width:1000px;height:600px;z-index:2"'
    rectStr += ' strokeColor="#000000" strokeweight="2px" arcsize="0.02">'
    rectStr += ' <v:fill color="#ffffff" opacity="0.1"/>'
    rectStr += ' </v:roundRect>'
   $('map').insertAdjacentHTML('beforeEnd', rectStr);
   var newShape = {};
   $('map').attachEvent('onmousedown',function(){
    if(window.event.button == '1'){
     mouseDown = true;
        initShape($(shapeTag),window.event);
    }
   });
   $('map').attachEvent('onmousemove',function(){
   
    if(mouseDown)
    {
  
    drawShape($(shapeTag),window.event);
    }
   });
   $('map').attachEvent('onmouseup',function(){
    mouseDown = false;
   $('map').appendChild(creatNewShape($(shapeTag),window.event)); 
  
   });
  };
  var selectShape = function(){
      shapeTag = event.srcElement.tagName;
  };
  
  var initShape = function(o,e){
      switch(o.tagName.toLowerCase()){
       case 'line':
              o.from = e.offsetX+','+e.offsetY;
     o.to   = e.offsetX+','+e.offsetY;
        break;

   }
      o.style.display = 'block';
  };
  var drawShape = function(o,e){
     if(mouseDown){
   switch(o.tagName.toLowerCase()){
    case 'line':
       o.to   = e.offsetX+','+e.offsetY;
    // o.to = e.offsetX +','+ e.offsetY ;
     break;
   
   }   
  }
  };
  /*---------------------------------------------------------------------
         新建的图形必须放在画板下面，否则鼠标的onmouseover事件会被干扰 
   ---------------------------------------------------------------------*/
  var creatNewShape = function(o,e){
  
  o.style.display = 'none';
  var shape = document.createElement('v:'+o.tagName);
  switch(o.tagName.toLowerCase()){
      case 'line':
    shape.from = o.from ;
    shape.to = o.to;
    break;

  }
  if(pint="")
  {
  point=shape.from+","+shape.to;
  }
  else
  {
  point+=";"+shape.from+","+shape.to;
  
  }
  shape.strokeWeight   = weight;
  shape.strokeColor    = color;
  //新建的图形放在画板背景下面
  shape.style.zIndex   = '1';
     shape.innerHTML = '<v:fill opacity="0.1"/>';
     return shape;
  };
  
  function saveEdit()
	{
	
	   <%= ClientScript.GetCallbackEventReference(this, "point", "getServerData",null)%>;
	    alert("保存成功！");
	}
	function save()
	{
	   <%= ClientScript.GetCallbackEventReference(this, "point", "getServerData",null)%>;
	    
	}
	function getServerData(rValue)
	{
	    alert(rValue);
	}

</script>
<%--<script type="text/javascript">
    
	setTimeout("abc()",300000);
	
 function abc(){
	window.location.reload(true);
	}oncontextmenu="return false" ondragstart="return false" onselectstart ="return false" onselect="document.selection.empty()" oncopy="document.selection.empty()" onbeforecopy="return false" onmouseup="document.selection.empty()"
	</script>--%>
<body style="text-align: center" >
    <form id="form1" runat="server"> 
 <div style="z-index: 101; left: 29px; width: 100%; position: absolute; top: 9px; height: 1px; text-align:center;">
         <table class="container">
            <tr>
                <td class="querytitleList">
                    <asp:Label ID="lbl_ForSca_Title" runat="server" CssClass="mLabel" Text="监测点：" Height="17px"></asp:Label>
                </td>
                <td class="queryctrlList">
                    <asp:DropDownList ID="drop_List" runat="server" CssClass="mDropDownList" 
                        Width="100px" 
                        onselectedindexchanged="drop_List_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
               
                <td>
                    <asp:Button ID="btn_search" runat="server" Text="搜索" CssClass="mButton" OnClick="btn_search_Click" />
                </td> 
                <td>
                        <input id="btn_save" type="button" value="编辑保存" onclick="saveEdit();" class="mButton"/></td>
                        <td>
                            <asp:Button ID="btn_delete" runat="server" CssClass="mButton"
                                Text="删除所有线" onclick="btn_delete_Click" />
                </td>
            </tr>
        </table>
        </div> 
   
    </form>
       
    </body>
</html>
