
-- Copy all ids and displayNames from PostalSuburb to NamedLocation

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('NamedLocation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('PostalSuburb') AND NAME = 'displayName')
	BEGIN

		-- Update the ids in the PostalSuburb table by adding 11000 to them.
	
		UPDATE
			dbo.PostalSuburb
		SET
			id = id + 11000
		WHERE
			id < 11000
	
		-- Insert a record in the NamedLocation table for each postal suburb.

		INSERT
			dbo.NamedLocation
		SELECT
			id, displayName
		FROM
			dbo.PostalSuburb
		WHERE
			id NOT IN (SELECT id FROM dbo.NamedLocation)
			AND id >= 11000

	END

END

GO