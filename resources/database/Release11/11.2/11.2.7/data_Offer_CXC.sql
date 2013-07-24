SELECT
	*
FROM
	dbo.OfferProvider
WHERE
	name = 'CXC Global'

UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name = 'CXC Global'