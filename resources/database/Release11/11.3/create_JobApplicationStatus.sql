BEGIN TRANSACTION
GO
ALTER TABLE dbo.CandidateListEntry ADD
	jobApplicationStatus dbo.JobApplicationStatus NULL
GO
COMMIT
