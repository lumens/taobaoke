using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class webXabara_class_ClassList : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();
    XabaraClass xClass = new XabaraClass();
    string typeClss = new XabaraCom().SafeSql(HttpContext.Current.Request["tClass"]);
    string titleTemp = new XabaraCom().SafeSql(HttpContext.Current.Request["title"]);
    string[] strValue = new string[10];

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("|" + typeClss + "|");
        if (!IsPostBack)
        {
            xClass.NewsTypeData(typeClss, DropDownListClass);
            classListName.Visible = true;
            classListName.Text = "如需排序，请选择带有子级分类的父分类并点击 显示所选分类排序 按钮！";
            xClass.getTreeList(typeClss, treeList, string.Empty, "top", false, true, false);

            titleClass.Text = Server.UrlDecode(titleTemp);

            DataTable dt = db.getDataTable("select Tid from newsTree where treeNameTxt is null");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                long cid = xc.SafeNum(dt.Rows[i]["Tid"].ToString().Trim());
                xClass.treeNameUpdate(cid);
                xClass.updateIDs(typeClss, cid);
            }
            dt.Dispose();

            FormCheck fCheck = new FormCheck();
            classUrlRegExpre.ValidationExpression = fCheck.RegExpressionValidator("urlHttp", 0, 0, false);
            classUrlRegExpre.ErrorMessage = fCheck.RegExpressionValidator("urlHttp", 0, 0, true);
        }
    }

    protected void editClass_Click(object sender, EventArgs e)
    {
        string cid = DropDownListClass.Text.Trim();
        string classNameTemp = className.Text.Trim();
        if (!String.IsNullOrEmpty(classNameTemp as string))
        {
            strValue[0] = classNameTemp;
            strValue[1] = classUrl.Text;
            strValue[2] = tk.Text;
            strValue[3] = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            xc.insertMyLog(xc.adminID, "admin", "修改分类：" + classNameTemp);
            string returnStr = db.InsertUpdateDB("NewsTree", "@TreeName,@TreeUrl,@taobaoKe,@PostDate", strValue, "Tid=" + cid + " and TreeType='" + typeClss + "'");

            string strSql = "select Tid from newsTree where Tid=" + cid;
            strSql += " union select Tid from newsTree where idLayerStr like '%|" + cid + "|%'";
            DataTable dt = db.getDataTable(strSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xClass.treeNameUpdate(xc.SafeNum(dt.Rows[i]["Tid"].ToString().Trim()));
            }
            dt.Dispose();

            xc.divError("", returnStr, 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
        }
        else
        {
            xc.divError("", "请填写分类名称！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
        }
    }

    protected void delClass_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(DropDownListClass.Text.Trim() as string))
        {
            string ClassID = DropDownListClass.Text.Trim();
            string strREADsql = "select TreeID,treeDel,TreeName,TreeNameImg from NewsTree where Tid=" + ClassID + " and TidNums=0 and TreeType='" + typeClss + "'";
            DataTable dt = db.getDataTable(strREADsql);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt64(dt.Rows[0]["treeDel"]) > 0)
                {
                    xc.divError("", "该分类为前台固定分类，不允许删除！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
                }
                else
                {
                    long updateID = Convert.ToInt64(dt.Rows[0]["TreeID"].ToString());

                    xc.insertMyLog(xc.adminID, "admin", "删除分类：" + dt.Rows[0]["TreeName"].ToString());
                    string oldImg = dt.Rows[0]["TreeNameImg"].ToString().Trim();
                    if (!string.IsNullOrEmpty(oldImg as string))
                    {
                        string DelFilePath = oldImg.Substring(5, 8) + "/" + oldImg;
                        xc.delFile(DelFilePath);
                    }

                    if (db.DelDB("NewsTree", "Tid", ClassID, string.Empty, false, string.Empty).Equals("删除成功"))
                    {
                        if (updateID > 0)   //更新子分类数
                        {
                            string strSqlSelect = "select TidNums from NewsTree where Tid=" + updateID + " and TreeType='" + typeClss + "'";
                            int tidNums = Convert.ToInt32(db.getDataTable(strSqlSelect).Rows[0]["TidNums"]) - 1;
                            strValue[0] = tidNums.ToString();
                            db.InsertUpdateDB("NewsTree", "@TidNums", strValue, "Tid=" + updateID + " and TreeType='" + typeClss + "'");
                        }

                        xc.divError("", "删除分类成功，建议清理数据一次！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
                    }
                    else
                    {
                        xc.divError("", "删除分类失败！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
                    }
                }
            }
            else
            {
                xc.divError("", "请从子分类最底层删除分类！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
            }
            dt.Dispose();
        }
        else
        {
            xc.divError("", "请选择相关分类再进行操作！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
        }
    }

    protected void displayClassList_Click(object sender, EventArgs e)
    {
        classListName.Text = string.Empty;

        string strSql = "select Tid,ListID,TreeName from NewsTree where TreeID=" + DropDownListClass.Text.Trim() + " and TreeType='" + typeClss + "' order by ListID ASC";
        DataTable dt = db.getDataTable(strSql);

        if (dt.Rows.Count > 0)
        {
            treeCount.Value = dt.Rows.Count.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                classListName.Text += "<li><input name=\"tree" + i.ToString() + "\" type=\"text\" id=\"tree" + i.ToString() + "\" class=\"inputText\" value=\"" + dt.Rows[i]["ListID"].ToString().Trim() + "\" style=\"width:25px; text-align:center;\" />&nbsp;" + dt.Rows[i]["TreeName"].ToString().Trim() + "<input type=\"hidden\" name=\"hid" + i.ToString() + "\" id=\"hid" + i.ToString() + "\" value=\"" + dt.Rows[i]["Tid"].ToString().Trim() + "\" /></li>";
            }

            classListName.Visible = true;
            editClassbottom.Visible = true;
        }
        else
        {
            classListName.Visible = true;
            classListName.Text = "<li>如需排序，请选择带有子级分类的父分类！</li>";
        }
        dt.Dispose();
    }

    protected void editClassbottom_Click(object sender, EventArgs e)
    {
        for (int r = 0; r < xc.SafeNum(treeCount.Value); r++)
        {
            strValue[0] = xc.SafeNum(Request["tree" + r.ToString()]).ToString();
            db.InsertUpdateDB("NewsTree", "@ListID", strValue, "Tid=" + xc.SafeNum(Request["hid" + r.ToString()]).ToString() + " and TreeType='" + typeClss + "'");
        }

        xc.insertMyLog(xc.adminID, "admin", "修改分类排序");

        xc.divError("", "更新排序成功！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
    }

    protected void waterImgButton_Click(object sender, EventArgs e)
    {
        uploadError.Text = string.Empty;
        string idTemp = DropDownListClass.Text;

        if (Convert.ToInt16(idTemp) > 0)
        {
            string UpFileReturnStr = xc.UploadFile(waterImgFileUpload, "Img", false);

            if (UpFileReturnStr.Equals("上传成功"))
            {
                DataTable dt = db.getDataTable("select TreeNameImg from NewsTree where Tid=" + idTemp);
                if (dt.Rows.Count > 0)
                {
                    string oldImg = dt.Rows[0]["TreeNameImg"].ToString().Trim();
                    if (!string.IsNullOrEmpty(oldImg as string))
                    {
                        xc.delFile(oldImg.Substring(5, 8) + "/" + oldImg);
                    }
                }
                dt.Dispose();

                strValue[0] = Session["NewFile"].ToString();
                db.InsertUpdateDB("NewsTree", "@TreeNameImg", strValue, "Tid=" + idTemp + " and TreeType='" + typeClss + "'");

                xc.insertMyLog(xc.adminID, "admin", "添加图片分类");

                uploadError.Text = "设置成功！";
            }
            else
            {
                xc.divError("", "操作失败，请重新操作！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
            }
        }
        else
        {
            xc.divError("", "请选择具体子分类再进行操作！", 350, 150, "ClassList.aspx?tClass=" + typeClss, "goto");
        }
    }

    protected void DropDownListClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = db.getDataTable("select * from newsTree where Tid=" + DropDownListClass.Text);
        if (dt.Rows.Count > 0)
        {
            className.Text = dt.Rows[0]["TreeName"].ToString().Trim();
            classUrl.Text = dt.Rows[0]["TreeUrl"].ToString().Trim();
            tk.Text = dt.Rows[0]["taobaoKe"].ToString().Trim();
        }
        dt.Dispose();
    }
}
