IF EXISTS (SELECT * FROM SYSOBJECTS WHERE id = object_id('[EmailSettings]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmailSettings]
GO

CREATE TABLE [EmailSettings] ( 
	[id] uniqueidentifier NOT NULL,
	[userId] uniqueidentifier NOT NULL,
	[sendPlainTextOnly] bit NOT NULL,
)
GO

ALTER TABLE [EmailSettings] ADD CONSTRAINT [PK_EmailSettings] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [EmailSettings] ADD CONSTRAINT [UQ_EmailSettings_UserId]
	UNIQUE ([userId])
GO

ALTER TABLE [EmailSettings] ADD CONSTRAINT [FK_EmailSettings_RegisteredUser]
	FOREIGN KEY (userId) REFERENCES RegisteredUser (id)
GO


