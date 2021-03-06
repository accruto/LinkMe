--  -------------------------------------------------- 
--  Generated by Enterprise Architect Version 7.0.813
--  Created On : Wednesday, 18 July, 2007 
--  DBMS       : SQL Server 2000 
--  -------------------------------------------------- 

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('MessageThreadParticipant') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE MessageThreadParticipant
GO

CREATE TABLE MessageThreadParticipant ( 
	userId uniqueidentifier NOT NULL,
	threadId uniqueidentifier NOT NULL,
	flags ThreadParticipantFlags NOT NULL,
	lastReadTime datetime NULL
)
GO

ALTER TABLE MessageThreadParticipant ADD CONSTRAINT PK_MessageThreadParticipant 
	PRIMARY KEY CLUSTERED (userId, threadId)
GO

ALTER TABLE MessageThreadParticipant ADD CONSTRAINT FK_MessageThreadParticipant_MessageThread 
	FOREIGN KEY (threadId) REFERENCES MessageThread (id)
GO

ALTER TABLE MessageThreadParticipant ADD CONSTRAINT FK_MessageThreadParticipant_ParticipantUser 
	FOREIGN KEY (userId) REFERENCES RegisteredUser (id)
GO





