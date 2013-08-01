<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="WebXabara_Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="Shortcut Icon" href="http://www.zdianpu.com/Images/favicon.ico" />
    <title>管理系统</title>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth" style="background-image: url(Images/topBg.jpg); background-repeat: repeat-x;">
        <table cellpadding="0" cellspacing="0" align="left" border="0" style="width:100%;">
            <tr>
                <td style="text-align:left; width:400px;"><asp:Image ID="Image2" runat="server" ImageUrl="images/logo.jpg" /></td>
                <td align="left" valign="top" style="background-image:url('images/rightBg.jpg'); background-position:right; background-repeat:no-repeat;">
                    <table cellpadding="0" cellspacing="0" align="left" border="0" style="width:100%;">
                        <tr>
                            <td class="whiteF" style="height: 28px; line-height: 28px; text-align: right; padding-right: 10px;">[&nbsp;<a href="/" style="color: White; font-weight: bold;" target="_blank">网站首页</a>&nbsp;]&nbsp;&nbsp;[&nbsp;<a href="adminUser/userEdit.aspx" style="color: White; font-weight: bold;" target="mainFrame" onclick="op('number2')">修改密码</a>&nbsp;]&nbsp;&nbsp;[&nbsp;<a href="adminUser/exit.aspx" style="color: White; font-weight: bold;" target="_top">我要退出</a>&nbsp;]&nbsp;&nbsp;欢迎&nbsp;<asp:Label
                                ID="LgName" runat="server" CssClass="yellowF"></asp:Label>&nbsp;第&nbsp;<asp:Label
                                    ID="lgNum" runat="server"></asp:Label>&nbsp;次登录</td>
                        </tr>
                        <tr>
                            <td style="vertical-align:bottom; height:48px;">
                                <asp:Table ID="navTable" runat="server" CellPadding="0" BorderWidth="0px" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell ID="default" runat="server" CssClass="headBlue"><asp:HyperLink ID="defaultLink" runat="server" NavigateUrl="admin.aspx" Target="_top" CssClass="headBlueLink"><span onclick="op('number1')">管理首页</span></asp:HyperLink></asp:TableCell>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell ID="taobaoke" runat="server" CssClass="headBlue"><asp:HyperLink ID="taobaokeLink" runat="server" NavigateUrl="shop/" Target="mainFrame" CssClass="headBlueLink"><span onclick="op('number7')">淘宝客</span></asp:HyperLink></asp:TableCell>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell ID="posterChannel" runat="server" CssClass="headBlue"><asp:HyperLink ID="posterChannelLink" runat="server" NavigateUrl="posterChannel/" Target="mainFrame" CssClass="headBlueLink"><span onclick="op('number8')">淘画报</span></asp:HyperLink></asp:TableCell>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                        <asp:TableCell ID="ad" runat="server" CssClass="headBlue">
                                            <asp:HyperLink ID="adLink" runat="server" NavigateUrl="ads/" Target="mainFrame" CssClass="headBlueLink"><span onclick="op('number6')">广告系统</span></asp:HyperLink></asp:TableCell>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell ID="sys" runat="server" CssClass="headBlue"><asp:HyperLink ID="sysLink" runat="server" NavigateUrl="frame/systemSet.aspx" Target="mainFrame" CssClass="headBlueLink"><span onclick="op('number3')">系统设置</span></asp:HyperLink></asp:TableCell>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell ID="help" runat="server" CssClass="headBlue"><asp:HyperLink ID="helpLink" runat="server" NavigateUrl="http://help.zdianpu.com/" Target="_blank" CssClass="headBlueLink">帮助中心</asp:HyperLink></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="divWidth" style="background-image: url(Images/infoBg.jpg); background-repeat: repeat-x;
        height: 30px; line-height: 30px;">
        <div class="divLeft">
            <div style="text-align: left; padding-left: 10px;">
                <asp:Label ID="timeJS" runat="server"></asp:Label></div>
        </div>
        <div class="divRight">
            <div style="text-align: right; padding-right: 10px;">
                <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" ImageUrl="images/Notice.gif" />&nbsp;<asp:HyperLink
                    ID="myXabara" runat="server" NavigateUrl="http://www.zdianpu.com/" Target="_blank">有问题请访问乡巴佬㊣找店铺网&nbsp;ZDianPU.com</asp:HyperLink></div>
        </div>
    </div>
    <div class="divWidth" id="main">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td valign="top" align="right" id="mainLeft" style="width: 180px; white-space:nowrap; overflow:hidden; ">
                    <div style="background-image: url('Images/left01.jpg'); background-repeat: no-repeat;
                        text-align: center; height: 44px; width: 176px;">
                        <ul>
                            <li style="height: 13px; font-size: 0px;"></li>
                            <li style="height: 31px; line-height: 31px; text-align: left; padding-left: 50px;"><span
                                class="whiteF"><strong>我的工作台</strong></span></li>
                        </ul>
                    </div>
                    
                    <!--淘宝客-->
                    <div class="leftTitle" onclick="op('number7')">
                            <span style="padding-left:20px;"><img src="images/iconShrink.gif" style="vertical-align: middle;" alt="淘宝客" />&nbsp;淘宝客</span></div>
                    <div id="number7" style="display: none; background-image: url('Images/left03.jpg');
                        width: 176px;" class="ulLeft">
                        <ul>
                           <li><asp:HyperLink ID="HyperLink1" runat="server" CssClass="leftNav" Target="mainFrame"
                        NavigateUrl="shop/add.aspx">手工添加</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink13" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="shop/">管理商品</asp:HyperLink> <asp:HyperLink ID="HyperLink17" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="shop/?isGood=1">推荐商品</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink12" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="shop/?ifGood=1">建议推荐</asp:HyperLink> <asp:HyperLink ID="HyperLink18" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="shop/?ifGood=-1">滞销商品</asp:HyperLink></li>
                            <li><asp:HyperLink ID="HyperLink16" runat="server" CssClass="leftNav" Target="mainFrame"
                        NavigateUrl="shop/taobao.aspx">自动更新</asp:HyperLink> <asp:HyperLink ID="HyperLink21" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="shop/taobaoke.aspx">淘客转换</asp:HyperLink></li>
                            <li><asp:HyperLink ID="HyperLink3" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="shop/ad.aspx">获取广告代码</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink14" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="class/ClassAdd.aspx?tClass=taobaoke&title=添加分类">添加分类</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink15" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="class/ClassList.aspx?tClass=taobaoke&title=管理分类">管理分类</asp:HyperLink></li>
                        </ul>                        
                        <div style="font-size: 0px;">
                            <asp:Image ID="Image3" runat="server" ImageUrl="images/left04.jpg" /></div>
                    </div>

                    <!--淘画报-->
                    <div class="leftTitle" onclick="op('number8')">
                            <span style="padding-left:20px;"><img src="images/iconShrink.gif" style="vertical-align: middle;" alt="淘画报" />&nbsp;淘画报</span></div>
                    <div id="number8" style="display: none; background-image: url('Images/left03.jpg');
                        width: 176px;" class="ulLeft">
                        <ul>
                            <li>
                                <asp:HyperLink ID="HyperLink9" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="posterChannel/">管理画报</asp:HyperLink> <asp:HyperLink ID="HyperLink11" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="posterChannel/?isGood=1">推荐画报</asp:HyperLink></li>
                           <li>
                                <asp:HyperLink ID="HyperLink5" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="posterChannel/?ifGood=1">建议推荐</asp:HyperLink></li>                           
                           <li>
                                <asp:HyperLink ID="HyperLink8" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="posterChannel/posterSearch.aspx">按最新时间同步画报</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink4" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="posterChannel/posterNews.aspx?type=RECOMMEND">按官方推荐同步画报</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink20" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="posterChannel/autoDel.aspx">自动清除</asp:HyperLink></li>                           
                            <li>
                                <asp:HyperLink ID="HyperLink6" runat="server" CssClass="leftNav" Target="mainFrame" NavigateUrl="posterChannel/posterIDs.aspx">同步画报分类</asp:HyperLink></li>
                        </ul>                        
                        <div style="font-size: 0px;">
                            <asp:Image ID="Image4" runat="server" ImageUrl="images/left04.jpg" /></div>
                    </div>

                    <!--广告系统-->
                    <div class="leftTitle" onclick="op('number6')">
                        <span style="padding-left: 20px;">
                            <img src="images/iconShrink.gif" style="vertical-align: middle;" alt="广告系统" />&nbsp;广告系统</span></div>
                    <div id="number6" style="display: none; background-image: url('Images/left03.jpg');
                        width: 176px;" class="ulLeft">
                        <ul>
                            <li>
                                <asp:HyperLink ID="HyperLink58" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="ads/add.aspx">发布广告</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink59" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="ads/">管理广告</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink60" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="class/ClassAdd.aspx?tClass=ads&title=添加广告位置">添加广告位置</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink61" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="class/ClassList.aspx?tClass=ads&title=管理广告位置">管理广告位置</asp:HyperLink></li>
                        </ul>
                        <div style="font-size: 0px;">
                            <asp:Image ID="Image6" runat="server" ImageUrl="images/left04.jpg" /></div>
                    </div>

                    <!--系统设置-->
                    <div class="leftTitle" onclick="op('number3')">
                            <span style="padding-left:20px;"><img src="images/iconShrink.gif" style="vertical-align: middle;" alt="系统设置" />&nbsp;系统设置</span></div>
                    <div id="number3" style="display: none; background-image: url('Images/left03.jpg');
                        width: 176px;" class="ulLeft">
                        <ul>
                            <li>
                                <asp:HyperLink ID="HyperLink7" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="frame/systemSet.aspx">基本参数</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="frame/otherSet.aspx">系统参数</asp:HyperLink></li>                           
                            <li>
                                <asp:HyperLink ID="HyperLink48" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="frame/regSoft.aspx">注册码认证</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink19" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="class/updateCache.aspx">全站缓存</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink22" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="frame/htm.aspx">生成静态首页</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink46" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="frame/sitemap.aspx?id=baidu">生成百度sitemap</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink50" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="frame/sitemap.aspx?id=google">生成谷歌sitemap</asp:HyperLink></li>
                        </ul>                       
                        <div style="font-size: 0px;">
                            <asp:Image ID="Image9" runat="server" ImageUrl="images/left04.jpg" /></div>
                    </div>

                    <!--管理团队-->
                    <div class="leftTitle" onclick="op('number2')">
                            <span style="padding-left:20px;"><img src="images/iconShrink.gif" style="vertical-align: middle;" alt="管理团队" />&nbsp;管理团队</span></div>
                    <div id="number2" style="display: none; background-image: url('Images/left03.jpg');
                        width: 176px;" class="ulLeft">
                        <ul>
                            <li>
                                <asp:HyperLink ID="HyperLink10" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="adminUser/userEdit.aspx">修改资料</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="HyperLink37" runat="server" CssClass="leftNav" Target="mainFrame"
                                    NavigateUrl="adminUser/exe.aspx?id=a">操作日志</asp:HyperLink></li>
                        </ul>                        
                        <div style="font-size: 0px;">
                            <asp:Image ID="Image10" runat="server" ImageUrl="images/left04.jpg" /></div>
                    </div>

                    <!--技术支持-->
                    <div class="leftTitle" onclick="op('number1')">
                            <span style="padding-left:20px;"><img src="images/iconShrink.gif" style="vertical-align: middle;" alt="技术支持" />&nbsp;技术支持</span></div>
                    <div id="number1" style="background-image: url('Images/left03.jpg');
                        width: 176px;">
                        <div class="height5px">
                        </div>
                        <table align="center" border="0" cellpadding="0" cellspacing="0" style="line-height: 20px;">
                            <tr>
                                <td style="text-align: right;">
                                    <strong>官网</strong>：
                                </td>
                                <td style="text-align: left;">
                                    <a href="http://www.zdianpu.com" target="_blank">ZDianPU.com</a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <strong>客服</strong>：
                                </td>
                                <td style="text-align: left;">
                                    839808029
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                </td>
                                <td valign="top" style="height:30px; text-align:center;">
                                    <asp:Literal ID="qqLink" runat="server"></asp:Literal></td>
                            </tr>
                        </table>                       
                        <div style="font-size: 0px;">
                            <asp:Image ID="Image11" runat="server" ImageUrl="images/left04.jpg" /></div>
                    </div>

                    <div style="width: 176px; text-align: center;">
                        <asp:Image ID="Image5" runat="server" ImageUrl="images/left05.jpg" />
                    </div>
                    <div class="height5px">
                    </div>
                </td>
                <td valign="middle" id="mainArrow" style="width: 20px; text-align: left; background-image: url('images/ChangDown.gif');
                    background-repeat: repeat-y;">
                    <img id="changArrow" src="Images/ChangLeft.gif" title="隐藏菜单" alt="隐藏菜单" style="cursor:pointer;" />
                </td>
                <td valign="top" id="mainRight">
                    <iframe name="mainFrame" id="mainFrame" scrolling="auto" align="right" frameborder="0"
                        marginheight="0" marginwidth="0" width="100%" src="frame/main.aspx"></iframe>
                </td>
            </tr>
        </table>
    </div>
    <div class="cp" style="background-image: url(Images/footBg.gif); height: 26px; line-height: 26px;
            text-align: center; position:absolute; z-index:1000; bottom:0px; left:0px; width:100%;">
            系统名称：<asp:Label ID="SoftVer" runat="server"></asp:Label>
    </div>
    <script type="text/javascript">
        $("#mainRight").css("width", $(window).width() - 200);
        $("#mainFrame").css("height", $(window).height() - 132);
        document.body.style.overflow = "hidden";

        $(document).ready(function () {
            $("#changArrow").click(function () {
                $("#mainLeft").toggle();
                if ($("#mainLeft").is(":hidden")) {
                    $("#changArrow").attr("src", "Images/ChangRight.gif");
                    $("#changArrow").attr("title", "显示菜单");
                    $("#changArrow").attr("alt", "显示菜单");
                }
                else {
                    $("#changArrow").attr("src", "Images/ChangLeft.gif");
                    $("#changArrow").attr("title", "隐藏菜单");
                    $("#changArrow").attr("alt", "隐藏菜单");
                }
            });
        });

        function op(obj) {
            var arr = new Array("number1", "number2", "number3", "number6", "number7", "number8");
            var num = document.getElementById(obj);

            if (num.style.display == "none") {
                num.style.display = "block";
            }
            else {
                if (num.style.display == "block") {
                    num.style.display = "none";
                }
            }

            for (var i = 0; i < arr.length; i++) {
                if (arr[i] != obj) {
                    var num = document.getElementById(arr[i]);
                    num.style.display = "none";
                }
            }
        }
    </script>
    </form>
</body>
</html>
