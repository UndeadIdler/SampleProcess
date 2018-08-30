<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="OUTNOItemParam.aspx.cs" Inherits="BaseData_OUTNOItemParam" Title="监测点污染物指标对应"
    StylesheetTheme="Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <div class="mLayer" style="left: 223px; top: 200px; width: 650px; height: 150px;display:none;" id="detail">
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
                <table style="width: 850px">
                    <tbody>
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                <asp:Label ID="lbl_Type" runat="server"  Text="Label" CssClass="mLabelTitle"></asp:Label>
                            </td>
                        </tr>                      
                        <tr>
                            <td  class="ctrlList">
                                <asp:Label ID="Label1" runat="server" Text="采样点：" CssClass="mLabel" ></asp:Label>
                            </td>
                            <td class="ctrlList">
                                 <asp:TextBox ID="txt_SampleSource" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="185px"></asp:TextBox>                   
                            </td>
                            <td class="ctrlList">
                                <asp:Label ID="Label2" runat="server" Text="污染物类型：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                 <asp:DropDownList ID="drop_wrw" runat="server" CssClass="mDropDownList" Width="98%" OnSelectedIndexChanged="drop_wrw_SelectedIndexChanged" AutoPostBack="true">
                                 </asp:DropDownList>  
                               </td>
                          
                            </tr>
                         <tr><td style="height: 4px" class="float_Middle">
                            </td>
                            <td class="float_Padding" colspan="2" align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" 
                                    Text="取消" CssClass="mButton"></asp:Button>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server"
                                    Text="确定" CssClass="mButton"></asp:Button>
                            </td>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                        </tr>
                        <tr><td colspan="4"><asp:GridView ID="grv_Item" runat="server" CssClass="mGridView" Caption="分析项目" Width="100%"
                         OnRowCreated="grv_Item_RowCreated" OnRowEditing="grv_Item_RowEditing" OnRowDeleting="grv_Item_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>  </td></tr>
                        <tr>
                         <td class="ctrlList" style="width:100px">
                             <asp:LinkButton ID="lbtn_chose" runat="server" OnClick="lbtn_chose_OnTextChanged">添加配置</asp:LinkButton>
                            </td>
                            
                        </tr>
                        <asp:Panel ID="panel_Item"  BackColor="#F5F9FF" runat="server" GroupingText="" ForeColor="#2292DD" Font-Size="X-Small" Visible="false"
                    Width="735px">
                        <tr><td> <asp:Label ID="Label3" runat="server" Text="说明：" CssClass="mLabel"></asp:Label></td><td colspan="7">
                            <asp:TextBox ID="txt_Remark" runat="server" CssClass="mTextBox"   Text="" Height="25px" 
                                    Width="100%"></asp:TextBox>
                            </td></tr>
                            <tr>
                            <td>
 <asp:Label ID="Label4" runat="server" Text="监测项：" CssClass="mLabel"></asp:Label>
                            </td><td colspan="7"><asp:TextBox ID="txt_Item" runat="server" CssClass="mTextBox" Visible="false"  Text="" Height="25px"  OnTextChanged="txt_Item_OnTextChanged" AutoPostBack="true"
                                    Width="100%"></asp:TextBox><%--<asp:HiddenField ID="hid_Item" runat="server"  Value="" />
                                <asp:HiddenField ID="hid_ID" runat="server" Value="" /> --%>  </td>
                            </tr>
                            <tr>
                               
                        <td class="ctrlList" colspan="9"> 
                              
                            </td>                          
                        </tr>
                             <tr><td class="ctrlList" colspan="9">
                                  
                                     <table width="100%">
                                      <tr><td> <asp:CheckBox ID="cb_cg" runat="server" OnCheckedChanged="cb_cg_CheckedChanged" AutoPostBack="true" /></td><td class="ctrlList" colspan="8">   
                                 <asp:LinkButton ID="btn_cg" runat="server"  OnClick="btn_cg_Onclick">常规</asp:LinkButton>
                                 </td>                                         
                          <td class="ctrlList" colspan="7">
                               <asp:Panel ID="panel_cg"  BackColor="#F5F9FF" runat="server" GroupingText="" ForeColor="#2292DD" Font-Size="X-Small"
                    Width="735px"> 
                           <asp:CheckBoxList ID="cb_analysisList" runat="server" RepeatDirection="Horizontal"  Font-Size="X-Small" RepeatColumns="8" OnSelectedIndexChanged="cb_analysisList_SelectedIndexChanged" AutoPostBack="true" Width="735px"  >
                           </asp:CheckBoxList>
                                   </asp:Panel>
                               </td></tr>
                             <tr><td class="ctrlList" colspan="7">
                                 <asp:LinkButton ID="btn_other" runat="server" OnClick="btn_other_Onclick">其他</asp:LinkButton>
                                 </td>
                               <td colspan="7"> 
                                <asp:Panel ID="panel_other" Visible="false"  BackColor="#F5F9FF" runat="server" GroupingText="" ForeColor="#2292DD" Font-Size="X-Small"
                    Width="735px">
                              <table style="margin: 0px; width: 735px">
                            <tr><td class="ctrlList" colspan="9">
                           <asp:CheckBoxList ID="cb_other" runat="server" RepeatDirection="Horizontal"  Font-Size="X-Small" RepeatColumns="8" OnSelectedIndexChanged="cb_analysisList_SelectedIndexChanged" AutoPostBack="true" Visible="true" Width="735px"  >
                           </asp:CheckBoxList></td></tr>
                                 
                                   </table>
                               </asp:Panel>
                             </td></tr>
                             <tr><td colspan="9"><asp:Button ID="btn_saveitem" runat="server" Text="保存" OnClick="btn_saveitem_Click" /></td></tr>
                            </table>
                                     </asp:Panel>
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
