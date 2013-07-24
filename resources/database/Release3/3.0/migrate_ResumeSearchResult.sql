EXEC sp_changeobjectowner 'linkme_owner.ResumeSearchResult', dbo
GO

if exists (select * from dbo.sysindexes where name = N'IX_ResumeSearchResult_resumeId' and id = object_id(N'[dbo].[ResumeSearchResult]'))
drop index [dbo].[ResumeSearchResult].[IX_ResumeSearchResult_resumeId]
GO

-- The results table used to store ALL results until about November 2006. We now store only the top 10 results
-- and don't use the rest, which take up about 80% of the space - delete them.

DELETE dbo.ResumeSearchResult
WITH (HOLDLOCK)
WHERE rank > 10

GO

-- Change the resumeId column from VARCHAR to UNIQUIDENTIFIER.

EXEC sp_rename 'dbo.ResumeSearchResult.resumeId', '_resumeId', 'COLUMN'
GO

ALTER TABLE dbo.ResumeSearchResult
ADD resumeId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.ResumeSearchResult
SET resumeId = dbo.GuidFromString(_resumeId)

GO

-- Also delete "orphaned" search results where the Resume doesn't exist. There aren't many of these and it
-- shouldn't really affect anyone (since we can't track who the Resume belonged to anyway).

DELETE dbo.ResumeSearchResult
WHERE NOT EXISTS
(
	SELECT *
	FROM dbo.Resume
	WHERE [id] = resumeId
)

GO

ALTER TABLE dbo.ResumeSearchResult
ALTER COLUMN resumeId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.ResumeSearchResult
DROP COLUMN _resumeId

GO

CREATE INDEX IX_ResumeSearchResult_resumeId
ON dbo.ResumeSearchResult(resumeId)

-- Add a foreign key to Resume.

ALTER TABLE dbo.ResumeSearchResult
ADD CONSTRAINT FK_ResumeSearchResult_Resume
FOREIGN KEY (resumeId) REFERENCES dbo.Resume([id])

GO

DBCC CLEANTABLE('', 'dbo.ResumeSearchResult')
DBCC UPDATEUSAGE('', 'dbo.ResumeSearchResult')

GO
