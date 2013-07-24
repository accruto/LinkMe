
-- Copy all ids and postcodes from PostalCode to NamedLocation

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = OBJECT_ID('NamedLocation') AND OBJECTPROPERTY(id, 'IsUserTable') = 1)
BEGIN

	IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('PostalCode') AND NAME = 'postcode')
	BEGIN

		-- Update the postal code ids by adding 3000 to them, doing the postal suburbs first.
	
		ALTER TABLE dbo.PostalSuburb
		DROP CONSTRAINT FK_PostalSuburb_PostalCode
	
		UPDATE
			dbo.PostalSuburb
		SET
			postcodeId = postcodeId + 3000
		WHERE
			postcodeId < 3000
	
		-- Update the ids in the PostalCode table.
	
		UPDATE
			dbo.PostalCode
		SET
			id = id + 3000
		WHERE
			id < 3000
	
		ALTER TABLE
			dbo.PostalSuburb
		ADD CONSTRAINT
			FK_PostalSuburb_PostalCode
		FOREIGN KEY
			(postcodeId)
		REFERENCES
			dbo.PostalCode (id)
	
		-- Insert a record in the NamedLocation table for each postal code.
	
		INSERT
			dbo.NamedLocation
		SELECT
			id, postcode
		FROM
			dbo.PostalCode
		WHERE
			id NOT IN (SELECT id FROM dbo.NamedLocation)
			AND id >= 3000

	END

END

GO