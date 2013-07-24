-- Disable all grad courses for UWS

UPDATE
	dbo.Offering
SET
	enabled = 0
WHERE
	providerId = '99B09DCB-A946-4638-B9A9-A5F561232672'
	AND name LIKE 'Grad%'

-- Disable the categories

UPDATE
	dbo.OfferCategory
SET
	enabled = 0
WHERE
	name LIKE 'Grad%'

