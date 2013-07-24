-- Find the old (redundant) locality for postcode 6992

DECLARE @oldLocalityId INT

SELECT @oldLocalityId = localityId
FROM PostalCode 
WHERE postcode = '6992'

-- Update "ARMADALE, WA" PO boxes to point to the same Locality as the "ARMADALE, WA" delivery area

UPDATE pt
SET localityId = pf.localityId
FROM PostalCode pf, PostalCode pt
WHERE pt.postcode = '6992' AND pf.postcode = '6112'

-- Delete the old PostalSuburb and Locality, but keep the PostalCode

DELETE PostalSuburb
FROM PostalSuburb ps
INNER JOIN PostalCode pc
ON ps.postCodeId = pc.[id]
WHERE pc.postcode = '6992'

DELETE LocalityCountrySubdivision
WHERE localityId = @oldLocalityId

DELETE Locality
WHERE [id] = @oldLocalityId

GO

/* Check queries:

SELECT *
FROM PostalSuburb ps
LEFT OUTER JOIN PostalCode pc
ON ps.postCodeId = pc.[id]
LEFT OUTER JOIN Locality l
ON pc.localityId = l.[id]
LEFT OUTER JOIN LocalityCountrySubdivision lcs
ON lcs.localityId = l.[id]
LEFT OUTER JOIN LocalityRegion lr
ON lr.localityId = l.[id]
WHERE pc.postcode = '6992'

SELECT *
FROM PostalCode
WHERE postcode IN ('6112', '6992')

*/
