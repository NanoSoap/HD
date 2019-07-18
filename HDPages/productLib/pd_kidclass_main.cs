using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;
using System.Data;
using HDBusiness;

namespace HDPages.productLib
{
    public class pd_kidclass_main: baseBusiness
    {
        public DataTable getBindTreeDataAsdt()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'元零件特征库' as nodetext,null as pid,0 as orderint,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,mainname as nodetext,'systemmenu' as pid,orderint,1 as isclick,1 as nodemenu " +
                     " from pd_kidclass_main " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,classname as nodetext,cast(mainID as varchar(36)) as pid,orderint,2 as isclick,1 as nodemenu " +
                     " from pd_kidclass_secondary) as a order by orderint ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
   
        public DataTable getBindTreeDataAsdt(string classname)
        {
            string sqltext = "select ID,mainname from pd_kidclass_main where mainname='" + classname + "'";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string mainID)
        {
            string sqltext = " select sm.ID,sm.mainname,sm.orderint,sm.maincode,sm.adddate " +
                " from pd_kidclass_main sm " +
                " where cast(sm.ID as varchar(36))='" + mainID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string mainID,string name)
        {
            string sqltext = " select sm.ID,sm.mainname,sm.orderint,sm.maincode,sm.adddate " +
                " from pd_kidclass_main sm " +
                " where sm.mainname='" + name + "' and sm.ID<>'"+mainID+"'";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getkidclass()
        {
            string sqltext = "select mainname " +
                    " from pd_kidclass_main ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
