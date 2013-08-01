<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="inClude_error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    <!--
        body,td,th {
        font-family: 'Microsoft Yahei',arial,SimSun,sans-serif;
        font-size: 14px;
        color: #333333;
    }
        body {
        margin:0px;
        padding:0px;
        background-color: #daecee;
    }
        div,form,img,ul,ol,li,dl,dt,dd {margin:0px; padding:0px; border:0px; }
        li{list-style-type:none;}
        em {font-style:normal;}
        h1,h2,h3,h4,h5,h6 { margin:0px; padding:0px;font-size:12px; font-weight:normal;}
    -->
    </style>
    <meta http-equiv="refresh" content="5;URL=/" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="png" style="position:absolute; z-index:200;" runat="server">
        <a href="/" target="_top"><img id="img404" src="/images/404.png" /></a>
    </div>
    <script type="text/javascript">
        document.getElementById("png").style.left = (document.documentElement.clientWidth - 607) / 2 + "px";
        document.getElementById("png").style.top = (document.documentElement.clientHeight - 607) / 2 + "px"; 
    </script>    
    </form>
</body>
</html>
