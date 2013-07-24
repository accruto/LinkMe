ALTER TABLE [dbo].[SavedJobSearch]
ALTER COLUMN [displayText] [nvarchar](2000) NULL
GO

UPDATE
	dbo.SavedJobSearch
SET
	displayText = NULL
WHERE
	displayText = ''