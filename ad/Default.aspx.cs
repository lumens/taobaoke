using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ad_Default : System.Web.UI.Page
{
    public string fontColor = "ffffff";
    public string bgColor = "000000";
    public string alpha = "0.5";
    public string adSizeStr = "298";

    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc=new XabaraCom();
        dbDataFunction db = new dbDataFunction();

        if (!IsPostBack)
        {
            long adSize = xc.SafeNum(Request.QueryString["size"]);
            if (adSize < 1)
            {
                adSize = 300;
            }
            double topSql = Math.Ceiling(adSize / 37.5);
            if (topSql % 2 == 1)
            {
                topSql -= 1;
            }
            adSize = adSize - 2;
            adSizeStr = adSize.ToString();            

            string f = Request.QueryString["f"];
            if (!string.IsNullOrEmpty(f as string))
            {
                fontColor = f;
            }
            string c = Request.QueryString["c"];
            if (!string.IsNullOrEmpty(c as string))
            {
                bgColor = c;
            }
            string a = Request.QueryString["a"];
            if (!string.IsNullOrEmpty(a as string))
            {
                alpha = a;
            }

            string strSql = "select top " + topSql.ToString() + " * from (select title,nick,pic_url,price,click_url,shop_click_url from taobaoKe where isBad>0 and commission>0 and isGood>0) as newGood order by newid()";
            DataTable dt = db.getDataTable(strSql);
            if (dt.Rows.Count < 1)
            {
                strSql = "select top " + topSql.ToString() + " * from (select top 100 title,nick,pic_url,price,click_url,shop_click_url from taobaoKe where isBad>0 and commission>0 order by volume DESC) as news order by newid()";
                dt = db.getDataTable(strSql);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string pic = dt.Rows[i]["pic_url"].ToString().Trim() + "_310x310.jpg";
                kinSlideshow.Text += "<a href=\"" + dt.Rows[i]["click_url"].ToString().Trim() + "\" target=\"_blank\"><img src=\"" + pic + "\" style=\"height:" + adSizeStr + "px; width:" + adSizeStr + "px;\" alt=\"" + dt.Rows[i]["title"].ToString().Trim() + "\" title=\"" + dt.Rows[i]["title"].ToString().Trim() + "\" /></a>";
            }
            dt.Dispose();
        }
    }
}