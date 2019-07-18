using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HDPages.craftLib;
using FineUIPro;
using System.Data;

namespace HDpmw.craftdesign
{
    /// <summary>
    /// mechanicalDesignManageGrid 的摘要说明
    /// </summary>
    public class mechanicalDesignManageGrid : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string rowId = context.Request.QueryString["id"];
            //int rowIdInt = Convert.ToInt32(rowId);
            cd_mechanical mc = new cd_mechanical();
            JArray ja = new JArray();
            DataTable dt = mc.getEditdata(rowId);
            DataRow r = dt.Rows[0];
            for (int i = 0; i < 2; i++)
            {
                JArray jaItem = new JArray();

                if (i == 0)
                {
                    jaItem.Add("日期");
                    jaItem.Add(r["designdate"].ToString());
                    jaItem.Add(r["auditdate"].ToString());
                    jaItem.Add(r["normaldate"].ToString());
                    jaItem.Add(r["meetdate"].ToString());
                }
                else if (i == 1)
                {
                    jaItem.Add("人员");
                    jaItem.Add(r["designperson"].ToString());
                    jaItem.Add(r["auditperson"].ToString());
                }




                ja.Add(jaItem);
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(ja.ToString(Newtonsoft.Json.Formatting.None));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}