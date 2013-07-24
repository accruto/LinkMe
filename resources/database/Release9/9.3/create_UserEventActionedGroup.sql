IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.UserEventActionedGroup') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.UserEventActionedGroup
GO

CREATE TABLE dbo.UserEventActionedGroup
( 
	eventId uniqueidentifier NOT NULL,
	groupId uniqueidentifier NOT NULL
)
GO

ALTER TABLE dbo.UserEventActionedGroup
ADD CONSTRAINT PK_UserEventActionedGroup 
PRIMARY KEY NONCLUSTERED (eventId)
GO


ALTER TABLE dbo.UserEventActionedGroup
ADD CONSTRAINT FK_UserEventActionedGroup_UserEvent 
FOREIGN KEY (eventId) REFERENCES UserEvent (id)
GO

ALTER TABLE dbo.UserEventActionedGroup
ADD CONSTRAINT FK_UserEventActionedGroup_Group 
FOREIGN KEY (groupId) REFERENCES [Group] (id)
GO
