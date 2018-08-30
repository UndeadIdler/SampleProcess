<%@ Page Language="C#" AutoEventWireup="true" CodeFile="attachment.aspx.cs" Inherits="Sample_UpLoad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报告上传</title>
    <link href="../App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />
   <meta http-equiv="Pragma"   content="no-cache" />   
   <meta    http-equiv="Cache-Control"   content="no-cache"/>   
   <meta    http-equiv="Expires"   content="0"/>   
    <script language="javascript" type="text/javascript">
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <table style="border-right: 2px groove; border-top: 2px groove; border-left: 2px groove;
            width: 800px; border-bottom: 2px groove">
            <tbody>
                <tr>
                    <td>
                        <asp:Panel ID="Panel2" BackColor="#F5F9FF" runat="server" Width="100%"
                            Font-Size="Large" GroupingText="报告上传">
                            <asp:FileUpload ID="FileUpload1"  runat="server" Width="500px" />
                            <asp:Button ID="btn_upload" runat="server" CssClass="mButton" Height="23px" OnClick="btn_upload_Click"
                                Text="上传" Width="66px" />
                            <p>
                                <asp:Label ID="LabMessage1" runat="server" Text="" Style="width: 400px; font-size: Large"></asp:Label></p>
                            <p>
                                <asp:Label ID="LabMessage2" runat="server" Text="" Style="width: 400px; font-size: Large"></asp:Label></p>
                                <br />
                       
                        <asp:GridView ID="GridView_Report" runat="server"  CssClass="mGridView"  
                                OnRowDeleting="GridView_Report_RowDeleting"  
                                OnRowCreated="GridView_Report_RowCreated" 
                                
                                onselectedindexchanged="GridView_Report_SelectedIndexChanged" >
                <Columns>
                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
                        </asp:GridView>
                         </asp:Panel>
                    </td>
                </tr>
            </tbody>
            </table>
    </div>
    </form>
</body>
</html>
