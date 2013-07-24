/*

Run this script if, after restoring a database that was replicated you get the following 
error when trying to truncate the log:

The log was not truncated because records at the beginning of the log are pending replication.
Ensure the Log Reader Agent is running or use sp_repldone to mark transactions as distributed.

If you get "The Distributor has not been installed correctly." then you need to configure
replication. In Enterprise Manager go to  Replication\Publications and select
"Configure Publishing, Subscribers and Distribution" from the context menu.

*/

DECLARE @dbName SYSNAME
SET @dbName = DB_NAME()

EXEC sp_replicationdboption @dbName, 'publish', 'true'
EXEC sp_repldone @xactid = NULL, @xact_segno = NULL, @numtrans = 0, @time = 0, @reset = 1
EXEC sp_replicationdboption @dbName, 'publish', 'false'

GO
