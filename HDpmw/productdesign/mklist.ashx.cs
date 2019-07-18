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
    public class mklist : IHttpHandler
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

            pd_machinekit pm = new pd_machinekit();
            System.Data.DataTable dt = pm.getmachinekit(strmtypename);

            foreach (DataRow r in dt.Rows)
            {
                sb.Append(r["mname"].ToString().Trim() + ',');
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