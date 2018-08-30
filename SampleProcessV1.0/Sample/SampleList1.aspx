<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="SampleList1.aspx.cs" Inherits="Query_SampleList1" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
     <style type="text/css"> 
    table{border-collapse:collapse;border-spacing:0px; width:100%; border:#4F7FC9 solid 1px;} 
    table td{border:1px solid #4F7FC9;height:25px; text-align:center; font-size:12px;} 
    table th{ background-color:#E3EFFF; border:#4F7FC9 solid 1px; white-space:nowrap; height:22px; border-top:0px;border-left:1px; font-size:14px;}   
          
</style> 
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
            <div style=" text-align:center; position:inherit; top:1px; width:800px; height: 15px;"><FONT style='WIDTH: 102.16%; COLOR: #2292DD;font-size:14pt; LINE-HEIGHT: 150%; FONT-FAMILY: 楷体_GB2312; HEIGHT: 35px'><b>样品列表</b></font></div>
<div style=" text-align:center; position:inherit; top:1px; width:900px; height: 15px;"><%=outputSum %></div>
<br>
<iframe src="SampleListItem.aspx" width="950px" height="580px" scrolling="no" frameborder="0"></iframe>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
