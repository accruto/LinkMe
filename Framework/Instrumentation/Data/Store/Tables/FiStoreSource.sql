-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreSource]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiStoreSource]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiStoreSource
(
	id INT IDENTITY (1, 1) NOT NULL,
	fullName VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FiStoreSource WITH NOCHECK
ADD CONSTRAINT PK_FiStoreSource PRIMARY KEY CLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiStoreSource_FullName
ON dbo.FiStoreSource (fullName)
GO
