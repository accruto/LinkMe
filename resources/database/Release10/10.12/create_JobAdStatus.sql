IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdStatus') AND type in (N'U'))
DROP TABLE dbo.JobAdStatus
GO

CREATE TABLE dbo.JobAdStatus
(
	id UNIQUEIDENTIFIER NOT NULL,
	jobAdId UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	previousStatus JobAdStatus NOT NULL,
	newStatus JobAdStatus NOT NULL
)

ALTER TABLE dbo.JobAdStatus
ADD CONSTRAINT PK_JobAdStatus PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_JobAdStatus_jobAdId] ON [dbo].[JobAdStatus]
(
	jobAdId
)
GO

ALTER TABLE dbo.JobAdStatus
ADD CONSTRAINT FK_JobAdStatus_JobAd FOREIGN KEY (jobAdId)
REFERENCES dbo.JobAd (id)
GO


