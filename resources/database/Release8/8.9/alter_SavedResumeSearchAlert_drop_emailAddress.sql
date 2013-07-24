
IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('SavedResumeSearchAlert') AND NAME = 'emailAddress')
	ALTER TABLE dbo.SavedResumeSearchAlert
	DROP COLUMN emailAddress

GO
