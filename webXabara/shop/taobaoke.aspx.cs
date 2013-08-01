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

public partial class webXabara_shop_taobaoke : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();

    protected void Page_Load(object sender, EventArgs e)
    {        
        dbDataFunction db = new dbDataFunction();

        xc.CheckAdminLogin("");

        string strDim = string.Empty;
        string[] strValue = new string[30];

        string appkey = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppKey");
        if (string.IsNullOrEmpty(appkey as string))
        {
            Response.Redirect("../frame/otherSet.aspx", true);
        }
        string appsecret = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAppSecret");
        string url = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeUrl");
        string alimamaID = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "taobaoKeAlimamaID");    //淘宝客推广ID

        ITopClient client = new DefaultTopClient(url, appkey, appsecret);

        Response.Write("只针对于过期或无效淘宝客再次请求转换有效链接，如出现死机或出错，重新点击<a href=\"taobaoke.aspx\">自动更新</a>即可；<br />");
        Response.Flush();

        string strSql = string.Empty;        

        //淘宝客自动更新操作        
        for (int f = 0; f < 5; f++)
        {
            string id = string.Empty;

            DataTable tkIDs = db.getDataTable("select top 40 num_iid from taobaoKe where updateDate<GETDATE() order by newid()");    //30天过期内的数据可再次更新
            if (tkIDs.Rows.Count > 0)
            {
                Response.Write("<br />淘宝客数据转换已完成 " + ((f * 40) + tkIDs.Rows.Count).ToString());
                Response.Flush();

                for (int i = 0; i < tkIDs.Rows.Count; i++)
                {
                    id += tkIDs.Rows[i]["num_iid"].ToString().Trim() + ",";
                }
                id = id.Substring(0, id.Length - 1);

                //锁定淘宝客更新条数                
                strValue[0] = "0";
                string updateID = id.Replace(",", " or num_iid=");
                updateID = "num_iid=" + updateID;
                //Response.Write(updateID);
                //Response.End();
                db.InsertUpdateDB("taobaoKe", "@isBad", strValue, updateID);
            }
            else
            {
                Response.Write("<br />当前淘宝客数据转换已全部完成！");
                break;
            }
            tkIDs.Dispose();

            //Response.Write(id.ToString());

            if (!string.IsNullOrEmpty(id as string))
            {
                TaobaokeItemsConvertRequest req = new TaobaokeItemsConvertRequest();
                req.Fields = "num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume";
                req.Nick = alimamaID;
                req.NumIids = id;
                TaobaokeItemsConvertResponse response = client.Execute(req);

                int tkCount = response.TaobaokeItems.Count; //转换后得到的淘客数量

                strDim = "@num_iid,@title,@nick,@pic_url,@price,@click_url,@commission,@commission_rate,@commission_num,@commission_volume,@shop_click_url,@seller_credit_score,@item_location,@volume,@updateDate,@isBad";

                for (int i = 0; i < tkCount; i++)
                {
                    strValue[0] = response.TaobaokeItems[i].NumIid.ToString();
                    strValue[1] = response.TaobaokeItems[i].Title.ToString();
                    strValue[2] = response.TaobaokeItems[i].Nick.ToString();
                    strValue[3] = response.TaobaokeItems[i].PicUrl.ToString();
                    strValue[4] = response.TaobaokeItems[i].Price.ToString();
                    strValue[5] = response.TaobaokeItems[i].ClickUrl.ToString();
                    strValue[6] = response.TaobaokeItems[i].Commission.ToString();
                    strValue[7] = response.TaobaokeItems[i].CommissionRate;
                    strValue[8] = response.TaobaokeItems[i].CommissionNum.ToString();
                    strValue[9] = response.TaobaokeItems[i].CommissionVolume.ToString();
                    strValue[10] = response.TaobaokeItems[i].ShopClickUrl.ToString();
                    strValue[11] = response.TaobaokeItems[i].SellerCreditScore.ToString();
                    strValue[12] = response.TaobaokeItems[i].ItemLocation.ToString();
                    strValue[13] = response.TaobaokeItems[i].Volume.ToString();
                    strValue[14] = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");
                    strValue[15] = "1";

                    db.InsertUpdateDB("taobaoKe", strDim, strValue, " num_iid =" + response.TaobaokeItems[i].NumIid.ToString());
                }
            }            
        }

        Response.Clear();
        this.js();  //定时刷新
    }

    /// <summary>
    /// js方式输出
    /// </summary>
    /// <param name="str">js代码</param>
    /// <returns></returns>
    private void js()
    {
        string str = "<script>window.setTimeout(\"window.location.href='taobaoke.aspx?tmp=" + xc.GetRnd("123", 6) + "'\",5000)</script>";

        ClientScriptManager cs = ((Page)HttpContext.Current.CurrentHandler).ClientScript;
        cs.RegisterClientScriptBlock(this.GetType(), "ClientScriptBlock", str);
    }
}