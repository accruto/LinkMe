
IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.PostalCode') AND NAME = 'postcode')
BEGIN

	ALTER TABLE dbo.PostalCode
	DROP COLUMN postcode
	
END
GO
