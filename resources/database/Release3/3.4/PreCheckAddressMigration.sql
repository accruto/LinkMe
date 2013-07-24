SET NOCOUNT ON

DECLARE @before INT
DECLARE @total INT
DECLARE @tally INT

SET @total = 0
SET @tally = 0

-- All addresses

SELECT @before = COUNT(*) FROM address
SELECT @total = @before

PRINT CONVERT(NVARCHAR(20), @before) + ' total addresses to be migrated'

-- All non-australian addresses

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId <= 11
AND countrySubdivisionId <> 1

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' non-australian addresses to be migrated'

-- All australian addresses, countrySubdivisionId = 1, suburb, postcode, localityId IS NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId = 1
AND suburb IS NULL
AND postcode IS NULL
AND localityId IS NULL

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' australian addresses with CS=1, suburb=NULL, postcode=NULL to be migrated'

-- All australian addresses, countrySubdivisionId = 1, suburb IS NULL, postcode localityId IS NOT NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId = 1
AND suburb IS NULL
AND postcode IS NOT NULL
AND localityId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' australian addresses with CS=1, suburb=NULL, postcode=NULL to be migrated'

-- All australian addresses, countrySubdivisionId <> 1, unstructuredLocation IS NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NULL

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' australian addresses with UL=NULL to be migrated'

-- All australian addresses, countrySubdivisionId <> 1, unstructuredLocation IS NOT NULL, suburb, postcode, localityId IS NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NOT NULL
AND suburb IS NULL
AND postcode IS NULL
AND localityId IS NULL

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' australian addresses with UL NOT NULL, suburb=NULL, postcode=NULL to be migrated'

-- All australian addresses, countrySubdivisionId <> 1, unstructuredLocation IS NOT NULL, suburb, postcode, localityId NOT NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NOT NULL
AND suburb IS NULL
AND postcode IS NOT NULL
AND localityId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @before

PRINT CONVERT(NVARCHAR(20), @before) + ' australian addresses with UL NOT NULL, suburb=NULL, postcode NOT NULL to be migrated'

-- Check the tally

PRINT ''
IF (@total = @tally)
	PRINT 'All addresses have been accounted for'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20),@tally) + ' addresses out of ' + CONVERT(NVARCHAR(20), @total) + ', (' + CONVERT(NVARCHAR(20), @total - @tally) + ' missing) have been accounted for'
