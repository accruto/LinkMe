IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.DiscussionBoardSubscriber') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionBoardSubscriber;

CREATE TABLE [dbo].DiscussionBoardSubscriber ( 
	boardId uniqueidentifier NOT NULL,
	contributorId uniqueidentifier NOT NULL
);

ALTER TABLE [dbo].DiscussionBoardSubscriber ADD CONSTRAINT PK_DiscussionBoardSubscriber
	PRIMARY KEY CLUSTERED (boardId, contributorId);

ALTER TABLE [dbo].DiscussionBoardSubscriber ADD CONSTRAINT FK_DiscussionBoardSubscriber_Contributor 
	FOREIGN KEY (contributorId) REFERENCES Contributor (id);

ALTER TABLE [dbo].DiscussionBoardSubscriber ADD CONSTRAINT FK_DiscussionBoardSubscriber_DiscussionBoard 
	FOREIGN KEY (boardId) REFERENCES [dbo].DiscussionBoard (id);
