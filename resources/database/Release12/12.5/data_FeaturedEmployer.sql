UPDATE
	dbo.FeaturedEmployer
SET
	enabled = 0
WHERE
	name = 'IPA'

INSERT
	dbo.FeaturedEmployer (id, name, logoUrl, logoOrder, enabled)
VALUES
	('{F6DFABAB-1949-4d2c-AB9F-E2F53FB766BA}', 'htr', '~/content/images/homepage/jobs-by-employers/htr.png', 4, 1)
