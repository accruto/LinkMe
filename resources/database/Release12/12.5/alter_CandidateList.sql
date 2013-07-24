IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = 'dbo' AND TABLE_NAME = 'CandidateList' AND CONSTRAINT_NAME = 'CK_CandidateList_name')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [CK_CandidateList_name]
GO

-- Add constraints

-- Only shortlist, favourites, flagged and blocklist lists can have null names

ALTER TABLE dbo.CandidateList
ADD CONSTRAINT CK_CandidateList_name
CHECK ((listType = 1) OR (listType = 7) OR (listType = 2 AND name IS NULL) OR (listType = 4 AND name IS NULL) OR (listType = 5 AND name IS NULL) OR (listType = 6 AND name IS NULL) OR (name IS NOT NULL))
GO

