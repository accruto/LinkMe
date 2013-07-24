
IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('JobAd') AND NAME = 'jobg8ApplyForm')
BEGIN
	ALTER TABLE dbo.JobAd
		ADD jobg8ApplyForm TEXT
END
ELSE
BEGIN
	ALTER TABLE dbo.JobAd
		ALTER COLUMN jobg8ApplyForm TEXT
END
GO
