
-----------------------------------------------------------------------------------------------------------------------------------------
-- Initialize an empty database
-----------------------------------------------------------------------------------------------------------------------------------------

USE EMPTY_Async -- Change to the real portal database

-----------------------------------------------------------------------------------------------------------------------------------------
-- Async Queue
-----------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM async_queue WHERE queue_id = '00000000-0000-0000-0000-000000000000')
BEGIN
	INSERT INTO async_queue 
	(queue_id, queue_name, thread_count, description, creation_user_id, creation_date, modified_user_id, modified_date)
	VALUES ('00000000-0000-0000-0000-000000000000', 'Default', 1, null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate())
END


-----------------------------------------------------------------------------------------------------------------------------------------
-- Async Tasks
-----------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = '56FD0122-7412-4503-9CE9-27033CEFE041')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('56FD0122-7412-4503-9CE9-27033CEFE041', 'FTPFileRetrieval', 'FTPFileRetrieval', null, 'Context parameters are comma separated name value pairs: Action=??,                   (File=Single File, Folder=All files in a folder)  HostName=??,            (FTP Server or IP Address) UserName=??,            (Username) Password=??,             (Password) RemoteFile=??,          (When Action=File - include the path i.e. leonardtest\testfile.xxx) LocalFile=??,              (When Action=File - local filename including path i.e. C:\FTP_IN\testfile.xxx) RemoteDir=??,           (When Action=Folder - remote path) LocalDir=??,               (When Action=Folder - local path) Pattern                        (When Action=Folder - optional file mask i.e. *.txt) ', '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = '92FA1AF4-0F15-4AEA-8E0A-4EE0569E6972')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('92FA1AF4-0F15-4AEA-8E0A-4EE0569E6972', 'SuccessTask', 'SuccessTask', null, null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = 'A3A60F45-314A-46EF-B8D0-53399263AFB1')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('A3A60F45-314A-46EF-B8D0-53399263AFB1', 'MurphyAlerts', 'MurphyAlerts', null, null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = '8D455D42-FD9F-498C-BCD6-779FAFC24A31')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('8D455D42-FD9F-498C-BCD6-779FAFC24A31', 'PAYCOMEmployeeImport', 'PAYCOMEmployeeImport', null, null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = '07FF038D-66D7-4F6D-9F6E-9BBB26FF04EB')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('07FF038D-66D7-4F6D-9F6E-9BBB26FF04EB', 'FTPFileSend', 'FTPFileSend', null, 'Context parameters are comma separated name value pairs: Action=??,                   (File=Single File, Folder=All files in a folder)  HostName=??,            (FTP Server or IP Address) UserName=??,            (Username) Password=??,             (Password) LocalFile=??,              (When Action=File - local filename including path i.e. C:\FTP_IN\testfile.xxx) RemoteFile=??,          (When Action=File - include the path i.e. leonardtest\testfile.xxx) LocalDir=??,               (When Action=Folder - local path) RemoteDir=??,           (When Action=Folder - remote path) Pattern                        (When Action=Folder - optional file mask i.e. *.txt) ', '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = '6517FBEF-D9F6-4E42-8F30-C32A957378CA')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('6517FBEF-D9F6-4E42-8F30-C32A957378CA', 'ExecuteStoredProcedure', 'ExecuteStoredProcedure', null, null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = 'D47B6053-B0D6-4142-8220-C8266013E689')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('D47B6053-B0D6-4142-8220-C8266013E689', 'FailureTask', 'FailureTask', null, null, '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

IF NOT EXISTS (SELECT 1 FROM async_task WHERE task_id = '17AE7730-1A81-4413-B29D-DB7309082C52')
BEGIN
	INSERT INTO async_task 
	(task_id, task_name, moniker, task_context, description, creation_user_id, creation_date, modified_user_id, modified_date, client_id)
	VALUES ('17AE7730-1A81-4413-B29D-DB7309082C52', 'EmailEventAlert', 'EmailEventAlert', null, 'Provide a single parameter of EventName and any un-emailed event of that type the event details will be emailed to subscribers', '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), '5FDD6452-C6B3-418D-8F52-E7B802C12253', getdate(), 'E90A452E-F178-4377-BCFD-83EC7503467F')
END

