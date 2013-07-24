
-- 1. All locations with 1 subdivision and postcode not set.

BEGIN TRANSACTION

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	ja.id, ja.location, jaa.areaId, lcs.countrySubdivisionId, jaa.areaId
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND postcode NOT IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)
	AND jal.jobAdId IS NULL

INSERT
	JobAdLocation (jobAdId, locationReferenceId)
SELECT
	ja.id, ja.id
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND postcode NOT IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)
	AND jal.jobAdId IS NULL

COMMIT TRANSACTION

-- 2. All locations with 1 subdivision and postcode set.

BEGIN TRANSACTION

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	ja.id, ja.location, pc.id, lcs.countrySubdivisionId, jaa.areaId
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL
	AND areaId < 500

INSERT
	JobAdLocation (jobAdId, locationReferenceId)
SELECT
	ja.id, ja.id
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL
	AND areaId < 500

COMMIT TRANSACTION

BEGIN TRANSACTION

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	ja.id, ja.location, pc.id, lcs.countrySubdivisionId, jaa.areaId
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL
	AND areaId >= 500 AND areaId < 1000

INSERT
	JobAdLocation (jobAdId, locationReferenceId)
SELECT
	ja.id, ja.id
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL
	AND areaId >= 500 AND areaId < 1000

COMMIT TRANSACTION

BEGIN TRANSACTION

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	ja.id, ja.location, pc.id, lcs.countrySubdivisionId, jaa.areaId
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL
	AND areaId >= 1000

INSERT
	JobAdLocation (jobAdId, locationReferenceId)
SELECT
	ja.id, ja.id
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	LocalityCountrySubdivision AS lcs ON lcs.localityId = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL
	AND areaId >= 1000

COMMIT TRANSACTION

-- 3. All locations with multiple subdivisions and postcode not set.

BEGIN TRANSACTION

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	ja.id, ja.location, jaa.areaId, 1, jaa.areaId
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND postcode NOT IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)
	AND jal.jobAdId IS NULL

INSERT
	JobAdLocation (jobAdId, locationReferenceId)
SELECT
	ja.id, ja.id
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND postcode NOT IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)
	AND jal.jobAdId IS NULL

COMMIT TRANSACTION

-- 4. All locations with multiple subdivisions and postcode set.

BEGIN TRANSACTION

INSERT
	LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
SELECT
	ja.id, ja.location, pc.id, 1, jaa.areaId
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL

INSERT
	JobAdLocation (jobAdId, locationReferenceId)
SELECT
	ja.id, ja.id
FROM
	JobAd AS ja
INNER JOIN
	JobAdArea AS jaa ON jaa.jobAdId = ja.id
INNER JOIN
	Locality AS l on l.id = jaa.areaId
INNER JOIN
	PostalCode AS pc ON pc.localityId = l.id
INNER JOIN
	NamedLocation AS nl ON (nl.id = pc.id AND nl.displayName = ja.postcode)
LEFT OUTER JOIN
	JobAdLocation AS jal ON jal.jobAdId = ja.id
WHERE
	((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
	AND areaId IN (46, 510, 561, 569, 1128, 1221, 1260)
	AND jal.jobAdId IS NULL

COMMIT TRANSACTION