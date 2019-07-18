using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.productLib
{
    public class pd_machinekit:baseBusiness
    {
        public DataTable getEditdata_mechanical(string strID)
        {
            string sqltext = " select * "+
                        " from pd_machinekit mk left outer join pd_blueprint bp on mk.id=bp.pid " +
                        " where cast(mk.ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getEditdata(string strID)
        {
            string sqltext = " select ID,mname,mtypeid,mcode,designer,checker,stanarder,examiner," +
                        " drawdate,drawer,specifications,operater,systemdate,isdelid " +
                        " from pd_machinekit  "+
                        " where cast(ID as varchar(36))='"+ strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getBindTreeDataAsdtClone()
        {
            string sqltext = " select * from (  																																										   " +
                     " select 'systemmenu' as id,'元零件库' as nodetext,null as pid,0 as orderint,0 as isclick,0 as nodemenu,0 as hascard  																		   " +
                     " union  																																													   " +
                     " select distinct cast(ID as varchar(36)) as id,mainname as nodetext,'systemmenu' as pid,orderint,0 as isclick,0 as nodemenu,0  															   " +
                     " from pd_kidclass_main  																																									   " +
                     " union  																																													   " +
                     " select distinct cast(ID as varchar(36)) as id,classname as nodetext,cast(mainID as varchar(36)) as pid,orderint,1 as isclick,1 as nodemenu,0  											   " +
                     " from pd_kidclass_secondary  																																								   " +
                     " union  																																													   " +
                     " select cast(ID as varchar(36)) as id,mname as nodetext,cast(mtypeid as varchar(36)) as pid,0 as orderint,2 as isclick,2 as nodemenu, (select count(*) from cd_mechanical m where k.ID=m.pid)" +
                     " from pd_machinekit k 																																									   " +
                     " ) as a order by orderint ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }


        public DataTable getBindTreeDataAsdt()
        {
            string sqltext = "select * from ( "+
                     " select 'systemmenu' as id,'元零件库' as nodetext,null as pid,0 as orderint,0 as isclick,0 as nodemenu "+
                     " union "+
                     " select distinct cast(ID as varchar(36)) as id,mainname as nodetext,'systemmenu' as pid,orderint,0 as isclick,0 as nodemenu " +
                     " from pd_kidclass_main "+
                     " union "+
                     " select distinct cast(ID as varchar(36)) as id,classname as nodetext,cast(mainID as varchar(36)) as pid,orderint,1 as isclick,1 as nodemenu " +
                     " from pd_kidclass_secondary "+
                     " union "+
                     " select cast(ID as varchar(36)) as id,mname as nodetext,cast(mtypeid as varchar(36)) as pid,0 as orderint,2 as isclick,2 as nodemenu " +
                     " from pd_machinekit " +
                     " ) as a order by orderint ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string isExistdata(string strfieldname,string strfieldvalue,string strID,bool isinsert)
        {//全库检查
            string strisinsert = "";
            if (isinsert)
            {
                strisinsert=" and cast(ID as varchar(36))<>'"+ strID + "' ";
            }

            string sqltext = "select "+strfieldname+" from pd_machinekit " +
                    " where "+strfieldname+" ='" + strfieldvalue + "' " +strisinsert;

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public string getmaxmcode(string strmtypeid)
        {
            string sqltext = "select case when mcode is null then 1 else mcode+1 end as mcode "+
                    " from(select MAX(cast(Right(Rtrim(mcode), 3) as int)) as mcode "+
                    " from pd_machinekit " +
                    " where cast(mtypeid as varchar(36)) = '" + strmtypeid + "') as a";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public string getpremcode(string strmtypeid)
        {
            string sqltext = " select pkm.maincode+''+pks.darycode "+
                    " from pd_kidclass_secondary pks "+
                    " left join pd_kidclass_main pkm on pkm.ID = pks.mainID "+
                    " where cast(pks.ID as varchar(36)) = '"+ strmtypeid + "'";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }
        
        public DataTable getmachinekit(string strmtypename)
        {
            if(strmtypename.Trim()!="All")
            {
                strmtypename = " where mtypeid in (select ID from pd_kidclass_secondary where classname='"+ strmtypename + "') ";
            }
            else
            {
                strmtypename = "";
            }

            string sqltext = "select mname " +
                    " from pd_machinekit " + strmtypename;

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getkidclass()
        {
            string sqltext = "select classname " +
                    " from pd_kidclass_secondary ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

    }
}
