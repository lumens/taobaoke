using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class webXabara_posterChannel_Default : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    XabaraClass xClass = new XabaraClass();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);

        if (!IsPostBack)
        {
            DataTable dt;
            dt = db.getDataTable("select * from posterChannelIDs order by id ASC");
            this.classID.DataSource = dt.DefaultView;            
            this.classID.DataTextField = "channel_name";
            this.classID.DataValueField = "id";
            this.classID.DataBind();
            dt.Dispose();
            this.classID.Items.Add(new ListItem("== 分类 ==", ""));           

            string ifStr = string.Empty;
            string pageStr = string.Empty;
            string searchStr = Server.UrlDecode(Request["keyWord"]);
            string searchIf = string.Empty;
            string orderBy = " Order By posterChannelTitle.tID desc";            

            if (!string.IsNullOrEmpty(searchStr as string))
            {
                searchIf = " and (posterChannelTitle.title like '%" + searchStr + "%' or posterChannelTitle.title_short like '%" + searchStr + "%' )";
                searchWord.Text = searchStr;
                pageStr += "&keyWord=" + searchStr;
            }

            long isGood = xc.SafeNum(Request.QueryString["isGood"]);
            if (isGood > 0 || this.goodCheck.Checked)
            {
                searchIf += " and posterChannelTitle.weight>10000 ";

                pageStr += "&isGood=1";

                orderBy = " Order By posterChannelTitle.weight DESC";

                this.goodCheck.Checked = true;
            }

            long ifGood = xc.SafeNum(Request.QueryString["ifGood"]);
            if (ifGood > 0) //建议推荐商品
            {
                searchIf += " and weight<100 and hits>10000 and create_date>'" + DateTime.Now.AddDays(-7).ToString() + "' ";
                pageStr += "&ifGood=1";
            }

            long cidTemp = xc.SafeNum(Request.QueryString["cid"]);
            if (cidTemp > 0)
            {
                ifStr += " and posterChannelTitle.channel_id=" + cidTemp.ToString();
                this.classID.Items.FindByValue(cidTemp.ToString()).Selected = true;
                pageStr += "&cid=" + cidTemp.ToString();
            }
            else
            {
                this.classID.Items.FindByText("== 分类 ==").Selected = true;
            }

            string strSql = "select * from posterChannelTitle left join posterChannelIDs on posterChannelTitle.channel_id=posterChannelIDs.id where  posterChannelTitle.flagID=1 " + ifStr + searchIf + orderBy;
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

    protected void del_Click(object sender, EventArgs e)
    {
        string ids = Request["batDel"];
        if (!string.IsNullOrEmpty(ids as string))
        {
            db.DelDB("posterChannelTitle", "id", ids, "", false, "");
            db.DelDB("posterTkID", "title_id", ids, "", false, "");
            Response.Redirect(Server.UrlDecode(xc.errorUrlTwo), true);
        }
        else
        {
            xc.divError("", "请先选择后再操作！", 350, 150, Server.UrlDecode(xc.errorUrlTwo) + "#foot", "goto");
        }
    }

    protected void good_Click(object sender, EventArgs e)
    {
        string[] strValue = new string[1];
        strValue[0] = DateTime.Now.ToString("yyMMddss");

        string ids = Request["batDel"];
        if (!string.IsNullOrEmpty(ids as string))
        {
            ids = ids.Replace(",", " or id=");
            ids = "id=" + ids;

            string returnStr = db.InsertUpdateDB("posterChannelTitle", "@weight", strValue, ids);
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

            string returnStr = db.InsertUpdateDB("posterChannelTitle", "@weight", strValue, ids);
            Response.Redirect(Server.UrlDecode(xc.errorUrlTwo) + "#foot", true);
        }
        else
        {
            xc.divError("", "请先选择后再操作！", 350, 150, Server.UrlDecode(xc.errorUrlTwo), "goto");
        }
    }
}