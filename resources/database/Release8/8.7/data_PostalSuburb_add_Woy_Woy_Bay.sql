-- The last id currently in the database is 21140, so start from there.

DECLARE @id INT
SET @id = 21141

DECLARE @displayName NVARCHAR(100)
SET @displayName = 'Woy Woy Bay'

DECLARE @postcodeId INT
DECLARE @countrySubdivisionId INT

-- Make the postcode and subdivision the same as Woy Woy.

SELECT
	@postcodeId = postcodeId,
	@countrySubdivisionId = countrySubdivisionId
FROM
	dbo.PostalSuburb AS ps
INNER JOIN
	dbo.NamedLocation AS nl ON nl.id = ps.id
WHERE
	nl.displayName = 'Woy Woy'

-- NamedLocation

IF NOT EXISTS (SELECT * FROM dbo.NamedLocation WHERE id = @id)
	INSERT
		dbo.NamedLocation (id, displayName)
	VALUES
		(@id, @displayName)

-- PostalSuburb

IF NOT EXISTS (SELECT * FROM dbo.PostalSuburb WHERE id = @id)
	INSERT
		dbo.PostalSuburb (id, postcodeId, countrySubdivisionId)
	VALUES
		(@id, @postcodeId, @countrySubdivisionId)

