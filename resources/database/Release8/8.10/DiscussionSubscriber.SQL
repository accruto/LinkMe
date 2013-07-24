IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.DiscussionSubscriber') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionSubscriber;

CREATE TABLE [dbo].DiscussionSubscriber ( 
	discussionId uniqueidentifier NOT NULL,
	contributorId uniqueidentifier NOT NULL
);

ALTER TABLE [dbo].DiscussionSubscriber ADD CONSTRAINT PK_DiscussionSubscriber
	PRIMARY KEY CLUSTERED (discussionId, contributorId);

ALTER TABLE [dbo].DiscussionSubscriber ADD CONSTRAINT FK_DiscussionSubscriber_Discussion 
	FOREIGN KEY (discussionId) REFERENCES [dbo].Discussion (id);

ALTER TABLE [dbo].DiscussionSubscriber ADD CONSTRAINT FK_DiscussionSubscriber_Contributor 
	FOREIGN KEY (contributorId) REFERENCES Contributor (id);

