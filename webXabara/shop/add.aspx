<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="webXabara_shop_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加商品</title>
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
        <div class="titleBg">添加商品</div>
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
                    <asp:DropDownList ID="classID" runat="server" CssClass="inputText" ToolTip="请选择分类">
                        <asp:ListItem Value="">== 请选择分类 ==</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="classID" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    商品ID号
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="ids" runat="server" CssClass="inputTextIME" Width="400px" 
                        Height="100px" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;<br />
                    多个用逗号,分隔 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="ids" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    &nbsp;
                </td>
                <td class="tdContent">
                    <asp:Button ID="taobaoKe" runat="server" Text="转换淘宝客" CssClass="inputBottom" 
                        onclick="taobaoKe_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
