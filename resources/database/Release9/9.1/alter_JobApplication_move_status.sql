IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.JobApplication') AND NAME = 'status')
BEGIN
	ALTER TABLE dbo.JobApplication
	ADD [status] JobApplicationStatus NULL
END
GO

UPDATE dbo.JobApplication
SET [status] = cle.applicationStatus
FROM dbo.CandidateListEntry cle
INNER JOIN dbo.JobApplication ja
ON cle.jobApplicationId = ja.[id]
GO

ALTER TABLE dbo.JobApplication
ALTER COLUMN [status] JobApplicationStatus NOT NULL

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.CandidateListEntry') AND NAME = 'applicationStatus')
BEGIN
	ALTER TABLE dbo.CandidateListEntry
	DROP COLUMN applicationStatus
END
GO
