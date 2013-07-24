IF EXISTS (SELECT * FROM SYSOBJECTS WHERE id = object_id('[EmailCategorySettings]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmailCategorySettings]
GO

CREATE TABLE [EmailCategorySettings] ( 
	[id] uniqueidentifier NOT NULL,
	[categoryId] uniqueidentifier NOT NULL,
	[settingsId] uniqueidentifier NOT NULL,
	[send] bit NOT NULL,
)
GO

ALTER TABLE [EmailCategorySettings] ADD CONSTRAINT [PK_EmailCategorySettings] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [EmailCategorySettings] ADD CONSTRAINT [FK_EmailCategorySettings_EmailCategory]
	FOREIGN KEY (categoryId) REFERENCES EmailCategory (id)
GO

ALTER TABLE [EmailCategorySettings] ADD CONSTRAINT [FK_EmailCategorySettings_EmailSettings]
	FOREIGN KEY (settingsId) REFERENCES EmailSettings (id)
GO

