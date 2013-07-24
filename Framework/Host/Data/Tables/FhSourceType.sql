-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSourceType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhSourceType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhSourceType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	class VARCHAR(512) NOT NULL,
	configurationHandlerClass VARCHAR(512) NOT NULL,
	configurationPropertyPageClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FhSourceType ADD
CONSTRAINT PK_FhSourceType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FhSourceType_Name
ON dbo.FhSourceType (name)
GO

