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

public partial class webXabara_shop_add : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    XabaraClass xClass = new XabaraClass();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);

        if (!IsPostBack)
        {
            xClass.NewsTypeData("taobaoke", classID);
            object selectClass = Session["shopClass"];
            if (!string.IsNullOrEmpty(selectClass as string))
            {
                this.classID.Items.FindByValue(selectClass.ToString().Trim()).Selected = true;
            }
        }
    }

    protected void taobaoKe_Click(object sender, EventArgs e)
    {
        Session.Add("shopClass", classID.Text);
        string id = ids.Text.Trim();

        string appkey = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppKey");
        string appsecret = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppSecret");
        string url = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeUrl");
        string alimamaID = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAlimamaID");    //淘宝客推广ID

        ITopClient client = new DefaultTopClient(url, appkey, appsecret);
        TaobaokeItemsConvertRequest req = new TaobaokeItemsConvertRequest();
        req.Fields = "num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume";
        req.Nick = alimamaID;
        req.NumIids = id;
        TaobaokeItemsConvertResponse response = client.Execute(req);

        int tkCount = response.TaobaokeItems.Count; //转换后得到的淘客数量

        string strDim = "@classID,@num_iid,@title,@nick,@pic_url,@price,@click_url,@commission,@commission_rate,@commission_num,@commission_volume,@shop_click_url,@seller_credit_score,@item_location,@volume,@updateDate,@postID,@postDate,@postIP,@isBad,@isGood";
        string[] strValue = new string[30];
        string strSql;

        for (int i = 0; i < tkCount; i++)
        {
            strValue[0] = classID.Text;
            strValue[1] = response.TaobaokeItems[i].NumIid.ToString();
            strValue[2] = response.TaobaokeItems[i].Title.ToString();
            strValue[3] = response.TaobaokeItems[i].Nick.ToString();
            strValue[4] = response.TaobaokeItems[i].PicUrl.ToString();
            strValue[5] = response.TaobaokeItems[i].Price.ToString();
            strValue[6] = response.TaobaokeItems[i].ClickUrl.ToString();
            strValue[7] = response.TaobaokeItems[i].Commission.ToString();
            strValue[8] = response.TaobaokeItems[i].CommissionRate;
            strValue[9] = response.TaobaokeItems[i].CommissionNum.ToString();
            strValue[10] = response.TaobaokeItems[i].CommissionVolume.ToString();
            strValue[11] = response.TaobaokeItems[i].ShopClickUrl.ToString();
            strValue[12] = response.TaobaokeItems[i].SellerCreditScore.ToString();
            strValue[13] = response.TaobaokeItems[i].ItemLocation.ToString();
            strValue[14] = response.TaobaokeItems[i].Volume.ToString();
            strValue[15] = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");
            strValue[16] = xc.adminID;
            strValue[17] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strValue[18] = xc.GetIP();
            strValue[19] = "1";
            strValue[20] = "0";

            strSql = "select * from taobaoKe where num_iid =" + response.TaobaokeItems[i].NumIid.ToString();
            DataTable dt = db.getDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                strValue[18] = Convert.ToDateTime(dt.Rows[0]["postDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                strValue[20] = dt.Rows[0]["isGood"].ToString();
                db.InsertUpdateDB("taobaoKe", strDim, strValue, " num_iid =" + response.TaobaokeItems[i].NumIid.ToString());
            }
            else
            {
                db.InsertUpdateDB("taobaoKe", strDim, strValue, "");
            }
            dt.Dispose();
        }

        Response.Redirect("default.aspx", true);
    }
}