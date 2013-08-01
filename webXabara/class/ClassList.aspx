<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassList.aspx.cs" Inherits="webXabara_class_ClassList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理分类</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />    
    <style type="text/css">
    <!--
    body {
	    margin:8px 8px 0px 0px;
    }
        -->
    </style>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
</head>

<body>
<form id="form1" runat="server">
<div class="divWidth">
    <div class="titleBg"><asp:Label ID="titleClass" runat="server"></asp:Label></div>
    <div class="height10px"></div>
</div

<div class="divWidth">
    <table width="100%" class="tableLine">
      <tr>
        <td class="tdTitle">分类名称</td>
        <td class="tdContent">
            <asp:DropDownList ID="DropDownListClass" runat="server" CssClass="inputText" 
                ToolTip="请选择分类，并注意根目录的选择" AutoPostBack="True" 
                onselectedindexchanged="DropDownListClass_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">== 请选择分类 ==</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Button ID="displayClassList" runat="server" CssClass="inputBottom" 
                Text="显示所选分类排序" onclick="displayClassList_Click" />
            &nbsp;<asp:Button ID="delClass" runat="server" Text="删除所选分类" 
                CssClass="inputBottom" onclick="delClass_Click" 
                OnClientClick="return confirm('此操作将删除该分类下所有的关联信息，确定吗？');" />
            &nbsp;<asp:RequiredFieldValidator  SetFocusOnError="True" ForeColor="#ff3300"   ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="DropDownListClass" ErrorMessage="请选择分类"></asp:RequiredFieldValidator>
          </td>
      </tr>
      <tr>
        <td class="tdTitle">修改名称</td>
        <td class="tdContent">
            <ul style="line-height:28px;">
                <li>现分类名称：<asp:TextBox ID="className" runat="server" CssClass="inputText" 
                Width="150px" ValidationGroup="editClass"></asp:TextBox> <asp:RequiredFieldValidator  SetFocusOnError="True" ForeColor="#ff3300"   ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="className" ErrorMessage="请填写新分类名称"  ValidationGroup="editClass"></asp:RequiredFieldValidator></li>
                <li>
                    现外链网址：<asp:TextBox ID="classUrl" 
                        runat="server" CssClass="inputTextIME" Width="300px" 
                        ValidationGroup="editClass"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="classUrlRegExpre"
                        runat="server" ControlToValidate="classUrl" ValidationGroup="editClass"></asp:RegularExpressionValidator>
                </li>
                <li>
                    淘宝客商家：<asp:TextBox ID="tk" 
                        runat="server" CssClass="inputText" Width="300px" 
                        ValidationGroup="editClass"></asp:TextBox>
                    <br />
                    添加品牌官方淘宝商城店铺ID，有利于淘宝客宝贝自动更新。</li>
                <li><asp:Button ID="editClass" runat="server" Text="修改所选分类名称" CssClass="inputBottom" 
                onclick="editClass_Click" ValidationGroup="editClass"/> </li>
            </ul>
            </td>
      </tr>
      <tr>
        <td class="tdTitle">分类图片</td>
        <td class="tdContent">
            <asp:FileUpload ID="waterImgFileUpload" runat="server" CssClass="inputText" 
                Width="200px" />
            <asp:Button ID="waterImgButton" runat="server" CssClass="inputBottom" 
                onclick="waterImgButton_Click" Text="添加/修改图片分类" ValidationGroup="img" />
            <asp:Label ID="uploadError" runat="server" ForeColor="#FF3300"></asp:Label>
            <asp:RequiredFieldValidator  SetFocusOnError="True" ForeColor="#ff3300"   ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="waterImgFileUpload" ErrorMessage="没特殊用途，此项不需要上传图片" 
                ValidationGroup="img"></asp:RequiredFieldValidator>
          </td>
      </tr>  
      <tr>
        <td class="tdTitle">分类排序</td>
        <td class="tdContent">
            <ul style="line-height:230%;">
            <asp:Literal ID="classListName" runat="server"></asp:Literal>        
            <li><asp:Button ID="editClassbottom" runat="server" CssClass="inputBottom" 
                onclick="editClassbottom_Click" Text="修改所选子分类排序" Visible="False" /><asp:HiddenField ID="treeCount" runat="server" /></li>
            </ul>
          </td>
      </tr>
    </table>
</div>

<div class="divWidth" style="height:10px;"></div>
    <div style=" text-align:left; color:Red;">注意：当分类有相关信息归类，请不要随意删除，这将会影响整个系统数据出错！</div>
    <div class="divWidth" style="text-align:left;"><asp:TreeView ID="treeList" runat="server" ImageSet="Arrows"></asp:TreeView></div>
</form>
</body>
</html>