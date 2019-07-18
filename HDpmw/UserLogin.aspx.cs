using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using HDBusiness;

namespace HDpmw
{
    public partial class UserLogin:Page
    { 
        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
             ;
        }

        #endregion

        #region 登陆验证
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Session["CaptchaImageText"]==null)
            {
                Response.AddHeader("Refresh", "0");
            }

            if (txtverifycode.Value.Trim().ToLower()!= Session["CaptchaImageText"].ToString().ToLower())
            {
                lblMessage.Text = "验证码错误！";
                return;
            }

            userLogin ul = new userLogin();
            string strtxtUserName = txtUserName.Value.Trim();
            string strtxtPassword = txtPassword.Value.Trim();

            if(strtxtUserName.Length>10 || strtxtPassword.Length>10)
            {
                return;
            }

            if (ul.isExistdata("userLogin", "username", strtxtUserName, "ID").Trim() == "")
            {
                lblMessage.Text = "用户名错误！";
            }
            else
            {
                if (ul.isExistdata("userLogin", "password", strtxtPassword, "ID").Trim() == "")
                {
                    lblMessage.Text = "密码错误！";
                }
                else
                {
                    Session["UserName"] = strtxtUserName;
                    Session["Password"] = strtxtPassword;

                    lblMessage.Text = "登陆成功！";

                    Response.Redirect("index.aspx");
                }
            }
        }

        #endregion

    }
}
