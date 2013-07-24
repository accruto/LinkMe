/*
If you get the error "The log was not truncated because records at the beginning of the log are pending
replication." then run "Clear pending replication transactions.sql"
*/

DECLARE @dbName SYSNAME
SET @dbName = DB_NAME()

BACKUP LOG @dbName WITH TRUNCATE_ONLY
DBCC SHRINKDATABASE(@dbName, 10)

GO

DBCC UPDATEUSAGE (0) WITH COUNT_ROWS
GO
