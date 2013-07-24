-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhContainer]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhContainer]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhContainer
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullName VARCHAR(512) NOT NULL,
	namespaceId UNIQUEIDENTIFIER NOT NULL,
	description NVARCHAR(512) NOT NULL,
)
GO

ALTER TABLE dbo.FhContainer ADD
CONSTRAINT PK_FhContainer PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FhContainer_Namespace
ON dbo.FhContainer (namespaceId)
GO

CREATE UNIQUE INDEX IX_FhContainer_FullName
ON dbo.FhContainer (fullName)
GO

ALTER TABLE dbo.FhContainer ADD
CONSTRAINT FK_FhContainer_FhNamespace FOREIGN KEY (namespaceId)
REFERENCES dbo.FhNamespace (id)
ON DELETE CASCADE
GO
