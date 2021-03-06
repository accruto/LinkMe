IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdRefresh') AND type in (N'U'))
DROP TABLE dbo.JobAdRefresh
GO

CREATE TABLE dbo.JobAdRefresh
(
	id UNIQUEIDENTIFIER NOT NULL,
	lastRefreshTime DATETIME NULL
)

ALTER TABLE dbo.JobAdRefresh
ADD CONSTRAINT PK_JobAdRefresh PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE CLUSTERED INDEX IX_JobAdRefresh_refreshTime ON dbo.JobAdRefresh
(
	lastRefreshTime
)
GO

ALTER TABLE dbo.JobAdRefresh
ADD CONSTRAINT FK_JobAdRefresh_JobAd FOREIGN KEY (id)
REFERENCES dbo.JobAd (id)
GO
