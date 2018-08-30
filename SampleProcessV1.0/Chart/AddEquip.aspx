<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="AddEquip.aspx.cs" Inherits="BaseData_AddEquip" Title="无标题页" %>

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
            
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" AllowPaging="true" PageSize="20"
                Caption="流程图" OnPageIndexChanging="grdvw_List_PageIndexChanging" OnRowEditing="grdvw_List_RowEditing"
                OnRowCreated="grdvw_List_RowCreated" 
                >
                <Columns>
                    <asp:BoundField></asp:BoundField>
                    <asp:BoundField></asp:BoundField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <%-- <asp:AsyncPostBacktrigger ControlID="drop_FirSca_Name" EventName="SelectedIndexChanged"></asp:AsyncPostBacktrigger>--%>
            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBackTrigger>
            <%-- <asp:AsyncPostBacktrigger ControlID="btn_Add" EventName="Click"></asp:AsyncPostBacktrigger>--%>
        </Triggers>
    </asp:UpdatePanel>
    <div class="mLayer" style="left: 223px; top: 543px; width: 363px; height: 150px;
        display: none;" id="detail">
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
                <table style="width: 357px" border="0">
                    <tbody>
                        <tr>
                            <td style="width: 100%; text-align: left" class="float_Middle" colspan="3">
                                <asp:Label ID="lbl_Type" runat="server" CssClass="mLabelTitle" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: left" colspan="3">
                                <asp:GridView ID="GridView1" runat="server" CssClass="mGridView"
                                    OnRowCreated="grdvbs_List_RowCreated" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" />
                                        <asp:BoundField DataField="name" HeaderText="名称" />
                                         <asp:BoundField DataField="x" HeaderText="长度" />
                                          <asp:BoundField DataField="y" HeaderText="宽度" />
                                           <asp:BoundField DataField="h" HeaderText="坐标x" />
                                            <asp:BoundField DataField="w" HeaderText="坐标y" />
                                             <asp:BoundField DataField="address" HeaderText="图片地址" />
                                       <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox" Text="" runat="server" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox" Text='<%# Eval("id")%>' runat="server" Width="100px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox" Text="" runat="server" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                               <asp:TextBox ID="TextBox" Text="" runat="server" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                              <asp:TextBox ID="TextBox" Text="" runat="server" Width="100px"></asp:TextBox>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>                            
                            <td colspan="3" align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" CssClass="mButton"
                                    Text="取消"></asp:Button>                        
                          
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" CssClass="mButton"
                                    Text="确定"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <%--<triggers>
    <asp:AsyncPostBacktrigger ControlID="btn_Add" EventName="Click"></asp:AsyncPostBacktrigger>
    </triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>
