<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportSampleData.aspx.cs" Inherits="Sample_ReportSampleData" Title="报告编制" %>

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
<span style="font-size: 10pt"> 样品编号</span>
</td>
<td style="height: 4px" class="ctrlList">
    <asp:TextBox ID="txt_sampleQuery" runat="server"></asp:TextBox>
</td>
<td style="height: 4px" class="Middle"></td>
    <td style="height: 4px; text-align: right" class="titleList">
<span style="font-size: 10pt">分析项目</span>
</td>
<td style="height: 4px" class="ctrlList">
    <asp:TextBox ID="txt_Itemquery" runat="server"></asp:TextBox>
</td>
    </tr><tr>
<td style="text-align: right" class="titleList">
<span style="font-size: 10pt">
<asp:Label ID="Label6" runat="server" Text="接样时间"></asp:Label></span>
</td>                          
<td class="ctrlList">
<asp:TextBox ID="txt_QueryTime" runat="server" CssClass="mTextBox"></asp:TextBox>
</td>
<td style="height: 4px" class="Middle"></td>
    <td style="height: 4px; text-align: right" class="titleList">
<span style="font-size: 10pt">项目状态</span>
</td>
<td style="height: 4px" class="ctrlList">
    <asp:DropDownList ID="drop_status" runat="server">
        <asp:ListItem Value="0" Text="未指派"></asp:ListItem>
        <asp:ListItem Value="1" Text="未领取"></asp:ListItem>
        
    </asp:DropDownList>
</td>

<td style="height: 4px" class="LeftandRight"></td><td>
<asp:Button ID="btn_query" runat="server" Text="查询" onclick="btn_query_Click" CssClass="mButton" /><asp:Button ID="btn_zp" runat="server" Text="指派保存" onclick="btn_zp_Click" CssClass="mButton" Width="75px" /></td>
<td style="height: 4px" class="LeftandRight"></td>
</tr>
</tbody>
</table>
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption=""
                
                OnRowCreated="grdvw_List_RowCreated"  OnRowDataBound="grdvw_List_RowDataBound"
                
                OnSelectedIndexChanging="grdvw_List_RowSelecting"  >
                <Columns>
                     <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="autoid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
           
        </ContentTemplate>
        <Triggers>
          
        </Triggers>
    </asp:UpdatePanel>
    
 
   
   
</asp:Content>
