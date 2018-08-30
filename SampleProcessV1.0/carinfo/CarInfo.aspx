<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CarInfo.aspx.cs" Inherits="carinfo_CarInfo" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../js/position.js"></script>
 <%--<script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js" ></script>--%>
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
    <div class="mLayer" style="left: 200px; top: 200px; width: 363px; height: 150px;display:none;" id="detail">
     <table style="width: 100%">
                <tr>
                    <td style="width: 60%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/Delete.gif" onclick="hiddenDetail()" />
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
            <ContentTemplate> <h2 style="padding-right: 0px; padding-left: 8px; font-size: 14px; background: #e2f3fa;
                            padding-bottom: 0px; margin: 0px; color: #0066a9; line-height: 24px; padding-top: 0px;
                            border-bottom: #a4d5e3 1px solid">
                          <asp:Label ID="lbl_Type" runat="server"  Text="Label" CssClass="mLabelTitle"></asp:Label></h2>
                        <div style="margin: 5px">
                <table style="width: 357px">
                    <tbody>                 
                        <tr>
                            <td style="width: 38px" class="Middle"></td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label1" runat="server" Text="车牌号：" CssClass="mLabel" ></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_CarNO" runat="server" CssClass="mTextBox"></asp:TextBox>                               
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38px" class="Middle"></td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label2" runat="server" Text="限载人数：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_Num" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr><td style="height: 4px" class="float_Middle">
                            </td>
                            <td class="float_Padding" colspan="2" align="center">
                                <%--<asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" 
                                    Text="取消" CssClass="mButton"></asp:Button>--%>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server"
                                    Text="确定" CssClass="mButton"></asp:Button>
                            </td>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                        </tr>
                    </tbody>
                </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Add" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>


