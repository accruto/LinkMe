IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'nagios')
	CREATE LOGIN nagios WITH PASSWORD = N'changeme2day', DEFAULT_DATABASE = [master], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'nagios')
	CREATE USER nagios FOR LOGIN nagios

EXEC sp_executesql N'USE master; GRANT VIEW SERVER STATE TO nagios'

GRANT VIEW DATABASE STATE TO nagios

GO
