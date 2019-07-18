using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;

namespace HDBusiness
{
    public class systemMenu:baseBusiness
    {
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by sm." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " sm.ID,sm.menuparent,sm.menuname,sm.nodeid,sm.sortcode " +
                " from systemMenu sm " +
                " where sm.menuname like '%" + dic["menuname"].ToString().Trim() + "%') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by sm." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " sm.ID,sm.menuparent,sm.menuname,sm.nodeid,sm.sortcode " +
                " from systemMenu sm " +
                " where sm.menuname like '%" + dic["menuname"].ToString().Trim() + "%') as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string strID)
        {
            string sqltext = " select sm.ID,sm.menuparent,sm.menuname,sm.nodeid,sm.sortcode " +
                " from systemMenu sm " +
                " where cast(sm.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getBindTreeDataAsdt()
        {
            string sqltext = "select 'systemmenu' as id,'系统菜单' as nodetext,null as pid "+
                    " union "+
                    " select distinct substring(sortcode,0,CHARINDEX(',', sortcode)) as id,menuparent as nodetext,'systemmenu' as pid "+
                    " from systemMenu "+
                    " union "+
                    " select cast(ID as varchar(36)) as id,menuname as nodetext,substring(sortcode, 0, CHARINDEX(',', sortcode)) as pid "+
                    " from systemMenu "+
                    " union "+
                    " select cast(ID as varchar(36)) as id,functionname as nodetext,cast(pid as varchar(36)) as pid "+
                    " from systemMenu_button ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
