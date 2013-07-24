-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF EXISTS (SELECT * FROM dbo.sysusers WHERE name = N'FiStoreReader' AND issqlrole=1)
BEGIN
	-- Is there an easier way to drop all role members?

	DECLARE csrRoleMember CURSOR FOR
	SELECT
		Users.name
	FROM
		dbo.sysusers Users
	INNER JOIN
		dbo.sysmembers ON Users.uid = memberuid
	INNER JOIN
		dbo.sysusers Roles ON groupuid = Roles.uid
	WHERE
		Roles.name = 'FiStoreReader'

	OPEN csrRoleMember

	DECLARE @roleMember VARCHAR(128)
	FETCH NEXT FROM csrRoleMember INTO @roleMember

	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC sp_droprolemember 'FiStoreReader', @roleMember
		FETCH NEXT FROM csrRoleMember INTO @roleMember
	END

	CLOSE csrRoleMember
	DEALLOCATE csrRoleMember

	EXEC sp_droprole N'FiStoreReader'
END
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FiStoreReader'

-- Grant

GRANT EXECUTE ON dbo.FiStoreGetAllMessages TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetMessage TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetMessageCount TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetMessageRange TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetMessageTimeRange TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetMessageTimeRangeFilter TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetMessageIdentifierRange TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetMessageIdentifierRangeFilter TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetParameters TO FiStoreReader
GRANT EXECUTE ON dbo.FiStoreGetDetails TO FiStoreReader

GO
