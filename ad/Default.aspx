<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ad_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>乡巴佬生活馆随机广告</title>
    <meta name="Keywords" content="时尚男装,时尚女装,平板电脑,智能手机,数码相机" />
    <meta name="Description" content="乡巴佬系列软件之淘宝客CMS软件_ZDianPU.com" />
    <link href="../inClude/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/kinSlideshow.min.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="KinSlideshow" style="visibility: hidden; text-align:left; background-color: #<%=bgColor %>; border: 1px solid #<%=bgColor %>;"><asp:Literal ID="kinSlideshow" runat="server"></asp:Literal></div>
    </form>
     <script type="text/javascript">
         $(function () {
             $("#KinSlideshow").KinSlideshow({ mouseEvent: "mouseover", intervalTime: 5, titleFont: { TitleFont_size: 14, TitleFont_family: "'微软雅黑','Microsoft Yahei',arial,SimSun,sans-serif", TitleFont_color: "#<%=fontColor %>" }, titleBar: { titleBar_height: 40, titleBar_bgColor: "#<%=bgColor %>", titleBar_alpha: <%=alpha %> }
             });
         });
    </script>
</body>
</html>
