-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandlerType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiMessageHandlerType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiMessageHandlerType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	displayName VARCHAR(128),
	class VARCHAR(512) NOT NULL,
	configurationHandlerClass VARCHAR(512) NOT NULL,
	configurationPropertyPageClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FiMessageHandlerType ADD
CONSTRAINT PK_FiMessageHandlerType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiMessageHandlerType_Name
ON dbo.FiMessageHandlerType (name)
GO

