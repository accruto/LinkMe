-- Case 744: set the coordinates for 2134 to be the same as for 1805

DECLARE @lat FLOAT
DECLARE @lon FLOAT

SELECT @lat = lat, @lon = lon
FROM linkme_owner.locality
WHERE postCode = '1805'

UPDATE linkme_owner.locality
SET lat = @lat, lon = @lon
WHERE postCode = '2134'

GO

-- Case 1723: set the coordinates for 4003 to be the same as for 4000.

DECLARE @lat FLOAT
DECLARE @lon FLOAT

SELECT @lat = lat, @lon = lon
FROM linkme_owner.locality
WHERE postCode = '4000'

UPDATE linkme_owner.locality
SET lat = @lat, lon = @lon
WHERE postCode = '4003'

GO

-- Correct 3-character postcodes

UPDATE linkme_owner.locality
SET postCode = '0' + postCode
WHERE postCode LIKE '8__'

GO

-- Delete localities that we don't really have the location of (postcodes 2004 and 2890)

DELETE linkme_owner.locality
WHERE lat = 0 OR lon = 0

GO
