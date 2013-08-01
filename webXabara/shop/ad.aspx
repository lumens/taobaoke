<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ad.aspx.cs" Inherits="webXabara_shop_ad"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>获取淘宝客广告代码</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 8px 8px 0px 0px;
        }
        .colorwell
        {
            border: 2px solid #fff;
            width: 6em;
            text-align: center;
            cursor: pointer;
        }
        body .colorwell-selected
        {
            border: 2px solid #000;
            font-weight: bold;
        }
    </style>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/inClude/farbtastic/farbtastic.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/inClude/farbtastic/farbtastic.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#demo').hide();
            var f = $.farbtastic('#picker');
            var p = $('#picker').css('opacity', 0.25);
            var selected;
            $('.colorwell')
                .each(function () { f.linkTo(this); $(this).css('opacity', 0.75); })
                .focus(function () {
                    if (selected) {
                        $(selected).css('opacity', 0.75).removeClass('colorwell-selected');
                    }
                    f.linkTo(this);
                    p.css('opacity', 1);
                    $(selected = this).css('opacity', 1).addClass('colorwell-selected');
                });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="titleBg">
            获取淘宝客广告代码</div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td class="tdTitle">
                    广告宽度
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adSize" runat="server" CssClass="inputTextIME" Width="100px">300</asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="adSize"
                        ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
                <td class="tdContent" rowspan="5"><div id="picker"></div></td>
                <td class="tdContent" rowspan="6" valign="middle" style="text-align: center; padding:20px;">
                    <asp:Literal ID="adView" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    标题背景透明度
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adTitle" runat="server" CssClass="inputTextIME" Width="100px">0.5</asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="adTitle"
                        ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    边框颜色
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="color1" name="color1" class="colorwell" runat="server" Width="100px">#333333</asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="color1"
                        ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    标题文字颜色
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="color2" name="color2" class="colorwell" runat="server" Width="100px">#ffffff</asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="color2"
                        ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    &nbsp;
                </td>
                <td class="tdContent">
                    <asp:Button ID="taobaoKe" runat="server" Text="获取代码" CssClass="inputBottom" OnClick="taobaoKe_Click" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    &nbsp; 广告代码
                </td>
                <td class="tdContent" colspan="2" style="text-align:center;">
                    <asp:TextBox ID="ad" runat="server" CssClass="inputTextIME" Width="98%" Height="100px"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>    
    </form>
</body>
</html>
