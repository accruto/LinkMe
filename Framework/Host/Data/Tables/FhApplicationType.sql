-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhApplicationType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhApplicationType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhApplicationType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	displayName VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	configurationHandlerClass VARCHAR(512) NOT NULL,
	configurationPropertyPageClass VARCHAR(512) NOT NULL,
	extensionClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FhApplicationType ADD
CONSTRAINT PK_FhApplicationType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FhApplicationType_Name
ON dbo.FhApplicationType (name)
GO

