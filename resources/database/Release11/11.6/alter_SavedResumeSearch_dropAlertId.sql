ALTER TABLE dbo.SavedResumeSearch
	DROP CONSTRAINT FK_SavedResumeSearch_SavedResumeSearchAlert
GO

ALTER TABLE dbo.SavedResumeSearch
	DROP COLUMN alertId
GO

