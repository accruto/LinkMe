IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdImportPostEvent') AND type in (N'U'))
DROP TABLE dbo.JobAdImportPostEvent
GO

CREATE TABLE dbo.JobAdImportPostEvent
(
	id UNIQUEIDENTIFIER NOT NULL,
	posterId UNIQUEIDENTIFIER NULL,
	failed INT NOT NULL,
	posted INT NOT NULL,
	closed INT NOT NULL,
	updated INT NOT NULL,
	duplicates INT NOT NULL,
	ignored INT NOT NULL
)

ALTER TABLE dbo.JobAdImportPostEvent
ADD CONSTRAINT PK_JobAdImportPostEvent PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.JobAdImportPostEvent
ADD CONSTRAINT FK_JobAdImportPostEvent_JobAdIntegrationEvent
FOREIGN KEY(id) REFERENCES dbo.JobAdIntegrationEvent (id)

GO
