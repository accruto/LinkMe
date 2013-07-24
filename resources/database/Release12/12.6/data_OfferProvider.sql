UPDATE
	dbo.OfferProvider
SET
	enabled = 0
WHERE
	name IN ('IT Futures', 'AHRI Further Education & Online Courses', 'AHRI Affiliate Member', 'AHRI Professional Member', 'AHRI Organisational Member', 'AHRI Student Member')
	