using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webXabara_adminUser_exit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string AName = string.Empty;
        Session.Add("AdminRndNums", string.Empty);
        if (Request.Cookies["XabaraAdmin"] != null)
        {
            AName = HttpUtility.UrlDecode(Request.Cookies["XabaraAdmin"]["AdminName"].Trim());
        }
        Response.Redirect("/", true);
    }
}