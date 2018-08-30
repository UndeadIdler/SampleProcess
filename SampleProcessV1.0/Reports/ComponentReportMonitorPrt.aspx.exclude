<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComponentReportMonitorPrt.aspx.cs"
    Inherits="ComponentReportMonitorPrt" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>检测报告数据组成表打印</title>
    <link href="../App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />
    <meta name="Generator" content="EditPlus" />
    <meta name="Author" content="" />
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />

    <script type="text/javascript">
                
    var    hkey_root,hkey_path,hkey_key   
        hkey_root="HKEY_CURRENT_USER"   
        hkey_path="\\Software\\Microsoft\\Internet   Explorer\\PageSetup\\"   
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

    //用于设置打印参数
    function printBase() {
    factory.printing.header  = ""   //页眉
    factory.printing.footer = ""   //页脚
    factory.printing.portrait = true   //true为纵向打印，false为横向打印
    factory.printing.leftMargin   =   0.5  
    factory.printing.topMargin   =   1.5   
    factory.printing.rightMargin   =   0.5  
    factory.printing.bottomMargin   =   1.5   
    }

    //用于调用设置打印参数的方法和显示预览界面
    function printReport(){
            //printBase();
            //pagesetup_null();
            document.all("button").style.display = "none";//隐藏按钮
            factory.printing.Preview();
            document.all("button").style.display = "block";//显示按钮
    }

    //使界面最大化
    maxWin();
    function maxWin()
    {
          var aw = screen.availWidth;
          var ah = screen.availHeight;
          window.moveTo(0, 0);
          window.resizeTo(aw, ah);
    }

    function printTure()
    {
         printBase();
         document.all("button").style.display = "none";//隐藏按钮
          //factory.printing.Preview();
         factory.printing.Print(false);
         alert('打印成功！');
         window.close();
         //document.all("button").style.display = "block";//显示按钮
    }
    </script>

</head>
<body>
    <object id="factory" codebase="smsx.cab#Version=6,5,439,50" height="0" width="0"
        classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814">
    </object>
    <br/>
    &nbsp;
    <center>
        <asp:Label ID="Label_H" runat="server" Text="Label"></asp:Label>
        <br/>
        &nbsp;
        <%=strTable %>
        <br/>
        &nbsp;
    </center>
    <div id="button" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px;
        margin: 0px; width: 98%; padding-top: 0px">
        <table cellspacing="1" cellpadding="4" width="100%" border="0">
            <tr>
                <td align="center">
                    <input onclick="printTure()" class="btn" type="button" value="打印" />
                    <input id="idPrint2" class="btn" type="button" value="页面设置" onclick="factory.printing.PageSetup()" />
                    <input id="idPrint3" class="btn" type="button" value="打印预览" onclick="printReport()" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
