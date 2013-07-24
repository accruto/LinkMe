-- Set the existing Companies to "verified" (by linkmeadmin since we don't know who really verified them).

DECLARE @adminId UNIQUEIDENTIFIER

SELECT @adminId = ru.[id]
FROM dbo.RegisteredUser ru
INNER JOIN dbo.Administrator a
ON ru.[id] = a.[id]
WHERE loginId = 'linkmeadmin' OR loginId = 'cccccc'

IF @adminId IS NULL
	RAISERROR('There is no administrator with login ID "linkmeadmin" or "cccccc".', 16, 1)

UPDATE dbo.Company
SET verifiedById = @adminId

GO
