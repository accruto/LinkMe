IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'Group' and COLUMN_NAME = 'boardId' )
BEGIN
	ALTER TABLE [Group] ADD boardId uniqueidentifier NULL
END

IF EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_Group_DiscussionBoard') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE [Group] DROP CONSTRAINT FK_Group_DiscussionBoard

ALTER TABLE [Group] ADD CONSTRAINT FK_Group_DiscussionBoard 
	FOREIGN KEY (boardId) REFERENCES DiscussionBoard (id);


















