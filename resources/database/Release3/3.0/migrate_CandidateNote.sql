EXEC sp_changeobjectowner 'linkme_owner.CandidateNote', dbo
GO

ALTER TABLE dbo.CandidateNote
ADD createdTime DATETIME NULL

ALTER TABLE dbo.CandidateNote
ADD lastUpdatedTime DATETIME NULL

GO

-- Change text to NVARCHAR. ALTER COLUMN doesn't work for NTEXT, so it has to be done the long way.

EXEC sp_rename 'dbo.CandidateNote.text', '_text', 'COLUMN'

ALTER TABLE dbo.CandidateNote
ADD [text] NVARCHAR(500) NULL

GO

UPDATE dbo.CandidateNote
SET [text] = _text

GO

ALTER TABLE dbo.CandidateNote
ALTER COLUMN [text] NVARCHAR(500) NOT NULL

ALTER TABLE dbo.CandidateNote
DROP COLUMN _text

GO

-- Change recruiterId and candidateId to UNIQUEIDENTIFIER, updating constraints and indexes.
-- Also rename recruiterId to searcherId.

ALTER TABLE [dbo].[CandidateNote] DROP CONSTRAINT [FK_CandidateNote_employer_profile]

ALTER TABLE [dbo].[CandidateNote] DROP CONSTRAINT [FK_CandidateNote_networker_profile]

ALTER TABLE [dbo].[CandidateNote] DROP CONSTRAINT [UQ_CandidateNote_Recruiter_Candidate]

GO

ALTER TABLE dbo.CandidateNote
ADD searcherId UNIQUEIDENTIFIER NULL

EXEC sp_rename 'dbo.CandidateNote.candidateId', '_candidateId', 'COLUMN'

ALTER TABLE dbo.CandidateNote
ADD candidateId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.CandidateNote
SET searcherId = dbo.GuidFromString(recruiterId)

UPDATE dbo.CandidateNote
SET candidateId = dbo.GuidFromString(_candidateId)

GO

ALTER TABLE dbo.CandidateNote
ALTER COLUMN searcherId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.CandidateNote
ALTER COLUMN candidateId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.CandidateNote
DROP COLUMN recruiterId

ALTER TABLE dbo.CandidateNote
DROP COLUMN _candidateId

GO

CREATE UNIQUE INDEX IX_CandidateNote_Searcher_Candidate
ON dbo.CandidateNote (searcherId ASC, candidateId ASC)

ALTER TABLE dbo.CandidateNote
ADD CONSTRAINT FK_CandidateNote_Candidate
FOREIGN KEY (candidateId) REFERENCES dbo.Candidate ([id])

ALTER TABLE dbo.CandidateNote
ADD CONSTRAINT FK_CandidateNote_CandidateSearcher 
FOREIGN KEY (searcherId) REFERENCES dbo.CandidateSearcher ([id])

GO
