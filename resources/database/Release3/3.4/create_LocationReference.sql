
IF NOT EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('LocationReference') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	CREATE TABLE LocationReference ( 
		id uniqueidentifier NOT NULL,
		unstructuredLocation nvarchar(100) NULL,
		namedLocationId int NULL,
		countrySubdivisionId int NOT NULL,
		localityId int NULL,
	)
	
	ALTER TABLE LocationReference ADD CONSTRAINT PK_LocationReference
		PRIMARY KEY CLUSTERED (id)
	
	ALTER TABLE LocationReference ADD CONSTRAINT FK_LocationReference_NamedLocation
		FOREIGN KEY (namedLocationId) REFERENCES NamedLocation (id)

	ALTER TABLE LocationReference ADD CONSTRAINT FK_LocationReference_CountrySubdivision 
		FOREIGN KEY (countrySubdivisionId) REFERENCES CountrySubdivision (id)

	ALTER TABLE LocationReference ADD CONSTRAINT FK_LocationReference_Locality
		FOREIGN KEY (localityId) REFERENCES Locality (id)

END	
	
	
	
	
	
	
	
	
