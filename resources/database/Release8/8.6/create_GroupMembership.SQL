IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupMembership') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupMembership;

CREATE TABLE GroupMembership ( 
	id uniqueidentifier NOT NULL,
	flags GroupMembershipFlags NOT NULL,
	groupId uniqueidentifier NOT NULL,
	contributorId uniqueidentifier NOT NULL
);

ALTER TABLE GroupMembership
	ADD CONSTRAINT UQ_GroupMembership UNIQUE (groupId, contributorId);

ALTER TABLE GroupMembership ADD CONSTRAINT PK_GroupMembership 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE GroupMembership ADD CONSTRAINT FK_GroupMembership_Contributor 
	FOREIGN KEY (contributorId) REFERENCES Contributor (id);

ALTER TABLE GroupMembership ADD CONSTRAINT FK_GroupMembership_Group 
	FOREIGN KEY (groupId) REFERENCES [Group] (id);





