using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class huabao_show : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();
    xabaraAD ad = new xabaraAD();

    protected void Page_Load(object sender, EventArgs e)
    {
        string classNav = "淘画报 ";
        string title = classNav;
        string tag = classNav;

        if (!IsPostBack)
        {
            DataTable dt;
            long cidTemp = 0;

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

            long idTemp = xc.SafeNum(Request["id"]);
            if (idTemp < 1)
            {
                string urlID = Page.RouteData.Values["sID"].ToString();
                urlID = urlID.Replace(".htm", "");
                idTemp = xc.SafeNum(urlID);
            }

            dt = db.getDataTable("select channel_id,title,tag from posterChannelTitle where id=" + idTemp.ToString());
            if (dt.Rows.Count > 0)
            {
                cidTemp = xc.SafeNum(dt.Rows[0]["channel_id"].ToString().Trim());
                title = dt.Rows[0]["title"].ToString().Trim();
                titleName.Text = title;
                tag = dt.Rows[0]["tag"].ToString().Trim();
            }
            dt.Dispose();

            string strSql = "select * from taobaoKe left join posterTkID on taobaoKe.num_iid=posterTkID.tK_num_iid where taobaoKe.isBad>0 and posterTkID.title_id=" + idTemp.ToString() + " order by commission DESC,volume DESC";
            lblCurrentPage.Text = db.RepeaterDB(RepeaterList, strSql, "", 30, "page", "right", true);
            string[] inputStr = lblCurrentPage.Text.Replace("<!-- input -->", "@").Split(new char[] { '@' });
            pageTop.Text = inputStr[0] + "</tr></table>";            

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

            navClass.Text = "淘画报";

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

            //热搜关键词
            string hot = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "hotSearch");
            string[] hotWord = hot.Split(new char[] { ',' });
            for (int h = 0; h <= hotWord.GetUpperBound(0); h++)
            {
                hotSearch.Text += "<a href=\"/search.htm?keyWord=" + Server.UrlEncode(hotWord[h]) + "\" target=\"_top\" title=\"搜索 " + hotWord[h] + "\">" + hotWord[h] + "</a>&nbsp;";
            }

            xc.webMeta(this.Page, tag, title);
        }

        this.Page.Title = title + " " + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webName") + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "titleMeta");
    }

    protected void searchImgButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/huabao/default.htm?keyWord=" + Server.UrlEncode(searchWord.Text), true);
    }

    public string money(string str)
    {
        return xc.getMoney(str);
    }
}