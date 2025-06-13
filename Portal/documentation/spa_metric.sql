

-- spa_metric


-------------------------------------------------------------------------------------
CREATE TABLE metric (
  metric_id uniqueidentifier NOT NULL,
  client_id uniqueidentifier NOT NULL,
  business_date datetime NOT NULL,
  dimension_1_id int NOT NULL,
  dimension_1_name varchar(50) NULL,
  dimension_2_id int NOT NULL,
  dimension_2_name varchar(50) NULL,
  dimension_3_id int NOT NULL,
  dimension_3_name varchar(50) NULL,
  value_1 decimal(18,2) NULL,
  value_2 decimal(18,2) NULL,
  value_3 decimal(18,2) NULL,
  created_date datetime NULL,
  modified_date datetime NULL,
  PRIMARY KEY (metric_id, client_id, business_date, dimension_1_id, dimension_2_id, dimension_3_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE metric_config (
  metric_id uniqueidentifier NOT NULL,
  metric_name varchar(50) NOT NULL,
  metric_detail varchar(200) NULL,
  dimension_1_label varchar(50) NULL,
  dimension_2_label varchar(50) NULL,
  dimension_3_label varchar(50) NULL,
  value_1_label varchar(50) NULL,
  value_2_label varchar(50) NULL,
  value_3_label varchar(50) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (metric_id)
) 


 