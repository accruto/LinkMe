IF EXISTS (SELECT * FROM SYSOBJECTS WHERE id = object_id('[EmailCategory]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmailCategory]
GO

CREATE TABLE [EmailCategory] ( 
	[id] uniqueidentifier NOT NULL,
	[name] nvarchar(100) NOT NULL,
)
GO

ALTER TABLE [EmailCategory] ADD CONSTRAINT [PK_EmailCategory] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [EmailCategory] ADD CONSTRAINT [UQ_EmailCategory_Name] 
	UNIQUE ([name])
GO

