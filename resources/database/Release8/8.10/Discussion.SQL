IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.Discussion') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [dbo].Discussion;

CREATE TABLE [dbo].Discussion ( 
	id uniqueidentifier NOT NULL,
	boardId uniqueidentifier NOT NULL,
	flags DiscussionFlags NOT NULL,
	views int NOT NULL,
	subject nvarchar(150) NOT NULL
);

ALTER TABLE [dbo].Discussion ADD CONSTRAINT PK_Discussion 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE [dbo].Discussion ADD CONSTRAINT FK_Discussion_DiscussionBoard 
	FOREIGN KEY (boardId) REFERENCES [dbo].DiscussionBoard (id);

ALTER TABLE [dbo].Discussion ADD CONSTRAINT FK_Discussion_UserContentItem 
	FOREIGN KEY (id) REFERENCES UserContentItem (id);






