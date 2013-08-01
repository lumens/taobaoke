using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webXabara_adminUser_exe : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);

        string uID = xc.SafeSql(Request["id"]).Trim();
        string ifStr = string.Empty;
        string pageStr = string.Empty;

        if (!uID.Equals("a"))
        {
            ifStr = " and userCardID='" + uID + "'";
        }
        else
        {
            //ifStr = " and userID<>'xabara'";
            pageStr = "&id=a";
        }

        string strSql = "select * from logLogin where userType='admin' " + ifStr + " order by id DESC";
        lblCurrentPage.Text = new dbDataFunction().RepeaterDB(RepeaterList, strSql, pageStr, 20, "page", "center", true);
    }

    public string ipStr(string str)
    {
        return xc.GetIpWhere(str);
    }
}