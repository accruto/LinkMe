


IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Address') AND NAME = 'postcode')
	ALTER TABLE dbo.Address
	DROP COLUMN postcode

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Address') AND NAME = 'suburb')
	ALTER TABLE dbo.Address
	DROP COLUMN suburb

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Address') AND NAME = 'unstructuredLocation')
	ALTER TABLE dbo.Address
	DROP COLUMN unstructuredLocation

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Address') AND NAME = 'countrySubdivisionId')
BEGIN

	ALTER TABLE dbo.Address
	DROP CONSTRAINT FK_Address_CountrySubdivision

	ALTER TABLE dbo.Address
	DROP COLUMN countrySubdivisionId

END

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Address') AND NAME = 'localityId')
BEGIN

	DROP INDEX dbo.Address.IX_Address_localityId

	ALTER TABLE dbo.Address
	DROP CONSTRAINT FK_Address_Locality

	ALTER TABLE dbo.Address
	DROP COLUMN localityId

END
	
GO
