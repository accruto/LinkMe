UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name = 'Milestone Learning' OR name = 'Milestone Learning Employers'
