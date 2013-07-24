-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhApplication]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhApplication]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhApplication
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	applicationType VARCHAR(128) NOT NULL,
	configurationData TEXT NOT NULL,
)
GO

ALTER TABLE dbo.FhApplication ADD
CONSTRAINT PK_FhApplication PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FhApplication_Name
ON dbo.FhApplication (name)
GO

