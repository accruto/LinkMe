IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupAllowedEmailDomain') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupAllowedEmailDomain;

CREATE TABLE GroupAllowedEmailDomain ( 
	id uniqueidentifier NOT NULL,
	groupId uniqueidentifier NOT NULL,
	emailDomain EmailAddress NOT NULL
);

ALTER TABLE GroupAllowedEmailDomain ADD CONSTRAINT PK_GroupAllowedEmailDomain 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE GroupAllowedEmailDomain ADD CONSTRAINT FK_GroupAllowedEmailDomain_Group 
	FOREIGN KEY (groupId) REFERENCES [Group] (id);




