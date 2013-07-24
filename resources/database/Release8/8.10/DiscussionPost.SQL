IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.DiscussionPost') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionPost;

CREATE TABLE [dbo].DiscussionPost ( 
	id uniqueidentifier NOT NULL,
	discussionId uniqueidentifier NOT NULL,
	text nvarchar(1000) NOT NULL
);

ALTER TABLE [dbo].DiscussionPost ADD CONSTRAINT PK_DiscussionPost 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE [dbo].DiscussionPost ADD CONSTRAINT FK_DiscussionPost_Discussion 
	FOREIGN KEY (discussionId) REFERENCES [dbo].Discussion (id);

ALTER TABLE [dbo].DiscussionPost ADD CONSTRAINT FK_DiscussionPost_UserContentItem 
	FOREIGN KEY (id) REFERENCES UserContentItem (id);




