<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportSampleQuery.aspx.cs" Inherits="Sample_ReportSampleQuery" Title="报告查询" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js" ></script>

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
<span style="font-size: 10pt"> 报告标识</span>
</td>
<td style="height: 4px" class="ctrlList">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<%--<asp:DropDownList id="drop_UserLevel" runat="server" CssClass="mDropDownList"  AutoPostBack="true">
</asp:DropDownList>--%>
</td>

<td style="height: 4px" class="Middle"></td>
<td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label6" runat="server" Text="收到时间"></asp:Label></span>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
    <td style="height: 4px" class="Middle"></td>
<td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label19" runat="server" Text="委托单位"></asp:Label></span>
                            </td>
                           
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_QueryWTDW" runat="server" CssClass="mTextBox"></asp:TextBox>
                            </td>
                             <td style="height: 4px" class="Middle"></td>
                             <td style="height: 4px" class="LeftandRight"></td><td>
                             <asp:Button ID="Button1" runat="server" Text="查询" 
        onclick="btn_query_Click" CssClass="mButton" /></td>
<td style="height: 4px" class="LeftandRight"></td>
</tr>
<%--<tr><td colspan="6"></td><td>
    </td></tr>--%>
</tbody>
</table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption=""
                AllowPaging="True" OnPageIndexChanging="grdvw_List_PageIndexChanging"
                OnRowCreated="grdvw_List_RowCreated"  
                
               OnRowEditing="grdvw_List_RowEditing" OnSelectedIndexChanging="grdvw_List_SelectedIndexChanging"
              >
                <Columns>
                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
            
           
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
                   <tbody>
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
                                   
                                </asp:DropDownList>
                            </td>
                        </tr>
                     <tr>
                              
                                <td style="text-align: right; font-size: x-small;" class="titleList">
                                    <asp:Label ID="Label1" runat="server" Text="项目名称"></asp:Label>
                                </td>
                                <td colspan="5">
                                     <asp:TextBox ID="txt_Projectname" runat="server" Width="100%"></asp:TextBox>
                                </td>
                               </tr> 
                           <tr>
                            <td style="text-align: right; font-size: x-small;" class="titleList">
                                <asp:Label ID="Label5" runat="server" Text="紧急程度"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drop_level" runat="server" CssClass="ctrlList" Width="125px">
                                    <asp:ListItem Value="0">一般</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                                <td style="font-size: x-small;" class="titleList">
                                  <asp:Label ID="Label14" runat="server" Text="项目负责人/报告编制人"></asp:Label>
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
                               <td style="width: 5px" class="float_Middle">
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
                            </tr>
                             <tr>
                             <td style="font-size: x-small;" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="Label3" runat="server" Text="单位地址"></asp:Label></span>
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
                                <asp:Label ID="Label4" runat="server" Text="监测方式"></asp:Label>
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
                            </table>
                        </asp:Panel>
                       </td>
                       </tr>
                   
                    <tr>
                          
                            <td style="width: 150px" class="titleList">
                                <asp:Label ID="Label12" runat="server" Text="备注" CssClass="mLabel"></asp:Label>
                            </td>
                            <td style="font-size: x-small; width: 600px; text-align: right;" colspan="5">
                                <asp:TextBox ID="drop_urgent" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td class="float_Middle">
                            </td>
                        </tr>
                    </tbody>
                </table>
           
                
            <asp:GridView ID="grdvw_ReportDetail" runat="server" CssClass="mGridView" Caption=""
                AllowPaging="True" OnPageIndexChanging="grdvw_ReportDetail_PageIndexChanging" OnRowEditing="grdvw_ReportDetail_RowEditing"
                OnRowCreated="grdvw_ReportDetail_RowCreated"  OnSelectedIndexChanging="grdvw_ReportDetail_SelectedIndexChanging" OnRowDataBound="grdvw_ReportDetail_RowDataBound"
               
               >
                <Columns>
                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
                 <asp:Panel ID="panel_data" BackColor="#F5F9FF" runat="server" GroupingText="现场监测数据" ForeColor="#2292DD" Font-Size="Smaller" Visible="false"
                    Width="800px">
                      <table style="width: 100%">
                <tr>
                    <td style="width: 60%">
                    </td>
                    <td style="" align="right">
                       <asp:ImageButton runat="server" ID="btn_xcclose" AlternateText="关闭"  OnClick="btn_xcclose_Click"  ImageUrl="../images/close.gif"/>
                    </td>
                </tr>
            </table>
                        <table>
                             <tr>
                           
                            <td class="titleList" style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label18" runat="server" Text="样品编号"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_SampleID" runat="server"  Width="100px"></asp:TextBox> 
                                <asp:HiddenField  runat="server" ID="hid_ID"/>
                            </td>
                          
                           <td class="titleList" style="text-align: right; font-size: x-small;">
                                <asp:Label ID="Label15" runat="server" Text="分析项目"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Item" runat="server"  Width="100px"></asp:TextBox> 
                                <asp:HiddenField  runat="server" ID="hidden_ItemID"/>
                            </td>
                          
                            <td class="titleList" style="font-size: x-small;"><span style="font-size: 10pt">
                                <asp:Label ID="Label16" runat="server" Text="分析值"></asp:Label>
                                </span></td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_value" runat="server"  Width="100px"></asp:TextBox>
                            </td></tr>
                        <tr>
                           
                           
                             <td class="titleList" style="font-size: x-small;"><span style="font-size: 10pt">
                                <asp:Label ID="Label17" runat="server" Text="分析方法"></asp:Label>
                                </span></td>
                            <td  colspan="5" >
                                <asp:RadioButtonList ID="rbl_method" runat="server"></asp:RadioButtonList>
                               
                            </td>
                          
                            </tr>
                            <tr><td> <asp:Button ID="btn_datasave" OnClick="btn_datasave_Click" runat="server" Text="保存分析数据"  CssClass="mButton"> </asp:Button ></td></tr>
                            </table>
                        </asp:Panel>
            <table class="container">
                <tbody>
                     <tr><td width="100px" > <span style="font-size: 10pt;" >
                                    <asp:Label ID="Label2" CssClass="mLabel" runat="server" Text="报告编号"></asp:Label></span></td>
                             <td width="750px" >
                                <asp:TextBox ID="txt_ReportNUM" runat="server" 
                                     Width="150px"></asp:TextBox>
                            </td></tr>
                        <tr><td width="100px" > <span style="font-size: 10pt;" >
                                    <asp:Label ID="Label13" CssClass="mLabel" runat="server" Text="备注"></asp:Label></span></td>
                             <td width="750px" >
                                <asp:TextBox ID="txt_ReportRemark" runat="server" TextMode="MultiLine" Height="67px" 
                                     Width="750px"></asp:TextBox>
                            </td></tr>
                    <tr>                       
                        <td align="center" colspan="5">
                          <%-- <asp:Button ID="Button2" OnClick="btn_CancelReport_Click" runat="server" Text="取消" CssClass="mButton">
                            </asp:Button> --%>
                          <%-- <asp:Button ID="Button5" OnClick="btn_BackReport_Click" runat="server" Text="回退"  CssClass="mButton"> </asp:Button >
                            <asp:Button ID="Button3" OnClick="btn_SaveReport_Click" runat="server" Text="确定" CssClass="mButton"> </asp:Button >
                            <asp:Button ID="Button4" OnClick="btn_SampleReport_Click" runat="server" Text="提交" CssClass="mButton"> </asp:Button >--%>
                          <asp:Button ID="btn_print" OnClick="btn_print_Click" runat="server" Text="导出快速报告" CssClass="mButton"> </asp:Button >
                             <asp:Button ID="btn_print2" OnClick="btn_print2_Click" runat="server" Text="导出检测报告" CssClass="mButton" Visible="false"> </asp:Button >
                        </td>
                    </tr>
                </tbody>
            </table>
           
            <%-- <asp:Panel ID="Panel3" BackColor="#F5F9FF"  runat="server" GroupingText="说明" ForeColor="#2292DD"
                Width="800px">
                <table class="container">
                    <tbody>
                        
                        <tr>
                           
                            <td width="750px" >
                               <asp:Label ID="Label_remark" Width="750px" runat="server" Text="（1）如果分析数据有问题，请单独点分析记录中的[退回]按钮；（2）若存在任务信息需要编辑，请点击下面的[回退]按钮；（3）提交报告，分析数据都已完成，方能提交！"></asp:Label>
                            </td>
                        </tr>
                      
                    </tbody>
                </table>
                </asp:Panel>--%>
            </ContentTemplate>
         
        </asp:UpdatePanel>
    </div>
</asp:Content>
