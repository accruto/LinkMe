BEGIN TRANSACTION
GO
ALTER TABLE dbo.JobApplication
	DROP COLUMN status
GO
COMMIT