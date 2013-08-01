using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebXabara_ads_edit : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("|adsEdit|");

        if (!IsPostBack)
        {
            FormCheck fCheck = new FormCheck();
            pdateRegExpree.ValidationExpression = fCheck.RegExpressionValidator("dateTime", 0, 0, false);
            pdateRegExpree.ErrorMessage = fCheck.RegExpressionValidator("dateTime", 0, 0, true);
            eDateRegExpree.ValidationExpression = fCheck.RegExpressionValidator("dateTime", 0, 0, false);
            eDateRegExpree.ErrorMessage = fCheck.RegExpressionValidator("dateTime", 0, 0, true);
            httpRegExpree.ValidationExpression = fCheck.RegExpressionValidator("urlHttp", 0, 0, false);
            httpRegExpree.ErrorMessage = fCheck.RegExpressionValidator("urlHttp", 0, 0, true);
            wRegExpree.ValidationExpression = fCheck.RegExpressionValidator("number", 0, 0, false);
            wRegExpree.ErrorMessage = "宽度" + fCheck.RegExpressionValidator("number", 0, 0, true);
            hRegExpree.ValidationExpression = fCheck.RegExpressionValidator("number", 0, 0, false);
            hRegExpree.ErrorMessage = "高度" + fCheck.RegExpressionValidator("number", 0, 0, true);

            XabaraClass xClass = new XabaraClass();
            xClass.NewsTypeData("ads", adClass);

            string img = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadImgType");
            this.adImg.Items.Add(new ListItem(img, "Img"));
            string flash = XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadFlashType");
            this.adImg.Items.Add(new ListItem(flash, "Flash"));
            this.adImg.Items.Add(new ListItem("广告代码", "Code"));

            DataTable dt = db.getDataTable("select * from ads where aID=" + xc.SafeNum(Request["id"]).ToString());
            if (dt.Rows.Count > 0)
            {
                this.adClass.Items.FindByValue(dt.Rows[0]["classID"].ToString().Trim()).Selected = true;
                string fileName = dt.Rows[0]["adImg"].ToString().Trim();
                upImg.Value = fileName;
                if (!string.IsNullOrEmpty(fileName as string))
                {
                    string fileExe = fileName.Substring(fileName.Length - 3);
                    if (img.IndexOf(fileExe) > -1)
                    {
                        this.adImg.Items.FindByValue("Img").Selected = true;
                    }
                    else
                    {
                        this.adImg.Items.FindByValue("Flash").Selected = true;
                    }
                }
                else
                {
                    this.adImg.Items.FindByValue("Code").Selected = true;
                }

                adW.Text = dt.Rows[0]["adW"].ToString().Trim();
                adH.Text = dt.Rows[0]["adH"].ToString().Trim();
                adHttp.Text = dt.Rows[0]["adHttp"].ToString().Trim();
                adCode.Text = dt.Rows[0]["adCode"].ToString().Trim();
                pDate.Text = Convert.ToDateTime(dt.Rows[0]["starDate"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
                eDate.Text = Convert.ToDateTime(dt.Rows[0]["stopDate"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
                adCodeImg.Text = dt.Rows[0]["adCode"].ToString().Trim();
            }
            else
            {
                xc.divError("", "您的操作有误！", 350, 150, "default.aspx", "goto");
            }
            dt.Dispose();
        }
    }

    protected void editMoney_Click(object sender, EventArgs e)
    {
        string imgFile = adImg.Text.Trim();
        string uploadFileStr = string.Empty;
        if (!imgFile.Equals("Code"))
        {
            uploadFileStr = xc.UploadFile(imgUpload, imgFile, false);
        }
        string url = adHttp.Text;

        string newFile = upImg.Value;
        if (uploadFileStr.Equals("上传成功"))
        {
            if (!string.IsNullOrEmpty(newFile as string))
            {
                xc.delFile(newFile.Substring(5, 8) + "/" + newFile);
            }
            newFile = Session["NewFile"].ToString().Trim();
        }

        string strDim = "@classID,@adW,@adH,@adImg,@adHttp,@adCode,@starDate,@stopDate,@postDate,@postIP,@adminID";
        string[] strValue = new string[11];
        strValue[0] = adClass.Text;
        strValue[1] = adW.Text;
        strValue[2] = adH.Text;
        strValue[3] = newFile;
        strValue[4] = url;
        switch (imgFile)
        {
            case "Img":
                if (!string.IsNullOrEmpty(url as string))
                {
                    strValue[5] = "<a href=\"" + url + "\" target=\"_blank\"><img src=\"http://" + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webDomains") + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\" style=\"boder:0px;width:" + adW.Text + "px;height:" + adH.Text + "px\" /></a>";
                }
                else
                {
                    strValue[5] = "<img src=\"" + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\"  style=\"boder:0px;width:" + adW.Text + "px;height:" + adH.Text + "px\" />";
                }
                break;
            case "Flash":
                strValue[5] = "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" height=\"" + adH.Text + "\" width=\"" + adW.Text + "\"><param name=\"quality\" value=\"high\" /><param name=\"movie\" value=\"" + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\" /><embed height=\"" + adH.Text + "\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" quality=\"high\" src=\"" + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\" type=\"application/x-shockwave-flash\" width=\"" + adW.Text + "\"></embed></object>";
                break;
            default:
                strValue[5] = adCode.Text;
                break;

        }
        strValue[6] = pDate.Text;
        strValue[7] = eDate.Text;
        strValue[8] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        strValue[9] = xc.GetIP();
        strValue[10] = xc.adminID;

        xc.insertMyLog(xc.adminID, "admin", "修改广告");

        xc.divError("", db.InsertUpdateDB("ads", strDim, strValue, "aID=" + xc.SafeNum(Request["id"]).ToString()), 350, 150, "default.aspx", "goto");
    }
}