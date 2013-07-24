UPDATE
	dbo.FeaturedEmployer
SET
	enabled = 0
WHERE
	name = 'Michael Page'

INSERT
	dbo.FeaturedEmployer (id, name, logoUrl, logoOrder, enabled)
VALUES
	('{DC147F53-C7F1-4e91-8291-7E4F74C16E54}', 'Vodafone', '~/content/images/homepage/jobs-by-employers/vodafone.png', 6, 1)
