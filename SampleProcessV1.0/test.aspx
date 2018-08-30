<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<%@ Register TagPrefix="uc" TagName="Spinner" Src="Controls\Spinner.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<body>
    <form id="Form1" runat="server">
    <table width="100%" cellpadding="1" onmouseover="kpr.style.display='';">
        <tr>
            <td width="100%" height="25" colspan="3">

                <script language="javascript"> 

function printsetup(){ 
// 打印页面设置 
wb.execwb(8,1); 
} 
function printpreview(){ 
// 打印页面预览 

wb.execwb(7,1); 
} 

function printit() 
{ 
if (confirm('确定打印吗？')){ 

wb.execwb(6,6) 
} 
} 
                </script>

                <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="wb" name="wb"
                    width="3">
                </object>
                <div id="kpr">
                    <input class="ipt" type="button" name="button" _print value="打印" onclick="kpr.style.display='none';javascript :printit();">
                    <input class="ipt" type="button" name="button" _setup value="打印页面设置" onclick=" javascript : printsetup();">
                    <input class="ipt" type="button" name="button_show" value="打印预览" onclick="kpr.style.display='none';javascript:printpreview();">
                    <input class="ipt" type="button" name="button" _fh value="关闭" onclick=" javascript:window.close();">
            </td>
            </div>
        </tr>
    </table>
    <%--参数方法:

WB.ExecWB(4,1)   
    
   4,1 保存网页   
   4,2 保存网页(可以重新命名)   
   6,1 直接打印   
   6,2 直接打印   
   7,1 打印预览   
   7,2 打印预览   
   8,1 选择参数   
   8,2 选择参数   
   10,1 查看页面属性   
   10,2 查看页面属性   
   17,1 全选   
   17,2 全选   
   22,1 重新载入当前页   
   22,2 重新载入当前页--%>
    <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WebBrowser"
        width="0">
    </object>
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(1,1)" type="button"
        value="打开">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(2,1)" type="button"
        value="关闭所有">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(4,1)" type="button"
        value="另存为">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(6,1)" type="button"
        value="打印">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(6,6)" type="button"
        value="直接打印">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(8,1)" type="button"
        value="页面设置">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(10,1)" type="button"
        value="属性">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(17,1)" type="button"
        value="全选">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(22,1)" type="button"
        value="刷新">
    <input name="Button" onclick="document.all.WebBrowser.ExecWB(45,1)" type="button"
        value="关闭">
    </form>
</body>
</html>
