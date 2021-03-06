ALTER TABLE dbo.Resume
ADD createdTime DATETIME NULL
GO

UPDATE
	dbo.Resume
SET
	createdTime = lastEditedTime

-- All other null values just set to now.

UPDATE
	dbo.Resume
SET
	createdTime = GETDATE()
WHERE
	createdTime IS NULL

UPDATE
	dbo.Resume
SET
	lastEditedTime = GETDATE()
WHERE
	lastEditedTime IS NULL

ALTER TABLE dbo.Resume
ALTER COLUMN createdTime DATETIME NOT NULL
GO

ALTER TABLE dbo.Resume
ALTER COLUMN lastEditedTime DATETIME NOT NULL
GO