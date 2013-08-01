<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WebXabara_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="Shortcut Icon" href="http://www.zdianpu.com/Images/favicon.ico" />
    <title>管理系统</title>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin: 0px;
	    background:url('Images/LoginCenterBg.gif'); background-repeat:repeat;
    }
    -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cp" style="background-image: url('Images/LoginLineBg.gif'); background-repeat: repeat-x;
        text-align: right; height: 32px; line-height: 32px; top: 0px; left:0px; position: absolute;
        width: 100%;">
        Licensed&nbsp;to：<asp:Literal ID="ClientName" runat="server"></asp:Literal>&nbsp;&nbsp;
    </div>
    <div id="loginDiv" style="position: absolute; z-index: 100px;">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 2px; background-image: url('Images/LoginCenterLine.gif');">
                </td>
                <td style="width: 250px; text-align:right;">
                    <table width="95%" border="0" cellspacing="1" cellpadding="0" align="right">
                        <tr>
                            <td style="text-align: left;">
                                <strong>用户名</strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 19px; text-align: left;">
                                <asp:TextBox CssClass="LoginForm" ID="UID" MaxLength="20" runat="server" TextMode="SingleLine"
                                    ToolTip="请输入用户名" Wrap="False" Width="177px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="6" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <strong>密 码</strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left;">
                                <asp:TextBox CssClass="LoginForm" ID="PW" MaxLength="20" runat="server" TextMode="Password"
                                    ToolTip="请输入密码" Width="177px" Wrap="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="6" colspan="2">
                            </td>
                        </tr>

                        <tr>
                            <td width="50" style="text-align: left; vertical-align: bottom;">
                                <asp:ImageButton ID="imgLogin" AlternateText="登录" ImageUrl="Images/Login.gif" ToolTip="点击登录"
                                    runat="server" BorderWidth="0px"  OnClick="imgLogin_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 20px;">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table border="0" cellpadding="1" cellspacing="1" align="center">
                        <tr>
                            <td height="20" align="left" rowspan="2">
                                建议浏览器及分辨率：IE8.0&nbsp;1280x800及以上&nbsp;&nbsp;[&nbsp;<asp:Label ID="clintIE" runat="server"></asp:Label><asp:Label ID="clintIEver" runat="server"></asp:Label>&nbsp;<script type="text/javascript">                                                                                         document.write(window.screen.width + "x" + window.screen.height);</script>&nbsp;]
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td height="35" align="left">
                                <asp:Literal ID="ieError" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="height10px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 25px;">
                            </td>
                            <td height="25" align="left">
                               
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 25px;">
                            </td>
                            <td height="25" align="left">
                    
                            </td>
                        </tr>                        
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="cp" style="background-image: url('Images/LoginLineBg.gif'); height: 32px;
        line-height: 32px; text-align: center; bottom: 0px; left:0px; position: absolute; width: 100%;">
        系统名称：<asp:Literal ID="WebVer" runat="server"></asp:Literal>
    </div>
    <script type="text/javascript">
        $("#loginDiv").css("left", ($(window).width() - 500) / 2);
        $("#loginDiv").css("top", ($(window).height() - 310) / 2);

        function MM_showHideLayers() {
            var i, p, v, obj, args = MM_showHideLayers.arguments;
            for (i = 0; i < (args.length - 2); i += 3)
                with (document) if (getElementById && ((obj = getElementById(args[i])) != null)) {
                    v = args[i + 2];
                    if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
                    obj.visibility = v;
                }
        }
    </script>
    </form>
</body>
</html>