using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        dbDataFunction db = new dbDataFunction();
        xabaraAD ad = new xabaraAD();
        XabaraClass xClass = new XabaraClass();
        webPublic wp = new webPublic();

        this.Page.Title = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName") + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "titleMeta");

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
            long adID = 116;

            vipCode.Text = "<div class=\"divWidth\"><a href=\"/278.htm\" target=\"_blank\"><img src=\"/ad/images/okitch_20120630.gif\" /></a></div><div class=\"height5px\"></div>";

            //淘画报          
            dt = db.getDataTable("select top 5 id,title,title_short,cover_pic_url_h,cover_pic_url_w from posterChannelTitle where flagID>0 and weight>10000 and modified_date>'" + DateTime.Now.AddDays(-7) + "' order by newid()");
            if (dt.Rows.Count > 4)
            {
                HuaBaoLeft.Text = "<table cellspacing=\"0\" cellpadding=\"0\" style=\"border-width:0px;\"><tr>";
                string txtLink = string.Empty;
                for (int i = 0; i < 3; i++)
                {
                    HuaBaoLeft.Text += "<td style=\"text-align:center; width:190px;\"><a href=\"/huabao/show/" + dt.Rows[i]["id"].ToString().Trim() + ".htm\" target=\"_blank\"><img src=\"" + dt.Rows[i]["cover_pic_url_h"].ToString().Trim() + "\" class=\"picHuaBaoH\" alt=\"" + dt.Rows[i]["title"].ToString().Trim() + "\" title=\"" + dt.Rows[i]["title_short"].ToString().Trim() + "\" id=\"imghbh\" /></a></td>";

                    txtLink += "<td style=\"text-align:center; height:28px; line-height:28px;\"><a href=\"/huabao/show/" + dt.Rows[i]["id"].ToString().Trim() + ".htm\" target=\"_blank\" class=\"huabao\">" + dt.Rows[i]["title_short"].ToString().Trim() + "</a></td>";
                }
                HuaBaoLeft.Text += "</tr><tr>" + txtLink + "</tr></table>";

                HuaBaoRight.Text = "<table cellspacing=\"0\" cellpadding=\"0\" style=\"border-width:0px;\"><tr>";
                txtLink = string.Empty;
                for (int i = 3; i < 5; i++)
                {
                    HuaBaoRight.Text += "<td style=\"text-align:center; width:190px;\"><a href=\"/huabao/show/" + dt.Rows[i]["id"].ToString().Trim() + ".htm\" target=\"_blank\"><img src=\"" + dt.Rows[i]["cover_pic_url_w"].ToString().Trim() + "\" class=\"picHuaBaoW\" alt=\"" + dt.Rows[i]["title"].ToString().Trim() + "\" title=\"" + dt.Rows[i]["title_short"].ToString().Trim() + "\" id=\"imghbw\" /></a></td>";

                    txtLink += "<td style=\"text-align:center; height:32px; line-height:32px;\"><a href=\"/huabao/show/" + dt.Rows[i]["id"].ToString().Trim() + ".htm\" target=\"_blank\" class=\"huabao\">" + dt.Rows[i]["title_short"].ToString().Trim() + "</a></td>";
                }
                HuaBaoRight.Text += "</tr><tr>" + txtLink + "</tr><tr><td colspan=\"2\" style=\"padding-top:5px;\"><table align=\"center\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-width:0px;width:98%;\"><tr><td style=\"text-align:left;\">" + ad.getAdCode(335, 0, 0, 3, 1, true, "") + "</td><td style=\"text-align:right;\">" + ad.getAdCode(334, 0, 0, 1, 0, true, "") + "</td></tr></table></td></tr></table>";
            }
            else
            {
                huabao.Visible = false;
            }
            dt.Dispose();

            //导航及产品
            headMenu.Text = "<td valign=\"middle\" class=\"headBg2\"><a href=\"/\" target=\"_top\" class=\"head\">网站首页</a></td>";
            dt = db.getDataTable("select Tid,TreeName from newsTree where TreeID=0 and TreeType='taobaoke' order by ListID ASC,Tid ASC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tid = dt.Rows[i]["Tid"].ToString().Trim();
                string treeName = dt.Rows[i]["TreeName"].ToString().Trim();

                //广告代码
                adID += 1;
                string adCodeWidth = ad.getAdCode(adID, 0, 0, 1, 1, true, "");
                int shopNums = 12;   //每个版块读取商品数
                if (!string.IsNullOrEmpty(adCodeWidth as string))
                {
                    adCodeWidth = "<div class=\"divWidth\">" + adCodeWidth + "</div>";                    
                }
                string adCodeHeight = ad.getAdCode(adID + 8, 0, 0, 1, 1, true, "");
                if (string.IsNullOrEmpty(adCodeHeight as string))
                {
                    DataTable ppDt = db.getDataTable("select top 22 Tid,TreeName from NewsTree where taobaoKe<>'' and idLayerStr like '%|" + tid + "|%' order by listID ASC,Tid ASC ");
                    if (ppDt.Rows.Count > 0)
                    {
                        adCodeHeight = "<table cellspacing=\"0\" cellpadding=\"0\" style=\"width:100%;\" align=\"center\" class=\"indexTable\">";
                        for (int p = 0; p < ppDt.Rows.Count; p++)
                        {
                            adCodeHeight += "<tr><td><a href=\"/" + ppDt.Rows[p]["Tid"].ToString().Trim() + ".htm\" target=\"_blank\" title=\"查看 " + ppDt.Rows[p]["TreeName"].ToString().Trim() + "\" alt=\"查看 " + ppDt.Rows[p]["TreeName"].ToString().Trim() + "\" class=\"classID\">" + ppDt.Rows[p]["TreeName"].ToString().Trim() + "</a></td></tr>";
                        }
                        adCodeHeight += "<tr><td style=\"text-align:right;\"><a href=\"/" + tid + ".htm\" target=\"_blank\" title=\"更多 " + treeName + " 品牌\" alt=\"更多 " + treeName + " 品牌\" class=\"classID\">>> 更多品牌</a></td></tr></table>";
                    }
                    ppDt.Dispose();
                }
                else
                {
                    shopNums = 12;
                }

                //商品读取
                string shopHtm = string.Empty;   
                string ifStr = string.Empty;
                string cidStr = xClass.getClassAllID("taobaoke", xc.SafeNum(tid)); //得到所有ID
                if (cidStr.IndexOf(",") > 0)
                {
                    string[] ids = cidStr.Split(new char[] { ',' });
                    int iiCount = ids.GetUpperBound(0);
                    int readTop = 1;
                    if (shopNums > iiCount-5)
                    {
                        readTop = 2;
                    }

                    for (int ii = 0; ii <= iiCount; ii++)
                    {
                        ifStr += " select top " + readTop.ToString() + " num_iid,title,pic_url,click_url,commission,price,volume from taobaoke where isBad>0 and isGood>0 and classID=" + ids[ii].ToString().Trim() + " order by volume DESC union ";
                    }
                }
                else
                {
                    ifStr = " select num_iid,title,pic_url,click_url,commission,price,volume from taobaoke where isBad>0 and isGood>0 and classID=" + tid + " union ";
                }
                ifStr = "(" + ifStr.Substring(0, ifStr.Length - 6) + ") as news";

                string strSql = "select top " + shopNums.ToString() + " * from " + ifStr + " Order By newid()";
                DataTable dtShop = db.getDataTable(strSql);
                //Response.Write(strSql);
                //Response.End();
                shopHtm = string.Empty;
                for (int s = 0; s < dtShop.Rows.Count; s++)
                {
                    shopHtm += "<td valign=\"top\" style=\"border-style: none solid solid none; border-width: 1px; border-color: #323232; text-align:center; width:200px;\"><a class=\"moneyLink\" href=\"/show/" + dtShop.Rows[s]["num_iid"].ToString().Trim() + ".htm\" target=\"_blank\" alt=\"" + dtShop.Rows[s]["title"].ToString().Trim() + "\" title=\"" + dtShop.Rows[s]["title"].ToString().Trim() + "\"><div style=\"position:absolute;\"><ul><li style=\"height:168px;\"></li><li class=\"moneyFont\" style=\"background-color:#323232; filter:Alpha(opacity=30);opacity:0.3; text-align:right; padding:5px; width:190px; color:#b8b8b8;\">" + xc.getMoney(dtShop.Rows[s]["price"].ToString().Trim()) + "</li></ul></div><img id=\"imgshow\" src=\"" + dtShop.Rows[s]["pic_url"].ToString().Trim() + "_310x310.jpg\" style=\"height:200px; width:200px;\" alt=\"" + dtShop.Rows[s]["title"].ToString().Trim() + "\" title=\"" + dtShop.Rows[s]["title"].ToString().Trim() + "\" /></a></td>";

                    if (s == 3 || s == 7)
                    {
                        shopHtm += "</tr><tr>";
                    }
                }
                dtShop.Dispose();
                
                headMenu.Text += "<td valign=\"middle\" class=\"headBg1\"><a href=\"/" + tid + ".htm\" target=\"_top\" class=\"head\">" + treeName + "</a></td>";
                //产品二级分类     
                string class2 = "<span class=\"headLine\">|</span>&nbsp;";
                DataTable dtClass = db.getDataTable("select Tid,TreeName from newsTree where TreeID=" + tid + " and TreeType='taobaoke' order by ListID ASC,Tid ASC");
                for (int c = 0; c < dtClass.Rows.Count; c++)
                {
                    string classID = dtClass.Rows[c]["Tid"].ToString().Trim();
                    string className = dtClass.Rows[c]["TreeName"].ToString().Trim();
                    class2 += "<a href=\"/" + dtClass.Rows[c]["Tid"].ToString().Trim() + ".htm\" target=\"_blank\" title=\"查看 " + className + "\" alt=\"查看 " + className + "\">" + className + "</a>&nbsp;<span class=\"headLine\">|</span>&nbsp;";          
                }
                dtClass.Dispose();

                shopList.Text += adCodeWidth;
                shopList.Text += "<div class=\"height5px\"></div>";
                shopList.Text += "<div class=\"divWidth\"><table cellspacing=\"0\" cellpadding=\"0\" style=\"border-width:0px; border-collapse:collapse; height:26px; width:100%;\"><tr><td style=\"width:125px; text-align:left;\"><img src=\"images/class" + tid + ".gif\" alt=\"" + treeName + "\" title=\"" + treeName + "\" /></td><td style=\"text-align:right; vertical-align:bottom; width:807px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;\">" + class2 + "</td><td style=\"text-align:right; vertical-align:bottom; width:38px;\"><a href=\"/" + tid + ".htm\" target=\"_top\"><img src=\"images/more.gif\" alt=\"更多 " + treeName + "\" title=\"更多 " + treeName + "\" /></a></td></tr></table></div>";
                shopList.Text += "<div class=\"divWidth\" style=\"height:2px; background-color:Black; font-size:0px;\"></div>";
                shopList.Text += "<div class=\"divWidth\"><table cellspacing=\"0\" cellpadding=\"0\"><tr><td valign=\"top\" style=\"background-color:#323232; width:166px; text-align:center;\" rowspan=\"3\">" + adCodeHeight + "</td>" + shopHtm + "</tr></table></div>";
            }
            headMenu.Text += "<td valign=\"middle\" class=\"headBg1\"><a href=\"/huabao/default.htm\" target=\"_top\" class=\"head\">图搜画报</a></td><td valign=\"middle\" class=\"headBg1\"><a href=\"/huabao/\" target=\"_top\" class=\"head\">画报淘宝</a></td>";
            dt.Dispose();

            //热搜关键词
            string hot = XmlReader.GetConfig(HttpContext.Current.Server.MapPath("~/xabara.config"), "hotSearch");
            string[] hotWord = hot.Split(new char[] { ',' });
            for (int h = 0; h <= hotWord.GetUpperBound(0); h++)
            {
                hotSearch.Text += "<a href=\"/search.htm?keyWord=" + Server.UrlEncode(hotWord[h]) + "\" target=\"_top\" title=\"搜索 " + hotWord[h] + "\">" + hotWord[h] + "</a>&nbsp;";
            }

            this.Page.MetaKeywords = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "KeyWord");
            this.Page.MetaDescription = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "metaStr");
        }
    }

    protected void searchImgButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/search.htm?keyWord=" + Server.UrlEncode(searchWord.Text), true);
    }
}