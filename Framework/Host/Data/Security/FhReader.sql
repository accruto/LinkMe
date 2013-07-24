-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysusers where name = N'FhReader' and issqlrole=1)
	EXEC sp_droprole N'FhReader'
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FhReader'

-- Grant

GRANT EXECUTE ON dbo.FhContainersGet TO FhReader
GRANT EXECUTE ON dbo.FhChannelsGet TO FhReader
GRANT EXECUTE ON dbo.FhSinkTypesGet TO FhReader
GRANT EXECUTE ON dbo.FhSinksGet TO FhReader
GRANT EXECUTE ON dbo.FhSourceTypesGet TO FhReader
GRANT EXECUTE ON dbo.FhSourcesGet TO FhReader
GRANT EXECUTE ON dbo.FhNamespacesGet TO FhReader
GRANT EXECUTE ON dbo.FhThreadPoolTypesGet TO FhReader
GRANT EXECUTE ON dbo.FhThreadPoolsGet TO FhReader
GRANT EXECUTE ON dbo.FhApplicationTypesGet TO FhReader
GRANT EXECUTE ON dbo.FhApplicationsGet TO FhReader

GO