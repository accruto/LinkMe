-- Change ownerId to UNIQUEIDENTIFIER, updating constraints.

ALTER TABLE [dbo].[SavedJobSearch] DROP CONSTRAINT [FK_SavedJobSearch_networker_profile]

EXEC sp_rename 'dbo.SavedJobSearch.ownerId', '_ownerId', 'COLUMN'

ALTER TABLE dbo.SavedJobSearch
ADD ownerId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.SavedJobSearch
SET ownerId = dbo.GuidFromString(_ownerId)

GO

ALTER TABLE dbo.SavedJobSearch
ALTER COLUMN ownerId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.SavedJobSearch
DROP COLUMN _ownerId

ALTER TABLE dbo.SavedJobSearch
ADD CONSTRAINT FK_SavedJobSearch_OwnerJobHunter
FOREIGN KEY (ownerId) REFERENCES dbo.JobHunter ([id])

GO
