
-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @candidateId UNIQUEIDENTIFIER
DECLARE @areaId INT
DECLARE @countrySubdivisionId INT
DECLARE @postalSuburbId INT
DECLARE @locationReferenceId UNIQUEIDENTIFIER

-- Countries

DECLARE cr CURSOR STATIC FORWARD_ONLY FOR
SELECT
	id, candidateId, areaId
FROM
	RelocationArea AS ra
WHERE
	areaId <= 11
	AND NOT EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

OPEN cr

FETCH NEXT FROM cr INTO @id, @candidateId, @areaId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @locationReferenceId = NEWID()

	-- Location reference

	INSERT
		LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
	VALUES
		(@locationReferenceId, NULL, @areaId, @areaId, NULL)

	-- Relocation location

	INSERT
		RelocationLocation (id, candidateId, locationReferenceId)
	VALUES
		(@id, @candidateId, @locationReferenceId)

	FETCH NEXT FROM cr INTO @id, @candidateId, @areaId
END

CLOSE cr
DEALLOCATE cr

-- Country Subdivisions

DECLARE cr CURSOR STATIC FORWARD_ONLY FOR
SELECT
	id, candidateId, areaId
FROM
	RelocationArea AS ra
WHERE
	areaId > 11 AND areaId <= 19
	AND NOT EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

OPEN cr

FETCH NEXT FROM cr INTO @id, @candidateId, @areaId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @locationReferenceId = NEWID()

	-- Location reference

	INSERT
		LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
	VALUES
		(@locationReferenceId, NULL, @areaId, @areaId, NULL)

	-- Relocation location

	INSERT
		RelocationLocation (id, candidateId, locationReferenceId)
	VALUES
		(@id, @candidateId, @locationReferenceId)

	FETCH NEXT FROM cr INTO @id, @candidateId, @areaId
END

CLOSE cr
DEALLOCATE cr

-- Regions

DECLARE cr CURSOR STATIC FORWARD_ONLY FOR
SELECT
	id, candidateId, areaId
FROM
	RelocationArea AS ra
WHERE
	areaId > 2807 AND areaId <= 2816
	AND NOT EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

OPEN cr

FETCH NEXT FROM cr INTO @id, @candidateId, @areaId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @locationReferenceId = NEWID()

	-- All regions except 2808 (Canberra) exist within the same country subdivision

	IF (@areaId <> 2808)
		SELECT
			@countrySubdivisionId = lcs.countrySubdivisionId
		FROM
			Region AS r
		INNER JOIN
			LocalityRegion AS lr ON lr.regionid = r.id
		INNER JOIN
			Locality AS l ON l.id = lr.localityid
		INNER JOIN
			LocalityCountrySubdivision AS lcs ON lcs.localityid = l.id
		WHERE
			r.id = @areaId
	ELSE
		SELECT
			@countrySubdivisionId = 1

	-- Location reference

	INSERT
		LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
	VALUES
		(@locationReferenceId, NULL, @areaId, @countrySubdivisionId, NULL)

	-- Relocation location

	INSERT
		RelocationLocation (id, candidateId, locationReferenceId)
	VALUES
		(@id, @candidateId, @locationReferenceId)

	FETCH NEXT FROM cr INTO @id, @candidateId, @areaId
END

CLOSE cr
DEALLOCATE cr

-- Postal Suburbs - good ones.

DECLARE cr CURSOR STATIC FORWARD_ONLY FOR
SELECT
	ra.id, candidateId, areaId, ps.id, cs.id
FROM
	RelocationArea AS ra
INNER JOIN
	Locality AS l ON l.id = ra.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN
	CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
INNER JOIN
	NamedLocation AS nl ON nl.id = ps.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId <> 46 AND areaId <> 96
	AND ra.displayName LIKE nl.displayName + ' ' + cs.shortDisplayName
	AND NOT EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

OPEN cr

FETCH NEXT FROM cr INTO @id, @candidateId, @areaId, @postalSuburbId, @countrySubdivisionId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @locationReferenceId = NEWID()

	-- Location reference

	INSERT
		LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
	VALUES
		(@locationReferenceId, NULL, @postalSuburbId, @countrySubdivisionId, @areaId)

	-- Relocation location

	INSERT
		RelocationLocation (id, candidateId, locationReferenceId)
	VALUES
		(@id, @candidateId, @locationReferenceId)

	FETCH NEXT FROM cr INTO @id, @candidateId, @areaId, @postalSuburbId, @countrySubdivisionId
END

CLOSE cr
DEALLOCATE cr

-- Postal Suburbs - parramatta

DECLARE cr CURSOR STATIC FORWARD_ONLY FOR
SELECT
	ra.id, candidateId, areaId, ps.id, cs.id
FROM
	RelocationArea AS ra
INNER JOIN
	Locality AS l ON l.id = ra.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN
	CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
INNER JOIN
	NamedLocation AS nl ON nl.id = ps.id
WHERE
	areaId = 46
	AND pc.id = 35
	AND ra.displayName LIKE nl.displayName + ' ' + cs.shortDisplayName
	AND NOT EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

OPEN cr

FETCH NEXT FROM cr INTO @id, @candidateId, @areaId, @postalSuburbId, @countrySubdivisionId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @locationReferenceId = NEWID()

	-- Location reference

	INSERT
		LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
	VALUES
		(@locationReferenceId, NULL, @postalSuburbId, @countrySubdivisionId, @areaId)

	-- Relocation location

	INSERT
		RelocationLocation (id, candidateId, locationReferenceId)
	VALUES
		(@id, @candidateId, @locationReferenceId)

	FETCH NEXT FROM cr INTO @id, @candidateId, @areaId, @postalSuburbId, @countrySubdivisionId
END

CLOSE cr
DEALLOCATE cr

-- Postal Suburbs - alice springs

DECLARE cr CURSOR STATIC FORWARD_ONLY FOR
SELECT
	ra.id, candidateId, areaId, ps.id, cs.id
FROM
	RelocationArea AS ra
INNER JOIN
	Locality AS l ON l.id = ra.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN
	CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
INNER JOIN
	NamedLocation AS nl ON nl.id = ps.id
WHERE
	areaId = 96
	AND pc.id = 95
	AND ra.displayName LIKE nl.displayName + ' ' + cs.shortDisplayName
	AND NOT EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

OPEN cr

FETCH NEXT FROM cr INTO @id, @candidateId, @areaId, @postalSuburbId, @countrySubdivisionId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @locationReferenceId = NEWID()

	-- Location reference

	INSERT
		LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
	VALUES
		(@locationReferenceId, NULL, @postalSuburbId, @countrySubdivisionId, @areaId)

	-- Relocation location

	INSERT
		RelocationLocation (id, candidateId, locationReferenceId)
	VALUES
		(@id, @candidateId, @locationReferenceId)

	FETCH NEXT FROM cr INTO @id, @candidateId, @areaId, @postalSuburbId, @countrySubdivisionId
END

CLOSE cr
DEALLOCATE cr




