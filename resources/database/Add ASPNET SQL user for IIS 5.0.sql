-- For IIS 5.0 machines only, ie. Windows XP 32-bit

DECLARE @loginName SYSNAME
SET @loginName = CAST(SERVERPROPERTY('MachineName') AS SYSNAME) + '\ASPNET'

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'ASPNET')
	EXEC sp_grantlogin @loginName

IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ASPNET')
	DROP USER [ASPNET]

DECLARE @sql NVARCHAR(200)
SET @sql = 'CREATE USER ASPNET FOR LOGIN [' + @loginName + ']'
EXEC sp_executesql @sql

EXEC sp_addrolemember 'db_owner', 'ASPNET'

GO