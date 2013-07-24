-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSink]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FhSink]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FhSink
(
	id UNIQUEIDENTIFIER NOT NULL,
	name VARCHAR(128) NOT NULL,
	channelId UNIQUEIDENTIFIER NOT NULL,
	description NVARCHAR(512) NOT NULL,
	sinkType VARCHAR(128) NOT NULL,
	configurationData TEXT NOT NULL,
	[index] INT NOT NULL
)
GO

ALTER TABLE dbo.FhSink ADD
CONSTRAINT PK_FhSink PRIMARY KEY NONCLUSTERED (id, name)
GO

CREATE CLUSTERED INDEX IX_FhSink_Channel
ON dbo.FhSink (channelId)
GO

ALTER TABLE dbo.FhSink ADD
CONSTRAINT FK_FhSink_FhChannel FOREIGN KEY (channelId)
REFERENCES dbo.FhChannel (id)
ON DELETE CASCADE
GO
