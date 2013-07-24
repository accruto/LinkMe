-- Iterate over all localities ordered by postcode, so that the lowest postcode is used as the
-- "display name" of the locality.

DECLARE localityCursor CURSOR FOR
SELECT locality, postCode, state, lat, lon
FROM linkme_owner.locality
ORDER BY postCode

OPEN localityCursor

DECLARE @locality VARCHAR(50)
DECLARE @postCode VARCHAR(8)
DECLARE @state VARCHAR(10)
DECLARE @lat REAL
DECLARE @lon REAL

FETCH NEXT FROM localityCursor
INTO @locality, @postCode, @state, @lat, @lon

WHILE @@FETCH_STATUS = 0
BEGIN
/*
	PRINT 'Processing: ' + @locality + ', ' + @postCode + ', ' + @state + ', lat = '
		+ CAST(@lat AS VARCHAR(20)) + ', lon = ' + CAST(@lon AS VARCHAR(20))
*/
	-- Find the Locality for this lat and lon.

	DECLARE @localityId INT
	DECLARE @existingPostcode VARCHAR(8)

	SET @localityId = NULL
	SET @existingPostcode = NULL

	SELECT @localityId = l.[id], @existingPostcode = ga.displayName
	FROM dbo.Locality l
	INNER JOIN dbo.GeographicalArea ga
	ON l.[id] = ga.[id]
	WHERE centroidLatitude = @lat AND centroidLongitude = @lon

	IF (@localityId IS NULL)
	BEGIN
		-- Not found, create a new locality using the postcode as its name.
/*
		PRINT 'Adding new Locality "' + @postcode + '", lat = ' + CAST(@lat AS VARCHAR(20))
			+ ', lon = ' + CAST(@lon AS VARCHAR(20))
*/
		SELECT @localityId = MAX([id]) + 1
		FROM dbo.GeographicalArea

		INSERT INTO dbo.GeographicalArea([id], displayName)
		VALUES (@localityId, @postcode)

		INSERT INTO dbo.Locality(id, centroidLatitude, centroidLongitude)
		VALUES(@localityId, @lat, @lon)
	END
/*	ELSE
	BEGIN
		-- Found an existing Locality, so this Locality has multiple postcodes.

		PRINT 'Found postcode "' + @postcode + '" for the same Locality as postcode "'
			+ @existingPostcode + '", lat = ' + CAST(@lat AS VARCHAR(20))
			+ ', lon = ' + CAST(@lon AS VARCHAR(20))
	END
*/
	-- Add the Locality->CountrySubdivision association, if it doesn't already exist.

	DECLARE @subdivisionId INT
	SET @subdivisionId = NULL

	SELECT @subdivisionId = cs.[id]
	FROM dbo.CountrySubdivision cs
	INNER JOIN dbo.GeographicalArea ga
	ON cs.[id] = ga.[id]
	WHERE displayName = @state

	IF (NOT EXISTS (
		SELECT * FROM dbo.LocalityCountrySubdivision
		WHERE localityId = @localityId AND countrySubdivisionId = @subdivisionId))
	BEGIN
/*
		PRINT 'Adding state "' + @state + '" for Locality "' + @locality + '"'
*/
		INSERT INTO dbo.LocalityCountrySubdivision(localityId, countrySubdivisionId)
		VALUES (@localityId, @subdivisionId)
	END

	-- Add the Postcode it it doesn't already exist.

	DECLARE @postcodeId INT
	SET @postcodeId = NULL

	SELECT @postcodeId = [id]
	FROM dbo.PostalCode
	WHERE localityId = @localityId AND postcode = @postCode

	IF (@postcodeId IS NULL)
	BEGIN
/*
		PRINT 'Adding postcode ' + @postCode
*/
		SELECT @postcodeId = MAX([id]) + 1
		FROM dbo.PostalCode

		IF (@postcodeId IS NULL) SET @postcodeId = 1

		INSERT INTO dbo.PostalCode([id], postcode, localityId)
		VALUES (@postcodeId, @postCode, @localityId)
	END

	-- Finally add the PostalSuburb if we have a name.

	IF (@locality <> '')
	BEGIN
/*
		PRINT 'Adding suburb ' + @locality
*/
		DECLARE @suburbId INT

		SELECT @suburbId = MAX([id]) + 1
		FROM dbo.PostalSuburb

		IF (@suburbId IS NULL) SET @suburbId = 1

		INSERT INTO dbo.PostalSuburb([id], displayName, postcodeId, countrySubdivisionId)
		VALUES (@suburbId, @locality, @postcodeId, @subdivisionId)
	END

	-- Fetch the next row.

	FETCH NEXT FROM localityCursor
	INTO @locality, @postCode, @state, @lat, @lon
END

CLOSE localityCursor
DEALLOCATE localityCursor

GO
