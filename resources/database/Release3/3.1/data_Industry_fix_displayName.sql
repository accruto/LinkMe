UPDATE
	dbo.Industry
SET
	displayName = map.displayName,
	shortDisplayName = map.shortDisplayName
FROM
	dbo.Industry AS I
INNER JOIN (
	SELECT displayName = 'Advertising, Media & Entertainment', shortDisplayName = 'Advertising, Media & Entertainment', oldDisplayName = 'Advert./Media/Entertain.'
	UNION SELECT displayName = 'Banking & Financial Services', shortDisplayName = 'Banking & Financial Services', oldDisplayName = 'Banking & Fin. Services'
	UNION SELECT displayName = 'Consulting & Corporate Strategy', shortDisplayName = 'Consulting & Corporate Strategy', oldDisplayName = 'Consulting & Corp. Strategy'
	UNION SELECT displayName = 'Manufacturing & Operations', shortDisplayName = 'Manufacturing & Operations', oldDisplayName = 'Manufacturing/Operations'
	UNION SELECT displayName = 'Call Centre & Customer Service', shortDisplayName = 'Call Centre & Customer Service', oldDisplayName = 'Call Centre/Cust. Service'
	UNION SELECT displayName = 'Government & Defence', shortDisplayName = 'Government & Defence', oldDisplayName = 'Government/Defence'
	UNION SELECT displayName = 'IT & Telecommunications', shortDisplayName = 'IT & Telecommunications', oldDisplayName = 'I.T. & T'
	UNION SELECT displayName = 'Retail & Consumer Products', shortDisplayName = 'Retail & Consumer Products', oldDisplayName = 'Retail & Consumer Prods.'
	) AS map ON map.oldDisplayName = I.displayName

GO
