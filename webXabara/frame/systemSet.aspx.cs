using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

public partial class WebXabara_frame_systemSet : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("xabaraCOM");
        if (!IsPostBack)
        {
            FormCheck fc = new FormCheck();
            emailRegularExpressionValidator.ValidationExpression = fc.RegExpressionValidator("email", 0, 0, false);
            emailRegularExpressionValidator.ErrorMessage = fc.RegExpressionValidator("email", 0, 0, true);
            telRegularExpressionValidator.ValidationExpression = fc.RegExpressionValidator("phone", 0, 0, false);
            telRegularExpressionValidator.ErrorMessage = fc.RegExpressionValidator("phone", 0, 0, true);
            faxRegularExpressionValidator.ValidationExpression = fc.RegExpressionValidator("phone", 0, 0, false);
            faxRegularExpressionValidator.ErrorMessage = fc.RegExpressionValidator("phone", 0, 0, true);
            mobileRegularExpressionValidator.ValidationExpression = fc.RegExpressionValidator("mobile", 0, 0, false);
            mobileRegularExpressionValidator.ErrorMessage = fc.RegExpressionValidator("mobile", 0, 0, true);
            urlRegExpress.ValidationExpression = fc.RegExpressionValidator("url", 0, 0, false);
            urlRegExpress.ErrorMessage = fc.RegExpressionValidator("url", 0, 0, true);

            string pathFlie = HttpContext.Current.Server.MapPath("/xabara.config");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathFlie);
            
            webName.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='webName']").Attributes["value"].Value;
            webDomains.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='webDomains']").Attributes["value"].Value;
            webBak.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='miibeian']").Attributes["value"].Value;
            webQQ.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='qq']").Attributes["value"].Value;
            wangwang.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='taobao']").Attributes["value"].Value;            
            webEmail.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='webEmail']").Attributes["value"].Value;
            webtel.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='webTel']").Attributes["value"].Value;
            webFax.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='webFax']").Attributes["value"].Value;
            webMobile.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='mobile']").Attributes["value"].Value;
            freePhone.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='freePhone']").Attributes["value"].Value;
            ips.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='ip']").Attributes["value"].Value;
            waterWords.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='waterMark']").Attributes["value"].Value;
            keyWords.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='KeyWord']").Attributes["value"].Value;
            mKeyWords.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='metaStr']").Attributes["value"].Value;
            titleMeta.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='titleMeta']").Attributes["value"].Value;
            countStr.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='countScript']").Attributes["value"].Value;

            ipAdd.Text = xc.GetIP();
        }
    }

    protected void waterImgButton_Click(object sender, EventArgs e)
    {
        if (waterImgFileUpload.HasFile)    //本地上传
        {
            string uploadFloderImg = "/images/";
            if (!Directory.Exists(Server.MapPath(uploadFloderImg))) //判断上传目录是否存在
            {
                Directory.CreateDirectory(Server.MapPath(uploadFloderImg));
            }

            string fileName = waterImgFileUpload.PostedFile.FileName.ToLower();
            if (fileName == string.Empty || fileName == null)
            {
                xc.divError("", "请选择文件后再上传！", 350, 150, "", "");
            }

            string fileExe = fileName.Substring(fileName.LastIndexOf(".") + 1); //取后缀名
            long UploadSize = xc.SafeNum(XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadImgSize"));  //上传大小
            if (!fileExe.Equals("png")) //判断该扩展名是否合法
            {
                xc.divError("", "请选择一张png格式的透明背景水印图！", 350, 150, "", "");
            }

            if (waterImgFileUpload.PostedFile.ContentLength > UploadSize) // 判断上传文件大小是否超过最大值
            {
                xc.divError("", "目前支持大小：" + Convert.ToString(UploadSize / 1048576) + "MB<br />当前大小为：" + Convert.ToString(waterImgFileUpload.PostedFile.ContentLength / 1048576) + "MB", 350, 150, "", "");
            }

            string UploadTemp = HttpContext.Current.Server.MapPath(@uploadFloderImg + "WaterMark.png");
            waterImgFileUpload.PostedFile.SaveAs(UploadTemp); //保存水印图片
            xc.divError("", "更换水印文件成功！", 350, 150, "", "");
        }
        else
        {
            xc.divError("", "请选择文件后再上传！", 350, 150, "", "");
        }
    }

    protected void sysSet_Click(object sender, EventArgs e)
    {
        string pathFlie = HttpContext.Current.Server.MapPath("/xabara.config");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(pathFlie);

        XmlNodeList topM = xmlDoc.DocumentElement.ChildNodes; //得到顶层节点列表
        foreach (XmlElement xElement in topM)
        {
            if (xElement.Name == "appSettings") //判断读取子层
            {
                XmlNodeList nodelist = xElement.ChildNodes; //得到该节点的子节点               
                if (nodelist.Count > 0)
                {
                    foreach (XmlNode xnode in nodelist)
                    {
                        XmlElement xe = (XmlElement)xnode;

                        string xeStr = xe.Attributes["value"].Value;
                        switch (xe.Attributes["key"].Value) //修改值
                        {
                            case "webName":
                                xeStr = webName.Text.Trim();
                                break;
                            case "webDomains":
                                xeStr = webDomains.Text.Trim();
                                break;
                            case "cookiesDomain":
                                string newDomain = string.Empty;
                                string[] domain = webDomains.Text.Trim().Split(new char[] { '.' });
                                int len = domain.Length;
                                for (int i = len - 1; i > 0; i--)
                                {
                                    newDomain += "." + domain[len - i];
                                }
                                xeStr = newDomain;
                                break;
                            case "miibeian":
                                xeStr = webBak.Text.Trim();
                                break;
                            case "qq":
                                xeStr = webQQ.Text.Trim();
                                break;
                            case "taobao":
                                xeStr = wangwang.Text.Trim();
                                break;
                            case "webEmail":
                                xeStr = webEmail.Text.Trim();
                                break;
                            case "webTel":
                                xeStr = webtel.Text.Trim();
                                break;
                            case "webFax":
                                xeStr = webFax.Text.Trim();
                                break;
                            case "mobile":
                                xeStr = webMobile.Text.Trim();
                                break;
                            case "freePhone":
                                xeStr = freePhone.Text.Trim();
                                break;
                            case "ip":
                                xeStr = ips.Text.Trim();
                                break;
                            case "waterMark":
                                xeStr = waterWords.Text.Trim();
                                break;
                            case "KeyWord":
                                xeStr = keyWords.Text.Trim();
                                break;
                            case "metaStr":
                                xeStr = mKeyWords.Text.Trim();
                                break;
                            case "titleMeta":
                                xeStr = titleMeta.Text.Trim();
                                break;
                            case "countScript":
                                xeStr = countStr.Text.Trim();
                                break;
                        }
                        xe.Attributes["value"].Value = xeStr;
                    }
                }
            }
        }

        xmlDoc.Save(pathFlie);
        Response.Redirect("systemSet.aspx", true);
    }

    protected void logoImgButton_Click(object sender, EventArgs e)
    {
        if (logoImgFile.HasFile)    //本地上传
        {
            string uploadFloderImg = "/images/";
            if (!Directory.Exists(Server.MapPath(uploadFloderImg))) //判断上传目录是否存在
            {
                Directory.CreateDirectory(Server.MapPath(uploadFloderImg));
            }

            string fileName = logoImgFile.PostedFile.FileName.ToLower();
            if (fileName == string.Empty || fileName == null)
            {
                xc.divError("", "请选择文件后再上传！", 350, 150, "", "");
            }

            string fileExe = fileName.Substring(fileName.LastIndexOf(".") + 1); //取后缀名
            long UploadSize = xc.SafeNum(XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadImgSize"));  //上传大小
            if (!fileExe.Equals("png")) //判断该扩展名是否合法
            {
                xc.divError("", "请选择一张背景透明PNG格式的logo图片！", 350, 150, "", "");
            }

            if (logoImgFile.PostedFile.ContentLength > UploadSize) // 判断上传文件大小是否超过最大值
            {
                xc.divError("", "目前支持大小：" + Convert.ToString(UploadSize / 1048576) + "MB<br />当前大小为：" + Convert.ToString(logoImgFile.PostedFile.ContentLength / 1048576) + "MB", 350, 150, "", "");
            }

            string UploadTemp = HttpContext.Current.Server.MapPath(@uploadFloderImg + "logo.png");
            logoImgFile.PostedFile.SaveAs(UploadTemp); //保存logo图片
            xc.divError("", "更换网站LOGO文件成功！", 350, 150, "", "");
        }
        else
        {
            xc.divError("", "请选择文件后再上传！", 350, 150, "", "");
        }
    }
}
