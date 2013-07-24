-- CountrySubdivsion

UPDATE
	dbo.CountrySubdivision
SET
	urlName = map.urlName
FROM
	dbo.CountrySubdivision AS CS
INNER JOIN
	dbo.GeographicalArea AS GA ON CS.id = GA.id
INNER JOIN (
	SELECT urlName = 'australian-capital-territory', displayName = 'ACT'
	UNION SELECT urlName = 'new-south-wales', displayName = 'NSW'
	UNION SELECT urlName = 'northern-territory', displayName = 'NT'
	UNION SELECT urlName = 'queensland', displayName = 'QLD'
	UNION SELECT urlName = 'south-australia', displayName = 'SA'
	UNION SELECT urlName = 'tasmania', displayName = 'TAS'
	UNION SELECT urlName = 'victoria', displayName = 'VIC'
	UNION SELECT urlName = 'western-australia', displayName = 'WA'
	UNION SELECT urlName = 'australian-capital-territory', displayName = 'Australian Capital Territory'
	UNION SELECT urlName = 'new-south-wales', displayName = 'New South Wales'
	UNION SELECT urlName = 'northern-territory', displayName = 'Northern Territory'
	UNION SELECT urlName = 'queensland', displayName = 'Queensland'
	UNION SELECT urlName = 'south-australia', displayName = 'South Australia'
	UNION SELECT urlName = 'tasmania', displayName = 'Tasmania'
	UNION SELECT urlName = 'victoria', displayName = 'Victoria'
	UNION SELECT urlName = 'western-australia', displayName = 'Western Australia'
	) AS map ON map.displayName = GA.displayName

