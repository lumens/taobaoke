<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="webXabara_shop_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理店铺信息</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 8px 8px 0px 0px;
        }
    </style>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
    <script src="/include/jqzoom/jquery.jqzoom-core-pack.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('a#piczoom').jqzoom({
                zoomType: 'reverse'
            });
        });

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
            管理商品 <asp:DropDownList 
                ID="classID" runat="server" CssClass="inputText" AutoPostBack="True" 
                onselectedindexchanged="classID_SelectedIndexChanged">
                <asp:ListItem Value="">== 分类 ==</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="clearData" runat="server" CssClass="inputBottom" Text="清除" 
                onclick="clearData_Click" />
        &nbsp;<asp:CheckBox ID="goodCheck" runat="server" Text="推荐商品" ToolTip="推荐商品" />
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
                            店名
                        </td>
                        <td class="trTitleBG">
                            所在地
                        </td>
                        <td class="trTitleBG">
                            效果图
                        </td>
                        <td class="trTitleBG">
                            价格
                        </td>
                        <td class="trTitleBG">
                            <a href="?sale=1">推广量</a>
                        </td>
                        <td class="trTitleBG">
                            提成
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
                    <td><%# getTreeName(Eval("classID").ToString().Trim())%><%# Convert.ToInt16(Eval("isGood").ToString().Trim())>0?" <span class=\"redF\">推荐</span>":"" %></td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("shop_click_url").ToString().Trim() %> ' Target="_blank"><%# Eval("nick").ToString().Trim()%></asp:HyperLink>
                    </td>
                    <td title="<%# Eval("title").ToString().Trim() %>">
                        <a href="<%# Eval("click_url").ToString().Trim() %>" target="_blank"><%# Eval("item_location").ToString().Trim()%></a>                        
                    </td>
                    <td style="text-align:center;">
                        <a id="piczoom" title="imagesZoom" href="<%# Eval("pic_url").ToString().Trim() %>" target="_blank"><img src="<%# Eval("pic_url").ToString().Trim()+ "_310x310.jpg" %>" style="height:100px;" /></a>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("price").ToString().Trim()%>
                    </td>
                    <td style=" text-align:center;">
                        <%# Eval("volume").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;">
                        <%# "佣金：<span class=\"redF\">" + Eval("commission").ToString().Trim() + "</span>元&nbsp;" + (Convert.ToDouble(Eval("commission_rate").ToString().Trim()) / 100).ToString() + "%　月推广量：" + Eval("commission_num").ToString().Trim() + " 件 佣金：" + Eval("commission_volume").ToString().Trim()+" 元"%>
                    </td>
                    <td style="text-align:center;">
                         <%# Convert.ToDateTime(Eval("updateDate")).ToString("yy-MM-dd")%>
                    <td style="text-align:center;">
                        <%# Convert.ToDateTime(Eval("postDate")).ToString("yy-MM-dd")%>
                    </td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td style="background-color: #E9F8FC; text-align:center;"><input name="batDel" type="checkbox" value='<%# Eval("id").ToString().Trim()%>' /></td>
                    <td style="background-color: #E9F8FC;"><%# getTreeName(Eval("classID").ToString().Trim())%><%# Convert.ToInt16(Eval("isGood").ToString().Trim())>0?" <span class=\"redF\">推荐</span>":"" %></td>
                    <td style="background-color: #E9F8FC;">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("shop_click_url").ToString().Trim() %> ' Target="_blank"><%# Eval("nick").ToString().Trim()%></asp:HyperLink>
                    </td>
                    <td style="background-color: #E9F8FC;" title="<%# Eval("title").ToString().Trim() %>">
                        <a href="<%# Eval("click_url").ToString().Trim() %>" target="_blank"><%# Eval("item_location").ToString().Trim()%></a>                        
                    </td>
                    <td style="text-align:center; background-color: #E9F8FC;">
                        <a id="piczoom" title="imagesZoom" href="<%# Eval("pic_url").ToString().Trim() %>" target="_blank"><img src="<%# Eval("pic_url").ToString().Trim()+ "_310x310.jpg" %>" style="height:100px;" /></a>
                    </td>
                    <td style="text-align:center;background-color: #E9F8FC;">
                        <%# Eval("price").ToString().Trim()%>
                    </td>
                    <td style=" text-align:center;background-color: #E9F8FC;">
                        <%# Eval("volume").ToString().Trim()%>
                    </td>
                    <td style="text-align:center;background-color: #E9F8FC;">
                        <%# "佣金：<span class=\"redF\">" + Eval("commission").ToString().Trim() + "</span>元&nbsp;" + (Convert.ToDouble(Eval("commission_rate").ToString().Trim()) / 100).ToString() + "%　月推广量：" + Eval("commission_num").ToString().Trim() + " 件 佣金：" + Eval("commission_volume").ToString().Trim()+" 元"%>
                    </td>
                    <td style="text-align:center;background-color: #E9F8FC;">
                         <%# Convert.ToDateTime(Eval("updateDate")).ToString("yy-MM-dd")%>
                    <td style="text-align:center;background-color: #E9F8FC;">
                        <%# Convert.ToDateTime(Eval("postDate")).ToString("yy-MM-dd")%>
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
                    CausesValidation="False" onclick="unGood_Click" />&nbsp;<asp:Button 
                    ID="autoGood" runat="server" Text="自动推荐" CssClass="inputBottom" 
                    CausesValidation="False" onclick="autoGood_Click" /></div>
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