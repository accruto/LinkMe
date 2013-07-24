-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiSource]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiSource]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiSource
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullyQualifiedReference VARCHAR(512) NOT NULL,
	namespaceId UNIQUEIDENTIFIER NOT NULL,
	enabledEvents NVARCHAR(128) NULL,
	sourceType NVARCHAR(128) NULL
)
GO

ALTER TABLE dbo.FiSource ADD
CONSTRAINT PK_FiSource PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FiSource_Namespace
ON dbo.FiSource (namespaceId)
GO

CREATE UNIQUE INDEX IX_FiSource_FullyQualifiedReference
ON dbo.FiSource (fullyQualifiedReference)
GO

ALTER TABLE dbo.FiSource ADD
CONSTRAINT FK_FiSource_FiNamespace FOREIGN KEY (namespaceId)
REFERENCES dbo.FiNamespace (id)
ON DELETE CASCADE
GO
