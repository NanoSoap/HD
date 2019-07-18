using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;
using System.Data;
using HDBusiness;

namespace HDPages.productLib
{
    public class pd_kidclass_secondrec : baseBusiness
    {
        public DataTable getEditdata(string mainID)
        {
            string sqltext = " select ID,mpci " +
                " from secondrecview " +
                " where cast(subID as varchar(36))='" + mainID + "'";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.mpci,ul.adddate " +
                " from secondrecview ul " +
                " where ul.subID = '" + dic["subID"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.mpci,ul.adddate " +
                " from secondrecview ul " +
                " where ul.subID = '" + dic["subID"].ToString().Trim() + "') as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
