ALTER TABLE dbo.SavedResumeSearch
ALTER COLUMN name NVARCHAR(200) NOT NULL
GO

ALTER TABLE dbo.SavedResumeSearch
ADD createdTime DATETIME NULL
GO

-- Not ideal but for existing data set it to the current time.

UPDATE
	dbo.SavedResumeSearch
SET
	createdTime = GETDATE()
WHERE
	createdTime IS NULL
GO

-- Make the column not null

ALTER TABLE dbo.SavedResumeSearch
ALTER COLUMN createdTime DATETIME NOT NULL
GO

