<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ZKSample1.aspx.cs" Inherits="ZKSample1" Title="质控统计" %>

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
                            <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" TabIndex="0" CssClass="mButton" />
                        </td>
                        <td> <asp:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_ExportR_Click" TabIndex="0" CssClass="mButton" /></td>
                    </tr>
                </tbody>
            </table>
             
              
        </ContentTemplate>
    </asp:UpdatePanel>
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
       <asp:Panel ID="Panel1"  BackColor="#F5F9FF" runat="server" GroupingText="质控统计" TabIndex="0" ForeColor="#2292DD" Font-Size="X-Small"
                    Width="100%">
             <asp:GridView ID="grdvw_ZKList" runat="server" CssClass="mGridView" Width="100%"  Caption="监测分析质量统计表" OnRowCreated="grdvw_ZKList_RowCreated" OnRowDataBound="grdvw_ZKList_RowDataBound" >
                <Columns>                            
                </Columns>
            </asp:GridView>
                 </asp:Panel>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
