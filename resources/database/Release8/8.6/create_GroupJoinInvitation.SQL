IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupJoinInvitation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupJoinInvitation;

CREATE TABLE GroupJoinInvitation ( 
	id uniqueidentifier NOT NULL,
	groupId uniqueidentifier NOT NULL,
	inviterContributorId uniqueidentifier NOT NULL,
	inviteeContributorId uniqueidentifier NULL,
	inviteeEmail EmailAddress NULL
);

ALTER TABLE GroupJoinInvitation ADD CONSTRAINT PK_GroupJoinInvitation 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE GroupJoinInvitation ADD CONSTRAINT FK_GroupJoinInvitation_Group 
	FOREIGN KEY (groupId) REFERENCES [Group] (id);

ALTER TABLE GroupJoinInvitation ADD CONSTRAINT FK_GroupJoinInvitation_Invitee 
	FOREIGN KEY (inviteeContributorId) REFERENCES Contributor (id);

ALTER TABLE GroupJoinInvitation ADD CONSTRAINT FK_GroupJoinInvitation_UserToUserRequest 
	FOREIGN KEY (id) REFERENCES UserToUserRequest (id);






