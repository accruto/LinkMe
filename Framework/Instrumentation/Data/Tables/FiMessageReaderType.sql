-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageReaderType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiMessageReaderType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiMessageReaderType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	displayName VARCHAR(128),
	class VARCHAR(512) NOT NULL,
	configurationHandlerClass VARCHAR(512) NOT NULL,
	configurationPropertyPageClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FiMessageReaderType ADD
CONSTRAINT PK_FiMessageReaderType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiMessageReaderType_Name
ON dbo.FiMessageReaderType (name)
GO

