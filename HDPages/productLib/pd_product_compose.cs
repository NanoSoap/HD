using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.productLib
{
    public class pd_product_compose:baseBusiness
    {
        public DataTable getEditdata(string strpid, string strppid)
        {
            string sqltext = " select ID,pid,ppid,pptype,pdnumber,remarks,operater,systemdate,isdelid " +
                        " from pd_product_compose  " +
                        " where cast(pid as varchar(36))='" + strpid + "' and cast(ppid as varchar(36))='" + strppid + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
