UPDATE
	dbo.Offering
SET
	enabled = 0
FROM
	dbo.Offering AS o
INNER JOIN
	dbo.OfferProvider AS p ON p.id = o.providerId
WHERE
	p.name = 'Head Start'

