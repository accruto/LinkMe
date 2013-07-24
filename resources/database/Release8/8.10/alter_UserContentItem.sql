if EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_UserContent_Administrator_blocker') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE UserContentItem DROP CONSTRAINT FK_UserContent_Administrator_blocker 

if EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_UserContentItem_RegisteredUser_poster') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE UserContentItem DROP CONSTRAINT FK_UserContentItem_RegisteredUser_poster 

if EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_UserContentRemovalRequest_RegisteredUser') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE UserContentRemovalRequest DROP CONSTRAINT FK_UserContentRemovalRequest_RegisteredUser

IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'UserContentItem' and COLUMN_NAME = 'blocked' )
	EXECUTE sp_rename N'dbo.UserContentItem.blocked', N'deleted', 'COLUMN' 
GO
IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'UserContentItem' and COLUMN_NAME = 'blockerId' )
	EXECUTE sp_rename N'dbo.UserContentItem.blockerId', N'deleterId', 'COLUMN' 
GO

IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'UserContentItem' and COLUMN_NAME = 'blockedTime' )
EXECUTE sp_rename N'dbo.UserContentItem.blockedTime', N'deletedTime', 'COLUMN' 
GO

IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'UserContentItem' and COLUMN_NAME = 'deletionReason' )
ALTER TABLE dbo.UserContentItem ADD
	deletionReason nvarchar(100) NULL
GO

IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'UserContentItem' and COLUMN_NAME = 'updatedTime' )
ALTER TABLE dbo.UserContentItem ADD
	updatedTime datetime NULL
GO

if NOT EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_UserContentItem_Contributor_Poster ') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE UserContentItem ADD CONSTRAINT FK_UserContentItem_Contributor_Poster 
	FOREIGN KEY (posterId) REFERENCES Contributor (id);
GO

if NOT EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_UserContentItem_Contributor_Deleter ') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE UserContentItem ADD CONSTRAINT FK_UserContentItem_Contributor_Deleter
	FOREIGN KEY (deleterId) REFERENCES Contributor (id);
GO
