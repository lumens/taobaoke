using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebXabara_ads_add : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("|adsAdd|");

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

            DateTime nStr = DateTime.Now;
            pDate.Text = nStr.ToString("yyyy-MM-dd HH:mm:ss");
            eDate.Text = nStr.AddYears(1).ToString("yyyy-MM-dd HH:mm:ss");

            XabaraClass xClass = new XabaraClass();
            xClass.NewsTypeData("ads", adClass);

            this.adImg.Items.Add(new ListItem("图片：" + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadImgType"), "Img"));
            this.adImg.Items.Add(new ListItem("Flash：" + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadFlashType"), "Flash"));
            this.adImg.Items.Add(new ListItem("广告代码", "Code"));
        }
    }

    protected void addMoney_Click(object sender, EventArgs e)
    {        
        dbDataFunction db = new dbDataFunction();

        string imgFile = adImg.Text.Trim();
        string uploadFileStr = string.Empty;
        if (!imgFile.Equals("Code"))
        {
            uploadFileStr = xc.UploadFile(imgUpload, imgFile, false);
        }
        string url = adHttp.Text;
        string newFile = string.Empty;

        if (uploadFileStr.Equals("上传成功"))   //上传文件判断
        {
            newFile = Session["NewFile"].ToString().Trim();
        }

        string strDim = "@classID,@adW,@adH,@adImg,@adHttp,@adCode,@starDate,@stopDate,@postDate,@postIP,@adminID";
        string[] strValue = new string[11];

        strValue[0] = adClass.Text;
        strValue[1] = adW.Text;
        strValue[2] = adH.Text;        
        strValue[3] = newFile;
        strValue[4] = url;
        if (string.IsNullOrEmpty(adCode.Text as string))
        {
            if (imgFile.Equals("Img"))
            {
                if (!string.IsNullOrEmpty(url as string))
                {
                    strValue[5] = "<a href=\"" + url + "\" target=\"_blank\"><img src=\"http://" + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "webDomains") + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\" style=\"boder:0px;width:" + adW.Text + "px;height:" + adH.Text + "px\" /></a>";
                }
                else
                {
                    strValue[5] = "<img src=\"" + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\"  style=\"boder:0px;width:" + adW.Text + "px;height:" + adH.Text + "px\" />";
                }
            }
            else
            {
                strValue[5] = "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" height=\"" + adH.Text + "\" width=\"" + adW.Text + "\"><param name=\"quality\" value=\"high\" /><param name=\"movie\" value=\"" + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\" /><embed height=\"" + adH.Text + "\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" quality=\"high\" src=\"" + xc.UploadFolder + newFile.Substring(5, 8) + "/" + newFile + "\" type=\"application/x-shockwave-flash\" width=\"" + adW.Text + "\"></embed></object>";
            }
        }
        else
        {
            strValue[5] = adCode.Text;
        }
        strValue[6] = pDate.Text;
        strValue[7] = eDate.Text;
        strValue[8] = DateTime.Now.ToString();
        strValue[9] = xc.GetIP();
        strValue[10] = xc.adminID;

        if (!string.IsNullOrEmpty(newFile as string) || !string.IsNullOrEmpty(adCode.Text as string))
        {
            xc.insertMyLog(xc.adminID, "admin", "发布广告");
            xc.divError("", db.InsertUpdateDB("ads", strDim, strValue, string.Empty), 350, 150, "default.aspx", "goto");
        }
        else
        {
            xc.divError("", "请上传广告图片或直接粘贴广告代码！", 350, 150, "add.aspx", "goto");
        }
    }
}