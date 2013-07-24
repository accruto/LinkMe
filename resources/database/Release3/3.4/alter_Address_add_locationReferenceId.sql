
IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Address') AND NAME = 'locationReferenceId')
BEGIN

	ALTER TABLE dbo.Address
	ADD locationReferenceId UNIQUEIDENTIFIER NULL

	ALTER TABLE
		dbo.Address
	ADD CONSTRAINT
		FK_Address_LocationReference
	FOREIGN KEY
		(locationReferenceId)
	REFERENCES
		dbo.LocationReference (id)

END
	
GO
