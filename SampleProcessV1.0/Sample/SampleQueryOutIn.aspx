﻿<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="SampleQueryOutIn.aspx.cs" Inherits="Sample_SampleListQuery3" Title="样品列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js" ></script>
    <script language="javascript" type="text/javascript" src="../js/position.js"></script>
       <script type="text/javascript">                
        var    hkey_root,hkey_path,hkey_key   
        hkey_root="HKEY_CURRENT_USER"   
        hkey_path="\\Software\\Microsoft\\Internet Explorer\\PageSetup\\"   
        //设置网页打印的页眉页脚为空   
      function   pagesetup_null()   
      {   
          try{   
              var   RegWsh   =   new   ActiveXObject("WScript.Shell")   
              hkey_key="header"           
              RegWsh.RegWrite(hkey_root+hkey_path+hkey_key,"")   
              hkey_key="footer"   
              RegWsh.RegWrite(hkey_root+hkey_path+hkey_key,"")   
          }catch(e){}   
      }   
    </script>
  <script   language= "javascript " type="text/javascript"> 
function dayin()   
{
  var code = "<center>";
  code+=document.all.dayin.innerHTML;
  code+="<object id='wb' name='wb'　 height='0' width='0' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'></object>"
  code+="<link href='../App_Themes/Default/StyleSheet.css' rel='stylesheet' type='text/css' />"
  code+="<input type='button' value='打印预览' style='border: solid 1px #a3a3a3; background-color: #e6e6e6; font-size: 12px' class='noprint' onclick='javascript:wb.execWB(7,1);' />&nbsp;&nbsp; <input type='button' value=' 打 印 ' style='border: solid 1px #a3a3a3; background-color: #e6e6e6; font-size: 12px' class='noprint' onclick='javascript:wb.execWB(6,1);' /></center>"
  code+="<style media='print' type='text/css'> .Noprint{display:none;} .PageNext{page-break-after: always;} </style>"
  var newwin=window.open('','','');
  newwin.opener = null;
  newwin.document.write(code);
   newwin.document.getElementById('ctl00_ContentPlaceHolder1_grdvw_List').style.width="700px";
  newwin.document.close();
  
}
</script>
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
          <table style="border-right: 2px groove; border-top: 2px groove; border-left: 2px groove;
                                        width: 800px; border-bottom: 2px groove">
                                        <tbody>
                                            <tr>
                                                <td>
        <asp:Panel ID="Panel6" BackColor="#F5F9FF" runat="server" GroupingText="查询条件" ForeColor= "#2292DD" Width="750px">
                 
                   <table class="container">
                <tbody>
                        <tr>
                        <td style="width:120px;text-align: right; height: 25px;" class="titleList" >
                            <asp:Label ID="Label13" runat="server" Text="样品编号"></asp:Label>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_samplequery" Width="156px" runat="server" Height="21px"></asp:TextBox>
                            
                        </td> 
                        <td class="Middle">
                        </td>
                       
                        <td style="width:120px;text-align: right" class="titleList" >
                           
                                <asp:Label ID="Label20" runat="server" Text="紧急程度"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                            <%--<asp:TextBox ID="txt_item" runat="server" Width="156px"></asp:TextBox>--%>
                            
                             <asp:DropDownList id="Drop_Urgent" runat="server" Width="156px">
                               <asp:ListItem Value="-1">请选择</asp:ListItem>
                               <asp:ListItem Value="0">一般</asp:ListItem>
                               <asp:ListItem Value="1">紧急</asp:ListItem>
                            </asp:DropDownList>
                      </td>
                        <td>
</td><td  class="Middle">
                        </td>
                        
                    </tr> 
                    <tr>
                        <td style="width:120px;text-align: right; " class="titleList" >
                           
                                <asp:Label ID="Label14" runat="server" Text="项目类型"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           
                            <asp:DropDownList id="txt_item" runat="server" Width="156px"></asp:DropDownList>
                        </td>
                        <td style="width: 48px" class="float_Middle">
                            </td>
                       <td style="width:120px;text-align: right" class="titleList" >
                           
                                <asp:Label ID="Label18" runat="server" Text="区域"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           <asp:DropDownList id="drop_client" runat="server" Width="156px"/>
                        </td>
                        <%-- <td style="width:120px;text-align: right" class="titleList" >
                           
                                <asp:Label ID="Label15" runat="server" Text="报告状态"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           <asp:DropDownList id="drop_status" runat="server" Width="156px"></asp:DropDownList>
                        </td>--%>
                        <td style="width: 48px" class="float_Middle">
                            </td>
                        <td class="ctrlList"><asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click"  
                                CssClass="mButton" Height="24px" Width="100px" /></td>
                                
                       </tr>
                     <tr>
                        
                        
                       <td style="width:120px;text-align: right; " class="titleList" >
                           
                                <asp:Label ID="Label2" runat="server" Text="样品类型"></asp:Label>
                        </td>
                        <td class="ctrlList" colspan="8" style="width:800px">
                            <asp:CheckBoxList ID="cbl_type"  runat="server" RepeatDirection="Horizontal" RepeatColumns="8"  Font-Size="X-Small"> 
                            </asp:CheckBoxList>
                        </td>
                        <td></td><td  class="Middle">
                        </td>
                        
                  </tr>
                  
                 
                    </tbody></table>
                    <asp:Panel ID="Panel7" BackColor="#F5F9FF" runat="server" Width="800px" Height="50px"
                                    Font-Size="X-Small" GroupingText="按接样时间" ForeColor= "#2292DD">
                    <table class=" container"><tbody>
                    <tr>
                    <td style=" width:115px;text-align: right" class="titleList" >
                                <asp:Label ID="Label1" runat="server" Text="开始时间"></asp:Label>
                            
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txt_QueryTime" runat="server" 
                                 Width="156px"></asp:TextBox>
                        </td>
                       <td class="Middle">
                        </td>
                       
                        <td style="width:115px;text-align: right" class="titleList" >
                                <asp:Label ID="Label17" runat="server" Text="结束时间"></asp:Label>
                            
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txt_QueryEndTime" runat="server" 
                                Height="21px" Width="156px"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td class="ctrlList"></td>
                        
                    </tr>
                </tbody>
            </table> 
            </asp:Panel>
           </asp:Panel> <div align="right"><asp:Button ID="btn_print"  CssClass="btn"  runat="server" Text="导出样品出入库登记表" OnClick="btn_print_Click" /><%--<input type="button" onclick="dayin()" value="打印样品列表" class="btn" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>--%>
          <center><div id="dayin" style=" text-align:center">
            <asp:GridView ID="grdvw_List" runat="server" Width="750px" CssClass="mGridView"  ShowHeader="true"  OnRowCreated="grdvw_List_RowCreated">
                <Columns>
                    <%--<asp:TemplateField HeaderText="选择"></asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            </div>
            </center> 
             </td></tr>
 </tbody></table>
         
        </ContentTemplate>
        <%-- <Triggers>
       <asp:PostBackTrigger ControlID="btn_print" />
       </Triggers>--%>
    </asp:UpdatePanel>
    <%--<div id="detail" class="mLayer" style=" display:none;left: 96px; width: 739px; top: 500px; height: 130px">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <asp:Panel ID="Panel2" runat="server"  BackColor="#F5F9FF" Width="100%"  Font-Size="X-Small"
                    GroupingText="标签打印项选择">
                 <table style="width: 100%">
                    <tr>
                        <td style="height: 7px" colspan="3">
                          <asp:CheckBoxList ID="cbl_Item" runat="server"></asp:CheckBoxList>
                        </td>
                    </tr>
                     <tr><td style="height: 7px" colspan="3"><asp:Button runat="server" ID="btn_save"  OnClick="btn_save_OnClick"/></td></tr>
                </table>
                   
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>
</asp:Content>
