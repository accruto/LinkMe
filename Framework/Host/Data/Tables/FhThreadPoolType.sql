-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhThreadPoolType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhThreadPoolType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhThreadPoolType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	class VARCHAR(512) NOT NULL,
	configurationHandlerClass VARCHAR(512) NOT NULL,
	configurationPropertyPageClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FhThreadPoolType ADD
CONSTRAINT PK_FhThreadPoolType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FhThreadPoolType_Name
ON dbo.FhThreadPoolType (name)
GO

