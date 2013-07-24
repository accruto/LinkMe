-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiEventDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiEventDetail]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiEventDetail
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(512) NOT NULL,
	factoryClass VARCHAR(512) NOT NULL
)
GO

ALTER TABLE dbo.FiEventDetail ADD
CONSTRAINT PK_FiEventDetail PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiEventDetail_Name
ON dbo.FiEventDetail (name)
GO

