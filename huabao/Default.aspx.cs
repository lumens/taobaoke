using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Top.Api;
using Top.Api.Domain;
using Top.Api.Parser;
using Top.Api.Request;
using Top.Api.Response;

public partial class huabao_Default : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();
    xabaraAD ad = new xabaraAD();
    XabaraClass xClass = new XabaraClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        string classNav = "图搜画报 - 画报淘宝";

        if (!IsPostBack)
        {
            DataTable dt;

            dt = db.getDataTable("select top 5 Tid,TreeName from NewsTree where taobaoKe<>'' order by newid() ");
            for (int i = 0; i < 5; i++)
            {
                ppRnd.Text += "<a href=\"/" + dt.Rows[i]["Tid"].ToString().Trim() + ".htm\" target=\"_blank\" title=\"查看 " + dt.Rows[i]["TreeName"].ToString().Trim() + "\" alt=\"查看 " + dt.Rows[i]["TreeName"].ToString().Trim() + "\">" + dt.Rows[i]["TreeName"].ToString().Trim() + "</a>";
                if (i < 4)
                {
                    ppRnd.Text += "&nbsp;<span class=\"headLine\">|</span>&nbsp;";
                }
            }
            dt.Dispose();
            ad468.Text = ad.getAdCode(0, 468, 60, 1, 0, true, string.Empty);
        
            listTop.Text = "<a href=\"/huabao/\" target=\"_top\">默认</a>";

            string pageStr = string.Empty;
            string searchStr = Server.UrlDecode(Request.QueryString["keyWord"]);
            string searchIf = string.Empty;
            string orderBy = " and volume > 100 Order By commission DESC,isGood desc";
            long sale = xc.SafeNum(Request.QueryString["sale"]);
            if (sale > 0)
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/?sale=1\" style=\"color:#cb0201; font-weight:bold;\" target=\"_top\">销售</a>";
                orderBy = " Order By volume DESC";
                pageStr += "&sale=1";
            }
            else
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/?sale=1\" target=\"_top\">销售</a>";
            }
            long price = xc.SafeNum(Request.QueryString["price"]);
            if (price > 0)
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/?price=1\" style=\"color:#cb0201; font-weight:bold;\" target=\"_top\">价格</a>";
                orderBy = " Order By price ASC";
                pageStr += "&price=1";
            }
            else
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/?price=1\" target=\"_top\">价格</a>";
            }           

            //导航
            headMenu.Text = "<td valign=\"middle\" class=\"headBg1\"><a href=\"/\" target=\"_top\" class=\"head\">网站首页</a></td>";
            dt = db.getDataTable("select Tid,TreeName from newsTree where TreeID=0 and TreeType='taobaoke' order by ListID ASC,Tid ASC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tid = dt.Rows[i]["Tid"].ToString().Trim();
                string treeName = dt.Rows[i]["TreeName"].ToString().Trim();
                string className = "headBg1";

                headMenu.Text += "<td valign=\"middle\" class=\"" + className + "\"><a href=\"/" + tid + ".htm\" target=\"_top\" class=\"head\">" + treeName + "</a></td>";
            }
            headMenu.Text += "<td valign=\"middle\" class=\"headBg1\"><a href=\"/huabao/default.htm\" target=\"_top\" class=\"head\">图搜画报</a></td><td valign=\"middle\" class=\"headBg2\"><a href=\"/huabao/\" target=\"_top\" class=\"head\">画报淘宝</a></td>";
            dt.Dispose();

            navClass.Text = "图搜画报 - 画报淘宝";
            if (!string.IsNullOrEmpty(searchStr as string))
            {
                if (xc.SafeNum(searchStr) > 0)
                {
                    searchIf = " and num_iid=" + searchStr;
                }
                else
                {
                    searchIf = " and title like '%" + searchStr + "%'";
                }

                searchWord.Text = searchStr;
                pageStr = "&keyWord=" + Server.UrlEncode(searchStr);

                classNav = "搜索结果";
                navClass.Text = classNav;
            }

            string strSql = "select top 3000 * from taobaoke where commission>5 " + searchIf + orderBy;
            //Response.Write(strSql);
            //Response.End();
            lblCurrentPage.Text = db.RepeaterDB(RepeaterList, strSql, pageStr, 30, "page", "right", true);
            string[] inputStr = lblCurrentPage.Text.Replace("<!-- input -->", "@").Split(new char[] { '@' });
            pageTop.Text = inputStr[0] + "</tr></table>";
            if (this.RepeaterList.Items.Count < 1)
            {
                if (string.IsNullOrEmpty(searchStr as string))
                {
                    errInfo.Text = "目前暂没有任何信息！";
                    errDiv.Visible = true;
                }
                else
                {
                    //即时搜索淘宝客显示商品
                    string appkey = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppKey");
                    string appsecret = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppSecret");
                    string url = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeUrl");
                    string alimamaID = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAlimamaID");    //淘宝客推广ID
                    ITopClient client = new DefaultTopClient(url, appkey, appsecret);
                    TaobaokeListurlGetRequest req = new TaobaokeListurlGetRequest();
                    req.Q = searchStr;
                    req.Nick = alimamaID;
                    TaobaokeListurlGetResponse response = client.Execute(req);

                    Response.Redirect(response.TaobaokeItem.KeywordClickUrl, true);
                }
            }
            else
            {
                errDiv.Visible = false;
            }

            //热搜关键词
            string hot = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "hotSearch");
            string[] hotWord = hot.Split(new char[] { ',' });
            for (int h = 0; h <= hotWord.GetUpperBound(0); h++)
            {
                hotSearch.Text += "<a href=\"/search.htm?keyWord=" + Server.UrlEncode(hotWord[h]) + "\" target=\"_top\" title=\"搜索 " + hotWord[h] + "\">" + hotWord[h] + "</a>&nbsp;";
            }

            xc.webMeta(this.Page, XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "KeyWord"), XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "metaStr"));

            this.Page.Title = classNav + " " + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName") + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "titleMeta");
        }
    }

    protected void searchImgButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/huabao/?keyWord=" + Server.UrlEncode(searchWord.Text), true);
    }

    public string money(string str)
    {
        return xc.getMoney(str);
    }
}