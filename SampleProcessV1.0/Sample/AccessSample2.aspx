<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="AccessSample2.aspx.cs" Inherits="Sample_AccessSample2" Title="������Ŀ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
                            <img src="../Images/minipro.gif" />ͨѶ�У����Ե�....</span>
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
                        <td style="text-align: right; font-size:x-small;"  class="titleList">
                            <span style="font-size: 10pt">��Ʒ���</span>
                        </td>
                        <td style="height: 4px" class="ctrlList">
                            <asp:TextBox ID="txt_samplequery" runat="server"></asp:TextBox>
                            
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="text-align: right" class="titleList">
                            <span style="font-size: 10pt">
                                <asp:Label ID="Label1" runat="server" Text="����ʱ��"></asp:Label></span>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_QueryTime" runat="server" CssClass="mTextBox"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                        <td>
                            <asp:Button ID="btn_query" runat="server" Text="��ѯ" OnClick="btn_query_Click" CssClass="mButton" />
                        </td>
                        <td style="height: 4px" class="LeftandRight">
                        </td>
                    </tr>
                    <%--<tr><td colspan="6"></td><td>
    </td></tr>--%>
                </tbody>
            </table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption="" AllowPaging="True"
                OnPageIndexChanging="grdvw_List_PageIndexChanging" 
                OnRowCreated="grdvw_List_RowCreated" OnSelectedIndexChanging="grdvw_List_RowSelecting" >
                <Columns>
                    <asp:TemplateField HeaderText="���"></asp:TemplateField>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_OKAnalysis" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="btn_SampleSave" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
   
    <div id="DetailAnalysisAdd" class="mLayer" style="display:none;left: 96px; width: 739px; top: 500px;
        height: 130px">
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
            <ProgressTemplate>
                <table style="width: 321px">
                    <tr>
                        <td style="height: 7px" colspan="3">
                            <span style="font-size: 10pt">
                                <img src="../Images/minipro.gif" />ͨѶ�У����Ե�....</span>
                        </td>
                    </tr>
                </table>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="margin: 0px; width: 735px">
                    <tbody>
                        <tr>
                            <td style="text-align: left" class="float_Middle">
                                <asp:Label ID="lbl_sample" Style="width: 735px; font-size: smaller" runat="server"
                                    Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" BackColor="#F5F9FF" runat="server" Width="100%" Height="50px"
                                    Font-Size="X-Small" GroupingText="������Ŀ">
                                    <table class="container">
                                        <tbody>
                                            <tr>
                                                <td style="text-align: right" class="titleList">
                                                    <span style="font-size: 10pt">
                                                        <asp:Label ID="lbl_AnalysisMainItem" runat="server" Text="������Ŀ����"></asp:Label></span></span>
                                                </td>
                                                <td class="ctrlList">
                                                    <asp:DropDownList ID="DropList_AnalysisMainItem" runat="server" AutoPostBack="true"
                                                        CssClass= "mDropDownList" OnSelectedIndexChanged="DropList_AnalysisMainItem_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 48px" class="float_Middle">
                                                    <td class="titleList">
                                                    </td>
                                                    <td class="ctrlList">
                                                    </td>
                                                    <td style="width: 48px" class="float_Middle">
                                                        <asp:Label ID="Label7" runat="server" Text="����"></asp:Label>
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
                                                    <asp:CheckBoxList ID="cb_analysisList" runat="server" RepeatDirection="Horizontal"  Font-Size="X-Small" RepeatColumns="6"
                                                        Width="100%">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label2" runat="server" Text="�Զ��������Ŀ" CssClass="mLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_own" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <%--<tr><td colspan="8">  <asp:Panel ID="Panel2" BackColor="#F5F9FF" runat="server" Width="100%" Height="50px"
                                    Font-Size="X-Small" GroupingText="���������ϴ�">
                            <asp:FileUpload ID="FileUpload1" runat="server"  Width="191px" Height="22px" /> 
                            <asp:Button ID="btn_upload" runat="server" CssClass="mButton" Height="23px" 
                                onclick="btn_upload_Click" Text="�ϴ�" Width="66px" />
                            </asp:Panel></td></tr>--%>
                        <tr>
                           
                            <td align="center">
                                <asp:Button ID="btn_CancelAnalysis" runat="server" CssClass="mButton" 
                                    OnClick="btn_CancelAnalysis_Click" Text="ȡ��" />
                           
                                <asp:Button ID="btn_OKAnalysis" runat="server" CssClass="mButton" 
                                    OnClick="btn_OKAnalysis_Click" Text="ȷ��" />
                           
                               <asp:Button ID="btn_SampleSave"  OnClick="btn_SampleSave_Click" CssClass="mButton"  runat="server" Text="�ύ" /></td>
                        </tr>
                    </tbody>
                </table>
                <asp:Panel ID="Panel4" BackColor="#F5F9FF" runat="server" GroupingText="������Ʊ�ע" ForeColor="#2292DD"
                        Width="800px">
                        <table class="container">
                            <tbody>
                                <tr>
                                     <td style="width: 100px">
                                        <span style="font-size: 10pt;">
                                            <asp:Label ID="Label6" CssClass="mLabel" runat="server" Text="��ע"></asp:Label></span>
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
               
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
