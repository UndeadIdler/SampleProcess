<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="SampleQuery1.aspx.cs" Inherits="Sample_SampleQuery1" Title="样品查询" %>

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
                           
                                <asp:Label ID="Label16" runat="server" Text="样品类型"></asp:Label>
                        </td>
                        <td class="ctrlList">
                          
                            <asp:DropDownList id="txt_type" runat="server" Width="156px" AutoPostBack="true" OnSelectedIndexChanged="DropList_AnalysisMainItem_SelectedIndexChanged">
</asp:DropDownList>
                        </td>
                        <td>
</td><td  class="Middle">
                        </td>
                        
                    </tr> 
                    <tr>
                        <td style="width:120px;text-align: right; " class="titleList" >
                           
                                <asp:Label ID="Label14" runat="server" Text="项目类型"></asp:Label>
                            
                        </td>
                        <td class="ctrlList">
                           
                            <asp:DropDownList id="txt_item" runat="server" Width="156px"></asp:DropDownList>
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
                        <td class="ctrlList"><asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_Click"  
                                CssClass="mButton" Height="24px" Width="100px" /></td>
                                
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
                    </tbody></table>
                    <asp:Panel ID="Panel7" BackColor="#F5F9FF" runat="server" Width="800px" Height="50px"
                                    Font-Size="X-Small" GroupingText="按接样时间" ForeColor= "#2292DD">
                    <table class=" container"><tbody>
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
            </asp:Panel>
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
                         
                        </td>
                        <td colspan="2">
                         
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
                                   </asp:Panel>      
           </asp:Panel> <div align="right"><input type="button" onclick="dayin()" value="打印结果" class="btn" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
          <center><div id="dayin" style=" text-align:center">
            <asp:GridView ID="grdvw_List" runat="server" CssClass="mGridView" Caption=""  OnRowCreated="grdvw_List_RowCreated"
                OnSelectedIndexChanging="grdvw_List_RowSelecting">
                <Columns>
                    <asp:TemplateField HeaderText="序号"></asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            </center> 
             </td></tr></tbody></table>           
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_ExitAnalysisItem" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <div id="detail" class="mLayer" style=" display:none;left: 96px; width: 739px; top: 500px; height: 130px">
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
            <asp:Panel ID="Panel2" runat="server"  BackColor="#F5F9FF" Width="100%"  Font-Size="X-Small"
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
                                <asp:TextBox ID="txt_ItemList" runat="server" CssClass=" mTextBox"></asp:TextBox>
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
                                <asp:TextBox ID="txt_SampleType" runat="server" CssClass=" mTextBox"></asp:TextBox>
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
                    GroupingText="报告校核">
                     <table style="margin: 0px; width: 800px" class="container">
                    <tbody>
                        
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                                <asp:Label ID="Label3" runat="server" CssClass="mLabel" Text="收到时间：" 
                                    Width="75px"></asp:Label>
                                </span>
                            </td>
                             <td class="ctrlList">
                                <asp:TextBox ID="txt_checktime" runat="server"></asp:TextBox>
                            </td>
                            <td class="float_Middle" style="width: 48px">
                            </td>
                             <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                               
                                <asp:Label ID="Label4" runat="server" CssClass="mLabel" Text="校核人："></asp:Label>
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
                            </td></tr>
                        <tr>
                           <td style="width: 5px; " class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label2" runat="server" CssClass="mLabel"  Text="备注："></asp:Label>
                                </td>
                                
                            <td colspan="7">
                                <asp:TextBox ID="txt_remark" runat="server" Height="72px" Style="width: 98%" 
                                    TextMode="MultiLine" Width="203px"></asp:TextBox>
                            </td>
                                
                            <td style="width: 43px" class="float_Middle">
                            </td>
                        </tr>
                        
                         </tbody>
                </table>
                   
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="报告审核">
                     <table style="margin: 0px; width: 800px" class="container">
                    <tbody>
                        
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                                <asp:Label ID="Label5" runat="server" CssClass="mLabel" Text="收到时间：" 
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
                               
                                <asp:Label ID="Label6" runat="server" CssClass="mLabel" Text="审核人："></asp:Label>
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
                   
                </asp:Panel>
                 <asp:Panel ID="Panel4" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="报告签发">
                     <table style="margin: 0px; width: 800px" class="container">
                    <tbody>
                        
                        <tr>
                            <td style="width: 48px" class="float_Middle">
                            </td>
                            <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                                <asp:Label ID="Label8" runat="server" CssClass="mLabel" Text="收到时间：" 
                                    Width="75px"></asp:Label>
                                </span>
                            </td>
                             <td class="ctrlList">
                                <asp:TextBox ID="txt_signtime" runat="server"></asp:TextBox>
                            </td>
                            <td class="float_Middle" style="width: 48px">
                            </td>
                             <td style="text-align: right" class="titleList">
                                <span style="font-size: 10pt">
                               
                                <asp:Label ID="Label9" runat="server" CssClass="mLabel" Text="签发人："></asp:Label>
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
                           <td style="width: 5px; " class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label10" runat="server" CssClass="mLabel"  Text="备注："></asp:Label>
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
                         </asp:Panel><asp:Panel ID="Panel5" runat="server" Width="100%" Height="50px" Font-Size="X-Small"
                    GroupingText="报告装订">
                     <table style="margin: 0px; width: 800px" class="container">
                    <tboday>
                        <tr>
                           <td style="width: 5px; " class="float_Middle">
                                </td>
                                <td style="text-align: right" class="titleList">
                                    <asp:Label ID="Label11" runat="server" CssClass="mLabel"  Text="收到时间："></asp:Label>
                                </td>
                                
                            <td>
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
                                
                            <td>
                                 <asp:TextBox ID="txt_endtime" runat="server"></asp:TextBox>
                            </td>
                                
                            <td style="width: 43px" class="float_Middle">
                            </td>
                        </tr>
                         <tr>                            
                            <td colspan="3" align="center">
                                <asp:Button ID="btn_ExitAnalysisItem" runat="server" CssClass="mButton" 
                                    OnClick="btn_ExitAnalysisItem_Click" Text="关闭" />
                            </td>
                        </tr>
                         </tbody>
                </table>
                   
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
