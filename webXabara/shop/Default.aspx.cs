using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class webXabara_shop_Default : System.Web.UI.Page
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

            string ifStr = string.Empty;
            string pageStr = string.Empty;
            string searchStr = Server.UrlDecode(Request["keyWord"]);
            string searchIf = string.Empty;
            string orderBy = " Order By id desc";
            long sale = xc.SafeNum(Request.QueryString["sale"]);
            if (sale > 0)
            {
                orderBy = " Order By volume DESC";
                pageStr += "&sale=1";
            }

            if (!string.IsNullOrEmpty(searchStr as string))
            {
                if (xc.SafeNum(searchStr) > 0)
                {
                    searchIf = " and num_iid=" + searchStr;
                }
                else
                {
                    searchIf = " and title like '%" + searchStr + "%'";
                }

                searchWord.Text = searchStr;
                pageStr += "&keyWord=" + searchStr;
            }

            //显示推荐商品
            long isGood = xc.SafeNum(Request.QueryString["isGood"]);
            if (isGood > 0 || this.goodCheck.Checked)
            {
                searchIf += " and isGood>0 ";

                pageStr += "&isGood=1";

                orderBy = " Order By volume DESC";

                this.goodCheck.Checked = true;
            }

            long ifGood = xc.SafeNum(Request.QueryString["ifGood"]);
            if (ifGood > 0) //建议推荐商品
            {
                searchIf += " and isGood<1 and commission>10 and volume>1 and classID>0 ";

                pageStr += "&ifGood=1";

                orderBy = " Order By volume DESC";
            }
            if (ifGood < 0) //建议取消推荐商品或删除商品
            {
                searchIf += " and volume<2 and isGood>0 ";

                pageStr += "&ifGood=-1";

                orderBy = " Order By commission ASC";
            }

            long isBad = xc.SafeNum(Request.QueryString["isBad"]);
            if (isBad > 0 ) //无效信息列表
            {
                searchIf += " and isBad=0 ";

                pageStr += "&isBad=1";
            }

            long cidTemp = xc.SafeNum(Request.QueryString["cid"]);
            if (cidTemp > 0)
            {
                this.classID.Items.FindByValue(cidTemp.ToString()).Selected = true;
                pageStr += "&cid=" + cidTemp.ToString();
            }

            if (cidTemp > 0)
            {
                string cidStr = xClass.getClassAllID("taobaoke", cidTemp); //得到所有ID
                if (cidStr.IndexOf(",") > 0)
                {
                    string[] ids = cidStr.Split(new char[] { ',' });
                    for (int i = 0; i <= ids.GetUpperBound(0); i++)
                    {
                        ifStr += " select * from taobaoke where classID=" + ids[i].ToString().Trim() + searchIf + " union ";
                    }
                }
                else
                {
                    ifStr = " select * from taobaoke where classID=" + cidTemp.ToString().Trim() + searchIf + " union ";
                }
            }
            else
            {
                ifStr = " select * from taobaoke where 1=1 " + searchIf + " union ";
            }

            ifStr = "(" + ifStr.Substring(0, ifStr.Length - 6) + ") as news";

            string strSql = "select * from " + ifStr + orderBy;
            //Response.Write(strSql);
            //Response.End();
            lblCurrentPage.Text = db.RepeaterDB(RepeaterList, strSql, pageStr, 30, "page", "center", true);
            if (this.RepeaterList.Items.Count < 1)
            {
                errInfo.Text = "目前暂没有任何信息！";
                errDiv.Visible = true;
            }
            else
            {
                errDiv.Visible = false;
            }
        }
    }

    public string getTreeName(string str)
    {
        string strName = xClass.getClass(Convert.ToInt32(str), string.Empty, string.Empty);
        if (string.IsNullOrEmpty(strName as string))
        {
            strName = "淘画报";
        }

        return strName;
    }
    

    public string cutWords(string str, int len)
    {
        return xc.CutWord(xc.SafeHtml(str), len);
    }

    protected void classID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string good = string.Empty;
        if (this.goodCheck.Checked)
        {
            good = "&isGood=1";
        }
        Response.Redirect("?cid=" + classID.Text + "&keyWord=" + Server.UrlEncode(searchWord.Text) + good, true);
    }

    protected void search_Click(object sender, EventArgs e)
    {
        string good = string.Empty;
        if (this.goodCheck.Checked)
        {
            good = "&isGood=1";
        }
        Response.Redirect("?cid=" + classID.Text + "&keyWord=" + Server.UrlEncode(searchWord.Text) + good, true);
    }

    protected void clearData_Click(object sender, EventArgs e)
    {
        string strSql;
        long cid = xc.SafeNum(classID.Text);

        if (cid > 0)
        {
            strSql = "delete from taobaoKe where classID=" + classID.Text;
        }
        else
        {
            strSql = "delete from taobaoKe where isBad=0";
        }
        db.exeSql(strSql);

        Response.Redirect("?cid=" + classID.Text, true);
    }

    protected void del_Click(object sender, EventArgs e)
    {
        string ids = Request["batDel"];
        if (!string.IsNullOrEmpty(ids as string))
        {
            string returnStr = db.DelDB("taobaoKe", "id", ids, "", false, "");
            Response.Redirect(Server.UrlDecode(xc.errorUrlTwo), true);
        }
        else
        {
            xc.divError("", "请先选择后再操作！", 350, 150, Server.UrlDecode(xc.errorUrlTwo) + "#foot", "goto");
        }
    }

    protected void good_Click(object sender, EventArgs e)
    {
        string[] strValue=new string[1];
        strValue[0]="1";

        string ids = Request["batDel"];
        if (!string.IsNullOrEmpty(ids as string))
        {
            ids = ids.Replace(",", " or id=");
            ids = "id=" + ids;

            string returnStr = db.InsertUpdateDB("taobaoKe", "@isGood", strValue, ids);
            Response.Redirect(Server.UrlDecode(xc.errorUrlTwo) + "#foot", true);
        }
        else
        {
            xc.divError("", "请先选择后再操作！", 350, 150, Server.UrlDecode(xc.errorUrlTwo), "goto");
        }
    }

    protected void unGood_Click(object sender, EventArgs e)
    {
        string[] strValue = new string[1];
        strValue[0] = "0";

        string ids = Request["batDel"];
        if (!string.IsNullOrEmpty(ids as string))
        {
            ids = ids.Replace(",", " or id=");
            ids = "id=" + ids;

            string returnStr = db.InsertUpdateDB("taobaoKe", "@isGood", strValue, ids);
            Response.Redirect(Server.UrlDecode(xc.errorUrlTwo) + "#foot", true);
        }
        else
        {
            xc.divError("", "请先选择后再操作！", 350, 150, Server.UrlDecode(xc.errorUrlTwo), "goto");
        }
    }

    protected void autoGood_Click(object sender, EventArgs e)
    {
        string[] strValue = new string[1];
        strValue[0] = "1";

        DataTable dt = db.getDataTable("select id from taobaoke where classID>0 and isBad>0 and isGood<1 and volume >49 ");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            db.InsertUpdateDB("taobaoKe", "@isGood", strValue, " id=" + dt.Rows[i]["id"].ToString().Trim());
        }
        dt.Dispose();

        Response.Redirect(Server.UrlDecode(xc.errorUrlTwo), true);
    }
}