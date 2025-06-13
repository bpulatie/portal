truncate table spa_portal..sys_user
truncate table spa_portal..employee


-- create me as a user
insert into spa_portal..sys_user
VALUES (newid(), 'steve', 'steve', 'a', 'Allen', 'Steve', 'm', '1956-11-02', null, null, null, getdate(), null, getdate())


-- import employees as users and employees
insert into spa_portal..sys_user
select		NEWID(), empid, '123456', 'u', LastName, FirstName, 'm', BirthDate, null, null, null, getdate(), null, getDate()
from		empcurrent
where		primaryjob = 'y'
and			terminationdate = ''

insert into spa_portal..employee
select user_id, empid, hiredate, null, e.email, cellphone, contactfullname, contactrelation, contacthomephone, ratetype, rate, nationalid, status, null, null, creation_date, null, modified_date
from spa_portal..sys_user u
join empcurrent e
on u.email = e.empid
and e.primaryjob = 'Y'
and e.terminationdate = ''

-- import employee history
insert into spa_portal..employee_history
select newid(), user_id, empid, hiredate, terminationdate, null, phone, contactfullname, null, contacthomephone, 

CASE ratetype WHEN 'true' THEN 'S' ELSE 'H' END, rate, nationalid, CASE status WHEN 'Active' THEN 'A' WHEN 'Terminated' THEN 'T' ELSE 'L' END, 

bucode, orgcode, terminationreason, payrollsystemnumber, userlevel, jobid, jobname, CASE primaryjob WHEN 'true' THEN 'Y' ELSE 'N' END, POSCODE, CASE magonly WHEN 'true' THEN 1 ELSE 0 END, seclevel, access,

null, null, getdate(), null, lastmodifieddate
from spa_portal..sys_user u
join emptrans e
on u.email = e.empid
order by empid


-- Associate the steve user to a role to sign in

select * from sys_role
select * from sys_user where email = 'steve'

insert into sys_user_role_list 
values ('EFCA0611-5464-4DEE-AE17-C86A2BB4F643', '1AA0A148-20BB-4521-BEEA-E05F7814FCC6', 'EFCA0611-5464-4DEE-AE17-C86A2BB4F643', getdate(), null, null)


-- Create User to site relationship
insert into spa_portal..sys_user_site_list
select user_id, site_id, null, getdate(), null, null
from spa_portal..sys_user u
join empcurrent e
on u.email = e.empid
and e.primaryjob = 'Y'
and e.terminationdate = ''

join spa_portal..site
on bucode = site_code




-- import jobs
insert into spa_portal..job
select newid(), organizationID, description, jobcode, alohajobcode, access, seclevel, magonly, maskpay, isactive, null, tipid, null, null, getdate(), null, getdate()
from lu_jobrole


-- import sites
insert into spa_portal..site
select newid(), sourcestore, sourcestore,  null, null, getdate(), null, getdate()
from LU_BusinessUnit




-- PAY DETAIL LEONARD SAMPLE 

insert into pay_detail
select		'ACC5E234-4438-43C7-B146-B034F86CD9D4', '28CC65FD-7D0A-45A0-82CA-CCD635C83C9F', e.user_id, job_id, '1', p.payrollnumber, newid(), p.hours, p.rate, p.dollars, p.businessdate, null, getdate(), null, null 
from		paysample p

join		sys_user e
ON			p.lastname = e.last_name
AND			p.firstname = e.first_name

join		job j
on			p.jobid = j.external_code

order by last_name, first_name








-- Pay detail - test data

insert into pay_detail
select	'60FA1D48-E602-45F6-9BD0-736552D353CA', 'EE3922E5-7069-41E6-B6F7-03436CBB0DB0', employee_id, 'B2E95B81-26B8-43EF-941C-0811CC8DC4C3', '1', 'Project X', 
		'OT', -- earning code
		0.5,  -- earning hours
		12.5, -- rate
		0,  -- earning amount
		'10/23/2016', null, getdate(), null, getdate()
from	employee e

insert into pay_detail
select	'60FA1D48-E602-45F6-9BD0-736552D353CA', 'EE3922E5-7069-41E6-B6F7-03436CBB0DB0', employee_id, 'B2E95B81-26B8-43EF-941C-0811CC8DC4C3', '1', 'Project X', 
		'REG', -- earning code
		40,  -- earning hours
		12.5, -- rate
		0,  -- earning amount
		'10/23/2016', null, getdate(), null, getdate()
from	employee e

insert into pay_detail
select	'60FA1D48-E602-45F6-9BD0-736552D353CA', 'EE3922E5-7069-41E6-B6F7-03436CBB0DB0', employee_id, 'B2E95B81-26B8-43EF-941C-0811CC8DC4C3', '1', 'Project X', 
		'TIPCS', -- earning code
		0,  -- earning hours
		12.5, -- rate
		0.5,  -- earning amount
		'10/23/2016', null, getdate(), null, getdate()
from	employee e

insert into pay_detail
select	'60FA1D48-E602-45F6-9BD0-736552D353CA', 'EE3922E5-7069-41E6-B6F7-03436CBB0DB0', employee_id, 'B2E95B81-26B8-43EF-941C-0811CC8DC4C3', '1', 'Project X', 
		'COT', -- earning code
		0.5,  -- earning hours
		12.5, -- rate
		0,  -- earning amount
		'10/23/2016', null, getdate(), null, getdate()
from	employee e

insert into pay_detail
select	'60FA1D48-E602-45F6-9BD0-736552D353CA', 'EE3922E5-7069-41E6-B6F7-03436CBB0DB0', employee_id, 'B2E95B81-26B8-43EF-941C-0811CC8DC4C3', '1', 'Project X', 
		'NWHR', -- earning code
		8,  -- earning hours
		12.5, -- rate
		0,  -- earning amount
		'10/23/2016', null, getdate(), null, getdate()
from	employee e
