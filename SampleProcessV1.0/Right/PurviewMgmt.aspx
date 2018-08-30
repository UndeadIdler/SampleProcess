<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="PurviewMgmt.aspx.cs" Inherits="BaseData_PurviewMgmt1" Title="用户管理" %>

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
            <table class="container">
                <tbody>
                    <tr>
                        <td style="height: 4px; text-align: right" class="titleList">
                            <span style="font-size: 10pt">操作员级别：</span>
                        </td>
                        <td style="height: 4px" class="ctrlList">
                            <asp:DropDownList ID="drop_UserLevel" runat="server" CssClass="mDropDownList" OnSelectedIndexChanged="drop_UserLevel_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="height: 4px" class="titleList">
                                    <asp:Label runat="server" ID="lbl_query" Text="姓名"></asp:Label>            </td>
                        <td style="height: 4px" class="ctrlList"> <asp:TextBox runat="server" ID="txt_Query"></asp:TextBox> 
                        </td>
                         <td> <asp:Button ID="Button1" OnClick="btn_Query_Click" runat="server" Text="查询" CssClass="mButton">
                            </asp:Button>  
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" Text="添加" CssClass="mButton">
                            </asp:Button>
                        </td>
                   
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption="111" AllowPaging="True"
                PageSize="30" OnPageIndexChanging="grdvw_List_PageIndexChanging" OnRowEditing="grdvw_List_RowEditing"  OnSelectedIndexChanging="grdvw_List_RowSelecting"
                OnRowCreated="grdvw_List_RowCreated" OnRowDeleting="grdvw_List_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                    <asp:BoundField HeaderText="用户名" DataField="UserID"></asp:BoundField>
                    <asp:BoundField HeaderText="姓名" DataField="Name"></asp:BoundField>
                    <asp:BoundField HeaderText="所属级别" DataField="LevelName"></asp:BoundField>
                    <asp:BoundField HeaderText="所属角色" DataField="RoleName"></asp:BoundField>
                    <asp:BoundField HeaderText="所属单位" DataField="DepartName"></asp:BoundField>
                </Columns>
            </asp:GridView>
           
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="btn_Cancel" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <div id="detail" class="mLayer" style=" display:none; left: 96px; width: 900px; top:200px;
        height: 130px">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
                <table style="width: 321px">
                    <tr>
                        <td style="height: 7px" colspan="3">
                            <span style="font-size: 10pt">
                                <img src="../images/minipro.gif" />通讯中，请稍等....</span>
                        </td>
                    </tr>
                </table>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="margin: 0px; width: 900px">
                    <tbody>
                        <tr>
                            <td style="text-align: left" class="float_Middle" colspan="7">
                                <asp:Label ID="lbl_Type" runat="server" CssClass="mLabelTitle" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="width: 119px; text-align: right" class="titleList">
                                <span style="font-size: 10pt">用户名：</span>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_UserName" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 105px" class="float_Middle">
                            </td>
                            <td style="width: 121px; text-align: right" class="titleList">
                                <span style="font-size: 10pt">所属级别：</span>
                            </td>
                            <td class="ctrlList">
                                <asp:DropDownList ID="drop_Level" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_Level_SelectedIndexChanged"
                                    CssClass="mDropDownList" OnDataBound="drop_Level_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 62px" class="float_LeftAndRight">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 48px; height: 3px" class="float_Middle">
                            </td>
                            <td style="width: 119px; height: 3px; text-align: right" class="titleList">
                                <span style="font-size: 10pt">权限角色：</span>
                            </td>
                            <td style="height: 3px" class="ctrlList">
                                <asp:DropDownList ID="drop_Role" runat="server" CssClass="mDropDownList">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 105px; height: 3px" class="float_Middle">
                            </td>
                            <td style="width: 121px; height: 3px" class="titleList">
                                 <span style="font-size: 10pt">所属部门：</span>
                                <%--<asp:Label id="lbl_FirSca_Title" runat="server" CssClass="mLabel" Text="Label" Visible="False"></asp:Label>--%>
                            </td>
                            <td style="height: 3px" class="ctrlList">
                                <asp:DropDownList ID="drop_ThrSca_Name" runat="server" CssClass="mDropDownList" OnSelectedIndexChanged="drop_ThrSca_Name_SelectedIndexChanged" AutoPostBack="true" >
                                </asp:DropDownList>
                                <%--<asp:DropDownList id="drop_FirSca_Name" runat="server" AutoPostBack="true"  CssClass="mDropDownList" OnDataBound="drop_FirSca_Name_SelectedIndexChanged" Visible="False"></asp:DropDownList></td><td style="WIDTH: 62px; HEIGHT: 3px" class="float_LeftAndRight">--%>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 48px; height: 3px" class="float_Middle">
                            </td>
                            <td style="width: 119px; height: 3px; text-align: right" class="titleList">
                                <span style="font-size: 10pt">登录密码：</span>
                            </td>
                            <td style="height: 3px" class="ctrlList">
                                <asp:TextBox ID="Txt_pwd" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 105px; height: 3px" class="float_Middle">
                            </td>
                            <td style="width: 121px; height: 3px" class="titleList">
                            <span style="font-size: 10pt">姓名：</span>
                               
                            </td>
                            <td style="height: 3px" class="ctrlList">
                               <asp:TextBox ID="txt_name" runat="server" CssClass="mTextBox"></asp:TextBox> <%--<asp:DropDownList id="drop_FirSca_Name" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_FirSca_Name_SelectedIndexChanged" CssClass="mDropDownList" OnDataBound="drop_FirSca_Name_SelectedIndexChanged" Visible="False"></asp:DropDownList></td><td style="WIDTH: 62px; HEIGHT: 3px" class="float_LeftAndRight">--%>
                            </td>
                        </tr>
                       
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" CssClass="mButton"
                                    Text="取消"></asp:Button>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" CssClass="mButton"
                                    Text="保存"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                 <asp:Panel ID="panel_role" runat="server" Visible="false" GroupingText="分析员设定">
                    
               <%-- <asp:LinkButton ID="lbtn_xk" runat="server" onclick="lbtn_xk_Click" Text="+分析员角色设定"></asp:LinkButton>--%></td></tr>
                    <tr><td style="width:20%"> 
                        <asp:Label ID="lbl_a" CssClass="mLabel" Text="A角"  runat="server"></asp:Label></td></tr>
                     <tr>
                         <td align="right">
                             <asp:Button ID="btn_a_Add" CssClass="btn" runat="server" Text="添加" OnClick="btn_a_Add_Click" /></td></tr>
                        <tr>
                        <td><asp:GridView ID="grv_a" runat="server" CssClass="mGridView"  OnRowCreated="grv_a_RowCreated" OnRowEditing="grv_a_RowEditing" OnRowDeleting="grv_a_RowDeleting" OnRowDataBound="grv_a_RowDataBound" >
                           
                            </asp:GridView></td></tr>
                     <tr><td style="width:20%"> <asp:Label ID="lbl_b" CssClass="mLabel" Text="B角"  runat="server">

                                                </asp:Label> </td>
                         </tr> <td align="right">
                             <asp:Button ID="btn_b_Add" CssClass="btn" runat="server" Text="添加" OnClick="btn_b_Add_Click" /></td></tr>
                        <tr>
                         <tr>
                        <td><asp:GridView ID="grv_b" runat="server" CssClass="mGridView"  OnRowCreated="grv_b_RowCreated" OnRowEditing="grv_b_RowEditing" OnRowDeleting="grv_b_RowDeleting" OnRowDataBound="grv_b_RowDataBound" >
                           
                            </asp:GridView></td></tr>                       
                <asp:Panel ID="panel_a" runat="server" Visible="false" GroupingText="分析员A角">
                        <table style="border-style: groove; border-color: inherit; border-width: 2px; width: 100%;">
                          <tr><td> <asp:DropDownList ID="drop_type" runat="server" OnSelectedIndexChanged="drop_type_SelectedIndexChanged" AutoPostBack="true">
                          </asp:DropDownList> </td>
                             <td style="Width:550px"><asp:TextBox ID="txt_a_Item" runat="server" CssClass="mTextBox" Width="100%" OnTextChanged="txt_a_Item_TextChanged" AutoPostBack="true" ></asp:TextBox>

                             </td>
                          <td><asp:Button ID="btn_save_a" runat="server" Text="A角保存" CssClass="btn" OnClick="btn_save_a_OnClick" /></td></tr>
                       <tr><td align="left">
                           <asp:Label ID="sampletypeA" CssClass="mLabel"  runat="server"></asp:Label>
                       </td><td><asp:Label ID="sampletypeIDA" Visible="false" runat="server"></asp:Label></td>  
                        </tr> 
                        <tr><td colspan="2">
                           <asp:CheckBoxList runat="server" ID="cbl_ItemlistA"  RepeatColumns="8" RepeatDirection="Horizontal" OnSelectedIndexChanged="cbl_ItemlistA_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                        </td></tr>
                          
                            </table>
                </asp:Panel>
               <asp:Panel ID="panel_b" runat="server" Visible="false" GroupingText="分析员B角">
               
               
                     <table style="border-style: groove; border-color: inherit; border-width: 2px; width: 100%;">
                          <tr><td> <asp:DropDownList ID="drop_type_b" runat="server" OnSelectedIndexChanged="drop_type_b_SelectedIndexChanged" AutoPostBack="true">
                          </asp:DropDownList> </td>
                             <td style="Width:550px"><asp:TextBox ID="txt_b_Item" runat="server" CssClass="mTextBox" Width="100%" OnTextChanged="txt_b_Item_TextChanged" AutoPostBack="true" ></asp:TextBox>

                             </td>
                          <td><asp:Button ID="btn_save_b" runat="server" Text="B角保存" CssClass="btn" OnClick="btn_save_b_OnClick" /></td></tr>
                       <tr><td align="left">
                           <asp:Label ID="Label1" CssClass="mLabel"  runat="server"></asp:Label>
                       </td><td><asp:Label ID="Label2" Visible="false" runat="server"></asp:Label></td>  
                        </tr> 
                        <tr><td colspan="2">
                           <asp:CheckBoxList runat="server" ID="cbl_ItemlistB"  RepeatColumns="8" RepeatDirection="Horizontal" OnSelectedIndexChanged="cbl_ItemlistB_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                        </td></tr>
                          
                            </table>
                      <%--  <table style="border-style: groove; border-color: inherit; border-width: 2px; width: 100%;">
                            <tr><td><asp:Button ID="btn_save_b" runat="server" Text="B角保存" OnClick="btn_save_b_OnClick" /></td></tr>
                         
                       <tr><td align="left">
                           <asp:Label ID="sampletypeB" CssClass="mLabel"  runat="server"></asp:Label>
                       </td><td><asp:Label ID="sampletypeIDB" Visible="false" runat="server"></asp:Label></td>
                           
                        </tr>
                            
                        <tr><td colspan="2">
                             <asp:CheckBoxList runat="server" ID="cbl_ItemlistB"  RepeatColumns="8" RepeatDirection="Horizontal"></asp:CheckBoxList>
                   <%-- <asp:GridView ID="grv_b" runat="server" CssClass="mGridView"  Visible="true"
                       OnRowCreated="grv_b_RowCreated"  OnRowDataBound="grv_b_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:CheckBox ID="autoid" runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField> 
                    </Columns>
                    </asp:GridView>
                        </td></tr>
                            
                            </table>--%>
                </asp:Panel>
                     </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Add" EventName="Click" ></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
