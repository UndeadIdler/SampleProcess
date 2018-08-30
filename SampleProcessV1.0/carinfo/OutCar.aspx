<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OutCar.aspx.cs" Inherits="carinfo_OutCar" Title="出车信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        a
        {
            text-decoration: none;
            font-size: 13px;
            color: #666666;
        }
        a:hover
        {
            text-decoration: none;
            color: #666666;
        }
        .NewsTabMenu
        {
            background-image: url(      "../images/tab2off.gif" );
            width: 95px;
            height: 27px;
            font-family: 黑体;
            color: #666666;
            font-size: 15px;
            font-weight: bold;
            line-height: 20px;
            text-decoration: none;
            text-align: center;
        }
        .selectedStyle
        {
            background-image: url(      "../images/tab2off.gif" );
            width: 95px;
            height: 27px;
            font-family: 黑体;
            font-size: 15px;
            font-weight: bold;
            text-decoration: none;
            text-align: center;
        }
        .selectedStyle a, a:hover
        {
            text-decoration: none;
            color: #1A8A98;
        }
        .style5
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 25px;
            color: #000000;
            width: 146px;
        }
    </style>

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
            <table style="width: 700px">
                <tr>
                    <td style="width: 129px">
                        出车日期:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_s" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 10px">
                        -
                    </td>
                    <td>
                        <asp:TextBox ID="txt_e" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btn_query" Text="查询" CssClass="btn" OnClick="btn_query_Click">
                        </asp:Button><asp:Button runat="server" ID="Button1" Text="添加" CssClass="btn" OnClick="btn_Add_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" OnPageIndexChanging="grdvw_List_PageIndexChanging"
                AllowPaging="true" PageSize="12" OnRowDeleting="grdvw_List_RowDeleting" OnRowEditing="grdvw_List_RowEditing"
                OnRowCreated="grdvw_List_RowCreated" OnSelectedIndexChanged="grdvw_List_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="autoid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="id" />
                    <asp:BoundField DataField="outstart" HeaderText="出车时间" />
                    <asp:BoundField DataField="outend" HeaderText="出车结束时间" />
                    <asp:BoundField DataField="carid" HeaderText="车牌号" />
                    <asp:BoundField DataField="destn" HeaderText="目的地" />
                    <asp:BoundField DataField="driver" HeaderText="用车人" />
                    <asp:BoundField DataField="num" HeaderText="限载人数" />
                    <asp:BoundField DataField="realnum" HeaderText="同行人数" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="mLayer" style="left: 200px; top: 20px; width: 800px; height: 150px;
        display: none;" id="detail">
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
                <table style="width: 100%">
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
             
                        <h2 style="padding-right: 0px; padding-left: 8px; font-size: 14px; background: #e2f3fa;
                            padding-bottom: 0px; margin: 0px; color: #0066a9; line-height: 24px; padding-top: 0px;
                            border-bottom: #a4d5e3 1px solid">
                            <asp:Label ID="lbl_Type" runat="server" Text="Label" CssClass="mLabelTitle"></asp:Label></h2>
                        <div style="margin: 5px">
                <table style="width: 100%">
                    <tbody>
                        
                        <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label1" runat="server" Text="车牌号：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:DropDownList ID="drop_carno" runat="server" Height="16px" Width="150px" AutoPostBack="True"
                                    OnSelectedIndexChanged="drop_carno_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                             <td style="width:10px"></td>
                            <td class="titleList">
                                <asp:Label ID="Label5" runat="server" Text="限坐人数：" CssClass="mLabel" ></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_num" runat="server" Text="" CssClass="mLabel" Width="50px"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label2" runat="server" Text="目的地：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList" style="width:150px">
                                <asp:TextBox ID="txt_destn" runat="server" CssClass="mTextBox" Width="150px" 
                                    Height="17px"></asp:TextBox>
                            </td>
                             <td style="width:10px"></td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label6" runat="server" Text="出车人：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList"  style="width:150px">
                                <asp:TextBox ID="txt_driver" runat="server" CssClass="mTextBox" Width="100px"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 119px" class="titleList">
                                <asp:Label ID="Label3" runat="server" Text="用车时间：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList"  style="width:150px">
                                <asp:TextBox ID="txt_start" runat="server" CssClass="mTextBox" Width="150px"></asp:TextBox>
                            </td>
                           <td style="width:10px">-</td>
                            <td class="ctrlList" colspan="2">
                                <asp:TextBox ID="txt_end" runat="server" CssClass="mTextBox" Width="150px"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="备注：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" Height="50px" Width="80%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                            <td class="float_Padding" colspan="4" align="center">
                               <%-- <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" Text="取消" CssClass="mButton">
                                </asp:Button>--%>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" Text="确定" CssClass="mButton">
                                </asp:Button>
                                <asp:Button ID="btn_adddetail" runat="server" Text="添加同行人员" OnClick="btn_adddetail_Click"
                                    CssClass="btn" />
                            </td>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="grdvw_Detail" Caption="同行人员" runat="server" CssClass="mGridView"
                                    OnPageIndexChanging="grdvw_Detail_PageIndexChanging" AllowPaging="true" PageSize="12"
                                    OnRowDeleting="grdvw_Detail_RowDeleting" OnRowEditing="grdvw_Detail_RowEditing"
                                    OnRowCreated="grdvw_Detail_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="autoid" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" />
                                        <asp:BoundField DataField="name" HeaderText="人员姓名" />
                                        <asp:BoundField DataField="destn" HeaderText="目的地" />
                                        <asp:BoundField DataField="remark" HeaderText="备注" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:Panel ID="panel_detail" runat="server" GroupingText="同行人员" Width="100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 60%">
                            </td>
                            <td style="" align="right">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/Delete.gif" runat="server"
                                    OnClick="ImageButton1_Click" />
                                <%-- <img alt="关闭" src="../../Images/MutMain/close.gif" onclick="hiddenDetail()" />--%>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tbody>
                            <tr>
                                <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                    <asp:Label ID="lbl_DteatilType" runat="server" Text="Label" CssClass="mLabelTitle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 38px" class="Middle">
                                </td>
                                <td style="width: 119px" class="titleList">
                                    <asp:Label ID="Label8" runat="server" Text="姓名：" CssClass="mLabel"></asp:Label>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 119px" class="titleList">
                                    <asp:Label ID="Label11" runat="server" Text="目的地：" CssClass="mLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_destndetail" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="备注：" CssClass="mLabel"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_detail" runat="server" TextMode="MultiLine" Height="50px" Width="344px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 4px" class="float_Middle">
                                </td>
                                <td class="float_Padding" colspan="4" align="center">
                                    <asp:Button ID="btn_Save" OnClick="btn_Save_Click" runat="server" Text="确定" CssClass="mButton">
                                    </asp:Button>
                                </td>
                                <td style="height: 4px" class="float_Middle">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                </div></div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
