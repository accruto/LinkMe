USE master
GO

DECLARE @dbVersion SYSNAME
SET @dbVersion = '9.3.0' -- Update this to the required version.

-- Kill DB connections

DECLARE @DBName varchar(50)

SET @DBName = 'LinkMe'

DECLARE @spidstr varchar(8000)
DECLARE @ConnKilled smallint
SET @ConnKilled=0
SET @spidstr = ''

SELECT @spidstr=coalesce(@spidstr,',' )+'kill '+convert(varchar, spid)+ '; '
FROM master..sysprocesses WHERE dbid=db_id(@DBName)

IF LEN(@spidstr) > 0 
BEGIN
	EXEC(@spidstr)

	SELECT @ConnKilled = COUNT(1)
	FROM master..sysprocesses WHERE dbid=db_id(@DBName) 

END

PRINT  CONVERT(VARCHAR(10), @ConnKilled) + ' Connection(s) killed for DB '  + @DBName

-- Restore

DECLARE @dbBackupPath SYSNAME
SET @dbBackupPath = '\\INTRANET2\DBBuilds\Build_' + @dbVersion + '_latest.bak'

-- Make sure we're not running in UAT or prod before restoring

IF (NOT (@@SERVERNAME LIKE 'DEVDB%' OR @@SERVERNAME LIKE 'DB%' OR @@SERVERNAME LIKE 'LINKMEDB%'))
	RESTORE DATABASE LinkMe
	FROM DISK = @dbBackupPath
	WITH REPLACE,
		MOVE 'LinkMe_Data' TO 'C:\SqlBuildData\LinkMe_Data.mdf',
		MOVE 'LinkMe_Log' TO 'C:\SqlBuildData\LinkMe_Log.ldf',
		MOVE 'sysft_CandidateCatalog' TO 'C:\SqlBuildData\LinkMe_sysft_CandidateCatalog',
		MOVE 'sysft_OrganisationCatalog' TO 'C:\SqlBuildData\LinkMe_sysft_OrganisationCatalog',
		MOVE 'sysft_JobAdCatalog' TO 'C:\SqlBuildData\LinkMe_sysft_JobAdCatalog'

GO

USE LinkMe
GO

-- Fix users

EXEC sp_change_users_login 'Auto_Fix', 'linkme_owner'
EXEC sp_change_users_login 'Auto_Fix', 'linkme_dev'
GO

-- Fix full text catalogs

EXEC sp_fulltext_database 'enable'

EXEC sp_fulltext_catalog 'JobAdCatalog', 'Rebuild'
EXEC sp_fulltext_catalog 'JobAdCatalog', 'start_full'

EXEC sp_fulltext_catalog 'OrganisationCatalog', 'Rebuild'
EXEC sp_fulltext_catalog 'OrganisationCatalog', 'start_full'

EXEC sp_fulltext_catalog 'CandidateCatalog', 'Rebuild'
EXEC sp_fulltext_catalog 'CandidateCatalog', 'start_full'

GO
