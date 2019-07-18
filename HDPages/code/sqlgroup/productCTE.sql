
use HDPMWDB

; with productCTE as ( 
                     select pid,ppid,pdnumber, pptype,2 as nodemenu,1 as isclick,cast(pid as varchar(max)) treepid
                     from pd_product_compose 
                     where pid=(select id from pd_product where Rtrim(pdname) = '阀门100') 
                     union all 
                     select ppc.pid,ppc.ppid,ppc.pdnumber,ppc.pptype,nodemenu + 1 as nodemenu,1 as isclick,
					 (cast(ppc.pid as varchar(max)))+','+(cast(pcte.treepid as varchar(max))) treepid
                     from  pd_product_compose ppc
                     inner join productCTE pcte on  pcte.ppid= ppc.pid 
                     ) 
                     select pcte.treepid as pid,
					 cast(pcte.ppid as varchar(36))+','+pcte.treepid as ppid,
					 ISNULL(pcte.pdnumber,0) as pdnumber,
					 case RTRIM(pcte.pptype) 
						when '元组件' then (select pc.comname from pd_compongall pc where cast(pc.ID as varchar(36))=pcte.ppid)
						when '元零件' then (select pm.mname from pd_machinekit pm where cast(pm.ID as varchar(36))=pcte.ppid)
						when '标准件' then (select ps.sname from pd_standardkit ps where cast(ps.ID as varchar(36))=pcte.ppid)
						when '外购件' then (select po.oname from pd_outbuykit po where cast(po.ID as varchar(36))=pcte.ppid)
						else pp.pdname
						end as pdname,
						pcte.pptype,pcte.nodemenu,pcte.isclick
                     from productCTE pcte 
                     left join pd_product pp on pp.ID = pcte.ppid
					 union
					 select cast(pcte.ppid as varchar(36))+','+pcte.treepid as pid,
						cast(pcc.ID as varchar(36))+','+cast(pcte.ppid as varchar(36))+','+pcte.treepid as ppid,
						ISNULL(pcc.number,0) as pdnumber,
						pcc.composename as pdname,'' as pptype,2 as nodemenu,0 as isclick
					 from productCTE pcte
					 left join (select pcce.id,pcce.number,pcce.componID,pcce.composeID,
						case RTRIM(pcce.type) 
                        when '标准件' then(select ps.sname from pd_standardkit ps where ps.ID = pcce.composeID)
                        when '元零件' then(select pm.mname from pd_machinekit pm where pm.ID = pcce.composeID)
                        when '外购件' then(select po.oname from pd_outbuykit po where po.ID = pcce.composeID)
                        end as composename
						from pd_compongall_compose pcce 
						) as pcc on pcc.componID=pcte.ppid
					 where pcc.ID is not null and pcte.pptype='元组件'
                     union 
                     select null as pid,cast(id as varchar(36)) as ppid,0 as pdnumber,pdname,null as pptype,1 as nodemenu,1 as isclick 
                     from pd_product
                     where Rtrim(pdname) = '阀门100'