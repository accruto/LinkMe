-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandler]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiMessageHandler]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiMessageHandler
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullName VARCHAR(512) NOT NULL,
	parentId UNIQUEIDENTIFIER NULL,
	messageHandlerType VARCHAR(128) NULL,
	configurationData TEXT NULL,
)
GO

ALTER TABLE dbo.FiMessageHandler ADD
CONSTRAINT PK_FiMessageHandler PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FiMessageHandler_Parent
ON dbo.FiMessageHandler (parentId)
GO

CREATE UNIQUE INDEX IX_FiMessageHandler_FullName
ON dbo.FiMessageHandler (fullName)
GO

ALTER TABLE dbo.FiMessageHandler ADD
CONSTRAINT FK_FiMessageHandler_FiMessageHandler FOREIGN KEY (parentId)
REFERENCES dbo.FiMessageHandler (id)
GO
