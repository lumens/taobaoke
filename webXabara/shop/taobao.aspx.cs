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

public partial class webXabara_shop_taobao : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();

    protected void Page_Load(object sender, EventArgs e)
    {        
        dbDataFunction db = new dbDataFunction();

        xc.CheckAdminLogin("");

        DataTable dt;
        string strDim = "@classID,@num_iid,@title,@nick,@pic_url,@price,@click_url,@commission,@commission_rate,@commission_num,@commission_volume,@shop_click_url,@seller_credit_score,@item_location,@volume,@updateDate,@postID,@postDate,@postIP,@isBad,@isGood,@poster_id";
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

        Response.Write("根据分类自动同步淘宝客开始，如出现死机或出错，重新点击<a href=\"taobao.aspx\">自动更新</a>即可；<br />");
        Response.Flush();

        this.js();  //定时刷新

        //更新店铺
        DateTime t = DateTime.Now.Date;
        string strSql = "select top 1 * from NewsTree with(nolock) where taobaoKe <>'' and PostDate<'" + t.ToString() + "' order by newid()";       
        dt = db.getDataTable(strSql); //当天不再请求更新
        if (dt.Rows.Count > 0)
        {
            string classID = dt.Rows[0]["Tid"].ToString().Trim();
            Response.Write("<br />" + dt.Rows[0]["treeNameTxt"].ToString().Trim() + "　同步完成！");
            Response.Flush();
            //System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 10));                

            ItemsGetRequest req = new ItemsGetRequest();
            req.Fields = "num_iid,title,nick,pic_url,cid,price,type,delist_time,post_fee,score,volume";
            req.Nicks = dt.Rows[0]["taobaoKe"].ToString().Trim();
            req.PageNo = 1L;
            req.OrderBy = "volume";
            req.StartPrice = 50L;
            req.EndPrice = 100000L;
            req.PageSize = 200L;    //取最畅销前200个商品
            ItemsGetResponse response = client.Execute(req);
            long shopNums = response.TotalResults;

            if (shopNums > 0)
            {
                strValue[0] = DateTime.Now.ToString();
                db.InsertUpdateDB("NewsTree", "@PostDate", strValue, "Tid=" + classID); //更新店铺宝贝同步

                for (int ii = 0; ii < response.Items.Count; ii++)
                {
                    strValue[0] = classID;
                    strValue[1] = response.Items[ii].NumIid.ToString();
                    strValue[2] = response.Items[ii].Title;
                    strValue[3] = response.Items[ii].Nick;
                    strValue[4] = response.Items[ii].PicUrl;
                    strValue[5] = response.Items[ii].Price;
                    strValue[6] = @"http://item.taobao.com/item.htm?id=" + response.Items[ii].NumIid.ToString();
                    strValue[7] = "0";
                    strValue[8] = "0";
                    strValue[9] = "0";
                    strValue[10] = "0";
                    strValue[11] = @"http://www.zdianpu.com";
                    strValue[12] = "0";
                    strValue[13] = "找店铺";
                    strValue[14] = response.Items[ii].Volume.ToString();
                    strValue[15] = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss");   //表示已过期 
                    strValue[16] = "sys";
                    strValue[17] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    strValue[18] = xc.GetIP();
                    strValue[19] = "1";
                    strValue[20] = "0";
                    strValue[21] = "0";

                    if (response.Items[ii].Title.IndexOf("邮费") < 0 || response.Items[ii].Title.IndexOf("补差") < 0)
                    {
                        DataTable dtIf = db.getDataTable("select * from taobaoKe with(nolock) where num_iid=" + response.Items[ii].NumIid.ToString());  //防止重复插入
                        if (dtIf.Rows.Count > 0)
                        {
                            strValue[6] = dtIf.Rows[0]["click_url"].ToString().Trim();
                            strValue[7] = dtIf.Rows[0]["commission"].ToString().Trim();
                            strValue[8] = dtIf.Rows[0]["commission_rate"].ToString().Trim();
                            strValue[9] = dtIf.Rows[0]["commission_num"].ToString().Trim();
                            strValue[10] = dtIf.Rows[0]["commission_volume"].ToString().Trim();
                            strValue[11] = dtIf.Rows[0]["shop_click_url"].ToString().Trim();
                            strValue[12] = dtIf.Rows[0]["seller_credit_score"].ToString().Trim();
                            strValue[13] = dtIf.Rows[0]["item_location"].ToString().Trim();
                            strValue[15] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            strValue[17] = dtIf.Rows[0]["postDate"].ToString().Trim();
                            strValue[20] = dtIf.Rows[0]["isGood"].ToString().Trim();
                            strValue[21] = dtIf.Rows[0]["poster_id"].ToString().Trim(); //淘画报

                            db.InsertUpdateDB("taobaoKe", strDim, strValue, "num_iid=" + response.Items[ii].NumIid.ToString());
                        }
                        else
                        {
                            db.InsertUpdateDB("taobaoKe", strDim, strValue, string.Empty);
                        }
                        dtIf.Dispose();
                    }
                }
            }
            else
            {
                strValue[0] = DateTime.Now.ToString();
                db.InsertUpdateDB("NewsTree", "@PostDate", strValue, "Tid=" + classID); //更新店铺宝贝同步时出错或非淘宝客，防止死锁
            }
        }
        //宝贝同步结束

        dt.Dispose();

        //淘宝客自动更新操作
        for (int f = 0; f < 5; f++)
        {
            string id = string.Empty;

            DataTable tkIDs = db.getDataTable("select top 40 num_iid from taobaoKe where isBad>0 and updateDate<GETDATE() order by updateDate ASC");    //30天过期内的数据可再次更新
            if (tkIDs.Rows.Count > 0)
            {
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

                Response.Write("<br />淘宝客数据转换已完成 " + ((f * 40) + tkIDs.Rows.Count).ToString()+"，请等待10秒...");
                Response.Flush();
            }
            else
            {
                Response.Write("<br />当前淘宝客数据转换已全部完成，请明天再手动更新！");
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
    }

    /// <summary>
    /// js方式输出
    /// </summary>
    /// <param name="str">js代码</param>
    /// <returns></returns>
    private void js()
    {
        string str = "<script>window.setTimeout(\"window.location.href='taobao.aspx?tmp=" + xc.GetRnd("123", 6) + "'\",5000)</script>";

        ClientScriptManager cs = ((Page)HttpContext.Current.CurrentHandler).ClientScript;
        cs.RegisterClientScriptBlock(this.GetType(), "ClientScriptBlock", str);
    }
}