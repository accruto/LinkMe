IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdNote') AND type in (N'U'))
DROP TABLE dbo.JobAdNote
GO

CREATE TABLE [dbo].[JobAdNote](
	[id] [uniqueidentifier] NOT NULL,
	[createdTime] [datetime] NOT NULL,
	[lastUpdatedTime] [datetime] NOT NULL,
	[text] [nvarchar](500) NOT NULL,
	[jobAdId] [uniqueidentifier] NOT NULL
)
GO

ALTER TABLE dbo.JobAdNote
ADD CONSTRAINT PK_JobAdNote PRIMARY KEY NONCLUSTERED
(
	id
)
GO

ALTER TABLE dbo.JobAdNote
ADD CONSTRAINT FK_JobAdNote_JobAd FOREIGN KEY ([jobAdId])
REFERENCES dbo.JobAd ([id])
GO
