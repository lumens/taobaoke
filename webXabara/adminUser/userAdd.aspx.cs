using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webXabara_adminUser_userAdd : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("|adminAdd|");
        if (!IsPostBack)
        {
            new XabaraClass().NewsTypeData("department", adminClass);

            FormCheck fCheck = new FormCheck();
            adminIDRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("userID", 3, 20, false);
            adminIDRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("userID", 3, 20, true);
            adminNameRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("chinese", 0, 0, false);
            adminNameRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("chinese", 0, 0, true);
            adminPWRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("passWord", 8, 20, false);
            adminPWRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("passWord", 8, 20, true);
        }
    }

    protected void addAdmin_Click(object sender, EventArgs e)
    {
        string adminIDTemp = xc.SafeSql(adminID.Text.Trim().ToLower());
        string strSql = "select * from AdminUser where userID='" + adminIDTemp + "'";
        if (db.getDataTable(strSql).Rows.Count > 0)
        {
            xc.divError("", "已存在该管理员！", 350, 150, "", "");
        }
        else
        {
            string dimStr = "@UserId,@UserName,@UserPW,@SessionError,@loginFlag,@LoginNum,@RegDate,@LoginDate,@LoginIP,@classID";

            string[] strValue = new string[10];
            strValue[0] = adminIDTemp;
            strValue[1] = adminName.Text.Trim();
            strValue[2] = xc.GetMd5(adminPW.Text.Trim());
            strValue[3] = "xabara.com";
            strValue[4] = "1";
            strValue[5] = "0";
            strValue[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strValue[7] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strValue[8] = xc.GetIP().ToString();
            strValue[9] = adminClass.Text;

            if (db.InsertUpdateDB("AdminUser", dimStr, strValue, string.Empty).Equals("发布成功"))
            {
                xc.insertMyLog(xc.adminID, "admin", "添加管理员：" + adminIDTemp);
                xc.divError("", "添加管理员成功，需对 " + adminName.Text.Trim() + " 进行权限设置后才可以正常登陆！", 500, 200, "userPopedom.aspx?id=" + adminIDTemp, "goto");
            }
            else
            {
                xc.divError("", "添加管理员失败！", 350, 150, "", "");
            }
        }
    }
}
