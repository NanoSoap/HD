using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;
using System.Data;

namespace HDPages.productLib
{
    public class pd_compongall : baseBusiness
    {
    


        public DataTable getBindTreeDataAsdt()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'元组件库' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct comtype as id,comtype as nodetext,'systemmenu' as pid,1 as isclick,1 as nodemenu " +
                     " from pd_compongall " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,comname as nodetext,comtype as pid,2 as isclick,2 as nodemenu " +
                     " from pd_compongall) as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbyproduct()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'产品库' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct pdtype as id,pdtype as nodetext,'systemmenu' as pid,1 as isclick,1 as nodemenu " +
                     " from pd_product " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,pdname as nodetext,pdtype as pid,2 as isclick,2 as nodemenu " +
                     " from pd_product) as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbyproduct(string Pname,string PID)
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'"+ Pname + "' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select cast(b.ID as varchar(36)) as id,b.pdname as nodetext,'systemmenu' as pid,2 as isclick,2 as nodemenu " +
                     " from pd_product_compose a,pd_product b where a.ppid=b.ID and a.pptype='产品' and a.pid='"+ PID + "') as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbymachine(string Pname, string PID)
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'" + Pname + "' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select cast(b.ID as varchar(36)) as id,b.mname as nodetext,'systemmenu' as pid,2 as isclick,2 as nodemenu " +
                     " from pd_product_compose a,pd_machinekit b where a.ppid=b.ID and a.pptype='元零件' and a.pid='" + PID + "') as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbystandard(string Pname, string PID)
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'" + Pname + "' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select cast(b.ID as varchar(36)) as id,b.sname as nodetext,'systemmenu' as pid,2 as isclick,2 as nodemenu " +
                     " from pd_product_compose a,pd_standardkit b where a.ppid=b.ID and a.pptype='标准件' and a.pid='" + PID + "') as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbyoutbuy(string Pname, string PID)
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'" + Pname + "' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select cast(b.ID as varchar(36)) as id,b.oname as nodetext,'systemmenu' as pid,2 as isclick,2 as nodemenu " +
                     " from pd_product_compose a,pd_outbuykit b where a.ppid=b.ID and a.pptype='外购件' and a.pid='" + PID + "') as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbycompongall(string Pname, string PID)
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'" + Pname + "' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select cast(b.ID as varchar(36)) as id,b.comname as nodetext,'systemmenu' as pid,2 as isclick,2 as nodemenu " +
                     " from pd_product_compose a,pd_compongall b where a.ppid=b.ID and a.pptype='元组件' and a.pid='" + PID + "') as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbymach()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'元零件库' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,mname as nodetext,'systemmenu' as pid,1 as isclick,1 as nodemenu " +
                     " from pd_machinekit) as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbystandard()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'标准件库' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct stype as id,stype as nodetext,'systemmenu' as pid,1 as isclick,1 as nodemenu " +
                     " from pd_standardkit where stype='零件' " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,sname as nodetext,stype as pid,1 as isclick,1 as nodemenu " +
                     " from pd_standardkit where stype='零件') as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbyoutbuy()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'外购件库' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct otype as id,otype as nodetext,'systemmenu' as pid,1 as isclick,1 as nodemenu " +
                     " from pd_outbuykit where otype='零件' " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,oname as nodetext,otype as pid,1 as isclick,1 as nodemenu " +
                     " from pd_outbuykit where otype='零件') as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindTreeDataAsdtbycompong()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'元组件库' as nodetext,null as pid,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,comname as nodetext,'systemmenu' as pid,1 as isclick,1 as nodemenu " +
                     " from pd_compongall) as a";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string isExistdata(string strfieldname, string strfieldvalue, string strID, bool isinsert)
        {//全库检查
            string strisinsert = "";
            if (isinsert)
            {
                strisinsert = " and cast(ID as varchar(36))<>'" + strID + "' ";
            }
            string sqltext = "select " + strfieldname + " from pd_compongall " +
                    " where " + strfieldname + " ='" + strfieldvalue + "' " + strisinsert;
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.comname,ul.adddate,ul.comcode,ul.remarks,ul.specification,ul.designer,ul.checker " +
                " from pd_compongall ul " +
                " where ul.comname like '%" + dic["comname"].ToString().Trim() + "%') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.comname,ul.adddate,ul.comcode,ul.remarks,ul.specification,ul.designer,ul.checker " +
                " from pd_compongall ul " +
                " where ul.comname like '%" + dic["comname"].ToString().Trim() + "%') as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdtbymechanical(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical ul " +
                " where ul.pid ='" + dic["pid"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdtbymechanical(Dictionary<string, string> dic, string strsort, string strSortDirection)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " * " +
                " from cd_mechanical ul " +
                " where ul.pid = '" + dic["pid"].ToString().Trim() + "') as s ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string getmaxmcode(string strmtypeid)
        {
            string sqltext = "select case when mcode is null then 1 else mcode+1 end as mcode " +
                    " from(select MAX(cast(Right(Rtrim(comcode), 3) as int)) as mcode " +
                    " from pd_compongall " +
                    ") as a";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdtbyproduct(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = "with cte as(select pid,ppid,pptype,pdnumber from pd_product_compose where pid='"+ dic["kitID"].ToString().Trim() + "' union all select a.pid,a.ppid,a.pptype,a.pdnumber from pd_product_compose a,cte b where a.pid=b.ppid)" +
                " select * from (select ROW_NUMBER() OVER(Order by "+strsort.Trim()+" "+strSortDirection+") as rowno,ID,name,number from " +
                " (select a.id as ID,a.sname as name from pd_standardkit a union select b.id as ID,b.mname name from pd_machinekit b union select c.id as ID,c.oname from pd_outbuykit c union select d.id as ID,d.comname from pd_compongall d union select e.ID,e.pdname from pd_product e) as idname," +
                " (select composeID,sum(number) as number from (" +
                " select a.composeID as composeID,isnull(b.pdnumber*a.number,0) as number from pd_compongall_compose a,cte b where a.componID=b.ppid and b.pid='" + dic["kitID"].ToString().Trim() + "'" +
                " union " +
                " select a.composeID as composeID,isnull(b.pdnumber*a.number,0) as number from pd_compongall_compose a," +
                " (select a.pid,a.ppid,a.pdnumber*b.pdnumber as pdnumber,a.pptype from cte a,cte b where a.pid=b.ppid) b where a.componID=b.ppid" +
                " union " +
                " select a.composeID as composeID,isnull(b.pdnumber*a.number,0) as number from pd_compongall_compose a," +
                " (select a.pid,a.ppid,a.pdnumber*b.pdnumber as pdnumber,a.pptype from cte a,cte b where a.pid=b.ppid) b where a.componID=b.ppid" +
                " union " +
                " select ppid as composeID,isnull(pdnumber,0) as number from cte where pid='" + dic["kitID"].ToString().Trim() + "'" +
                " union " +
                " select a.ppid as composeID,a.pdnumber*b.pdnumber as number from cte a,cte b where a.pid=b.ppid)" +
                " as sumste GROUP BY sumste.composeID) as ctenumber where idname.ID=ctenumber.composeID" +
                " ) as product where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getBindDataAsdtbyproduct(Dictionary<string, string> dic)
        {
            string sqltext = "with cte as(select pid,ppid,pptype,pdnumber from pd_product_compose where pid='" + dic["kitID"].ToString().Trim() + "' union all select a.pid,a.ppid,a.pptype,a.pdnumber from pd_product_compose a,cte b where a.pid=b.ppid)" +
                " select ROW_NUMBER() OVER(Order by number) as rowno,ID,name,number,ptype from " +
                " (select a.id as ID,a.sname as name from pd_standardkit a union select b.id as ID,b.mname name from pd_machinekit b union select c.id as ID,c.oname from pd_outbuykit c union select d.id as ID,d.comname from pd_compongall d union select e.ID,e.pdname from pd_product e) as idname," +
                " (select composeID,sum(number) as number,ptype from (" +
                " select a.composeID as composeID,isnull(b.pdnumber*a.number,0) as number,a.type as ptype from pd_compongall_compose a,cte b where a.componID=b.ppid and b.pid='" + dic["kitID"].ToString().Trim() + "'" +
                " union " +
                " select a.composeID as composeID,isnull(b.pdnumber*a.number,0) as number,a.type as ptype from pd_compongall_compose a," +
                " (select a.pid,a.ppid,a.pdnumber*b.pdnumber as pdnumber,a.pptype from cte a,cte b where a.pid=b.ppid) b where a.componID=b.ppid" +
                " union " +
                " select a.composeID as composeID,isnull(b.pdnumber*a.number,0) as number,a.type as ptype from pd_compongall_compose a," +
                " (select a.pid,a.ppid,a.pdnumber*b.pdnumber as pdnumber,a.pptype from cte a,cte b where a.pid=b.ppid) b where a.componID=b.ppid" +
                " union " +
                " select ppid as composeID,isnull(pdnumber,0) as number,pptype as ptype from cte where pid='" + dic["kitID"].ToString().Trim() + "'" +
                " union " +
                " select a.ppid as composeID,a.pdnumber*b.pdnumber as number,a.pptype as ptype from cte a,cte b where a.pid=b.ppid)" +
                " as sumste GROUP BY sumste.composeID,ptype) as ctenumber where idname.ID=ctenumber.composeID";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getEditdata(string strID)
        {
            string sqltext = " select ID,comname,comcode,remarks,designer,checker,stanarder,examiner,drawer,standards,specifications,operater,systemdate,isdelid,comtype,drawerdate,adddate " +
                        " from pd_compongall  " +
                        " where cast(ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string getPhotoFileName(string standardKitID)
        {
            string sqltext = "select p.filename from pd_compongall sk join pd_photo p on sk.ID=p.pid " +
                " where sk.ID='" + standardKitID + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);

        }
        public DataTable getEditdatabymachineki()
        {
            string sqltext = " select sm.ID,sm.mname,sm.mcode " +
                " from pd_machinekit sm " +
                " order by sm.systemdate";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string getEditdatabymachineki(string name)
        {
            string sqltext = "select ID " +
                "from pd_machinekit where mname='" + name + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);

        }
        public DataTable getEditdatabystandardki()
        {
            string sqltext = " select sm.ID,sm.sname,sm.scode " +
                " from pd_standardkit sm " +
                " order by sm.systemdate";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string getEditdatabystandardki(string name)
        {
            string sqltext = "select ID " +
                "from pd_standardkit where sname='" + name + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);

        }
        public DataTable getEditdatabybuyki()
        {
            string sqltext = " select sm.ID,sm.oname,sm.ocode " +
                " from pd_outbuykit sm " +
                " order by sm.systemdate";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string getEditdatabybuyki(string name)
        {
            string sqltext = "select ID " +
                "from pd_outbuykit where oname='" + name + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);

        }
        public DataTable getEditdatabycompongki()
        {
            string sqltext = " select sm.ID,sm.comname,sm.comcode " +
                " from pd_compongall sm " +
                " order by sm.adddate";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public string getEditdatabycompongki(string name)
        {
            string sqltext = "select ID " +
                "from pd_compongall where comname='" + name + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);

        }
        public string getPhotoID(string standardKitID)
        {
            string sqltext = "select p.id from pd_compongall sk join pd_photo p on sk.ID=p.pid" +
                " where sk.ID='" + standardKitID + "'";
            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }
        public DataTable getkidclass()
        {
            string sqltext = "select comname " +
                    " from pd_compongall ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        
        public DataTable getkidclassbymach()
        {
            string sqltext = "select mname " +
                    " from pd_machinekit ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getkidclassbystand()
        {
            string sqltext = "select sname " +
                    " from pd_standardkit ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
        public DataTable getkidclassbyoutbuy()
        {
            string sqltext = "select oname " +
                    " from pd_outbuykit ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }
    }
}
