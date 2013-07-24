SET NOCOUNT ON

DECLARE @before INT
DECLARE @total INT
DECLARE @tally INT

SET @total = 0
SET @tally = 0

-- All job locations

SELECT @before = COUNT(*) FROM JobAdArea
SELECT @total = @before

PRINT CONVERT(NVARCHAR(20), @before) + ' total locations to be migrated'

-- 1. All locations with 1 subdivision and postcode not set.

SELECT @before = COUNT(*)
FROM JobAd AS ja
INNER JOIN JobAdArea AS jaa ON jaa.jobAdId = ja.id
WHERE ((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
AND postcode NOT IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' locations with 1 subdivision and postcode not set to be migrated'

-- 2. All locations with 1 subdivision and postcode set.

SELECT @before = COUNT(*)
FROM JobAd AS ja
INNER JOIN JobAdArea AS jaa ON jaa.jobAdId = ja.id
WHERE ((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
AND areaId NOT IN (46, 510, 561, 569, 1128, 1221, 1260)
AND postcode IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' locations with 1 subdivision and postcode set to be migrated'

-- 3. All locations with multiple subdivisions and postcode not set.

SELECT @before = COUNT(*)
FROM JobAd AS ja
INNER JOIN JobAdArea AS jaa ON jaa.jobAdId = ja.id
WHERE ((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
AND areaId IN (46, 510, 561, 569, 1128, 1221, 1260)
AND postcode NOT IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' locations with multiple subdivisions and postcode not set to be migrated'

-- 4. All locations which multiple countrysubdivisions and postcode set.

SELECT @before = COUNT(*)
FROM JobAd AS ja
INNER JOIN JobAdArea AS jaa ON jaa.jobAdId = ja.id
WHERE ((areaId > 19 AND areaId <= 2807) OR areaId = 2817)
AND areaId IN (46, 510, 561, 569, 1128, 1221, 1260)
AND postcode IN (SELECT displayName FROM NamedLocation AS nl INNER JOIN PostalCode AS pc ON pc.id = nl.id WHERE pc.localityId = areaId)

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' locations with multiple subdivisions and postcode set to be migrated'

-- Check the tally

PRINT ''
IF (@total = @tally)
	PRINT 'All job ad locations have been accounted for'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20),@tally) + ' job ad locations out of ' + CONVERT(NVARCHAR(20), @total) + ', (' + CONVERT(NVARCHAR(20), @total - @tally) + ' missing) have been accounted for'
