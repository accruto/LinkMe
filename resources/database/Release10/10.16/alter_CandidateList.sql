IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = 'dbo' AND TABLE_NAME = 'CandidateList' AND CONSTRAINT_NAME = 'CK_CandidateList_name')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [CK_CandidateList_name]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = 'dbo' AND TABLE_NAME = 'CandidateList' AND CONSTRAINT_NAME = 'CK_CandidateList_duplicatenames')
ALTER TABLE [dbo].[CandidateList] DROP CONSTRAINT [CK_CandidateList_duplicatenames]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[HaveDuplicateCandidateListNames]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[HaveDuplicateCandidateListNames]
GO

ALTER TABLE dbo.CandidateList
ADD listType INT NULL
GO

-- Set all to 0 to signify normal.

UPDATE
	dbo.CandidateList
SET
	listType = 0

-- Make not null.

ALTER TABLE dbo.CandidateList
ALTER COLUMN listType INT NOT NULL
GO

-- Mark all private folders as 1

UPDATE
	dbo.CandidateList
SET
	listType = 1
WHERE
	name IS NULL
	AND sharedWithId IS NULL
GO

-- Mark all shared folders as 3

UPDATE
	dbo.CandidateList
SET
	listType = 3
WHERE
	sharedWithId IS NOT NULL
GO

-- Fill in NULL createdTime

-- Private folders.

UPDATE
	dbo.CandidateList
SET
	createdTime = (SELECT u.createdTime FROM dbo.RegisteredUser AS u WHERE u.id = ownerId)
WHERE
	createdTime IS NULL
	AND name IS NULL
GO

-- Job ad folders.

UPDATE
	dbo.CandidateList
SET
	listType = 6,
	createdTime = (SELECT j.createdTime FROM dbo.JobAd AS j WHERE j.id = name)
WHERE
	createdTime IS NULL
	AND name IS NOT NULL
GO

-- Make the column not null

ALTER TABLE dbo.CandidateList
ALTER COLUMN createdTime DATETIME NOT NULL
GO

-- Add constraints

-- Only shortlist, flagged and blocklist lists can have null names

ALTER TABLE dbo.CandidateList
ADD CONSTRAINT CK_CandidateList_name
CHECK ((listType = 1) OR (listType = 2 AND name IS NULL) OR (listType = 4 AND name IS NULL) OR (listType = 5 AND name IS NULL) OR (name IS NOT NULL))
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

-- It is possible some job ad applicant lists are not updated properly.
-- Look for GUIDs in names explicitly.

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[isuniqueidentifier]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[IsUniqueIdentifier]
GO

CREATE FUNCTION dbo.IsUniqueIdentifier (@unique NVARCHAR(100))  
RETURNS BIT AS
BEGIN

DECLARE @return BIT
SET @return = 0

IF RIGHT(LEFT(@unique,9),1) + RIGHT(LEFT(@unique,14),1) + RIGHT(LEFT(@unique,19),1) + RIGHT(LEFT(@unique,24),1) = '----'
IF LEN(@unique) = 36
SET @return = 1

RETURN @return

END
GO

UPDATE
	dbo.CandidateList
SET
	listType = 6
WHERE
	listType = 0 AND dbo.IsUniqueIdentifier(name) = 1

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[isuniqueidentifier]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[IsUniqueIdentifier]
GO
