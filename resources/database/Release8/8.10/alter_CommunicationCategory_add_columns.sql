
IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunicationCategory') AND NAME = 'type')
BEGIN
	ALTER TABLE dbo.CommunicationCategory
	ADD type CommunicationCategoryType NOT NULL DEFAULT 0	-- Default is 'regular'
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunicationCategory') AND NAME = 'defaultFrequency')
BEGIN
	ALTER TABLE dbo.CommunicationCategory
	ADD defaultFrequency CommunicationFrequency NOT NULL DEFAULT 0	-- Default is 'weekly'
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunicationCategory') AND NAME = 'deleted')
BEGIN
	ALTER TABLE dbo.CommunicationCategory
	ADD deleted BIT NOT NULL DEFAULT 0	-- Default is 'not deleted'
END
GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunicationCategory') AND NAME = 'roles')
BEGIN
	ALTER TABLE dbo.CommunicationCategory
	ADD roles INT NOT NULL DEFAULT 0	-- Default is 'anonymous'
END
GO
