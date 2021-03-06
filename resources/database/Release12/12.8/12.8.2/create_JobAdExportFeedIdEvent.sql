IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdExportFeedIdEvent') AND type in (N'U'))
DROP TABLE dbo.JobAdExportFeedIdEvent
GO

CREATE TABLE dbo.JobAdExportFeedIdEvent
(
	id UNIQUEIDENTIFIER NOT NULL
)

ALTER TABLE dbo.JobAdExportFeedIdEvent
ADD CONSTRAINT PK_JobAdExportFeedIdEvent PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.JobAdExportFeedIdEvent
ADD CONSTRAINT FK_JobAdExportFeedIdEvent_JobAdIntegrationEvent
FOREIGN KEY(id) REFERENCES dbo.JobAdIntegrationEvent (id)

GO
