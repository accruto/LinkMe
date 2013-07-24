-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhChannel]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhChannel]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhChannel
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullName VARCHAR(512) NOT NULL,
	containerId UNIQUEIDENTIFIER NOT NULL,
	description NVARCHAR(512) NOT NULL,
	threadPoolName VARCHAR(128) NOT NULL,
)
GO

ALTER TABLE dbo.FhChannel ADD
CONSTRAINT PK_FhChannel PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FhChannel_Container
ON dbo.FhChannel (containerId)
GO

CREATE UNIQUE INDEX IX_FhChannel_FullName
ON dbo.FhChannel (fullName)
GO

ALTER TABLE dbo.FhChannel ADD
CONSTRAINT FK_FhChannel_FhContainer FOREIGN KEY (containerId)
REFERENCES dbo.FhContainer (id)
ON DELETE CASCADE
GO
