ALTER TABLE dbo.SavedResumeSearchAlert
DROP CONSTRAINT FK_SavedResumeSearchAlert_ResumeSearchResultSet
GO

ALTER TABLE dbo.SavedResumeSearchAlert
DROP COLUMN lastResultSetId
GO
