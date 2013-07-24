IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEventAttendee') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEventAttendee;

CREATE TABLE GroupEventAttendee ( 
	id uniqueidentifier NOT NULL,
	eventId uniqueidentifier NOT NULL,
	attendeeContributorId uniqueidentifier NOT NULL,
	status GroupEventAttendanceStatus NOT NULL
);

ALTER TABLE GroupEventAttendee ADD CONSTRAINT PK_GroupEventAttendee 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE GroupEventAttendee ADD CONSTRAINT FK_GroupEventAttendee_Contributor 
	FOREIGN KEY (attendeeContributorId) REFERENCES Contributor (id);

ALTER TABLE GroupEventAttendee ADD CONSTRAINT FK_GroupEventAttendee_GroupEvent 
	FOREIGN KEY (eventId) REFERENCES GroupEvent (id);





