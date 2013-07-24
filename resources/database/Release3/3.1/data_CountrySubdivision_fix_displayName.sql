-- CountrySubdivision

UPDATE
	dbo.CountrySubdivision
SET
	shortDisplayName = map.shortDisplayName
FROM
	dbo.CountrySubdivision AS CS
INNER JOIN
	dbo.GeographicalArea AS GA ON CS.id = GA.id
INNER JOIN (
	SELECT shortDisplayName = 'ACT', displayName = 'ACT'
	UNION SELECT shortDisplayName = 'NSW', displayName = 'NSW'
	UNION SELECT shortDisplayName = 'NT', displayName = 'NT'
	UNION SELECT shortDisplayName = 'QLD', displayName = 'QLD'
	UNION SELECT shortDisplayName = 'SA', displayName = 'SA'
	UNION SELECT shortDisplayName = 'TAS', displayName = 'TAS'
	UNION SELECT shortDisplayName = 'VIC', displayName = 'VIC'
	UNION SELECT shortDisplayName = 'WA', displayName = 'WA'
	UNION SELECT shortDisplayName = 'ACT', displayName = 'Australian Capital Territory'
	UNION SELECT shortDisplayName = 'NSW', displayName = 'New South Wales'
	UNION SELECT shortDisplayName = 'NT', displayName = 'Northern Territory'
	UNION SELECT shortDisplayName = 'QLD', displayName = 'Queensland'
	UNION SELECT shortDisplayName = 'SA', displayName = 'South Australia'
	UNION SELECT shortDisplayName = 'TAS', displayName = 'Tasmania'
	UNION SELECT shortDisplayName = 'VIC', displayName = 'Victoria'
	UNION SELECT shortDisplayName = 'WA', displayName = 'Western Australia'
	) AS map ON map.displayName = GA.displayName

-- GeographicalArea

UPDATE
	dbo.GeographicalArea
SET
	displayName = map.newDisplayName
FROM
	dbo.GeographicalArea AS GA
INNER JOIN (
	SELECT newDisplayName = 'Australian Capital Territory', displayName = 'ACT'
	UNION SELECT newDisplayName = 'New South Wales', displayName = 'NSW'
	UNION SELECT newDisplayName = 'Northern Territory', displayName = 'NT'
	UNION SELECT newDisplayName = 'Queensland', displayName = 'QLD'
	UNION SELECT newDisplayName = 'South Australia', displayName = 'SA'
	UNION SELECT newDisplayName = 'Tasmania', displayName = 'TAS'
	UNION SELECT newDisplayName = 'Victoria', displayName = 'VIC'
	UNION SELECT newDisplayName = 'Western Australia', displayName = 'WA'
	) AS map ON map.displayName = GA.displayName

-- CountrySubdivsionAlias

DELETE
	dbo.CountrySubdivisionAlias
WHERE
	displayName = 'Australian Capital Territory'
	OR displayName = 'New South Wales'
	OR displayName = 'Northern Territory'
	OR displayName = 'Queensland'
	OR displayName = 'South Australia'
	OR displayName = 'Tasmania'
	OR displayName = 'Victoria'
	OR displayName = 'Western Australia'

