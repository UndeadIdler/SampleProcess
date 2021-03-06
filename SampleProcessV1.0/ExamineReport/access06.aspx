﻿<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="access06.aspx.cs" Inherits="ExamineReport_access06" Title="报告装订发放" %>

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
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption=""
                AllowPaging="true" OnPageIndexChanging="grdvw_List_PageIndexChanging" 
                OnRowCreated="grdvw_List_RowCreated"  OnRowEditing="grdvw_List_RowEditing" onselectedindexchanging="grdvw_List_SelectedIndexChanging" >
                <Columns>
                 <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
           
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="btn_Cancel" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <div id="detail" class="mLayer" style="display:none;left: 96px; width: 739px; top: 500px;
        height: 130px">
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
            <asp:Panel ID="Panel2" BackColor="#F5F9FF" runat="server" GroupingText="综合室业务外受理" ForeColor="#2292DD"
                    Width="800px">
                <table style="margin: 0px; width:100%">
                    <tbody>
                      
                        <tr> <td style="width: 10%; text-align: right" class="titleList">
                                <span style="font-size: 10pt">委托单位</span>
                            </td>
                            <td class="ctrlList" style="width: 40%;">
                                <asp:TextBox ID="txt_wtdw" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                             <td style="width: 10%; text-align: right" class="titleList">
                                <span style="font-size: 10pt">项目类型</span>
                            </td>
                            <td class="ctrlList" style="width: 40%;">
                                <asp:TextBox ID="txt_ItemName" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            </tr>
                             <tr> <td style="width: 10%; text-align: right" class="titleList">
                                <span style="font-size: 10pt">联系人</span>
                            </td>
                            <td class="ctrlList" style="width: 40%;">
                                <asp:TextBox ID="txt_man" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                             <td style="width: 10%; text-align: right" class="titleList">
                                <span style="font-size: 10pt">联系电话</span>
                            </td>
                            <td class="ctrlList" style="width: 40%;">
                                <asp:TextBox ID="txt_tel" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            </tr>
                        <tr>
                            <td style="width: 10%; text-align: right" class="titleList">
                                <span style="font-size: 10pt">受理人</span>
                            </td>
                            <td class="ctrlList" style="width: 40%;">
                                <asp:TextBox ID="txt_person0" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                           
                             <td style="width: 10%; text-align: right" class="titleList">
                                <span style="font-size: 10pt">受理时间</span></td>
                            <td style="width: 40%;" class="ctrlList">
                                <asp:TextBox ID="txt_AccessTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                           
                        </tr>
                        <tr>
                           
                            
                                <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" CssClass="mGridView" Caption=""
                                   >
                                    
                                </asp:GridView>
                           
                            </td>
                           
                        </tr>
                        </tbody>
                        </table>
                        </asp:Panel>
                         <asp:Panel ID="Panel1" BackColor="#F5F9FF" runat="server" GroupingText="任务确认" ForeColor="#2292DD"
                    Width="800px">
                       <table style="margin: 0px; width: 100%">
                        <tbody>
                        
                       <tr>
                        <td colspan="4">
                                <asp:GridView ID="GridView2" runat="server" CssClass="mGridView" Caption="" 
                                   > 
                                </asp:GridView>
                           
                            </td></tr>
                        
                    </tbody></table>
                    </asp:Panel>
                    <asp:Panel ID="Panel4" BackColor="#F5F9FF" runat="server" GroupingText="现场踏勘" ForeColor="#2292DD"
                    Width="800px">
                       <table style="margin: 0px; width: 100%">
                        <tbody>
                        
                       <tr>
                        <td colspan="4">
                                <asp:GridView ID="GridView3" runat="server" CssClass="mGridView" Caption="" 
                                   > 
                                </asp:GridView>
                           
                            </td></tr>
                        
                    </tbody></table>
                    </asp:Panel>
                     <asp:Panel ID="Panel3" BackColor="#F5F9FF" runat="server" GroupingText="函/验收方案编写" ForeColor="#2292DD"
                    Width="800px">
                       <table style="margin: 0px; width: 100%">
                        <tbody>
                        
                       <tr>
                        <td colspan="4">
                                <asp:GridView ID="GridView4" runat="server" CssClass="mGridView" Caption="" 
                                   > 
                                </asp:GridView>
                           
                            </td></tr>
                        
                    </tbody></table>
                    </asp:Panel>
                     <asp:Panel ID="Panel5" BackColor="#F5F9FF" runat="server" GroupingText="函/验收方案确认" ForeColor="#2292DD"
                    Width="800px">
                       <table style="margin: 0px; width: 100%">
                        <tbody>
                        
                       <tr>
                        <td colspan="4">
                                <asp:GridView ID="GridView5" runat="server" CssClass="mGridView" Caption="" 
                                   > 
                                </asp:GridView>
                           
                            </td></tr>
                        
                    </tbody></table>
                    </asp:Panel>
                     <asp:Panel ID="Panel6" BackColor="#F5F9FF" runat="server" GroupingText="函综合室外发/业务室完成监测" ForeColor="#2292DD"
                    Width="800px">
                       <table style="margin: 0px; width: 100%">
                        <tbody>
                        
                       <tr>
                        <td colspan="4">
                                <asp:GridView ID="GridView6" runat="server" CssClass="mGridView" Caption="" 
                                   > 
                                </asp:GridView>
                           
                            </td></tr>
                        
                    </tbody></table>
                    </asp:Panel>
                     <asp:Panel ID="Panel_now" BackColor="#F5F9FF" runat="server" GroupingText="监测数据移交" ForeColor="#2292DD"
                    Width="800px">
                       <table style="margin: 0px; width: 100%">
                        <tbody>
                         <tr>
                        <td colspan="4">
                                <asp:GridView ID="GridView_now" runat="server" CssClass="mGridView" Caption="" OnRowCreated="GridView1_RowCreated"> 
                                </asp:GridView>
                           
                            </td></tr>
                        <tr>
                           
                            <td style="width:10%; text-align: right" class="titleList">
                                <span style="font-size: 10pt">备注</span>
                            </td>
                            <td  colspan="3"  style="width:90%; ">
                                <asp:TextBox ID="txt_Remark_now" runat="server" Height="150px" TextMode="MultiLine" width="100%"></asp:TextBox>
                            </td>
                            
                           
                        </tr>
                        
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                          
                            
                            <td colspan="4" align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" CssClass="mButton"
                                    Text="取消"></asp:Button>
                                      <asp:Button ID="btn_back" OnClick="btn_back_Click" runat="server" CssClass="mButton"
                                    Text="退回"></asp:Button>
                                     <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" CssClass="mButton"
                                    Text="确定"></asp:Button>
                                <asp:Button ID="btn_submit" OnClick="btn_submit_Click" runat="server" CssClass="mButton"
                                    Text="提交"></asp:Button>
                            </td>
                            <td style="width: 62px" class="float_LeftAndRight">
                            </td>
                        </tr>
                        </tbody></table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_back" BackColor="#F5F9FF" runat="server" GroupingText="退回备注" ForeColor="#2292DD"
                    Width="800px">
                        <table style="margin: 0px; width: 800px">
                    <tbody>
                      
                         <tr>
                            
                            <td colspan="2">
                             <asp:GridView ID="GridView_back" runat="server" CssClass="mGridView" Caption="" >   
                                </asp:GridView>
                            </td>
                           
                           
                        </tr>
                        </tbody>
                        </table>
                        </asp:Panel>
            </ContentTemplate>
            
        </asp:UpdatePanel>
    </div>
</asp:Content>
