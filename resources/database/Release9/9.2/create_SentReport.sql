IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.SentReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.SentReport
GO

CREATE TABLE dbo.SentReport
(
	id uniqueidentifier NOT NULL,
	sentTime datetime NOT NULL,
	periodStart datetime NOT NULL,
	periodEnd datetime NOT NULL,
	reportDefinitionId uniqueidentifier NOT NULL,
	sentToAccountManagerId uniqueidentifier NULL,
	sentToClientEmail EmailAddress NULL
)
GO

ALTER TABLE dbo.SentReport
ADD CONSTRAINT PK_SentReport 
PRIMARY KEY NONCLUSTERED (id)
GO

ALTER TABLE dbo.SentReport
ADD CONSTRAINT FK_SentReport_AccountManager 
FOREIGN KEY (sentToAccountManagerId) REFERENCES dbo.Administrator (id)
GO

ALTER TABLE dbo.SentReport
ADD CONSTRAINT FK_SentReport_ReportDefinition 
FOREIGN KEY (reportDefinitionId) REFERENCES dbo.ReportDefinition (id)
GO








