EXEC sp_changeobjectowner 'linkme_owner.ResumeSearch', dbo
GO

-- Change searcherId to UNIQUEIDENTIFIER, updating constraints.

ALTER TABLE [dbo].[ResumeSearch] DROP CONSTRAINT [FK_ResumeSearch_employer_profile]

GO

EXEC sp_rename 'dbo.ResumeSearch.searcherId', '_searcherId', 'COLUMN'

ALTER TABLE dbo.ResumeSearch
ADD searcherId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.ResumeSearch
SET searcherId = dbo.GuidFromString(_searcherId)

GO

ALTER TABLE dbo.ResumeSearch
DROP COLUMN _searcherId

GO

ALTER TABLE dbo.ResumeSearch
ADD CONSTRAINT FK_ResumeSearch_CandidateSearcher
FOREIGN KEY (searcherId) REFERENCES dbo.CandidateSearcher ([id])

GO
