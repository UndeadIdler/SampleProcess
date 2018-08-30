<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="access.aspx.cs" Inherits="ExamineReport_access" Title="综合室业务受理" %>

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
                OnRowCreated="grdvw_List_RowCreated" 
                OnRowDeleting="grdvw_List_RowDeleting" OnRowEditing="grdvw_List_RowEditing" 
                onselectedindexchanging="grdvw_List_SelectedIndexChanging" >
                <Columns>
                 <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table class="container">
                <tbody>
                    <tr>
                        <td class="LeftandRight">
                        </td>
                        <td class="titleList">
                        </td>
                        <td class="ctrlList">
                        </td>
                        <td class="Middle">
                        </td>
                        <td>
                        </td>
                        <td class="LeftandRight">
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" Text="添加" CssClass="mButton">
                            </asp:Button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="btn_Cancel" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <div id="detail" class="mLayer" style="display:none;left: 96px; width: 739px; top: 200px;
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
             <asp:Panel ID="Panel2" BackColor="#F5F9FF" runat="server" GroupingText="综合室业务受理" ForeColor="#2292DD"
                    Width="800px">
                  <table style="margin: 0px; width: 735px">
                    <tbody>
                       <tr>
                            <td style="text-align: left" class="float_Middle" colspan="2">
                                <asp:Label ID="lbl_Type" runat="server" CssClass="mLabelTitle" Text="Label"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                           
                            <td style="text-align: right; width:10%" class="titleList">
                                <span style="font-size: 10pt">协议编号</span>
                            </td>
                            <td style=" width:90%" colspan="5">
                                <asp:TextBox ID="txt_NO" runat="server" CssClass="mTextBox" Height="20px" 
                                    width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        </tbody>
                        </table>

               <asp:Panel ID="Panel1" BackColor="#F5F9FF" runat="server" GroupingText="委托单位信息" ForeColor="#2292DD"
                    Width="800px">
                <table style="margin: 0px; width: 735px">
                    <tbody>
                        
                        
                         <tr>
                           
                            <td style="text-align: right; width:10%" class="titleList">
                                <span style="font-size: 10pt">委托单位</span>
                            </td>
                            <td style=" width:90%" colspan="5">
                                <asp:TextBox ID="txt_wtdw" runat="server" CssClass="mTextBox" Height="20px" 
                                    width="100%"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                           
                            <td style="text-align: right; width:10%" class="titleList">
                                <span style="font-size: 10pt">单位地址</span>
                            </td>
                            <td style=" width:90%"  colspan="5">
                                <asp:TextBox ID="txt_address" runat="server" CssClass="mTextBox" Height="20px" 
                                    width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="titleList">
                                <span style="font-size: 10pt">联系人</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_man" runat="server" CssClass="mTextBox" Height="20px"  
                                    width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: right;"class="titleList">
                                <span style="font-size: 10pt">联系电话</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_tel" runat="server" CssClass="mTextBox" Height="20px" 
                                    width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: right;" class="titleList">
                                <span style="font-size: 10pt">邮箱</span>
                            </td>
                            <td >
                                <asp:TextBox ID="txt_Email" runat="server" CssClass="mTextBox" Height="20px" 
                                     width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        </tbody>
                        </table>
                      </asp:Panel>
                         <asp:Panel ID="Panel3" BackColor="#F5F9FF" runat="server" GroupingText="监测要求" ForeColor="#2292DD"
                    Width="800px">
                 
                <table style="margin: 0px; width: 735px">
                    <tbody>
                            <tr>
                           
                            <td style="text-align: right; " class="titleList">
                                <span style="font-size: 10pt">监测目的</span>
                            </td>
                            <td >
                                <asp:DropDownList ID="drop_ItemName" runat="server"></asp:DropDownList>
                              <%-- <asp:TextBox ID="txt_ItemName" runat="server" CssClass="mTextBox" Height="20px" 
                                    width="100%"></asp:TextBox>--%>
                            </td>
                                
                            <td style="text-align: right; " class="titleList">
                                <span style="font-size: 10pt">监测方式及要求说明</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drop_class" runat="server"></asp:DropDownList>
                               <asp:TextBox ID="txt_bremark" runat="server" CssClass="mTextBox" Height="20px" 
                                   ></asp:TextBox>
                            </td>
                        </tr>
                       
                         <tr>
                           
                            <td style="text-align: right;" class="titleList">
                                <span style="font-size: 10pt">监测内容</span>
                            </td>
                            <td colspan="3">
                                <asp:CheckBoxList ID="cb_ItemList" runat="server" RepeatDirection="Horizontal"  RepeatColumns="4"></asp:CheckBoxList>
                                
                            </td>
                        </tr>
                         <tr>
                           
                           
                        </tr>
                       
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" CssClass="mGridView" Caption="" OnRowCreated="GridView1_RowCreated"
                                   >
                                   <%--<Columns>
                 <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>--%>
                                    
                                </asp:GridView>
                            </td></tr>
                        <tr>
                            <td style="text-align: right; width:10%" class="titleList">
                                <span style="font-size: 10pt">备注</span>
                            </td>
                            <td style=" width:90%" colspan="3">
                                
                                <asp:TextBox ID="txt_Remark" runat="server" Height="150px" TextMode="MultiLine" width="100%"></asp:TextBox>
                            </td>
                           
                           
                        </tr>
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                           
                            <td align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" CssClass="mButton"
                                    Text="取消"></asp:Button>
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
                                <%--<span style="font-size: 10pt">备注</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Remark1" runat="server" TextMode="MultiLine" width="80%" 
                                    ReadOnly="true" style="margin-left: 1px"></asp:TextBox>--%>
                            </td>
                           
                           
                        </tr>
                        </tbody>
                        </table>
                        </asp:Panel>
                 </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Add" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
