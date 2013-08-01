<%@ Page Language="C#" AutoEventWireup="true" CodeFile="systemSet.aspx.cs" Inherits="WebXabara_frame_systemSet"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    -->
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="divWidth">
        <div class="titleBg">基本参数设置</div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td colspan="4" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    网站名称
                </td>
                <td colspan="3" class="tdContent">
                    <asp:TextBox ID="webName" runat="server" CssClass="inputText" Width="500px" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="webName" ErrorMessage="请输入网站名称/单位名称/公司名称" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    网站域名
                </td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="webDomains" runat="server" CssClass="inputTextIME" Width="500px"
                        MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidatorWeb"
                        runat="server" ControlToValidate="webDomains" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>&nbsp;<asp:RegularExpressionValidator
                            SetFocusOnError="True" ID="urlRegExpress" runat="server" ControlToValidate="webDomains"
                            ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <br />
                    请输入网站域名，如www.zdianpu.com
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    备 案 号
                </td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="webBak" runat="server" CssClass="inputText" Width="200px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    腾讯 QQ
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="webQQ" runat="server" CssClass="inputTextIME" Width="200px" MaxLength="100"></asp:TextBox>
                    <br />
                    多个QQ请用 | 符合分隔
                </td>
                <td class="tdTitle" style="text-align: center">
                    淘宝旺旺
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="wangwang" runat="server" CssClass="inputText" Width="200px" 
                        MaxLength="100"></asp:TextBox>
                    <br />
                    多个旺旺请用 | 符合分隔
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    服务邮箱
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="webEmail" runat="server" CssClass="inputTextIME" Width="200px"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ID="emailRegularExpressionValidator"
                        runat="server" ControlToValidate="webEmail" ErrorMessage="RegularExpressionValidator"
                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                </td>
                <td class="tdTitle" style="text-align: center">
                    移动电话
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="webMobile" runat="server" CssClass="inputTextIME" Width="200px"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ID="mobileRegularExpressionValidator"
                        runat="server" ControlToValidate="webMobile" ErrorMessage="RegularExpressionValidator"
                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    服务电话
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="webtel" runat="server" CssClass="inputTextIME" Width="200px"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ID="telRegularExpressionValidator"
                        runat="server" ControlToValidate="webtel" ErrorMessage="RegularExpressionValidator"
                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                </td>
                <td class="tdTitle">
                    传真号码
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="webFax" runat="server" CssClass="inputTextIME" Width="200px"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ID="faxRegularExpressionValidator"
                        runat="server" ControlToValidate="webFax" ErrorMessage="RegularExpressionValidator"
                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>免费电话</strong>
                </td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="freePhone" runat="server" CssClass="inputTextIME" Width="400px"></asp:TextBox>
                    &nbsp; 如：400 或 800 电话
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    文字水印
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="waterWords" runat="server" CssClass="inputTextIME" Width="200px"
                        MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="waterWords" ErrorMessage="*"></asp:RequiredFieldValidator>
                    如图片水印，请不要修改 xabara.com 默认属性值
                </td>
                <td class="tdTitle" style="text-align: center">
                    图片水印
                </td>
                <td class="tdContent">
                    <asp:FileUpload ID="waterImgFileUpload" runat="server" CssClass="inputText" Width="200px" />
                    <asp:Button ID="waterImgButton" runat="server" CssClass="inputBottom" OnClick="waterImgButton_Click"
                        Text="更换图片水印" ValidationGroup="imgWater" />
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="waterImgFileUpload" ErrorMessage="*" ValidationGroup="imgWater"
                        ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    网站LOGO</td>
                <td class="tdContent" colspan="3">
                    <asp:FileUpload ID="logoImgFile" runat="server" CssClass="inputText" 
                        Width="200px" />
                    <asp:Button ID="logoImgButton" runat="server" CssClass="inputBottom"
                        Text="更换网站LOGO" ValidationGroup="logo" onclick="logoImgButton_Click" />
                    <asp:RequiredFieldValidator SetFocusOnError="True" 
                        ID="RequiredFieldValidator15" runat="server"
                        ControlToValidate="logoImgFile" ErrorMessage="*" ValidationGroup="logo"
                        ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">IP白名单</td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="ips" runat="server" CssClass="inputTextIME" Width="99%" Height="30px"
                        TextMode="MultiLine"></asp:TextBox>
                    <br />
                    只允许以上IP登录后台，IP间隔请用半角 | 符合间隔，如不是固定IP用户请不要设置此项。 您当前IP：<asp:Label ID="ipAdd" 
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    Keywords关键词
                 </td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="keyWords" runat="server" CssClass="inputText" Width="99%" Height="100px"
                        TextMode="MultiLine"></asp:TextBox>
                    <br />
                    以便搜索引擎（百度/Google等）收录，字符之间用半角 , 符合间隔
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="keyWords" ErrorMessage="请输入一些关键词" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    Description简述
                </td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="mKeyWords" runat="server" CssClass="inputText" Width="99%" Height="100px"
                        TextMode="MultiLine"></asp:TextBox>
                    <br />
                    以便搜索引擎（百度/Google等）收录，字符之间用半角 , 符合间隔
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="mKeyWords" ErrorMessage="请输入一些关键词，中英文均可以" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <strong>标题关键词</strong>
                </td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="titleMeta" runat="server" CssClass="inputText" Width="99%" Height="50px"
                        TextMode="MultiLine"></asp:TextBox>
                    <br />
                    以便搜索引擎（百度/Google等）收录，字符之间用 | 符合间隔
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14"
                        runat="server" ControlToValidate="titleMeta" ErrorMessage="请输入一关键词，中英文均可以" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 8px; background-color: #ceeff8;">
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    统计代码
                </td>
                <td class="tdContent" colspan="3">
                    <asp:TextBox ID="countStr" runat="server" CssClass="inputTextIME" Width="99%" Height="150px"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle"></td>
                <td colspan="3" class="tdContent">
                    <asp:Button ID="sysSet" runat="server" Text="设置完成" CssClass="inputBottom" OnClick="sysSet_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
