using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebXabara_Default : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {        
        Page.Title = " 管理系统";

        string errorStr = Request.QueryString["errorTitle"];
        if (!string.IsNullOrEmpty(errorStr as string))
        {
            xc.divError(string.Empty, Server.UrlDecode(errorStr), 400, 150, "?errorUrl=" + Server.UrlEncode(Request.QueryString["errorUrl"]), "top");
        }
        else
        {
            if (!String.IsNullOrEmpty(Session["AdminRndNums"] as string)) //基本session验证免登陆
            {
                Response.Redirect("admin.aspx", true);
            }
        }

        if (!IsPostBack)
        {
            ClientName.Text = Request.Url.Host;
            WebVer.Text =  "管理系统&nbsp;" + xc.XabaraVer.ToString().Trim();

            if (Request.Cookies["XabaraAdmin"] != null)
            {
                UID.Text = Request.Cookies["XabaraAdmin"]["AdminID"].Trim();
            }

            clintIE.Text = xc.clientBrower(1);
            clintIEver.Text = xc.clientBrower(2);

            if (xc.clientBrower(1).ToLower().Equals("ie")) //判断浏览器
            {
                if (Convert.ToDouble(xc.clientBrower(2).ToString()) < 8)  //判断浏览器版本
                {
                    ieError.Text = @"<strong>版本提示：</strong>为了更好的体验本管理系统，我们建议您升级 <a href='http://www.microsoft.com/china/windows/internet-explorer/default.aspx' target='_blank' style='color:red;font-weight:bold;'>Internet Explorer</a> 新版浏览器！";
                }
            }

            //string clientIP = xc.GetIP();
            //ipStr.Text = "<span style=\"color:red;\">" + clientIP + "</span>&nbsp;" + xc.GetIpWhere(clientIP);
            //ip限制
            //string ips = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "ip");
            //if (!string.IsNullOrEmpty(ips as string))
            //{
            //    if (ips.IndexOf(clientIP) < 0)
            //    {
            //        ipStr.Text += " <span class=\"redF\">您的IP不允许登录！</span>";
            //        imgLogin.Enabled = false;
            //        UID.Enabled = false;
            //        PW.Enabled = false;
            //        RndNum.Enabled = false;
            //    }
            //}

            //FormCheck fCheck = new FormCheck();
            //UIDRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("userID", 3, 20, false);
            //UIDRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("userID", 3, 20, true);
            //PWRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("passWord", 8, 20, false);
            //PWRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("passWord", 8, 20, true);
            //RndNumReg.ValidationExpression = fCheck.RegExpressionValidator("rndNums", 0, 0, false);
            //RndNumReg.ErrorMessage = fCheck.RegExpressionValidator("rndNums", 0, 0, true);

            //RndNum.Attributes.Add("onfocus", "MM_showHideLayers('rndgetValidate', '', 'show')");
        }
    }

    protected void imgLogin_Click(object sender, ImageClickEventArgs e)
    {

            string uid = UID.Text.Trim().ToLower().ToString();
            string pw = xc.GetMd5(PW.Text.ToString());
            string strSql = "select * from AdminUser where userID='" + uid + "' and UserPW='" + pw + "' and loginFlag=2 ";

            DataTable dt = db.getDataTable(strSql);

            if (dt.Rows.Count < 1)
            {
                xc.divError("登陆提示", "您的帐号或密码不正确！", 350, 150, xc.AdminFileName, "top");
            }
            else
            {
                long lgNums = xc.SafeNum(dt.Rows[0]["LoginNum"].ToString()) + 1;
                string strDim = "@SessionError,@LoginNum,@LoginDate,@LoginIP";
                string rndNums = xc.GetRnd("abc", 8);
                Session.Add("AdminRndNums", rndNums);

                string[] strValue = new string[5];
                strValue[0] = xc.GetMd5(rndNums);
                strValue[1] = lgNums.ToString();
                strValue[2] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strValue[3] = xc.GetIP();
                db.InsertUpdateDB("AdminUser", strDim, strValue, "userID='" + uid + "'");

                xc.insertMyLog(uid, "admin", "管理员登录");

                HttpCookie XabaraAdminCookies = new HttpCookie("XabaraAdmin");  //cookies写入磁盘
                XabaraAdminCookies.Values.Add("AdminID", uid);
                XabaraAdminCookies.Values.Add("AdminName", HttpUtility.UrlEncode(dt.Rows[0]["UserName"].ToString().Trim()));
                XabaraAdminCookies.Values.Add("AdminLgNums", lgNums.ToString());
                XabaraAdminCookies.Values.Add("AdminLgDate", dt.Rows[0]["LoginDate"].ToString().Trim());
                XabaraAdminCookies.Values.Add("AdminLgIP", dt.Rows[0]["LoginIP"].ToString().Trim());
                XabaraAdminCookies.Expires = DateTime.Now.AddDays(15d);
                Response.Cookies.Add(XabaraAdminCookies);

                dt.Dispose();
                Response.Redirect("admin.aspx", true);
           
                
        }
    }
}
