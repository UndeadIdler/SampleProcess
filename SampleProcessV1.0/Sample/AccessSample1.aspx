<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="AccessSample1.aspx.cs" Inherits="AccessSample1" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
            <table class="container">
                <tbody>
                    <tr>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                        <td style="text-align: right; font-size: x-small;" class="titleList">
                            <span style="font-size: 10pt">报告标识</span>
                        </td>
                        <td style="height: 4px" class="ctrlList">
                            <asp:TextBox ID="txt_samplequery" runat="server"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label4" runat="server" Text="时间"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_QueryTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td>
                            <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" CssClass="mButton" />
                        </td>
                        <td style="width: 50px">
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" CssClass="btn" Text="添加" />
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" OnPageIndexChanging="grdvw_List_PageIndexChanging"
                AllowPaging="true" PageSize="12" OnSelectedIndexChanging="grdvw_List_RowSelecting"
                OnRowDeleting="grdvw_List_RowDeleting" OnRowEditing="grdvw_List_RowEditing" OnRowCreated="grdvw_List_RowCreated">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="autoid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField></asp:BoundField>
                    <asp:BoundField></asp:BoundField>--%>
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
    <div id="detail" class="mLayer" style="display: none; left: 96px; width: 900px;
        top: 500px; ">
        <table style="width: 100%">
                <tr>
                    <td style="width: 60%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="hiddenDetail()" />
                    </td>
                </tr>
            </table>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
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
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" BackColor="#F5F9FF" runat="server" ForeColor="#2292DD" Width="800px">
                    <table style="margin: 0px; width: 800px" class="container">
                        <tbody>
                            <tr>
                                <td style="text-align: left" class="float_Middle" colspan="7">
                                    <asp:Label ID="lbl_Type" runat="server" Text="Label" CssClass="mLabelTitle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 38px" class="Middle">
                                </td>
                                <td style="width: 150px" class="titleList">
                                    <asp:Label ID="Label3" runat="server" Text="报告标识" CssClass="mLabel"></asp:Label>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_ReportID" runat="server" Width="125px"></asp:TextBox>
                                </td>
                                <td class="float_Middle">
                                </td>
                                <td style="width: 150px" class="titleList">
                                    <asp:Label ID="lbl_AccessTime" runat="server" Text="接到时间" CssClass="mLabel"></asp:Label>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_CreateDate" runat="server" Height="21px" Width="125px"></asp:TextBox>
                                </td>
                                <td style="width: 38px" class="Middle">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 38px" class="Middle">
                                </td>
                                <td style="text-align: right; font-size: x-small;" class="titleList">
                                    <asp:Label ID="Label1" runat="server" Text="区域"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropList_client" CssClass="ctrlList" Width="125px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="float_Middle">
                                </td>
                                <td style="font-size: x-small;" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="lbl_ItemName" runat="server" Text="项目类型"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:DropDownList ID="drop_ItemList" runat="server" CssClass="ctrlList" Width="125px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 5px" class="float_Middle">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 38px" class="Middle">
                                </td>
                                <td style="text-align: right; font-size: x-small;" class="titleList">
                                    <asp:Label ID="Label5" runat="server" Text="紧急程度"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drop_level" CssClass="ctrlList" Width="125px" runat="server">
                                        <asp:ListItem Value="0">一般</asp:ListItem>
                                        <asp:ListItem Value="1">紧急</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="float_Middle">
                                </td>
                                <td style="font-size: x-small;" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label9" runat="server" Text="委托单位"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_wtdepart" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 5px" class="float_Middle">
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 38px" class="Middle">
                                </td>
                                <td style="text-align: right; font-size: x-small;" class="titleList">
                                    <asp:Label ID="Label10" runat="server" Text="项目类型"></asp:Label>
                                </td>
                                <td>
                                     <asp:TextBox ID="txt_itemname" runat="server"></asp:TextBox>
                                </td>
                                <td class="float_Middle">
                                </td>
                                <td style="font-size: x-small;" class="titleList">
                                  
                                </td>
                                <td class="ctrlList">
                                  
                                </td>
                                <td style="width: 5px" class="float_Middle">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 38px" class="Middle">
                                </td>
                                <td style="width: 150px" class="titleList">
                                    <asp:Label ID="Label2" runat="server" Text="备注" CssClass="mLabel"></asp:Label>
                                </td>
                                <td style="font-size: x-small; width: 600px; text-align: right;" colspan="4">
                                    <asp:TextBox ID="drop_urgent" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td class="float_Middle">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="grdvw_ReportDetail" runat="server" CssClass="mGridView" Caption=""
                        AllowPaging="True" OnPageIndexChanging="grdvw_ReportDetail_PageIndexChanging"
                        OnSelectedIndexChanging="grdvw_ReportDetail_RowSelecting" OnRowCreated="grdvw_ReportDetail_RowCreated"
                        OnRowDeleting="grdvw_ReportDetail_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table class="container">
                        <tbody>
                            <tr>
                                <td colspan="8" align="center">
                                    <asp:Button ID="btn8" OnClick="btn_AddSample_Click" runat="server" CssClass="mButton"
                                        Text="新增样品"></asp:Button>
                                   
                                    <asp:Button ID="btn_OKSample" OnClick="btn_OK_Click" runat="server" CssClass="mButton"
                                        Text="确定"></asp:Button>
                                    <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" CssClass="mButton"
                                        Text="提交"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel3" BackColor="#F5F9FF" runat="server" GroupingText="样品单详细信息"
                    ForeColor="#2292DD" Width="800px">
                    <table style="margin: 0px; width: 800px; " >
                        <tbody>
                            <tr>
                                <td style="width: 151px">
                                </td>
                                <td style="text-align: left" class="float_Middle" colspan="4">
                                    <asp:Label ID="lbl_SampleDo" runat="server" CssClass="mLabelTitle" Text="添加"></asp:Label>
                                </td>
                                <td style="width: 5px">
                                </td>
                                <td style="width: 115px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; font-size: x-small;">
                                    <asp:Label ID="Label6" runat="server" Text="样品编号"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txt_SampleID" runat="server" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 5px" class="float_Middle">
                                </td>
                               <%-- <td style="text-align: right; font-size: x-small;">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label7" runat="server" Text="报告标识"></asp:Label>
                                    </span>
                                </td>
                                <td class="ctrlList">
                                    <asp:DropDownList ID="txt_report" runat="server" CssClass="mDropDownList" Width="150px">
                                    </asp:DropDownList>
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="text-align: right; font-size: x-small;">
                                    <asp:Label ID="lbl_SampleType" runat="server" Text="样品类型"></asp:Label>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox runat="server" ID="DropList_SampleType" Width="125px" autocomplete="off"
                                        Height="22px" />
                                    <ajaxToolkit:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExx" ID="AutoCompleteExtender1"
                                        TargetControlID="DropList_SampleType" ServicePath="AutoComplete.asmx" ServiceMethod="GetSampleTypeList"
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
                                var behavior = $find('AutoCompleteExx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExx')._height" EndValue="0" />
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
                                </td>
                                <td style="width: 5px" class="float_Middle">
                                </td>
                                <td style="text-align: right; font-size: x-small; width: 115px;" class="titleList">
                                    <asp:Label ID="Label8" runat="server" Text="接样时间"></asp:Label>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_SampleTime" runat="server" CssClass="mTextBox" Height="21px"
                                        Width="125px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td style="width: 115px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <table style="margin: 0px; width: 850px">
                                        <tbody>
                                            <tr>
                                                <td style="text-align: left" class="float_Middle">
                                                    <asp:Label ID="lbl_sample" Style="width: 800px; font-size: smaller" runat="server"
                                                        Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel2" BackColor="#F5F9FF" runat="server" Width="800px" 
                                                        Font-Size="X-Small" GroupingText="分析项目">
                                                        <table class="container">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="text-align: right" class="titleList">
                                                                        <span style="font-size: 10pt">
                                                                            <asp:Label ID="lbl_AnalysisMainItem" runat="server" Text="分析项目大类"></asp:Label></span></span>
                                                                    </td>
                                                                    <td class="ctrlList">
                                                                        <asp:DropDownList ID="DropList_AnalysisMainItem" runat="server" AutoPostBack="true"
                                                                            CssClass="mDropDownList" OnSelectedIndexChanged="DropList_AnalysisMainItem_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 48px" class="float_Middle">
                                                                        <td class="titleList">
                                                                        </td>
                                                                        <td class="ctrlList">
                                                                        </td>
                                                                        <td style="width: 48px" class="float_Middle">
                                                                            <asp:Label ID="Label13" runat="server" Text="数量"></asp:Label>
                                                                            <td class="titleList">
                                                                                <asp:TextBox ID="txt_num" runat="server" CssClass="mTextBox"></asp:TextBox>
                                                                            </td>
                                                                            <td class="ctrlList">
                                                                            </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table style="border-right: 2px groove; border-top: 2px groove; border-left: 2px groove;
                                                            width: 100%; border-bottom: 2px groove">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBoxList ID="cb_analysisList" runat="server" RepeatDirection="Horizontal"
                                                                            Font-Size="X-Small" RepeatColumns="6" Width="100%">
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100px">
                                                                    <asp:Label ID="Label14" runat="server" Text="自定义分析项目" CssClass="mLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_own" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                   
                                                    <asp:Button ID="btn_OKAnalysis" runat="server" CssClass="mButton" OnClick="btn_OKAnalysis_Click"
                                                        Text="保存" />
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel4" BackColor="#F5F9FF" runat="server" GroupingText="原始数据上报备注" ForeColor="#2292DD"
                        Width="800px">
                        <table class="container">
                            <tbody>
                                <tr>
                                     <td style="width: 100px">
                                        <span style="font-size: 10pt;">
                                            <asp:Label ID="Label7" CssClass="mLabel" runat="server" Text="备注"></asp:Label></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_send" runat="server" ReadOnly="true" TextMode="MultiLine" Height="67px" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
            </ContentTemplate>
            <Triggers>
              <%--  <asp:AsyncPostBackTrigger ControlID="btn_AddSample" EventName="Click"></asp:AsyncPostBackTrigger>--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
