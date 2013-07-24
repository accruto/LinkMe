-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiStoreType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiStoreType
(
	id INT IDENTITY (1, 1) NOT NULL,
	fullName VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FiStoreType WITH NOCHECK
ADD CONSTRAINT PK_FiStoreType PRIMARY KEY CLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiStoreType_FullName
ON dbo.FiStoreType (fullName)
GO
