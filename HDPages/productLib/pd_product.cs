using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDBusiness;
using YDCode;

namespace HDPages.productLib
{
    public class pd_product:baseBusiness
    {
        public DataTable getEditdata(string strID)
        {
            string sqltext = " select ID,pdname,pdcode,pdtype,designer,checker,stanarder,examiner," +
                        " drawdate,drawer,specifications,operater,systemdate,isdelid " +
                        " from pd_product  " +
                        " where cast(ID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getmaxpdcode()
        {
            string sqltext = "select case when pdcode is null then 1 else pdcode+1 end as pdcode " +
                    " from(select MAX(cast(Right(Rtrim(pdcode),4) as int)) as pdcode " +
                    " from pd_product) as a";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public string isExistdata(string strfieldname, string strfieldvalue, string strID, bool isinsert)
        {//全产品库检查
            string strisinsert = "";
            if (isinsert)
            {
                strisinsert = " and cast(ID as varchar(36))<>'" + strID + "' ";
            }

            string sqltext = "select " + strfieldname + " from pd_product " +
                    " where " + strfieldname + " ='" + strfieldvalue + "' " + strisinsert;

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getproduct(string strpdtypename)
        {
            string sqltext;
            if (strpdtypename.Trim() == "Clone")
            {
                sqltext = "select pdname as name" +
                        " from pd_product " +
                        " union all " +
                        " select comname as name from pd_compongall ";
            }
            else
            {

                if (strpdtypename.Trim() != "All")
                {
                    strpdtypename = " where Rtrim(pdtype)='" + strpdtypename + "' ";
                }
                else
                {
                    strpdtypename = "";
                }

                 sqltext = "select pdname as name " +
                        " from pd_product " + strpdtypename;
            }

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //产品
        public DataTable getBindPdDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.pdname,ul.pdcode,ul.pdtype,ul.drawdate,ul.specifications,ul.designer,ul.checker " +
                " from pd_product ul " +
                " where ul.pdname like '%" + dic["pdname"].ToString().Trim() + "%' and Rtrim(ul.pdtype)='" + dic["pdtype"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getPdtotalpage(Dictionary<string, string> dic)
        {
            string sqltext = " select count(ul.ID) as totalpage " +
                " from pd_product ul " +
                " where ul.pdname like '%" + dic["pdname"].ToString().Trim() + "%' and Rtrim(ul.pdtype)='" + dic["pdtype"].ToString().Trim() + "' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getpdtype()
        {
            string sqltext = "select distinct pdtype " +
                    " from pd_product ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getBindPdComposeDataAsdt(string strID)
        {
            string sqltext = "select ppc.ID,ppc.pdnumber,ppc.pptype,ppc.remarks," +
                        " case RTRIM(ppc.pptype)  " +
                        " when '标准件' then(select ps.sname from pd_standardkit ps where ps.ID = ppc.ppid) " +
                        " when '元零件' then(select pm.mname from pd_machinekit pm where pm.ID = ppc.ppid) " +
                        " when '外购件' then(select po.oname from pd_outbuykit po where po.ID = ppc.ppid) " +
                        " when '元组件' then(select pc.comname from pd_compongall pc where pc.ID = ppc.ppid) " +
                        " when '产品' then(select pp.pdname from pd_product pp where pp.ID = ppc.ppid) " +
                        " end as composename," +
                        " case RTRIM(ppc.pptype)  " +
                        " when '标准件' then(select ps.scode from pd_standardkit ps where ps.ID = ppc.ppid) " +
                        " when '元零件' then(select pm.mcode from pd_machinekit pm where pm.ID = ppc.ppid) " +
                        " when '外购件' then(select po.ocode from pd_outbuykit po where po.ID = ppc.ppid) " +
                        " when '元组件' then(select pc.comcode from pd_compongall pc where pc.ID = ppc.ppid) " +
                        " when '产品' then(select pp.pdcode from pd_product pp where pp.ID = ppc.ppid) " +
                        " end as composecode " +
                        " from pd_product_compose ppc " +
                        " where cast(ppc.pid as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

       

        public string[] cloneproduct(string strnewID,string strpdname,string strpdcode,string strusername,string strID)
        {
            string[] sqltext = new string[2];

            sqltext[0] = " insert into pd_product "+
                    " (ID,pdname,pdcode,pdtype,designer,checker,stanarder,examiner,drawer,drawdate,specifications,operater) " +
                    " select '"+ strnewID + "','"+ strpdname + "','"+ strpdcode +"',"+
                    " pdtype,designer,checker,stanarder,examiner,drawer,drawdate,specifications,'"+ strusername + "' " +
                    " from pd_product " +
                    " where cast(ID as varchar(36))='" +strID+"' ";

            sqltext[1] = " insert into pd_product_compose " +
                    " (ID,pid,ppid,pptype,pdnumber,remarks,operater)" +
                    " select newid(),'"+ strnewID + "',ppid,pptype,pdnumber,remarks,'" + strusername+ "' " +
                    " from pd_product_compose " +
                    " where cast(pid as varchar(36))='" + strID + "' ";

            return sqltext;
        }

        //标准件
        public DataTable getBindStandDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.sname,ul.scode,ul.specification,ul.stype,ul.material " +
                " from pd_standardkit ul " +
                " where ul.sname like '%" + dic["sname"].ToString().Trim() + "%' and Rtrim(ul.stype)='" + dic["stype"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getStandtotalpage(Dictionary<string, string> dic)
        {
            string sqltext = " select count(ul.ID) as totalpage " +
                " from pd_standardkit ul " +
                " where ul.sname like '%" + dic["sname"].ToString().Trim() + "%' and Rtrim(ul.stype)='" + dic["stype"].ToString().Trim() + "' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getstype()
        {
            string sqltext = "select distinct stype " +
                    " from pd_standardkit ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //外购件
        public DataTable getBindOutDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.oname,ul.ocode,ul.specification,ul.otype " +
                " from pd_outbuykit ul " +
                " where ul.oname like '%" + dic["oname"].ToString().Trim() + "%' and Rtrim(ul.otype)='" + dic["otype"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getOuttotalpage(Dictionary<string, string> dic)
        {
            string sqltext = " select count(ul.ID) as totalpage " +
                " from pd_outbuykit ul " +
                " where ul.oname like '%" + dic["oname"].ToString().Trim() + "%' and Rtrim(ul.otype)='" + dic["otype"].ToString().Trim() + "' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getotype()
        {
            string sqltext = "select distinct otype " +
                    " from pd_outbuykit ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //元零件

        public DataTable getBindMkitDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.mname,ul.mcode,ul.drawdate,ul.mtypeid " +
                " from pd_machinekit ul " +
                " where ul.mname like '%" + dic["mname"].ToString().Trim() + "%' and " +
                " cast(mtypeid as varchar(36))='" + dic["subclassID"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getMkittotalpage(Dictionary<string, string> dic)
        {
            string sqltext = " select count(ul.ID) as totalpage " +
                " from pd_machinekit ul " +
                " where ul.mname like '%" + dic["mname"].ToString().Trim() + "%' and " +
                " cast(mtypeid as varchar(36))='" + dic["subclassID"].ToString().Trim() + "' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getBindTree_2DataAsdt()
        {
            string sqltext = "select * from ( " +
                     " select 'systemmenu' as id,'元零件特征库' as nodetext,null as pid,0 as orderint,0 as isclick,0 as nodemenu " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,mainname as nodetext,'systemmenu' as pid,orderint,1 as isclick,1 as nodemenu " +
                     " from pd_kidclass_main " +
                     " union " +
                     " select distinct cast(ID as varchar(36)) as id,classname as nodetext,cast(mainID as varchar(36)) as pid,orderint,2 as isclick,1 as nodemenu " +
                     " from pd_kidclass_secondary) as a order by orderint ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getmainclass()
        {
            string sqltext = "select mainname,ID " +
                    " from pd_kidclass_main " +
                    " order by orderint ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getsubclass(string strmainclassID)
        {
            string sqltext = "select classname,ID " +
                    " from pd_kidclass_secondary " +
                    " where cast(mainID as varchar(36))='" + strmainclassID + "' " +
                    " order by orderint ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //元组件
        public DataTable getBindComDataAsdt(Dictionary<string, string> dic, string strsort, string strSortDirection, int intPagesize, int intPageindex)
        {
            string sqltext = " select * from (select ROW_NUMBER() OVER(Order by ul." + strsort.Trim() + " " + strSortDirection + ") as rowno," +
                " ul.ID,ul.comname,ul.adddate,ul.comcode,ul.remarks,ul.comtype,ul.standards,ul.designer,ul.checker " +
                " from pd_compongall ul " +
                " where ul.comname like '%" + dic["comname"].ToString().Trim() + "%' and Rtrim(ul.comtype)='"+ dic["comtype"].ToString().Trim() + "') as s " +
                " where rowno >" + ((intPageindex - 1) * intPagesize).ToString() + " and rowno<=" + (intPageindex * intPagesize).ToString() + " ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string getComtotalpage(Dictionary<string, string> dic)
        {
            string sqltext = " select count(ul.ID) as totalpage " +
                " from pd_compongall ul " +
                " where ul.comname like '%" + dic["comname"].ToString().Trim() + "%' and Rtrim(ul.comtype)='" + dic["comtype"].ToString().Trim() + "' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

        public DataTable getcomtype()
        {
            string sqltext = "select distinct comtype "+
                    " from pd_compongall ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public DataTable getBindComposeDataAsdt(string strID)
        {
            string sqltext = "select pcc.ID,pcc.number,pcc.type,pcc.specification," +
                        " case RTRIM(pcc.type)  "+
                        " when '标准件' then(select ps.sname from pd_standardkit ps where ps.ID = pcc.composeID) "+
                        " when '元零件' then(select pm.mname from pd_machinekit pm where pm.ID = pcc.composeID) "+
                        " when '外购件' then(select po.oname from pd_outbuykit po where po.ID = pcc.composeID) "+
                        " end as composename,"+
		                " case RTRIM(pcc.type)  "+
                        " when '标准件' then(select ps.scode from pd_standardkit ps where ps.ID = pcc.composeID) "+
                        " when '元零件' then(select pm.mcode from pd_machinekit pm where pm.ID = pcc.composeID) "+
                        " when '外购件' then(select po.ocode from pd_outbuykit po where po.ID = pcc.composeID) "+
                        " end as composecode "+
                        " from pd_compongall_compose pcc "+
                        " where cast(pcc.componID as varchar(36))='" + strID + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        //产品树
        public DataTable getBindTreeDataAsdt(string strpdname)
        {
            string sqltext = " ; with productCTE as ( "+
                     " select pid, ppid,pdnumber,pptype,2 as nodemenu,1 as isclick,cast(pid as varchar(max)) treepid " +
                     " from pd_product_compose "+
                     " where pid = (select id from pd_product where Rtrim(pdname) = '"+ strpdname + "') "+
                     " union all "+
                     " select ppc.pid,ppc.ppid,ppc.pdnumber,ppc.pptype,nodemenu + 1 as nodemenu,1 as isclick," +
                     " (cast(ppc.pid as varchar(max)))+'-'+(cast(pcte.treepid as varchar(max))) treepid " +
                     " from pd_product_compose ppc "+
                     " inner join productCTE pcte on pcte.ppid = ppc.pid "+
                     " ) "+
                     " select pcte.treepid as pid," +
                     " cast(pcte.ppid as varchar(36))+'-'+pcte.treepid as ppid," +
                     " ISNULL(pcte.pdnumber,0) as pdnumber," +
					 " case RTRIM(pcte.pptype) "+
                        " when '元组件' then(select pc.comname from pd_compongall pc where cast(pc.ID as varchar(36)) = pcte.ppid) "+
                        " when '元零件' then(select pm.mname from pd_machinekit pm where cast(pm.ID as varchar(36)) = pcte.ppid) "+
                        " when '标准件' then(select ps.sname from pd_standardkit ps where cast(ps.ID as varchar(36)) = pcte.ppid) "+
                        " when '外购件' then(select po.oname from pd_outbuykit po where cast(po.ID as varchar(36)) = pcte.ppid) "+
						" else pp.pdname "+
                        " end as pdname," +
						" pcte.pptype,pcte.nodemenu,pcte.isclick "+
                     " from productCTE pcte "+
                     " left join pd_product pp on pp.ID = pcte.ppid "+
                     " union "+
                     " select cast(pcte.ppid as varchar(36))+'-'+pcte.treepid as pid," +
                     " cast(pcc.ID as varchar(36))+'-'+cast(pcte.ppid as varchar(36))+'-'+pcte.treepid as ppid," +
                     " ISNULL(pcc.number,0) as pdnumber," +
                     " pcc.composename as pdname,'' as pptype,0 as nodemenu,0 as isclick " +
                     " from productCTE pcte "+
                     " left "+
                     " join (select pcce.id,pcce.componID,pcce.number,pcce.composeID," +
						" case RTRIM(pcce.type) "+
                        " when '标准件' then(select ps.sname from pd_standardkit ps where ps.ID = pcce.composeID) "+
                        " when '元零件' then(select pm.mname from pd_machinekit pm where pm.ID = pcce.composeID) "+
                        " when '外购件' then(select po.oname from pd_outbuykit po where po.ID = pcce.composeID) "+
                        " end as composename "+
                        " from pd_compongall_compose pcce "+
						" ) as pcc on pcc.componID = pcte.ppid "+
                     " where pcc.ID is not null and pcte.pptype = '元组件' " +
                     " union "+
                     " select null as pid,cast(id as varchar(36)) as ppid,0 as pdnumber,pdname,null as pptype," +
                     " 1 as nodemenu,1 as isclick " +
                     " from pd_product "+
                     " where Rtrim(pdname) = '"+ strpdname + "' ";

            return DBSQL.ExecutesqlTodt(sqltext, DBSQL.connstr);
        }

        public string isExistsProduct(string strcurpid,string strlookpid)
        {
            string sqltext = ";with isExistproductCTE as( "+
                    " select pid, ppid from pd_product_compose "+
                    " where cast(ppid as varchar(36))= '"+ strcurpid + "' and pptype = '产品' "+
                    " union all "+
                    " select ppc.pid, ppc.ppid "+
                    " from pd_product_compose ppc "+
                    " inner join isExistproductCTE isPCTE on isPCTE.pid = ppc.ppid "+
                    " where pptype = '产品' "+
                    " ) "+
                    " select pid from isExistproductCTE "+
                    " where cast(pid as varchar(36)) = '"+ strlookpid + "' ";

            return DBSQL.ExecutesqlTostring(sqltext, DBSQL.connstr);
        }

    }
}
