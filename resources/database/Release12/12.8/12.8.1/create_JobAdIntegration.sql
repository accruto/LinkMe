IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdIntegrationEvent') AND type in (N'U'))
DROP TABLE dbo.JobAdIntegrationEvent
GO

CREATE TABLE dbo.JobAdIntegrationEvent
(
	id UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	integratorUserId UNIQUEIDENTIFIER NOT NULL,
	eventType INT NOT NULL,
	success BIT NOT NULL,
	jobAds INT NULL,
	failed INT NULL,
	posted INT NULL,
	closed INT NULL,
	updated INT NULL
)

ALTER TABLE dbo.JobAdIntegrationEvent
ADD CONSTRAINT PK_JobAdIntegrationEvent PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX [IX_JobAdIntegrationEvent_time] ON [dbo].[JobAdIntegrationEvent]
(
	time ASC
)

GO
