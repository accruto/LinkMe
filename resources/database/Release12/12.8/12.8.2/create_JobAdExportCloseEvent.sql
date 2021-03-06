IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdExportCloseEvent') AND type in (N'U'))
DROP TABLE dbo.JobAdExportCloseEvent
GO

CREATE TABLE dbo.JobAdExportCloseEvent
(
	id UNIQUEIDENTIFIER NOT NULL,
	failed INT NOT NULL,
	closed INT NOT NULL,
	notFound INT NOT NULL
)

ALTER TABLE dbo.JobAdExportCloseEvent
ADD CONSTRAINT PK_JobAdExportCloseEvent PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.JobAdExportCloseEvent
ADD CONSTRAINT FK_JobAdExportCloseEvent_JobAdIntegrationEvent
FOREIGN KEY(id) REFERENCES dbo.JobAdIntegrationEvent (id)

GO
