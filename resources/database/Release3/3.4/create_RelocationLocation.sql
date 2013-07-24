IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RelocationLocation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE RelocationLocation
GO

CREATE TABLE RelocationLocation
( 
	[id] uniqueidentifier NOT NULL,
	candidateId uniqueidentifier NOT NULL,
	locationReferenceId uniqueidentifier NOT NULL
)
GO

ALTER TABLE RelocationLocation ADD CONSTRAINT PK_RelocationLocation
	PRIMARY KEY CLUSTERED ([id])
GO

ALTER TABLE RelocationLocation ADD CONSTRAINT FK_RelocationLocation_Candidate 
	FOREIGN KEY (candidateId) REFERENCES Candidate ([id])
GO

ALTER TABLE RelocationLocation ADD CONSTRAINT FK_RelocationLocation_GeographicalArea 
	FOREIGN KEY (locationReferenceId) REFERENCES LocationReference ([id])
GO
