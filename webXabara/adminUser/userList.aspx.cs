using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebXabara_userList : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    XabaraClass xClass = new XabaraClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);
        
        long cidTemp = xc.SafeNum(Request["cid"]);        
        string strSql = string.Empty;
        string ifStr = string.Empty;
        string keyWordStr = xc.SafeSql(Server.UrlDecode(Request["keyWords"])); 
        
        if (!IsPostBack)
        {
            xClass.NewsTypeData("department", classList);

            if (cidTemp > 0) //分类读取
            {
                string idNums = xClass.getClassAllID("department", cidTemp);

                if (!idNums.Equals(cidTemp.ToString()))
                {
                    string[] ids = idNums.Split(new char[] { ',' });
                    for (int i = 0; i <= ids.GetUpperBound(0); i++)
                    {
                        ifStr += "classID=" + ids[i].ToString().Trim() + " or ";
                    }
                    ifStr = " and (" + ifStr.Substring(0, ifStr.Length - 3) + ")";
                }
                else
                {
                    ifStr += " and classID=" + cidTemp.ToString().Trim();
                }
                this.classList.Items.FindByValue(cidTemp.ToString()).Selected = true;
            }
            if (!string.IsNullOrEmpty(keyWordStr as string))
            {
                ifStr += " and (UserName like '%" + keyWordStr + "%' or UserId like '%" + keyWordStr + "%') ";
                keyWord.Text = keyWordStr;
            }
            strSql = "select * from AdminUser where classID>0 " + ifStr + " Order By LoginDate desc";
            //Response.Write(strSql);

            lblCurrentPage.Text = new dbDataFunction().RepeaterDB(RepeaterList, strSql, "&keyWords=" + keyWordStr, 20, "page", "center", true);
        }
    }

    public string getClass(int cid)
    {
        string str = xClass.getClass(cid, string.Empty, string.Empty);
        if (string.IsNullOrEmpty(str as string))
        {
            str = "管理员";
        }
        return str;
    }

    public string getQQ(string str)
    {
        return xc.QQHTM(str, "5");
    }

    protected void classList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("userList.aspx?cid=" + classList.Text + "&keyWords=" + Server.UrlEncode(keyWord.Text.Trim()), true);
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("userList.aspx?cid=" + classList.Text + "&keyWords=" + Server.UrlEncode(keyWord.Text.Trim()), true);
    }
}
