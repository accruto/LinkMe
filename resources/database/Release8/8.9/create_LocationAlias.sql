IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('LocationAlias') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LocationAlias
GO

CREATE TABLE LocationAlias ( 
	id INT NOT NULL,
	countryId TINYINT NOT NULL,
	alias LocationDisplayName NOT NULL,
	displayName LocationDisplayName NOT NULL,
)
GO

ALTER TABLE LocationAlias ADD CONSTRAINT PK_LocationAlias 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE LocationAlias ADD CONSTRAINT IX_LocationAlias_Alias
	UNIQUE NONCLUSTERED (alias)
GO

ALTER TABLE LocationAlias ADD CONSTRAINT FK_LocationAlias_Country
	FOREIGN KEY (countryId) REFERENCES Country (id)



