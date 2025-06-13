
---------------------------------------------------------------------------
-- PAY PERIOD - 22 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_period
(pay_period_id, week_no, start_date, end_date, status_code, cycle_code, creation_user_id, creation_date, modified_user_id, modified_date, client_id)

SELECT pay_period_id, week_no, start_date, end_date, status_code, cycle_code, creation_user_id, creation_date, modified_user_id, modified_date, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'

FROM InsightFlow..pay_period

WHERE start_date between '2016-08-01' and '2017-01-01'



---------------------------------------------------------------------------
-- PAY EXCEPTIONS - 173 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_exception
(pay_period_id, pay_exception_id, site_id, employee_id, reason_id, exception_code, status_code, comment, creation_user_id, creation_date, modified_user_id, modified_date, client_id)

SELECT pay_period_id, pay_exception_id, site_id, employee_id, reason_id, exception_code, status_code, comment, creation_user_id, creation_date, modified_user_id, modified_date, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'

FROM InsightFlow..pay_exception

WHERE pay_period_id = '7E505E2B-A4CF-4556-A38A-B77A96EA3ACF'

AND	site_id in (	'B4B4E9D2-7C5B-4DB5-AB96-079F8319460E', '7B122562-F6CD-4CBF-BE70-24B5D85BEF7E', '401FC8F2-D5DA-4DF7-BAB0-37A7A8462BE9', 'FC065D5D-3B8E-47CF-9F6A-562D3A0D0601', 'F34F7764-5557-4227-8417-56DC65BADD42',
					'D62A4776-9E7E-4F39-A8E6-758D98941606', 'AC76F6EF-B99A-496A-A4ED-86C7C7F095D2', 'BDCA00D3-969C-4457-A134-C4F9710B50CA', 'AB612C95-1727-45EB-A19E-E0001B4E3532', '90F4EC64-D8BA-49C8-9344-E98F12AA3E40',
					'2B3F1564-8E7B-4B53-B616-EC45D89B39DC')



---------------------------------------------------------------------------
-- PAY DETAIL - 1773 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_detail
(pay_period_id, site_id, employee_id, job_id, earning_code, company, project, earning_hours, rate, earning_amount, charge_date, pay_group_id, creation_user_id, creation_date, modified_user_id, modified_date, client_id)

SELECT pay_period_id, site_id, employee_id, job_id, earning_code, company, project, earning_hours, rate, earning_amount, charge_date, pay_group_id, creation_user_id, creation_date, modified_user_id, modified_date, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'
FROM  InsightFlow..pay_detail

WHERE pay_period_id = '7E505E2B-A4CF-4556-A38A-B77A96EA3ACF'

AND site_id in (	'B4B4E9D2-7C5B-4DB5-AB96-079F8319460E', '7B122562-F6CD-4CBF-BE70-24B5D85BEF7E', '401FC8F2-D5DA-4DF7-BAB0-37A7A8462BE9', 'FC065D5D-3B8E-47CF-9F6A-562D3A0D0601', 'F34F7764-5557-4227-8417-56DC65BADD42',
					'D62A4776-9E7E-4F39-A8E6-758D98941606', 'AC76F6EF-B99A-496A-A4ED-86C7C7F095D2', 'BDCA00D3-969C-4457-A134-C4F9710B50CA', 'AB612C95-1727-45EB-A19E-E0001B4E3532', '90F4EC64-D8BA-49C8-9344-E98F12AA3E40',
					'2B3F1564-8E7B-4B53-B616-EC45D89B39DC')


---------------------------------------------------------------------------
-- SITES - 11 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..sys_site
(site_id, site_code, name, state, site_status, site_type, description, creation_user_id, creation_date, modified_user_id, modified_date, source_site, target_site, go_live, org_id, payroll_status, client_id)

SELECT site_id, site_code, name, state, site_status, site_type, description, creation_user_id, creation_date, modified_user_id, modified_date, source_site, target_site, go_live, org_id, payroll_status, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'
FROM  InsightFlow..site

WHERE site_id in (	'B4B4E9D2-7C5B-4DB5-AB96-079F8319460E', '7B122562-F6CD-4CBF-BE70-24B5D85BEF7E', '401FC8F2-D5DA-4DF7-BAB0-37A7A8462BE9', 'FC065D5D-3B8E-47CF-9F6A-562D3A0D0601', 'F34F7764-5557-4227-8417-56DC65BADD42',
					'D62A4776-9E7E-4F39-A8E6-758D98941606', 'AC76F6EF-B99A-496A-A4ED-86C7C7F095D2', 'BDCA00D3-969C-4457-A134-C4F9710B50CA', 'AB612C95-1727-45EB-A19E-E0001B4E3532', '90F4EC64-D8BA-49C8-9344-E98F12AA3E40',
					'2B3F1564-8E7B-4B53-B616-EC45D89B39DC')



---------------------------------------------------------------------------
-- EMPLOYEES - 224 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_employee
(employee_id, employee_no, last_name, first_name, gender, date_of_birth, bu_code, start_date, termination_date, email, cellphone, contact, contact_relation, contact_phone, rate_type, pay_rate, national_code, status_code, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)

SELECT employee_id, employee_no, last_name, first_name, gender, date_of_birth, bu_code, start_date, termination_date, e.email, cellphone, contact, contact_relation, contact_phone, rate_type, pay_rate, national_code, status_code, e.description, e.creation_user_id, e.creation_date, e.modified_user_id, e.modified_date, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'
FROM  InsightFlow..employee e
JOIN  InsightFlow..sys_user u
ON		e.employee_id = u.user_id

WHERE employee_id in (	SELECT DISTINCT employee_id FROM InsightFlow..pay_detail WHERE pay_period_id = '7E505E2B-A4CF-4556-A38A-B77A96EA3ACF')



---------------------------------------------------------------------------
-- EMPLOYEE HISTORY - 3957 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_employee_history

SELECT *
FROM  InsightFlow..employee_history

WHERE employee_id in (	SELECT DISTINCT employee_id FROM InsightFlow..pay_detail WHERE pay_period_id = '7E505E2B-A4CF-4556-A38A-B77A96EA3ACF')



---------------------------------------------------------------------------
-- PAY CODES - 14 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_code
(pay_code_id, company, source_column, source_code, target_code, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)

SELECT pay_code_id, company, source_column, source_code, target_code, description, creation_user_id, creation_date, modified_user_id, modified_date, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'
FROM  InsightFlow..pay_code



---------------------------------------------------------------------------
-- PAY GROUP - 3 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_group
(pay_group_id, pay_group_code, filter_description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)

SELECT pay_group_id, pay_group_code, filter_description, creation_user_id, creation_date, modified_user_id, modified_date, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'
FROM  InsightFlow..pay_group



---------------------------------------------------------------------------
-- PAY JOB - 37 rows
---------------------------------------------------------------------------
INSERT INTO DEMO_Portal..pay_job
(job_id, company, name, job_code,external_code, access_level, security_level, mag_only, mask_pay, active_flag, include_in_mgr_group, tip_status_code, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)

SELECT job_id, company, name, job_code,external_code, access_level, security_level,
	   CASE WHEN mag_only = 0 THEN 'y' ELSE 'n' END,
	   CASE WHEN mask_pay = 0 THEN 'y' ELSE 'n' END,
	   CASE WHEN active_flag = 0 THEN 'y' ELSE 'n' END,
	   CASE WHEN include_in_mgr_group = 0 THEN 'y' ELSE 'n' END, tip_status_code, description, creation_user_id, creation_date, modified_user_id, modified_date, 'A5BE917E-C4E2-44A2-A59B-2B8B47B18849'


FROM  InsightFlow..job
WHERE job_id in (	SELECT DISTINCT job_id FROM InsightFlow..pay_detail WHERE pay_period_id = '7E505E2B-A4CF-4556-A38A-B77A96EA3ACF')