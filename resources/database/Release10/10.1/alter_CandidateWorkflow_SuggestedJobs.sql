IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CandidateWorkflow') AND NAME = 'suggestedJobsWorkflowId')
BEGIN

	ALTER TABLE dbo.CandidateWorkflow
	ADD suggestedJobsWorkflowId UNIQUEIDENTIFIER NULL

END
	
GO
