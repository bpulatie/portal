
-----------------------------------------------------------------------------------------------------------------------------------------
-- Initialize an empty database
-----------------------------------------------------------------------------------------------------------------------------------------

USE EMPTY_Portal -- Change to the real portal database

-----------------------------------------------------------------------------------------------------------------------------------------
-- Admin Client and User
-----------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM sys_client WHERE client_id = 'E90A452E-F178-4377-BCFD-83EC7503467F')
BEGIN
	INSERT INTO sys_client 
	(client_id, name, status_code, contact_name, contact_email, contact_phone, description, creation_user_id, creation_date, modified_user_id, modified_date, client_root, category_levels)
	VALUES ('E90A452E-F178-4377-BCFD-83EC7503467F', 'RDM Retail Data Management', 'o', 'Steve Allen', 'steve@virtuallyracing.com', '404-547-1644', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), null, null)
END

IF NOT EXISTS (SELECT 1 FROM sys_user WHERE user_id = '5FDD6452-C6B3-418D-8F52-E7B802C12253')
BEGIN
	INSERT INTO sys_user
	(user_id, login_name, password, user_type, last_name, first_name, gender, date_of_birth, email, description, creation_user_id, creation_date, modified_user_id, modified_date, style_preference, menu_location, client_id, image_id, temp_password)
	VALUES ('5FDD6452-C6B3-418D-8F52-E7B802C12253', 'sysadmin', '!#/)zW¥§C‰JJ€Ã', 's', 'Admin', 'System', 'm', '1956-11-02', 'leonardshackelford@agilreleasegroup.com', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'cerulean', '1', 'E90A452E-F178-4377-BCFD-83EC7503467F', null, 'n')
END


-----------------------------------------------------------------------------------------------------------------------------------------
-- Applications
-----------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM sys_application WHERE application_id = '18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE')
BEGIN
	INSERT INTO sys_application
	(application_id, application_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', 'Portal', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_application WHERE application_id = '86748E69-B581-482E-9738-1217659F7988')
BEGIN
	INSERT INTO sys_application
	(application_id, application_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('86748E69-B581-482E-9738-1217659F7988', 'Metrics', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_application WHERE application_id = '174BB5AF-829D-4692-A2BC-2A0297356622')
BEGIN
	INSERT INTO sys_application
	(application_id, application_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('174BB5AF-829D-4692-A2BC-2A0297356622', 'Inventory', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_application WHERE application_id = '072CA160-DD7B-41EA-8DA2-495C0C51B55C')
BEGIN
	INSERT INTO sys_application
	(application_id, application_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('072CA160-DD7B-41EA-8DA2-495C0C51B55C', 'Workforce', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_application WHERE application_id = '8E49771E-5F9D-4E18-BFDD-8F0B574B2FEE')
BEGIN
	INSERT INTO sys_application
	(application_id, application_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('8E49771E-5F9D-4E18-BFDD-8F0B574B2FEE', 'Background Processing', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_application WHERE application_id = 'B03DB4DB-6047-47BA-B17C-FC22EE3D2957')
BEGIN
	INSERT INTO sys_application
	(application_id, application_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('B03DB4DB-6047-47BA-B17C-FC22EE3D2957', 'Custom', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_application WHERE application_id = '612713B9-C05C-4815-A4AC-34B3A4193707')
BEGIN
	INSERT INTO sys_application
	(application_id, application_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('612713B9-C05C-4815-A4AC-34B3A4193707', 'System', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-----------------------------------------------------------------------------------------------------------------------------------------
-- Features
-----------------------------------------------------------------------------------------------------------------------------------------


-- Portal -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'EF9234B1-E3C0-45CD-AB7C-1B3D43AC8297')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', 'EF9234B1-E3C0-45CD-AB7C-1B3D43AC8297', 'Events', '/portal/sys-event/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '3664ABFC-DA82-4AB4-9F4B-39DA510F8950')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', '3664ABFC-DA82-4AB4-9F4B-39DA510F8950', 'Menus', '/portal/sys-menu/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'E860E56F-68F1-437F-8D8B-588234DE88B9')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', 'E860E56F-68F1-437F-8D8B-588234DE88B9', 'Users', '/portal/sys-user/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '4774D536-4B2A-49F3-97E2-605781C64748')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', '4774D536-4B2A-49F3-97E2-605781C64748', 'Sessions', '/portal/sys-session/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '571D6FC4-427C-418F-889A-6A6484F70058')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', '571D6FC4-427C-418F-889A-6A6484F70058', 'Sites', '/portal/sys-sites/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'A5A92980-77E9-4DBC-AEFD-DA535277769F')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', 'A5A92980-77E9-4DBC-AEFD-DA535277769F', 'Profile', '/portal/sys-profile/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'BE77D170-6ACB-45E1-80A1-F5C8D324931E')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('18F840C2-A6D0-4C0C-9B7B-08D3E2A149CE', 'BE77D170-6ACB-45E1-80A1-F5C8D324931E', 'Roles', '/portal/sys-role/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-- Metrics -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '9A671A58-1E3D-45DA-99FC-D3A9E9D5DE4E')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('86748E69-B581-482E-9738-1217659F7988', '9A671A58-1E3D-45DA-99FC-D3A9E9D5DE4E', 'Metric Viewer', '/metric/viewer/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-- Inventory -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '986E0CFF-556D-4E82-B231-D60BB9CEEB51')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('174BB5AF-829D-4692-A2BC-2A0297356622', '986E0CFF-556D-4E82-B231-D60BB9CEEB51', 'Category Levels', '/inventory/category-level/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '377D3C6F-A984-4A55-8309-3A38656C916A')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('174BB5AF-829D-4692-A2BC-2A0297356622', '377D3C6F-A984-4A55-8309-3A38656C916A', 'Categories', '/inventory/category/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'C93CD0DC-5916-4ED1-AE72-06A03A901D8A')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('174BB5AF-829D-4692-A2BC-2A0297356622', 'C93CD0DC-5916-4ED1-AE72-06A03A901D8A', 'Items', '/inventory/item/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-- System -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '8197918B-E119-48BB-ACFF-408F4DFDCCDD')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('612713B9-C05C-4815-A4AC-34B3A4193707', '8197918B-E119-48BB-ACFF-408F4DFDCCDD', 'Options', '/portal/sys-option/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '32A76D80-F0CC-4970-BC02-274A2E5B8E1F')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('612713B9-C05C-4815-A4AC-34B3A4193707', '32A76D80-F0CC-4970-BC02-274A2E5B8E1F', 'Features', '/portal/sys-feature/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '739C5982-621B-4588-8220-31EA831F89D9')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('612713B9-C05C-4815-A4AC-34B3A4193707', '739C5982-621B-4588-8220-31EA831F89D9', 'Applications', '/portal/sys-application/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '18AC0AAD-A390-409E-B359-FCC7E4EA27C9')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('612713B9-C05C-4815-A4AC-34B3A4193707', '18AC0AAD-A390-409E-B359-FCC7E4EA27C9', 'Reasons', '/portal/sys-reason/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-- Workforce -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'BA27F8F2-9CEF-42BB-88A6-018FBF646CF7')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('072CA160-DD7B-41EA-8DA2-495C0C51B55C', 'BA27F8F2-9CEF-42BB-88A6-018FBF646CF7', 'Pay Group', '/workforce/pay-group/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '264C6A69-BCF5-4767-95CB-F92BE7A557D6')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('072CA160-DD7B-41EA-8DA2-495C0C51B55C', '264C6A69-BCF5-4767-95CB-F92BE7A557D6', 'Manage Pay', '/workforce/pay-specialist/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'D05BE3F2-A711-4291-BD08-29DDAF7A48BE')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('072CA160-DD7B-41EA-8DA2-495C0C51B55C', 'D05BE3F2-A711-4291-BD08-29DDAF7A48BE', 'Jobs', '/workforce/pay-job/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '11F10ABD-E8E6-44A6-8DB3-0C493EA9182C')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('072CA160-DD7B-41EA-8DA2-495C0C51B55C', '11F10ABD-E8E6-44A6-8DB3-0C493EA9182C', 'Employees', '/workforce/pay-employee/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '4F370EA1-4D74-47A5-9DAF-AC64CB611327')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('072CA160-DD7B-41EA-8DA2-495C0C51B55C', '4F370EA1-4D74-47A5-9DAF-AC64CB611327', 'Pay Periods', '/workforce/pay-periods/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-- Background Processing -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'AC9F7078-8ED8-4BD7-A2D5-8041DB77DD05')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('8E49771E-5F9D-4E18-BFDD-8F0B574B2FEE', 'AC9F7078-8ED8-4BD7-A2D5-8041DB77DD05', 'Aysnc Jobs', '/async/async-job/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '23A0C304-EC71-4068-AAC0-5C8FA36F7E46')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('8E49771E-5F9D-4E18-BFDD-8F0B574B2FEE', '23A0C304-EC71-4068-AAC0-5C8FA36F7E46', 'Aysnc Tasks', '/async/async-task/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = '551F9CE9-4304-4364-B0EF-0A0B7AE46CC1')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('8E49771E-5F9D-4E18-BFDD-8F0B574B2FEE', '551F9CE9-4304-4364-B0EF-0A0B7AE46CC1', 'Queue Monitor', '/async/async-monitor/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-- Custom -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'DDDF1D83-2485-434F-844B-ADA61A2F64D7')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('B03DB4DB-6047-47BA-B17C-FC22EE3D2957', 'DDDF1D83-2485-434F-844B-ADA61A2F64D7', 'Supplier Catalog', '/custom/supplier-catalog/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END

IF NOT EXISTS (SELECT 1 FROM sys_feature WHERE feature_id = 'DED9A863-766D-4175-A5C6-CBA28B66D303')
BEGIN
	INSERT INTO sys_feature
	(application_id, feature_id, feature_name, moniker, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('B03DB4DB-6047-47BA-B17C-FC22EE3D2957', 'DED9A863-766D-4175-A5C6-CBA28B66D303', 'Catalog Assignment', '/custom/catalog-assignment/browse.htm', null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-----------------------------------------------------------------------------------------------------------------------------------------
-- Access Features
-----------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM sys_access WHERE access_id = '5C3E524B-75B4-44FD-AC40-6CE14487CED4')
BEGIN
	INSERT INTO sys_access
	(access_id, access_name, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('5C3E524B-75B4-44FD-AC40-6CE14487CED4', 'Pay_ReprocessPayFile', 'Enables the Reprocess Pay File button on the Manage Pay Screen', '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


