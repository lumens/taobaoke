<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="InClude_eWebEditor_XabaraUpload_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件上传</title>
    <link href="../../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
<!--
body {
	margin:0px;
	padding:0px;
}
-->
</style>
    <script type="text/javascript" src="/InClude/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" align="left">
        <tr>
            <td>
                <table border="0" cellspacing="1" cellpadding="1">
                    <tr>
                        <td style="text-align: left; height: 30px;">
                            <asp:DropDownList ID="flag" runat="server" CssClass="inputText" ToolTip="请选择上传类型">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">
                            <asp:FileUpload ID="uploadfile" runat="server" CssClass="inputText" ToolTip="点击选择本地相关文件上传" />
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="WaterMark" runat="server" Text="水印" ToolTip="如需图片水印效果，请选择" Checked="True" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Button ID="ButUpload" runat="server" OnClick="Button1_Click" Text="上传" CssClass="inputBottom" />
                        </td>
                        <td style="text-align: left;">
                            <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator1"
                                runat="server" ControlToValidate="uploadfile" ErrorMessage="请选择文件后上传"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td rowspan="2">
                <asp:Image ID="preImage" runat="server" Visible="False" Height="60px" Width="90px" />
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="1" cellpadding="1" align="left">
                    <tr>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="indexPicSet" runat="server" CssClass="inputText" ToolTip="如需请设置首页图片预览图"
                                AutoPostBack="true" OnSelectedIndexChanged="indexPicSet_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">
                            <asp:Button ID="imgPreBottom" runat="server" CausesValidation="False" CssClass="inputBottom"
                                OnClick="imgPreBottom_Click" Text="重新插入" Visible="False" />
                            <asp:Button ID="delPreImg" runat="server" CausesValidation="False" CssClass="inputBottom"
                                Text="删除当前图片" Visible="False" OnClick="delPreImg_Click" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblinfo" runat="server" ForeColor="#ff3300"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
