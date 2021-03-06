--   -------------------------------------------------- 
--   Generated by Enterprise Architect Version 7.0.813
--   Created On : Thursday, 12 July, 2007 
--   DBMS       : SQL Server 2000 
--   -------------------------------------------------- 

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('NetworkerReference') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE NetworkerReference
GO

CREATE TABLE NetworkerReference ( 
	id uniqueidentifier NOT NULL,
	createdTime datetime NOT NULL,
	relationship RefereeRelationship NOT NULL,
	status RequestStatus NOT NULL,
	text nvarchar(2000) NOT NULL,
	subjectId uniqueidentifier NOT NULL,
	refereeId uniqueidentifier NOT NULL
)
GO

ALTER TABLE NetworkerReference ADD CONSTRAINT PK_NetworkerReference 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE NetworkerReference ADD CONSTRAINT FK_NetworkerReference_RefereeNetworker 
	FOREIGN KEY (refereeId) REFERENCES Networker (id)
GO

ALTER TABLE NetworkerReference ADD CONSTRAINT FK_NetworkerReference_SubjectNetworker 
	FOREIGN KEY (subjectId) REFERENCES Networker (id)
GO








