using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;

public partial class inClude_bottom : System.Web.UI.UserControl
{
    public string appKey = "1234";

    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        dbDataFunction db = new dbDataFunction();

        if (!IsPostBack)
        {
            //导航及产品
            bottomNav.Text = "<a href=\"/\" target=\"_top\">网站首页</a>&nbsp;<span class=\"headLine\">|</span>&nbsp;<a href=\"http://s.click.taobao.com/t_9?p=mm_14288563_0_0&l=http%3A%2F%2Ftemai.tmall.com\" target=\"_blank\">品牌特卖</a>";
            DataTable dt = db.getDataTable("select Tid,TreeName from newsTree where TreeID=0 and TreeType='taobaoke' order by ListID ASC,Tid ASC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tid = dt.Rows[i]["Tid"].ToString().Trim();
                string treeName = dt.Rows[i]["TreeName"].ToString().Trim();

                bottomNav.Text += "&nbsp;<span class=\"headLine\">|</span>&nbsp;<a href=\"/" + tid + ".htm\" target=\"_top\">" + treeName + "</a>";
            }
            dt.Dispose();

            miibeian.Text = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "miibeian");
            webTitle.Text = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName");
            webVer.Text = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webVer");            

            yearLab.Text = DateTime.Now.Year.ToString();

            countStr.Text = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "countScript");

            string qqID = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "qq");
            qqLink.Text = xc.QQHTM(qqID, "41");

            string tbID = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobao");
            tbLink.Text = xc.taobaoHTM(tbID, "1");            

            string tel = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "freePhone");
            if (!string.IsNullOrEmpty(tel as string))
            {
                telInfo.Text = tel + "&nbsp;";
            }
            tel = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "mobile");
            if (!string.IsNullOrEmpty(tel as string))
            {
                telInfo.Text += tel + "&nbsp;";
            }
            tel = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webTel");
            if (!string.IsNullOrEmpty(tel as string))
            {
                telInfo.Text += tel + "&nbsp;";
            }

            appKey = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppKey");
        }
    }
}