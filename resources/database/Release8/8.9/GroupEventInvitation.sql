IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEventInvitation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEventInvitation;

CREATE TABLE GroupEventInvitation ( 
	id uniqueidentifier NOT NULL,
	eventId uniqueidentifier NOT NULL,
	messageText nvarchar(2000) NULL,
	inviteeEmailAddress nvarchar(320) NULL,
	inviterContributorId uniqueidentifier NOT NULL,
	inviteeContributorId uniqueidentifier NULL
);

ALTER TABLE GroupEventInvitation ADD CONSTRAINT PK_GroupEventInvitation 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE GroupEventInvitation ADD CONSTRAINT FK_GroupEventInvitation_Contributor 
	FOREIGN KEY (inviteeContributorId) REFERENCES Contributor (id);

ALTER TABLE GroupEventInvitation ADD CONSTRAINT FK_GroupEventInvitation_GroupEvent 
	FOREIGN KEY (eventId) REFERENCES GroupEvent (id);

ALTER TABLE GroupEventInvitation ADD CONSTRAINT FK_GroupEventInvitation_InviterContributor 
	FOREIGN KEY (inviterContributorId) REFERENCES Contributor (id);







