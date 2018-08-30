<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="SampleItemQuery.aspx.cs" Inherits="Sample_SampleQuery" Title="样品查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script language="javascript" type="text/javascript" src="../js/cal/WdatePicker.js" ></script>
    <script language="javascript" type="text/javascript" src="../js/position.js"></script>
       <script type="text/javascript">                
        var    hkey_root,hkey_path,hkey_key   
        hkey_root="HKEY_CURRENT_USER"   
        hkey_path="\\Software\\Microsoft\\Internet Explorer\\PageSetup\\"   
        //设置网页打印的页眉页脚为空   
      function   pagesetup_null()   
      {   
          try{   
              var   RegWsh   =   new   ActiveXObject("WScript.Shell")   
              hkey_key="header"           
              RegWsh.RegWrite(hkey_root+hkey_path+hkey_key,"")   
              hkey_key="footer"   
              RegWsh.RegWrite(hkey_root+hkey_path+hkey_key,"")   
          }catch(e){}   
      }   
    </script>
  <script   language= "javascript " type="text/javascript"> 
function dayin()   
{
  var code = "<center>";
  code+=document.all.dayin.innerHTML;
  code+="<object id='wb' name='wb'　 height='0' width='0' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'></object>"
  code+="<link href='../App_Themes/Default/StyleSheet.css' rel='stylesheet' type='text/css' />"
  code+="<input type='button' value='打印预览' style='border: solid 1px #a3a3a3; background-color: #e6e6e6; font-size: 12px' class='noprint' onclick='javascript:wb.execWB(7,1);' />&nbsp;&nbsp; <input type='button' value=' 打 印 ' style='border: solid 1px #a3a3a3; background-color: #e6e6e6; font-size: 12px' class='noprint' onclick='javascript:wb.execWB(6,1);' /></center>"
  code+="<style media='print' type='text/css'> .Noprint{display:none;} .PageNext{page-break-after: always;} </style>"
  var newwin=window.open('','','');
  newwin.opener = null;
  newwin.document.write(code);
   newwin.document.getElementById('ctl00_ContentPlaceHolder1_grdvw_List').style.width="700px";
  newwin.document.close();
  
}

</script>
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
          <table style="border-right: 2px groove; border-top: 2px groove; border-left: 2px groove;
                                        width: 800px; border-bottom: 2px groove">
                                        <tbody>
                                            <tr>
                                                <td>
        <asp:Panel ID="Panel6" BackColor="#F5F9FF" runat="server" GroupingText="查询条件" ForeColor= "#2292DD" Width="800px">
                 
                   <table class="container">
                <tbody>
                        <tr>
                        <td style="width:120px;text-align: right; height: 25px;" class="titleList" >
                            <asp:Label ID="Label13" runat="server" Text="样品编号"></asp:Label>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_samplequery" Width="156px" runat="server" Height="21px"></asp:TextBox>
                            
                        </td> 
                        <td class="Middle">
                        </td>
                       <td style="width:120px;text-align: right; " class="titleList" >
                           
                                <asp:Label ID="Label14" runat="server" Text="项目类型"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           
                            <asp:DropDownList id="txt_item" runat="server" Width="156px"></asp:DropDownList>
                        </td>
                       
                        <td>
</td><td  class="Middle">
                        </td>
                        
                    </tr> 
                             <tr>
                        <td style="width:120px;text-align: right; height: 25px;" class="titleList" >
                            <asp:Label ID="Label2" runat="server" Text="样品来源"></asp:Label>
                        </td>
                        <td class="ctrlList">
                            <asp:TextBox ID="txt_QuerySource" Width="156px" runat="server" Height="21px"></asp:TextBox>
                            
                        </td> 
                        <td class="Middle">
                        </td>
                       <td style="width:120px;text-align: right; " class="titleList" >
                           
                           <asp:Label ID="Label3" runat="server" Text="报告标识"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           
                           <asp:TextBox ID="txt_QueryBS" Width="156px" runat="server" Height="21px"></asp:TextBox>
                        </td>
                        <td class="ctrlList"><asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click"  
                                CssClass="mButton" Height="24px" Width="100px" /></td>
                        <td>
</td><td  class="Middle">
                        </td>
                        
                    </tr> 

                    
                    </tbody></table>
            <asp:LinkButton ID="lb_show" runat="server"  OnClick="lb_show_Click">其他条件</asp:LinkButton>
                    <asp:Panel ID="Panel_Other" BackColor="#F5F9FF" runat="server" Width="800px" Height="50px" 
                                    Font-Size="X-Small"  ForeColor= "#2292DD" Visible="false">
                    <table class=" container"><tbody>
                        <tr>
                         <td style="width:120px;text-align: right; " class="titleList" >
                           
                                <asp:Label ID="Label16" runat="server" Text="样品类型"></asp:Label>
                        </td>
                        <td class="ctrlList">
                          
                            <asp:DropDownList id="txt_type" runat="server" Width="156px" AutoPostBack="true" OnSelectedIndexChanged="DropList_AnalysisMainItem_SelectedIndexChanged">
</asp:DropDownList>
                        </td>
                        <td style="width: 48px" class="float_Middle">
                            </td>
                       
                         <td style="width:120px;text-align: right" class="titleList" >
                           
                                <asp:Label ID="Label15" runat="server" Text="报告状态"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           <asp:DropDownList id="drop_status" runat="server" Width="156px"></asp:DropDownList>
                        </td>
                        <td style="width: 48px" class="float_Middle">
                            </td>
                       
                                
                       </tr>
                    <tr>
                        <td style="width:120px;text-align: right" class="titleList" >
                           
                                <asp:Label ID="Label18" runat="server" Text="区域"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           <asp:DropDownList id="drop_client" runat="server" Width="156px"/>
                        </td>
                        <td class="Middle">
                        </td>
                       <td style="width:120px;text-align: right" class="titleList" >
                           
                                <asp:Label ID="Label19" runat="server" Text="样品状态"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                        <asp:DropDownList id="drop_analysisstatus" runat="server" Width="156px">
                               <asp:ListItem Value="-1">请选择</asp:ListItem>
                               <asp:ListItem Value="0">在库中</asp:ListItem>
                               <asp:ListItem Value="1">领用中</asp:ListItem>
                            </asp:DropDownList>
                           
                        </td>
                        <td></td><td  class="Middle">
                        </td>
                        
                  </tr>
                  <tr>
                        <td style="width:120px;text-align: right" class="titleList" >
                           
                                <asp:Label ID="Label20" runat="server" Text="紧急程度"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                            <%--<asp:TextBox ID="txt_item" runat="server" Width="156px"></asp:TextBox>--%>
                            
                             <asp:DropDownList id="Drop_Urgent" runat="server" Width="156px">
                               <asp:ListItem Value="-1">请选择</asp:ListItem>
                               <asp:ListItem Value="0">一般</asp:ListItem>
                               <asp:ListItem Value="1">紧急</asp:ListItem>
                            </asp:DropDownList>
                      </td>
                        <td class="Middle">
                        </td>
                       <td style="width:120px;text-align: right" class="titleList" >
                           
                                
                            
                        </td>
                        <td class="ctrlList">
                           
                        </td>
                        <td></td><td  class="Middle">
                        </td>
                        
                    </tr>
                    <tr>
                    <td style=" width:115px;text-align: right" class="titleList" >
                                <asp:Label ID="Label1" runat="server" Text="开始时间"></asp:Label>
                            
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txt_QueryTime" runat="server" 
                                 Width="156px"></asp:TextBox>
                        </td>
                       <td class="Middle">
                        </td>
                       
                        <td style="width:115px;text-align: right" class="titleList" >
                                <asp:Label ID="Label17" runat="server" Text="结束时间"></asp:Label>
                            
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txt_QueryEndTime" runat="server" 
                                Height="21px" Width="156px"></asp:TextBox>
                        </td>
                        <td style="height: 4px" class="Middle">
                        </td>
                        <td class="ctrlList"></td>
                        
                    </tr>
                </tbody>
            </table> 
           
                 <asp:Panel ID="Panel8" BackColor="#F5F9FF" runat="server" Width="800px" Height="50px"
                                    Font-Size="X-Small" ForeColor= "#2292DD" GroupingText="按分析项目">
                                    <table class="container">
                                        <tbody>
                                            <tr>
                                                <td style="width:115px;text-align: right" class="titleList" >
                                                    
                                                        <asp:Label ID="lbl_AnalysisMainItem" runat="server" Text="分析大类"></asp:Label>
                                                </td>
                                                <td class="titleList" >
                                                    <asp:DropDownList ID="DropList_AnalysisMainItem" CssClass=" ctrlList" runat="server" AutoPostBack="true"
                                                         Width="156px"
                                                        OnSelectedIndexChanged="DropList_AnalysisMainItem_SelectedIndexChanged" 
                                                        >
                                                        <asp:ListItem Value="1">常规</asp:ListItem>
                               <asp:ListItem Value="0">其他</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                               <td style="height: 4px" class="Middle">
                        </td>
                        <td colspan="2" align="right">
                          <%-- 分析项目间的关系选择--%>
                        </td>
                        <td colspan="2">
                          <%--  <asp:RadioButtonList ID="RadioButtonList1"  RepeatDirection="Horizontal"  
                                Font-Size="X-Small" runat="server" 
                               >
                                <asp:ListItem Value="1">同时满足</asp:ListItem>
                                <asp:ListItem Value="2">分别满足</asp:ListItem>
                            </asp:RadioButtonList>--%>
                        </td>
                        
                       
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="border-right: 2px groove; border-top: 2px groove; border-left: 2px groove;
                                        width: 100%; border-bottom: 2px groove">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <%--<asp:RadioButtonList ID="cb_analysisList" runat="server">
                                                    </asp:RadioButtonList>--%>
                                                    <asp:CheckBoxList ID="cb_analysisList" runat="server" RepeatDirection="Horizontal"  Font-Size="X-Small" RepeatColumns="6"
                                                        Width="100%">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                   <%-- <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label13" runat="server" Text="自定义分析项目" CssClass="mLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_own" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>printdiv('PrintContent')--%></asp:Panel>      
            </asp:Panel>
           
           </asp:Panel> <div align="right"><input type="button" onclick="dayin()" value="打印结果" class="btn" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
          <center><div id="dayin" style=" text-align:center">
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Width="100%" Caption=""  OnRowCreated="grdvw_List_RowCreated"
                >
                <Columns>
                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            </center> 
             </td></tr></tbody></table>
            <%--<table class="container">
                <tbody>
                    <tr> AllowPaging="True" PageSize="20"
                OnPageIndexChanging="grdvw_List_PageIndexChanging"
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
           
        </Triggers>
    </asp:UpdatePanel>
   
</asp:Content>
