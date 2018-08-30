<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="AnalysisItemSet2.aspx.cs" Inherits="BaseData_AnalysisItemSet2" Title="分析项目维护"
    StylesheetTheme="Default" %>
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
            <table class="container">
                <tbody>
                    <tr>
                       
                        <td style="height: 4px" class="LeftandRight">
                            <asp:Label ID="lbl_ThrScaTil_Name" runat="server" CssClass="mLabel" Text="分析项目："
                                Width="100px"></asp:Label>
                        </td>
                        <td style="height: 4px" class="titleList">
                            <asp:TextBox ID="txt_AIName_S" runat="server" CssClass="mTextBox" Width="100px" OnTextChanged="txt_AIName_S_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="height: 4px;" class="ctrlList">
                            <asp:Button ID="btn_Query" runat="server" CssClass="btn" OnClick="btn_Query_Click"
                                Text="查询" />
                        </td>
                        <td style="width: 50px">
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" CssClass="btn" Text="添加" />
                        </td>
                        <td style="width: 180px">
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" PageSize="20" AllowPaging="true" OnPageIndexChanging="grdvw_List_PageIndexChanging"
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
                        <td align="center">
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="mLayer" style="left: 200px; top: 200px; width:780px; height: 150px;  display: none;" id="detail">
        
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
                <table style="width: 780px">
                    <tbody> 
                         <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                <asp:Label ID="lbl_Type" runat="server" Text="Label" CssClass="mLabelTitle"></asp:Label>
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label1" runat="server" Text="分析项目：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_AIName" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                           
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label2" runat="server" Text="分析项目代码：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_AICode" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                      <%-- <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label4" runat="server" Text="大致消耗时间：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_num" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                                 <asp:RadioButtonList ID="rbl_dw" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem value="0" Text="小时" Selected="true"></asp:ListItem>
                                     <asp:ListItem value="1" Text="分钟"></asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </td>
                        </tr>
                 <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label5" runat="server" Text="备注：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList" colspan="3">
                                <asp:TextBox ID="txt_Remark" runat="server" CssClass="mTextBox" Width="600px"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                                
                                
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                            <td class="float_Padding" colspan="4" align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" Text="取消" CssClass="mButton">
                                </asp:Button>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" Text="确定" CssClass="mButton">
                                </asp:Button>
                            </td>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
