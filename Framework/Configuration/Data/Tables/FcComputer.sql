-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcComputer]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcComputer]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcComputer
(
	domainName VARCHAR(128) NOT NULL,
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FcComputer ADD
CONSTRAINT PK_FcComputer PRIMARY KEY CLUSTERED (domainName, name)
GO

