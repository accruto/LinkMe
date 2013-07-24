IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('LocationAlias') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LocationAlias
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('LocationAbbreviation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LocationAbbreviation
GO

CREATE TABLE LocationAbbreviation ( 
	id INT NOT NULL,
	countryId TINYINT NOT NULL,
	abbreviation LocationDisplayName NOT NULL,
	displayName LocationDisplayName NOT NULL,
	prefix BIT NOT NULL,
	suffix BIT NOT NULL
)
GO

ALTER TABLE LocationAbbreviation ADD CONSTRAINT PK_LocationAbbreviation 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE LocationAbbreviation ADD CONSTRAINT IX_LocationAbbreviation_Abbreviation
	UNIQUE NONCLUSTERED (abbreviation)
GO

ALTER TABLE LocationAbbreviation ADD CONSTRAINT FK_LocationAbbreviation_Country
	FOREIGN KEY (countryId) REFERENCES Country (id)



