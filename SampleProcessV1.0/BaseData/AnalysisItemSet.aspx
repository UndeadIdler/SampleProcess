<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="AnalysisItemSet.aspx.cs" Inherits="BaseData_AnalysisItemSet" Title="分析项目维护"
    StylesheetTheme="Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
                        <td class="queryctrlList" style="width: 150px">
                            <asp:Label ID="Label8" runat="server" Text="分析样品分类：" Width="150px" Style="text-align: center"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropList_type_S" runat="server" CssClass="mTextBox" Width="100px"
                                AutoPostBack="true" OnSelectedIndexChanged="dropList_type_S_SelectedIndexChanged"
                                Height="22px">
                            </asp:DropDownList>
                        </td>
                        <td class="queryctrlList" style="width: 150px">
                            <asp:Label ID="Label7" runat="server" Text="组别：" Width="150px" Style="text-align: center"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropList_type_Query" runat="server" CssClass="mTextBox" Width="100px"
                                AutoPostBack="true" OnSelectedIndexChanged="dropList_type_S_SelectedIndexChanged"
                                Height="22px">
                                 <asp:ListItem Text="所有" Value=""></asp:ListItem>
                                <asp:ListItem Text="常规" Value="1"></asp:ListItem>
                                  <asp:ListItem Text="其他" Value="0"></asp:ListItem>

                            </asp:DropDownList>
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                            <asp:Label ID="lbl_ThrScaTil_Name" runat="server" CssClass="mLabel" Text="分析样品的项目："
                                Width="100px"></asp:Label>
                        </td>
                        <td style="height: 4px" class="titleList">
                            <asp:TextBox ID="txt_AIName_S" runat="server" CssClass="mTextBox" Width="100px" OnTextChanged="txt_AIName_S_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="height: 4px;" class="ctrlList">
                            <asp:Button ID="btn_Query" runat="server" CssClass="btn" OnClick="btn_Query_Click"
                                Text="查询" />
                        </td>
                        <td style="width: 50px">
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" CssClass="btn" Text="添加" />
                        </td>
                        <td style="width: 180px">
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" PageSize="20" AllowPaging="true" OnPageIndexChanging="grdvw_List_PageIndexChanging"
                OnRowDeleting="grdvw_List_RowDeleting" OnRowEditing="grdvw_List_RowEditing" OnRowCreated="grdvw_List_RowCreated">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="autoid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField></asp:BoundField>
                    <asp:BoundField></asp:BoundField>
                </Columns>
            </asp:GridView>
            <table class="container">
                <tbody>
                    <tr>
                        <td align="center">
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="mLayer" style="left: 200px; top: 200px; width: 780px; height: 150px;  display:none;" id="detail">
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
                <table style="width: 780px">
                    <tbody>    
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                <asp:Label ID="lbl_Type" runat="server" Text="Label" CssClass="mLabelTitle"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label3" runat="server" Text="样品类型" CssClass="mLabel"  style="width: 150px" ></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:DropDownList ID="dropList_type" runat="server" CssClass="mTextBox" 
                                    AutoPostBack="True" Height="22px">
                                </asp:DropDownList>
                            </td>
                           <td class="float_Middle" style="width: 150px"></td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label1" runat="server" Text="分析项目：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_AIName" runat="server" CssClass="mTextBox"></asp:TextBox>
                                       <ajaxToolkit:AutoCompleteExtender runat="server" BehaviorID="AutoComplete_Item" ID="AutoCompleteExtender_Item"
                                    TargetControlID="txt_AIName" ServicePath="AutoGet.asmx" ServiceMethod="GetClassList"
                                    MinimumPrefixLength="0" CompletionInterval="200" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :">
                                    <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoComplete_Item');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoComplete_Item')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoComplete_Item')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                                    </Animations>
                                </ajaxToolkit:AutoCompleteExtender>

                                <script type="text/javascript">
                                    // Work around browser behavior of "auto-submitting" simple forms
                                    var frm = document.getElementById("aspnetForm");
                                    if (frm) {
                                        frm.onsubmit = function () { return false; };
                                    }
                                </script>

                                <%-- Prevent enter in textbox from causing the collapsible panel from operating --%>
                                <input type="submit" style="display: none;" />
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label2" runat="server" Text="分析项目代码：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_AICode" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                           <td class="float_Middle"></td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label6" runat="server" Text="分类：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                 <asp:DropDownList ID="drop_type" runat="server" CssClass="mTextBox" 
                                    AutoPostBack="True" Height="22px">
                                     <asp:ListItem  Value="1" Text="常规" Selected="True"></asp:ListItem>
                                     <asp:ListItem  Value="0" Text="其他"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>  <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label4" runat="server" Text="时效性：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_num" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                                <asp:RadioButtonList ID="rbl_dw" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem value="0" Text="小时" Selected="true"></asp:ListItem>
                                     <asp:ListItem value="1" Text="天"></asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </td>
                                 <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label9" runat="server" Text="保存剂用量：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_dose" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                        </tr>
                         <tr> <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label5" runat="server" Text="分析方法：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBoxList runat="server" ID="cbl_Method" 
                                     RepeatDirection="Vertical"></asp:CheckBoxList>
                            </td>
                            <td class="float_Middle">
                               <asp:Button runat="server" ID="btn_choose" Text="..."  OnClick="btn_choose_Click"/>
                            </td></tr>
                        <tr><td></td><td colspan="6">
                            <asp:Panel runat="server" GroupingText="分析方法选择" ID="panel_choose" Visible="false">
                                 <table style="width: 100%">
                                    <tr>
                                        <td style="width: 60%">
                                        </td>
                                        <td style="" align="right">
                                            <asp:ImageButton  runat="server" ID="btn_Close" src="../images/close.gif" onclick="btn_Close_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        
                                        <td style="">
                              
                                  <asp:TextBox runat="server" ID="txt_method" Width="700px" autocomplete="off"
                                    Height="22px" />
                                <ajaxToolkit:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="AutoCompleteExtender2"
                                    TargetControlID="txt_method" ServicePath="AutoGet.asmx" ServiceMethod="GetUserList"
                                    MinimumPrefixLength="0" CompletionInterval="200" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:">
                                    <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                                    </Animations>
                                </ajaxToolkit:AutoCompleteExtender>

                                <script type="text/javascript">
                                    // Work around browser behavior of "auto-submitting" simple forms
                                    var frm = document.getElementById("aspnetForm");
                                    if (frm) {
                                        frm.onsubmit = function () { return false; };
                                    }
                                </script>

                                <%-- Prevent enter in textbox from causing the collapsible panel from operating --%>
                                <input type="submit" style="display: none;" />
                                <%-- </div>--%> 
                                            </td> <td> <asp:Button ID="btn_addm" runat="server" OnClick="btn_addm_Click" />
                                        </td></tr></table> </asp:Panel>
                            </td></tr>
                         <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label10" runat="server" Text="大致消耗时间：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_Effnum" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                                 <asp:RadioButtonList ID="rbl_Effdw" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem value="0" Text="小时" Selected="true"></asp:ListItem>
                                     <asp:ListItem value="1" Text="分钟"></asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </td>
                             <td></td>
                        </tr>
                        <tr>
                            <td style="width: 38px" class="Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label11" runat="server" Text="备注：" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList" colspan="5">
                                <asp:TextBox ID="txt_Remark" runat="server" CssClass="mTextBox" Width="600px"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="float_Middle" style="height: 4px"></td>
                            <td align="center" class="float_Padding" colspan="6">
                                <asp:Button ID="btn_Cancel" runat="server" CssClass="mButton" OnClick="btn_Cancel_Click" Text="取消" />
                                <asp:Button ID="btn_OK" runat="server" CssClass="mButton" OnClick="btn_OK_Click" Text="确定" />
                            </td>
                            <td class="float_Middle" style="height: 4px"></td>
                            </tr>
                       
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
