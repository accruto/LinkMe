-- Add JobAdArea rows based on JobAd postcodes. All the current data is Australian postcodes.

INSERT INTO dbo.JobAdArea(jobAdId, areaId)
SELECT DISTINCT ja.[id], l.[id]
FROM dbo.JobAd ja
INNER JOIN dbo.PostalCode p
ON ja.postcode = p.postcode
INNER JOIN dbo.Locality l
ON p.localityId = l.[id]
INNER JOIN dbo.LocalityCountrySubdivision lcs
ON l.[id] = lcs.localityId
INNER JOIN dbo.CountrySubdivision cs
ON lcs.countrySubdivisionId = cs.[id]
INNER JOIN dbo.Country c
ON cs.countryId = c.[id]
WHERE c.displayName = 'Australia'

GO

-- Some job ads have an invalid or missing postcode (somehow), which won't work with the new DB.
-- Set their Locality to 46 (GAPUWIYAK, NT, 0880), so that nobody ever finds them.
-- Yes, it's a major hack, but better than throwing exceptions on the search results page.

INSERT INTO dbo.JobAdArea(jobAdId, areaId)
SELECT ja.[id], 46
FROM JobAd ja
WHERE NOT EXISTS
(
	SELECT *
	FROM JobAdArea jaa
	WHERE jaa.jobAdId = ja.[id]
)

GO

