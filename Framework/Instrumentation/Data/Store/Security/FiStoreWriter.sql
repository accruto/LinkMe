-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

IF EXISTS (SELECT * FROM dbo.sysusers WHERE name = N'FiStoreWriter' AND issqlrole=1)
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
		Roles.name = 'FiStoreWriter'

	OPEN csrRoleMember

	DECLARE @roleMember VARCHAR(128)
	FETCH NEXT FROM csrRoleMember INTO @roleMember

	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC sp_droprolemember 'FiStoreWriter', @roleMember
		FETCH NEXT FROM csrRoleMember INTO @roleMember
	END

	CLOSE csrRoleMember
	DEALLOCATE csrRoleMember

	EXEC sp_droprole N'FiStoreWriter'
END
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FiStoreWriter'

-- Grant

GRANT EXECUTE ON dbo.FiStoreInsertMessage TO FiStoreWriter
GRANT EXECUTE ON dbo.FiStoreInsertParameter TO FiStoreWriter

GO
