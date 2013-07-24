-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysusers where name = N'FcReader' and issqlrole=1)
	EXEC sp_droprole N'FcReader'
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FcReader'

-- Grant

GRANT EXECUTE ON dbo.FcGetComputers TO FcReader
GRANT EXECUTE ON dbo.FcGetDomains TO FcReader
GRANT EXECUTE ON dbo.FcGetModules TO FcReader
GRANT EXECUTE ON dbo.FcGetRepositoryTypes TO FcReader
GRANT EXECUTE ON dbo.FcGetRepositories TO FcReader
GRANT EXECUTE ON dbo.FcGetStoreTypes TO FcReader
GRANT EXECUTE ON dbo.FcGetStores TO FcReader
GRANT EXECUTE ON dbo.FcGetDomainVariables TO FcReader
GRANT EXECUTE ON dbo.FcGetModuleVariables TO FcReader
GRANT EXECUTE ON dbo.FcGetVariables TO FcReader

GO