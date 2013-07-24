IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = 'dbo' AND TABLE_NAME = 'CandidateList' AND CONSTRAINT_NAME = 'CK_CandidateList_name')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [CK_CandidateList_name]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = 'dbo' AND TABLE_NAME = 'CandidateList' AND CONSTRAINT_NAME = 'CK_CandidateList_duplicatenames')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [CK_CandidateList_duplicatenames]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[HaveDuplicateCandidateListNames]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[HaveDuplicateCandidateListNames]
GO

-- Add constraints

-- Only shortlist, favourites, flagged and blocklist lists can have null names

ALTER TABLE dbo.CandidateList
ADD CONSTRAINT CK_CandidateList_name
CHECK ((listType = 1) OR (listType = 7) OR (listType = 2 AND name IS NULL) OR (listType = 4 AND name IS NULL) OR (listType = 5 AND name IS NULL) OR (name IS NOT NULL))
GO

CREATE FUNCTION dbo.HaveDuplicateCandidateListNames()
RETURNS bit
AS
BEGIN
	-- The CandidateList name must be unique "within its scope", which is either the ownerId for private
	-- lists (ie. you cannot have two private lists with the same name) or sharedWithId for shared lists
	-- (ie. a company cannot have two shared lists with the same name). Deleted lists are ignored.
	-- Job ad lists now have the job ad ID as the name, so they should always be unique.
	-- This function returns 1 if such duplicate names exist, otherwise 0.

	DECLARE @retval BIT

	IF EXISTS
	(
		SELECT 'Duplicates names!'
		FROM dbo.CandidateList cl
		WHERE isDeleted = 0
		AND name IS NOT NULL
		GROUP BY [name], COALESCE(sharedWithId, ownerId)
		HAVING COUNT(*) > 1
	)
		SET @retval = 1;
	ELSE
		SET @retval = 0;

	RETURN @retval;
END
GO

ALTER TABLE dbo.CandidateList WITH NOCHECK
ADD CONSTRAINT CK_CandidateList_duplicatenames
CHECK (dbo.HaveDuplicateCandidateListNames() = 0)
GO

