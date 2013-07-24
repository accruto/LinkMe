UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name = 'Scouts'

UPDATE
	dbo.OfferCategory
SET
	enabled = 0
WHERE
	name = 'Volunteering'