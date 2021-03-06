﻿<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ItemInfoSet.aspx.cs" Inherits="BaseData_ItemInfoSet" Title="分析项目维护"
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
                        
                        <td class="queryctrlList" style="width: 150px">
                            <asp:Label ID="Label7" runat="server" Text="项目类型：" Width="150px" Style="text-align: center"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropList_type_Query" runat="server" CssClass="mTextBox" Width="100px"
                                
                                Height="22px">
                                

                            </asp:DropDownList>
                        </td>
                        
                        <td style="height: 4px;" class="ctrlList">
                            <asp:Button ID="btn_Query" runat="server" CssClass="btn" OnClick="btn_Query_Click"
                                Text="查询" />
                        </td>
                        <td align="center"><asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" 
                                CssClass="mButton" Text="添加" />
                        </td>
                        <td style="width: 180px">
                        </td>
                    </tr>
                </tbody>
            </table>            
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
           
        </ContentTemplate>      
    </asp:UpdatePanel>
    <div class="mLayer" style="left: 223px; top: 143px; width: 363px; height: 150px;        display: none;" id="detail">
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
                             <td class="titleList" style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label11" runat="server" Text="项目类型" CssClass="mLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drop_rwtype" runat="server" CssClass="ctrlList" Width="125px">
                                   
                                </asp:DropDownList>
                            </td>
                             </tr>                      
                        <tr> <td style="width: 38px" class="Middle"></td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label1" runat="server" Text="项目名称：" CssClass="mLabel" ></asp:Label>
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
