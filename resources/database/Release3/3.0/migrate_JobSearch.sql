EXEC sp_changeobjectowner 'linkme_owner.JobSearch', dbo
GO

-- Change searcherId to UNIQUEIDENTIFIER, updating constraints and indexes.

if exists (select * from dbo.sysindexes where name = N'IX_JobSearch_searcherId' and id = object_id(N'[dbo].[JobSearch]'))
drop index [dbo].[JobSearch].[IX_JobSearch_searcherId]

ALTER TABLE [dbo].[JobSearch] DROP CONSTRAINT [FK_JobSearch_networker_profile]

GO

EXEC sp_rename 'dbo.JobSearch.searcherId', '_searcherId', 'COLUMN'

ALTER TABLE dbo.JobSearch
ADD searcherId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.JobSearch
SET searcherId = dbo.GuidFromString(_searcherId)

GO

ALTER TABLE dbo.JobSearch
DROP COLUMN _searcherId

GO

CREATE INDEX IX_JobSearch_searcherId
ON dbo.JobSearch (searcherId)

ALTER TABLE dbo.JobSearch
ADD CONSTRAINT FK_JobSearch_SearcherJobHunter
FOREIGN KEY (searcherId) REFERENCES dbo.JobHunter ([id])

GO
