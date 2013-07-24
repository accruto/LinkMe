-- For some reason the filename type is owned by linkme_owner in UAT, but dbo in prod,
-- so we have to do the hack below.

DECLARE @typename sysname

SELECT @typename = '[' + USER_NAME(uid) + '].[filename]'
FROM dbo.systypes
WHERE [name] = 'filename'

EXEC sp_rename @typename, '_filename', 'USERDATATYPE'
GO

