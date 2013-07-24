IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdIndexing_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdIndexing]'))
ALTER TABLE [dbo].[JobAdIndexing] DROP CONSTRAINT [FK_JobAdIndexing_JobAd]

/****** Object:  Table [dbo].[JobAdIndexing]    Script Date: 10/28/2010 10:12:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobAdIndexing]') AND type in (N'U'))
DROP TABLE [dbo].[JobAdIndexing]

CREATE TABLE dbo.JobAdIndexing
(
	jobAdId UNIQUEIDENTIFIER NOT NULL,
	modifiedTime DATETIME NOT NULL
)

ALTER TABLE dbo.JobAdIndexing ADD CONSTRAINT PK_JobAdIndexing
	PRIMARY KEY NONCLUSTERED (jobAdId)

ALTER TABLE dbo.JobAdIndexing ADD CONSTRAINT FK_JobAdIndexing_JobAd 
	FOREIGN KEY(jobAdId) REFERENCES dbo.JobAd (id)

CREATE CLUSTERED INDEX IX_JobAdIndexing_modifiedTime ON dbo.JobAdIndexing
(
	modifiedTime
)
