using HDBusiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YDCode;

namespace HDPages.productLib
{
    public class pd_outbuykit : baseBusiness
    {
        #region photo
        public string getPhotoID(string standardKitID)
        {
            string sqltext = "select p.id from pd_outbuykit ok join pd_photo p on ok.ID=p.pid" +
                " where ok.ID='" + standardKitID + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public string getPhotoFileName(string standardKitID)
        {
            string sqltext = "select p.filename from pd_outbuykit ok join pd_photo p on ok.Id=p.pid " +
                " where ok.ID='" + standardKitID + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);

        }
        #endregion

        #region 分页
        //分页get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ok." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from pd_outbuykit ok " +
                " where ok.oname like '%" + dic["oname"].ToString().Trim() + "%' ) as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //总列数get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ok." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from pd_outbuykit ok " +
                " where ok.oname like '%" + dic["oname"].ToString().Trim() + "%'  ) as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //编辑行get
        public DataTable getEditdata(string strID)
        {
            string sqltext = "select * " +
                " from pd_outbuykit ok " +
                " where cast(ok.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        #endregion
    }
}
