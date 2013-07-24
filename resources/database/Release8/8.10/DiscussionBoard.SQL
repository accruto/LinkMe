IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.DiscussionBoard') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].DiscussionBoard;

CREATE TABLE [dbo].DiscussionBoard ( 
	id uniqueidentifier NOT NULL,
	deleted bit NOT NULL
);

ALTER TABLE [dbo].DiscussionBoard ADD CONSTRAINT PK_DiscussionBoard 
	PRIMARY KEY CLUSTERED (id);



