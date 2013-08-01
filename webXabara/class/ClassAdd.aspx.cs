using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class webXabara_class_ClassAdd : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    string typeClss = HttpContext.Current.Request["tClass"].ToString().Trim();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("|" + typeClss + "|");
        if (!IsPostBack)
        {
            XabaraClass xClass = new XabaraClass();
            xClass.NewsTypeData(typeClss, DropDownListClass);
            if (!string.IsNullOrEmpty(Session[typeClss] as string))
            {
                this.DropDownListClass.Items.FindByValue(Session[typeClss].ToString().Trim()).Selected = true;
            }
            else
            {
                this.DropDownListClass.Items.FindByText("== 新增根分类 ==").Selected = true;
            }
            xClass.getTreeList(typeClss, treeList, string.Empty, "top", false, true, false);

            FormCheck fCheck = new FormCheck();
            classListRegExpre.ValidationExpression = fCheck.RegExpressionValidator("number", 0, 0, false);
            classListRegExpre.ErrorMessage = fCheck.RegExpressionValidator("number", 0, 0, true);
            classUrlRegExpre.ValidationExpression = fCheck.RegExpressionValidator("urlHttp", 0, 0, false);
            classUrlRegExpre.ErrorMessage = fCheck.RegExpressionValidator("urlHttp", 0, 0, true);

            titleClass.Text = Server.UrlDecode(xc.SafeSql(Request["title"]));
        }
    }

    protected void addClass_Click(object sender, EventArgs e)
    {
        string classNameTemp = className.Text.Trim();
        string oldClassID = DropDownListClass.Text.Trim();
        Session.Add(typeClss, oldClassID); //缓存
        string strSql = "select * from NewsTree where TreeID=" + oldClassID + " and TreeName='" + classNameTemp + "' and TreeType='" + typeClss + "'";

        if (db.getDataTable(strSql).Rows.Count > 0)
        {
            xc.divError("", "已存在该分类！", 350, 150, "ClassAdd.aspx?tClass=" + typeClss, "goto");
        }
        else
        {
            string dimStr = "@TreeID,@ListID,@TidNums,@TreeName,@TreeType,@treeAdmin,@treeDel,@PostDate,@AdminIP,@AdminID,@TreeUrl,@taobaoKe";

            string[] strValue = new string[12];
            strValue[0] = oldClassID;

            if (string.IsNullOrEmpty(classList.Text))
                strValue[1] = "0";
            else
                strValue[1] = classList.Text;

            strValue[2] = "0";
            strValue[3] = classNameTemp;
            strValue[4] = typeClss;
            strValue[5] = "0";
            strValue[6] = "0";
            strValue[7] = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd HH:mm:ss");
            strValue[8] = xc.GetIP();
            strValue[9] = xc.adminID;
            strValue[10] = classUrl.Text;
            strValue[11] = tk.Text;

            if (db.InsertUpdateDB("NewsTree", dimStr, strValue, string.Empty).Equals("发布成功"))
            {
                if (Convert.ToInt32(oldClassID) > 0) //更新子分类数
                {
                    string strSqlSelect = "select TreeID,TidNums,idLayerStr from NewsTree where Tid=" + oldClassID + " and TreeType='" + typeClss + "'";
                    DataTable dt = db.getDataTable(strSqlSelect);
                    int tidNums = Convert.ToInt32(dt.Rows[0]["TidNums"]) + 1;
                    strValue[0] = tidNums.ToString();
                    db.InsertUpdateDB("NewsTree", "@TidNums", strValue, "Tid=" + oldClassID + " and TreeType='" + typeClss + "'");

                    int tidTemp = Convert.ToInt32(dt.Rows[0]["TreeID"]);    //更新关联字符串
                    if (tidTemp < 1)
                    {
                        strValue[0] = "|";
                    }
                    else
                    {
                        strValue[0] = dt.Rows[0]["idLayerStr"].ToString();
                    }
                    strValue[0] = strValue[0].Trim() + oldClassID.ToString() + "|";
                    db.InsertUpdateDB("NewsTree", "@idLayerStr", strValue, "TreeID=" + oldClassID + " and TreeName='" + classNameTemp + "' and TreeType='" + typeClss + "'");

                    dt.Dispose();
                }

                xc.insertMyLog(xc.adminID, "admin", Server.UrlDecode(titleClass.Text + "：" + classNameTemp));

                xc.divError("", Server.UrlDecode(titleClass.Text) + "成功！", 350, 150, "ClassAdd.aspx?tClass=" + typeClss + "&title=" + titleClass.Text, "goto");
            }
            else
            {
                xc.divError("", Server.UrlDecode(titleClass.Text) + "失败！", 350, 150, "ClassAdd.aspx?tClass=" + typeClss + "&title=" + titleClass.Text, "goto");
            }
        }
    }
}
