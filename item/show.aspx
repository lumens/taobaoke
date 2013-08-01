<%@ Page Language="C#" AutoEventWireup="true" CodeFile="show.aspx.cs" Inherits="item_show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Shortcut Icon" href="/Images/favicon.ico" />
    <link href="/inClude/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            background-color: #f3f3f1;
            background-image:url('/images/no.gif');
        }
    </style>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
</head>
<body>
    <form id="loading" runat="server" name="loading">
    <div style="position: absolute; z-index: 200;" id="linkDiv" runat="server">
        <div>
            <ul>
                <li style="width: 383px; height: 12px; background-image: url('/images/link01.png');
                    background-repeat: no-repeat;"></li>
                <li style="width: 383px; background-image: url('/images/link02.png'); background-repeat: repeat-y; text-align: center;"><asp:Literal ID="img" runat="server"></asp:Literal></li>
                <li style="width: 383px; height: 12px; background-image: url('/images/link03.png');
                    background-repeat: no-repeat;"></li>
            </ul>
        </div>
        <div>
            <ul style="font-size: 14px; color: #7d7d7d;">
                <li style="height: 40px;"></li>
                <li style="text-align: center;">
                    <asp:TextBox ID="chart" runat="server" Style="border-style: none; padding: 0px; background-color: #f3f3f1; color: #7d7d7d; font-family: Arial; font-weight: bolder; width: 350px;"></asp:TextBox>                    
                </li>
                <li style="text-align:center;">
                    <asp:TextBox ID="percent" runat="server" Style="border-style: none; border-width: medium;
                        background-color: #f3f3f1; color: #7d7d7d; text-align: center; width: 30px; font-family: Courier New,Arial;"></asp:TextBox>
                </li>
                <li style="height: 40px;"></li>
                <li style="text-align: center; height: 28px; line-height: 28px; font-weight:bold;">
                    <asp:Label ID="linkWebName" runat="server" Text=""></asp:Label></li>
                <li style="text-align: center; height: 28px; line-height: 28px;">如果浏览器没有自动跳转，请
                    <asp:HyperLink ID="linkWebUrl" runat="server" ForeColor="#FF3300" Font-Size="14px">点击这里</asp:HyperLink>
                </li>
            </ul>
        </div>
    </div>
    <script type="text/javascript">
        document.getElementById("linkDiv").style.left = (document.documentElement.clientWidth - 383) / 2 + "px";
        document.getElementById("linkDiv").style.top = (document.documentElement.clientHeight - 520) / 2 + "px";
        
        var bar = 0;
        var line = "||";  
        var amount ="||";  
        count();
        function count() {
            bar = bar + 2;
            amount = amount + line;
            document.getElementById("chart").value = amount;
            document.getElementById("percent").value = bar + "%";
            if (bar < 98) {
                setTimeout("count()", 30);
            }
            else {
                window.top.location = "<%=urlStr %>";
            }
        }

        $(function () {
            $('img[id="imgshow"]').each(function () {
                $(this).LoadImage({ scaling: true, width: 350, height: 350, loadpic: "/images/loading.gif" });
            });
        });
    </script>
    <asp:Literal ID="countStr" runat="server"></asp:Literal>
    </form>
</body>
</html>
