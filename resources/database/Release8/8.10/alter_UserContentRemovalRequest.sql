IF EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_UserContentRemovalRequest_RegisteredUser') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE UserContentRemovalRequest DROP CONSTRAINT FK_UserContentRemovalRequest_RegisteredUser

IF NOT EXISTS(SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('FK_UserContentRemovalRequest_Contributor') AND  OBJECTPROPERTY(id, 'CnstIsColumn') = 1)
ALTER TABLE UserContentRemovalRequest ADD CONSTRAINT FK_UserContentRemovalRequest_Contributor 
	FOREIGN KEY (reporterId) REFERENCES Contributor (id);

