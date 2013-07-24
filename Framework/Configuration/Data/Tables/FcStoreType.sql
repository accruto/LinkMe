-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcStoreType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcStoreType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcStoreType
(
	moduleName VARCHAR(128) NOT NULL,
	name VARCHAR(128) NOT NULL,
	displayName VARCHAR(128) NOT NULL,
	storeDisplayName VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	class VARCHAR(512) NOT NULL,
	extensionClass VARCHAR(512) NOT NULL,
	isFileStore BIT NOT NULL
)
GO

ALTER TABLE dbo.FcStoreType ADD
CONSTRAINT PK_FcStoreType PRIMARY KEY CLUSTERED (moduleName, name)
GO

