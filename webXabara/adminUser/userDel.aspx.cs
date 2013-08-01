using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebXabara_userDel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();
        xc.CheckAdminLogin("|adminDel|");

        if (!xc.adminID.Equals(xc.SafeSql(Request["id"].Trim()).ToString()))
        {
            string returnValue = new dbDataFunction().DelDB("AdminUser", "UserId", xc.SafeSql(Request["id"].Trim()).ToString(), string.Empty, false, string.Empty);

            xc.insertMyLog(xc.adminID, "admin", "删除管理员");

            xc.divError("", returnValue, 350, 150, "userList.aspx", "goto");
        }
        else
        {
            xc.divError("", "不会吧，删除自己呀！", 350, 150, "userList.aspx", "goto");
        }
    }
}
