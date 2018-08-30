﻿<%@ Page Title="" Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true" CodeFile="ExpertTree.aspx.cs" Inherits="Content_ExpertTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <%--<link href="../App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 319px;
        }
    </style>--%>
   
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
        <table style="width: 100%;">
        <tr>
            <td>
              <table style="width: 100%;">
                       
                           <tr>
                            <td style="width: 20%; vertical-align: top">
                                <asp:Label ID="Label2" runat="server" Text="目录名：" Width="64px"></asp:Label>
                                <asp:TextBox ID="TextBox_Catalog" runat="server" Width="96px"></asp:TextBox><br />
                                <asp:Button ID="Button_AddCatalog" runat="server" OnClick="Button2_Click" Text="添加目录"
                                    CssClass="buttonstyle" />
                                <asp:Button ID="Button_Delete" runat="server" OnClientClick="return confirm('确定要删除该目录及目录包含的文件？')" OnClick="Button3_Click" Text="删除" Width="79px"
                                    CssClass="buttonstyle" />
                            </td>
                            </tr>  </table> 
            </td>  <td style="vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td>
                            关键字
                        </td>
                        <td style="text-align: left; ">
                            <asp:TextBox ID="txt_keyword" runat="server" style="text-align: left" 
                                Width="333px"></asp:TextBox><asp:Button ID="Button_Search" runat="server" OnClick="Button1_Click" Text="搜索" Width="79px"
                                CssClass="buttonstyle" />
                        </td>
                        <td style="text-align: left">
                            
                        </td></tr>
                        </table>
                        
        </tr>
      
           
          <tr>
           <td style="width: 20%; vertical-align: top" align="left" >
                <asp:TreeView ID="TreeView1" runat="server" Height="169px" Width="169px" AutoGenerateDataBindings="true"
                    OnSelectedNodeChanged="TreeView2_SelectedNodeChanged" ImageSet="WindowsHelp"
                    ExpandDepth="1">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="0px" VerticalPadding="1px" />
                </asp:TreeView>
            </td>
                        <td rowspan="2" colspan="3" valign="top">
                <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Width="100%"
                   OnPageIndexChanging="grdvw_List_PageIndexChanging" AllowPaging="true" PageSize="20"
                OnRowDeleting="grdvw_List_RowDeleting" OnRowEditing="grdvw_List_RowEditing" 
                                OnRowCreated="grdvw_List_RowCreated" 
                                onselectedindexchanged="grdvw_List_SelectedIndexChanged" 
                                >
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="autoid" runat="server" />
                            </ItemTemplate>
                           
                    
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="关键字" DataField="KEYWORDS" ></asp:BoundField>
                         <asp:BoundField HeaderText="文件名称" DataField="NAME" ></asp:BoundField>
                          <asp:BoundField HeaderText="上传时间" DataField="uploaddate" ></asp:BoundField>
                          <asp:BoundField HeaderText="文件目录" DataField="FILEPATH" ></asp:BoundField>
                          
                         
                    </Columns>
                </asp:GridView>
                </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
          
        </ContentTemplate>
        <Triggers>
         <asp:AsyncPostBackTrigger ControlID="TreeView1"  EventName="SelectedNodeChanged"></asp:AsyncPostBackTrigger>
       
        <asp:AsyncPostBackTrigger ControlID="Button_Delete"  EventName="Click"></asp:AsyncPostBackTrigger>
          <asp:AsyncPostBackTrigger ControlID="Button_AddCatalog"  EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
     <asp:Panel ID="Panel1" runat="server">
                    <table style="width: 100%;">
                        <tr><td style="width:20%"></td>
                            <td style=" text-align: left; vertical-align: top;">
                                <asp:Label ID="Label1" runat="server" Text="请输入关键字："></asp:Label>
                                <asp:TextBox ID="TextBox_KeyWord" runat="server" Width="99px"></asp:TextBox>
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="buttonstyle" Width="350px"  />
                                 <asp:ImageButton ID="Button_AddFile" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/UpLoad.jpg"
                        OnClick="ImageButton2_Click" CausesValidation="False" />
                              <%--  <asp:Button ID="" runat="server" Text="添加文件" OnClick="Button4_Click"
                                    CssClass="buttonstyle" />--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
</asp:Content>
