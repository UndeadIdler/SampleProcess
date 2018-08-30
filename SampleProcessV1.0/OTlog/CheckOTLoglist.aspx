<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="CheckOTLoglist.aspx.cs" Inherits="OTlog_CheckOTLoglist" Title="Untitled Page" StylesheetTheme="Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../js/position.js"></script>
        <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js"></script>

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
        <table class="container" width="90%">
                <tbody>
                    <tr>
                     <td style="text-align: right"><span style="font-size: 10pt"><asp:Label ID="Label6" runat="server" Text="部门"></asp:Label></span></td>
                        <td  class="ctrlList">
                            <asp:DropDownList ID="drop_depart" runat="server" Width="100px">
                            </asp:DropDownList>  
                        </td>
                        <td style="text-align: right">
                            <span style="font-size: 10pt">开始时间</span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txts_time1" runat="server" CssClass="mTextBox" Width="100"></asp:TextBox>
                        </td>
                         <td style="text-align: right">
                            <span style="font-size: 10pt">结束时间</span>
                        </td>
                        <td class="ctrlList">
                        <asp:TextBox ID="txts_time2" runat="server" CssClass="mTextBox" Width="100"></asp:TextBox>
                        </td>
                       <td>
                            <asp:Button ID="btn_query" runat="server" Text="查 询" OnClick="btn_query_Click" CssClass="mButton" Width="60" />
                          </td> <td> <asp:Button ID="btn_Export" runat="server" Text="导 出" OnClick="btn_Export_Click" CssClass="mButton" Width="60" />
                        </td>
                       </tr><tr>
                         <td style="text-align: right"><span style="font-size: 10pt"><asp:Label ID="Label3" runat="server" Text="加班人员"></asp:Label></span></td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txts_name" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <span style="font-size: 10pt"><asp:Label ID="Label4" runat="server" Text="加班内容"></asp:Label></span>
                        </td>
                                                <td class="ctrlList"><asp:TextBox ID="txts_ulog" runat="server" 
                                                        CssClass="mTextBox" Width="100px"></asp:TextBox></td>
                                                         <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label11" runat="server" Text="是否通过：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td >
                                <asp:DropDownList ID="drop_status_query" runat="server">
                                 <asp:ListItem Value="0" Text="待审核"></asp:ListItem>
                                <asp:ListItem Value="1" Text="通过"></asp:ListItem>
                                <asp:ListItem Value="2" Text="未通过"></asp:ListItem>
                                </asp:DropDownList>
                               
                            </td>

                        
                        <td style="width: 50px">
                            <%--<asp:Button ID="Button1" OnClick="btn_Add_Click" runat="server" CssClass="btn" Text="添 加" Width="60" />--%>
                        </td>                        
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" OnPageIndexChanging="grdvw_List_PageIndexChanging"  OnSelectedIndexChanging="grdvw_List_RowSelecting"
                 OnRowCreated="grdvw_List_RowCreated" AllowPaging="true" PageSize="20">
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
    <div class="mLayer" style="left: 223px; top: 200px; width: 500px; height: 250px;
        display: none;" id="detail">
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
                <table style="width: 500px">
                    <tbody>
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="2">
                                <asp:Label ID="lbl_Type" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label5" runat="server" Text="加班日期：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:TextBox ID="txt_date" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label7" runat="server" Text="加班时段：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 50px">
                                <asp:TextBox ID="txt_time1" runat="server" CssClass="mTextBox" Width="50px" ontextchanged="txt_time1_TextChanged" AutoPostBack="true"></asp:TextBox></td><td style="width:10px">-
                            </td>
                            <td style="width: 50px">
                                <asp:TextBox ID="txt_time2" runat="server" CssClass="mTextBox" Width="50px" ontextchanged="txt_time1_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                        
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label8" runat="server" Text="加班小时数：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txt_num" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label2" runat="server" Text="加班人员：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:TextBox ID="txt_name" runat="server" CssClass="mTextBox" ReadOnly="true" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label1" runat="server" Text="加班内容：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:TextBox ID="txt_ulog" runat="server" CssClass="mTextBox" TextMode="MultiLine"
                                    Height="200" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                       
                       
                        <tr>
                        <td colspan="6">
                            <asp:Panel ID="panel_sh" runat="server">
                           <table style="width: 500px"> <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label10" runat="server" Text="是否通过：" CssClass="mLabel"></asp:Label>
                           </td>
                            <td style="width: 100px" class="titleList">
                                <asp:DropDownList ID="drop_flag" runat="server">
                                <asp:ListItem Value="1" Text="通过"></asp:ListItem>
                                <asp:ListItem Value="2" Text="未通过"></asp:ListItem>
                                </asp:DropDownList>
                                </td>
                                <td></td>
                                </tr>
                               
                            <tr>
                            <td style="width:100px" class="titleList" >
                                <asp:Label ID="Label14" runat="server" Text="审核意见：" CssClass="mLabel"></asp:Label>
                           </td>
                            <td style="width: 80%" class="titleList" colspan="2">
                                <asp:TextBox ID="txt_remark" runat="server" CssClass="mTextBox" TextMode="MultiLine"
                                    Height="200" Width="98%"></asp:TextBox>
                                    </td></tr></table>
                                     </asp:Panel>
                            </td>
                        </tr>
                       
                        <tr align="center">
                            <td class="float_Padding" colspan="7" align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" Text="取消" CssClass="mButton">
                                </asp:Button>
                                
                                 <asp:Button ID="btn_sh" OnClick="btn_sh_Click" runat="server" Text="编辑" CssClass="mButton">
                                </asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>           
        </asp:UpdatePanel>
         </div>
       <%--  <div class="mLayer" style="position:relative; display: none;" id="div_shenhe">
        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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
                 <table style="width: 500px">
                    <tbody>
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="2">
                                <asp:Label ID="lbl_sh" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label12" runat="server" Text="加班日期：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label13" runat="server" Text="加班时段：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 50px">
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="mTextBox" Width="50px" ontextchanged="txt_time1_TextChanged" AutoPostBack="true"></asp:TextBox></td><td style="width:10px">-
                            </td>
                            <td style="width: 50px">
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="mTextBox" Width="50px" ontextchanged="txt_time1_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                        
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label15" runat="server" Text="加班小时数：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label16" runat="server" Text="加班人员：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="mTextBox" ReadOnly="true" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label17" runat="server" Text="加班内容：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="mTextBox" TextMode="MultiLine"
                                    Height="200" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td class="float_Padding" colspan="7" align="center">
                                <asp:Button ID="Button2" OnClick="btn_Cancel_Click" runat="server" Text="取消" CssClass="mButton">
                                </asp:Button>
                                <asp:Button ID="Button3" OnClick="btn_OK_Click" runat="server" Text="确定" CssClass="mButton">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="2">
                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label10" runat="server" Text="是否通过：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:DropDownList ID="drop_flag" runat="server">
                                <asp:ListItem Value="1" Text="通过"></asp:ListItem>
                                <asp:ListItem Value="2" Text="未通过"></asp:ListItem>
                                </asp:DropDownList>
                               
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 100px" class="titleList">
                                <asp:Label ID="Label14" runat="server" Text="审核意见：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="width: 400px" colspan="5">
                                <asp:TextBox ID="txt_remark" runat="server" CssClass="mTextBox" TextMode="MultiLine"
                                    Height="200" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td class="float_Padding" colspan="7" align="center">
                                <asp:Button ID="btn_qx" OnClick="btn_qx_Click" runat="server" Text="取消" CssClass="mButton">
                                </asp:Button>
                                <asp:Button ID="btn_sh" OnClick="btn_sh_Click" runat="server" Text="确定" CssClass="mButton">
                                </asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>           
        </asp:UpdatePanel>
   
    </div>--%>
    
</asp:Content>
