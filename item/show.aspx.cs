using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class item_show : System.Web.UI.Page
{
    public string urlStr = "http://www.zdianpu.com";

    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        dbDataFunction db = new dbDataFunction();
        string title = "找店铺 ZDianPU.com";

        if (!IsPostBack)
        {
            long idTemp = xc.SafeNum(Request.QueryString["id"]);
            if (idTemp < 1)
            {
                string urlID = Page.RouteData.Values["sID"].ToString();
                urlID = urlID.Replace(".htm", "");
                idTemp = xc.SafeNum(urlID);
            }

            DataTable dt = db.getDataTable("select title,pic_url,click_url from taobaoKe where num_iid=" + idTemp.ToString());
            if (dt.Rows.Count > 0)
            {
                title = dt.Rows[0]["title"].ToString().Trim();                
                linkWebName.Text = title;
                urlStr = dt.Rows[0]["click_url"].ToString().Trim();
                img.Text = "<a href=\"" + urlStr + "\"><img id=\"imgshow\" src=\"" + dt.Rows[0]["pic_url"].ToString().Trim() + "\" style=\"width:350px; height:350px;\" title=\"" + title + "\" alt=\"" + title + "\" /></a>";
                linkWebUrl.NavigateUrl = urlStr;
            }
            else
            {
                Response.Redirect("http://s.click.taobao.com/t_9?p=mm_14288563_0_0&l=http%3A%2F%2Fwww.tmall.com", true);
            }
            dt.Dispose();

            xc.webMeta(Page, title, XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "metaStr"));
            countStr.Text = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "countScript");
        }

        this.Page.Title = title + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName") + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "titleMeta");
    }
}