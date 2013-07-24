-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiNamespace]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiNamespace]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiNamespace
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullName VARCHAR(512) NOT NULL,
	parentId UNIQUEIDENTIFIER NULL,
	enabledEvents NVARCHAR(128) NULL,
	mixedEvents NVARCHAR(128) NULL
)
GO

ALTER TABLE dbo.FiNamespace ADD
CONSTRAINT PK_FiNamespace PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FiNamespace_Parent
ON dbo.FiNamespace (parentId)
GO

CREATE UNIQUE INDEX IX_FiNamespace_FullName
ON dbo.FiNamespace (fullName)
GO

ALTER TABLE dbo.FiNamespace ADD
CONSTRAINT FK_FiNamespace_FiNamespace FOREIGN KEY (parentId)
REFERENCES dbo.FiNamespace (id)
GO
