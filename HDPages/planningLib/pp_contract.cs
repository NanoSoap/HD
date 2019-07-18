using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.planningLib
{
    public class pp_contract:baseBusiness
    {
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by pc." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " pc.ID,pc.conname,pc.concode,pc.condate,pc.conserial,pc.partyA " +
                " from pp_contract pc " +
                " where pc.conname like '%" + dic["conname"].ToString().Trim() + "%' and pc.partyA like '%" + dic["partyA"].ToString().Trim() + "%') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string gettotalpage(Dictionary<string, string> dic)
        {
            string sqltext = " select count(pc.ID) " +
                " from pp_contract pc " +
                " where pc.conname like '%" + dic["conname"].ToString().Trim() + "%' and pc.partyA like '%" + dic["partyA"].ToString().Trim() + "%' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getEditdata(string strID)
        {
            string sqltext = " select pc.ID,pc.conname,pc.concode,pc.condate,pc.conserial,pc.partyA " +
                " from pp_contract pc " +
                " where cast(pc.ID as varchar(36))='"+ strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getmaxconcode()
        {
            string sqltext = "select case when concode is null then 1 else concode+1 end as concode " +
                    " from(select MAX(cast(Right(Rtrim(concode),4) as int)) as concode " +
                    " from pp_contract) as a";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }
    }
}
