using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDCode;

namespace HDpmw.common
{
    public partial class verificationCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strCaptchaImageText = YDCode.Rand.Str(5).Trim();
                Session["CaptchaImageText"] = strCaptchaImageText;
                string CaptchaImageText = strCaptchaImageText;
                MemoryStream ms = Rand.CreateCodeImg(CaptchaImageText);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
        }
    }
}