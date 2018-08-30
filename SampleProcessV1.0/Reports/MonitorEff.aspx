       
<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="MonitorEff.aspx.cs" Inherits="Report_MonitorEf" Title="监测项耗时统计" %>

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
                   
                       
                       
                    <td  width="80px" style="text-align:center;">
                           <asp:Label ID="Label5" runat="server"  Text="样品类型"></asp:Label>
                        </td>
                        <td class="querytitleList" width="200px">
                            <asp:CheckBoxList ID="cbl_sampleType" runat="server" RepeatDirection="Horizontal" Width="150px" CssClass="mLabel">
                                <asp:ListItem Text="废水" value="1"></asp:ListItem>
                                <asp:ListItem Text="地表水" value="10"></asp:ListItem>
                                <asp:ListItem Text="地下水" value="11"></asp:ListItem>
                                 <asp:ListItem Text="降水" value="12"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    <%-- <td  width="80px" style="text-align:center;">
                           <asp:Label ID="Label4" runat="server"  Text="实验员"></asp:Label>
                        </td>
                    
                        <td class="querytitleList">
                             <asp:DropDownList runat="server" ID="drop_man"></asp:DropDownList>
                        </td>--%>
                    
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
                    
                        <tr><td><asp:CheckBox ID="cbl_all" runat="server" Text="全选" OnCheckedChanged="cbl_all_CheckedChanged" AutoPostBack="true" /></td>
                            
                       <td  colspan="5">
                        <asp:CheckBoxList ID="cbl_man" runat="server" OnSelectedIndexChanged="cbl_man_SelectedIndexChanged" RepeatColumns="6" RepeatDirection="Horizontal" CssClass="mLabel" AutoPostBack="true"></asp:CheckBoxList>
                      
                          <%-- <td class="querytitleList"  colspan="2">
                        <asp:CheckBoxList ID="cbl_man_chose" runat="server" RepeatDirection="Horizontal" Width="150px"  CssClass="mLabel">
                            <asp:ListItem Text="按监测项分类统计"></asp:ListItem>
                            
                        </asp:CheckBoxList>
                        </td>--%>

                        </tr>
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
