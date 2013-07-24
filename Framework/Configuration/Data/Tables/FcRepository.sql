-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcRepository]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcRepository]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcRepository
(
	moduleName VARCHAR(128) NOT NULL,
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	repositoryType VARCHAR(128) NOT NULL,
	initialisationString VARCHAR(512) NOT NULL,
	isReadOnly BIT NOT NULL,
	isLocal BIT NOT NULL
)
GO

ALTER TABLE dbo.FcRepository ADD
CONSTRAINT PK_FcRepository PRIMARY KEY CLUSTERED (moduleName, name)
GO

