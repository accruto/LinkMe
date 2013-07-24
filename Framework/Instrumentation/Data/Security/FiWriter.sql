-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysusers where name = N'FiWriter' and issqlrole=1)
	EXEC sp_droprole N'FiWriter'
GO


-------------------------------------------------------------------------------
-- Create

EXEC sp_addrole N'FiWriter'

-- Grant

GRANT EXECUTE ON dbo.FiEventDetailDelete TO FiWriter
GRANT EXECUTE ON dbo.FiEventDetailUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiEventTypeDelete TO FiWriter
GRANT EXECUTE ON dbo.FiEventTypeUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiMessageHandlerTypeDelete TO FiWriter
GRANT EXECUTE ON dbo.FiMessageHandlerTypeUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiMessageHandlerDelete TO FiWriter
GRANT EXECUTE ON dbo.FiMessageHandlerUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiMessageReaderTypeDelete TO FiWriter
GRANT EXECUTE ON dbo.FiMessageReaderTypeUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiMessageReaderDelete TO FiWriter
GRANT EXECUTE ON dbo.FiMessageReaderUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiNamespaceDelete TO FiWriter
GRANT EXECUTE ON dbo.FiNamespaceUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiSourceTypeDelete TO FiWriter
GRANT EXECUTE ON dbo.FiSourceTypeUpdate TO FiWriter
GRANT EXECUTE ON dbo.FiSourceDelete TO FiWriter
GRANT EXECUTE ON dbo.FiSourceUpdate TO FiWriter

GO