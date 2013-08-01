<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="WebXabara_Main" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>服务器信息</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin:10px 10px 10px 5px;
    }
    -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="divLeft" style="width:70%;">
            <ul>
                <li class="titleBlue"><img src="../Images/serverSet.jpg" style="vertical-align:middle;" /> 常用操作</li>
                <li style="height:1px; background-color:#0598ca; font-size:0px;"></li>
                <li class="height5px"></li>
                <li class="title999">全局</li>
                <li class="height5px"></li>
                <li>
                    <table cellspacing="2" cellpadding="2" border="0">
                        <tr style="text-align:center;">
                            <td><a href="systemSet.aspx" target="mainFrame"><img src="../images/icon001.jpg" title="基本设置" /></a></td>
                            <td style="width:20px;"></td>
                            <td><a href="otherSet.aspx" target="mainFrame"><img src="../images/icon002.jpg" title="系统设置" /></a></td>
                            <td style="width:20px;"></td>
                            <td><a href="sitemap.aspx?id=baidu" target="mainFrame"><img src="../images/icon004.jpg" title="百度地图" /></a></td>
                            <td style="width:20px;"></td>
                            <td><a href="sitemap.aspx?id=google" target="mainFrame"><img src="../images/icon005.jpg" title="谷歌地图" /></a></td>
                            <td style="width:20px;"></td>
                            <td><a href="../ads/add.aspx" target="mainFrame"><img src="../images/icon007.jpg" title="广告系统" /></a></td>
                            <td style="width:20px;"></td>
                            <td><a href="../class/updateCache.aspx" target="mainFrame"><img src="../images/icon009.jpg" title="全站缓存" /></a></td>
                            <td style="width:20px;"></td>
                            <td><a href="htm.aspx" target="mainFrame"><img src="../images/icon010.jpg" title="首页缓存" /></a></td>
                        </tr>
                        <tr style="text-align:center;">
                            <td><a href="systemSet.aspx" target="mainFrame">基本设置</a></td>
                            <td style="width:20px;"></td>
                            <td><a href="otherSet.aspx" target="mainFrame">系统设置</a></td>
                            <td style="width:20px;"></td>
                            <td><a href="sitemap.aspx?id=baidu" target="mainFrame">百度地图</a></td>
                            <td style="width:20px;"></td>
                            <td><a href="sitemap.aspx?id=google" target="mainFrame">谷歌地图</a></td>
                            <td style="width:20px;"></td>
                            <td><a href="../ads/add.aspx" target="mainFrame">广告系统</a></td>
                            <td style="width:20px;"></td>
                            <td><a href="../class/updateCache.aspx" target="mainFrame">全站缓存</a></td>
                            <td style="width:20px;"></td>
                            <td><a href="htm.aspx" target="mainFrame">首页缓存</a></td>
                        </tr>
                    </table>
                </li>
                <li class="height5px"></li>
                <li class="title999">用户</li>
                <li>
                    <table cellspacing="2" cellpadding="2" border="0">
                        <tr style="text-align:center;">
                            <td><a href="../adminUser/exe.aspx?id=a" target="mainFrame"><img src="../images/icon104.jpg" title="后台日志" /></a></td>
                        </tr>
                        <tr style="text-align:center;">
                            <td><a href="../adminUser/exe.aspx?id=a" target="mainFrame">后台日志</a></td>
                        </tr>
                    </table>
                </li>                
                <li style="height:20px;"></li>
                <li>
                    <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                        <tr>
                            <td valign="top" style="width:59%;">
                                <ul>
                                    <li class="titleBlue"><img src="../Images/program.jpg" style="vertical-align:middle;" /> 系统信息</li>
                                    <li style="height:1px; background-color:#0598ca; font-size:0px;"></li>
                                    <li class="height5px"></li>
                                    <li style="line-height:200%; padding:2px 0px 2px 5px;">操作系统：<asp:Label ID="ServerOSVer" runat="server"></asp:Label>&nbsp;(<asp:Label ID="ServerOS" runat="server"></asp:Label>)</li>
                                    <li style="line-height:200%; background-color:#e9f8fc; padding:2px 0px 2px 5px;">服务器IP：<asp:Label ID="serverIP" runat="server"></asp:Label></li>
                                    <li style="line-height:200%; padding:2px 0px 2px 5px;">虚拟目录绝对路径：<asp:Label ID="ServerPath" runat="server"></asp:Label></li>
                                    <li style="line-height:200%; background-color:#e9f8fc; padding:2px 0px 2px 5px;">服务器现在时间：<asp:Label ID="serverTime" runat="server"></asp:Label></li>
                                    <li style="line-height:200%; padding:2px 0px 2px 5px;">IIS版本：<asp:Label ID="ServerSoft" runat="server"></asp:Label></li>
                                    <li style="line-height:200%; background-color:#e9f8fc; padding:2px 0px 2px 5px;">.Net版本：<asp:Label ID="netVer" runat="server"></asp:Label></li>
                                    <li style="line-height:200%; padding:2px 0px 2px 5px;">数据库版本：<asp:Label ID="sqlVer" runat="server"></asp:Label></li>
                                </ul>
                            </td>
                            <td style="width:2%;"></td>
                            <td valign="top" style="width:39%;">
                                <ul>
                                    <li class="titleBlue"><img src="../Images/tj.jpg" style="vertical-align:middle;" /> 信息统计</li>
                                    <li style="height:1px; background-color:#0598ca; font-size:0px;"></li>
                                    <li class="height5px"></li>
                                    <asp:Literal ID="infoLJ" runat="server"></asp:Literal>
                                </ul>
                            </td>
                        </tr>                        
                    </table>
                </li>                
            </ul>
        </div>
        <div class="divRight" style="width:28%;">
            <ul>
                <li class="titleBlue"><img src="../Images/ver.jpg" style="vertical-align:middle;" /> 版本检查</li>
                <li style="height:1px; background-color:#0598ca; font-size:0px;"></li>
                <li class="height5px"></li>
                <li class="lineYellow">
                    <asp:Literal ID="ver" runat="server"></asp:Literal>
                </li>
                <li class="height5px"></li>
                <li><a href="http://www.zdianpu.com" target="_blank">访问官方网站获取最新版本</a></li>
                <li class="height5px"></li>
                <li><asp:HyperLink ID="buyLink" runat="server" Target="_blank"><img src="../Images/buySoft.jpg" /></asp:HyperLink></li>
                <li class="height5px"></li>
                <li class="titleBlue"><img src="../Images/adminLogin.jpg" style="vertical-align:middle;" /> 后台登录日志</li>
                <li style="height:1px; background-color:#0598ca; font-size:0px;"></li>
                <li class="height5px"></li>
                <asp:Repeater ID="adminLog" runat="server">
                    <ItemTemplate>               
                    <li style="font-weight:bold; padding:2px 0px 2px 5px;"><%#Eval("userID").ToString().Trim()%></li>
                    <li style="padding:2px 0px 2px 5px;">登录时间：<%# Convert.ToDateTime(Eval("loginDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss")%></li>
                    <li style="padding:2px 0px 2px 5px;">登录地点：<%# ip(Eval("loginIP").ToString().Trim())%></li>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                    <li style="font-weight:bold; background-color:#e9f8fc; padding:2px 0px 2px 5px;"><%#Eval("userID").ToString().Trim()%></li>
                    <li style="background-color:#e9f8fc; padding:2px 0px 2px 5px;">登录时间：<%# Convert.ToDateTime(Eval("loginDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss")%></li>
                    <li style="background-color:#e9f8fc; padding:2px 0px 2px 5px;">登录地点：<%# ip(Eval("loginIP").ToString().Trim())%></li>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>