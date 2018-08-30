<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModifyPwd.aspx.cs" Inherits="BaseData_ModifyPwd" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <progresstemplate>
<table style="width: 321px">
<tr><td style="height: 7px" colspan="3">
<span style="font-size: 10pt">
<img src="../Images/minipro.gif" />通讯中，请稍等....</span></td></tr>
</table>
</progresstemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
<table style="width: 399px">
<tbody>
    <tr><td style="height: 29px" class="titleList" colspan="4">
        <span style="font-size: 10pt">密码修改</span></td>
     </tr>
     <tr><td style="width: 36px; height: 30px" class="Middle">
        </td>
     <td style="width: 122px; height: 30px; text-align: right" class="titleList">
        <span style="font-size: 10pt; text-align: left;">原密码：</span></td>
     <td style="width: 62px; height: 30px" class="ctrlList">
        <asp:TextBox id="txt_OrigPwd" runat="server" TextMode="Password" CssClass="mTextBox"></asp:TextBox></td>
         <td style="width: 85px; height: 30px" class="Middle"></td></tr>
     <tr><td style="width: 36px; height: 30px" class="Middle">
        </td>
        <td style="width: 122px; height: 30px; text-align: right" class="titleList">
        <span style="font-size: 10pt">新密码：</span></td><td style="width: 62px; height: 30px">
        <asp:TextBox id="txt_NewPwd" runat="server" TextMode="Password" CssClass="mTextBox"></asp:TextBox></td>
        <td style="width: 85px; height: 30px" class="Middle"></td></tr>
     <tr><td style="width: 36px; height: 29px" class="Middle">&nbsp;</td>
        <td style="width: 122px; height: 29px; text-align: right" class="titleList">
        <span style="font-size: 10pt">确认密码：</SPAN></td>
        <td style="width: 62px; height: 29px" class="ctrlList">
         <asp:TextBox id="txt_RetypePwd" runat="server" TextMode="Password" CssClass="mTextBox"></asp:TextBox></td>
         <td style="width: 85px; height: 29px" class="Middle"></td>
         </tr>
     <tr><td style="height: 6px" colspan="4"></td>
         </tr>     
     <tr><td colspan="4" align="center">
        <asp:Button id="btn_Cancel" onclick="btn_Cancel_Click" runat="server" CssClass="mButton" Text="清空">
        </asp:Button> <asp:Button id="btn_OK" onclick="btn_OK_Click" runat="server" CssClass="mButton" Text="确定"></asp:Button>
        </td>
        </tr>
</tbody></table>
</contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
