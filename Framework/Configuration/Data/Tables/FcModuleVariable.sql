-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcModuleVariable]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcModuleVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcModuleVariable
(
	moduleName VARCHAR(128) NOT NULL,
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	type VARCHAR(128) NOT NULL,
	value TEXT NOT NULL
)
GO

ALTER TABLE dbo.FcModuleVariable ADD
CONSTRAINT PK_FcModuleVariable PRIMARY KEY CLUSTERED (moduleName, name)
GO

