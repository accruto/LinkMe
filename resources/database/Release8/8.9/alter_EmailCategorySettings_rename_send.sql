IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('EmailCategorySettings') AND NAME = 'suppress')
	ALTER TABLE dbo.EmailCategorySettings
	ADD suppress BIT NULL
GO

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('EmailCategorySettings') AND NAME = 'send')
	ALTER TABLE dbo.EmailCategorySettings
	DROP COLUMN send
GO
