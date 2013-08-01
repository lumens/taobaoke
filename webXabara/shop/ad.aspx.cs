using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webXabara_shop_ad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        xc.CheckAdminLogin("");

        if (!IsPostBack)
        {
            ad.Text = "<iframe src=\"http://" + Request.Url.Host + "/ad/?size=300\" scrolling=\"no\" frameborder=\"0\" height=\"300px\" width=\"300px\" marginheight=\"0px\" marginwidth=\"0px\"></iframe>";
            adView.Text = ad.Text;
        }
    }

    protected void taobaoKe_Click(object sender, EventArgs e)
    {
        string w = adSize.Text;
        ad.Text = "<iframe src=\"http://" + Request.Url.Host + "/ad/?size=" + w.ToString() + "&a=" + adTitle.Text + "&c=" + color1.Text.Trim().Substring(1) + "&f=" + color2.Text.Trim().Substring(1) + "\" scrolling=\"no\" frameborder=\"0\" height=\"" + w.ToString() + "px\" width=\"" + w.ToString() + "px\" marginheight=\"0px\" marginwidth=\"0px\"></iframe>";
        adView.Text = ad.Text;
    }
}