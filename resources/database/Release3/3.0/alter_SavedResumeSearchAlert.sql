EXEC sp_changeobjectowner 'linkme_owner.SavedResumeSearchAlert', dbo
GO

ALTER TABLE dbo.SavedResumeSearchAlert
ALTER COLUMN emailAddress EmailAddress NOT NULL
GO
