IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RelocationArea') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE RelocationArea
GO

CREATE TABLE RelocationArea
( 
	[id] uniqueidentifier NOT NULL,
	displayName LocationDisplayName NOT NULL,
	candidateId uniqueidentifier NOT NULL,
	areaId int NOT NULL
)
GO

ALTER TABLE RelocationArea ADD CONSTRAINT PK_RelocationArea 
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE RelocationArea ADD CONSTRAINT FK_RelocationArea_Candidate 
	FOREIGN KEY (candidateId) REFERENCES Candidate ([id])
GO

ALTER TABLE RelocationArea ADD CONSTRAINT FK_RelocationArea_GeographicalArea 
	FOREIGN KEY (areaId) REFERENCES GeographicalArea ([id])
GO
