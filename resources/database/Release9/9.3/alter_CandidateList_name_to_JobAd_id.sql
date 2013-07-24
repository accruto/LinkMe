IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_SCHEMA = 'dbo' AND TABLE_NAME = 'CandidateList'
	AND CONSTRAINT_NAME = 'CK_CandidateList_name')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [CK_CandidateList_name]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[HaveDuplicateCandidateListNames]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[HaveDuplicateCandidateListNames]
GO

-- Update the existing CandidateList names

UPDATE dbo.CandidateList
SET [name] = ja.id
FROM dbo.CandidateList cl
INNER JOIN dbo.JobAd ja
ON cl.id = ja.candidateListId
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
		GROUP BY [name], COALESCE(sharedWithId, ownerId)
		HAVING COUNT(*) > 1
	)
		SET @retval = 1;
	ELSE
		SET @retval = 0;

	RETURN @retval;
END
GO

-- Manually check the constraint ONCE rather than for every row.

if (dbo.HaveDuplicateCandidateListNames() = 1)
	RAISERROR('The CandidateList table contains name conflicts.', 16, 1)

-- All OK - add the constraint.

ALTER TABLE dbo.CandidateList
WITH NOCHECK
ADD CONSTRAINT CK_CandidateList_name
CHECK (dbo.HaveDuplicateCandidateListNames() = 0)

GO
