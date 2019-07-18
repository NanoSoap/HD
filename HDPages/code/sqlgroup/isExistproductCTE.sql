



;with isExistproductCTE as(
	select pid, ppid from pd_product_compose
	where ppid='3c5673c4-247b-461b-8dcb-3b30c09e1701' and pptype='产品'
	union all
	select ppc.pid, ppc.ppid 
	from pd_product_compose ppc
	inner join isExistproductCTE isPCTE on isPCTE.pid = ppc.ppid
	where pptype='产品'
)
select pid from isExistproductCTE
where pid='DDDCF85B-E919-4754-B08F-909892ECB118'