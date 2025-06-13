

-- spa_portal


-------------------------------------------------------------------------------------
CREATE TABLE sys_access (
  access_id uniqueidentifier NOT NULL,
  access_name varchar(100) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (access_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_application (
  application_id uniqueidentifier NOT NULL,
  application_name varchar(100) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (application_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE sys_client (
  client_id uniqueidentifier NOT NULL,
  name varchar(100) NOT NULL,
  status_code varchar(1) NULL,
  contact_name varchar(100) NULL,
  contact_email varchar(100) NULL,
  contact_phone varchar(100) NULL,
  category_levels int NULL,
  client_root varchar(100) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (client_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_client_note (
  client_note_id uniqueidentifier NOT NULL,
  client_id uniqueidentifier NOT NULL,
  summary varchar(250) NOT NULL,
  description text,
  follow_up_date datetime NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (client_note_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_client_feature_list (
  client_feature_id uniqueidentifier NOT NULL,
  feature_id uniqueidentifier NOT NULL,
  client_id uniqueidentifier NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (feature_id, client_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE sys_event (
  event_id uniqueidentifier NOT NULL,
  event_category_id uniqueidentifier NULL,
  event_category varchar(45) NULL,
  event_type_id uniqueidentifier NULL,
  event_type char(1) NULL,
  event_status char(1) NULL,
  event_priority char(1) NULL,
  event_date datetime NOT NULL,
  event_summary varchar(200) NULL,
  event_details text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  email_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (event_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_event_category (
  event_category_id uniqueidentifier NOT NULL,
  event_category_name varchar(45) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (event_category_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_event_subscription (
  event_subscription_id uniqueidentifier NOT NULL,
  event_category_id uniqueidentifier NOT NULL,
  event_type_id uniqueidentifier NOT NULL,
  user_id uniqueidentifier NOT NULL,
  notification_type varchar(1) NOT NULL,
  email varchar(100) NULL,
  email_time datetime NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (event_subscription_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_event_type (
  event_type_id uniqueidentifier NOT NULL,
  event_name varchar(45) NOT NULL,
  event_category_id uniqueidentifier NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (event_type_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_feature (
  feature_id uniqueidentifier NOT NULL,
  application_id uniqueidentifier NOT NULL,
  feature_name varchar(100) NOT NULL,
  moniker varchar(100) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (feature_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE sys_image (
  image_id uniqueidentifier NOT NULL,
  image_name varchar(100) NOT NULL,
  the_image image NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (image_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_import_log (
  import_id uniqueidentifier NOT NULL,
  client_id uniqueidentifier NOT NULL,
  site_id uniqueidentifier NULL,
  filename varchar(256) NOT NULL,
  path varchar(256) NOT NULL,
  status_code char(1) NULL,
  comment text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (import_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_menu (
  menu_id uniqueidentifier NOT NULL,
  menu_name varchar(100) NOT NULL,
  sort_order int NOT NULL,
  description text,
  creation_user_id uniqueidentifier  NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (menu_id)
)  


-------------------------------------------------------------------------------------
CREATE TABLE sys_menu_item (
  menu_id uniqueidentifier NOT NULL,
  feature_id uniqueidentifier NOT NULL,
  menu_item_name varchar(100) NOT NULL,
  sort_order int NOT NULL,
  menu_mode int NOT NULL,
  description text,
  creation_user_id uniqueidentifier DEFAULT NULL,
  creation_date datetime DEFAULT NULL,
  modified_user_id uniqueidentifier DEFAULT NULL,
  modified_date datetime DEFAULT NULL,
  PRIMARY KEY (menu_id, feature_id)
)  


-------------------------------------------------------------------------------------
CREATE TABLE sys_option (
  option_id uniqueidentifier NOT NULL,
  option_name varchar(100) NOT NULL,
  value varchar(45) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (option_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_option_value (
  client_id uniqueidentifier NOT NULL,
  option_id uniqueidentifier NOT NULL,
  option_value varchar(45) NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (client_id, option_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_reason (
  reason_id uniqueidentifier NOT NULL,
  reason_category_id uniqueidentifier NOT NULL,
  reason_code varchar(45) NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (reason_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_reason_category (
  reason_category_id uniqueidentifier NOT NULL,
  reason_category varchar(45) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (reason_category_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_role (
  role_id uniqueidentifier NOT NULL,
  role_name varchar(100) NOT NULL,
  external_name varchar(100) NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (role_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_role_access_list (
  role_id uniqueidentifier NOT NULL,
  access_id uniqueidentifier NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (role_id, access_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_role_menu_list (
  role_id uniqueidentifier NOT NULL,
  menu_id uniqueidentifier NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (role_id, menu_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_session (
  session_id uniqueidentifier NOT NULL,
  user_id uniqueidentifier NULL,
  start_time datetime NOT NULL,
  last_activity_time datetime NOT NULL,
  end_time datetime NULL,
  ip_address varchar(20) NULL,
  username varchar(50) NULL,
  context text,
  session_status varchar(1) NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (session_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_site (
  site_id uniqueidentifier NOT NULL,
  site_code varchar(10) NOT NULL,
  name varchar(100) NOT NULL,
  state char(2) NULL,
  site_status char(1) NULL,
  site_type char(1) NULL,
  site_guid uniqueidentifier NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  --pfc custom
  source_site varchar(50) NULL,  
  target_site varchar(50) NULL,
  go_live datetime NULL,
  org_id char(1) NULL,
  payroll_status char(1) null,
  --pfc custom
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (site_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_site_group (
  site_group_id uniqueidentifier NOT NULL,
  site_group_code varchar(10) NOT NULL,
  name varchar(100) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (site_group_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_site_group_list (
  site_group_id uniqueidentifier NOT NULL,
  site_id uniqueidentifier NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (site_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_user (                                   
  user_id uniqueidentifier NOT NULL,
  login_name varchar(100) NOT NULL,
  email varchar(100) NOT NULL,
  password varchar(45) NOT NULL,
  temp_password varchar(1) NOT NULL,
  user_type varchar(1) NOT NULL,
  last_name varchar(45) NULL,
  first_name varchar(45) NULL,
  gender varchar(1) NULL,
  menu_location varchar(1) NULL,
  style_preference varchar(50) NULL,
  date_of_birth datetime NULL,
  image_id uniqueidentifier NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  client_id uniqueidentifier NOT NULL,
  PRIMARY KEY (user_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_user_role_list (
  user_id uniqueidentifier NOT NULL,
  role_id uniqueidentifier NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (user_id, role_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_user_site_list (
  user_id uniqueidentifier NOT NULL,
  site_id uniqueidentifier NOT NULL,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (user_id, site_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_value (
  group_id uniqueidentifier NOT NULL,
  value_id uniqueidentifier NOT NULL,
  value_code varchar(45) NOT NULL,
  value_text varchar(100) NOT NULL,
  sort_order int NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (value_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE sys_value_group (
  group_id uniqueidentifier NOT NULL,
  group_name varchar(100) NOT NULL,
  description text,
  creation_user_id uniqueidentifier NULL,
  creation_date datetime NULL,
  modified_user_id uniqueidentifier NULL,
  modified_date datetime NULL,
  PRIMARY KEY (group_id)
) 



