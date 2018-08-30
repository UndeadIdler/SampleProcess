<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>页面头</title>
    <link href="App_Themes/Default/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function exit()
    {
      if (window.confirm('您确认要退出系统吗？'))
      { 
       window.parent.opener=null;window.parent.close();
       }
    }
  
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-image: url(images/top_leftbg2.jpg)">
        <tr>
            <td colspan="5" background="images/top_bg12.jpg">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" valign="top" style="width: 40%">
                            <img src="images/logotop2.gif" alt="" height="48" style="width: 546px" />
                        </td>
                        <td width="60%" align="right" colspan="2">
                            <table width="400px" border="0" align="right" cellpadding="0" cellspacing="0" style="height: 24px">
                                <tr>
                                    <td height="22px" align="right" valign="top" style="width: 361px;">
                                    </td>
                                    <td height="22px" align="center" width="75" valign="middle">
                                        <asp:ImageButton ID="ImageButton3" ImageUrl="images/a_22.gif" runat="server" OnClientClick="window.parent.location.href='系统使用手册.doc';" />
                                    </td>
                                    <td height="22px" align="right" valign="middle" class="tixing" width="75">
                                        <asp:ImageButton ID="ImageButton2" ImageUrl="images/bar_01.gif" runat="server" OnClientClick="if (!window.confirm('您确认要注消当前登录用户吗？')){return false;}else{window.parent.location.href='login.aspx';}" />
                                    </td>
                                    <td width="22px" height="22" alt="" class="tixing" valign="middle">
                                        <asp:ImageButton ID="ImageButton1" ImageUrl="images/a_24.gif" runat="server" OnClientClick="exit()"  OnClick="ImageButton1_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="700" class="top3_bgimage">
                <table width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="150px">
                           <font face="宋体">
                                            <asp:Label ID="lbl_nowuser" runat="server" Text=""></asp:Label></font>
                        </td>
                        <td>
                            <table height="31" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="2" class="top4_bgimage" width="39" height="31">
                                    </td>
                                    <td height="8">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="Reports/SampleDayAccess.aspx" target="mainFrame">样品报告</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                       <%-- <td>
                            <table height="31" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="2" class="top4_bgimage" width="39" height="31">
                                    </td>
                                    <td height="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="Sample/SampleItemQuery.aspx" target="mainFrame">样品报告</a>
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                        <td>
                        </td>
                        <td>
                            <table height="31" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="2" class="top4_bgimage" width="39" height="31">
                                    </td>
                                    <td height="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="ExamineReport/ExamineReportQuery.aspx" target="mainFrame">委托监测</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table height="31" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="2" class="top4_bgimage" width="39" height="31">
                                    </td>
                                    <td height="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="login.aspx" target="_top" onclick="if (!window.confirm('您确认要注消当前登录用户吗？')){return false;}">
                                            注销</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table height="31" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="39" rowspan="2" class="top4_bgimage" height="31">
                                    </td>
                                    <td height="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="images/bar_07.gif" width="16" height="16" onclick="if(parent.topset.rows!='22,*'){parent.topset.rows='22,*';window.scroll(0,93)}else{parent.topset.rows='93,*'};"
                                            style="cursor: hand" title="点击这里可以收缩顶部">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="75" class="top5_bgimage" height="31">
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
