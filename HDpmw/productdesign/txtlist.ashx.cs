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
    /// kclist 的摘要说明
    /// </summary>
    public class txtlist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //System.Threading.Thread.Sleep(2000);

            if (!string.IsNullOrEmpty(context.Request["machinekitSearch"]))
            {
                string strmtypename = context.Request["machinekitSearch"];
                context.Response.ContentType = "text/plain";
                context.Response.Write(GetSearchkidclassInfo(strmtypename));
            }
        }

        public string GetSearchkidclassInfo(string strmtypename)
        {
            StringBuilder sb = new StringBuilder();

            pd_compongall pm = new pd_compongall();
            if (strmtypename == "1")
            {
                System.Data.DataTable dt = pm.getkidclassbymach();

                foreach (DataRow r in dt.Rows)
                {
                    sb.Append(r["mname"].ToString().Trim() + ',');
                }
            }
            if (strmtypename == "2")
            {
                System.Data.DataTable dt = pm.getkidclassbystand();

                foreach (DataRow r in dt.Rows)
                {
                    sb.Append(r["sname"].ToString().Trim() + ',');
                }
            }
            if (strmtypename == "3")
            {
                System.Data.DataTable dt = pm.getkidclassbyoutbuy();

                foreach (DataRow r in dt.Rows)
                {
                    sb.Append(r["oname"].ToString().Trim() + ',');
                }
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