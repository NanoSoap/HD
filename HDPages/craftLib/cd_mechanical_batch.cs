using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YDCode;
using HDBusiness;
namespace HDPages.craftLib
{
    public class cd_mechanical_batch : baseBusiness
    {
        #region page
        //分页get_node
        public DataTable getBindDataAsdtNode(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by bh." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical_bh " +
                " where bh.kitname like '%" + dic["kitname"].ToString().Trim() + "%' ) as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //分页get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by bh." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical_batch bh " +
                " where bh.mid = '" + dic["mid"].ToString().Trim() + "' ) as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }


        //总列数get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by bh." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical_batch bh " +
                " where bh.mid ='" + dic["mid"].ToString().Trim() + "' ) as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        //总列数get_Node
        public DataTable getBindDataAsdtNode(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by bh." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical_batch bh " +
                " where bh.kitname like '%" + dic["kitname"].ToString().Trim() + "%'  ) as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //编辑行get
        public DataTable getEditdata(string strID)
        {
            string sqltext = "SELECT [ID]" +
            "      ,[mname]														   " +
            "      ,[pid]														   " +
            "      ,[kitname]													   " +
            "      ,[kitcode]													   " +
            "      ,[bhode]														   " +
            "      ,[mtag]														   " +
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
            "  FROM [HDPMWDB].[dbo].[cd_mechanical_batch]								   " +
                " where cast(ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        #endregion

        #region clone
        //克隆
        public bool clone(string mechanicalcard_from, string mechanicalcard_to)
        {
            string sqltext = "INSERT INTO[dbo].[cd_mechanical_batch]"
             + "        ([ID]                           "
             + "       ,[mid]                           "
             + "       ,[mname]                         "
             + "       ,[batchnumber]                   "
             + "       ,[batchname]                     "
             + "       ,[batchtext]                     "
             + "       ,[workshop]                      "
             + "       ,[batchsession]                  "
             + "       ,[bdevice]                       "
             + "       ,[btool]                         "
             + "       ,[operater]                      "
             + "       ,[systemdate]                    "
             + "       ,[isdelid])                      "
             + "                                        "
             + "                                        "
             + "       (SELECT NEWID()                  "
             + "       ,'" + mechanicalcard_to + "'         "
             + "       ,[mname]                         "
             + "       ,[batchnumber]                   "
             + "       ,[batchname]                     "
             + "       ,[batchtext]                     "
             + "       ,[workshop]                      "
             + "       ,[batchsession]                  "
             + "       ,[bdevice]                       "
             + "       ,[btool]                         "
             + "       ,[operater]                      "
             + "       ,[systemdate]                    "
             + "       ,[isdelid]                       "
             + "    FROM[dbo].[cd_mechanical_batch]     "
             + "    where mid = '" + mechanicalcard_from + "'  )    ";

            return DBSQL.ExecutesqlTobool(sqltext, DBSQL.connstr) > 0;
        }
        public string[] getnewIDlist(string newcard)
        {
            string sqltext = "select ID from cd_mechanical_batch where mid = '" + newcard + "'";

            List<string> ls = new List<string>();  //存一整列所有的值 
            DataTable dt = DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
            foreach (DataRow dr in dt.Rows)
            {
                ls.Add(dr["ID"].ToString());
            }
            return ls.ToArray();
        }
        public string[] getfromIDlist(string fromcard)
        {
            string sqltext = "select ID from cd_mechanical_batch where mid = '" + fromcard + "'";

            List<string> ls = new List<string>();  //存一整列所有的值 
            DataTable dt = DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
            foreach (DataRow dr in dt.Rows)
            {
                ls.Add(dr["ID"].ToString());
            }
            return ls.ToArray();
        }

        #endregion
    }
}
