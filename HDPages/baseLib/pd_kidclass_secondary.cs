using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;
using System.Data;

namespace HDBusiness
{
    public class pd_kidclass_secondary : baseBusiness
    {
        public DataTable getBindTreeDataAsdt(string mainID)
        {
            string sqltext = "select ID,classname from pd_kidclass_secondary where cast(mainID as varchar(36))='" + mainID+"' order by orderint";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtname(string mainname)
        {
            string sqltext = "select ID,classname from pd_kidclass_secondary where classname='" + mainname + "'";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string strID)
        {
            string sqltext = " select sm.ID,sm.mainID,sm.classname,sm.orderint,sm.darycode,sm.adddate " +
                " from pd_kidclass_secondary sm " +
                " where cast(sm.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string mainID, string subID)
        {
            string sqltext = " select sm.ID,sm.mpci " +
                " from pd_kidclass_mainmpic sm " +
                " where cast(sm.mainID as varchar(36))='" + mainID + "' and mpci not in (select mpci from secondrecview where subID='" + subID + "')";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdatabyname(string strID,string name)
        {
            string sqltext = " select sm.ID,sm.mainID,sm.classname,sm.orderint,sm.darycode,sm.adddate " +
                " from pd_kidclass_secondary sm " +
                " where sm.classname='" + name + "' and sm.ID<>'" + strID + "'";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

    }
}
