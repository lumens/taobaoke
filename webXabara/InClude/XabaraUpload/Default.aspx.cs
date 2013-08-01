using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class InClude_eWebEditor_XabaraUpload_Default : System.Web.UI.Page
{
    XabaraCom xc = new XabaraCom();
    dbDataFunction db = new dbDataFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        xc.CheckAdminLogin(string.Empty);

        if (!IsPostBack)
        {
            flag.Items.Add(new ListItem("图片：" + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadImgType"), "Img"));
            flag.Items.Add(new ListItem("文件：" + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadFileType"), "File"));
            flag.Items.Add(new ListItem("Flash：" + XmlReader.GetConfig(Server.MapPath("~/xabara.config"), "UploadFlashType"), "Flash"));
            this.indexPicSet.Items.Add(new ListItem("== 上传图片后可选择其中一张作为首页面的预览图 ==", string.Empty));
            string upImgTemp = string.Empty;

            int imgCount = 0;
            if (xc.SafeSql(Request["postType"]).Equals("edit")) //修改信息
            { 
                string strSql = "select fileName from upFileName where fileFlag='Img' and fileID='" + Session["upFileID"].ToString() + "' order by Fid ASC ";
                DataTable dt = db.getDataTable(strSql);

                this.indexPicSet.Items.Clear();

                imgCount = dt.Rows.Count;

                if (imgCount > 0)
                {
                    this.indexPicSet.Items.Add(new ListItem("== 选择或取消页面预览图 ==", string.Empty));
                }

                if (imgCount == 0)
                {
                    this.indexPicSet.Items.Add(new ListItem("== 上传图片后可选择其中一张作为首页面的预览图 ==", string.Empty));
                }

                for (int i = 0; i < imgCount; i++)
                {
                    string fileNameStr = dt.Rows[i]["fileName"].ToString().Trim();
                    this.indexPicSet.Items.Add(new ListItem(fileNameStr, fileNameStr));
                }
                    this.indexPicSet.DataBind();           
            }

            if (!String.IsNullOrEmpty(Session["upPreFile"] as string) && imgCount > 0)
            {
                upImgTemp = Session["upPreFile"].ToString().Trim();

                this.indexPicSet.Items.FindByText(upImgTemp).Selected = true;
                this.indexPicSet.Items.FindByValue(upImgTemp).Selected = true;

                preImage.Visible = true;    //预览图
                preImage.ImageUrl = xc.UploadFolder.ToString() + upImgTemp.Substring(5, 8) + "/" + upImgTemp;
            }

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string RequestType = flag.Text.ToString().Trim();
        string RequestTable = xc.SafeChar(Request["fTable"].Trim());
        string UpFileReturnStr;
        string insertIframeCode = string.Empty;
        string fileName = uploadfile.FileName;

        if (RequestType.Equals("Img"))
        {
            if (WaterMark.Checked)
                UpFileReturnStr = xc.UploadFile(uploadfile, RequestType, true);
            else
                UpFileReturnStr = xc.UploadFile(uploadfile, RequestType, false);
        }
        else
            UpFileReturnStr = xc.UploadFile(uploadfile, RequestType, false);

        if (UpFileReturnStr.Equals("上传成功"))
        {
            //插入数据库
            string insertName = Session["NewFile"].ToString();
            
            string dimStr = "@fileID,@fileFlag,@fileTable,@fileName,@PostDate,@AdminIP,@AdminID";

            string[] strValue = new string[7];
            strValue[0] = Session["upFileID"].ToString(); //上传图片文件名ID关联"
            strValue[1] = RequestType;
            strValue[2] = RequestTable;
            strValue[3] = insertName;
            strValue[4] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strValue[5] = xc.GetIP();
            strValue[6] = xc.adminID;

            if (db.InsertUpdateDB("upFileName", dimStr, strValue, string.Empty).Equals("发布成功"))
            {
                UpFileReturnStr += " OK！";
            }
            else
            {
                UpFileReturnStr += " NO！";
            }

            switch (RequestType)
            {
                case "Img":
                    insertIframeCode = @"<p><img alt='' src='" + xc.UploadFolder + insertName.Substring(5, 8) + "/" + insertName + "' /></p>";
                    break;

                case "Flash":
                    insertIframeCode = "<p><object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0'><param name='quality' value='high' /><param name='movie' value='" + xc.UploadFolder + insertName.Substring(5, 8) + "/" + insertName + "' /><embed pluginspage='http://www.macromedia.com/go/getflashplayer' quality='high' src='" + xc.UploadFolder + insertName.Substring(5, 8) + "/" + insertName + "' type='application/x-shockwave-flash'></embed></object></p>";
                    break;

                default:
                    insertIframeCode = "<a target='_blank' href=" + xc.UploadFolder + insertName.Substring(5, 8) + "/" + insertName + ">" + insertName + "</a>";
                    break;
            }
            insertIframeCode = insertIframeCode + "<p>&nbsp;</p>";

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "ClientScriptBlock", "<script>parent.CKEDITOR.instances.txtContent.insertHtml(\"" + insertIframeCode + "\");</script>");

            lblinfo.Text = uploadfile.FileName.ToString() + "&nbsp;" + UpFileReturnStr;

            if (RequestType.Equals("Img"))
            {
                indexPicSet.Items.Add(new ListItem(insertName, insertName));
                preImage.Visible = true;    //预览图
                delPreImg.Visible = true;   //删除图片
                preImage.ImageUrl = xc.UploadFolder.ToString() + insertName.Substring(5, 8) + "/" + insertName;
            }
        }
        else
            lblinfo.Text = UpFileReturnStr;       
    }

    protected void indexPicSet_SelectedIndexChanged(object sender, EventArgs e)
    {
        string indexPic = indexPicSet.Text.Trim();
        Session.Add("upPreFile", indexPic);
	    lblinfo.Text=string.Empty;        

        this.indexPicSet.Items.Clear(); 
        
        string strSql = "select fileName from upFileName where fileFlag='Img' and fileID='" + Session["upFileID"].ToString() + "' order by Fid ASC ";
        DataTable dt = db.getDataTable(strSql);

        this.indexPicSet.Items.Add(new ListItem("== 选择或取消页面预览图 ==", string.Empty));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.indexPicSet.Items.Add(new ListItem(dt.Rows[i]["fileName"].ToString().Trim(), dt.Rows[i]["fileName"].ToString().Trim()));
        }
        this.indexPicSet.DataBind();

        if (!string.IsNullOrEmpty(indexPic as string))
        {
            this.indexPicSet.Items.FindByText(indexPic).Selected = true;
            this.indexPicSet.Items.FindByValue(indexPic).Selected = true;

            preImage.Visible = true;    //预览图
            preImage.ImageUrl = xc.UploadFolder.ToString() + indexPic.Substring(5, 8) + "/" + indexPic;
            imgPreBottom.Visible = true;
            delPreImg.Visible = true;
        }
        else
        {
            preImage.Visible = false;    //预览图
            imgPreBottom.Visible = false;
            delPreImg.Visible = false;

            Session.Add("upPreFile", string.Empty); //清空预选图
        }
    }

    protected void imgPreBottom_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(indexPicSet.Text.Trim() as string))
        {
            string insertIframeImgCode = "<p style='text-align: center;'><img border=0 src='" + xc.UploadFolder + indexPicSet.Text.Trim().Substring(5, 8) + "/" + indexPicSet.Text.Trim() + "'></p>";
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "ClientScriptBlock", "<script>parent.CKEDITOR.instances.txtContent.insertHtml(\"" + insertIframeImgCode + "\");</script>");
        }
        else
        {
            lblinfo.Text = "请选择相关图片后再点击重新插入按钮！";
        }
    }

    protected void delPreImg_Click(object sender, EventArgs e)
    {
        string picName = indexPicSet.Text.Trim();
        if (string.IsNullOrEmpty(picName as string))
        {
            lblinfo.Text = "请选择相关图片后再点击删除按钮！";
        }
        else
        {
            this.indexPicSet.Items.Remove(new ListItem(picName, picName));
            Session.Add("upPreFile", string.Empty); //清空预选图

            xc.delFile(picName);

            lblinfo.Text = db.DelDB("upFileName", "fileName", picName, string.Empty, false, string.Empty);
        }
    }   
}
