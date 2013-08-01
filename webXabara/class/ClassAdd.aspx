<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassAdd.aspx.cs" Inherits="webXabara_class_ClassAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加分类</title>
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
        <div class="titleBg"><asp:Label ID="titleClass" runat="server"></asp:Label></div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td class="tdTitle">
                    <strong>分类名称</strong>
                </td>
                <td class="tdContent">
                    <asp:DropDownList ID="DropDownListClass" runat="server" CssClass="inputText" ToolTip="请选择分类，并注意根目录的选择">
                        <asp:ListItem Value="0">== 新增根分类 ==</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:TextBox ID="className" runat="server" CssClass="inputText" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="className" ErrorMessage="请填写分类名称"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>分类排序</strong>
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="classList" runat="server" CssClass="inputTextIME" Width="60px"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="classListRegExpre"
                        runat="server" ControlToValidate="classList"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    外链接网址</td>
                <td class="tdContent">
                    <asp:TextBox ID="classUrl" runat="server" CssClass="inputTextIME" Width="300px"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="classUrlRegExpre"
                        runat="server" ControlToValidate="classUrl"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    淘宝客店铺ID</td>
                <td class="tdContent">
                    <asp:TextBox ID="tk" runat="server" CssClass="inputText" Width="300px"></asp:TextBox>
                    <br />
                    添加品牌官方淘宝商城店铺ID，有利于淘宝客宝贝自动更新。</td>
            </tr>
            <tr>
                <td class="tdTitle">
                    &nbsp;
                </td>
                <td class="tdContent">
                    <asp:Button ID="addClass" runat="server" Text="添加分类" CssClass="inputBottom" OnClick="addClass_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="height10px"></div>
    <div class="divWidth">
        <div style="text-align: left; color: Red;">
        注意：添加新分类时，请正确选择父目录！</div>
    </div>    
    <div class="divWidth" style="text-align: left;">
        <asp:TreeView ID="treeList" runat="server" ImageSet="Arrows">
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
