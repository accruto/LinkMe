ALTER TABLE dbo.JobAd
ALTER COLUMN contactDetailsId UNIQUEIDENTIFIER NULL
GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('JobAd') AND NAME = 'integratorReferenceId')
BEGIN
	ALTER TABLE dbo.JobAd
	ADD integratorReferenceId VARCHAR(50) NULL
END
GO

ALTER TABLE dbo.JobAd
ALTER COLUMN positionTitle NVARCHAR(200) NULL
GO

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('JobAd') AND NAME = 'internalReferenceId')
BEGIN
	ALTER TABLE dbo.JobAd
	DROP COLUMN internalReferenceId
END
GO
