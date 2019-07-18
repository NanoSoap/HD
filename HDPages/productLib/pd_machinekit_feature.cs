using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.productLib
{
    public class pd_machinekit_feature:baseBusiness
    {
        public DataTable getBindGridDataAsdt(string strmtypeid,string strID)
        {
            string sqltext = "select pkms.ID as featureid,pkms.mpci as featurename, " +
                    " ISNULL(pmf.featurevalue,'') as featurevalue, pmf.pid " +
                    " from (select pks.ID, pkm.mpci,pks.subID " +
                    " from pd_kidclass_secondrec pks " +
                    " left join pd_kidclass_mainmpic pkm on pkm.ID = pks.mpciID) as pkms " +
                    " left join (select featureid,featurevalue,pid " +
                        " from pd_machinekit_feature " +
                        " where cast(pid as varchar(36))='"+ strID + "')" +
                        " as pmf on pmf.featureid = pkms.ID " +
                    " where cast(pkms.subID as varchar(36))='" + strmtypeid + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable checkrepeat()
        {
            string sqltext = " select distinct a.pid " +
                    " from pd_machinekit_feature a " +
                    " left join pd_machinekit_feature b on a.pid<> b.pid and a.featureid = b.featureid and Ltrim(Rtrim(a.featurevalue))= Ltrim(Rtrim(b.featurevalue)) " +
                    " group by a.pid " +
                    " having count(b.id) = count(a.pid) ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
