

-- spa_workforce


-------------------------------------------------------------------------------------  NOT Coded YET
CREATE TABLE pay_code (
  pay_code_id uniqueidentifier NOT NULL,
  company char(1) NULL,
  source_column varchar(1) NULL,
  source_code varchar(50) NULL,
  target_code varchar(50) NULL,
  description varchar(100) NULL, 
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (pay_code_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE pay_detail (
  pay_period_id uniqueidentifier NOT NULL,
  site_id uniqueidentifier NOT NULL,
  employee_id uniqueidentifier NOT NULL,
  job_id uniqueidentifier NOT NULL,
  earning_code varchar(50) NOT NULL,
  company char(1) NULL,
  project varchar(50) NULL,
  earning_hours decimal(10,2) NULL, 
  rate decimal(10,2) NULL, 
  earning_amount decimal(10,2) NULL, 
  charge_date datetime NOT NULL, 
  pay_group_id uniqueidentifier NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (pay_period_id, site_id, employee_id, job_id, charge_date, earning_code)
)

-------------------------------------------------------------------------------------
CREATE TABLE pay_employee (                                       -- Rename from employee
  employee_id uniqueidentifier NOT NULL,
  employee_no varchar(45) NULL,
  last_name varchar(45) NULL,
  first_name varchar(45) NULL,
  gender varchar(1) NULL,
  date_of_birth datetime NULL,
  user_image_id uniqueidentifier NULL,
  bu_code varchar(45) NULL,
  start_date datetime NULL,
  termination_date datetime NULL,
  email varchar(50) NULL,
  cellphone varchar(50) NULL,
  contact varchar(50) NULL,
  contact_relation varchar(50) NULL,
  contact_phone varchar(50) NULL,
  rate_type char(1) NULL,
  pay_rate DECIMAL(10,2) NULL,
  national_code varchar(50) NULL,
  status_code char(1) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (employee_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE pay_employee_history (                                  -- Rename from employee_history
	employee_history_id uniqueidentifier NOT NULL,
	employee_id uniqueidentifier NOT NULL,
	employee_no varchar(45) NULL,
	start_date datetime NULL,
	termination_date datetime NULL,
	email varchar(50) NULL,
	cellphone varchar(50) NULL,
	contact varchar(50) NULL,
	contact_relation varchar(50) NULL,
	contact_phone varchar(50) NULL,
	rate_type varchar(5) NULL,
	pay_rate decimal(10, 2) NULL,
	national_code varchar(50) NULL,
	status_code char(1) NULL,
	bu_code varchar(45) NULL,
	org_code varchar(45) NULL,
	termination_reason varchar(50) NULL,
	payroll_system_no varchar(50) NULL,
	user_level varchar(50) NULL,
	job_code varchar(50) NULL,
	job_name varchar(50) NULL,
	primary_job varchar(5) NULL,
	POS_code varchar(50) NULL,
	mag_only char(1) NULL,
	security_level int NULL,
	access_level int NULL,
	description text NULL,
	meal_break_waiver char(1) NULL,
	creation_user_id uniqueidentifier NULL,
	creation_date datetime NULL,
	modified_user_id uniqueidentifier NULL,
	modified_date datetime NULL,
  PRIMARY KEY (employee_history_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE pay_employee_location_list (
  employee_id uniqueidentifier NOT NULL,
  location_id varchar(45) NULL,
  start_date datetime NULL,
  end_date datetime NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (employee_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE pay_exception (
  pay_period_id uniqueidentifier NOT NULL,
  pay_exception_id uniqueidentifier NOT NULL,
  site_id uniqueidentifier NULL,
  employee_id uniqueidentifier NULL,
  reason_id uniqueidentifier NULL,
  exception_code varchar(10) NOT NULL,
  status_code char(1) NOT NULL,
  comment text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (pay_exception_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE pay_job (															-- rename from job
  job_id uniqueidentifier NOT NULL,
  company varchar(100) NOT NULL,        
  name varchar(100) NOT NULL,
  job_code varchar(100) NOT NULL,
  external_code varchar(100)  NULL,
  access_level int  NULL,
  security_level int  NULL,
  mag_only char(1)  NULL,
  mask_pay char(1)  NULL,
  active_flag char(1)  NULL,
  include_in_mgr_group char(1)  NULL,
  tip_status_code nchar(1)  NULL,
  description text,
  creation_user_id uniqueidentifier  NULL,
  creation_date datetime  NULL,
  modified_user_id uniqueidentifier  NULL,
  modified_date datetime  NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (job_id)
) 

-------------------------------------------------------------------------------------
CREATE TABLE pay_group (
  pay_group_id uniqueidentifier NOT NULL,
  pay_group_code varchar(50) NOT NULL,
  filter_description varchar(50) NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (pay_group_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE pay_period (
  pay_period_id uniqueidentifier NOT NULL,
  week_no varchar(100) NOT NULL,
  start_date datetime NULL,
  end_date datetime NULL,
  status_code char(1) NULL,
  cycle_code varchar(110) NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (pay_period_id)
) 


