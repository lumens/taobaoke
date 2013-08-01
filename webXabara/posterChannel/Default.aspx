<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="webXabara_posterChannel_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理淘画报</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 8px 8px 0px 0px;
        }
    </style>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
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
            管理淘画报主题 <asp:DropDownList 
                ID="classID" runat="server" CssClass="inputText" AutoPostBack="True" 
                onselectedindexchanged="classID_SelectedIndexChanged">                
            </asp:DropDownList>
        &nbsp;<asp:CheckBox ID="goodCheck" runat="server" Text="推荐" ToolTip="推荐" />
            <asp:TextBox ID="searchWord" runat="server" CssClass="inputText" Width="150px"></asp:TextBox>
&nbsp;<asp:Button ID="search" runat="server" CssClass="inputBottom" Text="搜索" 
                onclick="search_Click" />
        </div>
        <div class="height10px">
        </div>
        <div runat="server" id="listNews">
            <asp:Repeater ID="RepeaterList" runat="server">
            <HeaderTemplate>
                <table width="100%" class="tableLine">
                    <tr>
                        <td class="trTitleBG">
                            <input name="chkall" value="1" type="checkbox" onclick="CheckAll(this,'batDel')" />
                        </td>
                        <td class="trTitleBG">
                            分类
                        </td>
                        <td class="trTitleBG">
                            主题
                        </td>
                        <td class="trTitleBG">
                            效果图
                        </td>
                        <td class="trTitleBG">
                            点击量
                        </td>
                        <td class="trTitleBG">
                            更新时间
                        </td>                        
                        <td class="trTitleBG">
                            发布时间
                        </td>
                    </tr>                    
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align:center;"><input name="batDel" type="checkbox" value='<%# Eval("id").ToString().Trim()%>' /></td>
                    <td><%# Eval("channel_name").ToString().Trim()%><%# Convert.ToInt32(Eval("weight").ToString().Trim()) > 10000 ? " <span class=\"redF\">推荐</span>" : ""%></td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("id","/huabao/show/{0}.htm").ToString().Trim() %> ' Target="_blank"><%# Eval("title").ToString().Trim()%></asp:HyperLink><br /><%# Eval("title_short").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;">
                       <img src="<%# Eval("cover_pic_url_w").ToString().Trim() %>" style="height:100px;" />
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("hits").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;">
                         <%# Convert.ToDateTime(Eval("modified_date")).ToString("yy-MM-dd")%>
                    <td style="text-align:center;">
                        <%# Convert.ToDateTime(Eval("create_date")).ToString("yy-MM-dd")%>
                    </td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td style="background-color: #E9F8FC; text-align:center;"><input name="batDel" type="checkbox" value='<%# Eval("id").ToString().Trim()%>' /></td>
                    <td style="background-color: #E9F8FC;"><%# Eval("channel_name").ToString().Trim()%><%# Convert.ToInt32(Eval("weight").ToString().Trim()) > 10000 ? " <span class=\"redF\">推荐</span>" : ""%></td>
                    <td style="background-color: #E9F8FC;">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("id","/huabao/show/{0}.htm").ToString().Trim() %> ' Target="_blank"><%# Eval("title").ToString().Trim()%></asp:HyperLink><br /><%# Eval("title_short").ToString().Trim()%>
                    </td>
                    <td style="text-align:center; background-color: #E9F8FC;">
                        <img src="<%# Eval("cover_pic_url_w").ToString().Trim() %>" style="height:100px;" />
                    </td>
                    <td style="text-align:center; background-color: #E9F8FC;">
                        <%# Eval("hits").ToString().Trim()%>
                    </td>
                    <td style="text-align:center; background-color: #E9F8FC;">
                         <%# Convert.ToDateTime(Eval("modified_date")).ToString("yy-MM-dd")%>
                    <td style="text-align:center; background-color: #E9F8FC;">
                        <%# Convert.ToDateTime(Eval("create_date")).ToString("yy-MM-dd")%>
                    </td>                    
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        </div>
        <div class="height5px">
        </div>
        <div id="errDiv" runat="server" class="tableAdminLine">
            <div style="height: 150px;">
            </div>
            <div style="text-align:center;">
                <asp:Label ID="errInfo" runat="server" ForeColor="#FF3300"></asp:Label></div>
            <div style="height: 150px;">
            </div>
        </div>
        <div class="height10px">
        </div>
        <div class="tableAdminLine" style="padding: 5px;" id="checkDiv" runat="server">
            <div style="text-align: center; height: 36px; line-height: 36px; background-color: White;">
                全选<input name="chkall" value="1" type="checkbox" onclick="CheckAll(this,'batDel')" />
                &nbsp;<asp:Button 
                    ID="del" runat="server" Text="删除" CssClass="inputBottom" 
                    CausesValidation="False" onclick="del_Click" />&nbsp;<asp:Button 
                    ID="good" runat="server" Text="推荐" CssClass="inputBottom" 
                    CausesValidation="False" onclick="good_Click" />&nbsp;<asp:Button 
                    ID="unGood" runat="server" Text="取消推荐" CssClass="inputBottom" 
                    CausesValidation="False" onclick="unGood_Click" /></div>
        </div>
        <div class="height10px">
        </div>
        <div>
            <a name="foot" id="foot"></a><asp:Literal ID="lblCurrentPage" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>