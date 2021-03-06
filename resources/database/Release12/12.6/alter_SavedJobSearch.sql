-- Drop indexes.

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SavedJobSearch]') AND name = N'IX_SavedJobSearch_criteriaSetId')
DROP INDEX [IX_SavedJobSearch_criteriaSetId] ON [dbo].[SavedJobSearch]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SavedJobSearch]') AND name = N'PK_SavedJobSearch')
ALTER TABLE [dbo].[SavedJobSearch] DROP CONSTRAINT [PK_SavedJobSearch]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SavedJobSearch]') AND name = N'IX_SavedJobSearch_ownerId_displayText')
DROP INDEX [IX_SavedJobSearch_ownerId_displayText] ON [dbo].[SavedJobSearch]
GO

-- Reduce and de-duplicate displayText.

UPDATE
	dbo.SavedJobSearch
SET
	displayText = NULL
WHERE
	LEN(displayText) > 200
GO

DECLARE @temp TABLE (ownerId UNIQUEIDENTIFIER, displayText NVARCHAR(2000), count INT)

INSERT
	@temp (ownerId, displayText, COUNT)
SELECT
	ownerId, displayText, COUNT(*)
FROM
	dbo.SavedJobSearch
WHERE
	NOT displayText IS NULL
GROUP BY
	ownerId, displayText
HAVING
	COUNT(*) > 1

DECLARE @temp2 TABLE (id UNIQUEIDENTIFIER)

INSERT
	@temp2
SELECT
	id
FROM
	dbo.SavedJobSearch AS s
INNER JOIN
	@temp AS t ON t.ownerId = s.ownerId AND t.displayText = s.displayText

UPDATE
	dbo.SavedJobSearch
SET
	displayText = NULL
WHERE
	id IN (SELECT id FROM @temp2)

ALTER TABLE dbo.SavedJobSearch
ALTER COLUMN displayText NVARCHAR(200) NULL
GO

-- Add the primary key and indexes

ALTER TABLE [dbo].[SavedJobSearch] ADD  CONSTRAINT [PK_SavedJobSearch] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)
GO

CREATE CLUSTERED INDEX [IX_SavedJobSearch_ownerId_displayText] ON [dbo].[SavedJobSearch] 
(
	[ownerId] ASC,
	[displayText] ASC
)
GO

-- Add check for duplicates.

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_SavedJobSearch_duplicatenames]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavedJobSearch]'))
ALTER TABLE [dbo].[SavedJobSearch] DROP CONSTRAINT [CK_SavedJobSearch_duplicatenames]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HaveDuplicateJobAdSearchNames]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[HaveDuplicateJobAdSearchNames]
GO

CREATE FUNCTION [dbo].[HaveDuplicateJobAdSearchNames]()
RETURNS bit
AS
BEGIN
	DECLARE @retval BIT

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.SavedJobSearch
		WHERE
			displayText IS NOT NULL
		GROUP BY
			ownerId, displayText
		HAVING
			COUNT(*) > 1
	)
		SET @retval = 1;
	ELSE
		SET @retval = 0;

	RETURN @retval;
END
GO

ALTER TABLE [dbo].[SavedJobSearch] ADD CONSTRAINT [CK_SavedJobSearch_duplicatenames] CHECK  (([dbo].[HaveDuplicateJobAdSearchNames]()=(0)))
