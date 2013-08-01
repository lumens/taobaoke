<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register src="inClude/bottom.ascx" tagname="bottom" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Shortcut Icon" href="/Images/favicon.ico" />
    <link href="inClude/style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/all.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-image:url('/images/bg.gif'); background-repeat:repeat-x; background-position:top;">
        <div class="divWidth">            
            <div class="divLeft" style="height:24px; line-height:24px;">
                <asp:Literal ID="ppRnd" runat="server" ViewStateMode="Disabled"></asp:Literal>
            </div>
            <div class="divRight" style="height:24px; line-height:24px;">
                <a href="http://www.zdianpu.com" target="_blank">乡巴佬㊣官网</a>&nbsp;<span class="headLine">|</span>&nbsp;<a href="http://www.zdianpu.com/soft/jiajiao/" target="_blank">乡巴佬㊣家教网</a>&nbsp;<span class="headLine">|</span>&nbsp;<a href="http://www.zdianpu.com/soft/taobaoke/" target="_blank">乡巴佬㊣淘宝客</a>
            </div>
        </div>
        <div class="height5px"></div>
        <div class="divWidth">
            <table cellspacing="0" cellpadding="0" style="border-width:0px; border-collapse:collapse; width:100%;">
                <tr>
                    <td valign="middle" style="text-align:left;"><a href="/" target="_top"><img src="/images/logo.png" alt="乡巴佬㊣生活馆" title="乡巴佬㊣生活馆" /></a></td>
                    <td valign="middle" align="right"><asp:Literal ID="ad468" runat="server" ViewStateMode="Disabled"></asp:Literal></td>
                </tr>
            </table>        
        </div>
    </div>
    <div class="height5px"></div>
    <div class="divWidth" style="background-image:url('/images/menuBg1.gif'); background-repeat:repeat-x; ">
        <table cellspacing="0" cellpadding="0" style="border-width:0px; border-collapse:collapse; height:35px;" align="center">
	        <tr>                
                <asp:Literal ID="headMenu" runat="server" ViewStateMode="Disabled"></asp:Literal>
	        </tr>
        </table>        
    </div>
    <div class="divWidth" style="background-image:url('images/searchBg.gif'); background-repeat:repeat-x; height:35px;" >
        <table cellspacing="0" cellpadding="0" style="border-width:0px; border-collapse:collapse; height:35px;">
            <tr>
                <td valign="middle" style="width:190px; text-align:right;">
                    <asp:TextBox ID="searchWord" runat="server" CssClass="inputTxt" ViewStateMode="Disabled" style="height:22px; width:180px;"></asp:TextBox>
                </td>
                <td valign="middle" style="width:80px; text-align:left;">
                    <asp:ImageButton ID="searchImgButton" runat="server" 
                        ImageUrl="~/images/searchBottom.gif" ImageAlign="Middle" 
                        ViewStateMode="Disabled" onclick="searchImgButton_Click" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="searchWord" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
                <td valign="middle" style="text-align:left; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"><strong>热搜：</strong>&nbsp;<asp:Literal ID="hotSearch" runat="server" ViewStateMode="Disabled"></asp:Literal></td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="vipCode" runat="server" ViewStateMode="Disabled"></asp:Literal>
    <div runat="server" id="huabao" class="divWidth" style="background-color:#323232;">
        <div class="divLeft" style="padding:10px 0px 5px 5px;">
            <asp:Literal ID="HuaBaoLeft" runat="server" ViewStateMode="Disabled"></asp:Literal>
        </div>

        <div class="divRight" style="padding:10px 5px 5px 0px;">
            <asp:Literal ID="HuaBaoRight" runat="server" ViewStateMode="Disabled"></asp:Literal>
        </div>
    </div>
    <asp:Literal ID="shopList" runat="server" ViewStateMode="Disabled"></asp:Literal>    
    <uc1:bottom ID="bottom1" runat="server" ViewStateMode="Disabled" />    
    </form>
    <script type="text/javascript">
        $(function () {
            $('img[id="imgshow"]').each(function () {
                $(this).LoadImage({ scaling: true, width: 200, height: 200, loadpic: "/images/loading.gif" });
            });
        });
        $(function () {
            $('img[id="imghbh"]').each(function () {
                $(this).LoadImage({ scaling: true, width: 180, height: 230, loadpic: "/images/loading.gif" });
            });
        });
        $(function () {
            $('img[id="imghbw"]').each(function () {
                $(this).LoadImage({ scaling: true, width: 180, height: 150, loadpic: "/images/loading.gif" });
            });
        });
   </script>
</body>
</html>
