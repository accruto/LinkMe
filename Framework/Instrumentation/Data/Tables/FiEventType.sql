-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiEventType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FiEventType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FiEventType
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(512) NOT NULL,
	isEnabled BIT NOT NULL,
	eventDetails VARCHAR(512) NULL
)
GO

ALTER TABLE dbo.FiEventType ADD
CONSTRAINT PK_FiEventType PRIMARY KEY NONCLUSTERED (id)
GO

CREATE UNIQUE INDEX IX_FiEventType_Name
ON dbo.FiEventType (name)
GO

