﻿<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="drawSample.aspx.cs" Inherits="drawSample" Title="领取样品" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../js/position.js"></script>

    
    <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js"></script>
   
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
                        </td>
                        <td style="text-align: right; font-size: x-small;" class="titleList">
                            <span style="font-size: 10pt">分析项目</span>
                        </td>
                        <td style="height: 4px" class="ctrlList">
                            <asp:TextBox ID="txt_Itemquery" runat="server"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label4" runat="server" Text="时间"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_QueryTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td>
                            <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" CssClass="mButton" />
                        </td>
                        
                        <td style="height: 4px" class="LeftandRight">
                             <asp:Button ID="btn_Draw" runat="server" Text="领样" OnClick="btn_Draw_Click" CssClass="mButton" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView"  
             
               OnRowCreated="grdvw_List_RowCreated" OnRowDataBound="grdvw_List_RowDataBound" >
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="autoid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
