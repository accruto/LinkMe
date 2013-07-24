IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEvent') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEvent;

CREATE TABLE GroupEvent ( 
	id uniqueidentifier NOT NULL,
	name nvarchar(200) NOT NULL,
	startTime datetime NOT NULL,
	endTime datetime NOT NULL,
	description nvarchar(500) NULL,
	websiteUrl nvarchar(1000) NULL,
	flags GroupEventFlags NOT NULL,
	whoCanAttend GroupEventAttendance NOT NULL,
	whiteboardId uniqueidentifier NULL,
	imageFileReferenceId uniqueidentifier NULL,
	addressId uniqueidentifier NULL
);

ALTER TABLE GroupEvent ADD CONSTRAINT PK_GroupEvent 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE GroupEvent ADD CONSTRAINT FK_GroupEvent_Address 
	FOREIGN KEY (addressId) REFERENCES Address (id);

ALTER TABLE GroupEvent ADD CONSTRAINT FK_GroupEvent_FileReference 
	FOREIGN KEY (imageFileReferenceId) REFERENCES FileReference (id);












