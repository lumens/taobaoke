using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

public partial class WebXabara_frame_otherSet : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    public string soft = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("xabaraCOM");
        if (!IsPostBack)
        {
            EncryptDecrypt ed = new EncryptDecrypt();
            soft = ed.softName();
            string[] softName = soft.Split(new char[] { '_' });
            soft = softName[0];

            string pathFlie = Server.MapPath("/xabara.config");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathFlie);

            taobaoKeAppKey.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='taobaoKeAppKey']").Attributes["value"].Value;
            taobaoKeAppSecret.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='taobaoKeAppSecret']").Attributes["value"].Value;
            taobaoKeAlimamaID.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='taobaoKeAlimamaID']").Attributes["value"].Value;
            hotSearch.Text = xmlDoc.DocumentElement.SelectSingleNode("//add[@key='hotSearch']").Attributes["value"].Value; 

            xmlDoc = null;
        }
    }

    protected void sysSet_Click(object sender, EventArgs e)
    {
        string pathFlie = Server.MapPath("/xabara.config");
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
                            case "taobaoKeAppKey":
                                xeStr = taobaoKeAppKey.Text.Trim();
                                break;
                            case "taobaoKeAppSecret":
                                xeStr = taobaoKeAppSecret.Text.Trim();
                                break;
                            case "taobaoKeAlimamaID":
                                xeStr = taobaoKeAlimamaID.Text.Trim();
                                break;
                            case "hotSearch":
                                xeStr = hotSearch.Text.Trim();
                                break;                            
                        }
                        xe.Attributes["value"].Value = xeStr;
                    }
                }
            }
        }

        xmlDoc.Save(pathFlie);
        xmlDoc = null;

        //xc.insertMyLog(xc.adminID, "admin", "网店参数设置");

        Response.Redirect("otherSet.aspx", true);
    }
}
