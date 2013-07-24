IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.ReportDefinition') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.ReportDefinition
GO

CREATE TABLE dbo.ReportDefinition
( 
	id uniqueidentifier NOT NULL,
	reportType varchar(50) NOT NULL,
	toAccountManager bit NOT NULL,
	toClient bit NOT NULL,
	organisationId uniqueidentifier NOT NULL,
	parameterSetId uniqueidentifier NULL
)
GO

ALTER TABLE dbo.ReportDefinition
ADD CONSTRAINT PK_ReportDefinition
PRIMARY KEY NONCLUSTERED (id)
GO

ALTER TABLE dbo.ReportDefinition
ADD CONSTRAINT FK_ReportDefinition_Organisation 
FOREIGN KEY (organisationId) REFERENCES dbo.Organisation (id)
GO

ALTER TABLE dbo.ReportDefinition
ADD CONSTRAINT FK_ReportDefinition_ReportParameterSet 
FOREIGN KEY (parameterSetId) REFERENCES ReportParameterSet (id)
GO

ALTER TABLE dbo.ReportDefinition
ADD CONSTRAINT UQ_ReportDefinition_organisation_type
UNIQUE (organisationId, reportType)
GO
