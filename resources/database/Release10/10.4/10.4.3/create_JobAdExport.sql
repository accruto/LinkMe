IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobAdExport]') AND type in (N'U'))
DROP TABLE dbo.JobAdExport

GO

CREATE TABLE dbo.JobAdExport
(
	jobAdId UNIQUEIDENTIFIER NOT NULL,
	jobSearchVacancyId BIGINT,
	jobSearchControlNumber INT
)

ALTER TABLE dbo.JobAdExport ADD CONSTRAINT PK_JobAdExport
	PRIMARY KEY NONCLUSTERED (jobAdId)

GO

ALTER TABLE dbo.JobAdExport ADD CONSTRAINT FK_JobAdExport_JobAd 
	FOREIGN KEY(jobAdId) REFERENCES dbo.JobAd (id)

GO
