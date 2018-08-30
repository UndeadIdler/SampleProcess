<%@ Page Language="C#" MasterPageFile="~/QueryMasterPage.master" AutoEventWireup="true"
    CodeFile="OutCarChart.aspx.cs" Inherits="carinfo_OutCarChart" Title="出车图表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <table>
        <tr>
            <td style="width:85px">
                <asp:Label ID="Label1" runat="server" Text="出车日期" CssClass="mLabel" Width="85px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_s" runat="server"></asp:TextBox>-
                <asp:TextBox ID="txt_e" runat="server"></asp:TextBox>
            </td>
            <td style="width:85px">
                <asp:Label ID="Label2" runat="server" Text="车牌号" CssClass="mLabel" Width="85px"></asp:Label>
            </td>
            <td style="width:85px">
                <asp:DropDownList ID="drop_car" runat="server" CssClass="mDropDownList"  Width="150px"
                    AutoPostBack="True" onselectedindexchanged="drop_car_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td><asp:Button ID="Button1" runat="server"  CssClass="btn" Text="查询" 
                    onclick="Button1_Click" /></td>
        </tr>
        
        <tr>
            <td width="600px" colspan="5">
                <asp:Chart ID="Chart1" runat="server" Height="650px" Width="850px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                    ImageType="Png" Palette="BrightPastel" BackColor="#D3DFF0" BorderDashStyle="Solid"
                    BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105">
                    <Titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                            Text="出车信息" ForeColor="26, 59, 105">
                        </asp:Title>
                    </Titles>
                    <Legends>
                      <asp:Legend LegendStyle="Row" IsTextAutoFit="False" Docking="Bottom" Name="Default"
                            BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <Series>
                        <%--<asp:series ChartArea="ChartArea1" Name="废水排放量(m³)" BorderColor="180, 26, 59, 105">									
								</asp:series>		--%>
                        <%--<asp:series  ChartArea="ChartArea2" Name="COD排放量(kg)" BorderColor="180, 26, 59, 105">									
								</asp:series>
								<asp:series  ChartArea="ChartArea2" Name="氨氮排放量(kg)" BorderColor="180, 26, 59, 105">									
								</asp:series>
								<asp:series  ChartArea="ChartArea3" Name="COD均值(g/l)" BorderColor="180, 26, 59, 105">									
								</asp:series>
								<asp:series  ChartArea="ChartArea3" Name="氨氮均值(g/l)" BorderColor="180, 26, 59, 105">									
								</asp:series>
							--%>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                            <AxisY2 Enabled="False">
                                <MajorGrid Enabled="False" />
                            </AxisY2>
                            <AxisX2 Enabled="False">
                                <MajorGrid Enabled="False" />
                            </AxisX2>
                            <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                WallWidth="0" IsClustered="False"></Area3DStyle>
                            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisY>
                            <AxisX TitleFont="Microsoft Sans Serif, 10pt, style=Bold" LineColor="64, 64, 64, 64"
                                IsLabelAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsStaggered="True" Format="#" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>
                            <AxisY Title="车辆" Enabled="False">
                            </AxisY>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
    </table>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
