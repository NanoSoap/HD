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
    /// mklist 的摘要说明
    /// </summary>
    public class sklist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //System.Threading.Thread.Sleep(2000);

            

            if (!string.IsNullOrEmpty(context.Request["machinekitSearch"]))
            {
                string strmtypename = context.Request["machinekitSearch"];

                context.Response.ContentType = "text/plain";
                context.Response.Write(GetSearchmachinekitInfo(strmtypename));
            }
        }

        public string GetSearchmachinekitInfo(string strmtypename)
        {
            StringBuilder sb = new StringBuilder();

            pd_kidclass_secondary pm = new pd_kidclass_secondary();
            System.Data.DataTable dt = pm.getmachinekit(strmtypename);

            foreach (DataRow r in dt.Rows)
            {
                sb.Append(r["classname"].ToString().Trim() + ',');
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