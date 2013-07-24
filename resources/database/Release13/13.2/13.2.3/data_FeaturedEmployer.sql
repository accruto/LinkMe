UPDATE
	dbo.FeaturedEmployer
SET
	enabled = 0
WHERE
	name = 'Vodafone'


INSERT
	dbo.FeaturedEmployer (id, name, logoUrl, logoOrder, enabled)
VALUES
	('{624027D0-C48B-4366-BDEA-95B8A113B29A}', 'The Network', '~/content/images/homepage/jobs-by-employers/the-network.png', 6, 1)
