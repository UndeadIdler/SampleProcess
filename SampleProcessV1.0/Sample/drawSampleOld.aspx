<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="drawSampleOld.aspx.cs" Inherits="drawSampleOld" Title="领取样品" %>

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
                            <%--<asp:CheckBoxList ID="CheckBoxList1" runat="server">
                            </asp:CheckBoxList>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" OnPageIndexChanging="grdvw_List_PageIndexChanging" AllowPaging="true" PageSize="12"
             OnRowDataBound="grdvw_List_RowDataBound"
               OnRowCreated="grdvw_List_RowCreated" OnRowEditing="grdvw_List_RowEditing" OnSelectedIndexChanging="grdvw_List_RowSelecting">
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
     <div class="mLayer" style="left: 223px; top: 200px;  width:400px; height:150px;
        display:none;" id="detail">
         <table style="width: 202%; height: 23px;">
                <tr>
                    <td style="width: 90%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="hiddenDetail()" />
                    </td>
                </tr>
            </table>
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
                <table style="width: 400px">
                    <tbody>    
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                <asp:Button ID="btn_draw" runat="server" Text="新建领样" OnClick="btn_draw_OnClick" />
                             <asp:GridView ID="grdvw_drawList" runat="server" CssClass="mGridView" Caption=""   OnRowEditing="grdvw_drawList_RowEditing" OnRowCreated="grdvw_drawList_RowCreated" OnRowDeleting="grdvw_drawList_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                               
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="mLayer" style="display:none; width:400px;left: 223px;top:350px" id="Add">
        <table style="width: 202%; height: 21px;">
                <tr>
                    <td style="width: 90%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="unshowDraw()" />
                    </td>
                </tr>
            </table>
         <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
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
                <table style="width: 400px">
                    <tbody>    
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                              <asp:GridView ID="grv_itemlist" runat="server" CssClass="mGridView"  OnRowDataBound="grv_itemlist_RowDataBound"
               OnRowCreated="grv_itemlist_RowCreated" >
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                        <asp:CheckBox runat="server" ID="cb_id"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                           
                                <td>
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
   
    <div class="mLayer" style="left: 223px; top: 543px; width: 363px; height: 150px;
        display: none;" id="DetailAnalysis">
        <table style="width: 224%; height: 23px;">
                <tr>
                    <td style="width: 90%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="hiddenDetailAnalysis()" />
                    </td>
                </tr>
            </table>
         <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table style="width: 400px">
                    <tbody>    
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                <asp:GridView ID="grdvw_ReportDetail" runat="server" CssClass="mGridView" Caption=""
                        OnRowEditing="grdvw_ReportDetail_RowEditing" OnRowCreated="grdvw_ReportDetail_RowCreated"
                       >
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
