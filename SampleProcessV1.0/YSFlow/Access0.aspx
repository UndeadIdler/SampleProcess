﻿<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="Access0.aspx.cs" Inherits="Sample_Access0" Title="验收受理" %>

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
                        
                       <td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label4" runat="server" Text="时间"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_QueryTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                         <td class="ctrlList">
                           <asp:DropDownList ID="drop_status" runat="server">
                               <asp:ListItem Value="0" Selected="True" Text="受理中"></asp:ListItem>
                                <asp:ListItem Value="1.7"  Text="初审不通过"></asp:ListItem>
                                <asp:ListItem Value="2.5"  Text="函编写完成"></asp:ListItem>
                               <asp:ListItem Value="1"  Text="其他"></asp:ListItem>
                           </asp:DropDownList>
                        </td>
                         <td>
                            <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" CssClass="mButton" />
                        </td>
                       </tr>
                        <tr><td style="height: 4px" class="LeftandRight">
                        </td> 
                        
                       
                              <td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label12" runat="server" Text="委托单位"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_wt_Query" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td><td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label14" runat="server" Text="项目名称"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_project_Query" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                       <td></td>
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
    <div class="mLayer" style=" display:none; left: 223px; top: 120px; width: 800px; height:200px
        " id="detail">
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
                   
                        <tr>
                            <td style="height: 4px; text-align: left" class="float_Padding" colspan="4">
                                <asp:Label ID="lbl_Type" runat="server" Text="Label" CssClass="mLabelTitle"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="lbl_reportNO" runat="server" Text="报告标识" CssClass="mLabel" Width="150px"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_ReportID" runat="server" Width="125px"></asp:TextBox>
                            </td>
                            
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="lbl_AccessTime" runat="server" Text="接到时间/委托时间" CssClass="mLabel"></asp:Label>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_CreateDate" runat="server" Height="21px" Width="125px"></asp:TextBox>
                            </td>
                           
                             <td class="titleList" style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label11" runat="server" Text="任务类型" CssClass="mLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drop_rwtype" runat="server" CssClass="ctrlList" Width="125px" OnSelectedIndexChanged="drop_rwtype_SelectedIndexChanged" AutoPostBack="true">
                                   
                                    <asp:ListItem Value="1" Text="委托监测"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                     <tr>
                              
                                <td style="text-align: right; font-size: x-small;" class="titleList">
                                    <asp:Label ID="Label13" runat="server" Text="项目名称" ></asp:Label>
                                </td>
                                <td colspan="5">
                                     <asp:TextBox ID="txt_Projectname" runat="server" Width="100%" ></asp:TextBox>
                                </td>
                               </tr> 
                          
                        <tr><td colspan="6">
                    <asp:Panel ID="panel_wtdw" BackColor="#F5F9FF" runat="server" GroupingText="委托单位信息" ForeColor="#2292DD" Font-Size="Smaller"
                    Width="800px">
                        <table>
                        <tr>
                             <td style="font-size: x-small;" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label9" runat="server" Text="委托单位"></asp:Label></span>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txt_wtdepart" runat="server" autocomplete="off" AutoPostBack="true"  Width="100%" OnTextChanged="txt_wtdepart_TextChanged"/>
                               <ajaxToolkit:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx1" ID="AutoCompleteExtender4"
                                    TargetControlID="txt_wtdepart" ServicePath="Complete.asmx" ServiceMethod="GetClientList"
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
                            </tr>
                             <tr>
                             <td style="font-size: x-small;" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label6" runat="server" Text="单位地址"></asp:Label></span>
                            </td>
                            <td  colspan="5">
                                 <asp:TextBox ID="txt_address" runat="server" Height="21px" Width="100%"></asp:TextBox>
                            </td>

                        </tr>
                            <tr>
                             <td style="font-size: x-small;" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label7" runat="server" Text="联系人"></asp:Label></span>
                            </td>
                            <td class="ctrlList">
                                 <asp:TextBox ID="txt_lxman" runat="server" Height="21px" Width="125px"></asp:TextBox>
                            </td>
                            <td style="font-size: x-small;" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label8" runat="server" Text="手机"></asp:Label></span>
                            </td>
                            <td class="ctrlList">
                                 <asp:TextBox ID="txt_lxtel" runat="server" Height="21px" Width="125px"></asp:TextBox>
                            </td>
                                 <td style="font-size: x-small;" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label10" runat="server" Text="邮箱"></asp:Label></span>
                            </td>
                            <td class="ctrlList">
                                 <asp:TextBox ID="txt_lxemail" runat="server" Height="21px" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                            </table>
     </asp:Panel></td></tr>
                   <tr> <td colspan="6">
                    <asp:Panel ID="Panel2" BackColor="#F5F9FF" runat="server" GroupingText="监测要求" ForeColor="#2292DD" Font-Size="Smaller"
                    Width="800px">
                        <table>
                        <tr>
                           
                            <td class="titleList" style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label1" runat="server" Text="监测方式"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drop_mode" runat="server" CssClass="ctrlList" Width="125px">
                                </asp:DropDownList>
                            </td>
                          
                            <td class="titleList" style="font-size: x-small;"><span style="font-size: 10pt">
                                <asp:Label ID="lbl_ItemName" runat="server" Text="监测目的"></asp:Label>
                                </span></td>
                            <td class="ctrlList">
                                <asp:DropDownList ID="drop_ItemList" runat="server" CssClass="ctrlList" Width="125px">
                                </asp:DropDownList>
                            </td>
                            <td class="float_Middle" style="width: 5px"></td>
                       <td class="float_Middle" style="width: 5px"></td>
                           </tr>
                           <%-- <tr><td colspan="4">
                                <asp:Repeater ID="RepeaterSample" runat="server" >
                        <HeaderTemplate>
                        <table style="border-style: groove; border-color: inherit; border-width: 2px; width: 100%;">
                            
                         </HeaderTemplate>
                        <ItemTemplate>
                                <tr> <td style="text-align: right; font-size: x-small; width: 75px;" ">
                                <asp:Label ID="Label6" runat="server" Text="监测内容"></asp:Label>
                            </td>
                             <td style="width: 100px">
                           <asp:CheckBox runat="server" ID="item"/>  </td>
                           
                             <td style="width: 100px">
                                <asp:GridView ID="grv_a" runat="server" CssClass="mGridView"  Visible="true"
                       OnRowCreated="grv_a_RowCreated"  OnRowDataBound="grv_a_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:CheckBox ID="autoid" runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField> 
                    </Columns>
                    </asp:GridView>
                            </td></tr>
                            </ItemTemplate>
                                    </asp:Repeater>--%>
                               
                            </table>
                        </asp:Panel>
                       </td>
                       </tr>
                   
                    <tr>
                          
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label2" runat="server" Text="备注" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="font-size: x-small; width: 600px; text-align: right;" colspan="5">
                                <asp:TextBox ID="drop_urgent" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                            <td class="float_Padding" colspan="5" align="center">
                                <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" Text="取消" CssClass="mButton">
                                </asp:Button>
                                <asp:Button ID="btn_OK" OnClick="btn_OK_Click" runat="server" Text="确定" CssClass="mButton">
                                </asp:Button>
                                 <asp:Button ID="btn_Save" OnClick="btn_Save_Click" runat="server" Text="提交" CssClass="mButton">
                                </asp:Button>
                            </td>
                            <td style="height: 4px" class="float_Middle">
                            </td>
                        </tr>
                   <tr><td colspan="6"> <asp:Panel ID="panel_csyj" BackColor="#F5F9FF" runat="server" GroupingText="初审意见" ForeColor="#2292DD" Font-Size="Smaller"
                    Width="800px">
                        <table>
                        <tr>
                           
                            <td class="titleList" style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label5" runat="server" Text="是否通过"></asp:Label>
                            </td>
                             <td class="titleList" style="text-align: right; font-size: x-small;">
                               <asp:RadioButtonList runat="server" ID="rbl_wether" RepeatDirection="Horizontal" CssClass="mLabel"> 
                                   <asp:ListItem Value="0" Text="是">

                                   </asp:ListItem>
                                    <asp:ListItem Value="1" Text="否">

                                   </asp:ListItem>
                               </asp:RadioButtonList>
                            </td>
                            </tr>
                         <tr>
                            <td class="titleList" style="text-align: right; font-size: x-small; " >
                                <asp:Label ID="Label3" runat="server" Text="初审意见"></asp:Label>
                            </td>
                            <td style="width: 600px;" colspan="5">
                                <asp:TextBox ID="txt_remark1" runat="server" Height="75px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            
                            </tr>
                            </table>
                       </asp:Panel></td></tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
    
</asp:Content>
