using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebXabara_ads_Default : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();
    XabaraClass xClass = new XabaraClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);

        xClass.NewsTypeData("ads", adWhere);
        
        string strSql = string.Empty;
        string ifStr = string.Empty;

        if (!IsPostBack)
        {
            Int64 aidTemp = xc.SafeNum(Request["aid"]);
            if (aidTemp > 0)
            {
                string idNums = xClass.getClassAllID("ads", Convert.ToInt32(aidTemp));

                if (!idNums.Equals(aidTemp.ToString()))
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
                    ifStr += " and classID=" + aidTemp.ToString().Trim();
                }                
                this.adWhere.Items.FindByValue(aidTemp.ToString()).Selected = true;
            }

            strSql = "select * from ads where 1=1 " + ifStr + " order by stopDate DESC,aID desc";
            //Response.Write(strSql);
            lblCurrentPage.Text = db.RepeaterDB(RepeaterList, strSql, "&aid=" + aidTemp, 5, "page", "center", true);
        }
    }

    protected void adWhere_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx?aid=" + adWhere.Text, true);
    }

    public string getClass(Int64 cid)
    {
        return xClass.getClass(cid, string.Empty, string.Empty);
    }
}