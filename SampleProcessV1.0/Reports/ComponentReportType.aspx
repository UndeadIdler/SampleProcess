<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ComponentReportType.aspx.cs" Inherits="Report_ComponentReportType" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js"></script>
    <%--<script language="javascript" type="text/javascript" src="../js/Calendar30.js"></script>--%>
     <script type="text/javascript">                
        var    hkey_root,hkey_path,hkey_key   
        hkey_root="HKEY_CURRENT_USER"   
        hkey_path="\\Software\\Microsoft\\Internet Explorer\\PageSetup\\"   
        //设置网页打印的页眉页脚为空   
      function   pagesetup_null()   
      {   
          try{   
              var RegWsh = new ActiveXObject("WScript.Shell")   
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
  code+="<input type='button' value='打印预览' style='border: solid 1px #a3a3a3; background-color: #e6e6e6; font-size: 12px' class='noprint' onclick='javascript:wb.execWB(7,1);' />&nbsp;&nbsp; <input type='button' value=' 打 印 ' style='border: solid 1px #a3a3a3; background-color: #e6e6e6; font-size: 12px' class='noprint' onclick='javascript:wb.execWB(6,1);alert(打印成功！);' /></center>"
  code+="<style media='print' type='text/css'> .Noprint{display:none;} .PageNext{page-break-after: always;} </style>"
  var newwin=window.open('','','');
  newwin.opener = null;
  newwin.document.write(code);
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
            <table class="container">
                <tbody> <tr>              
                   
                       
                        <td class="querytitleList"  style="width: 350px">                            
                        </td>
                        <td  width="80px" style="text-align:center;">
                           <asp:Label ID="Label1" runat="server"  Text="开始日期"></asp:Label>
                        </td>
                        <td class="querytitleList">
                             <asp:TextBox ID="txt_StartTime" runat="server" CssClass="mTextBox" style="text-align:center;vertical-align:middle"  Width="85px"></asp:TextBox>
                        </td>
                        <td  width="80px" style="text-align:center;">
                           <asp:Label ID="Label2" runat="server"  Text="结束日期" ></asp:Label>
                        </td>
                        <td class="querytitleList">
                            <asp:TextBox ID="txt_EndTime" runat="server" CssClass="mTextBox" style="text-align:center;vertical-align:middle"  Width="85px"></asp:TextBox>
                        </td>
                        <td class="queryctrlList" style="width: 90px">
                            <asp:Button ID="btn_CreateReport" runat="server" CssClass="btn" 
                                OnClick="btn_CreateReport_Click" Text="生成报表" Width="85px" />
                        </td>
                        </tr>
                         <tr> <td colspan='6' style=" height:15px"></td></tr>
                </tbody>
            </table>                     
             <%=strTable %>   <br>                 
            <div id="dayin" align="center" style="display:none;"><%=strTableP %></div> 
            <div align="center"> 
           <%-- <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="btn_ExportR_Click" Text="导出报表" />--%>
            <input type="button" onclick="dayin()" value="打印结果" class="btn" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
