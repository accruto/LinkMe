EXEC sp_changeobjectowner 'linkme_owner.SavedJobSearchAlert', dbo
GO

ALTER TABLE dbo.SavedJobSearchAlert
ALTER COLUMN emailAddress EmailAddress NOT NULL
GO
