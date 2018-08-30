<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportSignSend1.aspx.cs" Inherits="Sample_ReportSignSend1" Title="报告装订" %>

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
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <%--<asp:DropDownList id="drop_UserLevel" runat="server" CssClass="mDropDownList"  AutoPostBack="true">
</asp:DropDownList>--%>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label6" runat="server" Text="接样时间"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="btn_query_Click" CssClass="mButton" />
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                    </tr>
                    <%--<tr><td colspan="6"></td><td>
    </td></tr>--%>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_Report" runat="server" CssClass="mGridView" Caption="" AllowPaging="True"
                OnPageIndexChanging="grdvw_Report_PageIndexChanging" OnRowCreated="grdvw_Report_RowCreated"
                OnSelectedIndexChanging="grdvw_Report_RowSelecting" OnRowEditing="grdvw_Report_RowEditing">
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
            <%-- <asp:AsyncPostBackTrigger ControlID="btn_SampleSave" EventName="Click"></asp:AsyncPostBackTrigger>--%>
            <asp:AsyncPostBackTrigger ControlID="btn_ExitAnalysisItem" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <div id="DetailAnalysis" class="mLayer" style=" display:none;left: 96px; width: 739px; top: 500px;
        height: 130px">
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
                <asp:Panel ID="Panel1" BackColor="#F5F9FF" runat="server" GroupingText="报告装订" ForeColor="#2292DD"
                    Width="800px">
                    <table style="margin: 0px; width: 735px" class="container">
                        <tbody>
                            <tr>
                                <td style="text-align: left" class="float_Middle" colspan="7">
                                    <asp:Label ID="Label8" runat="server" CssClass="mLabelTitle" Visible="false" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="lbl_ReportID" runat="server" CssClass="mLabel" Text="报告标识" Width="75px"></asp:Label></span>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txt_ReportID" runat="server" Style="width: 100%"></asp:TextBox>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label10" CssClass="mLabel" runat="server" Text="时间"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="mTextBox"></asp:TextBox>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label11" CssClass="mLabel" runat="server" Text="项目类型" Width="75px"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_itemname" runat="server"></asp:TextBox>
                                    <%--<asp:DropDownList ID="DropList_ItemList" runat="server" CssClass="ctrlList">
                                </asp:DropDownList>--%>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <span style="font-size: 10pt">
                                        <asp:Label ID="Label12" CssClass="mLabel" runat="server" Text="委托单位" Width="75px"></asp:Label></span>
                                </td>
                                <td class="ctrlList">
                                    <asp:TextBox ID="txt_Client" runat="server"></asp:TextBox>
                                    <%--<asp:DropDownList ID="DropList_SampleType" runat="server" CssClass="ctrlList">
                                </asp:DropDownList>--%>
                                </td>
                                <td style="width: 48px" class="float_Middle">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="grdvw_ReportDetail" runat="server" CssClass="mGridView" Caption=""
                        AllowPaging="True" OnPageIndexChanging="grdvw_ReportDetail_PageIndexChanging"
                        OnRowCreated="grdvw_ReportDetail_RowCreated" OnSelectedIndexChanging="grdvw_ReportDetail_RowSelecting"
                        OnRowEditing="grdvw_ReportDetail_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table class="container">
                        <tbody>
                            <tr>
                                <td width="200px">
                                    <span style="font-size: 10pt;">
                                        <asp:Label ID="Label2" CssClass="mLabel" runat="server" Text="备注"></asp:Label></span>
                                </td>
                                <td width="650px">
                                    <asp:TextBox ID="txt_ReportRemark" ReadOnly="true" runat="server" TextMode="MultiLine"
                                        Height="67px" Width="650px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                  <%--  <asp:Panel ID="Panel2" BackColor="#F5F9FF" runat="server" GroupingText="报告校核" ForeColor="#2292DD"
                        Width="800px">
                        <table class="container">
                            <tbody>
                                <tr>
                                    <td style="text-align: right" class="titleList">
                                        <span style="font-size: 10pt">
                                            <asp:Label ID="Label3" runat="server" CssClass="mLabel" Text="收到报告时间：" Width="117px"></asp:Label>
                                        </span>
                                    </td>
                                    <td class="ctrlList">
                                        <asp:TextBox ID="txt_checktime" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right" class="titleList">
                                        <span style="font-size: 10pt">
                                            <asp:Label ID="Label4" runat="server" CssClass="mLabel" Text="校核人："></asp:Label>
                                        </span>
                                    </td>
                                    <td class="ctrlList">
                                        <asp:TextBox ID="txt_person" runat="server" ReadOnly="True" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200px">
                                        <span style="font-size: 10pt;">
                                            <asp:Label ID="Label13" CssClass="mLabel" runat="server" Text="备注"></asp:Label></span>
                                    </td>
                                    <td width="650px" colspan="3">
                                        <asp:TextBox ID="txt_CheckRemark" runat="server" TextMode="MultiLine" Height="67px"
                                            Width="650px"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>--%>
                   <asp:Panel ID="Panel3" BackColor="#F5F9FF" runat="server" GroupingText="报告审核" ForeColor="#2292DD"
                        Width="800px">
                        <table class="container">
                            <tbody>
                                <tr>
                                    <td width="150px">
                                        <span style="font-size: 10pt">
                                            <asp:Label ID="Label0" runat="server" CssClass="mLabel" Text="收到报告时间：" Width="117px"></asp:Label>
                                        </span>
                                    </td>
                                    <td class="ctrlList">
                                        <asp:TextBox ID="txt_VerifyTime" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                               
                              <asp:Label ID="lbl_verfiyName" runat="server" CssClass="mLabel" Text="审核人："></asp:Label>
                                </span>
                            </td>
                            <td class="ctrlList">
                             
                                <asp:TextBox ID="txt_verfiyName" runat="server" ReadOnly="True" Width="120px"></asp:TextBox>
                            </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        <asp:Label ID="Label5" runat="server" CssClass="mLabel" Text="备注："></asp:Label>
                                    </td>
                                    <td width="650px" colspan="3">
                                        <asp:TextBox ID="txt_VerifyRemark" runat="server" Height="72px" Style="width: 98%; margin-left: 0px;"
                                            TextMode="MultiLine" ></asp:TextBox>
                                    </td>
                                </tr></tbody>
                        </table>
                    </asp:Panel>
                                <asp:Panel ID="Panel4" BackColor="#F5F9FF" runat="server" GroupingText="报告签发" ForeColor="#2292DD"
                        Width="800px">
                       
                     <table style="margin: 0px; width: 800px" class="container">
                    <tbody>
                        
                        <tr>
                           
                            <td>
                                <span style="font-size: 10pt">
                                <asp:Label ID="Label9" runat="server" CssClass="mLabel" Text="收到报告时间：" 
                                    ></asp:Label>
                                </span>
                            </td>
                             <td class="ctrlList">
                                <asp:TextBox ID="txt_signtime" runat="server"></asp:TextBox>
                            </td>
                           
                             <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                               
                               <asp:Label ID="lbl_sign" runat="server" CssClass="mLabel" Text="签发人："></asp:Label>
                                </span>
                            </td>
                            <td class="ctrlList">
                              <asp:TextBox ID="txt_sign" runat="server" ReadOnly="True" Width="120px"></asp:TextBox>
                            </td>
                           
                            </tr>
                        <tr>
                           
                                <td style="text-align: right; ">
                                    <asp:Label ID="Label15" runat="server" CssClass="mLabel"  Text="备注："></asp:Label>
                                </td>
                                
                           <td width="650px" colspan="3">
                                <asp:TextBox ID="txt_signremark" runat="server" Height="72px" Style="width: 98%" 
                                    TextMode="MultiLine" ></asp:TextBox>
                            </td>
                                
                            
                        </tr>
                                
                            </tbody>
                        </table>
                    </asp:Panel>
                   
                                <asp:Panel ID="Panel5" BackColor="#F5F9FF" runat="server" GroupingText="报告装订人填写" ForeColor="#2292DD"
                        Width="800px">
                       
                     <table style="margin: 0px; width: 800px" class="container">
                    <tboday>
                        <tr>
                           
                                <td style="text-align: right; width:150px">
                                    <asp:Label ID="Label1" runat="server" CssClass="mLabel"  Text="收到时间："></asp:Label>
                                </td>
                                
                             <td width="650px" colspan="7">
                                <asp:TextBox ID="txt_receivetime" runat="server"></asp:TextBox>
                            </td>
                                
                            
                        </tr>
                       <tr>
                           
                                <td style="text-align: right; width:150px">
                                    <asp:Label ID="Label14" runat="server" CssClass="mLabel"  Text="装订时间："></asp:Label>
                                </td>
                                
                            <td colspan="7" >
                                 <asp:TextBox ID="txt_endtime" runat="server"></asp:TextBox>
                            </td>
                                
                            
                        </tr>
                    <tr>
                                    <td align="center" colspan="4">
                                        <asp:Button ID="Button2" OnClick="btn_CancelReport_Click" runat="server" Text="取消"
                                            CssClass="mButton"></asp:Button>
                                        <asp:Button ID="Button5" OnClick="btn_BackReport_Click" Visible="false" runat="server" Text="回退"
                                            CssClass="mButton"></asp:Button>
                                        <asp:Button ID="Button3" OnClick="btn_SaveReport_Click" runat="server" Text="确定"
                                            CssClass="mButton"></asp:Button>
                                        <asp:Button ID="Button4" OnClick="btn_SampleReport_Click" runat="server" Text="提交"
                                            CssClass="mButton"></asp:Button>
                                    </td>
                                </tr>
                                 </tbody>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
            <%-- <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btn_OKAnalysis" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_CancelAnalysis" EventName="Click"></asp:AsyncPostBackTrigger>
               
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
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
                <table style="margin: 0px; width: 735px" class="container">
                    <tbody>
                        <tr>
                            <td style="text-align: left" class="float_Middle" colspan="7">
                                <asp:Label ID="lbl_Type" runat="server" CssClass="mLabelTitle" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                                    <asp:Label ID="lbl_SampleID" runat="server" CssClass="mLabel" Text="样品编号" Width="75px"></asp:Label></span>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txt_SampleID" runat="server" Style="width: 100%"></asp:TextBox>
                            </td>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td>
                            </td>
                            <td>
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
                <asp:GridView ID="grdvw_ListAnalysisItem" runat="server" AllowPaging="True" Caption=""
                    CssClass="mGridView" OnPageIndexChanging="grdvw_ListAnalysisItem_PageIndexChanging"
                    OnRowCreated="grdvw_ListAnalysisItem_RowCreated">
                    <Columns>
                        <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <table class="container">
                    <tbody>
                        <tr>
                            <td width="5%">
                                <span style="font-size: 10pt;">
                                    <asp:Label ID="Label7" CssClass="mLabel" runat="server" Text="备注"></asp:Label></span>
                            </td>
                            <td width="90%">
                                <asp:TextBox ID="txt_doremark" runat="server" TextMode="MultiLine" Height="67px"
                                    Width="750px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btn_ExitAnalysisItem" OnClick="btn_ExitAnalysisItem_Click" runat="server"
                                    Text="取消" CssClass="mButton"></asp:Button>
                                <%-- <asp:Button ID="btn_Save" OnClick="btn_Save_Click" runat="server" Text="确定" CssClass="mButton"> </asp:Button >
                            <asp:Button ID="btn_SampleSave" OnClick="btn_SampleSave_Click" runat="server" Text="提交" CssClass="mButton"> </asp:Button >--%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <%-- <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btn_OKAnalysis" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_CancelAnalysis" EventName="Click"></asp:AsyncPostBackTrigger>
                
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>
