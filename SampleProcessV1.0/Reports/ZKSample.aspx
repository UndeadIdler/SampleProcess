       
<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ZKSample.aspx.cs" Inherits="Report_ZKSample" Title="Untitled Page" %>

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
                     <td style="text-align: right; font-size: x-small;" class="titleList">
                            <span style="font-size: 10pt">分析项目</span>
                        </td>
                        <td style="height: 4px" class="ctrlList">
                            <asp:TextBox ID="txt_Itemquery" runat="server"></asp:TextBox>
                        </td>
                        <td  width="80px" style="text-align:center;">
                           <asp:Label ID="Label1" runat="server"  Text="日期"></asp:Label>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_QueryTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                        <td class="queryctrlList" style="width: 90px">
                            <asp:Button ID="btn_CreateReport" runat="server" CssClass="btn" 
                                OnClick="btn_CreateReport_Click" Text="查询" Width="85px" />
                         </td>   <td>
                             <asp:Button ID="btn_print" runat="server" CssClass="btn"  
                                OnClick="btn_ExportR_Click" Text="导出" Width="85px" />
                        </td>
                        </tr>
                         <tr> <td colspan='6' style=" height:15px"></td></tr>
                </tbody>
            </table>                  
                 <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption="监测分析质量统计表"  Width="100%" OnRowCreated="grdvw_List_RowCreated">
                <Columns>
                      
                    
                </Columns>
            </asp:GridView>       

            </div>
        </ContentTemplate>
       
              <Triggers>
       <asp:PostBackTrigger ControlID="btn_print" />
       </Triggers>
       
    </asp:UpdatePanel>
    
</asp:Content>
