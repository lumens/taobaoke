<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userList.aspx.cs" Inherits="WebXabara_userList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理员列表</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="titleBg">
            管理员列表
            <asp:DropDownList ID="classList" runat="server" CssClass="inputText" OnSelectedIndexChanged="classList_SelectedIndexChanged"
                AutoPostBack="True">
                <asp:ListItem Value="">== 不限 ==</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="keyWord" runat="server" CssClass="inputText"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="inputBottom" Text="搜索" OnClick="searchButton_Click"
                Width="50px" />
            <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator1"
                runat="server" ControlToValidate="keyWord" ErrorMessage="*"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <asp:Repeater ID="RepeaterList" runat="server">
            <HeaderTemplate>
                <table width="100%" class="tableLine">
                    <tr>
                        <td class="trTitleBG">
                            用户名
                        </td>
                        <td class="trTitleBG">
                            姓名
                        </td>
                        <td class="trTitleBG">
                            部门
                        </td>
                        <td class="trTitleBG">
                            电子邮箱
                        </td>
                        <td class="trTitleBG">
                            联系电话
                        </td>
                        <td class="trTitleBG">
                            QQ
                        </td>
                        <td class="trTitleBG">
                            最近登陆时间
                        </td>
                        <td class="trTitleBG">
                            上次登陆IP地址
                        </td>
                        <td class="trTitleBG">
                            登录次数
                        </td>
                        <td class="trTitleBG">
                            操作
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("UserId").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("UserName").ToString().Trim() %>
                    </td>
                    <td style="text-align:center;">
                        <%# getClass(Convert.ToInt32(Eval("classID")))%>
                    </td>
                    <td>
                        <%# Eval("userEmail").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("userMobile").ToString().Trim() + (!string.IsNullOrEmpty(Eval("userTel").ToString().Trim()) ? " " + Eval("userTel").ToString().Trim() : "") %>
                    </td>
                    <td style="text-align:center;">
                        <%# getQQ(Eval("userQQ").ToString().Trim())%>
                    </td>
                    <td style="text-align:center;">
                        <%# Convert.ToDateTime(Eval("LoginDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss") %>
                    </td>
                    <td>
                        <%# Eval("LoginIP").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("LoginNum").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("UserId","userDel.aspx?id={0}").ToString().Trim() %>'
                            Font-Bold="True"><span onclick="return confirm('删除管理员将对老数据显示真实姓名有影响，确认吗？');">删除</span></asp:HyperLink>&nbsp;<asp:HyperLink
                                ID="DelHyperLink" runat="server" NavigateUrl='<%# Eval("Uid","userPw.aspx?id={0}").ToString().Trim() %>'
                                Font-Bold="True"><span onclick="return confirm('确定重设密码吗？');">重设密码</span></asp:HyperLink>&nbsp;<asp:HyperLink
                                    ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("UserId","exe.aspx?id={0}") %>'><strong>操作记录</strong></asp:HyperLink>&nbsp;&nbsp;<asp:HyperLink
                                        ID="setPopedom" runat="server" NavigateUrl='<%# Eval("UserId","userPopedom.aspx?id={0}") %>'
                                        Font-Bold="True">设置权限</asp:HyperLink>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td style="background-color: #E9F8FC;">
                        <%# Eval("UserId").ToString().Trim()%>
                    </td>
                    <td style="background-color: #E9F8FC; text-align:center;">
                        <%# Eval("UserName").ToString().Trim() %>
                    </td>
                    <td style="background-color: #E9F8FC; text-align:center;">
                        <%# getClass(Convert.ToInt32(Eval("classID")))%>
                    </td>
                    <td style="background-color: #E9F8FC;">
                        <%# Eval("userEmail").ToString().Trim()%>
                    </td>
                    <td style="background-color: #E9F8FC; text-align:center;">
                        <%# Eval("userMobile").ToString().Trim() + (!string.IsNullOrEmpty(Eval("userTel").ToString().Trim()) ? " " + Eval("userTel").ToString().Trim() : "") %>
                    </td>
                    <td style="background-color: #E9F8FC; text-align:center;">
                        <%# getQQ(Eval("userQQ").ToString().Trim())%>
                    </td>
                    <td style="background-color: #E9F8FC; text-align:center;">
                        <%# Convert.ToDateTime(Eval("LoginDate").ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss") %>
                    </td>
                    <td style="background-color: #E9F8FC;">
                        <%# Eval("LoginIP").ToString().Trim()%>
                    </td>
                    <td style="background-color: #E9F8FC; text-align:center;">
                        <%# Eval("LoginNum").ToString().Trim()%>
                    </td>
                    <td style="background-color: #E9F8FC; text-align:center;">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("UserId","userDel.aspx?id={0}").ToString().Trim() %>'
                            Font-Bold="True"><span onclick="return confirm('删除管理员将对老数据显示真实姓名有影响，确认吗？');">删除</span></asp:HyperLink>&nbsp;<asp:HyperLink
                                ID="DelHyperLink" runat="server" NavigateUrl='<%# Eval("Uid","userPw.aspx?id={0}").ToString().Trim() %>'
                                Font-Bold="True"><span onclick="return confirm('确定重设密码吗？');">重设密码</span></asp:HyperLink>&nbsp;<asp:HyperLink
                                    ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("UserId","exe.aspx?id={0}") %>'><strong>操作记录</strong></asp:HyperLink>&nbsp;&nbsp;<asp:HyperLink
                                        ID="setPopedom" runat="server" NavigateUrl='<%# Eval("UserId","userPopedom.aspx?id={0}") %>'
                                        Font-Bold="True">设置权限</asp:HyperLink>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="height10px">
    </div>
    <div class="divWidth">
        <asp:Literal ID="lblCurrentPage" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
