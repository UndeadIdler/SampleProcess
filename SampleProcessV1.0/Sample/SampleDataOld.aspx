<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="SampleDataOld.aspx.cs" Inherits="SampleDataOld" Title="分析登记" %>

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
                            <span style="font-size: 10pt">样品编号</span>
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
                            <%--<asp:CheckBoxList ID="CheckBoxList1" runat="server">
                            </asp:CheckBoxList>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" OnPageIndexChanging="grdvw_List_PageIndexChanging" AllowPaging="true" PageSize="12" 
               OnRowCreated="grdvw_List_RowCreated" OnRowEditing="grdvw_List_RowEditing" >
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
   <div id="DetailAnalysis" class="mLayer" style="display: none; position:relative; left: 0px; width: 800px; top: 0px;" >
        <table style="width: 100%">
                <tr>
                    <td style="width: 60%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="unshowAddEditAnalysis()" />
                    </td>
                </tr>
            </table>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" BackColor="#F5F9FF" runat="server" GroupingText="分析项列表" ForeColor="#2292DD"
                    Width="800px">
                    
                    <asp:GridView ID="grdvw_ReportDetail" runat="server" CssClass="mGridView" Caption="" OnRowDataBound="grdvw_ReportDetail_RowDataBound"
                        AllowPaging="True" OnPageIndexChanging="grdvw_ReportDetail_PageIndexChanging"
                        OnSelectedIndexChanging="grdvw_ReportDetail_RowSelecting" OnRowEditing="grdvw_ReportDetail_RowEditing" OnRowCreated="grdvw_ReportDetail_RowCreated"
                       >
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table class="container">
                        <tbody>
                            <tr><td>
                            <asp:Label ID="Label1" runat="server" Text="校核人"></asp:Label></td><td>
                                <asp:TextBox ID="txt_jhman" runat="server"></asp:TextBox></td><td> <asp:Label ID="lbl_jhdate" runat="server" Text="校核时间"></asp:Label></td><td><asp:TextBox ID="txt_jhdate" runat="server"></asp:TextBox></td><td></td></tr>
                           <tr>
                           <td colspan="7"> </td>
                                <td align="right" >
                               
                                    <asp:Button ID="btn_Commit" OnClick="btn_Commit_Click" runat="server" Text="提交分析报告"
                                        CssClass="mButton"></asp:Button>
                                  
                                </td>
                            </tr>
                           <%-- <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            </asp:RadioButtonList>--%>
                        </tbody>
                    </table>
                </asp:Panel>
        
            </ContentTemplate>

               
        </asp:UpdatePanel>
        </div>
</asp:Content>
