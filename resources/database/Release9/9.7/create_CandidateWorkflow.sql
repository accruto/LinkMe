/****** Object:  Table [dbo].[CandidateWorkflow]    Script Date: 07/06/2009 14:32:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CandidateWorkflow]') AND type in (N'U'))
DROP TABLE dbo.CandidateWorkflow

GO

CREATE TABLE dbo.CandidateWorkflow
(
	candidateId UNIQUEIDENTIFIER NOT NULL,
	statusWorkflowId UNIQUEIDENTIFIER
)

ALTER TABLE dbo.CandidateWorkflow ADD CONSTRAINT PK_CandidateWorkflow
	PRIMARY KEY NONCLUSTERED (candidateId)

GO

ALTER TABLE dbo.CandidateWorkflow ADD CONSTRAINT FK_CandidateWorkflow_Candidate 
	FOREIGN KEY(candidateId) REFERENCES dbo.Candidate (id)

GO
