-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSource]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhSource]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhSource
(
	id UNIQUEIDENTIFIER NOT NULL,
	fullName VARCHAR(512) NOT NULL,
	containerId UNIQUEIDENTIFIER NOT NULL,
	description NVARCHAR(512) NOT NULL,
	sourceType VARCHAR(128) NOT NULL,
	configurationData TEXT NOT NULL,
	channelName VARCHAR(128) NOT NULL
)
GO

ALTER TABLE dbo.FhSource ADD
CONSTRAINT PK_FhSource PRIMARY KEY NONCLUSTERED (id)
GO

CREATE CLUSTERED INDEX IX_FhSource_Container
ON dbo.FhSource (containerId)
GO

CREATE UNIQUE INDEX IX_FhSource_FullName
ON dbo.FhSource (fullName)
GO

ALTER TABLE dbo.FhSource ADD
CONSTRAINT FK_FhSource_FhContainer FOREIGN KEY (containerId)
REFERENCES dbo.FhContainer (id)
ON DELETE CASCADE
GO
