
-- Update some addresses that should have been updated with locality changes.

UPDATE
	Address
SET
	localityId = 2817
WHERE
	postcode = '6798'

UPDATE
	Address
SET
	localityId = 440
WHERE
	postcode = '2442'

-- 1. Non-australia addresses: countrySubdivisionId <= 11 and <> 1, postcode, suburb, localityId IS NULL.

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	a.id, a.unstructuredLocation, a.countrySubdivisionId, a.countrySubdivisionId, NULL
FROM
	Address a
WHERE
	countrySubdivisionId <= 11 AND countrySubdivisionId <> 1
	AND locationReferenceId IS NULL

UPDATE
	Address
SET
	locationReferenceId = id
WHERE
	countrySubdivisionId <= 11 AND countrySubdivisionId <> 1
	AND locationReferenceId IS NULL

-- 2. Australia addresses: countrySubdivisionId = 1, postcode, suburb, localityId IS NULL.

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	a.id, a.unstructuredLocation, a.countrySubdivisionId, a.countrySubdivisionId, NULL
FROM
	Address AS a
WHERE
	countrySubdivisionId = 1
	AND unstructuredLocation IS NOT NULL
	AND postcode IS NULL
	AND suburb IS NULL
	AND localityId IS NULL
	AND locationReferenceId IS NULL

UPDATE
	Address
SET
	locationReferenceId = id
WHERE
	countrySubdivisionId = 1
	AND unstructuredLocation IS NOT NULL
	AND postcode IS NULL
	AND suburb IS NULL
	AND localityId IS NULL
	AND locationReferenceId IS NULL

-- 3. Australia addresses: countrySubdivisionId = 1, suburb IS NULL, postcode, localityId IS NOT NULL.

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	a.id, a.unstructuredLocation, pcnl.id, a.countrySubdivisionId, a.localityId
FROM
	Address AS a
INNER JOIN
	NamedLocation AS pcnl ON pcnl.displayName = a.postcode
INNER JOIN
	PostalCode AS pc ON pc.id = pcnl.id
INNER JOIN
	Locality AS l ON l.id = pc.localityId
WHERE
	l.id = a.localityId
	AND a.countrySubdivisionId = 1
	AND a.postcode IS NOT NULL
	AND a.suburb IS NULL
	AND a.localityId IS NOT NULL
	AND a.locationReferenceId IS NULL

UPDATE
	Address
SET
	locationReferenceId = a.id
FROM
	Address AS a
INNER JOIN
	NamedLocation AS pcnl ON pcnl.displayName = a.postcode
INNER JOIN
	PostalCode AS pc ON pc.id = pcnl.id
INNER JOIN
	Locality AS l ON l.id = pc.localityId
WHERE
	l.id = a.localityId
	AND a.countrySubdivisionId = 1
	AND a.postcode IS NOT NULL
	AND a.suburb IS NULL
	AND a.localityId IS NOT NULL
	AND a.locationReferenceId IS NULL

-- 4. Australia addresses: countrySubdivisionId > 11, unstructuredLocation IS NULL.

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	a.id, NULL, ps.id, a.countrySubdivisionId, a.localityId
FROM
	Address AS a
INNER JOIN
	Locality AS l ON l.id = a.localityId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN
	NamedLocation AS psnl ON psnl.id = ps.id
INNER JOIN
	NamedLocation AS pcnl ON pcnl.id = pc.id
WHERE
	a.countrySubdivisionId > 11
	AND unstructuredLocation IS NULL
	AND ps.countrySubdivisionId = a.countrySubdivisionId
	AND psnl.displayName = a.suburb
	AND pcnl.displayName = a.postcode
	AND locationReferenceId IS NULL

UPDATE
	Address
SET
	locationReferenceId = a.id
FROM
	Address AS a
INNER JOIN
	Locality AS l ON l.id = a.localityId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN
	NamedLocation AS psnl ON psnl.id = ps.id
INNER JOIN
	NamedLocation AS pcnl ON pcnl.id = pc.id
WHERE
	a.countrySubdivisionId > 11
	AND unstructuredLocation IS NULL
	AND ps.countrySubdivisionId = a.countrySubdivisionId
	AND psnl.displayName = a.suburb
	AND pcnl.displayName = a.postcode
	AND locationReferenceId IS NULL

-- 5. Australia addresses: countrySubdivisionId > 11, unstructuredLocation IS NOT NULL, postcode, suburb, localityId IS NULL

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	a.id, a.unstructuredLocation, a.countrySubdivisionId, a.countrySubdivisionId, NULL
FROM
	Address AS a
WHERE
	countrySubdivisionId > 11
	AND unstructuredLocation IS NOT NULL
	AND postcode IS NULL
	AND suburb IS NULL
	AND localityId IS NULL
	AND locationReferenceId IS NULL

UPDATE
	Address
SET
	locationReferenceId = id
WHERE
	countrySubdivisionId > 11
	AND unstructuredLocation IS NOT NULL
	AND postcode IS NULL
	AND suburb IS NULL
	AND localityId IS NULL
	AND locationReferenceId IS NULL

-- 6. Australia addresses: countrySubdivisionId > 11, unstructuredLocation is NOT NULL, suburb IS NULL, postcode, localityId IS NOT NULL.

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	a.id, a.unstructuredLocation, pcnl.id, a.countrySubdivisionId, a.localityId
FROM
	Address AS a
INNER JOIN
	NamedLocation AS pcnl ON pcnl.displayName = a.postcode
INNER JOIN
	PostalCode AS pc ON pc.id = pcnl.id
INNER JOIN
	Locality AS l ON l.id = pc.localityId
WHERE
	l.id = a.localityId
	AND a.countrySubdivisionId > 11
	AND a.postcode IS NOT NULL
	AND a.suburb IS NULL
	AND a.localityId IS NOT NULL
	AND a.locationReferenceId IS NULL

UPDATE
	Address
SET
	locationReferenceId = a.id
FROM
	Address AS a
INNER JOIN
	NamedLocation AS pcnl ON pcnl.displayName = a.postcode
INNER JOIN
	PostalCode AS pc ON pc.id = pcnl.id
INNER JOIN
	Locality AS l ON l.id = pc.localityId
WHERE
	l.id = a.localityId
	AND a.countrySubdivisionId > 11
	AND a.postcode IS NOT NULL
	AND a.suburb IS NULL
	AND a.localityId IS NOT NULL
	AND a.locationReferenceId IS NULL

