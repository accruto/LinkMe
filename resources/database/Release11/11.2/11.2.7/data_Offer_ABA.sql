SELECT
	*
FROM
	dbo.OfferProvider
WHERE
	name = 'Australian Business Academy'

UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name = 'Australian Business Academy'