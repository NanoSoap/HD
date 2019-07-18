using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;

namespace HDBusiness
{
    public class userMenu:baseBusiness
    {
        public DataTable getEditdata(string strusername)
        {
            string sqltext = " select um.ID,um.username,um.menuid,um.buttonid," +
                " sb.menuparent,sb.menuname,sb.nodeid,sb.sortcode,"+
                " ISNULL(sb.functionname,'') as functionname,sb.btnid " +
                " from userMenu um " +
                " left join (select sm.ID,sm.menuparent,sm.menuname,sm.nodeid,sm.sortcode," +
                    " smb.ID as sID,smb.functionname,smb.buttonid as btnid " +
                    " from systemMenu sm " +
                    " left join systemMenu_button smb on smb.pid=sm.ID ) " +
                    " as sb  on sb.ID=um.menuid and (sb.sID=um.buttonid or um.buttonid is null) " +
                " where um.username='" + strusername + "' " +
                " order by substring(sortcode,0,CHARINDEX(',', sortcode)),sb.menuname ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable gettreeMenudata(string strusername)
        {
            string sqltext = " select * from "+
                    " (select distinct substring(sortcode, 0, CHARINDEX(',', sortcode)) as id, sm.menuparent as nodetext,null as pid, null as code, "+
                    " substring(nodeid,0,CHARINDEX('_', nodeid)) as nodeid "+
                    " from usermenu um "+
                    " left join systemMenu sm on sm.ID = um.menuid "+
                    " where Rtrim(um.username)= '"+ strusername +"' "+
                    " union "+
                    " select distinct sortcode+' '+nodeid as id, sm.menuname as nodetext, substring(sortcode, 0, CHARINDEX(',', sortcode)) as pid," +
                    " substring(sortcode, CHARINDEX(',', sortcode) + 1, Len(Rtrim(sortcode)) - CHARINDEX(',', sortcode)) as code, sm.nodeid "+
                    " from usermenu um "+
                    " left join systemMenu sm on sm.ID = um.menuid "+
                    " where Rtrim(um.username)= '" + strusername + "') as t " +
                    " order by id,code ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
