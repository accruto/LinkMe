-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysusers where name = N'FcWriter' and issqlrole=1)
	EXEC sp_droprole N'FcWriter'
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FcWriter'

-- Grant

GRANT EXECUTE ON dbo.FcUpdateComputer TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteComputer TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateDomain TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteDomain TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateModule TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteModule TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateRepositoryType TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteRepositoryType TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateRepository TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteRepository TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateStoreType TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteStoreType TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateStore TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteStore TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateDomainVariable TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteDomainVariable TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateModuleVariable TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteModuleVariable TO FcWriter
GRANT EXECUTE ON dbo.FcUpdateVariable TO FcWriter
GRANT EXECUTE ON dbo.FcDeleteVariable TO FcWriter


GO