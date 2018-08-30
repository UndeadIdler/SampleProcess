<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="StationInfo1.aspx.cs" Inherits="BaseData_StationInfo1" Title="Untitled Page" %>

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
                                                            <span class="left_txt2">在这里，您可以设置企业的</span><span class="left_txt3">基本信息</span><span
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
                                                            &nbsp;&nbsp;&nbsp;&nbsp;企业基本信息
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
                                                           排污权证号：
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" colspan="4">
                                                            <asp:TextBox ID="txt_pwNO" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" bgcolor="#f2f2f2" style="width: 100%" colspan="2">
                                                            <asp:Label ID="Label4" runat="server" Text="排污权证号" CssClass="left_txt"></asp:Label>
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
                                                        <td height="30" align="right" bgcolor="#F7F8F9" class="left_txt2" colspan="2">
                                                            单位法人代码：
                                                        </td>
                                                        <td height="30" bgcolor="#F7F8F9" colspan="4">
                                                            <asp:TextBox ID="txt_jgdm" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" bgcolor="#F7F8F9" style="width: 73px" colspan="2">
                                                            <asp:Label ID="lab_jgdm" runat="server" Text="单位法人代码" CssClass="left_txt" Width="98%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            所属镇街道：
                                                        </td>
                                                        <td height="30" colspan="2">
                                                            <asp:DropDownList ID="drp_sd" runat="server" CssClass="mDropDownList" Width="98%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            邮政编码：
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txt_yzbm" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            传真号码：
                                                        </td>
                                                        <td height="30" colspan="2">
                                                            <asp:TextBox ID="txt_czhm" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            电子邮箱：
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txt_email" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            建厂时间：
                                                        </td>
                                                        <td height="30" colspan="1">
                                                        <asp:TextBox ID="txt_createdate" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                          </td>
                                                        
                                                          <td height="30" align="right" class="left_txt2" colspan="1">
                                                           生产状态：
                                                        </td>
                                                        <td height="30" colspan="1"><asp:DropDownList ID="drop_status" runat="server" CssClass="mDropDownList" Width="98%">
                                                            </asp:DropDownList>
                                                        
                                                         <td height="30" align="right" class="left_txt2" colspan="1">
                                                            行业：
                                                        </td>
                                                        <td height="30" colspan="2"><asp:DropDownList ID="drop_industry" runat="server" CssClass="mDropDownList" Width="98%">
                                                            </asp:DropDownList>
                                                        
                                                          </td>
                                                    </tr>
                                                     <tr>
                                                         <td height="30" align="right" class="left_txt2" colspan="2">
                                                            级别
                                                        </td>
                                                        <td height="30" colspan="1">
                                                        <asp:DropDownList ID="drop_control" runat="server" CssClass="mDropDownList" Width="98%">
                                                            <asp:ListItem Text="国控" Value="2">  </asp:ListItem>
                                                             <asp:ListItem Text="省控" Value="1">  </asp:ListItem>
                                                             <asp:ListItem Text="其他" Value="0" Selected="true">  </asp:ListItem>
                                                            </asp:DropDownList>
                                                        
                                                          </td>
                                                        <td height="30" align="right" class="left_txt2" >
                                                          
                                                        </td>
                                                        <td height="30" colspan="4">
                                                        <asp:DropDownList ID="drop_bz" Visible="false" runat="server" CssClass="mDropDownList" Width="98%">
                                                            </asp:DropDownList>
                                                        
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" style="width: 12.5%">
                                                            法定代表人：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_frdb" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            电话：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_tel1" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            手机：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_mobile1" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            市府网：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_zfw1" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" style="width: 12.5%">
                                                            环保分管人：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_hbfg" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            电话：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_tel2" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            手机：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_mobile2" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td height="30" style="width: 12.5%" align="right" class="left_txt2">
                                                            市府网：
                                                        </td>
                                                        <td height="30" style="width: 12.5%">
                                                            <asp:TextBox ID="txt_zfw2" runat="server" CssClass="mTextBox" Height="22px" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" style="width: 12.5%">
                                                            环保负责人：
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
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            污水是否入网：
                                                        </td>
                                                        <td height="30" colspan="2">
                                                            <asp:DropDownList ID="drop_wsrw" runat="server" CssClass="mDropDownList" Width="98%">
                                                            <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                            <asp:ListItem Value="1"  Text="是"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            入网时间：
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txt_wstime" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            集中供热是否入网：
                                                        </td>
                                                        <td height="30" colspan="2">
                                                            <asp:DropDownList ID="drop_grrw" runat="server" CssClass="mDropDownList" Width="98%">
                                                            <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                            <asp:ListItem Value="1"  Text="是"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            入网时间：
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txt_grtime" runat="server" CssClass="mTextBox" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            工商营业执照经营范围：
                                                        </td>
                                                        <td height="30" colspan="6">
                                                        <asp:TextBox ID="txt_other" runat="server" CssClass="mTextBox" TextMode="MultiLine" Width="98%" Height="75px"></asp:TextBox>
                                                           
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td height="30" align="right" class="left_txt2" colspan="2">
                                                            企业近年实际产品产量：
                                                        </td>
                                                        <td height="30" colspan="6">
                                                        <asp:TextBox ID="txt_cp" runat="server" CssClass="mTextBox" TextMode="MultiLine" Width="98%" Height="75px"></asp:TextBox>
                                                           
                                                        
                                                    </tr><tr>
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
                                                   <%-- <tr><td></td><td>
                                                        <asp:LinkButton ID="lbtn_xk" runat="server" onclick="lbtn_xk_Click">+初始排污权信息</asp:LinkButton></td></tr>--%>
                                                   
                                                   
                                                </table>
                                                <%--<asp:Panel ID="Panel_infectant"  runat="server" Visible="false">
                                                <asp:Repeater ID="Repeater_list" runat="server"> <%--OnItemCommand="Repeater_list_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <table style="border-style: groove; border-color: inherit; border-width: 2px; width: 100%;
                                                                        height: 50px;">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="width: 100px">
                                                                        </td>
                                                                        <td style="width: 10px">
                                                                            <asp:CheckBox ID="cb_check" runat="server" />
                                                                            <asp:TextBox ID="txt_id" runat="server" Visible="false" Text="" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 60px; vertical-align:bottom;">
                                                                            <asp:Label ID="txt_wrw" runat="server" Text="COD" CssClass="mLabel" ReadOnly="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                    <tr>
                                                                        <td style="width: 15px">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td class="left_txt2">
                                                                            <asp:Label ID="Label34" runat="server" Text="许可量：" CssClass="mLabel" Width="100%"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:TextBox ID="txt_num" runat="server" Width="100px" Text=""></asp:TextBox>
                                                                        </td>
                                                                        <td align="left" style="width: 30px;">
                                                                            <asp:Label ID="Label31" runat="server" Text="吨/年" CssClass="mLabel" Width="100%"></asp:Label>
                                                                        </td>
                                                                        <td class="left_txt2">
                                                                        <asp:Label ID="Label5" runat="server" Text="排放浓度：" CssClass="mLabel" Width="100%"></asp:Label>
                                                                            
                                                                        </td> <td style="width: 100px">
                                                                            <asp:TextBox ID="txt_nd" runat="server" Width="100px" Text=""></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 10px"><asp:Label ID="Label12" runat="server" Text="mg/l" CssClass="mLabel" Width="100%"></asp:Label>
                                                                        </td>
                                                                        <td style=" text-align:right;">
                                                                            <asp:Label ID="lbl_fst" runat="server" Text="废水排放许可量：" CssClass="mLabel" Width="100%"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:TextBox ID="txt_fs" runat="server" Width="100px" Text="0"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                        <asp:Label ID="Label3" runat="server" Text="吨/年" CssClass="mLabel" Width="100%"></asp:Label>
                                                                        </td>
                                                                       </tr>
                                                                    <tr>
                                                                        <td colspan="10" bgcolor="#d5f2f5">
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                   
                                                                    </table></FooterTemplate>
                                                            </asp:Repeater>
                                                </asp:Panel>--%>
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
