DELETE
	dbo.OfferingCriteria
FROM
	dbo.OfferingCriteria AS c
INNER JOIN
	dbo.OfferingCriteriaSet AS s ON s.id = c.id
INNER JOIN
	dbo.Offering AS o ON o.id = s.offeringId
INNER JOIN
	dbo.OfferProvider AS p ON p.id = o.providerId
WHERE
	p.name = 'CIPSA'
	AND c.name = 'Country' AND c.value = 8

DELETE
	dbo.OfferingCriteriaSet
FROM
	dbo.OfferingCriteriaSet AS s
INNER JOIN
	dbo.Offering AS o ON o.id = s.offeringId
INNER JOIN
	dbo.OfferProvider AS p ON p.id = o.providerId
WHERE
	p.name = 'CIPSA'
	AND NOT EXISTS (SELECT * FROM dbo.OfferingCriteria WHERE id = s.id)


