using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Net;

public partial class webXabara_frame_htm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        xc.CheckAdminLogin("");

        if (!IsPostBack)
        {
            this.CreateUserPage("http://" + Request.Url.Host + "/default.aspx");
        }
    }

    //生成页面入口
    private void CreateUserPage(string url)
    {
        StringBuilder ret = new StringBuilder("");
        Uri uri = new Uri(url);
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
        HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
        Stream rspStream = rsp.GetResponseStream();
        StreamReader sr = new StreamReader(rspStream, System.Text.Encoding.UTF8);
        //获取数据   
        Char[] read = new Char[256];
        int count = sr.Read(read, 0, 256);
        while (count > 0)
        {
            ret.Append(read, 0, count);
            count = sr.Read(read, 0, 256);
        }

        WriteFile(ret.ToString(), "default"); //按时间生成页面名字
    }

    //生成HTML页 
    public static bool WriteFile(string body, string fileName)
    {
        string path = HttpContext.Current.Server.MapPath("/");  //生成文件的存放目录
        Encoding code = Encoding.GetEncoding("UTF-8");
        StreamWriter sw = null;
        string htmlfilename = fileName + ".html";
        // 写文件 
        try
        {
            sw = new StreamWriter(path + htmlfilename, false, code);
            sw.Write(body);
            sw.Flush();
        }
        catch
        {
            return false;
        }
        finally
        {
            sw.Close();
            sw.Dispose();

            HttpContext.Current.Response.Write("恭喜 <a href=\"/" + htmlfilename + "\" target=\"_blank\">" + htmlfilename + "</a> 已经生成成功！<br />");
        }
        return true;
    }
}