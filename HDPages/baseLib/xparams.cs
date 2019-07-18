using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;

namespace HDBusiness
{
    public class xparams: baseBusiness
    {
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by x." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " x.ID,x.paramcode,x.paramname,x.paramvalue,x.isdelid " +
                " from xparams x " +
                " where x.paramname like '%" + dic["paramname"].ToString().Trim() + "%' and x.paramvalue like '%" + dic["paramvalue"].ToString().Trim() + "%') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by x." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " x.ID,x.paramcode,x.paramname,x.paramvalue,x.isdelid " +
                " from xparams x " +
                " where x.paramname like '%" + dic["paramname"].ToString().Trim() + "%' and x.paramvalue like '%" + dic["paramvalue"].ToString().Trim() + "%') as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string strID)
        {
            string sqltext = "select x.ID,x.paramcode,x.paramname,x.paramvalue,x.isdelid " +
                " from xparams x " +
                " where cast(x.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getparamData(string strCode)
        {
            string sqltext = "select x.paramvalue " +
                " from xparams x " +
                " where paramcode='" + strCode + "' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public string getCodedata()
        {
            string sqltext = " select case when paramcode is null then 1 else paramcode+1 end as paramcode " +
                    " from (select MAX(cast(Right(Rtrim(x.paramcode),3) as int)) as paramcode " +
                " from xparams x ) as a ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }
    }
}
