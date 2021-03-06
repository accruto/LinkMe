-- Turn the primary key into a non-clustered index.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateAssessment]') AND name = N'PK_CandidateAssessment')
ALTER TABLE [dbo].[CandidateAssessment] DROP CONSTRAINT [PK_CandidateAssessment]
GO

ALTER TABLE [dbo].[CandidateAssessment]
ADD CONSTRAINT [PK_CandidateAssessment] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

-- Add index for candidateId

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CandidateAssessment]') AND name = N'IX_CandidateAssessment_candidateId')
DROP INDEX [IX_CandidateAssessment_candidateId] ON [dbo].[CandidateAssessment]
GO

CREATE CLUSTERED INDEX [IX_CandidateAssessment_candidateId] ON [dbo].[CandidateAssessment]
(
	[candidateId]
)
GO

