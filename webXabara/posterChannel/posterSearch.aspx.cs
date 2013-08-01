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

public partial class webXabara_posterChannel_posterSearch : System.Web.UI.Page
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

        Response.Write("自动同步淘画报，如出现死机或出错，重新点击自动更新即可；<br /><br />");
        Response.Flush();

        string requestType = Request.QueryString["type"];
        ITopClient client = new DefaultTopClient(url, appkey, appsecret);
        PosterPostersSearchRequest req = new PosterPostersSearchRequest();
        req.PageSize = 20L;
        req.PageNo = 1L;
        req.EditorRecommend = 1L;
        req.SortType = 4L;

        string strDim = "@id,@channel_id,@cover_pic_url_w,@cover_pic_url_h,@title,@title_short,@tag,@hits,@weight,@create_date,@modified_date,@isGood,@postID,@postIP,@postDate,@flagID";
        string[] strValue = new string[30];

        string posterId = string.Empty;
        dt = db.getDataTable("select id from posterChannelIDs where isFlag>0 order by newid()");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //获取画报标题
            req.ChannelIds = dt.Rows[i]["id"].ToString().Trim();
            PosterPostersSearchResponse response = client.Execute(req);

            for (int ii = 0; ii < response.Posters.Count; ii++)
            {
                strValue[0] = response.Posters[ii].Id.ToString();
                //标题ID赋值
                posterId = strValue[0];
                strValue[1] = response.Posters[ii].ChannelId.ToString();
                strValue[2] = string.Empty;
                strValue[3] = string.Empty;
                string pic = response.Posters[ii].CoverPicUrl.ToString();
                if (!string.IsNullOrEmpty(pic as string))
                {
                    string[] img = pic.Split(new char[] { ',' });
                    strValue[2] = img[0];
                    strValue[3] = img[img.GetUpperBound(0)];
                }
                strValue[4] = response.Posters[ii].Title;
                strValue[5] = response.Posters[ii].TitleShort;
                strValue[6] = response.Posters[ii].Tag;
                strValue[7] = response.Posters[ii].Hits.ToString();
                strValue[8] = response.Posters[ii].Weight.ToString();
                strValue[9] = response.Posters[ii].CreateDate.ToString();
                strValue[10] = response.Posters[ii].ModifiedDate.ToString();
                strValue[11] = "0";
                strValue[12] = xc.adminID;
                strValue[13] = xc.GetIP();
                strValue[14] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strValue[15] = "1"; //默认正常

                DataTable dtIF = db.getDataTable("select * from posterChannelTitle where id=" + posterId);  //防止重复插入
                if (dtIF.Rows.Count > 0)
                {
                    strValue[8] = dtIF.Rows[0]["weight"].ToString().Trim();
                    db.InsertUpdateDB("posterChannelTitle", strDim, strValue, "id=" + posterId);
                }
                else
                {
                    db.InsertUpdateDB("posterChannelTitle", strDim, strValue, string.Empty);
                }
                dtIF.Dispose();

                Response.Write("<br />同步 " + strValue[4] + " 已完成；<br />");
                Response.Flush();

                //获取关联商品
                PosterPostauctionsGetRequest poster = new PosterPostauctionsGetRequest();
                poster.PosterId = long.Parse(posterId);
                PosterPostauctionsGetResponse rsp = client.Execute(poster);
                int c = rsp.Posterauctions.Count;
                if (c > 9)
                {
                    string strDim2 = "@classID,@num_iid,@title,@nick,@pic_url,@price,@click_url,@commission,@commission_rate,@commission_num,@commission_volume,@shop_click_url,@seller_credit_score,@item_location,@volume,@updateDate,@postID,@postDate,@postIP,@isBad,@isGood,@poster_id,@auction_short_title";
                    for (int s = 0; s < c; s++)
                    {
                        strValue[0] = "0";
                        strValue[1] = rsp.Posterauctions[s].AuctionId.ToString();
                        strValue[2] = rsp.Posterauctions[s].AuctionShortTitle.ToString();
                        strValue[3] = "找店铺 ZdianPU.com";
                        strValue[4] = "/images/WaterMark.png";
                        strValue[5] = rsp.Posterauctions[s].AuctionPrice.ToString();
                        strValue[6] = @"http://item.taobao.com/item.htm?id=" + rsp.Posterauctions[s].AuctionId.ToString();
                        strValue[7] = "0";
                        strValue[8] = "0";
                        strValue[9] = "0";
                        strValue[10] = "0";
                        strValue[11] = @"http://www.zdianpu.com";
                        strValue[12] = "0";
                        strValue[13] = "找店铺";
                        strValue[14] = "0";
                        strValue[15] = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss");   //表示已过期 
                        strValue[16] = xc.adminID;
                        strValue[17] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        strValue[18] = xc.GetIP();
                        strValue[19] = "1";
                        strValue[20] = "0";
                        strValue[21] = rsp.Posterauctions[s].PosterId.ToString();
                        strValue[22] = rsp.Posterauctions[s].AuctionShortTitle.ToString();

                        DataTable dtIf = db.getDataTable("select * from taobaoKe with(nolock) where num_iid=" + rsp.Posterauctions[s].AuctionId.ToString());  //防止重复插入
                        if (dtIf.Rows.Count > 0)
                        {
                            strValue[0] = rsp.Posterauctions[s].PosterId.ToString();
                            strValue[1] = rsp.Posterauctions[s].AuctionShortTitle.ToString();
                            db.InsertUpdateDB("taobaoKe", "@poster_id,@auction_short_title", strValue, "num_iid=" + rsp.Posterauctions[s].AuctionId.ToString());
                        }
                        else
                        {
                            db.InsertUpdateDB("taobaoKe", strDim2, strValue, string.Empty);
                        }
                        dtIf.Dispose();
                    }

                    //淘宝客自动更新操作        
                    for (int f = 0; f < 100; f++)
                    {
                        string id = string.Empty;

                        DataTable tkIDs = db.getDataTable("select top 40 num_iid from taobaoKe where poster_id=" + posterId + " and isBad>0 and updateDate<GETDATE() order by updateDate ASC");    //30天过期内的数据可再次更新
                        if (tkIDs.Rows.Count > 0)
                        {
                            Response.Write("淘宝客数据转换已完成 " + ((f * 40) + tkIDs.Rows.Count).ToString() + "<br />");
                            Response.Flush();

                            for (int t = 0; t < tkIDs.Rows.Count; t++)
                            {
                                id += tkIDs.Rows[t]["num_iid"].ToString().Trim() + ",";
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
                            break;
                        }
                        tkIDs.Dispose();

                        //Response.Write(id.ToString());

                        if (!string.IsNullOrEmpty(id as string))
                        {
                            TaobaokeItemsConvertRequest reqTk = new TaobaokeItemsConvertRequest();
                            reqTk.Fields = "num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume";
                            reqTk.Nick = alimamaID;
                            reqTk.NumIids = id;
                            TaobaokeItemsConvertResponse responseTk = client.Execute(reqTk);

                            int tkCount = responseTk.TaobaokeItems.Count; //转换后得到的淘客数量

                            string strDim3 = "@num_iid,@title,@nick,@pic_url,@price,@click_url,@commission,@commission_rate,@commission_num,@commission_volume,@shop_click_url,@seller_credit_score,@item_location,@volume,@updateDate,@isBad";

                            for (int tk = 0; tk < tkCount; tk++)
                            {
                                strValue[0] = responseTk.TaobaokeItems[tk].NumIid.ToString();
                                strValue[1] = responseTk.TaobaokeItems[tk].Title.ToString();
                                strValue[2] = responseTk.TaobaokeItems[tk].Nick.ToString();
                                strValue[3] = responseTk.TaobaokeItems[tk].PicUrl.ToString();
                                strValue[4] = responseTk.TaobaokeItems[tk].Price.ToString();
                                strValue[5] = responseTk.TaobaokeItems[tk].ClickUrl.ToString();
                                strValue[6] = responseTk.TaobaokeItems[tk].Commission.ToString();
                                strValue[7] = responseTk.TaobaokeItems[tk].CommissionRate;
                                strValue[8] = responseTk.TaobaokeItems[tk].CommissionNum.ToString();
                                strValue[9] = responseTk.TaobaokeItems[tk].CommissionVolume.ToString();
                                strValue[10] = responseTk.TaobaokeItems[tk].ShopClickUrl.ToString();
                                strValue[11] = responseTk.TaobaokeItems[tk].SellerCreditScore.ToString();
                                strValue[12] = responseTk.TaobaokeItems[tk].ItemLocation.ToString();
                                strValue[13] = responseTk.TaobaokeItems[tk].Volume.ToString();
                                strValue[14] = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");
                                strValue[15] = "1";

                                db.InsertUpdateDB("taobaoKe", strDim3, strValue, " num_iid =" + responseTk.TaobaokeItems[tk].NumIid.ToString());
                                //更新商品一对多关联表
                                DataTable dtIf = db.getDataTable("select * from posterTkID with(nolock) where title_id=" + posterId + " and tK_num_iid=" + responseTk.TaobaokeItems[tk].NumIid.ToString());
                                if (dtIf.Rows.Count < 1)  //防止重复插入
                                {
                                    strValue[0] = posterId;
                                    strValue[1] = responseTk.TaobaokeItems[tk].NumIid.ToString();

                                    db.InsertUpdateDB("posterTkID", "@title_id,@tK_num_iid", strValue, "");
                                }
                                dtIf.Dispose();
                            }
                        }
                    }
                }
                else
                {
                    db.DelDB("posterChannelTitle", "id", response.Posters[ii].Id.ToString(), "", false, "");
                }
            }
        }
        dt.Dispose();
    }
}