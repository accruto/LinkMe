IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.DiscussionBoardModerator') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionBoardModerator;

CREATE TABLE [dbo].DiscussionBoardModerator ( 
	boardId uniqueidentifier NOT NULL,
	contributorId uniqueidentifier NOT NULL
);

ALTER TABLE [dbo].DiscussionBoardModerator ADD CONSTRAINT PK_DiscussionBoardModerator
	PRIMARY KEY CLUSTERED (boardId, contributorId);

ALTER TABLE [dbo].DiscussionBoardModerator ADD CONSTRAINT FK_DiscussionBoardModerator_Contributor 
	FOREIGN KEY (contributorId) REFERENCES Contributor (id);

ALTER TABLE [dbo].DiscussionBoardModerator ADD CONSTRAINT FK_DiscussionBoardModerator_DiscussionBoard 
	FOREIGN KEY (boardId) REFERENCES [dbo].DiscussionBoard (id);
