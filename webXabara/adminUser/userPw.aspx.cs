using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebXabara_userPw : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XabaraCom xc = new XabaraCom();

        xc.CheckAdminLogin("|adminPW|");

        string idTemp = xc.SafeNum(Request["id"].Trim()).ToString();       
        string dimStr = "@UserPW";
        string[] strValue = new string[1];
        string strVlaueTemp = xc.GetRnd("abc", 1) + xc.GetRnd("abc123", 7).ToLower();
        strValue[0] = xc.GetMd5(strVlaueTemp).ToString();

        if (new dbDataFunction().InsertUpdateDB("AdminUser", dimStr, strValue, "Uid=" + idTemp + " and UserId<>'" + xc.adminID + "'").Equals("更新成功"))
        {
            xc.insertMyLog(xc.adminID, "admin", "管理员密码初始化");
            xc.divError("", "新密码：" + strVlaueTemp, 350, 150, "userList.aspx", "goto");
        }
        else
        {
            xc.divError("", "不能给自己密码初始化或操作错误！", 350, 150, "userList.aspx", "goto");
        }
    }
}
