SELECT
	*
FROM
	dbo.OfferProvider
WHERE
	name = 'CIPSA'

UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name = 'CIPSA'