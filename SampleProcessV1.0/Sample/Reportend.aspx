<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="Reportend.aspx.cs" Inherits="Sample_ReportSignSend" Title="报告装订完成" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js"></script>
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
                            <span style="font-size: 10pt">样品编号</span>
                        </td>
                        <td style="height: 4px" class="ctrlList">
                            <asp:TextBox ID="txt_samplequery" runat="server"></asp:TextBox>
                            <%--<asp:DropDownList id="drop_UserLevel" runat="server" CssClass="mDropDownList"  AutoPostBack="true">
</asp:DropDownList>--%>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label1" runat="server" Text="接样时间"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_QueryTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                        <td>
                            <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" CssClass="mButton" />
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                    </tr>
                    <%--<tr><td colspan="6"></td><td>
    </td></tr>--%>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption="" AllowPaging="True"
                OnPageIndexChanging="grdvw_List_PageIndexChanging" OnRowCreated="grdvw_List_RowCreated"
                OnSelectedIndexChanging="grdvw_List_RowSelecting">
                <Columns>
                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--<table class="container">
                <tbody>
                    <tr>
                        <td class="LeftandRight">
                            &nbsp;
                        </td>
                        <td class="titleList">
                        </td>
                        <td class="ctrlList">
                        </td>
                        <td class="Middle">
                        </td>
                        <td>
                        </td>
                        <td class="LeftandRight">
                            <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" Text="添加" CssClass="mButton">
                            </asp:Button>
                        </td>
                    </tr>
                </tbody>
            </table>--%>
        </ContentTemplate>
        <Triggers>
            <%--            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click"></asp:AsyncPostBackTrigger>
            --%>
            <asp:AsyncPostBackTrigger ControlID="btn_ExitAnalysisItem" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <div id="detail" class="mLayer" style="display: none; left: 96px; width: 739px; top: 500px;
        height: 130px">
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
                <asp:Panel ID="Panel2" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="样品信息">
                    <table style="margin: 0px; width: 800px" class="container">
                        <tbody>
                            <tr>
                                <td style="text-align: left" class="float_Middle" colspan="7">
                                    <asp:Label ID="lbl_Type" runat="server" CssClass="mLabelTitle" Text="Label" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="lbl_SampleID" runat="server" CssClass="mLabel" Text="样品编号" Width="75px"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_SampleID" runat="server" CssClass=" mTextBox"></asp:TextBox>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                </td>
                                <td class="ctrlList">
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="lbl_AccessTime" CssClass="mLabel" runat="server" Text="接样时间"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_AccessTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="lbl_ItemName" CssClass="mLabel" runat="server" Text="项目类型" Width="75px"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_ItemList" runat="server"></asp:TextBox>
                                    <%--<asp:DropDownList ID="DropList_ItemList" runat="server" CssClass="ctrlList">
                                </asp:DropDownList>--%>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="lbl_SampleType" CssClass="mLabel" runat="server" Text="样品类型" Width="75px"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_SampleType" runat="server"></asp:TextBox>
                                    <%--<asp:DropDownList ID="DropList_SampleType" runat="server" CssClass="ctrlList">
                                </asp:DropDownList>--%>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="grdvw_ListAnalysisItem" runat="server" CssClass="mGridView" Caption=""
                        AllowPaging="True" OnPageIndexChanging="grdvw_ListAnalysisItem_PageIndexChanging"
                        OnRowCreated="grdvw_ListAnalysisItem_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="报告编制">
                    <table style="margin: 0px; width: 800px" class="container">
                        <tbody>
                            <tr>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label3" runat="server" CssClass="mLabel" Text="时间：" Width="75px"></asp:Label>
                                    </span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_checktime" runat="server"></asp:TextBox>
                                </td>
                                <td class="float_Middle" style="width: 48px">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label4" runat="server" CssClass="mLabel" Text="人员："></asp:Label>
                                    </span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_person" runat="server" ReadOnly="True" Width="120px"></asp:TextBox>
                                </td>
                                <td class="float_Middle" style="width: 48px">
                                </td>
                                <td class="titleList" style="text-align: right">
                                </td>
                                <td class="ctrlList" style="width: 98px">
                                </td>
                                <td class="float_Middle" style="width: 43px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5px;" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label2" runat="server" CssClass="mLabel" Text="备注："></asp:Label>
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txt_remark" runat="server" Height="72px" Style="width: 98%" TextMode="MultiLine"
                                        Width="203px"></asp:TextBox>
                                </td>
                                <td style="width: 43px" class="float_Middle">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <%--  <asp:Panel ID="Panel3" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="报告校核">
                     <table style="margin: 0px; width: 800px" class="container">
                    <tbody>
                        
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                                <asp:Label ID="Label5" runat="server" CssClass="mLabel" Text="时间：" 
                                    Width="75px"></asp:Label>
                                </span>
                            </td>
                             <td class="ctrlList">
                                <asp:TextBox ID="txt_VerifyTime" runat="server"></asp:TextBox>
                            </td>
                            <td class="float_Middle" style="width: 48px">
                            </td>
                             <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                               
                                <asp:Label ID="Label6" runat="server" CssClass="mLabel" Text="人员："></asp:Label>
                                </span>
                            </td>
                            <td class="ctrlList">
                                <asp:TextBox ID="txt_VerifyMan" runat="server" ReadOnly="True" Width="120px"></asp:TextBox>
                            </td>
                            <td class="float_Middle" style="width: 48px">
                            </td>
                            <td class="titleList" style="text-align: right">
                            </td>
                            <td class="ctrlList" style="width: 98px">
                            </td>
                            <td class="float_Middle" style="width: 43px">
                            </td></tr>
                        <tr>
                           <td style="width: 5px; " class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label7" runat="server" CssClass="mLabel"  Text="备注："></asp:Label>
                                </td>
                                
                            <td colspan="7">
                                <asp:TextBox ID="txt_VerifyRemark" runat="server" Height="72px" Style="width: 98%" 
                                    TextMode="MultiLine" Width="203px"></asp:TextBox>
                            </td>
                                
                            <td style="width: 43px" class="float_Middle">
                            </td>
                        </tr>
                       
                         </tbody>
                </table>
                   
                </asp:Panel>--%>
                <asp:Panel ID="Panel4" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="报告审核">
                    <table style="margin: 0px; width: 800px" class="container">
                        <tbody>
                            <tr>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label8" runat="server" CssClass="mLabel" Text="时间：" Width="75px"></asp:Label>
                                    </span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_signtime" runat="server"></asp:TextBox>
                                </td>
                                <td class="float_Middle" style="width: 48px">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label9" runat="server" CssClass="mLabel" Text="人员："></asp:Label>
                                    </span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_user" runat="server"></asp:TextBox>
                                </td>
                                <td class="float_Middle" style="width: 48px">
                                </td>
                                <td class="titleList" style="text-align: right">
                                </td>
                                <td class="ctrlList" style="width: 98px">
                                </td>
                                <td class="float_Middle" style="width: 43px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5px;" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label10" runat="server" CssClass="mLabel" Text="备注："></asp:Label>
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txt_signremark" runat="server" Height="72px" Style="width: 98%"
                                        TextMode="MultiLine" Width="203px"></asp:TextBox>
                                </td>
                                <td style="width: 43px" class="float_Middle">
                                </td>
                            </tr>
                        </tboday>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel5" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="报告签发与装订">
                    <table style="margin: 0px; width: 800px" class="container">
                        <tboday>
                        <tr>
                           <td style="width: 5px; " class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label11" runat="server" CssClass="mLabel"  Text="收到时间："></asp:Label>
                                </td>
                                
                            <td colspan="7">
                                <asp:TextBox ID="txt_receivetime" runat="server"></asp:TextBox>
                            </td>
                                
                            <td style="width: 43px" class="float_Middle">
                            </td>
                        </tr>
                        <tr>
                           <td style="width: 5px; " class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label12" runat="server" CssClass="mLabel"  Text="装订时间："></asp:Label>
                                </td>
                                
                            <td colspan="7">
                                 <asp:TextBox ID="txt_endtime" runat="server"></asp:TextBox>
                            </td>
                                
                            <td style="width: 43px" class="float_Middle">
                            </td>
                        </tr>
                         <tr>
                            <td colspan="10" align="center">                            
                                    <asp:Button ID="btn_OKAnalysis" runat="server" CssClass="mButton" 
                                        OnClick="btn_OKAnalysis_Click" Text="提交" Width="88px" />                          
                                <asp:Button ID="btn_ExitAnalysisItem" runat="server" CssClass="mButton" 
                                    OnClick="btn_ExitAnalysisItem_Click" Text="取消" />
                             </td>
                        </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
