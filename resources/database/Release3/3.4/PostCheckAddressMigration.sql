SET NOCOUNT ON

DECLARE @before INT
DECLARE @after INT
DECLARE @total INT
DECLARE @tally INT

SET @total = 0
SET @tally = 0

-- All addresses

SELECT @before = COUNT(*) FROM address
SELECT @total = @before

SELECT @after = COUNT(*) FROM address
WHERE locationReferenceId IS NOT NULL

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' addresses have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' addresses out of ' + CONVERT(NVARCHAR(100), @before) + ' addresses have been migrated'

-- All non-australian addresses

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId <= 11
AND countrySubdivisionId <> 1

SELECT @after = COUNT(*) FROM Address
WHERE countrySubdivisionId <= 11
AND countrySubdivisionId <> 1
AND locationReferenceId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' non-australian addresses have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' addresses out of ' + CONVERT(NVARCHAR(100), @before) + ' non-australian addresses have been migrated'

-- All australian addresses, countrySubdivisionId = 1, suburb, postcode, localityId IS NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId = 1
AND suburb IS NULL
AND postcode IS NULL
AND localityId IS NULL

SELECT @after = COUNT(*) FROM Address
WHERE countrySubdivisionId = 1
AND suburb IS NULL
AND postcode IS NULL
AND localityId IS NULL
AND locationReferenceId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' australian addresses with CS=1, suburb=NULL, postcode=NULL have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' addresses out of ' + CONVERT(NVARCHAR(100), @before) + ' australian addresses with CS=1, suburb=NULL, postcode=NULL have been migrated'

-- All australian addresses, countrySubdivisionId = 1, suburb IS NULL, postcode localityId IS NOT NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId = 1
AND suburb IS NULL
AND postcode IS NOT NULL
AND localityId IS NOT NULL

SELECT @after = COUNT(*) FROM Address
WHERE countrySubdivisionId = 1
AND suburb IS NULL
AND postcode IS NOT NULL
AND localityId IS NOT NULL
AND locationReferenceId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' australian addresses with CS=1, suburb=NULL, postcode=NULL have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' addresses out of ' + CONVERT(NVARCHAR(100), @before) + ' australian addresses with CS=1, suburb=NULL, postcode=NULL have been migrated'

-- All australian addresses, countrySubdivisionId <> 1, unstructuredLocation IS NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NULL

SELECT @after = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NULL
AND locationReferenceId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' australian addresses with UL=NULL have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' addresses out of ' + CONVERT(NVARCHAR(100), @before) + ' australian addresses with UL=NULL have been migrated'

-- All australian addresses, countrySubdivisionId <> 1, unstructuredLocation IS NOT NULL, suburb, postcode, localityId IS NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NOT NULL
AND suburb IS NULL
AND postcode IS NULL
AND localityId IS NULL

SELECT @after = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NOT NULL
AND suburb IS NULL
AND postcode IS NULL
AND localityId IS NULL
AND locationReferenceId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' australian addresses with UL NOT NULL, suburb=NULL, postcode=NULL have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' addresses out of ' + CONVERT(NVARCHAR(100), @before) + ' australian addresses with UL NOT NULL, suburb=NULL, postcode=NULL have been migrated'

-- All australian addresses, countrySubdivisionId <> 1, unstructuredLocation IS NOT NULL, suburb, postcode, localityId NOT NULL

SELECT @before = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NOT NULL
AND suburb IS NULL
AND postcode IS NOT NULL
AND localityId IS NOT NULL

SELECT @after = COUNT(*) FROM Address
WHERE countrySubdivisionId > 11
AND unstructuredLocation IS NOT NULL
AND suburb IS NULL
AND postcode IS NOT NULL
AND localityId IS NOT NULL
AND locationReferenceId IS NOT NULL

PRINT ''
SELECT @tally = @tally + @after

IF (@before = @after)
	PRINT 'All ' + CONVERT(NVARCHAR(20), @before) + ' australian addresses with UL NOT NULL, suburb=NULL, postcode NOT NULL have been migrated'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20), @after) + ' addresses out of ' + CONVERT(NVARCHAR(100), @before) + ' australian addresses with UL NOT NULL, suburb=NULL, postcode NOT NULL have been migrated'

-- Check the tally

PRINT ''
IF (@total = @tally)
	PRINT 'All addresses have been accounted for'
ELSE
	PRINT 'Only ' + CONVERT(NVARCHAR(20),@tally) + ' addresses out of ' + CONVERT(NVARCHAR(20), @total) + ', (' + CONVERT(NVARCHAR(20), @total - @tally) + ' missing) have been accounted for'
