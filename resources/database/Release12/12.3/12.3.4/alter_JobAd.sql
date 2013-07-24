ALTER TABLE dbo.JobAd
ADD hideCompany BIT NULL
GO

UPDATE
	dbo.JobAd
SET
	hideCompany = 0
GO

ALTER TABLE dbo.JobAd
ALTER COLUMN hideCompany BIT NOT NULL
GO

