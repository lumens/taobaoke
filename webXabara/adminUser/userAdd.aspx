<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userAdd.aspx.cs" Inherits="webXabara_adminUser_userAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加管理员</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
<!--
body {
	margin:8px 8px 0px 0px;
}
-->
</style>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="titleBg">
            添加管理员</div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td class="tdTitle">用户名</td>
                <td class="tdContent">
                    <asp:TextBox ID="adminID" runat="server" CssClass="inputText" Width="200px" MaxLength="20"
                        Wrap="False" Style="ime-mode: disabled;"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="adminID" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="adminIDRegularExpressionValidator"
                        runat="server" ControlToValidate="adminID"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">真实姓名</td>
                <td class="tdContent">
                    <asp:TextBox ID="adminName" runat="server" AutoCompleteType="Disabled" CssClass="inputText"
                        Width="200px" MaxLength="10" Wrap="False"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="adminName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="adminNameRegularExpressionValidator"
                        runat="server" ControlToValidate="adminName"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">部门</td>
                <td class="tdContent">
                    <asp:DropDownList ID="adminClass" runat="server" CssClass="inputText">
                        <asp:ListItem Value="">== 请选择 ==</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator6"
                        runat="server" ControlToValidate="adminClass" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">密 码</td>
                <td class="tdContent">
                    <asp:TextBox ID="adminPW" runat="server" CssClass="inputText" Width="200px" MaxLength="20"
                        Wrap="False" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator4"
                        runat="server" ControlToValidate="adminPW" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="adminPWRegularExpressionValidator"
                        runat="server" ControlToValidate="adminPW"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">重复密码</td>
                <td class="tdContent">
                    <asp:TextBox ID="adminPW2" runat="server" CssClass="inputText" Width="200px" MaxLength="20"
                        Wrap="False" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator5"
                        runat="server" ControlToValidate="adminPW2" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="adminPW"
                        ControlToValidate="adminPW2" ErrorMessage="两次输入的密码不同" ForeColor="#FF3300" 
                        SetFocusOnError="True"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle"></td>
                <td class="tdContent">
                    <asp:Button ID="addAdmin" runat="server" Text="添加管理员" CssClass="inputBottom" OnClick="addAdmin_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

