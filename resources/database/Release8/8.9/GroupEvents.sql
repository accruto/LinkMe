IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEvents') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEvents;

CREATE TABLE GroupEvents ( 
	eventId uniqueidentifier NOT NULL,
	groupId uniqueidentifier NOT NULL
);

ALTER TABLE GroupEvents ADD CONSTRAINT FK_GroupEvents_GroupEvent 
	FOREIGN KEY (eventId) REFERENCES GroupEvent (id);

ALTER TABLE GroupEvents ADD CONSTRAINT FK_GroupEvents_Group 
	FOREIGN KEY (groupId) REFERENCES [Group] (id);



