using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using FineUIPro;
using AspNet = System.Web.UI.WebControls;


namespace HDpmw
{
    public class PageBase : System.Web.UI.Page
    {
        #region OnInit

        private string sessionUserName { get; set; }
        private string sessionPassword { get; set; }

        public string SessionUserName
        {
            get { return this.sessionUserName; }
        }
        public string SessionPassword
        {
            get { return this.sessionPassword; }
        }

        protected override void OnInit(EventArgs e)
        {
            var pm = PageManager.Instance;
            if (pm != null)
            {
                HttpCookie themeCookie = Request.Cookies["Theme_Pro"];
                if (themeCookie != null)
                {
                    string themeValue = themeCookie.Value;

                    // 是否为内置主题
                    if (IsSystemTheme(themeValue))
                    {
                        pm.CustomTheme = String.Empty;
                        pm.Theme = (Theme)Enum.Parse(typeof(Theme), themeValue, true);
                    }
                    else
                    {
                        pm.CustomTheme = themeValue;
                    }
                }

                if (Constants.IS_BASE)
                {
                    pm.EnableAnimation = false;
                }
            }

            base.OnInit(e);

            //测试用
            Session["UserName"] = "开发测试";
            Session["Password"] = "123";

            //登陆验证--非cookie
            if (base.Session["UserName"] == null || Session.Count < 1)
            {
                //Response.Redirect("UserLogin.aspx");

                Response.Write("<script>var url='" + Request.ApplicationPath + "UserLogin.aspx';" +
                    "if(window.parent!=null){window.parent.location=url;}else{this.location=url;};</script>");

                //string url = Request.Url.ToString();
                //string url1 = url.Remove(0, 7);
                //var str = url1.Split('/');
                //int a = str.Length;
                //if (a > 2)
                //{
                //    Response.Redirect("../404.html");
                //}
                //else
                //{
                //    Response.Redirect("UserLogin.aspx");
                //}
            }
            else
            {
                this.sessionUserName = Session["UserName"].ToString();
                this.sessionPassword = Session["Password"].ToString();
            }
        }

        private bool IsSystemTheme(string themeName)
        {
            themeName = themeName.ToLower();
            string[] themes = Enum.GetNames(typeof(Theme));
            foreach (string theme in themes)
            {
                if (theme.ToLower() == themeName)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}
