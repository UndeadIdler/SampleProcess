<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true" CodeFile="roleManagement.aspx.cs" Inherits="BaseData_roleManagement" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../js/position.js"></script>
<script type="text/javascript">
function postBackByObject()
{
    var o = window.event.srcElement;
    if (o.tagName == "INPUT" && o.type == "checkbox")
    {
        __doPostBack("","");
    } 
 }
</script>

    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <progresstemplate>
<table style="width: 321px">
<tr>
<td style="height: 7px" colspan="3">
<span style="font-size: 10pt">
<img src="../Images/minipro.gif" />通讯中，请稍等....</span></td></tr></table>
</progresstemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
<table class="container">
<tbody>
<tr>
<td style="height: 4px; text-align: right" class="titleList">
<span style="font-size: 10pt">操作员级别：</span>
</td>
<td style="height: 4px" class="ctrlList">
<asp:DropDownList id="drop_UserLevel" runat="server" CssClass="mDropDownList" OnSelectedIndexChanged="drop_UserLevel_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>
</td>
<td style="height: 4px" class="LeftandRight"></td>
<td style="height: 4px" class="Middle"></td>
<td style="height: 4px" class="titleList"></td>
<td style="height: 4px" class="ctrlList"></td>
<td style="height: 4px" class="LeftandRight"></td>
</tr>
</tbody>
</table>
<asp:GridView id="grdvw_List" runat="server" CssClass="mGridView"  Caption="角色列表" 
                AllowPaging="True" OnPageIndexChanging="grdvw_List_PageIndexChanging" 
                OnRowEditing="grdvw_List_RowEditing" OnRowCreated="grdvw_List_RowCreated" 
                OnRowDeleting="grdvw_List_RowDeleting" OnSelectedIndexChanging="grdvw_List_RowSelecting"><Columns>

        <asp:TemplateField HeaderText="序号"></asp:TemplateField>
</Columns>
</asp:GridView>
 <table class="container">
 <tbody>
 <tr>
 <td colspan="6" align="center">
 <asp:Button id="btn_Add" onclick="btn_Add_Click" runat="server" Text="添加" CssClass="mButton"></asp:Button></td>
 </tr></tbody></table>
</contenttemplate>
        <triggers>
<asp:AsyncPostBacktrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBacktrigger>
<asp:AsyncPostBacktrigger ControlID="btn_Cancel" EventName="Click"></asp:AsyncPostBacktrigger>
</triggers>
    </asp:UpdatePanel>
    <div id="detail" class="mLayer" style="display: none; left: 96px; width: 739px;
        top:200px; height: 130px">
        <asp:UpdateProgress id="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <progresstemplate>
<table style="width: 321px">
<tr>
<td style="height: 7px" colspan="3">
<span style="font-size: 10pt">
<img src="../images/minipro.gif" />通讯中，请稍等....</span></td></tr></table>
</progresstemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel id="UpdatePanel2" runat="server">
            <contenttemplate>
<table style="margin: 0px; width: 735px" border="0" >
<tbody>
    <tr>
        <td style="text-align: left" class="float_Middle" colspan="7">
        <asp:Label id="lbl_Type" runat="server" CssClass="mLabelTitle" Text="Label"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 48px" class="float_Middle"></td>
        <td style="width: 119px; text-align: right" class="titleList">
        <span style="font-size: 10pt">
            <asp:Label ID="lbl_Role_Title" runat="server" Text=" 角色名称:"></asp:Label></span></td>
        <td class="ctrlList">
        <asp:TextBox id="txt_roleName" runat="server" CssClass="mTextBox"></asp:TextBox></td>
        <td style="width: 105px" class="float_Middle"></td>
        <td style="width: 121px; text-align: right" class="titleList"></td>
        <td class="ctrlList">
        </td>
        <td style="width: 62px" class="float_LeftAndRight"></td>
    </tr>
    <tr><td colspan="7" class="titleList" style=" font:10pt; font-weight:bold; background-color:#E3EFFF">
    <table width="100%"><tr><td style="font-weight:bold;font:10pt">&nbsp;&nbsp;&nbsp;&nbsp;基本权限设置</td></tr></table></td></tr>
    <tr>
        <td style="width: 48px; height: 3px" class="float_Middle"></td>
        <td style="width: 119px; height: 3px;  font:10pt" class="ctrlList">
            <asp:CheckBox ID="CheckBox_readwrite" runat="server" Text="读写权限" /></td>
        <td style="height: 3px; width: 119px; font:10pt; text-align:right;" class="ctrlList">
            <asp:CheckBox ID="CheckBox_reflash" runat="server" Text="刷新权限" /></td>
        <td style="width: 121px; height: 3px;font:10pt" class="ctrlList">
            <asp:CheckBox ID="CheckBox_set" runat="server" Text="设参权限"/></td>
        <td style="width: 121px; height: 3px;font:10pt; text-align:right;" class="ctrlList">
       <asp:CheckBox ID="CheckBox_control" runat="server"  Text="控制权限"/>
        </td>
         <td style="width: 121px; height: 3px;font:10pt; text-align:right;" class="ctrlList">
       <asp:CheckBox ID="CheckBox_Data" runat="server"  Text="数据权限"/>
        </td>
        <td style="width: 119px; height: 3px; text-align:right; font:10pt" class="titleList">
            <asp:Label ID="Label2" runat="server" Text="刷新时间" /></td>
        <td style="height: 3px" class="ctrlList">
            <asp:TextBox ID="TextBox_reflashTime" runat="server"></asp:TextBox> </td>
    </tr>
  <tr><td colspan="7" align="center">
      <asp:CheckBoxList ID="cbl_itemlist" RepeatColumns="4" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>

      </td></tr>
    <tr>        
        <td colspan="7" align="center">
        <asp:Button id="btn_Cancel" onclick="btn_Cancel_Click" runat="server" CssClass="mButton" Text="取消"></asp:Button> 
        <asp:Button id="btn_OK" onclick="btn_OK_Click" runat="server" CssClass="mButton" Text="确定"></asp:Button>
        </td>        
    </tr>
</tbody></table>
</contenttemplate>
       <triggers>
<asp:AsyncPostBacktrigger ControlID="btn_Add" EventName="Click"></asp:AsyncPostBacktrigger>
</triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

