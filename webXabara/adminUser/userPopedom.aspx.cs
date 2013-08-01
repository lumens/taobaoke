using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class webXabara_adminUser_userPopedom : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("|adminExe|");

        if (!IsPostBack)
        {
            string strSqlread = "select UserID,UserName,UserAdmin from AdminUser where userID='" + xc.SafeSql(Request["id"].Trim()) + "'";
            DataTable dtRead = db.getDataTable(strSqlread);
            if (dtRead.Rows.Count > 0 && xc.SafeSql(Request["id"].Trim()) != xc.adminID)
            {
                if (dtRead.Rows[0]["UserAdmin"].ToString().Trim().Equals("xabaraCOM"))
                {
                    xc.divError("", dtRead.Rows[0]["UserID"].ToString().Trim() + " 已是超级管理员，不需设置相应权限！", 380, 150, "userList.aspx", "goto");
                }
                string userAdminStr = dtRead.Rows[0]["UserAdmin"].ToString();

                uid.Text = dtRead.Rows[0]["UserName"].ToString() + "[" + dtRead.Rows[0]["UserID"].ToString() + "]";
                DataTable dt;

                dt = db.getDataTable("select * from classAdmin order by listID ASC ");
                if (dt.Rows.Count > 0)
                {
                    string ifType = string.Empty;
                    string bgColor = string.Empty;
                    selectPopedomHtm.Text = "<table border='0' align='left' class='tableAdminLine'><tr>";
                    for (int p = 0; p < dt.Rows.Count; p++)
                    {
                        if (!ifType.Equals(dt.Rows[p]["listClass"].ToString().Trim()) && p > 0)  //控制回车
                        {
                            selectPopedomHtm.Text += "</tr><tr>";
                        }
                        ifType = dt.Rows[p]["listClass"].ToString().Trim();

                        string selectValue = dt.Rows[p]["classCode"].ToString().Trim();
                        string selectBool = string.Empty;
                        if (userAdminStr.IndexOf(@"|" + selectValue + "|") >= 0)
                        {
                            selectBool = @" checked='checked'";
                        }

                        selectPopedomHtm.Text += "<td style='background-color:#f5fdfe;'><input name='adminPopedomSelect' type='checkbox' id='adminPopedomSelect' value='" + selectValue + "'" + selectBool + " />" + dt.Rows[p]["className"].ToString().Trim() + "</td>";

                    }
                    selectPopedomHtm.Text += "</tr></table>";
                }
                dt.Dispose();
            }
            else
            {
                xc.divError("", "您的操作有误！", 350, 150, "userList.aspx", "goto");
            }
            dtRead.Dispose();
        }
    }

    protected void setAdmin_Click(object sender, EventArgs e)
    {
        string selectValue = Request["adminPopedomSelect"];
        if (!string.IsNullOrEmpty(selectValue))
        {
            selectValue = selectValue.Replace(",", "|");

            string[] strValue = new string[2];
            strValue[0] = "|" + selectValue + "|";
            strValue[1] = "2";

            xc.insertMyLog(xc.adminID, "admin", "修改管理员权限");

            xc.divError("设置提示", db.InsertUpdateDB("AdminUser", "@UserAdmin,@loginFlag", strValue, "userID='" + xc.SafeSql(Request["id"].Trim()) + "'"), 350, 150, "userList.aspx", "goto");
        }
        else
        {
            xc.divError("", "请选择相应权限后再设置！", 350, 150, "", "");
        }
    }
}
