using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.baseInfoLib
{
    public class bi_heatdevice_repair: baseBusiness
    {
        public DataTable getEditdata(string strID)
        {
            string sqltext = " select bhdr.ID,bhdr.hdid,bhdr.hdrepairtime,bhdr.hdreworktime,bhdr.hderror,bhdr.hdrepaircontent" +
                " from bi_heatdevice_repair bhdr " +
                " where cast(bhdr.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getBindDataAsdt(string strhdid)
        {
            string sqltext = " select bhdr.ID,bhdr.hdid,bhdr.hdrepairtime,bhdr.hdreworktime,bhdr.hderror,bhdr.hdrepaircontent" +
                " from bi_heatdevice_repair bhdr " +
                " where cast(bhdr.hdid as varchar(36))='" + strhdid + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}