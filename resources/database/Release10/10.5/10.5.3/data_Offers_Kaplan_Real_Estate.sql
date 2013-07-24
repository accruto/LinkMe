-- Disable the provider.

UPDATE
	OfferProvider
SET
	enabled = 0
WHERE
	name = 'Kaplan Real Estate'
