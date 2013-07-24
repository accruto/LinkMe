IF EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_Group_DiscussionBoard') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
	ALTER TABLE [Group] DROP CONSTRAINT FK_Group_DiscussionBoard
GO

IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'Group' and COLUMN_NAME = 'boardId' )
	ALTER TABLE [Group] DROP COLUMN boardId
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('DiscussionPost') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
	DROP TABLE [dbo].DiscussionPost

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('DiscussionSubscriber') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionSubscriber

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('DiscussionBoardSubscriber') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionBoardSubscriber

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('DiscussionBoardModerator') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionBoardModerator

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Discussion') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].Discussion

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('DiscussionBoard') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionBoard


