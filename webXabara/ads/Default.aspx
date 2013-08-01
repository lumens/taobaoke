<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WebXabara_ads_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>广告管理</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin:8px 8px 0px 0px;
    }
    -->
    </style>
    <script type="text/javascript">
        function CheckAll(e, itemname) {
            var aa = document.getElementsByName(itemname);
            if (aa == undefined) return;
            for (var i = 0; i < aa.length; i++) aa[i].checked = e.checked;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="titleBg">
            广告管理
            <asp:DropDownList ID="adWhere" runat="server" CssClass="inputTextIME" AutoPostBack="True"
                OnSelectedIndexChanged="adWhere_SelectedIndexChanged">
                <asp:ListItem Value="">== 广告位置 ==</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="height10px">
        </div>
        <div>
            <asp:Repeater ID="RepeaterList" runat="server">
                <HeaderTemplate>
                    <table width="100%" class="tableLine">                        
                </HeaderTemplate>                
                <ItemTemplate>
                    <tr>
                        <td class="trTitleBG">
                            广告位置
                        </td>
                        <td class="trTitleBG">
                            开始时间
                        </td>
                        <td class="trTitleBG">
                            结束时间
                        </td>
                        <td class="trTitleBG">
                            发布时间
                        </td>
                        <td class="trTitleBG">
                            发布者
                        </td>
                        <td class="trTitleBG">
                            操作
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #E9F8FC;">
                            <%# getClass(Convert.ToInt64(Eval("classID").ToString().Trim()))%>
                        </td>
                        <td style="background-color: #E9F8FC; text-align:center;">
                            <%# Convert.ToDateTime(Eval("starDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </td>
                        <td style="background-color: #E9F8FC; text-align:center;">
                            <%# Convert.ToDateTime(Eval("stopDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </td>
                        <td style="background-color: #E9F8FC; text-align:center;">
                            <%# Convert.ToDateTime(Eval("postDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </td>
                        <td style="background-color: #E9F8FC; text-align:center;">
                            <%# Eval("adminID").ToString().Trim()%>
                        </td>
                        <td style="background-color: #E9F8FC; text-align:center;">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("aID","del.aspx?id={0}").ToString().Trim() %>'
                                Font-Bold="True"><span onclick="return confirm('确定删除吗？');">删除</span></asp:HyperLink>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("aID","edit.aspx?id={0}").ToString().Trim() %>' Font-Bold="True">修改</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="text-align: center; padding: 5px;">
                            <%# Eval("adCode").ToString().Trim()%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="height10px">
        </div>
        <div>
            <asp:Literal ID="lblCurrentPage" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>
