DECLARE @loginName SYSNAME
SET @loginName = CAST(SERVERPROPERTY('MachineName') AS SYSNAME) + '\LinkMeApp'

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE [name] = @loginName)
BEGIN
	DECLARE @sql NVARCHAR(1000)
	SET @sql = 'CREATE LOGIN [' + @loginName + '] FROM WINDOWS'
	EXEC sp_executesql @sql
END
GO
