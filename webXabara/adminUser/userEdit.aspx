<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userEdit.aspx.cs" Inherits="webXabara_adminUser_userEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改资料</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
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
            修改资料</div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td class="tdTitle">
                    <strong>用户名</strong>
                </td>
                <td class="tdContent">
                    <asp:Label ID="Uid" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>真实姓名</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adminName" runat="server" CssClass="inputText" Width="200px" MaxLength="20"
                        Wrap="False"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="adminName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="adminNameRegularExpressionValidator"
                        runat="server" ControlToValidate="adminName"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>部门</strong>
                </td>
                <td class="tdContent">
                    <asp:DropDownList ID="adminClass" runat="server" CssClass="inputText">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="adminClassValidator"
                        runat="server" ControlToValidate="adminClass" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:Label ID="classLab" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>新密码</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adminPW" runat="server" CssClass="inputText" Width="200px" TextMode="Password"
                        MaxLength="20" Wrap="False"></asp:TextBox>
                    &nbsp;如不需改密码留空即可<asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300"
                        ID="adminPWRegularExpressionValidator" runat="server" ControlToValidate="adminPW"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>重复新密码</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adminPW2" runat="server" CssClass="inputText" Width="200px" TextMode="Password"
                        MaxLength="20" Wrap="False"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="adminPW"
                        ControlToValidate="adminPW2" ErrorMessage="两次输入的密码不正确" ForeColor="#FF3300"></asp:CompareValidator>
                    <asp:HiddenField ID="oldPWHidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>电子邮箱</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adminMail" runat="server" CssClass="inputText" Width="200px" MaxLength="30"
                        Wrap="False" Style="ime-mode: disabled;"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="mailRegularExpressionValidator"
                        runat="server" ControlToValidate="adminMail"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>固定电话</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adminTel" runat="server" CssClass="inputText" Width="200px" MaxLength="30"
                        Wrap="False" Style="ime-mode: disabled;"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="telRegularExpressionValidator"
                        runat="server" ControlToValidate="adminTel"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>手机</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adminMobile" runat="server" CssClass="inputText" Width="200px" MaxLength="30"
                        Wrap="False" Style="ime-mode: disabled;"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="mobileRegularExpressionValidator"
                        runat="server" ControlToValidate="adminMobile"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>QQ</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adminQQ" runat="server" CssClass="inputText" Width="200px" MaxLength="30"
                        Wrap="False" Style="ime-mode: disabled;"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="qqRegularExpressionValidator"
                        runat="server" ControlToValidate="adminQQ"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle"></td>
                <td class="tdContent"><asp:Button ID="editAdmin" runat="server" Text="修改资料" CssClass="inputBottom" OnClick="editAdmin_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

