<%@ Page Language="C#" AutoEventWireup="true" CodeFile="exe.aspx.cs" Inherits="webXabara_adminUser_exe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理员操作记录</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
<!--
body {
	margin:8px 8px 0px 0px;
}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="titleBg">
            管理员操作记录
            </div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <asp:Repeater ID="RepeaterList" runat="server">
            <HeaderTemplate>
                <table width="100%" class="tableLine">
                    <tr>
                        <td class="trTitleBG">
                            用户名
                        </td>
                        <td class="trTitleBG">
                            登陆地点
                        </td>
                        <td class="trTitleBG">
                            操作时间
                        </td>                        
                        <td class="trTitleBG">
                            URL记录
                        </td>                        
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("UserId").ToString().Trim()%>
                    </td>
                    <td title="<%# Eval("loginIP").ToString().Trim() %>">
                        <%# ipStr(Eval("loginIP").ToString().Trim())%>
                    </td>
                    <td style="text-align:center;">
                        <%# Convert.ToDateTime(Eval("LoginDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss") %>
                    </td>
                    <td style="text-align:left;">
                        <%# Eval("exeTitle").ToString().Trim() + "　" + Server.UrlDecode(Eval("exeUrl").ToString().Trim())%>
                    </td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr>
                    <td style="background-color:#E9F8FC;">
                        <%# Eval("UserId").ToString().Trim()%>
                    </td>
                    <td style="background-color:#E9F8FC;" title="<%# Eval("loginIP").ToString().Trim() %>">
                        <%# ipStr(Eval("loginIP").ToString().Trim())%>
                    </td>
                    <td style="background-color:#E9F8FC; text-align:center;">
                        <%# Convert.ToDateTime(Eval("LoginDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss") %>
                    </td>
                    <td style="background-color:#E9F8FC;">
                        <%# Eval("exeTitle").ToString().Trim() + "　" + Server.UrlDecode(Eval("exeUrl").ToString().Trim())%>
                    </td>                    
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <asp:Literal ID="lblCurrentPage" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>