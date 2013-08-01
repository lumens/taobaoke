using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class webXabara_posterChannel_autoDel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        xc.CheckAdminLogin("");

        dbDataFunction db = new dbDataFunction();

        Response.Write("自动清除淘画报，如出现死机或出错，重新点击自动更新即可；<br /><br />");
        Response.Flush();

        //清除低佣金的画报
        db.exeSql("delete from taobaoKe where commission<5 and classID=0 and postDate<'" + DateTime.Now.AddDays(-15).ToString() + "'");
        //清除无佣金商品
        db.exeSql("delete from taobaoKe where isBad=0 and poster_id>0 and classID=0");

        DataTable dt = db.getDataTable("select id,title from posterChannelTitle order by tID DESC");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string idTemp = dt.Rows[i]["id"].ToString().Trim();
            string strSql = "select title from taobaoKe left join posterTkID on taobaoKe.num_iid=posterTkID.tK_num_iid where taobaoKe.isBad>0 and posterTkID.title_id=" + idTemp.ToString() + " order by commission DESC,volume DESC";
            DataTable ifDt = db.getDataTable(strSql);
            if (ifDt.Rows.Count < 10)
            {
                db.DelDB("posterChannelTitle", "id", idTemp.ToString(), "", false, "");
                db.DelDB("posterTkID", "title_id", idTemp.ToString(), "", false, "");

                Response.Write("自动清除　" + dt.Rows[i]["title"].ToString().Trim() + " 成功!<br />");
                Response.Flush();
            }
            ifDt.Dispose();
        }
        dt.Dispose();        

        Response.Write("自动清除淘画报完成！<br /><br />");
        Response.Flush();
    }
}