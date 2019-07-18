using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.craftLib
{
    public class cd_thermal:baseBusiness
    {
        public DataTable getEditdata(string strID)
        {
            string sqltext = " select ID,pid,tname,tcode,mtag,kitweight,craftmethod," +
                        " normalperson,approveperson,designperson,auditperson," +
                        " normaldate,approvedate,designdate,auditdate,operater,systemdate,isdelid " +
                        " from cd_thermal  " +
                        " where cast(pid as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getmaxmcode()
        {
            string sqltext = "select case when tcode is null then 1 else tcode+1 end as tcode " +
                    " from(select MAX(cast(Right(Rtrim(tcode),4) as int)) as tcode " +
                    " from cd_thermal) as a";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public string[] clonethermal(string strnewID, string strtname, string strtcode, string strpid,string strusername, string strtid)
        {
            string[] sqltext = new string[3];

            sqltext[0] = " insert into cd_thermal " +
                    " (ID,pid,tname,tcode,mtag,kitweight,craftmethod,normalperson,approveperson,designperson,auditperson," +
                        " normaldate,approvedate,designdate,auditdate,operater) " +
                    " select '" + strnewID + "','"+ strpid + "','" + strtname + "','" + strtcode + "'," +
                        " mtag,kitweight,craftmethod,normalperson,approveperson,designperson,auditperson,"+
                        " normaldate,approvedate,designdate,auditdate,'"+ strusername + "' " +
                    " from cd_thermal " +
                    " where cast(ID as varchar(36))='" + strtid + "' ";

            sqltext[1] = " insert into cd_thermal_batch " +
                    " (ID,tid,batchnumber,batchtext,bdevice,stove_code,stovetemp,heattemp," +
                        " heattime,keeptime,coolmedia,cooltemp,cooltime,workhour,operater)" +
                    " select newid(),'" + strnewID + "',batchnumber,batchtext,bdevice,stove_code,stovetemp,heattemp," +
                        " heattime,keeptime,coolmedia,cooltemp,cooltime,workhour,'"+strusername + "' " +
                    " from cd_thermal_batch " +
                    " where cast(tid as varchar(36))='" + strtid + "' ";

            sqltext[2] = " insert into cd_thermal_indicator " +
                    " (ID,tid,iname,ivalue,icheck,operater)" +
                    " select newid(),'" + strnewID + "',iname,ivalue,icheck,'" + strusername + "' " +
                    " from cd_thermal_indicator " +
                    " where cast(tid as varchar(36))='" + strtid + "' ";

            return sqltext;
        }
    }
}
