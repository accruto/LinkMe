IF EXISTS (SELECT * FROM SYSOBJECTS WHERE id = object_id('[EmailDefinitionSettings]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmailDefinitionSettings]
GO

CREATE TABLE [EmailDefinitionSettings] ( 
	[id] uniqueidentifier NOT NULL,
	[definitionId] uniqueidentifier NOT NULL,
	[settingsId] uniqueidentifier NOT NULL,
	[lastSentTime] datetime NULL
)
GO

ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [PK_EmailDefinitionSettings] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [UQ_EmailDefinitionSettings_SettingsId]
	UNIQUE ([settingsId])
GO

ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [FK_EmailDefinitionSettings_EmailDefinition]
	FOREIGN KEY (definitionId) REFERENCES EmailDefinition (id)
GO

ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [FK_EmailDefinitionSettings_EmailSettings]
	FOREIGN KEY (settingsId) REFERENCES EmailSettings (id)
GO

