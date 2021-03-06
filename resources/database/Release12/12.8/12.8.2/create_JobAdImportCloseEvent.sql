IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdImportCloseEvent') AND type in (N'U'))
DROP TABLE dbo.JobAdImportCloseEvent
GO

CREATE TABLE dbo.JobAdImportCloseEvent
(
	id UNIQUEIDENTIFIER NOT NULL,
	failed INT NOT NULL,
	closed INT NOT NULL,
	notFound INT NOT NULL
)

ALTER TABLE dbo.JobAdImportCloseEvent
ADD CONSTRAINT PK_JobAdImportCloseEvent PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.JobAdImportCloseEvent
ADD CONSTRAINT FK_JobAdImportCloseEvent_JobAdIntegrationEvent
FOREIGN KEY(id) REFERENCES dbo.JobAdIntegrationEvent (id)

GO
