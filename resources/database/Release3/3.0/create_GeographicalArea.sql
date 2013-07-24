--  -------------------------------------------------- 
--  Generated by Enterprise Architect Version 7.0.813
--  Created On : Friday, 13 July, 2007 
--  DBMS       : SQL Server 2000 
--  -------------------------------------------------- 

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GeographicalArea') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GeographicalArea
GO

CREATE TABLE GeographicalArea ( 
	id int NOT NULL,
	displayName LocationDisplayName NULL,    -- The name of this area as it would normally be displayed to a user. NULL for the default CountrySubdivision (ie. where a country has no subdivisions). Not globally unique, but should be unique in combination with linked classes, eg. the combination of CountrySubdivision and Country names should be unique, as should the combination of Locality CountrySubdivision and Country. 
)
GO

ALTER TABLE GeographicalArea ADD CONSTRAINT PK_GeographicalArea 
	PRIMARY KEY CLUSTERED (id)
GO


EXEC sp_addextendedproperty 'MS_Description', 'The name of this area as it would normally be displayed to a user. NULL for the default CountrySubdivision (ie. where a country has no subdivisions). Not globally unique, but should be unique in combination with linked classes, eg. the combination of CountrySubdivision and Country names should be unique, as should the combination of Locality, CountrySubdivision and Country.', 'User', dbo, 'table', GeographicalArea, 'column', displayName
GO
