using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public class webPublic
{
    XabaraCom xc = new XabaraCom();

    /// <summary>
    /// 页面缓存
    /// </summary>
    /// <param name="n">输入时间（分）</param>
    /// <returns>true</returns>
    public bool cacheTime(int n)
    {
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(15 * 60));
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);
        HttpContext.Current.Response.Cache.SetVaryByCustom("browser");
        HttpContext.Current.Response.Cache.VaryByParams["City"] = true;

        return true;
    }

    /// <summary>
    /// 淘宝店铺卖家信誉度
    /// </summary>
    /// <param name="credit">信誉度值</param>
    /// <returns></returns>
    public string taobaoCredit(string credit)
    {
        string htmStr = string.Empty;

        switch (credit)
        {
            case "1":
                htmStr = "b_red_1.gif";
                break;
            case "2":
                htmStr = "b_red_2.gif";
                break;
            case "3":
                htmStr = "b_red_3.gif";
                break;
            case "4":
                htmStr = "b_red_4.gif";
                break;
            case "5":
                htmStr = "b_red_5.gif";
                break;

            case "6":
                htmStr = "s_blue_1.gif";
                break;
            case "7":
                htmStr = "s_blue_2.gif";
                break;
            case "8":
                htmStr = "s_blue_3.gif";
                break;
            case "9":
                htmStr = "s_blue_4.gif";
                break;
            case "10":
                htmStr = "s_blue_5.gif";
                break;

            case "11":
                htmStr = "s_cap_1.gif";
                break;
            case "12":
                htmStr = "s_cap_2.gif";
                break;
            case "13":
                htmStr = "s_cap_3.gif";
                break;
            case "14":
                htmStr = "s_cap_4.gif";
                break;
            case "15":
                htmStr = "s_cap_5.gif";
                break;

            case "16":
                htmStr = "s_crown_1.gif";
                break;
            case "17":
                htmStr = "s_crown_2.gif";
                break;
            case "18":
                htmStr = "s_crown_3.gif";
                break;
            case "19":
                htmStr = "s_crown_4.gif";
                break;
            case "20":
                htmStr = "s_crown_5.gif";
                break;
        }

        htmStr = "<img src=\"/images/creditImg/" + htmStr + "\" />";

        return htmStr;
    }

    /// <summary>
    /// 淘宝读取商品页码数
    /// </summary>
    /// <param name="nums">TotalResults 值</param>
    /// <returns></returns>
    public int taobaoReadPages(long nums, int pageSize)
    {
        int pageNums = Convert.ToInt32(nums % pageSize);
        if (pageNums == 0)
        {
            pageNums = Convert.ToInt32(nums / pageSize);
        }
        else
        {
            pageNums = Convert.ToInt32(nums / pageSize + 1);
        }

        return pageNums;
    }
}