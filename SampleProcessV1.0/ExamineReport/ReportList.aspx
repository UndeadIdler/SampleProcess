﻿<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportList.aspx.cs" Inherits="ExamineReport_ReportList" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <script language="javascript" type="text/javascript" src="../js/Calendar30.js"></script>
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
            <div style=" text-align:center; position:inherit; top:1px; width:800px; height: 15px;"><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>样品列表</b></font></div>
<div style=" text-align:center; position:inherit; top:1px; width:900px; height: 15px;"><%=outputSum %></div>
<br>
<div class="t_r"> 
    <div class="t_r_t" id="t_r_t"> 
        <div class="t_r_title"> 
            <table> 
             <tr> 
             <th width="5%">序号</th> 
             <th width="7%">项目类型</th> 
              <th width="7%">综合室项目受理</th> 
             
               <th width="7%">移交外业室</th> 
               <th width="7%">外业室完成现场踏勘</th>
               <th width="7%">编制方案并移交综合业务室</th> 
               <th width="7%">综合室签定协议</th>
               <th width="7%">外业室完成现场监测</th> 
               <th width="8%">综合室向外业室移交监测数据</th>
               <th width="7%">报告编制后交室内审核</th> 
               <th width="7%">报告移交综合室审核</th> 
               <th width="7%">综合室审定意见返回外业室</th> 
               <th width="7%">报告交办公室装订</th> 
               <th width="10%">验收会议反馈意见表返回外业室</th> 
             </tr> 
            </table> 
        </div> 
    </div> 
<div class="t_r_content" id="t_r_content"> 
<div id="design" style="OVERFLOW: hidden; height:580px;">
<div id="design1" style="OVERFLOW: hidden"><%=outputStr%></div>
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
</asp:Content>
