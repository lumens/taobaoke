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

public partial class huabao_list : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();
    xabaraAD ad = new xabaraAD();
    XabaraClass xClass = new XabaraClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        string classNav = "淘画报 ";

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

            long cidTemp = xc.SafeNum(Request.QueryString["id"]);
            if (cidTemp < 1)
            {
                string urlID = Page.RouteData.Values["cID"].ToString();
                urlID = urlID.Replace(".htm", "");
                cidTemp = xc.SafeNum(urlID);
            }
            //Response.Write(cidTemp.ToString());            
            listTop.Text = "<a href=\"" + cidTemp.ToString() + ".htm\" target=\"_top\">默认</a>";

            string ifStr = string.Empty;
            string pageStr = string.Empty;
            string searchStr = Server.UrlDecode(Request["keyWord"]);
            string searchIf = string.Empty;
            string orderBy = "order by modified_date DESC,hits DESC";

            long sale = xc.SafeNum(Request.QueryString["hit"]);
            if (sale > 0)
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/" + cidTemp.ToString() + ".htm?hit=1\" style=\"color:#cb0201; font-weight:bold;\" target=\"_top\">热门</a>";
                orderBy = " Order By hits DESC";
                pageStr += "&hit=1";
            }
            else
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/" + cidTemp.ToString() + ".htm?hit=1\" target=\"_top\">热门</a>";
            }

            long good = xc.SafeNum(Request.QueryString["good"]);
            if (good > 0)
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/" + cidTemp.ToString() + ".htm?good=1\" style=\"color:#cb0201; font-weight:bold;\" target=\"_top\">推荐</a>";
                orderBy = " Order By weight DESC";
                pageStr += "&good=1";
            }
            else
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"/huabao/" + cidTemp.ToString() + ".htm?good=1\" target=\"_top\">推荐</a>";
            }

            //淘品牌分类
            dt = db.getDataTable("select * from posterChannelIDs where isFlag>0 order by id ASC");
            for (int l = 0; l < dt.Rows.Count; l++)
            {
                string css = string.Empty;
                long tidTree = xc.SafeNum(dt.Rows[l]["id"].ToString().Trim());
                if (tidTree == cidTemp)
                {
                    css = " style=\"color:#cb0201; font-weight:bold;\"";
                    classNav += dt.Rows[l]["channel_name"].ToString().Trim();
                }
                navList.Text += "<a href=\"/huabao/" + tidTree.ToString() + ".htm\" target=\"_top\" " + css + ">" + dt.Rows[l]["channel_name"].ToString().Trim() + "</a>&nbsp;&nbsp;";
            }
            dt.Dispose();

            navClass.Text = "图搜画报";

            if (!string.IsNullOrEmpty(searchStr as string))
            {
                searchIf = " and title like '%" + searchStr + "%'";

                searchWord.Text = searchStr;
                pageStr = "&keyWord=" + Server.UrlEncode(searchStr);

                classNav = "搜索结果";
                navClass.Text = classNav;
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
            headMenu.Text += "<td valign=\"middle\" class=\"headBg2\"><a href=\"/huabao/default.htm\" target=\"_top\" class=\"head\">图搜画报</a></td><td valign=\"middle\" class=\"headBg1\"><a href=\"/huabao/\" target=\"_top\" class=\"head\">画报淘宝</a></td>";
            dt.Dispose();

            if (cidTemp > 0)
            {
                ifStr = " and channel_id=" + cidTemp.ToString();
            }

            string strSql = "select top 3000 * from posterChannelTitle where flagID=1 " + ifStr + searchIf + orderBy;
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
        }

        this.Page.Title = classNav + " " + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName") + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "titleMeta");
    }

    protected void searchImgButton_Click(object sender, ImageClickEventArgs e)
    {
        long cidTemp = xc.SafeNum(Request.QueryString["id"]);
        if (cidTemp < 1)
        {
            string urlID = Page.RouteData.Values["cID"].ToString();
            urlID = urlID.Replace(".htm", "");
            cidTemp = xc.SafeNum(urlID);
        }

        Response.Redirect("/huabao/" + cidTemp.ToString() + ".htm?keyWord=" + Server.UrlEncode(searchWord.Text), true);
    }
}