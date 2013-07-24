EXEC sp_changeobjectowner 'linkme_owner.CandidateListEntry', dbo
GO

ALTER TABLE dbo.CandidateListEntry
ALTER COLUMN applicationStatus JobApplicationStatus NOT NULL
GO

-- Change ownerId to UNIQUEIDENTIFIER, updating constraints.

ALTER TABLE [dbo].[CandidateListEntry] DROP CONSTRAINT [FK_CandidateListEntry_NetworkerProfile]

ALTER TABLE [dbo].[CandidateListEntry] DROP CONSTRAINT [PK_CandidateListEntry]

GO

EXEC sp_rename 'dbo.CandidateListEntry.candidateId', '_candidateId', 'COLUMN'

ALTER TABLE dbo.CandidateListEntry
ADD candidateId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.CandidateListEntry
SET candidateId = dbo.GuidFromString(_candidateId)

GO

ALTER TABLE dbo.CandidateListEntry
ALTER COLUMN candidateId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.CandidateListEntry
DROP COLUMN _candidateId

GO

ALTER TABLE dbo.CandidateListEntry
ADD CONSTRAINT PK_CandidateListEntry 
PRIMARY KEY CLUSTERED (candidateListId, candidateId)

ALTER TABLE dbo.CandidateListEntry
ADD CONSTRAINT FK_CandidateListEntry_Candidate 
FOREIGN KEY (candidateId) REFERENCES dbo.Candidate ([id])

GO
