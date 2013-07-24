-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiSourceType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiSourceType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiSourceType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	availableEvents VARCHAR(128),
)
GO

ALTER TABLE dbo.FiSourceType ADD
CONSTRAINT PK_FiSourceType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiSourceType_Name
ON dbo.FiSourceType (name)
GO

