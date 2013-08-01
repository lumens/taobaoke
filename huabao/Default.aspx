<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="huabao_Default" %>
<%@ Register src="../inClude/bottom.ascx" tagname="bottom" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Shortcut Icon" href="/Images/favicon.ico" />
    <link href="../inClude/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
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
    <div class="divWidth" style="background-image:url('/images/searchBg.gif'); background-repeat:repeat-x; height:35px;" >
        <table cellspacing="0" cellpadding="0" style="border-width:0px; border-collapse:collapse; height:35px;">
            <tr>
                <td valign="middle" style="width:190px; text-align:right;">
                    <asp:TextBox ID="searchWord" runat="server" Width="180px" CssClass="inputTxt" Height="22px" ViewStateMode="Disabled"></asp:TextBox>
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
    <div class="divWidth">
        <div class="divHuiLine">
            <div style="background-image:url('/images/navArrowBg.gif'); background-repeat:repeat-x;"><table border="0" cellpadding="0" cellspacing="0" style="width:100%;"><tr><td style="text-align:left; height:32px; line-height:32px; padding-left:10px;"><asp:Literal ID="navClass" runat="server" ViewStateMode="Disabled"></asp:Literal><img src="/images/navArrow.gif" style="vertical-align:top;" /></td><td style="text-align:right; height:32px; line-height:32px; padding-right:10px;">所有商品均自动取自于淘宝淘画报栏目</td></tr></table></div>
            <div style="background-image:url('/images/searchBg.gif'); background-repeat:repeat-x;"><table border="0" cellpadding="0" cellspacing="0" style="width:100%;"><tr><td style="text-align:left; height:35px; line-height:35px; padding-left:10px;">排序：<asp:Literal ID="listTop" runat="server" ViewStateMode="Disabled"></asp:Literal></td><td style="text-align:right; height:35px; line-height:35px;"><asp:Literal ID="pageTop" runat="server" ViewStateMode="Disabled"></asp:Literal></td></tr></table></div>
        </div>
    </div>   
    <div class="height5px"></div>
    <div class="divWidth">
        <asp:Repeater ID="RepeaterList" runat="server" ViewStateMode="Disabled">
        <HeaderTemplate>
        <table border="0" cellpadding="0" cellspacing="0">
        </HeaderTemplate>
            <ItemTemplate>
			<asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%# (Container.ItemIndex + 1) % 5 ==1 %>'>
				<tr>
			</asp:PlaceHolder>
                <td onmouseover="this.className='divRedLine'" onmouseout="this.className='divOutLine'" valign="top" class="divOutLine">
                    <ul>
                        <li style="text-align:center; width:184px; height:184px;" class="divLineDotted"><a href="/show/<%# Eval("num_iid").ToString().Trim() %>.htm" target="_blank"><img id="imgshow" src="<%# Eval("pic_url").ToString().Trim() %>_310x310.jpg" alt="<%# Eval("title").ToString().Trim() %>" title="<%# Eval("auction_short_title").ToString().Trim() %>" style="width:184px; height:184px;" /></a></li>                        
                        <li class="moneyFont" style="height:26px; line-height:26px; text-align:left;"><%# money(Eval("price").ToString().Trim())%></li>
                        <li style="line-height:18px; text-align:left; color:#666666;"><a href="/show/<%# Eval("num_iid").ToString().Trim() %>.htm" target="_blank"><%# Eval("title").ToString().Trim() %></a></li>
                    </ul>
                </td>
            <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible='<%# (Container.ItemIndex + 1) % 5 ==0 %>'>
				</tr>
                <tr><td colspan="5" class="height5px"></td></tr>
			</asp:PlaceHolder>
		    </ItemTemplate>							
		<FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="divWidth">
        <div id="errDiv" runat="server" class="divHuiLine">
            <div style="height: 150px;">
            </div>
            <div style="text-align:center;">
                <asp:Label ID="errInfo" runat="server" ForeColor="#FF3300" ViewStateMode="Disabled"></asp:Label></div>
            <div style="height: 150px;">
            </div>
        </div>
        <div class="height5px"></div>
        <div style="height: 1px; background-color: #dddddd; font-size:0px;"></div>
        <div class="height10px"></div>
        <div style="text-align:right;"><asp:Literal ID="lblCurrentPage" runat="server" ViewStateMode="Disabled"></asp:Literal></div>
        <div class="height5px"></div>
    </div>
    <uc1:bottom ID="bottom1" runat="server" />
    </form>
    <script type="text/javascript">
        $(function () {
            $('img[id="imgshow"]').each(function () {
                $(this).LoadImage({ scaling: true, width: 184, height: 184, loadpic: "/images/loading.gif" });
            });
        });
   </script>
</body>
</html>
