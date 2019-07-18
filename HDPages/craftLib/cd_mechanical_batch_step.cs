using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.craftLib
{
   public class cd_mechanical_batch_step:baseBusiness
    {


        //分页get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by step." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical_batch_step step " +
                " where step.batchid = '" + dic["batchid"].ToString().Trim() + "' ) as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }


        //总列数get
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by step." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical_batch_step step " +
                " where step.batchid ='" + dic["batchid"].ToString().Trim() + "' ) as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        //克隆
        public string clone(string batch_from,string batch_to)
        {
            return "INSERT INTO [dbo].[cd_mechanical_batch_step]	 " +
                    "           ([ID]								 " +
                    "           ,[batchid]							 " +
                    "           ,[snumber]							 " +
                    "           ,[stext]							 " +
                    "           ,[stool]							 " +
                    "           ,[operater]							 " +
                    "           ,[systemdate]						 " +
                    "           ,[isdelid])							 " +
                    "			SELECT NEWID()						 " +
                    "			  ,'"+batch_to+"'						 " +
                    "			  ,[snumber]						 " +
                    "			  ,[stext]							 " +
                    "			  ,[stool]							 " +
                    "			  ,[operater]						 " +
                    "			  ,[systemdate]						 " +
                    "			  ,[isdelid]						 " +
                    "		  FROM [dbo].[cd_mechanical_batch_step] where batchid  ='"+batch_from+"'	 ";
        }
    }
}
