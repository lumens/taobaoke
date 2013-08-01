using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class webXabara_class_updateCache : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        dbDataFunction db = new dbDataFunction();
        XabaraClass xClass = new XabaraClass();

        xc.CheckAdminLogin("");

        DataTable dt = db.getDataTable("select Tid,TreeType from newsTree order by Tid");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            long cid = xc.SafeNum(dt.Rows[i]["Tid"].ToString().Trim());
            xClass.treeNameUpdate(cid);
            xClass.updateID(dt.Rows[i]["TreeType"].ToString().Trim(), cid);

            Response.Write("正在更新第 " + cid.ToString() + " 条<br />");
            Response.Flush();
        }
        dt.Dispose();

        Response.Write("更新全站缓存完成！");
    }
}