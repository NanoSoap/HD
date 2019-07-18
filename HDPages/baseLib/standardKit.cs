using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YDCode;

namespace HDBusiness
{
     public class standardKit:baseBusiness
    {
        //分页get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by sk." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " sk.ID,sk.sname,sk.scode,sk.specification,sk.material"+
                " from pd_standardkit sk " +
                " where sk.sname like '%" + dic["sname"].ToString().Trim() + "%' and sk.isdelid=1 ) as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //总列数get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by sk." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " sk.ID,sk.sname,sk.scode,sk.specification,sk.material" +
                " from pd_standardkit sk " +
                " where sk.sname like '%" + dic["sname"].ToString().Trim() + "%' and sk.isdelid=1 ) as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //编辑行get
        public DataTable getEditdata(string strID)
        {
            string sqltext = "select sk.ID,sk.sname,sk.scode,sk.specification,sk.material" +
                " from pd_standardkit sk " +
                " where cast(sk.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
