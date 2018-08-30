<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFile.aspx.cs" Inherits="DsoFramer_EditFile" %>
<html>
	<head>
		<title>文档在线浏览</title>
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">  
  
       <%-- <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">--%>
		
    <SCRIPT language="javascript" event="NotifyCtrlReady" for="FramerControl1">		
        function OpenHelpDoc()
        {
       var btn=document.getElementById('savebtn');
        
        var flag=<%=Request.Cookies["Cookies"].Values["u_purview"].ToString().Substring(3, 1) %>;
        if(flag=="0")
        btn.style.display="none";
                  // document.all.FramerControl1.Open("http://192.168.1.3:8087/file/文件管理/test/3b12cd84-e37e-4390-8c34-0784748430b6.XLS", true);

            document.all.FramerControl1.Open("<%=url %>", true);
        }
        OpenHelpDoc();        
    </SCRIPT>
		<script language="javascript">
		var documentopenflag=0;
        function NewDoc(filetype){
        if (filetype=='xls')
        document.all.FramerControl1.CreateNew("Excel.Sheet");
        if (filetype=='doc')
        document.all.FramerControl1.CreateNew("Word.Document");
        if (filetype=='ppt')
        document.all.FramerControl1.CreateNew("PowerPoint.Show");
        }
        function OpenDoc(){        
                document.all.FramerControl1.showdialog(1);
        }
        function OpenWebDoc(filetype){
            if (filetype=='doc')
            document.all.FramerControl1.Open("../UploadFile/633520231204062500.doc", true);//doc模板
            if (filetype=='xls')
            document.all.FramerControl1.Open("../UploadFile/633520231204062500.doc", true);//xls模板
        }
        function SaveToLocal(){
        alert('将保存在您电脑的c:\\mydoc.doc')
            document.all.FramerControl1.Save("c:\\mydoc.doc",true);
        }
        function SaveToWeb(){
            document.all.FramerControl1.HttpInit();
            
            document.all.FramerControl1.HttpAddPostString("RecordID","200601022");
            document.all.FramerControl1.HttpAddPostString("UserID","李局长");
            document.all.FramerControl1.HttpAddPostCurrFile("FileData", "<%=filaname %>");
            alert(window.location.host);
            document.all.FramerControl1.HttpPost("http://" + window.location.host + "/DsoFramer/SaveDoc.aspx?FilePath=<%=Serverurl %>"); 
            alert("对文件的修改已经保存成功！");
            window.close();          
        }
       
        function print(){
            document.all.FramerControl1.PrintOut();           
         }
         function printview(){        
            document.all.FramerControl1.PrintPreview();           
         }
         function printviewexit(){        
            document.all.FramerControl1.PrintPreviewExit();           
         }
        function fileclose(){
          document.all.FramerControl1.Close();
        }        
        
		</script>
</head>
<body>
    <form id="form1" runat="server" method="post" encType="multipart/form-data" >
    <div>    
     <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">            
            <tr>
                <td valign="middle" style="border-bottom: #006633 1px dashed; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                    <asp:Label ID="lbl_type" runat="server" Text="文件浏览"></asp:Label>  </td>
                <td align="right" valign="middle" style="border-bottom: #006633 1px dashed; height: 30px;">
                
                
            
                    <input id="savebtn" onclick="SaveToWeb()" style="width: 80px" type="button"
                                value="保存文件" />
                           </td>
            </tr>
            <tr>
            <td height="3px" colspan="2" style="background-color: #ffffff"></td>
        </tr>
        </table>
    <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1" height="100%">            
        <tr>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <object id="FramerControl1" classid="clsid:00460182-9E5E-11D5-B7C8-B8269041DD57"
                    codebase="dsoframer.ocx" height="95%" width="100%">
                    <param name="_ExtentX" value="16960">
                    <param name="_ExtentY" value="13600">
                    <param name="BorderColor" value="-2147483632">
                    <param name="BackColor" value="-2147483643">
                    <param name="ForeColor" value="-2147483640">
                    <param name="TitlebarColor" value="-2147483635">
                    <param name="TitlebarTextColor" value="-2147483634">
                    <param name="BorderStyle" value="1">
                    <param name="Titlebar" value="0">
                    <param name="Toolbars" value="1">
                    <param name="Menubar" value="1">
                </object>
                </td>
        </tr>
        </table></div>
    </form>
</body>
</html>