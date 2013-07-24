SET NOCOUNT ON

DECLARE @before INT
DECLARE @total INT
DECLARE @tally INT

SET @total = 0
SET @tally = 0

-- All relocation locations

SELECT @before = COUNT(*) FROM RelocationArea
SELECT @total = @before

PRINT CONVERT(NVARCHAR(20), @before) + ' total locations to be migrated'

-- All countries

SELECT @before = COUNT(*) FROM RelocationArea
WHERE areaId <= 11

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' countries to be migrated'

-- All country subdivisions

SELECT @before = COUNT(*) FROM RelocationArea
WHERE areaId > 11 AND areaId <= 19

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' country subdivisions to be migrated'

-- All regions

SELECT @before = COUNT(*) FROM RelocationArea
WHERE areaId > 2807 AND areaId <= 2816

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' regions to be migrated'

-- All good postal suburbs

SELECT @before = COUNT(*) FROM RelocationArea AS ra
INNER JOIN Locality AS l ON l.id = ra.areaId
INNER JOIN PostalCode AS pc ON pc.localityId = l.id
INNER JOIN PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
WHERE ((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
AND ra.displayName LIKE ps.displayName + ' ' + cs.shortDisplayName
AND areaId <> 46 AND areaId <> 96

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' good postal suburbs to be migrated'

-- Parramatta

SELECT @before = COUNT(*) FROM RelocationArea AS ra
INNER JOIN Locality AS l ON l.id = ra.areaId
INNER JOIN PostalCode AS pc ON pc.localityId = l.id
INNER JOIN PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
WHERE areaId = 46
AND pc.id = 35
AND ra.displayName LIKE ps.displayName + ' ' + cs.shortDisplayName

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' parramatta postal suburbs to be migrated'

-- Alice springs

SELECT @before = COUNT(*) FROM RelocationArea AS ra
INNER JOIN Locality AS l ON l.id = ra.areaId
INNER JOIN PostalCode AS pc ON pc.localityId = l.id
INNER JOIN PostalSuburb AS ps ON ps.postcodeId = pc.id
INNER JOIN CountrySubdivision AS cs ON cs.id = ps.countrySubdivisionId
WHERE areaId = 96
AND pc.id = 95
AND ra.displayName LIKE ps.displayName + ' ' + cs.shortDisplayName

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' alice springs postal suburbs to be migrated'

-- Check the tally

PRINT ''
IF (@total = @tally)
	PRINT 'All relocation locations have been accounted for'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20),@tally) + ' relocation locations out of ' + CONVERT(NVARCHAR(20), @total) + ', (' + CONVERT(NVARCHAR(20), @total - @tally) + ' missing) have been accounted for'
