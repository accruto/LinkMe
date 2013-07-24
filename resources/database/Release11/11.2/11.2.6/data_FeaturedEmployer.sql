UPDATE
	dbo.FeaturedEmployer
SET
	enabled = 0
WHERE
	name = 'IAG'

DECLARE @id UNIQUEIDENTIFIER
SET @id = '{FF1395B3-ACC2-4d3a-988A-B9318CF37391}'

IF EXISTS (SELECT * FROM dbo.FeaturedEmployer WHERE id = @id)
	UPDATE
		dbo.FeaturedEmployer
	SET
		name = 'Talent2',
		logoUrl = '~/content/images/homepage/jobs-by-employers/t2.png',
		logoOrder = 2,
		enabled = 1
	WHERE
		id = @id
ELSE
	INSERT
		dbo.FeaturedEmployer (id, name, logoUrl, logoOrder, enabled)
	VALUES
		(@id, 'Talent2', '~/content/images/homepage/jobs-by-employers/t2.png', 2, 1)

UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 3
WHERE
	name = 'Beilby'

UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 4
WHERE
	name = 'IPA'


UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 5
WHERE
	name = 'Vita Group'


UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 6
WHERE
	name = 'Michael Page'

SELECT
	*
FROM
	dbo.FeaturedEmployer