EXEC sp_changeobjectowner 'linkme_owner.CandidateList', dbo
GO

-- Change ownerId to UNIQUEIDENTIFIER, updating constraints and indexes.

if exists (select * from dbo.sysindexes where name = N'IX_CandidateList_ownerId' and id = object_id(N'[dbo].[CandidateList]'))
drop index [dbo].[CandidateList].[IX_CandidateList_ownerId]

ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [FK_CandidateList_employer_profile]

EXEC sp_rename 'dbo.CandidateList.ownerId', '_ownerId', 'COLUMN'

ALTER TABLE dbo.CandidateList
ADD ownerId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.CandidateList
SET ownerId = dbo.GuidFromString(_ownerId)

GO

ALTER TABLE dbo.CandidateList
ALTER COLUMN ownerId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.CandidateList
DROP COLUMN _ownerId

CREATE INDEX IX_CandidateList_ownerId
ON dbo.CandidateList (ownerId)

ALTER TABLE dbo.CandidateList
ADD CONSTRAINT FK_CandidateList_OwnerEmployer
FOREIGN KEY (ownerId) REFERENCES dbo.Employer ([id])

GO
