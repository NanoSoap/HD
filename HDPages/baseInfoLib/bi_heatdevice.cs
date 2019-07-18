using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.baseInfoLib
{
    public class bi_heatdevice:baseBusiness
    {
        public DataTable getheatdevice()
        {
            string sqltext = "select ID,hdname+'('+hdcode+')' as hdname " +
                    " from bi_heatdevice ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getmaxconcode()
        {
            string sqltext = "select case when hdcode is null then 1 else hdcode+1 end as hdcode " +
                    " from(select MAX(cast(Right(Rtrim(hdcode),3) as int)) as hdcode " +
                    " from bi_heatdevice) as a";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by bhd." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " bhd.ID,bhd.hdname,bhd.hdcode,bhd.hdtype,bhd.hdmodel,bhd.hdstatu " +
                " from bi_heatdevice bhd " +
                " where bhd.hdname like '%" + dic["hdname"].ToString().Trim() + "%' and bhd.hdtype like '%" + dic["hdtype"].ToString().Trim() + "%') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string gettotalpage(Dictionary<string, string> dic)
        {
            string sqltext = " select count(bhd.ID) " +
                " from bi_heatdevice bhd " +
                " where bhd.hdname like '%" + dic["hdname"].ToString().Trim() + "%' and bhd.hdtype like '%" + dic["hdtype"].ToString().Trim() + "%' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getEditdata(string strID)
        {
            string sqltext = " select bhd.ID,bhd.hdname,bhd.hdcode,bhd.hdtype,bhd.hdmodel,bhd.hdstatu " +
                " from bi_heatdevice bhd " +
                " where cast(bhd.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
