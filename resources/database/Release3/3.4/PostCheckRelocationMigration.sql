SET NOCOUNT ON

DECLARE @before INT
DECLARE @after INT
DECLARE @total INT
DECLARE @tally INT

SET @total = 0
SET @tally = 0

-- All relocation locations

SELECT @before = COUNT(*) FROM RelocationArea
SELECT @total = @before

SELECT @after = COUNT(*) FROM RelocationArea AS ra
WHERE EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' locations have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' locations out of ' + CONVERT(NVARCHAR(100), @before) + ' locations have been migrated'

-- All countries

SELECT @before = COUNT(*) FROM RelocationArea
WHERE areaId <= 11

SELECT @after = COUNT(*) FROM RelocationArea AS ra
WHERE areaId <= 11
AND EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' countries have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' countries out of ' + CONVERT(NVARCHAR(100), @before) + ' have been migrated'

-- All country subdivisions

SELECT @before = COUNT(*) FROM RelocationArea
WHERE areaId > 11 AND areaId <= 19

SELECT @after = COUNT(*) FROM RelocationArea AS ra
WHERE areaId > 11 AND areaId <= 19
AND EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' subdivisions have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' subdivisions out of ' + CONVERT(NVARCHAR(100), @before) + ' have been migrated'

-- All regions

SELECT @before = COUNT(*) FROM RelocationArea
WHERE areaId > 2807 AND areaId <= 2816

SELECT @after = COUNT(*) FROM RelocationArea AS ra
WHERE areaId > 2807 AND areaId <= 2816
AND EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' regions have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' regions out of ' + CONVERT(NVARCHAR(100), @before) + ' have been migrated'

-- All postal suburbs

SELECT @before = COUNT(*) FROM RelocationArea AS ra
INNER JOIN Locality AS l ON l.id = ra.areaId
INNER JOIN PostalCode AS pc ON pc.localityId = l.id
INNER JOIN PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
INNER JOIN NamedLocation AS nl ON nl.id = ps.id
WHERE ((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
AND ra.displayName LIKE nl.displayName + ' ' + cs.shortDisplayName

SELECT @after = COUNT(*) FROM RelocationArea AS ra
INNER JOIN Locality AS l ON l.id = ra.areaId
INNER JOIN PostalCode AS pc ON pc.localityId = l.id
INNER JOIN PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
INNER JOIN NamedLocation AS nl ON nl.id = ps.id
WHERE ((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
AND ra.displayName LIKE nl.displayName + ' ' + cs.shortDisplayName
AND EXISTS (SELECT * FROM RelocationLocation WHERE id = ra.id)

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' suburbs have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' suburbs out of ' + CONVERT(NVARCHAR(100), @before) + ' have been migrated'

-- Check the tally

PRINT ''
IF (@total = @tally)
	PRINT 'All relocation locations have been accounted for'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20),@tally) + ' relocation locations out of ' + CONVERT(NVARCHAR(20), @total) + ', (' + CONVERT(NVARCHAR(20), @total - @tally) + ' missing) have been accounted for'
