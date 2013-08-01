<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bottom.ascx.cs" Inherits="inClude_bottom" %>
<div class="height10px">
</div>
<div class="divWidth">
    <div style="height: 1px; background-color: #dddddd; font-size:0px;">
    </div>
    <div class="height5px">
    </div>
    <div style="text-align:center;">
        <asp:Literal ID="bottomNav" runat="server"></asp:Literal>
    </div>
    <div class="height5px">
    </div>
    <div>
        <ul style="text-align:center; line-height:180%; list-style-type:none;">
            <li>版权所有：<asp:Label ID="webTitle" runat="server" Text=""></asp:Label>&nbsp;Copyright&nbsp;&copy;&nbsp;2008-<asp:Label
            ID="yearLab" runat="server"></asp:Label>&nbsp;<asp:Literal ID="webVer" runat="server"></asp:Literal>&nbsp;<asp:Literal ID="miibeian" runat="server"></asp:Literal></li>
            <li>特别申明：本站为淘宝网购物商品导购网站，所有商品均选自淘宝网官方信誉大卖家，交易均在淘宝网完成，请放心购物！</li>
            <li style="text-align:center;">联系站长：<asp:Literal ID="telInfo" runat="server"></asp:Literal>&nbsp;<asp:Literal ID="qqLink" runat="server"></asp:Literal>&nbsp;<asp:Literal ID="tbLink" runat="server"></asp:Literal></li>
            <li style="text-align:center;"><asp:Literal ID="countStr" runat="server"></asp:Literal></li>
        </ul>
    </div>
</div>
<script type="text/javascript" src="http://toptrace.taobao.com/assets/getAppKey.js" topappkey="<%=appKey %>" defer="defer"></script> 