-- Drop all current constraints.

-- EmailCategorySettings

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailCategorySettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailCategorySettings] DROP CONSTRAINT [PK_EmailCategorySettings] 

	ALTER TABLE [EmailCategorySettings] DROP CONSTRAINT [FK_EmailCategorySettings_EmailCategory]
	
	ALTER TABLE [EmailCategorySettings] DROP CONSTRAINT [FK_EmailCategorySettings_EmailSettings]
END

-- EmailDefinitionSettings

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailDefinitionSettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailDefinitionSettings] DROP CONSTRAINT [PK_EmailDefinitionSettings] 

	IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EmailDefinitionSettings]') AND name = N'UQ_EmailDefinitionSettings_SettingsId')
	ALTER TABLE [EmailDefinitionSettings] DROP CONSTRAINT [UQ_EmailDefinitionSettings_SettingsId]

	ALTER TABLE [EmailDefinitionSettings] DROP CONSTRAINT [FK_EmailDefinitionSettings_EmailDefinition]

	ALTER TABLE [EmailDefinitionSettings] DROP CONSTRAINT [FK_EmailDefinitionSettings_EmailSettings]
END

-- EmailDefinition

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailDefinition') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailDefinition] DROP CONSTRAINT [PK_EmailDefinition]

	ALTER TABLE [EmailDefinition] DROP CONSTRAINT [UQ_EmailDefinition_Name] 

	ALTER TABLE [EmailDefinition] DROP CONSTRAINT [FK_EmailDefinition_EmailCategory]
END

-- EmailCategory

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailCategory') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailCategory] DROP CONSTRAINT [PK_EmailCategory] 

	ALTER TABLE [EmailCategory] DROP CONSTRAINT [UQ_EmailCategory_Name] 
END

-- EmailSettings

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailSettings] DROP CONSTRAINT [PK_EmailSettings] 

	IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EmailSettings]') AND name = N'UQ_EmailSettings_UserId')
	ALTER TABLE [EmailSettings] DROP CONSTRAINT [UQ_EmailSettings_UserId]

	ALTER TABLE [EmailSettings] DROP CONSTRAINT [FK_EmailSettings_RegisteredUser]
END

-- Add constraints back.

-- EmailCategory

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailCategory') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailCategory] ADD CONSTRAINT [PK_CommunicationCategory] 
		PRIMARY KEY CLUSTERED ([id])

	ALTER TABLE [EmailCategory] ADD CONSTRAINT [UQ_CommunicationCategory_Name] 
		UNIQUE ([name])
END

-- EmailDefinition

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailDefinition') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailDefinition] ADD CONSTRAINT [PK_CommunicationDefinition]
		PRIMARY KEY CLUSTERED ([id])

	ALTER TABLE [EmailDefinition] ADD CONSTRAINT [UQ_CommunicationDefinition_Name] 
		UNIQUE ([name])

	ALTER TABLE [EmailDefinition] ADD CONSTRAINT [FK_CommunicationDefinition_CommunicationCategory]
		FOREIGN KEY (categoryId) REFERENCES EmailCategory (id)
END

-- EmailSettings

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailSettings] ADD CONSTRAINT [PK_CommunicationSettings] 
		PRIMARY KEY CLUSTERED ([id])

	ALTER TABLE [EmailSettings] ADD CONSTRAINT [UQ_CommunicationSettings_UserId]
		UNIQUE ([userId])

	ALTER TABLE [EmailSettings] ADD CONSTRAINT [FK_CommunicationSettings_RegisteredUser]
		FOREIGN KEY (userId) REFERENCES RegisteredUser (id)
END

-- EmailCategorySettings

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailCategorySettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailCategorySettings] ADD CONSTRAINT [PK_CommunicationCategorySettings] 
		PRIMARY KEY CLUSTERED ([id])

	ALTER TABLE [EmailCategorySettings] ADD CONSTRAINT [FK_CommunicationCategorySettings_CommunicationCategory]
		FOREIGN KEY (categoryId) REFERENCES EmailCategory (id)

	ALTER TABLE [EmailCategorySettings] ADD CONSTRAINT [FK_CommunicationCategorySettings_CommunicationSettings]
		FOREIGN KEY (settingsId) REFERENCES EmailSettings (id)
END

-- EmailDefinitionSettings

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailDefinitionSettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN
	ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [PK_CommunicationDefinitionSettings] 
		PRIMARY KEY CLUSTERED ([id])

	ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [UQ_CommunicationDefinitionSettings_SettingsId_DefinitionId]
		UNIQUE ([settingsId], [definitionId])

	ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [FK_CommunicationDefinitionSettings_CommunicationDefinition]
		FOREIGN KEY (definitionId) REFERENCES EmailDefinition (id)

	ALTER TABLE [EmailDefinitionSettings] ADD CONSTRAINT [FK_CommunicationDefinitionSettings_CommunicationSettings]
		FOREIGN KEY (settingsId) REFERENCES EmailSettings (id)
END

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailSettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
	EXEC sp_rename 'dbo.EmailSettings', 'CommunicationSettings'
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailDefinition') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
	EXEC sp_rename 'dbo.EmailDefinition', 'CommunicationDefinition'
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailCategory') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
	EXEC sp_rename 'dbo.EmailCategory', 'CommunicationCategory'
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailCategorySettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
	EXEC sp_rename 'dbo.EmailCategorySettings', 'CommunicationCategorySettings'
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailDefinitionSettings') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
	EXEC sp_rename 'dbo.EmailDefinitionSettings', 'CommunicationDefinitionSettings'
GO
