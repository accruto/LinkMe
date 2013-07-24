-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysusers where name = N'FhWriter' and issqlrole=1)
	EXEC sp_droprole N'FhWriter'
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FhWriter'

-- Grant

GRANT EXECUTE ON dbo.FhContainerDelete TO FhWriter
GRANT EXECUTE ON dbo.FhContainerUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhChannelDelete TO FhWriter
GRANT EXECUTE ON dbo.FhChannelUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhSinkTypeDelete TO FhWriter
GRANT EXECUTE ON dbo.FhSinkTypeUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhSinkDelete TO FhWriter
GRANT EXECUTE ON dbo.FhSinkUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhSourceTypeDelete TO FhWriter
GRANT EXECUTE ON dbo.FhSourceTypeUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhSourceDelete TO FhWriter
GRANT EXECUTE ON dbo.FhSourceUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhNamespaceDelete TO FhWriter
GRANT EXECUTE ON dbo.FhNamespaceUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhThreadPoolTypeDelete TO FhWriter
GRANT EXECUTE ON dbo.FhThreadPoolTypeUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhThreadPoolDelete TO FhWriter
GRANT EXECUTE ON dbo.FhThreadPoolUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhApplicationTypeDelete TO FhWriter
GRANT EXECUTE ON dbo.FhApplicationTypeUpdate TO FhWriter
GRANT EXECUTE ON dbo.FhApplicationDelete TO FhWriter
GRANT EXECUTE ON dbo.FhApplicationUpdate TO FhWriter


GO