IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdExportPostEvent') AND type in (N'U'))
DROP TABLE dbo.JobAdExportPostEvent
GO

CREATE TABLE dbo.JobAdExportPostEvent
(
	id UNIQUEIDENTIFIER NOT NULL,
	failed INT NOT NULL,
	posted INT NOT NULL,
	updated INT NOT NULL
)

ALTER TABLE dbo.JobAdExportPostEvent
ADD CONSTRAINT PK_JobAdExportPostEvent PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.JobAdExportPostEvent
ADD CONSTRAINT FK_JobAdExportPostEvent_JobAdIntegrationEvent
FOREIGN KEY(id) REFERENCES dbo.JobAdIntegrationEvent (id)

GO
