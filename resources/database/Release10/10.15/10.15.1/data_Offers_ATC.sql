UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name = 'Australasian Talent Conference'

UPDATE
	dbo.OfferCategory
SET
	enabled = 0
WHERE
	name = 'Conferences & Events (Paid)'