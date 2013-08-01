using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InClude_getValidate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string codeType = Request["code"];
        if (string.IsNullOrEmpty(codeType as string))
        {
            codeType = "123";
        }
        ValidateCodeImg vCodeImg = new ValidateCodeImg();
        string verifyCode = vCodeImg.CreateVerifyCode(codeType, 6, 0);
        Session["ImgCode"] = verifyCode;
        System.Drawing.Bitmap bitmap = vCodeImg.CreateImage(verifyCode, false);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        Response.Clear();
        Response.ContentType = "image/Png";
        Response.BinaryWrite(ms.GetBuffer());
        bitmap.Dispose();
        ms.Dispose();
        ms.Close();
        Response.End();
    }
}
