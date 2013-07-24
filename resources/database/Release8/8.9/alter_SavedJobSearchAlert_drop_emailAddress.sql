
IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('SavedJobSearchAlert') AND NAME = 'emailAddress')
	ALTER TABLE dbo.SavedJobSearchAlert
	DROP COLUMN emailAddress

GO
