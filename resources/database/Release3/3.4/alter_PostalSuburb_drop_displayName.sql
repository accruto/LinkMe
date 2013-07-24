
IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.PostalSuburb') AND NAME = 'displayName')
BEGIN

	ALTER TABLE dbo.PostalSuburb
	DROP COLUMN displayName
	
END
GO
