-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcModule]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcModule]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcModule
(
	name VARCHAR(128) NOT NULL,
	displayName VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	extensionClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FcModule ADD
CONSTRAINT PK_FcModule PRIMARY KEY CLUSTERED (name)
GO

