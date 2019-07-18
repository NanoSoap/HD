using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.craftLib
{
    public class cd_integration : baseBusiness
    {
        #region page
        //分页get_node
        public DataTable getBindDataAsdtNode(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ig." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_integration ig " +
                " where ig.pid = '" + dic["kitID"].ToString().Trim() + "' ) as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //分页get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ig." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_integration ig " +
                " where ig.iname like '%" + dic["iname"].ToString().Trim() + "%' and ig.kitname like '%" + dic["kitname"].ToString() + "%' ) as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }


        //总列数get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ig." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_integration ig " +
                " where ig.iname like '%" + dic["iname"].ToString().Trim() + "%'  and ig.kitname like '%" + dic["kitname"].ToString() + "%'  ) as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        //总列数get_Node
        public DataTable getBindDataAsdtNode(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ig." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_integration ig " +
                " where ig.pid = '" + dic["kitID"].ToString().Trim() + "'  ) as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //编辑行get
        public DataTable getEditdata(string strID)
        {
            string sqltext = "SELECT [ID]" +
            "      ,[iname]														   " +
            "      ,[pid]														   " +
            "      ,[kitname]													   " +
            "      ,[kitcode]													   " +
            "      ,[icode]														   " +
            "      ,[itag]														   " +
            "      ,[rawtype]													   " +
            "      ,[rawsize]													   " +
            "      ,[nperraw]													   " +
            "      ,[nperdesk]													   " +
            "      ,[designperson]												   " +
            "      ,[auditperson]												   " +
            //筛选默认值日期
            "      ,nullif([normaldate],'1900-01-01 00:00:00.000') normaldate	   " +
            "      ,nullif([meetdate],'1900-01-01 00:00:00.000') meetdate		   " +
            "      ,nullif([designdate],'1900-01-01 00:00:00.000') designdate	   " +
            "      ,nullif([auditdate],'1900-01-01 00:00:00.000') auditdate		   " +
            "      ,[operater]													   " +
            "      ,[systemdate]												   " +
            "      ,[isdelid]													   " +
            "  FROM [HDPMWDB].[dbo].[cd_integration]								   " +
                " where cast(ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getClonedata(string kitID)
        {
            string sqltext = "SELECT [ID]" +
            "      ,[iname]														   " +
            "      ,[pid]														   " +
            "      ,[kitname]													   " +
            "      ,[kitcode]													   " +
            "      ,[icode]														   " +
            "      ,[itag]														   " +
            "      ,[rawtype]													   " +
            "      ,[rawsize]													   " +
            "      ,[nperraw]													   " +
            "      ,[nperdesk]													   " +
            "      ,[designperson]												   " +
            "      ,[auditperson]												   " +
            //筛选默认值日期
            "      ,nullif([normaldate],'1900-01-01 00:00:00.000') normaldate	   " +
            "      ,nullif([meetdate],'1900-01-01 00:00:00.000') meetdate		   " +
            "      ,nullif([designdate],'1900-01-01 00:00:00.000') designdate	   " +
            "      ,nullif([auditdate],'1900-01-01 00:00:00.000') auditdate		   " +
            "      ,[operater]													   " +
            "      ,[systemdate]												   " +
            "      ,[isdelid]													   " +
            "  FROM [HDPMWDB].[dbo].[cd_integration]								   " +
                " where pid='" + kitID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public bool existCard(string kitid)
        {
            string sqltext = "select * from cd_integration ig where ig.pid='" + kitid + "'";
            return DBSQL.ExecutesqlToint(sqltext, DBSQL.connstr) >= 0;
        }

        #endregion
    }
}
