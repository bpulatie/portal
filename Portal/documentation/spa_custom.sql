

-- spa_custom

-------------------------------------------------------------------------------------
CREATE TABLE supplier (
  supplier_id uniqueidentifier NOT NULL,
  client_id uniqueidentifier NULL,
  external_id nvarchar(50) NULL,
  name nvarchar(50) NULL,
  safety_stock int NULL,
  edi_supplier_id nvarchar(50) NULL,
  external_id2 int NULL,
  PRIMARY KEY (supplier_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE supplier_catalog_current (
	supplier_catalog_id uniqueidentifier NOT NULL,
	client_id uniqueidentifier NULL,
	supplier_id uniqueidentifier NULL,
	supplier_id_file varchar(50) NULL,
	supplier_item_code varchar(50) NULL,
	product_description varchar(50) NULL,
	uom_type varchar(50) NULL,
	uom_count varchar(50) NULL,
	cost_level varchar(50) NULL,
	cost varchar(50) NULL,
	status1 varchar(20) NULL,
	upc_pack varchar(50) NULL,
	upc_retail varchar(50) NULL,
	supplier_category varchar(50) NULL,
	status2 varchar(20) NULL,
	action varchar(20) NULL,
	pending_delete datetime NULL,
	exported_flag varchar(1) NULL,
	last_modified datetime NULL,
	supplier_item_department_id uniqueidentifier NULL,
  PRIMARY KEY (supplier_catalog_id)
) 


-------------------------------------------------------------------------------------
CREATE TABLE supplier_item_department (
	supplier_item_department_id uniqueidentifier NOT NULL,
	client_id uniqueidentifier NULL,
	supplier_id uniqueidentifier NULL,
	department_name nvarchar(50) NULL,
	department_code nvarchar(50) NULL,
	external_id nvarchar(50) NULL,
  PRIMARY KEY (supplier_item_department_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE custom_shifts (
	bu_id int NOT NULL,
	employee_id int NOT NULL,
	shift_id int NOT NULL,
	drawer_id int NOT NULL,
	bu_name varchar(50) NOT NULL,
	employee_name varchar(50) NOT NULL,
	bu_date datetime NOT NULL,
	eod_time datetime NULL,
	shift_open_time datetime NULL,
	shift_close_time datetime NULL,
	shift_status_code varchar(1) NULL,
	open_balance_amt decimal(18,2) NULL,
	net_trans_qty decimal(18,2) NULL,
	gross_sold_amt decimal(18,2) NULL,
	adj_system_net_sold_amt decimal(18,2) NULL,
	system_net_sold_amt decimal(18,2) NULL,
	over_short_amt decimal(18,2) NULL,
	cashier_override_amt decimal(18,2) NULL,
	cashier_override_qty decimal(18,2) NULL,
	net_discount_qty decimal(18,2) NULL,
	net_discount_amt decimal(18,2) NULL,
	net_coupon_qty decimal(18,2) NULL,
	net_coupon_amt decimal(18,2) NULL,
	no_sale_qty decimal(18,2) NULL,
	refund_amt decimal(18,2) NULL,
	payout_amt decimal(18,2) NULL,
	item_cancel_amt decimal(18,2) NULL,
	item_cancel_qty decimal(18,2) NULL,
	trans_cancel_qty decimal(18,2) NULL,
	trans_cancel_amt decimal(18,2) NULL,
	fuel_gallons decimal(18,2) NULL,
	waste_amt decimal(18,2) NULL,
	created_date datetime NULL,
	modified_date datetime NULL,
	PRIMARY KEY (bu_id, bu_date, employee_id, shift_id, drawer_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE custom_sales_lines (
	bu_id int NOT NULL,
	bu_date datetime NOT NULL,
	employee_id int NOT NULL,
	shift_id int NOT NULL,
	transaction_id int NOT NULL,
	transaction_line_id int NOT NULL,
	transaction_timestamp datetime NULL,
	sales_type_id int NULL,
	sales_item_id int NULL,
	barcode varchar(50) NULL,
	sale_qty decimal(18,2) NULL,
	retail_price decimal(18,2) NULL,
	gross_amt decimal(18,2) NULL,
	unit_price decimal(18,2) NULL,
	promo_price decimal(18,2) NULL,
	reduction_amt decimal(18,2) NULL,
	refund_qty decimal(18,2) NULL,
	refund_amt decimal(18,2) NULL,
	refund_reduction_amt decimal(18,2) NULL,
	sales_dest_id int NULL,
	component_flag varchar(1) NULL,
	item_name varchar(100) NULL,
	bu_name varchar(50) NULL,
	pump_number int NULL,
	hose int NULL,
	created_date datetime NULL,
	modified_date datetime NULL,
	PRIMARY KEY (bu_id, bu_date, employee_id, shift_id, transaction_id, transaction_line_id)
)


-------------------------------------------------------------------------------------
CREATE TABLE custom_sales_mix (
	bu_id int NOT NULL,
	bu_name varchar(50) NOT NULL,
	bu_date datetime NOT NULL,
	department varchar(50) NOT NULL,
	category varchar(50) NOT NULL,
	sub_category varchar(50) NOT NULL,
	item_name varchar(100) NOT NULL,
	gross_sold_qty decimal(18,2) NULL,
	gross_sold_amt decimal(18,2) NULL,
	net_sold_amt decimal(18,2) NULL,
	refund_amt decimal(18,2) NULL,
	reduction_amt decimal(18,2) NULL,
	created_date datetime NULL,
	modified_date datetime NULL,
	PRIMARY KEY (bu_id, bu_date, department, category, sub_category, item_name)
)

