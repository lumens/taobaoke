using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Data;

public partial class WebXabara_Main : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    EncryptDecrypt ed = new EncryptDecrypt();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);

        if (!Page.IsPostBack)
        {
            //自动升级
            long updateID = xc.SafeNum(Request.QueryString["update"]);
            if (updateID > 0)
            {
                xc.autoUpdate();
            }

            string soft = ed.softName();
            string[] softName = soft.Split(new char[] { '_' });
            soft = softName[0];

            //检查版本
            string oldVer = "v1.12.0330";
            xc.xmlSave("webVer", oldVer); //自动更新版本号
            string newVer = xc.getRomoteHtmCode("http://www.zdianpu.com/api/ver/?id=" + ed.softName(), "utf-8", "", "");
            if (string.IsNullOrEmpty(newVer as string) || newVer.IndexOf("zdianpu.com") > 0)
            {
                ver.Text = "当前版本：" + oldVer;
            }
            else
            {
                if (oldVer.Equals(newVer))
                {
                    ver.Text = "当前版本：" + oldVer + "<br />您已经是最新版本！";
                }
                else
                {
                    ver.Text = "当前版本：" + oldVer + "<br />最新版本：<span style=\"color:#b60202; font-weight:bold;\">" + newVer + "</span> 【<a href=\"http://www.zdianpu.com/soft/" + soft + "/\" target=\"_blank\" style=\"font-weight:bold;\">手工升级</a>】【<a href=\"main.aspx?update=1\" style=\"font-weight:bold;\">在线升级</a>】";
                }
            }
            buyLink.NavigateUrl = "http://www.zdianpu.com/soft/" + soft + "/server.aspx";

            adminLog.DataSource = db.getDataTable("select top 6 * from logLogin where userType='admin' and  exeTitle='管理员登录' order by id desc");
            adminLog.DataBind();
            adminLog.Dispose();

            serverIP.Text = Request.ServerVariables["LOCAL_ADDR"] + ":" + Request.ServerVariables["Server_Port"].ToString();
            ServerOS.Text = Environment.OSVersion.ToString();
            ServerOSVer.Text = xc.os(Environment.OSVersion.ToString());
            ServerSoft.Text = Request.ServerVariables["SERVER_SOFTWARE"];
            netVer.Text = Environment.Version.ToString();
            ServerPath.Text = Request.ServerVariables["PATH_TRANSLATED"];
            serverTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sqlVer.Text = db.sqlVer();

            string infoTxt = "";
            infoTxt += "<li style=\"line-height:200%; padding:2px 0px 2px 5px;\">统计离上次登录网站信息情况</li><li style=\"line-height:200%; background-color:#e9f8fc; padding:2px 0px 2px 5px;\">共有 ";

            DataTable dt;
            dt = db.getDataTable("select count(Tid) as nums from newsTree where taobaoKe<>''");
            infoTxt += dt.Rows[0]["nums"].ToString().Trim() + " 个店铺关联信息&nbsp;&nbsp;<a href=\"../shop/taobao.aspx\" target=\"mainFrame\" style=\"color:red;\">自动更新</a></li><li style=\"line-height:200%; padding:2px 0px 2px 5px;\">共有 ";
            dt.Dispose();

            dt = db.getDataTable("select count(num_iid) as nums from taobaoKe where isBad>0");
            infoTxt += dt.Rows[0]["nums"].ToString().Trim() + " 条淘宝客有效信息</li><li style=\"line-height:200%; background-color:#e9f8fc; padding:2px 0px 2px 5px;\">共有 ";
            dt.Dispose();

            dt = db.getDataTable("select count(num_iid) as nums from taobaoKe where isBad<1");
            infoTxt += dt.Rows[0]["nums"].ToString().Trim() + " 个淘宝客无效信息&nbsp;&nbsp;<a href=\"../shop/Default.aspx?isBad=1\" target=\"mainFrame\" style=\"color:red;\">查看</a>&nbsp;&nbsp;<a href=\"../shop/taobaoke.aspx\" target=\"mainFrame\" style=\"color:red;\">强制转换</a></li>";
            dt.Dispose();

            infoLJ.Text = infoTxt;
        }
    }

    public string ip(string ip)
    {
        return xc.GetIpWhere(ip);
    }

    string chkObj(string obj) //自检查
    {
        try
        {
            object meobj = Server.CreateObject(obj);
            return "支持";
        }
        catch (Exception objexe)
        {

            return objexe.Message.ToString();
        }
    }
}
