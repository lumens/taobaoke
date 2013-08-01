using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class webXabara_frame_sitemap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (id.Equals("baidu"))
        {
            id = "";
        }

        writeMap("http://" + Request.Url.Host, Server.MapPath("/sitemap" + id + ".xml"));
    }

    public static void writeMap(string _fullFileName, string mapPath)
    {
        dbDataFunction db = new dbDataFunction();
        XabaraClass xClass = new XabaraClass();
        DataTable dt;

        string strSql = string.Empty;
        string cidStr = string.Empty;
        string ifStr = string.Empty;

        string id = HttpContext.Current.Request.QueryString["id"];
        string fullFileName = _fullFileName;
        string strLoc = string.Empty;
        string siteMapPath = mapPath;

        FileInfo XMLFile = null;
        StreamWriter writerXMLFile = null;
        XMLFile = new FileInfo(siteMapPath);
        writerXMLFile = XMLFile.CreateText();    //添加sitMap的头 
        writerXMLFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        if (id.Equals("google")) //google　加一句
        {
            writerXMLFile.WriteLine("<urlset xmlns=\"http://www.google.com/schemas/sitemap/0.9\">");
        }
        else
        {
            writerXMLFile.WriteLine("<urlset>");
        }

        //频道各首页，每站固定格式
        addXMLSitMap(writerXMLFile, fullFileName, 1.0);
        addXMLSitMap(writerXMLFile, fullFileName + "/default.htm", 0.8);
        addXMLSitMap(writerXMLFile, fullFileName + "/huabao/default.htm", 0.8);

        //所有分类读取
        dt = db.getDataTable("select Tid from NewsTree where TreeType='taobaoke' order by ListID ASC,Tid ASC");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strLoc = fullFileName + "/" + dt.Rows[i]["Tid"].ToString().Trim() + ".htm";
            addXMLSitMap(writerXMLFile, strLoc, 0.8);
        }
        dt.Dispose();

        //商品读取
        dt = db.getDataTable("select top 40000 num_iid from taobaoKe where isBad>0 order by isGood DESC,id ASC");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strLoc = fullFileName + "/show/" + dt.Rows[i]["num_iid"].ToString().Trim() + ".htm";
            addXMLSitMap(writerXMLFile, strLoc, 0.8);
        }
        dt.Dispose();

        //画报读取
        dt = db.getDataTable("select top 5000 id from posterChannelTitle where flagID>0 order by weight DESC");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strLoc = fullFileName + "/huabao/show/" + dt.Rows[i]["id"].ToString().Trim() + ".htm";
            addXMLSitMap(writerXMLFile, strLoc, 0.8);
        }
        dt.Dispose();

        //添加sitMap的尾,千万不能忘了这句
        writerXMLFile.WriteLine("</urlset>");
        writerXMLFile.Close();
        writerXMLFile.Dispose();

        HttpContext.Current.Response.Write("<div style=\"text-align:left;padding:50px;font-size:14px;\"><strong>" + id + " sitemap 生成成功！请分别向以下免费搜索引擎提交：</strong><br /><br />向 ask.com 提交：<a href=\"http://submissions.ask.com/ping?sitemap=http://" + HttpContext.Current.Request.Url.Host + "/sitemap.xml\" target=\"_blank\" style=\"color:red;font-size:14px;\"><strong>点击向 ask.com 提交</strong></a><br /><br />向 bing 提交：<a href=\"http://cn.bing.com/webmaster/ping.aspx?sitemap=http://" + HttpContext.Current.Request.Url.Host + "/sitemap.xml\" target=\"_blank\" style=\"color:red;font-size:14px;\"><strong>点击向 bing 提交</strong></a><br /><br />向 Google 提交：<a href=\"http://www.google.com/webmasters/\" target=\"_blank\" style=\"color:red;font-size:14px;\"><strong>点击向 google 提交</strong></a>【需要申请google帐号登录后操作】</div>");
    }
    /// <summary>
    /// 生成站点地图中间部分 
    /// </summary> 
    /// <param name="writerFile"></param>
    /// <param name="strLoc"></param>
    /// <param name="priority"></param> 
    private static void addXMLSitMap(StreamWriter writerFile, string strLoc, double priority)
    {
        writerFile.WriteLine(" <url>");
        writerFile.WriteLine("  <loc>" + strLoc + "</loc>");
        writerFile.WriteLine("  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod>");
        writerFile.WriteLine("  <changefreq>daily</changefreq>");
        writerFile.WriteLine("  <priority>" + priority.ToString("0.0") + "</priority>");
        writerFile.WriteLine(" </url>");
    }
}