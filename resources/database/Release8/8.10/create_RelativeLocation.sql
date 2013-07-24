IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RelativeLocation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE RelativeLocation
GO

CREATE TABLE RelativeLocation ( 
	id INT NOT NULL,
	countryId TINYINT NOT NULL,
	displayName LocationDisplayName NOT NULL,
	prefix BIT NOT NULL,
	suffix BIT NOT NULL
)
GO

ALTER TABLE RelativeLocation ADD CONSTRAINT PK_RelativeLocation 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE RelativeLocation ADD CONSTRAINT IX_RelativeLocation_DisplayName
	UNIQUE NONCLUSTERED (displayName)
GO

ALTER TABLE RelativeLocation ADD CONSTRAINT FK_RelativeLocation_Country
	FOREIGN KEY (countryId) REFERENCES Country (id)



