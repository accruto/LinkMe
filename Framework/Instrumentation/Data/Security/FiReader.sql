-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysusers where name = N'FiReader' and issqlrole=1)
	EXEC sp_droprole N'FiReader'
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FiReader'

-- Grant

GRANT EXECUTE ON dbo.FiNamespacesGet TO FiReader
GRANT EXECUTE ON dbo.FiEventDetailsGet TO FiReader
GRANT EXECUTE ON dbo.FiEventTypesGet TO FiReader
GRANT EXECUTE ON dbo.FiMessageHandlerTypesGet TO FiReader
GRANT EXECUTE ON dbo.FiMessageHandlersGet TO FiReader
GRANT EXECUTE ON dbo.FiMessageReaderTypesGet TO FiReader
GRANT EXECUTE ON dbo.FiMessageReadersGet TO FiReader
GRANT EXECUTE ON dbo.FiNamespacesGet TO FiReader
GRANT EXECUTE ON dbo.FiSourceTypesGet TO FiReader
GRANT EXECUTE ON dbo.FiSourcesGet TO FiReader

GO