using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace HDpmw
{
     ///<summary>
     ///增加*.aspx页面登录状态判断,HttpModule处理效果更好
     ///</summary> 
   public class ydHttpmodule : IHttpModule,IReadOnlySessionState
   { 
       public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(PreRequestHandlerExecute);
        }

        void PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication ha = (HttpApplication)sender;

            string path = ha.Context.Request.Url.ToString();

            //排除首页
            if (path.IndexOf("index.aspx") > 0)
            {

            }
            else if (ha.Context.Session["UserID"] == null || ha.Context.Session["UserID"].ToString() == "")//验证是否登录   
            {
                ha.Context.Response.Write("<script>alert('登录失效，请重新登录！');url='" + ha.Context.Request.ApplicationPath + "/index.aspx';if(window.parent!=null){window.parent.location=url;}else{this.location=url;};</script>");

                //ha.Context.Response.Redirect(ha.Context.Request.ApplicationPath+ "/RedirectIndex.html" + (isRedircetPre ? "?url=" + path :""));

                ha.Context.Response.End();
            }
        }
    }

   // Web.Config添加如下：
   //<httpModules>
   //  <addname="NewModule"type="MyHandler.NewModule"></add>
   //</httpModules>




}
