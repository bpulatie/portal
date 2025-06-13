

-- spa_async


-------------------------------------------------------------------------------------
CREATE TABLE async_execution (
  execution_id uniqueidentifier NOT NULL,
  process_id int NULL,
  job_id uniqueidentifier NULL,
  task_id uniqueidentifier NULL,
  queue_id uniqueidentifier NULL,
  async_name varchar(100) NOT NULL,
  task_no int NULL,
  queued_time datetime NULL,
  start_time datetime NULL,
  end_time datetime NULL,
  status_code varchar(1) NULL,
  execution_type varchar(1) NULL,
  context text,
  user_id uniqueidentifier NULL,
  client_id uniqueidentifier NULL,
  PRIMARY KEY (execution_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE async_execution_detail (
  execution_detail_id uniqueidentifier NOT NULL,
  execution_id uniqueidentifier NOT NULL,
  process_id int DEFAULT NULL,
  job_id uniqueidentifier DEFAULT NULL,
  task_id uniqueidentifier DEFAULT NULL,
  task_no int DEFAULT NULL,
  execution_time datetime DEFAULT NULL,
  status_code varchar(1) DEFAULT NULL,
  status_message text,
  PRIMARY KEY (execution_detail_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE async_job (                                   
  job_id uniqueidentifier NOT NULL,
  job_name varchar(100) NOT NULL,
  job_context text,
  active_flag char(1) NULL,
  schedule_code char(1) NULL,
  schedule_time datetime NULL,
  schedule_frequency int NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (job_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE async_job_task_list (                                   
  job_task_id uniqueidentifier NOT NULL,
  job_id uniqueidentifier NOT NULL,
  task_id uniqueidentifier NOT NULL,
  sort_order int DEFAULT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (job_task_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE async_queue (                                   
  queue_id uniqueidentifier NOT NULL,
  queue_name varchar(100) NOT NULL,
  thread_count int NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (queue_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE async_task (                                   
  task_id uniqueidentifier NOT NULL,
  task_name varchar(100) NOT NULL,
  moniker varchar(100) NOT NULL,
  task_context text,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (task_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE async_task_parameter (                                   
  parameter_id uniqueidentifier NOT NULL,
  task_id uniqueidentifier NOT NULL,
  parameter_name varchar(30) NOT NULL,
  data_type varchar(1) NOT NULL,
  required varchar(1) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (parameter_id)
)


 