<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script>
window.onload=function()
{
window.moveTo(0,0) ;
window.resizeTo(screen.availWidth,screen.availHeight); //窗口最大化
//window.resizeTo(table1.clientWidth,table1.clientHeight);//窗口和表格的宽高同
}
</script>
<title>海盐县环保监测站样品报告管理平台</title>
<style type="text/css">
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	overflow:hidden;
}
.STYLE3 {font-size: 14px; color: #adc9d9; }

</style>
</head>
<body>
<form runat="server">
<table width="100%"  height="100%" border="0" cellspacing="0" cellpadding="0">
  <!--<tr>
    <td bgcolor="#1075b1">&nbsp;</td>
  </tr>-->
  <tr>
    <td height="608" background="images/login_03.gif"><table width="847" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="318" background="images/login_04.gif">&nbsp;</td>
      </tr>
      <tr>
        <td height="84"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="381" height="84" background="images/login_06.gif">&nbsp;</td>
            <td width="162" valign="middle" background="images/login_07.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="44" height="24" valign="bottom"><div align="right"><span class="STYLE3">用户</span></div></td>
                <td width="10" valign="bottom">&nbsp;</td>
                <td height="24" colspan="2" valign="bottom">
                  <div align="left">
                    <%--<input type="text" name="textfield" id="textfield" style="width:100px; height:17px; background-color:#87adbf; border:solid 1px #153966; font-size:12px; color:#283439; ">--%>
                   <asp:TextBox ID="txt_UserName" runat="server" Width="100px" Height="20px" BackColor="#87adbf"  BorderColor="#153966" BorderStyle="Solid" BorderWidth="1" Font-Size="12pt" ForeColor="#3e0000"></asp:TextBox>
                  </div></td>
              </tr>
              <tr>
                <td height="24" valign="bottom"><div align="right"><span class="STYLE3">密码</span></div></td>
                <td width="10" valign="bottom">&nbsp;</td>
                <td height="24" colspan="2" valign="bottom">
                <%--<input type="password" name="textfield2" id="textfield2" style="width:100px; height:17px; background-color:#87adbf; border:solid 1px #153966; font-size:12px; color:#283439; ">--%>
              <asp:TextBox ID="txt_Pwd" runat="server" Width="100px" Height="20px" BackColor="#87adbf"  BorderColor="#153966" BorderStyle="Solid" BorderWidth="1"  Font-Size="12pt" ForeColor="#3e0000" TextMode="Password"></asp:TextBox></td>
              </tr>            
              <tr></tr>
            </table></td>
            <td width="26"><img src="images/login_08.gif" width="26" height="84"></td>
            <td width="67" background="images/login_09.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="25"><div align="center"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/dl.gif" OnClick="btn_Login_Click" BorderStyle="None" Height="20" Width="57" /></div></td>
              </tr>
              <tr>
                <td height="25"><div align="center"><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cz.gif" OnClick="btn_Clean_Click" BorderStyle="None" Height="20" Width="57"/> </div></td>
              </tr>
            </table></td>
            <td width="211" background="images/login_10.gif">&nbsp;</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="206" background="images/login_11.gif">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td bgcolor="#152753">&nbsp;</td>
  </tr>
</table>
</form>
</body>
</html>