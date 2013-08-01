using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebXabara_Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        xc.CheckAdminLogin(string.Empty);
        Page.Title = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName") + " 管理系统";

        if (!IsPostBack)
        {            
            LgName.Text = HttpUtility.UrlDecode(Request.Cookies["XabaraAdmin"]["AdminName"].Trim()) + "[" + xc.adminID + "]";
            lgNum.Text = Request.Cookies["XabaraAdmin"]["AdminLgNums"].ToString().Trim();

            timeJS.Text = xc.getDataTime(1, 1, 0);
            qqLink.Text = xc.QQHTM("839808029", "41");
            SoftVer.Text = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName") + " " + xc.XabaraVer.ToString().Trim();
        }
    }
}
