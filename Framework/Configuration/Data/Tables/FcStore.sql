-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcStore]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcStore]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcStore
(
	moduleName VARCHAR(128) NOT NULL,
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	storeType VARCHAR(128) NOT NULL,
	initialisationString VARCHAR(512) NOT NULL,
	repositoryType VARCHAR(128) NOT NULL,
	repositoryInitialisationString VARCHAR(512) NOT NULL,
	isReadOnly BIT NOT NULL
)
GO

ALTER TABLE dbo.FcStore ADD
CONSTRAINT PK_FcStore PRIMARY KEY CLUSTERED (moduleName, name)
GO

