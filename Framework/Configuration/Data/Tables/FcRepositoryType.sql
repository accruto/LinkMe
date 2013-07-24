-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcRepositoryType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcRepositoryType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcRepositoryType
(
	moduleName VARCHAR(128) NOT NULL,
	name VARCHAR(128) NOT NULL,
	displayName VARCHAR(128) NOT NULL,
	repositoryDisplayName VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	class VARCHAR(512) NOT NULL,
	isFileRepository BIT NOT NULL,
	isVisible BIT NOT NULL
)
GO

ALTER TABLE dbo.FcRepositoryType ADD
CONSTRAINT PK_FcRepositoryType PRIMARY KEY CLUSTERED (moduleName, name)
GO

