-- Create the spammer table.

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.SpammerReport') AND type in (N'U'))
DROP TABLE dbo.SpammerReport
GO

CREATE TABLE dbo.SpammerReport
(
	id UNIQUEIDENTIFIER NOT NULL,
	reportedByUserId UNIQUEIDENTIFIER NOT NULL,
	reportedTime DATETIME NOT NULL,
	userId UNIQUEIDENTIFIER NULL,
	firstName NVARCHAR(200) NULL,
	lastName NVARCHAR(200) NULL
)
GO

ALTER TABLE dbo.SpammerReport
ADD CONSTRAINT PK_SpammerReport PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE CLUSTERED INDEX [IX_SpammerReport_reported] ON [dbo].[SpammerReport]
(
	userId,
	reportedTime	
)
GO