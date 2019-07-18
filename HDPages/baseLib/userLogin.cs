using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;

namespace HDBusiness
{
    public class userLogin:baseBusiness
    {
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.department,ul.fullname,ul.staffno,ul.username,ul.password,ul.sex,ul.birdate,ul.address,ul.telephone," +
                " ul.regperson,ul.regdate "+
                " from userLogin ul " +
                " where ul.username like '%" + dic["username"].ToString().Trim() + "%' and ul.address like '%" + dic["address"].ToString().Trim() + "%') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.department,ul.fullname,ul.staffno,ul.username,ul.password,ul.sex,ul.birdate,ul.address,ul.telephone," +
                " ul.regperson,ul.regdate " +
                " from userLogin ul " +
                " where ul.username like '%" + dic["username"].ToString().Trim() + "%' and ul.address like '%" + dic["address"].ToString().Trim() + "%') as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string strID)
        {
            string sqltext = "select ul.ID,ul.staffno,ul.department, ul.fullname,ul.username,ul.password,ul.sex,ul.birdate,ul.address,ul.telephone," +
                " ul.regperson,ul.regdate " +
                " from userLogin ul " +
                " where cast(ul.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getUserinfo(string username)
        {
            string sqltext = "select ul.ID,fullname,ul.department,ul.staffno,ul.username,ul.password,ul.sex,ul.birdate,ul.address,ul.telephone," +
                " ul.regperson,ul.regdate " +
                " from userLogin ul " +
                " where ul.username='" + username + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

    }
}
