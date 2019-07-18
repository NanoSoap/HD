using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;

namespace HDBusiness
{
    public class systemMenu_button: baseBusiness
    {
        public DataTable getBindDataAsdt(string strpid)
        {
            string sqltext = " select smb.ID,smb.pid,smb.functionname,smb.buttonid " +
                " from systemMenu_button smb " +
                " where cast(smb.pid as varchar(36))='" + strpid + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getEditdata(string strID)
        {
            string sqltext = " select smb.ID,smb.pid,smb.functionname,smb.buttonid " +
                " from systemMenu_button smb " +
                " where cast(smb.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
