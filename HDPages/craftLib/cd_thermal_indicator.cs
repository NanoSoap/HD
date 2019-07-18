using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.craftLib
{
    public class cd_thermal_indicator:baseBusiness
    {
        public DataTable getBindGridDataAsdt(string strtid)
        {
            string sqltext = "SELECT ID,tid,iname,ivalue,icheck,operater,systemdate,isdelid "+
                    " FROM cd_thermal_indicator " +
                    " where cast(tid as varchar(36))='" + strtid + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getEditdata(string strID)
        {
            string sqltext = " select ID,tid,iname,ivalue,icheck,operater,systemdate,isdelid " +
                        " from cd_thermal_indicator  " +
                        " where cast(ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
