IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEventCoordinator') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEventCoordinator;

CREATE TABLE GroupEventCoordinator ( 
	eventId uniqueidentifier NOT NULL,
	contributorId uniqueidentifier NOT NULL
);

ALTER TABLE GroupEventCoordinator ADD CONSTRAINT FK_EventCoordinator_Contributor 
	FOREIGN KEY (contributorId) REFERENCES Contributor (id);

ALTER TABLE GroupEventCoordinator ADD CONSTRAINT FK_EventCoordinator_GroupEvent 
	FOREIGN KEY (eventId) REFERENCES GroupEvent (id);



