IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupAffiliation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupAffiliation;

CREATE TABLE GroupAffiliation ( 
	id uniqueidentifier NOT NULL,
	groupId uniqueidentifier NOT NULL,
	affiliationId uniqueidentifier NOT NULL
);

ALTER TABLE GroupAffiliation ADD CONSTRAINT PK_GroupAffiliation 
	PRIMARY KEY (id);

ALTER TABLE GroupAffiliation ADD CONSTRAINT FK_GroupAffiliation_Group 
	FOREIGN KEY (groupId) REFERENCES [Group] (id);




