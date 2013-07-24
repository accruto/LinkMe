ALTER TABLE dbo.CandidateNote
ADD isShared BIT NULL
GO

-- Set all existing notes to private

UPDATE dbo.CandidateNote
SET isShared = 0
GO

ALTER TABLE dbo.CandidateNote
ALTER COLUMN isShared BIT NOT NULL
GO
