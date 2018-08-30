<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="Depart.aspx.cs" Inherits="BaseData_Depart" Title="部门管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../js/position.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <table style="width: 321px">
                <tr>
                    <td style="height: 7px" colspan="3" rowspan="3">
                        <span style="font-size: 10pt">
                            <img src="../Images/minipro.gif" />通讯中，请稍等....</span>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                </tr>
            </table>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption="部门管理"
                AllowPaging="true" OnPageIndexChanging="grdvw_List_PageIndexChanging" OnRowEditing="grdvw_List_RowEditing"
                OnRowCreated="grdvw_List_RowCreated" OnRowDeleting="grdvw_List_RowDeleting">
                <Columns>
                 <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table class="container">
                <tbody>
                    <tr>
                        <td class="LeftandRight">
                        </td>
                        <td class="titleList">
                        </td>
                        <td class="ctrlList">
                        </td>
                        <td class="Middle">
                        </td>
                        <td>
                        </td>
                        <td class="LeftandRight">
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" Text="添加" CssClass="mButton">
                            </asp:Button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="btn_Cancel" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <div id="detail" class="mLayer" style="display: none; left: 96px; width: 739px; top: 500px;
        height: 130px">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
                <table style="width: 321px">
                    <tr>
                        <td style="height: 7px" colspan="3" rowspan="3">
                            <span style="font-size: 10pt">
                                <img src="../Images/minipro.gif" />通讯中，请稍等....</span>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                </table>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="margin: 0px; width: 735px">
                    <tbody>
                        <tr>
                            <td style="text-align: left" class="float_Middle" colspan="7">
                                <asp:Label ID="lbl_Type" runat="server" CssClass="mLabelTitle" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="width: 119px; text-align: right" class="titleList">
                                <span style="font-size: 10pt">部门名称：</span>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_DepartName" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 105px" class="float_Middle">
                            </td>
                            <td style="width: 121px; text-align: right" class="titleList">
                                <span style="font-size: 10pt"></span>
                            </td>
                            <td class="ctrlList">
                            </td>
                        </tr>
                        
                         <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="width: 119px; text-align: right" class="titleList">
                                <span style="font-size: 10pt">联系人：</span>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_Contact" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 105px" class="float_Middle">
                            </td>
                            <td style="width: 121px; text-align: right" class="titleList">
                                <span style="font-size: 10pt"></span>
                            </td>
                            <td class="ctrlList">
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="width: 119px; text-align: right" class="titleList">
                                <span style="font-size: 10pt">联系电话：</span>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_tel" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 105px" class="float_Middle">
                            </td>
                            <td style="width: 121px; text-align: right" class="titleList">
                                <span style="font-size: 10pt"></span>
                            </td>
                            <td class="ctrlList">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="width: 119px" class="titleList">
                            </td>
                            <td class="ctrlList">
                            </td>
                            <td style="width: 105px" class="float_Middle">
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" CssClass="mButton"
                                    Text="取消"></asp:Button>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" CssClass="mButton"
                                    Text="确定"></asp:Button>
                            </td>
                            <td style="width: 62px" class="float_LeftAndRight">
                            </td>
                        </tr>
                    </tbody></table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Add" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
