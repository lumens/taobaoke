using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class webXabara_adminUser_userEdit : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);

        XabaraClass xClass = new XabaraClass();

        if (!IsPostBack)
        {
            string uidTemp = xc.adminID;

            Uid.Text = uidTemp;

            xClass.NewsTypeData("department", adminClass);

            string strSql = "select * from AdminUser where userID='" + uidTemp + "'";
            DataTable dt = db.getDataTable(strSql);

            if (dt.Rows.Count > 0)
            {
                adminName.Text = dt.Rows[0]["UserName"].ToString().Trim();

                int cID = Convert.ToInt32(dt.Rows[0]["classID"].ToString().Trim());
                string cidName = xClass.getClass(cID, string.Empty, string.Empty);
                if (string.IsNullOrEmpty(cidName as string))
                {
                    if (cID > 0)
                    {
                        cidName = "原分类已被删除，请重新选择！";
                        this.adminClass.Items.Add(new ListItem("== 请选择 ==", ""));
                        this.adminClass.Items.FindByValue(string.Empty).Selected = true;
                    }
                    else
                    {
                        this.adminClass.Items.Add(new ListItem("超级管理员", "0"));
                        this.adminClass.Items.FindByValue(cID.ToString()).Selected = true;
                    }
                }
                else
                {
                    this.adminClass.Items.FindByValue(cID.ToString()).Selected = true;
                }

                classLab.Text = cidName;
                if (dt.Rows[0]["UserAdmin"].ToString().Trim().Equals("xabaraCOM") && !string.IsNullOrEmpty(xc.SafeSql(Request["uid"]) as string))
                {
                    adminClass.Visible = true;
                    adminClassValidator.Visible = true;
                }

                oldPWHidden.Value = dt.Rows[0]["UserPW"].ToString().Trim();
                adminMail.Text = dt.Rows[0]["userEmail"].ToString().Trim();
                adminTel.Text = dt.Rows[0]["userTel"].ToString().Trim();
                adminMobile.Text = dt.Rows[0]["userMobile"].ToString().Trim();
                adminQQ.Text = dt.Rows[0]["userQQ"].ToString().Trim();
            }
            else
            {
                xc.divError("", "您的操作有误！", 350, 150, "userList.aspx", "goto");
            }
            dt.Dispose();

            FormCheck fCheck = new FormCheck();
            adminNameRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("chinese", 0, 0, false);
            adminNameRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("chinese", 0, 0, true);
            adminPWRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("passWord", 0, 0, false);
            adminPWRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("passWord", 0, 0, true);
            mailRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("email", 0, 0, false);
            mailRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("email", 0, 0, true);
            telRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("phone", 0, 0, false);
            telRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("phone", 0, 0, true);
            mobileRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("mobile", 0, 0, false);
            mobileRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("mobile", 0, 0, true);
            qqRegularExpressionValidator.ValidationExpression = fCheck.RegExpressionValidator("qq", 0, 0, false);
            qqRegularExpressionValidator.ErrorMessage = fCheck.RegExpressionValidator("qq", 0, 0, true);
        }
    }

    protected void editAdmin_Click(object sender, EventArgs e)
    {
        string dimStr = "@classID,@UserName,@UserPW,@userEmail,@userTel,@userMobile,@userQQ";

        string[] strValue = new string[7];
        strValue[0] = adminClass.Text;
        strValue[1] = adminName.Text.Trim();
        string pw = adminPW.Text;
        if (!string.IsNullOrEmpty(pw as string))
        {
            pw = xc.GetMd5(pw);
        }
        else
        {
            pw = oldPWHidden.Value;
        }
        strValue[2] = pw;
        strValue[3] = adminMail.Text.Trim();
        strValue[4] = adminTel.Text.Trim();
        strValue[5] = adminMobile.Text.Trim();
        strValue[6] = adminQQ.Text.Trim();

        if (db.InsertUpdateDB("AdminUser", dimStr, strValue, "userID='" + xc.adminID + "'").Equals("更新成功"))
        {
            xc.insertMyLog(xc.adminID, "admin", "修改资料");

            Session.Add("AdminRndNums", string.Empty);

            xc.divError("", "更新资料成功！", 350, 150, xc.AdminFileName, "top");
        }
        else
        {
            xc.divError("", "更新资料失败！", 350, 150, "userEdit.aspx", "goto");
        }
    }
}
