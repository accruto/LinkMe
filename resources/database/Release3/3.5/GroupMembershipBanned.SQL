IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupMembershipBanned') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupMembershipBanned;

CREATE TABLE GroupMembershipBanned ( 
	id uniqueidentifier NOT NULL,
	time datetime NOT NULL,
	reason nvarchar(300) NULL,
	bannedContributorId uniqueidentifier NOT NULL,
	bannedByContributorId uniqueidentifier NOT NULL,
	groupId uniqueidentifier NOT NULL
);

ALTER TABLE GroupMembershipBanned
	ADD CONSTRAINT UQ_BannedContributor UNIQUE (groupId, bannedContributorId);

ALTER TABLE GroupMembershipBanned ADD CONSTRAINT PK_GroupMembershipBanned 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE GroupMembershipBanned ADD CONSTRAINT FK_GroupMembershipBanned_ByContributor 
	FOREIGN KEY (bannedByContributorId) REFERENCES Contributor (id);

ALTER TABLE GroupMembershipBanned ADD CONSTRAINT FK_GroupMembershipBanned_Group 
	FOREIGN KEY (groupId) REFERENCES [Group] (id);

ALTER TABLE GroupMembershipBanned ADD CONSTRAINT FK_GroupMembershipBanned_Contributor 
	FOREIGN KEY (bannedContributorId) REFERENCES Contributor (id);







