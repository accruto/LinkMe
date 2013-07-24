IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[UserContentRemovalRequest]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [UserContentRemovalRequest]
GO

CREATE TABLE [UserContentRemovalRequest] ( 
	[id] uniqueidentifier NOT NULL,
	[reportedFromUrl] varchar(200) NOT NULL,
	[userContentItemId] uniqueidentifier NOT NULL,
	[reporterId] uniqueidentifier NOT NULL
)
GO

ALTER TABLE [UserContentRemovalRequest] ADD CONSTRAINT [PK_UserContentRemovalRequest] 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE [UserContentRemovalRequest] ADD CONSTRAINT [FK_UserContentRemovalRequest_RegisteredUser] 
	FOREIGN KEY ([reporterId]) REFERENCES [RegisteredUser] ([id])
GO

ALTER TABLE [UserContentRemovalRequest] ADD CONSTRAINT [FK_UserContentRemovalRequest_UserContentItem] 
	FOREIGN KEY ([userContentItemId]) REFERENCES [UserContentItem] ([id])
GO

ALTER TABLE [UserContentRemovalRequest] ADD CONSTRAINT [FK_UserContentRemovalRequest_UserToUserRequest] 
	FOREIGN KEY ([id]) REFERENCES [UserToUserRequest] ([id])
GO





