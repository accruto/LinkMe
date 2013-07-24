
-- Copy all ids and displayNames from GeographicalArea to NamedLocation

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = OBJECT_ID('NamedLocation') AND OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('GeographicalArea') AND NAME = 'displayName')
	BEGIN

		INSERT
			dbo.NamedLocation
		SELECT
			id, displayName
		FROM
			dbo.GeographicalArea AS GA
		WHERE
			GA.id NOT IN (SELECT id FROM dbo.NamedLocation)

	END

END

GO