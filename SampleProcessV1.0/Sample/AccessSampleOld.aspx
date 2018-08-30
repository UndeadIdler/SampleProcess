<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="AccessSampleOld.aspx.cs" Inherits="AccessSampleOld" Title="样品接收" %>

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
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" OnPageIndexChanging="grdvw_List_PageIndexChanging" AllowPaging="true" PageSize="12" 
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
                        <td align="center">
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="mLayer" style=" display:none; left: 223px; top: 143px; width: 800px; height:200px " id="detail">
         <table style="width: 100%">
                <tr>
                    <td style="width: 60%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="hiddenDetail()" />
                    </td>
                </tr>
            </table>
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
                <table style="width: 800px">
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
                                <asp:Label ID="Label3" runat="server" Text="报告标识" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_ReportID" runat="server" Width="125px"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="lbl_AccessTime" runat="server" Text="委托时间" CssClass="mLabel"></asp:Label>
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
                                <asp:TextBox ID="txt_wtdepart" runat="server" autocomplete="off" />
                               <ajaxToolkit:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx1" ID="AutoCompleteExtender4"
                                    TargetControlID="txt_wtdepart" ServicePath="AutoComplete.asmx" ServiceMethod="GetClientList"
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
                                var behavior = $find('AutoCompleteEx1');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx1')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx1')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                                    </Animations>
                                </ajaxToolkit:AutoCompleteExtender>


                            </td>
                            <td style="width: 5px" class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                                <td style="width: 38px" class="Middle">
                                </td>
                                <td style="text-align: right; font-size: x-small;" class="titleList">
                                    <asp:Label ID="Label13" runat="server" Text="项目名称"></asp:Label>
                                </td>
                                <td>
                                     <asp:TextBox ID="txt_Projectname" runat="server"></asp:TextBox>
                                </td>
                                <td class="float_Middle">
                                </td>
                                <td style="font-size: x-small;" class="titleList">
                                  <asp:Label ID="Label7" runat="server" Text="项目负责人"></asp:Label>
                                </td>
                                <td class="ctrlList">
                                 <%-- <asp:TextBox ID="txt_xmfzr" runat="server"></asp:TextBox>--%>
                                  <asp:TextBox runat="server" ID="txt_xmfzr" Width="125px" autocomplete="off"
                                    Height="22px" />
                                <ajaxToolkit:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="AutoCompleteExtender2"
                                    TargetControlID="txt_xmfzr" ServicePath="AutoComplete.asmx" ServiceMethod="GetUserList"
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
                        <tr>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                            <td class="float_Padding" colspan="5" align="center">
                              
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" Text="保存" CssClass="mButton">
                                </asp:Button>
                                 <asp:Button ID="btn_Save" OnClick="btn_Save_Click" runat="server" Text="提交" OnClientClick="return confirm('确定已完成样品的添加，提交此报告？');" CssClass="mButton">
                                </asp:Button>
                            </td>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
         <div id="DetailAnalysis" class="mLayer" style="display: none; position:relative; left: 0px; width: 800px; top: 0px;" >
       
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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
                <asp:Panel ID="Panel1" BackColor="#F5F9FF" runat="server" GroupingText="样品单列表" ForeColor="#2292DD"
                    Width="800px">
                     <table class="container">
                        <tbody>
                           <tr>
                           <td colspan="7"> </td>
                                <td align="right" >
                               
                                    <asp:Button ID="btn_AddSample" OnClick="btn_AddSample_Click" runat="server" Text="添加样品"
                                        CssClass="mButton"></asp:Button>
                                  
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
                   
                </asp:Panel>
        
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_OKSample" EventName="Click"></asp:AsyncPostBackTrigger>
                </Triggers>
        </asp:UpdatePanel>
         <div id="DetailAnalysisAdd" class="mLayer" style="display: none; position:relative;left: 0px; width: 800px;
        top: 0px; height: 130px">
         <table style="width: 100%">
                <tr>
                    <td style="width: 60%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="hiddenDetailAnalysisAdd()" />
                    </td>
                </tr>
            </table>
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
                <table style="margin: 0px; width: 735px">
                    <tbody>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="text-align: left" class="float_Middle" colspan="4">
                                <asp:Label ID="lbl_SampleDo" runat="server" CssClass="mLabelTitle" Text="Label"></asp:Label>
                            </td>
                            <td style="width: 5px">
                            </td>
                            <td style="width: 115px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                           <td></td>
                           <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="lbl_SampleType" runat="server" Text="样品类别" Width="85px"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox runat="server" ID="DropList_SampleType" Width="185px" autocomplete="off" OnTextChanged="txt_SampleType_OnTextChanged"
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
                             <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label12" runat="server" Text="采样时间">
                                    
                                </asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_SampleTime" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> <td></td>
                          <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label11" runat="server" Text="样品来源"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_SampleSource" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="185px"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExxx" ID="AutoCompleteExtender3"
                                    TargetControlID="txt_SampleSource" ServicePath="AutoComplete.asmx" ServiceMethod="GetSampleSourceList"
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
                                var behavior = $find('AutoCompleteExxx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExxx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExxx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                                     </Animations>
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>  <td style="width: 5px" class="float_Middle">
                            </td>
                            <td style="text-align: right; font-size: x-small; width: 100px;" ">
                                <asp:Label ID="Label8" runat="server" Text="接样时间/现场分析时间"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_AccessSampleTime" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td>
                             <td></td>
                            </tr>
                           <%-- <tr> <td></td>
                          <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label27" runat="server" Text="检测地点"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="tbTestPlace" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td>  <td style="width: 5px" class="float_Middle">
                            </td>
                            <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label28" runat="server" Text="检测时间"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="tbTestTime" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td>
                             <td></td>
                            </tr>--%>
                             <tr> <td></td>
                          <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label29" runat="server" Text="采样人/现场检测人"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="tbSampleMan" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td>
                            </tr>
                        
                            <tr><td colspan="6">
                        <asp:Panel ID="Panel_Sample"  BackColor="#F5F9FF" runat="server" GroupingText="样品列表" ForeColor="#2292DD" Font-Size="X-Small"
                    Width="735px">
                              <table width="100%">
                                <tr> <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label6" runat="server" Text="样品编号"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleID1" runat="server" Width="100px"></asp:TextBox><asp:HiddenField ID="hid_ID1" runat="server" />
                            </td>
                             <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label10" runat="server" Text="采样点"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleAddress1" runat="server" Width="100px"></asp:TextBox>
                            </td>
                              <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label22" runat="server" Text="样品性状"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_xz1" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="100px"></asp:TextBox>

                            </td> 
                            <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label27" runat="server" Text="数量"></asp:Label>
                            </td>
                           
                            <td class="ctrlList" style="width:50px">
                                <asp:TextBox ID="txt_num1" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="50px"></asp:TextBox>
                            </td>
                                    <td class="ctrlList" style="text-align: right; font-size: x-small;">
                                 <asp:CheckBox ID="ck_xcflag_1" runat="server"  Text="详细" AutoPostBack="true" OnCheckedChanged="ck_xcflag_OnCheckedChanged"/>
                                
                            </td></tr>
                            <tr>
                         <td style="text-align: right; font-size: x-small; " class="titleList" colspan="1">
                             <asp:LinkButton ID="lbtn_chose1" runat="server" OnClientClick="showchose()" OnClick="lbtn_chose1_OnClick">添加分析项目</asp:LinkButton>
                            </td><td  colspan="8"><asp:TextBox ID="txt_Item1" runat="server" CssClass="mTextBox"  Height="25px" OnTextChanged="txt_Item1_OnTextChanged" AutoPostBack="true"
                                    Width="100%"></asp:TextBox><asp:HiddenField ID="hid_Item1" runat="server" />   </td>

                            </tr>
                            <tr>
                        <td class="ctrlList" colspan="9"> 
                             <asp:GridView ID="grv_Item1" runat="server" CssClass="mGridView" Caption="分析项目"
                         OnRowCreated="grv_Item_RowCreated" OnRowDataBound="grv_Item_RowDataBound"
                        OnRowDeleting="grv_Item_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                               
                            </td>
                          
                        </tr>

                         <tr>
                              <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label14" runat="server" Text="样品编号"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleID2" runat="server" Width="100px"></asp:TextBox><asp:HiddenField ID="hid_ID2" runat="server" />
                            </td>
                               <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label23" runat="server" Text="采样点"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleAddress2" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label15" runat="server" Text="样品性状"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_xz2" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="100px"></asp:TextBox>
                            </td>
                              <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label28" runat="server" Text="数量"></asp:Label>
                            </td>
                           
                            <td class="ctrlList" style="width:50px">
                                <asp:TextBox ID="txt_num2" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="50px"></asp:TextBox>
                            </td>
                             <td class="ctrlList" style="text-align: right; font-size: x-small;">
                                 <asp:CheckBox ID="ck_xcflag_2" runat="server"  Text="详细" AutoPostBack="true" OnCheckedChanged="ck_xcflag_OnCheckedChanged"/>
                                
                            </td>

                         </tr>
                            <tr>
                         <td style="text-align: right; font-size: x-small; " class="titleList" colspan="1">
                             <asp:LinkButton ID="lbtn_chose2" runat="server" OnClientClick="showchose()" OnClick="lbtn_chose2_OnClick">添加分析项目</asp:LinkButton>
                            </td><td  colspan="8"><asp:TextBox ID="txt_Item2" runat="server" CssClass="mTextBox"   Height="25px" OnTextChanged="txt_Item2_OnTextChanged" AutoPostBack="true"
                                    Width="100%"></asp:TextBox><asp:HiddenField ID="hid_Item2" runat="server" /></tr>
                          <tr>
                        <td class="ctrlList" colspan="9"> 
                             <asp:GridView ID="grv_Item2" runat="server" CssClass="mGridView" Caption="分析项目" OnRowDataBound="grv_Item_RowDataBound"
                        OnRowCreated="grv_Item_RowCreated"
                        OnRowDeleting="grv_Item_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                               
                                    
                            </td>
                          
                        </tr>
                         <tr>
                              <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label16" runat="server" Text="样品编号"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleID3" runat="server" Width="100px"></asp:TextBox><asp:HiddenField ID="hid_ID3" runat="server" />
                            </td>
                               <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label24" runat="server" Text="采样点"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleAddress3" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label17" runat="server" Text="样品性状"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_xz3" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="100px"></asp:TextBox>
                            </td>
                              <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label34" runat="server" Text="数量"></asp:Label>
                            </td>
                           
                            <td class="ctrlList" style="width:50px">
                                <asp:TextBox ID="txt_num3" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="50px"></asp:TextBox>
                            </td>
                             <td class="ctrlList" style="text-align: right; font-size: x-small;">
                                 <asp:CheckBox ID="ck_xcflag_3" runat="server"  Text="详细" AutoPostBack="true" OnCheckedChanged="ck_xcflag_OnCheckedChanged"/>
                                
                            </td></tr>
                            <tr>
                         <td style="text-align: right; font-size: x-small;" class="titleList" colspan="1">
                             <asp:LinkButton ID="lbtn_chose3" runat="server" OnClientClick="showchose()" OnClick="lbtn_chose3_OnClick">添加分析项目</asp:LinkButton>
                            </td>
                             <td colspan="8"> <asp:TextBox ID="txt_Item3" runat="server" CssClass="mTextBox"   Height="25px" OnTextChanged="txt_Item3_OnTextChanged" AutoPostBack="true"
                                    Width="100%"></asp:TextBox><asp:HiddenField ID="hid_Item3" runat="server" /></td></tr>
                            <tr>
                         <td class="ctrlList" colspan="9">
                             <asp:GridView ID="grv_Item3" runat="server" CssClass="mGridView" Caption="分析项目" 
                        OnRowCreated="grv_Item_RowCreated" OnRowDataBound="grv_Item_RowDataBound"
                        OnRowDeleting="grv_Item_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                               
                                    
                            </td>
                          
                        </tr>
                         <tr>
                              <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label18" runat="server" Text="样品编号"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleID4" runat="server" Width="100px"></asp:TextBox><asp:HiddenField ID="hid_ID4" runat="server" />
                            </td>
                               <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label25" runat="server" Text="采样点"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleAddress4" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label19" runat="server" Text="样品性状"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_xz4" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="100px"></asp:TextBox>
                            </td>
                              <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label35" runat="server" Text="数量"></asp:Label>
                            </td>
                           
                            <td class="ctrlList" style="width:50px">
                                <asp:TextBox ID="txt_num4" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="50px"></asp:TextBox>
                            </td>
                             <td class="ctrlList" style="text-align: right; font-size: x-small;">
                                 <asp:CheckBox ID="ck_xcflag_4" runat="server"  Text="详细" AutoPostBack="true" OnCheckedChanged="ck_xcflag_OnCheckedChanged"/>
                                
                            </td></tr>
                            <tr>
                         <td style="text-align: right; font-size: x-small;" class="titleList" colspan="1">
                             <asp:LinkButton ID="lbtn_chose4" runat="server" OnClientClick="showchose()" OnClick="lbtn_chose4_OnClick">添加分析项目</asp:LinkButton>
                            </td>
                            <td colspan="8">    <asp:TextBox ID="txt_Item4" runat="server" CssClass="mTextBox"   Height="25px" OnTextChanged="txt_Item4_OnTextChanged" AutoPostBack="true"
                                    Width="100%"></asp:TextBox><asp:HiddenField ID="hid_Item4" runat="server" /> </tr>
                              <tr>
                        <td class="ctrlList" colspan="9">
                        <asp:GridView ID="grv_Item4" runat="server" CssClass="mGridView" Caption="分析项目"
                         OnRowCreated="grv_Item_RowCreated" OnRowDataBound="grv_Item_RowDataBound"
                        OnRowDeleting="grv_Item_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                             
                                    
                            </td>
                          
                        </tr>
                         <tr> <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label20" runat="server" Text="样品编号"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleID5" runat="server" Width="100px"></asp:TextBox><asp:HiddenField ID="hid_ID5" runat="server" />
                            </td>
                               <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label26" runat="server" Text="采样点"></asp:Label>
                            </td>
                             <td style="width: 100px">
                                <asp:TextBox ID="txt_SampleAddress5" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; font-size: x-small;width: 75px;" ">
                                <asp:Label ID="Label21" runat="server" Text="样品性状"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_xz5" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="100px"></asp:TextBox>
                            </td>
                              <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label36" runat="server" Text="数量"></asp:Label>
                            </td>
                           
                            <td class="ctrlList" style="width:50px">
                                <asp:TextBox ID="txt_num5" runat="server" CssClass="mTextBox" Height="21px" 
                                    Width="50px"></asp:TextBox>
                            </td>
                             <td class="ctrlList" style="text-align: right; font-size: x-small;">
                                 <asp:CheckBox ID="ck_xcflag_5" runat="server"  Text="详细" AutoPostBack="true" OnCheckedChanged="ck_xcflag_OnCheckedChanged"/>
                                
                            </td>
                            </tr>
                            <tr>
                         <td style="text-align: right; font-size: x-small; " class="titleList" colspan="1">
                             <asp:LinkButton ID="lbtn_chose5" runat="server" OnClientClick="showchose()" OnClick="lbtn_chose5_OnClick">添加分析项目</asp:LinkButton>
                            </td>
                                <td colspan="8">  <asp:TextBox ID="txt_Item5" runat="server" CssClass="mTextBox"  Height="25px" OnTextChanged="txt_Item5_OnTextChanged" AutoPostBack="true"
                                    Width="100%"></asp:TextBox><asp:HiddenField ID="hid_Item5" runat="server" />
                                    </td>
                                </tr>
                            <tr>
                        <td class="ctrlList" colspan="9">
                            <asp:GridView ID="grv_Item5" runat="server" CssClass="mGridView" Caption="分析项目"
                        OnRowCreated="grv_Item_RowCreated" OnRowDataBound="grv_Item_RowDataBound"
                        OnRowDeleting="grv_Item_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                              
                            </td>
                          
                        </tr>
                     </table>
                        </asp:Panel>
                        </td></tr>
                         <%--<tr> <td><asp:HiddenField ID="hid_status" runat="server" /></td>
                          <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label30" runat="server" Text="分析人"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_fxman" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td> <td></td>
                             <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label31" runat="server" Text="分析时间"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_fxtime" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td> <td></td>
                            </tr>--%>
                        <%--<tr>
                            <td></td>
                          <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label32" runat="server" Text="校核人"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_jhman" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td> <td></td>
                             <td style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label33" runat="server" Text="校核时间"></asp:Label>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_jhtime" runat="server" CssClass="mTextBox" Height="21px"
                                    Width="125px"></asp:TextBox>
                            </td> <td></td>
                            </tr>--%>
                     <tr>
                            <td colspan="8" align="center">
                               
                                <asp:Button ID="btn_OKSample" OnClick="btn_SampleSave_Click" runat="server" CssClass="mButton"
                                    Text="保存"></asp:Button>
                            
              
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
               </div> 
    </div>
    </div>
         <div id="div_choose" class="mLayer" style="display: none; position:absolute;left:200px; width: 800px;
         top: 520px;">
         <table style="width: 100%">
                <tr>
                    <td style="width: 60%">
                    </td>
                    <td style="" align="right">
                        <img alt="关闭" src="../images/close.gif" onclick="unshowchose()" />
                    </td>
                </tr>
            </table>
        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
  <table style="border-right: 2px groove; border-top: 2px groove; border-left: 2px groove;
                                        width: 100%; border-bottom: 2px groove">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList ID="cb_analysisList" runat="server" RepeatDirection="Horizontal"  Font-Size="X-Small" RepeatColumns="6"
                                                        Width="100%">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>

                                            <tr>
                                            <td>

                                        <tr><td> <asp:Button ID="btn_item"  runat="server" CssClass="mButton" OnClientClick="unshowchose()" OnClick="btn_item_Click"
                                    Text="确定"></asp:Button></td></tr>
                                    </table>  
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lbtn_chose1" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
                               </asp:UpdatePanel>

<%--</asp:UpdateProgress>  --%>                          
    </div>

   
          
 
   
   
</asp:Content>
