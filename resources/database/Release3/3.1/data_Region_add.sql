-- The last id currently in the database is 2806, so start from there.

-- GeographicalArea

INSERT		dbo.GeographicalArea
SELECT		id = 2808, displayName = 'Canberra'
UNION SELECT	id = 2809, displayName = 'Sydney'
UNION SELECT	id = 2810, displayName = 'Darwin'
UNION SELECT	id = 2811, displayName = 'Brisbane'
UNION SELECT	id = 2812, displayName = 'Adelaide'
UNION SELECT	id = 2813, displayName = 'Hobart'
UNION SELECT	id = 2814, displayName = 'Melbourne'
UNION SELECT	id = 2815, displayName = 'Perth'

-- Region

INSERT		dbo.Region
SELECT		id = 2808, urlName = 'canberra'
UNION SELECT	id = 2809, urlName = 'sydney'
UNION SELECT	id = 2810, urlName = 'darwin'
UNION SELECT	id = 2811, urlName = 'brisbane'
UNION SELECT	id = 2812, urlName = 'adelaide'
UNION SELECT	id = 2813, urlName = 'hobart'
UNION SELECT	id = 2814, urlName = 'melbourne'
UNION SELECT	id = 2815, urlName = 'perth'

-- LocalityRegion

INSERT dbo.LocalityRegion
SELECT localityId = l.id, regionId = 2808 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '2600'
UNION SELECT localityId = l.id, regionId = 2809 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '2000'
UNION SELECT localityId = l.id, regionId = 2810 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '0800'
UNION SELECT localityId = l.id, regionId = 2811 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '4000'
UNION SELECT localityId = l.id, regionId = 2812 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '5000'
UNION SELECT localityId = l.id, regionId = 2813 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '7000'
UNION SELECT localityId = l.id, regionId = 2814 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '3000'
UNION SELECT localityId = l.id, regionId = 2815 FROM dbo.Locality AS l INNER JOIN PostalCode AS pc ON pc.localityId = l.id WHERE pc.postcode = '6000'

