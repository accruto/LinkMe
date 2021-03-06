IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdExportFeedEvent') AND type in (N'U'))
DROP TABLE dbo.JobAdExportFeedEvent
GO

CREATE TABLE dbo.JobAdExportFeedEvent
(
	id UNIQUEIDENTIFIER NOT NULL
)

ALTER TABLE dbo.JobAdExportFeedEvent
ADD CONSTRAINT PK_JobAdExportFeedEvent PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.JobAdExportFeedEvent
ADD CONSTRAINT FK_JobAdExportFeedEvent_JobAdIntegrationEvent
FOREIGN KEY(id) REFERENCES dbo.JobAdIntegrationEvent (id)

GO
