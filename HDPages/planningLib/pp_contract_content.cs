using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.planningLib
{
    public class pp_contract_content:baseBusiness
    {
        public DataTable getBindDataAsdt(string strpid)
        {
            string sqltext = " select pcc.ID,pcc.pid,pcc.conpdname,pcc.conpdmodel,pcc.conpduint," +
                " pcc.conpdprice,pcc.conpdnumber,ISNULL(pcc.conpdnumber,0)*ISNULL(pcc.conpdprice,0) as conpdmoney," +
                " pcc.deliverydate,pcc.deliveryaddress "+
                " from pp_contract_content pcc " +
                " where cast(pcc.pid as varchar(36))='" + strpid + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getEditdata(string strID)
        {
            string sqltext = " select pcc.ID,pcc.pid,pcc.conpdname,pcc.conpdmodel,pcc.conpduint," +
                " pcc.conpdprice,pcc.conpdnumber,ISNULL(pcc.conpdnumber,0)*ISNULL(pcc.conpdprice,0) as conpdmoney," +
                " pcc.deliverydate,pcc.deliveryaddress " +
                " from pp_contract_content pcc " +
                " where cast(pcc.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
