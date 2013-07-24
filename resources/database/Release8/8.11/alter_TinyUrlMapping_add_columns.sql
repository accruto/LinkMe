
IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('TinyUrlMapping') AND NAME = 'userId')
BEGIN
	ALTER TABLE dbo.TinyUrlMapping
	ADD userId UNIQUEIDENTIFIER NULL
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('TinyUrlMapping') AND NAME = 'createdTime')
BEGIN
	ALTER TABLE dbo.TinyUrlMapping
	ADD createdTime DATETIME NOT NULL DEFAULT GETDATE()
END
GO
