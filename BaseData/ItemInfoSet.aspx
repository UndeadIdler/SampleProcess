<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ItemInfoSet.aspx.cs" Inherits="BaseData_ItemInfoSet" Title="Untitled Page"
    StylesheetTheme="Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script language="javascript" type="text/javascript" src="../js/position.js"></script>
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
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView"
                 OnPageIndexChanging="grdvw_List_PageIndexChanging"
                OnRowDeleting="grdvw_List_RowDeleting" OnRowEditing="grdvw_List_RowEditing" OnRowCreated="grdvw_List_RowCreated">
                <Columns>
                 <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="autoid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField></asp:BoundField>
                    <asp:BoundField></asp:BoundField>
                </Columns>
            </asp:GridView>
            <table class="container">
                <tbody>
                    <tr>
                        <td align="center"><asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" 
                                CssClass="mButton" Text="添加" />
                        </td>
                        
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>      
    </asp:UpdatePanel>
    <div class="mLayer" style="left: 223px; top: 543px; width: 363px; height: 150px;        display: none;" id="detail">
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
                <table style="width: 357px">
                    <tbody>
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                <asp:Label ID="lbl_Type" runat="server"  Text="Label" CssClass="mLabelTitle"></asp:Label>
                            </td>
                        </tr>                      
                        <tr>
                            <td style="width: 38px" class="Middle"></td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label1" runat="server" Text="项目类型：" CssClass="mLabel" ></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_ItemName" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38px" class="Middle"></td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label2" runat="server" Text="项目代码：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_ItemCode" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td class="float_Padding" colspan="4"  align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" 
                                    Text="取消" CssClass="mButton"></asp:Button>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server"
                                    Text="确定" CssClass="mButton"></asp:Button>
                            </td>
                           
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Add" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
