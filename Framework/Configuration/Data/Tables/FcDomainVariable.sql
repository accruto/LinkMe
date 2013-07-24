-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcDomainVariable]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcDomainVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcDomainVariable
(
	domainName VARCHAR(128) NOT NULL,
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	type VARCHAR(128) NOT NULL,
	value TEXT NOT NULL
)
GO

ALTER TABLE dbo.FcDomainVariable ADD
CONSTRAINT PK_FcDomainVariable PRIMARY KEY CLUSTERED (domainName, name)
GO

