UPDATE dbo.Candidate
SET lastEditedTime = GETDATE()
WHERE lastEditedTime IS NULL
GO

ALTER TABLE dbo.Candidate
ALTER COLUMN lastEditedTime DATETIME NOT NULL
GO