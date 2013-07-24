-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSinkType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhSinkType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhSinkType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	class VARCHAR(512) NOT NULL,
	configurationHandlerClass VARCHAR(512) NOT NULL,
	configurationPropertyPageClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FhSinkType ADD
CONSTRAINT PK_FhSinkType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FhSinkType_Name
ON dbo.FhSinkType (name)
GO

