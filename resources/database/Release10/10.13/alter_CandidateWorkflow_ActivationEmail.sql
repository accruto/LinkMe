IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CandidateWorkflow') AND NAME = 'activationEmailWorkflowId')
BEGIN

	ALTER TABLE dbo.CandidateWorkflow
	ADD activationEmailWorkflowId UNIQUEIDENTIFIER NULL

END
	
GO
