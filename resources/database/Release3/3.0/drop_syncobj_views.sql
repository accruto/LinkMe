-- Drop all views starting with syncobj_. These are used by replication (not sure how exactly) and
-- they are now all out of date anyway.

DECLARE curSyncViews CURSOR FOR
SELECT [name]
FROM dbo.sysobjects
WHERE [name] LIKE 'syncobj_%' AND OBJECTPROPERTY(id, N'IsView') = 1

OPEN curSyncViews

DECLARE @viewName sysname

FETCH NEXT FROM curSyncViews
INTO @viewName

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @command NVARCHAR(1000)
	SET @command = N'drop view ' + @viewName

	EXEC sp_executesql @command

	FETCH NEXT FROM curSyncViews
	INTO @viewName
END

CLOSE curSyncViews
DEALLOCATE curSyncViews

GO
