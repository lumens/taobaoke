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

public partial class item_Default : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();
    xabaraAD ad = new xabaraAD();
    XabaraClass xClass = new XabaraClass();

    protected void Page_Load(object sender, EventArgs e)
    {        
        string classNav = "找店铺_ZDianPU.com";

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
            string searchStr = xc.SafeSql(Server.UrlDecode(Request["keyWord"]));
            string searchIf = string.Empty;
            string orderBy = " Order By isGood desc,volume DESC";
            long sale = xc.SafeNum(Request.QueryString["sale"]);
            if (sale > 0)
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"" + cidTemp.ToString() + ".htm?sale=1\" style=\"color:#cb0201; font-weight:bold;\" target=\"_top\">销售</a>";
                orderBy = " Order By volume DESC";
                pageStr += "&sale=1";
            }
            else
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"" + cidTemp.ToString() + ".htm?sale=1\" target=\"_top\">销售</a>";
            }
            long price = xc.SafeNum(Request.QueryString["price"]);
            if (price > 0)
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"" + cidTemp.ToString() + ".htm?price=1\" style=\"color:#cb0201; font-weight:bold;\" target=\"_top\">价格</a>";
                orderBy = " Order By price ASC";
                pageStr += "&price=1";
            }
            else
            {
                listTop.Text += "&nbsp;&nbsp;<a href=\"" + cidTemp.ToString() + ".htm?price=1\" target=\"_top\">价格</a>";
            }

            navClass.Text = xClass.getClass(cidTemp, "*.htm", "");

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

                searchWord.Text = HttpUtility.HtmlEncode(searchStr);
                pageStr = "&keyWord=" + Server.UrlEncode(searchStr);

                classNav = "搜索结果";
                navClass.Text = classNav;
            }

            dt = db.getDataTable("select Tid,TreeID,treeNameTxt,idLayerStr from newsTree where Tid=" + cidTemp.ToString());
            if (dt.Rows.Count > 0)
            {
                string tID = dt.Rows[0]["Tid"].ToString().Trim();
                string treeID = dt.Rows[0]["TreeID"].ToString().Trim();
                classNav = dt.Rows[0]["treeNameTxt"].ToString().Trim();

                //读取二级分类
                string id = dt.Rows[0]["idLayerStr"].ToString().Trim();
                if (!string.IsNullOrEmpty(id as string))
                {
                    string[] bigID = id.Split(new char[] { '|' });
                    id = bigID[1];
                }
                else
                {
                    id = cidTemp.ToString();
                }
                dt = db.getDataTable("select Tid,TreeName from NewsTree where TreeID=" + id + " order by ListID ASC,Tid ASC");
                for (int l = 0; l < dt.Rows.Count; l++)
                {
                    string css = string.Empty;
                    long tidTree = xc.SafeNum(dt.Rows[l]["Tid"].ToString().Trim());
                    if (tidTree == cidTemp || classNav.IndexOf(dt.Rows[l]["TreeName"].ToString().Trim() + "&nbsp;") > -1)
                    {
                        css = " style=\"color:#cb0201; font-weight:bold;\"";
                    }
                    navList.Text += "<a href=\"" + tidTree.ToString() + ".htm\" target=\"_top\" " + css + ">" + dt.Rows[l]["TreeName"].ToString().Trim() + "</a>&nbsp;&nbsp;";
                }
                dt.Dispose();

                //读取品牌
                ppClass.Text = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:960px;\">";                
                dt = db.getDataTable("select Tid,TreeName from newsTree where taobaoKe<>'' and TreeID=" + treeID + " order by ListID ASC,Tid ASC");
                if (dt.Rows.Count > 0)
                {
                    for (int p = 0; p < dt.Rows.Count; p++)
                    {
                        if ((p + 1) % 6 == 1)
                        {
                            ppClass.Text += "<tr>";
                        }
                        string css = string.Empty;
                        long tidTree = xc.SafeNum(dt.Rows[p]["Tid"].ToString().Trim());
                        if (tidTree == cidTemp)
                        {
                            css = " style=\"color:#cb0201; font-weight:bold;\"";
                        }
                        ppClass.Text += "<td style=\"height:22px; line-height:22px; text-align:left; width:160px;\"><a href=\"" + tidTree.ToString() + ".htm\" target=\"_top\" " + css + ">" + dt.Rows[p]["TreeName"].ToString().Trim() + "</a></td>";
                        if ((p + 1) % 6 == 0)
                        {
                            ppClass.Text += "</tr>";
                        }
                    }
                }
                else
                {
                    dt = db.getDataTable("select Tid,TreeName from newsTree where taobaoKe<>'' and idLayerStr like '%|" + tID + "|%' order by ListID ASC,Tid ASC");
                    if (dt.Rows.Count > 0)
                    {
                        for (int p = 0; p < dt.Rows.Count; p++)
                        {
                            if ((p + 1) % 6 == 1)
                            {
                                ppClass.Text += "<tr>";
                            }
                            string css = string.Empty;
                            long tidTree = xc.SafeNum(dt.Rows[p]["Tid"].ToString().Trim());
                            if (tidTree == cidTemp)
                            {
                                css = " style=\"color:#cb0201; font-weight:bold;\"";
                            }
                            ppClass.Text += "<td style=\"height:22px; line-height:22px; text-align:left; width:160px;\"><a href=\"" + tidTree.ToString() + ".htm\" target=\"_top\" " + css + ">" + dt.Rows[p]["TreeName"].ToString().Trim() + "</a></td>";
                            if ((p + 1) % 6 == 0)
                            {
                                ppClass.Text += "</tr>";
                            }
                        }
                    }
                }
            }
            dt.Dispose();
            ppClass.Text += "</table>";

            //导航
            headMenu.Text = "<td valign=\"middle\" class=\"headBg1\"><a href=\"/\" target=\"_top\" class=\"head\">网站首页</a></td>";
            dt = db.getDataTable("select Tid,TreeName from newsTree where TreeID=0 and TreeType='taobaoke' order by ListID ASC,Tid ASC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tid = dt.Rows[i]["Tid"].ToString().Trim();
                string treeName = dt.Rows[i]["TreeName"].ToString().Trim();
                string className = "headBg1";
                if (xc.SafeNum(tid) == cidTemp || classNav.IndexOf(treeName + "&nbsp;") > -1)
                {
                    className = "headBg2";
                }

                headMenu.Text += "<td valign=\"middle\" class=\"" + className + "\"><a href=\"/" + tid + ".htm\" target=\"_top\" class=\"head\">" + treeName + "</a></td>";
            }
            headMenu.Text += "<td valign=\"middle\" class=\"headBg1\"><a href=\"/huabao/default.htm\" target=\"_top\" class=\"head\">图搜画报</a></td><td valign=\"middle\" class=\"headBg1\"><a href=\"/huabao/\" target=\"_top\" class=\"head\">画报淘宝</a></td>";
            dt.Dispose();

            if (cidTemp > 0)
            {
                string cidStr = xClass.getClassAllID("taobaoke", cidTemp); //得到所有ID
                
                if (cidStr.IndexOf(",") > 0)
                {
                    string[] ids = cidStr.Split(new char[] { ',' });
                    for (int i = 0; i <= ids.GetUpperBound(0); i++)
                    {
                        ifStr += " select * from taobaoke where classID=" + ids[i].ToString().Trim() + searchIf + " union ";
                    }
                }
                else
                {
                    ifStr = " select * from taobaoke where classID=" + cidTemp.ToString().Trim() + searchIf + " union ";
                }
            }
            else
            {
                ifStr = " select * from taobaoke where isBad=1 " + searchIf + " union ";
            }

            ifStr = "(" + ifStr.Substring(0, ifStr.Length - 6) + ") as news";

            string strSql = "select top 3000 * from " + ifStr + orderBy;
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

        Response.Redirect("/" + cidTemp.ToString() + ".htm?keyWord=" + Server.UrlEncode(searchWord.Text), true);
    }

    public string money(string str)
    {
        return xc.getMoney(str);
    }
}