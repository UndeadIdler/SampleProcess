       
<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ComponentReportMonitor2.aspx.cs" Inherits="Report_ComponentReportMonitor2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js"></script>
<%--<script language="javascript" type="text/javascript" src="../js/Calendar30.js"></script>--%>
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
                <tbody> <tr>              
                   
                       
                        <td class="querytitleList"  style="width: 350px">                            
                        </td>
                        <td  width="80px" style="text-align:center;">
                           <asp:Label ID="Label1" runat="server"  Text="开始日期"></asp:Label>
                        </td>
                        <td class="querytitleList">
                             <asp:TextBox ID="txt_StartTime" runat="server" CssClass="mTextBox" style="text-align:center;vertical-align:middle"  Width="85px"></asp:TextBox>
                        </td>
                        <td  width="80px" style="text-align:center;">
                           <asp:Label ID="Label2" runat="server"  Text="结束日期" ></asp:Label>
                        </td>
                        <td class="querytitleList">
                            <asp:TextBox ID="txt_EndTime" runat="server" CssClass="mTextBox" style="text-align:center;vertical-align:middle"  Width="85px"></asp:TextBox>
                        </td>
                        <td class="queryctrlList" style="width: 90px">
                            <asp:Button ID="btn_CreateReport" runat="server" CssClass="btn" 
                                OnClick="btn_CreateReport_Click" Text="查询" Width="85px" />
                             <asp:Button ID="btn_print" runat="server" CssClass="btn" 
                                OnClick="btn_ExportR_Click" Text="导出" Width="85px"  />
                        </td>
                        </tr>
                         <tr> <td colspan='6' style=" height:15px"></td></tr>
                </tbody>
            </table>                  
                 <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView"  OnRowCreated="grdvw_List_RowCreated">
                <Columns>
                    
                    
                </Columns>
            </asp:GridView>       
           
           <%-- <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="btn_ExportR_Click" Text="导出报表" />--%>
            
            </div>
        </ContentTemplate>
         <Triggers>
       <asp:PostBackTrigger ControlID="btn_print" />
       </Triggers>
       
    </asp:UpdatePanel>
</asp:Content>
