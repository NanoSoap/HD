using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;
using System.Data;

namespace HDBusiness
{
    public class pd_kidclass_mainmpic : baseBusiness
    {
        public DataTable getEditdata(string mainID)
        {
            string sqltext = " select sm.ID,sm.mpci " +
                " from pd_kidclass_mainmpic sm " +
                " where cast(sm.mainID as varchar(36))='" + mainID + "' order by sm.orderint";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        
        public DataTable getEditdatabyID(string ID)
        {
            string sqltext = " select sm.ID,sm.mpci,sm.explain " +
                " from pd_kidclass_mainmpic sm " +
                " where cast(sm.ID as varchar(36))='" + ID + "'";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string getCodedata(string mainID,string mpci)
        {
            string sqltext = " select ID from pd_kidclass_mainmpic where cast(mainID as varchar(36))='" + mainID + "' and mpci='" + mpci + "'";
                
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.mainID,ul.mpci,ul.explain,ul.orderint,ul.adddate " +
                " from pd_kidclass_mainmpic ul " +
                " where ul.mainID = '" + dic["mID"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

    }
}
