ALTER TABLE dbo.CandidateList
ADD createdTime DATETIME NULL
GO

ALTER TABLE dbo.CandidateList
ADD isDeleted BIT NULL
GO

UPDATE dbo.CandidateList
SET isDeleted = 0
GO

ALTER TABLE dbo.CandidateList
ALTER COLUMN isDeleted BIT NOT NULL
GO

ALTER TABLE dbo.CandidateList
ADD sharedWithId UNIQUEIDENTIFIER NULL
GO
