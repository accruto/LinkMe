EXEC sp_changeobjectowner 'linkme_owner.CandidateResumeFile', dbo
GO

-- Add a new "id" column and make it the primary key

ALTER TABLE dbo.CandidateResumeFile
DROP CONSTRAINT PK_CandidateResumeFile
GO

ALTER TABLE dbo.CandidateResumeFile
ADD [id] UNIQUEIDENTIFIER NULL
GO

UPDATE dbo.CandidateResumeFile
SET [id] = NEWID()
GO

ALTER TABLE dbo.CandidateResumeFile
ALTER COLUMN [id] UNIQUEIDENTIFIER NOT NULL
GO

ALTER TABLE dbo.CandidateResumeFile
ADD CONSTRAINT PK_CandidateResumeFile
PRIMARY KEY CLUSTERED ([id])
GO

-- Convert the "candidateId" column to GUID

ALTER TABLE dbo.CandidateResumeFile
DROP CONSTRAINT FK_CandidateResumeFile_networker_profile
GO

EXEC sp_rename 'dbo.CandidateResumeFile.candidateId', '_candidateId', 'COLUMN'
GO

ALTER TABLE dbo.CandidateResumeFile
ADD candidateId UNIQUEIDENTIFIER NULL
GO

UPDATE dbo.CandidateResumeFile
SET candidateId = dbo.GuidFromString(_candidateId)
GO

ALTER TABLE dbo.CandidateResumeFile
ALTER COLUMN candidateId UNIQUEIDENTIFIER NOT NULL
GO

ALTER TABLE dbo.CandidateResumeFile
ADD CONSTRAINT FK_CandidateResumeFile_Candidate
FOREIGN KEY (candidateId) REFERENCES dbo.Candidate ([id])
GO

ALTER TABLE dbo.CandidateResumeFile
DROP COLUMN _candidateId
GO

-- Add uploadedTime (default it to lastUsedTime, which isn't exactly right, but is better than nothing)

ALTER TABLE dbo.CandidateResumeFile
ADD uploadedTime DATETIME NULL
GO

UPDATE dbo.CandidateResumeFile
SET uploadedTime = lastUsedTime
GO

ALTER TABLE dbo.CandidateResumeFile
ALTER COLUMN uploadedTime DATETIME NOT NULL
GO

-- Change the File foreign key to point to FileReference

ALTER TABLE dbo.CandidateResumeFile
DROP CONSTRAINT FK_CandidateResumeFile_File

ALTER TABLE dbo.CandidateResumeFile
ADD CONSTRAINT FK_CandidateResumeFile_FileReference
FOREIGN KEY (fileId) REFERENCES FileReference ([id])

GO
