-- Fix the "orphaned" users - logins with these names should already exist.

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'linkme_owner')
	EXEC sp_addlogin 'linkme_owner', 'linkme'

EXEC sp_change_users_login 'Auto_Fix', 'linkme_owner'

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'LinkMeDev')
	EXEC sp_addlogin 'LinkMeDev', 'linkme'

EXEC sp_change_users_login 'Auto_Fix', 'LinkMeDev'

DECLARE @loginName SYSNAME
SET @loginName = CAST(SERVERPROPERTY('MachineName') AS SYSNAME) + '\LinkMeApp'

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'LinkMeApp')
	EXEC sp_grantlogin @loginName

IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'LinkMeApp')
	DROP USER LinkMeApp

DECLARE @sql NVARCHAR(200)
SET @sql = 'CREATE USER LinkMeApp FOR LOGIN [' + @loginName + ']'
EXEC sp_executesql @sql

EXEC sp_addrolemember 'db_owner', 'LinkMeApp'

GO