<%@ Page Language="C#" AutoEventWireup="true" CodeFile="regSoft.aspx.cs" Inherits="webXabara_frame_regSoft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>注册码认证</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body 
    {
        margin:8px 8px 0px 0px;
    } 
    -->
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="divWidth">
        <div class="titleBg">
            注册码认证</div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td colspan="2" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle" style="text-align:center;">
                    认证码
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="myCode" runat="server" CssClass="inputTextIME" Width="600px" 
                        Height="80px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle" style="text-align:center;">
                    注册码
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="regCode" runat="server" CssClass="inputText" Width="600px" Height="80px"
                        TextMode="MultiLine"></asp:TextBox>
                    <br />
                   <span style="color:Red;">注：授权认证请到乡巴佬㊣找店铺网站获取注册码</span> 
                    <asp:HyperLink ID="softLink" runat="server" Target="_blank">http://www.zdianpu.com</asp:HyperLink> &nbsp;<asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator6"
                        runat="server" ControlToValidate="regCode" ErrorMessage="请输入注册码"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                </td>
                <td class="tdContent">
                    <asp:Button ID="sysSet" runat="server" Text="确认注册" CssClass="inputBottom" 
                        OnClick="sysSet_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>