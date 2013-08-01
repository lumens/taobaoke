<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userPopedom.aspx.cs" Inherits="webXabara_adminUser_userPopedom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理员权限设置</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin:8px;
    }
    -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="titleBg">
            管理员权限设置</div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">        
        <table width="100%" class="tableLine">
            <tr>
                <td class="trTitleBG" style="text-align:left;">
                    管理员
                    <asp:Label ID="uid" runat="server" ForeColor="#FF3300"></asp:Label>
                    &nbsp;管理权限分配：
                </td>
            </tr>
            <tr>
                <td style="text-align: left; background-color: #E9F8FC;">
                    <asp:Literal ID="selectPopedomHtm" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tdTitle" style="text-align:left;">
                    <asp:Button ID="setAdmin" runat="server" Text="设置权限" CssClass="inputBottom" OnClick="setAdmin_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
