<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="SampleData.aspx.cs" Inherits="SampleData" Title="分析登记" %>

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
                            <span style="font-size: 10pt">分析项目</span>
                        </td>
                        <td style="height: 4px" class="ctrlList">
                            <asp:TextBox ID="txt_Itemquery" runat="server"></asp:TextBox>
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
                            <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click" TabIndex="0" CssClass="mButton" />
                        </td>
                       
                        
                        <td style="height: 4px" class="LeftandRight">
                          <asp:LinkButton ID="lbn_choose" runat="server" OnClick="lbn_choose_Click">列选择</asp:LinkButton>    
                        </td>
                    </tr>
                    <tr><td colspan="8">
                        <asp:CheckBoxList ID="cbl_choose" runat="server" Visible="false" RepeatDirection="Horizontal" RepeatColumns="6"  AutoPostBack="true" OnSelectedIndexChanged="cbl_choose_SelectedIndexChanged">
                            <asp:ListItem Value="7" Text="采样点"></asp:ListItem>
                            <asp:ListItem Value="8" Text="采样时间"></asp:ListItem>
                            <asp:ListItem Value="9" Text="接样时间"></asp:ListItem>

                          
                             <asp:ListItem Value="22" Text="执行标准"></asp:ListItem> 
                            <%-- <asp:ListItem Value="23" Text="标准值"></asp:ListItem> --%>
                             <asp:ListItem Value="11" Text="样品类型"></asp:ListItem> 
                             <asp:ListItem Value="12" Text="样品性状"></asp:ListItem> 
                             <asp:ListItem Value="13" Text="项目名称"></asp:ListItem> 
                             <asp:ListItem Value="14" Text="项目负责人"></asp:ListItem> 
                             <asp:ListItem Value="24" Text="分析指标"></asp:ListItem> 
                             <asp:ListItem Value="25" Text="是否走绿色通道"></asp:ListItem> 
                             
                        </asp:CheckBoxList></td></tr>
                </tbody>
            </table>
             
              <asp:Panel ID="Panel_Sample"  BackColor="#F5F9FF" runat="server" GroupingText="分析登记" TabIndex="0" ForeColor="#2292DD" Font-Size="X-Small"
                    Width="100%">
             <table class="container">
                <tbody>
                   
                    <tr>
                        <td></td>
                        <td   colspan="6">
  <asp:Button ID="btn_Save" runat="server" Text="保存选中项" OnClick="btn_Save_Click" CssClass="mButton" />
                    <asp:Button ID="btn_Submit" runat="server" Text="提交选中项" OnClick="btn_Submit_Click" CssClass="mButton" />
                        <asp:CheckBox ID="cbl_check" runat="server" Text="导出选择项"/>    <asp:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_Export_Click" CssClass="mButton" />
                        </td>
                       
                        
                         <%--<td style="height: 4px" class="LeftandRight">
                             <asp:Button ID="btn_back" runat="server" Text="退回选中项" OnClick="btn_Draw_Click" CssClass="mButton" />
                        </td>--%>
                    </tr>
                </tbody>
            </table> 
             
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Width="100%" 
             
               OnRowCreated="grdvw_List_RowCreated" OnRowDataBound="grdvw_List_RowDataBound" OnSelectedIndexChanging="grdvw_List_SelectedIndexChanging" OnRowEditing="grdvw_List_RowEditing" OnRowDeleting="grdvw_List_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="autoid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView> 
            </asp:Panel>  
        </ContentTemplate>
    </asp:UpdatePanel>
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
       <asp:Panel ID="Panel1"  BackColor="#F5F9FF" runat="server" GroupingText="质控登记" TabIndex="0" ForeColor="#2292DD" Font-Size="X-Small"
                    Width="100%">
             <table class="container">
                <tbody>
                    <tr>
                        <td></td>
                        <td   colspan="6">
 <asp:Button ID="btn_SaveZK" runat="server" Text="保存所有项" OnClick="btn_SaveZK_Click" CssClass="mButton" />
                   <asp:Button ID="btn_AddItem" runat="server" Text="追加一行" OnClick="btn_AddItem_Click" CssClass="mButton" />
                    <%--      <asp:CheckBox ID="CheckBox1" runat="server" Text="导出选择项"/>    <asp:Button ID="Button3" runat="server" Text="导出" OnClick="btn_Export_Click" CssClass="mButton" />--%>
                        </td>
                       
                    </tr>
                </tbody>
            </table> 
             <asp:GridView ID="grdvw_ZKList" runat="server" CssClass="mGridView" Width="100%"  Caption="监测分析质量统计表" OnRowCreated="grdvw_ZKList_RowCreated" OnRowDataBound="grdvw_ZKList_RowDataBound" OnSelectedIndexChanging="grdvw_ZKList_SelectedIndexChanging" OnRowDeleting="grdvw_ZKList_RowDeleting" >
                <Columns>
                     <asp:BoundField DataField="ID" HeaderText="ID" />
                     <asp:BoundField DataField="itemid" HeaderText="分析项目ID" />
                                                <asp:TemplateField HeaderText="分析项目">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drop_wrw" runat="server" Width="100px" OnSelectedIndexChanged="drop_wrw_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name" HeaderText="分析人" />
                                                <asp:TemplateField HeaderText="分析样品数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_analysisnum" Text='<%# Eval("analysisnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="现场平行样检查数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_scenejcnum" Text='<%# Eval("scenejcnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="现场平行样合格数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_scenehgnum" Text='<%# Eval("scenehgnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="实验室平行样检查数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_experimentjcnum" Text='<%# Eval("experimentjcnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="实验室平行样合格数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_experimenthgnum" Text='<%# Eval("experimenthgnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="加标回收检查数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_jbhsjcnum" Text='<%# Eval("jbhsjcnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="加标回收合格数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_jbhshgnum" Text='<%# Eval("jbhshgnum") %>' runat="server" Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="全程空白检查数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_alljcnum" Text='<%# Eval("alljcnum") %>' runat="server" Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="全程空白合格数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_allhgnum" Text='<%# Eval("allhgnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="密码样检查数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_mmjcnum" Text='<%# Eval("mmjcnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="密码样合格数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_mmhgnum" Text='<%# Eval("mmhgnum") %>' runat="server" Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="标样检查数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_byjcnum" Text='<%# Eval("byjcnum") %>' runat="server" Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="标样合格数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_byhgnum" Text='<%# Eval("byhgnum") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="总检查数">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_amount" Text='<%# Eval("amount") %>' runat="server"
                                                            Width="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                </Columns>
            </asp:GridView>
                 </asp:Panel>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
