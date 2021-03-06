IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdListEntry') AND type in (N'U'))
DROP TABLE dbo.JobAdListEntry
GO

CREATE TABLE [dbo].[JobAdListEntry](
	[jobAdListId] [uniqueidentifier] NOT NULL,
	[jobAdId] [uniqueidentifier] NOT NULL,
	[createdTime] [datetime] NOT NULL
)

ALTER TABLE dbo.JobAdListEntry
ADD CONSTRAINT PK_JobAdListEntry PRIMARY KEY CLUSTERED
(
	jobAdListId,
	jobAdId
)
GO

ALTER TABLE dbo.JobAdListEntry
ADD CONSTRAINT FK_JobAdListEntry_JobAd FOREIGN KEY (jobAdId)
REFERENCES dbo.JobAd (id)
GO

ALTER TABLE dbo.JobAdListEntry
ADD CONSTRAINT FK_JobAdListEntry_JobAdList FOREIGN KEY (jobAdListId)
REFERENCES dbo.JobAdList (id)
GO
