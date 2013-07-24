
IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('GeographicalArea') AND NAME = 'displayName')
BEGIN

	ALTER TABLE dbo.GeographicalArea
	DROP COLUMN displayName
	
END
GO
