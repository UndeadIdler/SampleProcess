<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="WTStationInfo1.aspx.cs" Inherits="BaseData_WTStationInfo1" Title="委托单位" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../js/position.js"></script>

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
            <table class="containerb">
                <tbody>
                    
                    <tr>
                        
                        <td class="titleList">
                            <asp:Label ID="lbl_StationName_forSearch" runat="server" CssClass="mLabel" Text="Label">单位名称：</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_StationName_forSearch" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                        <td class="titleList">
                            
                        </td>
                        <td>
                            
                        </td>
                        <td class="titleList">
                            <asp:Button ID="btn_Query" OnClick="btn_Query_Click" runat="server" CssClass="btn"
                                Text="查询"></asp:Button>
                        </td>
                        <td class="titleList">
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" CssClass="btn" Text="添加">
                            </asp:Button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" OnRowDeleting="grdvw_List_RowDeleting" Width="98%"
                OnRowEditing="grdvw_List_RowEditing" OnRowCreated="grdvw_List_RowCreated"  Caption=""
                AllowPaging="true" PageSize="20" OnPageIndexChanging="grdvw_List_PageIndexChanging">
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
     <div class="mLayer" style="left: 223px; top: 243px; width: 900px; height: 150px;
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
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="17" valign="top" background="../images/mail_leftbg.gif">
                        <img src="../images/left-top-right.gif" width="17" height="29" />
                    </td>
                    <td valign="top" background="../images/content-bg.gif">
                        <table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" class="left_topbg"
                            id="table2">
                            <tr>
                                <td height="31">
                                    <div class="titlebt">
                                        基本设置</div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="16" valign="top" background="../images/mail_rightbg.gif">
                        <img src="../images/nav-right-bg.gif" width="16" height="29" />
                    </td>
                </tr>
                <tr>
                    <td height="71" valign="middle" background="../images/mail_leftbg.gif">
                        &nbsp;
                    </td>
                    <td valign="top" bgcolor="#F7F8F9">
                        <table width="100%" height="138" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="13" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="10%" height="55" valign="middle">
                                                            <img src="../images/ad.gif" width="54" height="55">
                                                        </td>
                                                        <td width="90%" valign="top">
                                                            <span class="left_txt2">在这里，您可以设置单位的</span><span class="left_txt3">基本信息</span><span
                                                                class="left_txt2">！</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" class="nowtable">
                                                    <tr>
                                                        <td class="left_bt2">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;基本信息
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td height="30" align="right" bgcolor="#f2f2f2" class="left_txt2" colspan="2">
                                                            单位全称：
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" colspan="4">
                                                            <asp:TextBox ID="txt_qymc" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" style="width: 100%" colspan="2">
                                                            <asp:Label ID="lab_qymc" runat="server" Text="单位名称，必填！" CssClass="left_txt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td height="30" align="right" bgcolor="#f2f2f2" class="left_txt2" colspan="2">
                                                            单位详细地址：
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" colspan="4">
                                                            <asp:TextBox ID="txt_dz" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" style="width: 73px" colspan="2">
                                                            <asp:Label ID="Label1" runat="server" Text="单位详细地址" CssClass="left_txt" Width="98%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                   
                                                    <tr>
                                                       
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            邮政编码：
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txt_yzbm" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                   
                                                   
                                                   
                                                   <tr>
                                                        <td height="30" align="right" bgcolor="#f2f2f2" class="left_txt2" colspan="2">
                                                            单位曾用名全称：
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" colspan="4">
                                                            <asp:TextBox ID="txt_cname" runat="server" TextMode="MultiLine" Width="98%" CssClass="mTextBox"
                                                                Height="56px"></asp:TextBox>
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" style="width: 73px" colspan="2">
                                                            <asp:Label ID="Label2" runat="server" Text="单位曾用名全称，用“；”间隔！" CssClass="left_txt"
                                                                Width="282%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                       <tr>
                                                        <td height="30" align="right" class="left_txt2" style="width: 12.5%">
                                                            联系人：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_hbfz" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            电话：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_tel3" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            手机：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_mobile3" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            市府网：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_zfw3" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr> 
                                                </table>
                                               
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" height="30" align="right">
                                                <asp:Button ID="btn_Save" OnClientClick="setReadOnly()" OnClick="btn_Save_Click" runat="server" CssClass="btn" Text="保存">
                                                </asp:Button>
                                            </td>
                                            <td width="6%" height="30" align="right">
                                                &nbsp;
                                            </td>
                                            <td width="44%" height="30">
                                                <asp:Button ID="btn_cancel" OnClick="btn_Cancel_Click" runat="server" CssClass="btn"
                                                    Text="取消"></asp:Button>
                                            
                                                <asp:Button ID="btn_print" OnClick="btn_print_Click" runat="server" CssClass="btn"
                                                    Text="打印"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td background="../images/mail_rightbg.gif">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="middle" background="../images/mail_leftbg.gif">
                        <img src="../images/buttom_left2.gif" width="17" height="17" />
                    </td>
                    <td height="17" valign="top" background="../images/buttom_bgs.gif">
                        <img src="../images/buttom_bgs.gif" width="17" height="17" />
                    </td>
                    <td background="../images/mail_rightbg.gif">
                        <img src="../images/buttom_right2.gif" width="16" height="17" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
  </asp:Content>
