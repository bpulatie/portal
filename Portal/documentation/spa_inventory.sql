

-- spa_inventory


-------------------------------------------------------------------------------------
CREATE TABLE inv_category (
  category_id uniqueidentifier NOT NULL,
  category_level_id uniqueidentifier NOT NULL,
  parent_category_id uniqueidentifier NULL,
  name varchar(50) NULL,
  external_id varchar(50) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (category_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE inv_category_level (
  category_level_id uniqueidentifier NOT NULL,
  level_name varchar(50) NULL,
  depth int NULL,
  item_level char(1) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (category_level_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE inv_item (
  item_id uniqueidentifier NOT NULL,
  item_name varchar(50) NULL,
  category_id uniqueidentifier NOT NULL,
  external_id varchar(50) NULL,
  item_type char(1) NULL,
  active_flag char(1) NULL,
  buy_flag char(1) NULL,
  sell_flag char(1) NULL,
  count_flag char(1) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (item_id)
) 

