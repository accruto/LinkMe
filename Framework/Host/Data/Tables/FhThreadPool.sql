-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhThreadPool]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhThreadPool]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhThreadPool
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullName VARCHAR(512) NOT NULL,
	containerId UNIQUEIDENTIFIER NOT NULL,
	description NVARCHAR(512) NOT NULL,
	threadPoolType VARCHAR(128) NOT NULL,
	configurationData TEXT NOT NULL,
)
GO

ALTER TABLE dbo.FhThreadPool ADD
CONSTRAINT PK_FhThreadPool PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FhThreadPool_Container
ON dbo.FhThreadPool (containerId)
GO

CREATE UNIQUE INDEX IX_FhThreadPool_FullName
ON dbo.FhThreadPool (fullName)
GO

ALTER TABLE dbo.FhThreadPool ADD
CONSTRAINT FK_FhThreadPool_FhContainer FOREIGN KEY (containerId)
REFERENCES dbo.FhContainer (id)
ON DELETE CASCADE
GO
