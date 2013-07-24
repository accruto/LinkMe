UPDATE
	dbo.FeaturedEmployer
SET
	enabled = 0
WHERE
	name = 'Vita Group'

INSERT
	dbo.FeaturedEmployer (id, name, logoUrl, logoOrder, enabled)
VALUES
	('{F3702930-1F1A-4f81-8C50-12DBBCC92666}', 'Green Light', '~/content/images/homepage/jobs-by-employers/GreenLight.png', 5, 1)
