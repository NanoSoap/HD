using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;
using System.Data;

namespace HDPages.productLib
{
    public class pd_compongall_compose : baseBusiness
    {
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by d." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " d.ID,idname.name,d.number,d.remarks,d.specification,d.adddate from " +
                "(select a.id as ID,a.sname name from pd_standardkit a union select b.id as ID,b.mname name from pd_machinekit b union select c.id as ID,c.oname from pd_outbuykit c where c.otype='零件' union select d.id as ID,d.comname from pd_compongall d ) as idname,pd_compongall_compose d " +
                " where idname.ID=d.composeID and d.componID ='" + dic["componID"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection,string type)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by d." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " d.ID,idname.pID,idname.name,d.number,d.remarks,d.specification,d.adddate from " +
                " (select a.id as pID,a.sname name from pd_standardkit a union select b.id as pID,b.mname name from pd_machinekit b union select c.id as pID,c.oname from pd_outbuykit c where c.otype='零件' union select d.id as pID,d.comname from pd_compongall d ) as idname,pd_compongall_compose d " +
                " where idname.pID=d.composeID and d.componID = '" + dic["componID"].ToString().Trim() + "' and d.type='"+ type + "') as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string strID)
        {
            string sqltext = " select ID,number,specification,remarks,systemdate,isdelid,adddate " +
                        " from pd_compongall_compose  " +
                        " where cast(ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdataByID(string strID)
        {
            string sqltext = " select ID,composeID,number,specification,remarks,systemdate,isdelid,adddate,type " +
                        " from pd_compongall_compose  " +
                        " where cast(componID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdataByID(string strID,string ID)
        {
            string sqltext = " select ID,composeID,number,specification,remarks,systemdate,isdelid,adddate,type " +
                        " from pd_compongall_compose  " +
                        " where cast(componID as varchar(36))='" + strID + "' and cast(composeID as varchar(36))='" + ID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string DeleteDatasql(string tablename, string strfieldname, string strfieldvalue, string strfieldname1, string strfieldvalue1)
        {
            string sqltext = "delete from " + tablename + " where " + strfieldname + "='" + strfieldvalue + "' and "+ strfieldname1 + "='"+ strfieldvalue1 + "'";

            return sqltext;
        }
        public string AddDatasql(string tablename, Dictionary<string,string> dic)
        {
            string sqltext = "INSERT INTO pd_compongall (ID,comname,comcode,remarks,standards,designer,checker,examiner,drawer,stanarder,specifications,drawerdate,comtype,isdelid,operater,systemdate)" +
                " VALUES ('"+ dic["ID"].ToString().Trim() + "','" + dic["comname"].ToString().Trim() + "','" 
                + dic["comcode"].ToString().Trim() + "','" + dic["remarks"].ToString().Trim() + "','" + dic["standards"].ToString().Trim() + "','" + dic["designer"].ToString().Trim() + "','" 
                + dic["checker"].ToString().Trim() + "','" + dic["examiner"].ToString().Trim() + "','" + dic["drawer"].ToString().Trim() + "','" + dic["stanarder"].ToString().Trim() + "','" 
                + dic["specifications"].ToString().Trim() + "','" + dic["drawerdate"].ToString().Trim() + "','" + dic["comtype"].ToString().Trim() + "','" + dic["isdelid"].ToString().Trim() + "','" + dic["operater"].ToString().Trim() + "','" + dic["systemdate"].ToString().Trim() + "')";

            return sqltext;
        }
    }
}
