-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageReader]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiMessageReader]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiMessageReader
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(512) NOT NULL,
	messageReaderType VARCHAR(128) NOT NULL,
	configurationData TEXT NOT NULL,
)
GO

ALTER TABLE dbo.FiMessageReader ADD
CONSTRAINT PK_FiMessageReader PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiMessageReader_Name
ON dbo.FiMessageReader (name)
GO

