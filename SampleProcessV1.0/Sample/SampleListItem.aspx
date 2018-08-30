<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleListItem.aspx.cs" Inherits="Sample_SampleListItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>样品列表细分</title>
     <link href="../App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body class="body">
    <form id="form1" runat="server">
    <script type="text/javascript">
   setTimeout("Reflash()",300000);
    function Reflash(){
	window.location.reload(true);
	}
    </script>
  <style type="text/css"> 
    table{border-collapse:collapse;border-spacing:0px; width:100%; border:#4F7FC9 solid 1px;} 
    table td{border:1px solid #4F7FC9;height:25px; text-align:center; font-size:12px;} 
    table th{ background-color:#E3EFFF; border:#4F7FC9 solid 1px; white-space:nowrap; height:22px; border-top:0px;border-left:1px; font-size:14px;}   
            /*t_r_content和cl_freeze高度相差20px， 高度为外观显示高度，可根据情况调整*/ 
    .t_r_content{width:900px;  background:#EEF8FF; } 
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
            
<div class="t_r"> 
    <div class="t_r_t" id="t_r_t"> 
        <div class="t_r_title"> 
            <table> 
             <tr> 
             <th width="5%">序号</th> 
             <th width="8%">报告标识</th> 
              <th width="8%">项目类型</th> 
             
               <th width="10%">接样时间</th> 
               <th width="5%">备注</th>
               <th width="8%">样品类型</th> 
               <th width="8%">样品编号</th>
               <th width="8%">报告备注</th> 
               <th width="8%">报告编制</th>
               <th width="8%">报告校核</th> 
               <th width="8%">报告审核</th> 
               <th width="8%">报告签发</th> 
               <th width="8%">报告装订</th> 
             </tr> 
            </table> 
        </div> 
    </div> 
<div class="t_r_content" id="t_r_content"> 
<div id="design" class="t_r_content" style="OVERFLOW: hidden; height:580px;">
<div id="design1" style="OVERFLOW: hidden;"><%=outputStr%></div>
<div id="design2"></div>
</div>
</div>
</div>

<script  type="text/javascript">
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
</script>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
