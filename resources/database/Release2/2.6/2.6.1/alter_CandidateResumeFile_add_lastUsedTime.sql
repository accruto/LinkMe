ALTER TABLE linkme_owner.CandidateResumeFile
ADD lastUsedTime DATETIME NULL
GO

ALTER TABLE linkme_owner.CandidateResumeFile
ALTER COLUMN lastUsedTime DATETIME NOT NULL
GO
