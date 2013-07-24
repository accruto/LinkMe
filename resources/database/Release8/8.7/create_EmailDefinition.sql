IF EXISTS (SELECT * FROM SYSOBJECTS WHERE id = object_id('[EmailDefinition]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmailDefinition]
GO

CREATE TABLE [EmailDefinition] ( 
	[id] uniqueidentifier NOT NULL,
	[name] nvarchar(100) NOT NULL,
	[categoryId] uniqueidentifier NOT NULL,
)
GO

ALTER TABLE [EmailDefinition] ADD CONSTRAINT [PK_EmailDefinition] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [EmailDefinition] ADD CONSTRAINT [UQ_EmailDefinition_Name] 
	UNIQUE ([name])
GO

ALTER TABLE [EmailDefinition] ADD CONSTRAINT [FK_EmailDefinition_EmailCategory]
	FOREIGN KEY (categoryId) REFERENCES EmailCategory (id)
GO

