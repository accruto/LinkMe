EXEC sp_changeobjectowner 'linkme_owner.SavedResumeSearch', dbo
GO

-- Drop the displayText column - it's no longer used and has been removed from the model.

ALTER TABLE [dbo].[SavedResumeSearch]
DROP COLUMN displayText
GO

-- Change ownerId to UNIQUEIDENTIFIER, updating constraints.

ALTER TABLE [dbo].[SavedResumeSearch]
DROP CONSTRAINT [FK_SavedResumeSearch_employer_profile]

EXEC sp_rename 'dbo.SavedResumeSearch.ownerId', '_ownerId', 'COLUMN'

ALTER TABLE dbo.SavedResumeSearch
ADD ownerId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.SavedResumeSearch
SET ownerId = dbo.GuidFromString(_ownerId)

GO

ALTER TABLE dbo.SavedResumeSearch
ALTER COLUMN ownerId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.SavedResumeSearch
DROP COLUMN _ownerId

ALTER TABLE dbo.SavedResumeSearch
ADD CONSTRAINT FK_SavedResumeSearch_OwnerCandidateSearcher
FOREIGN KEY (ownerId) REFERENCES dbo.CandidateSearcher ([id])

GO
