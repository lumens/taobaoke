<%@ Page Language="C#" AutoEventWireup="true" CodeFile="otherSet.aspx.cs" Inherits="WebXabara_frame_otherSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参数设置</title>
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
        .style1
        {
            height: 7px;
        }
    -->
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="divWidth">
        <div class="titleBg">
            系统参数设置</div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td colspan="2" style="background-color: #ceeff8;" class="style1">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <a href="http://www.zdianpu.com/help/view/82.htm" target="_blank" style="color:Red;">如何申请？</a>&nbsp;淘宝客 AppKey</td>
                <td class="tdContent">
                    <asp:TextBox ID="taobaoKeAppKey" runat="server" CssClass="inputTextIME" 
                        Width="500px" MaxLength="100"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="taobaoKeAppKey" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    淘宝客 AppSecret
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="taobaoKeAppSecret" runat="server" CssClass="inputTextIME" 
                        Width="500px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="taobaoKeAppSecret" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <a href="http://www.zdianpu.com/help/view/81.htm" target="_blank" style="color:Red;">如何申请？</a>&nbsp;阿里妈妈 捆绑淘宝帐号
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="taobaoKeAlimamaID" runat="server" CssClass="inputText" 
                        Width="500px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="taobaoKeAlimamaID" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">网站搜索热门关键词</td>
                <td class="tdContent">
                    <asp:TextBox ID="hotSearch" runat="server" CssClass="inputText" Width="500px" 
                        MaxLength="100" Height="60px" TextMode="MultiLine"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="hotSearch" 
                        ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                </td>
                <td class="tdContent">
                    <asp:Button ID="sysSet" runat="server" Text="设置完成" CssClass="inputBottom" OnClick="sysSet_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
