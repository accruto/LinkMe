-- Update all the UWS categories to show (NSW)

UPDATE
	dbo.OfferCategory
SET
	name = name + ' (NSW)'
WHERE
	id IN
	(
		SELECT DISTINCT
			c.id
		FROM
			dbo.OfferProvider AS p
		INNER JOIN
			dbo.Offering AS o ON o.providerId = p.id
		INNER JOIN
			dbo.OfferCategoryOffering AS co ON co.offeringId = o.id
		INNER JOIN
			dbo.OfferCategory AS c ON c.id = co.categoryId
		WHERE
			p.id = '99B09DCB-A946-4638-B9A9-A5F561232672'
	)
	AND name NOT LIKE '% (NSW)'

