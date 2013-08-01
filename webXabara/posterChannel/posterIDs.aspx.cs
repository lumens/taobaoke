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

public partial class webXabara_posterChannel_posterIDs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        dbDataFunction db = new dbDataFunction();

        xc.CheckAdminLogin("");

        DataTable dt;        

        string appkey = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppKey");
        if (string.IsNullOrEmpty(appkey as string))
        {
            Response.Redirect("../frame/otherSet.aspx", true);
        }
        string appsecret = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppSecret");
        string url = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeUrl");
        string alimamaID = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAlimamaID");    //淘宝客推广ID

        ITopClient client = new DefaultTopClient(url, appkey, appsecret);
        PosterChannelsGetRequest req = new PosterChannelsGetRequest();
        PosterChannelsGetResponse response = client.Execute(req);

        Response.Write("自动同步淘画报频道ID，如出现死机或出错，重新点击自动更新即可；<br />");
        Response.Flush();

        string strDim = "@id,@channel_name,@name_en,@description,@postID,@postDate,@postIP,@isFlag";
        string[] strValue = new string[7];

        for (int ii = 0; ii < response.Channels.Count; ii++)
        {
            strValue[0] = response.Channels[ii].Id.ToString();
            strValue[1] = response.Channels[ii].ChannelName.ToString();
            strValue[2] = response.Channels[ii].NameEn.ToString();
            if (string.IsNullOrEmpty(response.Channels[ii].Description as string))
            {
                strValue[3] = string.Empty;
            }
            else
            {
                strValue[3] = response.Channels[ii].Description;
            }
            strValue[4] = xc.adminID;
            strValue[5] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strValue[6] = xc.GetIP();

            dt = db.getDataTable("select * from posterChannelIDs where id=" + response.Channels[ii].Id.ToString());  //防止重复插入
            if (dt.Rows.Count > 0)
            {
                strDim = "@id,@channel_name,@name_en,@description,@postID,@postDate,@postIP";
                db.InsertUpdateDB("posterChannelIDs", strDim, strValue, "id=" + response.Channels[ii].Id.ToString());
            }
            else
            {
                db.InsertUpdateDB("posterChannelIDs", strDim, strValue, string.Empty);
            }
            dt.Dispose();

            Response.Write("同步 " + strValue[1] + " [" + strValue[2] + "] " + " 已完成；<br />");
            Response.Flush();
        }
        //宝贝同步结束        
    }
}