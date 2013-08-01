using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webXabara_frame_regSoft : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();

    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin("xabaraCOM");

        if (!IsPostBack)
        {
            myCode.Text = xc.regUserCode();

            EncryptDecrypt ed = new EncryptDecrypt();
            string soft = ed.softName();
            string[] softName = soft.Split(new char[] { '_' });
            soft = softName[0];
            softLink.NavigateUrl = "http://www.zdianpu.com/soft/" + soft + "/server.aspx";
        } 
    }

    protected void sysSet_Click(object sender, EventArgs e)
    {
        string str = regCode.Text.Trim();
        if (!string.IsNullOrEmpty(str as string))
        {
            xc.xmlSave("regCode", str);

            Response.Redirect("/", true);
        }
    }
}