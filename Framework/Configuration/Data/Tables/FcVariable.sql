-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcVariable]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcVariable
(
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	type VARCHAR(128) NOT NULL,
	value TEXT NOT NULL
)
GO

ALTER TABLE dbo.FcVariable ADD
CONSTRAINT PK_FcVariable PRIMARY KEY CLUSTERED (name)
GO

