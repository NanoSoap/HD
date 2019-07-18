using HDPages.productLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace HDpmw.productdesign
{
    /// <summary>
    /// pdlist 的摘要说明
    /// </summary>
    public class pdlist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //System.Threading.Thread.Sleep(2000);

            if (!string.IsNullOrEmpty(context.Request["productSearch"]))
            {
                string strpdtypename = context.Request["productSearch"];

                context.Response.ContentType = "text/plain";
                context.Response.Write(GetSearchmachinekitInfo(strpdtypename));
            }
        }

        public string GetSearchmachinekitInfo(string strpdtypename)
        {
            StringBuilder sb = new StringBuilder();

            pd_product pp= new pd_product();
            System.Data.DataTable dt = pp.getproduct(strpdtypename);

            foreach (DataRow r in dt.Rows)
            {
                sb.Append(r["name"].ToString().Trim() + ',');
            }

            return sb.ToString().TrimEnd(',');
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