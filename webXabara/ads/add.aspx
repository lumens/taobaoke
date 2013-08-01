<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="WebXabara_ads_add" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加广告</title>
    <link href="../InClude/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    <!--
    body {
	    margin:8px 8px 0px 0px;
    }
    -->
    </style>
    <script type="text/javascript" src="/InClude/DatePicker/WdatePicker.js"></script>
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tipswindow.js" type="text/javascript"></script>
    <script src="/Scripts/all.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divWidth">
        <div class="titleBg">
            添加广告</div>
        <div class="height10px">
        </div>
    </div>
    <div class="divWidth">
        <table width="100%" class="tableLine">
            <tr>
                <td class="tdTitle">
                    广告位置
                </td>
                <td class="tdContent">
                    <asp:DropDownList ID="adClass" runat="server" CssClass="inputTextIME">
                        <asp:ListItem Value="">== 请选择 ==</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator6"
                        runat="server" ControlToValidate="adClass" ErrorMessage="请选择广告位置"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    广告尺寸
                </td>
                <td class="tdContent">
                    宽
                    <asp:TextBox ID="adW" runat="server" CssClass="inputTextIME" Width="50px"></asp:TextBox>
                    像素<asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator7"
                        runat="server" ControlToValidate="adW" ErrorMessage="*"></asp:RequiredFieldValidator>
                    高
                    <asp:TextBox ID="adH" runat="server" CssClass="inputTextIME" Width="50px"></asp:TextBox>
                    像素<asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator8"
                        runat="server" ControlToValidate="adH" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="wRegExpree"
                        runat="server" ControlToValidate="adW"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="hRegExpree"
                        runat="server" ControlToValidate="adH"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    广告类型
                </td>
                <td class="tdContent">
                    <asp:RadioButtonList ID="adImg" runat="server" RepeatDirection="Horizontal" 
                            RepeatLayout="Flow" CellPadding="2" CellSpacing="2">
                        </asp:RadioButtonList>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator9"
                        runat="server" ControlToValidate="adImg" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    选择文件
                </td>
                <td class="tdContent">
                    <asp:FileUpload ID="imgUpload" runat="server" CssClass="inputTextIME" Width="500px" />
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    连接网址
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="adHttp" runat="server" CssClass="inputTextIME" Width="500px"></asp:TextBox>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="httpRegExpree"
                        runat="server" ControlToValidate="adHttp"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    广告代码</td>
                <td class="tdContent">
                    <asp:TextBox ID="adCode" runat="server" CssClass="inputTextIME" Width="500px" 
                        Height="200px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    开始时间
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="pDate" runat="server" Width="200px" Wrap="False" ToolTip="如没特殊，请使用默认时间"
                        MaxLength="50" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                        BackColor="#F5FDFE" BorderColor="#63C4E0" BorderStyle="Dashed" BorderWidth="1px"
                        Font-Names="courier new, courier, verdana, monospace" Font-Size="12px" ForeColor="#004B7D"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator4"
                        runat="server" ControlToValidate="pDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="pdateRegExpree"
                        runat="server" ControlToValidate="pDate"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    结束时间
                </td>
                <td class="tdContent">
                    <asp:TextBox ID="eDate" runat="server" Width="200px" Wrap="False" ToolTip="如没特殊，请使用默认时间"
                        MaxLength="50" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                        BackColor="#F5FDFE" BorderColor="#63C4E0" BorderStyle="Dashed" BorderWidth="1px"
                        Font-Names="courier new, courier, verdana, monospace" Font-Size="12px" ForeColor="#004B7D"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="#ff3300" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="eDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SetFocusOnError="True" ForeColor="#ff3300" ID="eDateRegExpree"
                        runat="server" ControlToValidate="eDate"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    &nbsp;</td>
                <td class="tdContent">
                    <asp:Button ID="addMoney" runat="server" Text="确认提交" CssClass="inputBottom" OnClick="addMoney_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
