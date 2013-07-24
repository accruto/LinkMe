IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[UserContentItem]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [UserContentItem]
GO

CREATE TABLE [UserContentItem] ( 
	[id] uniqueidentifier NOT NULL,
	[createdTime] datetime NOT NULL,
	[blocked] bit NULL,
	[blockerId] uniqueidentifier NULL,
	[posterId] uniqueidentifier NULL,
	[blockedTime] datetime NULL
)
GO

ALTER TABLE [UserContentItem] ADD CONSTRAINT [PK_UserContentItem] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [UserContentItem] ADD CONSTRAINT [FK_UserContent_Administrator_blocker] 
	FOREIGN KEY ([blockerId]) REFERENCES [Administrator] ([id])
GO

ALTER TABLE [UserContentItem] ADD CONSTRAINT [FK_UserContentItem_RegisteredUser_poster] 
	FOREIGN KEY ([posterId]) REFERENCES [RegisteredUser] ([id])
GO







