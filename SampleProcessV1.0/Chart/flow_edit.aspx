<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="flow_edit.aspx.cs" Inherits="flow_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>流程图编辑</title>
    <%--<link href="../App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
    <script language="javascript" type="text/javascript">
    var canmove=false;
    var cobj;
    var sum=<%=intPointSum %>;
    var position=new Array(sum*2);
	//
	function mouseup()
	{
		canmove=false;
		for(i=0;i<sum;i++)
		{
        position[i]=new Array(2);
		position[i][0]=parseInt(document.getElementById("p_"+i).style.left);
		position[i][1]=parseInt(document.getElementById("p_"+i).style.top);
		}
					
	}
	//
	function mousedown(iobj)
	{
		canmove=true;
		cobj=iobj;
	}
	function mousemove(iobj)
	{
	  if (canmove) 
	  {
        iobj.style.left=(parseInt(event.clientX)-14)*10-parseInt(iobj.style.width)/2;
        iobj.style.top=(parseInt(event.clientY)-57)*6000/450*768/1024-parseInt(iobj.style.height)/2;
        if(parseInt(iobj.style.left)<0) {iobj.style.left=0;}
        if(parseInt(iobj.style.top)<0) {iobj.style.top=0;}
        if(parseInt(iobj.style.left)>10000-parseInt(iobj.style.width)) {iobj.style.left=10000-parseInt(iobj.style.width);}
        if(parseInt(iobj.style.top)>6000-parseInt(iobj.style.height)) {iobj.style.top=6000-parseInt(iobj.style.height)} 
        var curText=iobj.id+"_t";
        document.getElementById(curText).style.left=parseInt(iobj.style.left)-300;
        document.getElementById(curText).style.top=parseInt(iobj.style.top)-300*768/1024;
      }
	}
	//---------------------------------callback-----------------------------------------//
	function init()
	{
	
	    <%= ClientScript.GetCallbackEventReference(this, "position", "getServerData",null)%>;
	    alert("位置保存成功！");
	   
	}
	
	
	function getServerData(rValue)
	{
	    alert(rValue);
	}
	//-----------------------------------------------------------------------------------//
    </script>
    <style type="text/css">
        .style1
        {
            height: 7px;
            width: 76px;
        }
    </style>
</head>
<body onmousemove="mousemove(cobj)"  bgcolor="#F5F9FF">
    <form id="form1" runat="server">
        <div style="z-index: 101; left: 29px; width: 64px; position: absolute; top: 9px;
            height: 1px">
             
        <table class="container">
            <tr>
                <td class="querytitleList"> 
                    <asp:Label ID="lbl_ForSca_Title" runat="server" CssClass="mLabel" Text="流程图：" 
                        Height="17px" Width="68px"></asp:Label>
                </td>
                <td class="queryctrlList">
                    <asp:DropDownList ID="drop_List" runat="server" CssClass="mDropDownList" 
                        Width="100px" 
                        onselectedindexchanged="drop_List_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
               
                <td>
                    <asp:Button ID="btn_search" runat="server" Text="搜索" CssClass="mButton" OnClick="btn_search_Click" />
                </td> 
                        <td>
                        <input id="btn_save" type="button" value="编辑保存" onclick="init();" class="mButton"/></td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
