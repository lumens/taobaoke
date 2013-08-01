using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebXabara_ads_del : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        xc.CheckAdminLogin("|adsDel|");
       
        dbDataFunction db = new dbDataFunction();
        string returnValue = string.Empty;        

        long idTemp = xc.SafeNum(Request["id"].Trim());
        
        returnValue = db.DelDB("ads", "aID", xc.SafeSql(Request["id"].Trim()).ToString(), string.Empty, false, "adImg");

        xc.insertMyLog(xc.adminID, "admin", "删除广告");

        xc.divError("", returnValue, 350, 150, Server.UrlDecode(xc.errorUrlTwo), "goto");
    }
}