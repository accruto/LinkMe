-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhNamespace]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhNamespace]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhNamespace
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullName VARCHAR(512) NOT NULL,
	parentId UNIQUEIDENTIFIER NULL
)
GO

ALTER TABLE dbo.FhNamespace ADD
CONSTRAINT PK_FhNamespace PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FhNamespace_Parent
ON dbo.FhNamespace (parentId)
GO

CREATE UNIQUE INDEX IX_FhNamespace_FullName
ON dbo.FhNamespace (fullName)
GO

ALTER TABLE dbo.FhNamespace ADD
CONSTRAINT FK_FhNamespace_FhNamespace FOREIGN KEY (parentId)
REFERENCES dbo.FhNamespace (id)
GO
