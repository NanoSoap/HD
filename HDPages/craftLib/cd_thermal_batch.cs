using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.craftLib
{
    public  class cd_thermal_batch:baseBusiness
    {
        public DataTable getBindGridDataAsdt(string strtid)
        {
            string sqltext = "SELECT ID,tid,batchnumber,batchtext,bdevice,stove_code,stovetemp,heattemp," +
                    " heattime,keeptime,coolmedia,cooltemp,cooltime,workhour,operater,systemdate,isdelid " +
                    " FROM cd_thermal_batch " +
                    " where cast(tid as varchar(36))='" + strtid + "' " +
                    " order by batchnumber";

            return DBSQL.ExecutesqlTodt(sqltext,DBSQL.connstr);
        }

        public DataTable getEditdata(string strID)
        {
            string sqltext = "SELECT ID,tid,batchnumber,batchtext,bdevice,stove_code,stovetemp,heattemp," +
                    " heattime,keeptime,coolmedia,cooltemp,cooltime,workhour,operater,systemdate,isdelid " +
                    " FROM cd_thermal_batch " +
                        " where cast(ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
